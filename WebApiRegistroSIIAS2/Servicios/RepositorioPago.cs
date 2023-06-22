using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using WebApiRegistroSIIAS2.Modelos;

namespace WebApiRegistroSIIAS2.Servicios
{
    public interface IRepositorioPago
    {
        Task<RespuestaPago> Insertar(Pago pago);
    }
    public class RepositorioPago: IRepositorioPago
    {
        private readonly string connectionString;
        public RepositorioPago(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnectionOracle");
        }

        /// <summary>
        /// Inserta pago tercero
        /// </summary>
        /// <param name="pago"></param>
        /// <returns></returns>
        public async Task<RespuestaPago> Insertar(Pago pago)
        {
            var oRespuesta = new RespuestaPago();
            string erroresCampos = string.Empty;

            try
            {
                erroresCampos += pago.UnaVigencia == null ? "El valor UnaVigencia es requerido.\n" : string.Empty;
                erroresCampos += string.IsNullOrEmpty(pago.UnaFechaConsignacion) ? "El valor UnaFechaConsignacion es requerido.\n" : string.Empty;
                erroresCampos += pago.UnTeridOrigen == null ? "El valor UnTeridOrigen es requerido.\n" : string.Empty;
                erroresCampos += string.IsNullOrEmpty(pago.UnExpediente) ? "El valor UnExpediente es requerido.\n" : string.Empty;
                erroresCampos += pago.UnVigExpediente == null ? "El valor UnVigExpediente es requerido.\n" : string.Empty;
                erroresCampos += string.IsNullOrEmpty(pago.UnResolucion) ? "El valor UnResolucion es requerido.\n" : string.Empty;
                erroresCampos += pago.UnVigResolucion == null ? "El valor UnVigResolucion es requerido.\n" : string.Empty;
                erroresCampos += pago.UnCodigoArea == null ? "El valor UnCodigoArea es requerido.\n" : string.Empty;
                erroresCampos += string.IsNullOrEmpty(pago.UnFechaEjecutoria) ? "El valor UnFechaEjecutoria es requerido.\n" : string.Empty;
                erroresCampos += string.IsNullOrEmpty(pago.UnFechaActo) ? "El valor UnFechaActo es requerido.\n" : string.Empty;
                erroresCampos += string.IsNullOrEmpty(pago.UnNumeroActo) ? "El valor UnNumeroActo es requerido.\n" : string.Empty;
                erroresCampos += string.IsNullOrEmpty(pago.UnNumeroSoporte) ? "El valor UnNumeroSoporte es requerido.\n" : string.Empty;
                erroresCampos += string.IsNullOrEmpty(pago.UnCoteId1) ? "El valor UnCoteId1 es requerido.\n" : string.Empty;                
                erroresCampos += pago.UnCuota == null ? "El valor UnCuota es requerido.\n" : string.Empty;
                erroresCampos += pago.UnValorCapital == null ? "El valor UnValorCapital es requerido.\n" : string.Empty;
                erroresCampos += string.IsNullOrEmpty(pago.UnDetalle) ? "El valor UnDetalle es requerido.\n" : string.Empty;

                if (pago.UnCoteId2 == null && pago.UnValorInteres != null)
                {
                    erroresCampos += "El campo UnCoteId2 no puede ser nulo cuando UnValorInteres posee valor. \n";
                }

                if (pago.UnCoteId2 != null && pago.UnValorInteres == null)
                {
                    erroresCampos += "El campo UnValorInteres no puede ser nulo cuando UnCoteId2 posee valor. \n";
                }

                if (!string.IsNullOrEmpty(erroresCampos))
                {
                    oRespuesta.Resultado = false;
                    oRespuesta.NumeroRecibo = null;
                    oRespuesta.Mensaje = erroresCampos;

                    return oRespuesta;
                }
                

                var parameters = new DynamicParameters();
                parameters.Add("una_vigencia", pago.UnaVigencia);
                parameters.Add("una_fecha_consignacion", pago.UnaFechaConsignacion);
                parameters.Add("un_terid_origen", pago.UnTeridOrigen);
                parameters.Add("un_expediente", pago.UnExpediente);
                parameters.Add("un_vig_expediente", pago.UnVigExpediente);
                parameters.Add("un_RESOLUCION", pago.UnResolucion);
                parameters.Add("un_vig_RESOLUCION", pago.UnVigResolucion);
                parameters.Add("un_codigo_area", pago.UnCodigoArea);
                parameters.Add("un_FECHA_EJECUTORIA", pago.UnFechaEjecutoria);
                parameters.Add("un_FECHA_ACTO", pago.UnFechaActo);
                parameters.Add("un_NUMERO_ACTO", pago.UnNumeroActo);
                parameters.Add("un_numero_soporte", pago.UnNumeroSoporte);
                parameters.Add("un_cote_id1", pago.UnCoteId1);
                parameters.Add("un_cote_id2", pago.UnCoteId2);
                parameters.Add("un_cuota", pago.UnCuota);
                parameters.Add("un_valor_Capital", pago.UnValorCapital);
                parameters.Add("un_valor_Interes", pago.UnValorInteres);
                parameters.Add("un_detalle", pago.UnDetalle);
                parameters.Add("un_numero_recibo", dbType: DbType.Single, direction: ParameterDirection.Output);
                parameters.Add("un_msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 400);


                using var connection = new OracleConnection(connectionString);

                await connection.ExecuteAsync("ogt_pr_inserta_registro_siias2",
                        parameters, commandType: CommandType.StoredProcedure);

                float numRecibo = parameters.Get<float>("un_numero_recibo");
                string mensaje = parameters.Get<string>("un_msg");

                oRespuesta.Resultado = true;
                oRespuesta.NumeroRecibo = numRecibo;
                oRespuesta.Mensaje = mensaje;
               
            }
            catch (Exception ex)
            {
                oRespuesta.Resultado = false;
                oRespuesta.NumeroRecibo = null;
                oRespuesta.Mensaje = $"No fue posible realizar la transacción: {ex.Message}";
            }           

            return oRespuesta;
        }
    }
}
