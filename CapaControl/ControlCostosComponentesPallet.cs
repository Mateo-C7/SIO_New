using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls;

namespace CapaControl
{
  public  class ControlCostosComponentesPallet
    {
        CapaDatos.BdDatos objconex = new BdDatos();

        public void Listar_TipoPallet(DropDownList myListaPallet)
        {
            DataTable dTable = new DataTable();
            string sql = "SELECT tpopall_id, tpopall_descripcion from  Tipo_Pallet where tpopall_habi=1";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["tpopall_descripcion"].ToString(),
                                            row["tpopall_id"].ToString());
                myListaPallet.Items.Insert(myListaPallet.Items.Count, lst);
            }
        }

        public void ListarCombo_TipoComponente(DropDownList myListaComponente)
        {
            DataTable dTable = new DataTable();
            string sql = " SELECT tpocomp_id,tpocomp_nombre FROM Tipo_Comp_Pallet";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["tpocomp_nombre"].ToString(),
                                            row["tpocomp_id"].ToString());
                myListaComponente.Items.Insert(myListaComponente.Items.Count, lst);
            }
        }

        public DataSet ListarTipoComponente()
        {
            string sql = " SELECT tpocomp_id,tpocomp_nombre FROM Tipo_Comp_Pallet";
            return BdDatos.consultarConDataset(sql);
        }

        public DataSet ListarTipoComponenteEdit()
        {
            string sql = " SELECT tpocomp_id,tpocomp_nombre FROM Tipo_Comp_Pallet";
            return BdDatos.consultarConDataset(sql);
        }

        public DataTable ObtenerDetalleTipoPalletEdit(int idpallet)
        {
            string sql = "SELECT  Tipo_Comp_Pallet.tpocomp_id,Tipo_Comp_Pallet.tpocomp_nombre, Tipo_Comp_Pallet.tpocomp_valor, Componente_Pallet.compal_unidades " +
                   " FROM Componente_Pallet INNER JOIN Tipo_Pallet ON Componente_Pallet.tpopall_id = Tipo_Pallet.tpopall_id " +
                   " INNER JOIN Tipo_Comp_Pallet ON Componente_Pallet.tpocomp_id = Tipo_Comp_Pallet.tpocomp_id " +
                   " WHERE(Componente_Pallet.tpopall_id =" + idpallet.ToString() + ")";
            return CapaDatos.BdDatos.CargarTabla(sql);
        }
        public DataSet ObtenerDetalleTipoPallet(int idpallet)
        {
            string sql = "SELECT  Tipo_Comp_Pallet.tpocomp_id,Tipo_Comp_Pallet.tpocomp_nombre,Tipo_Comp_Pallet.tpocomp_valor, Componente_Pallet.compal_unidades " +
                        " FROM Componente_Pallet INNER JOIN Tipo_Pallet ON Componente_Pallet.tpopall_id = Tipo_Pallet.tpopall_id " +
                        " INNER JOIN Tipo_Comp_Pallet ON Componente_Pallet.tpocomp_id = Tipo_Comp_Pallet.tpocomp_id " +
                        " WHERE(Componente_Pallet.tpopall_id =" + idpallet.ToString() + ")";
            return CapaDatos.BdDatos.consultarConDataset(sql);
        }


        public int Actualizar_Unidades_Componente(int idPallet,int idComponente,int valorUnidad)
        {
            string sql = "UPDATE Componente_Pallet set compal_unidades="+valorUnidad.ToString()+" " +
                         " WHERE  tpopall_id="+idPallet.ToString()+" AND tpocomp_id="+idComponente.ToString()+"";
            return BdDatos.ejecutarSql(sql);
        }     
        
        public int  Eliminar_ComponentePallet(int idPallet, int idComponente)
        {
            string sql= "DELETE FROM Componente_Pallet WHERE tpopall_id=" + idPallet.ToString() + " AND tpocomp_id=" + idComponente.ToString() + "";

            return BdDatos.ejecutarSql(sql);
        }

        public DataSet Obtener_TiposPallet()
        {
            string sql = "SELECT  Tipo_Comp_Pallet.tpocomp_id,Tipo_Comp_Pallet.tpocomp_nombre,Tipo_Comp_Pallet.tpocomp_valor" +                       
                         " FROM Tipo_Comp_Pallet";
            return BdDatos.consultarConDataset(sql);
        }

        public DataTable Obtener_TiposPalletEdit()
        {
            string sql = "SELECT  Tipo_Comp_Pallet.tpocomp_id,Tipo_Comp_Pallet.tpocomp_nombre,Tipo_Comp_Pallet.tpocomp_valor" +
                         " FROM Tipo_Comp_Pallet";
            return BdDatos.CargarTabla(sql);
        }

        public int Actualizar_Valor_TipoComponente(int idCompo,float valorCompo)
        {
            string sql = "UPDATE Tipo_Comp_Pallet SET tpocomp_valor="+valorCompo.ToString()+" " +
                        " WHERE tpocomp_id=" + idCompo.ToString()+"";
            return BdDatos.ejecutarSql(sql);
        }


        public DataTable Verificar_Duplicidad_Componente(int idPallet)
        {
            string sql = "SELECT Tipo_Pallet.tpopall_id, Tipo_Comp_Pallet.tpocomp_id " +
                       " FROM Tipo_Pallet INNER JOIN Componente_Pallet ON Tipo_Pallet.tpopall_id = Componente_Pallet.tpopall_id " +
                       " INNER JOIN Tipo_Comp_Pallet ON Componente_Pallet.tpocomp_id = Tipo_Comp_Pallet.tpocomp_id " +
                       " WHERE(Tipo_Pallet.tpopall_id =" + idPallet.ToString() + ")";
            return BdDatos.CargarTabla(sql);
        }

        public DataTable Obtener_ValorUnitario_Componente(int idCompo)
        {
            string sql = "SELECT tpocomp_id, tpocomp_valor" +
                       " FROM     Tipo_Comp_Pallet " +
                       " WHERE(tpocomp_id = " + idCompo.ToString() + ")";
            return BdDatos.CargarTabla(sql);
        }

        public int Agregar_Nuevo_Componente_Pallet(int unidades,int idCompo,int idPallet)
        {
            string sql = "INSERT INTO Componente_Pallet VALUES(" + unidades.ToString() + ", " + idCompo.ToString() + "," + idPallet.ToString() + ")";
            return BdDatos.ejecutarSql(sql);
        }
    }
}
