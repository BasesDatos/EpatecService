using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http;
using EpatecService.Models;
using System.Data.SqlClient;
using System.Data;

namespace EpatecService.Controllers
{
    [RoutePrefix("sucursales")]
    public class SucursalesController : ApiController
    {
        [Route("all/")]
        [HttpGet]
        public IHttpActionResult all()
        {
            List<Sucursal> sucursales = new List<Sucursal>();

            using (SqlConnection connection = DataBase.connect())
            {
                SqlCommand command = new SqlCommand("dbo.listarSucursales", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Sucursal suc = new Sucursal();
                        suc._id = reader.GetInt32(0);
                        suc._nombre = reader.GetString(1);
                        suc._direccion = reader.GetString(2);
                        suc._estado = reader.GetBoolean(3);
                        sucursales.Add(suc);
                    }

                    return Json(sucursales);
                }
                catch (SqlException ex) { return Json(new Resultado("Error de conexión con la base de datos")); }
                finally { connection.Close(); }
            }
        }
    }
}
