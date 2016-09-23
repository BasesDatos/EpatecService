using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpatecService.Models
{
    public class Empleado
    {
        public Int64 _id { get; set; }
        public string _nombre { get; set; }
        public string _primerApellido { get; set; }
        public string _segundoApellido { get; set; }
        public Boolean _estatus { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int _residencia { get; set; }
        public string _telefono { get; set; }
    }
}