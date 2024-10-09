using System;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace CapaControl
{
    /// Clase que se encarga de contener las credenciales de autenticación: Usuario y Clave
    public class Autenticacion : SoapHeader
    {
        private string sUserPass;
        private string sUserName;

        /// Lee o escribe la clave del usuario
        public string UsuarioClave
        {
            get { return sUserPass; }
            set { sUserPass = value; }
        }

        /// Lee o escribe el nombre del usuario
        public string UsuarioNombre
        {
            get { return sUserName; }
            set { sUserName = value; }
        }
    }

    public class ControlSeguridadWS : WebService
    {
        
        /// Contiene la referencia a la clase Autenticacion
        public static Autenticacion CredencialAutenticacion;

    }
}

