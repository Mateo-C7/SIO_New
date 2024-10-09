using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaDatos;
using System.Data;
using System.Net.Mail;



namespace CapaControl
{
    public class ControlEvento
    {
        public String guardarDatosEvento(int pais, int ciudad, string feria, string nom, string fechaIni, string fechaFin, int origen, string objetivo, string conclusion)
        {
            string sql;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
            String mensaje = "";
            DataTable consulta = null;
            try
            {
                sql = "DECLARE @ultimoId INT;" +
                   "insert into dbo.lite_tipo_fuente (tifuente_descripcion " +
                   ",tifuente_activo " +
                   ",tifuente_origen_id " +
                   ",tifuente_usucrea " +
                   ",tifuente_fechacrea " +
                   ",tifuente_pais " +
                   ",tifuente_ciudad " +
                   ",tifuente_fechaini " +
                   ",tifuente_fechafin " +
                   ",tifuente_objetivo " +
                   ",tifuente_conclusion) " +
                   "  values( '" + feria + "',1," + origen + ",'" + nom + "','" + fecha + "',+" + pais + "," + ciudad + ",'" + fechaIni + "','" + fechaFin + "','" + objetivo + "','" + conclusion + "'); " +
                   "SET @ultimoId = SCOPE_IDENTITY(); SELECT @ultimoId AS id;";
                consulta = BdDatos.CargarTabla(sql);
                foreach (DataRow row in consulta.Rows)
                {
                    mensaje = row["id"].ToString();
                }
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT EVENTO lite_tipo_fuente)";
            }
            return mensaje;
        }
        public int actualizarDatosFeria(int pais, int ciudad, string feria, string nom, int idevento, string fechaIni, string fechaFin, int origen, string objetivo, string conclusion)
        {
            string sql;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
            sql = "update  dbo.lite_tipo_fuente set " +
               " tifuente_descripcion = '" + feria + "'" +
               ",tifuente_activo = 1 " +
               ",tifuente_origen_id = " + origen + "" +
               ",tifuente_usucrea = '" + nom + "'" +
               ",tifuente_fechacrea = '" + fecha + "'" +
               ",tifuente_pais = " + pais + "" +
               ",tifuente_ciudad = " + ciudad + "" +
               ",tifuente_fechaini = '" + fechaIni + "'" +
               ",tifuente_fechafin = '" + fechaFin + "'" +
               ",tifuente_objetivo= '" + objetivo + "'" +
               ",tifuente_conclusion = '" + conclusion + "'" +
               " where tifuente_id = " + idevento;
            return BdDatos.Actualizar(sql);
        }
        public DataSet ConsultarEventos()
        {
            string sql;
            sql = "SELECT     lite_tipo_fuente.tifuente_id AS id, lite_tipo_fuente.tifuente_descripcion AS nombre, pais.pai_nombre AS pais, ciudad.ciu_nombre AS ciudad, " +
                   "   lite_tipo_fuente.tifuente_usucrea AS usuario, CONVERT(varchar, lite_tipo_fuente.tifuente_fechacrea, 103) AS fecha, CONVERT(varchar,  " +
                   "   lite_tipo_fuente.tifuente_fechaini, 103) AS fecha_ini, CONVERT(varchar, lite_tipo_fuente.tifuente_fechafin, 103) AS fecha_fin " +
                   " FROM         pais INNER JOIN " +
                    "  lite_tipo_fuente ON pais.pai_id = lite_tipo_fuente.tifuente_pais INNER JOIN " +
                    "  ciudad ON lite_tipo_fuente.tifuente_ciudad = ciudad.ciu_id INNER JOIN " +
                     " lite_tipo_origen ON lite_tipo_fuente.tifuente_origen_id = lite_tipo_origen.tiorigen_id " +
                    " WHERE     (lite_tipo_origen.tiorigen_maestro = 1) and (lite_tipo_fuente.tifuente_activo = 1) " +
                    " ORDER BY lite_tipo_fuente.tifuente_fechacrea DESC ";
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }

