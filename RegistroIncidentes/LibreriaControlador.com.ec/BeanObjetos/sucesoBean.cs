using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibreriaControlador.com.ec.BeanObjetos
{
    public class sucesoBean
    {
        public int codigo_usuario { get; set; }
        public string codigoIncidente { get; set; }
        public bool recibidoSistemas { get; set; }
        public int codigoTipoSistemas { get; set; }
        public DateTime registroIncidente { get; set; }
        public DateTime reporteIncidente { get; set; }
        public DateTime primerInteraccion { get; set; }
        public int codigoGrupoAsignado { get; set; }
        public int codigoDato { get; set; }
        public int codigoSop { get; set; }
        public int estadoIncidente { get; set; }
        public int codigoTierUno { get; set; }
        public int codigoTierDos { get; set; }
        public int codigoTierTres { get; set; }
        public bool enviadoSistmas { get; set; }
        public DateTime fechaCierre { get; set; }
        public string descripcionincidente { get; set; }
        public string pais { get; set; }
        public bool estadistica { get; set; }
        public int codigo_usuario_reporta { get; set; }
        public string etiqueta { get; set; }
    }
}
