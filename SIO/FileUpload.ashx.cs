using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace SIO
{
    /// <summary>
    /// File Upload httphandler to receive files and save them to the server.
    /// </summary>
    public class FileUpload : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count == 0)
            {
                // LogRequest("No files sent.");
                context.Response.ContentType = "text/plain";
                context.Response.Write("No files received.");
            }
            else
            {
                String modo = context.Request.Form["nombreFile"];
                HttpPostedFile uploadedfile = context.Request.Files[0];
                AgendamientoVis agevis= new AgendamientoVis();
                String FileName = uploadedfile.FileName;
                string FileType = uploadedfile.ContentType;
                int FileSize = uploadedfile.ContentLength;
                string parametroRuta = agevis.parametroruta();//C://caperta/capertadondeseguarda
                // LogRequest(FileName + ", " + FileType + ", " + FileSize);
                String separado = "";
                separado = Path.GetExtension(FileName);
                // String live = HttpContext.Current.Server.MapPath("/" + parametroRuta );
                // uploadedfile.SaveAs(live + "\\" + modo + separado);<-----------SI ES CARPETA
                uploadedfile.SaveAs(parametroRuta.Replace("\\\\", "\\") + "\\" + modo + separado);//<-----------SI ES RUTA COMPLETA
                //uploadedfile.SaveAs(HttpContext.Current.Server.MapPath("/Upload") + "\\" + modo + separado);
                context.Response.ContentType = "text/plain";
                context.Response.Write("{\"name\":\"" + FileName + "\",\"type\":\"" + FileType + "\",\"size\":\"" + FileSize + "\"}");
            }
        }
        public bool IsReusable
        {
            get {  return false;  }
        }
        private void LogRequest(string Log)
        {
            StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath("/Log") + "\\Log.txt", true);
            sw.WriteLine(DateTime.Now.ToString() + " - " + Log);
            sw.Flush();
            sw.Close();
        }
    }
}