using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpatecService.Models
{
    public class Residencia
    {
        public Int64 _id { get; set; }
        public string _pais { get; set; }
        public string _provincia { get; set; }
        public int _codigoPostal { get; set; }
        public string _direccion { get; set; }
        public string _calle { get; set; }
        public int _numeroCasa { get; set; }
    }
}