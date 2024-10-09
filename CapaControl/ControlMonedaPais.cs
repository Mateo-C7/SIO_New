using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls;

namespace CapaControl
{
   public class ControlMonedaPais
    {
        CapaDatos.BdDatos objconex = new BdDatos();

        public DataSet ObtenerMoneda()
        {
            string sqlobtmoneda = "select mon_id,mon_descripcion from moneda";

            return BdDatos.consultarConDataset(sqlobtmoneda);
        }


        public void Listar_Combo_Moneda(DropDownList myListaMoneda)
        {
            DataTable dTable = new DataTable();
            string sql = "select mon_id,mon_descripcion from moneda";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["mon_descripcion"].ToString(),
                                            row["mon_id"].ToString());
                myListaMoneda.Items.Insert(myListaMoneda.Items.Count, lst);
            }
        }


    }  
}
