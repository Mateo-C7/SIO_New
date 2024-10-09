using System;
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
    public partial class CargaEstibas : System.Web.UI.Page
    {
        public ControlLogistica CL = new ControlLogistica();
        static List<InfoOrden> linfoOrd = new List<InfoOrden>();
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
            cboContenedor1.Items.Clear();
            lblResPalletTotal.Text = "";
            String NumOr = txtNumOrden1.Text.ToString();
            linfoOrd = CL.buscarDespachos(NumOr);
            Session["Orden3"] = txtNumOrden1.Text;
            cboDespacho1.Items.Add("Despacho");
            lblContPallet2.Text = "";
            if (linfoOrd.Count > 0)
            {
                //llenar despachos
                int i = 1;
                foreach (InfoOrden info in linfoOrd)
                {
                    if (i == 1)
                    {
                        lblResulCiudad1.Text = info.ciudad;
                        lblResulPais1.Text = info.pais;
                        lblContPallet2.Text = "";
                        cboDespacho1.Focus();
                    }
                    ++i;
                    cboDespacho1.Items.Add(info.despacho.ToString()); //<*------------------
                    lblContPallet2.Text = "";
                }
                buscarContenedores();   
            }
            else { 
            limpiarCampos();
            }   
        }

        protected void cboDespacho1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*busqueda de contenedores*/
            if ("Despacho" != cboDespacho1.SelectedValue)
            {
                Session["Desp3"] = cboDespacho1.SelectedItem.ToString();
                buscarContenedores();
                cboContenedor1.Focus();
                txtCodEstiba1.Text = "";
            }
            else {
               Session["Desp3"] = cboDespacho1.SelectedItem.ToString();
               cboContenedor1.Items.Clear();
               txtCodEstiba1.Text = "";
               lblResulPlaca1.Text = "";
               lblContPallet2.Text = "";
               lblContPallet2.Text = "";
               lblTotaP.Text = "";
               lblPalletCargados2.Text = "";
               lblPalletFaltantes2.Text = "";
               btnCA.Text = "";
               lblCA.Text = "";
            }
            
        }

        /*Relaiza la busqueda de contenedores*/
        private void buscarContenedores()
        {
            lblContPallet2.Text = "";
            txtCodEstiba1.Text = "";
            linfCont = new List<InfoContenedor>();

            if ("Despacho" != cboDespacho1.SelectedValue)
            {
            InfoOrden ordenAct = linfoOrd.ElementAt(cboDespacho1.SelectedIndex-1);
            String idDespacho = ordenAct.idDespacho.ToString();
            linfCont = CL.buscarContenedores(idDespacho);
            cboContenedor1.Items.Clear();
            cboContenedor1.Items.Add("Contenedor");
            if (linfCont.Count > 0)
            {
                //llenar contenedores
                foreach (InfoContenedor infd in linfCont)
                {
                    cboContenedor1.Items.Add(new ListItem(infd.transPlaca.ToString(), infd.Desp_Trans_id.ToString()));
                }
                infco = linfCont.ElementAt(cboContenedor1.SelectedIndex);
            }
            }
            txtCodEstiba1.Text = "";
        }

        public void limpiarCampos()
        {
            btnCA.Text = "";
            lblCA.Text = "";
            lblTotaP.Text = "";
            lblContPallet2.Text = "";
            txtCodEstiba1.Text = "";
            lblResulPais1.Text = "";
            lblResulCiudad1.Text = "";
            lblResulPlaca1.Text = "";
            cboContenedor1.Items.Clear();
            cboDespacho1.Items.Clear();
            lblPalletCargados2.Text = "";
            lblPalletFaltantes2.Text = "";
            lblContPallet2.Text = "";
        }
        //--------------------------------Boton invisible para Guardar la estiba en el contenedor------------------------------------------//
        protected void Button2_Click(object sender, EventArgs e)
        {

            if (txtCodEstiba1.Text.ToString().Length == 9)
            {
                String str = txtCodEstiba1.Text.Substring(0, 2);
                ELogcappeso log = CL.buscar_Tamaño("log_str_carga", str);

                int index = cboDespacho1.SelectedIndex;
                InfoOrden infoO = linfoOrd.ElementAt(index-1);

                if (log != null)
                {
                    int pallet = int.Parse(txtCodEstiba1.Text.Substring(2, 7));

                    if (log.tabla.Trim().ToUpper() == "ALUMINIO")
                    {
                        //Probar aluminio 
                        InfoEstiba InfoEst = CL.verificaPalletvsOfaAL(pallet, infoO.idofa);
                        if (InfoEst != null)
                        {
                            if (InfoEst.trasnId == 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "ScriptJorge", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_PanelCarga').style.backgroundColor='rgb(97, 171, 213)'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_PanelCarga').style.backgroundColor='white'},2000) } , 100); ", true);
                                lblMensaje.Text = ":: Pallet asignada al contenedor!! ::";
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='Lime'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none'}, 3200) } , 100); ", true); 
                                CL.actualizarContenedorAluminio(pallet, infco.Desp_Trans_id, infco.trans_idContenedor);
                                conteo();
                                sumaTotalPeso();
                                txtCodEstiba1.Text = "";
                                txtCodEstiba1.Focus();
                                
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
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Pallet no corresponde a orden !!')", true);
                            txtCodEstiba1.Text = "";
                            txtCodEstiba1.Focus();
                        }
                    }
                    else if (log.tabla.Trim().ToUpper() == "ACCESORIOS")
                    {
                        //Probar accesorios
                        InfoEstiba InfoEst = CL.verificaPalletvsOfaACC(pallet, infoO.idofa);
                        if (InfoEst != null)
                        {
                            if (InfoEst.trasnId == 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "ScriptJorge", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_PanelCarga').style.backgroundColor='rgb(97, 171, 213)'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_PanelCarga').style.backgroundColor='white'},2000) } , 100); ", true); 
                                lblMensaje.Text = ":: Pallet asignada al contenedor!! ::";
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='Lime'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none'}, 3200) } , 100); ", true);
                                CL.actualizarContenedorAccesorios(pallet, infco.Desp_Trans_id, infco.trans_idContenedor);
                                conteo();
                                sumaTotalPeso();
                                txtCodEstiba1.Text = "";
                                txtCodEstiba1.Focus();
                               
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
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Pallet no corresponde a orden !!')", true);
                            txtCodEstiba1.Text = "";
                            txtCodEstiba1.Focus();
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Pallet invalida !!')", true);
                    txtCodEstiba1.Text = "";
                    txtCodEstiba1.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Longitud de pallet invalida !!')", true);
                txtCodEstiba1.Focus();
                txtCodEstiba1.Text = "";
            }
        }
        //--------------------------------Boton invisible para Guardar la estiba en el contenedor------------------------------------------//
        

        protected void btnAsociar_Click(object sender, EventArgs e)
        {
            //Script para cambiar color 

            if (infco.estadoAC == "false")// si esta cerrado(false(0)) no deja hacer nada
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
        //cargar el combo de contenedores
        protected void cboContenedor1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            String rojo= "red";
            String verde = "green";
            String conte = cboContenedor1.SelectedValue;
            String conteP = cboContenedor1.SelectedItem.ToString();
            btnCA.Text = "";
            lblCA.Text = "";
            if ("Contenedor" != cboContenedor1.SelectedValue)
            {
                conteo();
                sumaTotalPeso();
                Session["conteP"] = conteP;
                Session["conte"] = conte;
                lblResulPlaca1.Text = infco.getTransPlaca();
                txtCodEstiba1.Text = "";
                txtCodEstiba1.Focus();

                if (infco.estadoAC == "True")// true si esta abierto / true = 1
                {
                    txtCodEstiba1.Enabled = true;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_btnCA').style.backgroundColor='" + rojo + "'} , 100);", true);
                    btnCA.Text = "Cerrar Cargue?";
                    lblCA.Text = "Abierto: ";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblCA').style.color='" + verde + "'} , 100);", true);
                   
                }
                else { //false si esta cerrado / false = 0
                    txtCodEstiba1.Enabled = false;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_btnCA').style.backgroundColor='" + verde + "'} , 100);", true);
                    btnCA.Text = "Abrir Cargue?";
                    lblCA.Text = "Cerrado: ";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblCA').style.color='" + rojo + "'} , 100);", true);
                }
            }
            else
            {
                buscarContenedores();
                lblTotaP.Text = "";
                Session["conte"] = conte;
                txtCodEstiba1.Text = "";
                lblContPallet2.Text = "";
                lblResulPlaca1.Text = "";
                btnCA.Text = "";
                lblCA.Text = "";
            }
        }
        public void sumaTotalPeso()
        {
            String conte = cboContenedor1.SelectedValue;
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
        {   //estibas cargadas en este de contenedores
            String sql = "SELECT     'PalletsAcc' AS tipo, COUNT(pallet_acc_id_of_p) AS CANT "
                + " FROM         pallet_acc "
                + " WHERE     (pallet_acc_cant > 0)"
                + " GROUP BY pallet_acc_id_trans"
                + " HAVING      (pallet_acc_id_trans = " + infco.getDesp_Trans_id() + ")"
                + " UNION ALL"
                + " SELECT     'PalletsAluminio' AS tipo, COUNT(pallet_al_id) AS CANT"
                + " FROM         pallet_aluminio"
                + " WHERE     (pallet_al_cant > 0)"
                + " GROUP BY Pallet_Trans_Id"
                + " HAVING      (Pallet_Trans_Id = " + infco.getDesp_Trans_id() + ")";
                lblContPallet2.Text = CL.conteo(sql).ToString();

                ////cargados totales
                if ("Despacho" != cboDespacho1.SelectedValue)
                {
                    InfoOrden ordenAct = linfoOrd.ElementAt(cboDespacho1.SelectedIndex-1);
                    String idofa = ordenAct.idofa.ToString();

                    sql = "select dbo.CantPalletsCargados (" + idofa + ") AS CANT";
                    lblPalletCargados2.Text = CL.conteo(sql).ToString();


                    //total de la orden
                    sql = "SELECT     'PalletsAluminio' AS tipo, COUNT(pallet_al_id) AS CANT "
                          + " FROM         pallet_aluminio "
                          + " WHERE     (pallet_al_Id_ofa = " + idofa + ") AND (pallet_al_cant > 0) "
                          + " UNION ALL "
                          + " SELECT     'PalletsAccesorios' AS tipo, COUNT(pallet_acc_id_of_p) AS CANT "
                          + " FROM         pallet_acc "
                          + " WHERE  "
                          + " (pallet_acc_id_of_p = " + idofa + ") AND (pallet_acc_cant > 0)";

                    lblResPalletTotal.Text = CL.conteo(sql).ToString();

                    int pt = int.Parse(lblResPalletTotal.Text);
                    int pc = int.Parse(lblPalletCargados2.Text);
                    lblPalletFaltantes2.Text = (pt - pc).ToString(); // faltantes
                }
        }

       

        protected void btnCA_Click(object sender, EventArgs e)
        {
            String rojo = "red";
            String verde = "green";
            if (btnCA.Text == "Abrir Cargue?")//cerrar/rojo
            {
                txtCodEstiba1.Enabled = true;
                txtCodEstiba1.Focus();
                String idconte = cboContenedor1.SelectedValue;
                CL.actualizaContenedor(int.Parse(idconte), 1); //mandar a cerrar (false(0)) / true = 1 / false =0
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_btnCA').style.backgroundColor='" + rojo + "'} , 100);", true);
                btnCA.Text = "Cerrar Cargue?";
                lblCA.Text = "Abierto: ";
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblCA').style.color='" + verde + "'} , 100);", true);
            }
            else if (btnCA.Text == "Cerrar Cargue?")//abrir/verde
            {
                txtCodEstiba1.Enabled = false;
                String idconte = cboContenedor1.SelectedValue;
                CL.actualizaContenedor(int.Parse(idconte), 0); //mandar a abrir (true(1)) / true = 1 / false =0
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_btnCA').style.backgroundColor='" + verde + "'} , 100);", true);
                btnCA.Text = "Abrir Cargue?";
                lblCA.Text = "Cerrado: ";
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblCA').style.color='" + rojo + "'} , 100);", true);
            }
        }
    }
}