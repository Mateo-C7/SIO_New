using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using System.Data;


namespace SIO
{
    public partial class Pais : System.Web.UI.Page
    {
        CapaControl.ControlPais ctrlPais = new ControlPais();
        CapaControl.ControlZona crtlzona = new ControlZona();
        CapaControl.ControlGrupoPais crtlgrupopais = new ControlGrupoPais();
        CapaControl.ControlMonedaPais crtlmoneda = new ControlMonedaPais();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarPaises();
                pnlCamposDetaPias.Visible = false;
                btn_CancelarDetaPais.Visible = false;
                btn_GuardarDetaPais.Visible = false;
                //obtiene el nombre de usuario y lo establece en un label
                lblusu.Text = (string)Session["Nombre_Usuario"];
            }
        }
        // llena combo pais
        public object ObtenerPais()
        {
            DataSet ds;
            ds = ctrlPais.ObtenerPais();
            return ds;
        }

        //obtiene la zona de un pais
        public object ObtenerZona()
        {
            DataSet ds;
            ds = crtlzona.Obtenerzonas();
            return ds;
        }

        //obtiene la moneda de un pais
        public object ObtenerMoneda()
        {
            DataSet ds;
            ds = crtlmoneda.ObtenerMoneda();
            return ds;
        }

        //obtiene el grupod de un pais
        public object ObtenerGrupoPais()
        {
            DataSet ds;
            ds = crtlgrupopais.ObtenerGrupoPais();
            return ds;
        }

        //Llena el gridview con los paises y las zonas
        public void ListarPaises()
        {
            GridView_Pais.DataSource = ctrlPais.ListarPaises();
            GridView_Pais.DataMember = ctrlPais.ListarPaises().ToString();
            GridView_Pais.DataBind();
        }

        //Index de la pagina
        protected void GridView_Pais_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_Pais.EditIndex = -1;
            {
                ListarPaises();
                GridView_Pais.PageIndex = e.NewPageIndex;
                this.GridView_Pais.DataBind();
            }
        }

        //Habilita el gridview para editar campos
        protected void GridView_Pais_RowEditing1(object sender, GridViewEditEventArgs e)
        {
            GridView_Pais.Columns[12].Visible = false;
            btn_Habilitar_pnlcampdetapais.Visible = false;
            GridView_Pais.EditIndex = e.NewEditIndex;
            ListarPaises();
        }
        //cancela la edicion en el gridview
        protected void GridView_Pais_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_Pais.Columns[12].Visible = true;// cuando se cancela la edicion se muestra la columna eliminar
            btn_Habilitar_pnlcampdetapais.Visible = true;//muestra el boton
            LblMsgTotalItems.Text = "";
            GridView_Pais.EditIndex = -1;
            ListarPaises();
        }

        protected void GridView_Pais_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        //actualiza detalle del pais
        protected void GridView_Pais_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            float porcetaje = 100,sum=0;
            int respuesta;
            string impuesto, zona, moneda, longitud, latitud, zonasiat, idpais;
            string impuesto1, zona1, moneda1, longitud1, latitud1, zonasiat1, idpais1;
            string campimpu, campzona, campmone, camplong, camplati, campzosia;
            String fecha = DateTime.Now.ToShortDateString();
            string tabla = "Pais";
            string evento = "U";

            TextBox txt = new TextBox();

            DropDownList cbo = new DropDownList();
            try
            {       
                 
            txt = (TextBox)GridView_Pais.Rows[e.RowIndex].FindControl("Txt_Impuesto");
            impuesto = txt.Text;      
            cbo = (DropDownList)GridView_Pais.Rows[e.RowIndex].FindControl("cbo_Grup_Pais");
            zona = cbo.Text;
            cbo = (DropDownList)GridView_Pais.Rows[e.RowIndex].FindControl("cbo_Moneda");
            moneda = cbo.Text;
            txt = (TextBox)GridView_Pais.Rows[e.RowIndex].FindControl("Txt_Longitud");
            longitud = txt.Text;
            txt = (TextBox)GridView_Pais.Rows[e.RowIndex].FindControl("Txt_Latitud");
            latitud = txt.Text;
            cbo = (DropDownList)GridView_Pais.Rows[e.RowIndex].FindControl("cbo_Zona");
            zonasiat = cbo.Text;
            txt = (TextBox)GridView_Pais.Rows[e.RowIndex].FindControl("cbo_Pais");
            idpais = txt.Text;

                //Se obtienen los valores de los campos detalle pais en el dt, residentes de la BD
                DataTable dt = null;
                dt = ctrlPais.Met_Consultar_Detalle_Pais(int.Parse(idpais));
                impuesto1 = dt.Rows[0][2].ToString();//valores antiguos para comparar con los ingresados por el usuarioS
                campimpu = dt.Columns[2].ColumnName.ToString();//obtiene el nombre del campo
                zona1 = dt.Rows[0][3].ToString();
                campzona = dt.Columns[3].ColumnName.ToString();//obtiene el nombre del campo
                moneda1 = dt.Rows[0][4].ToString();
                campmone = dt.Columns[4].ColumnName.ToString();//obtiene el nombre del campo
                longitud1 = dt.Rows[0][5].ToString();
                camplong = dt.Columns[5].ColumnName.ToString();//obtiene el nombre del campo
                latitud1 = dt.Rows[0][6].ToString();
                camplati = dt.Columns[6].ColumnName.ToString();//obtiene el nombre del campo
                zonasiat1 = dt.Rows[0][7].ToString();
                campzosia = dt.Columns[7].ColumnName.ToString();//obtiene el nombre del campo 
                                                 
                idpais1 = dt.Rows[0][0].ToString();  //este valor solo es referencia para la insercion    
             
                int resultado;
                float resultado2;
                      
                if(impuesto!="" && longitud != "" && latitud != "")
                {               
                if(float.TryParse(impuesto, out resultado2))
                {
                    if (float.TryParse(longitud, out resultado2))
                    {
                            if (float.TryParse(latitud, out resultado2))
                            {
                                if (float.Parse(impuesto) >= 0 && float.Parse(impuesto) <=30)//evalua si el impuesto se encuentra en dentro del rango
                                {                                                                                 
                                    if (impuesto.Length>2)
                                    {
                                        sum = float.Parse(impuesto) * porcetaje;
                                        sum = sum / porcetaje;                               
                                    }
                                    else
                                    {
                                        sum = float.Parse(impuesto) / porcetaje;
                                    }                
                                        //procedimiento almacenado para insertar el log
                                        //ctrlPais.Ejecutar_ProcAlma_Generar_Log_Pais(int.Parse(idpais1), campimpu, lblusu.Text, fecha, impuesto1, impuesto);

                                        //Compara los valores rescatados de la BD con los digitados por el usuari
                                        if (impuesto1 != sum.ToString())
                                        {
                                            ctrlPais.Met_Insertar_Log_Pais(tabla, int.Parse(idpais1), campimpu, lblusu.Text, fecha, impuesto1, sum.ToString(), evento);
                                        }
                                        else { }
                                        if (zona1 != zona)
                                        {
                                            ctrlPais.Met_Insertar_Log_Pais(tabla, int.Parse(idpais1), campzona, lblusu.Text, fecha, zona1, zona, evento);
                                        }
                                        else { }
                                        if (moneda1 != moneda)
                                        {
                                            ctrlPais.Met_Insertar_Log_Pais(tabla, int.Parse(idpais1), campmone, lblusu.Text, fecha, moneda1, moneda, evento);
                                        }
                                        else { }
                                        if (longitud1 != longitud)
                                        {
                                            ctrlPais.Met_Insertar_Log_Pais(tabla, int.Parse(idpais1), camplong, lblusu.Text, fecha, longitud1, longitud, evento);
                                        }
                                        else { }
                                        if (latitud1 != latitud)
                                        {
                                            ctrlPais.Met_Insertar_Log_Pais(tabla, int.Parse(idpais1), camplati, lblusu.Text, fecha, latitud1, latitud, evento);
                                        }
                                        else { }
                                        if (zonasiat1 != zonasiat)
                                        {
                                            ctrlPais.Met_Insertar_Log_Pais(tabla, int.Parse(idpais1), campzosia, lblusu.Text, fecha, zonasiat1, zonasiat, evento);
                                        }
                                        else { }
                                    
                                    respuesta = ctrlPais.Met_Actualizar_Detalle_Pais(sum, int.Parse(zona), int.Parse(moneda), float.Parse(longitud), float.Parse(latitud), int.Parse(zonasiat), int.Parse(idpais));
                                if (respuesta >= 1)
                                {
                                    GridView_Pais.EditIndex = -1;
                                    LblMsgTotalItems.Text = "Registro Exitoso";
                                    GridView_Pais.Columns[12].Visible = true;
                                    btn_Habilitar_pnlcampdetapais.Visible = true;//muestra el boton
                                    ListarPaises();
                                }
                                else
                                {
                                    LblMsgTotalItems.Text = "Registro fallido";
                                    GridView_Pais.Columns[12].Visible = true;
                                    GridView_Pais.EditIndex = -1;
                                    ListarPaises();                                
                                }
                                }
                                else
                                {
                                    LblMsgTotalItems.Text = "El impuesto no es valido";
                                }
                            }
                        else
                        {
                            LblMsgTotalItems.Text = "El contenido del campo latitud es incorrecto";
                        }
                    }
                        else
                    {
                        LblMsgTotalItems.Text = "El contenido del campo longitud es incorrecto";
                    }
                }
                    else
                {
                   LblMsgTotalItems.Text = "El contenido del campo impuesto es incorrecto";
                }
                }
                else
                {
                    LblMsgTotalItems.Text = "Debe llenar todos los campos";
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                LblMsgRegistroPais.Text = "Debe cambiar los formatos de fecha,hora,moneda,numero para continuar";
            }
        }
                  
        protected void btn_Habilitar_pnlcampdetapais_Click(object sender, EventArgs e)
        {
            pnlCamposDetaPias.Visible = true;
            btn_CancelarDetaPais.Visible = true;
            btn_GuardarDetaPais.Visible = true;
            btn_Habilitar_pnlcampdetapais.Visible = false;
            GridView_Pais.Visible = false;
            LblMsgTotalItems.Text = "";
            Ctxt_Impuesto.Text = "0";
            Ctxt_Latitud.Text = "0";
            Ctxt_Longitud.Text = "0";
            Ctxt_NombrePais.Text = "";
            LLenar_Combos();
        }
        public void Limpiar_CamposPnlDetaaPais()
        {

            Ctxt_Impuesto.Text = "";
            Ctxt_Latitud.Text = "";
            Ctxt_Longitud.Text = "";

        }

        //Llena
        public void LLenar_Combos()
        {
            Ccbo_Moneda.Items.Clear();
            Ccbo_zona_siat.Items.Clear();
            Ccbo_Grup_Pais.Items.Clear();
            //llena combos
            crtlmoneda.Listar_Combo_Moneda(Ccbo_Moneda);
            crtlzona.Listar_Combo_ZonaSiat(Ccbo_zona_siat);
            crtlgrupopais.Listar_Combo_Zona(Ccbo_Grup_Pais);
        }
        //Cancela el poder agregar un nuevo pais
        protected void btn_CancelarDetaPais_Click(object sender, EventArgs e)
        {
            Met_Mostrar_Ocultar_Pnl_Gridview(false);
        }
    
        //oculta o muestra la grilla y el paneldetalle alternativamente
        public void Met_Mostrar_Ocultar_Pnl_Gridview(bool h)
        {
            if (h == true)
            {
                pnlCamposDetaPias.Visible = true;
                btn_CancelarDetaPais.Visible = true;
                btn_GuardarDetaPais.Visible = true;
                btn_Habilitar_pnlcampdetapais.Visible = false;
                GridView_Pais.Visible = false;
                Limpiar_CamposPnlDetaaPais();
                LblMsgRegistroPais.Text = "";
            }
            else
            {
                pnlCamposDetaPias.Visible = false;
                btn_CancelarDetaPais.Visible = false;
                btn_GuardarDetaPais.Visible = false;
                btn_Habilitar_pnlcampdetapais.Visible = true;
                GridView_Pais.Visible = true;
                Limpiar_CamposPnlDetaaPais();
                LblMsgRegistroPais.Text = "";
            }
        }

        //Guarda el detalle del pais
        protected void btn_GuardarDetaPais_Click(object sender, EventArgs e)
        {
            float porcetaje = 100, sum = 0, resultado;
           string cadenalong = Ctxt_Longitud.Text,
            cadenalati = Ctxt_Latitud.Text,
            cadenaimpuesto = Ctxt_Impuesto.Text;
      
            try
            {
                int respuesta;
                if (Ctxt_NombrePais.Text != "" && Ctxt_Impuesto.Text != "" && Ctxt_Longitud.Text != "" && Ctxt_Latitud.Text != "")
                {

                    if (float.TryParse(cadenalong, out resultado))//evalua si el valor en el campo es un float 
                    {
                        if (float.TryParse(cadenalati, out resultado))
                        {
                            if (float.TryParse(cadenaimpuesto, out resultado))
                            {
                                if (ctrlPais.Met_Verificar_Duplicidad_Pais(Ctxt_NombrePais.Text).Rows.Count == 0)
                                {
                                    if(float.Parse(cadenaimpuesto)>=0 && float.Parse(cadenaimpuesto) <= 30)//evalua si el impuesto se encuentra en dentro del rango
                                    {
                                    sum = float.Parse(cadenaimpuesto) / porcetaje;

                                    respuesta = ctrlPais.Met_Crear_Nuevo_Pais(Ctxt_NombrePais.Text, sum, int.Parse(Ccbo_Grup_Pais.Text), int.Parse(Ccbo_Moneda.Text), float.Parse(cadenalong), float.Parse(cadenalati), int.Parse(Ccbo_zona_siat.Text));
                                                             
                                    if (respuesta == 1)
                                    {
                                       LblMsgTotalItems.Text = "Pais creado correctamente";
                                        ListarPaises();
                                        Met_Mostrar_Ocultar_Pnl_Gridview(false);

                                    }
                                    else
                                    {
                                        LblMsgRegistroPais.Text = "Registro Fallido";
                                    }

                                    }
                                    else
                                    {
                                        LblMsgRegistroPais.Text = "El impuesto no es valido";
                                    }
                                }
                                else
                                {
                                    LblMsgRegistroPais.Text = "El pais ya se encuentra registrado";
                                }
                            }
                            else
                            {
                                LblMsgRegistroPais.Text = "El contenido en el campo es incorrecto";
                                Ctxt_Impuesto.Focus();
                            }
                        }
                        else
                        {
                            LblMsgRegistroPais.Text = "El contenido en el campo es incorrecto";
                            Ctxt_Latitud.Focus();
                        }
                    }
                    else
                    {
                        LblMsgRegistroPais.Text = "El contenido en el campo es incorrecto";
                        Ctxt_Longitud.Focus();
                    }
                }
                else
                {
                    LblMsgRegistroPais.Text = "Debe digitar todos los campos";
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                LblMsgRegistroPais.Text = "Debe cambiar los formatos de fecha,hora,moneda,numero para continuar";
            }
        }

        //Elimina un pais con su detalle
        protected void GridView_Pais_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String fecha = DateTime.Now.ToShortDateString();
            string tabla = "Pais";
            string evento = "D";
            int idpai = Convert.ToInt32(GridView_Pais.DataKeys[e.RowIndex].Value);
            try {                              
            ctrlPais.Met_Insertar_Log_Delete_Pais(tabla, idpai,lblusu.Text, fecha,evento);//inserta en la tabla General_log
            ctrlPais.Met_Eliminar_Pais(idpai);
            LblMsgTotalItems.Text = "Pais Eliminado Correctamente";
            ListarPaises();
              }
            catch (System.Data.SqlClient.SqlException)
            {
                LblMsgRegistroPais.Text = "Debe cambiar los formatos de fecha,hora,moneda,numero para continuar";
            }
        }
    }
}
