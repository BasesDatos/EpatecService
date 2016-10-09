using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpatecService.Models
{
    public class Categoria
    {
        public Int64 _id { get; set; }
        public string _descripcion { get; set; }
        public string _nombre { get; set; }
        public Boolean _estado { get; set; }
    }
}