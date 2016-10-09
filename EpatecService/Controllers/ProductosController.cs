using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EpatecService.Models;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EpatecService.Controllers
{
    [RoutePrefix("products")]
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
                //command.Parameters.AddWithValue("@imagen", SqlDbType.Image).Value = pProducto._imagen;
                command.Parameters.AddWithValue("@id_sucursal", SqlDbType.Int).Value = pProducto._idSucursal;


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


        /// <summary>
        /// Borra un producto de la base de datos, ejecutando el procedimiento almancenado
        /// "eliminarProducto"
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        [Route("delete/")]
        [HttpPost]
        public IHttpActionResult delete(Producto pProducto)
        {
            using(SqlConnection connection = DataBase.connect())
            {
                SqlCommand command = new SqlCommand("dbo.eliminarProducto", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_producto", SqlDbType.BigInt).Value = pProducto._id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    return Json(new Resultado(reader.GetString(0)));
                }
                catch(SqlException ex) { return Json(new Resultado("Error de conexión con la base de datos")); }
                finally { connection.Close(); }
            }
        }


        /// <summary>
        /// Actualiza la información de un producto en la base de datos, ejecutando el 
        /// procedimiento almacenado "actualizarProducto"
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        [Route("update/")]
        [HttpPost]
        public IHttpActionResult update(Producto pProducto)
        {
            using (SqlConnection connection = DataBase.connect())
            {
                SqlCommand command = new SqlCommand("dbo.actualizarProducto", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_producto", connection).Value = pProducto._id;
                command.Parameters.AddWithValue("@nombre", SqlDbType.VarChar).Value = pProducto._nombre;
                command.Parameters.AddWithValue("@descripcion", SqlDbType.VarChar).Value = pProducto._descripcion;
                command.Parameters.AddWithValue("@exento", SqlDbType.Bit).Value = pProducto._exento;
                command.Parameters.AddWithValue("@cantidad_disp", SqlDbType.Int).Value = pProducto._cantidadDisponible;
                command.Parameters.AddWithValue("@precio", SqlDbType.Money).Value = pProducto._precio;
                command.Parameters.AddWithValue("@id_categoria", SqlDbType.BigInt).Value = pProducto._idCategoria;
                command.Parameters.AddWithValue("@id_proveedor", SqlDbType.BigInt).Value = pProducto._idProveedor;
                //command.Parameters.AddWithValue("@imagen", SqlDbType.Image).Value = pProducto._imagen;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    return Json(new Resultado(reader.GetString(0)));
                }
                catch (SqlException ex) { return Json(new Resultado("Error de conexión con la base de datos")); }
                finally { connection.Close(); }
            }
        }


        /// <summary>
        /// Obtiene la información de un producto según su id, ejecutando el procedimiento
        /// almacenado "consultarProducto"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("product/{id:int}")]
        [HttpGet]
        public IHttpActionResult product(int id)
        {

            Producto producto = null;
            using (SqlConnection connection = DataBase.connect())
            {
                SqlCommand command = new SqlCommand("dbo.consultarProducto", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_producto", SqlDbType.BigInt).Value = id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    producto = new Producto();
                    producto._nombre = reader.GetString(0);
                    producto._descripcion = reader.GetString(1);
                    producto._exento = reader.GetBoolean(2);
                    producto._cantidadDisponible = reader.GetInt32(3);
                    producto._precio = reader.GetDecimal(4);
                    //producto._imagen = reader.GetDecimal(5);
                    producto._nombreProveedor = reader.GetString(6);
                    producto._nombreCategoria = reader.GetString(7);
                    producto._nombreSucursal = reader.GetString(8);


                    return Json(producto);
                }
                catch(SqlException ex) { return Json(new Resultado("Error de conexión con la base de datos")); }
                finally { connection.Close(); }
            }   
        }



        /// <summary>
        /// Obtiene la lista de todos los productos de la base de datos, ejecutando el procedimiento 
        /// almacenado "listaarProductos"
        /// </summary>
        /// <returns></returns>
        [Route("all/")]
        [HttpGet]
        public IHttpActionResult all()
        {
            List<Producto> productos = new List<Producto>();
            using(SqlConnection connection = DataBase.connect())
            {
                SqlCommand command = new SqlCommand("dbo.listarProductos", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto prod = new Producto();
                        prod._id = reader.GetInt64(0);
                        prod._nombre = reader.GetString(1);
                        prod._descripcion = reader.GetString(2);
                        prod._exento = reader.GetBoolean(3);
                        prod._cantidadDisponible = reader.GetInt32(4);
                        prod._precio = reader.GetDecimal(5);
                        prod._nombreSucursal = reader.GetString(6);
                        productos.Add(prod);
                    }

                    return Json(productos);
                }
                catch (SqlException ex) { return Json(new Resultado("Prueba")); }//Error de conexión con la base de datos
                finally { connection.Close(); }
            }
        }

    }
}