        public DataSet ConsultarCiudades(int pais)
        {
            string sql;
            sql = "SELECT        ciudad.ciu_id AS IdCiudad, ciudad.ciu_nombre AS Ciudad, ISNULL(zonaCiudad.zonac_id, 0) AS IdZonaCiu, " +
            " ISNULL(zonaCiudad.zonac_nom, 'Sin') AS ZonaCiudad, pais.pai_id AS IdPais,   pais.pai_nombre AS Pais " +
            " FROM     ciudad INNER JOIN  pais ON ciudad.ciu_pai_id = pais.pai_id AND ciudad.ciu_pai_id = pais.pai_id LEFT OUTER JOIN " +
            "  zonaCiudad ON ciudad.ciu_zona = zonaCiudad.zonac_id  WHERE  (ciudad.ciu_id <> 573) AND (ciudad.ciu_id <> 569)  AND (pais.pai_id =" + pais + ") " +
            " ORDER BY Ciudad, zonaCiudad.zonac_nom ";
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }

        public DataSet ConsultarCiudadesxSubzona(int pais, int ZonaCiudad)
        {
            string sql;
            sql = "SELECT        ciudad.ciu_id AS IdCiudad, ciudad.ciu_nombre AS Ciudad, ISNULL(zonaCiudad.zonac_id, 0) AS IdZonaCiu, " +
            " ISNULL(zonaCiudad.zonac_nom, 'Sin') AS ZonaCiudad, pais.pai_id AS IdPais,   pais.pai_nombre AS Pais " +
            " FROM     ciudad INNER JOIN  pais ON ciudad.ciu_pai_id = pais.pai_id AND ciudad.ciu_pai_id = pais.pai_id LEFT OUTER JOIN " +
            "  zonaCiudad ON ciudad.ciu_zona = zonaCiudad.zonac_id  WHERE  (ciudad.ciu_id <> 573) AND (ciudad.ciu_id <> 569)  AND (pais.pai_id =" + pais + ") and (zonaCiudad.zonac_id =" + @ZonaCiudad + " )"+
            " ORDER BY Ciudad, zonaCiudad.zonac_nom ";
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }

