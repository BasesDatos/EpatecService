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
    [RoutePrefix("empleados")]
    public class EmpleadosController : ApiController
    {

        /// <summary>
        /// Obtiene un listado con los datos de todos los empleados, ejecutando el procedimiento almacenado "listarEmpleados"
        /// </summary>
        /// <returns></returns>
        [Route("all/")]
        [HttpGet]
        public IHttpActionResult all()
        {
            List<Empleado> empleados = new List<Empleado>();

            using (SqlConnection connection = DataBase.connect())
            {
                SqlCommand command = new SqlCommand("dbo.listarEmpleados", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Empleado emp = new Empleado();
                        emp._id = reader.GetInt64(0);
                        emp._primerApellido = reader.GetString(1);
                        emp._segundoApellido = reader.GetString(2);
                        emp._nombre = reader.GetString(3);
                        emp._cedula = reader.GetString(4);
                        emp._fechaNacimiento = reader.GetDateTime(5);
                        emp._telefono = reader.GetString(6);
                        emp._pais = reader.GetString(7);
                        emp._provincia = reader.GetString(8);
                        emp._codigoPostal = reader.GetInt32(9);
                        emp._direccion = reader.GetString(10);
                        emp._calle = reader.GetString(11);
                        emp._numeroCasa = reader.GetInt32(12);
                        emp._estado = reader.GetBoolean(13);
                        empleados.Add(emp);
                    }

                    return Json(empleados);
                }
                catch (SqlException ex) { return Json(new Resultado("Error de conexión con la base de datos")); }
                finally { connection.Close(); }
            }
        }



    }
}
