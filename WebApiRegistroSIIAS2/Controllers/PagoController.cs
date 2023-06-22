
using Microsoft.AspNetCore.Mvc;
using WebApiRegistroSIIAS2.Modelos;
using WebApiRegistroSIIAS2.Servicios;

namespace WebApiRegistroSIIAS2.Controllers
{
    [ApiController]
    [Route("api/pago")]
    public class PagoController: ControllerBase
    {
        private readonly IRepositorioPago repositorioPago;

        public PagoController(IRepositorioPago repositorioPago)
        {
            this.repositorioPago = repositorioPago;
        }

        [HttpPost("insertarTransaccion")]
        
        public async Task<ActionResult<RespuestaPago>> InsertarPago([FromBody] Pago pago)
        {

            var RespPago = await repositorioPago.Insertar(pago);

            if (!RespPago.Resultado) {
                return BadRequest(RespPago);
            }

            return RespPago;
        }
    }
}
