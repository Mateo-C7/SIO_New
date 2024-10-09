using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaDatos;
using System.Web;
//using AjaxControlToolkit.HTMLEditor.ToolbarButton;

namespace CapaControl
{
    public class ControlInicio
    {
        //CONSULTAR IDIOMA HOME
        public DataSet ConsultarIdiomaHome()
        {
            string sql;
            sql = "SELECT idioma_home.* FROM idioma_home";
            DataSet ds_idiomaHome = BdDatos.consultarConDataset(sql);
            return ds_idiomaHome;
        }
        
        //CONSULTA IDIOMA ESPAÑOL
        public SqlDataReader ConsultarIdiomaEspañol(int pos)
        {
            string sql;
            sql = "SELECT idiomini_español FROM idioma_inicio WHERE (idiomini_id = " + pos + ")";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA IDIOMA INGLES
        public SqlDataReader ConsultarIdiomaIngles(int pos)
        {
            string sql;
            sql = "SELECT idiomini_ingles FROM idioma_inicio WHERE (idiomini_id = " + pos + ")";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA IDIOMA PORTUGUES
        public SqlDataReader ConsultarIdiomaPortugues(int pos)
        {
            string sql;
            sql = "SELECT idiomini_portugues FROM idioma_inicio WHERE (idiomini_id = " + pos + ")";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //VERIFICAMOS EL LOGIN
        public int verificarLogin(string login)
        {
            SqlDataReader reader = null;
            login = login.Trim();
            int respuesta = 0;

            string sql = "SELECT usu_login FROM usuario WHERE usu_login = '" + login + "'AND usu_activo = 1";
            reader = BdDatos.ConsultarConDataReader(sql);
            if (reader.HasRows == true)
            {
                if (reader.Read())
                {
                    respuesta = 1;
                }
                    else
                {
                    respuesta = 0;
                }                   
            }

            reader.Close();
            this.CerrarConexion();

            return respuesta;  

        }

        public int CerrarConexion()
        {
            return BdDatos.desconectar();
        }

        //VERIFICAR LA CONTRASEÑA
        public string verificarContrasena(string login, string contrasena)
        {
            string sql;
            SqlDataReader reader = null;
            string respuesta = "";
            
            sql = "SELECT usu_passwd FROM usuario WHERE usu_login='" + login + "'";
            reader = BdDatos.ConsultarConDataReader(sql);
            if (reader.HasRows == true)
            {
                reader.Read();
                respuesta= reader.GetString(0);
            }
            else
            {
                respuesta= "";
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
            return respuesta;


        }

        //OBTENGO EL ROL ID DEL USUARIO
        public SqlDataReader obtenerRolByUsuLogin(string login)
        {
            string sql;
            int rol = 0, usuId = 0;
            SqlDataReader reader = null;
            sql = "SELECT usu_rap_id, usu_siif_id, CONVERT(INT,Isnull(usu_modificaPlazo,0)) usu_modificaPlazo FROM usuario WHERE usu_login='" + login + "'";
            return BdDatos.ConsultarConDataReader(sql);

            //reader = BdDatos.ConsultarConDataReader(sql);
            ////if (reader.HasRows == true)
            ////{
            ////    reader.Read();
            ////    rol = reader.GetInt32(0);
            ////    usuId = reader.GetInt32(1);
            ////}            
            ////reader.Close();
           
            //int cerrarCon = BdDatos.desconectar();
            //return reader;
        }

        //OBTENEMOS EL NOMBRE DEL ROL DEL USUARIO
        public string obtenerNombreRolByID(int rap_id)
        {
            SqlDataReader reader = null;
            string sql, nombreRol = "";
            sql = "SELECT rap_nombre FROM rolapp WHERE (rap_id=" + rap_id + ")";

            reader = BdDatos.ConsultarConDataReader(sql);
            if (reader.HasRows == true)
            {
                reader.Read();
                nombreRol = reader.GetString(0);
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();

            return nombreRol;
        }

        //FECHA DE ACTUALIZACIÓN DE LA CONTRASEÑA
        public SqlDataReader ObtenerFechaActualizacion(string seleccionado)
        {
            string sql;

            sql = "SELECT fecha_actualiza FROM usuario WHERE (usu_login = '" + seleccionado + "')";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public int Guardar_Log_Sesion(int idEmple, string usuario, string pagina)
        {
            string sql;

            sql = "INSERT INTO LOG_CONEXIONES  (LogCo_IdAplicacion, LogCo_IdEmpleado, LogCo_FeConex, LogCo_FeDesCon, LogCo_PlantaId,LogCo_Usuario,LogCo_Pagina) " +
                                   " VALUES  (1, "+ idEmple + ", SYSDATETIME(), NULL, 1,'" + usuario + "','" + pagina + "')";
            return BdDatos.ejecutarSql(sql);
        }

        public int Actualizar_Log_Sesion(int idEmpleado, string usuario, string pagina)
        {
            string sql;

            sql = "UPDATE  LOG_CONEXIONES " +
                   " SET LogCo_FeDesCon = SYSDATETIME(), LogCo_Usuario = '" + usuario + "', LogCo_Pagina ='"+ pagina + "' WHERE LogCo_IdEmpleado = " + idEmpleado + "" +
                                                        " AND LogCo_FeDesCon IS NULL" +
                                                        " AND LoCo_Id = (SELECT MAX(LC2.LoCo_Id) " +
                                                                       " FROM LOG_CONEXIONES LC2 " +
                                                                       " WHERE LogCo_IdEmpleado = " + idEmpleado + ")";
            return BdDatos.ejecutarSql(sql);

        }


        //CONSULTA PARA OBTENER SI ES ADMINISTRADOR DE GASTO
        public SqlDataReader ObtenerAdminGasto(string seleccionado)
        {
            string sql;

            sql = "SELECT usu_gasto, usu_emp_usu_num_id FROM usuario WHERE (usu_login = '" + seleccionado + "')";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA PARA RECUPERAR LA CONTRASEÑA DEL USUARIO
        public SqlDataReader recuperarPass(string seleccionado)
        {
            string sql;

            sql = "SELECT usu_login, usu_passwd, usu_rap_id FROM usuario WHERE usu_login = '" + seleccionado + "' AND usu_activo = 1 ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA PARA OBTENER EMAIL DEL USUARIO
        public SqlDataReader obtenerMailUsuario(string seleccionado)
        {
            string sql;

            sql = "SELECT emp_correo_electronico FROM  usuario INNER JOIN empleado ON usu_emp_usu_num_id = emp_usu_num_id " + 
                  "WHERE usu_login = '" + seleccionado + "' AND usu_activo = 1 ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA PARA OBTENER EMAIL DE USUARIO ROL 3
        public SqlDataReader obtenerMailRepresentante(string seleccionado)
        {
            string sql;

            sql = "SELECT rc_email FROM usuario INNER JOIN representantes_comerciales ON usu_siif_id = rc_usu_siif_id " + 
                "WHERE usu_login = '" + seleccionado + "' AND usu_activo = 1 ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //ACTUALIZAR CONTRASEÑA
        public int CambiarContrasena(string usuario, string contrasena, string nuevacontrasena)
        {
            string sql;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

            sql = "UPDATE usuario SET usu_passwd = '" + nuevacontrasena + "', fecha_actualiza = '" + fecha + "' " +
                  "WHERE usu_login = '" + usuario + "' AND usu_passwd = '" + contrasena + "' ";

            return BdDatos.Actualizar(sql);
        }

        public SqlDataReader ObtenerRepresentante(string seleccionado)
        {
            string sql;

            sql = "SELECT representantes_comerciales.rc_id, representantes_comerciales.rc_descripcion, representantes_comerciales.rc_email, pais_representante.pr_id_pais, " +
                  "pais.pai_nombre, usuario.usu_siif_id, isnull(usuario.infraestructura,0),   CAST(CASE WHEN usuario.usu_rap_id = 38 THEN 1 ELSE 0 END AS bit) AS servPostventa ,isnull(usuario.solicita_pallet,0) as solicitaPallet, isnull(usuario.IdCliente,0) as idClienteUsuario, usu_creaOF as CreaOf " +
                  "FROM usuario INNER JOIN representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN pais_representante ON " +
                  "representantes_comerciales.rc_id = pais_representante.pr_id_representante INNER JOIN pais ON pais_representante.pr_id_pais = pais.pai_id " +
                "WHERE (usuario.usu_login = '" + seleccionado + "') AND (representantes_comerciales.rc_activo = 1) ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ObtenerCorreoSistema()
        {
            string sql;

            sql = "SELECT     mail_cuenta_direccion, mail_cuenta_observacion "+
                   " FROM         mail_cuenta "+
                   " WHERE     (mail_cuenta_activo = 1)";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ObtenerProcesoPreciosItem()
        {
            string sql;

            sql = "SELECT        TOP (1)  FechaProceso, Periodo, Usuario "+
                    " FROM Temp_PreciosVentaItem_Proceso "+
                    " ORDER BY TempPrecioProceso_Id DESC";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader EjecutarJob()
        {
            string sql;

            sql = " EXEC dbo.sp_start_job 'Actualiza Clientes Sf Erps' ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //Consultar Nombre Representante
        public SqlDataReader consultarNombre(string nombre)
        {
            string sql;
            sql = "SELECT empleado.emp_nombre + ' ' + empleado.emp_apellidos AS Nombre, emp_correo_electronico, " +
                "emp_area_id, usuario.usu_siif_id, isnull(usuario.infraestructura,0),   CAST(CASE WHEN usuario.usu_rap_id = 38 THEN 1 ELSE 0 END AS bit) AS servPostventa, isnull(usuario.solicita_pallet,0) as solicitaPallet , isnull(usuario.IdCliente,0) as idClienteUsuario  " +
                "FROM empleado INNER JOIN usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id " +
                "WHERE (usuario.usu_login = '" + nombre + "')";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader obtenerDatosPaisRepresentante(int representante)
        {
            string sql;

            sql = "SELECT pais.pai_nombre, pais.pai_id, representantes_comerciales.rc_id " +
                "FROM pais_representante INNER JOIN representantes_comerciales ON " +
                "pais_representante.pr_id_representante = representantes_comerciales.rc_id INNER JOIN " +
                "pais ON pais_representante.pr_id_pais = pais.pai_id " +
                "WHERE (representantes_comerciales.rc_id = " + representante + ") AND (pais_representante.pr_activo = 1) ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader obtenerZonaRepresentante(int representante)
        {
            string sql;

            sql = "SELECT rc.rc_id, c.ciu_zona FROM representantes_comerciales rc " +
                  "INNER JOIN ciudad_representante cr on cr.cr_rc_id = rc.rc_id " +
                  "INNER JOIN ciudad c on c.ciu_id = cr.cr_ciu_id " +
                  "WHERE (rc.rc_id = " + representante + ") AND (rc.rc_activo = 1)";

            return BdDatos.ConsultarConDataReader(sql);

        }

        //Consulta las plantas por usuario
        public SqlDataReader consultarPlantaColombia(int idusuario)
        {
            string sql = "";
            sql = "SELECT CASE WHEN plantausu_activo = 1 THEN 1 ELSE 0 END AS Expr1 FROM    planta_usuario WHERE   (plantausu_idusuario = " + idusuario + ") AND (plantausu_idplanta = 1)";

            return BdDatos.ConsultarConDataReader(sql);
        }

    }
}
