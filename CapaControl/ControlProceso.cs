using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;


namespace CapaControl
{
    public class ControlProceso
    {
        CapaDatos.BdDatos objconex = new BdDatos();

        public void ListarProcesosPlanta(DropDownList myListaPlanta)
        {   //Recuperar lista de proceso
            DataTable dTable = new DataTable();
            string sql2 = "select id_proceso , proceso from proceso where Proceso_Activo = 1 and proc_are_id =18";
            dTable = BdDatos.CargarTabla(sql2);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["proceso"].ToString(),
                                            row["Id_Proceso"].ToString());
                myListaPlanta.Items.Insert(myListaPlanta.Items.Count, lst);
            }
        }


        //metodo para consultar el idProceso y la observacion de la tabla proceso
        public DataTable Met_Consultar_Proceso_Observacio_Maestro(int meprId)
        {
            string sqlconprocobser = "select Id_proceso, Mepr_Observacion from Meta_Produccion where Mepr_Id=" + meprId.ToString() + "";

            return BdDatos.CargarTabla(sqlconprocobser);
        }

    }
}
