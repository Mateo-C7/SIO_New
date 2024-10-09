using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Specialized;
using System.Collections;
using System.Data;
using CapaControl;

namespace SIO
{
    public partial class itemPiciz : System.Web.UI.Page
    {
        static int requestCounter;
        static ArrayList hostData = new ArrayList();
        static StringCollection hostNames = new StringCollection();
        static void UpdateUserInterface()
        {
            // Print a message to indicate that the application
            // is still working on the remaining requests.

            Console.WriteLine("{0} requests remaining.", requestCounter);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool sysExpect;
            sysExpect = System.Net.ServicePointManager.Expect100Continue;
            System.Net.ServicePointManager.Expect100Continue = false;
            if (PicizGeneral.PaginaActual != Page.Title)
            {
                PicizGeneral.PaginaActual = Page.Title;

                if (!IsPostBack)
                {
                    if (PicizGeneral.itemDatos != null)
                    {
                        txtCDITEM.Text = PicizGeneral.CodItemGen;
                        CodGenItem = PicizGeneral.CodItemGen;
                        EnviaDatosGrillaComp(PicizGeneral.CompoDatos.Copy());

                        EnviaDatosGrillaItem(PicizGeneral.itemDatos.Copy());
                        CodBusqueda = PicizGeneral.CodBusqueda;
                    }

                }

            }


        }


        protected void ImgbtnFiltrar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string strItem;
                if (!String.IsNullOrEmpty(txtCDITEM.Text) && txtCDITEM.Text != "")
                {
                    strItem = txtCDITEM.Text;
                    strItem = strItem.ToUpper();
                    CodGenItem = strItem;
                    CodBusqueda = strItem;
                    PicizGeneral.CodBusqueda = strItem;
                    PicizGeneral.CodItemGen = strItem;

                    int tipo = 1;
                    switch (tipo)
                    {
                        case 1:
                            CargaGrillaItemSql(strItem);
                            break;
                        case 0:
                            CargarItemPiciz();
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                lblCreado.Text = ex.Message;
                lblCreado.Visible = true;
            }
        }

        protected void VerificaCarga()
        {
            try
            {
               bool ischeck;
                foreach (GridViewRow row in grdDataCompon.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("CheckBox2");
                    if (chk != null)
                    {
                       ischeck= chk.Checked;
                       int posRow = row.RowIndex;
                       DtDatosCompo.Rows[posRow]["CARGA_P"] = ischeck;
                    }
                }
                PicizGeneral.CompoDatos = DtDatosCompo;
            }
            catch (Exception ex)
            {
                lblCreado.Text = ex.Message;
                lblCreado.Visible = true;
            }
        }


        protected void CargaAct_CheckedChanged(Object sender, EventArgs e)
        {
            try
            {
                CheckBox showCheckBox = (CheckBox)sender;
                bool ischeck;

                ischeck=showCheckBox.Checked;
                if (ischeck ==false)
                {
                    GridViewRow row = grdDataCompon.HeaderRow;
                    CheckBox chk = (CheckBox)row.FindControl("CargaTodoCheckBox");
                    chk.Checked = false;
                           
                }
                   

                foreach (GridViewRow row in grdDataCompon.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("CheckBox2");
                    if (chk != null)
                    {
                        ischeck = chk.Checked;
                        int posRow = row.RowIndex;
                        DtDatosCompo.Rows[posRow]["CARGA_P"] = ischeck;
                    }
                }
                PicizGeneral.CompoDatos = DtDatosCompo;
            }
            catch (Exception ex)
            {
                lblCreado.Text = ex.Message;
                lblCreado.Visible = true;
            }
        }

        protected void CargaAllCheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            try
            {
                CheckBox showCheckBox = (CheckBox)sender;

                bool ischeck;

                if (showCheckBox.Checked)
                {
                    ischeck = true;
                }
                else
                {
                    ischeck = false;
                }

                foreach (GridViewRow row in grdDataCompon.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("CheckBox2");
                    if (chk != null)
                    {
                        chk.Checked = ischeck;
                        int posRow = row.RowIndex;
                        DtDatosCompo.Rows[posRow]["CARGA_P"] = ischeck;
                    }
                }
                PicizGeneral.CompoDatos = DtDatosCompo;
            }
            catch (Exception ex)
            {
                lblCreado.Text = ex.Message;
                lblCreado.Visible = true;
            }
        }

