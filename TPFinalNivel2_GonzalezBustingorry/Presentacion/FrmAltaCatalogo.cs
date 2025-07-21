using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class FrmAltaCatalogo : Form
    {//Event2
        public FrmAltaCatalogo()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btAceptar_Click(object sender, EventArgs e)
        {
            Catalogo cata = new Catalogo();
            ConexionCatalogo conexion = new ConexionCatalogo();
            try
            {
                cata.Codigo = txtCodigoArticulo.Text;
                cata.Nombre = txtNombre.Text;
                cata.Descripcion = txtDescripcion.Text;
                cata.ImagenUrl = txtImagen.Text;
                decimal precio = 0;
                decimal.TryParse(txtPrecio.Text, out precio);
                cata.Precio = precio;
                if (precio == 0 || txtPrecio.Text.Contains(",")
 )
                {
                    MessageBox.Show("Verificá que el precio ingresado sea válido.");
                    return;
                }
                cata.Firmas = (Marcas)cbMarca.SelectedItem;
                cata.Inventario = (Categorias)cbCategoria.SelectedItem;
                conexion.agregarCatalogo(cata);
                MessageBox.Show("Articulo agregado correctamente");
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void FrmAltaCatalogo_Load(object sender, EventArgs e)
        {
            ConexionMarcas conexionMarcas = new ConexionMarcas();
            ConexionCatagorias conexionCategorias = new ConexionCatagorias();
            try
            {
                cbMarca.DataSource = conexionMarcas.listar();
                cbCategoria.DataSource = conexionCategorias.listar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }
        private void cargarImagen(string imagenn)
        {

            try
            {
                pBCatalogo.Load(imagenn);
            }
            catch (Exception ex)
            {

                pBCatalogo.Load("https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png");
            }
        }

    }
}
