using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaDatos;

namespace CapaControl
{
    public class ControlPlaneadorCotizacion
    {
        //CONSULTAR PERSONAL STV
        public SqlDataReader ConsultarPersonalSTV()
        {
            string sql;
            sql = "SELECT emp_usu_num_id, emp_nombre + ' ' + emp_apellidos AS Nombre " +
            "FROM usuario INNER JOIN empleado ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id " +
            "WHERE (usuario.usu_rap_id = 25) AND (usuario.usu_activo = 1) OR (usuario.usu_rap_id = 26) " +
            "AND (usuario.usu_activo = 1) " +
            "ORDER BY Nombre";
            return BdDatos.ConsultarConDataReader(sql);
        }
        public int CerrarConexion()
        {
            return BdDatos.desconectar();
        }
        //CONSULTAR PERSONAL COTIZACION
        public SqlDataReader ConsultarPersonalCotizacion()
        {
            string sql;
            sql = "SELECT emp_usu_num_id, emp_nombre + ' ' + emp_apellidos AS Nombre, " +
                "usuario.usu_login, empleado.emp_reg_id, empleado.emp_activo " +
                "FROM  usuario INNER JOIN empleado ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id " +
                "WHERE (usuario.usu_activo = 1)   AND (usuario.usu_resp_cot = 1) " +
                "ORDER BY Nombre";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTAR PERSONAL COTIZACION
        public SqlDataReader ConsultarProceso()
        {
            string sql;
            sql = "SELECT        are_id, are_nombre  FROM area  WHERE (are_planCotizador = 1) order by are_nombre ";
            return BdDatos.ConsultarConDataReader(sql);
        }
        //CONSULTA VERSION DEL FUP ACTIVA
        public SqlDataReader PoblarVersion(int FUP)
        {
            string sql;
            sql = "SELECT eect_id, eect_vercot_id, isnull(eect_observacion,'') FROM fup_enc_entrada_cotizacion " +
                "WHERE eect_fup_id = " + FUP + " AND eect_activo = 1 and eect_fec_VistoBueno IS NOT NULL ORDER BY eect_id DESC";

            return BdDatos.ConsultarConDataReader(sql);
        }
        //REPROGRAMACION COTIZACION
        public int ReprogramarCotizacion(int fup, string ver)
        {
            string sql;

            sql = "UPDATE fup_enc_entrada_cotizacion SET entcot_estado = 'Pendiente' " +
                  "WHERE eect_fup_id = " + fup + " AND eect_vercot_id = '" + ver + "'";

            return BdDatos.Actualizar(sql);
        }
        public SqlDataReader COnsultarModulaciones(int fup, string ver)
        {
            string sql;

            sql = "SELECT     eect_cant_modulacion,isnull( CONVERT(varchar(10), eect_fec_Ini_cot, 103),'') , isnull(CONVERT(varchar(10), eect_fec_programada, 103),'') ,isnull( eect_estado, 'Pendiente') FROM fup_enc_entrada_cotizacion  " +
                  "WHERE eect_fup_id = " + fup + " AND eect_vercot_id = '" + ver + "'";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarFechapolitica(int fup, string ver)
        {
            string sql;

            //sql = "SELECT  CONVERT(varchar(10),eect_FecPolitica,103)      FROM fup_enc_entrada_cotizacion  " +
            //      "WHERE eect_fup_id = " + fup + " AND eect_vercot_id = '" + ver + "'";

            sql = "SELECT CONVERT(varchar(10), fup_enc_entrada_cotizacion.eect_FecPolitica, 103) , 'Clasificacion Cliente: '+Vista_ClasificacionCliente.ClasificaCliente,   " +
                 " pais.pai_nombre + ' / ' + cliente.cli_nombre , obra.obr_nombre  " +
              "  FROM cliente INNER JOIN  " +
                       "  formato_unico ON cliente.cli_id = formato_unico.fup_cli_id INNER JOIN  " +
                       "  pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN  " +
                       " fup_enc_entrada_cotizacion ON formato_unico.fup_id = fup_enc_entrada_cotizacion.eect_fup_id INNER JOIN  " +
                       "  obra ON formato_unico.fup_obr_id = obra.obr_id LEFT OUTER JOIN  " +
                        " Vista_ClasificacionCliente ON CASE WHEN cliente.cli_ClasificacionMcdo = 0 THEN 7 ELSE ISNULL(cliente.cli_ClasificacionMcdo, 7) END = Vista_ClasificacionCliente.IdClasificaCliente  " +
                        " WHERE eect_fup_id = " + fup + " AND eect_vercot_id = '" + ver + "'";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarComparaPolitica(int fup, string ver, string FechaPrograma)
        {
            string sql;

            sql = "SELECT  CONVERT(varchar(10),eect_FecPolitica,103)      FROM fup_enc_entrada_cotizacion  " +
                  "WHERE eect_fup_id = " + fup + " AND eect_vercot_id = '" + ver + "'";

            return BdDatos.ConsultarConDataReader(sql);
        }


        //INACTIVAR COTIZACION
        public int InactivarCotizacion(int fup, string ver, string observaciones )
        {
            string sql;

            sql = "UPDATE fup_enc_entrada_cotizacion SET eect_activo = 0 , eect_estado_proc = 11 , eect_observacion = '"  + observaciones +"' " +
                  "WHERE eect_fup_id = " + fup + " AND eect_vercot_id = '" + ver + "'";

            return BdDatos.Actualizar(sql);
        }
        //INGRESO DE PLANEADOR DE COTIZACIONES
        public int IngPlanCot(int numfup, string ver, string asignado, string resp, string he,
            int cantrec, string fecprog, string observa, int empleado, string fecinicio,int area)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;
            
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[11];
            sqls[0] = new SqlParameter("pFupID", numfup);
            sqls[1] = new SqlParameter("pVersion", ver);
            sqls[2] = new SqlParameter("pAsignado", asignado);
            sqls[3] = new SqlParameter("pResponsable", resp);
            sqls[4] = new SqlParameter("pHorasExt", he);
            sqls[5] = new SqlParameter("pCantRec", cantrec);
            sqls[6] = new SqlParameter("pFecProgramada", fecprog);
            sqls[7] = new SqlParameter("pObservacion", observa);
            sqls[8] = new SqlParameter("@pEmpleado", empleado);
            sqls[9] = new SqlParameter("pFechaInicial", fecinicio);
            sqls[10] = new SqlParameter("@pArea", area);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_entcot_Ingenieria", con))
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
        public SqlDataReader consultarEmailCotizacion(int cedula)
        {
            string sql;
            sql = "SELECT usuario.usu_emp_usu_num_id, empleado.emp_nombre + ' ' + empleado.emp_apellidos AS Nombre, usuario.usu_login, empleado.emp_correo_electronico " +
            "FROM usuario INNER JOIN empleado ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id " +
            "WHERE (usu_emp_usu_num_id = " + cedula + ")" +
            "ORDER BY Nombre";
            return BdDatos.ConsultarConDataReader(sql);
        }
        //OBTENER EMAIL ADMIN
        public SqlDataReader ObtenerMailAdmin()
        {
            string sql = "SELECT emp_correo_electronico FROM empleado WHERE emp_mail_admin = 1 AND emp_activo = 1 ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //OBTENER EMAIL ADMIN
        public SqlDataReader ObtenerMailEspecialistas()
        {
            string sql = "SELECT        empleado.emp_correo_electronico FROM empleado INNER JOIN "+
                         " usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id "+
                        " WHERE(empleado.emp_activo = 1) AND(usuario.usu_rap_id = 26) AND(usuario.usu_activo = 1) AND(usuario.coordinadorCotizaciones = 1)";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA DE PLANEADOR
        public DataSet ConsultarPlaneador(string fecini, string fecfin, string estado, string cot)
        {
            DataSet dsPlan = new DataSet();
            DateTime FechaIni, FechaFin;

            FechaIni = Convert.ToDateTime(fecini);
            FechaFin = Convert.ToDateTime(fecfin);

            SqlParameter[] sqls = new SqlParameter[4];
            sqls[0] = new SqlParameter("@estado ", estado);
            sqls[1] = new SqlParameter("@fechaini", FechaIni);
            sqls[2] = new SqlParameter("@fechafin", FechaFin);
            sqls[3] = new SqlParameter("@respcot", cot);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("PlanIngenieria", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;

                    da.Fill(dsPlan);
                }  
            }   
            return dsPlan;
        }
    }
}
