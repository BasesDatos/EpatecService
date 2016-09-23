using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpatecService
{
    public class Resultado
    {
        public string estado { get; set; }

        public Resultado(string pEstado)
        {
            estado = pEstado;
        }
    }
}