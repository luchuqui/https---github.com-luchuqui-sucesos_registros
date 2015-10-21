using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibreriaControlador.com.ec.BeanObjetos
{
    public class ParametroConfiguracion
    {
        public int id_parametro
        {
            set;
            get;
        }

        public string descripcion
        {
            get;
            set;
        }

        public string valor
        {
            get;
            set;
        }

        public string estado
        {
            get;
            set;
        }
    }
}
