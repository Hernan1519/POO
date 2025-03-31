using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Base_datos;
using Negocios;
using Dominio;
using System.Xml.Linq;
using System.Runtime.InteropServices.WindowsRuntime;


namespace Finalproyecto
{
    public partial class FrmArticulos : Form
    {
        public FrmArticulos()
        {
            InitializeComponent();
        }
        private List<Articulos> listaArticulos;
        private void Articulos_Load(object sender, EventArgs e)
        {

            validarFiltro();
            cargar();
            cbxCampo.Items.Add("Codigo");
            cbxCampo.Items.Add("Nombre");
            cbxCampo.Items.Add("Precio");

            cbxCriterio.Items.Clear(); 
            cbxCampo.SelectedIndex = -1;
            cbxCriterio.SelectedIndex = -1; 

            
            validarFiltro();

        }

        private void cargarImagen(string imagen)
        {
            try
            {

                pbxImagen.Load(imagen);

            }
            catch (Exception)
            {
                pbxImagen.Load("https://i0.wp.com/stretchingmexico.com/wp-content/uploads/2024/07/placeholder-1.webp?w=1200&quality=80&ssl=1");
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulos seleccionado = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlImagen);
            }
        }
        private void cargar()
        {
            Articulonegocio negocio = new Articulonegocio();
            listaArticulos = negocio.listar();
            dgvArticulos.DataSource = listaArticulos;
            dgvArticulos.Columns["UrlImagen"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "N2";
            cargarImagen(listaArticulos[0].UrlImagen);

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAlta alta = new FrmAlta();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {          
            if (dgvArticulos.CurrentRow == null)
            {
                MessageBox.Show("Debes seleccionar un artículo antes de modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Articulos seleccionado = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
            FrmAlta modificar = new FrmAlta(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            Articulos seleccionado;
            Articulonegocio negocio = new Articulonegocio();
            try
            {
                DialogResult respuesta = MessageBox.Show("Desear Eliminar el articulo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado);
                }
                cargar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }


        }

        private void validarFiltro()
        {
            btnFiltro.Enabled = cbxCampo.SelectedIndex >= 0 && cbxCriterio.SelectedIndex >= 0;
        }
        private void btnFiltro_Click(object sender, EventArgs e)
        {
            Articulonegocio negocio = new Articulonegocio();
            try
            {
                if (cbxCampo.SelectedItem.ToString() == "Precio")
                {
                    string esNumero = tbxFiltroA.Text;
                    if (!soloNumero(esNumero))
                    {
                        MessageBox.Show("El campo seleccionado solo debe tener numero");
                        tbxFiltroA.Text = "";
                        return;
                    }
                }
                string campo = cbxCampo.SelectedItem.ToString();
                string criterio = cbxCriterio.SelectedItem.ToString();
                string filtro = tbxFiltroA.Text;
                dgvArticulos.DataSource = negocio.Filtro(campo, criterio, filtro);

                cbxCampo.Items.Clear();
                cbxCriterio.Items.Clear();
                cbxCampo.SelectedIndex = -1;
                cbxCriterio.SelectedIndex = -1;
                cbxCampo.Items.Add("Codigo");
                cbxCampo.Items.Add("Nombre");
                cbxCampo.Items.Add("Precio");
                cbxCriterio.Text = "";
                tbxFiltroA.Text = "";

                validarFiltro();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }


        }
        private void tbxFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulos> listafiltrada;
            string filtro = tbxFiltro.Text;
            if (filtro.Length >= 3)
            {
                listafiltrada = listaArticulos.FindAll(x => x.Nombre.ToLower().Contains(filtro.ToLower()) || x.Marca.Descripcion.ToLower().Contains(filtro.ToLower()));
            }
            else
            {
                listafiltrada = listaArticulos;
            }
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listafiltrada;
            dgvArticulos.Columns["UrlImagen"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
        }

        private void cbxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            validarFiltro();

            string opcion = cbxCampo.SelectedItem.ToString();
            if (opcion == "Precio")
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Mayor a");
                cbxCriterio.Items.Add("Menor a");
                cbxCriterio.Items.Add("Igual a");
            }
            else
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Comienza con");
                cbxCriterio.Items.Add("Termina con");
                cbxCriterio.Items.Add("Contiene");
            }
        }

        private void cbxCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            validarFiltro();
        }
        private bool soloNumero(string Numero)
        {

            bool esNumeroDecimal = double.TryParse(Numero, out double numeroDecimal);
            return esNumeroDecimal;

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow == null)
            {
                MessageBox.Show("Debes seleccionar un artículo antes de ver detalles.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Articulos seleccionado;
            seleccionado = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
            ArticuloDetalles Ver=new ArticuloDetalles(seleccionado);
            Ver.ShowDialog();
            cargar();
                
            }
        
        }
    }