        public SqlDataReader consultarEvento(int idevento)
        {
            string sql;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

            sql = "SELECT     lite_tipo_fuente.tifuente_id AS id, pais.pai_id, pais.pai_nombre AS pais, ciudad.ciu_id, ciudad.ciu_nombre AS ciudad, lite_tipo_fuente.tifuente_activo, " +
                    "  lite_tipo_fuente.tifuente_descripcion AS nombre,isnull( CONVERT(varchar, lite_tipo_fuente.tifuente_fechaini, 103),'') AS fecha_ini,isnull( CONVERT(varchar, " +
                     " lite_tipo_fuente.tifuente_fechafin, 103),'') AS fecha_fin, lite_tipo_fuente.tifuente_origen_id, lite_tipo_origen.tiorigen_descripcion,isnull(tifuente_objetivo,'') as objetivo,isnull(tifuente_conclusion,'') as conclusion " +
                    " FROM         pais INNER JOIN " +
                     " lite_tipo_fuente ON pais.pai_id = lite_tipo_fuente.tifuente_pais INNER JOIN " +
                     " ciudad ON lite_tipo_fuente.tifuente_ciudad = ciudad.ciu_id INNER JOIN " +
                    "  lite_tipo_origen ON lite_tipo_fuente.tifuente_origen_id = lite_tipo_origen.tiorigen_id " +
                    " WHERE     (lite_tipo_origen.tiorigen_maestro = 1) AND (lite_tipo_fuente.tifuente_id =" + idevento + ")" +
                    " ORDER BY nombre";

            return BdDatos.ConsultarConDataReader(sql);
        }
        //Carga el combo de los participantes
        public DataTable cargarParti(String nombre)
        {
            String sql = "SELECT  usuario.usu_login AS usu, empleado.emp_nombre + ' ' + empleado.emp_apellidos AS nombre FROM  usuario INNER JOIN empleado "
            + " ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id WHERE (empleado.emp_activo = 1) AND (usuario.usu_activo = 1)  AND (empleado.emp_nombre + ' ' + empleado.emp_apellidos LIKE '%" + nombre + "%') "
            + " UNION "
            + " SELECT   usuario.usu_login AS usu,  repre.rc_descripcion AS nombre "
            + " FROM     representantes_comerciales repre INNER JOIN "
            + " usuario ON repre.rc_usu_siif_id = usuario.usu_siif_id "
            + " WHERE  (repre.rc_activo = 1) AND (usuario.usu_activo = 1)  AND (repre.rc_descripcion  LIKE '%" + nombre + "%') ";
            //+ " AND (usuario.usu_emp_usu_num_id NOT IN (SELECT  empleado.emp_usu_num_id  FROM  empleado WHERE (empleado.emp_activo = 1))) ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //inserta los participantes
        public Boolean insertParti(String listaPart, String idFuente)
        {
            Boolean confi = false;
            try
            {
                String sql = "MERGE lite_participante WITH(HOLDLOCK) AS tablaPart "
                + " USING (SELECT   usuario.usu_login  AS usu,  " + idFuente + " AS idFuente FROM  usuario  WHERE  (usuario.usu_login IN (" + listaPart + "))) "
                + " AS tablaUsu (usu, idFuente) "
                + " ON  (tablaPart.part_usuario = tablaUsu.usu) AND (tablaPart.part_tifuente_id = tablaUsu.idFuente) "
                + " WHEN matched THEN "
                + " UPDATE "
                + " SET tablaPart.part_activo = 1 "
                + " WHEN not matched THEN "
                + " INSERT (part_tifuente_id,  part_usuario, part_fecha, part_activo) "
                + " VALUES ( tablaUsu.idFuente, tablaUsu.usu, SYSDATETIME(), 1); "
                + " UPDATE lite_participante SET part_activo = 0 WHERE  (part_usuario  NOT IN (" + listaPart + ")) AND (part_tifuente_id = " + idFuente + " );";
                BdDatos.Actualizar(sql);
                confi = true;
            }
            catch { confi = false; }
            return confi;
        }
        //Carga los participantes del evento
        public DataTable cargarPartiEvento(String filtro)
        {
            String sql = "SELECT  usuario.usu_login AS usu, empleado.emp_nombre + ' ' + empleado.emp_apellidos AS nombre, empleado.emp_correo_electronico AS correo FROM   empleado INNER JOIN  usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id INNER JOIN "
            + " lite_participante part ON usuario.usu_login = part.part_usuario WHERE (part.part_activo = 1) AND (usuario.usu_activo = 1) AND (empleado.emp_activo = 1) " + filtro + ""
            + " UNION "
            + " SELECT   usuario.usu_login AS usu,  repre.rc_descripcion AS nombre, repre.rc_email AS correo FROM  usuario INNER JOIN lite_participante part ON usuario.usu_login = part.part_usuario INNER JOIN "
            + " representantes_comerciales repre ON usuario.usu_siif_id = repre.rc_usu_siif_id WHERE  (usuario.usu_emp_usu_num_id NOT IN (SELECT  empleado.emp_usu_num_id  FROM  empleado WHERE (empleado.emp_activo = 1))) "
            + " AND (part.part_activo = 1) AND (usuario.usu_activo = 1) AND (repre.rc_activo = 1)" + filtro + "";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Ejecuta el correo de los participantes
        public void correoArchiICS(String usuPlan, String idEvento, String path)
        {
            ControlVisitaComercial CVC = new ControlVisitaComercial();
            DataTable consulta = cargarPartiEvento(idEvento);
            String correos = "";
            int conList = 0;
            foreach (DataRow row in consulta.Rows)
            {
                if (conList == 1)
                {
                    correos = correos + "," + row["correo"].ToString();
                }
                else
                {
                    correos = correos + row["correo"].ToString();
                    conList = 1;
                }
            }
            String cuerpo = "Adjuntamos el archivo .ics para que lo suba al calendario propio, gracias!";
            CVC.crearCorreo(cuerpo, "", "Nuevos Evento/Feria", "", correos, false, true, path, false);
        }
    }
}