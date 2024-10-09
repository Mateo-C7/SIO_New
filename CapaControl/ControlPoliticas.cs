using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using CapaDatos;
using System.Diagnostics;
using System.Threading;

namespace CapaControl
{
    public class ControlPoliticas
    {
        /*********MENU********/
        //Consulta los poli_modulos para crear el menu, dependiendo del usuario y su rol
        public DataTable consultapoli_modulos(String usuario)
        {
            DataTable consulta = null;
            String sql = "SELECT poli_modulos.mod_id AS idMod, poli_modulos.mod_nom AS nomMod, poli_modulos.mod_enlace AS enlaceMod "
                + " FROM         poli_modulos INNER JOIN "
                + " poli_rol_mod ON poli_modulos.mod_id = poli_rol_mod.rm_mod_id INNER JOIN "
                + " rolapp ON poli_rol_mod.rm_rap_id = rolapp.rap_id INNER JOIN "
                + " usuario ON rolapp.rap_id = usuario.usu_rap_id "
                + " WHERE     (usuario.usu_login = '" + usuario + "') AND (poli_rol_mod.rm_act = 1) AND (poli_modulos.mod_act = 1) "
                + " GROUP BY poli_modulos.mod_id, poli_modulos.mod_nom, poli_modulos.mod_enlace ";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Consulta las poli_rutinas para crear el submenu, dependiendo del usuario y su rol
        public DataTable consultaRutinas(String usuario, int modulo)
        {
            DataTable consulta = null;
            String sql = "SELECT     poli_rutinas.rut_id, poli_rutinas.rut_nom AS nomRut, poli_rutinas.rut_enlace AS enlaceRut"
                    + " FROM         poli_rutinas INNER JOIN "
                    + " poli_rol_rut ON poli_rutinas.rut_id = poli_rol_rut.rr_rut_id INNER JOIN "
                    + " rolapp ON poli_rol_rut.rr_rap_id = rolapp.rap_id INNER JOIN "
                    + " usuario ON rolapp.rap_id = usuario.usu_rap_id "
                    + " WHERE     (usuario.usu_login = '" + usuario + "')  AND (poli_rutinas.rut_mod_id = " + modulo + ") AND (poli_rol_rut.rr_act = 1) AND (poli_rutinas.rut_act = 1) "
                    + " GROUP BY poli_rutinas.rut_id, poli_rutinas.rut_nom, poli_rutinas.rut_enlace, poli_rutinas.rut_orden   order by poli_rutinas.rut_orden ";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        /*********MENU********/
        /*********ADMINISTRACION********/
        public DataTable cargarModulosXRolFiltro(int idRol)
        {
            DataTable consulta = null;
            String sql = "SELECT     poli_modulos.mod_id AS idMod, poli_modulos.mod_nom AS nomMod, poli_rol_mod.rm_act AS activoMod "
                    + " FROM         poli_modulos INNER JOIN "
                    + " poli_rol_mod ON poli_modulos.mod_id = poli_rol_mod.rm_mod_id "
                    + " WHERE     (poli_rol_mod.rm_rap_id = " + idRol + ") AND (poli_modulos.mod_act = 1)  AND (poli_rol_mod.rm_act =1) AND (poli_modulos.mod_id IN (SELECT  rut_mod_id  FROM  poli_rutinas  GROUP BY rut_mod_id)) ";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Carga los poli_modulos dependiendo del id del Rol, solo los que esten activos
        public DataTable cargarModulosFiltro(int idRol)
        {
            DataTable consulta = null;
            String sql = "SELECT     poli_modulos.mod_id AS idMod, poli_modulos.mod_nom  AS nomMod, poli_rol_mod.rm_act AS activoMod, (CASE WHEN mod_sub = 0 THEN '' ELSE (SELECT S.mod_nom FROM  poli_modulos S  WHERE  (S.mod_id = poli_modulos.mod_sub)) END) AS principal, (CASE WHEN poli_modulos.mod_sub = 0 THEN mod_id ELSE poli_modulos.mod_sub END) as ordeSubmod, mod_ordenSub AS ordenSub "
            + " FROM         poli_modulos INNER JOIN poli_rol_mod ON poli_modulos.mod_id = poli_rol_mod.rm_mod_id "
            + " WHERE     (poli_rol_mod.rm_rap_id = " + idRol + ") AND (poli_modulos.mod_act = 1) "
            + " UNION  "
            + " SELECT     poli_modulos.mod_id AS idMod, poli_modulos.mod_nom AS nomMod , 'False' AS activoMod, (CASE WHEN mod_sub = 0 THEN '' ELSE (SELECT S.mod_nom FROM  poli_modulos S  WHERE  (S.mod_id = poli_modulos.mod_sub)) END) AS principal, (CASE WHEN poli_modulos.mod_sub = 0 THEN mod_id ELSE poli_modulos.mod_sub END) as ordeSubmod, mod_ordenSub AS ordenSub "
            + " FROM         poli_modulos  "
            + " WHERE      (poli_modulos.mod_act = 1) AND (poli_modulos.mod_id NOT IN (SELECT  poli_modulos.mod_id FROM  poli_modulos INNER JOIN poli_rol_mod ON poli_modulos.mod_id = poli_rol_mod.rm_mod_id WHERE (poli_rol_mod.rm_rap_id = " + idRol + "))) ORDER BY ordenSub";//ORDER BY  ordeSubmod
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Consulta en la tabla poli_rol_mod si ya existe
        public String consultarRolMod(int idRol, int idMod)
        {
            String existe = "";
            int exis = 0;
            DataTable consulta = null;
            String sql = "SELECT COUNT(rm_rap_id) AS existe FROM poli_rol_mod WHERE  (poli_rol_mod.rm_rap_id = " + idRol + ") AND (poli_rol_mod.rm_mod_id = " + idMod + ")";
            consulta = BdDatos.CargarTabla2(sql);
            foreach (DataRow row in consulta.Rows)
            {
                exis = int.Parse(row["existe"].ToString());
            }

            if (exis == 0)
            {  existe = "NO"; }
            else  {  existe = "SI"; }
            return existe;
        }
        //Inserta la relacion del rol con el modulo
        public String insertarRolMod(int idRol, int idMod, String usuario)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO poli_rol_mod (rm_rap_id, rm_mod_id, rm_act, rm_fecha_crea, rm_usu_crea) VALUES (" + idRol + "," + idMod + ",1, SYSDATETIME(),'" + usuario + "')";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT poli_rol_mod)";
            }
            return mensaje;
        }
        //Carga todos los roles
        public DataTable cargarRoles()
        {
            DataTable consulta = null;
            String sql = "SELECT   rap_id AS idRol, rap_nombre AS nomRol, (CASE WHEN rap_lvice IS NULL THEN 'False' ELSE rap_lvice END) AS viceRol, (CASE WHEN rap_gerente IS NULL THEN 'False' ELSE rap_gerente END) AS genRol, (CASE WHEN rap_lrepre IS NULL THEN 'False' ELSE rap_lrepre END) AS repRol, (CASE WHEN rap_lviajes IS NULL THEN 'False' ELSE rap_lviajes END) AS viaRol FROM  rolapp WHERE (rap_activo = 1) ORDER BY nomRol ";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Actualiza el estado de la relacion del modulo con el rol, dependiendo del id del rol y el id del modulo
        public void cambiarPolRol(int idRol, int idMod, bool valor, String usuario)
        {
            String sql = "UPDATE poli_rol_mod SET  poli_rol_mod.rm_act = '" + valor + "', poli_rol_mod.rm_fecha_crea = SYSDATETIME(), poli_rol_mod.rm_usu_crea = '" + usuario + "' WHERE poli_rol_mod.rm_rap_id = " + idRol + " AND poli_rol_mod.rm_mod_id = " + idMod + "";
            BdDatos.Actualizar(sql);
        }
        //Carga las poli_rutinas dependiendo del id del rol y el id del modulo
        public DataTable cargarRutinasFiltro(int idRol, int idMod)
        {
            DataTable consulta = null;
            String sql = "SELECT     poli_rutinas.rut_id AS idRut, poli_rutinas.rut_nom AS nomRut, poli_rol_rut.rr_agregar AS agrRut, poli_rol_rut.rr_eliminar AS eliRut, poli_rol_rut.rr_imprimir AS impRut,  "
                    + " poli_rol_rut.rr_editar AS ediRut, poli_rol_rut.rr_act AS activoRut "
                    + " FROM         poli_rutinas INNER JOIN "
                    + " poli_rol_rut ON poli_rutinas.rut_id = poli_rol_rut.rr_rut_id "
                    + " WHERE     (poli_rol_rut.rr_rap_id = " + idRol + ") AND (poli_rutinas.rut_mod_id = " + idMod + ")  AND (poli_rol_rut.rr_act = 1)";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Consulta en la tabla poli_rol_rut si ya existe
        public String consultarRolRut(int idRol, int idRut)
        {
            String existe = "";
            int exis = 0;
            DataTable consulta = null;
            String sql = "SELECT COUNT(rr_rut_id) AS existe FROM poli_rol_rut WHERE  (rr_rap_id = " + idRol + ") AND (rr_rut_id  = " + idRut + ")";
            consulta = BdDatos.CargarTabla2(sql);
            foreach (DataRow row in consulta.Rows)
            {
                exis = int.Parse(row["existe"].ToString());
            }

            if (exis == 0)
            { existe = "NO"; }
            else {  existe = "SI";}
            return existe;
        }
        //Inserta la relacion del rol con la rutina
        public String insertarRolRut(int idRol, int idRut, String filtro, bool valor, String usuario)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO poli_rol_rut (rr_rap_id, rr_rut_id, rr_fecha_crea, rr_usu_crea, " + filtro + ") VALUES (" + idRol + "," + idRut + ",SYSDATETIME(),'" + usuario + "','" + valor + "')";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT poli_rol_rut)";
            }
            return mensaje;
        }
        //Actualiza el estado de la relacion de la rutina con el rol, dependiendo del id del rol y el id de la rutina
        public void cambiarPolRut(String filtro, int idRol, int idRut, bool valor, String usuario)
        {
            String sql = "UPDATE poli_rol_rut SET  " + filtro + " = '" + valor + "',  rr_fecha_crea = SYSDATETIME(), rr_usu_crea = '" + usuario + "'  WHERE poli_rol_rut.rr_rap_id = " + idRol + " AND poli_rol_rut.rr_rut_id = " + idRut + "";
            BdDatos.Actualizar(sql);
        }
        //Inserta en la tabla  rolapp en nuevo rol
        public String insertarRol(String nomRol, bool vice, bool gere, bool repre, bool viajes)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO rolapp (rap_id, rap_mod_id, rap_nombre, rap_lvice, rap_gerente, rap_lrepre, rap_lviajes) VALUES ((SELECT MAX(rap_id)+1 FROM rolapp), 1, '" + nomRol + "', '" + vice + "','" + gere + "','" + repre + "', '" + viajes + "')";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT rolapp)";
            }
            return mensaje;
        }
        //Actualiza el estado del rol, dependiendo del id del rol
        public void cambiarEstRol(String filtro, int idRol, bool valor)
        {
            String sql = "UPDATE rolapp SET  " + filtro + " = '" + valor + "' WHERE rap_id = " + idRol + "";
            BdDatos.Actualizar(sql);
        }
        //Elimina el rol
        public void eliminarRol(int idRol, int activo)
        {
            String sql = "UPDATE rolapp SET rap_activo = " + activo + " WHERE rap_id = " + idRol + "";
            BdDatos.Actualizar(sql);
        }
        //Busca los poli_modulos principales
        public DataTable buscarMenusPrin(String usuario)
        {
            DataTable consulta = null;
            String sql = "SELECT poli_modulos.mod_id AS idMod, poli_modulos.mod_nom AS nomMod, poli_modulos.mod_enlace AS enlaceMod, poli_modulos.mod_sub AS sub "
                    + " FROM         poli_modulos INNER JOIN "
                    + " poli_rol_mod ON poli_modulos.mod_id = poli_rol_mod.rm_mod_id INNER JOIN "
                    + " rolapp ON poli_rol_mod.rm_rap_id = rolapp.rap_id INNER JOIN "
                    + " usuario ON rolapp.rap_id = usuario.usu_rap_id "
                    + " WHERE     (usuario.usu_login = '" + usuario + "') AND (poli_rol_mod.rm_act = 1) AND (poli_modulos.mod_act = 1) AND (poli_modulos.mod_prin = 1) AND (poli_modulos.mod_sub = 0) "
                    + " GROUP BY poli_modulos.mod_id, poli_modulos.mod_nom, poli_modulos.mod_enlace, poli_modulos.mod_sub, poli_modulos.mod_ordenSub order by poli_modulos.mod_ordenSub ";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Busca los subpoli_modulos
        public DataTable buscarMenuSub(int idMenuPrin, String usuario)
        {
            DataTable consulta = null;
            String sql = "SELECT poli_modulos.mod_id AS idMod, poli_modulos.mod_nom AS nomMod, poli_modulos.mod_enlace AS enlaceMod, poli_modulos.mod_sub AS sub, poli_modulos.mod_ordenSub "
                    + " FROM         poli_modulos INNER JOIN "
                    + " poli_rol_mod ON poli_modulos.mod_id = poli_rol_mod.rm_mod_id INNER JOIN "
                    + " rolapp ON poli_rol_mod.rm_rap_id = rolapp.rap_id INNER JOIN "
                    + " usuario ON rolapp.rap_id = usuario.usu_rap_id "
                    + " WHERE     (usuario.usu_login = '" + usuario + "') AND (poli_rol_mod.rm_act = 1) AND (poli_modulos.mod_act = 1) AND (poli_modulos.mod_sub = " + idMenuPrin + ") "
                    + " GROUP BY poli_modulos.mod_id, poli_modulos.mod_nom, poli_modulos.mod_enlace, poli_modulos.mod_sub, poli_modulos.mod_ordenSub order by poli_modulos.mod_ordenSub ";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Cuenta si tiene subpoli_modulos
        public int contarMenuSub(int idMenuPrin, String usuario)
        {
            int sub = 0;
            DataTable consulta = null;
            String sql = "SELECT COUNT(poli_modulos.mod_id) AS idMod"
                    + " FROM         poli_modulos INNER JOIN "
                    + " poli_rol_mod ON poli_modulos.mod_id = poli_rol_mod.rm_mod_id INNER JOIN "
                    + " rolapp ON poli_rol_mod.rm_rap_id = rolapp.rap_id INNER JOIN "
                    + " usuario ON rolapp.rap_id = usuario.usu_rap_id "
                    + " WHERE     (usuario.usu_login = '" + usuario + "') AND (poli_rol_mod.rm_act = 1) AND (poli_modulos.mod_act = 1) AND (poli_modulos.mod_sub = " + idMenuPrin + ") "
                    + " GROUP BY poli_modulos.mod_id, poli_modulos.mod_nom, poli_modulos.mod_enlace, poli_modulos.mod_sub ";
            consulta = BdDatos.CargarTabla2(sql);
            if (consulta != null)
            {
                foreach (DataRow row in consulta.Rows)
                {
                    sub = int.Parse(row["idMod"].ToString());
                }
            }
            return sub;
        }
        //Inserta todas las poli_rutinas con el rol
        public String insertarRolRutinas(String usuario)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO poli_rol_rut "
                + " SELECT  (SELECT MAX(rolapp.rap_id) FROM rolapp ) AS rol, poli_rutinas.rut_id, 1 AS activo,  SYSDATETIME() AS fechaCrea, '" + usuario + "' AS usuCrea,  poli_rutinas.rut_agregar, poli_rutinas.rut_eliminar, poli_rutinas.rut_imprimir, poli_rutinas.rut_editar "
                + " FROM    poli_rutinas "
                + " ORDER BY poli_rutinas.rut_id ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT poli_rol_rut)";
            }
            return mensaje;
        }
        //Inserta las poli_rutinas nuevas con el rol
        public String insertarRolRutinasNew(String usuario, String idRol, String idMod)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO poli_rol_rut  "
                            + " SELECT  " + idRol + " AS rol, poli_rutinas.rut_id AS rutina, 1 AS activo, SYSDATETIME() AS fechaCrea, '" + usuario + "' AS usuCrea, poli_rutinas.rut_agregar, poli_rutinas.rut_eliminar, poli_rutinas.rut_imprimir, poli_rutinas.rut_editar "
                            + " FROM     poli_rutinas  "
                            + " WHERE    (poli_rutinas.rut_id NOT IN (SELECT   poli_rutinas.rut_id  FROM  poli_rutinas INNER JOIN poli_rol_rut ON poli_rutinas.rut_id = poli_rol_rut.rr_rut_id "
                            + " WHERE    (poli_rutinas.rut_mod_id = " + idMod + ") AND (poli_rol_rut.rr_rap_id = " + idRol + "))) AND (poli_rutinas.rut_mod_id = " + idMod + ") AND (poli_rutinas.rut_act = 1) "
                            + " ORDER BY poli_rutinas.rut_id ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT poli_rol_rut)";
            }
            return mensaje;
        }
        //Carga todos los usuarios
        public DataTable cargarListaUsu(String texto)
        {
            DataTable consulta = null;
            String sql = "SELECT  usu_siif_id AS usuId, usu_login AS usuLogin FROM usuario WHERE (usu_login LIKE N'%" + texto + "%')";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Carga todos los roles
        public DataTable cargarListaRoles()
        {
            DataTable consulta = null;
            String sql = "SELECT    rap_id AS rolId, rap_nombre AS nomRol FROM rolapp WHERE (rap_activo = 1)";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Busca el usuario para modificarlo dependiendo del id
        public DataTable buscarUsuId(int usuId)
        {
            DataTable consulta = null;
            String sql = "SELECT     usu_siif_id AS usuId, usu_login AS login, usu_passwd AS pass, usu_emp_usu_num_id AS cedula, usu_rap_id AS rolId, "
                      + " (CASE WHEN usu_activo IS NULL THEN 'False' ELSE usu_activo END) AS activo, (CASE WHEN usu_colombia IS NULL  "
                      + " THEN 'False' ELSE usu_colombia END) AS usuCol, (CASE WHEN usu_mexico IS NULL THEN 'False' ELSE usu_mexico END) AS usuMex,  "
                      + " (CASE WHEN usu_uruguay IS NULL THEN 'False' ELSE usu_uruguay END) AS usuUru, (CASE WHEN usu_brasil IS NULL  "
                      + " THEN 'False' ELSE usu_brasil END) AS usuBra, (CASE WHEN usu_aprobador_tablero IS NULL THEN 'False' ELSE usu_aprobador_tablero END)  "
                      + " AS usuAprobadorT, (CASE WHEN usu_of_cierre IS NULL THEN 'False' ELSE usu_of_cierre END) AS usuCierre, (CASE WHEN usu_gasto IS NULL  "
                      + " THEN 'False' ELSE usu_gasto END) AS usuGasto, (CASE WHEN usu_admin_casino IS NULL  THEN 'False' ELSE usu_admin_casino END) AS usuCasino  FROM usuario  WHERE (usu_siif_id = " + usuId + ")";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Busca las planta relacionadas con el usuario
        public DataTable buscarUsuPlanta(int usuId)
        {
            DataTable consulta = null;
            String sql = "SELECT  planta_forsa.planta_id AS idPlanta, planta_forsa.planta_descripcion AS nomPlanta "
            + " FROM   planta_usuario INNER JOIN  planta_forsa ON planta_usuario.plantausu_idplanta = planta_forsa.planta_id "
            + " WHERE  (planta_usuario.plantausu_activo = 1) AND (planta_forsa.planta_activo = 1) AND (planta_usuario.plantausu_idusuario = " + usuId + ")";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }

        public SqlDataReader consultarPlantaDefecto(int usuario)
        {
            string sql;
            sql = "SELECT        plantausu_idplanta FROM planta_usuario WHERE plantausu_idusuario ="+ usuario+" "+
                " AND (plantausu_defecto = 1)";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //Busca los datos del comercial relacionadas con el usuario
        public DataTable buscarUsuComer(int usuId)
        {
            DataTable consulta = null;
            String sql = "SELECT     rc_id AS idComer, rc_descripcion AS nomComer, rc_email AS correoComer, rc_activo AS actComer, rc_director_ofic AS actDirOfiComer, rc_oficina AS oficina, "
            + " rc_direccion_ofic AS dirreOfi, rc_telef_ofic AS telComer, rc_celular AS celComer, rc_pais_oficina AS idOfiPais "
            + " FROM         representantes_comerciales   WHERE     (rc_usu_siif_id = " + usuId + ")";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Busca los paises relacionados con el comercial
        public DataTable buscarPaisComer(String idComer)
        {
            DataTable consulta = null;
            String sql = "SELECT  pais.pai_id AS idPais, pais.pai_nombre AS nomPais  FROM   pais_representante INNER JOIN "
            + " pais ON pais_representante.pr_id_pais = pais.pai_id  WHERE  (pais_representante.pr_id_representante = " + idComer + ") AND (pais_representante.pr_activo = 1)";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Busca las ciudades relacionadas con el comercial
        public DataTable buscarCiuComer(String idComer)
        {
            DataTable consulta = null;
            String sql = "SELECT   ciudad.ciu_id AS idCiudad, ciudad.ciu_nombre AS nomCiudad  FROM   ciudad_representante INNER JOIN "
            + " ciudad ON ciudad_representante.cr_ciu_id = ciudad.ciu_id  WHERE  (ciudad_representante.cr_rc_id = " + idComer + ") AND (ciudad_representante.cr_activo = 1)";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Busca los permisos de casino
        public DataTable buscarUsuCas(String usuario)
        {
            DataTable consulta = null;
            String sql = "SELECT  (CASE WHEN usuario.usu_admin_casino = 1 THEN 'True' ELSE 'False' END) AS admiCas, (CASE WHEN usuario.usu_archPlano_nmuno = 1 THEN 'True' ELSE 'False' END) AS arcPlanoCas,  "
            + " (CASE WHEN empleado.emp_autorizado_pedido_cas = 1 THEN 'True' ELSE 'False' END) AS pedidoCas, "
            + " (CASE WHEN emp_Apru_Memo = 1 THEN 'True' ELSE 'False' END) AS formaleta, (CASE WHEN emp_Apru_Memo_Acc = 1 THEN 'True' ELSE 'False' END) AS accesorios,  "
            + " (CASE WHEN proceso_siif.proceso_id IS NULL THEN 'Seleccionar' ELSE CONVERT(VARCHAR(5),proceso_siif.proceso_id) END) AS idProceso "
            + " FROM   empleado INNER JOIN usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id LEFT OUTER JOIN "
            + " proceso_siif ON empleado.emp_reg_proceso = proceso_siif.proceso_id WHERE  (usuario.usu_login = N'" + usuario + "')";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Busca el usuario dependiendo del login
        public Boolean buscarUsuLogin(String usuario)
        {
            Boolean existe = false;
            DataTable consulta = null;
            String sql = "SELECT   usu_siif_id AS usuId  FROM usuario  WHERE (usu_login = '" + usuario + "')";
            consulta = BdDatos.CargarTabla2(sql);
            if (consulta.Rows.Count > 0)
            { existe = true; }
            else { existe = false; }
            return existe;
        }
        //Carga la tabla de usuarios
        public DataTable cargarTablaUsu()
        {
            DataTable consulta = null;
            String sql = "SELECT     usu_siif_id AS usuId, empleado.emp_nombre + ' ' + empleado.emp_apellidos AS nomUsu, usu_login AS login, usu_passwd AS pass, (CASE WHEN usu_emp_usu_num_id = 0 THEN 'NO' ELSE CONVERT(VARCHAR(MAX), usu_emp_usu_num_id) END) AS cedula, (CASE WHEN usu_emp_usu_num_id = 0 THEN 'NO' ELSE 'SI' END) AS empleadoF, rolapp.rap_nombre AS rol,  "
                      + " (CASE WHEN usu_activo = 1 THEN 'SI' ELSE 'NO' END) AS activo, (CASE WHEN usu_colombia  = 1  "
                      + " THEN 'SI' ELSE 'NO' END) AS usuCol, (CASE WHEN usu_mexico  = 1 THEN 'SI' ELSE 'NO' END) AS usuMex,  "
                      + " (CASE WHEN usu_uruguay  = 1  THEN 'SI' ELSE 'NO' END) AS usuUru, (CASE WHEN usu_brasil  = 1   "
                      + " THEN 'SI' ELSE 'NO' END) AS usuBra, (CASE WHEN usu_aprobador_tablero  = 1 THEN 'SI' ELSE 'NO' END)   "
                      + " AS usuAprobadorT, (CASE WHEN usu_of_cierre  = 1  THEN 'SI' ELSE 'NO' END) AS usuCierre, (CASE WHEN usu_gasto  = 1   "
                      + " THEN 'SI' ELSE 'NO' END) AS usuGasto, (CASE WHEN usu_pass_calidad  IS NULL THEN '' ELSE usu_pass_calidad END) AS usuCalidad,  "
                      + " (CASE WHEN usu_pass_supervisor  IS NULL  THEN '' ELSE usu_pass_supervisor END) AS usuSuperV, (CASE WHEN usu_admin_casino  = 1    "
                      + " THEN 'SI' ELSE 'NO' END) AS usuCasino   FROM   usuario  INNER JOIN  rolapp ON usuario.usu_rap_id = rolapp.rap_id INNER JOIN "
                      + " empleado ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id ORDER BY  usu_siif_id DESC";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Inserta el usuario
        public String insertaUsu(String login, String pass, String idRol, Boolean activo, String usuCrea, String cedula)
        {
            String mensaje = "";
            DataTable consulta = null;
            try
            {
                String sql = "INSERT INTO usuario (usuario.usu_siif_id, usu_login, usu_passwd, usu_rap_id, usu_activo, usu_crea, fecha_crea, usu_actualiza, fecha_actualiza,  usu_emp_usu_num_id)  "
                + " VALUES ((SELECT  MAX(usuario.usu_siif_id) + 1  FROM usuario), '" + login + "', '" + pass + "', " + idRol + ", 1, '" + usuCrea + "', SYSDATETIME(),'" + usuCrea + "', SYSDATETIME(), " + cedula + ");"
                + " SELECT  MAX(usuario.usu_siif_id) AS idUsu FROM usuario ";
                consulta = BdDatos.CargarTabla(sql);
                foreach (DataRow row in consulta.Rows)
                {
                    mensaje = row["idUsu"].ToString();
                }
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT usuario)";
            }
            return mensaje;
        }
        //Editar el usuario
        public String editarUsu(String idUsu, String pass, String idRol, Boolean activo, String usuAct, String filtroCedu)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE usuario SET  usu_passwd = '" + pass + "', usu_rap_id = " + idRol + ", usu_activo = '" + activo + "' ,  usu_actualiza = '" + usuAct + "' , fecha_actualiza = SYSDATETIME() " + filtroCedu + "  "
                + " WHERE (usu_siif_id = " + idUsu + ")  ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE usuario)";
            }
            return mensaje;
        }
        //Consulta las politicas dependiendo del usuario y la rutina
        public DataTable politicasBotones(int rutina, String usuario)
        {
            DataTable consulta = null;
            String sql = "SELECT   poli_rol_rut.rr_agregar AS agregar, poli_rol_rut.rr_eliminar AS eliminar, poli_rol_rut.rr_imprimir AS imprimir, poli_rol_rut.rr_editar AS editar"
                    + " FROM     poli_rol_rut INNER JOIN rolapp ON poli_rol_rut.rr_rap_id = rolapp.rap_id INNER JOIN "
                    + " usuario ON rolapp.rap_id = usuario.usu_rap_id  WHERE  (poli_rol_rut.rr_rut_id = " + rutina + ") AND (usuario.usu_login = N'" + usuario + "') ";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Carga los paises
        public DataTable cargarPaises(String filIdZona)
        {
            DataTable consulta = null;
            String sql = "SELECT   pai_id AS idPais, pai_nombre AS nomPais  FROM  pais  " + filIdZona + "";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Carga las zonas de los paises
        public DataTable cargarZonasP()
        {
            DataTable consulta = null;
           // String sql = "SELECT  zonap_id AS idZonaP, zonap_nom AS nomZonaP   FROM    zonaPais   WHERE   (zonap_activo = 1)";
            String sql = "SELECT   grpa_id AS idZonaP, grpa_gp1_nombre  AS nomZonaP FROM fup_grupo_pais WHERE        (activo = 1)  ORDER BY grpa_gp1_nombre ";
            consulta = BdDatos.CargarTabla2(sql);
            return consulta;
        }
        //Carga la lista de las ciudades dependiendo del pais y zona
        public DataTable cargarCiudades(String idPais, String filIdZonaC)
        {
            DataTable consulta = null;
            String proc = "SELECT  ciu_id AS idCiudad, ciu_nombre AS nomCiu  FROM  ciudad WHERE (ciu_pai_id = " + idPais + ") " + filIdZonaC + "";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Carga las zonas del pais
        public DataTable cargarZonasCPais(String idPais)
        {
            DataTable consulta = null;
            String proc = "SELECT     zonac_id AS idZonaC, zonac_nom AS nomZonaC  FROM    zonaCiudad   WHERE   (zonac_pais = " + idPais + ") AND (zonac_activo = 1)";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Carga el pais de la ciudad
        public DataTable cargarPaisCiudad(String idCiu)
        {
            DataTable consulta = null;
            String proc = "SELECT  pais.pai_id AS idPais, pais.pai_nombre AS nomPais  FROM  ciudad INNER JOIN   pais ON ciudad.ciu_pai_id = pais.pai_id   WHERE   (ciudad.ciu_id = " + idCiu + ")";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Consulta el privilegio dependiendo del Rol
        public DataTable priviRol(String idRol)
        {
            String sql = "SELECT (CASE WHEN rolapp.rap_gerente IS NULL THEN 'false' ELSE rolapp.rap_gerente END) AS gerente, (CASE WHEN rolapp.rap_lvice IS NULL THEN 'false' ELSE rolapp.rap_lvice END) AS vice, (CASE WHEN rolapp.rap_lrepre IS NULL THEN 'false' ELSE rolapp.rap_lrepre END) AS repre, (CASE WHEN rolapp.rap_lagente IS NULL THEN 'false' ELSE rolapp.rap_lagente END) AS agente, (CASE WHEN rolapp.rap_lviajes IS NULL THEN 'false' ELSE rolapp.rap_lviajes END) AS viajes "
            + " FROM   rolapp  WHERE  (rolapp.rap_id = " + idRol + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Consulta si el usuario existe con la cedula
        public DataTable consultaEmpleado(String idCedula)
        {
            String sql = "SELECT     emp_usu_num_id AS idCedula, emp_nombre + ' ' + emp_apellidos + ' / ' + area.are_nombre AS nomEmp  FROM   empleado  INNER JOIN  area ON empleado.emp_area_id = area.are_id   WHERE   (emp_usu_num_id = " + idCedula + ") AND (emp_activo = 1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga todos las plantas
        public DataTable cargarPlantas()
        {
            DataTable consulta = null;
            String proc = "SELECT   planta_id AS idPlanta, planta_descripcion AS nomPlanta   FROM    planta_forsa   WHERE     (planta_activo = 1)";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Carga todos los procesos de permisos
        public DataTable cargarProPer()
        {
            DataTable consulta = null;
            String proc = "SELECT  proceso_id AS idProc, proceso_descripcion AS nomProc   FROM  proceso_siif  WHERE  (proceso_activo = 1)";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //******//
        //Inserta las poli_rutinas nuevas con el rol
        public String insertarPlantaUsu(String idUsu, String listaPlan, int defecto)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO planta_usuario"
                + " SELECT   planta_forsa.planta_id AS idPlanta, " + idUsu + " AS idUsu , 0 AS activo, 0 as defecto, 0 ,0 FROM    planta_forsa   WHERE  (planta_forsa.planta_activo = 1) "
                + " AND (planta_forsa.planta_id NOT IN (SELECT planta_usuario.plantausu_idplanta FROM planta_usuario WHERE  (planta_usuario.plantausu_idusuario = " + idUsu + ")));"
                + " UPDATE planta_usuario SET plantausu_activo = 0 WHERE  (plantausu_idusuario = " + idUsu + ");"
                + " UPDATE planta_usuario SET plantausu_activo = 1 WHERE (plantausu_idplanta IN(" + listaPlan + ")) AND (plantausu_idusuario = " + idUsu + "); "
                + " UPDATE planta_usuario SET plantausu_defecto = 0 WHERE (plantausu_idusuario = " + idUsu + "); "
                + " UPDATE planta_usuario SET plantausu_defecto = 1 WHERE (plantausu_idplanta  = " + defecto + ") AND (plantausu_idusuario = " + idUsu + "); ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT planta_usuario)";
            }
            return mensaje;
        }
        //Inserta las ciudades al comercial
        public String insertarCiudadesComer(String idCom, String listaCiudades)
        {
            String mensaje = "";
            try
            {
                String sql = "MERGE ciudad_representante WITH(HOLDLOCK) as tablaCiudadR "
                + " using (SELECT  ciu_pai_id AS idPais, ciu_id AS idCiudad, " + idCom + " AS repre FROM  ciudad  WHERE  (ciu_id IN (" + listaCiudades + "))) "
                + " as tablaCiudad (idPais, idCiudad, repre) "
                + " on (tablaCiudadR.cr_ciu_id  = tablaCiudad.idCiudad) AND (tablaCiudadR.cr_pai_id = tablaCiudad.idPais) AND(tablaCiudadR.cr_rc_id = tablaCiudad.repre) "
                + " when matched then "
                + " update "
                + " set tablaCiudadR.cr_activo = 1 "
                + " when not matched then "
                + " insert ( cr_pai_id, cr_ciu_id, cr_rc_id, cr_activo) "
                + " values ( tablaCiudad.idPais, tablaCiudad.idCiudad, tablaCiudad.repre, 1); "
                + " UPDATE ciudad_representante SET cr_activo = 0 WHERE  (cr_ciu_id  NOT IN (" + listaCiudades + ")) AND (cr_rc_id = " + idCom + ");";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT ciudad_representante)";
            }
            return mensaje;
        }
        //Inserta los paises al comercial
        public String insertarPaisesComer(String idCom, String listaPais)
        {
            String mensaje = "";
            try
            {
                String sql = "MERGE pais_representante WITH(HOLDLOCK) as tablaPaisR "
                + " using (SELECT    pai_id AS idPais, " + idCom + " AS repre FROM  pais  WHERE  (pai_id IN (" + listaPais + "))) "
                + " as tablaPais (idPais, repre) "
                + " on  (tablaPaisR.pr_id_pais = tablaPais.idPais) AND(tablaPaisR.pr_id_representante = tablaPais.repre) "
                + " when matched then "
                + " update "
                + " set tablaPaisR.pr_activo = 1 "
                + " when not matched then "
                + " insert (pr_id_pais,  pr_id_representante, pr_activo) "
                + " values ( tablaPais.idPais, tablaPais.repre, 1); "
                + " UPDATE pais_representante SET pr_activo = 0 WHERE  (pr_id_pais  NOT IN (" + listaPais + ")) AND (pr_id_representante = " + idCom + ");";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT pais_representante)";
            }
            return mensaje;
        }
        //Anula todas las ciudades al comercial si no ha escogido ninguna ciudad o si ha quitados todos
        public String anulaTodosCiuComer(String idCom)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE ciudad_representante SET cr_activo = 0 WHERE (cr_rc_id = " + idCom + ");";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE ciudad_representante)";
            }
            return mensaje;
        }
        //Inserta el comercial
        public String insertaComer(String idUsu, String nombre, String email, Boolean director, String oficina, String direccion, String telefono, String celular, String idPaisOfi)
        {
            String mensaje = "";
            DataTable consulta = null;
            try
            {
                String sql = "INSERT INTO representantes_comerciales (rc_id, rc_descripcion, rc_email, rc_activo, rc_usu_siif_id, rc_director_ofic, rc_oficina, rc_direccion_ofic, rc_telef_ofic, rc_celular, rc_pais_oficina)  "
                + " VALUES ((SELECT MAX(rc_id) + 1 AS idRepre FROM representantes_comerciales),'" + nombre + "','" + email + "', 1, " + idUsu + ",'" + director + "','" + oficina + "','" + direccion + "','" + telefono + "','" + celular + "'," + idPaisOfi + ")"
                + " SELECT  MAX(representantes_comerciales.rc_id) AS idComer FROM representantes_comerciales ";
                consulta = BdDatos.CargarTabla(sql);
                foreach (DataRow row in consulta.Rows)
                {
                    mensaje = row["idComer"].ToString();
                }
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT representantes_comerciales)";
            }
            return mensaje;
        }
        //Editar el comeciarl
        public String editarComer(String idUsu, String nombre, String email, Boolean activo, Boolean director, String oficina, String direccion, String telefono, String celular, String idPaisOfi)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE representantes_comerciales SET rc_descripcion = '" + nombre + "', rc_email = '" + email + "', rc_activo = '" + activo + "', rc_director_ofic = '" + director + "', rc_oficina = '" + oficina + "', rc_direccion_ofic = '" + direccion + "', rc_telef_ofic = '" + telefono + "', rc_celular = '" + celular + "', rc_pais_oficina = '" + idPaisOfi + "' "
                + " WHERE (rc_usu_siif_id = " + idUsu + ")  ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE representantes_comerciales)";
            }
            return mensaje;
        }
        //Edita los permisos del casino
        public String editarCasino(String usuario, Boolean pedido, Boolean admiCasino, Boolean archPlano)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE empleado SET empleado.emp_autorizado_pedido_cas = '" + pedido + "' FROM empleado  INNER JOIN usuario  ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id WHERE (usuario.usu_login = N'" + usuario + "');"
                + " UPDATE usuario SET usuario.usu_admin_casino = '" + admiCasino + "', usuario.usu_archPlano_nmuno = '" + archPlano + "'  FROM usuario  WHERE (usuario.usu_login = N'" + usuario + "'); ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE empleado/usuario)";
            }
            return mensaje;
        }
        //Edita los permisos del SIIF
        public String editarSiif(String usuario, String filtroIdPro, Boolean formaleta, Boolean accesorio)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE empleado SET emp_Apru_Memo = '" + formaleta + "', emp_Apru_Memo_Acc = '" + accesorio + "' " + filtroIdPro + " FROM empleado  INNER JOIN usuario  ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id WHERE (usuario.usu_login = N'" + usuario + "')";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE empleado/usuario)";
            }
            return mensaje;
        }
    }
}