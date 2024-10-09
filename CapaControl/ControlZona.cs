using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls;

namespace CapaControl
{
  public  class ControlZona
    {
        CapaDatos.BdDatos objconex = new BdDatos();


        public DataSet Obtenerzonas()
        {
            string sqlobtzonas = "select siat_zona_id,nombre_zona from siat_zona_pais";

            return BdDatos.consultarConDataset(sqlobtzonas);
        }
      public void Listar_Combo_ZonaSiat(DropDownList myListaZonaSiat)
        {
            DataTable dTable = new DataTable();
            string sql = "select siat_zona_id,nombre_zona from siat_zona_pais";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["nombre_zona"].ToString(),
                                            row["siat_zona_id"].ToString());
                myListaZonaSiat.Items.Insert(myListaZonaSiat.Items.Count, lst);
            }
        }





    }
}
