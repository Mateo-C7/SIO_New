using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaDatos;
using System.Data;


namespace CapaControl
{
    public class ControlCotizacionPreliminar
    {
        public DataSet ConsultarIdiomaCotizacionPreliminar()
        {
            string sql;
            sql = "SELECT idioma_cotizacion_preliminar.* FROM idioma_cotizacion_preliminar";
            DataSet dsCotizacionPreliminar = BdDatos.consultarConDataset(sql);
            return dsCotizacionPreliminar;
        }

        public SqlDataReader ObtenerContacto(int cliente)
        {
            string sql;
            sql = "SELECT ccl_nombre + ' ' + ccl_nombre2 + ' ' + ccl_apellido + ' ' + ccl_apellido2 AS nombre, ccl_id, ccl_cli_id " +
                  "FROM contacto_cliente " +
                  "WHERE(ccl_cli_id = " + cliente + ") AND ccl_activo = 1 " +
                  "ORDER BY nombre";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarMailContacto(int contacto)
        {
            string sql;
            sql = "SELECT ccl_email FROM contacto_cliente " +
                  "WHERE(ccl_id = " + contacto + ") AND ccl_activo = 1 " ;
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ObtenerObra(int cliente)
        {
            string sql;
            sql = "SELECT DISTINCT obra.obr_id, obra.obr_nombre FROM obra " +
                  "WHERE (obr_cli_id = " + cliente + ") AND (obra.obr_perdido = 0) " +
                  "ORDER BY obr_nombre ASC ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarTipoProyecto()
        {
            string sql;
            sql = "SELECT tp_id, tp_descripcion_español, tp_descripcion_ingles, tp_descripcion_portugues " +
                "FROM tipo_proyecto ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarPrecioEquipoNuevo(int pais, string formaleta)
        {
            string sql;
            sql = "SELECT pmp_eqn_cot, pmp_eqn_cierre FROM precio_venta_mercado " +
            "WHERE pmp_pai_id = " + pais + " AND pmp_formaleta_espanol = '" + formaleta + "' OR " +
            "pmp_pai_id = " + pais + " AND pmp_formaleta_ingles = '" + formaleta + "' OR " +
            "pmp_pai_id = " + pais + " AND pmp_formaleta_portugues = '" + formaleta + "' " ;
           
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarPrecioAdaptacion(int pais, string formaleta)
        {
            string sql;
            sql = "SELECT pmp_adap_cot, pmp_adap_cierre FROM precio_venta_mercado " +
            "WHERE pmp_pai_id = " + pais + " AND pmp_formaleta_espanol = '" + formaleta + "' OR " +
            "pmp_pai_id = " + pais + " AND pmp_formaleta_ingles = '" + formaleta + "' OR " +
            "pmp_pai_id = " + pais + " AND pmp_formaleta_portugues = '" + formaleta + "' ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarPrecioListado(int pais, string formaleta)
        {
            string sql;
            sql = "SELECT pmp_list_cot, pmp_list_cierre FROM precio_venta_mercado " +
            "WHERE pmp_pai_id = " + pais + " AND pmp_formaleta_espanol = '" + formaleta + "' OR " +
            "pmp_pai_id = " + pais + " AND pmp_formaleta_ingles = '" + formaleta + "' OR " +
            "pmp_pai_id = " + pais + " AND pmp_formaleta_portugues = '" + formaleta + "' ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarTipoProyectoSeleccion(int tipo)
        {
            string sql;
            sql = "SELECT tp_id, tp_minimo, tp_maximo FROM tipo_proyecto " +
                "WHERE tp_id = " + tipo + " ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public decimal m2min(decimal area, decimal tpmin, int cantadap)
        {

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            decimal Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[3];
            sqls[0] = new SqlParameter("area", area);
            sqls[1] = new SqlParameter("tpmin", tpmin);
            sqls[2] = new SqlParameter("cantadap", cantadap);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("FUNC_CP_M2MIN", con))
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
                        Id = Convert.ToDecimal(retValue.Value);
                    }
                }
            }

            return Id;
        }

        public decimal m2max(decimal area, decimal tpmax, int cantadap)
        {

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            decimal Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[3];
            sqls[0] = new SqlParameter("area", area);
            sqls[1] = new SqlParameter("tpmax", tpmax);
            sqls[2] = new SqlParameter("cantadap", cantadap);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("FUNC_CP_M2MAX", con))
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
                        Id = Convert.ToDecimal(retValue.Value);
                    }
                }
            }

            return Id;
        }

