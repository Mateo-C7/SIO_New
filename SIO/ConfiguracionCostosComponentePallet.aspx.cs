using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaControl;
using System.Net;
using System.IO;
using System.Diagnostics;
namespace SIO
{
    public partial class ConfiguracionCostosComponentePallet : System.Web.UI.Page
    {
        ControlCostosComponentesPallet ctrlcostocompo = new ControlCostosComponentesPallet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Lista el tipo de pallet en un combo
                ctrlcostocompo.Listar_TipoPallet(cboTipoPallet);
                ctrlcostocompo.ListarCombo_TipoComponente(cbo_NuevoCompo);
                ObtenerDetalleTipoPallet();
                ObtenerTipoPallet();
                Habilitar_Agregar_Componentes(false);
                txt_NuevoValorCompo.ReadOnly = true;              
            }
        }

        public void Habilitar_Agregar_Componentes(bool habilitado)
        {
            if (habilitado == true)
            {
                cbo_NuevoCompo.Visible = true;
                txt_NuevoValorCompo.Visible = true;
                txt_NuevaUnidadCompo.Visible = true;
                Btn_AgregarCompo.Visible = true;
                Btn_CancelarCompo.Visible = true;
            }
            else
            {
                cbo_NuevoCompo.Visible = false;
                txt_NuevoValorCompo.Visible = false;
                txt_NuevaUnidadCompo.Visible = false;
                Btn_AgregarCompo.Visible = false;
                Btn_CancelarCompo.Visible = false;
            }
        }

        public void ObtenerDetalleTipoPalletEdit()
        {
            string idPallet = cboTipoPallet.SelectedValue.ToString();// recupero el valor del combo
            if (ctrlcostocompo.ObtenerDetalleTipoPalletEdit(int.Parse(idPallet)).Rows.Count != 0)
            {
                lbl_Msg_Detalle_Pallet.Text = "";
                GridView_DetallePallet.DataSource = ctrlcostocompo.ObtenerDetalleTipoPalletEdit(int.Parse(idPallet));
                GridView_DetallePallet.DataMember = ctrlcostocompo.ObtenerDetalleTipoPalletEdit(int.Parse(idPallet)).ToString();
                GridView_DetallePallet.Visible = true;
                GridView_DetallePallet.DataBind();
            }
            else
            {                             
                GridView_DetallePallet.ShowHeaderWhenEmpty = true;
                GridView_DetallePallet.DataSource = null;
                GridView_DetallePallet.DataMember = null;
                GridView_DetallePallet.DataBind();
                lbl_Msg_Detalle_Pallet.Text = "El pallet no contiene ningun Componente, Agrege uno nuevo";
            }
        }
        public void ObtenerDetalleTipoPallet()
        {
            string idPallet = cboTipoPallet.SelectedValue.ToString();// recupero el valor del combo
            if (ctrlcostocompo.ObtenerDetalleTipoPallet(int.Parse(idPallet)).Tables[0].Rows.Count != 0)
            {
                lbl_Msg_Detalle_Pallet.Text = "";
                GridView_DetallePallet.DataSource = ctrlcostocompo.ObtenerDetalleTipoPallet(int.Parse(idPallet));
                GridView_DetallePallet.DataMember = ctrlcostocompo.ObtenerDetalleTipoPallet(int.Parse(idPallet)).Tables[0].ToString();
                GridView_DetallePallet.Visible = true;
                GridView_DetallePallet.Columns[4].Visible = true;
                GridView_DetallePallet.DataBind();
            }
            else
            {
                GridView_DetallePallet.ShowHeaderWhenEmpty = true;
                GridView_DetallePallet.DataSource = null;
                GridView_DetallePallet.DataMember = null;
                GridView_DetallePallet.DataBind();
                GridView_DetallePallet.ShowHeaderWhenEmpty = true;
                mensajeVentana("El pallet no contiene ningun Componente, Agrege uno nuevo");
            }
        }


        //Permite mostrar mensajes de alerta
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        public void ObtenerTipoPallet()
        {
            if (ctrlcostocompo.Obtener_TiposPallet().Tables[0].Rows.Count != 0)
            {
                GridMaestroComponente.DataSource = ctrlcostocompo.Obtener_TiposPallet();
                GridMaestroComponente.DataMember = ctrlcostocompo.Obtener_TiposPallet().Tables[0].ToString();
                GridMaestroComponente.DataBind();
            }
            else
            {
                GridMaestroComponente.ShowHeaderWhenEmpty = true;
            }
        }

        public void Obtener_TiposPalletEdit()
        {
            if (ctrlcostocompo.Obtener_TiposPalletEdit().Rows.Count != 0)
            {
                GridMaestroComponente.DataSource = ctrlcostocompo.Obtener_TiposPalletEdit();
                GridMaestroComponente.DataMember = ctrlcostocompo.Obtener_TiposPalletEdit().ToString();
                GridMaestroComponente.DataBind();
            }
            else
            {
                GridMaestroComponente.ShowHeaderWhenEmpty = true;
            }
        }

        protected void cboTipoPallet_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView_DetallePallet.EditIndex = -1;
            ObtenerDetalleTipoPallet();
            ObtenerDetalleTipoPallet();
            Habilitar_Agregar_Componentes(false);
        }

        public object ListarTipoComponente()
        {
            DataSet ds;
            ds = ctrlcostocompo.ListarTipoComponente();
            return ds;
        }
        protected void GridView_DetallePallet_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView_DetallePallet.SelectedIndex = -1;
            GridView_DetallePallet.EditIndex = e.NewEditIndex;
            GridView_DetallePallet.Columns[4].Visible = false;
            ObtenerDetalleTipoPalletEdit();
        }
        protected void GridView_DetallePallet_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_DetallePallet.Columns[4].Visible = true;// cuando se cancela la edicion se muestra la columna eliminar                    
            GridView_DetallePallet.EditIndex = -1;
            ObtenerDetalleTipoPallet();
        }
        public void ListarTipoComponenteEdit()
        {
            if (ctrlcostocompo.ListarTipoComponenteEdit().Tables[0].Rows.Count != 0)
            {
                GridMaestroComponente.DataSource = ctrlcostocompo.ListarTipoComponenteEdit();
                GridMaestroComponente.DataMember = ctrlcostocompo.ListarTipoComponenteEdit().Tables.ToString();
                GridMaestroComponente.DataBind();
                ObtenerDetalleTipoPalletEdit();
            }
        }

        protected void GridView_DetallePallet_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string Unidades, idComponente;
            lbl_Msg_Detalle_Pallet.Text = "";
            TextBox txt = new TextBox();
            DropDownList cbo = new DropDownList();
            cbo = (DropDownList)GridView_DetallePallet.Rows[e.RowIndex].FindControl("cbo_tpocomp_id");
            idComponente = cbo.Text;
            txt = (TextBox)GridView_DetallePallet.Rows[e.RowIndex].FindControl("Txt_Unidades");
            Unidades = txt.Text;

            int resultado;

            if (Unidades != "")
            {
                if (int.TryParse(Unidades, out resultado))//evalua si el valor en el campo es del tipo correcto
                {
                    ctrlcostocompo.Actualizar_Unidades_Componente(int.Parse(cboTipoPallet.SelectedValue.ToString()),
                        int.Parse(idComponente), int.Parse(Unidades));
                    GridView_DetallePallet.EditIndex = -1;
                    GridView_DetallePallet.Columns[4].Visible = true;
                    ObtenerDetalleTipoPallet();
                    mensajeVentana("Detalle del pallet actualizado");
                    //lbl_Msg_Detalle_Pallet.Text = "Detalle del pallet actualizado";
                }
                else
                {
                    mensajeVentana("El tipo de dato no es valido");
                    // lbl_Msg_Detalle_Pallet.Text = "El tipo de dato no es valido";
                }
            }
            else
            {
                mensajeVentana("Debe ingresar la cantidad de unidades");
                //lbl_Msg_Detalle_Pallet.Text = "Debe ingresar la cantidad de unidades";
            }
        }

        protected void GridMaestroComponente_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridMaestroComponente.SelectedIndex = -1;
            GridMaestroComponente.EditIndex = e.NewEditIndex;
            Obtener_TiposPalletEdit();
        }

        protected void GridMaestroComponente_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridMaestroComponente.EditIndex = -1;
            ObtenerTipoPallet();
        }
        protected void GridMaestroComponente_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string valorCompo, idComponente;
            lbl_Msg_GridMaestroComponente.Text = "";
            TextBox txt = new TextBox();
            DropDownList cbo = new DropDownList();
            txt = (TextBox)GridMaestroComponente.Rows[e.RowIndex].FindControl("Txt_ValorUnitario");
            valorCompo = txt.Text;
            //txt = (TextBox)GridMaestroComponente.Rows[e.RowIndex].FindControl("Txt_idCompo");
            //idComponente = txt.Text;
            cbo = (DropDownList)GridMaestroComponente.Rows[e.RowIndex].FindControl("cbo_idCompo");
            idComponente = cbo.Text;

            float resultado;

            if (valorCompo != "")
            {
                if (float.TryParse(valorCompo, out resultado))//evalua si el valor en el campo es del tipo correcto
                {
                    ctrlcostocompo.Actualizar_Valor_TipoComponente(int.Parse(idComponente), float.Parse(valorCompo));
                    GridMaestroComponente.EditIndex = -1;
                    ObtenerDetalleTipoPallet();
                    ObtenerTipoPallet();
                    mensajeVentana("Componente Actualizado");                   
                }
                else
                {
                    mensajeVentana("El tipo de dato, en el campo unidades no es valido");
                    //lbl_Msg_GridMaestroComponente.Text = "El tipo de dato no es valido";
                }
            }
            else
            {
                mensajeVentana("Debe ingresar la cantidad de unidades");
                // lbl_Msg_GridMaestroComponente.Text = "Debe ingresar la cantidad de unidades";
            }
        }

        protected void GridView_DetallePallet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idTipoComponente = Convert.ToInt32(GridView_DetallePallet.DataKeys[e.RowIndex].Value);
            ctrlcostocompo.Eliminar_ComponentePallet(int.Parse(cboTipoPallet.SelectedValue.ToString()), idTipoComponente);
            ObtenerDetalleTipoPallet();
            GridView_DetallePallet.EditIndex = -1;
            mensajeVentana("Detalle del componente eliminado.");
            // lbl_Msg_Detalle_Pallet.Text = "Detalle del componente eliminado.";
        }

        protected void GridMaestroComponente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void Btn_NuevoCompo_Click(object sender, EventArgs e)
        {
            DataTable dt;
            Habilitar_Agregar_Componentes(true);
            dt = ctrlcostocompo.Obtener_ValorUnitario_Componente(int.Parse(cbo_NuevoCompo.SelectedValue.ToString()));
            txt_NuevoValorCompo.Text = dt.Rows[0][1].ToString();
            txt_NuevaUnidadCompo.Focus();
        }

        protected void Btn_CancelarCompo_Click(object sender, EventArgs e)
        {
            Habilitar_Agregar_Componentes(false);
            Limpiar_Campos_Agregar_Componente();
        }

        public void Limpiar_Campos_Agregar_Componente()
        {
            txt_NuevoValorCompo.Text = "";
            txt_NuevaUnidadCompo.Text = "";
            lbl_Msg_Detalle_Pallet.Text = "";
        }

        public void Verificar_Duplicidad_Componente()
        {
            DataTable dt;
            bool existe = false;
            int NuevoCompo = 0, CompoExistente = 0;
            //Se guarda el id del nuevo componente
            NuevoCompo = int.Parse(cbo_NuevoCompo.SelectedValue.ToString());
            dt = ctrlcostocompo.Verificar_Duplicidad_Componente(int.Parse(cboTipoPallet.SelectedValue.ToString()));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CompoExistente = int.Parse(dt.Rows[i][1].ToString());

                if (NuevoCompo == CompoExistente)
                {
                    existe = true;
                }
            }

            if (existe != true)
            {
                Agregar_Nuevo_Compo_Pallet();
            }
            else
            {
                mensajeVentana("El tipo de componente ya existe en este pallet");
                //lbl_Msg_Detalle_Pallet.Text = "El componente ya existe en este pallet";
            }
        }


        public void Agregar_Nuevo_Compo_Pallet()
        {
            string cadenaunidades;
            cadenaunidades = txt_NuevaUnidadCompo.Text;
            int resultado;
            if (txt_NuevaUnidadCompo.Text != "")
            {
                if (int.TryParse(cadenaunidades, out resultado))
                {
                    ctrlcostocompo.Agregar_Nuevo_Componente_Pallet(int.Parse(txt_NuevaUnidadCompo.Text),
                                                                        int.Parse(cbo_NuevoCompo.SelectedValue.ToString()),
                                                                        int.Parse(cboTipoPallet.SelectedValue.ToString()));
                    Limpiar_Campos_Agregar_Componente();
                    mensajeVentana("Componente agregado correctamente");
                    // lbl_Msg_Detalle_Pallet.Text = "Componente agregado correctamente";
                    Habilitar_Agregar_Componentes(false);
                    ObtenerDetalleTipoPallet();
                }
                else
                {
                    mensajeVentana("El campo unidades contiene un valor incorrecto");
                    // lbl_Msg_Detalle_Pallet.Text = "El campo unidades contiene un valor incorrecto";
                    txt_NuevaUnidadCompo.Focus();
                }
            }
            else
            {
                mensajeVentana("Debe asignar las unidades al componente");
                //lbl_Msg_Detalle_Pallet.Text = "Debe asignar las unidades al componente";
                txt_NuevaUnidadCompo.Focus();
            }
        }
        protected void Btn_AgregarCompo_Click(object sender, EventArgs e)
        {
            Verificar_Duplicidad_Componente();
        }

        protected void cbo_NuevoCompo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt;
            if (ctrlcostocompo.Obtener_ValorUnitario_Componente(int.Parse(cbo_NuevoCompo.SelectedValue.ToString())).Rows.Count != 0)
            {
                dt = ctrlcostocompo.Obtener_ValorUnitario_Componente(int.Parse(cbo_NuevoCompo.SelectedValue.ToString()));
                txt_NuevoValorCompo.Text = dt.Rows[0][1].ToString();
                txt_NuevaUnidadCompo.Focus();
            }
            else
            {
                mensajeVentana("El componente no tiene valor unitario");
                //lbl_Msg_Detalle_Pallet.Text = "El componente no tiene valor unitario";
            }
       }      
    }
}