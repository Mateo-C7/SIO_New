using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using CapaDatos;

namespace CapaControl
{
    public class ControlPiezas
    {
        CapaDatos.BdDatos objconex = new BdDatos();

        public DataSet Met_Obtener_TipoPieza()
        {
            string sqlcontipz = "select TpoPza_id, TpoPza_dscrpcion from tipo_pieza  where TpoPza_Id >0";

            return BdDatos.consultarConDataset(sqlcontipz);
        }

        public void Listar_Tipo_Pieza(DropDownList myListaPlanta)
        {
            DataTable dTable = new DataTable();
            string sql = "select TpoPza_id, TpoPza_dscrpcion from tipo_pieza  where TpoPza_Id >0";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {

                ListItem lst = new ListItem(row["TpoPza_dscrpcion"].ToString(),
                                            row["TpoPza_id"].ToString());
                myListaPlanta.Items.Insert(myListaPlanta.Items.Count, lst);
            }
        }
    }
}