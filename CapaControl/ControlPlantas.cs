using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;

namespace CapaControl
{
    public class ControlPlantas
    {
        CapaDatos.BdDatos objconex = new BdDatos();
        public void ListarPlantasProduccion(DropDownList myListaPlanta)
        {   //Recuperar lista de plantas
            DataTable dTable = new DataTable();
            string sql = " SELECT  Plan_Prod_Id,Plan_Prod_Nombre " +
                         " FROM Plantas_PRODUCCION " +
                         " WHERE Plan_Prod_Id = 2 or Plan_Prod_Id = 8 " +
                         " AND Plan_Prod_Activa = 1 ";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {

                ListItem lst = new ListItem(row["Plan_Prod_Nombre"].ToString(),
                                            row["Plan_Prod_Id"].ToString());
                myListaPlanta.Items.Insert(myListaPlanta.Items.Count, lst);
            }
        }
    }

}