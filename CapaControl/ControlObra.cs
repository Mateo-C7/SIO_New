using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls;
using CapaControl.Entity;

namespace CapaControl

{
    public class ControlObra
    {
        //GUARDAMOS LOS DATOS DE OBRA
        public int guardarDatosObra(int obr_pai_id, int obr_ciu_id, int obr_cli_id, string obr_nombre, string obr_direccion, string obr_telef,
            string obr_telef2, int obr_total_unidades, int estado_socioecono, double obr_m2_vivienda, string usuario, string tipo_vivienda)
        {
            string sql;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

            sql = "INSERT INTO obra (obr_pai_id, obr_ciu_id, obr_cli_id, obr_nombre, obr_direccion, obr_telef, obr_telef2, obr_total_unidades, obr_ese_id, " +
                  " obr_m2_vivienda, fecha_crea, fecha_actualiza, usu_crea, usu_actualiza, obr_tipo_vivienda) " +
                  " VALUES (" + obr_pai_id + "," + obr_ciu_id + "," + obr_cli_id + ",'" + obr_nombre + "','" + obr_direccion + "','" + obr_telef + "','"
                  + obr_telef2 + "'," + obr_total_unidades + "," + estado_socioecono + "," + obr_m2_vivienda + ",'"
                  + fecha + "','" + fecha + "','" + usuario + "','" + usuario + "','" + tipo_vivienda + "')";

            return BdDatos.ejecutarSql(sql);
        }

        public DataSet ConsultarIdiomaObra()
        {
            string sql;
            sql = "SELECT idioma_obra.* FROM idioma_obra";
            DataSet ds_idiomaObra = BdDatos.consultarConDataset(sql);
            return ds_idiomaObra;
        }    

