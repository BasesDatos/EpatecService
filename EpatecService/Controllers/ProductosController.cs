using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EpatecService.Models;
using System.Data.SqlClient;
using System.Data;

namespace EpatecService.Controllers
{
    [RoutePrefix("api/productos")]
    public class ProductosController : ApiController
    {

        /// <summary>
        /// Registra un nuevo producto en la base de datos, llamando al procedimiento almacenado "registrar producto"
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns>Estado que indica el resultado de la operación</returns>
        [Route("register/")]
        [HttpPost]
        public IHttpActionResult register(Producto pProducto)
        {
            using (SqlConnection connection = DataBase.connect())
            {
                SqlCommand command = new SqlCommand("dbo.registrarProducto", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@nombre", SqlDbType.VarChar).Value = pProducto._nombre;
                command.Parameters.AddWithValue("@descripcion", SqlDbType.VarChar).Value = pProducto._descripcion;
                command.Parameters.AddWithValue("@exento", SqlDbType.Bit).Value = pProducto._exento;
                command.Parameters.AddWithValue("@cantidad_disp", SqlDbType.Int).Value = pProducto._cantidadDisponible;
                command.Parameters.AddWithValue("@precio", SqlDbType.Money).Value = pProducto._precio;
                command.Parameters.AddWithValue("@id_categoria", SqlDbType.BigInt).Value = pProducto._idCategoria;
                command.Parameters.AddWithValue("@id_proveedor", SqlDbType.BigInt).Value = pProducto._idProveedor;
                command.Parameters.AddWithValue("@imagen", SqlDbType.Image).Value = pProducto._imagen;

             
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    return Json(new Resultado(reader.GetString(0)));
                }
                catch (SqlException ex) { return Ok(new Resultado("Error al registrar el producto")); }
                finally { connection.Close(); }
            }
        }
    }
}
