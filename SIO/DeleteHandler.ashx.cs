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
    /// Descripción breve de UploadHandler
    /// </summary>
    public class DeleteHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            
            string NomArchivo = @"I:" + context.Request.Form["Ruta"] + context.Request.Form["NombreArchivo"];
            var objeto = new
            {
                id = 0,
                descripcion = ""
            };

                NomArchivo.Replace("I:~/", "I:/");
 
                if ((NomArchivo).Length < 4)
                    NomArchivo = @"I:" + context.Request.QueryString["Ruta"].ToString() + context.Request.QueryString["NombreArchivo"].ToString(); 

                if (!string.IsNullOrEmpty(NomArchivo) && File.Exists(NomArchivo))  
                {
                    try
                    {
                        File.Delete(NomArchivo);
                        int idfup = Convert.ToInt32(context.Request.Form["idfup"]);
                        int idplano = Convert.ToInt32(context.Request.Form["idplano"]);
                        string usuario = (string)context.Session["Usuario"];

                        ControlDatos.BorrarArchivos(idfup, idplano, usuario);
                        objeto = new
                        {
                            id = 1,
                            descripcion = "Archivo Borrado Correctamente"
                        };

                    }
                    catch (Exception ex)
                    {
                        objeto = new
                                        {
                                            id = 2,
                                            descripcion = "Error Borrando Archivo : " + ex.Message
                                        };
                        
                    }
                }  
                else  
                {
                    objeto = new
                    {
                        id = 3,
                        descripcion = "Error Borrando no Existe Archivo : "
                    };

                }

                var query = new
                {
                    conf = objeto
                };

                string response = JsonConvert.SerializeObject(query);
                context.Response.ContentType = "text/plain";
                context.Response.Write(response);           
            
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}