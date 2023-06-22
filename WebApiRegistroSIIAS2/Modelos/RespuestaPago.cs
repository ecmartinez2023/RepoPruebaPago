using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.DirectoryServices.Protocols;

namespace WebApiRegistroSIIAS2.Modelos
{
    public class RespuestaPago
    {
        public bool Resultado { get; set; }
        public float? NumeroRecibo { get; set; }
        public string Mensaje { get; set; }
    }
}
