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
{//EVENT
    public partial class Form1 : Form
    {
        private List<Catalogo> listaArticulos;//atributo privado para manipular lista libremente(conexion.listar)
        public Form1()
        {
            InitializeComponent();
        }
        
        

        private void Form1_Load(object sender, EventArgs e)//LOAD
        {
            cargar();
        }

        private void dgvCatalogo_SelectionChanged(object sender, EventArgs e)// propiedad de dgv para  cambiar seleccion
        {
           Catalogo seleccionado=(Catalogo) dgvCatalogo.CurrentRow.DataBoundItem;// objeto transformado en articulo del catalogo
            cargarImagen(seleccionado.ImagenUrl);

        }
        private void cargar()
        {
            ConexionCatalogo conexion = new ConexionCatalogo();

            try
            {
                listaArticulos = conexion.listar();// con listaArticulos puedo trabajar mas facil
                dgvCatalogo.DataSource = listaArticulos;//datasource> mostrmae este dato/dgv lo ordena automaticamente en columnas
                dgvCatalogo.Columns["Id"].Visible = false;
                dgvCatalogo.Columns["ImagenUrl"].Visible = false;
                cargarImagen(listaArticulos[0].ImagenUrl);//carga imagen en el picture box
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void cargarImagen(string imagen)
        {
            
            try
            {
                pbCatalogo.Load(imagen);
            }
            catch (Exception ex)
            {

                pbCatalogo.Load("https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAltaCatalogo alta =  new FrmAltaCatalogo();
            alta.ShowDialog();
            cargar();
        }

    }
}
