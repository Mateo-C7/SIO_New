using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaControl;

namespace SIO
{
    public partial class GrupoEquivalente : System.Web.UI.Page
    {
        DataTable dtGrupo;
        ListItem[] lstDatEquiv;
        string[] nomItem;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrupos();
            }
            else
            {
                dtGrupo = PicizGeneral.DatosGrupos;
                lstDatEquiv = Consulta.ConvLista(dtGrupo, "PZ_GRP_EQUI_DESC", "PZ_GRP_EQUI_ID");
            }
        }

        protected void CargarGrupos()
        {

            lblError.Visible = false;
            dtGrupo = Consulta.ConsultaGrupoEquiv();

            PicizGeneral.DatosGrupos = dtGrupo;
            lstDatEquiv = Consulta.ConvLista(dtGrupo, "PZ_GRP_EQUI_DESC", "PZ_GRP_EQUI_ID");
            lstEquiv.Items.Clear();
            lstEquiv.Items.AddRange(lstDatEquiv);
            MuestraDatosGrupo(0);

            txtDesc.Enabled = false;
            chkGrpActivo.Enabled = false;
            lstEquiv.Enabled = true;
            btAdiciona.Enabled = true;
            btEditar.Text = "Editar";
            btAdiciona.Text = "Adicionar...";

        }
        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void grdDataEquiv_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                GridViewRow row = grdDataEquiv.SelectedRow;
                int posRow = row.RowIndex;

                txtEquiv.Text = row.Cells[0].Text;
                txtOrig.Text = row.Cells[1].Text;
                txtMed1.Text = row.Cells[2].Text;
                txtMed2.Text = row.Cells[3].Text;
                string isAct = row.Cells[4].Text;
                if (isAct == "SI")
                    chkActivo.Checked = true;
                else
                    chkActivo.Checked = false;

                btEditaItem.Enabled = true;

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void grdDataEquiv_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }

        protected void lstEquiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itSel = lstEquiv.SelectedIndex;
            if (itSel >= 0)
            {

                MuestraDatosGrupo(itSel);
            }
        }

        private void MuestraDatosGrupo(int posic)
        {
            string strGrp = lstEquiv.Items[posic].Text;
            string idGrp = lstEquiv.Items[posic].Value;

            txtDesc.Text = strGrp;
            DataRow grRow = dtGrupo.Rows[posic];
            bool isAct = Convert.ToBoolean(grRow["PZ_GRP_EQUI_ACTIVO"]);
            chkGrpActivo.Checked = isAct;
            btEditar.Enabled = true;

            DataTable dtGrpItem = Consulta.ConsultaTodoItemEquiv(idGrp);
            grdDataEquiv.DataSource = dtGrpItem;
            grdDataEquiv.DataBind();

            Panel2.Visible = true;
            txtEquiv.Enabled = false;
            txtOrig.Enabled = false;
            txtMed1.Enabled = false;
            txtMed2.Enabled = false;
            chkActivo.Enabled = false;
            btEditaItem.Text = "Editar";
            btNuevo.Text = "Nuevo";

            btAdiciona.Enabled = true;
            btEditar.Enabled = true;
            btEditaItem.Enabled = false;
            if (dtGrpItem.Rows.Count > 0)
            {
                grdDataEquiv.SelectedIndex = 0;
                grdDataEquiv_SelectedIndexChanged(null, null);
            }

        }
        protected void btAdiciona_Click(object sender, EventArgs e)
        {
            if (btAdiciona.Text == "Adicionar...")
            {
                txtDesc.Enabled = true;
                chkGrpActivo.Enabled = true;
                lstEquiv.Enabled = false;
                btAdiciona.Text = "Cancelar";
                btEditar.Text = "Crear";
                btEditar.Enabled = true;
            }
            else
            {
                CargarGrupos();
            }

        }

        protected void btEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (btEditar.Text == "Crear")
                {
                    bool boCrea = false;
                    if (txtDesc.Text != "")
                    {

                        if (lstEquiv.Items.FindByText(txtDesc.Text) != null)
                        {
                            lblError.Text = "El grupo ya existe";
                            lblError.Visible = true;
                        }
                        else
                        {
                            boCrea = true;
                        }
                    }
                    else
                    {
                        lblError.Text = "Escriba una descripción";
                        lblError.Visible = true;
                    }

                    if (boCrea)
                    {
                        boCrea = Consulta.CreaGrupoEquiv(txtDesc.Text);
                        if (boCrea)
                        {
                            CargarGrupos();
                        }
                        else
                        {
                            lblError.Text = "Error al crear el grupo";
                            lblError.Visible = true;
                        }

                    }
                }
                else if (btEditar.Text == "Editar")
                {
                    txtDesc.Enabled = true;
                    chkGrpActivo.Enabled = true;
                    lstEquiv.Enabled = false;
                    btAdiciona.Enabled = true;
                    btEditar.Text = "Modificar";
                    btAdiciona.Text = "Cancelar";
                    btEditar.Enabled = true;
                }
                else if (btEditar.Text == "Modificar")
                {
                    int isAct = 0;
                    int idGrp;
                    if (chkGrpActivo.Checked)
                    {
                        isAct = 1;
                    }

                    idGrp = Convert.ToInt32(lstEquiv.SelectedItem.Value);
                    Consulta.ActualizaGrupoEquiv(txtDesc.Text, idGrp, isAct);

                    CargarGrupos();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        protected void btEditaItem_Click(object sender, EventArgs e)
        {
            try
            {
                int iditem = -1;

                GridViewRow row = grdDataEquiv.SelectedRow;
                if (row != null)
                {
                    iditem = Convert.ToInt16(row.Cells[5].Text);
                }
                int idGrup = Convert.ToInt32(lstEquiv.SelectedItem.Value);

                if (btEditaItem.Text == "Crear")
                {
                    bool boCrea = false;
                    DataTable itDat = Consulta.ConsultaExisteItemEquiv(txtEquiv.Text, txtOrig.Text, txtMed1.Text, txtMed2.Text, idGrup);

                    if (txtEquiv.Text != "" && txtOrig.Text != "")
                    {

                        if (itDat != null)
                        {
                            if (itDat.Rows.Count == 0)
                            {
                                boCrea = true;
                            }
                            else
                            {
                                lblError.Text = "El item ya existe";
                                lblError.Visible = true;
                            }

                        }
                        else
                        {
                            boCrea = true;
                        }
                    }
                    else
                    {
                        lblError.Text = "Escriba valores para Equivalente y Origen";
                        lblError.Visible = true;
                    }

                    if (boCrea)
                    {
                        boCrea = Consulta.CreaItemEquiv(idGrup, txtEquiv.Text, txtOrig.Text, txtMed1.Text, txtMed2.Text, 1);
                        if (boCrea)
                        {
                            int itSel = lstEquiv.SelectedIndex;
                            MuestraDatosGrupo(itSel);
                            lstEquiv.Enabled = true;
                        }
                        else
                        {
                            lblError.Text = "Error al crear el grupo";
                            lblError.Visible = true;
                        }

                    }
                }
                else if (btEditaItem.Text == "Editar")
                {
                    txtEquiv.Enabled = true;
                    txtOrig.Enabled = true;
                    txtMed1.Enabled = true;
                    txtMed2.Enabled = true;
                    chkActivo.Enabled = true;

                    btAdiciona.Enabled = false;
                    btEditar.Enabled = false;

                    btEditaItem.Text = "Modificar";
                    btNuevo.Text = "Cancelar";

                }
                else if (btEditaItem.Text == "Modificar")
                {
                    int isAct = 0;

                    if (chkActivo.Checked)
                    {
                        isAct = 1;
                    }

                    if ((txtEquiv.Text != "") && (txtOrig.Text != ""))
                    {

                        Consulta.ActualizaItemEquiv(txtEquiv.Text, txtOrig.Text, txtMed1.Text, txtMed2.Text, iditem, isAct);
                        int itSel = lstEquiv.SelectedIndex;
                        MuestraDatosGrupo(itSel);
                    }
                    else
                    {
                        lblError.Text = "Digite valores para Item Equivalente y Origen";
                        lblError.Visible = true;
                    }


                    CargarGrupos();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        protected void btNuevoItem_Click(object sender, EventArgs e)
        {
            if (btNuevo.Text == "Nuevo")
            {
                txtEquiv.Enabled = true;
                txtOrig.Enabled = true;
                txtMed1.Enabled = true;
                txtMed2.Enabled = true;
                chkActivo.Enabled = false;
                lstEquiv.Enabled = false;
                btAdiciona.Enabled = false;
                btEditar.Enabled = false;

                btEditaItem.Text = "Crear";
                btNuevo.Text = "Cancelar";

                btEditaItem.Enabled = true;
            }
            else
            {
                int itSel = lstEquiv.SelectedIndex;
                if (itSel >= 0)
                {
                    MuestraDatosGrupo(itSel);
                    lstEquiv.Enabled = true;
                    btAdiciona.Enabled = true;
                    btEditar.Enabled = true;
                }
            }

        }
    }
}