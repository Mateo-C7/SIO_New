using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Data.OracleClient;

////using System.Data.OracleClient;
//using Oracle.DataAccess.Client;
//using Oracle.DataAccess.Types; 

namespace CapaDatos
{
    public class BdDatos
    {
        public static OracleConnection m;
        private static SqlConnection conexion = new SqlConnection();
        private static SqlConnection conexion1;
        private static SqlCommand comando;
        private static DataSet ds = new DataSet();
        
        //private static OracleConnection conn;      
        public static string nombre_rol = "";
        public static string cont_rol = "";
        

        public static string obtenerConexionString ()
        {
            string ConnString = ConfigurationManager.AppSettings["Conn"];
            string ConexionString = "";

            switch (ConnString)
            {
                case "REAL":
                    //ConexionString = @"data source=172.21.224.130;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//REALA
                    ConexionString = @"data source=10.75.131.2;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//REALA
                    break;
                case "FORSACAD":
                    ConexionString = @"data source=172.21.0.38;persist security info=False;initial catalog=ForsaCad; user id=sio; password=forsa2006";//CAPACITACION APUNTA A FORSACAD
                    break;
                case "NUEVO":
                    ConexionString = @"data source=172.21.0.70;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//PRUEBA SQL2012
                    break;
                case "NUBE":
                    //ConexionString = @"data source=172.21.224.130;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//DESARROLLO	
                    ConexionString = @"data source=10.75.131.2;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";
                    break;
                case "FUP":
                    ConexionString = @"data source=172.21.0.38;persist security info=False;initial catalog=Forsa_Costos; user id=sio; password=forsa2006";//FORSA NEW
                    break;
                case "PRUEBAS":
                    ConexionString = @"data source=172.21.0.39;persist security info=False;initial catalog=Forsa; user id=forsa; password=forsa2006";//DESARROLLO	
                    break;
                case "PRUEBASMT":
                    ConexionString = @"data source=172.21.0.39;persist security info=False;initial catalog=ForsaMetro; user id=forsa; password=forsa2006";//DESARROLLO	
                    break;
                case "PRUEBASMETRO1":
                    ConexionString = @"data source=172.21.0.38;persist security info=False;initial catalog=Forsa_Metro1; user id=forsa; password=forsa2006";//DESARROLLO	
                    break;
                default:
                    ConexionString = @"data source=172.21.0.38;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//DESARROLLO	
                    break;
            }            
            
                return ConexionString;          

        }
    