        public SqlDataReader PoblarEstadoSocioEconomico()
        {
            string sql;

            sql = "SELECT ese_id, ese_descripcion, ese_desc_ingles, ese_desc_portugues, ese_activo, ese_orden FROM estado_socioeconomico " +
            "WHERE (ese_activo = 1) ORDER BY ese_orden";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader PoblarTipoViviendaObra()
         {
            string sql;

            sql = "SELECT        fdom_CodDominio AS Id, fdom_Descripcion AS Descripcion FROM    fup_Dominios "+
                    " WHERE        (fdom_Dominio = 'TIPO_VIVIENDA') ORDER BY fdom_OrdenDominio";

            return BdDatos.ConsultarConDataReader(sql);
        }
        //xxx
        public int ActualizarFechaIniDesp( int idActa, string fecha)
        {
            string sql = "UPDATE fup_acta_seguimiento SET actseg_fecIni_Desp='" + fecha + "', actseg_fecDespacho='" + fecha + "' where actseg_id =" + idActa.ToString() + "";
            return BdDatos.ejecutarSql(sql);
        }

        public SqlDataReader PoblarDatosGeneralesOrden(int idOfp)
        {
            string sql;

            sql = "SELECT        pais.pai_nombre AS pais, ciudad.ciu_nombre AS ciudad, cliente.cli_nombre AS cliente, obra.obr_nombre AS obra, fup_tipo_cotizacion.ftco_nombre AS tipoCotizacion, RTRIM(Orden_Seg.Tipo_Of) "+ 
                    "     + ' ' + RTRIM(Orden_Seg.Num_Of) + '-' + RTRIM(Orden_Seg.Ano_Of) AS Orden, fup_tipo_venta_proyecto.descripcion AS tipoFormaleta, planta_forsa.planta_descripcion AS planta,  "+ 
                        "  solicitud_facturacion.Sf_parte AS parte, CAST(fup_enc_entrada_cotizacion.eect_fup_id AS varchar(MAX)) + ' ' + fup_enc_entrada_cotizacion.eect_vercot_id AS fupVersion, fup_acta_seguimiento_1.actseg_id,  "+
                        "  fup_acta_seguimiento_1.actseg_idofp,CONVERT(varchar(10),isnull( fup_acta_seguimiento_1.actseg_fecIni_Desp, '1900/01/01'),120)   as FechaIniDesp  " + 
                   "  FROM            cliente INNER JOIN  "+ 
                   "       formato_unico ON cliente.cli_id = formato_unico.fup_cli_id INNER JOIN "+ 
                   "       ciudad ON cliente.cli_ciu_id = ciudad.ciu_id INNER JOIN "+ 
                   "       pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN "+ 
                   "       fup_acta_seguimiento AS fup_acta_seguimiento_1 ON formato_unico.fup_id = fup_acta_seguimiento_1.actseg_fup_id INNER JOIN  "+ 
                   "       fup_enc_entrada_cotizacion ON fup_acta_seguimiento_1.actseg_fup_id = fup_enc_entrada_cotizacion.eect_fup_id AND  "+ 
                   "       fup_acta_seguimiento_1.actseg_version = fup_enc_entrada_cotizacion.eect_vercot_id INNER JOIN "+ 
                   "       fup_tipo_venta_proyecto ON fup_enc_entrada_cotizacion.eect_tipo_venta_proy_id = fup_tipo_venta_proyecto.fup_tipo_venta_proy_id INNER JOIN "+ 
                   "       obra ON formato_unico.fup_obr_id = obra.obr_id INNER JOIN "+ 
                   "       fup_tipo_cotizacion ON fup_enc_entrada_cotizacion.eect_tipo_cotizacion = fup_tipo_cotizacion.ftco_id INNER JOIN "+ 
                   "       fup_tipo_proyectos ON fup_tipo_venta_proyecto.fup_tipo_proyecto_id = fup_tipo_proyectos.ftpr_id INNER JOIN "+ 
                   "       Orden_Seg ON fup_acta_seguimiento_1.actseg_idofp = Orden_Seg.Id_Ofa INNER JOIN "+ 
                   "       solicitud_facturacion ON Orden_Seg.sf_id = solicitud_facturacion.Sf_id INNER JOIN "+ 
                   "       planta_forsa ON Orden_Seg.planta_id = planta_forsa.planta_id "+
                "  WHERE        (fup_acta_seguimiento_1.actseg_anulado = 0) AND (fup_enc_entrada_cotizacion.eect_activo = 1) AND (Orden_Seg.Anulado = 0) AND (Orden_Seg.Id_Ofa =  " + idOfp + ")";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public String guardarSeguimientoObra(int obraid, int tipo, string observacion, string usuario, int empresa, string linkEvernot)
        {
            string sql;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
            String mensaje = "";
            DataTable consulta = null;
            try
            {
                sql = "DECLARE @ultimoId INT;" +
                   "insert into dbo.obra_seguimiento (obrseg_obr_id " +
                   ",obrtiseg_id " +
                   ",obrseg_comentarios  " +
                   ",usuario " +
                   ",obrseg_empresa_compe,obrseg_link_evernot)" +
                   "  values( " + obraid + "," + tipo + ",'" + observacion + "', '" + usuario + "', "+empresa+",'"+linkEvernot+"'); " +
                   " SET @ultimoId = SCOPE_IDENTITY(); SELECT @ultimoId AS id ;";
                consulta = BdDatos.CargarTabla(sql);
                foreach (DataRow row in consulta.Rows)
                {
                    mensaje = row["id"].ToString();
                }
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT EVENTO lite_tipo_fuente)";
            }
            return mensaje;
        }
        public SqlDataReader PoblarEstadoObra()
        {
            string sql;

            sql = "SELECT  est_obr_id, est_obr_descripcion FROM obra_estados WHERE     (est_obr_activo = 1)";

            return BdDatos.ConsultarConDataReader(sql);
        }
        public SqlDataReader PoblarTipoSeguimiento(int na)
        {
            string sql;
            if (na == 1)
            {
                sql = "SELECT   obrtiseg_id as idTipoSeg, obrtiseg_descripcion as tipoSeguimiento FROM  obra_tipo_seguimiento WHERE (obrtiseg_activo = 1)  AND (obrtiseg_Ordenador <>3) ORDER BY obrtiseg_Ordenador ASC";
            }
            else if (na == 2)
            {
                sql = "SELECT   obrtiseg_id as idTipoSeg, obrtiseg_descripcion as tipoSeguimiento FROM  obra_tipo_seguimiento WHERE (obrtiseg_Ordenador = 3) and (obrtiseg_activo = 1) ORDER BY obrtiseg_Ordenador ASC";
            }
            else
            {
                sql = "SELECT   obrtiseg_id as idTipoSeg, obrtiseg_descripcion as tipoSeguimiento FROM  obra_tipo_seguimiento WHERE (obrtiseg_activo = 1) ORDER BY obrtiseg_Ordenador ASC";
            }
            return BdDatos.ConsultarConDataReader(sql);
        }


        public int cerrarConexion()
        {
            return BdDatos.desconectar();
        }

        //LLAMAMOS EL PROCEDIMIENTO DE INGRESO DE OBRA
        public int Obra(int pais, int ciudad, int cliente, string obra, string direccion, string tele1, string tele2,
            int unidad, int estrato, decimal m2, int tipovivienda, string representante, string prefijo1, string prefijo2,
            string fechaini, string fechafin, int estado, int tecnico, string comentario, int fuente, int activa, string periodosim, int tipoSegmento, int desa,int a, int b)
        {

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
            int cli_CM = 1;

            // Parametros de la BBDD              
            SqlParameter[] sqls = new SqlParameter[24];

            sqls[0] = new SqlParameter("Pais", pais);
            sqls[1] = new SqlParameter("Ciudad", ciudad);
            sqls[2] = new SqlParameter("Cliente", cliente);
            sqls[3] = new SqlParameter("Nombre", obra);
            sqls[4] = new SqlParameter("Direccion", direccion);
            sqls[5] = new SqlParameter("Telefono", tele1);
            sqls[6] = new SqlParameter("Telefono2", tele2);
            sqls[7] = new SqlParameter("Unidades", unidad);
            sqls[8] = new SqlParameter("Estrato", estrato);
            sqls[9] = new SqlParameter("M2", m2);
            sqls[10] = new SqlParameter("TipoVivienda", tipovivienda);
            sqls[11] = new SqlParameter("Usuario", representante);
            sqls[12] = new SqlParameter("Fecha", fecha);
            sqls[13] = new SqlParameter("Prefijo1", prefijo1);
            sqls[14] = new SqlParameter("Prefijo2", prefijo2);
            sqls[15] = new SqlParameter("Fechaini", fechaini);
            sqls[16] = new SqlParameter("Fechafin", fechafin);
            sqls[17] = new SqlParameter("Estado", estado);
            sqls[18] = new SqlParameter("Tecnico", tecnico);
            sqls[19] = new SqlParameter("Comentario", comentario);
            //==stiven palacios========================================
            sqls[20] = new SqlParameter("Fuente", fuente);
            sqls[21] = new SqlParameter("Activa", activa);
            sqls[22] = new SqlParameter("Periodosim", periodosim);
            sqls[23] = new SqlParameter("TipoSegmento", tipoSegmento);
            //=========================================================

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarOBRAnew", con))
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

        public void LlenarComboSegmento(DropDownList ListaSegmento)
        {
            //Instancia de variable tipo DataTable
            DataTable dt = new DataTable();
            //Cadena de consulta
            string sql = "SELECT segobr_id, segobr_descripcion FROM Obra_Segmento WHERE segobr_activo=1";
            //Al la tabla le asignamos lo que recupera de la consulta
            dt = BdDatos.CargarTabla(sql);
            //Recorre todas las filas de la tabla una a una
            foreach (DataRow row in dt.Rows)
            {
                //Crea una lista los dos parametros
                ListItem lst = new ListItem(row["segobr_descripcion"].ToString(),//Valor mostrado al usuario
                                            row["segobr_id"].ToString());// Valor real del campo
                //Asignamos al combo los items que se capturaron en el lst
                ListaSegmento.Items.Insert(ListaSegmento.Items.Count, lst);
            }
        }

        public void LlenarComboSegmentoTipo(DropDownList ListaSegmentoTipo, int Segmento)
        {
            //Instancia de variable tipo DataTable
            DataTable dt = new DataTable();
            //Cadena de consulta
            string sql = "SELECT obrtipo_id, obrtipo_descripcion FROM Obra_Segmento_Tipo " +
                         " WHERE obrtipo_activo=1 AND segobr_id=" + Segmento + " ";

            //Al la tabla le asignamos lo que recupera de la consulta
            dt = BdDatos.CargarTabla(sql);
            //Recorre todas las filas de la tabla una a una
            foreach (DataRow row in dt.Rows)
            {
                //Crea una lista los dos parametros
                ListItem lst = new ListItem(row["obrtipo_descripcion"].ToString(),//Valor mostrado al usuario
                                            row["obrtipo_id"].ToString());// Valor real del campo
                //Asignamos al combo los items que se capturaron en el lst
                ListaSegmentoTipo.Items.Insert(ListaSegmentoTipo.Items.Count, lst);
            }
        }

        public void LlenarComboTipoVivienda(DropDownList ListaTipoVivienda)
        {
            //Instancia de variable tipo DataTable
            DataTable dt = new DataTable();
            //Cadena de consulta
           string sql = "SELECT  fdom_CodDominio, fdom_Descripcion FROM    fup_Dominios " +
                        " WHERE (fdom_Dominio = 'TIPO_VIVIENDA') ORDER BY fdom_OrdenDominio";

            //Al la tabla le asignamos lo que recupera de la consulta
            dt = BdDatos.CargarTabla(sql);
            //Recorre todas las filas de la tabla una a una
            foreach (DataRow row in dt.Rows)
            {
                //Crea una lista los dos parametros
                ListItem lst = new ListItem(row["fdom_Descripcion"].ToString(),//Valor mostrado al usuario
                                            row["fdom_CodDominio"].ToString());// Valor real del campo
                //Asignamos al combo los items que se capturaron en el lst
                ListaTipoVivienda.Items.Insert(ListaTipoVivienda.Items.Count, lst);
            }
        }


        public int ObraLite(int pais, int ciudad, int cliente, string obra, string direccion, string tele1, string tele2,
          int unidad, int estrato, decimal m2, string tipovivienda, string representante, string prefijo1, string prefijo2,
          int estado, int tecnico, string id_cli_sim, int fila, string archivo, int idObra, int fuente, string id_sim_obra,
          string periodo_sim, string descripcion, string geo_latitud, string geo_longitud, string url_obra,
          int tipo_segmento, string fecha_ini, string fecha_fin ,int sistemaConstru,int sis,int a, int b)
        {

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;
            //string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

            //Convierte el valor en formato date corta
            DateTime dateini, datefin;
            if (fecha_ini != "")
            {
                dateini = DateTime.Parse(fecha_ini);
                fecha_ini = dateini.ToString("dd/MM/yyyy");
            }
            if (fecha_fin != "")
            {
                datefin = DateTime.Parse(fecha_fin);
                fecha_fin = datefin.ToString("dd/MM/yyyy");
            }


            string fecha = "";
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[32];
            sqls[0] = new SqlParameter("Pais", pais);
            sqls[1] = new SqlParameter("Ciudad", ciudad);
            sqls[2] = new SqlParameter("Cliente", cliente);
            sqls[3] = new SqlParameter("Nombre", obra);
            sqls[4] = new SqlParameter("Direccion", direccion);
            sqls[5] = new SqlParameter("Telefono", tele1);
            sqls[6] = new SqlParameter("Telefono2", tele2);
            sqls[7] = new SqlParameter("Unidades", unidad);
            sqls[8] = new SqlParameter("Estrato", estrato);
            sqls[9] = new SqlParameter("M2", m2);
            sqls[10] = new SqlParameter("TipoVivienda", tipovivienda);
            sqls[11] = new SqlParameter("Usuario", representante);
            sqls[12] = new SqlParameter("Fecha", fecha);
            sqls[13] = new SqlParameter("Prefijo1", prefijo1);
            sqls[14] = new SqlParameter("Prefijo2", prefijo2);
            sqls[15] = new SqlParameter("Estado", estado);
            sqls[16] = new SqlParameter("Tecnico", tecnico);
            sqls[17] = new SqlParameter("IdCliSIM", id_cli_sim);
            sqls[18] = new SqlParameter("fila", fila);
            sqls[19] = new SqlParameter("archivo", archivo);
            sqls[20] = new SqlParameter("id", idObra);
            sqls[21] = new SqlParameter("fuente", fuente);
            sqls[22] = new SqlParameter("id_sim_obra", id_sim_obra);
            sqls[23] = new SqlParameter("periodo_sim", periodo_sim);
            sqls[24] = new SqlParameter("descripcion", descripcion);
            sqls[25] = new SqlParameter("geo_latitud", geo_latitud);
            sqls[26] = new SqlParameter("geo_longitud", geo_longitud);
            sqls[27] = new SqlParameter("url_obra", url_obra);
            sqls[28] = new SqlParameter("tipo_segmento", tipo_segmento);
            sqls[29] = new SqlParameter("fecha_Ini", fecha_ini);
            sqls[30] = new SqlParameter("fecha_Fin", fecha_fin);
            sqls[31] = new SqlParameter("sistemaConstru", sistemaConstru);
  

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarObraLite", con))
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

        public int ActualizarObra(int idObra, int pais, int ciudad, string obra, string direccion, string telefono,
            string telefono2, int estrato, int vivienda, int unidades, decimal m2, int cliente, string usuario,        //==stiven palacios================
            string prefijo1, string prefijo2, string fechaini, string fechafin, int tecnico, string comentario,
            int fuente, int estado, int alquiler, int venta, int tipoSegmento, int desa)
        //==stiven palacios=================
        {
            string sql;
            string fecha = System.DateTime.Now.ToShortDateString();

            sql = "UPDATE obra SET obr_pai_id = " + pais + ", obr_ciu_id = " + ciudad + ", obr_nombre = '" + obra +
                "', obr_direccion = '" + direccion + "', obr_telef = '" + telefono + "', obr_telef2 = '" + telefono2 +
                "', obr_ese_id = " + estrato + ", obr_tipo_vivienda = '" + vivienda + "', obr_total_unidades = " + unidades +
                ", obr_m2_vivienda = '" + m2 + "', obr_cli_id = " + cliente + ", usu_actualiza = '" + usuario +
                "', fecha_actualiza = '" + fecha + "',obr_prefijo1 = '" + prefijo1 + "', obr_prefijo2= '" + prefijo2 +                                                                   //==stiven palacios=================
                "', obr_fecha_ini = '" + fechaini + "',obr_fecha_fin = '" + fechafin + "',obr_tecnico =" + tecnico + ", " +
                " obr_descripcion ='" + comentario + "' ,obr_fuente_id=" + fuente + ",obr_estado=" + estado + ", " +
                " obr_alquiler=" + alquiler + ",obr_venta=" + venta + ",obr_tipo_segmento_id=" + tipoSegmento + " " +
                " WHERE obr_id = " + idObra + " ";
            return BdDatos.Actualizar(sql);
        }

        public int ActualizarObraFup(int idObra,int estrato, string vivienda, int unidades, decimal m2, string usuario)
        {
            string sql;
            string fecha = System.DateTime.Now.ToShortDateString();

            sql = "UPDATE obra SET obr_ese_id = " + estrato + ", obr_tipo_vivienda = '" + vivienda + "', obr_total_unidades = " + unidades +
                ", obr_m2_vivienda = '" + m2 + "', usu_actualiza = '" + usuario +
                "', fecha_actualiza = '" + fecha + "' WHERE obr_id = " + idObra + " ";

            return BdDatos.Actualizar(sql);
        }

        public SqlDataReader ConsultarObra(int obra)
        {
            string sql;
            sql = "SELECT        o.obr_pai_id, pa.pai_nombre, o.obr_ciu_id, ciu.ciu_nombre, o.obr_nombre, o.obr_direccion, o.obr_telef, o.obr_telef2, o.obr_ese_id, es.ese_descripcion, es.ese_desc_ingles, es.ese_desc_portugues, " +
                           " o.obr_tipo_vivienda, o.obr_total_unidades, o.obr_m2_vivienda, o.obr_cli_id, cl.cli_nombre, cl.cli_pai_id, pacli.pai_nombre AS Expr1, cl.cli_ciu_id, ciucli.ciu_nombre AS Expr2, o.obr_prefijo1, o.obr_prefijo2,  " +
                           " ISNULL(o.obr_fecha_ini, '') AS Expr3, ISNULL(o.obr_fecha_fin, '') AS Expr4, ISNULL(o.obr_estado, 0) AS idestado, ISNULL(obra_estados.est_obr_descripcion, 'Seleccione el Estado') AS estadodescrip,  " +
                           " ISNULL(o.obr_tecnico, 0) AS idtecnico, ISNULL(empleado.emp_nombre_mayusculas + ' ' + empleado.emp_apellidos_mayusculas, 'Seleccione el Tecnico') AS tecnicodescrip, o.usu_actualiza,  " +
                           " ISNULL(o.fecha_actualiza, '') AS Expr5, ISNULL(o.obr_descripcion, '') AS comentario,lite_tipo_fuente.tifuente_id AS fuente, o.obr_url as url, isnull(o.obr_descripcion, '') as descripcion, " +
                           " lite_tipo_fuente.tifuente_origen_id AS Origen,obr_alquiler,obr_venta, " +
                           " Obra_Segmento_Tipo.segobr_id AS segmento, o.obr_tipo_segmento_id AS tipo_Segmento " +//==stiven palacios===
                           " FROM  obra AS o INNER JOIN pais AS pa ON o.obr_pai_id = pa.pai_id INNER JOIN " +
                           " ciudad AS ciu ON o.obr_ciu_id = ciu.ciu_id INNER JOIN " +
                           " estado_socioeconomico AS es ON o.obr_ese_id = es.ese_id INNER JOIN " +
                           " cliente AS cl ON o.obr_cli_id = cl.cli_id INNER JOIN " +
                           " pais AS pacli ON pacli.pai_id = cl.cli_pai_id INNER JOIN " +
                           " ciudad AS ciucli ON cl.cli_ciu_id = ciucli.ciu_id INNER JOIN " +
                           " Obra_Segmento_Tipo ON o.obr_tipo_segmento_id = Obra_Segmento_Tipo.obrtipo_id LEFT OUTER JOIN " +
                           " lite_tipo_fuente ON o.obr_fuente_id = lite_tipo_fuente.tifuente_id LEFT OUTER JOIN " +
                           " obra_estados ON o.obr_estado = obra_estados.est_obr_id LEFT OUTER JOIN " +
                           " empleado ON o.obr_tecnico = empleado.emp_usu_num_id " +
                     " WHERE obr_id = " + obra + " ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public void insertLogObraLite(int id, int fila, string archivo, string usuario, string observacion, string nombre, int pais, int ciudad)
        {
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
            string sql = "INSERT INTO LogObraLite(obra_id, fila_excel, observacion, nombre_archivo, fecha, usuario, estado, nombre_obra, pais, ciudad, ultimo_registro) VALUES(" + id + "," + fila + ", '" + observacion + "' , '" + archivo + "' , '" + fecha + "' , '" + usuario + "', 0, '" + nombre + "', " + pais + ", " + ciudad + ", 1)";
            BdDatos.Actualizar(sql);
        }
        //==stiven palacios=================================================================================
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

        public DataTable ToolTip_Fuente(int idfuente)
        {
            string sql = "SELECT tifuente_descripcion " +
                             " FROM lite_tipo_fuente " +
                             " WHERE tifuente_id=" + idfuente + "";
            return BdDatos.CargarTabla(sql);
        }

      //Actualiza el estado de la obra
        public int Anular_Obra(int obra, int anulada)
        {
            string sql = "UPDATE obra SET obr_activa=" + anulada.ToString() + " where obr_id=" + obra.ToString() + "";
            return BdDatos.ejecutarSql(sql);
        }

        //Guarda el log de actualizacion la tabla obra
        public int Met_Insertar_Log_ActualizaObra(string tabla, int idpais, string campo, string usuario, string fecha, string valor, string valorNuevo, string evento)
        {
            string sqlinsert = "INSERT INTO Log_transacciones VALUES('" + tabla + "'," + idpais.ToString() + ",'" + campo + "'," +
                                                                   " '" + usuario + "','" + fecha + "','" + valor + "','" + valorNuevo + "', " +
                                                                   " '" + evento + "')";
            return BdDatos.ejecutarSql(sqlinsert);
        }

        //Insertar el log de anulacion de la tabla obra
        public int Met_Insertar_Log_AnulaObra(string tabla, int meprid, string usuario, string fecha, string evento)
        {
            string sqlinsertlogdel = "insert into Log_transacciones  (genlog_tabla,genlog_id_registro,genlog_usuario,genlog_fecha,genlog_Evento) values('" + tabla + "'," + meprid.ToString() + ",'" + usuario + "','" + fecha + "','" + evento + "')";

            return BdDatos.ejecutarSql(sqlinsertlogdel);
        }
        //===================================================================================

        //Insertar el log de fecha inicial
        public int InsertarLogFecIniDesp(int actseg_id ,string usuario, string fecha)
        {
            string sqlinsertlogdel = "insert into fup_acta_seguimiento_log (actseg_id,actlog_campo,actlog_usuario,actlog_descripcion,actlog_valor) values (" + actseg_id + ",'actseg_fecIni_Desp','" + usuario + "','Fecha Inicial Desp','" + fecha + "')";

            return BdDatos.ejecutarSql(sqlinsertlogdel);
        }

        public void LlenarComboEmpresaCompe(DropDownList ListaEmpreCompe)
        {
            //Instancia de variable tipo DataTable
            DataTable dt = new DataTable();
            //Cadena de consulta
            string sql = "SELECT cli_id,cli_nombre FROM cliente WHERE cli_activo=1 AND cli_competencia=1";
            //Al la tabla le signamos lo que recupera de la consulta
            dt = BdDatos.CargarTabla(sql);
            //Recorre todas las filas de la tabla una a una
            foreach (DataRow row in dt.Rows)
            {
                //Crea una lista los dos parametros
                ListItem lst = new ListItem(row["cli_nombre"].ToString(),//Valor mostrado al usuario
                                            row["cli_id"].ToString());// Valor real del campo
                //Asignamos al combo los items que se capturaron en el lst
                ListaEmpreCompe.Items.Insert(ListaEmpreCompe.Items.Count, lst);
            }

        }

        /// <summary>
        /// Metodo encargado de consultar los estados socio economicos
        /// Autor: Global BI
        /// Fecha: 09-09-2018
        /// </summary>
        /// <returns>Lista de estados socio economicos</returns>
        public List<EstadoSocioEconomico> obtenerEstadoSocioEconomico()
        {
            string sql;

            sql = "SELECT ese_id, ese_descripcion, ese_desc_ingles, ese_desc_portugues, ese_activo, ese_orden FROM estado_socioeconomico " +
            "WHERE (ese_activo = 1) ORDER BY ese_orden";
            DataTable dt = BdDatos.CargarTabla(sql);
            List<EstadoSocioEconomico> lstObject = dt.AsEnumerable()
                .Select(row => new EstadoSocioEconomico
                {
                    Id = (int)row["ese_id"],
                    Descripcion = (string)row["ese_descripcion"]
                }).ToList();
            dt.Clear();
            dt.Dispose();
            return lstObject;
        }

        /// <summary>
        /// Metodo encargado de consultar los datos de una obra especifica
        /// Autor: Global BI
        /// Fecha: 09-09-2018
        /// </summary>
        /// <returns>ObraInformacion</returns>
        public ObraInformacion obtenerDatosObra(int obra)
        {
            string sql;
            sql = "SELECT        o.obr_pai_id, pa.pai_nombre, o.obr_ciu_id, ciu.ciu_nombre, o.obr_nombre, o.obr_direccion, o.obr_telef, o.obr_telef2, o.obr_ese_id, es.ese_descripcion, es.ese_desc_ingles, es.ese_desc_portugues, " +
                         " o.obr_tipo_vivienda, o.obr_total_unidades, o.obr_m2_vivienda, o.obr_cli_id, cl.cli_nombre, cl.cli_pai_id, pacli.pai_nombre AS Expr1, cl.cli_ciu_id, ciucli.ciu_nombre AS Expr2, o.obr_prefijo1, o.obr_prefijo2,  " +
                         " ISNULL(CONVERT(VARCHAR(10),o.obr_fecha_ini,120), '1900-01-01') AS Expr3, ISNULL(o.obr_fecha_fin, '') AS Expr4, ISNULL(o.obr_estado, 0) AS idestado, ISNULL(obra_estados.est_obr_descripcion, 'Seleccione el Estado') AS estadodescrip,  " +
                         " ISNULL(o.obr_tecnico, 0) AS idtecnico, ISNULL(empleado.emp_nombre_mayusculas + ' ' + empleado.emp_apellidos_mayusculas, 'Seleccione el Tecnico') AS tecnicodescrip, o.usu_actualiza,  " +
                         " ISNULL(o.fecha_actualiza, '') AS Expr5, ISNULL(o.obr_descripcion, '') AS comentario,lite_tipo_fuente.tifuente_id AS fuente, ISNULL(o.obr_url,'') as url, isnull(o.obr_descripcion, '') as descripcion " +
                         " ,lite_tipo_fuente.tifuente_origen_id AS Origen,obr_alquiler,obr_venta, isnull(o.obr_total_und_forsa,o.obr_total_unidades) obr_total_und_forsa, dom.fdom_Descripcion  " +//==stiven palacios===
                          " FROM            obra AS o INNER JOIN  " +
                         " pais AS pa ON o.obr_pai_id = pa.pai_id INNER JOIN  " +
                         " ciudad AS ciu ON o.obr_ciu_id = ciu.ciu_id INNER JOIN  " +
                         " estado_socioeconomico AS es ON o.obr_ese_id = es.ese_id INNER JOIN  " +
                         " cliente AS cl ON o.obr_cli_id = cl.cli_id INNER JOIN  " +
                         " pais AS pacli ON pacli.pai_id = cl.cli_pai_id INNER JOIN  " +
                         " ciudad AS ciucli ON cl.cli_ciu_id = ciucli.ciu_id LEFT OUTER JOIN  " +
                         " lite_tipo_fuente ON o.obr_fuente_id = lite_tipo_fuente.tifuente_id LEFT OUTER JOIN  " +
                         " obra_estados ON o.obr_estado = obra_estados.est_obr_id LEFT OUTER JOIN  " +
                         " empleado ON o.obr_tecnico = empleado.emp_usu_num_id INNER JOIN  " +
                         " fup_Dominios AS dom ON o.obr_tipo_vivienda = dom.fdom_CodDominio  " +
                " WHERE obr_id = " + obra + " AND dom.fdom_Dominio = 'TIPO_VIVIENDA' ";

            DataTable dt = BdDatos.CargarTabla(sql);
            List<ObraInformacion> lstObject = dt.AsEnumerable()
                .Select(row => new ObraInformacion
                {
                    IdObra = obra,
                    IdPais = (int)row["obr_pai_id"],
                    IdCiudad = (int)row["obr_ciu_id"],
                    NombreCiudad = (string)row["ciu_nombre"],
                    IdEstratoSocioEconomico = (int)row["obr_ese_id"],
                    ObraTipoVivienda = (string)row["obr_tipo_vivienda"],
                    TotalUnidades = (int)row["obr_total_unidades"],
                    M2Vivienda = (decimal)row["obr_m2_vivienda"],
                    TotalUnidadesForsa = (int)row["obr_total_und_forsa"],
                    TipoViviendaDominioDescripcion = (string)row["fdom_Descripcion"],
                    url = (string)row["url"],
                    FechaInicio = (string)row["Expr3"]
                }).ToList();
            dt.Clear();
            dt.Dispose();

            ObraInformacion obraInformacion = new ObraInformacion();
            if(lstObject != null && lstObject.Count > 0)
            {
                obraInformacion = lstObject.FirstOrDefault();
            }
            return obraInformacion;
        }
    }
}
