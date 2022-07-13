using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibreriaControlador.com.ec.BeanObjetos
{
    public class UsuarioBean
    {
        private int codigoUsuario;
        private string numeroDocumento;
        private string nombres;
        private string apellidos;
        private string estadoUsuario;
        private DateTime fechaCreacion;
        private DateTime fechaUltimoAcceso;
        private int nivelAcceso;
        private string password;
        private string correoUsuario;

        public int getCodigoUsuario() {
            return codigoUsuario;
        }

        public void setCodigoUsuario(int codigoUsr) {
            this.codigoUsuario = codigoUsr;
        }

        public string getNumeroDocumento() {
            return numeroDocumento;
        }

        public void setNumeroDocumento(string documento) {
            this.numeroDocumento = documento;
        }

        public string getNombres() {
            return nombres;
        }

        public void setNombres(string nombres) {
            this.nombres = nombres;
        }

        public string getApellido() {
            return apellidos;
        }

        public void setApellido(string apellido) {
            this.apellidos = apellido;
        }

        public void setEstadoUsuario(string estado) {
            this.estadoUsuario = estado;
        }

        public string getEstadoUsuario() {
            return this.estadoUsuario;
        }

        public void setFechaCreacion(DateTime fechaCrea) {
            this.fechaCreacion = fechaCrea;
        }

        public DateTime getFechaCreacion() {
            return this.fechaCreacion;
        }

        public void setFechaUltimoAcceso(DateTime ultimoAcceso) {
            this.fechaUltimoAcceso = ultimoAcceso;
        }

        public DateTime getFechaUltimoAcceso() {
            return this.fechaUltimoAcceso;
        }

        public void setNivelAcceso(int nivelAcceso) {
            this.nivelAcceso = nivelAcceso;
        }

        public int getNivelAcceso() {
            return this.nivelAcceso;
        }

        public string getPassword() {
            return this.password;
        }

        public void setPassword(string password) {
            this.password = password;
        }

        public void setCorreo(string correo) {
            this.correoUsuario = correo;
        }

        public string getCorreo() {
            return this.correoUsuario;
        }
    }
}
