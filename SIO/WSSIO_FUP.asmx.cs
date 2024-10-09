using System;
using System.Collections.Generic;
using CapaControl;
using System.Data.SqlClient;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.Services.Protocols;


namespace SIO
{
    [WebService(Namespace = "http://tempuri.org/SecureWebService/SecureWebService", Name = "WebServicesSIO_FUP", Description = "Servicio para integracion FUP")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class WSSIO_FUP : ControlSeguridadWS 
    {

        [WebMethod, SoapHeader("CredencialAutenticacion")]
        public string ValidarLogin (string Usuario, string Password)
        {
            string response;
            int rol = -1, usuId = -1;
            SqlDataReader reader = null;

            if (VerificarPermisos(CredencialAutenticacion))
            {
                ControlInicio CI = new ControlInicio();

                // revisar reader
                int existeLogin = CI.verificarLogin(Usuario);
                if (existeLogin != 0)
                {
                    string passwdOk = CI.verificarContrasena(Usuario, Password);
                    if (passwdOk == Password)
                    {
                        reader = CI.obtenerRolByUsuLogin(Usuario);

                        if (reader.HasRows)
                        {
                            reader.Read();
                            rol = reader.GetInt32(0);
                            usuId = reader.GetInt32(1);
                        }

                    }
                }

            }
            var respuesta = new
            {
                rol = rol,
                IdRepresentante = usuId
            };

            response = JsonConvert.SerializeObject(respuesta);
            return response;
        }

        [WebMethod, SoapHeader("CredencialAutenticacion")]
        public string obtenerListadoPaises(int IdRepresentante, int rol)
        {
            string response;

            if (VerificarPermisos(CredencialAutenticacion))
            {
                ControlUbicacion controlUbicacion = new ControlUbicacion();

                List<CapaControl.Entity.Pais> lstPais = null;
                if ((rol == 3) || (rol == 28) || (rol == 29) || (rol == 33) || (rol == 30))
                {
                    lstPais = controlUbicacion.obtenerListaPaisRepresentante(IdRepresentante);
                }
                else
                {
                    lstPais = controlUbicacion.obtenerListaPais();
                }
                response = JsonConvert.SerializeObject(lstPais);
            }
            else
            {
                response =  "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("CredencialAutenticacion")]
        public string obtenerListadoCiudadesMoneda(int idPais, int IdRepresentante, int rol)
        {
            string response;

            if (VerificarPermisos(CredencialAutenticacion))
            {
                ControlUbicacion controlUbicacion = new ControlUbicacion();
                Dictionary<string, object> queryResult = new Dictionary<string, object>();

                List<CapaControl.Entity.Ciudad> lstCiudad = null;

                if (((rol == 3) || (rol == 30) || (rol == 34) || (rol == 40)) && ((idPais == 8) || (idPais == 6) || (idPais == 21)))
                {
                    lstCiudad = controlUbicacion.obtenerCiudadesRepresentantesColombia(IdRepresentante);
                }
                else
                {
                    lstCiudad = controlUbicacion.obtenerListaCiudades(idPais);
                }

                response = JsonConvert.SerializeObject(lstCiudad);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("CredencialAutenticacion")]
        public string obtenerListadoClientesCiudad(string idCiudad, string IdCliente)
        {
            string response;

            if (VerificarPermisos(CredencialAutenticacion))
            {
                string IdClienteUsuario = "0";

                if (IdCliente != null)
                {
                    IdClienteUsuario = IdCliente.ToString();
                }

                ControlCliente control = new ControlCliente();
                Dictionary<string, object> queryResult = new Dictionary<string, object>();
                List<CapaControl.Entity.Cliente> lstObject = control.obtenerDatosCliente(int.Parse(idCiudad), int.Parse(IdClienteUsuario));
                queryResult.Add("varCliente", lstObject);
                response = JsonConvert.SerializeObject(queryResult);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("CredencialAutenticacion")]
        public string obtenerListadoContactos_Obras_porCliente(string idCliente)
        {
            string response;

            if (VerificarPermisos(CredencialAutenticacion))
            {
                ControlCliente controlCliente = new ControlCliente();
                ControlContacto controlContacto = new ControlContacto();
                Dictionary<string, object> queryResult = new Dictionary<string, object>();
                List<CapaControl.Entity.ContactoCliente> lstContactoCliente = controlCliente.obtenerContactoCliente(int.Parse(idCliente));
                List<CapaControl.Entity.ObraCliente> lstObraCliente = controlContacto.obtenerObrasDistPV(int.Parse(idCliente));
                queryResult.Add("varContacto", lstContactoCliente);
                queryResult.Add("varObra", lstObraCliente);
                response = JsonConvert.SerializeObject(queryResult);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        public WSSIO_FUP()
        {

        }
    }
}
