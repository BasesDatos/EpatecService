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
    [RoutePrefix("pedidos")]
    public class PedidosController : ApiController
    {

        [Route("register/")]
        [HttpPost]
        public IHttpActionResult register(Pedido pPedido) {

            Int64 idPedido = 0;
            using (SqlConnection connection = DataBase.connect()) {

                SqlCommand command = new SqlCommand("dbo.registrarPedido", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@idCliente", SqlDbType.VarChar).Value = pPedido._idCliente;
                command.Parameters.AddWithValue("@hora", SqlDbType.Time).Value = pPedido._horaPedido;
                command.Parameters.AddWithValue("@fecha", SqlDbType.Date).Value = pPedido._fechaPedido;
                command.Parameters.AddWithValue("@total", SqlDbType.Money).Value = pPedido._total;

                try {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    idPedido = reader.GetInt64(0);
                }
                catch(SqlException ex) { return Json(new Resultado("Error al registrar el pedido")); }
                finally { connection.Close(); }

                
                if (idPedido != -1)
                {
                    int result = this.addProducts(pPedido._productos, idPedido);
                    if (result == 1) { return Json(new Resultado("Pedido registrado")); }
                    else { return Json(new Resultado("Error al registrar informacion del pedido")); }

                }
                else { return Json(new Resultado("No existe el usuario que realiza el pepdio")); }
                
            }
        }


        private int addProducts(List<Producto> pProducts, Int64 pIdPedido) {

            using (SqlConnection connection = DataBase.connect())
            {
                
                try {
                    

                    for (int i = 0; i < pProducts.Count; i++) {
                        connection.Open();
                        SqlCommand command = new SqlCommand("dbo.productosPedido", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@idPedido", SqlDbType.BigInt).Value = pIdPedido;
                        command.Parameters.AddWithValue("@idProducto", SqlDbType.BigInt).Value = pProducts.ElementAt(i)._id;
                        command.Parameters.AddWithValue("@cantidad", SqlDbType.Int).Value = pProducts.ElementAt(i)._cantidadDisponible;

                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    return 1;
                }
                catch(SqlException ex) { return -1; }
            }
        }





    }
}
