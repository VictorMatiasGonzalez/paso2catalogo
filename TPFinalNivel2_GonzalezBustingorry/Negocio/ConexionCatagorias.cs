using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Negocio;
using Dominio;

namespace Negocio
{
    public class ConexionCatagorias
    {
        public List<Categorias> listar()
        {

        List<Categorias> lista = new List<Categorias>();
        AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select id, Descripcion from Categorias");
                datos.ejecutarLectura();
                
                while(datos.Lector.Read())
                {
                    Categorias aux = new Categorias();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

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
                datos.cerrarConexion();
            }
        }
    }
}
