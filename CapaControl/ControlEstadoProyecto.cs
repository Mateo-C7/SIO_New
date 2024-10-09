using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using CapaDatos;
namespace CapaControl
{
    public class ControlEstadoProyecto
    { 
        public DataTable traerIdEntrega(int fup, String version) 
        {
            String sql = "SELECT fup_enc_entrada_cotizacion.eect_id AS idEntrada, fup_estado_proceso.ep_desc_estado AS estado, fup_estado_proceso.ep_id AS idEstAnt,fup_enc_entrada_cotizacion.eect_fup_id AS fup, fup_enc_entrada_cotizacion.eect_vercot_id AS version "
                     + " FROM fup_enc_entrada_cotizacion INNER JOIN "
                     + " fup_estado_proceso ON fup_enc_entrada_cotizacion.eect_estado_proc = fup_estado_proceso.ep_id "
                     + " WHERE (fup_enc_entrada_cotizacion.eect_fup_id = " + fup + ") AND (fup_enc_entrada_cotizacion.eect_vercot_id = '" + version + "')";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public DataTable traerEstados(int idEstado)
        {
            String sql = "SELECT ep_id AS idEstado, ep_desc_estado AS estado FROM fup_estado_proceso WHERE ep_id < " + idEstado + "";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public DataTable traerMotivos()
        {
            String sql = "SELECT  mce_id AS idMotivo,  mce_desc AS motivo FROM fup_motivo_cambioestado WHERE (mce_activo = 1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public String actualizarEntrega(int fup, String version, int estado)
        {
            String mensaje = "";
            int inactivaVb = 0;
            String sql = "";
            
            try
            {
                if (estado < 4)
                {
                    sql = "UPDATE fup_enc_entrada_cotizacion SET  eect_estado_proc = " + estado + " , eect_visto_bueno = 0 WHERE (eect_fup_id = " + fup + ") AND (eect_vercot_id = '" + version + "')";
                }
                else
                {
                    sql = "UPDATE fup_enc_entrada_cotizacion SET  eect_estado_proc = " + estado + " WHERE (eect_fup_id = " + fup + ") AND (eect_vercot_id = '" + version + "')";
                }


                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR Al ACTUALIZAR (fup_enc_entrada_cotizacion)";
            }
            return mensaje;
        }

        public String insertEstadoLog(int idEntrada, int estInicial, int estFinal, int motivo, String obser, String usu)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO fup_estado_log (el_eect_idEntrada, el_estadoIncial, el_estadoFinal, el_fecha, el_mce_id, el_obser, el_usu) "
                + " VALUES(" + idEntrada + ", " + estInicial + ", " + estFinal + ", SYSDATETIME() ," + motivo + ",'" + obser + "','" + usu + "')";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR Al INSERTAR (fup_estado_log)";
            }
            return mensaje;
        }

        public DataTable cargarTablaCambio(int idEntrada) {
            String sql = "SELECT     TOP (200) fup_enc_entrada_cotizacion.eect_fup_id AS fup, fup_enc_entrada_cotizacion.eect_vercot_id AS version, fup_estado_log.el_fecha AS fecha, "
                      + " fup_estado_log.el_obser AS obser, fup_estado_log.el_usu AS usuario, fup_motivo_cambioestado.mce_desc AS motivo, " 
                      + " fup_estado_proceso_1.ep_desc_estado AS EstIncial, fup_estado_proceso.ep_desc_estado AS EstFinal "
                      + " FROM         fup_estado_log INNER JOIN "
                      + " fup_motivo_cambioestado ON fup_estado_log.el_mce_id = fup_motivo_cambioestado.mce_id INNER JOIN "
                      + " fup_estado_proceso AS fup_estado_proceso_1 ON fup_estado_log.el_estadoIncial = fup_estado_proceso_1.ep_id INNER JOIN "
                      + " fup_estado_proceso ON fup_estado_log.el_estadoFinal = fup_estado_proceso.ep_id INNER JOIN "
                      + " fup_enc_entrada_cotizacion ON fup_estado_log.el_eect_idEntrada = fup_enc_entrada_cotizacion.eect_id "
                      + " WHERE     (fup_estado_log.el_eect_idEntrada = " + idEntrada + ") ORDER BY fecha DESC";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
    }
}
