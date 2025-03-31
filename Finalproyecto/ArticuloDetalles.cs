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
    public partial class ArticuloDetalles : Form
    {
        private Articulos ver;
        public ArticuloDetalles()
        {
            InitializeComponent();
        }
        public ArticuloDetalles(Articulos ver)
        {
            InitializeComponent();
            this.ver = ver;
        }

        private void ArticuloDetalles_Load(object sender, EventArgs e)
        {         
                tbxCodigo.Text = ver.Cod;
                tbxNombre.Text = ver.Nombre;
                tbxDescripcion.Text = ver.Descripcion;
                tbxCategoria.Text = ver.Categoria.Descripcion;
                tbxMarca.Text = ver.Marca.Descripcion;
                tbxUrl.Text = ver.UrlImagen;
                CargarImagen.Imagen(pbxImagen, tbxUrl.Text);
                tbxPrecio.Text = ver.Precio.ToString("F2");
                
                

            this.ActiveControl = lblNombre;


        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            FrmAlta modificar = new FrmAlta(ver);
            modificar.ShowDialog();
            CargarImagen.Imagen(pbxImagen, tbxUrl.Text);
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Articulonegocio negocio = new Articulonegocio();
            try
            {
                DialogResult respuesta = MessageBox.Show("Desear Eliminar el articulo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    
                    negocio.eliminar(ver);
                }
                CargarImagen.Imagen(pbxImagen, tbxUrl.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void paginaPrincipalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
