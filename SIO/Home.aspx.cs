using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using CapaControl;
using CapaDatos;
using Microsoft.Reporting.WebForms;
using System.Web.UI;

namespace SIO
{
    public partial class Home : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public ControlInicio controlInicio = new ControlInicio();
        private DataSet dsHome1 = new DataSet();
        public BdDatos BdDatos = new BdDatos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Rol"] != null)
                {

                int rol = (int)Session["Rol"];

                Session["SesionActiva"] = "1";  
                string idioma = (string)Session["Idioma"];
                string usuconect = (string)Session["Usuario"];
                
                string nomUsuario="";
                string rcID="0";
                string idUsuario = "0";
                string pais = "0", zona = "0";
                string correoUsu = "";
                string area = "0";
                string correoSistema = "";
                bool infra = false;
                bool posventa = false;
                bool creaOf = false;
                bool solicitaPallet = false;
                int IdClienteUsuario = 0;

                //if (rol == 1)
                //{
                //    lblConectados.Text = Application["usuarios"].ToString();
                //    lblConectados.Visible = true;
                //}

                //consulto el correo del sistema para el envio de notificaciones
                reader = controlInicio.ObtenerCorreoSistema();
                    if (reader.HasRows == true)
                    {
                        reader.Read();
                        correoSistema = reader.GetValue(0).ToString();
                    }
                reader.Close();
                reader.Dispose();
                controlInicio.CerrarConexion();



                //consula los datos del usuario
                if ((rol == 3) || (rol == 9) || (rol == 54) || (rol == 2) || (rol == 28) || (rol == 33) || (rol == 30) || (rol == 46))
                {
                    reader = controlInicio.ObtenerRepresentante(usuconect);
                        if (reader.HasRows == true)
                        {
                            reader.Read();

                            nomUsuario = reader.GetValue(1).ToString();
                            rcID = reader.GetValue(0).ToString();
                            idUsuario = reader.GetValue(5).ToString();
                            correoUsu = reader.GetValue(2).ToString();
                            infra = reader.GetBoolean(6);
                            posventa = reader.GetBoolean(7);
                            solicitaPallet = reader.GetBoolean(8);
                            IdClienteUsuario = Convert.ToInt32( reader.GetValue(9).ToString());
                            creaOf= reader.GetBoolean(10);

                        }
                    reader.Close();
                    reader.Dispose();
                    controlInicio.CerrarConexion();
                }
                else
                {
                    reader = controlInicio.consultarNombre(usuconect);
                        if (reader.HasRows == true)
                        {
                            reader.Read();

                            nomUsuario = reader.GetValue(0).ToString();
                            correoUsu = reader.GetValue(1).ToString();
                            area = reader.GetValue(2).ToString();
                            idUsuario = reader.GetValue(3).ToString();
                            infra = reader.GetBoolean(4);
                            posventa = reader.GetBoolean(5);
                            solicitaPallet = reader.GetBoolean(6);
                            IdClienteUsuario = Convert.ToInt32(reader.GetValue(7).ToString());
                        }

                    reader.Close();
                    reader.Dispose();
                    controlInicio.CerrarConexion();
                }

                reader = controlInicio.obtenerDatosPaisRepresentante(Convert.ToInt32(rcID));
                    if (reader.HasRows == true)
                    {
                       while (reader.Read())
                      {
                        if (reader.GetInt32(1) == 8)
                        {
                            pais = "8";
                        }
                    }
                }
                reader.Close();
                reader.Dispose();
                controlInicio.CerrarConexion();

                reader = controlInicio.obtenerZonaRepresentante(Convert.ToInt32(rcID));
                    if (reader.HasRows == true)
                    {
                        if (reader.Read() == true)
                        {
                            zona = reader.GetValue(1).ToString();
                        }
                        else
                        {
                            zona = "0";
                        }
                    }
                reader.Close();
                reader.Dispose();
                controlInicio.CerrarConexion();

                //CARGA LAS VARIABLES DE SESION PARA UTILIZARLAS EN TODAS LAS PAGINAS
                Session["Usuario"] = usuconect;
                Session["Nombre_Usuario"] = nomUsuario;
                Session["rcID"] = rcID;
                Session["Pais"] = pais;
                Session["rcEmail"] = correoUsu;
                Session["Rol"] = rol;
                Session["Area"] = area;
                Session["Zona"] = zona;
                Session["CorreoSistema"] = correoSistema;
                Session["UsuarioAsunto"] = usuconect.ToUpper();
                Session["Infra"] = infra;
                Session["Posventa"] = posventa;
                Session["idUsuario"] = idUsuario;
                Session["solicitaPallet"] = solicitaPallet;
                Session["IdClienteUsuario"] = IdClienteUsuario;
                Session["CreaOf"] = creaOf;

                    reader = controlInicio.consultarPlantaColombia(Convert.ToInt32(idUsuario));
                    if (reader.HasRows == true)
                    {
                        if (reader.Read() == true)
                        {
                            Session["plantaColombia"] = Convert.ToInt32(reader.GetValue(0).ToString());
                            int col = Convert.ToInt32(reader.GetValue(0).ToString());

                        }
                    }

                reader.Close();
                reader.Dispose();
                controlInicio.CerrarConexion();

                List<ReportParameter> parametro = new List<ReportParameter>();

                if (rol == 3)
                {
                    parametro.Add(new ReportParameter("usuario", nomUsuario, true));
                }
                else
                {
                    parametro.Add(new ReportParameter("usuario", "Todos", true));
                }

                    //ReportViewer1.Width = 1050;
                    //ReportViewer1.Height = 500;
                    //ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    //IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
                    //ReportViewer1.ServerReport.ReportServerCredentials = irsc;

                    //ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
                    //ReportViewer1.ServerReport.ReportPath = "/InformesCRM/COM_HomeNuevo";
                    //this.ReportViewer1.ServerReport.SetParameters(parametro);

                    //ReportViewer2.Width = 800;
                    //ReportViewer2.Height = 400;
                    //ReportViewer2.ProcessingMode = ProcessingMode.Remote;
                    //ReportViewer2.ServerReport.ReportServerCredentials = irsc;

                    if ((int)Session["Rol"] == 16)
                    {
                        Response.Redirect("FormHojaDeVidaProyecto.aspx");
                    }
                    

                }
                else
                {
                    string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    Response.Redirect("Inicio.aspx");
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 12000;
        }

        public class CustomReportCredentials : IReportServerCredentials
        {
            private string _UserName;
            private string _PassWord;
            private string _DomainName;
            public CustomReportCredentials(string UserName, string PassWord, string DomainName)
            {
                _UserName = UserName;
                _PassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }
            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
            }
            public bool GetFormsCredentials
                (
                out Cookie authCookie,
                out string user,
                out string password,
                out string authority
                )
            { authCookie = null; user = password = authority = null; return false; }
        }
    }
}