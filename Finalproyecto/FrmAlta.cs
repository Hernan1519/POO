using Dominio;
using Negocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Finalproyecto
{
    public partial class FrmAlta : Form
    {
        private Articulos articulo = null;
        public FrmAlta()
        {
            InitializeComponent();
            Text = "Agregar articulo";
        }
        public FrmAlta(Articulos Articulo)
        {
            InitializeComponent();
            this.articulo = Articulo;
            Text = "Modificar articulo";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            Articulonegocio negocio = new Articulonegocio();
            try
            {
                if (articulo == null)
                    articulo = new Articulos();
               
                if (!validarCamposLlenos())
                {
                    MessageBox.Show("Todos los campos deben estar completos antes de cargar los datos.");
                    return;
                }
                if (!validarPrecio(out decimal precio))
                {
                    MessageBox.Show("Ingrese un numero válido (número mayor a 0).");
                    return;
                }


                articulo.Cod = tbxCodigo.Text;
                articulo.Nombre = tbxNombre.Text;
                articulo.Descripcion = tbxDescripcion.Text;              
                articulo.Marca = (Marca)cbxMarca.SelectedItem;
                articulo.Categoria = (Categorias)cbxCategoria.SelectedItem;
                articulo.UrlImagen = tbxUrl.Text;
                cargarImagen(articulo.UrlImagen);
                articulo.Precio = decimal.Parse(tbxPrecio.Text);
               
                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado con exito");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado con exito");
                    
                }
                
                Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmAlta_Load(object sender, EventArgs e)

        {
            Articulonegocio negocio= new Articulonegocio();


            cbxCategoria.DataSource = negocio.listaCategorias();
            cbxCategoria.ValueMember = "Id";
            cbxCategoria.DisplayMember = "Descripcion";


            cbxMarca.DataSource = negocio.listaMarcas();
            cbxMarca.ValueMember = "Id";
            cbxMarca.DisplayMember = "Descripcion";
            


            if (articulo != null)
            {

                tbxCodigo.Text = articulo.Cod;
                tbxNombre.Text = articulo.Nombre;
                tbxDescripcion.Text = articulo.Descripcion;
                cbxCategoria.SelectedValue = articulo.Categoria.Id;
                cbxMarca.SelectedValue = articulo.Marca.Id;              
                tbxUrl.Text = articulo.UrlImagen;
                cargarImagen(articulo.UrlImagen);
                tbxPrecio.Text = articulo.Precio.ToString();
                
            }

        }

        private void tbxUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(tbxUrl.Text);
        }
        private void cargarImagen(string imagen)
        {
            try
            {

                pbxImagen2.Load(imagen);

            }
            catch (Exception)
            {
                pbxImagen2.Load("https://i0.wp.com/stretchingmexico.com/wp-content/uploads/2024/07/placeholder-1.webp?w=1200&quality=80&ssl=1");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private bool validarCamposLlenos()
        {
            return !(string.IsNullOrWhiteSpace(tbxCodigo.Text) ||
                     string.IsNullOrWhiteSpace(tbxNombre.Text) ||
                     string.IsNullOrWhiteSpace(tbxDescripcion.Text) ||
                     string.IsNullOrWhiteSpace(tbxUrl.Text) ||
                     cbxMarca.SelectedItem == null ||
                     cbxCategoria.SelectedItem == null ||
                     string.IsNullOrWhiteSpace(tbxPrecio.Text));
        }
        private bool validarPrecio(out decimal precio)
        {
            return decimal.TryParse(tbxPrecio.Text, out precio) && precio > 0;
        }
    }
    }
