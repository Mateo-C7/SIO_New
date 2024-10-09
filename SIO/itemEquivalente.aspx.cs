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
    public partial class itemEquivalente : System.Web.UI.Page
    {
        DataTable valData = null;
        GridView gridPrev;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (PicizGeneral.PaginaActual != Page.Title)
                {
                    PicizGeneral.PaginaActual = Page.Title;

                    CodItem = PicizGeneral.CodItem;
                    PosicItem = PicizGeneral.PosicItem;
                    CodGenItem = PicizGeneral.CodItemGen;
                    itemDatos = PicizGeneral.itemDatos.Copy();
                    CompoDatos = PicizGeneral.CompoDatos.Copy();
                    if (!IsPostBack)
                    {

                        DataTable dt = PicizGeneral.EquivDatos.Copy();
                        valData = dt;
                        if (dt.Rows.Count > 0)
                        {
                            lblMsg.Visible = false;
                            grdDataEquiv.DataSource = dt;
                            grdDataEquiv.DataBind();

                        }

                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        protected void grdDataEquiv_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = grdDataEquiv.Rows[e.NewSelectedIndex];
            int posRow = row.RowIndex;
            CodEquivalente = row.Cells[0].Text;

        }

        protected void grdDataEquiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = grdDataEquiv.SelectedRow;
                int posRow = row.RowIndex;
                CodEquivalente = row.Cells[0].Text;
                ReemplazarItem();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void grdDataEquiv_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDataEquiv.PageIndex = e.NewPageIndex;

        }

        protected void btSelecc_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                int posRow = row.RowIndex;
                CodEquivalente = row.Cells[0].Text;
                ReemplazarItem();
            }
            catch { }

        }

        protected void btEdita_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                int posRow = row.RowIndex;
                string codItem = row.Cells[0].Text;
                ReemplazarItem();
            }
            catch { }

        }

        protected void ReemplazarItem()
        {

            if (PicizGeneral.CodItem == CodItem)
            {
                CompoDatos = PicizGeneral.CompoDatos.Copy();
                if (CompoDatos.Rows.Count >= PosicItem)
                {
                    CompoDatos.Rows[PosicItem]["COD_ITEM_MP"] = CodEquivalente;
                    PicizGeneral.CompoDatos = CompoDatos.Copy();
                }
                else
                {
                    PicizGeneral.CompoDatos.Rows[PosicItem]["COD_ITEM_MP"] = CodEquivalente;
                }

            }

            Volver();

        }

        private void Volver()
        {
            string strQuery = Request.UrlReferrer.PathAndQuery;
            string namePrevPage = Request.UrlReferrer.ToString();
            Response.Redirect("itemPiciz.aspx", true);

        }

        private static string _codItem;
        private static string _codItGen;
        private static string _codEquiv;
        private static int _posItem;
        private static DataTable _dt;
        private static DataTable _dtComp;

        public static string CodItem
        {
            get { return _codItem; }
            set { _codItem = value; }
        }

        public static int PosicItem
        {
            get { return _posItem; }
            set { _posItem = value; }
        }

        public static string CodEquivalente
        {
            get { return _codEquiv; }
            set { _codEquiv = value; }
        }

        public string CodGenItem
        {
            get
            {
                return _codItGen;
            }
            set { _codItGen = value; }
        }

        public static DataTable itemDatos
        {
            get { return _dt; }
            set { _dt = value; }
        }

        public static DataTable CompoDatos
        {
            get { return _dtComp; }
            set { _dtComp = value; }
        }
    }
}