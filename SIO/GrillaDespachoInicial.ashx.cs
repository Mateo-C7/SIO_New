﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIO
{
    /// <summary>
    /// Descripción breve de GrillaDespachoInicial
    /// </summary>
    public class GrillaDespachoInicial : IHttpHandler
    {

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