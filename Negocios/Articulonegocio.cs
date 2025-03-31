using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Base_datos;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Collections;

namespace Negocios
{
    public class Articulonegocio
    {
        public List<Articulos> listar()
        {
            List<Articulos> lista = new List<Articulos>();
            Accesodatos datos = new Accesodatos();
            try
            {
                datos.Setearconsulta("SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, c.Id AS IdCategoria, c.Descripcion AS Categoria, m.Id AS IdMarca,m.Descripcion AS Marca, a.ImagenUrl, a.Precio \r\nFROM ARTICULOS a, CATEGORIAS c, MARCAS m \r\nWHERE a.IdCategoria = c.Id AND a.IdMarca = m.Id\r\n");
                datos.Ejecutarlectura();

                while (datos.Lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Cod = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Categoria = new Categorias();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];

                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);

                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.Cerrarconexion();
            }
        }
        public void agregar(Articulos nuevo)
        {
            Accesodatos datos = new Accesodatos();
            try
            {
                datos.Setearconsulta("INSERT INTO ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio) values (@Codigo,@Nombre,@Descripcion,@IdMarca,@IdCategoria,@ImagenUrl,@Precio)");
                datos.Setearparametros("@Codigo", nuevo.Cod);
                datos.Setearparametros("@Nombre", nuevo.Nombre);
                datos.Setearparametros("@Descripcion", nuevo.Descripcion);
                datos.Setearparametros("@IdMarca", nuevo.Marca.Id);
                datos.Setearparametros("@IdCategoria", nuevo.Categoria.Id);
                datos.Setearparametros("@ImagenUrl", nuevo.UrlImagen);
                datos.Setearparametros("@Precio", nuevo.Precio);
                datos.ejecutarAccion();
            }

            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.Cerrarconexion();
            }

        }
        public void modificar(Articulos modificado)
        {
            Accesodatos datos = new Accesodatos();
            try
            {
                datos.Setearconsulta("update ARTICULOS set Codigo=@Codigo,Nombre=@Nombre,Descripcion=@Descripcion,IdMarca=@IdMarca,IdCategoria=@IdCategoria,ImagenUrl=@ImagenUrl,Precio=@Precio where Id=@Id;");
                datos.Setearparametros("@Codigo", modificado.Cod);
                datos.Setearparametros("@Nombre", modificado.Nombre);
                datos.Setearparametros("@Descripcion", modificado.Descripcion);
                datos.Setearparametros("@IdMarca", modificado.Marca.Id);
                datos.Setearparametros("@IdCategoria", modificado.Categoria.Id);
                datos.Setearparametros("@ImagenUrl", modificado.UrlImagen);
                datos.Setearparametros("@Precio", modificado.Precio);
                datos.Setearparametros("@Id", modificado.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.Cerrarconexion();
            }

        }
        public void eliminar(Articulos eliminar)
        {
            Accesodatos datos = new Accesodatos();
            try
            {
                datos.Setearconsulta("delete from ARTICULOS WHERE Id=@Id");
                datos.Setearparametros("@Id", eliminar.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.Cerrarconexion();
            }

        }

        public List<Articulos> Filtro(string campo, string criterio, string filtro)
        {
            List<Articulos> listafiltro = new List<Articulos>();
            Accesodatos datos = new Accesodatos();
            try
            {
                string consulta = "SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, c.Id AS IdCategoria, c.Descripcion AS Categoria, m.Id AS IdMarca,m.Descripcion AS Marca, a.ImagenUrl, a.Precio FROM ARTICULOS a, CATEGORIAS c, MARCAS m WHERE a.IdCategoria = c.Id AND a.IdMarca = m.Id and ";
                if (campo == "Precio")
                {
                    if (criterio == "Mayor a")
                        consulta += "a.Precio > '" + filtro + "'";
                    else if (criterio == "Menor a")
                        consulta += "a.Precio < '" + filtro + "'";
                    else
                        consulta += "a.Precio = '" + filtro + "'";
                }
                else if (campo == "Codigo" || campo == "Nombre")
                {
                    if (criterio == "Comienza con")
                        consulta += campo + " LIKE '" + filtro + "%'";
                    else if (criterio == "Termina con")
                        consulta += campo + " LIKE '%" + filtro + "'";
                    else
                        consulta += campo + " LIKE '%" + filtro + "%'";
                }
                datos.Setearconsulta(consulta);
                datos.Ejecutarlectura();
                while (datos.Lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Cod = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Categoria = new Categorias();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];

                    aux.Precio = (decimal)datos.Lector["Precio"];


                    listafiltro.Add(aux);
                }
                return listafiltro;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.Cerrarconexion();

            }
        }
        public List<Categorias> listaCategorias()
        {
            List<Categorias> categorias = new List<Categorias>();
            Accesodatos datos = new Accesodatos();
            try

            {

                datos.Setearconsulta("select Id,Descripcion from CATEGORIAS");
                datos.Ejecutarlectura();
                while (datos.Lector.Read())
                {
                    Categorias aux = new Categorias();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    categorias.Add(aux);
                }
                return categorias;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.Cerrarconexion();
            }
        }
        public List<Marca> listaMarcas()
        {
            List<Marca> marca = new List<Marca>();
            Accesodatos datos = new Accesodatos();
            try

            {

                datos.Setearconsulta("select Id,Descripcion from Marcas");
                datos.Ejecutarlectura();
                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    marca.Add(aux);
                }
                return marca;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.Cerrarconexion();
            }

        }
    }

}