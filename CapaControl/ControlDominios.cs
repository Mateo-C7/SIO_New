using CapaControl.Entity;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CapaControl
{
    public class ControlDominios
    {
        public static List<Dominios> obtener_dominios(string enumerador)
        {
            string sql;
            sql = @"SELECT fdom_id, fdom_Dominio, fdom_CodDominio, fdom_OrdenDominio, fdom_Descripcion,
                        fdom_DescripcionEN, fdom_DescripcionPO
                    FROM fup_Dominios WHERE fdom_Dominio = @dominio 
                    ORDER BY fdom_OrdenDominio";
            Dictionary<string, object> parametros = new Dictionary<string, object>() { { "@dominio", enumerador } };
            DataTable dt = BdDatos.CargarTablaConParametros(sql, parametros);
            List<Dominios> lstDominios = dt.AsEnumerable()
                .Select(row => new Dominios
                {
                    fdom_id = (int)row["fdom_id"],
                    fdom_Dominio = (string)row["fdom_Dominio"],
                    fdom_CodDominio = (string)row["fdom_CodDominio"],
                    fdom_OrdenDominio = Convert.ToInt16(row["fdom_OrdenDominio"]),
                    fdom_Descripcion = (string)row["fdom_Descripcion"],
                    fdom_DescripcionEN = (string)row["fdom_DescripcionEN"],
                    fdom_DescripcionPO = (string)row["fdom_DescripcionPO"]
                }).ToList();
            dt.Clear();
            dt.Dispose();
            return lstDominios;
        }
    }
}
