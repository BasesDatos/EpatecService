using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpatecService.Models
{
    public class Producto
    {
        public Int64 _id { get; set; }
        public string _nombre { get; set; }
        public string _descripcion { get; set; }
        public Boolean _exento { get; set; }
        public int _cantidadDisponible { get; set; }
        public Decimal _precio { get; set; }
        public Int64 _idCategoria { get; set; }
        public Int64 _idProveedor { get; set; }
        public Boolean _estado { get; set; }
        public Byte[] _imagen { get; set; }
    }
}