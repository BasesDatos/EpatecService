using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpatecService.Models
{
    public class Pedido
    {
        public Int64 _idPedido { get; set; }
        public string _idCliente { get; set; }
        public TimeSpan _horaPedido { get; set; }
        public DateTime _fechaPedido { get; set; }
        public List<Producto> _productos { get; set; }
        //public int _sucursal { get; set; }
        public Decimal _total { get; set; }
        //public Int64 _empleadoEncargado { get; set; }

    }
}