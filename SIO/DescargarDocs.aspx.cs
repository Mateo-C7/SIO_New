using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIO
{
    public partial class DescargarDocs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            descArchivo(Session["ruta"].ToString(),Session["nombre"].ToString());
        }
        private void descArchivo(String ruta, String nombre)
        {
            // Se abre el archivo para porderlo desacargar a el cliente    
            if (!String.IsNullOrEmpty(nombre))//verifica si el archivo exite
            {
                System.IO.FileInfo toDownload = new System.IO.FileInfo(ruta);
                if (toDownload.Exists)
                {
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + nombre);
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(ruta);//Se carga la ruta para la descarga
                    Response.Flush();
                    Response.Close();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }
    }
}