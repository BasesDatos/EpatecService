using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EpatecService.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http.Cors;
//http://epatecservice.azurewebsites.net

namespace EpatecService.Controllers
{
   // [EnableCors("*", "*", "*")]
    [RoutePrefix("clientes")]
    public class ClientesController : ApiController
    {

        /// <summary>
        /// Obtiene la información de un cliente de acuerdo a su cédula
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("client/{id:int}")]
        [HttpGet]
        public IHttpActionResult client(int id)
        {
            Cliente cliente = null;

            using (SqlConnection connection = DataBase.connect())
            {
                string sqlConsult = "Select * from CLIENTE where Cedula = '{0}'";
                SqlCommand command = new SqlCommand(string.Format(sqlConsult, id), connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        cliente = new Cliente();
                        cliente._cedula = reader.GetString(0);
                        cliente._nombre = reader.GetString(1);
                        cliente._primerApellido = reader.GetString(2);
                        cliente._segundoApellido = reader.GetString(3);
                        cliente._usuario = reader.GetString(4);
                        cliente._contrasena = reader.GetString(5);
                        cliente._fechaNacimiento = reader.GetDateTime(6);
                        cliente._telefono = reader.GetString(7);
                        cliente._prioridad = reader.GetString(8);
                        cliente._pais = reader.GetString(9);
                        cliente._provincia = reader.GetString(10);
                        cliente._codigoPostal = reader.GetInt32(11);
                        cliente._direccion = reader.GetString(12);
                        cliente._calle = reader.GetString(13);
                        cliente._numeroCasa = reader.GetInt32(14);
                    }

                    if (cliente == null) { return Ok(new Resultado("El cliente no existe")); }
                    else return Json(cliente);
                }
                catch (SqlException ex) { return Ok(new Resultado("Error de conexión con la base de datos")); }
                finally { connection.Close(); }
            }
        }



        /// <summary>
        /// Autentica un usuario registrado
        /// Llama al procedimiento almacenado "iniciarSesionCliente" el cual recibe el el nombre de usuario y la contraseña 
        /// y realiza todas las validaciones, en caso de ser correctan la información de login, se retorna toda la información
        /// del cliente
        /// </summary>
        /// <param name="pCliente"></param>
        /// <returns></returns>
        [Route("login/")]
        [HttpPost]
        public IHttpActionResult login(Cliente pCliente) {

            Cliente cliente = null;

            using (SqlConnection connection = DataBase.connect())
            {
                //string validateLogin = "Select * from CLIENTE where Usuario = '{0}' and Contraseña = '{1}'";
                SqlCommand command = new SqlCommand("dbo.iniciarSesionCliente", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Usuario", SqlDbType.VarChar).Value = pCliente._usuario;
                command.Parameters.AddWithValue("@Contraseña", SqlDbType.VarChar).Value = pCliente._contrasena;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    int x = reader.FieldCount;
                    reader.Read();

                    if (x > 1)
                    {
                        cliente = new Cliente();
                        cliente._cedula = reader.GetString(0);
                        cliente._nombre = reader.GetString(1);
                        cliente._primerApellido = reader.GetString(2);
                        cliente._segundoApellido = reader.GetString(3);
                        cliente._usuario = reader.GetString(4);
                        cliente._contrasena = reader.GetString(5);
                        cliente._fechaNacimiento = reader.GetDateTime(6);
                        cliente._telefono = reader.GetString(7);
                        cliente._prioridad = reader.GetString(8);
                        cliente._pais = reader.GetString(9);
                        cliente._provincia = reader.GetString(10);
                        cliente._codigoPostal = reader.GetInt32(11);
                        cliente._direccion = reader.GetString(12);
                        cliente._calle = reader.GetString(13);
                        cliente._numeroCasa = reader.GetInt32(14);

                        return Ok(cliente);
                    }
                    else { return Ok(new Resultado(reader.GetString(0))); }

                } 
                catch (SqlException ex) { return Ok(new Resultado("Error de conexión con la base de datos")); }
                finally { connection.Close(); }
            }         
        }



        /// <summary>
        /// Reactiva la cuenta de un usuario, cuando esta fue borrada con anterioridad
        /// </summary>
        /// <param name="pCliente"></param>
        /// <returns></returns>
        [Route("reactive/")]
        [HttpPost]
        public IHttpActionResult reactive(Cliente pCliente)
        {
            using (SqlConnection connection = DataBase.connect())
            {
                SqlCommand command = new SqlCommand("dbo.activarCliente", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Usuario", SqlDbType.VarChar).Value = pCliente._usuario;
                command.Parameters.AddWithValue("@Contraseña", SqlDbType.VarChar).Value = pCliente._contrasena;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    string resultado = reader.GetString(0);
                    return Ok(new Resultado(resultado));
                }
                catch(SqlException ex) { return Ok(new Resultado("Error de conexión con la base de datos")); }
                finally { connection.Close(); }
            }
        }



        /// <summary>
        /// Registra un nuevo usuario en la base de datos
        /// </summary>
        /// <param name="pCliente"></param>
        /// <returns>Json que indica el resultado</returns>
        [Route("register/")]
        [HttpPost]
        public IHttpActionResult register(Cliente pCliente)
        {
            string resultado = "";
            using(SqlConnection connection = DataBase.connect())
            {
                SqlCommand command = new SqlCommand("dbo.registrarCliente", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Cedula", SqlDbType.VarChar).Value = pCliente._cedula;
                command.Parameters.AddWithValue("@Nombre", SqlDbType.VarChar).Value = pCliente._nombre;
                command.Parameters.AddWithValue("@Primer_Apellido", SqlDbType.VarChar).Value = pCliente._primerApellido;
                command.Parameters.AddWithValue("@Segundo_Apellido", SqlDbType.VarChar).Value = pCliente._segundoApellido;
                command.Parameters.AddWithValue("@Usuario", SqlDbType.VarChar).Value = pCliente._usuario;
                command.Parameters.AddWithValue("@Contraseña", SqlDbType.VarChar).Value = pCliente._contrasena;
                command.Parameters.AddWithValue("@Fecha_Nacimiento", SqlDbType.Date).Value = pCliente._fechaNacimiento;
                command.Parameters.AddWithValue("@Telefono", SqlDbType.VarChar).Value = pCliente._telefono;
                command.Parameters.AddWithValue("@Pais", SqlDbType.VarChar).Value = pCliente._pais;
                command.Parameters.AddWithValue("@Estado_Provincia", SqlDbType.VarChar).Value = pCliente._provincia;
                command.Parameters.AddWithValue("@Codigo_Postal", SqlDbType.Int).Value = pCliente._codigoPostal;
                command.Parameters.AddWithValue("@Direccion_Det", SqlDbType.VarChar).Value = pCliente._direccion;
                command.Parameters.AddWithValue("@Calle", SqlDbType.VarChar).Value = pCliente._calle;
                command.Parameters.AddWithValue("@Numero_Casa", SqlDbType.Int).Value = pCliente._numeroCasa;


                try {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    resultado = reader.GetString(0);
                    return Json(new Resultado(resultado));
                }
                catch (SqlException e) {
                    return Json(new Resultado("Registro de cliente sin exito"));
                }  
                finally { connection.Close(); }              
            }
        }


        /// <summary>
        /// Elimina un usuario registrado de la base de datos
        /// </summary>
        /// <param name="pCliente"></param>
        /// <returns></returns>
        [Route("delete/")]
        [HttpPost]
        public IHttpActionResult delete(Cliente pCliente)
        {
            using (SqlConnection connection = DataBase.connect())
            {
                SqlCommand command = new SqlCommand("dbo.eliminarCliente", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Usuario", SqlDbType.VarChar).Value = pCliente._usuario;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();
                    if (reader.GetInt32(0) == 1)
                    {
                        return Ok(new Resultado("Usuario eliminado"));
                    }
                    else
                    {
                        return Ok(new Resultado("Usuario no existe"));
                    }
                }

                catch (SqlException ex) { return Ok(new Resultado("Error de conexión con la base de datos")); }
                finally { connection.Close(); }
            }
        }


        [Route("all/")]
        [HttpGet]
        public IHttpActionResult all()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (SqlConnection connection = DataBase.connect())
            {
                SqlCommand command = new SqlCommand("dbo.listarClientes", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Cliente cliente = new Models.Cliente();
                        cliente._primerApellido = reader.GetString(0);
                        cliente._segundoApellido = reader.GetString(1);
                        cliente._nombre = reader.GetString(2);
                        cliente._cedula = reader.GetString(3);
                        cliente._usuario = reader.GetString(4);
                        cliente._contrasena = reader.GetString(5);
                        cliente._fechaNacimiento = reader.GetDateTime(6);
                        cliente._telefono = reader.GetString(7);
                        cliente._pais = reader.GetString(8);
                        cliente._provincia = reader.GetString(9);
                        cliente._codigoPostal = reader.GetInt32(10);
                        cliente._direccion = reader.GetString(11);
                        cliente._calle = reader.GetString(12);
                        cliente._numeroCasa = reader.GetInt32(13);
                        cliente._prioridad = reader.GetString(14);
                        clientes.Add(cliente);
                    }

                    return Json(clientes);
                }
                catch(SqlException ex) { return Json(new Resultado("Error de conexión con la base de datos")); }
                finally { connection.Close(); }
            }
        }



    }
}
