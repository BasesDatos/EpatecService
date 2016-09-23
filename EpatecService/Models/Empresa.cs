using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpatecService.Models
{
    public class Empresa
    {
        public int _id { get; set; }
        public string _nombre { get; set; }
        public string _direccion { get; set; }
        public string _cedulaJuridica { get; set; }
        public string _telefono { get; set; }
        public string _correo { get; set; }
    }
}