﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CapaControl;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.Diagnostics;
using System.Threading;

namespace SIO
{
    public partial class CargaEstibas2 : System.Web.UI.Page
    {
        public ControlLogistica CL = new ControlLogistica();
        static List<InfoOrden> linfoCOrd = new List<InfoOrden>();
        static List<InfoContenedor> linfCont = new List<InfoContenedor>();
        static InfoContenedor infco = new InfoContenedor();
        static InfoEstiba infoE = new InfoEstiba();
        protected void Page_Load(object sender, EventArgs e)
        {
            btnAsociar.Visible = false;
            lblMensaje.Text = "";
        }
        protected void Button1_Click(object sender, EventArgs e)//<---------------
        {
            limpiarCampos();
            cboOrden.Items.Clear();
            if (txtContenedor.Text != "")
            {
                String placaNumcont = txtContenedor.Text.ToString();
                Session["numConten"] = placaNumcont;
                linfCont = CL.buscarDatosTrans(placaNumcont);
                cboOrden.Items.Add("Orden");
                if (linfCont.Count > 0)
                {
                    foreach (InfoContenedor linfC in linfCont)
                    {
                        int idG = linfC.Desp_idGrupo;
                        lblResulPlaca1.Text = linfC.TransPlaca;
                        Session["conte1"] = linfC.Desp_Trans_id;
                        Session["idGrupo"] = idG;
                        Session["conteP1"] = linfC.TransPlaca;
                    }
                    infco = linfCont.ElementAt(1 - 1);
                    linfoCOrd = CL.buscarOrdenes(Session["idGrupo"].ToString(), int.Parse(Session["conte1"].ToString()));
                    if (linfoCOrd.Count > 0)
                    {
                        foreach (InfoOrden infoO in linfoCOrd)
                        {
                            cboOrden.Items.Add(new ListItem(infoO.Orden.ToString(), infoO.Idofa.ToString()));
                        }
                    }
                }
                else
                {
                    txtContenedor.Focus();
                    limpiarCampos();
                    cboOrden.Items.Clear();
                    cboOrden.Items.Add("");
                    lblMensaje.Text = ":: La Placa/Contenedor no existe!! ::";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='red'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none';setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtContenedor').focus()}, 1300) }, 3200) } , 100); ", true);
                    txtContenedor.Text = "";
                    txtContenedor.Focus();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtContenedor').focus()}, 1300);", true);
                }
            }
            else
            {
                lblMensaje.Text = "";
                limpiarCampos();
                cboOrden.Items.Clear();
                cboOrden.Items.Add("");
                txtContenedor.Text = "";
                txtContenedor.Focus();
                lblMensaje.Text = ":: Ingrese la Placa/Contenedor por favor!! ::";
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='red'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none';}, 3200) } , 100); ", true);
            }
        }
        public void recargar(int numCont)
        {
            linfCont = CL.buscarDatosTransRecargar(numCont);
            if (linfCont.Count > 0)
            {
                foreach (InfoContenedor linfC in linfCont)
                {
                    Session["conte1"] = linfC.Desp_Trans_id;
                    Session["conteP1"] = linfC.TransPlaca;
                }
                infco = linfCont.ElementAt(1 - 1);
            }
        }
        protected void cboOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiarCampos();
            String rojo = "red";
            String verde = "green";
            btnCA.Text = "";
            lblCA.Text = "";
            linfCont = new List<InfoContenedor>();
            if ("Orden" != cboOrden.SelectedValue)
            {
                Session["Orden3"] = cboOrden.SelectedItem.ToString();
                linfoCOrd = CL.buscarDespachosOr(cboOrden.SelectedValue.ToString());
                txtCodEstiba1.Text = "";
                txtCodEstiba1.Focus();
                int i = 1;
                foreach (InfoOrden info in linfoCOrd)
                {
                    if (i == 1)
                    {
                        lblResulCiudad1.Text = info.Ciudad;
                        lblResulPais1.Text = info.Pais;
                        txtDespacho.Text = info.Despacho;
                        Session["idD"] = info.IdDespacho.ToString();
                        Session["Desp3"] = info.Despacho;
                        Session["conte"] = Session["conte1"].ToString();
                        Session["conteP"] = Session["conteP1"].ToString();
                    }
                    ++i;
                }
                recargar(int.Parse(Session["conte1"].ToString()));
                sumaTotalPeso();
                conteo();
                if (infco.EstadoAC == "True")// true si esta abierto / true = 1
                {
                    txtCodEstiba1.Enabled = true;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_btnCA').style.backgroundColor='" + rojo + "'} , 100);", true);
                    btnCA.Text = "Cerrar Cargue?";
                    lblCA.Text = "Abierto: ";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblCA').style.color='" + verde + "'} , 100);", true);
                }
                else
                { //true si esta abierto / true = 1
                    txtCodEstiba1.Enabled = false;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_btnCA').style.backgroundColor='" + verde + "'} , 100);", true);
                    btnCA.Text = "Abrir Cargue?";
                    lblCA.Text = "Cerrado: ";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblCA').style.color='" + rojo + "'} , 100);", true);
                }
            }
            else
            {
                Session["conteP1"] = "";
                Session["conte1"] = "";
                Session["conteP"] = "0";
                Session["conte"] = "0";
                Session["Desp3"] = "";
                Session["Orden3"] = cboOrden.SelectedItem.ToString();
                txtCodEstiba1.Text = "";
                txtDespacho.Text = "";
                lblResulPlaca1.Text = "";
                lblPalletOrden2.Text = "";
                lblPalletTotalCont2.Text = "";
                lblPalletCargadosCont2.Text = "";
                lblPalletFaltantesCont2.Text = "";
                lblTotaP.Text = "";
                btnCA.Text = "";
                lblCA.Text = "";
                cboOrden.Items.Clear();
                cboOrden.Items.Add("");
                txtContenedor.Text = "";
                txtContenedor.Focus();
            }
        }
        public void limpiarCampos()
        {
            btnCA.Text = "";
            lblCA.Text = "";
            lblTotaP.Text = "";
            txtDespacho.Text = "";
            lblPalletOrden2.Text = "";
            lblPalletTotalCont2.Text = "";
            lblPalletCargadosCont2.Text = "";
            lblPalletFaltantesCont2.Text = "";
            txtCodEstiba1.Text = "";
            lblResulPais1.Text = "";
            lblResulCiudad1.Text = "";
            lblPalletCargadosOrden2.Text = "";
        }
        //--------------------------------Boton invisible para Guardar la estiba en el contenedor------------------------------------------//
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (txtCodEstiba1.Text.ToString().Length == 9)
            {
                String str = txtCodEstiba1.Text.Substring(0, 2);
                ELogcappeso log = CL.buscar_Tamaño("log_str_carga", str);
                InfoOrden infoO = linfoCOrd.ElementAt(1 - 1);
                String idcont = infco.Trans_idContenedor.ToString();
                if (log != null)
                {
                    int pallet = int.Parse(txtCodEstiba1.Text.Substring(2, 7));
                    if (log.tabla.Trim().ToUpper() == "ALUMINIO")
                    {
                        //Probar aluminio 
                        InfoEstiba InfoEst = CL.verificaPalletvsOfaAL(pallet, infoO.Idofa);
                        if (InfoEst != null)
                        {
                            if (InfoEst.Estado == 4)
                            {
                                if (InfoEst.TrasnId == 0)
                                {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "ScriptJorge", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_PanelCarga').style.backgroundColor='rgb(97, 171, 213)'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_PanelCarga').style.backgroundColor='white'},2000) } , 100); ", true);
                                    lblMensaje.Text = ":: Pallet asignada al contenedor!! ::";
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='Lime'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtCodEstiba1').focus()}, 2000); setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none'}, 3200) } , 100); ", true);
                                    CL.actualizarContenedorAluminio(pallet, infco.Desp_Trans_id, infco.Trans_idContenedor);
                                    conteo();
                                    sumaTotalPeso();
                                    txtCodEstiba1.Text = "";
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtCodEstiba1').focus()}, 2000);", true);
                                    txtCodEstiba1.Focus();
                                    recargar(int.Parse(Session["conte1"].ToString()));
                                    String fecha = infco.FechaInicio;
                                    if (fecha != null && fecha != "")
                                    {
                                    }
                                    else
                                    {
                                        CL.actualizarHorasInicio(infco.Desp_Trans_id);
                                    }
                                }
                                else
                                {
                                    lblMensaje.Text = "La Pallet ya se encuentra asociada al contenedor!!";
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='RED'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none'}, 3200) } , 100); ", true);
                                    btnAsociar.Visible = true;
                                    btnAsociar.Focus();
                                }
                            }
                            else
                            {
                                lblMensaje.Text = "Verifique el estado del pallet!!";
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='RED'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none'}, 3200) } , 100); ", true);
                                txtCodEstiba1.Text = "";
                                txtCodEstiba1.Focus();
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtCodEstiba1').focus()}, 1700);", true);
                            }
                        }
                        else
                        {
                            lblMensaje.Text = "Pallet no corresponde a orden !!";
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='RED'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none'}, 3200) } , 100); ", true);
                            txtCodEstiba1.Text = "";
                            txtCodEstiba1.Focus();
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtCodEstiba1').focus()}, 1700);", true);
                        }
                    }
                    else if (log.tabla.Trim().ToUpper() == "ACCESORIOS")
                    {
                        //Probar accesorios
                        InfoEstiba InfoEst = CL.verificaPalletvsOfaACC(pallet, infoO.Idofa);
                        if (InfoEst != null)
                        {
                            if (InfoEst.Estado == 4)
                            {
                                if (InfoEst.TrasnId == 0)
                                {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "ScriptJorge", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_PanelCarga').style.backgroundColor='rgb(97, 171, 213)'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_PanelCarga').style.backgroundColor='white'; setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtCodEstiba1').focus()}, 1300) },2000) } , 100); ", true);
                                    lblMensaje.Text = ":: Pallet asignada al contenedor!! ::";
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='Lime'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtCodEstiba1').focus()}, 2000); setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none'}, 3200) } , 100); ", true);
                                    CL.actualizarContenedorAccesorios(pallet, infco.Desp_Trans_id, infco.Trans_idContenedor);
                                    conteo();
                                    sumaTotalPeso();
                                    txtCodEstiba1.Text = "";
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtCodEstiba1').focus()}, 2000);", true);
                                    txtCodEstiba1.Focus();
                                    recargar(int.Parse(Session["conte1"].ToString()));
                                    String fecha = infco.FechaInicio;
                                    if (fecha != null && fecha != "")
                                    {
                                    }
                                    else
                                    {
                                        CL.actualizarHorasInicio(infco.Desp_Trans_id);
                                    }
                                }
                                else
                                {
                                    lblMensaje.Text = "La Pallet ya se encuentra asociada al contenedor!!";
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='RED'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none'}, 3200) } , 100); ", true);
                                    btnAsociar.Visible = true;
                                    btnAsociar.Focus();
                                }
                            }
                            else
                            {
                                lblMensaje.Text = "Verifique el estado del pallet!!";
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='RED'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none'}, 3200) } , 100); ", true);
                                txtCodEstiba1.Text = "";
                                txtCodEstiba1.Focus();
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtCodEstiba1').focus()}, 1700);", true);
                            }
                        }
                        else
                        {
                            lblMensaje.Text = "Pallet no corresponde a orden !!";
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='RED'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none'}, 3200) } , 100); ", true);
                            txtCodEstiba1.Text = "";
                            txtCodEstiba1.Focus();
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtCodEstiba1').focus()}, 1700);", true);
                        }
                    }
                }
                else
                {
                    lblMensaje.Text = "Pallet invalida !!";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='RED'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none'}, 3200) } , 100); ", true);
                    txtCodEstiba1.Text = "";
                    txtCodEstiba1.Focus();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtCodEstiba1').focus()}, 1700);", true);
                }
            }
            else
            {
                lblMensaje.Text = "Longitud de pallet invalida !!";
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='RED'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none'}, 3200) } , 100); ", true);
                txtCodEstiba1.Text = "";
                txtCodEstiba1.Focus();
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtCodEstiba1').focus()}, 1700);", true);
            }
        }
        //--------------------------------Boton invisible para Guardar la estiba en el contenedor------------------------------------------//
        protected void btnAsociar_Click(object sender, EventArgs e)
        {
            //Script para cambiar color 
            recargar(int.Parse(Session["conte1"].ToString()));
            if (infco.EstadoAC == "false")// si esta cerrado(false(0)) no deja hacer nada
            {
                lblMensaje.Text = "El pallet esta asociado a un contenedor cerrado!!";
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='blue'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none'}, 3200) } , 100); ", true);
                btnAsociar.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "ScriptJorge", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_PanelCarga').style.backgroundColor='rgb(18, 114, 168)'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_PanelCarga').style.backgroundColor='white'},2000) } , 100); ", true);
                lblMensaje.Text = "";
                btnAsociar.Visible = false;
                String str = txtCodEstiba1.Text.Substring(0, 2);
                int pallet = int.Parse(txtCodEstiba1.Text.Substring(2, 7));
                lblTotaP.Text = "";
                if (str == "99")
                {
                    CL.desasociarContenedorAccesorios(pallet);
                    lblMensaje.Text = "(Accesorios) La pallet se ha Desasociado !!";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='rgb(255, 217, 15)'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none'}, 3200) } , 100); ", true);
                    sumaTotalPeso();
                    txtCodEstiba1.Text = "";
                    txtCodEstiba1.Focus();
                }
                else if (str == "88")
                {
                    sumaTotalPeso();
                    CL.desasociarContenedorAluminio(pallet);
                    lblMensaje.Text = "(Aluminio) La pallet se ha Desasociado !!";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='rgb(255, 217, 15)'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none'}, 3200) } , 100); ", true);
                    sumaTotalPeso();
                    txtCodEstiba1.Text = "";
                    txtCodEstiba1.Focus();
                }
                conteo();
            }
        }
        public void sumaTotalPeso()
        {
            String conte = Session["conte"].ToString();
            int con = int.Parse(conte);
            DataTable tabla = CL.sumaPesoAlYACC(con);
            int suma = 0;
            foreach (DataRow dr in tabla.Rows)
            {
                suma += Convert.ToInt32(dr[0]);
                lblTotaP.Text = suma.ToString();
            }
        }
        public void conteo()
        {
            ////Pallet Cargados[Orden]
            InfoOrden ordenAct = linfoCOrd.ElementAt(1 - 1);
            String idofa = ordenAct.Idofa.ToString();
            String sql = "select dbo.CantPalletsCargados (" + idofa + ") AS CANT";
            lblPalletCargadosOrden2.Text = CL.conteo(sql).ToString();
            //Pallets de la Orden
            sql = "SELECT     'PalletsAluminio' AS tipo, COUNT(pallet_al_id) AS CANT "
                  + " FROM         pallet_aluminio "
                  + " WHERE     (pallet_al_Id_ofa = " + idofa + ") AND (pallet_al_cant > 0) "
                  + " UNION ALL "
                  + " SELECT     'PalletsAccesorios' AS tipo, COUNT(pallet_acc_id_of_p) AS CANT "
                  + " FROM         pallet_acc "
                  + " WHERE  "
                  + " (pallet_acc_id_of_p = " + idofa + ") AND (pallet_acc_cant > 0)";

            lblPalletOrden2.Text = CL.conteo(sql).ToString();
            //Pallet total cargados en el contendor
            sql = " SELECT 'PalletsAluminio' AS tipo, COUNT(pallet_aluminio.pallet_al_id) AS CANT "
            + " FROM pallet_aluminio INNER JOIN "
            + " Orden AS Orden_1 INNER JOIN "
            + " Despa_Cliente AS Despa_Cliente_1 LEFT OUTER JOIN "
            + " Desp_Transporte AS Desp_Transporte_1 ON Despa_Cliente_1.DesC_Grp_Id = Desp_Transporte_1.Desp_Grp_Id ON  "
            + " Orden_1.Id_Ofa = Despa_Cliente_1.DesC_OfPId ON pallet_aluminio.pallet_al_Id_ofa = Orden_1.Id_Ofa AND  "
            + " pallet_aluminio.Pallet_Trans_Id = Desp_Transporte_1.Desp_Trans_Id "
            + " WHERE (Orden_1.Despachada = 0) AND (Orden_1.Fec_Desp IS NULL) AND (Orden_1.Anulada = 0) AND (pallet_aluminio.pallet_al_cant > 0) AND  "
            + " (pallet_aluminio.Pallet_Trans_Id <> 0) AND (Desp_Transporte_1.Desp_Trans_Id = " + infco.Desp_Trans_id + ") "
            + " GROUP BY pallet_aluminio.Pallet_Trans_Id "
            + " union all "
            + " SELECT 'PalletsAcc' AS tipo, COUNT(pallet_acc.pallet_acc_id) AS CANT "
            + " FROM Orden AS Orden_1 INNER JOIN "
            + " Despa_Cliente AS Despa_Cliente_1 LEFT OUTER JOIN "
            + " Desp_Transporte AS Desp_Transporte_1 ON Despa_Cliente_1.DesC_Grp_Id = Desp_Transporte_1.Desp_Grp_Id ON  "
            + " Orden_1.Id_Ofa = Despa_Cliente_1.DesC_OfPId INNER JOIN "
            + " pallet_acc ON Desp_Transporte_1.Desp_Trans_Id = pallet_acc.pallet_acc_id_trans AND Orden_1.Id_Ofa = pallet_acc.pallet_acc_id_of_p "
            + " WHERE (Orden_1.Despachada = 0) AND (Orden_1.Fec_Desp IS NULL) AND (Orden_1.Anulada = 0) AND (Desp_Transporte_1.Desp_Trans_Id = " + infco.Desp_Trans_id + ") AND  "
            + " (pallet_acc.pallet_acc_id_trans <> 0) AND (pallet_acc.pallet_acc_cant > 0) "
            + " GROUP BY pallet_acc.pallet_acc_id ";
            lblPalletCargadosCont2.Text = CL.conteo(sql).ToString();
            //Total de pallets en el contenedor
            sql = " SELECT     'PalletsAluminio' AS tipo, COUNT(pallet_aluminio.pallet_al_id) AS CANT "
                 + " FROM         pallet_aluminio INNER JOIN "
                 + " Orden AS Orden_1 INNER JOIN "
                 + " Despa_Cliente AS Despa_Cliente_1 LEFT OUTER JOIN "
                 + " Desp_Transporte AS Desp_Transporte_1 ON Despa_Cliente_1.DesC_Grp_Id = Desp_Transporte_1.Desp_Grp_Id ON  "
                 + " Orden_1.Id_Ofa = Despa_Cliente_1.DesC_OfPId ON pallet_aluminio.pallet_al_Id_ofa = Orden_1.Id_Ofa "
                 + " WHERE     (Orden_1.Despachada = 0) AND (Orden_1.Fec_Desp IS NULL) AND (Orden_1.Anulada = 0) AND (pallet_aluminio.pallet_al_cant > 0) AND  "
                 + " (Desp_Transporte_1.Desp_Trans_Id = " + infco.Desp_Trans_id + ") "
                 + " GROUP BY pallet_aluminio.Pallet_Trans_Id "
                 + " UNION ALL "
                 + " SELECT     'PalletsAcc' AS tipo, COUNT(pallet_acc.pallet_acc_id) AS CANT "
                 + " FROM         Orden AS Orden_1 INNER JOIN "
                 + " Despa_Cliente AS Despa_Cliente_1 LEFT OUTER JOIN "
                 + " Desp_Transporte AS Desp_Transporte_1 ON Despa_Cliente_1.DesC_Grp_Id = Desp_Transporte_1.Desp_Grp_Id ON  "
                 + " Orden_1.Id_Ofa = Despa_Cliente_1.DesC_OfPId INNER JOIN "
                 + " pallet_acc ON Orden_1.Id_Ofa = pallet_acc.pallet_acc_id_of_p "
                 + " WHERE     (Orden_1.Despachada = 0) AND (Orden_1.Fec_Desp IS NULL) AND (Orden_1.Anulada = 0) AND (Desp_Transporte_1.Desp_Trans_Id = " + infco.Desp_Trans_id + ") AND  "
                 + " (pallet_acc.pallet_acc_cant > 0) "
                 + " GROUP BY pallet_acc.pallet_acc_id ";
            lblPalletTotalCont2.Text = CL.conteo(sql).ToString();
            //Total de pallets faltantes
            int idGrupo = int.Parse(Session["idGrupo"].ToString());
            sql = " SELECT 'PalletsAluminio' AS tipo, COUNT(pallet_aluminio.pallet_al_id) AS CANT, Despa_Grupo.Id_Grupo, pallet_aluminio.Pallet_Trans_Id "
            + " FROM pallet_aluminio INNER JOIN "
            + " Orden AS Orden_1 INNER JOIN "
            + " Despa_Cliente AS Despa_Cliente_1 ON Orden_1.Id_Ofa = Despa_Cliente_1.DesC_OfPId ON  "
            + " pallet_aluminio.pallet_al_Id_ofa = Orden_1.Id_Ofa INNER JOIN "
            + " Despa_Grupo ON Despa_Cliente_1.DesC_Grp_Id = Despa_Grupo.Id_Grupo "
            + " WHERE (Orden_1.Anulada = 0) AND (pallet_aluminio.pallet_al_cant > 0) AND (Despa_Grupo.Id_Grupo = " + idGrupo + ") AND (pallet_aluminio.Pallet_Trans_Id <> 0) "
            + " GROUP BY Despa_Grupo.Id_Grupo, pallet_aluminio.Pallet_Trans_Id "
            + " UNION ALL "
            + " SELECT 'PalletsAcc' AS tipo, COUNT(pallet_acc.pallet_acc_id) AS CANT, Despa_Grupo_1.Id_Grupo, pallet_acc.pallet_acc_id_trans "
            + " FROM Orden AS Orden_1 INNER JOIN "
            + " Despa_Cliente AS Despa_Cliente_1 ON Orden_1.Id_Ofa = Despa_Cliente_1.DesC_OfPId INNER JOIN "
            + " pallet_acc ON Orden_1.Id_Ofa = pallet_acc.pallet_acc_id_of_p INNER JOIN "
            + " Despa_Grupo AS Despa_Grupo_1 ON Despa_Cliente_1.DesC_Grp_Id = Despa_Grupo_1.Id_Grupo "
            + " WHERE (Orden_1.Anulada = 0) AND (pallet_acc.pallet_acc_cant > 0) AND (Despa_Grupo_1.Id_Grupo =" + idGrupo + ") AND (pallet_acc.pallet_acc_id_trans <> 0) "
            + " GROUP BY Despa_Grupo_1.Id_Grupo, pallet_acc.pallet_acc_id_trans ";
            int totalGrupo = int.Parse(CL.conteo(sql).ToString());
            int totalContenedores = int.Parse(lblPalletTotalCont2.Text);
            lblPalletFaltantesCont2.Text = (totalContenedores - totalGrupo).ToString();
        }
        protected void btnCA_Click(object sender, EventArgs e)
        {
            String rojo = "red";
            String verde = "green";
            if (btnCA.Text == "Abrir Cargue?")//cerrar/rojo
            {
                txtCodEstiba1.Enabled = true;
                txtCodEstiba1.Focus();
                int idTrans = int.Parse(Session["conte1"].ToString());
                CL.actualizaSoloContenedor(idTrans, 1); //mandar a cerrar (false(0)) / true = 1 / false =0
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_btnCA').style.backgroundColor='" + rojo + "'} , 100);", true);
                btnCA.Text = "Cerrar Cargue?";
                lblCA.Text = "Abierto: ";
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblCA').style.color='" + verde + "'} , 100);", true);
            }
            else if (btnCA.Text == "Cerrar Cargue?")//abrir/verde
            {
                txtCodEstiba1.Enabled = false;
                int idTrans = int.Parse(Session["conte1"].ToString());
                CL.actualizaSoloContenedor(idTrans, 0); //mandar a abrir (true(1)) / true = 1 / false =0
                CL.actualizarHorasFin(idTrans);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_btnCA').style.backgroundColor='" + verde + "'} , 100);", true);
                btnCA.Text = "Abrir Cargue?";
                lblCA.Text = "Cerrado: ";
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblCA').style.color='" + rojo + "'} , 100);", true);
            }
        }
    }
}