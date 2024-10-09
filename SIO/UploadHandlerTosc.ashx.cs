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
    public class UploadHandlerTosc : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    HttpFileCollection files = context.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        string fname;
                        if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        fname.Replace(@"[^a-zA-Z0-9_.- ]", "");

                        string carpetafup = context.Request.Form["idfup"] + context.Request.Form["version"];
                        carpetafup = carpetafup.Trim();
                        string pathfup = @"i:\Planos\";
                        string web_pathfup = "/Planos/";

                        if (!Directory.Exists(pathfup))
                        {
                            Directory.CreateDirectory(pathfup);
                        }

                        pathfup = pathfup + carpetafup + @"\";
                        web_pathfup = web_pathfup + carpetafup + @"/";

                        if (!Directory.Exists(pathfup))
                        {
                            Directory.CreateDirectory(pathfup);
                        }

                        string carpetatipo = context.Request.Form["tipo"];
                        switch (carpetatipo)
                        {
                            case "1":
                                {
                                    fname = @"listado_" + fname;
                                }
                                break;
                            case "2":
                                {
                                    fname = @"plano_" + fname;
                                }
                                break;
                            case "3":
                                {
                                    fname = @"doc_" + fname;
                                }
                                break;
                            case "4":
                                {
                                    fname = @"foto_" + fname;
                                }
                                break;
                            case "5":
                                {
                                    fname = @"planotf_" + fname;
                                    pathfup = pathfup + @"PTF\";
                                    web_pathfup = web_pathfup + @"PTF/";

                                    if (!Directory.Exists(pathfup))
                                    {
                                        Directory.CreateDirectory(pathfup);
                                    }
                                }
                                break;
                            case "6":
                                {
                                    fname = @"cartacot_" + fname;
                                    pathfup = pathfup + @"Carta_Cotizacion\";
                                    web_pathfup = web_pathfup + @"Carta_Cotizacion/";

                                    if (!Directory.Exists(pathfup))
                                    {
                                        Directory.CreateDirectory(pathfup);
                                    }
                                }
                                break;
                            case "7":
                                {
                                    fname = @"planofinal_" + fname;
                                }
                                break;
                            case "8":
                                {
                                    fname = @"memoria_" + fname;
                                }
                                break;
                            case "9":
                                {
                                    fname = @"CartaFin_" + fname;
                                }
                                break;
                        }

                        pathfup = pathfup + fname;

                        if (!File.Exists(pathfup))
                        {
                            // Crear log de 

                            int idfup = Convert.ToInt32(context.Request.Form["idfup"]);
                            string version = context.Request.Form["version"];
                            int idtipo = Convert.ToInt32(context.Request.Form["tipo"]);
                            string zonaArchivo = "Salida Cotizacion";
                            string usuario = "ToscanaSoft";
                            int idtipoEvento = Convert.ToInt32(0);

                            //string logMensaje = "Toscana FUP: " + context.Request.Form["idfup"] + "Vers: "+ context.Request.Form["version"] +
                            //    "Tipo: " + context.Request.Form["tipo"];
                            //ControlDatos.GuardarDebugLog("UploadHandlerTosc", logMensaje, usuario);

                            ControlDatos.GuardarAnexo(idfup, fname, web_pathfup, idtipo, usuario, version, zonaArchivo, idtipoEvento);

                            file.SaveAs(pathfup);

                            var objeto = new
                            {
                                id = 1,
                                descripcion = "Archivo Subido Correctamente"
                            };

                            var query = new
                            {
                                conf = objeto
                            };
                            string response = JsonConvert.SerializeObject(query);
                            context.Response.ContentType = "text/plain";
                            context.Response.Write(response);
                        }
                        else
                        {
                            throw new Exception("Archivo ya subido");
                        }
                    }
                }
                else
                {
                    throw new Exception("No se ha subido ningun archivo");
                }
            }
            catch (Exception ex)
            {
                var objeto = new
                {
                    id = 2,
                    descripcion = "Error guardando imagenes: " + ex.Message
                };

                var query = new
                {
                    conf = objeto
                };

                string response = JsonConvert.SerializeObject(query);
                context.Response.ContentType = "text/plain";
                context.Response.Write(response);
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