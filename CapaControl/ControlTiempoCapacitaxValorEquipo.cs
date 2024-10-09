using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaDatos;
using System.Data;
using System.Web.UI.WebControls;

namespace CapaControl
{
    public class ControlTiempoCapacitaxValorEquipo
    {
        public DataSet Obtener_ZonasSiat()
        {
            string sql = "select zonsia_id,zonsia_descripcion,zonsia_activo from Zona_Siat where zonsia_activo=1";
            return BdDatos.consultarConDataset(sql);
        }
      
        //Anular configuracion zona
        public int Anular_ConfiguracionZona(int siatrangosid)
        {
            string sql = "UPDATE siat_rangos SET activo = 0 WHERE siat_rangos_id=" + siatrangosid.ToString() + "";
            return BdDatos.ejecutarSql(sql);
        }

        public void Listar_ZonaSiat(DropDownList myListazona)
        {   //Recuperar lista de zonas
            DataTable dTable = new DataTable();
            string sql = "SELECT zonsia_id,zonsia_descripcion FROM Zona_Siat WHERE zonsia_activo    =1";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["zonsia_descripcion"].ToString(),
                                            row["zonsia_id"].ToString());
                myListazona.Items.Insert(myListazona.Items.Count, lst);
            }
        }

        public void Listar_Tiposol(DropDownList myListaTipoSol)
        {   //Recuperar lista de zonas
            DataTable dTable = new DataTable();
            string sql = "SELECT T_Sol_Id,T_Sol_Materia FROM Tipos_Sol WHERE T_Sol_Id=5 or T_Sol_Id=7 ";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["T_Sol_Materia"].ToString(),
                                            row["T_Sol_Id"].ToString());
                myListaTipoSol.Items.Insert(myListaTipoSol.Items.Count, lst);
            }
        }


        public void Listar_TipoMoneda(DropDownList myListaTipoMoneda)
        {   //Recuperar lista de monedas
            DataTable dTable = new DataTable();
            string sql = "SELECT mon_id, mon_descripcion FROM moneda  WHERE mon_activo=1 ";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["mon_descripcion"].ToString(),
                                            row["mon_id"].ToString());
                myListaTipoMoneda.Items.Insert(myListaTipoMoneda.Items.Count, lst);
            }
        }

        public DataSet Listar_TipoMoneda()
        {
            string sql = "SELECT mon_id,mon_descripcion FROM moneda WHERE mon_activo=1";
            return BdDatos.consultarConDataset(sql);
        }



        public DataSet Listar_TipoOrden()
        {
            string sql = "SELECT T_Sol_Id,T_Sol_Materia FROM Tipos_Sol WHERE  T_Sol_Id=5 or T_Sol_Id=7 ";
            return BdDatos.consultarConDataset(sql);
        }




        public DataTable Obtener_RangosZona(int zona,int tiposol, int idrango)
        {
            string sql = "SELECT siat_rangos.rango_min, siat_rangos.rango_max " +
                          " FROM  siat_rangos INNER JOIN moneda ON siat_rangos.moneda_id = moneda.mon_id " +
                                " INNER JOIN Tipos_Sol ON siat_rangos.tipos_sol_id = Tipos_Sol.T_Sol_Id " +
                           " WHERE(siat_rangos.siat_zona_pais_id =" + zona.ToString() + ") AND (siat_rangos.tipos_sol_id =" + tiposol + ") AND (siat_rangos.activo = 1) AND (siat_rangos.siat_rangos_id<>"+idrango+")";
            return BdDatos.CargarTabla(sql);
        }

        public DataTable Obtener_RangosZona(int zona, int tiposol)
        {
            string sql = "SELECT siat_rangos.rango_min, siat_rangos.rango_max " +
                          " FROM  siat_rangos INNER JOIN moneda ON siat_rangos.moneda_id = moneda.mon_id " +
                                " INNER JOIN Tipos_Sol ON siat_rangos.tipos_sol_id = Tipos_Sol.T_Sol_Id " +
                           " WHERE(siat_rangos.siat_zona_pais_id =" + zona.ToString() + ") AND (siat_rangos.tipos_sol_id =" + tiposol + ") AND (siat_rangos.activo = 1)";
            return BdDatos.CargarTabla(sql);
        }

        ////public DataTable Obtener_ConfiguracionDeZonaEdit(int zona)
        ////{
        ////    string sql = "SELECT  siat_rangos.siat_rangos_id, siat_rangos.moneda_id AS monedaid, moneda.mon_descripcion, " +
        ////                        " siat_rangos.rango_min, siat_rangos.rango_max,siat_rangos.activo, siat_rangos.dias," +
        ////                        " siat_rangos.siat_zona_pais_id, siat_rangos.tipos_sol_id AS TisolId ,Tipos_Sol.T_Sol_Tipo," +
        ////                        " siat_rangos.dias_adicionales " +
        ////                  " FROM  siat_rangos INNER JOIN moneda ON siat_rangos.moneda_id = moneda.mon_id " +
        ////                        " INNER JOIN Tipos_Sol ON siat_rangos.tipos_sol_id = Tipos_Sol.T_Sol_Id " +
        ////                        " WHERE(siat_rangos.siat_zona_pais_id = " + zona + "  AND siat_rangos.activo=1)";
        ////    return BdDatos.CargarTabla(sql);
        ////}


        public DataTable Obtener_ConfiguracionDeZonaEdit(int zona, int tiposol)
        {
            string sql = "SELECT  siat_rangos.siat_rangos_id, siat_rangos.moneda_id AS monedaid, moneda.mon_descripcion, " +
                                " siat_rangos.rango_min, siat_rangos.rango_max,siat_rangos.activo, siat_rangos.dias," +
                                " siat_rangos.siat_zona_pais_id, siat_rangos.tipos_sol_id AS TisolId, Tipos_Sol.T_Sol_Materia," +
                                " siat_rangos.dias_adicionales,Zona_Siat.zonsiat_moneda " +
                         " FROM siat_rangos INNER JOIN  moneda ON siat_rangos.moneda_id = moneda.mon_id INNER JOIN " +
                              " Tipos_Sol ON siat_rangos.tipos_sol_id = Tipos_Sol.T_Sol_Id INNER JOIN " +
                              " Zona_Siat ON siat_rangos.siat_zona_pais_id = Zona_Siat.zonsia_id " +
                         " WHERE(siat_rangos.siat_zona_pais_id =" + zona.ToString() + ") AND (siat_rangos.tipos_sol_id =" + tiposol + ") AND (siat_rangos.activo = 1) " +
                         " ORDER BY siat_rangos.rango_max ASC";
            return BdDatos.CargarTabla(sql);
        }

        public DataSet Obtener_ConfiguracionDeZona(int zona, int tiposol)
        {
            string sql = "SELECT  siat_rangos.siat_rangos_id, siat_rangos.moneda_id, moneda.mon_descripcion, " +
                                " siat_rangos.rango_min, siat_rangos.rango_max,siat_rangos.activo, siat_rangos.dias," +
                                " siat_rangos.siat_zona_pais_id, siat_rangos.tipos_sol_id, Tipos_Sol.T_Sol_Materia," +
                                " siat_rangos.dias_adicionales,Zona_Siat.zonsiat_moneda " +
                         " FROM siat_rangos INNER JOIN  moneda ON siat_rangos.moneda_id = moneda.mon_id INNER JOIN " +
                              " Tipos_Sol ON siat_rangos.tipos_sol_id = Tipos_Sol.T_Sol_Id INNER JOIN " +
                              " Zona_Siat ON siat_rangos.siat_zona_pais_id = Zona_Siat.zonsia_id " +
                        " WHERE(siat_rangos.siat_zona_pais_id =" + zona.ToString() + ") AND (siat_rangos.tipos_sol_id =" + tiposol + ") AND (siat_rangos.activo = 1) " +
                        " ORDER BY siat_rangos.rango_max ASC";
            return BdDatos.consultarConDataset(sql);
        }


        public int Crear_Zona(string descripcion)
        {
            string sql = "INSERT INTO Zona_Siat (zonsia_descripcion,zonsia_activo)  VALUES('" + descripcion + "',1)";
            return BdDatos.ejecutarSql(sql);
        }

        public int Crear_ConfiguracionZona(int moneda, int dias, decimal rangomin, decimal rangomax, int activo, int zonaid, int tiposol, int diasadic)
        {
            string sql = "INSERT INTO siat_rangos (moneda_id,dias,rango_min,rango_max,activo,siat_zona_pais_id,tipos_sol_id,dias_adicionales) " +
                            " VALUES (" + moneda.ToString() + "," + dias.ToString() + "," + rangomin.ToString() + "," + rangomax.ToString() + "," + activo.ToString() + "," + zonaid.ToString() + "," + tiposol.ToString() + "," + diasadic.ToString() + ")";
            return BdDatos.ejecutarSql(sql);
        }


        public int Actualizar_ConfigZona(int moneda, int dias, decimal rangomin, decimal rangomax, int tiposol, int diasadicio, int siatid)
        {
            string sql = "UPDATE siat_rangos SET moneda_id=" + moneda.ToString() + ",dias=" + dias.ToString() + ",rango_min=" + rangomin.ToString() + ", " +
                                               " rango_max=" + rangomax.ToString() + ",tipos_sol_id=" + tiposol.ToString() + ", " +
                                               " dias_adicionales=" + diasadicio.ToString() + " " +
                                         " WHERE siat_rangos_id=" + siatid.ToString() + "";
            return BdDatos.ejecutarSql(sql);
        }

        public int Actualizar_ConfigZona2(int moneda, int dias, decimal rangomin, decimal rangomax, int tiposol, int diasadicio, int zona,
            int monedaT, int diasT, decimal rangominT, decimal rangomaxT, int tiposolT, int diasadicioT, int zonaT)
        {
            string sql = "UPDATE siat_rangos SET moneda_id=" + moneda.ToString() + ",dias=" + dias.ToString() + ",rango_min=" + rangomin.ToString() + ", " +
                                               " rango_max=" + rangomax.ToString() + ",tipos_sol_id=" + tiposol.ToString() + "," +
                                               " dias_adicionales=" + diasadicio.ToString() + " " +
                                         " WHERE siat_zona_pais_id=" + zonaT.ToString() + " AND tipos_sol_id = 3 AND dias_adicionales="+diasadicioT+ " " +
                                         " AND dias ="+diasT+ " AND rango_min ="+rangominT+ " AND rango_max="+rangomaxT+"";
            return BdDatos.ejecutarSql(sql);
        }

        public DataTable Consultar_ConfigZona2(int idrango)
        {
            string sql= " SELECT moneda_id, dias, rango_min, rango_max, siat_zona_pais_id, tipos_sol_id," +
                               " dias_adicionales from siat_rangos" +
                               " WHERE siat_rangos_id ="+idrango+"";
            return BdDatos.CargarTabla(sql);
        }

        public DataTable Obtener_ModenaZona(int zona)
        {
            string sql = "SELECT moneda.mon_descripcion " +
                        " FROM moneda INNER JOIN  Zona_Siat ON moneda.mon_id = Zona_Siat.zonsiat_moneda " +
                        " WHERE(Zona_Siat.zonsia_id =" + zona + ")";
            return BdDatos.CargarTabla2(sql);
        }

        public DataTable Obtener_ValorMaximoMaterial(int zona, int tiposol)
        {
            string sql = "SELECT MAX(siat_rangos.rango_max) AS ValorMax, MAX(siat_rangos.dias_adicionales) AS DiasAdicio  " +
                     " FROM siat_rangos INNER JOIN  moneda ON siat_rangos.moneda_id = moneda.mon_id INNER JOIN " +
                          " Tipos_Sol ON siat_rangos.tipos_sol_id = Tipos_Sol.T_Sol_Id INNER JOIN " +
                          " Zona_Siat ON siat_rangos.siat_zona_pais_id = Zona_Siat.zonsia_id " +
                          " WHERE(siat_rangos.siat_zona_pais_id =" + zona.ToString() + ") AND " +
                          " (siat_rangos.tipos_sol_id =" + tiposol + ") AND (siat_rangos.activo = 1)";
            return BdDatos.CargarTabla2(sql);
        }

        public DataTable Obtener_Moneda_diasAcionales(int zona, int tiposol)
        {
            string sql = "SELECT Zona_Siat.zonsiat_moneda, MAX(siat_rangos.dias_adicionales) AS DiasAdicio " +
                        " FROM Zona_Siat INNER JOIN siat_rangos ON Zona_Siat.zonsia_id = siat_rangos.siat_zona_pais_id INNER JOIN " +
                              " moneda ON Zona_Siat.zonsiat_moneda = moneda.mon_id " +
                        " WHERE(siat_rangos.siat_zona_pais_id = " + zona.ToString() + ") AND(siat_rangos.tipos_sol_id = " + tiposol + ") " +
                        " GROUP BY Zona_Siat.zonsiat_moneda";
            return BdDatos.CargarTabla(sql);
        }

        public DataTable Obtener_Moneda(int zona)
        {
            string sql = "SELECT Zona_Siat.zonsiat_moneda" +
                        " FROM Zona_Siat " +
                        " WHERE(zonsia_id = " + zona.ToString() + ")" +
                        " GROUP BY Zona_Siat.zonsiat_moneda";
            return BdDatos.CargarTabla(sql);
        }


    }
}
