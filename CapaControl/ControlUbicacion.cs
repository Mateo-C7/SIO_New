using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaDatos;
using System.Data;
using System.Net.Mail;
using CapaControl.Entity;

namespace CapaControl
{
    public class ControlUbicacion
    {
        public SqlDataReader poblarListaPaisRepresentante(int representante)
        {
            string sql;

            sql = "SELECT pais.pai_nombre, pais.pai_id, representantes_comerciales.rc_id " +
                "FROM pais_representante INNER JOIN representantes_comerciales ON " +
                "pais_representante.pr_id_representante = representantes_comerciales.rc_id INNER JOIN " +
                "pais ON pais_representante.pr_id_pais = pais.pai_id " +
                "WHERE (representantes_comerciales.rc_id = " + representante + ") AND (pais_representante.pr_activo = 1)  AND " +  
                "(pai_id <> 74)" + 
                "ORDER BY pai_nombre DESC";

            return BdDatos.ConsultarConDataReader(sql);

        }

        public SqlDataReader poblarListaOfs()
        {
            string sql;

            sql = "SELECT        RTRIM(Orden_Seg.Num_Of) + '-' + Orden_Seg.Ano_Of AS Orden, Orden_Seg.Id_Ofa AS Idofp " +
                    " FROM            Orden_Seg INNER JOIN " +
                        " Orden ON Orden_Seg.Id_Ofa = Orden.Id_Ofa LEFT OUTER JOIN " +
                        " Saldos ON Orden.Id_Ofa = Saldos.Id_Ofa_P LEFT OUTER JOIN " +
                        " Of_Accesorios ON Orden.Id_Ofa = Of_Accesorios.Id_Ofa_Papa " +
                    " WHERE        (Orden_Seg.Anulado = 0) AND (Orden.Despachada = 0) AND (Orden.Anulada = 0) AND (Orden_Seg.fecha_crea > CONVERT(DATETIME, '2016-01-01 00:00:00', 102)) AND (Orden.Tipo_Of = 'OF') AND " +
                      "   (Orden.Abierta = 1) " +
                    " GROUP BY Orden.Tipo_Of, RTRIM(Orden_Seg.Num_Of) + '-' + Orden_Seg.Ano_Of, Orden_Seg.Id_Ofa, Saldos.Id_Ofa_P " +
                " ORDER BY Orden";

            return BdDatos.ConsultarConDataReader(sql);

        }

        public SqlDataReader poblarOrigen()
        {
            string sql;

            sql = "SELECT     tiorigen_id, tiorigen_descripcion " +
                  "  FROM         lite_tipo_origen " +
                  "  WHERE     (tiorigen_activo = 1) AND (tiorigen_maestro = 1) " +
                   " ORDER BY tiorigen_descripcion";

            return BdDatos.ConsultarConDataReader(sql);

        }

