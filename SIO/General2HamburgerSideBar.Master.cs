using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using CapaControl;
using CapaDatos;
using System.Configuration;

namespace SIO
{
    public partial class General2HamburgerSideBar : System.Web.UI.MasterPage
    {
        public SqlDataReader reader = null;
        public ControlInicio controlInicio = new ControlInicio();
        public ControlContacto controlCont = new ControlContacto();
        private DataSet dsHome = new DataSet();
        public ControlPoliticas CP = new ControlPoliticas();
        public BdDatos BdDatos = new BdDatos();
        int submenu_counter = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)  //|| Application["usuarios"] != null)
                {
                    string pagina = "";
                    pagina = Request.Url.Segments[Request.Url.Segments.Length - 1];
                    controlInicio.Guardar_Log_Sesion(0, Convert.ToString(Session["Usuario"]), pagina);

                    string usuarioSesion = "";
                    usuarioSesion = Session["Usuario"].ToString();

                    //ScriptManager.RegisterStartupScript(this.GetType(), "Set","get("Test();",true);
                    //string estado = Session["estadoSesion"].ToString();
                    String menu = crearMenus(usuarioSesion);//Crear los modulos, submodulos y rutinas
                    Page.ClientScript.RegisterStartupScript(GetType(), "script20", menu, true);

                    //String script = "document.getElementById('bPreguntar').innerHTML  = '" + estado + "'; ";
                    //Page.ClientScript.RegisterStartupScript(GetType(), "bPreguntar", script, true);

                    //para temporizar y cerrar la sesion
                    // Page.RegisterRedirectOnSessionEndScript();

                    string idioma = (string)Session["Idioma"];
                    string usuconect = (string)Session["Usuario"];
                    int arRol = (int)Session["Rol"];
                    lblNombre.Text = (string)Session["Nombre_Usuario"];
                    //conectados.Text = Application["activos"].ToString();
                    //conectados.Text = OnlineActiveUsers.OnlineUsersInstance.OnlineUsers.GuestUsersCount.ToString() + " "+ "Usr";

                    string Conn = ConfigurationManager.AppSettings["Conn"];
                    if (Conn != "REAL")
                    {
                        lblConectadoA.Text = Conn;
                        lblConectadoA.Visible = true;
                    }
                }
                else
                {
                    string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    Response.Redirect("Inicio.aspx");
                }
            }

            try
            {
                string nombre = Session["Nombre_Usuario"].ToString();
                string usuario = Session["Usuario"].ToString();
            }
            catch
            {
                mensajeVentana("La sesion ha Expirado, por favor inicie sesión nuevamente. Gracias!");
                cerrarSesion();
            }
        }

        private void mensajeVentana(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        private void cerrarSesion()
        {
            Session["Nombre_Usuario"] = null;
            Session["Usuario"] = null;
        }

        // politicas
        /*Crear los modulos, submodulos y rutinas*/
        public String crearMenus(String usuario)
        {
            String menuP = "";
            String menuR = "";
            String script = "";
            DataTable menuPricipales = null;
            menuPricipales = CP.buscarMenusPrin(usuario);//Busca solo los modulos principales

            if (menuPricipales != null)
            {
                foreach (DataRow row in menuPricipales.Rows)
                {
                    int IdModulo = 0;
                    IdModulo = int.Parse(row["idMod"].ToString());
                    menuR = "";
                    menuR = menuR + consultaMosSub(IdModulo, usuario);//Consulta si tiene submodulos
                    if (menuR == "")
                    {
                        menuP = menuP + "<li><a href=" + row["enlaceMod"].ToString() + "><span>" + row["nomMod"].ToString() + "</span></a>";
                    }
                    else
                    {
                        menuP = menuP + "<li class=nav-submenu><a class=collapsed href=# data-toggle=collapse data-target=#submenu" + submenu_counter+"><span>" + row["nomMod"].ToString() + "</span></a>";
                        menuP = menuP + "<div class=collapse id=submenu" + submenu_counter+" aria-expanded=false>";
                        submenu_counter++;
                    }
                    if(menuR == "")
                    {
                        menuP = menuP + menuR + "</li>";
                    }
                    else
                    {
                        menuP = menuP + menuR + "</div></li>";
                    }
                    
                    script = "document.getElementById('urlcss').innerHTML  = '" + menuP + "'; ";
                }
            }

            return script;
        }

        //Crea los submodulos
        public String consultaMosSub(int idMod, String usuario)
        {
            String menuSub = "";
            String menuR = "";
            int contSubMenus = 0;
            String rutinas = "";
            int noRut = 1;//un stop para dejar de buscar rutinas/=0 no busque mas rutinas /=1 que busque rutinas
            contSubMenus = CP.contarMenuSub(idMod, usuario);//entra la primera vez como modulo principal, las demas veces son submodulos//consulta si tiene submenus
            if (contSubMenus >= 1)//si tiene submodulos, busca si ese submodulo tiene otros submodulos y asi consecutivamente
            {
                noRut = 1;
                DataTable subMenus = null;
                subMenus = CP.buscarMenuSub(idMod, usuario);
                if (subMenus == null)
                {
                    string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    Response.Redirect("Inicio.aspx");
                }
                else
                    foreach (DataRow row in subMenus.Rows)
                    {
                        menuSub = menuSub + "<ul><li class=nav-submenu><a class=collapsed href=# data-toggle=collapse data-target=#submenu" + submenu_counter + "><span>" + row["nomMod"].ToString() + "</span></a>";
                        menuSub = menuSub + "<div class=collapse id=submenu" + submenu_counter + " aria-expanded=false>";
                        submenu_counter++;
                        menuR = "";
                        menuR = menuR + consultaMosSub(int.Parse(row["idMod"].ToString()), usuario);//busca si ese submodulo tiene otros submodulos y asi consecutivamente
                        menuSub = menuSub + menuR + "</li></ul>";
                    }
                //menuSub += "</div>";
            }
            else if (contSubMenus == 0)//si ese submodulo no tiene mas submodulos, pasa ya a crear las rutinas
            {
                noRut = 0;
                menuR = "";
                menuR = crearSoloRutinas(idMod, usuario);//crea las rutinas
                menuSub = menuSub + menuR;
            }

            if (noRut == 1)//busca rutinas//un stop para dejar de buscar rutinas/=0 no busque mas rutinas /=1 que busque rutinas
            {
                rutinas = "";
                rutinas = crearSoloRutinas(idMod, usuario) + "</div>";//crea las rutinas
            }
            else { rutinas = ""; }
            return menuSub + rutinas;
        }
        //Crea las rutinas
        public String crearSoloRutinas(int idMod, String usuario)
        {
            String rut = "";
            String menuR = "";
            DataTable rutinas = CP.consultaRutinas(usuario, idMod);//busca las rutinas
            if (rutinas != null)
            {
                if (rutinas.Rows.Count == 0)
                { rut = ""; }
                else
                {
                    rut = "<ul class=pl-1>";
                    foreach (DataRow row in rutinas.Rows)
                    {
                        rut = rut + "<li><a href=" + row["enlaceRut"].ToString() + "><span>" + row["nomRut"].ToString() + "</span></a></li>";
                    }
                    menuR = rut + "</ul>";
                }
            }
            return menuR;
        }



        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //string linea = " <tr> <th>" + Session["Usuario"].ToString() + "</th> <th>" + Session["hora"].ToString() + "</th> </tr> ";

            //Application.Lock();
            //Application["activos"] = (int)Application["activos"] - 1;
            //Application["usuarios"] = Application["usuarios"].ToString().Replace(Session["linea"].ToString(), "");

            //Application.UnLock();
            //OnlineActiveUsers.OnlineUsersInstance.OnlineUsers.SetUserOffline(Session["Usuario"].ToString());
            //BdDatos.desconectar();
            //Session["estadoSesion"] = "false";

            Response.Redirect("Inicio.aspx");
        }

    }
}

