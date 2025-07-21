using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;


namespace Negocio
{//CENTRALIZAR CONEXION
    public class AccesoDatos
    {//objetos para conectarnos
        private SqlConnection conexion;// Atributos
        private SqlCommand comando;   //La segunda opción, con atributos de clase(esta), es útil cuando estás armando algo más estructurado —
        private SqlDataReader lector; //como una clase DAO que realiza varias operaciones (listar, agregar, modificar, eliminar) reutilizando los mismos objetos
        
        public SqlDataReader Lector
        {
            get { return lector; }// si o si hacerlo publico para leerlo desde el exterior
        }

        public AccesoDatos()//constructor// !!!!! podria crear si quisiera un metodo conectarse sin constructor
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=True"); //constructor sobrecargado
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;


        }
        
        
        public void ejecutarLectura()
        {
            comando.Connection = conexion;

            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public void setearParametros(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }
        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();//ejecuta esa sentencia y ejecuta la mofidicacion de la base de datos
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       public void cerrarConexion()

        {
            if (lector != null)
                lector.Close();
                   conexion.Close();
        }        
    }
}
