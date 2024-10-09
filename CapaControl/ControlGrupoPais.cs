using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls;

namespace CapaControl
{
   public class ControlGrupoPais
    {
        CapaDatos.BdDatos objconex = new BdDatos();

        //lista el grupo al que pertenece un pais
        public DataSet ObtenerGrupoPais()
        {
            string sqlobtgrupais = "select grpa_id,grpa_gp1_nombre from fup_grupo_pais where  grpa_id>0  order by grpa_gp1_nombre  asc";

            return BdDatos.consultarConDataset(sqlobtgrupais);
        }

        public void Listar_Combo_Zona(DropDownList myListaZona)
        {
            DataTable dTable = new DataTable();
            string sql = "select grpa_id,grpa_gp1_nombre from fup_grupo_pais order by grpa_gp1_nombre  asc";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["grpa_gp1_nombre"].ToString(),
                                            row["grpa_id"].ToString());
                myListaZona.Items.Insert(myListaZona.Items.Count, lst);
            }
        }


    }
}
