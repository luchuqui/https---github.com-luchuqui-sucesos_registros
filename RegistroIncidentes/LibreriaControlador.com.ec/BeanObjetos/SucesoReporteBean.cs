using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibreriaControlador.com.ec.BeanObjetos
{
    public class SucesoReporteBean
    {
        public string documento { get; set; }
        public string codigo_usuario { get; set; }
        public string codigoIncidente { get; set; }
        public bool recibidoSistemas { get; set; }
        public string codigoTipoSistemas { get; set; }
        //public DateTime registroIncidente { get; set; }
        //public DateTime reporteIncidente { get; set; }
        //public DateTime primerInteraccion { get; set; }
        public string registroIncidente { get; set; }
        public string reporteIncidente { get; set; }
        public string primerInteraccion { get; set; }
        public string codigoGrupoAsignado { get; set; }
        public string codigoDato { get; set; }
        public string codigoSop { get; set; }
        public string estadoIncidente { get; set; }
        public string codigoTierUno { get; set; }
        public string codigoTierDos { get; set; }
        public string codigoTierTres { get; set; }
        public bool enviadoSistmas { get; set; }
        //public DateTime fechaCierre { get; set; }
        public string fechaCierre { get; set; }
        public string descripcionincidente { get; set; }
        public string pais { get; set; }
        public bool estadistica { get; set; }
        public string usuarioAsigna { get; set; }
    }
}