        protected void SelectAllCheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            try
            {
                CheckBox showCheckBox = (CheckBox)sender;

                bool ischeck;

                if (showCheckBox.Checked)
                {
                    ischeck = true;
                }
                else
                {
                    ischeck = false;
                }

                foreach (GridViewRow row in grdDataCompon.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                    if (chk != null)
                    {
                        chk.Checked = ischeck;

                    }
                }

            }
            catch (Exception ex)
            {
                lblCreado.Text = ex.Message;
                lblCreado.Visible = true;
            }
        }


        protected void EditAllTextBox_TextChanged(Object sender, EventArgs e)
        {
            try
            {
                TextBox activeTextBox = (TextBox)sender;
                String contText = activeTextBox.Text;
                foreach (GridViewRow row in grdDataCompon.Rows)
                {
                    TextBox actText = (TextBox)row.FindControl("TextBox1");
                    if (actText != null)
                    {
                        int posRow = row.RowIndex;
                        DtDatosCompo.Rows[posRow]["SALDO_P"] = actText.Text;
               
                    }
                }
                PicizGeneral.CompoDatos = DtDatosCompo;
            }
            catch (Exception ex)
            {
                lblCreado.Text = ex.Message;
                lblCreado.Visible = true;
            }
        }

        protected void CargarItemPiciz()
        {
            try
            {
                string strItem;
                if (!String.IsNullOrEmpty(txtCDITEM.Text) && txtCDITEM.Text != "")
                {
                    strItem = txtCDITEM.Text;

                    CapaControl.ConsultasParametricas.RESULTADODATOSITEM resItem =
                        CapaControl.AdminService.DatosItem(strItem);

                    if (resItem != null)
                    {
                        CapaControl.ConsultasParametricas.RESULTADODATOSITEM itCreado;
                        itCreado = resItem;
                        if (itCreado.CDCIA_USUARIA != "0")
                        {
                            lblCreado.Text = "Item en Piciz";
                            btCrea.Text = "Modificar...";
                            btCrea.Visible = true;
                            CargaGrillaItemCreado(itCreado);
                        }
                        else
                        {
                            lblCreado.Text = "Item No esta en Piciz";
                            btCrea.Text = "Crear...";
                            btCrea.Visible = true;
                            LimpiaGrillaItemCreado();
                        }
                    }
                    else
                    {
                        lblCreado.Text = "Item No esta en Piciz";
                        btCrea.Text = "Crear...";
                        btCrea.Visible = true;
                        LimpiaGrillaItemCreado();
                    }

                    if (grdDataItem.Rows.Count > 0)
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CargaGrillaItemSql(string valItem)
        {

            DtDatosCompo = null;
            DtDatositem = null;
            PicizGeneral.CompoDatos = null;
            PicizGeneral.itemDatos = null;

            lblCreado.Text = "";
            DataTable dt = Consulta.ConsultaTablaEstOrden(valItem);
            PicizGeneral.itemDatos = dt;
            EnviaDatosGrillaItem(dt);
            if (dt.Rows.Count > 0)
            {
                CargaGrillaCompSql(valItem);
                
            }
        }

        private void EnviaDatosGrillaItem(DataTable dt)
        {
            try
            {
                grdDataItem.DataSource = dt;
                grdDataItem.DataBind();
                int ctRow = grdDataItem.Rows.Count;
                DtDatositem = dt;

                if (ctRow > 0)
                {
                    grdDataItem.Visible = true;
                    Panel1.Visible = true;
                    lblOrden.Visible = true;
                    lblCreado.Text = "";

                    btCrea.Visible = true;
                    cmbTipMerc.Enabled = true;
                    cmbTipoItem.Enabled = true;
                    cmbUndCom.Enabled = true;
                    cmbUndMed.Enabled = true;
                    cmbSubP.Enabled = true;

                    cmbTipMerc.DataSource = Consulta.ConsultaCDMerc();
                    cmbTipoItem.DataSource = Consulta.ConsultaTipo();
                    cmbUndCom.DataSource = Consulta.ConsultaUndCom();
                    cmbUndMed.DataSource = Consulta.ConsultaUndMed();
                    cmbSubP.DataSource = Consulta.ConsultaCDSubPartida();

                    cmbTipMerc.DataBind();
                    cmbTipoItem.DataBind();
                    cmbUndCom.DataBind();
                    cmbUndMed.DataBind();
                    cmbSubP.DataBind();

                    if (cmbTipMerc.Items.Count > 0)
                    {
                        cmbTipMerc.SelectedIndex = 0;
                        PicizGeneral.CDMERCANCIA = Convert.ToDecimal(cmbTipMerc.Items[0].Text);
                    }

                    if (cmbSubP.Items.Count > 0)
                    {
                        cmbSubP.SelectedIndex = 0;
                        PicizGeneral.CDSUBPARTIDA = Convert.ToDecimal(cmbSubP.Items[0].Text);
                    }

                    if (cmbTipoItem.Items.Count > 0)
                    {
                        cmbTipoItem.SelectedIndex = 0;
                        PicizGeneral.CDTIPO = cmbTipoItem.Items[0].Text;
                    }

                    if (cmbUndCom.Items.Count > 0)
                    {
                        cmbUndCom.SelectedIndex = 0;
                        PicizGeneral.CDUNDCOM = cmbUndCom.Items[0].Text;
                    }

                    if (cmbUndMed.Items.Count > 0)
                    {
                        cmbUndMed.SelectedIndex = 0;
                        PicizGeneral.CDUNDMED = cmbUndMed.Items[0].Text;
                    }

                }
                else
                {
                    LimpiaGrillaOrden();
                    lblCreado.Text = "No existen Datos";
                    grdDataCompon.Visible = false;
                    Panel1.Visible = true;
                    lblOrden.Visible = false;
                    lblCreado.Visible = true;

                    btCrea.Visible = false;
                    cmbTipMerc.Enabled = false;
                    cmbTipoItem.Enabled = false;
                    cmbUndCom.Enabled = false;
                    cmbUndMed.Enabled = false;
                    cmbSubP.Enabled = false;
                    btSaldo.Visible = false;
                    Panel1.Visible = false;

                    DtDatosCompo = null;
                    PicizGeneral.CompoDatos = null;

                    if (cmbTipMerc.Items.Count > 0)
                    {
                        cmbTipMerc.Items.Clear();
                        PicizGeneral.CDMERCANCIA = 0;
                    }

                    if (cmbSubP.Items.Count > 0)
                    {
                        cmbSubP.Items.Clear();
                        PicizGeneral.CDSUBPARTIDA = 0;
                    }

                    if (cmbTipoItem.Items.Count > 0)
                    {
                        cmbTipoItem.Items.Clear();
                        PicizGeneral.CDTIPO = "";
                    }

                    if (cmbUndCom.Items.Count > 0)
                    {
                        cmbUndCom.Items.Clear();
                        PicizGeneral.CDUNDCOM = "";
                    }

                    if (cmbUndMed.Items.Count > 0)
                    {
                        cmbUndMed.Items.Clear();
                        PicizGeneral.CDUNDMED = "";
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void CargaGrillaCompSql(String dtlItem)
        {
            try
            {
                DataTable dt = Consulta.ConsultaOrdenMP(dtlItem);
                PicizGeneral.CompoDatos = dt;
                EnviaDatosGrillaComp(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void EnviaDatosGrillaComp(DataTable dt)
        {
            try
            {
                DtDatosCompo = dt;

                if (dt.Rows.Count > 0)
                {
                    grdDataCompon.DataSource = dt;
                    grdDataCompon.DataBind();
                    DtDatosCompo = dt;
                    int ctRow = grdDataCompon.Rows.Count;

                    if (ctRow > 0)
                    {
                        grdDataCompon.Visible = true;
                        lblComp.Visible = true;
                        lblCreado.Text = "";
                        btSaldo.Visible = true;

                        foreach (GridViewRow row in grdDataCompon.Rows)
                        {
                            DataRow dtRow = dt.Rows[row.RowIndex];

                            TextBox unText = (TextBox)row.FindControl("TextBox1");
                            if (unText != null)
                            {
                               
                                unText.Text = Convert.ToString(dtRow["SALDO_P"]);
                                                    
                            }

                            CheckBox unChk = (CheckBox)row.FindControl("CheckBox2");
                            if (unChk != null)
                            {
                               unChk.Checked = Convert.ToBoolean(dtRow["CARGA_P"]);
                               bool ischeck = unChk.Checked;
                               if (ischeck == false)
                               {
                                   GridViewRow Headrow = grdDataCompon.HeaderRow;
                                   CheckBox chk = (CheckBox)Headrow.FindControl("CargaTodoCheckBox");
                                   chk.Checked = false;

                               }

                            }
                        }
                        PicizGeneral.CompoDatos = DtDatosCompo;
                    }
                    else
                    {
                        LimpiaGrillaOrden();
                        lblCreado.Text = "No existen Componentes";
                        grdDataCompon.Visible = false;
                        Panel1.Visible = false;
                        lblComp.Visible = false;
                        lblCreado.Visible = true;
                        btSaldo.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CargaGrillaItemCreado(CapaControl.ConsultasParametricas.RESULTADODATOSITEM resItem)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CIA_USUARIA");
                dt.Columns.Add("ITEM");
                dt.Columns.Add("TIPO DE MERCANCIA");
                dt.Columns.Add("TIPO ITEM");
                dt.Columns.Add("COD. SUBPARTIDA");
                dt.Columns.Add("SUBPARTIDA");
                dt.Columns.Add("UND. COMERCIAL");
                dt.Columns.Add("UND. MEDIDA");
                dt.Columns.Add("CONVERSION");
                dt.Columns.Add("COMPONENTES");

                lblComp.Text = "DATOS DEL ITEM EN PICIZ";
                lblComp.Visible = true;

                DataRow row = dt.NewRow();
                row["CIA_USUARIA"] = resItem.CDCIA_USUARIA;
                row["ITEM"] = resItem.CDITEM;
                row["TIPO DE MERCANCIA"] = resItem.DSTIPO_MERCANCIA;
                row["TIPO ITEM"] = resItem.DSTIPO;
                row["COD. SUBPARTIDA"] = resItem.CDSUBPARTIDA;
                row["SUBPARTIDA"] = resItem.DSSUBPARTIDA;
                row["UND. COMERCIAL"] = resItem.DSUNIDAD_COMERCIAL;
                row["UND. MEDIDA"] = resItem.DSUNIDAD_MED;
                row["CONVERSION"] = resItem.NMCONVERSION;
                row["COMPONENTES"] = resItem.SNCOMPONENTES;

                dt.Rows.Add(row);
                grdDataCompon.DataSource = dt;
                grdDataCompon.DataBind();
                int ctRow = grdDataCompon.Rows.Count;
                grdDataCompon.Visible = true;
                Panel1.Visible = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LimpiaGrillaItemCreado()
        {
            try
            {
                lblComp.Text = "";
                lblComp.Visible = false;

                grdDataCompon.DataSource = null;
                grdDataCompon.DataBind();
                int ctRow = grdDataCompon.Rows.Count;
                grdDataCompon.Visible = false;
                Panel1.Visible = false;
                DtDatosCompo = null;
                PicizGeneral.CompoDatos = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LimpiaGrillaOrden()
        {
            try
            {
                lblComp.Text = "";
                lblComp.Visible = false;

                grdDataItem.DataSource = null;
                grdDataItem.DataBind();
                int ctRow = grdDataItem.Rows.Count;
                grdDataItem.Visible = false;
                PicizGeneral.CompoDatos = null;
                PicizGeneral.itemDatos = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void BuscaItemSaldoEnSaldoEnGrilla()
        {
            foreach (GridViewRow row in grdDataCompon.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        string codItem = row.Cells[0].Text;
                        String valSaldo = Convert.ToString(CapaControl.AdminService.ConsultaSaldo(codItem));
                                             
                        int posRow = row.RowIndex;
                        DtDatosCompo.Rows[posRow]["SALDO_P"] = valSaldo; 
                        TextBox unText = (TextBox)row.FindControl("TextBox1");
                        if (unText != null)
                        {
                            unText.Text = valSaldo;
                        }
                  
                        
                    }
                }
            }
            PicizGeneral.CompoDatos = DtDatosCompo;
        }

        protected void BuscaItemEquivalente(string codItem, int posRow)
        {
            FilaActual = posRow;
            CodigoActual = codItem;
            lblItemEquiv.Text = codItem;
            lblPosItem.Text = Convert.ToString(posRow);
            PicizGeneral.PosicItem = FilaActual;
            PicizGeneral.CodItem = codItem;

            DataTable dt = Consulta.ConsultaItemEquiv(codItem);


            if (dt.Rows.Count > 0)
            {
                
                DataColumn colUND_PZ = new DataColumn("SALDO_PZ");
                colUND_PZ.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(colUND_PZ);
                foreach (DataRow row in dt.Rows)
                {

                    string codRow = Convert.ToString(row["PZ_ITEM_EQUI_PICIZ"]);
                    row["SALDO_PZ"] = Convert.ToString(CapaControl.AdminService.ConsultaSaldo(codRow));

                }

                PicizGeneral.EquivDatos = dt.Copy();
                VerificaCarga();
                lblMensaje.Text = "";
                lblMensaje.Visible = false;
                Server.Transfer("itemEquivalente.aspx", true);

            }
            else
            {
                PicizGeneral.EquivDatos = dt.Copy();
                lblMensaje.Text = "No existen codigos equivalentes";
                lblMensaje.Visible = true;
            }

        }
        protected void btSaldo_Click(object sender, EventArgs e)
        {
            BuscaItemSaldoEnSaldoEnGrilla();
        }

        protected void btEdita_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                int posRow = row.RowIndex;
                string codItem = row.Cells[0].Text;
                PicizGeneral.CompoDatos = DtDatosCompo;
                BuscaItemEquivalente(codItem, posRow);
            }
            catch { }

        }

        protected void grdDataCompon_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = grdDataCompon.SelectedRow;
            int posRow = row.RowIndex;

            DataRow dtRow = DtDatosCompo.Rows[posRow];
            string codItem = Convert.ToString(dtRow["GRP_EQUI_ID"]);
            BuscaItemEquivalente(codItem, posRow);
        }

        protected void grdDataCompon_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = grdDataCompon.Rows[e.NewSelectedIndex];
            int posRow = row.RowIndex;
            DataRow dtRow = DtDatosCompo.Rows[posRow];
            string codItem = Convert.ToString(dtRow["GRP_EQUI_ID"]);
            BuscaItemEquivalente(codItem, posRow);
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {

            grdDataCompon.PageIndex = e.NewPageIndex;
            grdDataCompon.DataBind();
        }

        protected void grdDataCompon_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDataCompon.PageIndex = e.NewPageIndex;
            grdDataCompon.DataSource = DtDatosCompo;
            grdDataCompon.DataBind();

        }

        protected void grdDataCompon_OnPageIndexChanging(GridViewPageEventArgs e)
        {
            grdDataCompon.PageIndex = e.NewPageIndex;
            grdDataCompon.DataSource = DtDatosCompo;
            grdDataCompon.DataBind();

        }
        protected void grdDataItem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {

        }

        int _posRow;
        string _codSel;
        string _codIt;
        string CodBusqueda;
        private static DataTable _dt;
        private static DataTable _dtComp;

        public int FilaActual
        {
            get
            {
                return _posRow;
            }
            set { _posRow = value; }
        }

        public string CodigoActual
        {
            get
            {
                return _codSel;
            }
            set { _codSel = value; }
        }

        public string CodGenItem
        {
            get
            {
                return _codIt;
            }
            set { _codIt = value; }
        }

        public DataTable DtDatositem
        {
            get { return _dt; }
            set { _dt = value; }
        }

        public DataTable DtDatosCompo
        {
            get { return _dtComp; }
            set { _dtComp = value; }
        }


        protected void txtCDITEM_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cmbSubP_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itSel = cmbSubP.SelectedIndex;
            if (itSel >= 0)
            {
                PicizGeneral.CDSUBPARTIDA = Convert.ToDecimal(cmbSubP.Items[itSel].Text);
            }
        }

        protected void cmbTipMerc_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itSel = cmbTipMerc.SelectedIndex;
            if (itSel >= 0)
            {
                PicizGeneral.CDMERCANCIA = Convert.ToDecimal(cmbTipMerc.Items[itSel].Text);
            }

        }
        protected void cmbTipoItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itSel = cmbTipoItem.SelectedIndex;
            if (itSel >= 0)
            {
                PicizGeneral.CDTIPO = cmbTipoItem.Items[itSel].Text;
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itSel = cmbUndCom.SelectedIndex;
            if (itSel >= 0)
            {
                PicizGeneral.CDUNDCOM = cmbUndCom.Items[itSel].Text;
            }
        }
        protected void cmbUndMed_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itSel = cmbUndMed.SelectedIndex;
            if (itSel >= 0)
            {
                PicizGeneral.CDUNDMED = cmbUndMed.Items[itSel].Text;
            }
        }
        protected void btCrea_Click1(object sender, EventArgs e)
        {
            try
            {
                if (PicizGeneral.itemDatos != null)
                {
                    if (PicizGeneral.itemDatos.Rows.Count > 0)
                    {
                        bool boCrea = false;
                        int itEnBD = -1;
                        string strCom = "N";

                        if (PicizGeneral.CompoDatos != null)
                        {
                            if (PicizGeneral.CompoDatos.Rows.Count > 0)
                            {
                                strCom = "S";
                            }
                            else
                            {
                                strCom = "N";
                            }
                        }
                        else
                        {
                            strCom = "N";
                        }
                        
                        PicizGeneral.CodItemGen = txtCDITEM.Text;

                        bool itEnPiciz = AdminService.ItemCreado(PicizGeneral.CodItemGen);
                        if (itEnPiciz)
                        {
                            boCrea = true;
                        }
                        else
                        {
                            boCrea = AdminService.InsertaItemPiciz(PicizGeneral.CodItemGen, PicizGeneral.CDTIPO,
                                     PicizGeneral.CDMERCANCIA, PicizGeneral.CodItemGen, PicizGeneral.CDUNDCOM,
                                    PicizGeneral.CDUNDMED, strCom, PicizGeneral.CDSUBPARTIDA);
                        }

                        itEnBD = Consulta.ConsultaItemCreado(PicizGeneral.CodItemGen);
                        if (itEnBD == -1)
                        {
                          if (boCrea) {
                                itEnBD = Consulta.CreaItemEnBD(CodBusqueda, PicizGeneral.CodItemGen, 1, AdminService.PzUser);
                            }
                           
                        }

                        if (itEnBD > 0)
                        {
                            AgregarComponentes(itEnBD);
                        }
                       
                       

                        if (!boCrea)
                        {
                            lblCreado.Visible = true;
                            lblCreado.Text = "No se crearon items en Piciz: " + AdminService.MsgCrea;
                            AdminService.MsgCrea = "";
                        }
                        else
                        {
                            lblCreado.Visible = true;
                            lblCreado.Text = "Proceso Terminado: " + AdminService.MsgCrea;
                            AdminService.MsgCrea = "";
                        }
                    }
                    else
                    {
                        lblCreado.Visible = true;
                        lblCreado.Text = "El item no tiene datos";
                    }

                }
                else
                {
                    lblCreado.Visible = true;
                    lblCreado.Text = "El item no tiene datos";
                }
            }
            catch (Exception ex)
            {
                lblCreado.Visible = true;
                lblCreado.Text = ex.Message;
            }
        }

        protected void AgregarComponentes(int IdItemMatriz)
        {
            VerificaCarga();
            bool boCrea = AdminService.InsertaCompoPiciz(PicizGeneral.CodItemGen, Consulta.UnificarItem(DtDatosCompo));

            if (boCrea)
                Consulta.CreaCompoItemEnBD(IdItemMatriz, PicizGeneral.CodItemGen, Consulta.UnificarItem(DtDatosCompo));
        }
    }


    public class MyAppException : ApplicationException
    {
        public MyAppException(String message)
            : base(message)
        { }
        public MyAppException(String message, Exception inner) : base(message, inner) { }
    }
    public class ExceptExample
    {
        public void ThrowInner()
        {
            throw new MyAppException("ExceptExample inner exception");
        }
        public void CatchInner()
        {
            try
            {
                this.ThrowInner();
            }
            catch (Exception e)
            {
                throw new MyAppException("Error caused by trying ThrowInner.", e);
            }
        }
    }
}