    public static int conectar()
        {
            //Clase para conectar a la Base De Datos
            //Instanciamos el objeto para la conexion

            string Conn = ConfigurationManager.AppSettings["Conn"];
            SqlConnection conexion = new SqlConnection();
            //conexion = new SqlConnection();
            switch (Conn)
            {
                case "REAL":
                    //conexion.ConnectionString = @"data source=172.21.224.130;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//REALA
                    conexion.ConnectionString = @"data source=10.75.131.2;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";
                    break;
                case "FORSACAD":
                    conexion.ConnectionString = @"data source=172.21.0.38;persist security info=False;initial catalog=ForsaCad; user id=sio; password=forsa2006";//CAPACITACION APUNTA A FORSACAD
                    break;
                case "NUEVO":
                    conexion.ConnectionString = @"data source=172.21.0.70;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//PRUEBA SQL2012
                    break;
                case "NUBE":
                    //conexion.ConnectionString = @"data source=172.21.224.130;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//DESARROLLO	
                    conexion.ConnectionString = @"data source=10.75.131.2;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";
                    break;
                case "FUP":
                    conexion.ConnectionString = @"data source=172.21.0.38;persist security info=False;initial catalog=Forsa_Costos; user id=sio; password=forsa2006";//FORSA NEW
                    break;
                case "PRUEBAS":
                    conexion.ConnectionString = @"data source=172.21.0.39;persist security info=False;initial catalog=Forsa; user id=forsa; password=forsa2006";//DESARROLLO	
                    break;
                case "PRUEBASMETRO1":
                    conexion.ConnectionString = @"data source=172.21.0.38;persist security info=False;initial catalog=Forsa_Metro1; user id=forsa; password=forsa2006";//DESARROLLO	
                    break;
                default:
                    conexion.ConnectionString = @"data source=172.21.0.38;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//DESARROLLO	
                    break;
            }
            //conexion.ConnectionString = @"data source=172.21.0.38;persist security info=False;initial catalog=ForsaPruebas; user id=sapruebas; password=pruebas2013";//PRUEBAS
            //conexion.ConnectionString = @"data source=172.21.0.38;persist security info=False;initial catalog=ForsaFup; user id=sapruebas; password=pruebas2013";//PRUEBAS FUP

            try
            {
                conexion.Open();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }

       



        //OJO AQUI CAMBIO LA RUTA DE PRUEBAS A 10.38 POR PROBLEMAS DE CONEXIONxx
        public static string conexionScope()
        {
            string conex;
            string Conn = ConfigurationManager.AppSettings["Conn"];
                        
            switch (Conn)
            {
                case "REAL":
                    //conex = @"data source=172.21.224.130;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//REALA
                    //Se extiende el Connection TimeOut a 60 seg para que la App espere un poco mas la respuesta de conexion de la BD
                    conex = @"data source=10.75.131.2;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006; Connection Timeout=60";
                    break;
                case "FORSACAD":
                    conex = @"data source=172.21.0.38;persist security info=False;initial catalog=ForsaCad; user id=sio; password=forsa2006";//CAPACITACION APUNTA A FORSACAD
                    break;
                case "NUEVO":
                    conex = @"data source=172.21.0.70;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//PRUEBA SQL2012
                    break;
                case "NUBE":
                    //conex = @"data source=172.21.224.130;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//DESARROLLO	
                    conex = @"data source=10.75.131.2;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";
                    break;
                case "FUP":
                    conex = @"data source=172.21.0.38;persist security info=False;initial catalog=Forsa_Costos; user id=sio; password=forsa2006";//FORSA NEW
                    break;
                case "PRUEBAS":
                    conex = @"data source=172.21.0.39;persist security info=False;initial catalog=Forsa; user id=forsa; password=forsa2006";//DESARROLLO	
                    break;
                case "PRUEBASMETRO1":
                    conex = @"data source=172.21.0.38;persist security info=False;initial catalog=Forsa_Metro1; user id=forsa; password=forsa2006";//DESARROLLO	
                    break;
                default:
                    conex = @"data source=172.21.0.70;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//DESARROLLO	
                    break;
            }

            return conex;
        }

        //OJO AQUI CAMBIO LA RUTA DE PRUEBAS A 10.38 POR PROBLEMAS DE CONEXIONxx
        public static string conexionScopeGerbo()
        {
            string conex;
            string Conn = ConfigurationManager.AppSettings["Conn"];

            switch (Conn)
            {
                //case "REAL":
                //    conex = @"data source=172.21.22.7;persist security info=False;initial catalog=FORSA; user id=FORSA; password=masterkey";//REALA
                //    break;

                // -------Nube
                case "REAL":
                    conex = @"data source=10.203.194.153,1454;persist security info=False;initial catalog=gerbo03; user id=gerbo03; password=masterkey";//REALA
        // anterior conex = @"data source=10.203.186.51,1435;persist security info=False;initial catalog=gerbo03; user id=gerbo03; password=masterkey";//REAL
                    break;

                case "PRUEBAS":
                    conex = @"data source=172.21.0.39;persist security info=False;initial catalog=Forsa; user id=forsa; password=forsa2006";//DESARROLLO	
                    break;
                case "NUBE":
                    //conex = @"data source=172.21.224.130;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//DESARROLLO	
                    conex = @"data source=10.75.131.2;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";
                    break;

                default:
                    conex = @"data source=172.21.0.0;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//DESARROLLO	
                    break;
            }

            return conex;
        }

        
        
        public static void activarRolUsuario(string nRol, string pRol)
        {
            string cadenaRol;
            int filas_afectadas;
            string cadenaConexion = conexionScope();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            //OJO CAMBIAR USE FORSA CAMBIAR PRUEBAS
            if (nombre_rol != "" && cont_rol != "")
            {
                cadenaRol = "USE Forsa";
                comando = new SqlCommand(cadenaRol, conexion);
                filas_afectadas = comando.ExecuteNonQuery();
                cadenaRol = "EXEC sp_setapprole '" + nRol + "', '" + pRol + "'";
                comando = new SqlCommand(cadenaRol, conexion);
                filas_afectadas = comando.ExecuteNonQuery();
            }
            conexion.Close();
        }


       

        public static SqlDataReader ConsultarConDataReader(string sql)
        {
            string cadenaConexion = conexionScope();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            try
            {
                SqlDataReader reader = null;                
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandTimeout = 3600;
                cmd.Connection = conexion; 
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);               

                return reader;
            }
            catch (Exception e)
            {
                conexion.Close();
                return null;
            }           
        }
        
