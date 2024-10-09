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
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Web.UI.WebControls;
using System.IO;

namespace CapaControl
{
    public class ControlSIAT
    {
        /*******/
        //Trae todas las actividades
        public DataTable cargarActividades()
        {
            DataTable consulta = null;
            String sql = "SELECT     CONVERT(VARCHAR(MAX), siat_act_id) + '|' + CONVERT(VARCHAR(MAX), siat_act_pantalla) + '|' + CONVERT(VARCHAR(MAX), siat_act_letra) + '|' + CONVERT(VARCHAR(MAX), siat_act_inven) + '|' + CONVERT(VARCHAR(MAX), siat_act_imple) AS idPanLetraActTipo, "
            + " siat_act_nombre AS nomAct, siat_act_id AS idAct, siat_act_letra AS letra, siat_act_color AS color, siat_act_inven AS inv, siat_act_imple AS imp "
            + " FROM  siat_actividad WHERE   (siat_act_activo = 1)";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Trae todos los tecnicos
        public DataTable cargarTecnicos(String filtroTec)
        {
            DataTable consulta = null;
            String sql = "SELECT empleado.emp_usu_num_id AS cedulaEmp, empleado.emp_nombre + ' ' + empleado.emp_apellidos AS nomEmpComp, empleado.emp_nombre AS nomEmp, empleado.emp_apellidos AS apeEmp, empleado.emp_telefono_movil AS celEmp,  empleado.emp_correo_electronico  AS correoEmp  FROM     empleado INNER JOIN"
            + " usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id  WHERE  (empleado.emp_activo = 1) AND (usuario.usu_activo = 1) AND (usuario.usu_rap_id = 16) " + filtroTec + " ORDER BY empleado.emp_nombre + ' ' + empleado.emp_apellidos";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Trae todos los clientes
        public DataTable cargarClientes(String filtro)
        {
            DataTable consulta = null;
            String sql = " SELECT   cli_id AS idCliente, cli_nombre AS nomCliente  FROM  cliente   WHERE   (cli_nombre LIKE '%" + filtro + "%') AND (cli_activo = 1)";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Trae todas las obras
        public DataTable cargarObras(String filtro)
        {
            DataTable consulta = null;
            String sql = "SELECT    obra.obr_id AS idObra, obra.obr_nombre AS nomObra, CONVERT(VARCHAR, obra.obr_id) + '|' + obra.obr_nombre + ' - ' + cliente.cli_nombre + ' - ' + pais.pai_nombre + ' - ' + ciudad.ciu_nombre + ' - ' + obra.obr_direccion AS datosIdObra, "
             + " obra.obr_nombre + ' - ' + cliente.cli_nombre + ' - ' + pais.pai_nombre + ' - ' + ciudad.ciu_nombre + ' - ' + obra.obr_direccion AS datosObra "
             + " FROM   pais INNER JOIN  cliente INNER JOIN  formato_unico ON cliente.cli_id = formato_unico.fup_cli_id INNER JOIN "
             + " obra ON formato_unico.fup_obr_id = obra.obr_id ON pais.pai_id = obra.obr_pai_id INNER JOIN  "
             + " ciudad ON obra.obr_ciu_id = ciudad.ciu_id  INNER JOIN   Orden ON formato_unico.fup_id = Orden.Yale_Cotiza WHERE " + filtro + " "
             + " GROUP BY obra.obr_id, obra.obr_nombre, pais.pai_nombre, ciudad.ciu_nombre, obra.obr_direccion, cliente.cli_nombre ORDER BY nomObra";

            try { consulta = BdDatos.CargarTabla(sql); }
            catch { consulta = new DataTable(); }
            return consulta;
        }

        //Trae todas las OF/ordenes
        public DataTable cargarOf(String filtroIdObra, String filtroIdOF)
        {
            DataTable consulta = null;
            String sql = "SELECT    Orden.Id_Ofa AS idOF, Orden.Numero + '-' + Orden.ano AS nomOF FROM   formato_unico INNER JOIN "
            + " obra ON formato_unico.fup_obr_id = obra.obr_id INNER JOIN  Orden ON formato_unico.fup_id = Orden.Yale_Cotiza "
            + " WHERE (Orden.letra = '1') " + filtroIdObra + filtroIdOF + " ";
            try { consulta = BdDatos.CargarTabla(sql); }
            catch { consulta = new DataTable(); }
            return consulta;
        }

        public DataTable Obtener_OF_Garantia(string OG)
        {
            string sql = " SELECT  Orden.Id_Ofa AS idOF, Orden.Numero + '-' + Orden.ano AS OG, Orden_1.Numero + '-' + Orden_1.ano AS OF_ ,Orden_1.Tipo_Of  " +
                             " FROM obra INNER JOIN formato_unico INNER JOIN Orden ON formato_unico.fup_id = Orden.Yale_Cotiza " +
                               " ON obra.obr_id = formato_unico.fup_obr_id INNER JOIN Orden AS Orden_1 ON Orden.Id_Of_P = Orden_1.Id_Ofa " +
                             " WHERE(Orden.Tipo_Of = 'OG') AND(orden.Ofa like '"+OG+"')" +
                             " GROUP BY formato_unico.fup_id, Orden.Numero, Orden.ano, Orden.Tipo_Of, obra.obr_nombre, " +
                                      " formato_unico.fup_fecha_creacion, Orden.fecha_ingenieria, Orden.M2_Reales, Orden_1.Numero, " +
                             " Orden_1.ano, formato_unico.fup_cli_id ,Orden_1.Tipo_Of,Orden.Id_Ofa";
            return BdDatos.CargarTabla(sql);
        }

        //Trae todas las OG/ordenes de garantias
        public DataTable cargarOG(String filtroIdcliente)
        {
            DataTable consulta = null;
            String sql = "SELECT   Orden.Id_Ofa AS idOF,Orden.Numero + '-' + Orden.ano AS OG, Orden_1.Numero + '-' + Orden_1.ano AS OF_ " +
                         " FROM obra INNER JOIN formato_unico INNER JOIN Orden ON formato_unico.fup_id = Orden.Yale_Cotiza ON" +
                         " obra.obr_id = formato_unico.fup_obr_id INNER JOIN Orden AS Orden_1 ON Orden.Id_Of_P = Orden_1.Id_Ofa " +
                         " INNER JOIN cliente ON formato_unico.fup_cli_id = cliente.cli_id " +
                         " WHERE(Orden.Tipo_Of = 'OG') AND ( " + filtroIdcliente + ")" +
                         " GROUP BY formato_unico.fup_id, Orden.Numero, Orden.ano, Orden.Tipo_Of, obra.obr_nombre," +
                                  " formato_unico.fup_fecha_creacion, Orden.fecha_ingenieria, Orden.M2_Reales, Orden_1.Numero, Orden_1.ano," +
                                  " formato_unico.fup_cli_id, Orden.Id_Ofa ";                      
            try { consulta = BdDatos.CargarTabla(sql); }
            catch { consulta = new DataTable(); }
            return consulta;
        }
    
        //Carga los contactos del cliente
        public DataTable cargarContactos(String filtroIdCliente, String filtroIdCont)
        {
            DataTable consulta = null;
            String sql = "SELECT     contacto_cliente.ccl_id AS idCont, contacto_cliente.ccl_nombre + ' ' + contacto_cliente.ccl_apellido AS nomCont "
            + " FROM         cliente INNER JOIN  contacto_cliente ON cliente.cli_id = contacto_cliente.ccl_cli_id "
            + " WHERE  (contacto_cliente.ccl_activo = 1) " + filtroIdCliente + filtroIdCont + " ";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Inserta datos viaje
        public String insertarViajeTEC(String usuCrea, String idActi, String idTec, String tel, String hotel, String direc, String fechaIniVia, String fechaFinVia, String fechaIniObra, String fechaFinObra, String tipoAct, String dTotal, String dReal, String dInv, String dImp, String idInv, String dPend, String observacion, int idCotizacion, int estado)
        {
            String mensaje = "";
            DataTable consulta = null;
            try
            {
                String sql = "DECLARE @ultimoId INT;"
                + " INSERT INTO siat_viaje (siat_via_usu_plan, siat_via_act_id, siat_via_tec_id, siat_estado_viaje_id, siat_via_tel, siat_via_hotel, siat_via_direccion, siat_via_fechaInicio, siat_via_fechaFin,"
                + " siat_via_fecha_plan, siat_via_obra_fechaInicio, siat_via_obra_fechaFin, siat_via_cargo, siat_via_aproba_comp, siat_via_tipoAct, siat_via_dTotal, siat_via_dReal, siat_via_dInv, siat_via_dImp, siat_via_idInv, siat_via_dpend, siat_via_observacion, siat_cotizacion_id) "
                + " VALUES ('" + usuCrea + "'," + idActi + "," + idTec + ", " + estado + "," + tel + ",'" + hotel + "','" + direc + "','" + fechaIniVia + "','" + fechaFinVia + "', SYSDATETIME(),'" + fechaIniObra + "','" + fechaFinObra + "',"
                + " 'Tecnico', 0, '" + tipoAct + "', " + dTotal + ", " + dReal + ", " + dInv + ", " + dImp + ", " + idInv + ", " + dPend + ", '" + observacion + "' , " + idCotizacion + ");"
                + " SET @ultimoId = SCOPE_IDENTITY(); SELECT @ultimoId AS id;";
                consulta = BdDatos.CargarTabla(sql);
                foreach (DataRow row in consulta.Rows)
                {
                    mensaje = row["id"].ToString();
                }
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT TEC siat_viaje)";
            }
            return mensaje;
        }
        //Inserta o edita las ofs con la viaje/visita
        public String insertarOFs(String idVia, String listaOFs)
        {
            String mensaje = "";
            try
            {
                String sql = "MERGE siat_of_viaje WITH(HOLDLOCK) AS tablaOfViaje "
                + " USING (SELECT    Id_Ofa  AS idOfa, " + idVia + " AS idVisita FROM  orden  WHERE  (orden.Id_Ofa IN (" + listaOFs + ")))  "
                + " AS tablaPais (idOfa, idVisita)  "
                + " ON  (tablaOfViaje.siat_of_viaje_id_ofa = tablaPais.idOfa) AND (tablaOfViaje.siat_of_viaje_via_id = tablaPais.idVisita)  "
                + " WHEN matched THEN  "
                + " UPDATE  "
                + " SET tablaOfViaje.siat_of_viaje_activo = 1 "
                + " WHEN not matched THEN  "
                + " INSERT (siat_of_viaje_id_ofa,  siat_of_viaje_via_id, siat_of_viaje_activo)  "
                + " VALUES ( tablaPais.idOfa, tablaPais.idVisita, 1);  "
                + " UPDATE siat_of_viaje SET siat_of_viaje_activo = 0 WHERE  (siat_of_viaje_id_ofa  NOT IN (" + listaOFs + ")) AND (siat_of_viaje_via_id =  " + idVia + " );";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT siat_of_viaje)";
            }
            return mensaje;
        }
        //Editar datos viaje
        public String editarViajeTEC(String usuMod, String tel, String hotel, String direc, String fechaIniObra, String fechaFinObra, String idViaje, String filtroCampos)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE siat_viaje SET siat_via_usu_mod = '" + usuMod + "', siat_via_fechaMod = SYSDATETIME(), siat_via_tel = " + tel + ", siat_via_hotel = '" + hotel + "', siat_via_direccion = '" + direc + "' "
                + " , siat_via_obra_fechaInicio = '" + fechaIniObra + "', siat_via_obra_fechaFin = '" + fechaFinObra + "' " + filtroCampos + "  WHERE siat_via_id = " + idViaje + " ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE TEC siat_viaje)";
            }
            return mensaje;
        }
        //Editar estado del viaje TECNICO
        public String editarEstadoV(int estado, String idViaje, String filtro)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE siat_viaje SET siat_estado_viaje_id = " + estado + " " + filtro + " WHERE siat_via_id = " + idViaje + " ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE estado siat_viaje)";
            }
            return mensaje;
        }
        //Crear datos para Encuesta postVisita
        public String CrearEncuestaPostVisita(int idFup, string fVersion, String idViaje, string ContactoId, String Contacto, int TipoEncuesta)
        {
            String mensaje = "";
            try
            {
                String sql = "EXECUTE USP_fup_UPD_EnviaEncuestas " + idFup + ", '" + fVersion + "', " + ContactoId + ", " + idViaje + ", " + TipoEncuesta + "";
                
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (USP_fup_UPD_EnviaEncuestas)";
            }
            return mensaje;
        }
        //Buscar visita por medio de la ORDEN
        public DataTable filtrarOF(String filtroOrden)
        {
            DataTable consulta = null;
            String sql = "SELECT   siat_of_viaje.siat_of_viaje_id AS idOfaxV, Orden.Id_Ofa AS idOf, siat_viaje.siat_via_id  AS idViaje, CONVERT(CHAR(10), siat_viaje.siat_via_fechaInicio, 103) AS fechaInicio, CONVERT(CHAR(10), siat_viaje.siat_via_fechaFin, 103) AS fechaFin, Orden.Numero + '-' + Orden.ano AS orden, "
                  + " empleado.emp_nombre + ' ' + empleado.emp_apellidos AS tecnico, cliente.cli_nombre AS cliente, obra.obr_nombre + ' - ' + pais.pai_nombre + ' - ' + ciudad.ciu_nombre + ' - ' + obra.obr_direccion AS datosObra, siat_viaje.siat_cotizacion_id "
                  + " FROM   formato_unico INNER JOIN  Orden ON formato_unico.fup_id = Orden.Yale_Cotiza INNER JOIN  siat_of_viaje ON Orden.Id_Ofa = siat_of_viaje.siat_of_viaje_id_ofa INNER JOIN "
                  + " cliente ON cliente.cli_id = formato_unico.fup_cli_id INNER JOIN obra ON formato_unico.fup_obr_id = obra.obr_id INNER JOIN "
                  + " ciudad ON obra.obr_ciu_id = ciudad.ciu_id INNER JOIN pais ON obra.obr_pai_id = pais.pai_id INNER JOIN "
                  + " siat_viaje ON siat_of_viaje.siat_of_viaje_via_id = siat_viaje.siat_via_id INNER JOIN  empleado ON siat_viaje.siat_via_tec_id = empleado.emp_usu_num_id  INNER JOIN "
                  + " siat_actividad ON siat_actividad.siat_act_id = siat_viaje.siat_via_act_id "
                  + " " + filtroOrden + "  ORDER BY idViaje DESC";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Inserta otra actividad
        public String insertarAct(String usuCrea, String idActi, String idTec, String fechaIniVia, String fechaFinVia, String obser)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO  siat_mov_actividad (siat_mov_activ_usu_crea, siat_mov_activ_fechaCrea, siat_mov_activ_act_id, siat_mov_activ_tec_id, siat_mov_activ_fechaInicio, siat_mov_activ_fechaFinal, siat_mov_activ_obser) "
                + "VALUES ('" + usuCrea + "', SYSDATETIME(), " + idActi + "," + idTec + ", '" + fechaIniVia + "', '" + fechaFinVia + "', '" + obser + "') ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT siat_mov_actividad)";
            }
            return mensaje;
        }
        //Los clientes asociados por OF
        public int contarClientesOF(String listaOfs)
        {
            DataTable consulta = null;
            String sql = "SELECT   cliente.cli_id AS clientes  FROM  cliente INNER JOIN  formato_unico ON cliente.cli_id = formato_unico.fup_cli_id INNER JOIN "
            + " Orden ON formato_unico.fup_id = Orden.Yale_Cotiza  WHERE   (Orden.Id_Ofa IN (" + listaOfs + ")) GROUP BY cliente.cli_id";
            consulta = BdDatos.CargarTabla(sql);
            return consulta.Rows.Count;
        }
        //Los clientes asociados por OBRA
        public DataTable contarClientesOBRA(String listaObra)
        {
            DataTable consulta = null;
            String sql = "SELECT  cliente.cli_id AS cliente FROM  obra INNER JOIN  cliente ON obra.obr_cli_id = cliente.cli_id WHERE (obra.obr_id IN (" + listaObra + ")) GROUP BY cliente.cli_id";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Trae datos del viaje tecnico
        public DataTable cargarViajeTEC(String idViaje)
        {
            DataTable consulta = null;
            String sql = " SELECT  sv.siat_via_act_id AS idAct,  "
            + " (CASE WHEN  sv.siat_via_tipoAct = 'inv' AND sv.siat_estado_viaje_id = 3 THEN  "
            + " (SELECT  CONVERT(VARCHAR(MAX), sa.siat_act_id) + '|' + CONVERT(VARCHAR(MAX), sa.siat_act_pantalla) + '|' + sa.siat_act_letra + '|' + CONVERT(VARCHAR(MAX), siat_act_inven) + '|' + CONVERT(VARCHAR(MAX), siat_act_imple) FROM siat_actividad sa WHERE siat_act_id = 7) "
            + " ELSE CONVERT(VARCHAR(MAX), sa.siat_act_id) + '|' + CONVERT(VARCHAR(MAX), sa.siat_act_pantalla) + '|' + sa.siat_act_letra + '|' + CONVERT(VARCHAR(MAX), siat_act_inven) + '|' + CONVERT(VARCHAR(MAX), siat_act_imple)  END) AS idPanLetraAct, "
            + " sv.siat_via_tec_id AS idTec, (CASE WHEN  sv.siat_via_tipoAct = 'inv' AND sv.siat_estado_viaje_id = 3 THEN 1 ELSE sv.siat_estado_viaje_id END) AS estado, (CASE WHEN sv.siat_via_tel IS NULL THEN '' ELSE sv.siat_via_tel END) AS tel, sv.siat_via_hotel AS hotel, "
            + " sv.siat_via_direccion AS direc, CONVERT(CHAR(10), sv.siat_via_fechaInicio, 103) AS fechaIni, CONVERT(CHAR(10), sv.siat_via_fechaFin, 103) AS fechaFin,  "
            + " CONVERT(CHAR(10), sv.siat_via_obra_fechaInicio, 103) AS fechaObraIni, CONVERT(CHAR(10), sv.siat_via_obra_fechaFin, 103) AS fechaObraFin,  "
            + " (SELECT (STUFF((SELECT ', ' + CONVERT(VARCHAR, sov.siat_of_viaje_id_ofa) FROM  siat_of_viaje sov  WHERE  (sov.siat_of_viaje_via_id = " + idViaje + ") AND (sov.siat_of_viaje_activo = 1) FOR XML PATH('')), 1, 1, ''))) AS ordenes, "
            + " (CASE WHEN  sv.siat_via_tipoAct = 'inv' AND sv.siat_estado_viaje_id = 3 THEN 'imp' ELSE sv.siat_via_tipoAct END) AS tipoAlt, isnull(sv.siat_via_dTotal, 0) AS dTotal, isnull(sv.siat_via_dReal, 0) AS dReal, isnull(sv.siat_via_dInv, 0) AS dInv, isnull(sv.siat_via_dImp, 0) AS dImp, sv.siat_via_tipoAct AS tipo, isnull(sv.siat_via_dpend, 0) AS dPend, sv.siat_via_observacion as observacion, sv.siat_cotizacion_id as cotizacion, siat_estado_viaje.descripcion, siat_cotizacion.consecutivo, siat_cotizacion.siat_cotizacion_id, isnull(siat_cotizacion.cotizacion,0) as bitCotizacion, siat_cotizacion.dias "
            + " FROM   siat_viaje sv  INNER JOIN siat_actividad sa ON sa.siat_act_id = sv.siat_via_act_id inner join siat_estado_viaje on sv.siat_estado_viaje_id = siat_estado_viaje.siat_estado_viaje_id INNER JOIN siat_cotizacion on siat_cotizacion.siat_cotizacion_id = sv.siat_cotizacion_id"
            + " WHERE (siat_via_id = " + idViaje + ")  ";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Trae datos del viaje ingeniero
        public DataTable cargarViajeING(String idViaje)
        {
            DataTable consulta = null;
            String sql = "SELECT  CONVERT(CHAR(10), siat_via_fechaInicio, 103) AS fechaIni, CONVERT(CHAR(10), siat_via_fechaFin, 103) AS fechaFin, siat_via_even_ing AS evento "
            + " FROM   siat_viaje WHERE (siat_via_id = " + idViaje + ")";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Inserta o edita las ofs con la viaje/visita
        public String insertarObras(String idVia, String listaObra)
        {
            String mensaje = "";
            String hayObras = "";
            if (listaObra != "")
            {
                hayObras = "INSERT INTO  siat_obr_viaje (siat_obr_viaje_via_id, siat_obr_viaje_obr_id, siat_obr_viaje_usos) VALUES  " + listaObra + " ; ";
            }
            try
            {
                String sql = "DELETE FROM  siat_obr_viaje WHERE siat_obr_viaje_via_id = " + idVia + "; " + hayObras + "";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT siat_obr_viaje)";
            }
            return mensaje;
        }
        //Inserta los contactos
        public void insertarCont(String idVia, String conta)
        {
            String hayConta = "";
            if (conta != "")
            {
                hayConta = " INSERT INTO siat_cont_viaje (siat_cont_viaje_via_id, siat_cont_viaje_cont_id, siat_cont_viaje_cli_id) "
                + " SELECT " + idVia + " AS idVia, ccl_id AS conta, ccl_cli_id AS cliente FROM contacto_cliente WHERE (ccl_id IN (" + conta + ")); ";
            }
            try
            {
                String sql = "DELETE FROM siat_cont_viaje WHERE siat_cont_viaje_via_id = " + idVia + "; " + hayConta + " ";
                BdDatos.Actualizar(sql);
            }
            catch
            {
            }
        }
        //Inserta los contactos
        public void insertarCiu(String idVia, String ciudades)
        {
            String hayCiu = "";
            if (ciudades != "")
            {
                hayCiu = "INSERT INTO siat_ciu_viaje  (siat_ciu_viaje_via_id, siat_ciu_viaje_ciu_id) "
                + " SELECT " + idVia + " AS idVia, ciu_id AS ciu  FROM ciudad WHERE (ciu_id IN (" + ciudades + ")); ";
            }
            try
            {
                String sql = "DELETE FROM siat_ciu_viaje WHERE siat_ciu_viaje_via_id = " + idVia + "; " + hayCiu + " ";
                BdDatos.Actualizar(sql);
            }
            catch
            {
            }
        }
        //Carga los contactos del cliente
        public DataTable cargarContaVia(String idViaje)
        {
            DataTable consulta = null;
            String sql = "SELECT     CONVERT(VARCHAR, siat_cont_viaje.siat_cont_viaje_cli_id) + '|' + CONVERT(VARCHAR, siat_cont_viaje.siat_cont_viaje_cont_id) AS idClienCont, "
            + " contacto_cliente.ccl_nombre + ' ' + contacto_cliente.ccl_apellido AS nomCont FROM  siat_cont_viaje INNER JOIN "
            + " contacto_cliente ON siat_cont_viaje.siat_cont_viaje_cont_id = contacto_cliente.ccl_id  WHERE (siat_cont_viaje.siat_cont_viaje_via_id = " + idViaje + ")";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Trae todos las novedades
        public DataTable cargarNovedades(String filtroNov)
        {
            DataTable consulta = null;
            String sql = "SELECT    siat_nov_id AS idNov, siat_nov_nombre + ' (' + siat_nov_tipo + ')' AS nomNov, CONVERT(VARCHAR, siat_nov_corre) + '|' + CONVERT(VARCHAR, siat_nov_id) AS idCorNov "
            + " FROM   siat_novedades  WHERE  (siat_nov_activo = 1) " + filtroNov + "";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Trae todas las OF/ordenes
        public DataTable cargarOfViajes(String filtroIdVia)
        {
            DataTable consulta = null;
            String sql = "SELECT     siat_of_viaje.siat_of_viaje_id AS idViaOf, Orden.Numero + '-' + Orden.ano AS nomOF  FROM  siat_of_viaje INNER JOIN "
            + " Orden ON siat_of_viaje.siat_of_viaje_id_ofa = Orden.Id_Ofa WHERE (siat_of_viaje.siat_of_viaje_activo = 1) " + filtroIdVia + "";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga los datos del viaje
        public DataTable llenarInfoViaje(String idViaje)
        {
            DataTable consulta = null;
            String sql = " SELECT   siat_noved_of.siat_noved_of_id AS idNovOf, empleado.emp_nombre + ' ' + empleado.emp_apellidos AS nomTec, ciudad.ciu_nombre AS ciudad, pais.pai_nombre AS pais, "
                      + " cliente.cli_nombre AS cliente, obra.obr_nombre AS obra, Orden.Numero + '-' + Orden.ano AS nomOF, siat_noved_of.siat_noved_of_obser AS obser, "
                      + " siat_novedades.siat_nov_nombre + '(' + siat_novedades.siat_nov_tipo + ')' AS nomNov, siat_novedades.siat_nov_id AS idNov, mensaje.men_mensaje AS titulo "
                      + " FROM   obra INNER JOIN  formato_unico ON obra.obr_id = formato_unico.fup_obr_id INNER JOIN  cliente ON formato_unico.fup_cli_id = cliente.cli_id INNER JOIN "
                      + " ciudad ON obra.obr_ciu_id = ciudad.ciu_id INNER JOIN  pais ON obra.obr_pai_id = pais.pai_id INNER JOIN  siat_viaje INNER JOIN "
                      + " siat_of_viaje ON siat_viaje.siat_via_id = siat_of_viaje.siat_of_viaje_via_id INNER JOIN "
                      + " Orden ON siat_of_viaje.siat_of_viaje_id_ofa = Orden.Id_Ofa ON formato_unico.fup_id = Orden.Yale_Cotiza INNER JOIN "
                      + " empleado ON siat_viaje.siat_via_tec_id = empleado.emp_usu_num_id INNER JOIN "
                      + " siat_noved_of ON siat_of_viaje.siat_of_viaje_id = siat_noved_of.siat_noved_of_viaje_id INNER JOIN "
                      + " siat_novedades ON siat_noved_of.siat_noved_of_nov_id = siat_novedades.siat_nov_id INNER JOIN  mensaje ON siat_novedades.siat_nov_men = mensaje.men_id "
                      + " WHERE   (siat_viaje.siat_via_id = " + idViaje + ") AND (siat_novedades.siat_nov_corre = 1) AND (siat_noved_of.siat_noved_of_correo = 1)";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga los correos para novedades
        public DataTable llenarCorreosNov(String idNov)
        {
            DataTable consulta = null;
            String sql = " SELECT   empleado.emp_correo_electronico AS correo "
                      + " FROM    mail_proceso INNER JOIN "
                      + " mensaje ON mail_proceso.mail_proc_mensaje_id = mensaje.men_id INNER JOIN "
                      + " siat_novedades ON mensaje.men_id = siat_novedades.siat_nov_men INNER JOIN "
                      + " empleado ON mail_proceso.mail_proc_empleado_id = empleado.emp_usu_num_id "
                      + " WHERE   (siat_novedades.siat_nov_activo = 1) AND (siat_novedades.siat_nov_id = " + idNov + ") ";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga los correos generales
        public DataTable llenarCorreosComp(String idComp)
        {
            DataTable consulta = null;
            String sql = " SELECT   empleado.emp_correo_electronico AS correo, mensaje.men_asunto AS asunto"
                     + " FROM    mail_proceso INNER JOIN "
                     + " mensaje ON mail_proceso.mail_proc_mensaje_id = mensaje.men_id INNER JOIN "
                     + " siat_compensatorio ON mensaje.men_id = siat_compensatorio.siat_comp_men INNER JOIN "
                     + " empleado ON mail_proceso.mail_proc_empleado_id = empleado.emp_usu_num_id "
                     + " WHERE   (siat_compensatorio.siat_comp_activo = 1) AND (siat_compensatorio.siat_comp_id = " + idComp + ") ";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Se construye el cuerpo en HTML por completo para el correo de agendamiento
        private String cuerpoCorreoVia(String tec, String pais, String ciu, String cli, String obra, String of, String obs, String nov)
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
            + " El técnico <strong>" + tec.ToUpper() + "</strong> el cual se encuentra de viaje en <strong>" + pais.ToUpper() + "-" + ciu.ToUpper() + "</strong> notifico una novedad tipo <strong>" + nov.ToUpper() + "</strong>:"
            + " <br /> "
            + " <br /> "
            + " CLIENTE : <strong> " + cli.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " OBRA : <strong> " + obra.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " OF : <strong> " + of.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " OBSERVACIÓN : <strong> " + obs.ToUpper() + " </strong> "
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
        //Arma el correo
        public Boolean correoSiatNov(String idViaje)
        {
            Boolean confi = true;
            String cuerpo = "";
            String correos = "";
            String asunto = "";
            String titulo = "";
            DataTable datos = llenarInfoViaje(idViaje);
            foreach (DataRow row in datos.Rows)
            {
                cuerpo = cuerpoCorreoVia(row["nomTec"].ToString(), row["pais"].ToString(), row["ciudad"].ToString(), row["cliente"].ToString(), row["obra"].ToString(), row["nomOF"].ToString(), row["obser"].ToString(), row["nomNov"].ToString());
                asunto = "NOTIFICACION DE " + row["nomNov"].ToString().ToUpper() + " EN LA OF " + row["nomOF"].ToString() + "";
                titulo = row["titulo"].ToString();
                DataTable tbCorreos = llenarCorreosNov(row["idNov"].ToString());
                foreach (DataRow row0 in tbCorreos.Rows)
                {
                    correos = correos + row0["correo"].ToString() + ",";
                }
                try { crearCorreo(cuerpo, asunto, correos + "<", titulo); }
                catch { confi = false; }
            }
            return confi;
        }
        //Este metodo crea el correo SIN archivo adjunto y con un metodo donde se crea el cuerpo HTML
        public void crearCorreo(String cuerpo, String asunto, String correos, String titulo)
        {
            String correoF = "";
            String contraF = "";
            String host = "";
            DataTable correForsa = cosultarCorreoForsa();
            foreach (DataRow row2 in correForsa.Rows)
            {
                correoF = row2["correo"].ToString();
                contraF = row2["contra"].ToString();
                host = row2["host"].ToString();
            }
            //Configuración del Mensaje
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient();//forsa : smtp.office365.com // gmail : smtp.gmail.com
            //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
            mail.From = new MailAddress(correoF, titulo, Encoding.UTF8);
            //Aquí ponemos el asunto del correo
            mail.Subject = asunto;
            //Aquí ponemos el mensaje que incluirá el correo
            mail.Body = cuerpo;
            mail.IsBodyHtml = true;
            //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
            //mail.To.Add((correos.Replace(",<", " ")));
            mail.To.Add(correos.Replace(",<", ""));
            //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran
            // mail.Attachments.Add(new Attachment(@"C:\Documentos\carta.docx"));
            //Configuracion del SMTP
            SmtpServer.Host = host;
            //Especificamos las credenciales con las que enviaremos el mail
            SmtpServer.Credentials = new System.Net.NetworkCredential(correoF, contraF);
            SmtpServer.Port = 25; //Puerto que utiliza Gmail para sus servicios // gmail : 587 // forsa : 25
            SmtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                string mensaje = "ERROR: " + ex.Message;
            }
        }
        //Consulta el correo forsa
        public DataTable cosultarCorreoForsa()
        {
            DataTable consulta = null;
            String sql = "SELECT TOP(1) par_correo AS correo, par_correo_contra AS contra, par_host AS host FROM  Parametros";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Inserta las novedades
        public String insertarNov(String idVia, String listaNov)
        {
            String mensaje = "";
            try
            {
                String sql = "DELETE nov FROM siat_noved_of nov  INNER JOIN siat_of_viaje ON siat_of_viaje_id = siat_noved_of_viaje_id "
                + " INNER JOIN siat_viaje ON siat_via_id = siat_of_viaje_via_id WHERE siat_via_id = " + idVia + "; "
                + " INSERT INTO siat_noved_of (siat_noved_of_viaje_id, siat_noved_of_nov_id, siat_noved_of_obser, siat_noved_of_correo, siat_noved_of_fechacrea) VALUES "
                + " " + listaNov + "";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT siat_noved_of)";
            }
            return mensaje;
        }
        //Actualiza el estado del las novedades realcionadas con el id del viaje para mandar el correo
        public void actEstNovCorreo(String idViaje)
        {
            String sql = "UPDATE t SET  t.siat_noved_of_correo = 0 FROM siat_noved_of t INNER JOIN  siat_viaje INNER JOIN siat_of_viaje ON siat_viaje.siat_via_id = siat_of_viaje.siat_of_viaje_via_id ON  "
            + " t.siat_noved_of_viaje_id = siat_of_viaje.siat_of_viaje_id WHERE  (siat_viaje.siat_via_id = " + idViaje + ")";
            BdDatos.Actualizar(sql);
        }
        //Consulta las novedades ya guardadas
        public DataTable consultaNovVia(String idViaje)
        {
            DataTable consulta = null;
            String proc = "SELECT   CONVERT(VARCHAR, siat_noved_of.siat_noved_of_viaje_id)  + '|' + CONVERT(VARCHAR, siat_noved_of.siat_noved_of_nov_id) + '|' + CONVERT(VARCHAR, siat_noved_of.siat_noved_of_correo) + '|' + CONVERT(VARCHAR, (ROW_NUMBER() OVER(ORDER BY  siat_noved_of_id DESC) - 1)) AS idNov, "
                      + " '(' + Orden.Numero + '-' + Orden.ano + ') - ' + siat_novedades.siat_nov_nombre + ' (' + siat_novedades.siat_nov_tipo + ')' + ' :' + siat_noved_of.siat_noved_of_obser AS nomNov "
                      + " FROM   siat_novedades INNER JOIN "
                      + " siat_noved_of ON siat_novedades.siat_nov_id = siat_noved_of.siat_noved_of_nov_id INNER JOIN "
                      + " siat_of_viaje ON siat_noved_of.siat_noved_of_viaje_id = siat_of_viaje.siat_of_viaje_id INNER JOIN "
                      + " siat_viaje ON siat_of_viaje.siat_of_viaje_via_id = siat_viaje.siat_via_id INNER JOIN "
                      + " Orden ON siat_of_viaje.siat_of_viaje_id_ofa = Orden.Id_Ofa "
                      + " WHERE   (siat_viaje.siat_via_id = " + idViaje + ") ";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Inserta los costos
        public String insertarCostosR(String idVia, String hotel, String tiq, String alimen, String trans, String llam, String lavan, String otros, String penal, String trm, String usu, String obs)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO  siat_costos_real (siat_cr_via_id, siat_cr_hotel, siat_cr_tiquete, siat_cr_alimentacion, siat_cr_trans, siat_cr_llamadas, siat_cr_lavanderia, "
                + " siat_cr_otros, siat_cr_usu, siat_cr_fecha, siat_cr_penal, siat_cr_trm, siat_cr_obs) "
                + " VALUES (" + idVia + "," + hotel.Replace(".","") + "," + tiq.Replace(".","") + "," + alimen.Replace(".","") + "," + trans.Replace(".","") + "," 
                + llam.Replace(".", "") + "," + lavan.Replace(".", "") + "," + otros.Replace(".", "") + ", '" + usu + "', SYSDATETIME(), " + penal.Replace(".", "") + ", " + trm.Replace(".", "") + ", '" + obs + "')";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT siat_costos_real)";
            }
            return mensaje;
        }
        //Consulta los costos ya guardadas
        public DataTable consultaCosR(String idViaje)
        {
            DataTable consulta = null;
            String sql = "SELECT  siat_cr_hotel AS hotel, siat_cr_tiquete AS tiq, siat_cr_alimentacion AS ali, siat_cr_trans_interno AS transInt, siat_cr_trans_aero AS transAereo, siat_cr_llamadas AS llam, "
            + " siat_cr_lavanderia AS lav, siat_cr_otros AS otros, siat_cr_penal AS penal, siat_cr_trm AS trm, siat_cr_obs AS obs "
            + " FROM  siat_costos_real WHERE  (siat_cr_via_id = " + idViaje + ")";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Edita los costos
        public String editarCostosR(String idVia, String hotel, String tiq, String alimen, String trans, String llam, String lavan, String otros, String penal, String trm, String usu, String obs)
        {
            String mensaje = "";
            try
            {
                String sql = " UPDATE  siat_costos_real SET siat_cr_hotel = " + hotel.Replace(",", "") + ", siat_cr_tiquete = " + tiq.Replace(",", "") + ", siat_cr_alimentacion = " + alimen.Replace(",", "")
                + ", siat_cr_trans = " + trans.Replace(",","") + ", siat_cr_llamadas = " + llam.Replace(",","") + ", siat_cr_lavanderia = " + lavan.Replace(",","")
                + ", siat_cr_otros = " + otros.Replace(",", "") + ", siat_cr_usu = '" + usu + "', siat_cr_fecha = SYSDATETIME(), siat_cr_penal = " + penal.Replace(",", "") + ", siat_cr_trm = " + trm.Replace(",", "") + ", siat_cr_obs = '" + obs
                + "'  WHERE  (siat_cr_via_id = " + idVia + ")";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE siat_costos_real)";
            }
            return mensaje;
        }
        //Inserta datos viaje INGENIERO
        public String insertarViajeING(String usuCrea, String fechaIni, String fechaFin, String evento)
        {
            String mensaje = "";
            DataTable consulta = null;
            try
            {
                String sql = "DECLARE @ultimoId INT "
                + " INSERT INTO siat_viaje (siat_via_usu_plan, siat_via_fecha_plan, siat_via_cargo, siat_estado_viaje_id, siat_via_fechaInicio, siat_via_fechaFin, siat_via_even_ing) "
                + " VALUES ('" + usuCrea + "', SYSDATETIME(),'Ingeniero',1,'" + fechaIni + "','" + fechaFin + "', '" + evento + "') "
                + " SET @ultimoId = SCOPE_IDENTITY(); SELECT @ultimoId AS id;";
                consulta = BdDatos.CargarTabla(sql);
                foreach (DataRow row in consulta.Rows)
                {
                    mensaje = row["id"].ToString();
                }
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT ING siat_viaje)";
            }
            return mensaje;
        }
        //Editar datos viaje INGENIERO
        public String editarViajeING(String usuMod, String fechaIni, String fechaFin, String idViaje, String evento)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE siat_viaje SET siat_via_usu_mod = '" + usuMod + "', siat_via_fechaMod = SYSDATETIME(), siat_via_fechaInicio = '" + fechaIni + "', siat_via_fechaFin = '" + fechaFin + "', "
                + " siat_via_even_ing = '" + evento + "'  WHERE siat_via_id = " + idViaje + " ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE ING siat_viaje)";
            }
            return mensaje;
        }
        //Buscar visita por medio del tecnico para el compensatorio
        public DataTable datosCompe(String filtro)
        {
            DataTable consulta = null;
            String sql = "SELECT     siat_viaje.siat_via_id AS idViaje, CONVERT(CHAR(10), siat_viaje.siat_via_fechaInicio, 103) AS fechaInicio, CONVERT(CHAR(10), "
                      + " siat_viaje.siat_via_fechaFin, 103) AS fechaFin, pais.pai_nombre AS pais, ciudad.ciu_nombre AS ciudad, "
                      + " empleado.emp_nombre + ' ' + empleado.emp_apellidos AS nomEmp, cliente.cli_nombre AS cliente, siat_viaje.siat_via_dias_comp AS diasComp, "
                      + " siat_viaje.siat_via_porce_comp AS porceComp, DATEDIFF(day, siat_viaje.siat_via_fechaInicio, siat_viaje.siat_via_fechaFin) AS diasViaje "
                      + " FROM   obra INNER JOIN formato_unico ON obra.obr_id = formato_unico.fup_obr_id INNER JOIN "
                      + " siat_viaje INNER JOIN  siat_of_viaje ON siat_viaje.siat_via_id = siat_of_viaje.siat_of_viaje_via_id INNER JOIN "
                      + " Orden ON siat_of_viaje.siat_of_viaje_id_ofa = Orden.Id_Ofa ON formato_unico.fup_id = Orden.Yale_Cotiza INNER JOIN "
                      + " pais ON obra.obr_pai_id = pais.pai_id INNER JOIN  ciudad ON obra.obr_ciu_id = ciudad.ciu_id INNER JOIN "
                      + " empleado ON siat_viaje.siat_via_tec_id = empleado.emp_usu_num_id INNER JOIN cliente ON formato_unico.fup_cli_id = cliente.cli_id "
                      + " WHERE     (siat_viaje.siat_estado_viaje_id = 3) AND (siat_viaje.siat_via_aproba_comp <> 1) " + filtro + " "
                      + " GROUP BY siat_viaje.siat_via_id, siat_viaje.siat_via_fechaInicio, siat_viaje.siat_via_fechaFin, pais.pai_nombre, ciudad.ciu_nombre, empleado.emp_nombre, "
                      + " empleado.emp_apellidos, cliente.cli_nombre, siat_viaje.siat_via_dias_comp, siat_viaje.siat_via_porce_comp"
                      + " HAVING  DATEDIFF(day, siat_viaje.siat_via_fechaInicio, siat_viaje.siat_via_fechaFin) > 30";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //carga los datos del compensatorio
        public DataTable cargaCompe(String dia)
        {
            DataTable consulta = null;
            String sql = "SELECT     TOP (1) siat_comp_id AS idComp, siat_dias_inicio AS dIni, siat_dias_fin AS dFin, siat_porcentaje AS porce "
            + " FROM         siat_compensatorio  WHERE     (" + dia + " > siat_dias_inicio) AND (" + dia + " <= siat_dias_fin) AND (siat_comp_activo = 1)";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //carga los datos del compensatorio
        public DataTable cargaDatosUsu(String usu)
        {
            DataTable consulta = null;
            String sql = " SELECT   empleado.emp_nombre_mayusculas + ' ' + empleado.emp_apellidos_mayusculas AS nomEmp, empleado.emp_correo_electronico AS correo "
            + " FROM    empleado INNER JOIN  usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id  WHERE  (usuario.usu_login = '" + usu + "')";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Se construye el cuerpo en HTML por completo para el correo de agendamiento
        private String cuerpoCorreoComp(String usuApro, String tec, String cli, String pais, String fechaIni, String fechaFin, String diasV, String porce, String diasC)
        {
            String cuerpo = "";
            cuerpo = "<html> "
            + " <head> "
            + " <title></title> "
            + " </head> "
            + " <body style='width: 740px; height: 264px'> "
            + " <br /> "
            + " Aprobada por : <strong>" + usuApro.ToUpper() + " </strong>"
            + " Para: Gestión Humana."
            + " <br /> "
            + " <br /> "
            + " De la manera más atenta y teniendo en cuenta las políticas aprobadas para los técnicos en misión, vigentes a partir del 01 de Julio de 2009, le adjunto la información correspondiente del señor <strong>" + tec.ToUpper() + "</strong>"
            + " <br /> "
            + " <br /> "
            + " CLIENTE : <strong> " + cli.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " PAIS : <strong> " + pais.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " FECHA DE SALIDA : <strong> " + fechaIni + " </strong> "
            + " <br /> "
            + " <br /> "
            + " FECHA DE REGRESO : <strong> " + fechaFin + " </strong> "
            + " <br /> "
            + " <br /> "
            + " TOTAL TIEMPO : <strong> " + diasV.ToUpper() + " </strong> DIAS"
            + " <br /> "
            + " <br /> "
            + " PORCENTAJE SALARIO : <strong> " + porce.ToUpper() + " </strong> "
            + " <br /> "
            + " <br /> "
            + " DIAS COMPENSADOS : <strong> " + diasC.ToUpper() + " </strong> "
             + " <br /> "
            + " <br /> "
            + " <br /> "
            + " <br /> "
            + " Muchas gracias por su colaboración. "
            + " <br /> "
            + " <br /> "
            + " </body> "
            + " </html>";
            return cuerpo;
        }
        //Arma el correo
        public Boolean correoSiatComp(String usuApro, String tec, String cli, String pais, String fechaIni, String fechaFin, String diasV, String porce, String diasC, String idComp)
        {
            Boolean confi = false;
            String cuerpo = "";
            String correos = "";
            String aprobador = "";
            String asunto = "";
            DataTable tbUsu = cargaDatosUsu(usuApro);
            foreach (DataRow row in tbUsu.Rows)
            {
                aprobador = row["nomEmp"].ToString();
            }
            cuerpo = cuerpoCorreoComp(aprobador, tec, cli, pais, fechaIni, fechaFin, diasV, porce, diasC);
            DataTable tbCorreos = llenarCorreosComp(idComp);
            foreach (DataRow row0 in tbCorreos.Rows)
            {
                correos = correos + row0["correo"].ToString() + ",";
                asunto = row0["asunto"].ToString();
            }
            try
            {
                crearCorreo(cuerpo, asunto, correos + "<", asunto);
                confi = true;
            }
            catch { confi = false; }
            return confi;
        }
        //Carga las zonas
        public DataTable cargarZonas()
        {
            String sql = "SELECT zonap_id AS idZona, zonap_nom AS nombre FROM  zonaPais  WHERE  (zonap_activo = 1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga los paises
        public DataTable cargarPais(String idZona)
        {
            DataTable consulta = null;
            String sql = "SELECT  pai_id AS idPais, pai_nombre AS nomPais FROM   pais WHERE  (pai_zona_id = " + idZona + ") ORDER BY nomPais";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga las zonas de las ciudades del pais
        public DataTable cargarZonasC(String idPais)
        {
            DataTable consulta = null;
            String sql = "SELECT  zonac_id AS idZonC, zonac_nom AS nomZonC, zonac_activo AS actZonC, zonac_pais AS paisZonC  FROM  zonaCiudad  WHERE  (zonac_pais = " + idPais + ") AND (zonac_activo = 1) ORDER BY nomZonC";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        } 
        //Carga los costos
        public DataTable consultaCosP(String filtro)
        {
            DataTable consulta = null;
            String sql = "SELECT   siat_cp_hotel AS hotel, siat_cp_tiquete AS tiq, siat_cp_alimentacion AS ali, siat_cp_trans_interno AS transInt, siat_cp_trans_aero AS transAereo, siat_cp_llamadas AS llam, siat_cp_lavanderia AS lav,  siat_cp_otros AS otros, siat_cp_penal AS penal "
            + " FROM   siat_costos_plan   " + filtro;
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Inserta los costos
        public String insertarCostosP(String columPaisZonaC, String idPaisZonaC, String hotel, String tiq, String alimen, String transInt, String transAreo, String llam, String lavan, String otros, String usu, String penal)
        { 
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO  siat_costos_plan (" + columPaisZonaC + ", siat_cp_hotel, siat_cp_tiquete, siat_cp_alimentacion, siat_cp_trans_interno, siat_cp_trans_aero, siat_cp_llamadas, siat_cp_lavanderia, siat_cp_otros, siat_cp_usu, siat_cp_fecha, siat_cp_penal) "
                + " VALUES (" + idPaisZonaC + "," + hotel.Replace(".","") + "," + tiq.Replace(".","") + "," + alimen.Replace(".","") + "," + transInt.Replace(".","") + "," + transAreo.Replace(".","") + "," + llam.Replace(".","") + "," + lavan.Replace(".","") + "," + otros.Replace(".","") + ", '" + usu + "', SYSDATETIME(), " + penal.Replace(".","") + ")";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT siat_costos_plan)";
            }
            return mensaje;
        }
        //Edita los costos
        public String editarCostosP(String filtro, String hotel, String tiq, String alimen, String transInt, String transAreo, String llam, String lavan, String otros, String usu, String penal)
        {
            String mensaje = "";
            try
            {
                String sql = " UPDATE  siat_costos_plan SET siat_cp_hotel = " + hotel.Replace(".","") + ", siat_cp_tiquete = " + tiq.Replace(".","") + ", siat_cp_alimentacion = " + alimen.Replace(".","") + ", siat_cp_trans_interno = " + transInt.Replace(".","") + ", siat_cp_trans_aero = " + transAreo.Replace(".","") + ", "
                + " siat_cp_llamadas = " + llam.Replace(".","") + ", siat_cp_lavanderia = " + lavan.Replace(".","") + ", siat_cp_otros = " + otros.Replace(".","") + ", siat_cp_usu = '" + usu + "', siat_cp_fecha = SYSDATETIME(), siat_cp_penal = " + penal.Replace(".","") + filtro; 
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE siat_costos_plan)";
            }
            return mensaje;
        }
        //Carga los costos del pais de la visita
        public DataTable cargarPaisCostos(String lista)
        {
            DataTable consulta = null;
            String sql = "SELECT TOP (1) pais.pai_id AS idPais, pais.pai_nombre AS nomPais, zonaCiudad.zonac_id AS idZCiu, zonaCiudad.zonac_nom AS nomZCiu  FROM  formato_unico INNER JOIN  obra ON formato_unico.fup_obr_id = obra.obr_id INNER JOIN "
            + " siat_of_viaje INNER JOIN  Orden ON siat_of_viaje.siat_of_viaje_id_ofa = Orden.Id_Ofa ON formato_unico.fup_id = Orden.Yale_Cotiza INNER JOIN "
            + " pais ON obra.obr_pai_id = pais.pai_id INNER JOIN ciudad ON obra.obr_ciu_id = ciudad.ciu_id INNER JOIN zonaCiudad ON ciudad.ciu_zona = zonaCiudad.zonac_id WHERE  (siat_of_viaje.siat_of_viaje_id_ofa IN (" + lista + ")) GROUP BY pais.pai_id, pais.pai_nombre, zonaCiudad.zonac_id, zonaCiudad.zonac_nom";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga las ordenes para planear
        public DataTable cargarOrdenesPlan(string year, string filtro)
        {
            DataTable consulta = null;
            String sql = " SELECT DISTINCT " +
                         " Orden.Id_Ofa AS idOf, fu.fup_cli_id AS idCliente, obra.obr_id AS idObra, cliente.cli_nombre AS cliente, Orden.Numero + '-' + Orden.ano AS orden, CONVERT(VARCHAR, " +
                         " CONVERT(MONEY, sf.Sf_vlr_comercial), 1) AS valor, ISNULL(CONVERT(CHAR(10), fas.actseg_fecLlegada_obra, 103), '') AS fechaLlegObra, ISNULL(CONVERT(CHAR(10), " +
                         " Orden.Fec_Real_IngR, 103), '') AS fechaDesReal, ISNULL(CONVERT(CHAR(10), fas.actseg_fecDespacho, 103), '') AS fechaDesPlan, " +
                         " obra.obr_nombre + ' - ' + pais.pai_nombre + ' - ' + ciudad.ciu_nombre + ' - ' + obra.obr_direccion AS datosObra, Orden.Yale_Cotiza, moneda.mon_id AS moneda, " +
                         " YEAR(Orden_Seg.fecha_crea) AS Expr1, moneda.mon_descripcion " +
                         " FROM cliente INNER JOIN " +
                         " formato_unico AS fu ON cliente.cli_id = fu.fup_cli_id INNER JOIN " +
                         " obra ON fu.fup_obr_id = obra.obr_id INNER JOIN " +
                         " pais ON obra.obr_pai_id = pais.pai_id INNER JOIN  " +
                         " ciudad ON obra.obr_ciu_id = ciudad.ciu_id INNER JOIN " +
                         " Orden INNER JOIN " +
                         " fup_enc_entrada_cotizacion AS feec ON Orden.Yale_Cotiza = feec.eect_fup_id AND Orden.ord_version = feec.eect_vercot_id INNER JOIN " +
                         " fup_acta_seguimiento AS fas ON feec.eect_fup_id = fas.actseg_fup_id AND feec.eect_vercot_id = fas.actseg_version ON fu.fup_id = feec.eect_fup_id INNER JOIN " +
                         " solicitud_facturacion AS sf ON Orden.Yale_Cotiza = sf.Sf_fup_id AND Orden.ord_version = sf.Sf_version AND Orden.ord_parte = sf.Sf_parte INNER JOIN " +
                         " moneda ON fu.fup_unm_id = moneda.mon_id INNER JOIN " +
                         " Orden_Seg ON Orden.ordenseg_id = Orden_Seg.Id_Seg_Of AND sf.Sf_id = Orden_Seg.sf_id " +
                         " WHERE(Orden.letra = '1') AND(Orden.Anulada = 0) " + year + filtro + " ";

            //String sql = " SELECT DISTINCT  Orden.Id_Ofa AS idOf, fu.fup_cli_id AS idCliente, obra.obr_id AS idObra, cliente.cli_nombre AS cliente, Orden.Numero + '-' + Orden.ano AS orden, CONVERT(VARCHAR, CONVERT(MONEY, " 
            //+ " sf.Sf_vlr_venta), 1) AS valor, isnull(CONVERT(CHAR(10), fas.actseg_fecLlegada_obra, 103), '') AS fechaLlegObra, isnull(CONVERT(CHAR(10), Orden.Fec_Real_IngR, 103), '') AS fechaDesReal, isnull(CONVERT(CHAR(10), " 
            //+ " fas.actseg_fecDespacho, 103), '') AS fechaDesPlan, obra.obr_nombre + ' - ' + pais.pai_nombre + ' - ' + ciudad.ciu_nombre + ' - ' + obra.obr_direccion AS datosObra, Orden.Yale_Cotiza, moneda.mon_id as moneda "
            //+ " FROM  cliente INNER JOIN  formato_unico fu ON cliente.cli_id = fu.fup_cli_id INNER JOIN  obra ON fu.fup_obr_id = obra.obr_id INNER JOIN  pais ON obra.obr_pai_id = pais.pai_id INNER JOIN "
            //+ " ciudad ON obra.obr_ciu_id = ciudad.ciu_id INNER JOIN  Orden INNER JOIN "
            //+ " fup_enc_entrada_cotizacion AS feec ON Orden.Yale_Cotiza = feec.eect_fup_id AND Orden.ord_version = feec.eect_vercot_id INNER JOIN "
            //+ " fup_acta_seguimiento AS fas ON feec.eect_fup_id = fas.actseg_fup_id AND feec.eect_vercot_id = fas.actseg_version ON fu.fup_id = feec.eect_fup_id INNER JOIN "
            //+ " solicitud_facturacion AS sf ON Orden.Yale_Cotiza = sf.Sf_fup_id AND Orden.ord_version = sf.Sf_version AND Orden.ord_parte = sf.Sf_parte INNER JOIN       moneda ON fu.fup_unm_id = moneda.mon_id "
            //+ " WHERE  (Orden.letra = '1') AND (Orden.Anulada = 0) " + filtro + " ORDER BY idOf DESC ";
            //AND (orden.Despachada <> 1)

            /* "SELECT  Orden.Id_Ofa AS idOf,formato_unico.fup_cli_id AS idCliente, obra.obr_id AS idObra, cliente.cli_nombre AS cliente, Orden.Numero + '-' + Orden.ano AS orden, CONVERT(VARCHAR,CONVERT(MONEY, fas.actseg_ValorEXW),1) AS valor, CONVERT(CHAR(10),fas.actseg_fecLlegada_obra, 103) AS fechaLlegObra, CONVERT(CHAR(10),Orden.Fec_Real_IngR, 103) AS fechaDesReal, "
            + " CONVERT(CHAR(10),fas.actseg_fecDespacho, 103) AS fechaDesPlan,  obra.obr_nombre + ' - ' + pais.pai_nombre + ' - ' + ciudad.ciu_nombre + ' - ' + obra.obr_direccion AS datosObra "
            + " FROM  cliente INNER JOIN  formato_unico ON cliente.cli_id = formato_unico.fup_cli_id INNER JOIN  obra ON formato_unico.fup_obr_id = obra.obr_id INNER JOIN  pais ON obra.obr_pai_id = pais.pai_id INNER JOIN "
            + " ciudad ON obra.obr_ciu_id = ciudad.ciu_id INNER JOIN  Orden INNER JOIN  fup_enc_entrada_cotizacion feec ON Orden.Yale_Cotiza = feec.eect_fup_id AND Orden.ord_version = feec.eect_vercot_id INNER JOIN "
            + " fup_acta_seguimiento fas ON feec.eect_fup_id = fas.actseg_fup_id AND  feec.eect_vercot_id = fas.actseg_version ON "
            + " formato_unico.fup_id = feec.eect_fup_id  WHERE   (Orden.letra = '1') AND (Orden.Anulada = 0) " + filtro + " ORDER BY  Orden.Id_Ofa DESC";*/
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga la planeacion de las actividades
        public DataTable cargarPlaneacionAct(String ano)
        {
            //String ini = Convert.ToDateTime(inicio).ToString("dd/MM/yyyy"); 
            //String fin = Convert.ToDateTime(final).ToString("dd/MM/yyyy");

            //, String inicio, String final
            DataTable consulta = null;
            //String sql = "SELECT 'act' AS tipoAct, siat_mov_actividad.siat_mov_activ_id AS idAct, siat_mov_actividad.siat_mov_activ_tec_id AS tec, CONVERT(CHAR(10),siat_mov_actividad.siat_mov_activ_fechaInicio, 103) AS fechaIni, CONVERT(CHAR(10),siat_mov_actividad.siat_mov_activ_fechaFinal, 103)AS fechaFin, "
            //+ " siat_actividad.siat_act_letra AS letra, siat_actividad.siat_act_color AS color, siat_mov_actividad.siat_mov_activ_obser AS obs FROM  siat_mov_actividad INNER JOIN siat_actividad ON siat_mov_actividad.siat_mov_activ_act_id = siat_actividad.siat_act_id "
            //+ " WHERE (YEAR(siat_mov_actividad.siat_mov_activ_fechaInicio) >= '" + ano + "') AND (YEAR(siat_mov_actividad.siat_mov_activ_fechaFinal) <= '" + ano + "') "
            //+ " UNION "
            //+ " SELECT 'via' AS tipoAct, siat_viaje.siat_via_id AS idAct, siat_viaje.siat_via_tec_id AS tec, CONVERT(CHAR(10), siat_viaje.siat_via_fechaInicio, 103) AS fechaIni,CONVERT(CHAR(10), siat_viaje.siat_via_fechaFin, 103) AS fechaFin, "
            //+ " siat_actividad.siat_act_letra AS letra, siat_actividad.siat_act_color AS color,+ '(' + siat_viaje.siat_estado_viaje_id + ')  ' + pais.pai_nombre + '-' + ciudad.ciu_nombre AS obs "
            //+ " FROM  Orden INNER JOIN  siat_viaje INNER JOIN siat_actividad ON siat_viaje.siat_via_act_id = siat_actividad.siat_act_id INNER JOIN siat_of_viaje ON siat_viaje.siat_via_id = siat_of_viaje.siat_of_viaje_via_id "
            //+ " ON Orden.Id_Ofa = siat_of_viaje.siat_of_viaje_id_ofa INNER JOIN formato_unico INNER JOIN ciudad INNER JOIN obra INNER JOIN pais ON obra.obr_pai_id = pais.pai_id ON ciudad.ciu_id = obra.obr_ciu_id "
            //+ " ON formato_unico.fup_obr_id = obra.obr_id ON  Orden.Yale_Cotiza = formato_unico.fup_id WHERE (YEAR(siat_viaje.siat_via_fechaInicio) >= '" + ano + "') AND (YEAR(siat_viaje.siat_via_fechaFin) <= '" + ano + "') AND (siat_viaje.siat_via_cargo = 'Tecnico') AND (siat_viaje.siat_estado_viaje_id <> 4)"
            //+ " GROUP BY siat_viaje.siat_via_id, siat_viaje.siat_via_tec_id, siat_viaje.siat_via_fechaInicio, siat_viaje.siat_via_fechaFin, siat_actividad.siat_act_letra,  "
            //+ " siat_actividad.siat_act_color, pais.pai_nombre, siat_viaje.siat_estado_viaje_id, ciudad.ciu_nombre ";

            //String sql = "SELECT        'act' AS tipoAct, siat_mov_actividad.siat_mov_activ_id AS idAct, siat_mov_actividad.siat_mov_activ_tec_id AS tec, CONVERT(CHAR(10), " +
            //             " siat_mov_actividad.siat_mov_activ_fechaInicio, 103) AS fechaIni, CONVERT(CHAR(10), siat_mov_actividad.siat_mov_activ_fechaFinal, 103) AS fechaFin, " +
            //             " siat_actividad.siat_act_letra AS letra, siat_actividad.siat_act_color AS color, siat_mov_actividad.siat_mov_activ_obser AS obs " +
            //             " FROM            siat_mov_actividad INNER JOIN " +
            //             " siat_actividad ON siat_mov_actividad.siat_mov_activ_act_id = siat_actividad.siat_act_id " +
            //             " WHERE(YEAR(siat_mov_actividad.siat_mov_activ_fechaInicio) >= '"+ ano + "') AND(YEAR(siat_mov_actividad.siat_mov_activ_fechaFinal) <= '" + ano + "') " +
            //             " UNION " +
            //             " SELECT        'via' AS tipoAct, siat_viaje.siat_via_id AS idAct, siat_viaje.siat_via_tec_id AS tec, CONVERT(CHAR(10), siat_viaje.siat_via_fechaInicio, 103) AS fechaIni, " +
            //             " CONVERT(CHAR(10), siat_viaje.siat_via_fechaFin, 103) AS fechaFin, siat_actividad_1.siat_act_letra AS letra, siat_actividad_1.siat_act_color AS color, "+
            //             " +siat_estado_viaje.descripcion + ' ' + pais.pai_nombre + '-' + ciudad.ciu_nombre AS obs " +
            //             " FROM Orden INNER JOIN " +
            //             " siat_viaje INNER JOIN " +
            //             " siat_actividad AS siat_actividad_1 ON siat_viaje.siat_via_act_id = siat_actividad_1.siat_act_id INNER JOIN " +
            //             " siat_of_viaje ON siat_viaje.siat_via_id = siat_of_viaje.siat_of_viaje_via_id ON Orden.Id_Ofa = siat_of_viaje.siat_of_viaje_id_ofa INNER JOIN " +
            //             " formato_unico INNER JOIN " +
            //             " ciudad INNER JOIN " +
            //             " obra INNER JOIN " +
            //             " pais ON obra.obr_pai_id = pais.pai_id ON ciudad.ciu_id = obra.obr_ciu_id ON formato_unico.fup_obr_id = obra.obr_id ON " +
            //             " Orden.Yale_Cotiza = formato_unico.fup_id INNER JOIN " +
            //             " siat_estado_viaje ON siat_estado_viaje.siat_estado_viaje_id = siat_viaje.siat_estado_viaje_id " +
            //             " WHERE(YEAR(siat_viaje.siat_via_fechaInicio) >= '"+ ano + "') AND(YEAR(siat_viaje.siat_via_fechaFin) <= '" + ano + "') AND(siat_viaje.siat_via_cargo = 'Tecnico') AND " +
            //             " (siat_viaje.siat_estado_viaje_id <> 4) " +
            //             " GROUP BY siat_viaje.siat_via_id, siat_viaje.siat_via_tec_id, siat_viaje.siat_via_fechaInicio, siat_viaje.siat_via_fechaFin, siat_actividad_1.siat_act_letra, " + 
            //             " siat_actividad_1.siat_act_color, pais.pai_nombre, siat_viaje.siat_estado_viaje_id, ciudad.ciu_nombre, siat_estado_viaje.descripcion";

            String sql = " SELECT        'act' AS tipoAct, siat_mov_actividad.siat_mov_activ_id AS idAct, siat_mov_actividad.siat_mov_activ_tec_id AS tec, CONVERT(CHAR(10), " +
                         " siat_mov_actividad.siat_mov_activ_fechaInicio, 103) AS fechaIni, CONVERT(CHAR(10), siat_mov_actividad.siat_mov_activ_fechaFinal, 103) AS fechaFin, " +
                         " siat_actividad.siat_act_letra AS letra, siat_actividad.siat_act_color AS color, siat_mov_actividad.siat_mov_activ_obser AS obs " +
                         " FROM            siat_mov_actividad INNER JOIN " +
                         " siat_actividad ON siat_mov_actividad.siat_mov_activ_act_id = siat_actividad.siat_act_id INNER JOIN " +
                         " empleado ON siat_mov_actividad.siat_mov_activ_tec_id = empleado.emp_usu_num_id INNER JOIN " +
                         " usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id " +
                         " WHERE(YEAR(siat_mov_actividad.siat_mov_activ_fechaInicio) >= '" + ano + "') AND(YEAR(siat_mov_actividad.siat_mov_activ_fechaFinal) <= '" + ano + "') AND    (empleado.emp_estado_laboral = 'ACTIVO') AND (usuario.usu_activo = 1) " +
                //" and siat_mov_actividad.siat_mov_activ_fechaInicio >= '" + ini+ "' and siat_mov_actividad.siat_mov_activ_fechaFinal <= '"+fin+"' " +
                         " UNION " +
                         " SELECT        'via' AS tipoAct, siat_viaje.siat_via_id AS idAct, siat_viaje.siat_via_tec_id AS tec, CONVERT(CHAR(10), siat_viaje.siat_via_fechaInicio, 103) AS fechaIni, " +
                         " CONVERT(CHAR(10), siat_viaje.siat_via_fechaFin, 103) AS fechaFin, siat_actividad_1.siat_act_letra AS letra, siat_actividad_1.siat_act_color AS color, " +
                         " siat_estado_viaje.descripcion + ' ' + pais.pai_nombre + '-' + ciudad.ciu_nombre AS obs " +
                         " FROM            siat_cotizacion INNER JOIN " +
                         " siat_estado_viaje INNER JOIN " +
                         " siat_viaje INNER JOIN " +
                         " siat_actividad AS siat_actividad_1 ON siat_viaje.siat_via_act_id = siat_actividad_1.siat_act_id ON " +
                         " siat_estado_viaje.siat_estado_viaje_id = siat_viaje.siat_estado_viaje_id ON siat_cotizacion.siat_cotizacion_id = siat_viaje.siat_cotizacion_id INNER JOIN " +
                         " cliente ON siat_cotizacion.cliente_id = cliente.cli_id INNER JOIN " +
                         " pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN " +
                         " ciudad ON cliente.cli_ciu_id = ciudad.ciu_id INNER JOIN " +
                         " empleado ON siat_viaje.siat_via_tec_id = empleado.emp_usu_num_id INNER JOIN " +
                         " usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id LEFT OUTER JOIN " +
                         " siat_of_viaje ON siat_viaje.siat_via_id = siat_of_viaje.siat_of_viaje_via_id  " +
                         " WHERE(YEAR(siat_viaje.siat_via_fechaInicio) >= '" + ano + "') AND(YEAR(siat_viaje.siat_via_fechaFin) <= '" + ano + "') AND (siat_viaje.siat_via_cargo = 'Tecnico') AND " +
                         " (siat_viaje.siat_estado_viaje_id <> 4) AND (empleado.emp_estado_laboral = 'ACTIVO') AND (usuario.usu_activo = 1) " +
                //and  siat_viaje.siat_via_fechaInicio >= '"+ini+ "' and siat_viaje.siat_via_fechaFin <= '"+fin+"' " +
                         " GROUP BY siat_viaje.siat_via_id, siat_viaje.siat_via_tec_id, siat_viaje.siat_via_fechaInicio, siat_viaje.siat_via_fechaFin, siat_actividad_1.siat_act_letra, " +
                         " siat_actividad_1.siat_act_color, siat_viaje.siat_estado_viaje_id, siat_estado_viaje.descripcion, siat_viaje.siat_cotizacion_id, siat_estado_viaje.descripcion,  " +
                         " pais.pai_nombre, ciudad.ciu_nombre";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Consulta año actual
        public String anoActual()
        {
            String idAgenda = "2015";
            DataTable consulta = null;
            String sql = "SELECT YEAR(SYSDATETIME()) AS ano";
            consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                idAgenda = row["ano"].ToString();
            }
            return idAgenda;
        }
        //Consulta si ya existe el viaje
        public Boolean existeViajeTec(String tec, String fechaIni, String fechaFin, String filtro)
        {
            Boolean existe = true;
            DataTable consulta = null;
            String sql = "SELECT  siat_via_id AS idAct FROM  siat_viaje  WHERE (siat_estado_viaje_id <> 4) AND (siat_via_cargo = 'Tecnico') AND (siat_via_tec_id = " + tec + ") AND (CONVERT(DATETIME, '" + fechaIni + "', 103) <= siat_via_fechaFin) AND (CONVERT(DATETIME, '" + fechaFin + "', 103) >= siat_via_fechaInicio) " + filtro 
            + " UNION "
            + " SELECT  siat_mov_activ_id AS idAct  FROM  siat_mov_actividad  WHERE (siat_mov_activ_tec_id = " + tec + ")  AND (CONVERT(DATETIME, '" + fechaIni + "', 103) <= siat_mov_activ_fechaFinal) AND (CONVERT(DATETIME, '" + fechaFin + "', 103) >= siat_mov_activ_fechaInicio)";
            consulta = BdDatos.CargarTabla(sql);
            if (consulta.Rows.Count > 0) { existe = true; } else { existe = false; }
            return existe;
        }
        //Consulta si ya existe un viaje de inventario para crear un de implementacion
        public Boolean existeViajeTecInv(String idVia, String fechaIni, String fechaFin)
        {
            Boolean existe = true;
            DataTable consulta = null;
            String sql = "SELECT  siat_via_id AS idAct FROM  siat_viaje  WHERE (siat_estado_viaje_id <> 4) AND (siat_via_cargo = 'Tecnico')  AND (siat_via_tipoAct = 'inv') AND (CONVERT(DATETIME, '" + fechaIni + "', 103) <= siat_via_fechaFin) AND (CONVERT(DATETIME, '" + fechaFin + "', 103) >= siat_via_fechaInicio) AND (siat_via_id = " + idVia + ")";
            consulta = BdDatos.CargarTabla(sql);
            if (consulta.Rows.Count > 0) { existe = true; } else { existe = false; }
            return existe;
        }
        //Consulta si ya existe el viaje
        public Boolean existeViajeIng(String ing, String fechaIni, String fechaFin, String filtro)
        {
            Boolean existe = true;
            DataTable consulta = null;
            String sql = "SELECT  siat_via_id AS idAct FROM  siat_viaje  WHERE (siat_estado_viaje_id <> 4) AND (siat_via_cargo = 'Ingeniero') AND (siat_via_usu_plan = '" + ing + "') AND (CONVERT(DATETIME, '" + fechaIni + "', 103) <= siat_via_fechaFin) AND (CONVERT(DATETIME, '" + fechaFin + "', 103) >= siat_via_fechaInicio) " + filtro;
            consulta = BdDatos.CargarTabla(sql);
            if (consulta.Rows.Count > 0) { existe = true; } else { existe = false; }
            return existe;
        }
        //Carga los datos del viaje cuando lo selecciona en el calendario
        public DataTable cargarDatosViaje(String idVia)
        {
            DataTable consulta = null;
            String sql = "SELECT (SELECT  STUFF(( SELECT ', ' + cliente.cli_nombre FROM  cliente INNER JOIN  formato_unico ON cliente.cli_id = formato_unico.fup_cli_id INNER JOIN siat_viaje INNER JOIN "
            + " siat_of_viaje ON siat_viaje.siat_via_id = siat_of_viaje.siat_of_viaje_via_id INNER JOIN Orden ON siat_of_viaje.siat_of_viaje_id_ofa = Orden.Id_Ofa ON "
            + " formato_unico.fup_id = Orden.Yale_Cotiza  WHERE (siat_viaje.siat_via_id = " + idVia + ") GROUP BY cliente.cli_id, cliente.cli_nombre for xml path('')), 1, 1, '')) AS clientes, "
            + " (SELECT STUFF(( SELECT ', ' +  obra.obr_nombre FROM  formato_unico INNER JOIN  siat_viaje INNER JOIN siat_of_viaje ON siat_viaje.siat_via_id = siat_of_viaje.siat_of_viaje_via_id INNER JOIN "
            + " Orden ON siat_of_viaje.siat_of_viaje_id_ofa = Orden.Id_Ofa ON formato_unico.fup_id = Orden.Yale_Cotiza INNER JOIN obra ON formato_unico.fup_obr_id = obra.obr_id "
            + " WHERE  (siat_viaje.siat_via_id = " + idVia + ") GROUP BY obra.obr_nombre, obra.obr_id for xml path('')), 1, 1, '')) AS obras, "
            + " (SELECT STUFF(( SELECT ', ' + Orden.Numero + '-' + Orden.ano FROM  siat_viaje INNER JOIN siat_of_viaje ON siat_viaje.siat_via_id = siat_of_viaje.siat_of_viaje_via_id INNER JOIN "
            + " Orden ON siat_of_viaje.siat_of_viaje_id_ofa = Orden.Id_Ofa WHERE (siat_viaje.siat_via_id = " + idVia + ") GROUP BY Orden.Numero, Orden.ano for xml path('')), 1, 1, '')) AS ofs, "
            + " (SELECT  e.emp_nombre + ' '+ e.emp_apellidos + ' ' + (CASE WHEN siat_via_idInv IS NULL THEN '' ELSE (SELECT ', ' +  e.emp_nombre + ' '+ e.emp_apellidos "
			+ " FROM siat_viaje sv2 INNER JOIN empleado e ON e.emp_usu_num_id = sv2.siat_via_tec_id WHERE (sv2.siat_via_id = sv1.siat_via_idInv)) END)  "
            + " FROM siat_viaje sv1 INNER JOIN empleado e ON e.emp_usu_num_id = sv1.siat_via_tec_id WHERE (sv1.siat_via_id = " + idVia + ")) AS nomTec, "
            + " sv0.siat_via_dTotal AS dTotal, sv0.siat_via_dReal AS dReal, sv0.siat_via_dInv AS dInv, sv0.siat_via_dImp AS dImp, sv0.siat_via_dpend AS dPend, ciudad.ciu_nombre as ciudad, pais.pai_nombre as pais, siat_estado_viaje.descripcion AS estado,  sc.consecutivo as cotizacion , sv0.siat_via_obra_fechaInicio as fechaInicio, sv0.siat_via_obra_fechaFin as fechaFin FROM siat_viaje sv0  INNER JOIN "
            + " siat_cotizacion AS sc ON sc.siat_cotizacion_id = sv0.siat_cotizacion_id INNER JOIN " 
            + " cliente ON cliente.cli_id = sc.cliente_id INNER JOIN " 
            + " pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN "
            + " ciudad ON cliente.cli_ciu_id = ciudad.ciu_id INNER JOIN " 
            + "siat_estado_viaje ON sv0.siat_estado_viaje_id = siat_estado_viaje.siat_estado_viaje_id "  
            + " WHERE (sv0.siat_via_id = " + idVia + ")";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga los datos del viaje cuando lo selecciona en el calendario
        public DataTable filtrarPais(String filtroPais, String fechaIni, String fechaFin)
        {
            string filtroFecha = "";

            if (!String.IsNullOrEmpty(fechaIni) && !String.IsNullOrEmpty(fechaFin))
            {
                filtroFecha = " AND  siat_viaje.siat_via_fechaInicio >= '" + fechaIni + "' AND siat_viaje.siat_via_fechaFin <= '" + fechaFin + "' ";
            }
            else if (!String.IsNullOrEmpty(fechaIni))
            {
                filtroFecha = " AND siat_viaje.siat_via_fechaInicio >= '" + fechaIni + "'";
            }
            else if (!String.IsNullOrEmpty(fechaFin))
            {
                filtroFecha = " AND siat_viaje.siat_via_fechaFin <= '" + fechaFin + "' ";
            }

            DataTable consulta = null;
            String sql = "SELECT  siat_ciu_viaje.siat_ciu_viaje_id AS idCiuViaje, siat_ciu_viaje.siat_ciu_viaje_via_id AS idViaje, empleado.emp_nombre + ' ' + empleado.emp_apellidos AS nomIng, "
             + " pais.pai_nombre AS pais, ciudad.ciu_nombre AS ciudad, CONVERT(CHAR(10), siat_viaje.siat_via_fechaInicio, 103) AS fechaIni, CONVERT(CHAR(10), siat_viaje.siat_via_fechaFin, 103) AS fechaFin "
             + " FROM    siat_ciu_viaje INNER JOIN siat_viaje ON siat_ciu_viaje.siat_ciu_viaje_via_id = siat_viaje.siat_via_id INNER JOIN ciudad ON siat_ciu_viaje.siat_ciu_viaje_ciu_id = ciudad.ciu_id INNER JOIN "
             + " pais ON ciudad.ciu_pai_id = pais.pai_id INNER JOIN usuario ON siat_viaje.siat_via_usu_plan = usuario.usu_login INNER JOIN "
             + " empleado ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id  WHERE  " + filtroPais + filtroFecha + " ORDER BY siat_viaje.siat_via_fechaInicio DESC";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Buscar viaje por medio de la OBRA
        public DataTable filtrarObra(String filtroObra)
        {
            DataTable consulta = null;
            String sql = "SELECT   siat_obr_viaje.siat_obr_viaje_id AS idViajeObra,  siat_viaje.siat_via_id AS idViaje, CONVERT(CHAR(10), siat_viaje.siat_via_fechaInicio, 103) AS fechaInicio, CONVERT(CHAR(10), siat_viaje.siat_via_fechaFin, 103) AS fechaFin,"
            + " cliente.cli_nombre AS cliente,  '(Usos:' + CONVERT(VARCHAR, siat_obr_viaje.siat_obr_viaje_usos)  + ') ' + obra.obr_nombre + ' - ' + pais.pai_nombre + ' - ' + ciudad.ciu_nombre + ' - ' + obra.obr_direccion AS usosObra,"
            + " CONVERT(VARCHAR, siat_obr_viaje.siat_obr_viaje_obr_id) + '|' + CONVERT(VARCHAR, siat_obr_viaje.siat_obr_viaje_usos) AS idUsosObra, obra.obr_nombre + ' - ' + pais.pai_nombre + ' - ' + ciudad.ciu_nombre + ' - ' + obra.obr_direccion AS datosObra "
            + " FROM         siat_viaje INNER JOIN siat_obr_viaje ON siat_viaje.siat_via_id = siat_obr_viaje.siat_obr_viaje_via_id INNER JOIN "
            + " obra ON siat_obr_viaje.siat_obr_viaje_obr_id = obra.obr_id INNER JOIN  cliente ON obra.obr_cli_id = cliente.cli_id INNER JOIN "
            + " pais ON obra.obr_pai_id = pais.pai_id INNER JOIN  ciudad ON obra.obr_ciu_id = ciudad.ciu_id "
            + " WHERE     (siat_viaje.siat_estado_viaje_id = 1) AND (siat_viaje.siat_via_cargo = 'Ingeniero') " + filtroObra + " ";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Buscar ciudades del viaje
        public DataTable cargarCiuViajes(String idViaje)
        {
            DataTable consulta = null;
            String sql = "SELECT  ciudad.ciu_id AS idCiu, ciudad.ciu_nombre AS ciudad, pais.pai_nombre + ' - ' + ciudad.ciu_nombre AS nomPaisCiu,  CONVERT(VARCHAR, ciudad.ciu_pai_id) + '|' + CONVERT(VARCHAR,ciudad.ciu_id) AS idPaisCiu FROM siat_ciu_viaje INNER JOIN  siat_viaje ON siat_ciu_viaje.siat_ciu_viaje_via_id = siat_viaje.siat_via_id INNER JOIN "
            + " ciudad ON siat_ciu_viaje.siat_ciu_viaje_ciu_id = ciudad.ciu_id INNER JOIN   pais ON ciudad.ciu_pai_id = pais.pai_id WHERE  (siat_ciu_viaje.siat_ciu_viaje_via_id = " + idViaje + ")";
            consulta = BdDatos.CargarTabla(sql);
            return consulta; // ciudad.ciu_pai_id
        }

        //Consultar los dias totales dado el valor segun la tabla rangos
        public int consultarDiasTotales(int moneda, double valor, int fup, string version)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[4];
            sqls[0] = new SqlParameter("valor", valor);
            sqls[1] = new SqlParameter("moneda", moneda);
            sqls[2] = new SqlParameter("fup", fup);
            sqls[3] = new SqlParameter("version", version);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("consultarDiasRangoOrden", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    // Esta es la clave, hemos de añadir un parámetro que recogerá el valor de retorno
                    SqlParameter retValue = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
                    retValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(retValue);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        Id = Convert.ToInt32(retValue.Value);
                    }
                }
            }

            return Id;
        }

        //Trae los servicios visibles en cotizacion
        public DataTable cargarServicios()
        {
            DataTable consulta = null;
            String sql = "SELECT servicios_siat_id, nombre FROM servicios_siat WHERE (activo = 1) and (v_cotizacion = 1)";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        //Trae todos los servicios 
        public DataTable cargarServiciosTodos()
        {
            DataTable consulta = null;
            String sql = "SELECT servicios_siat_id, nombre FROM servicios_siat WHERE (activo = 1)";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public DataTable consultarMonedaCliente(int idCliente)
        {
            DataTable consulta = new DataTable();
            String sql = "SELECT        moneda.mon_id, cliente.cli_nombre, cliente.cli_id, moneda.mon_descripcion AS moneda, pais.pai_id as pais " +
                         " FROM moneda INNER JOIN " +
                         " pais ON moneda.mon_id = pais.pai_moneda RIGHT OUTER JOIN " +
                         " cliente ON pais.pai_id = cliente.cli_pai_id " +
                         " WHERE(moneda.mon_activo = 1) and  cliente.cli_id = " + idCliente + "";
            consulta = BdDatos.CargarTabla(sql);            
            return consulta;
        }

        //Insertar cotizacion de SIAT
        public int insertarCotizacion(int servicio, int cliente, int tecnicos, double honorarios, double tiquete, 
                                      double total, string usuario, int dias, int moneda, int estado, int cotizacion,
                                      string observacion, bool facturable)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[13];
            sqls[0] = new SqlParameter("servicio", servicio);
            sqls[1] = new SqlParameter("cliente", cliente);
            sqls[2] = new SqlParameter("tecnicos", tecnicos);
            sqls[3] = new SqlParameter("honorarios", honorarios);
            sqls[4] = new SqlParameter("tiquete", tiquete);
            sqls[5] = new SqlParameter("total", total);
            sqls[6] = new SqlParameter("usuario", usuario);
            sqls[7] = new SqlParameter("dias", dias);
            sqls[8] = new SqlParameter("moneda", moneda);
            sqls[9] = new SqlParameter("estado", estado);
            sqls[10] = new SqlParameter("cotizacion", cotizacion);
            sqls[11] = new SqlParameter("observacion", observacion);
            sqls[12] = new SqlParameter("facturable", facturable);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarCotizacionSiat", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    // Esta es la clave, hemos de añadir un parámetro que recogerá el valor de retorno
                    SqlParameter retValue = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
                    retValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(retValue);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        Id = Convert.ToInt32(retValue.Value);
                    }
                }
            }
            return Id;
        }

        //Insertar cotizacion obra
        public void insertarCotizacionObra(int idCotizacion, ListBox obra)
        {
            string val = "";
            String sql = "INSERT INTO siat_cotizacion_obra (siat_cotizacion_id, siat_obra_id, activo) VALUES";

            foreach (ListItem lis in obra.Items)
            {
                val += " ( " + idCotizacion + ", " + Convert.ToInt32(lis.Value)  + ", 1 ), ";
            }

            val = val.Substring(0, val.Length - 2);

            sql += val;
            BdDatos.Actualizar(sql);
        }

        //Insertar cotizacion orden
        public void insertarCotizacionOrden(int idCotizacion, ListBox orden)
        {
            string val = "";
            String sql = "INSERT INTO siat_cotizacion_orden (siat_cotizacion_id, siat_orden, activo, siat_orden_id) VALUES";

            foreach (ListItem lis in orden.Items)
            {
                val += " ( " + idCotizacion + ", '" + lis.Text + "', 1, " + lis.Value + " ), ";
            }

            val = val.Substring(0, val.Length - 2);

            sql += val;
            BdDatos.Actualizar(sql);
        }

        public DataTable buscarCotizacion(int cotizacion)
        {
            DataTable dt = new DataTable();
            string sql = " SELECT        siat_cotizacion.siat_cotizacion_id, siat_cotizacion.servicio_id, siat_cotizacion.cliente_id, siat_cotizacion.tecnicos, siat_cotizacion.honorarios, " +
                         " siat_cotizacion.tiquete, siat_cotizacion.total, siat_cotizacion.fecha, siat_cotizacion.usuario, siat_cotizacion.estado_cotizacion_id as estado_id, sec.nombre as estado, " +
                         " siat_cotizacion.motivo_rechazo, cliente.cli_nombre AS cliente, servicios_siat.nombre AS servicio, siat_cotizacion.dias, siat_cotizacion.moneda_id, moneda.mon_descripcion, siat_cotizacion.consecutivo, siat_cotizacion.observacion,siat_facturable " +
                         " FROM            siat_cotizacion INNER JOIN " +
                         " cliente ON siat_cotizacion.cliente_id = cliente.cli_id INNER JOIN " +
                         " servicios_siat ON siat_cotizacion.servicio_id = servicios_siat.servicios_siat_id INNER JOIN moneda ON siat_cotizacion.moneda_id = moneda.mon_id " +
                         " INNER JOIN siat_estado_cotizacion as sec ON sec.siat_estado_cotizacion_id = siat_cotizacion.estado_cotizacion_id " +
                         " WHERE siat_cotizacion.siat_cotizacion_id = " + cotizacion + "";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public DataTable buscarCotizacionObra(int cotizacion)
        {
            DataTable dt = new DataTable();
            string sql = " SELECT        siat_cotizacion_obra.siat_cotizacion_obra_id, siat_cotizacion_obra.siat_cotizacion_id, siat_cotizacion_obra.siat_obra_id, obra.obr_nombre " +
                         " FROM siat_cotizacion_obra LEFT OUTER JOIN " +
                         "      obra ON siat_cotizacion_obra.siat_obra_id = obra.obr_id " +
                         " WHERE(siat_cotizacion_obra.activo = 1) AND(siat_cotizacion_obra.siat_cotizacion_id = " + cotizacion + ")";

            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public DataTable buscarCotizacionOrden(int cotizacion)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT        siat_cotizacion_orden_id, siat_cotizacion_id, siat_orden, siat_orden_id " +
                         " FROM siat_cotizacion_orden " +
                         " WHERE(activo = 1) AND(siat_cotizacion_id = " + cotizacion + ")";

            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        //Actualizar cotizacion de SIAT
        public int ActualizarCotizacion(int cotizacion, int servicio, int cliente, int tecnicos, double honorarios, double tiquete, double total, int dias, int moneda, int estado, string motivo, string usuario, string observacion, bool facturable)
        {
            int Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[14];
            sqls[0] = new SqlParameter("cotizacion", cotizacion);
            sqls[1] = new SqlParameter("servicio", servicio);
            sqls[2] = new SqlParameter("cliente", cliente);
            sqls[3] = new SqlParameter("tecnicos", tecnicos);
            sqls[4] = new SqlParameter("honorarios", honorarios);
            sqls[5] = new SqlParameter("tiquete", tiquete);
            sqls[6] = new SqlParameter("total", total);
            sqls[7] = new SqlParameter("dias", dias);
            sqls[8] = new SqlParameter("moneda", moneda);
            sqls[9] = new SqlParameter("estado", estado);
            sqls[10] = new SqlParameter("motivo_rechazo", motivo);
            sqls[11] = new SqlParameter("usuario", usuario);
            sqls[12] = new SqlParameter("observacion", observacion);
            sqls[13] = new SqlParameter("facturable", facturable);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("ActualizarCotizacionSiat", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    // Esta es la clave, hemos de añadir un parámetro que recogerá el valor de retorno
                    SqlParameter retValue = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
                    retValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(retValue);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        Id = Convert.ToInt32(retValue.Value);
                    }
                }
            }

            return Id;
        }

        //Actualizar cotizacion obras
        public void actualizaCotizacionrObra(int cotizacion, ListBox obras)
        {
            String sql = "DELETE FROM siat_cotizacion_obra WHERE siat_cotizacion_id = " + cotizacion + ";";
            BdDatos.Actualizar(sql);
            if(obras.Items.Count > 0)
                insertarCotizacionObra(cotizacion, obras);
        }

        //Actualizar cotizacion orden
        public void actualizarCotizacionOrden(int cotizacion, ListBox orden)
        {
            String sql = "DELETE FROM siat_cotizacion_orden WHERE siat_cotizacion_id = " + cotizacion + ";";
            BdDatos.Actualizar(sql);
            if(orden.Items.Count > 0)
                insertarCotizacionOrden(cotizacion, orden);
        }

        //Carga las cotizaciones aprobadas
        public DataTable cargarCotizacionesAprobadas()
        {
            DataTable dt = new DataTable();
            string sql = " SELECT        siat_cotizacion_id, consecutivo FROM siat_cotizacion " +
                         " WHERE(estado_cotizacion_id = 2) AND(siat_cotizacion_id NOT IN " +
                         " (SELECT        sv.siat_cotizacion_id " +
                         " FROM            siat_viaje AS sv where sv.siat_cotizacion_id is not null))";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public string consultarCorreoUsuarios(ListBox usuarios)
        {
            string correos = "";

            if(usuarios.Items.Count > 0)
            {
                foreach (ListItem lis in usuarios.Items)
                {
                    correos += consultaCorreoUsuario(lis.Value) + ",";                    
                }
                correos = correos.Substring(0, correos.Length - 1);
            }

            return correos;
        }

        private string consultaCorreoUsuario(string usuario)
        {
            string correo = "";
            DataTable dt = new DataTable();
            string sql = "SELECT        (CASE WHEN empleado.emp_correo_electronico IS NULL THEN representantes_comerciales.rc_email ELSE empleado.emp_correo_electronico END) AS correo " +
                         " FROM empleado INNER JOIN " +
                         " usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id LEFT OUTER JOIN " +
                         " representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id " +
                         " WHERE(usuario.usu_login = '" + usuario + "')";
            dt = BdDatos.CargarTabla(sql);
            if (dt.Rows.Count > 0)
            {
                correo = dt.Rows[0]["correo"].ToString();
            }
            return correo;
        }

        public DataTable consultarValorPlaneado(int idCliente)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT        siat_cp_alimentacion + siat_cp_hotel + siat_cp_lavanderia + siat_cp_llamadas + siat_cp_trans_interno AS honorarios, siat_cp_tiquete AS tiquete " +
                         " FROM siat_costos_plan " +
                         " WHERE(siat_cp_pai_id = " +
                         " (SELECT        pais.pai_id " +
                         " FROM            pais INNER JOIN " +
                         " cliente ON pais.pai_id = cliente.cli_pai_id " +
                         " WHERE(cliente.cli_id = " + idCliente + ")))";

            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        //Cargo las ordenes que se crearon en el viaje para mostrarlas en la cotizacion
        public DataTable cargarOrdenesCotizacionViaje(int idCotizacion)
        {
            DataTable dt = new DataTable();
            string sql = "select siat_viaje.siat_via_id , (SELECT (STUFF((SELECT ', ' + CONVERT(VARCHAR, sov.siat_of_viaje_id_ofa) FROM  siat_of_viaje sov  WHERE  (sov.siat_of_viaje_via_id = siat_viaje.siat_via_id) AND (sov.siat_of_viaje_activo = 1) FOR XML PATH('')), 1, 1, ''))) AS ordenes from siat_viaje where siat_viaje.siat_cotizacion_id = " + idCotizacion + " ";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        //Carga los datos de la implementacion de obra de acuerdo al fup
        public DataTable cargarDatosImplementacionObraFup(string fup, string version)
        {
            DataTable dt = new DataTable();
            String sql = "SELECT        Orden.Tipo_Of, Orden.Numero + '-' + Orden.ano AS ordenes, cierre_comercial.cieseg_fup_id AS fup, cierre_comercial.cierre_version, cliente.cli_nombre, " +
                         " obra.obr_nombre, solicitud_facturacion.Sf_vlr_comercial AS valor, cliente.cli_id, obra.obr_id, Orden.Id_Ofa, CONVERT(VARCHAR, obra.obr_id) " +
                         " + '|' + obra.obr_nombre + ' - ' + cliente.cli_nombre + ' - ' + pais.pai_nombre + ' - ' + ciudad.ciu_nombre + ' - ' + obra.obr_direccion AS datosIdObra, " +
                         " isnull(cierre_comercial.siat_dias_disponibles, 0) as dtotal, isnull(cierre_comercial.siat_dias_consumidos, 0) as dconsumidos " +
                         " FROM            cliente INNER JOIN " +
                         " formato_unico ON cliente.cli_id = formato_unico.fup_cli_id INNER JOIN " +
                         " obra ON formato_unico.fup_obr_id = obra.obr_id INNER JOIN " +
                         " cierre_comercial ON formato_unico.fup_id = cierre_comercial.cieseg_fup_id INNER JOIN " +
                         " Orden ON cierre_comercial.cieseg_fup_id = Orden.Yale_Cotiza AND cierre_comercial.cierre_version = Orden.ord_version INNER JOIN " +
                         " solicitud_facturacion ON Orden.Yale_Cotiza = solicitud_facturacion.Sf_fup_id AND Orden.ord_version = solicitud_facturacion.Sf_version AND " +
                         " Orden.ord_parte = solicitud_facturacion.Sf_parte INNER JOIN " +
                         " pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN " +
                         " ciudad ON cliente.cli_ciu_id = ciudad.ciu_id " +
                         " GROUP BY Orden.fecha_ingenieria, Orden.Tipo_Of, Orden.Ofa, Orden.letra, Orden.Numero, Orden.ano, cierre_comercial.cieseg_fup_id, cierre_comercial.cierre_version, " +
                         " cliente.cli_nombre, obra.obr_nombre, cierre_comercial.cierre_valor_final, solicitud_facturacion.Sf_vlr_comercial, cliente.cli_id, obra.obr_id, Orden.Id_Ofa, " +
                         " pais.pai_nombre, ciudad.ciu_nombre, obra.obr_direccion, cierre_comercial.siat_dias_disponibles, cierre_comercial.siat_dias_consumidos " +
                         " HAVING(Orden.letra = '1') AND(cierre_comercial.cieseg_fup_id = "+fup+") AND(cierre_comercial.cierre_version = '"+version+"') " +
                         " ORDER BY Orden.fecha_ingenieria DESC";
                         dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public DataTable consultarViajeOf(string of)
        {
            DataTable dt = new DataTable();
            String sql = "SELECT        siat_viaje.siat_via_id AS idViaje, Orden.Ofa " +
                         " FROM Orden INNER JOIN " +
                         " siat_of_viaje ON Orden.Id_Ofa = siat_of_viaje.siat_of_viaje_id_ofa INNER JOIN " +
                         " siat_viaje ON siat_of_viaje.siat_of_viaje_via_id = siat_viaje.siat_via_id " +
                         " WHERE(siat_viaje.siat_via_cargo = 'Tecnico') AND(siat_of_viaje.siat_of_viaje_activo = 1) AND(Orden.Numero + '-' + Orden.ano LIKE '%" + of + "%') AND (siat_viaje.siat_estado_viaje_id <> 4) " +
                         " ORDER BY idViaje DESC";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public string actualizarDiasConsumidos(int fup, string version, int dReal, int dConsumidos)
        {
            string mensaje = "";
            int diasConsumidos = dConsumidos + dReal;
            string sql = "UPDATE cierre_comercial SET siat_dias_consumidos = "+diasConsumidos+ " where cieseg_fup_id = "+fup+ " and cierre_version = '"+version+"'";
            try
            {
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "Error en el Query ControlSIAT->actualizarDiasConsumidos";
            }
            return mensaje;
        }

        public DataTable buscarCotizacionSiatViaje(int idCotizacion)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT        siat_cotizacion.siat_cotizacion_id, siat_cotizacion.servicio_id, siat_cotizacion.cliente_id, siat_cotizacion.tecnicos, siat_cotizacion.honorarios, " +
                         " siat_cotizacion.tiquete, siat_cotizacion.total, siat_cotizacion.fecha, siat_cotizacion.usuario, siat_cotizacion.estado_cotizacion_id AS estado_id, " +
                         " sec.nombre AS estado, siat_cotizacion.motivo_rechazo, cliente.cli_nombre AS cliente, servicios_siat.nombre AS servicio, siat_cotizacion.dias, "+
                         " siat_cotizacion.moneda_id, moneda.mon_descripcion, siat_cotizacion.consecutivo, siat_viaje.siat_via_id, siat_cotizacion.cotizacion as bitCotizacion " +
                         " ,siat_via_act_id as Actividad " +
                         " FROM            siat_cotizacion INNER JOIN " +
                         " cliente ON siat_cotizacion.cliente_id = cliente.cli_id INNER JOIN " +
                         " servicios_siat ON siat_cotizacion.servicio_id = servicios_siat.servicios_siat_id INNER JOIN " +
                         " moneda ON siat_cotizacion.moneda_id = moneda.mon_id INNER JOIN " +
                         " siat_estado_cotizacion AS sec ON sec.siat_estado_cotizacion_id = siat_cotizacion.estado_cotizacion_id LEFT OUTER JOIN " +
                         " siat_viaje ON siat_cotizacion.siat_cotizacion_id = siat_viaje.siat_cotizacion_id " +
                         " WHERE(siat_cotizacion.estado_cotizacion_id = 2) AND (siat_cotizacion.siat_cotizacion_id = "+idCotizacion+")";

            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        //Dado un viaje returno una de sus ordenes
        public string buscarOFPorViaje(int idViaje)
        {
            string orden = "";
            DataTable dt = new DataTable();
            string sql = "SELECT        siat_viaje.siat_via_id AS idViaje, Orden.Numero + '-' + Orden.ano AS orden " +
                          " FROM Orden INNER JOIN " +
                         " siat_of_viaje ON Orden.Id_Ofa = siat_of_viaje.siat_of_viaje_id_ofa INNER JOIN " +
                         " siat_viaje ON siat_of_viaje.siat_of_viaje_via_id = siat_viaje.siat_via_id " +
                         " WHERE (siat_of_viaje.siat_of_viaje_activo = 1) AND (siat_viaje.siat_via_id = " + idViaje+")";
            dt = BdDatos.CargarTabla(sql);

            if (dt.Rows.Count > 0)
            {
                orden = dt.Rows[0]["orden"].ToString();
            }

            return orden;
        }

        public DataTable buscarCostoRealErp(int idCotizacion, int pais)
        {
            DataTable dt = new DataTable();
            string sql = "";
            //Si el pais es Colombia 
            //if (pais == 8)
            //{
            //    sql = "SELECT       SUM(Vista_Gastos_viaje_Erp.VLR_BRUTO) as valor, siat_grupo_servicios_gastos_erp.siat_grupo_servicios_gastos_erp_id as grupo " +
            //             " FROM siat_servicios_gastos_erp INNER JOIN " +
            //             " Vista_Gastos_viaje_Erp ON siat_servicios_gastos_erp.id_servicio_erp COLLATE SQL_Latin1_General_CP1_CI_AS = Vista_Gastos_viaje_Erp.ID_SERVICIO INNER JOIN " +
            //             " siat_grupo_servicios_gastos_erp ON siat_servicios_gastos_erp.siat_grupo_servicios_gastos_erp_id = siat_grupo_servicios_gastos_erp.siat_grupo_servicios_gastos_erp_id " +
            //             " WHERE ISNULL((charindex('CP-', Vista_Gastos_viaje_Erp.DESCRIPCION COLLATE SQL_Latin1_General_CP1_CI_AS)), 0) > 0 and " +
            //             " (convert(varchar, " + idCotizacion + ") = convert(varchar, (ISNULL((substring(Vista_Gastos_viaje_Erp.DESCRIPCION COLLATE SQL_Latin1_General_CP1_CI_AS, " +
            //             " ISNULL((charindex('CP-', Vista_Gastos_viaje_Erp.DESCRIPCION COLLATE SQL_Latin1_General_CP1_CI_AS)) + 3, 0), " +
            //             " isnull((charindex('CP-', Vista_Gastos_viaje_Erp.DESCRIPCION COLLATE SQL_Latin1_General_CP1_CI_AS)) + 3, 0))), 0)))) " +
            //             " group by siat_grupo_servicios_gastos_erp.siat_grupo_servicios_gastos_erp_id";
            //}
            //else
            //{
            //    sql = "SELECT       SUM(Vista_Gastos_viaje_Erp.VLR_BRUTO_ALT) as valor, siat_grupo_servicios_gastos_erp.siat_grupo_servicios_gastos_erp_id as grupo " +
            //             " FROM siat_servicios_gastos_erp INNER JOIN " +
            //             " Vista_Gastos_viaje_Erp ON siat_servicios_gastos_erp.id_servicio_erp COLLATE SQL_Latin1_General_CP1_CI_AS = Vista_Gastos_viaje_Erp.ID_SERVICIO INNER JOIN " +
            //             " siat_grupo_servicios_gastos_erp ON siat_servicios_gastos_erp.siat_grupo_servicios_gastos_erp_id = siat_grupo_servicios_gastos_erp.siat_grupo_servicios_gastos_erp_id " +
            //             " WHERE ISNULL((charindex('CP-', Vista_Gastos_viaje_Erp.DESCRIPCION COLLATE SQL_Latin1_General_CP1_CI_AS)), 0) > 0 and " +
            //             " (convert(varchar, " + idCotizacion + ") = convert(varchar, (ISNULL((substring(Vista_Gastos_viaje_Erp.DESCRIPCION COLLATE SQL_Latin1_General_CP1_CI_AS, " +
            //             " ISNULL((charindex('CP-', Vista_Gastos_viaje_Erp.DESCRIPCION COLLATE SQL_Latin1_General_CP1_CI_AS)) + 3, 0), " +
            //             " isnull((charindex('CP-', Vista_Gastos_viaje_Erp.DESCRIPCION COLLATE SQL_Latin1_General_CP1_CI_AS)) + 3, 0))), 0)))) " +
            //             " group by siat_grupo_servicios_gastos_erp.siat_grupo_servicios_gastos_erp_id";
            //}


            sql = "SELECT   SUM(convert(numeric(18,3),Vista_Gastos_viaje_Erp.VLR_BRUTO)) AS valor, siat_grupo_servicios_gastos_erp.siat_grupo_servicios_gastos_erp_id as grupo " +
                        " FROM siat_servicios_gastos_erp INNER JOIN " +
                        " Vista_Gastos_viaje_Erp ON siat_servicios_gastos_erp.id_servicio_erp COLLATE SQL_Latin1_General_CP1_CI_AS = Vista_Gastos_viaje_Erp.ID_SERVICIO INNER JOIN " +
                        " siat_grupo_servicios_gastos_erp ON siat_servicios_gastos_erp.siat_grupo_servicios_gastos_erp_id = siat_grupo_servicios_gastos_erp.siat_grupo_servicios_gastos_erp_id " +
                        " WHERE ISNULL((charindex('CP-', Vista_Gastos_viaje_Erp.DESCRIPCION COLLATE SQL_Latin1_General_CP1_CI_AS)), 0) > 0 and " +
                        " (convert(varchar, " + idCotizacion + ") = convert(varchar, (ISNULL((substring(Vista_Gastos_viaje_Erp.DESCRIPCION COLLATE SQL_Latin1_General_CP1_CI_AS, " +
                        " ISNULL((charindex('CP-', Vista_Gastos_viaje_Erp.DESCRIPCION COLLATE SQL_Latin1_General_CP1_CI_AS)) + 3, 0), " +
                        " isnull((charindex('CP-', Vista_Gastos_viaje_Erp.DESCRIPCION COLLATE SQL_Latin1_General_CP1_CI_AS)) + 3, 0))), 0)))) " +
                        " group by siat_grupo_servicios_gastos_erp.siat_grupo_servicios_gastos_erp_id";

            dt = BdDatos.CargarTabla(sql);
            return dt;                
        }

        public string consultarPais(int idPais)
        {
            string pais = "";
            DataTable dt = new DataTable(); 
            string sql = "SELECT        pai_nombre FROM pais WHERE(pai_id = " + idPais + ")";
            dt = BdDatos.CargarTabla(sql);
            if (dt.Rows.Count > 0)
            {
                pais = dt.Rows[0]["pai_nombre"].ToString();
            }
            return pais;
        }

        public DataTable consultarPaisId(int cotizacion)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT        siat_cotizacion.siat_cotizacion_id, pais.pai_id as pais, siat_cotizacion.dias as dias " +
                         " FROM pais INNER JOIN " +
                         " cliente ON pais.pai_id = cliente.cli_pai_id INNER JOIN " +
                         " siat_cotizacion ON cliente.cli_id = siat_cotizacion.cliente_id WHERE siat_cotizacion.siat_cotizacion_id = "+cotizacion+"";
            dt = BdDatos.CargarTabla(sql);            
            return dt;
        }

        public decimal consultarTRM(int cotizacion)
        {
            decimal trm = 1;
            DataTable dt = new DataTable();
            string sql = "SELECT        TOP (1) convert(decimal(18,3),Vista_Gastos_viaje_Erp.TASA_LOCAL) as tasa " +
                         " FROM siat_servicios_gastos_erp INNER JOIN " +
                         " Vista_Gastos_viaje_Erp ON " +
                         " siat_servicios_gastos_erp.id_servicio_erp COLLATE SQL_Latin1_General_CP1_CI_AS = Vista_Gastos_viaje_Erp.ID_SERVICIO INNER JOIN " +
                         " siat_grupo_servicios_gastos_erp ON " +
                         " siat_servicios_gastos_erp.siat_grupo_servicios_gastos_erp_id = siat_grupo_servicios_gastos_erp.siat_grupo_servicios_gastos_erp_id " +
                         " WHERE(ISNULL(CHARINDEX('CP-', Vista_Gastos_viaje_Erp.DESCRIPCION), 0) > 0) AND(CONVERT(varchar, "+ cotizacion + ") = CONVERT(varchar, " +
                         " ISNULL(SUBSTRING(Vista_Gastos_viaje_Erp.DESCRIPCION, ISNULL(CHARINDEX('CP-', Vista_Gastos_viaje_Erp.DESCRIPCION) + 3, 0), ISNULL(CHARINDEX('CP-', " +
                         " Vista_Gastos_viaje_Erp.DESCRIPCION) + 3, 0)), 0))) AND(Vista_Gastos_viaje_Erp.TASA_LOCAL > 1)";
            dt = BdDatos.CargarTabla(sql);
            if (dt.Rows.Count > 0)
            {
                trm = Convert.ToDecimal(dt.Rows[0]["tasa"]);
            }
            return trm;
        }


        public DataTable Consultar_DiasConsumidos(string of)
        {
            string sql = "SELECT siat_dias_consumidos " +
                        " FROM cierre_comercial " +
                        " WHERE cierre_ofs_desc='" + of + "'";
            return BdDatos.CargarTabla(sql);
        }
    }
}