using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaDatos;
using System.Data;
using System.Web.UI.WebControls;
using CapaControl.Entity;

namespace CapaControl
{
    public class ControlCliente
    {
        //CONSULTA IDIOMA ESPAÑOL
        public DataSet ConsultarIdiomaCliente()
        {
            string sql;
            sql = "SELECT idioma_cliente.* FROM idioma_cliente";
            DataSet ds_idiomaCliente = BdDatos.consultarConDataset(sql);
            return ds_idiomaCliente;   
        }       

        //LLAMAMOS EL PROCEDIMIENTO DE INGRESO DE CLIENTE MATRIZ
        public int Matriz(string cli_nombre, int cli_pai_id, int cli_ciu_id, string cli_direccion, string cli_telefono, 
            string cli_telefono_2, string cli_fax, string cli_mail, string cli_web, string cli_nit, int cli_tco_id,
            string representante, string cli_prefijo, string cli_prefijo2, string cli_prefijofax, int apoyo, int tipoapoyo,
            int vivienda, int infra, int reclamo, string txtreclamo, int competencia, int fuente)
        {

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
            int cli_CM = 1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[25];
            sqls[0] = new SqlParameter("CM", cli_CM);
            sqls[1] = new SqlParameter("Cliente", cli_nombre);
            sqls[2] = new SqlParameter("ID_Pais", cli_pai_id);
            sqls[3] = new SqlParameter("ID_Ciudad", cli_ciu_id);
            sqls[4] = new SqlParameter("Direccion", cli_direccion);
            sqls[5] = new SqlParameter("Telefono", cli_telefono);
            sqls[6] = new SqlParameter("Telefono2", cli_telefono_2);
            sqls[7] = new SqlParameter("Fax", cli_fax);
            sqls[8] = new SqlParameter("Mail", cli_mail);
            sqls[9] = new SqlParameter("Web", cli_web);
            sqls[10] = new SqlParameter("Nit", cli_nit);
            sqls[11] = new SqlParameter("TipoContribuyente", cli_tco_id);
            sqls[12] = new SqlParameter("Usuario ", representante);
            sqls[13] = new SqlParameter("Fecha ", fecha);
            sqls[14] = new SqlParameter("cli_prefijo", cli_prefijo);
            sqls[15] = new SqlParameter("cli_prefijo2", cli_prefijo2);
            sqls[16] = new SqlParameter("cli_prefijofax", cli_prefijofax);
            sqls[17] = new SqlParameter("cli_apoyo", apoyo);
            sqls[18] = new SqlParameter("cli_tipoapoyo", tipoapoyo);
            sqls[19] = new SqlParameter("vivienda", vivienda);
            sqls[20] = new SqlParameter("infra", infra);
            sqls[21] = new SqlParameter("reclamo", reclamo);
            sqls[22] = new SqlParameter("reclamo_comentario", txtreclamo);
            sqls[23] = new SqlParameter("competencia", competencia);
             sqls[24] = new SqlParameter("fuente", fuente);
            
          
            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarCLIENTEMATRIZnew", con))
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

        //LLAMAMOS EL PROCEDIMIENTO DE INGRESO DE CLIENTE MATRIZ LITE
        public int MatrizLite(string cli_nombre, int cli_pai_id, int cli_ciu_id, string cli_direccion, string cli_telefono,
            string cli_telefono_2, string cli_fax, string cli_mail, string cli_web, string cli_nit, int cli_tco_id,
            string representante, string cli_prefijo, string cli_prefijo2, string cli_prefijofax, int apoyo, int tipoapoyo,
            int vivienda, int infra, int fila, string filename, string id_cli_sim, int fuente, int id)
        {

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
            int cli_CM = 1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[26];
            sqls[0] = new SqlParameter("CM", cli_CM);
            sqls[1] = new SqlParameter("Cliente", cli_nombre);
            sqls[2] = new SqlParameter("ID_Pais", cli_pai_id);
            sqls[3] = new SqlParameter("ID_Ciudad", cli_ciu_id);
            sqls[4] = new SqlParameter("Direccion", cli_direccion);
            sqls[5] = new SqlParameter("Telefono", cli_telefono);
            sqls[6] = new SqlParameter("Telefono2", cli_telefono_2);
            sqls[7] = new SqlParameter("Fax", cli_fax);
            sqls[8] = new SqlParameter("Mail", cli_mail);
            sqls[9] = new SqlParameter("Web", cli_web);
            sqls[10] = new SqlParameter("Nit", cli_nit);
            sqls[11] = new SqlParameter("TipoContribuyente", cli_tco_id);
            sqls[12] = new SqlParameter("Usuario ", representante);
            sqls[13] = new SqlParameter("Fecha ", fecha);
            sqls[14] = new SqlParameter("cli_prefijo", cli_prefijo);
            sqls[15] = new SqlParameter("cli_prefijo2", cli_prefijo2);
            sqls[16] = new SqlParameter("cli_prefijofax", cli_prefijofax);
            sqls[17] = new SqlParameter("cli_apoyo", apoyo);
            sqls[18] = new SqlParameter("cli_tipoapoyo", tipoapoyo);
            sqls[19] = new SqlParameter("vivienda", vivienda);
            sqls[20] = new SqlParameter("infra", infra);
            sqls[21] = new SqlParameter("fila", fila);
            sqls[22] = new SqlParameter("archivo", filename);
            sqls[23] = new SqlParameter("id_cli_sim", id_cli_sim);
            sqls[24] = new SqlParameter("fuente", fuente);
            sqls[25] = new SqlParameter("id", id);
            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarClienteMatrizLite", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    // Esta es la clave, hemos de añadir un parámetro que recogerá el valor de retorno
                    SqlParameter retValue = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
                    retValue.Direction = ParameterDirection.Output;
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

        public int ActualizarIDClienteMatriz(int idCliente)
        {
            string sql;

            sql = "UPDATE cliente SET cli_casamatriz_id = " + idCliente + " WHERE cli_id = " + idCliente;

            return BdDatos.Actualizar(sql);
        }

        public int ActualizarTipoCliente(int tipo, int idClientePlanta)
        {
            string sql;

            sql = "update cliente_planta set cliente_tipo_planta_id = "+tipo+"  where cliente_planta_id =  "  + idClientePlanta ;

            return BdDatos.Actualizar(sql);
        }

         public int InsertarTipoCliente(int cliente, int clienteTipoPlanta)
        {
            string sql;

            sql = " INSERT INTO cliente_planta  (cliente_id ,cliente_tipo_planta_id) VALUES ( "+
                    @cliente + "," + clienteTipoPlanta + ")";

            return BdDatos.Actualizar(sql);
        }   

        public DataSet ConsultarTipoCliente(int cliente)
        {
            string sql;
            sql = "SELECT       planta_forsa.planta_descripcion AS Planta, cliente_tipo.descripcion AS Tipo, cliente_planta.cliente_planta_id as Id" +
                    " FROM            cliente_tipo_planta INNER JOIN " +
                        " cliente_planta ON cliente_tipo_planta.cliente_tipo_planta_id = cliente_planta.cliente_tipo_planta_id INNER JOIN " +
                        " cliente_tipo ON cliente_tipo_planta.cliente_tipo_id = cliente_tipo.cliente_tipo_id INNER JOIN " +
                        " planta_forsa ON cliente_tipo_planta.planta_id = planta_forsa.planta_id " +
                    " WHERE        (cliente_planta.cliente_id = " + @cliente + ") ";
            
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }

        //LLAMAMOS EL PROCEDIMIENTO DE INGRESO DE CLIENTE SUCURSAL
        public int Sucursal(string cli_nombre, int cli_id, int cli_pai_id, int cli_ciu_id, string cli_direccion, string cli_telefono,
            string cli_telefono_2, string cli_fax, string cli_mail, string cli_web, string cli_nit, int cli_tco_id,
            string representante, string cli_prefijo, string cli_prefijo2, string cli_prefijofax, int vivienda, int infra, int reclamo, string txtreclamo, int fuente)
        {

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
            int cli_CM = 1;
            

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[23];
            sqls[0] = new SqlParameter("CS", cli_CM);
            sqls[1] = new SqlParameter("Cli_Matriz", cli_id);
            sqls[2] = new SqlParameter("Cliente", cli_nombre);
            sqls[3] = new SqlParameter("ID_Pais", cli_pai_id);
            sqls[4] = new SqlParameter("ID_Ciudad", cli_ciu_id);
            sqls[5] = new SqlParameter("Direccion", cli_direccion);
            sqls[6] = new SqlParameter("Telefono", cli_telefono);
            sqls[7] = new SqlParameter("Telefono2", cli_telefono_2);
            sqls[8] = new SqlParameter("Fax", cli_fax);
            sqls[9] = new SqlParameter("Mail", cli_mail);
            sqls[10] = new SqlParameter("Web", cli_web);
            sqls[11] = new SqlParameter("Nit", cli_nit);
            sqls[12] = new SqlParameter("TipoContribuyente", cli_tco_id);
            sqls[13] = new SqlParameter("Usuario ", representante);
            sqls[14] = new SqlParameter("Fecha ", fecha);
            sqls[15] = new SqlParameter("cli_prefijo", cli_prefijo);
            sqls[16] = new SqlParameter("cli_prefijo2", cli_prefijo2);
            sqls[17] = new SqlParameter("cli_prefijofax", cli_prefijofax);
            sqls[18] = new SqlParameter("vivienda", vivienda);
            sqls[19] = new SqlParameter("infra", infra);
            sqls[20] = new SqlParameter("reclamo", reclamo);
            sqls[21] = new SqlParameter("reclamo_comentario", txtreclamo);
            sqls[22] = new SqlParameter("fuente",fuente);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarCLIENTESUCURSALnew", con))
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

        public SqlDataReader ConsultarDatosClienteMatriz(int ciudad)
        {
            string sql;

            sql = "SELECT cli_id, cli_nombre, pai_nombre, ciu_nombre " +
                    "FROM  cliente INNER JOIN pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN ciudad ON cliente.cli_ciu_id = ciudad.ciu_id AND pais.pai_id = ciudad.ciu_pai_id " +
                    "WHERE cli_ciu_id = " + ciudad + " AND (cli_CM = 1) AND (cli_activo = 1) ORDER BY cli_nombre";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarDatosCliente(int ciudad, int idClienteUsuario)
        {
            string sql;

            if (idClienteUsuario == 0)
            {
                sql = "SELECT cli_id, cli_nombre, pai_nombre, ciu_nombre " +
                        "FROM  cliente INNER JOIN pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN ciudad ON cliente.cli_ciu_id = ciudad.ciu_id AND pais.pai_id = ciudad.ciu_pai_id " +
                        "WHERE cli_ciu_id = " + ciudad + " AND (cli_activo = 1)  ORDER BY cli_nombre";
            }
            else
            {
                sql = "SELECT cli_id, cli_nombre, pai_nombre, ciu_nombre " +
                        "FROM  cliente INNER JOIN pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN ciudad ON cliente.cli_ciu_id = ciudad.ciu_id AND pais.pai_id = ciudad.ciu_pai_id " +
                        "WHERE cli_ciu_id = " + ciudad + " AND (cli_activo = 1) AND (cli_id = " + idClienteUsuario + ") ORDER BY cli_nombre";
            }

            return BdDatos.ConsultarConDataReader(sql);
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

        public int CerrarConexion()
        {
            return BdDatos.desconectar();
        }
        
        public SqlDataReader PoblarTipoContribuyente()
        {
            string sql;

            sql = "SELECT tco_id, tco_descripcion FROM tipo_contribuyente ORDER BY tco_descripcion DESC";

            return BdDatos.ConsultarConDataReader(sql);
        }
       
        public SqlDataReader PoblarTipoClientePlanta(int planta)
        {
            string sql;

            sql = "SELECT        cliente_tipo_planta.cliente_tipo_planta_id, cliente_tipo.descripcion  "+
                    " FROM    cliente_tipo INNER JOIN cliente_tipo_planta ON   "+
                    " cliente_tipo.cliente_tipo_id = cliente_tipo_planta.cliente_tipo_id  "+
                    " WHERE        (cliente_tipo.activo = 1) AND (cliente_tipo_planta.planta_id = "+planta+") AND (cliente_tipo_planta.activo = 1)";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader PoblarPlantaIni()
        {
            string sql;

            sql = "SELECT  planta_id, planta_descripcion FROM   planta_forsa WHERE  (planta_activo = 1)";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader PoblarPlanta(int cliente)
        {
            string sql;

            sql = "SELECT        planta_id, planta_descripcion FROM   planta_forsa "+
                    " WHERE        (planta_activo = 1) AND (planta_id NOT IN "+
                      "  (SELECT        cliente_tipo_planta.planta_id "+
                       "  FROM            cliente_planta INNER JOIN  "+
                        " cliente_tipo_planta ON cliente_planta.cliente_tipo_planta_id = cliente_tipo_planta.cliente_tipo_planta_id "+
                         "  WHERE        (cliente_planta.activo = 1) AND (cliente_planta.cliente_id ="+cliente+")))";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader PoblarTipoClienteGrilla(int cliente)
        {
            string sql;

            sql = "SELECT        cliente.cli_nombre AS cliente, cliente_tipo.descripcion AS tipo "+
                    " FROM            cliente_planta INNER JOIN   cliente_tipo_planta ON cliente_planta.cliente_tipo_planta_id = cliente_tipo_planta.cliente_tipo_planta_id INNER JOIN "+
                      "   cliente_tipo ON cliente_tipo_planta.cliente_tipo_id = cliente_tipo.cliente_tipo_id INNER JOIN "+
                      "   cliente ON cliente_planta.cliente_id = cliente.cli_id " +
                    " WHERE        (cliente_planta.activo = 1) AND (cliente_tipo_planta.activo = 1) AND (cliente.cli_id = "+@cliente+")";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader PoblarTipoApoyo()
        {
            string sql;

            sql = "SELECT     tipo_grupo_id, tipo_grupo_descripcion "+
                    " FROM         tipo_grupoapoyo "+
                    " WHERE     (tipo_grupo_activo = 1) AND (tipo_grupo_id <> 0)  ORDER BY orden_visualiza ";

            return BdDatos.ConsultarConDataReader(sql);
        }
        

        public SqlDataReader PoblarBusqCliente(int rol,int pais, int idrepres, string nombre)
        {
            string sql;

            sql = "execute dbo.BusqClientesxRol "+rol.ToString()+ ", "+pais.ToString()+", "+idrepres.ToString()+", '"+nombre+"'";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader PoblarBusqApoyo(int rol, int pais, int idrepres, string nombre)
        {
            string sql;

            sql = "execute dbo.BusqApoyoxRol " + rol.ToString() + ", " + pais.ToString() + ", " + idrepres.ToString() + ", '" + nombre + "'";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarCliente(int cliente)
        {
            string sql;

            sql = "SELECT cli_CM, cli_CS, cli_nombre, cli_direccion, cli_telefono, cli_telefono_2, cli_fax, cli_mail, cli_web, " +
                  " cli_nit, cli_pai_id, pai_nombre, cli_ciu_id, ciu_nombre, cli_tco_id, tco_descripcion,cliente.usu_actualiza, "+
                  " cliente.fecha_actualiza,cliente.cli_prefijo,cliente.cli_prefijo2,cliente.cli_prefijo_fax,cli_tipo_estado, " +
                  " cliente.cli_tipo_apoyo,cliente.cli_vivienda,cliente.cli_infra,isnull(cliente.reclamo,0), isnull(cliente.reclamo_comentario,''), " +
                  " isnull(cli_competencia,0) as cli_competencia,cliente_tipo_fuente_id,lite_tipo_fuente.tifuente_origen_id " +
                  " FROM cliente INNER JOIN pais ON cliente.cli_pai_id = pais.pai_id " +
                  " INNER JOIN ciudad ON cliente.cli_ciu_id = ciudad.ciu_id " +
                  " INNER JOIN tipo_contribuyente ON cliente.cli_tco_id = tipo_contribuyente.tco_id " +
                  " INNER JOIN lite_tipo_fuente ON cliente.cliente_tipo_fuente_id = lite_tipo_fuente.tifuente_id " +
                  " WHERE (cli_id = " + cliente + ") ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ObtenerPrefijo(int pais)
        {
            string sql;

            sql = "SELECT it_id, it_prefijo, it_pais_id FROM indicativos_telefonicos " +
                  "WHERE it_pais_id = " + pais + " ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarClienteSucursal(int cliente)
        {
            string sql;

            sql = "SELECT cliente_1.cli_nombre AS Matriz, cliente_1.cli_id AS IDMatriz, pais_1.pai_nombre, pais_1.pai_id, " + 
                  "ciudad_1.ciu_nombre, ciudad_1.ciu_id " +
                  "FROM cliente INNER JOIN pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN ciudad ON " +
                    "cliente.cli_ciu_id = ciudad.ciu_id AND pais.pai_id = ciudad.ciu_pai_id INNER JOIN " +
                    "tipo_contribuyente ON cliente.cli_tco_id = tipo_contribuyente.tco_id INNER JOIN " +
                    "cliente AS cliente_1 ON cliente.cli_casamatriz_id = cliente_1.cli_id INNER JOIN " +
                    "pais AS pais_1 ON cliente_1.cli_pai_id = pais_1.pai_id INNER JOIN " +
                    "ciudad AS ciudad_1 ON cliente_1.cli_ciu_id = ciudad_1.ciu_id " +
                  "WHERE cliente.cli_id = " + cliente + " ";


            return BdDatos.ConsultarConDataReader(sql);
        }

        public int ActualizarCliente(int idCliente, string nombre, string direccion, string nit, string tel1, string tel2, 
            string fax, string mail, string web, int contribuyente, string usuario, string prefijo1 , string prefijo2, string prefijo3,int tipoapoyo, int activo,
            int vivienda, int infra, int reclamo, string txtreclamo, int pais, int ciudad , int competencia, int fuente)
        {
            string sql;
            string fecha = System.DateTime.Now.ToShortDateString();

            sql = "UPDATE cliente SET cli_pai_id = " + pais + ", cli_ciu_id = "+ciudad+", cli_nombre = '" + nombre + "', cli_direccion = '" + direccion + "',  cli_nit = '" + nit + 
                "', cli_telefono = '" + tel1 + "', cli_telefono_2 = '" + tel2 + "', cli_fax = '" + fax + "', cli_mail = '" + mail +
                "', cli_web = '" + web + "', cli_tco_id = " + contribuyente + ", usu_actualiza = '" + usuario +
                "', fecha_actualiza = '" + fecha + "' , cli_prefijo= '" + prefijo1 +"', cli_prefijo2 = '"+ prefijo2 +"', cli_prefijo_fax = '"+prefijo3+ "' " +
                " , cli_tipo_apoyo = " + tipoapoyo + " , cli_activo =" +activo + " "+" , cli_vivienda =" +vivienda + " , " +
                " cli_infra =" +infra +  " ,reclamo= "+reclamo+ " ,reclamo_comentario = '"+txtreclamo+"',cli_competencia="+competencia+",cliente_tipo_fuente_id="+fuente+" "+ 
                "  WHERE cli_id = " + idCliente;

            return BdDatos.Actualizar(sql);
        }

        public SqlDataReader ConsultarTDN()
        {
            string sql;
            sql = "SELECT ternog_id, ternog_sigla FROM termino_negociacion ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarTipoClientePlanta(int idClientePlanta)
        {
            string sql;

            sql = "SELECT        planta_forsa.planta_id, cliente_planta.cliente_tipo_planta_id  , planta_forsa.planta_descripcion  " +
                    " FROM            cliente_tipo_planta INNER JOIN   "+
                    " cliente_planta ON cliente_tipo_planta.cliente_tipo_planta_id = cliente_planta.cliente_tipo_planta_id INNER JOIN  "+
                    " cliente_tipo ON cliente_tipo_planta.cliente_tipo_id = cliente_tipo.cliente_tipo_id INNER JOIN  "+
                    " planta_forsa ON cliente_tipo_planta.planta_id = planta_forsa.planta_id  "+
                    " WHERE        (cliente_planta.cliente_planta_id ="+idClientePlanta+")" ;

            //sql = "SELECT cli_CM, cli_CS, cli_nombre, cli_direccion, cli_telefono, cli_telefono_2, cli_fax, cli_mail, cli_web, " +
            //      "cli_nit, cli_pai_id, pai_nombre, cli_ciu_id, ciu_nombre, cli_tco_id, tco_descripcion,cliente.usu_actualiza, " +
            //      " cliente.fecha_actualiza,cliente.cli_prefijo,cliente.cli_prefijo2,cliente.cli_prefijo_fax,cli_tipo_estado, cliente.cli_tipo_apoyo,cliente.cli_vivienda,cliente.cli_infra " +
            //      "FROM cliente INNER JOIN pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN ciudad ON " +
            //      "cliente.cli_ciu_id = ciudad.ciu_id INNER JOIN tipo_contribuyente ON " +
            //      "cliente.cli_tco_id = tipo_contribuyente.tco_id " +
            //      "WHERE (cli_id = " + cliente + ") ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public void insertLogClienteLite(int id, int fila, string archivo, string usuario, string observacion, string nombre, int pais, int ciudad)
        {
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
            string sql = "INSERT INTO LogClienteLite(cliente_id, fila_excel, observacion, nombre_archivo, fecha, usuario, estado, nombre_cliente, pais, ciudad, ultimo_registro) VALUES(" + id + "," + fila + ",'" + observacion + "', '" + archivo + "', '" + fecha + "' , '" + usuario + "', 0, '" + nombre + "', " + pais + ", " + ciudad + ", 1)";
            BdDatos.Actualizar(sql);
        }

        public int insertarValidadorCliente(string cliente, int id_pais, int id_ciudad, int fila, string archivo, string id_cli_sim, string usuario)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[8];
            sqls[0] = new SqlParameter("Cliente", cliente);
            sqls[1] = new SqlParameter("ID_Pais", id_pais);
            sqls[2] = new SqlParameter("ID_Ciudad", id_ciudad);
            sqls[3] = new SqlParameter("Fecha", fecha);
            sqls[4] = new SqlParameter("fila", fila);
            sqls[5] = new SqlParameter("archivo", archivo);
            sqls[6] = new SqlParameter("id_cli_sim", id_cli_sim);
            sqls[7] = new SqlParameter("Usuario", usuario);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("ValidadorCliente", con))
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

        public void insertarLogValidadorCliente(string Cliente, string archivo, int fila, int ID_Pais, int ID_Ciudad, string id_cli_sim, string observacion)
        {
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
            string sql = " INSERT INTO LogValidadorCliente(nombre_cliente_archivo, archivo, fila, pais_cliente_id_archivo, ciudad_cliente_id_archivo, id_cli_sim_archivo, fecha, observacion, ultimo_registro) Values('" + Cliente + "' , '" + archivo + "', " + fila + ", " + ID_Pais + ", " + ID_Ciudad + ", '" + id_cli_sim + "', '" + fecha + "', '" + observacion + "', 1)";
            BdDatos.Actualizar(sql);
        }

        public void actualizarEstadoLogs(string tabla)
        {
            string sql = "UPDATE " + tabla + " SET ultimo_registro = 0";
            BdDatos.Actualizar(sql);
        }



        //250618
        public void LlenarComboFuente(DropDownList ListFuente, int origen)
        {
            DataTable dt = new DataTable();

            string sql = "SELECT lite_tipo_fuente.tifuente_id, lite_tipo_fuente.tifuente_descripcion " +
                               " FROM lite_tipo_origen INNER JOIN lite_tipo_fuente " +
                                     " ON lite_tipo_origen.tiorigen_id = lite_tipo_fuente.tifuente_origen_id " +
                                " WHERE(lite_tipo_origen.tiorigen_id =" + origen + ") AND tiorigen_activo=1  AND (lite_tipo_fuente.tifuente_activo = 1)";
            dt = BdDatos.CargarTabla(sql);

            foreach (DataRow row in dt.Rows)
            {
                //Crea una lista los dos parametros
                ListItem lst = new ListItem(row["tifuente_descripcion"].ToString(),//Valor mostrado al usuario
                                            row["tifuente_id"].ToString());// Valor real del campo
                //Asignamos al combo los items que se capturaron en el lst
                ListFuente.Items.Insert(ListFuente.Items.Count, lst);
            }
        }

        public void LlenarComboOrigen(DropDownList ListaOrigen)
        {
            //Instancia de variable tipo DataTable
            DataTable dt = new DataTable();
            //Cadena de consulta
            string sql = "SELECT tiorigen_id,tiorigen_descripcion " +
                              " FROM lite_tipo_origen";
            //Al la tabla le signamos lo que recupera de la consulta
            dt = BdDatos.CargarTabla(sql);
            //Recorre todas las filas de la tabla una a una
            foreach (DataRow row in dt.Rows)
            {
                //Crea una lista los dos parametros
                ListItem lst = new ListItem(row["tiorigen_descripcion"].ToString(),//Valor mostrado al usuario
                                            row["tiorigen_id"].ToString());// Valor real del campo
                //Asignamos al combo los items que se capturaron en el lst
                ListaOrigen.Items.Insert(ListaOrigen.Items.Count, lst);
            }

        }

        /// <summary>
        /// Metodo encargado de consultar los clientes de acuerdo a la ciudad seleccionada
        /// Autor: Global BI
        /// Fecha: 09-09-2018
        /// </summary>
        /// <returns>Lista de clientes</returns>
        public List<Cliente> obtenerDatosCliente(int ciudad, int idClienteUsuario)
        {
            string sql;

            if (idClienteUsuario == 0)
            {
                sql = "SELECT cli_id, cli_nombre, pai_nombre, ciu_nombre " +
                        "FROM  cliente INNER JOIN pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN ciudad ON cliente.cli_ciu_id = ciudad.ciu_id AND pais.pai_id = ciudad.ciu_pai_id " +
                        "WHERE cli_ciu_id = " + ciudad + " AND (cli_activo = 1)  ORDER BY cli_nombre";
            }
            else
            {
                sql = "SELECT cli_id, cli_nombre, pai_nombre, ciu_nombre " +
                        "FROM  cliente INNER JOIN pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN ciudad ON cliente.cli_ciu_id = ciudad.ciu_id AND pais.pai_id = ciudad.ciu_pai_id " +
                        "WHERE cli_ciu_id = " + ciudad + " AND (cli_activo = 1) and (cli_id = "+ idClienteUsuario + ") ORDER BY cli_nombre";
            }

            DataTable dt = BdDatos.CargarTabla(sql);
            List<Cliente> lstObject = dt.AsEnumerable()
                .Select(row => new Cliente
                {
                    Id = (int)row["cli_id"],
                    Nombre = (string)row["cli_nombre"],
                    NombrePais = (string)row["pai_nombre"],
                    NombreCiudad = (string)row["ciu_nombre"]
                }).ToList();
            dt.Clear();
            dt.Dispose();
            return lstObject;
        }

        /// <summary>
        /// Metodo encargado de consultar los contactos del cliente seleccionado
        /// Autor: Global BI
        /// Fecha: 09-09-2018
        /// </summary>
        /// <returns>Lista de contactos clientes</returns>
        public List<ContactoCliente> obtenerContactoCliente(int cliente)
        {
            string sql;
            sql = "SELECT ccl_nombre + ' ' + ccl_nombre2 + ' ' + ccl_apellido + ' ' + ccl_apellido2 AS nombre, ccl_id, ccl_cli_id " +
                  "FROM contacto_cliente " +
                  "WHERE(ccl_cli_id = " + cliente + ") AND ccl_activo = 1 " +
                  "ORDER BY nombre";
            DataTable dt = BdDatos.CargarTabla(sql);
            List<ContactoCliente> lstObject = dt.AsEnumerable()
                .Select(row => new ContactoCliente
                {
                    Id = (int)row["ccl_id"],
                    Nombre = (string)row["nombre"],
                    IdCliente = (int)row["ccl_cli_id"]
                }).ToList();
            dt.Clear();
            dt.Dispose();
            return lstObject;
        }
    }
}
