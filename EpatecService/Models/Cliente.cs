using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpatecService.Models
{
    public class Cliente
    {

        public static string ATRIBUTOS = "Cedula, Nombre, Primer_Apellido, Segundo_Apellido, Usuario, Contraseña, Fecha_Nacimiento, Telefono, Prioridad, Pais, Estado_Provincia, Codigo_Postal, Direccion_Det, Calle, Numero_Casa";

        public string _cedula { get; set; }
        public string _nombre { get; set; }
        public string _primerApellido { get; set; }
        public string _segundoApellido { get; set; }
        public string _usuario { get; set; }
        public string _contrasena { get; set; }
        public DateTime _fechaNacimiento { get; set; }
        public string _telefono { get; set; }
        public string _prioridad { get; set; }
        public string _pais { get; set; }
        public string _provincia { get; set; }
        public int _codigoPostal { get; set; }
        public string _direccion { get; set; }
        public string _calle { get; set; }
        public int _numeroCasa { get; set; }
        
    }
}