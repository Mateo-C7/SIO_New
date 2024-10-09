using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace SIO
{
    /// <summary>
    /// Summary description for refresh_session
    /// </summary>
    public class refresh_session : IHttpHandler, IRequiresSessionState
    {

        //public class MantenSesionHandler : IHttpHandler, IRequiresSessionState
        //{
        //    //aquí el código del manejador, que esencialmente estará vacío.
        //}

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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