        public SqlDataReader poblarMoneda()
        {
            string sql;
            sql = "SELECT  mon_id, mon_descripcion FROM   moneda WHERE    (mon_activo = 1)";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader poblarListaPais()
        {
            string sql;
            sql = "SELECT pai_id, pai_nombre FROM pais WHERE  (pai_id <> 74) ORDER BY pai_nombre";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public String guardarCiudad(int pais, int zona, string nombreciudad, string usuario)
        {
            string sql;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
            String mensaje = "";
            DataTable consulta = null;
            try
            {
                sql = "DECLARE @ultimoId INT;" +
                   "insert into dbo.ciudad (ciu_pai_id " +
                   ",ciu_zona " +
                   ",ciu_nombre  " +
                   ",ciu_usu_actualiza ) " +
                   "  values( " + pais + "," + zona + ",'" + nombreciudad + "', '"+usuario+"'); " +
                   " SET @ultimoId = SCOPE_IDENTITY(); SELECT @ultimoId AS id ;";
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

        public int actualizarCiudad(int idCiudad, int pais, int zona, string nombreciudad, string usuario)
        {
            string sql;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

            sql = "DECLARE @ultimoId INT;" +
                "update ciudad set ciu_pai_id = " + pais +
                ",ciu_zona= " + zona +
                ",ciu_nombre = '" + nombreciudad + "'" +
                ",ciu_usu_actualiza = '" + usuario + "'" +
                "  where ciu_id =  " + idCiudad + "; ";              
            
            return BdDatos.Actualizar(sql);            
           
        }

        public SqlDataReader poblarListaSubzona(int pais)
        {
            string sql;
            sql = "SELECT        ISNULL(zonac_id, 0) AS idZona, ISNULL(zonac_nom, 0) AS NombreZona " +
            " FROM            zonaCiudad    WHERE        (zonac_pais =" + pais + ")   ORDER BY NombreZona ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader poblarListaTecnicos()
        {
            string sql;
            sql = "SELECT     empleado.emp_usu_num_id AS cedula, empleado.emp_nombre_mayusculas + ' ' + empleado.emp_apellidos_mayusculas AS empleado "+
                    "FROM         rolapp INNER JOIN "+
                    "usuario ON rolapp.rap_id = usuario.usu_rap_id INNER JOIN "+
                    "empleado ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id "+
                    "WHERE     (rolapp.rap_id = 16) AND (usuario.usu_activo = 1) AND (empleado.emp_estado_laboral = 'ACTIVO') " +
                    "ORDER BY usuario.usu_login";
            return BdDatos.ConsultarConDataReader(sql);


        }


        public SqlDataReader poblarCiudadesRepresentantesColombia(int rol)
        {
            string sql;

            sql = "SELECT ciudad_representante.cr_ciu_id, ciudad.ciu_nombre FROM ciudad_representante INNER JOIN ciudad ON " +
                  "ciudad_representante.cr_ciu_id = ciudad.ciu_id WHERE (ciudad_representante.cr_rc_id = " + rol + ")AND (ciu_id <> 573 AND ciu_id <> 569) " +
                  "ORDER BY ciudad.ciu_nombre ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader poblarListaCiudades(int pais)
        {
            string sql;
            sql = "SELECT ciu_id, ciu_nombre FROM ciudad WHERE ciu_pai_id = " + pais + " AND (ciu_id <> 573 AND ciu_id <> 569) ORDER BY ciu_nombre";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader poblarListaZonas()
        {
            string sql;
            sql = "SELECT        grpa_id, grpa_gp1_nombre  FROM  fup_grupo_pais  WHERE   (activo = 1)  ORDER BY grpa_gp1_nombre ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public int cerrarConexion()
        {
            return BdDatos.desconectar();
        }

        //Consulta las plantas por usuario
        public SqlDataReader poblarPlantaUsuario(string usuario)
        {
            string sql = "";
            sql = "SELECT pf.planta_descripcion, pf.planta_id, pu.plantausu_idplanta from planta_forsa pf JOIN planta_usuario pu ON pu.plantausu_idplanta = pf.planta_id JOIN usuario u ON u.usu_siif_id = pu.plantausu_idusuario WHERE u.usu_login ='" + usuario + "' AND pu.plantausu_activo = 1 AND pf.planta_id <> 3 ;";
            return BdDatos.ConsultarConDataReader(sql);
        }
               

        //Consulta las plantas por usuario
        public SqlDataReader poblarPlantaGeneral()
        {
            string sql = "";
            sql = "SELECT    planta_id AS IdPlanta, planta_descripcion AS Planta FROM   planta_forsa WHERE     (planta_activo = 1)";
            return BdDatos.ConsultarConDataReader(sql);
        }

        /// <summary>
        /// Metodo encargado de consultar los paises habilitados para un representante comercial
        /// Autor: Global BI
        /// Fecha: 09-09-2018
        /// </summary>        
        /// <param name="representante">id del representante comercial</param>
        /// <returns>Lista de paises</returns>
        public List<Pais> obtenerListaPaisRepresentante(int representante)
        {
            string sql;

            sql = "SELECT pais.pai_nombre, pais.pai_id, representantes_comerciales.rc_id, pais.pai_moneda " +
                "FROM pais_representante INNER JOIN representantes_comerciales ON " +
                "pais_representante.pr_id_representante = representantes_comerciales.rc_id INNER JOIN " +
                "pais ON pais_representante.pr_id_pais = pais.pai_id " +
                "WHERE (representantes_comerciales.rc_id = " + representante + ") AND (pais_representante.pr_activo = 1)  AND " +
                "(pai_id <> 74)" +
                "ORDER BY pai_nombre DESC";

            DataTable dt = BdDatos.CargarTabla(sql);
            List<Pais> lstPais = dt.AsEnumerable()
                .Select(row => new Pais
                {
                    Id = (int)row["pai_id"],
                    Nombre = (string)row["pai_nombre"],
                    IdMoneda = (int)row["pai_moneda"]
                }).ToList();
            dt.Clear();
            dt.Dispose();
            return lstPais;
        }

        /// <summary>
        /// Metodo encargado de consultar los paises
        /// Autor: Global BI
        /// Fecha: 09-09-2018
        /// </summary>        
        /// <param name="representante">id del representante comercial</param>
        /// <returns>Lista de paises</returns>
        public List<Pais> obtenerListaPais()
        {
            string sql;
            sql = "SELECT pai_id, pai_nombre, pai_moneda FROM pais WHERE  (pai_id <> 74) ORDER BY pai_nombre";
            DataTable dt = BdDatos.CargarTabla(sql);
            List<Pais> lstPais = dt.AsEnumerable()
                .Select(row => new Pais
                {
                    Id = (int)row["pai_id"],
                    Nombre = (string)row["pai_nombre"],
                    IdMoneda = (int)row["pai_moneda"]
                }).ToList();
            dt.Clear();
            dt.Dispose();
            return lstPais;
        }

        /// <summary>
        /// Metodo encargado de consultar los paises
        /// Autor: Global BI
        /// Fecha: 09-09-2018
        /// </summary>        
        /// <param name="rol">id del representante comercial</param>
        /// <returns>Lista de ciudades</returns>
        public List<Ciudad> obtenerCiudadesRepresentantesColombia(int representanteComercialId)
        {
            string sql;

            sql = "SELECT ciudad_representante.cr_ciu_id, ciudad.ciu_nombre FROM ciudad_representante INNER JOIN ciudad ON " +
                  "ciudad_representante.cr_ciu_id = ciudad.ciu_id WHERE (ciudad_representante.cr_rc_id = " + representanteComercialId + ")AND (ciu_id <> 573 AND ciu_id <> 569) " +
                  "ORDER BY ciudad.ciu_nombre ";

            DataTable dt = BdDatos.CargarTabla(sql);
            List<Ciudad> lstCiudad = dt.AsEnumerable()
                .Select(row => new Ciudad
                {
                    Id = (int)row["cr_ciu_id"],
                    Nombre = (string)row["ciu_nombre"]
                }).ToList();
            dt.Clear();
            dt.Dispose();
            return lstCiudad;
        }

        /// <summary>
        /// Metodo encargado de consultar las ciudades
        /// Autor: Global BI
        /// Fecha: 09-09-2018
        /// </summary>        
        /// <param name="pais">id del pais seleccionado</param>
        /// <returns>Lista de ciudades</returns>
        public List<Ciudad> obtenerListaCiudades(int pais)
        {
            string sql;
            sql = "SELECT ciu_id, ciu_nombre FROM ciudad WHERE ciu_pai_id = " + pais + " AND (ciu_id <> 573 AND ciu_id <> 569) ORDER BY ciu_nombre";
            DataTable dt = BdDatos.CargarTabla(sql);
            List<Ciudad> lstCiudad = dt.AsEnumerable()
                .Select(row => new Ciudad
                {
                    Id = (int)row["ciu_id"],
                    Nombre = (string)row["ciu_nombre"]
                }).ToList();
            dt.Clear();
            dt.Dispose();
            return lstCiudad;
        }

        /// <summary>
        /// Metodo encargado de consultar las monedas
        /// Autor: Global BI
        /// Fecha: 09-09-2018
        /// </summary>
        /// <returns>Lista de monedas</returns>
        public List<Moneda> obtenerMoneda()
        {
            string sql;
            sql = "SELECT  mon_id, mon_descripcion FROM   moneda WHERE    (mon_activo = 1)";
            DataTable dt = BdDatos.CargarTabla(sql);
            List<Moneda> lstMoneda = dt.AsEnumerable()
                .Select(row => new Moneda
                {
                    Id = (int)row["mon_id"],
                    Descripcion = (string)row["mon_descripcion"]
                }).ToList();
            dt.Clear();
            dt.Dispose();
            return lstMoneda;
        }
    }    
}
