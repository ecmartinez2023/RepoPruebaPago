using System.ComponentModel.DataAnnotations;

namespace WebApiRegistroSIIAS2.Modelos
{
    public class Pago
    {                
        public float? UnaVigencia { get; set; }        
        public string UnaFechaConsignacion { get; set; }        
        public float? UnTeridOrigen { get; set; }       
        public string UnExpediente { get; set; }        
        public float? UnVigExpediente { get; set; }        
        public string UnResolucion { get; set; }        
        public float? UnVigResolucion { get; set; }        
        public float? UnCodigoArea { get; set; }        
        public string UnFechaEjecutoria { get; set; }        
        public string UnFechaActo { get; set; }        
        public string UnNumeroActo { get; set; }       
        public string UnNumeroSoporte { get; set; }        
        public string UnCoteId1 { get; set; }
        public string UnCoteId2 { get; set; }        
        public float? UnCuota { get; set; }        
        public float? UnValorCapital { get; set; }
        public float? UnValorInteres { get; set; }            
        public string UnDetalle { get; set; }
    }
}
