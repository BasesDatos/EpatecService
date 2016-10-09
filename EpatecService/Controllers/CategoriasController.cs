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
    [RoutePrefix("categorias")]
    public class CategoriasController : ApiController
    {

        [Route("all/")]
        [HttpGet]
        public IHttpActionResult all()
        {
            List<Categoria> categorias = new List<Categoria>();

            using (SqlConnection connection = DataBase.connect())
            {
                SqlCommand command = new SqlCommand("dbo.listarCategorias", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Categoria cat = new Categoria();
                        cat._id = reader.GetInt64(0);
                        cat._descripcion = reader.GetString(1);
                        cat._nombre = reader.GetString(2);
                        cat._estado = reader.GetBoolean(3);
                        categorias.Add(cat);
                    }

                    return Json(categorias);
                }
                catch(SqlException ex) { return Json(new Resultado("Error de conexión con la base de datos")); }
                finally { connection.Close(); }
            }
        }
    }
}
