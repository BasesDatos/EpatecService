using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EpatecService.Models;
using System.Web.Http;
using EpatecService.Models;
using System.Data.SqlClient;
using System.Data;

namespace EpatecService.Controllers
{
    [RoutePrefix("proveedores")]
    public class ProveedoresController : ApiController
    {

        /// <summary>
        /// Obtiene un listado de la información de todos los proveedores en la base
        /// de datos, ejecutando el procedimiento almacenado "listarProveedores"
        /// </summary>
        /// <returns></returns>
        [Route("all/")]
        [HttpGet]
        public IHttpActionResult all()
        {
            List<Proveedor> proveedores = new List<Proveedor>();

            using (SqlConnection connection = DataBase.connect())
            {
                SqlCommand command = new SqlCommand("dbo.listarProveedores", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Proveedor prov = new Proveedor();
                        prov._id = reader.GetInt64(0);
                        prov._primerApellido = reader.GetString(1);
                        prov._segundoApellido = reader.GetString(2);
                        prov._nombre = reader.GetString(3);
                        prov._fechaNacimiento = reader.GetDateTime(4);
                        prov._telefono = reader.GetString(5);
                        prov._pais = reader.GetString(6);
                        prov._provincia = reader.GetString(7);
                        prov._codigoPostal = reader.GetInt32(8);
                        prov._dirreccion = reader.GetString(9);
                        prov._calle = reader.GetString(10);
                        prov._numeroCasa = reader.GetInt32(11);
                        prov._estado = reader.GetBoolean(12);
                        proveedores.Add(prov);
                    }

                    return Json(proveedores);
                }
                catch(SqlException ex) { return Json(new Resultado("Error de conexión con la base de datos")); }
                finally { connection.Close(); }
            }
        }



    }
}