        public decimal vrm2min(decimal m2min, decimal vrm2form)
        {

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            decimal Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[2];
            sqls[0] = new SqlParameter("m2min", m2min);
            sqls[1] = new SqlParameter("vrm2form", vrm2form);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("FUNC_CP_VRM2MIN", con))
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
                        Id = Convert.ToDecimal(retValue.Value);
                    }
                }
            }

            return Id;
        }

        public decimal vrm2max(decimal m2max, decimal vrm2form)
        {

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            decimal Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[2];
            sqls[0] = new SqlParameter("m2max", m2max);
            sqls[1] = new SqlParameter("vrm2form", vrm2form);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("FUNC_CP_VRM2MAX", con))
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
                        Id = Convert.ToDecimal(retValue.Value);
                    }
                }
            }

            return Id;
        }

        //CREACION DEL FORMATO UNICO DE PROYECTOS (FUP)
        public int fup(string fecha, int cliente, int unm, int contacto, int obra, string menu, 
            string usuario)
        {

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[7];
            sqls[0] = new SqlParameter("F_Creacion", fecha);
            sqls[1] = new SqlParameter("ID_Cliente", cliente);
            sqls[2] = new SqlParameter("ID_Moneda", unm);
            sqls[3] = new SqlParameter("ID_Contacto", contacto);
            sqls[4] = new SqlParameter("ID_Obra", obra);
            sqls[5] = new SqlParameter("ID_Menu", menu);
            sqls[6] = new SqlParameter("Usuario", usuario);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarFUP", con))
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

        //INGRESO DE LA COTIZACION PRELIMINAR
        public int cotizacion_preliminar(int fup, decimal m2min, decimal m2max, decimal area, int cant_adapta, 
            decimal prem2min, decimal prem2max, decimal vrm2, string vrtmin, string vrtmax, string tipocot, 
            bool equipnuevo, bool adaptacion, bool listado, bool plastico, bool aluminio, string usuario, string fecha,int idusuario)
        {

            int eqn = 0, adapta = 0, lista = 0, plast = 0, alum = 0;

            if(equipnuevo == true)
            {
                eqn = 1;
            }

            if(adaptacion == true)
            {
                adapta = 1;
            }

            if(listado == true)
            {
                lista = 1;
            }

            if(plastico == true)
            {
                plast = 1;
            }

            if(aluminio == true)
            {
                alum = 1;
            }
            
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[19];
            sqls[0] = new SqlParameter("FUP", fup);
            sqls[1] = new SqlParameter("M2MIN", m2min);
            sqls[2] = new SqlParameter("M2MAX", m2max);
            sqls[3] = new SqlParameter("AREA", area);
            sqls[4] = new SqlParameter("ADAPTACIONES", cant_adapta);
            sqls[5] = new SqlParameter("PREM2MIN", prem2min);
            sqls[6] = new SqlParameter("PREM2MAX", prem2max);
            sqls[7] = new SqlParameter("VRM2", vrm2);
            sqls[8] = new SqlParameter("VRTMIN", vrtmin);
            sqls[9] = new SqlParameter("VRTMAX", vrtmax);
            sqls[10] = new SqlParameter("TIPOCOT", tipocot);
            sqls[11] = new SqlParameter("EQUIPNUEVO", eqn);
            sqls[12] = new SqlParameter("ADAPTACION", adapta);
            sqls[13] = new SqlParameter("LISTADO", lista);
            sqls[14] = new SqlParameter("PLASTICO", plast);
            sqls[15] = new SqlParameter("ALUMINIO", alum);
            sqls[16] = new SqlParameter("USUARIO", usuario);
            sqls[17] = new SqlParameter("FECHA", fecha);
            sqls[18] = new SqlParameter("IDUSUARIO", idusuario);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarCOTIZACIONPRELIMINAR", con))
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

        //OBTENER GERENTES COMERCIALES
        public SqlDataReader ObtenerComercial(int seleccion)
        {
            string sql;

            sql = "SELECT rc_id, rc_descripcion, representantes_comerciales.rc_email " +
            "FROM representantes_comerciales INNER JOIN usuario ON " +
            "representantes_comerciales.rc_usu_siif_id = usuario.usu_siif_id INNER JOIN " +
            "pais_representante ON representantes_comerciales.rc_id = pais_representante.pr_id_representante " +
            "INNER JOIN rolapp ON usuario.usu_rap_id = rolapp.rap_id " +
            "WHERE (representantes_comerciales.rc_activo = 1) AND (pais_representante.pr_id_pais = " + seleccion + ") " +
            "AND (rolapp.rap_id = 2) AND (pais_representante.pr_activo = 1) OR (representantes_comerciales.rc_activo = 1) AND " +
            "(pais_representante.pr_id_pais = " + seleccion + ") AND (rolapp.rap_id = 9) AND (pais_representante.pr_activo = 1) OR " +
            "(representantes_comerciales.rc_activo = 1) AND (pais_representante.pr_id_pais = " + seleccion + ")  " +
            "AND (rolapp.rap_id = 29) AND (pais_representante.pr_activo = 1) OR (representantes_comerciales.rc_activo = 1) AND " +
            "(pais_representante.pr_id_pais = " + seleccion + ") AND (rolapp.rap_id = 30) AND (pais_representante.pr_activo = 1)";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public int cerrarConexion()
        {
            return BdDatos.desconectar();
        }
    }

}
