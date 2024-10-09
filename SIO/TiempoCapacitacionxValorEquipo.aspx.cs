using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using System.Data;
using Microsoft.Reporting.WebForms;

namespace SIO
{
    public partial class TiempoCapacitacionxValorEquipo : System.Web.UI.Page
    {
        ControlTiempoCapacitaxValorEquipo ctrltiemcapavalo = new ControlTiempoCapacitaxValorEquipo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Obtener_ZonasSiat();
                ctrltiemcapavalo.Listar_ZonaSiat(cboZonasiat);
                ctrltiemcapavalo.Listar_Tiposol(cbotiposol);
                ctrltiemcapavalo.Listar_TipoMoneda(cboMoneda);
                this.CargarReporte();
                //permite solo valores decimales 
                txtRangoMin.Attributes.Add("onKeyPress", " return valideKeyenteros(event,this);");
                txtRangoMax.Attributes.Add("onKeyPress", " return valideKeyenteros(event,this);");
                txtDiasAdicio.Attributes.Add("onKeyPress", " return valideKeyenteros(event,this);");
                txtDias.Attributes.Add("onKeyPress", " return valideKeyenteros(event,this);");
                cboMoneda.Enabled = false;
            }
        }

        //Permite mostrar mensajes de alerta
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        public void Obtener_ZonasSiat()
        {
            grid_ZonaSiat.DataSource = ctrltiemcapavalo.Obtener_ZonasSiat();
            grid_ZonaSiat.DataMember = ctrltiemcapavalo.Obtener_ZonasSiat().Tables[0].ToString();
            grid_ZonaSiat.DataBind();
        }

        public void Obtener_Moneda_diasAcionales()
        {
            DataTable dt;

            if (cboZonasiat.SelectedValue.ToString() == "0" || cbotiposol.SelectedValue.ToString() == "0")
            {
                lbl_ValDiasAdic.Text = "";
                txtDiasAdicio.Text = "";
            }
            else
            {
                if (ctrltiemcapavalo.Obtener_Moneda_diasAcionales(int.Parse(cboZonasiat.SelectedValue), int.Parse(cbotiposol.SelectedValue)).Rows.Count != 0)
                {
                    if (GridConfigvalor.Visible == true)
                    {
                        dt = ctrltiemcapavalo.Obtener_Moneda_diasAcionales(int.Parse(cboZonasiat.SelectedValue), int.Parse(cbotiposol.SelectedValue));
                        double diasadic = double.Parse(dt.Rows[0][1].ToString());
                        cboMoneda.SelectedValue = dt.Rows[0][0].ToString();
                        txtDiasAdicio.Text = diasadic.ToString("");
                    }
                    else
                    {
                        lbl_ValDiasAdic.Text = "";
                        txtDiasAdicio.Text = "";
                    }
                }
                else
                {
                    lbl_ValDiasAdic.Text = "";
                    txtDiasAdicio.Text = "";
                }
            }
        }


        public void Obtener_Moneda()
        {
            DataTable dt;

            if (ctrltiemcapavalo.Obtener_Moneda(int.Parse(cboZonasiat.SelectedValue)).Rows.Count != 0)
            {
                dt = ctrltiemcapavalo.Obtener_Moneda(int.Parse(cboZonasiat.SelectedValue));
                cboMoneda.SelectedValue = dt.Rows[0][0].ToString();
            }
        }


        public void Obtener_ValorMaximoMaterial()
        {
            DataTable dt;
            if (cboZonasiat.SelectedValue.ToString() == "0" || cbotiposol.SelectedValue.ToString() == "0")
            {
                lbl_ValDiasAdic.Text = "";
                txtDiasAdicio.Text = "";
            }
            else
            {
                if (GridConfigvalor.Visible == true)
                {
                    if (ctrltiemcapavalo.Obtener_ValorMaximoMaterial(int.Parse(cboZonasiat.SelectedValue), int.Parse(cbotiposol.SelectedValue)).Rows.Count != 0)
                    {
                        dt = ctrltiemcapavalo.Obtener_ValorMaximoMaterial(int.Parse(cboZonasiat.SelectedValue), int.Parse(cbotiposol.SelectedValue));
                        double valdiasadic = double.Parse(dt.Rows[0][0].ToString());
                        lbl_ValDiasAdic.Text = valdiasadic.ToString("C");
                        txtDiasAdicio.Text = dt.Rows[0][1].ToString();
                    }
                    else
                    {
                        lbl_ValDiasAdic.Text = "";
                        txtDiasAdicio.Text = "";
                    }
                }
                else
                {

                }
            }
        }

        protected void cbotiposol_SelectedIndexChanged(object sender, EventArgs e)
        {
            Obtener_ConfiguracionZona();
            Obtener_ValorMaximoMaterial();
            Obtener_Moneda_diasAcionales();
            Obtener_Moneda();
        }


        public object Listar_TipoMoneda()
        {
            DataSet ds;
            ds = ctrltiemcapavalo.Listar_TipoMoneda();
            return ds;
        }

        public object Listar_TipoOrden()
        {
            DataSet ds;
            ds = ctrltiemcapavalo.Listar_TipoOrden();
            return ds;
        }
        public void Obtener_ConfiguracionZona()
        {
            if (cboZonasiat.SelectedValue.ToString() == "0" || cbotiposol.SelectedValue.ToString() == "0")
            {
                GridConfigvalor.DataSource = null;
                GridConfigvalor.Visible = false;
                lbl_ValDiasAdic.Text = "";
            }
            else
            {
                if (ctrltiemcapavalo.Obtener_ConfiguracionDeZona(int.Parse(cboZonasiat.SelectedValue), int.Parse(cbotiposol.SelectedValue)).Tables[0].Rows.Count != 0)
                {
                    GridConfigvalor.DataSource = ctrltiemcapavalo.Obtener_ConfiguracionDeZona(int.Parse(cboZonasiat.SelectedValue), int.Parse(cbotiposol.SelectedValue));
                    GridConfigvalor.DataMember = ctrltiemcapavalo.Obtener_ConfiguracionDeZona(int.Parse(cboZonasiat.SelectedValue), int.Parse(cbotiposol.SelectedValue)).Tables[0].ToString();
                    GridConfigvalor.Visible = true;
                    GridConfigvalor.Columns[6].Visible = false;
                    GridConfigvalor.DataBind();
                }
                else
                {
                    GridConfigvalor.DataSource = null;
                    GridConfigvalor.Visible = false;
                }
            }
        }

        public void Obtener_ConfiguracionZonaEdit()
        {
            GridConfigvalor.DataSource = ctrltiemcapavalo.Obtener_ConfiguracionDeZonaEdit(int.Parse(cboZonasiat.SelectedValue), int.Parse(cbotiposol.SelectedValue));
            GridConfigvalor.DataMember = ctrltiemcapavalo.Obtener_ConfiguracionDeZonaEdit(int.Parse(cboZonasiat.SelectedValue), int.Parse(cbotiposol.SelectedValue)).ToString();
            GridConfigvalor.DataBind();
        }

        public bool validar_Campos(float CampRanmin, float CampRanmax, float Ranmin, float Ranrmax)
        {
            //dim1rmin = dimension minima que se va a crear
            //dim1rmax = dimension maxima que se va a crear
            //Campdim1min = dimension minima que recupera
            //Campdim1max = dimension maxima que recupera
            bool retorno = false;
            if ((CampRanmin <= Ranmin && Ranmin <= CampRanmax) ||
               (CampRanmin <= Ranrmax && Ranrmax <= CampRanmax))
            {
                retorno = true;
            }
            return retorno;
        }

        protected void cboZonasiat_TextChanged(object sender, EventArgs e)
        {
            GridConfigvalor.EditIndex = -1;
            Obtener_ConfiguracionZona();
            Obtener_Moneda_diasAcionales();
            Obtener_Moneda();
            Limpiar_Campos_Configuracion();
            Obtener_ValorMaximoMaterial();
            lbldesctiposol.Text = "";
            //ctrltiemcapavalo.Listar_TipoMoneda(cboMoneda);
        }

        protected void GridConfigvalor_RowUpdating(object sender, GridViewUpdateEventArgs f)
        {
            string siatRangosId, moneda, valorMin, valorMax, dias, tipoOrden, diasAdic;
            int respuesta;
            DataTable dt;
            bool aprobar = false;
            bool valida = false;

            TextBox txt = new TextBox();
            DropDownList cbo = new DropDownList();

            txt = (TextBox)GridConfigvalor.Rows[f.RowIndex].FindControl("txt_rangosid");
            siatRangosId = txt.Text;
            cbo = (DropDownList)GridConfigvalor.Rows[f.RowIndex].FindControl("cboMoneda");
            moneda = cbo.Text;
            txt = (TextBox)GridConfigvalor.Rows[f.RowIndex].FindControl("Txt_ValorMin");
            valorMin = txt.Text;
            txt = (TextBox)GridConfigvalor.Rows[f.RowIndex].FindControl("Txt_ValorMax");
            valorMax = txt.Text;
            txt = (TextBox)GridConfigvalor.Rows[f.RowIndex].FindControl("txt_Dias");
            dias = txt.Text;
            cbo = (DropDownList)GridConfigvalor.Rows[f.RowIndex].FindControl("cboTipoOrden");
            tipoOrden = cbo.Text;


            int resultado;

            if (!String.IsNullOrEmpty(valorMin) && !String.IsNullOrEmpty(valorMax) && !String.IsNullOrEmpty(dias) && !String.IsNullOrEmpty(txtDiasAdicio.Text))
            {
                if (int.TryParse(valorMin, out resultado))//evalua si el valor en el campo es del tipo correcto
                {
                    if (int.TryParse(valorMax, out resultado))//evalua si el valor en el campo es del tipo correcto
                    {
                        if (int.TryParse(dias, out resultado))//evalua si el valor en el campo es del tipo correcto
                        {
                            if (decimal.Parse(valorMin) <= decimal.Parse(valorMax))
                            {
                                if (dias != "0")
                                {                                
                                if (ctrltiemcapavalo.Obtener_RangosZona(int.Parse(cboZonasiat.SelectedValue), int.Parse(cbotiposol.SelectedValue),int.Parse(siatRangosId)).Rows.Count != 0)
                                {
                                    dt = ctrltiemcapavalo.Obtener_RangosZona(int.Parse(cboZonasiat.SelectedValue), int.Parse(cbotiposol.SelectedValue), int.Parse(siatRangosId));

                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (aprobar == false)
                                        {
                                            if (validar_Campos(float.Parse(dt.Rows[i][0].ToString()), float.Parse(dt.Rows[i][1].ToString()), float.Parse(valorMin), float.Parse(valorMax)) ||
                                                validar_Campos(float.Parse(valorMin), float.Parse(valorMax), float.Parse(dt.Rows[i][0].ToString()), float.Parse(dt.Rows[i][1].ToString()))
                                               )
                                            {
                                                valida = true;
                                            }
                                            else
                                            {
                                                valida = false;
                                            }
                                            //Si aprobar es true, ya no valida mas y se sale del ciclo
                                            if (valida == false)
                                            {
                                                aprobar = false;
                                            }
                                            else
                                            {
                                                aprobar = true;
                                            }
                                        }
                                    }
                                }
                                if (aprobar == false)
                                {
                                        string monedaT, valorMinT, valorMaxT, diasT, tipoOrdenT,zonaT, diasAdicT;
                                        DataTable dt2;                                 
                                       dt2= ctrltiemcapavalo.Consultar_ConfigZona2(int.Parse(siatRangosId));

                                        monedaT = dt2.Rows[0][0].ToString();
                                        diasT = dt2.Rows[0][1].ToString();
                                        valorMinT = dt2.Rows[0][2].ToString();
                                        valorMaxT = dt2.Rows[0][3].ToString();
                                        zonaT = dt2.Rows[0][4].ToString();
                                        tipoOrdenT = dt2.Rows[0][5].ToString();
                                        diasAdicT = dt2.Rows[0][6].ToString();


                                        respuesta = ctrltiemcapavalo.Actualizar_ConfigZona2(int.Parse(moneda), int.Parse(dias), decimal.Parse(valorMin),
                                                                                              decimal.Parse(valorMax),3,int.Parse(txtDiasAdicio.Text), 
                                                                                              int.Parse(cboZonasiat.Text),int.Parse(monedaT), int.Parse(diasT),
                                                                                              decimal.Parse(valorMinT),decimal.Parse(valorMaxT),3,
                                                                                              int.Parse(diasAdicT), int.Parse(zonaT));

                                        respuesta = ctrltiemcapavalo.Actualizar_ConfigZona(int.Parse(moneda), int.Parse(dias), decimal.Parse(valorMin),
                                                                                               decimal.Parse(valorMax), int.Parse(tipoOrden),
                                                                                               int.Parse(txtDiasAdicio.Text), int.Parse(siatRangosId));
                                    if (respuesta == 1)
                                    {
                                        GridConfigvalor.EditIndex = -1;
                                        Obtener_ConfiguracionZonaEdit();
                                        mensajeVentana("Registro Actualizado Correctamente");
                                    }
                                    else
                                    {
                                        mensajeVentana("No se pudo actualizar correctamente la configuracion");
                                    }
                                }
                                else
                                {
                                    mensajeVentana("Valide que los valores para actualizar, no se cruzen con los existententes");
                                }

                                }
                                else
                                {
                                    mensajeVentana("Los dias no pueden ser menores a 1");                                   
                                }
                            }
                            else
                            {
                                mensajeVentana("El valor minimo debe ser mayor a el valor maximo ");
                            }
                        }
                        else
                        {
                            mensajeVentana("El tipo de dato en el campo (dias) es incorrecto");
                        }
                    }
                    else
                    {
                        mensajeVentana("El tipo de dato en el campo (Valor Maximo) es incorrecto");
                    }
                }
                else
                {
                    mensajeVentana("El tipo de dato en el campo (Valor Minimo) es incorrecto");
                }
            }
            else
            {
                mensajeVentana("Valide que los campos Valor Minimo, Valor Maximo, Dias y Dias Adicionales contengan un valor");
            }

        }

        protected void GridConfigvalor_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridConfigvalor.SelectedIndex = -1;
            GridConfigvalor.EditIndex = e.NewEditIndex;
            Obtener_ConfiguracionZonaEdit();
        }



        protected void btnAgregar_Click1(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDescriZona.Text))
            {
                //Pasa el texto en mayuscula
                txtDescriZona.Text = txtDescriZona.Text.ToUpper();

                ctrltiemcapavalo.Crear_Zona(txtDescriZona.Text);
                Obtener_ZonasSiat();
                cboZonasiat.Items.Clear();
                ctrltiemcapavalo.Listar_ZonaSiat(cboZonasiat);
                txtDescriZona.Text = "";
                mensajeVentana("La zona se creo correctamente");
            }
            else
            {
                mensajeVentana("Debe dijitar la descripcion de la zona");
                txtDescriZona.Focus();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int respuesta;
            DataTable dt;
            bool aprobar = false;
            bool valida = false;
            GridConfigvalor.EditIndex = -1;
            Obtener_ConfiguracionZonaEdit();

            if ((int.Parse(cboZonasiat.SelectedValue) != 0) && (int.Parse(cbotiposol.SelectedValue) != 0) && (int.Parse(cboMoneda.SelectedValue) != 0))
            {
                if (!string.IsNullOrEmpty(txtDiasAdicio.Text))
                {
                    if (!string.IsNullOrEmpty(txtDias.Text))
                    {
                        if (!string.IsNullOrEmpty(txtRangoMin.Text))
                        {
                            if (!string.IsNullOrEmpty(txtRangoMax.Text))
                            {
                                if (decimal.Parse(txtRangoMin.Text) <= decimal.Parse(txtRangoMax.Text))
                                {
                                    if (txtDias.Text != "0")
                                    {                                    
                                    if (ctrltiemcapavalo.Obtener_RangosZona(int.Parse(cboZonasiat.SelectedValue), int.Parse(cbotiposol.SelectedValue)).Rows.Count != 0)
                                    {
                                        dt = ctrltiemcapavalo.Obtener_RangosZona(int.Parse(cboZonasiat.SelectedValue), int.Parse(cbotiposol.SelectedValue));

                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            if (aprobar == false)
                                            {
                                                if (validar_Campos(float.Parse(dt.Rows[i][0].ToString()), float.Parse(dt.Rows[i][1].ToString()), float.Parse(txtRangoMin.Text), float.Parse(txtRangoMax.Text)) ||
                                                    validar_Campos(float.Parse(txtRangoMin.Text), float.Parse(txtRangoMax.Text), float.Parse(dt.Rows[i][0].ToString()), float.Parse(dt.Rows[i][1].ToString()))
                                                   )
                                                {
                                                    valida = true;
                                                }
                                                else
                                                {
                                                    valida = false;
                                                }
                                                //Si aprobar es true, ya no valida mas y se sale del ciclo
                                                if (valida == false)
                                                {
                                                    aprobar = false;
                                                }
                                                else
                                                {
                                                    aprobar = true;
                                                }
                                            }
                                        }
                                    }
                                    if (aprobar == false)
                                    {
                                        if (int.Parse(cbotiposol.SelectedValue) == 3 || int.Parse(cbotiposol.SelectedValue) == 7)
                                        {
                                            respuesta = ctrltiemcapavalo.Crear_ConfiguracionZona(int.Parse(cboMoneda.SelectedValue), int.Parse(txtDias.Text), decimal.Parse(txtRangoMin.Text),
                                                                decimal.Parse(txtRangoMax.Text), 1, int.Parse(cboZonasiat.SelectedValue),
                                                                3, int.Parse(txtDiasAdicio.Text));

                                            respuesta = ctrltiemcapavalo.Crear_ConfiguracionZona(int.Parse(cboMoneda.SelectedValue), int.Parse(txtDias.Text), decimal.Parse(txtRangoMin.Text),
                                                                decimal.Parse(txtRangoMax.Text), 1, int.Parse(cboZonasiat.SelectedValue),
                                                               7, int.Parse(txtDiasAdicio.Text));
                                        }
                                        else
                                        {
                                            respuesta = ctrltiemcapavalo.Crear_ConfiguracionZona(int.Parse(cboMoneda.SelectedValue), int.Parse(txtDias.Text), decimal.Parse(txtRangoMin.Text),
                                                              decimal.Parse(txtRangoMax.Text), 1, int.Parse(cboZonasiat.SelectedValue),
                                                              int.Parse(cbotiposol.SelectedValue), int.Parse(txtDiasAdicio.Text));
                                        }
                                        if (respuesta == 1)
                                        {
                                            Limpiar_Campos_Configuracion();
                                            Obtener_ConfiguracionZona();
                                            Obtener_ValorMaximoMaterial();
                                            Obtener_Moneda_diasAcionales();
                                            this.CargarReporte();
                                            mensajeVentana("Se creo correctamente la configuracion");
                                        }
                                        else
                                        {
                                            mensajeVentana("Error al intentar crear la configuracion");
                                        }
                                    }
                                    else
                                    {
                                        mensajeVentana("Valide que los valores a crear, no se cruzen con los existententes");
                                    }

                                    }
                                    else
                                    {
                                        mensajeVentana("Los dias no pueden ser menores a 1");
                                        txtDias.Focus();
                                    }
                                }
                                else
                                {
                                    mensajeVentana("El valor minimo debe ser mayor a el valor maximo ");
                                    txtRangoMin.Focus();
                                }
                            }
                            else
                            {
                                mensajeVentana("Debe dijitar un valor ");
                                txtRangoMax.Focus();
                            }
                        }
                        else
                        {
                            mensajeVentana("Debe dijitar un valor ");
                            txtRangoMin.Focus();
                        }
                    }
                    else
                    {
                        mensajeVentana("Debe dijitar un numero de dias ");
                        txtDias.Focus();
                    }
                }
                else
                {
                    mensajeVentana("Debe digitar el numero de dias adicionales");
                    txtDiasAdicio.Focus();
                }
            }
            else
            {
                mensajeVentana("Debe selecionar una zona, moneda y un tipo de orden");
            }
        }

        public void Limpiar_Campos_Configuracion()
        {
            txtDias.Text = "";
            txtDiasAdicio.Text = "";
            txtRangoMax.Text = "";
            txtRangoMin.Text = "";
        }



        protected void GridConfigvalor_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridConfigvalor.EditIndex = -1;
            Obtener_ConfiguracionZona();
        }

        private void CargarReporte()
        {
            ReportViewer2.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReportViewer2.ServerReport.ReportServerCredentials = irsc;
            ReportViewer2.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReportViewer2.ServerReport.ReportPath = "/Comercial/COM_TiempoCapacitacionValorEquipo";
            // this.ReportViewer2.ServerReport.SetParameters(parametro);
        }


        protected void GridConfigvalor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridConfigvalor.EditIndex = -1;

            if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int siatrangosid = Convert.ToInt32(GridConfigvalor.DataKeys[index].Value);
                ctrltiemcapavalo.Anular_ConfiguracionZona(siatrangosid);
                GridConfigvalor.EditIndex = -1;
                Obtener_ConfiguracionZonaEdit();
            }
        }
    
        /*Este metodo recarga la informacion de la grilla despues de que se anule una configuracion, ya que con el rowcommand solo anula,
         por lo que estos dos metodos funcionan unificadamente*/
        protected void GridConfigvalor_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {          
            GridConfigvalor.EditIndex = -1;
            Obtener_ConfiguracionZonaEdit();
            mensajeVentana("Configuracion anulada correctamente");
        }

    
    }
}