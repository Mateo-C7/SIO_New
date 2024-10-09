using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CapaDatos;

namespace CapaControl
{
    public class ControlListaChequeo
    {
        #region "Definiciones"
        private DataTable mvarTblEncuesta;
        private DataTable mvarTblCapitulos;
        #endregion

        #region "Propiedades"
        public DataTable tblEncuesta
        {
            get { return mvarTblEncuesta; }
            set { mvarTblEncuesta = value; }
        }

        public DataTable tblCapitulos
        {
            get { return mvarTblCapitulos; }
            set { mvarTblCapitulos = value; }
        }
        #endregion
        
        #region "Metodos"
		public ControlListaChequeo()
		{			
			mvarTblEncuesta = new DataTable();
			mvarTblCapitulos = new DataTable();
		}
        #endregion

        public SqlDataReader ConsultarTecnico()
        {
            string sql;

            sql = "SELECT DISTINCT empleado.emp_usu_num_id, empleado.emp_nombre + ' ' + empleado.emp_apellidos AS Nombre " +
                "FROM empleado INNER JOIN empleado_cargo ON empleado.emp_usu_num_id = empleado_cargo.emc_emp_usu_num_id " +
                "INNER JOIN cargo ON empleado_cargo.emc_car_id = cargo.car_id " +
                "WHERE (empleado_cargo.emc_car_id = 64) AND (empleado.emp_activo = 1) OR (empleado_cargo.emc_car_id = 20) " +
                "AND (empleado.emp_activo = 1)" +
                "ORDER BY Nombre";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA DE FOTOGRAFIAS LISTA DE CHEQUEO
        public DataSet ConsultarFotografiaLista(int fup, string ver)
        {
            string sql;
            sql = "SELECT id_plano, plano_version VERS, plano_consecutivo CONS, ISNULL(tp.ftpr_descripcion,'NO DEFINIDO') [TIPO_PROY] " +
                ",ISNULL(ta.tan_desc_esp,'NO DEFINIDO') [TIPO_DOC] ,plano_of_referencia  [OF_REFERENCIA] " +
                ",p.plano_ruta_archivo + p.plano_nombre_real AS [RUTA] ,plano_nombre_real [NOMBRE] " +
                "FROM Plano p  LEFT OUTER JOIN fup_tipo_proyectos tp ON tp.ftpr_id = p.plano_tipo_proyecto_id " +
                "LEFT OUTER JOIN fup_tipo_anexo ta ON ta.tan_id = p.plano_tipo_anexo_id " +
                "WHERE id_fup_plano = " + fup + " AND plano_version = '" + ver + "' " +
                "AND p.plano_tipo_anexo_id = 99 ORDER BY id_plano DESC";
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }

        //CREACION DE LA LISTA DE CHEQUEO
        public int IngEncabezadoLista(int Lista, string NumOrden, int Estado, 
            string TipoArmado, string Cedula, string Usuario, string Mail)
        {
            // Parametros de la BBDD
            int Id = -1;

            SqlParameter[] sqls = new SqlParameter[7];
            sqls[0] = new SqlParameter("pIDLista ", Lista);
            sqls[1] = new SqlParameter("pNumOrden", NumOrden);
            sqls[2] = new SqlParameter("pEstado", Estado);
            sqls[3] = new SqlParameter("pTipoArmado", TipoArmado);
            sqls[4] = new SqlParameter("pTec_cedula", Cedula);
            sqls[5] = new SqlParameter("pUsuario", Usuario);
            sqls[6] = new SqlParameter("pEmail", Mail);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_ListaChequeo", con))
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

        public void CargaTabla(string IdEncuesta, string Capitulo, int IdLista)
        {
            mvarTblEncuesta.Clear();
            System.Text.StringBuilder sqlString = new System.Text.StringBuilder();
            sqlString.AppendLine("SELECT");
            sqlString.AppendLine(" 'consecutivo'+cast(P.plc_id as varchar(max) ) as nombreC1,");
            sqlString.AppendLine(" P.plc_id as consecutivo,");
            sqlString.AppendLine(" 'descripcion'+cast(P.plc_id as varchar(max) ) as nombreC2,");
            sqlString.AppendLine(" P.plc_pregunta as descripcion,");
            sqlString.AppendLine(" 'conf'+cast(P.plc_id as varchar(max) ) as nombreC3,");
            sqlString.AppendLine(" ISNULL(D.lcd_confirma,0) as conf,");
            sqlString.AppendLine(" 'memo'+cast(P.plc_id as varchar(max) ) as nombreC4,");
            sqlString.AppendLine(" ISNULL(D.lcd_memo,'') as memo,");
            sqlString.AppendLine(" 'observacion'+cast(P.plc_id as varchar(max)) as nombreC5,");
            sqlString.AppendLine(" ISNULL(D.lcd_observacion,'') as observacion");
            sqlString.AppendLine("FROM fup_preguntas_lista_cheq P ");
            sqlString.AppendLine(" LEFT OUTER JOIN fup_lista_cheq_detalle D ON D.lcd_pregunta_id = P.plc_id");
            sqlString.AppendLine("  AND (D.lcd_lista_id = " + IdLista.ToString() + " OR D.lcd_lista_id IS NULL) ");
            sqlString.AppendLine("WHERE P.plc_grupo = '" + Capitulo + "'");
            BdDatos.DameTabla(ref mvarTblEncuesta, sqlString.ToString());
        }

        public void CargaCapitulos()
        {
            mvarTblCapitulos.Clear();
            BdDatos.DameTabla(ref mvarTblCapitulos, "select plc_id_grupo as idcapitulo, plc_grupo as abreviatura, plc_grupo as descripcion from fup_preguntas_lista_cheq group by plc_id_grupo, plc_grupo");
        }

        public void GrabarEncuesta(string usuario, string cadenamerge)
        {
            System.Text.StringBuilder sqlString = new System.Text.StringBuilder();
            sqlString.AppendLine("MERGE INTO fup_lista_cheq_detalle AS T");
            sqlString.AppendLine("USING (VALUES " + cadenamerge + ")");
            sqlString.AppendLine("       AS S (IdLista,IdPregunta,conf,memo,Obs)");
            sqlString.AppendLine("ON T.lcd_lista_id = s.IdLista AND T.lcd_pregunta_id = s.IdPregunta");
            sqlString.AppendLine("WHEN MATCHED THEN");
            sqlString.AppendLine("UPDATE ");
            sqlString.AppendLine("   SET [lcd_confirma] = s.conf");
            sqlString.AppendLine("      ,[lcd_memo] = s.memo");
            sqlString.AppendLine("      ,[lcd_observacion] = s.obs");
            sqlString.AppendLine("      ,[lcd_fecha_actualiza] = GETDATE()");
            sqlString.AppendLine("      ,[lcd_usu_actualiza] = '" + usuario + "'");
            sqlString.AppendLine("WHEN NOT MATCHED BY TARGET THEN");
            sqlString.AppendLine(" INSERT ([lcd_lista_id] ,[lcd_pregunta_id] ,[lcd_confirma] ,[lcd_memo]");
            sqlString.AppendLine("           ,[lcd_observacion] ,[lcd_fecha_crea] ,[lcd_usu_crea] ,[lcd_fecha_actualiza]");
            sqlString.AppendLine("           ,[lcd_usu_actualiza])");
            sqlString.AppendLine("     VALUES");
            sqlString.AppendLine("           (s.IdLista ,s.IdPregunta ,s.conf ,s.memo ,s.Obs ");
            sqlString.AppendLine("           ,GETDATE() ,'" + usuario + "' ,GETDATE() ,'" + usuario + "');");
            BdDatos.ejecutarSql(sqlString.ToString());
        }

        public int CerrarConexion()
        {
            return BdDatos.desconectar();
        }

        public int ActualizarEstado(int idofa, int estado)
        {
            string sql;

            sql = "UPDATE fup_lista_chequeo SET lch_Estado = " + estado + " " +
                  "WHERE lch_ofa_id = " + idofa + " ";

            return BdDatos.Actualizar(sql);
        }

        public SqlDataReader CorreosCalidad()
        {
            string sql;

            sql = "SELECT empleado.emp_nombre + ' ' + empleado.emp_apellidos AS nombre, empleado.emp_correo_electronico " +
                  "FROM empleado INNER JOIN usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id " +
                  "WHERE (usuario.usu_rap_id = 18) AND (usuario.usu_activo = 1) AND (empleado.emp_estado_laboral = 'ACTIVO') AND " +
                  "(empleado.emp_correo_electronico IS NOT NULL)";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader CorreosListaAdmin()
        {
            string sql;

            sql = "SELECT emp_nombre + ' ' + emp_apellidos AS nombre, emp_correo_electronico, emp_mail_lista, emp_mail_admin " +
                  "FROM empleado " +
                  "WHERE (emp_estado_laboral = 'ACTIVO') AND (emp_correo_electronico IS NOT NULL) AND (emp_mail_lista = 1) OR " +
                  "(emp_estado_laboral = 'ACTIVO') AND (emp_correo_electronico IS NOT NULL) AND (emp_mail_admin = 1)";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarEstado(int OFA)
        {
            string sql;

            sql = "SELECT lch_Estado, lch_mail_crea FROM fup_lista_chequeo WHERE lch_ofa_id = " + OFA + "";

            return BdDatos.ConsultarConDataReader(sql);
        }
    }
}
