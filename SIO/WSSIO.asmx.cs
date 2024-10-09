using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using CapaControl;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
namespace SIO
{
    [WebService(Namespace = "http://localhost/WebService", Name = "WebServicesSIO", Description = "Servicio para cargar combo")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]//importantisimo
    public class WSSIO : System.Web.Services.WebService
    {
        #region WEB METODOS GENERALES
        /***FILTRA TODOS LOS PAISES***/
        [WebMethod(EnableSession = true)]
        public string[] GetListaPais(string prefixText, int count)
        {
            ControlVisitaComercial CVCD = new ControlVisitaComercial();
            DataTable dt = CVCD.cargarListaPais(prefixText);
            ListItem ListI = new ListItem();
            List<string> items = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["nomPais"].ToString(), dt.Rows[i]["idPais"].ToString()));
            }
            return items.ToArray();
        }
        /***FILTRA TODAS LAS CIUDADES DEPENDIENDO DEL PAIS***/
        [WebMethod(EnableSession = true)]
        public string[] GetListaCiudad(string prefixText, int count)
        {
            ListItem ListI = new ListItem();
            List<string> items = new List<string>();
            ControlVisitaComercial CVCD = new ControlVisitaComercial();
            if (Session["idPaisViaje"].ToString() != "NO")
            {
                DataTable dt = CVCD.cargarListaCiudad(prefixText, Session["idPaisViaje"].ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["nomCiu"].ToString(), dt.Rows[i]["idCiudad"].ToString()));
                }
            }
            else { items = null; }
            return items.ToArray();
        }
        #endregion

        #region WEB METODOS POLITICAS
        /******FILTRA LOS USUARIOS EN EL TEXTBOX ******/
        [WebMethod(EnableSession = true)]
        public string[] GetListaUsuarios(string prefixText, int count)
        {
            ControlPoliticas CP = new ControlPoliticas();
            DataTable dt = CP.cargarListaUsu(prefixText);
            ListItem ListI = new ListItem();
            List<string> items = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["usuLogin"].ToString(), dt.Rows[i]["usuId"].ToString()));
            }
            return items.ToArray();
        }
        #endregion

        #region WEB METODOS MAESTRO ITEM AGRUPADOR
        /******FILTRA LAS DESCRIPCIONES EN EL TEXTBOX ******/
        [WebMethod(EnableSession = true)]
        public string[] GetListaDescripcion(string prefixText, int count)
        {
            ControlMaestroItemPlanta CMI = new ControlMaestroItemPlanta();
            DataTable dt = CMI.cargarDescripcion(prefixText);
            ListItem ListI = new ListItem();
            List<string> items = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["descripcion"].ToString(), dt.Rows[i]["itemid"].ToString()));
            }
            return items.ToArray();
        }
        #endregion

        #region WEB METODOS MAESTRO ITEM PLANTA
        /******FILTRA LAS DESCRIPCIONES EN EL TEXTBOX PARA ITEM PLANTA ******/
        [WebMethod(EnableSession = true)]
        public string[] GetDescIPlanta(string prefixText, int count)
      {
            string planta = Session["PlantaID"].ToString();
            List<string> items = new List<string>();
            int id_planta = 0;
            if (!String.IsNullOrEmpty(planta) && planta != "0")
            {
                id_planta = Convert.ToInt32(planta);
                ControlMaestroItemPlanta cmip = new ControlMaestroItemPlanta();
                DataTable dt = cmip.CargarDesc1(prefixText, id_planta);
                ListItem ListI = new ListItem();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["descripcion"].ToString(), dt.Rows[i]["item_planta_id"].ToString()));
                }
            }        
            return items.ToArray();
        }
        #endregion

        #region WEB METODOS SIAT
        /******FILTRA LOS CLIENTES EN EL TEXTBOX  -- SIAT  ******/
        [WebMethod(EnableSession = true)]
        public string[] GetListaClienteSIAT(string prefixText, int count)
        {
            ControlSIAT CS = new ControlSIAT();
            DataTable dt = CS.cargarClientes(prefixText);
            ListItem ListI = new ListItem();
            List<string> items = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["nomCliente"].ToString(), dt.Rows[i]["idCliente"].ToString()));
            }
            return items.ToArray();
        }
        #endregion


        public WSSIO()
        {

        }

        [WebMethod(true)]
        public string Echo(DateTime tiempo)
        {
            return tiempo.Ticks.ToString();
        }
        #region CLASES METODOS DE VISITAS COMERCIALES
        public class DatosVisita : List<Visita>
        {
            public DatosVisita() { }
        }
        public class Visita
        {
            public Visita() { }
            public string nomCliente
            { get; set; }
            public string paisCliente
            { get; set; }
            public string ciuCliente
            { get; set; }
            public string motivo
            { get; set; }
            public string objetivo
            { get; set; }
            public Boolean ValidaCierre
            { get; set; }
            public string idcli
            { get; set; }
            public Boolean visitaCancelada
            { get; set; }
            public string fechaViajeIni
            { get; set; }
            public string fechaViajeFin
            { get; set; }
            public string procesos
            { get; set; }
            public string contacto
            {
                get;
                set;
            }
            public string remoto
            {
                get;
                set;
            }
            public string soporte
            {
                get;
                set;
            }
            public string obra
            {
                get;
                set;
            }
        }
        public class DatosContlite : List<Contatoc>
        {
            public DatosContlite() { }
        }
        public class Contatoc
        {
            public Contatoc() { }
            public string idcli
            { get; set; }
            public string nombre
            { get; set; }
            public string cargo
            { get; set; }
            public string telefono
            { get; set; }
            public string direccion
            { get; set; }
            public string email
            { get; set; }
            public string validanombase
            { get; set; }
        }
        public class DatosEjecucion : List<Dejecu>
        {
            public DatosEjecucion() { }
        }
        public class Dejecu
        {
            public Dejecu() { }
            public string conclusion
            { get; set; }
            public string actividades
            { get; set; }
            public string responsables
            { get; set; }
            public string ActiRespoResul
            { get; set; }
            public string fechaAct
            { get; set; }
        }

        public class DCierr
        {
            public DCierr() { }
            public string Conclusion
            { get; set; }
            public string Fup
            { get; set; }
            public string Version
            { get; set; }
            public string Metros
            { get; set; }
            public string Valor
            { get; set; }
            public string Moneda
            { get; set; }
            public string Vinculo
            { get; set; }
        }
        public class ValidacionesFup : List<DValida>
        {
            public ValidacionesFup() { }
        }
        public class DValida
        {
            public DValida() { }
            public Boolean ValidaFupvarsion
            { get; set; }
            public Boolean ValidaCierre
            { get; set; }
            public String colorVisita
            { get; set; }
            public String estadoVisita
            { get; set; }

        }
        public class DatosEventosF : List<DEventosF>
        {
            public DatosEventosF() { }
        }
        public class DEventosF
        {
            public DEventosF() { }
            public String nomEve
            { get; set; }
            public String fechaIniEve
            { get; set; }
            public String fechaFinEve
            { get; set; }
            public String ciudadEve
            { get; set; }
            public String paisEve
            { get; set; }
            public String partici
            { get; set; }
        }
        public class DMoneda
        {
            public DMoneda() { }
            private String idMoneda;
            public String IdMoneda
            {
                get { return idMoneda; }
                set { idMoneda = value; }
            }
        }
        #endregion

        #region WEB METODOS DE VISITAS COMERCIALES
        /******FILTRA LOS CLIENTES EN EL TEXTBOX PARA VISITAS COMERCIALES ******/

        //cargarParticipantes
        [WebMethod(EnableSession = true)]
        public string[] GetListaParticipantes(string prefixText, int count)
        {
            ControlEvento CE = new ControlEvento();
            DataTable dt = CE.cargarParti(prefixText);
            ListItem ListI = new ListItem();
            List<string> items = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["nombre"].ToString(), dt.Rows[i]["usu"].ToString()));
            }
            return items.ToArray();
        }

        [WebMethod(EnableSession = true)]
        public string[] GetListaClientes(string prefixText, int count, string contextKey)
        {
            ControlVisitaComercial CVCD = new ControlVisitaComercial();
            DataTable dt = CVCD.cargarClientes(prefixText, contextKey);
            ListItem ListI = new ListItem();
            Session["base"] = "SIO";
            List<string> items = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["nomCliente"].ToString(), dt.Rows[i]["idCliente"].ToString()));
            }
            return items.ToArray();
        }
        /***FILTRA TODOS LOS REPRESENTANTES***/
        [WebMethod(EnableSession = true)]
        public string[] GetListaComer(string prefixText, int count)
        {
            ControlVisitaComercial CVCD = new ControlVisitaComercial();
            DataTable dt = CVCD.cargarListaComer(prefixText);
            ListItem ListI = new ListItem();
            List<string> items = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["nomUsu"].ToString(), dt.Rows[i]["usuario"].ToString()));
            }
            return items.ToArray();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuardarEventos(String idvisita, String fechaAgenda, String usuario)
        {
            PlaneacionVis visPla = new PlaneacionVis();
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            CVC.agregarFechaAgenda(usuario, (fechaAgenda == "NULL") ? "NULL" : DateTime.Parse(fechaAgenda).ToString(), idvisita);
            CVC.insertViajeVisita(idvisita, fechaAgenda, usuario);//inserta el id del viaje para la visita
            //DataTable datosIcs = CVC.cargarDatosICS(idvisita);
            //foreach (DataRow row in datosIcs.Rows)
            //{
            //    visPla.crearArchivoICS("VisitaCal", usuario, row["cliente"].ToString(), row["objetivo"].ToString(), String.Format("{0:yyyyMMdd}", DateTime.Parse(fechaAgenda)).Replace("/", "").Replace("-", ""), row["ciudad"].ToString(), row["pais"].ToString(), row["fechaAct"].ToString().Trim().Replace("/", "").Replace("-", "").Replace(":", "").Replace(".", ""), row["motivo"].ToString(), row["correoPro"].ToString(), row["procesos"].ToString(), row["usuSoli"].ToString());
            //}
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String GuardarEjecucion(String idvisita, String conclusion, String fechaAgenda, String usuario, String fup, String version, String metro, String valor, String moneda, String evernote)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            String concluReal = "";
            if(!String.IsNullOrEmpty(conclusion.Trim()))
                concluReal =  conclusion.Replace("\r\n", " ").Replace("\n", " ").Replace(":", " ");
            String fecha = DateTime.Parse(fechaAgenda).ToString();
            String confi = CVC.agregarFechaEje(usuario, concluReal, idvisita, fup, version, metro, valor, moneda, evernote);
            return confi;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EnviarCorreoEje(String idvisita, String conclusion, String fechaAgenda, String usuario)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            CVC.correoEjecucion(idvisita, "VisitasCEje", usuario);
        }

        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public void EnviarCorreoAgen(String idvisita, String fechaAgenda, String usuario) // String nota,
        //{
        //    ControlVisitaComercial CVC = new ControlVisitaComercial();
        //    if (fechaAgenda == "NULL")
        //    { CVC.correoAgen(idvisita, "VisitasCAge", usuario, "NoAge", "Eliminación del Agendamiento de la Visita:"); }
        //    else { CVC.correoAgen(idvisita, "VisitasCAge", usuario, "Age", "Agendamiento de la Visita: "); }
        //}

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EnviarCorreoCierr(String idvisita, String usuario)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            CVC.correoCierre(idvisita, "VisitasCCierre", usuario);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String GuardarContactoNuevoLite(String Nombre, String Cargo, String Telefono, String Direccion, String Email, String idcliente)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            String confi = CVC.agregarContactoLite(Nombre, Cargo, Telefono, Direccion, Email, idcliente);
            return confi;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String GuardarDatoCierrContactoSIO(String idvisita, String idContacto)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            String confi = CVC.actCierreContSIO(idvisita, idContacto);
            return confi;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String GuardarCierre(String idVisita, String idActiv, String fechaAgenda, String usuario, String resp, String fup, String version, String metro, String valor, String moneda)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            String respReal = resp.Replace("\r\n", " ").Replace("\n", " ").Replace(":", " ");
            String fecha = DateTime.Parse(fechaAgenda).ToString();
            String confi = CVC.agregarNofup(usuario, idVisita, respReal, idActiv, fup, version, metro, valor, moneda);
            return confi;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ValidacionesFup ValidarFupVersion(String Fup, String Version)
        {
            ValidacionesFup ValidacionesFupf = null;
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            Boolean confi = CVC.validarfupoVersion(Fup, Version);
            ValidacionesFupf = new ValidacionesFup() { new DValida { ValidaFupvarsion = confi } };
            return ValidacionesFupf;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ValidacionesFup consultarcolorvisita(String idVisita)
        {
            ValidacionesFup ValidacionesFupf = null;
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            String confi = CVC.colorEstadoVis(idVisita);
            string[] consulta = confi.Split('/');
            string color = consulta[0];
            string estado = consulta[1];
            ValidacionesFupf = new ValidacionesFup() { new DValida { colorVisita = color, estadoVisita = estado } };
            return ValidacionesFupf;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DMoneda> ConsultarModenas(String filtro)
        {
            List<DMoneda> listaMon = new List<DMoneda>();
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            DataTable monedas = CVC.consultaMonedas("");
            DMoneda dmon = null;
            foreach (DataRow row in monedas.Rows)
            {
                dmon = new DMoneda();
                dmon.IdMoneda = row["idNomMoneda"].ToString();
                listaMon.Add(dmon);
            }
            return listaMon;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String CheckCierr(String idvisita, String usuario, String conclusion, String fup, String version, String metro, String valor, String moneda, String evernote)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            String confi = CVC.activarCierre(idvisita, usuario, conclusion, fup, version, metro, valor, moneda, evernote);
            return confi;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public DCierr ConsultarParaEjecsaactivHisto(String idvisita)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            DCierr listaCierr = null;
            DataTable visita = CVC.consultaconclusion(idvisita);

            String conclusion1 = "", fup = "", version = "", metros = "", valor = "", vinculo = "", moneda = "";
            conclusion1 = visita.Rows[0]["conclusion"].ToString();
            fup = visita.Rows[0]["vis_fup"].ToString();
            version = visita.Rows[0]["vis_version"].ToString();
            metros = visita.Rows[0]["vis_metroC"].ToString();
            valor = visita.Rows[0]["vis_valorC"].ToString();
            moneda = visita.Rows[0]["moneda"].ToString();
            vinculo = visita.Rows[0]["vis_vinculo_evernote"].ToString();

            listaCierr = new DCierr() { Conclusion = conclusion1, Fup = fup, Version = version, Metros = metros, Moneda = moneda, Valor = valor, Vinculo = vinculo };

            return listaCierr;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public DatosEjecucion ConsultarParaEjecsaactiv(String idvisita)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            DataTable consultaActiLib = CVC.ListaActivdadesLibres(idvisita);
            DataTable consultaResponsable = CVC.ListaResponsalbes();
            //DataTable Conclusionr = CVC.consultaconclusion(idvisita);
            DatosEjecucion listaejecucion = null;
            var activs = "";
            int count = 0;
            var finalt = "";
            foreach (DataRow row in consultaActiLib.Rows)
            {
                count += 1;
                if (consultaActiLib.Rows.Count != count)
                { finalt = ";"; }
                else { finalt = ""; }
                activs = activs + row["actv_id"].ToString() + ":" + row["actv_nombre"].ToString() + finalt;
            }
            var respons = "";
            int count1 = 0;
            var finalt1 = "";
            foreach (DataRow row in consultaResponsable.Rows)
            {
                count1 += 1;
                if (consultaResponsable.Rows.Count != count1)
                { finalt1 = ";"; }
                else { finalt1 = ""; }
                respons = respons + row["idRespVis"].ToString() + ":" + row["idResNom"].ToString() + finalt1;
            }

            listaejecucion = new DatosEjecucion() {
                new Dejecu {  actividades = activs , responsables =respons}
            };
            return listaejecucion;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public DatosContlite ContatosLitecampos(String idcliente, String textAbuscar)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            DataTable Contactolite = CVC.contatosLitebuscar(idcliente, textAbuscar);
            DatosContlite listacontatos = null;
            foreach (DataRow row in Contactolite.Rows)
            {
                listacontatos = new DatosContlite() {
                new Contatoc { idcli =row["idcli"].ToString() , nombre =row["clite_contacto"].ToString(),cargo = row["clite_cargo"].ToString(), telefono = row["clite_telefono"].ToString(), direccion = row["clite_direccion"].ToString(), email = row["clite_correo"].ToString()}
                };
            }
            return listacontatos;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public DatosContlite ContatosLite(String idcliente)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            DataTable Contactolite = CVC.contatosLite(idcliente);
            DatosContlite listacontatos = null;
            foreach (DataRow row in Contactolite.Rows)
            {
                listacontatos = new DatosContlite() {
                new Contatoc { idcli =row["idcli"].ToString() , nombre =row["clite_contacto"].ToString(),cargo = row["clite_cargo"].ToString(), telefono = row["clite_telefono"].ToString(), direccion = row["clite_direccion"].ToString(), email = row["clite_correo"].ToString()}
                };
            }
            return listacontatos;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public DatosContlite ContatosSIOcampos(String idcliente, String textAbuscar)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            DataTable Contactolite = CVC.contatosSIObuscar(idcliente, textAbuscar);
            DatosContlite listacontatos = null;
            var activs = "";
            int count = 0;
            var finalt = "";
            foreach (DataRow row in Contactolite.Rows)
            {
                count += 1;
                if (Contactolite.Rows.Count != count)
                { finalt = ";"; }
                else { finalt = ""; }
                activs = activs + row["idContacto"].ToString() + ":" + row["nomContacto"].ToString() + finalt;
            }

            listacontatos = new DatosContlite() 
            {
            new Contatoc { idcli ="" , nombre =activs ,cargo = "", telefono ="", direccion = "", email = ""}
            };
            return listacontatos;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public DatosContlite ValidarBaseD(String idcliente)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            DataTable validabase = CVC.ValidarBaseD(idcliente);
            DatosContlite listacontatos = null;
            foreach (DataRow row in validabase.Rows)
            {
                listacontatos = new DatosContlite() { new Contatoc { validanombase = row["bdOrigen"].ToString() } };
            }
            return listacontatos;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public DatosVisita ConsultarVisita(String idVisita)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            DataTable consulta = CVC.cargarDatosPopup(idVisita);
            string usuarios = CVC.cargarUsuariosAcompanantes(idVisita);
            DatosVisita listaVisitas = null;
            foreach (DataRow row in consulta.Rows)
            {
                listaVisitas = new DatosVisita() { new Visita { nomCliente = row["nomCliente"].ToString(), paisCliente = row["paisVis"].ToString(), ciuCliente = row["ciuVis"].ToString(),
                motivo = row["motivo"].ToString(), objetivo = row["objetivo"].ToString(), ValidaCierre = Boolean.Parse(row["cierre"].ToString()), idcli = row["idcli"].ToString(),
                visitaCancelada = Boolean.Parse(row["cancelada"].ToString()), fechaViajeIni = row["fechaViajeIni"].ToString(), fechaViajeFin = row["fechaViajeFin"].ToString(), procesos = (usuarios.Trim() == "")? " No tiene":usuarios.Trim(), contacto = row["contacto"].ToString(), remoto = row["remoto"].ToString(), soporte = row["soporte"].ToString(), obra = row["obra"].ToString()}};
            }
            return listaVisitas;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public DatosEventosF ConsultarEventoF(String idEvento)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            ControlEvento CE = new ControlEvento();
            DataTable datos = CVC.cargarEventosF(" AND (eveFer.tifuente_id = " + idEvento + ")");
            DataTable parti = CE.cargarPartiEvento(" AND (part.part_tifuente_id = " + idEvento + ")");
            String participante = "";
            int cont = 0;
            if (parti.Rows.Count > 0)
            {
                foreach (DataRow row1 in parti.Rows)
                {
                    if (cont > 0)
                    {
                        participante = participante + ", " + row1["nombre"].ToString();
                    }
                    else
                    {
                        participante = participante + row1["nombre"].ToString();
                        cont = 1;
                    }
                }
            }
            else { participante = "Ninguno"; }
            DatosEventosF listaEventos = null;
            foreach (DataRow row in datos.Rows)
            {
                listaEventos = new DatosEventosF() { new DEventosF { nomEve = row["nomEve"].ToString(), paisEve = row["paisEve"].ToString(), ciudadEve = row["ciudadEve"].ToString(), fechaIniEve = row["fechaIniEve"].ToString(), fechaFinEve = row["fechaFinEve"].ToString(), partici = participante } };
            }
            return listaEventos;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String VerificarFechaActual(String idVisitaa)
        {// Este metodo me compara la fecha actual con la fecha de agendamiento de la visita, para ver si se puede ejecutar
            String permiso = "True";
            ControlVisitaComercial cvc = new ControlVisitaComercial();
            DateTime fechaActual = cvc.fechaLocal();
            DateTime fechaVis = cvc.fechaAgenVis(idVisitaa);
            if (fechaActual >= fechaVis)
            {
                permiso = "True";
            }
            else
            {
                permiso = "False";
            }
            return permiso;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String cancelarVisita(String idVisitaa)
        {
            String confi = "";
            ControlVisitaComercial cvc = new ControlVisitaComercial();
            confi = cvc.cancelarVisita(idVisitaa);
            return confi;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public Boolean consultarTieneContacto(String idvisitaa)
        {
            Boolean confi = false;
            ControlVisitaComercial cvc = new ControlVisitaComercial();
            confi = cvc.consulTieneContacto(idvisitaa);
            return confi;
        }
        #endregion

        #region CLASES METODOS DE SIAT
        /**METODOS***/
        public class DatosViaje : List<DatosVia>
        {
            public DatosViaje() { }
        }
        public class DatosVia
        {
            public DatosVia() { }
            public string clientes
            { get; set; }
            public string obras
            { get; set; }
            public string ofs
            { get; set; }
            public String tecnicos
            { get; set; }
            public string dTotal
            { get; set; }
            public string dReal
            { get; set; }
            public string dInv
            { get; set; }
            public string dImp
            { get; set; }
            public string dPend
            {
                get;
                set;
            }
            public string pais { get; set; }
            public string ciudad { get; set; }
            public string estado { get; set; }
            public string cotizacion { get; set; }
            public string fechaInicio { get; set; }
            public string fechaFin { get; set; }
        }
        #endregion

        #region WEB METODOS DE SIAT
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public DatosViaje cargaDatosViaje(String idVia)
        {
            DatosViaje listaViaje = null;
            ControlSIAT CS = new ControlSIAT();
            DataTable datos = CS.cargarDatosViaje(idVia);
            foreach (DataRow row in datos.Rows)
            {
                listaViaje = new DatosViaje() {
                new DatosVia {   clientes = row["clientes"].ToString(), obras = row["obras"].ToString(), ofs = row["ofs"].ToString(), tecnicos = row["nomTec"].ToString(), 
                dTotal = row["dTotal"].ToString(), dReal = row["dReal"].ToString(), dInv = row["dInv"].ToString(), dImp = row["dImp"].ToString(), dPend = row["dPend"].ToString(), pais = row["pais"].ToString(), ciudad = row["ciudad"].ToString(), estado = row["estado"].ToString(), cotizacion = row["cotizacion"].ToString(), fechaInicio = Convert.ToDateTime(row["fechaInicio"]).ToShortDateString(), fechaFin = Convert.ToDateTime(row["fechaFin"]).ToShortDateString()}
                };
            }
            return listaViaje;
        }
        #endregion

        #region WEB METODOS PLANEADOR

        public class recordsGrid
        {
            public recordsGrid() { }
            public int recid
            {
                get;
                set;
            }
            public string value
            {
                get;
                set;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String getRecords(String idOfa, String filtro, Object[] columnGroups, string kambam, string idEstado)
        {
            ControlDatos CD = new ControlDatos();
            //Trae los items de la orden
            DataTable datos = CD.getItemsPlaneador(Convert.ToInt32(idOfa), filtro, kambam, Convert.ToInt32(idEstado));
            //Trae los atributos de los records para llenar los items
            DataTable mp = CD.getMateriaPrimaItems(Convert.ToInt32(idOfa), filtro, kambam);

            //string que emula al JSON
            String text = "";

            if (datos.Rows.Count > 0)
            {
                text = "{ ";
                //organizando archivo json para los records
                text += "'total': '" + datos.Rows.Count + "',";
                text += "'records': [ ";

                string records = "";
                int contadorGrid = 0;

                foreach (DataRow row in datos.Rows)
                {
                    contadorGrid++;
                    string recid = contadorGrid.ToString();
                    string idSaldos = row["Identificador"].ToString();
                    string idPieza = row["idPieza"].ToString();
                    string nombre = row["nombre"].ToString();
                    string cantidad = row["cantidad"].ToString();
                    string item = row["item"].ToString();
                    string familia = row["familia"].ToString();
                    string idFamilia = row["Gno"].ToString();
                    int explosionado = Convert.ToInt32(row["Explosion"]);
                    string explosion = "";

                    

                    if (explosionado == 1)
                    {
                        explosion = "SI";
                    }
                    else
                    {
                        explosion = "NO";
                    }

                    ArrayList listColGroups = new ArrayList();
                    listColGroups = llenarListaColumnas(columnGroups);

                    string recordColumns = "";
                    for (int i = 0; i < mp.Rows.Count; i++)
                    {
                        string nom = mp.Rows[i]["nombre"].ToString();
                        string cant = mp.Rows[i]["cantidad"].ToString();
                        string medidas = mp.Rows[i]["medidas"].ToString();
                        string materiaPrima = mp.Rows[i]["materiaPrima"].ToString();
                        string identificador = mp.Rows[i]["Identificador"].ToString();

                        string metodo = mp.Rows[i]["metodo"].ToString(); 

                        if (idSaldos == identificador)
                        {
                            int indexColumn = listColGroups.IndexOf(materiaPrima.Trim());
                            int index = Convert.ToInt32(indexColumn + 1);
                            listColGroups[indexColumn] = materiaPrima + "_X";
                            if (Convert.ToInt32(metodo) > 1)
                            {
                                recordColumns += " , 'cant" + index + "': '" + cant + "', 'long" + index + "': '" + medidas + "','w2ui': { 'style': 'background-color: #FBFEC0' }";
                            }
                            else
                            {
                                recordColumns += " , 'cant" + index + "': '" + cant + "', 'long" + index + "': '" + medidas + "'";
                            }
                        }
                    }
                    records += "{ 'recid': '" + recid + "', 'idSaldos': '" + idSaldos + "', 'idPieza': '" + idPieza + "' , 'nombre': '" + nombre + "' , 'cantidad': '" + cantidad + "' ,'itemP': '" + item + "' ,'idFamilia': '" + idFamilia + "', 'familia': '" + familia + "',  'explosion': '" + explosion + "'" + recordColumns + "}, ";
                }
                records = records.Substring(0, records.Length - 2);
                text += records + "]}";

                text = text.Replace("'", "\"");
            }
            else { }
            return text;
        }

        private ArrayList llenarListaColumnas(Object[] columnGroups)
        {
            ArrayList list = new ArrayList();
            for (int i = 2; i < columnGroups.Length; i++)
            {
                object objColumn = columnGroups[i];
                var jsonColumns = new JavaScriptSerializer().Serialize(objColumn);
                JObject jObjectColumns = JObject.Parse(jsonColumns);
                string columna = (string)jObjectColumns["caption"];

                if (columna != "")
                {
                    string cadena = "";
                    string[] arrayColumna = columna.Split(' ');
                    for (int j = 1; j < arrayColumna.Length; j++)
                    {
                        cadena += arrayColumna[j] + " ";
                    }
                    list.Add(cadena.Trim());
                }
            }
            return list;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String getRecordsMP(String idOfa, String filtro, String kambam)
        {
            ControlDatos CD = new ControlDatos();
            //Trae los atributos de los records para llenar los items
            DataTable mp = CD.getMateriaPrimaItems(Convert.ToInt32(idOfa), filtro, kambam);

            //string que emula al JSON
            String text = "";
            if (mp.Rows.Count > 0)
            {
                text = "{ ";
                //organizando archivo json para los records
                text += "'records': [ ";

                string records = "";
                ArrayList materiaPrima = new ArrayList();
                int recid = 0;
                string recordVacio = "{ 'recid': '0', 'mp': ' ', 'codigo': ' '}, ";
                for (int i = 0; i < mp.Rows.Count; i++)
                {
                    string nombre = mp.Rows[i]["materiaPrima"].ToString();
                    string codigo = "";
                    try
                    {
                        codigo = mp.Rows[i]["Explo_MpId"].ToString();
                    }
                    catch
                    {
                        codigo = "";
                    }
                    if (!materiaPrima.Contains(nombre))
                    {
                        recid++;
                        records += "{ 'recid': '" + recid + "', 'mp': '" + nombre + "', 'codigo': '" + codigo + "'}, ";
                        materiaPrima.Add(nombre);
                    }
                }

                //Agregamos un registro vacío
                if (recid <=9 || recid <11)
                {
                    records = recordVacio + records;
                }

                records = records.Substring(0, records.Length - 2);
                text += records + "]}";

                text = text.Replace("'", "\"");
            }
            return text;
        }

        public class columnasGrid
        {
            public columnasGrid() { }
            public int span
            {
                get;
                set;
            }
            public string caption
            {
                get;
                set;
            }
        }

        public class Columnas : List<columnasGrid>
        {
            public Columnas() { }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public Columnas cargarPlaneador(String idOfa, String filtro, String kambam, String idEstado, string columnasEstaticas)
        {
            int numColumnasEstaticas = Convert.ToInt32(columnasEstaticas);
            ControlDatos CD = new ControlDatos();
            //Trae los items de la orden
            DataTable datos = CD.getItemsPlaneador(Convert.ToInt32(idOfa), filtro, kambam, Convert.ToInt32(idEstado));
            //Trae los atributos de los records para llenar los items
            DataTable mp = CD.getMateriaPrimaItems(Convert.ToInt32(idOfa), filtro, kambam);

            //algoritmo para armar estructuras de datos para llenar grid
            //Lista de retorno con el grupo de columnas
            Columnas list = new Columnas();
            columnasGrid col1 = new columnasGrid();
            columnasGrid col2 = new columnasGrid();
            col1.caption = " ";
            col1.span = 1;
            list.Add(col1);
            col2.caption = "Items";
            col2.span = numColumnasEstaticas - 1;
            list.Add(col2);

            ArrayList listPrincipal = new ArrayList();
            if (datos.Rows.Count > 0)
            {
                foreach (DataRow row in datos.Rows)
                {
                    ArrayList listAdicionales = new ArrayList();
                    string idSaldos = row["Identificador"].ToString();

                    for (int i = 0; i < mp.Rows.Count; i++)
                    {
                        string materiaPrima = mp.Rows[i]["materiaPrima"].ToString();
                        string identificador = mp.Rows[i]["Identificador"].ToString();
                        if (idSaldos == identificador)
                        {
                            if (!listPrincipal.Contains(materiaPrima))
                            {
                                listAdicionales.Add(materiaPrima);
                            }
                            else
                            {
                                int index = listPrincipal.IndexOf(materiaPrima);
                                listPrincipal[index] = materiaPrima + "_X";
                            }
                        }
                    }

                    //DEPURACIÓN Y ADICIÓN DE REGISTROS EN LA LISTA DE RETORNO Y COMPARACIÓN
                    ArrayList listAuxiliar = new ArrayList();

                    //se agegan elementos a la lista principal
                    foreach (string item in listAdicionales)
                    {
                        listPrincipal.Add(item);
                    }
                    //se clona la lista principal
                    foreach (string item in listPrincipal)
                    {
                        listAuxiliar.Add(item);
                    }
                    //se modifica la lista auxiliar
                    for (int i = 0; i < listPrincipal.Count; i++)
                    {
                        string item = listPrincipal[i].ToString();
                        if (item.Contains("_X"))
                        {
                            listAuxiliar[i] = item.Substring(0, item.Length - 2);
                        }
                    }
                    //se pasa la lista mofificada a la lista principal
                    listPrincipal.Clear();
                    foreach (string item in listAuxiliar)
                    {
                        listPrincipal.Add(item);
                    }
                }
            }
            else { }

            int contador = 0;
            //Se agregan las columnas de materia prima a la lista de retorno
            foreach (string e in listPrincipal)
            {
                contador++;
                string nombre = "#" + contador + " " + e;
                columnasGrid colg = new columnasGrid();
                colg.span = 3;
                colg.caption = nombre;
                list.Add(colg);
            }

            //retorno la lista de grupo de columnas
            return list;
        }

        [WebMethod(EnableSession = true)]
        public string guardarExplosion(string idOfaP, string idOfa, object[] columnGroups, object[] columns, object[] records, object[] recordsMateriaPrima,string idFamilia)
        {
            //variable de retorno
            string msj = "";

            ControlDatos CD = new ControlDatos();

            //string planta = Session["PlantaID"].ToString();
            string usuario = Session["Usuario"].ToString();
            string cedula = Session["cedula"].ToString();
            DateTime date = DateTime.Now;

            string Explo_IdOfaP = idOfaP;
            string Explo_MpId = "0";
            string Explo_PieId = "";
            string Explo_OfaId = idOfa;
            string Explo_SalId = "";
            string Explo_Cant = "";
            string Explo_CanMm = "0";
            string Explo_CanKit = "";
            string Explo_CanEnt = "0";
            string Explo_CanRec = "0";
            string Explo_FePMm = "0";
            string Explo_FeRMm = "0";
            string Explo_IeEmpC = cedula;
            string Explo_Anula = "0";
            string Explo_Planea = "0";
            string Explo_Med1 = "0";
            string Explo_Med2 = "0";
            string Explo_Med3 = "0";
            string Explo_Med4 = "0";
            string Explo_Pos = "Sin";
            string Explo_PlantaId = "1";
            string Explo_OrAccId = "0";
            string usu_Crea = cedula;
            string fecha_Crea = String.Format("{0:d/M/yyyy HH:mm:ss}", date);
            string Explo_Mp_Desc = "";
            string Explo_Inventor = "1";
            string Explo_Mp_Principal_Id = "0"; 
            string sqlIni = " INSERT INTO Explo_Mp( Explo_IdOfaP, Explo_MpId, Explo_PieId, Explo_OfaId, Explo_SalId, Explo_Cant, Explo_CanMm, Explo_CanKit, Explo_CanEnt, Explo_CanRec, Explo_FePMm, Explo_FeRMm, Explo_IeEmpC, Explo_Anula, Explo_Planea, Explo_Med1, Explo_Med2, Explo_Med3, Explo_Med4, Explo_Pos, Explo_PlantaId, Explo_OrAccId, usu_Crea, fecha_Crea, Explo_Mp_Desc, Explo_Inventor, Explo_Mp_Principal_Id) VALUES ( ";
            string sqlDelete = " DELETE FROM Explo_Mp WHERE Explo_OfaId = " + idOfa + " ";            
            string sql = " ";

            string values = " ";

            int estadoExplosionador = 1; // Estado Guardado
            int idExploPrincipal = -1;

            int cantidadPiezas = 0;

            try
            {
                for (int i = 0; i < records.Length; i++)
                {
                    object obj = records[i];
                    var json = new JavaScriptSerializer().Serialize(obj);
                    JObject jObject = JObject.Parse(json);
                    string piezaId = (string)jObject["idPieza"];
                    Explo_PieId = piezaId;
                    string idSaldos = (string)jObject["idSaldos"];
                    Explo_SalId = idSaldos;
                    cantidadPiezas = Convert.ToInt32(jObject["cantidad"]);
                    string explosionado = (string)jObject["explosion"];

                    for (int j = 2; j < columnGroups.Length; j++)
                    {
                        values = " ";
                        Explo_Mp_Desc = "";
                        Explo_Cant = "0";
                        Explo_Med1 = "0";
                        Explo_Med2 = "0";
                        Explo_Med3 = "0";
                        Explo_Med4 = "0";
                        Explo_MpId = "0";

                        object objColGroups = columnGroups[j];
                        var jsonColGroups = new JavaScriptSerializer().Serialize(objColGroups);
                        JObject jObjectColGroups = JObject.Parse(jsonColGroups);

                        if ((string)jObjectColGroups["caption"] != "")
                        {
                            string nombreMp = (string)jObjectColGroups["caption"];
                            int index = 0;

                            string[] numeroNombre = nombreMp.Split(' ');
                            string[] numero = numeroNombre[0].Split('#');
                            string cadena = "";
                            for (int x = 1; x < numeroNombre.Length; x++)
                            {
                                cadena += numeroNombre[x] + " ";
                            }
                            Explo_Mp_Desc = cadena.Trim();
                            index = Convert.ToInt32(numero[1]);

                            object objRecords = records[i];
                            var jsonRecords = new JavaScriptSerializer().Serialize(objRecords);
                            JObject jObjectRecords = JObject.Parse(jsonRecords);

                            Explo_MpId = getCodigoMateriaPrima(recordsMateriaPrima, Explo_Mp_Desc);

                            try
                            {
                                string longitud = (string)jObjectRecords["long" + index];
                                double cantKit = 0;
                                try
                                {
                                    Explo_Cant = (string)jObjectRecords["cant" + index];
                                    cantKit = Convert.ToDouble(Explo_Cant) / cantidadPiezas;
                                    Explo_CanKit = cantKit.ToString();
                                }
                                catch
                                {
                                    cantKit = 0;
                                    Explo_CanKit = Explo_CanKit.ToString();
                                }
                                try
                                {
                                    string[] longitudMedidas = longitud.ToUpper().Split('X');
                                    int longitudMedidasArray = 0;
                                    longitudMedidasArray = longitudMedidas.Length;

                                    switch (longitudMedidasArray)
                                    {
                                        case 1:
                                            if (!String.IsNullOrEmpty(longitudMedidas[0]))
                                                Explo_Med1 = longitudMedidas[0];
                                            break;
                                        case 2:
                                            if (!String.IsNullOrEmpty(longitudMedidas[0]))
                                                Explo_Med1 = longitudMedidas[0];
                                            if (!String.IsNullOrEmpty(longitudMedidas[1]))
                                                Explo_Med2 = longitudMedidas[1];
                                            break;
                                        case 3:
                                            if (!String.IsNullOrEmpty(longitudMedidas[0]))
                                                Explo_Med1 = longitudMedidas[0];
                                            if (!String.IsNullOrEmpty(longitudMedidas[1]))
                                                Explo_Med2 = longitudMedidas[1];
                                            if (!String.IsNullOrEmpty(longitudMedidas[2]))
                                                Explo_Med3 = longitudMedidas[2];
                                            break;
                                        case 4:
                                            if (!String.IsNullOrEmpty(longitudMedidas[0]))
                                                Explo_Med1 = longitudMedidas[0];
                                            if (!String.IsNullOrEmpty(longitudMedidas[1]))
                                                Explo_Med2 = longitudMedidas[1];
                                            if (!String.IsNullOrEmpty(longitudMedidas[2]))
                                                Explo_Med3 = longitudMedidas[2];
                                            if (!String.IsNullOrEmpty(longitudMedidas[3]))
                                                Explo_Med4 = longitudMedidas[3];
                                            break;
                                        default:
                                            Explo_Med1 = longitudMedidas[0];
                                            break;
                                    }
                                }
                                catch
                                {
                                    Explo_Med1 = longitud;
                                }

                                msj = validarValoresPlaneador(nombreMp, Explo_Cant, Explo_Med1, Explo_Med2, i + 1, cantKit);
                                if (String.IsNullOrEmpty(msj))
                                {
                                    if (!String.IsNullOrEmpty(Explo_Cant))
                                    {
                                        values = " " + Explo_IdOfaP + ", " + Explo_MpId + ", " + Explo_PieId + ", " + Explo_OfaId + ", " + Explo_SalId + ", " + Explo_Cant + ", " + Explo_CanMm + ", " + Explo_CanKit + ", " + Explo_CanEnt + ", " + Explo_CanRec + ", " + Explo_FePMm + ", " + Explo_FeRMm + ", " + Explo_IeEmpC + ", " + Explo_Anula + ", " + Explo_Planea + ", " + Explo_Med1 + ", " + Explo_Med2 + ", " + Explo_Med3 + ", " + Explo_Med4 + ", '" + Explo_Pos + "', " + Explo_PlantaId + ", " + Explo_OrAccId + ", '" + usu_Crea + "', '" + fecha_Crea + "', '" + Explo_Mp_Desc + "', " + Explo_Inventor + "); ";
                                        sql += sqlIni + values;
                                    }
                                    else
                                    {
                                        values = "";
                                    }
                                }
                                else
                                {
                                    return msj;
                                }
                            }
                            catch
                            {
                                sql = "";
                            }
                        }
                    }
                }
                if (sql.Trim() != "")
                {
                    idExploPrincipal = CD.getIdExploPrincipal(Convert.ToInt32(idOfa), Convert.ToInt32(idFamilia));
                    if (idExploPrincipal == 0)
                    {
                        idExploPrincipal = CD.insertLogExploPrincipal(Convert.ToInt32(idOfa), estadoExplosionador, usuario,Convert.ToInt32(idFamilia));
                    }
                    if (idExploPrincipal != -1)
                    {
                        string query = "", sentence = "";
                        CD.insertLogExploMp(idExploPrincipal, estadoExplosionador, usuario);
                        string[] arrInsertEXploMP = sql.Split(';');
                        for (int i = 0; i < arrInsertEXploMP.Length - 1; i++)
                        {
                            sentence = arrInsertEXploMP[i].Substring(0, arrInsertEXploMP[i].Length - 1);
                            query += sentence + ", " + idExploPrincipal + "); ";
                        }
                        sqlDelete = sqlDelete + "AND Explo_Mp_Principal_Id= " + idExploPrincipal + "; ";
                        query = sqlDelete + query;
                        string[] prueba = query.Split(';');
                        CD.exectQuery(query);
                        msj = "OK";
                    }
                    else
                    {
                        msj = "";
                    }
                }
                else
                {
                    msj = "";
                }
            }
            catch (Exception error)
            {
                msj = "";
            }
            return msj;
        }

        private string validarValoresPlaneador(string materiaPrima, string cantidad, string medida1, string medida2, int indexRecord, double canKit)
        {
            string msj = "";

            if (canKit % 1 != 0)
            {
                msj = "La celda ubicada en el REGISTRO: " + indexRecord + ", COLUMNA " + materiaPrima + " no corresponde a una cantidad exacta.";
            }

            else if ((!String.IsNullOrEmpty(cantidad) && cantidad != "0") && (String.IsNullOrEmpty(medida1) || medida1 == "0"))
            {
                msj = "La celda ubicada en el REGISTRO: " + indexRecord + ", COLUMNA: " + materiaPrima + " debe tener la medida asociada diferente a cero (0). Gracias.";
            }
            else if ((String.IsNullOrEmpty(cantidad) || cantidad == "0") && (!String.IsNullOrEmpty(medida1) && medida1 != "0"))
            {
                msj = "La celda ubicada en el REGISTRO: " + indexRecord + ", COLUMNA: " + materiaPrima + " debe tener la cantidad asociada diferente de cero (0). Gracias.";
            }
            else if (materiaPrima.Contains("LAM"))
            {
                if (cantidad != "0" && !String.IsNullOrEmpty(cantidad))
                {
                    if ((String.IsNullOrEmpty(medida1) || String.IsNullOrEmpty(medida2) || medida1 == "0" || medida2 == "0"))
                    {
                        msj = "La celda ubicada en el REGISTRO: " + indexRecord + ", COLUMNA: " + materiaPrima + " debe tener dos medidas separadas por (x). Gracias.";
                    }
                }
            }

            return msj;
        }

        private string getCodigoMateriaPrima(object[] recordsMateriaPrima, string mp)
        {
            string codigoMateriaPrima = "";
            for (int i = 0; i < recordsMateriaPrima.Length; i++)
            {
                object objRecords = recordsMateriaPrima[i];
                var jsonRecords = new JavaScriptSerializer().Serialize(objRecords);
                JObject jObjectRecords = JObject.Parse(jsonRecords);
                string materiaPrima = (string)jObjectRecords["mp"];
                if (materiaPrima == mp)
                {
                    codigoMateriaPrima = (string)jObjectRecords["codigo"];
                    break;
                }
            }
            return codigoMateriaPrima;
        }

        [WebMethod(EnableSession = true)]
        public string updateExplosion(string idOfa, object[] columnGroups, object[] columns, object[] records, object[] recordsMateriaPrima, int idExploPrincipal)
        {
            //variable de retorno
            string msj = "";

            ControlDatos CD = new ControlDatos();

            //string planta = Session["PlantaID"].ToString();
            string usuario = Session["Usuario"].ToString();

            string Explo_MpId = "0";
            string Explo_PieId = "";
            string Explo_OfaId = idOfa;
            string Explo_SalId = "";
            string Explo_Cant = "";
            string Explo_CanKit = "";
            string Explo_Med1 = "0";
            string Explo_Med2 = "0";
            string Explo_Med3 = "0";
            string Explo_Med4 = "0";
            string Explo_Mp_Desc = "";
            string sqlIni = " UPDATE Explo_Mp ";
            string sql = "";

            string values = " ";

            int cantidadPiezas = 0;

            try
            {
                for (int i = 0; i < records.Length; i++)
                {
                    object obj = records[i];
                    var json = new JavaScriptSerializer().Serialize(obj);
                    JObject jObject = JObject.Parse(json);
                    string piezaId = (string)jObject["idPieza"];
                    Explo_PieId = piezaId;
                    string idSaldos = (string)jObject["idSaldos"];
                    Explo_SalId = idSaldos;
                    cantidadPiezas = Convert.ToInt32(jObject["cantidad"]);
                    string explosionado = (string)jObject["explosion"];

                    for (int j = 2; j < columnGroups.Length; j++)
                    {
                        values = " ";
                        Explo_Mp_Desc = "";
                        Explo_Cant = "0";
                        Explo_Med1 = "0";
                        Explo_Med2 = "0";
                        Explo_Med3 = "0";
                        Explo_Med4 = "0";
                        Explo_MpId = "0";

                        object objColGroups = columnGroups[j];
                        var jsonColGroups = new JavaScriptSerializer().Serialize(objColGroups);
                        JObject jObjectColGroups = JObject.Parse(jsonColGroups);

                        if ((string)jObjectColGroups["caption"] != "")
                        {
                            string nombreMp = (string)jObjectColGroups["caption"];
                            int index = 0;

                            string[] numeroNombre = nombreMp.Split(' ');
                            string[] numero = numeroNombre[0].Split('#');
                            string cadena = "";
                            for (int x = 1; x < numeroNombre.Length; x++)
                            {
                                cadena += numeroNombre[x] + " ";
                            }
                            Explo_Mp_Desc = cadena.Trim();
                            index = Convert.ToInt32(numero[1]);

                            object objRecords = records[i];
                            var jsonRecords = new JavaScriptSerializer().Serialize(objRecords);
                            JObject jObjectRecords = JObject.Parse(jsonRecords);

                            Explo_MpId = getCodigoMateriaPrima(recordsMateriaPrima, Explo_Mp_Desc);
                            double cantKit = 0;
                            try
                            {
                                string longitud = (string)jObjectRecords["long" + index];

                                try
                                {
                                    Explo_Cant = (string)jObjectRecords["cant" + index];
                                    cantKit = Convert.ToDouble(Explo_Cant) / cantidadPiezas;
                                    Explo_CanKit = cantKit.ToString();
                                }
                                catch
                                {
                                    cantKit = 0;
                                    Explo_CanKit = Explo_CanKit.ToString();
                                }
                                try
                                {
                                    string[] longitudMedidas = longitud.ToUpper().Split('X');
                                    int longitudMedidasArray = 0;
                                    longitudMedidasArray = longitudMedidas.Length;

                                    switch (longitudMedidasArray)
                                    {
                                        case 1:
                                            if (!String.IsNullOrEmpty(longitudMedidas[0]))
                                                Explo_Med1 = longitudMedidas[0];
                                            break;
                                        case 2:
                                            if (!String.IsNullOrEmpty(longitudMedidas[0]))
                                                Explo_Med1 = longitudMedidas[0];
                                            if (!String.IsNullOrEmpty(longitudMedidas[1]))
                                                Explo_Med2 = longitudMedidas[1];
                                            break;
                                        case 3:
                                            if (!String.IsNullOrEmpty(longitudMedidas[0]))
                                                Explo_Med1 = longitudMedidas[0];
                                            if (!String.IsNullOrEmpty(longitudMedidas[1]))
                                                Explo_Med2 = longitudMedidas[1];
                                            if (!String.IsNullOrEmpty(longitudMedidas[2]))
                                                Explo_Med3 = longitudMedidas[2];
                                            break;
                                        case 4:
                                            if (!String.IsNullOrEmpty(longitudMedidas[0]))
                                                Explo_Med1 = longitudMedidas[0];
                                            if (!String.IsNullOrEmpty(longitudMedidas[1]))
                                                Explo_Med2 = longitudMedidas[1];
                                            if (!String.IsNullOrEmpty(longitudMedidas[2]))
                                                Explo_Med3 = longitudMedidas[2];
                                            if (!String.IsNullOrEmpty(longitudMedidas[3]))
                                                Explo_Med4 = longitudMedidas[3];
                                            break;
                                        default:
                                            Explo_Med1 = longitudMedidas[0];
                                            break;
                                    }
                                }
                                catch
                                {
                                    Explo_Med1 = longitud;
                                }

                                msj = validarValoresPlaneador(nombreMp, Explo_Cant, Explo_Med1, Explo_Med2, i + 1, cantKit);
                                if (String.IsNullOrEmpty(msj))
                                {
                                    if (!String.IsNullOrEmpty(Explo_Cant) && explosionado == "SI")
                                    {
                                        values = " SET Explo_MpId = " + Explo_MpId + ", Explo_PieId = " + Explo_PieId + ", Explo_Cant = " + Explo_Cant + ", Explo_CanKit = " + Explo_CanKit + ", Explo_Med1 = " + Explo_Med1 + ", Explo_Med2 = " + Explo_Med2 + ", Explo_Mp_Desc= '" + Explo_Mp_Desc + "' " +
                                                 " WHERE Explo_SalId = " + Explo_SalId + " AND " + " Explo_OfaId = " + Explo_OfaId + " AND Explo_Mp_Principal_Id = " + idExploPrincipal + "; ";
                                        sql += sqlIni + values;
                                    }
                                    else
                                    {
                                        values = "";
                                    }
                                }
                                else
                                {
                                    return msj;
                                }
                            }
                            catch (Exception error)
                            {
                                sql = "";
                            }
                        }
                    }
                }
                if (sql.Trim() != "")
                {
                    CD.exectQuery(sql);
                    msj = "OK";
                }
                else
                {
                    msj = "";
                }
            }
            catch
            {
                msj = "";
            }
            return msj;
        }

        public class Estado
        {
            public Estado() { }
            public int id
            {
                get;
                set;
            }
            public string descripcion
            {
                get;
                set;
            }
        }

        [WebMethod(EnableSession = true)]
        public Estado getEstadoExplosionador(int idOfa,int idFamilia)
        {
            ControlDatos CD = new ControlDatos();
            DataTable dt = new DataTable();
            dt = CD.getEstadoExplosionador(idOfa, idFamilia);

            Estado estado = new Estado();

            if (dt.Rows.Count > 0)
            {
                estado.id = Convert.ToInt32(dt.Rows[0]["id_estado_explosionador"]);
                estado.descripcion = dt.Rows[0]["descripcion"].ToString();
            }
            else
            {
                estado.id = 0;
                estado.descripcion = "Nuevo";
            }
            return estado;
        }

        public class ListaMateriaPrima : List<MateriaPrima>
        {
            public ListaMateriaPrima() { }
        }
        public class MateriaPrima
        {
            public MateriaPrima() { }
            public string materiaPrima
            { get; set; }
            public double total
            { get; set; }
            public int factor
            { get; set; }
            public double result
            { get; set; }
            public double medida
            { get; set; }
            public int codigo
            { get; set; }
            public string familia
            { get; set; }
            public int idFamilia
            { get; set; }
            public double peso
            { get; set; }
        }

        private int getFactorMateriaPrima(string codigo, int idPlanta)
        {
            int factor = 100;
            ControlDatos CD = new ControlDatos();
            DataTable dt = new DataTable();
            dt = CD.getParametrosMP(codigo, idPlanta);
            if (dt.Rows.Count > 0)
            {
                string unidad = dt.Rows[0]["unidad"].ToString();
                switch (unidad)
                {
                    case "Longitud":
                        factor = 100;
                        break;
                    case "Area":
                        factor = 10000;
                        break;
                    default:
                        factor = 100;
                        break;
                }
            }
            return factor;
        }

        private double getMedidaMateriaPrima(string codigo, int idPlanta)
        {
            double medida = 0;
            ControlDatos CD = new ControlDatos();
            DataTable dt = new DataTable();
            dt = CD.getParametrosMP(codigo, idPlanta);
            if (dt.Rows.Count > 0)
            {
                string unidad = dt.Rows[0]["unidad"].ToString();
                double medida1 = Convert.ToDouble(dt.Rows[0]["medida1"]);
                double medida2 = Convert.ToDouble(dt.Rows[0]["medida2"]);
                if (unidad == "Area")
                {
                    medida = medida1 * medida2;
                }
                else
                {
                    medida = medida2;
                }
            }
            return medida;
        }

        [WebMethod(EnableSession = true)]
        public ListaMateriaPrima calcularResumenMateriaPrima(object[] records, object[] columnGroups, object[] recordsMateriaPrima, string idPlanta)
        {
            ListaMateriaPrima objListaMateriaPrima = new ListaMateriaPrima();

            for (int i = 0; i < records.Length; i++)
            {
                object objRecord = records[i];
                var json = new JavaScriptSerializer().Serialize(objRecord);
                JObject jObject = JObject.Parse(json);

                string familia = (string)jObject["familia"];
                string idFamilia = (string)jObject["idFamilia"];

                for (int j = 2; j < columnGroups.Length; j++)
                {
                    object objColGroups = columnGroups[j];
                    var jsonColGroups = new JavaScriptSerializer().Serialize(objColGroups);
                    JObject jObjectColGroups = JObject.Parse(jsonColGroups);

                    if ((string)jObjectColGroups["caption"] != "")
                    {
                        string nombreMp = (string)jObjectColGroups["caption"];
                        int index = 0;

                        string[] numeroNombre = nombreMp.Split(' ');
                        string[] numero = numeroNombre[0].Split('#');
                        string cadena = "";
                        for (int x = 1; x < numeroNombre.Length; x++)
                        {
                            cadena += numeroNombre[x] + " ";
                        }
                        string materiaPrima = cadena.Trim();
                        index = Convert.ToInt32(numero[1]);

                        object objRecords = records[i];
                        var jsonRecords = new JavaScriptSerializer().Serialize(objRecords);
                        JObject jObjectRecords = JObject.Parse(jsonRecords);

                        string longitud = "";
                        string cantidadMateriaPrima = "";
                        string medida1 = "";
                        string medida2 = "";
                        string medida3 = "";
                        string medida4 = "";
                        double medida = 0;
                        double total = 0;
                        double peso = 0;
                        //Valor agregado por perdida en corte
                        double longExtra = 0.4;

                        try
                        {
                            longitud = (string)jObjectRecords["long" + index];
                            cantidadMateriaPrima = (string)jObjectRecords["cant" + index];
                            medida1 = "";
                            medida2 = "";
                    
                            try
                            {
                                string[] longitudMedidas = longitud.ToUpper().Split('X');
                                medida1 = longitudMedidas[0];
                                medida2 = "";                      
                                if (!String.IsNullOrEmpty(longitudMedidas[1]))
                                    medida2 = longitudMedidas[1];
                            }
                            catch
                            {
                                medida1 = longitud;
                            }

                            if (!String.IsNullOrEmpty(medida2))
                            {
                                medida = (Convert.ToDouble(medida1) + longExtra) * (Convert.ToDouble(medida2) + longExtra);
                            }
                            else
                            {
                                medida = Convert.ToDouble(medida1) + longExtra;
                            }

                            total = Convert.ToDouble(cantidadMateriaPrima) * medida;

                            if (total != 0)
                            {
                                string codigo = getCodigoMateriaPrima(recordsMateriaPrima, materiaPrima);
                                ControlDatos CD = new ControlDatos();
                                DataTable dt = CD.getParametrosMP(codigo,Convert.ToInt32(idPlanta.ToString()));

                                if (dt.Rows.Count > 0)
                                {
                                    peso = Convert.ToDouble(dt.Rows[0]["peso"]);
                                }
                                string temp = "";
                                for (int x = 0; x < objListaMateriaPrima.Count; x++)
                                {
                                    if (objListaMateriaPrima[x].materiaPrima == materiaPrima && objListaMateriaPrima[x].familia == familia)
                                    {
                                        temp = "OK";
                                        objListaMateriaPrima[x].total += total;
                                        objListaMateriaPrima[x].peso += peso;
                                        break;
                                    }
                                }

                                if (String.IsNullOrEmpty(temp))
                                {
                                    int factor = getFactorMateriaPrima(codigo, Convert.ToInt32(idPlanta.ToString()));
                                    double medidaMp = getMedidaMateriaPrima(codigo, Convert.ToInt32(idPlanta.ToString()));
                                    MateriaPrima objMateriaPrima = new MateriaPrima();
                                    objMateriaPrima.materiaPrima = materiaPrima;
                                    objMateriaPrima.factor = factor;
                                    objMateriaPrima.medida = medidaMp;
                                    objMateriaPrima.codigo = Convert.ToInt32(codigo);
                                    objMateriaPrima.familia = familia;
                                    objMateriaPrima.idFamilia = Convert.ToInt32(idFamilia);
                                    objMateriaPrima.total += total;
                                    objMateriaPrima.peso += peso;                               
                                    objListaMateriaPrima.Add(objMateriaPrima);
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }

            ListaMateriaPrima obj = new ListaMateriaPrima();
            obj = calcularResultMateriaPrima(objListaMateriaPrima);

            return obj;
        }

        private ListaMateriaPrima calcularResultMateriaPrima(ListaMateriaPrima objListaMateriaPrima)
        {
            double result = 0;
            for (int i = 0; i < objListaMateriaPrima.Count; i++)
            {
                result = objListaMateriaPrima[i].total / objListaMateriaPrima[i].medida;
                objListaMateriaPrima[i].result = Math.Ceiling(result);
            }
            return objListaMateriaPrima;
        }

        [WebMethod(EnableSession = true)]
        public string validarCodigoMateriaPrima(object[] records)
        {
            ControlDatos CD = new ControlDatos();
            int result = 0;
            string msjInicio = "Código NO válido para siguiente Materia Prima: ";
            string msj = "";

            for (int i = 0; i < records.Length; i++)
            {
                object obj = records[i];
                var jsonRecords = new JavaScriptSerializer().Serialize(obj);
                JObject jObjectRecords = JObject.Parse(jsonRecords);

                string materiaPrima = (string)jObjectRecords["mp"];
                string codigo = (string)jObjectRecords["codigo"];

                if (materiaPrima.Trim() != "")
                {
                    if (codigo.Trim() != "" && codigo.Trim() != "0")
                    {
                        result = CD.validarCodigoMateriaPrima(materiaPrima, codigo);

                        if (result == -1)
                        {
                            msj += materiaPrima + ", ";
                        }
                    }
                    else
                    {
                        msj += materiaPrima + ", ";
                    }
                }
            }
            if (!String.IsNullOrEmpty(msj))
            {
                msj = msj.Substring(0, msj.Length - 2);
                msj = msjInicio + msj;
            }
            return msj;
        }

        [WebMethod(EnableSession = true)]
        public string agregarSolicitudMateriaPrima(object[] records, object[] columnGroups, object[] recordsMateriaPrima, string idOfa, string idOfaPadre, string idExploPrincipal, string idPlanta)
        {
            string msj = "";
            ListaMateriaPrima list = new ListaMateriaPrima();
            ControlDatos CD = new ControlDatos();

            DateTime date = DateTime.Now;
            double fecha = date.ToOADate();
            string usuario = Session["Usuario"].ToString();
            string cedula = Session["cedula"].ToString();

            //Definición de parametros para insertar Sol_Mp            
            //string fecha = String.Format("{0:d/M/yyyy HH:mm:ss}", date);
            double Mp_Sol_Fec = fecha;
            int Mp_Sol_Dev_Id = 0;
            string Mp_Sol_EmpId = cedula;
            int Mp_Sol_ApruId = 0;
            string Mp_Sol_IdOfa = idOfa;
            string Mp_Sol_Flia = "";
            string Mp_Sol_Obs = "Explosionado desde SIO";
            string Mp_Sol_Sen = "Solicitud";
            int Mp_Sol_Anula = 0;
            int Mp_Sol_PlantaId = 1;
            int Mp_Sol_Origen = 1;
            string Mp_Sol_IdOfaPa = idOfaPadre;
            int planta_id = Convert.ToInt32(idPlanta);

            string sqlIni = " INSERT INTO Mp_Sol(Mp_Sol_Fec, Mp_Sol_Dev_Id, Mp_Sol_EmpId, Mp_Sol_ApruId, Mp_Sol_IdOfa, " +
                            " Mp_Sol_Flia, Mp_Sol_Obs, Mp_Sol_Sen, Mp_Sol_Anula, Mp_Sol_PlantaId, Mp_Sol_IdOfaPa, planta_id,Mp_Sol_Origen) VALUES( ";
            string sql = "";

            list = calcularResumenMateriaPrima(records, columnGroups, recordsMateriaPrima, idPlanta);
            ArrayList listFamilias = new ArrayList();
            for (int i = 0; i < list.Count; i++)
            {
                string values = "";
                Mp_Sol_Flia = list[i].idFamilia.ToString();

                if (!listFamilias.Contains(Mp_Sol_Flia))
                {
                    values = Mp_Sol_Fec + ", " + Mp_Sol_Dev_Id + ", "
                         + Mp_Sol_EmpId + ", " + Mp_Sol_ApruId + ", " + Mp_Sol_IdOfa + ", "
                         + Mp_Sol_Flia + ", '" + Mp_Sol_Obs + "', '"
                         + Mp_Sol_Sen + "', " + Mp_Sol_Anula + ", " + Mp_Sol_PlantaId + ", "
                         + Mp_Sol_IdOfaPa + ", " + planta_id + "," + Mp_Sol_Origen + " ); ";
                    sql += sqlIni + values;
                    listFamilias.Add(Mp_Sol_Flia);
                }
            }

            if (!String.IsNullOrEmpty(sql.Trim()))
            {
                try
                {
                    CD.exectQuery(sql);
                    int estadoExplosionador = 4; //Estado Solicitado
                    CD.updateEstadoExplo(Convert.ToInt32(idExploPrincipal), estadoExplosionador, usuario);
                    CD.insertLogExploMp(Convert.ToInt32(idExploPrincipal), estadoExplosionador, usuario);

                    string sqlDetalleSol = "";
                    string sqlDetalleSolIni = " INSERT INTO Mp_sol_Det(Sol_Mp_Det_SolMp_Id, Sol_Mp_Det_OfaId, " +
                                              " Sol_Mp_Det_MpId, Sol_Mp_Det_Cant_R, Sol_Mp_Det_Cant_E, Sol_Mp_Det_PesoE, " +
                                              " Sol_Mp_Det_PesoR, Sol_Mp_Det_Anula, Sol_Mp_Det_EmpId) VALUES( ";

                    //Definición de parámetros Mp_sol_Det
                    string Sol_Mp_Det_SolMp_Id = ""; //id sol mp
                    string Sol_Mp_Det_OfaId = idOfa;
                    string Sol_Mp_Det_MpId = ""; //materia prima id
                    string Sol_Mp_Det_Cant_R = "0";
                    string Sol_Mp_Det_Cant_E = "";
                    string Sol_Mp_Det_PesoE = "";
                    string Sol_Mp_Det_PesoR = "0";
                    string Sol_Mp_Det_Anula = "0";
                    string Sol_Mp_Det_EmpId = cedula;

                    ListaSolMp listaSol = new ListaSolMp();
                    listaSol = getListaSolMp(Convert.ToInt32(idOfa),Convert.ToInt32(Mp_Sol_Flia.ToString()));

                    for (int i = 0; i < list.Count; i++)
                    {
                        int idFamilia = list[i].idFamilia;
                        Sol_Mp_Det_SolMp_Id = getIdSolMp(listaSol, idFamilia).ToString();
                        Sol_Mp_Det_MpId = list[i].codigo.ToString();
                        Sol_Mp_Det_Cant_E = list[i].result.ToString();
                        Sol_Mp_Det_PesoE = list[i].peso.ToString();

                        string values = "";
                        values = Sol_Mp_Det_SolMp_Id + ", " + Sol_Mp_Det_OfaId + ", "
                                 + Sol_Mp_Det_MpId + ", " + Sol_Mp_Det_Cant_R + ", " + Sol_Mp_Det_Cant_E + ", "
                                 + Sol_Mp_Det_PesoE + ", " + Sol_Mp_Det_PesoR + ", "
                                 + Sol_Mp_Det_Anula + ", " + Sol_Mp_Det_EmpId + " ); ";
                        sqlDetalleSol += sqlDetalleSolIni + values;
                    }

                    if (!String.IsNullOrEmpty(sqlDetalleSol))
                    {
                        CD.exectQuery(sqlDetalleSol);
                       
                        msj = "Solicitud creada con éxito!";

                    }

                    else { msj = "Ha ocurrido un error"; }
                }
                catch (Exception error)
                {
                    msj = "Error en la creación de la solicitud";
                }
            }
            else
            {
                msj = "Ha ocurrido un error";
            }
            return msj;
        }

        private string completarCeros(string input, int size)
        {
            string result = input;
            while (result.Length < size)
            {
                result = "0" + result;
            }
            return result;
        }

        private ListaSolMp getListaSolMp(int idOfa, int id_familia)
        {
            ControlDatos CD = new ControlDatos();
            DataTable dt = new DataTable();
            ListaSolMp lista = new ListaSolMp();
            dt = CD.getListaSolMp(idOfa, id_familia);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SolMp obj = new SolMp();
                    obj.idSolMp = Convert.ToInt32(dt.Rows[i]["Mp_Sol_Id"]);
                    obj.idFamilia = Convert.ToInt32(dt.Rows[i]["Mp_Sol_Flia"]);
                    lista.Add(obj);
                }
            }
            return lista;
        }

        public class ListaSolMp : List<SolMp>
        {
            public ListaSolMp() { }
        }
        public class SolMp
        {
            public SolMp() { }
            public int idSolMp
            { get; set; }
            public int idFamilia
            { get; set; }
        }

        private int getIdSolMp(ListaSolMp lista, int idFamilia)
        {
            int id = 0;
            for (int i = 0; i < lista.Count; i++)
            {
                if (idFamilia == lista[i].idFamilia)
                {
                    id = lista[i].idSolMp;
                    break;
                }
            }
            return id;
        }
        #endregion
    }
}
