using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpatecService.Models
{
    public class Sucursal
    {
        public int _id { get; set; }
        public string _nombre { get; set; }
        public int _idEmpresa { get; set; }
        public string _direccion { get; set; }
        public Boolean _estado { get; set; }
    }
}