        public static SqlDataReader ConsultarConexionBases(string sql, string bd)
        {
            string cadenaConexion = "";

            if (bd == "forsa")
            {
                //cadenaConexion = @"data source=172.21.224.130;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";//REAL
                cadenaConexion = @"data source=10.75.131.2;persist security info=False;initial catalog=Forsa; user id=sio; password=forsa2006";
            }

            if (bd == "unoee")
            {
                cadenaConexion = @"data source=siesa-m4-sqlsw-db2.ceqnrhbwqaoo.us-east-1.rds.amazonaws.com;persist security info=False;initial catalog=Forsa; user id=Forsaapp; password=Forsa$21$%";//REAL
            }

            if (bd == "gerbo")
            {
                cadenaConexion = @"data source=10.203.194.153,1454;persist security info=False;initial catalog=gerbo03; user id=gerbo03; password=masterkey";//REAL
            }

            SqlConnection conexion = new SqlConnection(cadenaConexion);            

            try
            {
                conexion.Open();
                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandTimeout = 3600;
                cmd.Connection = conexion;
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
            catch (Exception e)
            {
                conexion.Close();
                return null;
            }
        }
        

        public static SqlDataReader ConsultarConDataReaderGerbo(string sql)
        {
            string cadenaConexion = conexionScopeGerbo();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            try
            {
                SqlDataReader reader = null;
                //SqlCommand cmd = null;
                //conectar();
                //cmd = new SqlCommand(sql);
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandTimeout = 3600;
                cmd.Connection = conexion;
                //reader = cmd.ExecuteReader();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
            catch (Exception e)
            {
                conexion.Close();
                return null;
            }
            //finally
            //{
            //    connection.Close();
            //    if (reader != null)
            //        reader.Close();
            //}

        }
        public static int desconectar()
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static DataSet consultarConDataset(string sql)
        {
            string cadenaConexion = conexionScope();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
               
                conexion.Open();
                //conectar();
                ds.Clear();
                ds.EnforceConstraints = false;
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand comando = new SqlCommand(sql, conexion);
                adapter.SelectCommand = comando;
                adapter.FillSchema(ds, SchemaType.Source);
                adapter.Fill(ds);
                //desconectar();
                conexion.Close();
                return ds;
            }
            catch (Exception e)
            {
                conexion.Close();
                //desconectar();
                return null;
            }
        }

        public static int Actualizar(string sql)
        {
            return ejecutarSql(sql);
        }

        public static int ejecutarSql(string sql)
        {
            string cadenaConexion = conexionScope();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            int filas_afectadas = -1;
            try
            {
                //conectar();
                
                conexion.Open();
                comando = new SqlCommand(sql, conexion);
                filas_afectadas = comando.ExecuteNonQuery();
                //desconectar();
                conexion.Close();
            }
            catch (Exception e)
            {
                conexion.Close();
                //desconectar();
                filas_afectadas= -1;
            }
            
            return (filas_afectadas);
        }

        public static bool DameTabla(ref DataTable Tabla, string SqlString)
        {            
            try
            {
                string cadenaConexion = conexionScope();
                SqlConnection conexion = new SqlConnection(cadenaConexion);
                conexion.Open();
                //conectar();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(SqlString, conexion);
                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();
                Tabla.Load(cmd.ExecuteReader());
                conexion.Close();
                //desconectar();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static DataTable CargarTabla(String sql)
        {
            try
            {
                string cadenaConexion = conexionScope();
                SqlConnection conexion = new SqlConnection(cadenaConexion);
                conexion.Open();
                //conectar();
                ds.Clear();
                ds.EnforceConstraints = false;
                DataTable tabla = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand comando = new SqlCommand(sql, conexion);
                adapter.SelectCommand = comando;
                adapter.FillSchema(tabla, SchemaType.Source);
                try { adapter.Fill(tabla); }
                catch { }
                conexion.Close();
                //desconectar();
                return tabla;
            }
            catch (Exception e)
            {
                desconectar();
                return null;
            }
        }

        public static DataTable CargarTablaProccedure(String nomPro)
        {
            string cadenaConexion = conexionScope();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            DataTable tabla = new DataTable();
            try
            {
               
                conexion.Open();
                //conectar();
                ds.Clear();
                SqlCommand Query = new SqlCommand(nomPro, conexion);
                SqlDataAdapter adapter = new SqlDataAdapter(Query);
                adapter.Fill(tabla);
                conexion.Close();
                //desconectar();
            }
            catch
            {
                conexion.Close();
                //desconectar();
                tabla = null;
            }
            return tabla;
        }

        public static DataTable CargarTabla2(String sql)
        {
            string cadenaConexion = conexionScope();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                
                conexion.Open();
                //conectar();
                DataTable tabla = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand comando = new SqlCommand(sql, conexion);
                adapter.SelectCommand = comando;
                 adapter.FillSchema(tabla, SchemaType.Source);
                adapter.Fill(tabla);
                conexion.Close();
                //desconectar();
                return tabla;
            }
            catch (Exception e)
            {
                conexion.Close();
                //desconectar();
                return null;
            }


        }
  ////      /*CONECTAR CON ORACLE**/
  //      public static bool OpenConnection()
  //      {
  //          try
  //          {
  //              string OraConnStr = ConfigurationManager.ConnectionStrings["OraConnStr"].ConnectionString;
  //              m = new OracleConnection(OraConnStr);
  //              // abrimos la conexion 
  //              m.Open();
  //              return true;

  //          }
  //          catch (Exception)//Si ocurre algun error
  //          {
  //              return false;
  //          }
  //      }
  //      // metodo para cerrar la conexion
  //      public static bool closeConnection()
  //      {
  //          try
  //          {
  //              m.Close();
  //              return true;

  //          }
  //          catch (Exception)
  //          {

  //              return false;
  //          }
  //      }


        //      /*CONECTAR CON SQL**/
        public static bool OpenConnection()
        {
            try
            {
                string SqlConnStr = ConfigurationManager.ConnectionStrings["OraConnStr"].ConnectionString;
                conexion = new SqlConnection(SqlConnStr);
                // abrimos la conexion 
                conexion.Open();
                return true;
            }
            catch (Exception)//Si ocurre algun error
            {
                return false;
            }
        }

        // metodo para cerrar la conexion
        public static bool closeConnection()
        {
            try
            {
                conexion.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //public static OracleDataReader consultarConDataReaderOracle(string sql)
        //{
        //    OracleDataReader Oreader = null;
        //    OracleCommand cmd = null;

        //    cmd = new OracleCommand(sql);


        //    try
        //    {
        //        OpenConnection();
        //        cmd.Connection = m;
        //        Oreader = cmd.ExecuteReader();
        //    }
        //    catch (OracleException e)
        //    {
        //        string errorMessage = "Code: " + e.Code + "\n" +
        //                        "Message: " + e.Message;

        //        System.Diagnostics.EventLog log = new System.Diagnostics.EventLog();
        //        log.Source = "My Application";
        //        log.WriteEntry(errorMessage);

        //    }
        //    // closeConnection();
        //    return Oreader;

        //}

        public static SqlDataReader consultarConDataReaderOracle(string sql)
        {
            SqlDataReader Oreader = null;
          
            SqlCommand cmd = null;

            cmd = new SqlCommand(sql);
           
           
            try 
            {
                OpenConnection();
                cmd.Connection = conexion;
                Oreader = cmd.ExecuteReader();
            }
             catch (SqlException e) 
            {
                    string errorMessage = "Code: " + e.ErrorCode + "\n" +
                                    "Message: " + e.Message;

                    System.Diagnostics.EventLog log = new System.Diagnostics.EventLog();
                        log.Source = "My Application";
                        log.WriteEntry(errorMessage);
                       
             }
           // closeConnection();
            return Oreader;
            
        }

        public static DataTable EjecutarConsultaConParametros(string consulta, Dictionary<string, object> parametros)
        {
                string cadenaConexion = conexionScope();
                SqlConnection conexion = new SqlConnection(cadenaConexion);
                conexion.Open();
                //conectar();
                ds.Clear();
                ds.EnforceConstraints = false;
                DataTable tabla = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.CommandType = CommandType.Text;

                adapter.SelectCommand = comando;
                foreach (KeyValuePair<string, object> parametro in parametros)
                {
                    adapter.SelectCommand.Parameters.AddWithValue(parametro.Key, parametro.Value);
                }

                try
                {
                    adapter.FillSchema(tabla, SchemaType.Source);
                    adapter.Fill(tabla);
                }
                catch (Exception ex) { }
                conexion.Close();
                //desconectar();

                return tabla;
            
            
        }

        public static DataTable EjecutarStoreProcedureConParametros(string storeprocedure, Dictionary<string, object> parametros)
        {
            //conectar();
            string cadenaConexion = conexionScope();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            ds.Clear();
            ds.EnforceConstraints = false;
            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand(storeprocedure, conexion);
            comando.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand = comando;
            foreach (KeyValuePair<string, object> parametro in parametros)
            {
                adapter.SelectCommand.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }

            try
            {
                adapter.FillSchema(tabla, SchemaType.Source);
                adapter.Fill(tabla);
            }
            catch (Exception ex) { }
            //desconectar();
            conexion.Close();

            return tabla;
        }

        public static void GuardarConsultaConParametros(string consulta, Dictionary<string, object> parametros)
        {
            //conectar();
            string cadenaConexion = conexionScope();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.CommandType = CommandType.Text;
            adapter.InsertCommand = comando;

            foreach (KeyValuePair<string, object> parametro in parametros)
            {
                adapter.InsertCommand.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }

            try
            {
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception ex) { throw; }
            conexion.Close();
            //desconectar();

        }
        public static int GuardarStoreProcedureConParametros(string storeprocedure, Dictionary<string, object> parametros)
        {
            int id = 0;
            //conectar();
            string cadenaConexion = conexionScope();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            ds.Clear();
            ds.EnforceConstraints = false;
            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand(storeprocedure, conexion);
            comando.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand = comando;
            adapter.SelectCommand.CommandTimeout = 3600;
            foreach (KeyValuePair<string, object> parametro in parametros)
            {
                adapter.SelectCommand.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }

            try
            {
                adapter.FillSchema(tabla, SchemaType.Source);
                adapter.Fill(tabla);
            }
            catch (Exception ex) { id = -1; }
            //desconectar();
            conexion.Close();

            if (tabla.Rows.Count == 1)
            {
                if (tabla.Columns.Count == 1)
                {
                    id = Convert.ToInt32(tabla.Rows[0][0]);
                }

            }
            return id;
        }
        public static DataTable CargarTablaConParametros(String sql, Dictionary<string, object> parametros)
        {
            string cadenaConexion = conexionScope();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            //conectar();
            ds.Clear();
            ds.EnforceConstraints = false;
            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand(sql, conexion);

            adapter.SelectCommand = comando;
            foreach (KeyValuePair<string, object> parametro in parametros)
            {
                adapter.SelectCommand.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }

            adapter.FillSchema(tabla, SchemaType.Source);
            try { adapter.Fill(tabla); }
            catch { }
            //desconectar();
            conexion.Close();
            return tabla;
        }

        public int RegistraExcepcion(string user, string stModulo, string stMes, string stSql = "")
        {
            string sqlSel;
            string sqlErr;

            sqlErr = stSql.Replace("'", " ");
            if (String.IsNullOrEmpty(user))
            {
                user = "QWE";
            }
            sqlSel = " INSERT INTO LOGSIIF (Usuario,Fecha,Origen,Detalle,Sistema,Query,SistVers) "
                            + " VALUES ( '" + user
                            + "' , SYSDATETIME(),'" + stModulo + "','" + stMes
                            + "', 'SIO','" + sqlErr + "','"
                            + ConfigurationManager.AppSettings[" "].ToString() + "')";
            return ejecutarSql(sqlSel);
        }
    }
}
