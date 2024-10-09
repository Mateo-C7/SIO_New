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
    public class ControlActaEntrega
    {
        public DataTable cargarContatosCliente(int idCliente) 
        {
            String sql = "SELECT TOP (200) contacto_cliente.ccl_id AS idCont, contacto_cliente.ccl_nombre + ' ' + contacto_cliente.ccl_nombre2 + ' ' + contacto_cliente.ccl_apellido + ' ' + contacto_cliente.ccl_apellido2 AS nombre, "
                     + " contacto_cliente.ccl_cargo_nombre AS cargoCont"
                     + " FROM  contacto_cliente INNER JOIN "
                     + " cliente ON contacto_cliente.ccl_cli_id = cliente.cli_id "
                     + " WHERE (contacto_cliente.ccl_cli_id = " + idCliente + ") ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public DataTable cargarEmpleadosForsa(int idRol)
        {
            String sql = "SELECT     TOP (100) empleado.emp_nombre_mayusculas + ' ' + empleado.emp_apellidos_mayusculas AS nombre, rolapp.rap_nombre AS nomRol, "
                       + " empleado.emp_usu_num_id AS idEmpleado "
                       + " FROM         empleado INNER JOIN "
                       + " usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id INNER JOIN "
                       + " rolapp ON usuario.usu_rap_id = rolapp.rap_id "
                       + " WHERE     (usuario.usu_activo = 1) AND (usuario.usu_rap_id = " + idRol + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public DataTable cargarComboNomRoles()
        {
            String sql = "SELECT rap_id AS idRol, rap_nombre AS nomRol FROM rolapp WHERE (rap_id = 2) OR (rap_id = 4) OR (rap_id = 9) OR (rap_id = 16) OR (rap_id = 18) OR (rap_id = 24) OR (rap_id = 25) OR (rap_id = 30)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public int existeActaModificar(int idOfa)
        {
            String sql = "SELECT COUNT(ae_id) AS hay FROM  fup_acta_entrega WHERE (ae_idOfa = " + idOfa + ") AND (ae_finalizado = 0)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            int numActa = 0;
            foreach (DataRow row in consulta.Rows)
            {
                numActa = int.Parse(row["hay"].ToString());
            }
            return numActa;
        }

        public int existeActa(int idOfa)
        {
            String sql = "SELECT COUNT(ae_id) AS hay FROM  fup_acta_entrega WHERE (ae_idOfa = " + idOfa + ") AND (ae_finalizado = 1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            int numActa = 0;
            foreach (DataRow row in consulta.Rows)
            {
                numActa = int.Parse(row["hay"].ToString());
            }
            return numActa;
        }

        public DataTable traerDatosModificarTextos(int idOfa)
        {
            String sql = "SELECT     fup_acta_entrega.ae_tec_equipos AS tecEquipos, fup_acta_entrega.ae_crono_despacho AS cronoDespacho, " 
                      + " fup_acta_entrega.ae_condi_embalaje AS condEmbalaje, fup_acta_entrega.ae_cond_financiera AS condFinanciera, " 
                      + " fup_acta_entrega.ae_responsable_obra AS idContacto, "
                      + " contacto_cliente.ccl_nombre + ' ' + contacto_cliente.ccl_nombre2 + ' ' + contacto_cliente.ccl_apellido + ' ' + contacto_cliente.ccl_apellido2 AS nombre,ae_tec_referencia as referencia "
                      + " FROM fup_acta_entrega INNER JOIN contacto_cliente ON fup_acta_entrega.ae_responsable_obra = contacto_cliente.ccl_id "
                      +" WHERE (fup_acta_entrega.ae_idOfa = " + idOfa + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public DataTable datosEmpleadosForsa(int idOfa)
        {
            String sql = "SELECT empleado.emp_apellidos_mayusculas + ' ' + empleado.emp_nombre_mayusculas AS nombre, "
                 + " fup_acta_entrega_invol.aeinvol_emp_forsa_id AS idEmpForsa "
                 + " FROM fup_acta_entrega INNER JOIN "
                 + " fup_acta_entrega_invol ON fup_acta_entrega.ae_id = fup_acta_entrega_invol.aeinvol_fup_ae_id INNER JOIN "
                 + " empleado ON fup_acta_entrega_invol.aeinvol_emp_forsa_id = empleado.emp_usu_num_id "
                 + " WHERE (fup_acta_entrega.ae_idOfa = " + idOfa + ") ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public DataTable datosContactoCliente(int idOfa)
        {
            String sql = "SELECT fup_acta_entrega_invol.aeinvol_contacto_id AS idContacto, "
                       + " contacto_cliente.ccl_nombre + ' ' + contacto_cliente.ccl_nombre2 + ' ' + contacto_cliente.ccl_apellido + ' ' + contacto_cliente.ccl_apellido2 AS nombre "
                       + " FROM fup_acta_entrega INNER JOIN "
                       + " fup_acta_entrega_invol ON fup_acta_entrega.ae_id = fup_acta_entrega_invol.aeinvol_fup_ae_id INNER JOIN "
                       + " contacto_cliente ON fup_acta_entrega_invol.aeinvol_contacto_id = contacto_cliente.ccl_id "
                       + " WHERE (fup_acta_entrega.ae_idOfa = " + idOfa + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public String insertDatos(int idOfa, String tecEquipos, String cronoDesp, String condEmbalaje, String condFinanciera, int reponsable, String usuario, string ordenes)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO fup_acta_entrega (ae_idOfa, ae_tec_equipos,ae_crono_despacho,ae_condi_embalaje,ae_cond_financiera,ae_responsable_obra,ae_fecha_crea,ae_finalizado,ae_usu_crea,ae_tec_referencia) "
                    + " VALUES(" + idOfa + ",'" + tecEquipos + "','" + cronoDesp + "','" + condEmbalaje + "','" + condFinanciera + "'," + reponsable + ",SYSDATETIME(),0,'" + usuario + "', '" +ordenes+"' )";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (fup_acta_entrega)";
            }
            return mensaje;
        }

        public String insertListasContaCliente(int idOfa, int idCont)
        {
            String mensaje = "";
            try
            {
                int idActa = numeroActaEntrega(idOfa);
                String sql = "INSERT INTO fup_acta_entrega_invol (aeinvol_fup_ae_id,aeinvol_contacto_id) VALUES(" + idActa + "," + idCont + ")";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (fup_acta_entrega_invol)";
            }
            return mensaje;
        }

        public String insertListasEmpForsa(int idOfa, int idEmp)
        {
            String mensaje = "";
            try
            {
                int idActa = numeroActaEntrega(idOfa);
                String sql = "INSERT INTO fup_acta_entrega_invol (aeinvol_fup_ae_id,aeinvol_emp_forsa_id) VALUES(" + idActa + "," + idEmp + ")";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (fup_acta_entrega_invol)";
            }
            return mensaje;
        }

        public int numeroActaEntrega(int idOfa) {
            String sql = "SELECT ae_id AS idActa FROM fup_acta_entrega WHERE (ae_idOfa = " + idOfa + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            int idO = 0;
            foreach (DataRow row in consulta.Rows)
            {
                idO = int.Parse(row["idActa"].ToString());
            }
            return idO;
        }

        public String actualizarDatos(int idOfa, String tecEquipos, String cronoDesp, String condEmbalaje, String condFinanciera, int reponsable, String usuario, string ordenes)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE fup_acta_entrega SET ae_tec_equipos = '" + tecEquipos + "' ,ae_crono_despacho = '" + cronoDesp + "', "
                    + " ae_condi_embalaje = '" + condEmbalaje + "',ae_cond_financiera = '" + condFinanciera + "', "
                    + " ae_responsable_obra = '" + reponsable + "',ae_fecha_crea = SYSDATETIME(), ae_usu_crea = '" + usuario + "', ae_tec_referencia = '"+ordenes+"' WHERE ae_idOfa = " + idOfa + "";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR Al ACTUALIZAR (fup_acta_entrega)";
            }
            return mensaje;
        }

        public String actualizarListas(int idOfa, String filtro)
        {
            String mensaje = "";
            try
            {
                int idActa = numeroActaEntrega(idOfa);
                String sql = "DELETE fup_acta_entrega_invol WHERE (aeinvol_fup_ae_id = " + idActa + ") AND (" + filtro + " > 0)";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR Al ACTUALIZAR (fup_acta_entrega_invol(" + filtro + "))";
            }
            return mensaje;
        }

        public String cerrarActa(int idOfa) {
            String mensaje = "";
            try
            {
                int idActa = numeroActaEntrega(idOfa);
                String sql = "UPDATE fup_acta_entrega SET ae_finalizado = 1, ae_fecha_finalizado = SYSDATETIME() WHERE ae_id = " + idActa + "";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR Al ACTUALIZAR (fup_acta_entrega(ae_finalizado))";
            }
            return mensaje;
        }

        public DataTable cargarComboOrdenes(int fup, String version) {
            String sql = "SELECT  Id_Ofa AS idofa, Numero + '-' + ano AS orden FROM Orden WHERE (Yale_Cotiza = " + fup + ") AND (ord_version = '" + version + "') AND (letra = '1')";
 
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
    }
}

