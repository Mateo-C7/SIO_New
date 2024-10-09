using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Text;
using CapaDatos;


namespace CapaControl
{
    public class ControlVisitaComercial
    {
        ////Trae los datos del usuario que ingreso al SIO
        public String usuarioActual(String usuario)
        {
            String permisos = "";
            String sql = "SELECT (CASE WHEN rolapp.rap_gerente = 1 THEN 'GERENTE' WHEN rolapp.rap_lvice = 1 THEN 'VICE' WHEN rolapp.rap_lrepre = 1 THEN 'REPRE' WHEN rolapp.rap_lagente = 1 THEN 'AGENTE' ELSE 'NOROL' END) AS permisos "
            + " FROM   usuario INNER JOIN  rolapp ON usuario.usu_rap_id = rolapp.rap_id "
            + "  WHERE  (usuario.usu_login = '" + usuario + "')";
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                permisos = row["permisos"].ToString();
            }
            return permisos;
        }
        //Consulta el procedimiento para traer los representantes
        public DataTable cargarAgentes(String usuario)
        {
            DataTable consulta = null;
            String proc = "EXEC VisitasCargarAgentes '" + usuario + "'";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Carga el combo de los motivos
        public DataTable cargarMotivos()
        {
            String sql = "SELECT  motivov_id AS idMotivo, motivov_nombre AS nomMotivo FROM  motivos_visita WHERE (motivov_lactivo = 1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga el combo de los clientes
        public DataTable cargarClientes(String texto, String usuario)
        {
            String proc = "EXEC VisitasCargarClientes '" + usuario + "', '" + texto + "'";
            DataTable consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Inserta en la tabal mov_visitas_mercadeo
        public String insertMovVisMerca(String usuPlanifica, String usuEjecuta, String idCliente, String idMotivo, String bdOrigen, String filtroFeria, String idFeria, String filtroProyec, String idProyec, String objetivo, String fechaAge, String idCiu, String idContacto, List<string> listProces, int remoto, int soporte)
        {
            String idVisitaPrincipal = "" ,sql = "";
            DataTable consulta = null;
            try
            {
                //Inserta el id principal
                sql = " DECLARE @ultimoId INT; "
                + " INSERT INTO mov_visita_mercadeo (vis_lactiva, vis_dfecha_plan, vis_usu_plan, vis_usu_ejecuta, vis_cli_id, vis_motivov_id, vis_bd_origen, vis_usu_aprobo, vis_dfecha_aprob, vis_laprobada, vis_objetivo, vis_usu_agendo, vis_dfecha_agendo, vis_idciudad " + filtroFeria + filtroProyec + ", vis_idcontacto, vis_remoto, vis_soporte_tecnico) "
                + " VALUES (1, CONVERT (char(10), SYSDATETIME(), 103),'" + usuPlanifica + "', '" + usuEjecuta + "', " + idCliente + ", " + idMotivo + ", '" + bdOrigen + "', '" + usuPlanifica + "', SYSDATETIME(), 1, '" + objetivo + "', '" + usuPlanifica + "', '" + fechaAge + "', " + idCiu + " " + idFeria + idProyec + " , " + idContacto + " , " + remoto + ", " + soporte + "); "
                + " SET @ultimoId = SCOPE_IDENTITY(); SELECT @ultimoId AS id;";
                consulta = BdDatos.CargarTabla(sql);

                //Obtiene el id principal               
                idVisitaPrincipal = consulta.Rows[0]["id"].ToString();

                //Inserta los acompanantes del id principal
                sql = "";
                for (int i = 0; i < listProces.Count; i++)
                {
                    sql += " INSERT INTO mov_vis_procesos(vis_pro_vis_ncons, vis_pro_usu) Values (" + idVisitaPrincipal + ", '" + listProces.ElementAt(i).ToString() + "');";
                }
                if(sql != "")
                    BdDatos.Actualizar(sql);

                //Inserta la visita de los acompanantes y sus acompanantes
                sql = "";
                string idVisita = "";                
                for (int i=0; i<listProces.Count; i++)
                {                    
                    sql = "DECLARE @ultimoId INT;" +              
                         " INSERT INTO mov_visita_mercadeo (vis_lactiva, vis_dfecha_plan, vis_usu_plan, vis_usu_ejecuta, vis_cli_id, vis_motivov_id, vis_bd_origen, vis_usu_aprobo, vis_dfecha_aprob, vis_laprobada, vis_objetivo, vis_usu_agendo, vis_dfecha_agendo, vis_idciudad " + filtroFeria + filtroProyec + ", vis_idcontacto, vis_id_visita_principal, vis_remoto) "
                         + " VALUES (1, CONVERT (char(10), SYSDATETIME(), 103),'" + listProces.ElementAt(i).ToString() + "', '" + listProces.ElementAt(i).ToString() + "', " + idCliente + ", " + idMotivo + ", '" + bdOrigen + "', '" + listProces.ElementAt(i).ToString() + "', SYSDATETIME(), 1, '" + objetivo + "', '" + listProces.ElementAt(i).ToString() + "', '" + fechaAge + "', " + idCiu + " " + idFeria + idProyec + " , " + idContacto + " , " + idVisitaPrincipal + ", " + remoto + "); "
                         +" SET @ultimoId = SCOPE_IDENTITY(); SELECT @ultimoId AS id;";
                    consulta = BdDatos.CargarTabla(sql);

                    idVisita = consulta.Rows[0]["id"].ToString();

                    List<string> listProcesNew = new List<string>(listProces);
                    listProcesNew.Add(usuPlanifica);
                    listProcesNew.Remove(listProces.ElementAt(i));
                    //Inserta los acompanantes del id principal
                    sql = "";
                    for (int j = 0; j < listProcesNew.Count; j++)
                    {
                        sql += " INSERT INTO mov_vis_procesos(vis_pro_vis_ncons, vis_pro_usu) Values (" + idVisita + ", '" + listProcesNew.ElementAt(j).ToString() + "');";
                    }
                    if(sql != "")
                        BdDatos.Actualizar(sql);
                }
            }
            catch
            {
                idVisitaPrincipal = "ERROR EN EL QUERY (mov_visita_mercadeo)";
            }
            return idVisitaPrincipal;
        }
        //Carga la tabla de visitas
        public DataTable cargarTablaVis(String usuario, String rango)
        {
            String filtro = "";
            if (rango == "GERENTE")
            {
                filtro = " AND (vis_usu_ejecuta IN ( "
                + " SELECT  usuario.usu_login AS usuario "
                + " FROM    usuario INNER JOIN   representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
                + " pais_representante ON pais_representante.pr_id_representante = representantes_comerciales.rc_id  "
                + " WHERE   (usuario.usu_activo = 1) AND (pais_representante.pr_activo = 1) AND (representantes_comerciales.rc_activo = 1) "
                + " AND (pais_representante.pr_id_pais IN (SELECT  pais_representante.pr_id_pais  "
                + " FROM  usuario INNER JOIN   representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
                + " pais_representante ON pais_representante.pr_id_representante = representantes_comerciales.rc_id "
                + " WHERE  (pais_representante.pr_activo = 1) AND (usuario.usu_login = '" + usuario + "'))) "
                + " GROUP BY usuario.usu_login)) ";
            }
            else if (rango == "VICE") { filtro = ""; }
            else { filtro = "AND (mov_visita_mercadeo.vis_usu_ejecuta  = '" + usuario + "')"; }
            DataTable consulta = null;
            String sql = "SELECT    (CASE WHEN mov_visita_mercadeo.vis_feria IS NULL "
                       + " THEN Vista_Clientes_Todos.nomCliente ELSE Vista_Clientes_Todos.nomCliente + ' ' + eveFer.tifuente_descripcion END) AS cliente, "
                       + " (CASE WHEN mov_visita_mercadeo.vis_feria IS NULL THEN Vista_Clientes_Todos.paisCliente ELSE pais_1.pai_nombre END) AS pais,  "
                       + " (CASE WHEN mov_visita_mercadeo.vis_feria IS NULL THEN Vista_Clientes_Todos.ciudadCliente ELSE ciudad_1.ciu_nombre END) AS ciudad,  "
                       + " Vista_Clientes_Todos.telefono, Vista_Clientes_Todos.email, representantes_comerciales.rc_descripcion AS ejeVisita,  "
                       + " (CASE WHEN mov_visita_mercadeo.vis_laprobada = 1 THEN 'Aprobado' WHEN mov_visita_mercadeo.vis_laprobada = 0 THEN 'No Aprobado' WHEN mov_visita_mercadeo.vis_laprobada "
                       + " IS NULL THEN 'Esperando Aprobacion' END) AS aprobacion, mov_visita_mercadeo.vis_bd_origen AS bdOrigen, mov_visita_mercadeo.vis_ncons,  "
                       + " mov_visita_mercadeo.vis_dias AS dias "
                       + " FROM       ciudad AS ciudad_1 RIGHT OUTER JOIN "
                       + " Vista_Clientes_Todos INNER JOIN "
                       + " motivos_visita INNER JOIN "
                       + " mov_visita_mercadeo ON motivos_visita.motivov_id = mov_visita_mercadeo.vis_motivov_id ON  "
                       + " Vista_Clientes_Todos.idCliente = mov_visita_mercadeo.vis_cli_id AND   Vista_Clientes_Todos.bdOrigen = mov_visita_mercadeo.vis_bd_origen INNER JOIN "
                       + " usuario INNER JOIN "
                       + " representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id ON  "
                       + " mov_visita_mercadeo.vis_usu_ejecuta = usuario.usu_login LEFT OUTER JOIN "
                       + " lite_tipo_fuente eveFer mov_visita_mercadeo.vis_feria = eveFer.tifuente_id LEFT OUTER JOIN "
                       + " pais AS pais_1 ON eveFer.fer_pai_id = pais_1.pai_id ON ciudad_1.ciu_id = eveFer.fer_ciu_id "
                       + " WHERE  (mov_visita_mercadeo.vis_lactiva = 1) " + filtro + " ORDER BY vis_ncons DESC ";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga los clientes de la base de datos sql lite
        public DataTable cargarTablaCliLite(String usuario, String filCliente, String filOrigen, String filFuente, String ciudad, String periodo)
        {
            DataTable consulta = null;
            //String sql = " SELECT cliente_lite.clite_id AS idClienteL, cliente_lite.clite_empresa AS nomClieL, pais.pai_nombre AS paisClieL, ciudad.ciu_nombre AS ciudadClieL, "
            //          + " lite_tipo_origen.tiorigen_descripcion AS origenClieL, lite_tipo_fuente.tifuente_descripcion AS fuenteClieL, clite_id_pais AS idPaisClieL, clite_proyecto AS proyClieL"
            //          + " FROM   cliente_lite INNER JOIN "
            //          + " lite_tipo_modalidad ON cliente_lite.clite_id_tipomodalidad = lite_tipo_modalidad.timoda_id INNER JOIN "
            //          + " lite_tipo_fuente ON cliente_lite.clite_id_tipofuente = lite_tipo_fuente.tifuente_id INNER JOIN "
            //          + " lite_tipo_proyecto ON cliente_lite.clite_id_tipoproyecto = lite_tipo_proyecto.tiproy_id INNER JOIN "
            //          + " lite_tipo_producto ON cliente_lite.clite_id_tipoproducto = lite_tipo_producto.tiprodu_id INNER JOIN "
            //          + " lite_tipo_origen ON lite_tipo_fuente.tifuente_origen_id = lite_tipo_origen.tiorigen_id INNER JOIN "
            //          + " pais ON cliente_lite.clite_id_pais = pais.pai_id INNER JOIN "
            //          + " ciudad ON cliente_lite.clite_id_ciudad = ciudad.ciu_id "
            //          + " WHERE  (cliente_lite.clite_activo = 1) "
            //          + usuario + filCliente + " " + filOrigen + " " + filFuente + "";
            String sql = " SELECT        cliente.cli_id AS idClienteL, cliente.cli_nombre AS nomClieL, pais.pai_nombre AS paisClieL, ciudad.ciu_nombre AS ciudadClieL, lite_tipo_origen.tiorigen_descripcion AS origenClieL, " +
                         " lite_tipo_fuente.tifuente_descripcion AS fuenteClieL, cliente.cli_pai_id AS idPaisClieL, ISNULL(obra.obr_nombre, ' ') AS proyClieL, isnull(obra.obr_periodo_sim, '') as periodo " +
                         " FROM lite_tipo_origen INNER JOIN " +
                         " lite_tipo_fuente ON lite_tipo_origen.tiorigen_id = lite_tipo_fuente.tifuente_origen_id INNER JOIN " +
                         " cliente ON lite_tipo_fuente.tifuente_id = cliente.cliente_tipo_fuente_id INNER JOIN " +
                         " pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN " +
                         " ciudad ON cliente.cli_ciu_id = ciudad.ciu_id AND pais.pai_id = ciudad.ciu_pai_id LEFT OUTER JOIN " +
                         " obra ON cliente.cli_id = obra.obr_cli_id " +
                         " WHERE(lite_tipo_fuente.tifuente_activo = 1) AND(lite_tipo_origen.tiorigen_activo = 1) AND(cliente.cli_activo = 1) " +
                          usuario + " " + filCliente + " " + filOrigen + " " + filFuente + " " + ciudad + " " + periodo;
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga las ciudades de los representantes
        public DataTable cargarCiuRepre(String usuario)
        {
            DataTable consulta = null;
            String sql = "SELECT  ciudad_representante.cr_ciu_id FROM    usuario INNER JOIN "
            + " representantes_comerciales  ON "
            + " usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
            + " pais_representante ON "
            + " representantes_comerciales.rc_id = pais_representante.pr_id_representante FULL OUTER JOIN "
            + " ciudad_representante ON representantes_comerciales.rc_id = ciudad_representante.cr_rc_id "
            + " WHERE  (usuario.usu_login = 'adrianazuluaga') AND (pais_representante.pr_activo = 1) AND (representantes_comerciales.rc_activo = 1)";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga el combo del origen
        public DataTable cargarOrigen()
        {
            String sql = "SELECT tiorigen_id AS idOrigen, tiorigen_descripcion AS nomOrigen  FROM    lite_tipo_origen   WHERE     (tiorigen_activo = 1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga el combo del fuente
        public DataTable cargarFuente(String idOrigen)
        {
            String sql = "SELECT  tifuente_id AS idFuente, tifuente_descripcion AS nomFuente  FROM    lite_tipo_fuente  WHERE   (tifuente_activo = 1) AND (tifuente_origen_id = " + int.Parse(idOrigen) + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga el combo de los tipos de agentes
        public DataTable cargarTiposAgentes()
        {
            String sql = "SELECT    tipoag_rol AS rol,  tipoag_nombre AS nomTipo    FROM   vis_tipo_agente    WHERE     (tipoag_lvalido = 1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Consulta si el motivo es tipo feria
        public Boolean consultaMotivo(String idMotivo)
        {
            Boolean feria = false;
            DataTable consulta = null;
            String sql = "SELECT  motivov_lferia AS feria FROM  motivos_visita  WHERE  (motivov_id = " + idMotivo + ")";
            consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                feria = Boolean.Parse(row["feria"].ToString());
            }
            return feria;
        }
        //Carga el combo de los proyectos
        public DataTable cargarProyec(String idCliente)
        {
            String sql = "SELECT  obr_id AS idProy, obr_nombre AS nomProy  FROM  obra   WHERE   (obr_cli_id = " + idCliente + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga el cliente feria
        public DataTable cargarCliFeria()
        {
            String sql = "SELECT  cli_id AS idCliente, cli_nombre AS nomCliente  FROM  cliente  WHERE   (cli_feria = 1) AND (cli_nombre = 'Feria')";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        /**********************************************************************************************************************************************************/
        //Carga la tabla de visitas con filtro con usuario,agenda, tipo y zona
        public DataTable recargarTablaVis(String usuario, String agenda, String tipo, String zona, String usuIniSession, String rango)
        {
            String filtro = "";
            if (rango == "GERENTE")
            {
                filtro = "AND (vis_usu_ejecuta IN ( SELECT  usuario.usu_login AS usuario "
              + "  FROM    usuario INNER JOIN   representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
              + "  pais_representante ON pais_representante.pr_id_representante = representantes_comerciales.rc_id  "
              + "  WHERE   (usuario.usu_activo = 1) AND (pais_representante.pr_activo = 1) AND (representantes_comerciales.rc_activo = 1) "
              + "  AND (pais_representante.pr_id_pais IN (SELECT  pais_representante.pr_id_pais  "
              + "  FROM  usuario INNER JOIN   representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
              + "  pais_representante ON pais_representante.pr_id_representante = representantes_comerciales.rc_id "
              + "  WHERE  (pais_representante.pr_activo = 1) AND (usuario.usu_login = '" + usuIniSession + "'))) "
              + "  GROUP BY usuario.usu_login)) ";
            }
            else
            {
                filtro = "";
            }
            DataTable consulta = null;
            String sql = "SELECT  mvm.vis_ncons AS numVisita, cliente.cli_nombre AS cliente, pais.pai_nombre AS pais, ciudad.ciu_nombre AS ciudad, "
                      + "  cliente.cli_telefono AS telefono, cliente.cli_mail AS email, representantes_comerciales.rc_descripcion AS ejeVisita,  "
                      + "  (CASE WHEN mvm.vis_laprobada = 1 THEN 'Aprobado' WHEN mvm.vis_laprobada = 0 THEN 'No Aprobado' "
                      + "  WHEN mvm.vis_laprobada IS NULL THEN 'Esperando Aprobacion' END) AS aprobacion, mvm.vis_bd_origen AS bdOrigen , rc_email AS correoComer "
                      + "  FROM  usuario INNER JOIN "
                      + "  mov_visita_mercadeo mvm INNER JOIN "
                      + "  motivos_visita ON mvm.vis_motivov_id = motivos_visita.motivov_id INNER JOIN "
                      + "  cliente ON mvm.vis_cli_id = cliente.cli_id INNER JOIN "
                      + "  pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN "
                      + "  ciudad ON cliente.cli_ciu_id = ciudad.ciu_id ON usuario.usu_login = mvm.vis_usu_ejecuta INNER JOIN "
                      + "  representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
                      + "  rolapp ON usuario.usu_rap_id = rolapp.rap_id INNER JOIN "
                      + "  Vista_Clientes_Todos vct ON vct.idCliente =  mvm.vis_cli_id "
                      + "  WHERE (mvm.vis_lactiva = 1)  " + usuario + agenda + tipo + zona + filtro
                      + "  ORDER BY vis_ncons DESC ";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Edita el estado de la visita
        public String editarEstadoVisita(String numVisita, String estadoNew, String aprobo, String usuAprobo)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE mov_visita_mercadeo SET vis_usu_aprobo = '" + usuAprobo + "', mov_visita_mercadeo.vis_laprobada = " + estadoNew + ", vis_dfecha_aprob = " + aprobo + " WHERE  mov_visita_mercadeo.vis_ncons = " + numVisita + "";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY AL MODIFICAR (mov_visita_mercadeo)";
            }
            return mensaje;
        }
        //Carga el combo de los representantes
        public DataTable cargarRepresentates(String GenAgen, String zona, String usuario, String rango)
        {
            String sql = "";
            if (rango == "VICE")
            {
                sql = "SELECT  usuario.usu_login AS usuario,  representantes_comerciales.rc_descripcion AS nombre, 1 AS orden "
                          + "  FROM  usuario INNER JOIN representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
                          + "  rolapp ON usuario.usu_rap_id = rolapp.rap_id INNER JOIN pais_representante ON representantes_comerciales.rc_id = pais_representante.pr_id_representante INNER JOIN "
                          + "  pais ON pais_representante.pr_id_pais = pais.pai_id WHERE   (usuario.usu_activo = 1) " + GenAgen + " AND (representantes_comerciales.rc_activo = 1)  " + zona + " AND  (pais_representante.pr_activo = 1) AND  (rap_lrepre = 1)"
                          + "  OR (usuario.usu_activo = 1) " + GenAgen + " AND (representantes_comerciales.rc_activo = 1)  " + zona + " AND (pais_representante.pr_activo = 1) AND (rap_lagente = 1)"
                          + "  OR (usuario.usu_activo = 1) " + GenAgen + " AND (representantes_comerciales.rc_activo = 1)  " + zona + " AND (pais_representante.pr_activo = 1) AND (rap_gerente = 1)"
                          + "  GROUP BY usuario.usu_login, representantes_comerciales.rc_descripcion UNION SELECT 'Todos' AS usuario,'Todos' AS nombre, 0 AS orden ORDER BY orden";
            }
            else
            {
                sql = " SELECT      usuario.usu_login AS usuario, representantes_comerciales.rc_descripcion AS nombre, 1 AS orden "
                       + "  FROM    usuario INNER JOIN representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN"
                       + "  pais_representante ON pais_representante.pr_id_representante = representantes_comerciales.rc_id INNER JOIN "
                       + "  pais ON pais_representante.pr_id_pais = pais.pai_id INNER JOIN "
                       + "  rolapp ON usuario.usu_rap_id = rolapp.rap_id"
                       + "  WHERE     (usuario.usu_activo = 1) AND (representantes_comerciales.rc_activo = 1) AND (usuario.usu_login = '" + usuario + "') " + zona + GenAgen + ""
                       + "  UNION "
                       + "  SELECT  usuario.usu_login AS usuario, representantes_comerciales.rc_descripcion AS nombre, 1 AS orden "
                       + "  FROM    usuario INNER JOIN "
                       + "  representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
                       + "  pais_representante ON pais_representante.pr_id_representante = representantes_comerciales.rc_id INNER JOIN"
                       + "  pais ON pais_representante.pr_id_pais = pais.pai_id INNER JOIN "
                       + "  rolapp ON usuario.usu_rap_id = rolapp.rap_id "
                       + "  WHERE   (usuario.usu_activo = 1) AND (pais_representante.pr_activo = 1) AND (representantes_comerciales.rc_activo = 1) AND (rap_lrepre = 1) " + zona + GenAgen + ""
                       + "  AND (pais_representante.pr_id_pais IN (SELECT  pais_representante.pr_id_pais FROM  usuario INNER JOIN "
                       + "  representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
                       + "  pais_representante ON pais_representante.pr_id_representante = representantes_comerciales.rc_id "
                       + "  WHERE     (usuario.usu_activo = 1) AND (pais_representante.pr_activo = 1) AND (representantes_comerciales.rc_activo = 1) AND (usuario.usu_login = '" + usuario + "'))) "
                       + "  OR      (usuario.usu_activo = 1) AND (pais_representante.pr_activo = 1) AND (representantes_comerciales.rc_activo = 1) AND (rap_lagente = 1) " + zona + GenAgen + ""
                       + "  AND (pais_representante.pr_id_pais IN (SELECT  pais_representante.pr_id_pais FROM  usuario INNER JOIN "
                       + "  representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
                       + "  pais_representante ON pais_representante.pr_id_representante = representantes_comerciales.rc_id "
                       + "  WHERE     (usuario.usu_activo = 1) AND (pais_representante.pr_activo = 1) AND (representantes_comerciales.rc_activo = 1) AND  (usuario.usu_login = '" + usuario + "'))) "
                       + "  UNION SELECT 'Todos' AS usuario,'Todos' AS nombre, 0 AS orden ORDER BY orden ";
            }
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga combo de las zonas
        public DataTable cargarZonas()
        {
            String sql = "SELECT   grpa_id AS idZona, grpa_gp1_nombre AS nombre  FROM   fup_grupo_pais ";
            //String sql = "SELECT zonap_id AS idZona, zonap_nom AS nombre FROM  zonaPais  WHERE  (zonap_activo = 1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        /********************************************************************************************************************************************************/
        //Me trae las agendas que estan asociadas al usuario
        public DataTable cargarComboAgenAgendamiento(String usuario)
        {
            DataTable consulta = null;
            String sql = "SELECT   agenda_visita.ag_id AS idAgenda, agenda_visita.ag_nombre + ' ' + CONVERT(VARCHAR(MAX), agenda_visita.vis_dfecha_plan_ini) + '  /  ' + CONVERT(VARCHAR(MAX), "
                      + "  agenda_visita.vis_dfecha_plan_fin) AS nomAgenda FROM agenda_visita INNER JOIN "
                      + "  mov_visita_mercadeo ON agenda_visita.ag_id = mov_visita_mercadeo.vis_agenda_id "
                      + "  WHERE   (agenda_visita.ag_lvalido = 1) AND (mov_visita_mercadeo.vis_usu_ejecuta = '" + usuario + "') AND (mov_visita_mercadeo.vis_lactiva = 1) AND  "
                      + "  (mov_visita_mercadeo.vis_laprobada = 1) "
                      + "  GROUP BY agenda_visita.ag_id, agenda_visita.ag_nombre, agenda_visita.vis_dfecha_plan_ini, agenda_visita.vis_dfecha_plan_fin ";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Me trae todas las agendas
        public DataTable cargarComboAgenTodas()
        {
            DataTable consulta = null;
            String sql = "SELECT   agenda_visita.ag_id AS idAgenda, agenda_visita.ag_nombre + ' ' + CONVERT(VARCHAR(MAX), agenda_visita.vis_dfecha_plan_ini) + '  /  ' + CONVERT(VARCHAR(MAX), "
                      + "  agenda_visita.vis_dfecha_plan_fin) AS nomAgenda FROM agenda_visita "
                      + "  WHERE   (agenda_visita.ag_lvalido = 1) "
                      + "  GROUP BY agenda_visita.ag_id, agenda_visita.ag_nombre, agenda_visita.vis_dfecha_plan_ini, agenda_visita.vis_dfecha_plan_fin ";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Me trae las visitas que estan asociadas al usuario y con la agenda
        public DataTable cargarVisitasAgen(String usuario, String ano, String mes)//, int idAgenda
        {
            DataTable consulta = null;
            String sql = " SELECT   mvm.vis_ncons AS numVisita, CONVERT(VARCHAR(MAX), mvm.vis_ncons)  "
                     + " + '.' + (CASE WHEN mvm.vis_feria IS NULL THEN vct.nomCliente ELSE vct.nomCliente + ' ' + eveFer.tifuente_descripcion END)  AS nomVisita, CONVERT(char(10),   "
                     + " mvm.vis_dfecha_agendo, 103) AS fechaAgendada, (CASE WHEN vis_lcierre = 1 THEN '3' WHEN vis_cancelada = 1 THEN '4' WHEN vis_dfecha_ejecuto IS NOT NULL THEN '2' ELSE '1' END) AS color, (CASE WHEN mvm.vis_lcierre = 1 THEN 'CERRADA' WHEN mvm.vis_cancelada = 1 THEN 'CANCELADA' WHEN mvm.vis_usu_ejecuto IS NOT NULL THEN 'EJECUTANDO' ELSE 'AGENDADA' END) AS estado  "
                     + " FROM   mov_visita_mercadeo mvm INNER JOIN  "
                     + " Vista_Clientes_Todos vct ON  vct.idCliente = mvm.vis_cli_id LEFT OUTER JOIN  "
                     + " lite_tipo_fuente  eveFer ON mvm.vis_feria = eveFer.tifuente_id  "
                     + " WHERE   (mvm.vis_lactiva = 1) AND (mvm.vis_dfecha_aprob IS NOT NULL) AND  (mvm.vis_dfecha_agendo IS NOT NULL) "
                     + " AND (mvm.vis_usu_ejecuta = '" + usuario + "') AND (MONTH(mvm.vis_dfecha_agendo) = '" + mes + "')  AND (YEAR(mvm.vis_dfecha_agendo) = '" + ano + "') ";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Me trae las visitas que estan asociadas al usuario y con la agenda
        public DataTable cargarTodasVisitasAgen(String usuario, String ano, String mes, String rango, String zona, String comercial) //int idAgenda,
        {
            String filtro = "";
            if (rango == "GERENTE")
            {
                if (comercial != "")
                {
                    filtro = "AND (mvm.vis_usu_ejecuta = '" + comercial + "')";
                }
                else
                {
                    filtro = "AND (mvm.vis_usu_ejecuta IN ( SELECT usuario.usu_login AS usuario FROM   usuario INNER JOIN "
                    + " representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
                    + " pais_representante ON pais_representante.pr_id_representante = representantes_comerciales.rc_id INNER JOIN "
                    + " pais ON pais_representante.pr_id_pais = pais.pai_id "
                    + " WHERE  (usuario.usu_activo = 1) AND (pais_representante.pr_activo = 1) AND (representantes_comerciales.rc_activo = 1) AND (pais_representante.pr_id_pais IN "
                    + " (SELECT  pais_representante_1.pr_id_pais FROM   usuario AS usuario_1 INNER JOIN "
                    + " representantes_comerciales AS representantes_comerciales_1 ON "
                    + " usuario_1.usu_siif_id = representantes_comerciales_1.rc_usu_siif_id INNER JOIN "
                    + " pais_representante AS pais_representante_1 ON  "
                    + " pais_representante_1.pr_id_representante = representantes_comerciales_1.rc_id "
                    + " WHERE (pais_representante_1.pr_activo = 1) AND (usuario_1.usu_login = '" + usuario + "'))) " + zona + " "
                    + " GROUP BY usuario.usu_login )) ";
                }
            }
            else
            {
                if (comercial != "")
                {
                    filtro = "AND (mvm.vis_usu_ejecuta = '" + comercial + "')";
                }
                else
                {
                    filtro = "";
                }
            }
            DataTable consulta = null;
            String sql = " SELECT   mvm.vis_ncons AS numVisita, CONVERT(VARCHAR(MAX), mvm.vis_ncons) + '.' + representantes_comerciales.rc_descripcion + ':' + "
            + " (CASE WHEN mvm.vis_feria IS NULL THEN vct.nomCliente ELSE vct.nomCliente + ' ' + eveFer.tifuente_descripcion END)  AS nomVisita, CONVERT(char(10),  "
            + "  mvm.vis_dfecha_agendo, 103) AS fechaAgendada, (CASE WHEN vis_lcierre = 1 THEN '3' WHEN vis_dfecha_ejecuto IS NOT NULL  THEN '2' WHEN vis_cancelada = 1 THEN '4' ELSE '1' END) AS color, (CASE WHEN vis_lcierre = 1 THEN 'CERRADA' WHEN vis_cancelada = 1 THEN 'CANCELADA' WHEN vis_dfecha_ejecuto IS NOT NULL THEN 'EJECUTANDO' ELSE 'AGENDADA' END) AS estado "
            + " FROM    mov_visita_mercadeo mvm INNER JOIN "
            + " usuario ON mvm.vis_usu_ejecuta = usuario.usu_login INNER JOIN "
            + " representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
            + " pais_representante ON pais_representante.pr_id_representante = representantes_comerciales.rc_id INNER JOIN "
            + " Vista_Clientes_Todos vct ON  vct.bdOrigen = mvm.vis_bd_origen AND vct.idCliente = mvm.vis_cli_id  LEFT OUTER JOIN  "
            + " lite_tipo_fuente  eveFer ON mvm.vis_feria = eveFer.tifuente_id  "
            + " WHERE     (mvm.vis_lactiva = 1) AND (mvm.vis_dfecha_aprob IS NOT NULL) AND  "
            + " (mvm.vis_dfecha_agendo IS NOT NULL) AND (MONTH(mvm.vis_dfecha_agendo) = '" + mes + "') AND (YEAR(mvm.vis_dfecha_agendo) = '" + ano + "') " + filtro + zona
            + " GROUP BY mvm.vis_ncons, rc_descripcion, vis_feria, nomCliente, tifuente_descripcion,vis_dfecha_agendo,vis_lcierre,vis_dfecha_ejecuto,vis_cancelada "
            + " ORDER BY mvm.vis_ncons ";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga años para la agenda
        public DataTable cargarAños(Boolean act)
        {
            DataTable consulta = null;
            String sql = "SELECT mov_año_id AS ano, mov_año_activo AS anoAct FROM mov_año WHERE mov_año_activo = '" + act + "'";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Valida la fecha de ejecucion
        public DataTable ValidarFechaEjecucion(String idvisita)
        {
            DataTable consulta = null;
            String sql = "SELECT   COUNT(vis_ncons) AS ejecutada  FROM   mov_visita_mercadeo   WHERE  (vis_ncons = " + idvisita + ") AND (vis_dfecha_ejecuto IS NOT NULL)";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Inserta fecha de agenda en la tabla mov_visitas_mercadeo
        public String agregarFechaAgenda(String usuAgenda, String fechaAgen, String idVisita) // String nota,
        {
            String mensaje = "";
            try
            {
                String sql = "DECLARE @fecha CHAR(10); SET @fecha = '" + fechaAgen + "'; UPDATE mov_visita_mercadeo SET vis_usu_agendo = '" + usuAgenda + "', vis_dfecha_agendo = (CASE WHEN @fecha = 'NULL' THEN NULL ELSE CONVERT(DATETIME, @fecha) END) WHERE vis_ncons  = " + idVisita + " ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (mov_visita_mercadeo)";
            }
            return mensaje;
        }
        //Me trae los datos generales de las visitas para el popup
        public DataTable cargarDatosPopup(String idVisita)
        {
            DataTable consulta = null;
            String sql = "";

            sql = "SELECT mvm.vis_cli_id AS idcli, (CASE WHEN mvm.vis_feria IS NULL THEN vct.nomCliente ELSE vct.nomCliente + ' ' + eveFer.tifuente_descripcion END) AS nomCliente, "+
                      "pais.pai_nombre AS paisVis, ciudad.ciu_nombre AS ciuVis, motivos_visita.motivov_nombre AS motivo, (CASE WHEN mvm.vis_objetivo IS NULL " +
                      "THEN '' ELSE mvm.vis_objetivo END) AS objetivo, (CASE WHEN vis_lcierre IS NULL THEN 'false' ELSE vis_lcierre END) AS cierre, (CASE WHEN vis_cancelada IS NULL " +
                      "THEN 'false' ELSE vis_cancelada END) AS cancelada, (CASE WHEN mov_vis_viajes.via_id IS NULL THEN 'No tiene fecha' ELSE CONVERT(CHAR(10), " +
                      "mov_vis_viajes.via_dfechaIni, 103) END) AS fechaViajeIni, (CASE WHEN mov_vis_viajes.via_id IS NULL THEN 'No tiene fecha' ELSE CONVERT(CHAR(10), " +
                      "mov_vis_viajes.via_dfechaFin, 103) END) AS fechaViajeFin, " +
                      "ISNULL((SELECT(CASE WHEN mvm.vis_idcontacto IS NULL THEN 'No tiene' ELSE contacto_cliente.ccl_nombre + ' ' + contacto_cliente.ccl_apellido END) AS Expr1 " +
                      " FROM      mov_visita_mercadeo AS mvm INNER JOIN " +
                      "                   contacto_cliente ON mvm.vis_idcontacto = contacto_cliente.ccl_id " +
                      " WHERE(mvm.vis_ncons = " +idVisita+ ")), 'No Tiene') AS contacto, (CASE WHEN mvm.vis_remoto = 1 THEN 'Visita Remota' ELSE '' END) AS remoto, " +
                      " (CASE WHEN mvm.vis_soporte_tecnico = 1 THEN 'Soporte Tecnico' ELSE '' END) AS soporte, " +
                      "(SELECT obra.obr_nombre FROM obra INNER JOIN mov_visita_mercadeo mvm ON obra.obr_id = mvm.vis_proyecto where mvm.vis_ncons = "+idVisita+ ") as obra "+  
                      "FROM mov_visita_mercadeo AS mvm INNER JOIN " +
                      "Vista_Clientes_Todos AS vct ON mvm.vis_cli_id = vct.idCliente INNER JOIN " +
                      "motivos_visita ON mvm.vis_motivov_id = motivos_visita.motivov_id INNER JOIN " +
                      " ciudad ON mvm.vis_idciudad = ciudad.ciu_id INNER JOIN " +
                      "pais ON ciudad.ciu_pai_id = pais.pai_id LEFT OUTER JOIN " +
                      "mov_vis_viajes ON mvm.vis_viaje = mov_vis_viajes.via_id LEFT OUTER JOIN " +
                      "lite_tipo_fuente AS eveFer ON mvm.vis_feria = eveFer.tifuente_id " +
                      "WHERE(mvm.vis_ncons = " +idVisita+") ";
            
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public string cargarUsuariosAcompanantes(String idVisita)
        {
            string usuarios = "", sql = "";
            DataTable consulta = new DataTable();

            sql = "SELECT empleado.emp_nombre + ' ' + empleado.emp_apellidos AS Expr1 "+
                  "FROM mov_visita_mercadeo INNER JOIN "+
                  "mov_vis_procesos ON mov_visita_mercadeo.vis_ncons = mov_vis_procesos.vis_pro_vis_ncons INNER JOIN "+
                  "usuario INNER JOIN "+
                  "empleado ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id ON mov_vis_procesos.vis_pro_usu = usuario.usu_login "+
                  "WHERE(mov_visita_mercadeo.vis_ncons = "+ idVisita +")";
            consulta = BdDatos.CargarTabla(sql);

            for(int i = 0; i< consulta.Rows.Count; i++)
            {
                usuarios += consulta.Rows[i]["Expr1"].ToString() + " - ";
            }

            if (!string.IsNullOrEmpty(usuarios))
            {
                usuarios = usuarios.Substring(0, usuarios.Length - 2);
            }
            return usuarios;                
        }
        //Grafica
        public DataTable graficaVisitas(String usuario, String idAgenda)
        {
            DataTable consulta = null;
            String proc = "EXEC ReporteGraficaVisitasCOM '" + usuario + "', " + int.Parse(idAgenda) + "";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Carga los reponsables de las visitas
        public DataTable cargaResponVis(String usuario, String idAgenda)
        {
            DataTable consulta = null;
            String proc = " SELECT respv_id AS idRespVis, respv_nombre AS nomRespVis FROM  responsables_visita WHERE  (respv_lactivo = 1)";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Carga las actividades de las visitas
        public DataTable cargaActVis(String usuario, String idAgenda)
        {
            DataTable consulta = null;
            String proc = "SELECT  actv_id AS idActVis, actv_nombre AS nomActVis FROM  actividades_visita  WHERE   (actv_lvalido = 1) order by idActVis desc";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Carga los datos para la tabla Resumida
        public DataTable cargarTablaResVis(String fechaIni, String fechaFin, String usuario, String cliente)
        {
            DataTable consulta = null;
            String proc = " SELECT    mvc.vis_usu_ejecuta AS usuario, representantes_comerciales.rc_descripcion AS nomUsuario, "
                    + " COUNT(mvc.vis_dfecha_aprob) AS visPlan, COUNT(mvc.vis_dfecha_cierre) AS visEje,  "
                    + " COUNT(mvc.vis_dfecha_aprob) - COUNT(mvc.vis_dfecha_cierre) AS diferencia, CONVERT(VARCHAR,  "
                    + " COUNT(mvc.vis_dfecha_cierre) * 100 / COUNT(mvc.vis_dfecha_aprob)) + '%' AS porceCer, "
                    + " COUNT (mvc.vis_dfecha_cance) AS visCan,  CONVERT(VARCHAR,   COUNT(mvc.vis_dfecha_cance) * 100 / COUNT(mvc.vis_dfecha_aprob)) + '%' AS porceCan "
                    + " FROM   usuario INNER JOIN "
                    + " representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
                    + " mov_visita_mercadeo mvc ON usuario.usu_login = mvc.vis_usu_ejecuta INNER JOIN "
                    + " cliente ON mvc.vis_cli_id = cliente.cli_id "
                    + " WHERE  (mvc.vis_lactiva = 1) AND (mvc.vis_laprobada = 1)"
                    + " AND (mvc.vis_dfecha_agendo BETWEEN (CONVERT(DATETIME, '" + fechaIni + "', 103)) AND (CONVERT(DATETIME, '" + fechaFin + "', 103)))"
                    + " " + usuario + "  " + cliente + " "
                    + " GROUP BY representantes_comerciales.rc_descripcion, mvc.vis_usu_ejecuta ";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Carga los datos para la tabla Detallada
        public DataTable cargarTablaDetVis(String fechaIni, String fechaFin, String usuario, String filtro, String cliente)
        {
            DataTable consulta = null;
            String proc = " SELECT     mvc.vis_ncons AS numVis, mvc.vis_usu_ejecuta,  (CASE WHEN mvc.vis_feria IS NULL THEN vct.nomCliente ELSE vct.nomCliente + ' ' + eveFer.tifuente_descripcion END) AS cliente, "
            + " (CASE WHEN mvc.vis_feria IS NULL THEN vct.paisCliente ELSE pais.pai_nombre END) AS pais,  (CASE WHEN mvc.vis_feria IS NULL THEN vct.ciudadCliente ELSE ciudad.ciu_nombre END) AS ciudad, "
            + " (CASE WHEN mvc.vis_cancelada = 1  THEN 'C' WHEN mvc.vis_dfecha_cierre IS NULL THEN 'N' ELSE 'S' END) AS realizada,  "
            + " 'Objetivo' AS notAgen, 'Datos' AS datosEje,   'Datos' AS datosCierre, 'Datos' AS datosDoc  "
            + " FROM     mov_visita_mercadeo mvc INNER JOIN   "
            + " Vista_Clientes_Todos vct ON  mvc.vis_cli_id = vct.idCliente AND vct.bdOrigen = mvc.vis_bd_origen LEFT OUTER JOIN "
            + " lite_tipo_fuente  eveFer ON  mvc.vis_feria = eveFer.tifuente_id LEFT OUTER JOIN "
            + " pais ON eveFer.tifuente_pais = pais.pai_id LEFT OUTER JOIN "
            + " ciudad ON eveFer.tifuente_ciudad = ciudad.ciu_id "
            + " WHERE  (mvc.vis_dfecha_agendo  BETWEEN (CONVERT(DATETIME, '" + fechaIni + "', 103)) AND (CONVERT(DATETIME, '" + fechaFin + "', 103))) AND (mvc.vis_dfecha_aprob IS NOT NULL) "
            + " " + usuario + "  " + filtro + "  " + cliente + " "
            + " ORDER BY numVis DESC ";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Carga la nota de la visita
        public String cargarNota(String idVisita)
        {
            String nota = "";
            DataTable consulta = null;
            String sql = "SELECT (CASE WHEN vis_objetivo IS NULL THEN ' ' ELSE vis_objetivo END) AS objetivo FROM  mov_visita_mercadeo WHERE  (vis_ncons = " + idVisita + ")";
            consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                nota = row["objetivo"].ToString();
            }
            return nota;
        }
        //Carga las actividades, responsable y concluciones
        public DataTable cargarDatosEje(String idVisita)
        {
            DataTable consulta = null;
            String proc = "SELECT     actividades_visita.actv_nombre AS nomAct, responsables_visita.respv_nombre AS nomRes, (CASE WHEN mov_visita_mercadeo.vis_conclusion IS NULL THEN '' ELSE mov_visita_mercadeo.vis_conclusion END) AS conclucion"
                      + " FROM         mov_visita_mercadeo INNER JOIN "
                      + " mov_actividad_responsable ON mov_visita_mercadeo.vis_ncons = mov_actividad_responsable.vis_ncons INNER JOIN "
                      + " actividades_visita ON mov_actividad_responsable.mov_actv_id = actividades_visita.actv_id INNER JOIN "
                      + " responsables_visita ON mov_actividad_responsable.mov_respv_id = responsables_visita.respv_id "
                      + " WHERE     (mov_visita_mercadeo.vis_ncons = " + idVisita + ")";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Carga las actividades, responsable, respuesta y concluciones
        public DataTable cargarDatosRes(String idVisita)
        {
            DataTable consulta = null;
            String proc = "SELECT     actividades_visita.actv_nombre AS nomAct, responsables_visita.respv_nombre AS nomRes, (CASE WHEN mov_actividad_responsable.mov_respuesta IS NULL THEN CONVERT(VARCHAR(MAX), mov_visita_mercadeo.vis_fup) + ' ' + mov_visita_mercadeo.vis_version ELSE mov_actividad_responsable.mov_respuesta END) AS respuesta, (CASE WHEN mov_visita_mercadeo.vis_conclusion IS NULL THEN '' ELSE mov_visita_mercadeo.vis_conclusion END)  AS conclucion"
                      + " FROM         mov_visita_mercadeo INNER JOIN "
                      + " mov_actividad_responsable ON mov_visita_mercadeo.vis_ncons = mov_actividad_responsable.vis_ncons INNER JOIN "
                      + " actividades_visita ON mov_actividad_responsable.mov_actv_id = actividades_visita.actv_id INNER JOIN "
                      + " responsables_visita ON mov_actividad_responsable.mov_respv_id = responsables_visita.respv_id "
                      + " WHERE     (mov_visita_mercadeo.vis_ncons = " + idVisita + ")";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Me trae la carpeta donde se guardan y se encuentran los documentos
        public String parametrosDoc()
        {
            String ruta = "";
            DataTable consulta = null;
            String sql = "SELECT  parvi_dirdocs AS rutaDoc FROM  Parametros";
            consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                ruta = row["rutaDoc"].ToString();
            }
            return ruta;
        }
        //Toma la fecha actual del sistema
        public DateTime fechaLocal()
        {
            String sql = "SELECT CONVERT (varchar, SYSDATETIME(), 103) AS fecha";
            DataTable consulta = BdDatos.CargarTabla(sql);
            DateTime fecha = new DateTime();
            foreach (DataRow row in consulta.Rows)
            {
                fecha = DateTime.Parse(row["fecha"].ToString());
            }
            return fecha;
        }
        //Toma la fecha que se agendo de la visita
        public DateTime fechaAgenVis(String idVisita)
        {
            String sql = "SELECT  vis_dfecha_agendo AS fecha  FROM   mov_visita_mercadeo    WHERE   (vis_ncons = " + idVisita + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            DateTime fecha = new DateTime();
            foreach (DataRow row in consulta.Rows)
            {
                fecha = DateTime.Parse(row["fecha"].ToString());
            }
            return fecha;
        }
        //Las actividades que estan libres
        public DataTable ListaActivdadesLibres(String idVisita)
        {
            String sql = "SELECT  actividades_visita.actv_id, actividades_visita.actv_nombre   FROM    actividades_visita " +
                          " WHERE     (actividades_visita.actv_id  NOT IN  (SELECT  actividades_visita.actv_id  FROM         mov_actividad_responsable INNER JOIN" +
                          " actividades_visita ON mov_actividad_responsable.mov_actv_id = actividades_visita.actv_id   WHERE     (mov_actividad_responsable.vis_ncons = " + idVisita + "))) order by actividades_visita.actv_id desc";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga la lista de responsables
        public DataTable ListaResponsalbes()
        {
            String sql = " SELECT     respv_id AS idRespVis, respv_nombre AS idResNom   FROM  responsables_visita   WHERE     (respv_lactivo = 1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Me valida de que base de datos es
        public DataTable ValidarBaseD(String idcliente)
        {
            String sql = "select vis_bd_origen AS bdOrigen from  mov_visita_mercadeo where vis_cli_id = " + idcliente + "";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Busca los datos del contacto SIO
        public DataTable contatosSIObuscar(String idcliente, String textAbuscar)
        {
            String sql = "SELECT   contacto_cliente.ccl_id AS idContacto, contacto_cliente.ccl_nombre + ' ' +  ccl_apellido AS nomContacto   FROM   cliente INNER JOIN " +
            "  contacto_cliente ON cliente.cli_id = contacto_cliente.ccl_cli_id   WHERE    (contacto_cliente.ccl_cli_id = " + idcliente + ") AND (contacto_cliente.ccl_nombre like '%" + textAbuscar + "%') AND (contacto_cliente.ccl_activo = 1)  ORDER BY nomContacto";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Busca los datos del contacto LITE
        public DataTable contatosLite(String idcliente)
        {
            String sql = "SELECT  clite_id as idcli,   clite_contacto, clite_cargo, clite_telefono, clite_correo, clite_direccion   FROM   cliente_lite  WHERE  (clite_activo = 1) AND (clite_id = " + idcliente + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        public DataTable contatosLitebuscar(String idcliente, String textAbuscar)
        {
            String sql = "SELECT  clite_id as idcli, clite_contacto, clite_cargo, clite_telefono, clite_correo, clite_direccion " +
                        " FROM    cliente_lite   WHERE     (clite_activo = 1) AND (clite_contacto like '%" + textAbuscar + "%') AND (clite_id = " + idcliente + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Trae la conclucion y la fecha convertida para guardar el nombre del archivo que carga
        public DataTable consultaconclusion(String idVisita)
        {
            //,REPLACE(CONVERT (varchar, SYSDATETIME(), 104), '.','') + REPLACE(CONVERT (varchar, SYSDATETIME(), 108),':','')   AS fecha
            String sql = "SELECT isnull(vis_conclusion, '') + ' ' + isnull(vis_respuesta, '') as conclusion, vis_fup, vis_version, vis_metroC, vis_valorC, isnull(vis_moneda, 0) as moneda, vis_vinculo_evernote FROM   mov_visita_mercadeo  WHERE   (vis_ncons = " + idVisita + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga las actividades que tienen resultado//Consulta la actividad, responsable y respuesta
        public DataTable consultaActivResponResultados(String idVisita)
        {
            String sql = " SELECT    CONVERT(VARCHAR(MAX), mar.mov_actv_id) + '|' +  CONVERT(VARCHAR(MAX), av.actv_fup) + '|' +  CONVERT(VARCHAR(MAX), av.actv_rapida)  AS idActRes, "
            + " (CASE WHEN av.actv_fup = 1 THEN (av.actv_nombre + ' - ' + rv.respv_nombre + ' - ' +  CONVERT(VARCHAR(MAX), (CASE WHEN mvm.vis_fup IS NULL THEN '' ELSE mvm.vis_fup END)) + '-' + (CASE WHEN mvm.vis_version IS NULL THEN '' ELSE mvm.vis_version  END)) "
            + " WHEN av.actv_rapida = 1 THEN (av.actv_nombre + ' - ' + rv.respv_nombre + ' - ' + (CASE WHEN mar.mov_metroC IS NULL THEN '' ELSE ' M²(' + CONVERT(VARCHAR(MAX), mar.mov_metroC) + ')' END) "
            + "  + ' - ' + (CASE WHEN mar.mov_valorC IS NULL THEN '' ELSE ' Valor Cot.(' + CONVERT(VARCHAR(MAX), mar.mov_valorC) + ')' END)  + ' - ' +  (CASE WHEN mar.mov_moneda IS NULL THEN '' ELSE ' MON(' + moneda.mon_descripcion + ')' END)) "
            + " ELSE (av.actv_nombre + ' - ' + rv.respv_nombre + ' - ' + (CASE WHEN mar.mov_respuesta IS NULL THEN ' ' ELSE mar.mov_respuesta END)) END) AS nomActRespFup, "
            + " (CASE WHEN av.actv_fup = 1 THEN (CASE WHEN mvm.vis_fup IS NULL THEN '0' ELSE '1' END) "
            + " WHEN av.actv_rapida = 1 THEN (CASE WHEN mar.mov_valorC IS NOT NULL AND mar.mov_valorC IS NOT NULL AND mar.mov_moneda IS NOT NULL THEN '1' ELSE '0' END) "
            + " ELSE (CASE WHEN mar.mov_respuesta IS NULL THEN '0' ELSE '1' END) END) AS color  "
            + " FROM     mov_visita_mercadeo mvm INNER JOIN   mov_actividad_responsable AS mar ON   mvm.vis_ncons = mar.vis_ncons INNER JOIN "
            + " actividades_visita AS av  ON mar.mov_actv_id = av.actv_id INNER JOIN  responsables_visita AS rv  ON mar.mov_respv_id = rv.respv_id LEFT JOIN "
            + " moneda ON mar.mov_moneda = moneda.mon_id "
            + " WHERE  (mar.vis_ncons = " + idVisita + ") ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga las respuestas de las actividades
        public DataTable consultaRespuestas(String idVisita, String idActi)
        {
            String sql = " SELECT   mov_respuesta AS repuesta, av.actv_fup AS fup, av.actv_rapida AS rap   FROM   mov_actividad_responsable mar INNER JOIN actividades_visita av "
            + " ON av.actv_id = mar.mov_actv_id  WHERE   (mov_actv_id = " + idActi + ") AND (vis_ncons = " + idVisita + ") ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Agrega el resultado
        public DataTable ValidarCierreActive(String idVisita)
        {
            String sql = "SELECT  vis_lcierre FROM   mov_visita_mercadeo where vis_ncons = " + idVisita;
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Agrega el contacto SIO
        public String actCierreContSIO(String idVisita, String idContacto)
        {
            String mensaje = "";
            String sql = "";
            try
            {
                sql = "UPDATE mov_visita_mercadeo SET  vis_idcontacto = " + idContacto + " WHERE vis_ncons = " + idVisita;
                BdDatos.Actualizar(sql);
                mensaje = "OK"; ;
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (mov_visita_mercadeo)";
            }
            return mensaje;
        }
        //Agrega la respuesta, el fup, la version, metros, valor y moneda
        public String agregarNofup(String usuEje, String idVisita, String reps, String idActiv, String fup, String version, String metro, String valor, String moneda)
        {
            String mensaje = "";
            String sql = "";
            try
            {
                if (fup == "0")
                {
                    if (metro == "0")
                    {
                        if (reps != "")
                        {
                            sql = "UPDATE   mov_actividad_responsable SET mov_fecha_res = SYSDATETIME(), mov_actividad_responsable.mov_respuesta = '" + reps + "'  WHERE mov_actividad_responsable.vis_ncons = " + idVisita + "  AND  mov_actividad_responsable.mov_actv_id = " + idActiv;
                        }
                    }
                    else
                    {
                        sql = " UPDATE   mov_actividad_responsable SET mov_fecha_res = SYSDATETIME(), mov_metroC = " + metro + ", mov_valorC = " + valor + ", mov_moneda = " + moneda + "  WHERE mov_actividad_responsable.vis_ncons = " + idVisita + "  AND  mov_actividad_responsable.mov_actv_id = " + idActiv;
                    }
                }
                else
                {
                    sql = "UPDATE mov_visita_mercadeo SET vis_fup = " + fup + " , vis_version = '" + version + "' WHERE vis_ncons =" + idVisita;
                }
                BdDatos.Actualizar(sql);
                mensaje = "OK"; ;
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (mov_visita_mercadeo)";
            }
            return mensaje;
        }
        //Agrega la fecha de ejecucion y la actividad con el responsable
        public String agregarFechaEje(String usuEje, String conclucion, String idVisita, String fup, String version, String metro, String valor, String moneda, String evernote)
        {            
            String mensaje = "";
            try
            {
                //if (!String.IsNullOrEmpty(conclucion.Trim()))
                //{
                    String sql = "";
                    if (String.IsNullOrEmpty(fup))
                    {
                        sql = "UPDATE mov_visita_mercadeo SET vis_usu_ejecuto = '" + usuEje + "', vis_conclusion = '" + conclucion + "', vis_dfecha_ejecuto = SYSDATETIME(), vis_version = '" + version + "', vis_metroC = '" + metro + "', vis_valorC = '" + valor + "', vis_moneda = '" + moneda + "', vis_vinculo_evernote = '" + evernote + "' WHERE  mov_visita_mercadeo.vis_ncons = " + idVisita + "";
                    }

                    else
                    {
                        sql = "UPDATE mov_visita_mercadeo SET vis_usu_ejecuto = '" + usuEje + "', vis_conclusion = '" + conclucion + "', vis_dfecha_ejecuto = SYSDATETIME(), vis_fup = '" + fup + "' , vis_version = '" + version + "', vis_metroC = '" + metro + "', vis_valorC = '" + valor + "', vis_moneda = '" + moneda + "', vis_vinculo_evernote = '" + evernote + "' WHERE  mov_visita_mercadeo.vis_ncons = " + idVisita + "";
                    }

                    if (!String.IsNullOrEmpty(sql))
                    {
                        BdDatos.Actualizar(sql);
                        mensaje = "OK";
                    }

                    else { mensaje = "ERROR EN EL QUERY (mov_visita_mercadeo)"; }
                //}
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (mov_visita_mercadeo)";
            }

            return mensaje;
        }
        //Actualiza el contacto LITE
        public String agregarContactoLite(String Nombre, String Cargo, String Telefono, String Direccion, String Email, String idcliente)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE cliente_lite SET clite_contacto = '" + Nombre + "', clite_cargo = '" + Cargo + "', clite_telefono = '" + Telefono + "', clite_correo = '" + Email + "', clite_direccion = '" + Direccion + "' WHERE  (clite_id = " + idcliente + ")";
                BdDatos.Actualizar(sql);
                mensaje = "OK up";
            }
            catch
            {
                mensaje = "ERROR Ejecucion";
            }
            return mensaje;
        }
        //Valida el FUP y la version
        public Boolean validarfupoVersion(String Fup, String Version)
        {
            String sql = "SELECT    eect_fup_id, eect_vercot_id   FROM  fup_enc_entrada_cotizacion   WHERE  (eect_fup_id = " + Fup + ") AND (eect_vercot_id = '" + Version + "')";
            Boolean Validaok = false;
            DataTable consulta = BdDatos.CargarTabla(sql);
            if (consulta.Rows.Count >= 1)
            {
                Validaok = true;
            }
            else
            {
                Validaok = false;
            }
            return Validaok;
        }
        //Cambia el estado del cierre
        public String activarCierre(String idVisita, String usuCierre, String conclusion, String fup, String version, String metro, String valor, String moneda, String evernote)
        {
            String mensaje = "";
            try
            {
                if (!String.IsNullOrEmpty(conclusion.Trim()))
                {
                    String sql = "";
                    if (String.IsNullOrEmpty(fup))
                    {
                        sql = "UPDATE mov_visita_mercadeo SET vis_lcierre = 1, vis_usu_cierre = '" + usuCierre + "', vis_dfecha_cierre = SYSDATETIME(), vis_usu_ejecuto = '" + usuCierre + "', vis_conclusion = '" + conclusion + "', vis_dfecha_ejecuto = SYSDATETIME(), vis_version = '" + version + "', vis_metroC = '" + metro + "', vis_valorC = '" + valor + "', vis_moneda = '" + moneda + "', vis_vinculo_evernote = '" + evernote + "'  WHERE vis_ncons  = " + idVisita + "";
                    }
                    else
                    {
                        sql = "UPDATE mov_visita_mercadeo SET vis_lcierre = 1, vis_usu_cierre = '" + usuCierre + "', vis_dfecha_cierre = SYSDATETIME(), vis_usu_ejecuto = '" + usuCierre + "', vis_conclusion = '" + conclusion + "', vis_dfecha_ejecuto = SYSDATETIME(), vis_fup = '" + fup + "' , vis_version = '" + version + "', vis_metroC = '" + metro + "', vis_valorC = '" + valor + "', vis_moneda = '" + moneda + "', vis_vinculo_evernote = '" + evernote + "'  WHERE vis_ncons  = " + idVisita + "";
                    }

                    if (!String.IsNullOrEmpty(sql))
                    {
                        BdDatos.Actualizar(sql);
                        mensaje = "OK";
                    }
                    else
                    {
                        mensaje = "ERROR EN EL QUERY (mov_visita_mercadeo)";
                    }
                }
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (mov_visita_mercadeo)";
            }
            
            return mensaje;
        }

        public String colorEstadoVis(String idVisita)
        {
            String color = "";
            String sql = "SELECT  (CASE WHEN mvm.vis_lcierre = 1 THEN '3' WHEN mvm.vis_cancelada = 1 THEN '4' WHEN mvm.vis_dfecha_ejecuto IS NOT NULL THEN '2' ELSE '1' END) AS color, (CASE WHEN mvm.vis_lcierre = 1 THEN 'CERRADA' WHEN mvm.vis_cancelada = 1 THEN 'CANCELADA' WHEN mvm.vis_usu_ejecuto IS NOT NULL THEN 'EJECUTANDO' ELSE 'AGENDADA' END) AS estado  FROM     mov_visita_mercadeo  as mvm WHERE    (mvm.vis_ncons = " + idVisita + ")";
            DataTable consulta = BdDatos.CargarTabla2(sql);
            foreach (DataRow row in consulta.Rows)
            {
                if (row["color"].ToString() == "3")
                { color = "#37CA5C"; }
                else if (row["color"].ToString() == "2")
                { color = "#CAC337"; }
                else if (row["color"].ToString() == "1")
                { color = "#3a87ad"; }
                else if (row["color"].ToString() == "4")
                { color = "#E13B3B"; }
            }
            return color + "/" + consulta.Rows[0]["estado"].ToString();
        }
        //Trae los datos del ejecutor
        public String correoEjecutor(String usuEje)
        {
            String sql = "SELECT  representantes_comerciales.rc_descripcion AS nomEje, CONVERT(VARCHAR, SYSDATETIME(), 103) AS fechaAct, rc_email AS correo  FROM   usuario INNER JOIN "
            + " representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id  WHERE  (usuario.usu_login = '" + usuEje + "') ";
            DataTable consulta = BdDatos.CargarTabla2(sql);
            String nomEje = "";
            String fechaAct = "";
            String correoEje = "";
            foreach (DataRow row in consulta.Rows)
            {
                nomEje = row["nomEje"].ToString();
                fechaAct = row["fechaAct"].ToString();
                correoEje = row["correo"].ToString();//correo del quien hace la operacion, osea el que ejecuta
            }
            return nomEje + "|" + fechaAct + "|" + correoEje;
        }
        //Ejecuta el correo en ejecucion
        public void correoEjecucion(String idVisita, String tipoCorreo, String usuEje)
        {
            //0=nomEjecuto|1=fechaactual|2=correo
            String[] datos = correoEjecutor(usuEje).Split('|');
            String correoGen = consultaCorreoGeren(usuEje);
            //String correoAdiResp = "";
            //DataTable correoResp = consultarCorreos(" AND (mail_proceso.mail_proc_mensaje_id IN (SELECT respv_mensaje FROM  responsables_visita WHERE (respv_id = " + idResp + ")))");
            //if (correoResp.Rows.Count > 0)
            //{
            //    foreach (DataRow row1 in correoResp.Rows)
            //    {
            //        correoAdiResp = correoAdiResp + "," + row1["correoEmp"].ToString();
            //    }
            //}
            string correo = "";
            if (!String.IsNullOrEmpty(correoGen))
            {
                correo = datos[2] + "," + correoGen;
            }
            else
            {
                correo = datos[2];                   
            }

            String cuerpo = cuerpoCorreoEje(idVisita);
            crearCorreo(cuerpo, tipoCorreo, datos[0], "Ejecución de la Actividad: ", correo, true, false, "", false);
        }
        //Ejecuta el correo en cierre
        public void correoCierre(String idVisita, String tipoCorreo, String usuCerr)
        {
            //0=nomEjecuto|1=fechaactual|2=correo
            String[] datos = correoEjecutor(usuCerr).Split('|');
            String correoGen = consultaCorreoGeren(usuCerr);
            String cuerpo = cuerpoCorreoCierre(idVisita, datos[0], datos[1]);
            string correo = "";
            if (!String.IsNullOrEmpty(correoGen))
            {
                correo = datos[2] + "," + correoGen;
            }
            else
            {
                correo = datos[2];
            }
            crearCorreo(cuerpo, tipoCorreo, datos[0], "Cierre de la Visita: ", correo, true, false, "", false);
        }
        //Ejecuta el correo en agendamiento
        public void correoAgen(String idVisita, String tipoCorreo, String usuAgen, String operacion, String estado, bool soporteTec, string obra)
        {
            //0=nomEjecuto|1=fechaactual|2=correo
            String[] datos = correoEjecutor(usuAgen).Split('|');
            String correos = "";
            //String correoGen = consultaCorreoGeren(usuAgen);
            if (soporteTec)
               correos  = correoSoporteTecnico();
            String cuerpo = cuerpoCorreoAgen(idVisita, datos[0], datos[1], operacion, obra);
            string correo = "";
            if (!String.IsNullOrEmpty(correos))
            {
                correo = datos[2] + "," + correos;
            }
            else
            {
                correo = datos[2];
            }

            DataTable datosIcs = cargarDatosICS(idVisita);
            string procesos = datosIcs.Rows[0]["procesos"].ToString();

            if (!String.IsNullOrEmpty(procesos))
            {
                correo += ", " + procesos;
            }

            crearCorreo(cuerpo, tipoCorreo, datos[0], estado, correo, true, false, "", false);
        }

        public String correoSoporteTecnico()
        {
            String correos = "";
            string sql = "";

            sql = "SELECT STUFF((SELECT  ', ' +        empleado.emp_correo_electronico "+
                   "FROM empleado INNER JOIN "+
                   "usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id INNER JOIN "+
                   "rolapp ON usuario.usu_rap_id = rolapp.rap_id "+
                   "WHERE(usuario.usu_rap_id = 38) AND(usuario.usu_activo = 1) AND(empleado.notificaciones = 1) FOR XML PATH('')), 1, 1, '') as correo";
            DataTable db = BdDatos.CargarTabla(sql);

            correos = db.Rows[0]["correo"].ToString();
            return correos;
        }

        //Ejecuta el correo de aprobacion
        public void correoAprobacion(String idVisita, String cliente, String pais, String estado, String ciudad, String usuComer, String usuApr, String tipoCorreo)
        {
            String sql = "SELECT     representantes_comerciales.rc_descripcion AS nomComer, CONVERT(VARCHAR, SYSDATETIME(), 103) AS fecha, "
                     + " representantes_comerciales.rc_email AS correoComer, representantes_comerciales_1.rc_descripcion AS nomApr,  "
                     + " representantes_comerciales_1.rc_email AS correoApr "
                     + " FROM         usuario AS usuario_1 INNER JOIN "
                     + " representantes_comerciales AS representantes_comerciales_1 ON usuario_1.usu_siif_id = representantes_comerciales_1.rc_usu_siif_id INNER JOIN "
                     + " representantes_comerciales INNER JOIN "
                     + " usuario ON representantes_comerciales.rc_usu_siif_id = usuario.usu_siif_id INNER JOIN "
                     + " mov_visita_mercadeo ON usuario.usu_login = mov_visita_mercadeo.vis_usu_ejecuta ON  "
                     + " usuario_1.usu_login = mov_visita_mercadeo.vis_usu_aprobo "
                     + " WHERE     (mov_visita_mercadeo.vis_ncons = " + idVisita + ")";
            DataTable consulta = BdDatos.CargarTabla2(sql);
            String nomApr = "";
            String fecha = "";
            String correoApr = "";
            String correoComer = "";
            foreach (DataRow row in consulta.Rows)
            {
                nomApr = row["nomApr"].ToString();
                fecha = row["fecha"].ToString();
                correoApr = row["correoApr"].ToString();//correo de quien hace la operacion, osea el que aprobo
                correoComer = row["correoComer"].ToString();//correo a quien le hacen la operacion, osea el que le aprobaron la visita
            }
            String cuerpo = cuerpoCorreoApr(idVisita, cliente, pais, estado, ciudad, usuComer, nomApr, fecha);
            string correo = "";
            if (!String.IsNullOrEmpty(correoComer))
            {
                correo = correoApr + "," + correoComer;
            }
            else
            {
                correo = correoApr;
            }
            crearCorreo(cuerpo, tipoCorreo, nomApr, estado, correo, true, false, "", false);
        }
        //Ejecuta el correo de viajes
        public void correoViajes(String nomComer, String usuComer, String pais, String ciudad, String fechaIni, String fechaFin, String horaIni, String horaFin, String tipoCorreo, String estado, String usuEje)
        {
            //0=nomEjecuto|1=fechaactual|2=correo
            String[] datos = correoEjecutor(usuEje).Split('|');
            String correoGen = consultaCorreoGeren(usuComer);
            String[] correoComer = correoEjecutor(usuEje).Split('|');// consultaCorreoComer(usuComer); 
            String cuerpo = cuerpoCorreoVia(nomComer, pais, ciudad, fechaIni, fechaFin, horaIni, horaFin, estado, datos[0]);
            string correo = "";
            if (!String.IsNullOrEmpty(correoGen))
            {
                correo = datos[2] + "," + correoGen;
            }
            else
            {
                correo = datos[2];
            }

            if (!String.IsNullOrEmpty(correoComer[2]))
            {
                correo += "," + correoComer[2];
            }
            crearCorreo(cuerpo, tipoCorreo, datos[0], estado, correo, true, false, "", false);
        }
        //Ejecuta el correo de planeacion
        public void correoPlanea(String nomComer, String usuComer, String cliente, String pais, String ciudad, String usuPlan, String motivo, String asunto, String dias, String tipoCorreo)
        {
            //0=nomEjecuto|1=fechaactual|2=correo
            String[] datos = correoEjecutor(usuPlan).Split('|');
            String correoGen = consultaCorreoGeren(usuComer);
            String[] correoComer = correoEjecutor(usuPlan).Split('|'); //consultaCorreoComer(usuComer);
            String cuerpo = cuerpoCorreoPlan(nomComer, cliente, pais, ciudad, datos[1], motivo, asunto, datos[0], dias);
            string correo = "";
            if (!String.IsNullOrEmpty(correoGen))
            {
                correo = datos[2] + "," + correoGen;
            }
            else
            {
                correo = datos[2];
            }

            if (!String.IsNullOrEmpty(correoComer[2]))
            {
                correo += "," + correoComer[2];
            }
            crearCorreo(cuerpo, tipoCorreo, datos[0], "Planeación de la Visita: ", correo, true, false, "", false);
        }
        //Ejecuta el correo de planeacion
        public void correoArchiICS(String usuPlan, String tipoCorreo, String path, String correoAdicio)
        {
            //0=nomEjecuto|1=fechaactual|2=correo
            String[] datos = correoEjecutor(usuPlan).Split('|');
            String cuerpo = " Adjuntamos el archivo con la información de la visita planeada para que la agende en su calendario. Gracias!";
            string correo = "";
            if (!String.IsNullOrEmpty(correoAdicio))
            {
                correo = datos[2] + "," + correoAdicio;
            }
            else
            {
                correo = datos[2];
            }

            crearCorreo(cuerpo, tipoCorreo, datos[0], "Planeación de la Visita: ", correo, true, true, path, false);
        }

        //Ejecuta el correo de planeacion
        public string correoArchiICSacomp(String usuPlan, String tipoCorreo, String path, String correoAdicio, bool soporteTec, string idVisita, string operacion, string obra)
        {
            string msj = "";
            //0=nomEjecuto|1=fechaactual|2=correo
            String[] datos = correoEjecutor(usuPlan).Split('|');
            String cuerpoFin = "\r\nEn el archivo adjunto, se encuentra el detalle de la solicitud de acompañamiento para el desarrollo de la visita comercial y sea agendado en el calendario outlook. Gracias!";
            String cuerpoSporteTec = "\r\nSe requiere acompañamiento del proceso Soporte Técnico.";
            string correo = "";
            string correoSoprte = "";
            if (!String.IsNullOrEmpty(correoAdicio))
                correo = datos[2] + ',' + correoAdicio;
            else
                correo = datos[2];

            String cuerpo = cuerpoCorreoAgen(idVisita, datos[0], datos[1], operacion, obra);

           
            if (soporteTec)
            {
                correoSoprte = correoSoporteTecnico();
                cuerpo += cuerpoSporteTec;
            }           
            
            if (!String.IsNullOrEmpty(correoSoprte))
            {
                correo += ", " + correoSoprte;
            }
            cuerpo += cuerpoFin;
            msj = crearCorreo(cuerpo, tipoCorreo, datos[0], "Planeación de la Visita: ", correo, true, true, path, false);
            return msj;
        }

        //Carga el correo del gerente dependiendo del usuario comercial
        public String consultaCorreoGeren(String usuario)
        {
            DataTable consulta = null;
            String correoUsu = "";
            String sql = "SELECT    representantes_comerciales_1.rc_email AS correo   FROM   pais_representante AS pais_representante_1 INNER JOIN "
                      + " representantes_comerciales AS representantes_comerciales_1 ON  "
                      + " pais_representante_1.pr_id_representante = representantes_comerciales_1.rc_id INNER JOIN "
                      + " usuario AS usuario_1 ON representantes_comerciales_1.rc_usu_siif_id = usuario_1.usu_siif_id INNER JOIN "
                      + " representantes_comerciales INNER JOIN "
                      + " usuario ON representantes_comerciales.rc_usu_siif_id = usuario.usu_siif_id INNER JOIN "
                      + " pais_representante ON representantes_comerciales.rc_id = pais_representante.pr_id_representante ON  "
                      + " pais_representante_1.pr_id_pais = pais_representante.pr_id_pais "
                      + " WHERE     (usuario.usu_login = N'" + usuario + "') AND (pais_representante.pr_activo = 1) AND (usuario_1.usu_rap_id = 2) AND (pais_representante_1.pr_activo = 1) "
                      + " AND (representantes_comerciales.rc_activo = 1) ";
            consulta = BdDatos.CargarTabla2(sql);
            foreach (DataRow row in consulta.Rows)
            {
                correoUsu = row["correo"].ToString();
            }
            return correoUsu;
        }
        //consulta el usuario del comercial cuando no se puede obtener de otro lado, con el id del viaje
        public String consultaUsuComerViaje(int idViaje)
        {
            DataTable consulta = null;
            String correoUsu = "";
            String sql = "SELECT  via_usu_viaja AS usuario FROM    mov_vis_viajes  WHERE    (via_id = " + idViaje + ")";
            consulta = BdDatos.CargarTabla2(sql);
            foreach (DataRow row in consulta.Rows)
            {
                correoUsu = row["usuario"].ToString();//correo de quien hace la operacion, osea el que creo el viaje
            }
            return correoUsu;
        }
        //Se construye el cuerpo en HTML por completo para el correo de aprobacion
        private String cuerpoCorreoVia(String usuComer, String pais, String ciudad, String fechaIni, String fechaFin, String horaIni, String horaFin, String estado, String usuEje)
        {
            String cuerpo = "";
            cuerpo = "<html> "
            + " <head> "
            + " <title></title> "
            + " </head> "
            + " <body style='width: 740px; height: 264px'> "
            + " <br /> "
            + " <br /> "
            + " El usuario <strong> " + usuEje.ToUpper() + "</strong> a realizado la <strong>" + estado.ToUpper() + " </strong>"
            + " <br /> "
            + " <br /> "
            + " COMERCIAL ENCARGADO : <strong> " + usuComer.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " PAIS : <strong> " + pais.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " CIUDAD : <strong> " + ciudad.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " FECHA INICIO DEL VIAJE : <strong> " + fechaIni.ToUpper() + " </strong>  /  HORA INICIO DEL VIAJE : <strong> " + horaIni.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " FECHA FIN DEL VIAJE : <strong> " + fechaFin.ToUpper() + " </strong>  /  HORA FIN DEL VIAJE : <strong> " + horaFin.ToUpper() + " </strong>"
            + " <br /> "
            + " <br /> "
            + " <br /> "
            + " <br /> "
            + " Muchas gracias por su atención. "
            + " <br /> "
            + " <br /> "
            + " </body> "
            + " </html>";
            return cuerpo;
        }
        //Se construye el cuerpo en HTML por completo para el correo de aprobacion
        private String cuerpoCorreoApr(String idVisita, String cliente, String pais, String estado, String ciudad, String usuComer, String nomApr, String fecha)
        {
            String cuerpo = "";
            cuerpo = "<html> "
            + " <head> "
            + " <title></title> "
            + " </head> "
            + " <body style='width: 740px; height: 264px'> "
            + " <br /> "
            + " <br /> "
            + " El usuario <strong>" + nomApr.ToUpper() + " </strong> ha realizado la <strong> " + estado.ToUpper() + "  #" + idVisita.ToString() + ".</strong>"
            + " <br /> "
            + " <br /> "
            + " CLIENTE : <strong> " + cliente.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " PAIS : <strong> " + pais.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " CIUDAD : <strong> " + ciudad.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " COMERCIAL ENCARGADO : <strong> " + usuComer.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " <br /> "
            + " <br /> "
            + " Muchas gracias por su atención. "
            + " <br /> "
            + " <br /> "
            + " </body> "
            + " </html>";
            return cuerpo;
        }
        //Se construye el cuerpo en HTML por completo para el correo de ejecucion
        private String cuerpoCorreoEje(String idVisita)
        {
            DataTable datosCorreo = llenarCorreoInf(idVisita);
            //DataTable tablaCorreo = llenarCorreoActResp(idVisita);
            String cuerpo = "";
            //String actv = "";
            //foreach (DataRow row2 in tablaCorreo.Rows)
            //{
            //    actv = actv + "<strong>" + row2["actividades"].ToString() + "</strong> <br /> ";
            //}
            foreach (DataRow row in datosCorreo.Rows)
            {
                cuerpo = "<html> "
                + " <head> "
                + " <title></title> "
                + " </head> "
                + " <body style='width: 740px; height: 264px'> "
                + " <br /> "
                + " Buen día, "
                + " <br /> "
                + " <br /> "
                + " El usuario <strong> " + row["usuEjecuto"].ToString().ToUpper() + " </strong> actualizó la visita <strong> #" + row["idVisita"].ToString().ToUpper() + "</strong> que tiene por fecha el día <strong> " + row["fecha"].ToString() + "</strong>"
                //ejecuto la actividad <strong>" + row["nomAct"].ToString().ToUpper() + "</strong> en la visita agendada para el día <strong>" + row["fecha"].ToString() + "</strong>"
                + " <br /> "
                + " <br /> "
                + " CLIENTE : <strong> " + row["nomCliente"].ToString().ToUpper() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " PAIS : <strong> " + row["nomPais"].ToString().ToUpper() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " CIUDAD : <strong> " + row["nomCiudad"].ToString().ToUpper() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " MOTIVO : <strong> " + row["motivo"].ToString().ToUpper() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " OBJETIVO : <strong> " + row["objetivo"].ToString() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " CONCLUSIÓN : <strong> " + row["conclusion"].ToString() + " </strong> "
                //+ " <br /> "
                //+ " <br /> "
                //+ " ACTIVIDAD - RESPONSABLE :"
                //+ " <br /> "
                //+ " <br /> "
                //+ actv
                + " <br /> "
                + " <br /> "
                + " <br /> "
                + " <br /> "
                + " Muchas gracias por su atención. "
                + " <br /> "
                + " <br /> "
                + " </body> "
                + " </html>";
            }
            return cuerpo;
        }
        //Se construye el cuerpo en HTML por completo para el correo de cierre
        private String cuerpoCorreoCierre(String idVisita, String nomCierra, String fechaActual)
        {
            DataTable datosCorreo = llenarCorreoInf2(idVisita);
            //DataTable tablaCorreo = consultaActivResponResultados(idVisita);
            String cuerpo = "";
            //String actv = "";
            //foreach (DataRow row2 in tablaCorreo.Rows)
            //{
            //    actv = actv + "<strong>" + row2["nomActRespFup"].ToString() + "</strong> <br /> ";
            //}
            foreach (DataRow row in datosCorreo.Rows)
            {
                cuerpo = "<html> "
                + " <head> "
                + " <title></title> "
                + " </head> "
                + " <body style='width: 740px; height: 264px'> "
                + " <br /> "
                + " Buen día, "
                + " <br /> "
                + " <br /> "
                + " El usuario<strong> " + nomCierra.ToUpper() + " </strong> cerró la visita <strong>#" + idVisita + "</strong> el día <strong>" + fechaActual + "</strong> que estaba agendada para la fecha <strong>" + row["fecha"].ToString() + "</strong>"
                + " <br /> "
                + " <br /> "
                + " CLIENTE : <strong> " + row["nomCliente"].ToString().ToUpper() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " PAIS : <strong> " + row["nomPais"].ToString().ToUpper() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " CIUDAD : <strong> " + row["nomCiudad"].ToString().ToUpper() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " MOTIVO : <strong> " + row["motivo"].ToString().ToUpper() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " OBJETIVO : <strong> " + row["objetivo"].ToString() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " CONCLUSIÓN : <strong> " + row["conclusion"].ToString() + " </strong> "
                + " <br /> "
                + " <br /> "               
                + " <br /> "
                + " <br /> "
                + " Muchas gracias por su atención. "
                + " <br /> "
                + " <br /> "
                + " </body> "
                + " </html>";
            }
            return cuerpo;
        }
        //Se construye el cuerpo en HTML por completo para el correo de planecion
        private String cuerpoCorreoPlan(String nomComer, String cliente, String pais, String ciudad, String fecha, String motivo, String asunto, String nomPlan, String dias)
        {
            String cuerpo = "";
            cuerpo = "<html> "
            + " <head> "
            + " <title></title> "
            + " </head> "
            + " <body style='width: 740px; height: 264px'> "
            + " <br /> "
            + " Buen día, "
            + " <br /> "
            + " <br /> "
            + " El usuario <strong>" + nomPlan.ToUpper() + " </strong> ha planeado una visita el día <strong>" + fecha.ToString() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " COMERCIAL ENCARGADO : <strong> " + nomComer.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " CLIENTE : <strong> " + cliente.ToUpper() + " </strong> "
            + dias
            + " <br /> "
            + " <br /> "
            + " PAIS : <strong> " + pais.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " CIUDAD : <strong> " + ciudad.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " MOTIVO : <strong> " + motivo.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " ASUNTO : <strong> " + asunto.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " <br /> "
            + " <br /> "
            + " <br /> "
            + " Muchas gracias por su atención. "
            + " <br /> "
            + " <br /> "
            + " </body> "
            + " </html>";
            return cuerpo;
        }
        ////Se construye el cuerpo en HTML por completo para el correo de agendamiento
        public String cuerpoCorreoAgen(String idVisita, String nomAge, String fechaActual, String operacion, String obra)
        {
            DataTable datosCorreo = llenarCorreoInf2(idVisita);
            String cuerpo = "";
            String accion = "";
            if (operacion == "NoAge")
            { accion = "elimino el agendamiento de la visita"; }
            else { accion = "agendo la visita"; }
            String fechaAge = "";
            foreach (DataRow row in datosCorreo.Rows)
            {
                if (operacion == "NoAge")
                { fechaAge = "no tiene fecha agendada"; }
                else { fechaAge = row["fecha"].ToString(); }
                cuerpo = "<html> "
                + " <head> "
                + " <title></title> "
                + " </head> "
                + " <body style='width: 740px; height: 264px'> "
                + " <br /> "
                + " Buen día, "
                + " <br /> "
                + " <br /> "
                + " El usuario<strong> " + nomAge.ToUpper() + " </strong> " + accion + " <strong>#" + idVisita + "</strong> el día <strong>" + fechaActual.ToString() + "</strong>"
                + " <br /> "
                + " <br /> "
                + " FECHA DE LA VISITA : <strong> " + fechaAge.ToUpper() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " CLIENTE : <strong> " + row["nomCliente"].ToString().ToUpper() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " OBRA : <strong> " + obra.ToUpper() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " PAIS : <strong> " + row["nomPais"].ToString().ToUpper() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " CIUDAD : <strong> " + row["nomCiudad"].ToString().ToUpper() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " OBJETIVO : <strong> " + row["objetivo"].ToString() + " </strong> "
                + " <br /> "
                + " <br /> "
                + " <br /> "
                + " <br /> "
                + " <br /> "
                + " Muchas gracias por su atención. "
                + " <br /> "
                + " <br /> "
                + " </body> "
                + " </html>";
            }
            return cuerpo;
        }

        public String cuerpoCorreoAgenEverNote(String idVisita, String nomAge, String fechaActual, String operacion, String obra)
        {
            DataTable datosCorreo = llenarCorreoInf2(idVisita);
            String cuerpo = "";
            String accion = "";
            if (operacion == "NoAge")
            { accion = "elimino el agendamiento de la visita"; }
            else { accion = "agendó la visita"; }
            String fechaAge = "";
            foreach (DataRow row in datosCorreo.Rows)
            {
                if (operacion == "NoAge")
                { fechaAge = "no tiene fecha agendada"; }
                else { fechaAge = row["fecha"].ToString(); }
                cuerpo = "<html> "
                + " <head> "
                + " <title></title> "
                + " </head> "
                + " <body style='width: 740px; height: 264px'> "
                + " Buen día, "
                + " <br /> "
                + " <br /> "
                + " El usuario<strong> " + nomAge.ToUpper() + " </strong> " + accion + " <strong>#" + idVisita + "</strong> el día <strong>" + fechaActual.ToString() + "</strong>"
                + " <br /> "
                + " <br /> "
                + " <strong> FECHA: </strong>" + fechaAge.ToUpper()
                + " <br /> "
                + " <strong> CLIENTE: </strong> " + row["nomCliente"].ToString().ToUpper()
                + " <br /> "
                + " <strong> OBRA: </strong> " + obra.ToUpper()
                + " <br /> "
                + " <strong> CIUDAD CLIENTE: </strong> " + row["ciudad_nombre_cliente"].ToString().ToUpper()
                + " <br /> "
                + " <strong> PAIS: </strong> " + row["nomPais"].ToString().ToUpper()
                + " <br /> "
                + " <strong> CIUDAD VISITA: </strong> " + row["nomCiudad"].ToString().ToUpper() 
                + " <br /> "
                + " <strong> OBJETIVO: </strong>" + row["objetivo"].ToString()
                + " <br /> "
                + " <br /> "
                + " <strong> NOTAS (Comentarios y actividades pendientes):  </strong>"
                + " </body> "
                + " </html>";
            }
            return cuerpo;
        }
        //Se crea el correo
        //Este metodo crea el correo con un metodo donde se crea el cuerpo HTML
        public string crearCorreo(String cuerpo, String tipoCorreo, String nomAsunto, String accion, String correosAdicional, Boolean correosForsa, Boolean adjuArchi, String path, Boolean evernote)
        {
            string msj = "";       
            String correoF = "";
            String contraF = "";
            String host = "";
            string correo_sistema = "";
            
            DataTable correForsa = cosultarCorreoForsa();
            foreach (DataRow row2 in correForsa.Rows)
            {
                correoF = row2["correo"].ToString();
                contraF = row2["contra"].ToString();
                host = row2["host"].ToString();
                correo_sistema = row2["correo_sistema"].ToString();
            }
            String titulo = "";
            String correos = "";
            String asunto = "";
            if (correosForsa)
            {
                DataTable tbCorreos = consultarCorreos(" AND (mensaje.men_tipo = '" + tipoCorreo + "')");
                foreach (DataRow row in tbCorreos.Rows)
                {
                    correos += row["correoEmp"].ToString();
                    asunto = row["asunto"].ToString();
                    titulo = row["titulo"].ToString();
                }
            }
            if (!String.IsNullOrEmpty(correosAdicional))
                correos += ',' + correosAdicional;

            Byte[] correo = new Byte[0];
            WebClient clienteWeb = new WebClient();
            clienteWeb.Dispose();
            clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "forsa", "FORSA");
            
            //DEFINIMOS LA CLASE DE MAILMESSAGE
            MailMessage mail = new MailMessage();
            //INDICAMOS EL EMAIL DE ORIGEN
            mail.From = new MailAddress(correo_sistema, titulo, Encoding.UTF8);
            //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
            //Aquí ponemos el asunto del correo
            if(evernote)
                mail.Subject = accion + nomAsunto;
            else
                mail.Subject = asunto + accion + nomAsunto;
            //Aquí ponemos el mensaje que incluirá el correo
            mail.Body = cuerpo;
            mail.To.Add(correos);
            //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            //DEFINIMOS LA PRIORIDAD DEL MENSAJE
            mail.Priority = System.Net.Mail.MailPriority.Normal;
            //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
            mail.IsBodyHtml = true;

            //ADJUNTAMOS EL ARCHIVO
            Attachment data = null;
            if (adjuArchi == true)
            {   //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran
                data = new Attachment(path, System.Net.Mime.MediaTypeNames.Application.Octet);
                mail.Attachments.Add(data);
            }

            SmtpClient smtp = new SmtpClient();
            //DEFINIMOS NUESTRO SERVIDOR SMTP
            smtp.Host = "smtp.office365.com";
            //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
            smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
            smtp.Port = 25;
            smtp.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            try
            {
                smtp.Send(mail);
                data = null;
                mail.Attachments.Dispose();
                mail.Dispose();
                smtp.Dispose();
            }
            catch (Exception ex)
            {
                string mensaje = "ERROR: " + ex.Message;
                data = null;
                mail.Attachments.Dispose();
                mail.Dispose();
                smtp.Dispose();
            }
            return msj;            
        }
        //Me trae los datos para llenar el correo
        private DataTable llenarCorreoInf(String idVisita)
        {
            String sql = " SELECT     mvc.vis_ncons AS idVisita, representantes_comerciales.rc_descripcion AS usuEjecuto,  "
            + " (CASE WHEN mvc.vis_feria IS NULL THEN vct.nomCliente ELSE vct.nomCliente + ' ' +  eveFer.tifuente_descripcion END) AS nomCliente, "
            + " (CASE WHEN mvc.vis_feria IS NULL THEN pais.pai_nombre ELSE pais_1.pai_nombre END) AS nomPais,  "
            + " (CASE WHEN mvc.vis_feria IS NULL THEN ciudad.ciu_nombre ELSE ciudad_1.ciu_nombre END) AS nomCiudad, "
            //actividades_visita.actv_nombre AS nomAct, responsables_visita.respv_nombre AS nomResp, "
            + "  mvc.vis_conclusion AS conclusion, motivos_visita.motivov_nombre AS motivo,  "
            + "  CONVERT(CHAR(10), mvc.vis_dfecha_agendo, 103) AS fecha, CONVERT(CHAR(10), SYSDATETIME(),103) AS fechaActual, mvc.vis_objetivo AS objetivo "
            + " FROM    mov_visita_mercadeo mvc INNER JOIN "
            //+ " mov_actividad_responsable ON mov_actividad_responsable.vis_ncons = mvc.vis_ncons INNER JOIN "
            //+ " actividades_visita ON mov_actividad_responsable.mov_actv_id = actividades_visita.actv_id INNER JOIN "
            //+ " responsables_visita ON mov_actividad_responsable.mov_respv_id = responsables_visita.respv_id INNER JOIN   "
            + " usuario ON mvc.vis_usu_ejecuta = usuario.usu_login INNER JOIN "
            + " representantes_comerciales ON representantes_comerciales.rc_usu_siif_id = usuario.usu_siif_id  INNER JOIN  "
            + " Vista_Clientes_Todos vct ON  mvc.vis_cli_id = vct.idCliente AND vct.bdOrigen = mvc.vis_bd_origen INNER JOIN "
            + " pais  ON pais.pai_id = vct.idPais INNER JOIN   "
            + " ciudad ON vct.idCiudad = ciudad.ciu_id INNER JOIN   "
            + " motivos_visita ON mvc.vis_motivov_id = motivos_visita.motivov_id LEFT OUTER JOIN "
            + " lite_tipo_fuente  eveFer ON mvc.vis_feria = eveFer.tifuente_id LEFT OUTER JOIN "
            + " pais AS pais_1 ON eveFer.tifuente_pais = pais_1.pai_id LEFT OUTER JOIN "
            + " ciudad AS ciudad_1 ON eveFer.tifuente_ciudad = ciudad_1.ciu_id "
            + " WHERE   (mvc.vis_ncons = " + idVisita + ")  ";
            //AND (mov_actividad_responsable.mov_actv_id = " + idAct + ") AND (mov_actividad_responsable.mov_respv_id = " + idResp + ") 
            DataTable consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Me trae los datos para llenar el correo, especialmente el de cierre, son datos generales dependiendo del id de la visita
        private DataTable llenarCorreoInf2(String idVisita)
        {
            //String sql = " SELECT   (CASE WHEN mvc.vis_feria IS NULL THEN Vista_Clientes_Todos.nomCliente ELSE Vista_Clientes_Todos.nomCliente + ' ' + eveFer.tifuente_descripcion END) AS nomCliente,   "
            //             + " (CASE WHEN mvc.vis_feria IS NULL THEN Vista_Clientes_Todos.paisCliente ELSE pais_1.pai_nombre END) AS nomPais,   "
            //             + " (CASE WHEN mvc.vis_feria IS NULL THEN Vista_Clientes_Todos.ciudadCliente ELSE ciudad_1.ciu_nombre END) AS nomCiudad,  "
            //             + " motivos_visita.motivov_nombre AS motivo, mvc.vis_conclusion AS conclusion,  "
            //             + " CONVERT(CHAR(10), mvc.vis_dfecha_agendo, 103) AS fecha, mvc.vis_objetivo AS objetivo  "
            //             + " FROM  mov_visita_mercadeo mvc INNER JOIN   "
            //             + " Vista_Clientes_Todos ON mvc.vis_cli_id = Vista_Clientes_Todos.idCliente AND   "
            //             + " mvc.vis_bd_origen = Vista_Clientes_Todos.bdOrigen INNER JOIN  "
            //             + " motivos_visita ON mvc.vis_motivov_id = motivos_visita.motivov_id LEFT OUTER JOIN  "
            //             + " lite_tipo_fuente  eveFer ON mvc.vis_feria = eveFer.tifuente_id LEFT OUTER JOIN  "
            //             + " pais AS pais_1 ON eveFer.tifuente_pais = pais_1.pai_id LEFT OUTER JOIN  "
            //             + " ciudad AS ciudad_1 ON eveFer.tifuente_ciudad = ciudad_1.ciu_id  "
            //             + " WHERE   (mvc.vis_ncons = " + idVisita + ")  ";

            String sql = " SELECT   (CASE WHEN mvc.vis_feria IS NULL THEN Vista_Clientes_Todos.nomCliente ELSE Vista_Clientes_Todos.nomCliente + ' ' + eveFer.tifuente_descripcion END) AS nomCliente,   "+
                          "(CASE WHEN mvc.vis_feria IS NULL THEN Vista_Clientes_Todos.paisCliente ELSE pais_1.pai_nombre END) AS nomPais,  "+ 
                          "(CASE WHEN mvc.vis_feria IS NULL THEN Vista_Clientes_Todos.ciudadCliente ELSE ciudad_1.ciu_nombre END) AS ciudad_nombre_cliente,   " +
                          "(CASE WHEN mvc.vis_feria IS NULL THEN ciudad.ciu_nombre ELSE ciudad_1.ciu_nombre END) AS nomCiudad, (CASE WHEN mvc.vis_feria IS NULL THEN ciudad.ciu_id ELSE ciudad_1.ciu_id END) as ciudad_id, "+
                          "motivos_visita.motivov_nombre AS motivo, mvc.vis_conclusion AS conclusion,  "+
                          "CONVERT(CHAR(10), mvc.vis_dfecha_agendo, 103) AS fecha, mvc.vis_objetivo AS objetivo "+ 
                          "FROM  mov_visita_mercadeo mvc INNER JOIN   "+
                          "Vista_Clientes_Todos ON mvc.vis_cli_id = Vista_Clientes_Todos.idCliente AND  "+ 
                          "mvc.vis_bd_origen = Vista_Clientes_Todos.bdOrigen INNER JOIN  "+
                          "motivos_visita ON mvc.vis_motivov_id = motivos_visita.motivov_id LEFT OUTER JOIN  "+
                          "lite_tipo_fuente  eveFer ON mvc.vis_feria = eveFer.tifuente_id LEFT OUTER JOIN  "+
                          "pais AS pais_1 ON eveFer.tifuente_pais = pais_1.pai_id LEFT OUTER JOIN  "+
                          "ciudad AS ciudad_1 ON eveFer.tifuente_ciudad = ciudad_1.ciu_id  "+
						  "INNER JOIN    ciudad ON mvc.vis_idciudad = ciudad.ciu_id "+
                          "WHERE   (mvc.vis_ncons = "+idVisita+")"; 
            DataTable consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Me trae los actividades para llenar el correo
        private DataTable llenarCorreoActResp(String idVisita)
        {
            String sql = " SELECT    actividades_visita.actv_nombre + '  -  ' + responsables_visita.respv_nombre AS actividades "
                      + "FROM    mov_actividad_responsable INNER JOIN "
                      + "mov_visita_mercadeo ON mov_actividad_responsable.vis_ncons = mov_visita_mercadeo.vis_ncons INNER JOIN "
                      + "actividades_visita ON mov_actividad_responsable.mov_actv_id = actividades_visita.actv_id INNER JOIN "
                      + "responsables_visita ON mov_actividad_responsable.mov_respv_id = responsables_visita.respv_id "
                      + " WHERE   (mov_visita_mercadeo.vis_ncons = " + idVisita + ") ";
            DataTable consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Consulta los correos
        public DataTable consultarCorreos(String filtro)
        {
            DataTable consulta = null;
            String proc = "SELECT     empleado.emp_nombre AS nomEmp, empleado.emp_correo_electronico AS correoEmp, mensaje.men_asunto AS asunto, mensaje.men_mensaje AS titulo "
                + " FROM         mail_proceso INNER JOIN "
                + " mensaje ON mail_proceso.mail_proc_mensaje_id = mensaje.men_id INNER JOIN "
                + " empleado ON mail_proceso.mail_proc_empleado_id = empleado.emp_usu_num_id "
                + " WHERE     (mail_proceso.mail_proc_activo = 1) " + filtro;
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Consulta el correo forsa
        public DataTable cosultarCorreoForsa()
        {
            DataTable consulta = null;
            String proc = "SELECT TOP(1) par_correo AS correo, par_correo_contra AS contra, par_host AS host , par_correo_sistema as correo_sistema FROM  Parametros";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Traer mes y ano actual
        public DataTable anoMesActual()
        {
            DataTable consulta = null;
            String sql = "SELECT (YEAR(SYSDATETIME())) AS ano, (MONTH(SYSDATETIME())) AS mes ";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Canselo la visita con el id de la visita
        public String cancelarVisita(String idVisita)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE mov_visita_mercadeo SET vis_cancelada = 1, vis_dfecha_cance = SYSDATETIME() WHERE mov_visita_mercadeo.vis_ncons = " + idVisita + "";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY AL MODIFICAR (mov_visita_mercadeo)";
            }
            return mensaje;
        }
        //Trae los datos de la feria
        public DataTable consultaDatosEveFer(String idEveFer)
        {
            DataTable consulta = null;
            String proc = "SELECT  eveFer.tifuente_descripcion AS nomFeria, ciudad.ciu_nombre AS ciudadFeria, pais.pai_nombre AS paisFeria, SYSDATETIME() AS fechaAct, eveFer.tifuente_fechaini AS fechaIni, eveFer.tifuente_fechafin AS fechaFin"
            + " FROM  pais INNER JOIN lite_tipo_fuente eveFer ON pais.pai_id = eveFer.tifuente_pais INNER JOIN "
            + " ciudad ON eveFer.tifuente_ciudad = ciudad.ciu_id WHERE  (eveFer.tifuente_activo = 1) AND (eveFer.tifuente_id = " + idEveFer + ")";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Trae los datos de la feria
        public DataTable consultaMonedas(String filtro)
        {
            DataTable consulta = null;
            String sql = "SELECT CAST(mon_id AS varchar(MAX)) + ':' + mon_descripcion AS idNomMoneda, mon_id AS idMoneda, mon_descripcion AS nomMoneda  FROM   moneda   WHERE  (mon_activo = 1) " + filtro;
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Trae datos de todos los clientes
        public DataTable cargarDatosICS(String idVisita)
        {
            DataTable consulta = null;
            //String proc = " SELECT  (CASE WHEN mvc.vis_feria IS NULL THEN cliente.cli_nombre ELSE cliente.cli_nombre + ' ' + eveFer.tifuente_descripcion END) AS cliente, "
            //+ " (CASE WHEN mvc.vis_feria IS NULL THEN pais.pai_nombre ELSE pais_1.pai_nombre END) AS pais, (CASE WHEN mvc.vis_feria IS NULL THEN ciudad.ciu_nombre ELSE ciudad_1.ciu_nombre END) AS ciudad, "
            //+ " SYSDATETIME() AS fechaAct, mvc.vis_objetivo AS objetivo, motivos_visita.motivov_nombre AS motivo, "
            ////+ " (SELECT CAST((SELECT  ', ' +  emp.emp_correo_electronico FROM   empleado emp INNER JOIN  area ON area.are_id = emp.emp_area_id WHERE  (emp_jefe_area = 1) "
            ////+ " AND (emp_area_id IN (SELECT mvp.vis_pro_usu  FROM mov_vis_procesos mvp WHERE mvp.vis_pro_vis_ncons = mvc.vis_ncons)) FOR XML PATH('')) AS VARCHAR(MAX))) AS correoPro, "
            //+ "SELECT (CASE WHEN empleado.emp_correo_electronico IS NULL THEN representantes_comerciales.rc_email ELSE empleado.emp_correo_electronico END) AS Expr1 FROM empleado INNER JOIN  usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id INNER JOIN  representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id WHERE(usuario.usu_login = 'danilocampo') AS correoPro, "
            //+ " (SELECT STUFF((SELECT  ', ' + empleado.emp_nombre + ' ' + empleado.emp_apellidos AS nombre  FROM  usuario INNER JOIN empleado ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id INNER JOIN mov_vis_procesos mvp ON usuario.usu_login = mvp.vis_pro_usu  WHERE  (mvp.vis_pro_vis_ncons = " + idVisita + ") FOR XML PATH('')), 1, 1, '')) AS procesos, "
            //+ " (CASE WHEN  rc.rc_descripcion IS NULL THEN e.emp_nombre + ' ' + e.emp_apellidos ELSE rc.rc_descripcion END) AS usuSoli "
            //+ " FROM   mov_visita_mercadeo mvc INNER JOIN  cliente ON cliente.cli_id = mvc.vis_cli_id INNER JOIN   "
            //+ " ciudad ON mvc.vis_idciudad = ciudad.ciu_id INNER JOIN  "
            //+ " pais  ON pais.pai_id = ciudad.ciu_pai_id INNER JOIN  "
            //+ " motivos_visita ON mvc.vis_motivov_id = motivos_visita.motivov_id  LEFT OUTER JOIN  "
            //+ " lite_tipo_fuente  eveFer ON mvc.vis_feria = eveFer.tifuente_id LEFT OUTER JOIN "
            //+ " pais AS pais_1 ON eveFer.tifuente_pais = pais_1.pai_id LEFT OUTER JOIN "
            //+ " ciudad AS ciudad_1 ON eveFer.tifuente_ciudad = ciudad_1.ciu_id LEFT JOIN "
            //+ " usuario u ON u.usu_login = mvc.vis_usu_plan  LEFT OUTER JOIN  "
            //+ " empleado e ON u.usu_emp_usu_num_id = e.emp_usu_num_id LEFT OUTER JOIN  "
            //+ " usuario u1 ON u1.usu_login = mvc.vis_usu_plan  LEFT OUTER JOIN  "
            //+ " representantes_comerciales rc ON u1.usu_siif_id = rc.rc_usu_siif_id "
            //+ " WHERE   (mvc.vis_ncons = " + idVisita + ") ";

            //String proc = "SELECT  (CASE WHEN mvc.vis_feria IS NULL THEN cliente.cli_nombre ELSE cliente.cli_nombre + ' ' + eveFer.tifuente_descripcion END) AS cliente,  (CASE WHEN mvc.vis_feria IS NULL THEN pais.pai_nombre ELSE pais_1.pai_nombre END) AS pais, (CASE WHEN mvc.vis_feria IS NULL THEN ciudad.ciu_nombre ELSE ciudad_1.ciu_nombre END) AS ciudad,  SYSDATETIME() AS fechaAct, mvc.vis_objetivo AS objetivo, motivos_visita.motivov_nombre AS motivo, (SELECT (CASE WHEN empleado.emp_correo_electronico IS NULL THEN representantes_comerciales.rc_email ELSE empleado.emp_correo_electronico END) AS Expr1 FROM empleado INNER JOIN  usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id INNER JOIN  representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN mov_visita_mercadeo as mvc ON mvc.vis_usu_agendo = usuario.usu_login WHERE(mvc.vis_ncons = " + idVisita + ")) AS correoPro, " +
            //              " (SELECT STUFF((SELECT  ', ' + (CASE WHEN empleado.emp_correo_electronico IS NULL THEN representantes_comerciales.rc_email ELSE empleado.emp_correo_electronico END) FROM     empleado INNER JOIN "+
            //              " usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id INNER JOIN "+
            //              " representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN mov_vis_procesos mvp ON usuario.usu_login = mvp.vis_pro_usu  WHERE(mvp.vis_pro_vis_ncons = "+idVisita+") FOR XML PATH('')), 1, 1, '')) AS procesos, "+
            //              " (SELECT STUFF((SELECT  ', ' +  mvp.vis_pro_usu FROM  mov_vis_procesos mvp WHERE(mvp.vis_pro_vis_ncons = " + idVisita + ") FOR XML PATH('')), 1, 1, '')) AS etiqueta, " +
            //              " (CASE WHEN  rc.rc_descripcion IS NULL THEN e.emp_nombre + ' ' + e.emp_apellidos ELSE rc.rc_descripcion END) AS usuSoli  FROM mov_visita_mercadeo mvc INNER JOIN cliente ON cliente.cli_id = mvc.vis_cli_id INNER JOIN    ciudad ON mvc.vis_idciudad = ciudad.ciu_id INNER JOIN   pais ON pais.pai_id = ciudad.ciu_pai_id INNER JOIN   motivos_visita ON mvc.vis_motivov_id = motivos_visita.motivov_id  LEFT OUTER JOIN lite_tipo_fuente  eveFer ON mvc.vis_feria = eveFer.tifuente_id LEFT OUTER JOIN pais AS pais_1 ON eveFer.tifuente_pais = pais_1.pai_id LEFT OUTER JOIN ciudad AS ciudad_1 ON eveFer.tifuente_ciudad = ciudad_1.ciu_id LEFT JOIN  usuario u ON u.usu_login = mvc.vis_usu_plan  LEFT OUTER JOIN empleado e ON u.usu_emp_usu_num_id = e.emp_usu_num_id LEFT OUTER JOIN usuario u1 ON u1.usu_login = mvc.vis_usu_plan  LEFT OUTER JOIN representantes_comerciales rc ON u1.usu_siif_id = rc.rc_usu_siif_id  WHERE(mvc.vis_ncons = " + idVisita + "); ";

            string proc = "SELECT  (CASE WHEN mvc.vis_feria IS NULL THEN cliente.cli_nombre ELSE cliente.cli_nombre + ' ' + eveFer.tifuente_descripcion END) AS cliente,  (CASE WHEN mvc.vis_feria IS NULL THEN pais.pai_nombre ELSE pais_1.pai_nombre END) AS pais, (CASE WHEN mvc.vis_feria IS NULL THEN ciudad.ciu_nombre ELSE ciudad_1.ciu_nombre END) AS ciudad_nombre_visita, (CASE WHEN mvc.vis_feria IS NULL THEN ciudad.ciu_id ELSE ciudad_1.ciu_id END) as ciudad_id_visita,  SYSDATETIME() AS fechaAct, mvc.vis_objetivo AS objetivo, motivos_visita.motivov_nombre AS motivo, (SELECT (CASE WHEN empleado.emp_correo_electronico IS NULL THEN representantes_comerciales.rc_email ELSE empleado.emp_correo_electronico END) AS Expr1 FROM empleado INNER JOIN  usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id INNER JOIN  representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN mov_visita_mercadeo as mvc ON mvc.vis_usu_agendo = usuario.usu_login WHERE(mvc.vis_ncons = " + idVisita + ")) AS correoPro, " +
                          "(CASE WHEN mvc.vis_feria IS NULL THEN Vista_Clientes_Todos.ciudadCliente ELSE ciudad_1.ciu_nombre END) AS ciudad_nombre_cliente,  "+
                          "(CASE WHEN mvc.vis_feria IS NULL THEN Vista_Clientes_Todos.idCiudad ELSE ciudad_1.ciu_id END) AS ciudad_id_cliente,  "+
                          "(SELECT STUFF((SELECT  ', ' + (CASE WHEN empleado.emp_correo_electronico IS NULL THEN representantes_comerciales.rc_email ELSE empleado.emp_correo_electronico END) FROM     empleado INNER JOIN  "+
                          "usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id INNER JOIN  "+
                          "representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN mov_vis_procesos mvp ON usuario.usu_login = mvp.vis_pro_usu  WHERE(mvp.vis_pro_vis_ncons = "+idVisita+") FOR XML PATH('')), 1, 1, '')) AS procesos,  "+
                          "(SELECT STUFF((SELECT  ', ' +  mvp.vis_pro_usu FROM  mov_vis_procesos mvp WHERE(mvp.vis_pro_vis_ncons = "+idVisita+") FOR XML PATH('')), 1, 1, '')) AS etiqueta,  "+
                          "(CASE WHEN  rc.rc_descripcion IS NULL THEN e.emp_nombre + ' ' + e.emp_apellidos ELSE rc.rc_descripcion END) AS usuSoli  FROM mov_visita_mercadeo mvc INNER JOIN cliente ON cliente.cli_id = mvc.vis_cli_id INNER JOIN    ciudad ON mvc.vis_idciudad = ciudad.ciu_id INNER JOIN   pais ON pais.pai_id = ciudad.ciu_pai_id INNER JOIN   motivos_visita ON mvc.vis_motivov_id = motivos_visita.motivov_id  LEFT OUTER JOIN lite_tipo_fuente  eveFer ON mvc.vis_feria = eveFer.tifuente_id LEFT OUTER JOIN pais AS pais_1 ON eveFer.tifuente_pais = pais_1.pai_id LEFT OUTER JOIN ciudad AS ciudad_1 ON eveFer.tifuente_ciudad = ciudad_1.ciu_id LEFT JOIN  usuario u ON u.usu_login = mvc.vis_usu_plan  LEFT OUTER JOIN empleado e ON u.usu_emp_usu_num_id = e.emp_usu_num_id LEFT OUTER JOIN usuario u1 ON u1.usu_login = mvc.vis_usu_plan  LEFT OUTER JOIN representantes_comerciales rc ON u1.usu_siif_id = rc.rc_usu_siif_id  "+
						  "INNER JOIN   "+
                          "Vista_Clientes_Todos ON mvc.vis_cli_id = Vista_Clientes_Todos.idCliente   "+
                          //" AND mvc.vis_bd_origen = Vista_Clientes_Todos.bdOrigen  " +
						  "WHERE(mvc.vis_ncons = " + idVisita + ");";
            
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Trae datos del pais del cliente
        public DataTable cargarPaisCli(String idCli)
        {
            DataTable consulta = null;
            String proc = "SELECT   cliente.cli_pai_id AS idCliPais, pais.pai_nombre AS nomCliPais FROM  cliente INNER JOIN pais ON cliente.cli_pai_id = pais.pai_id WHERE cliente.cli_id = " + idCli + "";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        /***********************VIAJES********************************/
        //Inserta en la tabal mov_visitas_mercadeo
        public String insertarViaje(String usuComercial, String fechaIni, String fechaFin, String idPais, String idCiudad, String usuCrea, String horaInicio, String horaFin)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO mov_vis_viajes (via_usu_viaja, via_dfechaIni, via_dfechaFin, via_pais, via_ciudad, via_usu_crea, via_fecha_crea, via_activo, via_horaIni, via_horaFin ) "
                + " VALUES ('" + usuComercial + "', '" + fechaIni + "', '" + fechaFin + "', " + idPais + ", " + idCiudad + ", '" + usuCrea + "', SYSDATETIME(), 1, '" + horaInicio + "', '" + horaFin + "'); ";
                BdDatos.Actualizar(sql);
                mensaje = "OK"; ;
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (mov_visita_mercadeo)";
            }
            return mensaje;
        }
        //Me valida si ya exites un rango de fechas con el usuario
        public Boolean validarViajeExiste(String fechaIni, String fechaFin, String usuComercial)
        {
            String sql = "SELECT  via_id FROM   mov_vis_viajes WHERE  (via_activo=1)  AND   (via_usu_viaja = '" + usuComercial + "') AND (CONVERT(DATETIME, '" + fechaIni + "', 103) > via_dfechaIni) AND (CONVERT(DATETIME, '" + fechaIni + "', 103)  < via_dfechaFin) OR "
            + " (via_activo=1)  AND (via_usu_viaja = '" + usuComercial + "') AND (CONVERT(DATETIME, '" + fechaFin + "', 103) > via_dfechaIni) AND (CONVERT(DATETIME, '" + fechaFin + "', 103)  < via_dfechaFin); ";
            Boolean exisViaje = false;
            DataTable consulta = BdDatos.CargarTabla(sql);
            if (consulta.Rows.Count >= 1)
            {
                exisViaje = true;
            }
            else
            {
                exisViaje = false;
            }
            return exisViaje;
        }
        //Carga la lista de l0s comerciales
        public DataTable cargarListaComer(String texto)
        {
            DataTable consulta = null;
            String proc = "SELECT     usuario.usu_login AS usuario, representantes_comerciales.rc_descripcion AS nomUsu   FROM         representantes_comerciales INNER JOIN"
            + " usuario ON representantes_comerciales.rc_usu_siif_id = usuario.usu_siif_id   WHERE     (representantes_comerciales.rc_activo = 1) AND (usuario.usu_activo = 1)  AND (representantes_comerciales.rc_descripcion LIKE '%" + texto + "%');";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Carga la lista de los paises
        public DataTable cargarListaPais(String texto)
        {
            DataTable consulta = null;
            String proc = "SELECT  pai_id AS idPais, pai_nombre AS nomPais  FROM pais WHERE (pais.pai_nombre LIKE '%" + texto + "%');";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Carga la lista de las ciudades
        public DataTable cargarListaCiudad(String texto, String idPais)
        {
            DataTable consulta = null;
            String proc = "SELECT  ciu_id AS idCiudad, ciu_nombre AS nomCiu  FROM  ciudad WHERE (ciudad.ciu_nombre LIKE '%" + texto + "%') AND (ciu_pai_id = " + idPais + ");";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Carga la tabla de viajes
        public DataTable cargarTablaViajes(String usuario)
        {
            DataTable consulta = null;
            String proc = "SELECT    mov_vis_viajes.via_id AS idViaje, representantes_comerciales.rc_descripcion AS nomCom, pais.pai_nombre AS pais, ciudad.ciu_nombre AS ciudad, CONVERT(VARCHAR(MAX),mov_vis_viajes.via_dfechaIni,103) AS fechaIni, "
            + " CONVERT(VARCHAR(MAX),mov_vis_viajes.via_dfechaFin, 103) AS fechaFin, via_horaIni AS horaIni, via_horaFin AS horaFin"
            + " FROM  representantes_comerciales INNER JOIN "
            + " usuario ON representantes_comerciales.rc_usu_siif_id = usuario.usu_siif_id INNER JOIN "
            + " mov_vis_viajes INNER JOIN "
            + " pais ON mov_vis_viajes.via_pais = pais.pai_id INNER JOIN "
            + " ciudad ON mov_vis_viajes.via_ciudad = ciudad.ciu_id INNER JOIN "
            + " usuario AS usuario_1 ON mov_vis_viajes.via_usu_crea = usuario_1.usu_login ON usuario.usu_login = mov_vis_viajes.via_usu_viaja "
            + " WHERE (mov_vis_viajes.via_activo = 1) AND (usuario_1.usu_login = N'" + usuario + "') ORDER BY via_id DESC ";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Trae el permiso del rol del usuario que ingreso al SIO, se tuvo que hacer aparte ya que puede tener activados dos
        public DataTable permisosViajes(String usuario)
        {
            String sql = "SELECT usuario.usu_login AS hay  FROM   usuario INNER JOIN  rolapp ON usuario.usu_rap_id = rolapp.rap_id  WHERE  (usuario.usu_login = '" + usuario + "') AND (rolapp.rap_lviajes = 1); ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Anula el viaje
        public String anularViaje(int idViaje)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE mov_vis_viajes SET via_activo = 0 WHERE mov_vis_viajes.via_id = " + idViaje + "";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY AL MODIFICAR (mov_vis_viajes)";
            }
            return mensaje;
        }
        //Me trae la fechas de los viajes dependiendo el usuario
        public DataTable fechaViajes(String usuario, String mes, String ano)
        {
            DataTable consulta = null;
            String sql = " SELECT     mov_vis_viajes.via_dfechaIni AS fechaViaIni, mov_vis_viajes.via_dfechaFin AS fechaViaFin, pais.pai_nombre + ' / ' + ciudad.ciu_nombre + ' ( ' + CONVERT(VARCHAR, mov_vis_viajes.via_dfechaIni, 103) "
                     + "  + ' : ' + mov_vis_viajes.via_horaIni + '  -  ' + CONVERT(VARCHAR, mov_vis_viajes.via_dfechaFin, 103)  + ' : ' + mov_vis_viajes.via_horaFin + ')' AS destino "
                     + " FROM    pais INNER JOIN "
                     + " ciudad ON pais.pai_id = ciudad.ciu_pai_id INNER JOIN mov_vis_viajes ON ciudad.ciu_id = mov_vis_viajes.via_ciudad "
                     + " WHERE  (MONTH(mov_vis_viajes.via_dfechaIni) = '" + mes + "') AND (YEAR(mov_vis_viajes.via_dfechaIni) = '" + ano + "') AND (mov_vis_viajes.via_activo = 1) AND (mov_vis_viajes.via_usu_viaja = '" + usuario + "') ORDER BY fechaViaIni ";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Busca el color del viaje
        public String colorViajes(int idColor)
        {
            DataTable consulta = null;
            String color = "";
            String sql = "SELECT  via_col_nombre AS color  FROM  mov_vis_viajes_color   WHERE  (via_col_id = " + idColor + "); ";
            consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                color = row["color"].ToString();
            }
            return color;
        }
        //Busca el id del viaje por medio de la fecha de agendamiento, despues toma el id del viaje y lo inserta en la visita
        public String insertViajeVisita(String idVisita, String fechaAgen, String usuario)
        {
            String mensaje = "";
            String sql = "";
            if (fechaAgen == "NULL")
            {
                sql = "UPDATE mov_visita_mercadeo SET vis_viaje = null WHERE mov_visita_mercadeo.vis_ncons = " + idVisita + "";
            }
            else
            {
                sql = " UPDATE mov_visita_mercadeo SET vis_viaje = (SELECT  via_id AS idViaje FROM  mov_vis_viajes  WHERE   (via_activo = 1) AND (CONVERT(DATETIME, '" + fechaAgen + "', 102) >= via_dfechaIni) "
                + " AND (CONVERT(DATETIME, '" + fechaAgen + "', 102) <= via_dfechaFin) AND (via_usu_viaja = '" + usuario + "') AND (via_ciudad = (SELECT m.vis_idciudad  FROM mov_visita_mercadeo m WHERE m.vis_ncons = " + idVisita + "))) "
                + " WHERE mov_visita_mercadeo.vis_ncons = " + idVisita + " ;";
            }

            try
            {
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY(mov_vis_viajes)";
            }
            return mensaje;
        }
        //inserta los procesos que van a acompañar a la visita
        //public void insertProcesos(String idVisita, List<string> list)
        //{
        //    //if (lisProces != "")
        //    //{
        //    //String sql = " INSERT INTO mov_vis_procesos  SELECT " + idVisita + " AS idVisita, are_id AS idArea FROM  area WHERE (are_id IN (" + lisProces + "))";
        //    //BdDatos.Actualizar(sql);
        //    //}

        //    string sql = "";

        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        sql += " INSERT INTO mov_vis_procesos(vis_pro_vis_ncons, vis_pro_usu) Values ("+idVisita+", '" + list.ElementAt(i).ToString() + "');";
        //    }
        //    BdDatos.Actualizar(sql);
        //}
        //Carga el combo de los procesos
        public DataTable cargarProcesos(String filtro)
        {
            String sql = " SELECT  are_id AS idArea, are_nombre AS nomArea FROM  area WHERE  (are_activo = 1) AND (are_acompaña = 1) " + filtro;
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga el combo de los procesos
        public DataTable cargarCorreoProcesos(String filtro)
        {
            String sql = " SELECT  emp_usu_num_id AS cedula, emp_correo_electronico AS correoPro FROM empleado WHERE (emp_jefe_area = 1) AND (emp_area_id IN (" + filtro + ")) ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Consulta si tiene contacto
        public Boolean consulTieneContacto(String idVisita)
        {
            DataTable consulta = null;
            Boolean contacto = false;
            String sql = "SELECT  vis_idcontacto AS idConta  FROM   mov_visita_mercadeo  WHERE   (vis_ncons = " + idVisita + ") AND (vis_idcontacto IS NOT NULL)";
            consulta = BdDatos.CargarTabla(sql);
            if (consulta.Rows.Count > 0)
            {
                contacto = true;
            }
            else { contacto = false; }
            return contacto;
        }
        /***********************EVENTOS/FERIAS********************************/
        //Me trae los eventos y las ferias
        public DataTable cargarEventosF(String filtro)
        {
            DataTable consulta = null;
            String sql = " SELECT eveFer.tifuente_id AS numEve, CONVERT(VARCHAR(MAX), eveFer.tifuente_id) + '.' + eveFer.tifuente_descripcion AS idNomEve, eveFer.tifuente_descripcion AS nomEve, pais.pai_id AS idPaisEve, "
            + " CONVERT(CHAR(10),CONVERT(DATE,eveFer.tifuente_fechaini)) AS fechaIniEve, CONVERT(CHAR(10),CONVERT(DATE,eveFer.tifuente_fechafin)) AS fechaFinEve, ciudad.ciu_nombre AS ciudadEve, pais.pai_nombre AS paisEve, SYSDATETIME() AS fechaAct "
            + " FROM   ciudad INNER JOIN  lite_tipo_fuente eveFer ON ciudad.ciu_id = eveFer.tifuente_ciudad INNER JOIN  pais ON eveFer.tifuente_pais = pais.pai_id  INNER JOIN  lite_tipo_origen lto ON eveFer.tifuente_origen_id = lto.tiorigen_id "
            + " WHERE (eveFer.tifuente_activo = 1) AND (lto.tiorigen_id = 1) " + filtro + " OR "
            + " (eveFer.tifuente_activo = 1) AND (lto.tiorigen_id = 4) " + filtro + " OR "
            + " (eveFer.tifuente_activo = 1) AND (lto.tiorigen_id = 5) " + filtro;
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public string cargarUltimaVisita(int visita)
        {
            DataTable consulta = null;
            String sql = " SELECT   convert(varchar(MAX), mvm.vis_ncons)   + ' ' + (CASE WHEN mvm.vis_feria IS NULL THEN vct.nomCliente ELSE vct.nomCliente END)  AS nomVisita  FROM   mov_visita_mercadeo mvm INNER JOIN   Vista_Clientes_Todos vct ON  vct.idCliente = mvm.vis_cli_id WHERE (mvm.vis_ncons = " + visita + ") ";
            consulta = BdDatos.CargarTabla(sql);
            string result = consulta.Rows[0]["nomVisita"].ToString();
            return result;
        }

        //Consulta el correo forsa
        public DataTable consultarParametrosEverNote()
        {
            DataTable consulta = null;
            String sql = "SELECT par_correo_evernote AS correo FROM Parametros";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public int getBitEvernote(int rol)
        {
            int bitEvernote = 0;
            string sql = "SELECT comercial FROM rolapp WHERE rap_id =" + rol;
            DataTable dt = new DataTable();
            dt = BdDatos.CargarTabla(sql);
            if (!String.IsNullOrEmpty(dt.Rows[0]["comercial"].ToString()))
            {
                bitEvernote = Convert.ToInt32(dt.Rows[0]["comercial"]);
            }
            return bitEvernote;
        }

        public string getPathCarpetaEvernote(int ciudad)
        {
            string path = "";
           // string sql = "SELECT         ISNULL(zonaCiudad.nombre_carpeta, '@CRM ' + pais.pai_nombre) AS nombre_carpeta, ciudad.ciu_nombre " +
            string sql = " select   ISNULL(CASE WHEN fup_grupo_pais.grpa_id = 2 THEN  '@CRM ' +  fup_grupo_pais.grpa_gp1_nombre ELSE zonaCiudad.nombre_carpeta END, '@CRM ' + pais.pai_nombre) AS nombre_carpeta,ciudad.ciu_nombre  " +      
                "FROM fup_grupo_pais INNER JOIN " +
                         "pais ON fup_grupo_pais.grpa_id = pais.pai_grupopais_id INNER JOIN " +
                         "ciudad ON pais.pai_id = ciudad.ciu_pai_id LEFT OUTER JOIN " +
                         "zonaCiudad ON ciudad.ciu_zona = zonaCiudad.zonac_id AND pais.pai_id = zonaCiudad.zonac_pais " +
                         "WHERE(ciudad.ciu_id = " + ciudad + ")";
            DataTable dt = new DataTable();
            dt = BdDatos.CargarTabla(sql);

            if (!String.IsNullOrEmpty(dt.Rows[0]["nombre_carpeta"].ToString()))
                path = dt.Rows[0]["nombre_carpeta"].ToString();

            return path;            
        }

        public int CerrarConexion()
        {
            return BdDatos.desconectar();
        }

        public DataTable cargarTipoFuente()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT        tifuente_id, tifuente_descripcion FROM lite_tipo_fuente WHERE(tifuente_activo = 1)";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }
    }
}