using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpatecService.Models
{
    public class Sucursal
    {
        public Int64 _id { get; set; }
        public string _nombre { get; set; }
        public int _idEmpresa { get; set; }
        public string _direccion { get; set; }
    }
}