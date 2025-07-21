using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.SqlClient;// conexion sql


namespace Negocio
{
    public class ConexionCatalogo
    {
        public List<Catalogo> listar()
     {
         List<Catalogo> lista = new List<Catalogo>(); 
         SqlConnection conexion = new SqlConnection();//OBJETOS configurados par aluego hacer la lectura solo en este metodo!
         SqlCommand comando = new SqlCommand();
         SqlDataReader lector;

        try 
	  {	        
		 conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=True";  
         comando.CommandType = System.Data.CommandType.Text;
         comando.CommandText= "select A.Id,Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, C.Descripcion Inventario,M.Descripcion Firmas from ARTICULOS A, CATEGORIAS C, MARCAS M where C.Id= A.IdCategoria and M.Id = A.IdMarca";
         comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while(lector.Read())
                {
                    Catalogo aux = new Catalogo();
                    aux.Id = (int)lector["Id"];
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.ImagenUrl = (string)lector["ImagenUrl"];
                    aux.Inventario = new Categorias();
                    aux.Inventario.Descripcion = (string)lector["Inventario"];
                    aux.Firmas = new Marcas();
                    aux.Firmas.Descripcion = (string)lector["Firmas"];
                    aux.Precio = Convert.ToDecimal(lector["Precio"]);

                    lista.Add(aux);
                }
                conexion.Close();
                return lista;
          }
	      catch (Exception ex)
	      {

		  throw;
	      }

         }

        public void agregarCatalogo(Catalogo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre,Descripcion,ImagenUrl,Precio, IdMarca,IdCategoria ) values(@Codigo, @Nombre, @Descripcion, @ImagenUrl, @Precio, @IdMarca, @IdCategoria)");
                datos.setearParametros("@Codigo", nuevo.Codigo);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Descripcion", nuevo.Descripcion);
                datos.setearParametros("@ImagenUrl", nuevo.ImagenUrl);
                datos.setearParametros("@Precio", nuevo.Precio);
                datos.setearParametros("@IdMarca", nuevo.Firmas.Id);
                datos.setearParametros("@IdCategoria", nuevo.Inventario.Id);
                
               


                datos.ejecutarAccion();
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
        public void modificarCatalogo(Catalogo modificar)
        {

        }
    }
}