/* politicas
/*Crear los modulos, submodulos y rutinas
public String crearMenus(String usuario)
{
    String menuP = "";
    String menuR = "";
    String script = "";
    DataTable menuPricipales = null;
    menuPricipales = CP.buscarMenusPrin(usuario);//Busca solo los modulos principales

    if (menuPricipales != null)
    {
        foreach (DataRow row in menuPricipales.Rows)
        {
            int IdModulo = 0;
            IdModulo = int.Parse(row["idMod"].ToString());
            menuR = "";
            menuR = menuR + consultaMosSub(IdModulo, usuario);//Consulta si tiene submodulos
            if (menuR == "")
            {
                menuP = menuP + "<li><a href=" + row["enlaceMod"].ToString() + "><span>" + row["nomMod"].ToString() + "</span></a>";
            }
            else
            {
                menuP = menuP + "<li class=has-sub><a href=" + row["enlaceMod"].ToString() + "><span>" + row["nomMod"].ToString() + "</span></a>";
            }
            menuP = menuP + menuR + "</li>";
            //script = "document.getElementById('urlcss').innerHTML  = '" + menuP + "'; ";
        }
    }

    return script;
}

//Crea los submodulos
public String consultaMosSub(int idMod, String usuario)
{
    String menuSub = "";
    String menuR = "";
    int contSubMenus = 0;
    String rutinas = "";
    int noRut = 1;//un stop para dejar de buscar rutinas/=0 no busque mas rutinas /=1 que busque rutinas
    contSubMenus = CP.contarMenuSub(idMod, usuario);//entra la primera vez como modulo principal, las demas veces son submodulos//consulta si tiene submenus
    if (contSubMenus >= 1)//si tiene submodulos, busca si ese submodulo tiene otros submodulos y asi consecutivamente
    {
        noRut = 1;
        DataTable subMenus = null;
        subMenus = CP.buscarMenuSub(idMod, usuario);
        if (subMenus == null)
        {
            string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            Response.Redirect("Inicio.aspx");
        }
        else
            foreach (DataRow row in subMenus.Rows)
            {
                menuSub = menuSub + "<ul><li class=has-sub><a href=" + row["enlaceMod"].ToString() + "><span>" + row["nomMod"].ToString() + "</span></a>";
                menuR = "";
                menuR = menuR + consultaMosSub(int.Parse(row["idMod"].ToString()), usuario);//busca si ese submodulo tiene otros submodulos y asi consecutivamente
                menuSub = menuSub + menuR + "</li></ul>";
            }
    }
    else if (contSubMenus == 0)//si ese submodulo no tiene mas submodulos, pasa ya a crear las rutinas
    {
        noRut = 0;
        menuR = "";
        menuR = crearSoloRutinas(idMod, usuario);//crea las rutinas
        menuSub = menuSub + menuR;
    }

    if (noRut == 1)//busca rutinas//un stop para dejar de buscar rutinas/=0 no busque mas rutinas /=1 que busque rutinas
    {
        rutinas = "";
        rutinas = crearSoloRutinas(idMod, usuario);//crea las rutinas
    }
    else { rutinas = ""; }
    return menuSub + rutinas;
}

public String crearSoloRutinas(int idMod, String usuario)
{
    String rut = "";
    String menuR = "";
    DataTable rutinas = CP.consultaRutinas(usuario, idMod);//busca las rutinas
    if (rutinas != null)
    {
        if (rutinas.Rows.Count == 0)
        { rut = ""; }
        else
        {
            rut = "<ul>";
            foreach (DataRow row in rutinas.Rows)
            {
                rut = rut + "<li class=last><a href=" + row["enlaceRut"].ToString() + "><span>" + row["nomRut"].ToString() + "</span></a></li>";
            }
            menuR = rut + "</ul>";
        }
    }
    return menuR;
}*/
