using CapaControl;
using CapaControl.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace SIO
{
    /// <summary>
    /// Summary description for UploadFilesPQRSResumen
    /// </summary>
    public class UploadFilesPQRSResumen : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            int fileTypeSP = 0;
            try
            {
                switch(context.Request.Form["Tipo"])
                {
                    case "Radicado":
                        fileTypeSP = 1;
                        break;
                    case "Listados":
                        fileTypeSP = 4;
                        break;
                    case "Implementacion":
                        fileTypeSP = 3;
                        break;
                    case "Cierre":
                        fileTypeSP = 2;
                        break;
                    case "RespuestaProcesos":
                        fileTypeSP = 5;
                        break;
                    case "SolicitudInformacionProcesos":
                        fileTypeSP = 0;
                        break;
                    case "ComunicadoCliente":
                        fileTypeSP = 6;
                        break;
                }
                HttpFileCollection files = context.Request.Files;
                List<string> rutasGuardadas = new List<string>();
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];

                    //String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                    String rutaAplicacion = @"i:\";//Se mueve al File server 
                    rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                    String dlDir = @"ArchivosPQRS\P" + context.Request.Form["IdPQRS"] + @"\";
                    String directorio = rutaAplicacion + dlDir;
                    if (!Directory.Exists(directorio))
                    {
                        Directory.CreateDirectory(directorio);
                    }
                    string pathFile = directorio + file.FileName;
                    string fileName = file.FileName;
                    if (File.Exists(pathFile))
                    {
                        Random rnd = new Random();
                        string[] fileNamePartition = file.FileName.Split('.');
                        fileName = fileNamePartition[0] + " " + rnd.Next(1000, 9999).ToString() + "." + fileNamePartition[fileNamePartition.Length - 1];
                        pathFile = directorio + fileName;
                    }

                    file.SaveAs(pathFile);
                    rutasGuardadas.Add(pathFile);

                    if (fileTypeSP != 0)
                    {
                        Dictionary<string, object> parametros = new Dictionary<string, object>();
                        parametros.Add("@pTipoArchivo", fileTypeSP);
                        parametros.Add("@pIdPQRS", context.Request.Form["IdPQRS"]);
                        parametros.Add("@pFilePATH", dlDir + fileName);
                        parametros.Add("@pFileName", fileName);
                        parametros.Add("@pUsuario", (string)HttpContext.Current.Session["Usuario"]);
                        if (context.Request.Form["Tipo"] == "Listados")
                        {
                            parametros.Add("@pUsuario", (string)HttpContext.Current.Session["Usuario"]);
                            parametros.Add("@pCorreo", (string)HttpContext.Current.Session["rcEmail"]);
                        } else if (context.Request.Form["Tipo"] == "RespuestaProcesos")
                        {
                            parametros["@pIdPQRS"] = context.Request.Form["IdPQRSRespuesta"];
                        }
                        List<RespuestaFilasAfectadas> affectedRows = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_UPD_PQRSArchivosGeneral", parametros);

                        // Notificamos al interno de forsa que se cargaron más archivos
                        ControlPQRS controlPQRS = new ControlPQRS();
                        PQRSDTOConsulta pqrs = controlPQRS.ObtenerPQRSId(context.Request.Form["IdPQRS"].ToString(), string.Empty);
                        if (pqrs.IdFuenteReclamo == 6)
                        {
                            string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];
                            HttpContext.Current.Server.MapPath("~");
                            String rutaAplicacion2 = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                            rutaAplicacion2 = rutaAplicacion2.Replace("\\\\", "\\");
                            FormPQRSConsulta.NotificarInternoForsa(pqrs.IdPQRS, pqrs.Cliente, 2, context.Request.Form["Tipo"], correoSistema, rutaAplicacion2, pqrs.Colaborador);
                        }
                    }
                }
                HttpContext.Current.Session["solicitudInformacionArchivosGuardados"] = string.Join(";", rutasGuardadas.ToArray());
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}