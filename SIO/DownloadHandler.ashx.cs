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
    public class DownloadHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
                string NomArchivo = @"I:" + context.Request.Form["Ruta"] + context.Request.Form["NombreArchivo"];

                NomArchivo.Replace("I:~/", "I:/");
 
                if ((NomArchivo).Length < 4)
                    NomArchivo = @"I:" + context.Request.QueryString["Ruta"].ToString() + context.Request.QueryString["NombreArchivo"].ToString(); 

                if (!string.IsNullOrEmpty(NomArchivo) )
                    
                    //&& File.Exists(NomArchivo))  
                {  
                    context.Response.Clear();  
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(NomArchivo));
                    context.Response.WriteFile(NomArchivo);  
                    // This would be the ideal spot to collect some download statistics and / or tracking  
                    // also, you could implement other requests, such as delete the file after download  
                    context.Response.End();          
  
                }  
                else  
                {  
                    context.Response.ContentType = "text/plain";  
                    context.Response.Write("File not be found!");  
                }                

            
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