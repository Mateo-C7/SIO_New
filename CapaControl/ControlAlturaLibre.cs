using CapaControl.Entity;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CapaControl
{
    public class ControlAlturaLibre
    {

        public static List<AlturaLibre> ObtenerAlturaLibre()
        {
            string sql;
            sql = @"SELECT fall_id, fall_AlturaLibre, fall_AlturaFM, fall_AlturaUml, fall_TipoUml, fall_UnidadMedida
                    FROM fup_AlturaLibre Order by fall_UnidadMedida, case when fall_UnidadMedida = 1 then [fall_AlturaLibre] else convert(varchar(10),fall_id) end ";
            DataTable dt = BdDatos.CargarTabla(sql);
            List<AlturaLibre> lstAlturaLibre = dt.AsEnumerable()
                .Select(row => new AlturaLibre
                {
                    fall_id = (int)row["fall_id"],
                    fall_AlturaLibre = Convert.ToString(row["fall_AlturaLibre"]),
                    fall_AlturaFM = Convert.ToString(row["fall_AlturaFM"]),
                    fall_AlturaUml = Convert.ToString(row["fall_AlturaUml"]),
                    fall_TipoUml = Convert.ToString(row["fall_TipoUml"]),
                    fall_UnidadMedida = Convert.ToInt32(row["fall_UnidadMedida"])
                }).ToList();
            dt.Clear();
            dt.Dispose();
            return lstAlturaLibre;
        }
    }
}
