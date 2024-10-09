using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using CapaControl;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


namespace SIO
{
    public partial class MobilCotizacion : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        private DataSet dsCotizacionPreliminar = new DataSet();
        public ControlUbicacion contubi = new ControlUbicacion();
        public ControlCotizacionPreliminar contcp = new ControlCotizacionPreliminar();
        public ControlCliente concli = new ControlCliente();
        public ControlInicio controlInicio = new ControlInicio();
                
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CargarReporteOrdenes();                
            }
        }

        public void CargarReporteOrdenes()
        {
            string orden = "";

            if (Request.QueryString["orden"] != null)
            {
                orden = Request.QueryString["orden"];               
            }

            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("orden", orden, true));

            //// Creo uno o varios parámetros de tipo ReportParameter con sus valores.
            //ReportParameter parametro = new ReportParameter("fup", fup);
            //// Añado uno o varios parámetros(En este caso solo uno al ReportViewer
            //this.ReporteOrden.ServerReport.SetParameters(new ReportParameter [] {parametro});

            //string idfp = cboOfs.SelectedItem.Value.ToString();
            this.ReporteOrden.KeepSessionAlive = true;
            this.ReporteOrden.AsyncRendering = true;
            ReporteOrden.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReporteOrden.ServerReport.ReportServerCredentials = irsc;
            ReporteOrden.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReporteOrden.ServerReport.ReportPath = "/Produccion/Orden";

            this.ReporteOrden.ServerReport.SetParameters(parametro);
            ReporteOrden.ShowToolBar = true;
            
        }

        public class CustomReportCredentials : IReportServerCredentials
        {
            private string _UserName;
            private string _PassWord;
            private string _DomainName;
            public CustomReportCredentials(string UserName, string PassWord, string DomainName)
            {
                _UserName = UserName;
                _PassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }
            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
            }
            public bool GetFormsCredentials
                (
                out Cookie authCookie,
                out string user,
                out string password,
                out string authority
                )
            { authCookie = null; user = password = authority = null; return false; }
        }


        //private void PoblarListaOf()
        //{
           
        //    cboOfs.Items.Clear();
        //    reader = contubi.poblarListaOfs();
        //    string idioma = (string)Session["Idioma"];

        //    cboOfs.Items.Add(new ListItem("Orden..","0"));
            
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            cboOfs.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(1).ToString()));
        //        }
        //    }
             
        //    reader.Close();
        //    contubi.cerrarConexion();
        //}

        protected void cboOfs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string idfp = cboOfs.SelectedItem.Value.ToString();

            //if (idfp != "0")
            //{
            //    this.CargarReporteOrdenes(idfp);
            //    ReporteOrden.Visible = true;
            //}
            //else
            //{
            //    ReporteOrden.Visible = false;
            //}
        }

        //private void PoblarListaPais2()
        //{
        //    CboPais.Items.Clear();

        //    reader = contubi.poblarListaPais();
        //    string idioma = (string)Session["Idioma"];
        //    if (idioma == "Español")
        //    {
        //        CboPais.Items.Add("Seleccione El Pais");
        //    }
        //    if (idioma == "Ingles")
        //    {
        //        CboPais.Items.Add("Select The Country");
        //    }
        //    if (idioma == "Portugues")
        //    {
        //        CboPais.Items.Add("Selecione O País");
        //    }

        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            CboPais.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
        //        }
        //    }
        //    else
        //    {
        //        string mensaje = "Usted no posee paises asociados.";
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                
        //    }

        //    reader.Close();
        //    contubi.cerrarConexion();
        //}
        ////CARGAMOS LOS DATOS DEL REPRESENTANTE
        //private void CargarDatosRep()
        //{
        //    string idioma = (string)Session["Idioma"];
        //    string usuconect = (string)Session["Usuario"];
        //    int rol = (int)Session["Rol"];
        //    string rcID = (string)Session["rcID"];
        //    int idusuario = 0;
        //    string nomUsuario = "";
        //    string correoUsu = "";
        //    string area = "0";

        //    if ((rol == 3) || (rol == 9) || (rol == 2) || (rol == 28))
        //    {
        //        reader = controlInicio.ObtenerRepresentante(usuconect);
        //        reader.Read();

        //        nomUsuario = reader.GetValue(1).ToString();
        //        rcID = reader.GetValue(0).ToString();
        //        correoUsu = reader.GetValue(2).ToString();
        //        idusuario = Convert.ToInt32(reader.GetValue(5).ToString());
        //        reader.Close();
        //    }
        //    else
        //    {
        //        reader = controlInicio.consultarNombre(usuconect);
        //        reader.Read();

        //        nomUsuario = reader.GetValue(0).ToString();
        //        correoUsu = reader.GetValue(1).ToString();
        //        area = reader.GetValue(2).ToString();
        //        idusuario = Convert.ToInt32(reader.GetValue(3).ToString());
        //        reader.Close();
        //    }

        //    reader.Close();

        //    //CARGA LAS VARIABLES DE SESION PARA UTILIZARLAS EN TODAS LAS PAGINAS
        //    Session["Usuario"] = usuconect;
        //    Session["Nombre_Usuario"] = nomUsuario;
        //    Session["rcID"] = rcID;
        //    Session["rcEmail"] = correoUsu;
        //    Session["Rol"] = rol;
        //    Session["Area"] = area;
        //    Session["idUsuario"] = idusuario;
        //}

        //private void PoblarTipoProyecto()
        //{
        //    string idioma = (string)Session["Idioma"];
        //    cboTipo.Items.Clear();
            
        //    reader = contcp.ConsultarTipoProyecto();            
        //    if (idioma == "Español")
        //    {
        //        cboTipo.Items.Add("Seleccione");
        //        if (reader.HasRows == true)
        //        {
        //            while (reader.Read())
        //            {
        //                cboTipo.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
        //            }
        //        }                
        //    }
        //    if (idioma == "Ingles")
        //    {
        //        cboTipo.Items.Add("Select");
        //        if (reader.HasRows == true)
        //        {
        //            while (reader.Read())
        //            {
        //                cboTipo.Items.Add(new ListItem(reader.GetString(2), reader.GetInt32(0).ToString()));
        //            }
        //        }
        //    }
        //    if (idioma == "Portugues")
        //    {
        //        cboTipo.Items.Add("Selecione");
        //        if (reader.HasRows == true)
        //        {
        //            while (reader.Read())
        //            {
        //                cboTipo.Items.Add(new ListItem(reader.GetString(3), reader.GetInt32(0).ToString()));
        //            }
        //        }
        //    }
        //}

        //private void Idioma()
        //{
        //    int idiomaId = 0;
        //    string idioma = (string)Session["Idioma"];
        //    if (idioma == "Español")
        //        idiomaId = 2;
        //    if (idioma == "Ingles")
        //        idiomaId = 3;
        //    if (idioma == "Portugues")
        //        idiomaId = 4;

        //    int posicion = 0;

        //    dsCotizacionPreliminar = contcp.ConsultarIdiomaCotizacionPreliminar();

        //    foreach (DataRow fila in dsCotizacionPreliminar.Tables[0].Rows)
        //    {
        //        posicion = posicion + 1;
        //        if (posicion == 1)
        //            lblPais.Text = fila[idiomaId].ToString();
        //        if (posicion == 2)
        //            lblCiudad.Text = fila[idiomaId].ToString();
        //        if (posicion == 3)
        //            lblCliente.Text = fila[idiomaId].ToString();
        //        if (posicion == 4)
        //            lblContacto.Text = fila[idiomaId].ToString();
        //        if (posicion == 5)
        //            lblObra.Text = fila[idiomaId].ToString();
        //        if (posicion == 6)
        //            lblTipo.Text = fila[idiomaId].ToString();
        //        if (posicion == 7)
        //            lblArea.Text = fila[idiomaId].ToString();
        //        if (posicion == 8)
        //            lblAdaptaciones.Text = fila[idiomaId].ToString();
        //        if (posicion == 9)
        //            btnGuardar.Text = fila[idiomaId].ToString();
        //        if (posicion == 10)
        //            lblCalPrem.Text = fila[idiomaId].ToString();                
        //        if (posicion == 11)
        //            lblvrm.Text = fila[idiomaId].ToString();
        //        if (posicion == 12)
        //            lblVrEquip1.Text = fila[idiomaId].ToString();
        //        if (posicion == 13)
        //            btnSalvar.Text = fila[idiomaId].ToString();
        //        if (posicion == 14)
        //            lblCond.Text = fila[idiomaId].ToString();
        //        if (posicion == 15)
        //            lblCond1.Text = fila[idiomaId].ToString();
        //        if (posicion == 16)
        //            btnNuevo.Text = fila[idiomaId].ToString();
        //        if (posicion == 17)
        //            lblRef.Text = fila[idiomaId].ToString();
        //        if (posicion == 18)
        //            lblFormaleta.Text = fila[idiomaId].ToString();
        //        if (posicion == 19)
        //            lblTituloCP.Text = fila[idiomaId].ToString();
        //        if (posicion == 20)
        //            lblClienteCotizado.Text = fila[idiomaId].ToString();
        //        if (posicion == 21)
        //            lblVrEquip2.Text = fila[idiomaId].ToString();
        //        if (posicion == 22)
        //            lblEncabGeneral.Text = fila[idiomaId].ToString();
        //        if (posicion == 23)
        //            lblCotPrem.Text = fila[idiomaId].ToString();
        //    }
        //    dsCotizacionPreliminar.Tables.Remove("Table");
        //    dsCotizacionPreliminar.Dispose();
        //    dsCotizacionPreliminar.Clear();

        //    if (idioma == "Español")
        //    {
        //        cboFormaleta.Items.Clear();
        //        cboFormaleta.Items.Add("ALUMINIO");
        //        cboFormaleta.Items.Add("PLASTICO");

        //        cboRef.Items.Clear();
        //        cboRef.Items.Add("EQUIPO NUEVO");
        //        cboRef.Items.Add("Seleccione");                
        //        cboRef.Items.Add("ADAPTACIÓN");
        //        cboRef.Items.Add("LISTADO");
        //    }
        //    if (idioma == "Ingles")
        //    {
        //        cboFormaleta.Items.Clear();
        //        cboFormaleta.Items.Add("ALUMINIUM");
        //        cboFormaleta.Items.Add("PLASTIC");

        //        cboRef.Items.Clear();
        //        cboRef.Items.Add("NEW EQUIPMENT");
        //        cboRef.Items.Add("Select");                
        //        cboRef.Items.Add("ADAPTATION");
        //        cboRef.Items.Add("LISTING");
        //    }
        //    if (idioma == "Portugues")
        //    {
        //        cboFormaleta.Items.Clear();
        //        cboFormaleta.Items.Add("ALUMINIO");
        //        cboFormaleta.Items.Add("PLASTICO");

        //        cboRef.Items.Clear();
        //        cboRef.Items.Add("EQUIPAMENTO NOVO");
        //        cboRef.Items.Add("Selecione");                
        //        cboRef.Items.Add("ADAPTAÇÃO");
        //        cboRef.Items.Add("LISTA");
        //    }
        //}    

        //protected void btnGuardar_Click(object sender, EventArgs e)
        //{
        //    reader = contcp.ConsultarTipoProyectoSeleccion(Convert.ToInt32(cboTipo.SelectedItem.Value));
        //    reader.Read();
        //    string min = reader.GetValue(1).ToString();
        //    string max = reader.GetValue(2).ToString();
        //    reader.Close();

        //    decimal m2min = contcp.m2min(Convert.ToDecimal(txtArea.Text.Replace(",",".")), Convert.ToDecimal(min), 
        //        Convert.ToInt32(txtAdaptaciones.Text));

        //    lblCP1.Text = Convert.ToString(m2min);

        //    decimal m2max = contcp.m2max(Convert.ToDecimal(txtArea.Text.Replace(",", ".")), Convert.ToDecimal(max), 
        //       Convert.ToInt32(txtAdaptaciones.Text));

        //    lblCP2.Text = Convert.ToString(m2max);

        //    cboRef.SelectedItem.Text = "EQUIPO NUEVO";
        //    //CONSULTAR PRECIO
        //    decimal precio = 0;
        //    if ((cboRef.SelectedItem.Text == "EQUIPO NUEVO") || (cboRef.SelectedItem.Text == "NEW EQUIPMENT") ||
        //        (cboRef.SelectedItem.Text == "EQUIPAMENTO NOVO"))
        //    {
        //        reader = contcp.ConsultarPrecioEquipoNuevo(Convert.ToInt32(CboPais.SelectedItem.Value),
        //            cboFormaleta.SelectedItem.Text);
        //        reader.Read();
        //        precio = reader.GetSqlMoney(0).Value;
        //        //lblvrmt2.Text = Convert.ToString(precio.ToString("#,##.##"));
        //        lblvrmt2.Text = string.Format("{0:c}",precio);
        //        reader.Close();
        //        Session["VrM2"] = precio;
        //    }

        //    if ((cboRef.SelectedItem.Text == "ADAPTACIÓN") || (cboRef.SelectedItem.Text == "ADAPTATION") ||
        //        (cboRef.SelectedItem.Text == "ADAPTAÇÃO"))
        //    {
        //        reader = contcp.ConsultarPrecioAdaptacion(Convert.ToInt32(CboPais.SelectedItem.Value),
        //          cboFormaleta.SelectedItem.Text);
        //        reader.Read();
        //        precio = reader.GetSqlMoney(0).Value;
        //        //lblvrmt2.Text = Convert.ToString(precio.ToString("#,##.##"));
        //        lblvrmt2.Text = string.Format("{0:c}", precio);
        //        reader.Close();
        //        Session["VrM2"] = precio;
        //    }

        //    if ((cboRef.SelectedItem.Text == "LISTADO") || (cboRef.SelectedItem.Text == "LISTING") ||
        //        (cboRef.SelectedItem.Text == "LISTA"))
        //    {
        //        reader = contcp.ConsultarPrecioListado(Convert.ToInt32(CboPais.SelectedItem.Value),
        //          cboFormaleta.SelectedItem.Text);
        //        reader.Read();
        //        precio = reader.GetSqlMoney(0).Value;
        //        //lblvrmt2.Text = Convert.ToString(precio.ToString("#,##.##"));
        //        lblvrmt2.Text = string.Format("{0:c}", precio);
        //        reader.Close();
        //        Session["VrM2"] = precio;
        //    }           

        //    decimal vrmin = contcp.vrm2min(m2min, precio);
        //    //lblVrT1.Text = Convert.ToString(vrmin.ToString("#,##.##"));
        //    lblVrT1.Text = string.Format("{0:c}", vrmin);
        //    Session["VrMin"] = vrmin;

        //    decimal vrmax = contcp.vrm2min(m2max, precio);
        //    //lblVrT2.Text = Convert.ToString(vrmax.ToString("#,##.##"));
        //    lblVrT2.Text = string.Format("{0:c}", vrmax);
        //    Session["VrMax"] = vrmax;

        //    lblClienteCotizado.Text = cboCliente.SelectedItem.Text;
        //    Accordion1.SelectedIndex = 1;
        //}

        //protected void CboPais_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.PoblarCiudad();
        //}

        //private void PoblarCiudad()
        //{
        //    int rol = (int)Session["Rol"];
        //    string rcID = (string)Session["rcID"];
        //    string idioma = (string)Session["Idioma"];

        //    cboCiudad.Items.Clear();
        //    if (idioma == "Español")
        //    {
        //        cboCiudad.Items.Add("Seleccione La Ciudad");
        //    }
        //    if (idioma == "Ingles")
        //    {
        //        cboCiudad.Items.Add("Select The City");
        //    }
        //    if (idioma == "Portugues")
        //    {
        //        cboCiudad.Items.Add("Selecione A Cidade");
        //    }

        //    if ((rol == 3) && (Convert.ToInt32(CboPais.SelectedItem.Value) == 8))
        //    {
        //        reader = contubi.poblarCiudadesRepresentantesColombia(Convert.ToInt32(rcID));

        //        if (reader.HasRows == true)
        //        {
        //            while (reader.Read())
        //            {
        //                cboCiudad.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
        //            }
        //        }
        //        reader.Close();
        //    }
        //    else
        //    {
        //        reader = contubi.poblarListaCiudades(Convert.ToInt32(CboPais.SelectedItem.Value));
        //        if (reader.HasRows == true)
        //        {
        //            while (reader.Read())
        //            {
        //                cboCiudad.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
        //            }
        //        }
        //    }
        //}

        //protected void cboCiudad_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.PoblarCliente();
        //}

        //private void PoblarCliente()
        //{
        //    string idioma = (string)Session["Idioma"];

        //    cboCliente.Items.Clear();
        //    reader = concli.ConsultarDatosCliente(Convert.ToInt32(cboCiudad.SelectedItem.Value));
        //    if (idioma == "Español")
        //    {
        //        cboCliente.Items.Add("Seleccione Cliente");
        //    }
        //    if (idioma == "Ingles")
        //    {
        //        cboCliente.Items.Add("Select Customer");
        //    }
        //    if (idioma == "Portugues")
        //    {
        //        cboCliente.Items.Add("Selecione Cliente");
        //    }
        //    if (reader.HasRows == true)
        //    {
        //        while (reader.Read())
        //        {
        //            cboCliente.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
        //        }
        //    }
        //}

        //protected void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.PoblarContacto();
        //    this.PoblarObra();
        //}

        //private void PoblarContacto()
        //{
        //    if (cboCliente.SelectedItem.Text == "Seleccione El Cliente")
        //    {
        //        cboContacto.Items.Clear();
        //    }
        //    else
        //    {

        //        reader = contcp.ObtenerContacto(Convert.ToInt32(cboCliente.SelectedValue));
        //        cboContacto.Items.Clear();
        //        cboContacto.Items.Add("Seleccione El Contacto");
        //        while (reader.Read())
        //        {
        //            string nombre_contacto = reader.GetValue(0).ToString();
        //            cboContacto.Items.Add(new ListItem(nombre_contacto, reader.GetInt32(1).ToString()));
        //        }
        //        reader.Close();
        //        contcp.cerrarConexion();
        //    }
        //}

        //private void PoblarObra()
        //{
        //    if (cboCliente.SelectedItem.Text == "Seleccione El Cliente")
        //    {
        //        cboObra.Items.Clear();
        //    }
        //    else
        //    {
        //        reader = contcp.ObtenerObra(Convert.ToInt32(cboCliente.SelectedValue));
        //        cboObra.Items.Clear();
        //        cboObra.Items.Add("Seleccione La Obra");
        //        while (reader.Read())
        //        {
        //            cboObra.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));

        //        }
        //        reader.Close();
        //        contcp.cerrarConexion();
        //    }
        //}

        //protected void btnNuevo_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("MobilCotizacion.aspx");
        //}

        //protected void btnSalvar_Click(object sender, EventArgs e)
        //{
        //    btnSalvar.Enabled = false;
        //    string mensaje = "";           
        //    string Representante = (string)Session["Nombre_Usuario"];
        //    string idioma = (string)Session["Idioma"];

        //    int moneda = 0;
        //    if (CboPais.SelectedItem.Value == "8")
        //    {
        //        moneda = 1;
        //    }
        //    else
        //    {
        //        moneda = 2;
        //    }

        //    int idusuario = (int)Session["idUsuario"];
        //    //CREAMOS EL NUMERO DE FUP
        //    string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
        //    int fup = contcp.fup(fecha, Convert.ToInt32(cboCliente.SelectedItem.Value), moneda,
        //        Convert.ToInt32(cboContacto.SelectedValue), Convert.ToInt32(cboObra.SelectedValue), "DT",
        //        Representante);

        //    Session["FUP"] = fup;

        //    reader = contcp.ConsultarTipoProyectoSeleccion(Convert.ToInt32(cboTipo.SelectedItem.Value));
        //    reader.Read();
        //    string min = reader.GetValue(1).ToString();
        //    string max = reader.GetValue(2).ToString();
        //    reader.Close();

        //    bool equiponuevo = false, adap = false, listado = false, alum = false, plast = false;
        //    if ((cboRef.SelectedItem.Text == "EQUIPO NUEVO") || (cboRef.SelectedItem.Text == "NEW EQUIPMENT") ||
        //        (cboRef.SelectedItem.Text == "EQUIPAMENTO NOVO"))
        //    {
        //        equiponuevo = true;
        //    }
        //    else
        //    {
        //        equiponuevo = false;
        //    }

        //    if ((cboRef.SelectedItem.Text == "ADAPTACIÓN") || (cboRef.SelectedItem.Text == "ADAPTATION") ||
        //        (cboRef.SelectedItem.Text == "ADAPTAÇÃO"))
        //    {
        //        adap = true;
        //    }
        //    else
        //    {
        //        adap = false;
        //    }

        //    if ((cboRef.SelectedItem.Text == "LISTADO") || (cboRef.SelectedItem.Text == "LISTINGT") ||
        //        (cboRef.SelectedItem.Text == "LISTA"))
        //    {
        //        listado = true;
        //    }
        //    else
        //    {
        //        listado = false;
        //    }

        //    if ((cboFormaleta.SelectedItem.Text ==  "ALUMINIO") || (cboFormaleta.SelectedItem.Text == "ALUMINIUM") ||
        //        (cboFormaleta.SelectedItem.Text == "ALUMINIO"))
        //    {
        //       alum = true;
        //    }
        //    else
        //    {
        //        alum = false;
        //    }

        //    if ((cboFormaleta.SelectedItem.Text == "PLASTICO") || (cboFormaleta.SelectedItem.Text == "PLASTIC") ||
        //        (cboFormaleta.SelectedItem.Text == "PLASTICO"))
        //    {
        //        plast = true;
        //    } 
        //    else
        //    {
        //        plast = false;
        //    }

        //    decimal vrmin = (decimal)Session["VrMin"];
        //    decimal vrmax = (decimal)Session["VrMax"];
        //    decimal vrm2 = (decimal)Session["VrM2"];

        //    //INGRESAMOS LA COTIZACION PRELIMINAR
        //    int cp = contcp.cotizacion_preliminar(fup, Convert.ToDecimal(min), Convert.ToDecimal(max),
        //        Convert.ToDecimal(txtArea.Text), Convert.ToInt32(txtAdaptaciones.Text), Convert.ToDecimal(lblCP1.Text),
        //        Convert.ToDecimal(lblCP2.Text), vrm2, Convert.ToString(vrmin),
        //        Convert.ToString(vrmax), cboTipo.SelectedItem.Text, equiponuevo, adap, listado, plast, alum,
        //        Representante, fecha,idusuario);

            
        //    if (idioma == "Español")
        //    {
        //        mensaje = "Cotización preliminar ingresada con éxito.";
        //    }
        //    if (idioma == "Ingles")
        //    {
        //        mensaje = "Successfully entered preliminary quote.";
        //    }
        //    if (idioma == "Portugues")
        //    {
        //        mensaje = "Inseridas com êxito orçamento preliminar.";
        //    }
            
        //    this.Correo();
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);             
            
        //}

        //private void Correo()
        //{
        //    reader = contcp.ObtenerComercial(Convert.ToInt32(CboPais.SelectedItem.Value));
        //    if (reader.Read() == false)
        //    {
        //        string mensaje = "El pais seleccionado no tiene asignado ningun comercial.";
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                
        //    }
        //    else
        //    {
        //        string acEmail = "";
        //        reader = contcp.ObtenerComercial(Convert.ToInt32(CboPais.SelectedItem.Value));           
        //        while (reader.Read())
        //        {
        //            if (acEmail == "")
        //            {
        //                acEmail = reader.GetValue(2).ToString();
        //                Session["acEmail"] = acEmail;
        //            }
        //            else
        //            {
        //                acEmail = acEmail + "," + reader.GetValue(2).ToString();
        //                Session["acEmail"] = acEmail;
        //            }
        //        }
        //        reader.Close();
                
        //        int fup = (int)Session["FUP"];
        //        string Representante = (string)Session["Nombre_Usuario"];

        //        WebClient clienteWeb = new WebClient();
        //        clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");
        //        Byte[] correo = clienteWeb.DownloadData(@"http://10.75.131.2:81/ReportServer?/InformesCRM/COM_CotizacionPreliminar&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + fup + "");
        //        MemoryStream ms = new MemoryStream(correo);

        //        string sujeto, solucion, cuerpo;
        //        string rcEmail = (string)Session["rcEmail"];
        //        string EmailCita = (string)Session["acEmail"];

        //        //CONSULTAMOS EL EMAIL DEL CONTACTO
        //        reader = contcp.ConsultarMailContacto(Convert.ToInt32(cboContacto.SelectedItem.Value));
        //        reader.Read();
        //        string ContactoMail = reader.GetValue(0).ToString();
        //        reader.Close();

        //        string CorreoDestinatario = "";
        //        if (ContactoMail == "")
        //        {
        //            CorreoDestinatario = txtCorreo.Text;
        //        }
        //        else
        //        {
        //            if ((ContactoMail != "") && (txtCorreo.Text == ""))
        //            {
        //                CorreoDestinatario = ContactoMail;
        //            }
        //            else
        //            {
        //                CorreoDestinatario = ContactoMail + "," + txtCorreo.Text;
        //            }
        //        }
        //        string correoSistema = (string)Session["CorreoSistema"];
        //        string UsuarioAsunto = (string)Session["UsuarioAsunto"];
        //        string CopiaRepresentante = rcEmail + "," + acEmail;

        //        //DEFINIMOS LA CLASE DE MAILMESSAGE
        //        MailMessage mail = new MailMessage();
        //        //INDICAMOS EL EMAIL DE ORIGEN
        //        mail.From = new MailAddress(correoSistema);
        //        //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
        //        mail.To.Add(CorreoDestinatario);
        //        //AÑADIMOS COPIA AL REPRESENTANTE
        //        mail.CC.Add(CopiaRepresentante);
        //        //AÑADIMOS LA DIRRECCIÓN DE COPIA AL ADMINISTRADOR DEL APLICATIVO
        //        mail.Bcc.Add("andressuarez@forsa.com.co, ivanvidal@forsa.com.co");
        //        //INCLUIMOS EL ASUNTO DEL MENSAJE
        //        sujeto = "Cotización Preliminar. Proyecto " + cboObra.SelectedItem.Text + " Cliente " + cboCliente.SelectedItem.Text + " ";
        //        mail.Subject = sujeto;
        //        //AÑADIMOS EL CUERPO DEL MENSAJE
        //        solucion = "Representante. " + Representante + "\n" + "FUP No. " + fup + " \n" + "SitioWeb CRM. http://app.forsa.com.co/comercial/MobilInicio.aspx";
        //        cuerpo = "Nos complace informar que se ha ingresado la cotización preliminar correspondiente al proyecto " + cboObra.SelectedItem.Text + ", " +
        //        " Cliente " + cboCliente.SelectedItem.Text + " Pais " + CboPais.SelectedItem.Text + " Ciudad " + cboCiudad.SelectedItem.Text +
        //        " Contacto " + cboContacto.SelectedItem.Text + " \n\n\n" +
        //        "Cordiamente, " + "\n\n" + "Gestión Informática.";
        //        mail.Body = solucion + "\n\n" + cuerpo;
        //        //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
        //        mail.BodyEncoding = System.Text.Encoding.UTF8;
        //        //DEFINIMOS LA PRIORIDAD DEL MENSAJE
        //        mail.Priority = System.Net.Mail.MailPriority.Normal;
        //        //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
        //        mail.IsBodyHtml = false;
        //        mail.Attachments.Add(new Attachment(ms, "CotizacionPreliminar" + fup + ".pdf"));
        //        //DECLARAMOS LA CLASE SMTPCLIENT
        //        SmtpClient smtp = new SmtpClient();
        //        //DEFINIMOS NUESTRO SERVIDOR SMTP
        //        smtp.Host = "smtp.office365.com";
        //        //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
        //        smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
        //        smtp.Port = 25;
        //        smtp.EnableSsl = true;

        //        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
        //        SslPolicyErrors sslPolicyErrors)
        //        {
        //            return true;
        //        };
        //        try
        //        {
        //            smtp.Send(mail);
        //        }
        //        catch (Exception ex)
        //        {
        //            string mensaje = "ERROR: " + ex.Message;
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                                   
        //        }
        //    }
        //}
    }
}