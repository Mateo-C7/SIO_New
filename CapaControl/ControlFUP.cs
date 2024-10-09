using System;
using System.Data;
using System.Data.SqlClient;
using CapaDatos;
using CapaControl.Entity;
using System.Collections.Generic;
using System.Linq;

namespace CapaControl
{
    public class ControlFUP
    {
        public DataSet ConsultarIdiomaContacto()
        {
            string sql;
            sql = "SELECT idioma_entrada_cotizacion.* FROM idioma_entrada_cotizacion";
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }

        //INGRESO COMENTARIOS CONTROL DE CAMBIOS
        public int ActCOMENCAMBIOS(int id, int idtema, string comtema, string nombre, int accion)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            string Fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[6];
            sqls[0] = new SqlParameter("pCcc_id", id);
            sqls[1] = new SqlParameter("pControlID", idtema);
            sqls[2] = new SqlParameter("pFecha", Fecha);
            sqls[3] = new SqlParameter("pComentario", comtema);
            sqls[4] = new SqlParameter("pUsu_crea", nombre);
            sqls[5] = new SqlParameter("pAccion", accion);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_CntrlCam_Comentario", con))
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

        //CAMBIAR ESTADO TEMA
        public SqlDataReader EstadoTema(int tema)
        {
            string sql, Fecha = System.DateTime.Now.ToString("dd/MM/yyyy"); ;

            sql = "UPDATE  fup_control_cambios SET coc_Estado = 2, coc_fecha_cierre = '" + Fecha + "' WHERE coc_id = " + tema + "";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public DataSet ConsultarTema(int fup)
        {
            string sql;
            sql = "EXECUTE USP_fup_SEL_ControlCambio '" + fup + "' ";
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }

        //INGRESO CONTROL DE CAMBIOS
        public int ActCAMBIOS(int id, int numfup, string tema, string fecCierre, int area, int usu_responsable, string nombre,
            int accion)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            string Fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[9];
            sqls[0] = new SqlParameter("pCoc_id", id);
            sqls[1] = new SqlParameter("pFup_id", numfup);
            sqls[2] = new SqlParameter("pTema", tema);
            sqls[3] = new SqlParameter("pFecApertura", Fecha);
            sqls[4] = new SqlParameter("pFecCierre", fecCierre);
            sqls[5] = new SqlParameter("pArea_responsable", area);
            sqls[6] = new SqlParameter("pUsu_responsable", usu_responsable);
            sqls[7] = new SqlParameter("pUsu_crea", nombre);
            sqls[8] = new SqlParameter("pAccion", accion);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_ControlCambios", con))
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

        //CREACION DE FLETE
        public int IngFlete(int numfup, string ver, int trans, int agent, int ptoOrig, int ptoDest, int tdn, int lead, int c1, int c2, int c3,
            string vrOrig1, string vrOrig2, string vrOrig3, string vrGastOrig, string vrAduaOrig, string vrInt1, string vrInt2, string vrGastDest,
            string vrAduanDest, string vrDest1, string vrDest2, string usu, int c4, string vrOrig4, string Seguro)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[26];
            sqls[0] = new SqlParameter("pFupID", numfup);
            sqls[1] = new SqlParameter("pVersion", ver);
            sqls[2] = new SqlParameter("transportador_id", trans);
            sqls[3] = new SqlParameter("agente_carga_id", agent);
            sqls[4] = new SqlParameter("puerto_origen_id", ptoOrig);
            sqls[5] = new SqlParameter("puerto_destino_id", ptoDest);
            sqls[6] = new SqlParameter("termino_negociacion_id", tdn);
            sqls[7] = new SqlParameter("leadTime", lead);
            sqls[8] = new SqlParameter("cantidad_t1", c1);
            sqls[9] = new SqlParameter("cantidad_t2", c2);
            sqls[10] = new SqlParameter("cantidad_t3", c3);
            sqls[11] = new SqlParameter("vr_origen_t1", vrOrig1);
            sqls[12] = new SqlParameter("vr_origen_t2", vrOrig2);
            sqls[13] = new SqlParameter("vr_origen_t3", vrOrig3);
            sqls[14] = new SqlParameter("vr_gastos_origen", vrGastOrig);
            sqls[15] = new SqlParameter("vr_aduana_origen", vrAduaOrig);
            sqls[16] = new SqlParameter("vr_internacional_t1", vrInt1);
            sqls[17] = new SqlParameter("vr_internacional_t2", vrInt2);
            sqls[18] = new SqlParameter("vr_gastos_destino", vrGastDest);
            sqls[19] = new SqlParameter("vr_aduana_destino", vrAduanDest);
            sqls[20] = new SqlParameter("vr_destino_t1", vrDest1);
            sqls[21] = new SqlParameter("vr_destino_t2", vrDest2);
            sqls[22] = new SqlParameter("pUsu_crea", usu);
            sqls[23] = new SqlParameter("cantidad_t4", c4);
            sqls[24] = new SqlParameter("vr_origen_t4", vrOrig4);
            sqls[25] = new SqlParameter("vr_seguro", Seguro);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_det_fletes", con))
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

        //POBLAR COMBO PUERTO ORIGEN
        public SqlDataReader CargarPuertoOrigen()
        {
            string sql;

            sql = "SELECT pue_id, pue_descripcion FROM sie.puerto " +
                    "WHERE pue_tipo_puerto_id = 1  AND pue_pais_id = 8";

            return BdDatos.ConsultarConDataReader(sql);
        }

         //CONSULTO LOS CONTENEDORES 
        public SqlDataReader CargarContenedorSalida(int fup , string vers)
        {
            string sql;

            sql = "SELECT  isnull( fup_salida_cotizacion.sct_contenedor20,0), isnull(fup_salida_cotizacion.sct_contenedor40,0), "+
                  "  CASE WHEN pais.pai_id = 8 THEN 'Col' ELSE 'Ext' END "+
		          " FROM         pais INNER JOIN "+
				    " cliente ON pais.pai_id = cliente.cli_pai_id INNER JOIN "+
				    " formato_unico ON cliente.cli_id = formato_unico.fup_cli_id INNER JOIN "+
				    " fup_salida_cotizacion INNER JOIN  "+
				    " fup_enc_entrada_cotizacion ON fup_salida_cotizacion.sct_enc_entrada_cot_id = fup_enc_entrada_cotizacion.eect_id ON  "+
                    " formato_unico.fup_id = fup_enc_entrada_cotizacion.eect_fup_id " +
                  " WHERE     (fup_enc_entrada_cotizacion.eect_fup_id =" + fup + " ) AND (fup_enc_entrada_cotizacion.eect_vercot_id = '" + vers + "') ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        

        //POBLAR COMBO PUERTO DESTINO
        public SqlDataReader CargarPuertoDestino(int pais)
        {
            string sql;

            sql = "SELECT pue_id, pue_descripcion FROM sie.puerto " +
                  "WHERE pue_tipo_puerto_id = 1 AND pue_pais_id = '" + pais + "' ORDER BY pue_descripcion";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public DataSet ConsultarOpcion(int rol)
        {
            string sql;
            sql = "SELECT for_rol_id, for_opc_id, for_select, for_update FROM fup_opcion_rol " +
                "WHERE (for_rol_id = " + rol + ")";
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }

        public DataSet ConsultarComentariosTema(int idComTema)
        {
            string sql;
            sql = "EXECUTE USP_fup_SEL_CtrlCam_Comentarios '" + idComTema + "'";
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }

        public DataSet ConsultarRechazoGrilla(int fup, string ver)
        {
            string sql;
            sql = "SELECT rct_id, CASE WHEN rct_validado =1 THEN 'SI' ELSE 'NO' END VALIDADO, eect_vercot_id VERS, CONVERT(varchar(10),rct_fecha_rechazo,120) FECHA, tr.tre_descripcion TEMA, re.rct_observacion OBSERVACION, re.rct_usu_crea AS RECHAZADO_POR " +
		          "FROM dbo.fup_enc_entrada_cotizacion ec INNER JOIN dbo.fup_rechazo_entrada re " +
                  "ON re.rct_enc_entrada_cot_id = ec.eect_id INNER JOIN fup_tipo_rechazo tr " +
                  "ON tr.tre_id = re.rct_tipo_rechazo_id " +
                  "WHERE eect_fup_id = " + fup + " AND eect_vercot_id = '" + ver + "' AND (tr.tre_tipo = 1)";
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;          
        }

        public SqlDataReader ConsultarValoresSf(int fup, string ver)
        {
            string sql;
            sql = "SELECT        COUNT(Sf_parte) AS Cant_Sf, SUM(Sf_vlr_venta) AS Vlr_Cotizado, " +
                  "100- ((SUM(Sf_vlr_comercial) /  SUM(Sf_vlr_venta) ) * 100)  AS porcDscto,  SUM(Sf_vlr_venta) - SUM(Sf_vlr_comercial)   AS vlrDesc, " +
                  "SUM(Sf_vlr_comercial) AS Vlr_Comercial, stuff((select ' / '+     Sf_razondesc from " +
                  "solicitud_facturacion as p2  WHERE    (Sf_fup_id = "+@fup+") AND (Sf_version = '"+@ver+"')  ORDER BY p2.Sf_id  " +
                  "for xml path('') ), 1, 1, '') as Razon_Descuento, stuff((select ' * '+     usu_crea from "+
                  "solicitud_facturacion as p2  WHERE    (Sf_fup_id = " + @fup + ") AND (Sf_version = '" + @ver + "')  ORDER BY p2.Sf_id  " +
                  "for  xml path('')      ), 1, 1, '') as usuario FROM  solicitud_facturacion WHERE  " +
                  "(Sf_fup_id = "+@fup+") AND (Sf_version = '"+@ver+"') GROUP BY Sf_fup_id, Sf_version";
            //DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return BdDatos.ConsultarConDataReader(sql);
        }

        public DataSet ConsultarRechazoGrillaCom(int fup, string ver)
        {
            string sql;
            sql = "SELECT rct_id, CASE WHEN rct_validado =1 THEN 'SI' ELSE 'NO' END VALIDADO, eect_vercot_id VERS, CONVERT(varchar(10),rct_fecha_rechazo,120) FECHA, tr.tre_descripcion TEMA, re.rct_observacion OBSERVACION, re.rct_usu_crea AS RECHAZADO_POR , re.rct_respuesta as RESPUESTA " +
                  "FROM dbo.fup_enc_entrada_cotizacion ec INNER JOIN dbo.fup_rechazo_entrada re " +
                  "ON re.rct_enc_entrada_cot_id = ec.eect_id INNER JOIN fup_tipo_rechazo tr " +
                  "ON tr.tre_id = re.rct_tipo_rechazo_id " +
                  "WHERE eect_fup_id = " + fup + " AND eect_vercot_id = '" + ver + "' AND (tr.tre_tipo = 2)";
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }

        public DataSet ConsultarRecotizaGrilla(int fup)
        {
            string sql;
            sql = "SELECT CONVERT(varchar(10),rco_fecha,120) FECHA, CHAR(ASCII(LTRIM(eect_vercot_id))+1) VERSION, trc_descripcion MOTIVO, rco_observacion OBSERVACION " +
                  "FROM fup_recotiza_entrada INNER JOIN fup_tipo_recotizacion ON fup_recotiza_entrada.rco_tipo_recotiza_id = fup_tipo_recotizacion.trc_id " +
                  "INNER JOIN fup_enc_entrada_cotizacion ON fup_recotiza_entrada.rco_enc_entrada_cot_id = fup_enc_entrada_cotizacion.eect_id " +
                  "WHERE eect_fup_id = " + fup + " ";
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }

        public SqlDataReader poblarTemaRechazoCom(int fup, string vers)
        {
            string sql;
            sql = "SELECT     re.rct_id, tr.tre_descripcion " +
                 "FROM         fup_enc_entrada_cotizacion AS ec INNER JOIN " +
                      "fup_rechazo_entrada AS re ON re.rct_enc_entrada_cot_id = ec.eect_id INNER JOIN " +
                      "fup_tipo_rechazo AS tr ON tr.tre_id = re.rct_tipo_rechazo_id " +
                 "WHERE     (ec.eect_fup_id =" + fup + ") AND (ec.eect_vercot_id = '" + vers + "') AND (tr.tre_tipo = 2) AND (re.rct_fec_respuesta IS NULL)";
            
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ValidarRecotiza(int fup, string vers)
        {
            string sql;
            sql = "SELECT COUNT(1) " +
                  "FROM fup_recotiza_entrada INNER JOIN fup_tipo_recotizacion ON fup_recotiza_entrada.rco_tipo_recotiza_id = fup_tipo_recotizacion.trc_id " +
                  "INNER JOIN fup_enc_entrada_cotizacion ON fup_recotiza_entrada.rco_enc_entrada_cot_id = fup_enc_entrada_cotizacion.eect_id " +
                  "WHERE eect_fup_id = " + fup + " AND eect_vercot_id = '"+vers+"' ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarRechazoxId(int id)
        {
            string sql;
            sql = "SELECT     rct_id, rct_observacion FROM    fup_rechazo_entrada WHERE     (rct_id =" + @id + ")";                

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ValidarRechazos(int fup, string vers)
        {
            string sql;
            sql = "SELECT COUNT(1) " +
                  "FROM fup_rechazo_entrada re INNER JOIN fup_enc_entrada_cotizacion ec ON re.rct_enc_entrada_cot_id = ec.eect_id  " +
                  "WHERE  ISNULL(re.rct_validado,0) = 0 AND eect_fup_id = " + fup + " AND eect_vercot_id = '" + vers + "' ";

            return BdDatos.ConsultarConDataReader(sql);
        }
        public DataSet ConsultarPlanoForsa(int fup)
        {
            string sql;
            sql = "SELECT eect_vercot_id VERSION, CONVERT(varchar(10),pf.spf_fecha_crea,120) [FECHA SISTEMA], CONVERT(varchar(10),pf.spf_fecha_cierre,120) [FECHA EVENTO], " +
                  "ct.Nombre RESPONSABLE, tes_descripcion EVENTO, pf.spf_observacion OBSERVACION, " +
                  "CASE WHEN pf.spf_enviado= 1 THEN 'SI' ELSE 'NO' END ENVIADO " +
                  "FROM dbo.fup_enc_entrada_cotizacion ec INNER JOIN dbo.fup_seg_plano_forsa pf " +
                  "ON pf.spf_enc_entrada_cot_id = ec.eect_id INNER JOIN " +
                  " fup_tipo_evento_segpf ev ON pf.spf_evento_id = ev.tes_id INNER JOIN " +
                  "(SELECT u.usu_emp_usu_num_id, e.emp_nombre + ' ' + e.emp_apellidos AS Nombre " +
                  "FROM usuario u INNER JOIN empleado e ON u.usu_emp_usu_num_id = e.emp_usu_num_id " +
                  "WHERE u.usu_rap_id in (24,26)) ct ON ct.usu_emp_usu_num_id = pf.spf_responsable_id " +
                  "WHERE eect_fup_id = " + fup + " ORDER BY eect_vercot_id DESC, spf_fecha_crea DESC";
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }

        //CONSULTA DE ARCHIVOS
        public DataSet ConsultarArchivoForsa(int fup, string ver)
        {
            string sql;
            sql = "SELECT id_plano, plano_version VERS, plano_consecutivo CONS, ISNULL(tp.ftpr_descripcion,'NO DEFINIDO') [TIPO_PROY] " +
                ",ISNULL(ta.tan_desc_esp,'NO DEFINIDO') [TIPO_DOC] ,plano_of_referencia  [OF_REFERENCIA] " +
                ",p.plano_ruta_archivo + p.plano_nombre_real AS [RUTA] , CONVERT(char(10), p.fecha_crea, 103) AS Fecha  " +
                "FROM Plano p  LEFT OUTER JOIN fup_tipo_proyectos tp ON tp.ftpr_id = p.plano_tipo_proyecto_id " +
                "LEFT OUTER JOIN fup_tipo_anexo ta ON ta.tan_id = p.plano_tipo_anexo_id " +
                "WHERE id_fup_plano = " + fup + " AND plano_version = '" + ver + "' AND p.plano_tipo_anexo_id <> 99 AND p.plano_tipo_anexo_id <> 6 AND p.anulado = 0  " +
                "ORDER BY id_plano DESC ";
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }


        public DataTable ConsultarArchivoForsa2(int fup, string ver)
        {
            string sql;
            sql = "SELECT id_plano, plano_version VERS, plano_consecutivo CONS, ISNULL(tp.ftpr_descripcion,'NO DEFINIDO') [TIPO_PROY] " +
                ",ISNULL(ta.tan_desc_esp,'NO DEFINIDO') [TIPO_DOC] ,plano_of_referencia  [OF_REFERENCIA] " +
                ",p.plano_ruta_archivo,  p.fecha_envio AS Fecha , p.fecha_envio,plano_nombre_real as [NOMBRE_ARCHIVO] " +
                "FROM Plano p  LEFT OUTER JOIN fup_tipo_proyectos tp ON tp.ftpr_id = p.plano_tipo_proyecto_id " +
                "LEFT OUTER JOIN fup_tipo_anexo ta ON ta.tan_id = p.plano_tipo_anexo_id " +
                "WHERE id_fup_plano = " + fup + " AND plano_version = '" + ver + "' AND (p.plano_tipo_anexo_id = 6 or p.plano_tipo_anexo_id = 8)  " +
                "ORDER BY p.fecha_envio DESC ";
            return BdDatos.CargarTabla(sql);
        }


        public DataTable Consultar_CartasCotiza(int fup, string ver)
        {
            string sql;
            sql = " SELECT plano_nombre_real as [NOMBRE_ARCHIVO] " +
                " FROM Plano p LEFT OUTER JOIN fup_tipo_proyectos tp ON tp.ftpr_id = p.plano_tipo_proyecto_id " +
                " LEFT OUTER JOIN fup_tipo_anexo ta ON ta.tan_id = p.plano_tipo_anexo_id " +
                " WHERE id_fup_plano = " + fup + " AND plano_version = '" + ver + "' AND (p.plano_tipo_anexo_id = 6 or p.plano_tipo_anexo_id = 8)  " +
                " ORDER BY p.fecha_envio DESC ";

            return BdDatos.CargarTabla(sql);
        }



        //CONSULTA DE ARCHIVOS
        public DataSet ConsultarCartas(int fup, string ver)
        {
            string sql;
            sql = "SELECT id_plano, plano_version VERS, plano_consecutivo CONS, ISNULL(tp.ftpr_descripcion,'NO DEFINIDO') [TIPO_PROY] " +
                ",ISNULL(ta.tan_desc_esp,'NO DEFINIDO') [TIPO_DOC] ,plano_of_referencia  [OF_REFERENCIA] " +
                ",p.plano_ruta_archivo + p.plano_nombre_real AS [RUTA] ,  p.fecha_sube AS Fecha , p.fecha_envio,plano_nombre_real as nombreCarta " +
                "FROM Plano p  LEFT OUTER JOIN fup_tipo_proyectos tp ON tp.ftpr_id = p.plano_tipo_proyecto_id " +
                "LEFT OUTER JOIN fup_tipo_anexo ta ON ta.tan_id = p.plano_tipo_anexo_id " +
                "WHERE id_fup_plano = " + fup + " AND plano_version = '" + ver + "' AND ( p.plano_tipo_anexo_id = 6 or p.plano_tipo_anexo_id = 8)  " +
                "ORDER BY p.fecha_sube DESC ";
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }

        public DataSet ConsultarOFs(int fup, string ver)
        {
            string sql;
            sql = "SELECT   o.Id_Ofa, o.Tipo_Of AS TIPO, o.Numero + '-' + o.ano + ' ' + planta_forsa.planta_pre AS ORDEN, planta_forsa.planta_descripcion AS PRODUCIDO_EN, " +
                  "  o.ord_version AS VERSION, o.ord_parte AS PARTE, CONVERT(varchar(10), S.fecha_crea_of, 120) AS FECHA, ISNULL(o.Resp_Ingenieria, '') AS RESPONSABLE,solicitud_facturacion.sf_m2 as M2, solicitud_facturacion.Sf_vlr_comercial as VALOR, " +
                  "  CAST(SUM(ISNULL(Saldos.Cant_Final_Req, 0) * ISNULL(Saldos.Area, 0)) AS decimal(18, 2)) AS m2Prod, CAST(solicitud_facturacion.sf_m2 - SUM(ISNULL(Saldos.Cant_Final_Req, 0) * ISNULL(Saldos.Area, 0)) AS decimal(18, 2)) AS m2Diferencia " +
                  "  FROM   Orden AS o INNER JOIN Orden_Seg AS S ON S.Id_Ofa = o.Id_Ofa INNER JOIN planta_forsa ON S.planta_id = planta_forsa.planta_id INNER JOIN "+
                  "  solicitud_facturacion ON S.sf_id = solicitud_facturacion.Sf_id LEFT OUTER JOIN Saldos ON o.Id_Ofa = Saldos.Id_Ofa_P " +
             " WHERE o.Yale_Cotiza = " + fup + " AND O.Anulada = 0 AND o.ord_version = '" + ver + "' " +
             " GROUP BY o.Id_Ofa, o.Tipo_Of, o.Numero + '-' + o.ano + ' ' + planta_forsa.planta_pre, planta_forsa.planta_descripcion, o.ord_version, o.ord_parte, CONVERT(varchar(10), S.fecha_crea_of, 120), ISNULL(o.Resp_Ingenieria, ''),  " +
             " solicitud_facturacion.sf_m2, solicitud_facturacion.Sf_vlr_comercial  ";
                        
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }

        //CONSULTA DE FUP POR OF COLOMBIA
        public SqlDataReader consultarFUPOrdenFabricacionColombia(string txtNumero)
        {
            string sql;

            sql = "SELECT Orden.Yale_Cotiza, Orden.ord_version FROM Orden WHERE (Orden.Ofa LIKE '%" + txtNumero + "%') AND (Orden.letra = '1')";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA DE RECHAZO COMERCIAL
        public SqlDataReader consultarRechazoComercial(int fup, string version)
        {
            string sql;

            sql = "SELECT eect_rechazo_com FROM fup_enc_entrada_cotizacion WHERE (eect_fup_id ="+ fup+") AND (eect_vercot_id = '"+version+"')";
            
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA DE OF POR FUP COLOMBIA
        public SqlDataReader consultarOrdenFabricacionColombiaFUP(int fup, string ver)
        {
            string sql;

            sql = "SELECT Orden.Numero, Orden.ano FROM Orden WHERE (Orden.Yale_Cotiza = " + fup + ") " +
                "AND (Orden.ord_version = '" + ver + "') AND (Orden.letra = '1') AND (Orden.Anulada = 0) ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA DE OF POR FUP COLOMBIA
        public SqlDataReader consultarProbabilidad(int fup, string ver)
        {
            string sql;

            sql = " SELECT        fup_actaseg_probCierre.aspc_id, fup_actaseg_probCierre.aspc_descripcion, SUBSTRING(ISNULL(cast (fup_acta_seguimiento.actseg_mes_factura as varchar (max)),'1900/01/01'), 0, 11) AS fecha " +
            " FROM            fup_acta_seguimiento INNER JOIN " +
            "    fup_actaseg_probCierre ON fup_acta_seguimiento.actseg_Prob_cierre = fup_actaseg_probCierre.aspc_id " +
            " WHERE        (fup_acta_seguimiento.actseg_fup_id =" + fup + ") AND (fup_acta_seguimiento.actseg_version ='" + ver + "')";
                       
            return BdDatos.ConsultarConDataReader(sql);
        }


        //CREACION DEL FUP Y ENTRADA DE COTIZACION
        public int IngFUP(int cli, int cont, int obr, string nom, bool FAlum, bool FPlast,
            bool FAcero, int tipcot, bool acce, int numequip, bool planoTF, bool cotRap,
            string alcance, string descrip, int moneda, int clase, int fupAnterior,int tipoVentaProyecto)
        {
            int ForsaAlum = 0, ForsaPlast = 0, ForsaAcero = 0, Accesorios = 0, PlanoTipoForsa = 0,
                CotizacionRapida = 0;
            if (FAlum == true)
                ForsaAlum = 1;
            if (FPlast == true)
                ForsaPlast = 1;
            if (FAcero == true)
                ForsaAcero = 1;
            if (acce == true)
                Accesorios = 1;
            if (planoTF == true)
                PlanoTipoForsa = 1;
            if (cotRap == true)
                CotizacionRapida = 1;
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[18];
            sqls[0] = new SqlParameter("pID_Cliente ", cli);
            sqls[1] = new SqlParameter("pID_Contacto", cont);
            sqls[2] = new SqlParameter("pID_Obra", obr);
            sqls[3] = new SqlParameter("pUsuario", nom);
            sqls[4] = new SqlParameter("pFAlum", ForsaAlum);
            sqls[5] = new SqlParameter("pFPlast", ForsaPlast);
            sqls[6] = new SqlParameter("pFAcero", ForsaAcero);
            sqls[7] = new SqlParameter("pTipoCotizacion", tipcot);
            sqls[8] = new SqlParameter("pAccesorios", Accesorios);
            sqls[9] = new SqlParameter("pNumeroEquipos", numequip);
            sqls[10] = new SqlParameter("pPlanoTipoForsa", PlanoTipoForsa);
            sqls[11] = new SqlParameter("pCotizacionRapida", CotizacionRapida);
            sqls[12] = new SqlParameter("pAlcance", alcance);
            sqls[13] = new SqlParameter("pDescripcion", descrip);
            sqls[14] = new SqlParameter("pID_Moneda", moneda);
            sqls[15] = new SqlParameter("pClase", clase);
            sqls[16] = new SqlParameter("pFupAnterior", fupAnterior);
            sqls[17] = new SqlParameter("pTipoVentaProyecto", tipoVentaProyecto);


            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_INS_entrada_cotizacionNew", con))
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

        //ACTUALIZACIÓN DE ENTRADA DE COTIZACION
        public int ActFUP(int numfup, string ver, string nom, bool acce, int numequip, bool planoTF, bool cotRap,
            string alcance, string descrip, bool VistoBueno, int modulaciones, int clase,int fupAnterior, int tipoVentaProyecto, int rol)
        {
            string usuVistoBueno = "";
            int Accesorios = 0, PlanoTipoForsa = 0, CotizacionRapida = 0, VB = 0;            
            if (acce == true)
                Accesorios = 1;
            if (planoTF == true)
                PlanoTipoForsa = 1;
            if (cotRap == true)
                CotizacionRapida = 1;
            if (VistoBueno == true) { VB = 1; usuVistoBueno = nom; }

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[16];
            sqls[0] = new SqlParameter("pFupID", numfup);
            sqls[1] = new SqlParameter("pVersion", ver);
            sqls[2] = new SqlParameter("pUsuario", nom);
            sqls[3] = new SqlParameter("pAccesorios", Accesorios);
            sqls[4] = new SqlParameter("pNumeroEquipos", numequip);
            sqls[5] = new SqlParameter("pPlanoTipoForsa", PlanoTipoForsa);
            sqls[6] = new SqlParameter("pCotizacionRapida", CotizacionRapida);
            sqls[7] = new SqlParameter("pAlcance", alcance);
            sqls[8] = new SqlParameter("pDescripcion", descrip);
            sqls[9] = new SqlParameter("pVistoBueno", VB);
            sqls[10] = new SqlParameter("pModulaciones", modulaciones);
            sqls[11] = new SqlParameter("pClase", clase);
            sqls[12] = new SqlParameter("pFupAnterior", fupAnterior);
            sqls[13] = new SqlParameter("pTipoVentaProyecto", tipoVentaProyecto);
            sqls[14] = new SqlParameter("rol", rol);
            sqls[15] = new SqlParameter("pUsuVistobueno", usuVistoBueno);


            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_entrada_cotizacionNew", con))
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

        //INGRESO DE RECOTIZACION
        public int ActREC(int numfup, string ver, string nom, bool alum, bool plast, bool b_acero,
            bool acce, int numequip, bool planoTF, bool cotRap, string alcance, string descrip,
            int tiporec, int clase,int fupAnterior,int tipoVentaProyecto)
        {
            int Accesorios = 0, PlanoTipoForsa = 0, CotizacionRapida = 0, Aluminio = 0, 
                Plastico = 0, Acero = 0;
            if (alum == true)
                Aluminio = 1;
            if (plast == true)
                Plastico = 1;
            if (b_acero == true)
                Acero = 1;
            if (acce == true)
                Accesorios = 1;
            if (planoTF == true)
                PlanoTipoForsa = 1;
            if (cotRap == true)
                CotizacionRapida = 1;
            
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[16];
            sqls[0] = new SqlParameter("pFupID", numfup);
            sqls[1] = new SqlParameter("pVersion", ver);
            sqls[2] = new SqlParameter("pUsuario", nom);
            sqls[3] = new SqlParameter("pFAlum", Aluminio);
            sqls[4] = new SqlParameter("pFPlast", Plastico);
            sqls[5] = new SqlParameter("pFAcero", Acero);
            sqls[6] = new SqlParameter("pAccesorios", Accesorios);
            sqls[7] = new SqlParameter("pNumeroEquipos", numequip);
            sqls[8] = new SqlParameter("pPlanoTipoForsa", PlanoTipoForsa);
            sqls[9] = new SqlParameter("pCotizacionRapida", CotizacionRapida);
            sqls[10] = new SqlParameter("pAlcance", alcance);
            sqls[11] = new SqlParameter("pDescripcion", descrip);
            sqls[12] = new SqlParameter("pTipoRecotizacion", tiporec);
            sqls[13] = new SqlParameter("pClase", clase);
            sqls[14] = new SqlParameter("pFupAnterior", fupAnterior);
            sqls[15] = new SqlParameter("pTipoVentaProyecto", tipoVentaProyecto);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_recotizacionNew", con))
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

        //CONSULTA VERSION DEL FUP
        public SqlDataReader PoblarVersion(int FUP)
        {
            string sql;

            sql = "SELECT eect_id, eect_vercot_id FROM fup_enc_entrada_cotizacion " +
                "WHERE eect_fup_id = " + FUP + " ORDER BY eect_id DESC";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA RESPONSABLES
        public SqlDataReader PoblarResponsable()
        {
            string sql;

            sql = "SELECT usuario.usu_emp_usu_num_id, empleado.emp_nombre + ' ' + empleado.emp_apellidos AS Nombre " + 
            "FROM usuario INNER JOIN empleado ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id " +
            "WHERE (usuario.usu_rap_id = 24) AND (usuario.usu_activo = 1) OR (usuario.usu_rap_id = 26) AND (usuario.usu_activo = 1) " + 
            "ORDER BY Nombre";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA RESPONSABLES
        public SqlDataReader PoblarResponsableControlCambios(int idarea)
        {
            string sql;

            sql = "SELECT     rolapp.rap_area_id, empleado.emp_nombre_mayusculas + ' ' + empleado.emp_apellidos_mayusculas AS Expr1 " +
                    "FROM         rolapp INNER JOIN " +
                     " usuario ON rolapp.rap_id = usuario.usu_rap_id INNER JOIN " +
                     "empleado ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id " +
                    " WHERE     (rolapp.rap_area_id = " + idarea + ") AND (usuario.usu_activo = 1) AND " +
                    " (empleado.emp_activo = 1) AND (empleado.emp_estado_laboral = 'ACTIVO') " +
                "ORDER BY Expr1";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA AREA RESPONSABLES
        public SqlDataReader PoblarAreaResponsableControlCambios()
        {
            string sql;

            sql = "SELECT are_id, are_nombre FROM area WHERE are_ControlCambio = 1 ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA EVENTOS
        public SqlDataReader PoblarEventos()
        {
            string sql;

            sql = "SELECT tes_id, tes_descripcion FROM fup_tipo_evento_segpf";

            return BdDatos.ConsultarConDataReader(sql);
        }


        //CONSULTA TIPOS DE COTIZACION
        public SqlDataReader PoblarClaseCotizacion()
        {
            string sql;

            sql = "SELECT     clase_cot_id, clase_cot_sigla, clase_cot_descripcion "+
                    " FROM         fup_clase_cotizacion_ent WHERE     (clase_cot_activo = 1)";

            return BdDatos.ConsultarConDataReader(sql);
        }
        
        //CONSULTA TIPOS DE COTIZACION
        public SqlDataReader ConsultarNombreCarta(int fup, string version)
        {
            string sql = " SELECT        TOP (1) plano_nombre_real " +
            " FROM            Plano " +
            " WHERE        (id_fup_plano = " + fup + ") AND (plano_version = '" + version+ "') AND (plano_tipo_anexo_id = 6) " +
            " ORDER BY fecha_sube DESC ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA TIPOS DE COTIZACION
        public SqlDataReader consultarCartaEnviada(int fup, string version)
        {
            string sql = " SELECT        TOP (1) plano_nombre_real " +
            " FROM            Plano " +
            " WHERE        (id_fup_plano = " + fup + ") AND (plano_version = '" + version + "') AND (plano_tipo_anexo_id = 6) " +
            " ORDER BY fecha_sube DESC ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA TIPO DE UNION
        public SqlDataReader PoblarTipoUnion()
        {
            string sql;

            sql = "SELECT tu_id, tu_desc_esp FROM fup_tipo_union ORDER BY tu_desc_esp DESC";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA FORMA DE CONSTRUCCIÓN
        public SqlDataReader PoblarFormaConstruccion()
        {
            string sql;

            sql = "SELECT fc_id, fc_descripcion FROM fup_forma_construccion ORDER BY fc_id ASC";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA TIPO PROYECTO
        public SqlDataReader PoblarTipoProyecto()
        {
            string sql;

            sql = "SELECT ftpr_id ,ftpr_descripcion FROM fup_tipo_proyectos " +
            "ORDER BY ftpr_id";
            
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA TIPO VENTA PROYECTO
        public SqlDataReader PoblarTipoVentaProyecto(int proyecto)
        {
            string sql;

            sql = "SELECT  fup_tipo_venta_proyecto.fup_tipo_venta_proy_id, fup_tipo_venta_proyecto.descripcion "+
                    " FROM fup_tipo_venta_proyecto INNER JOIN " +
                   "      fup_tipo_proyectos ON fup_tipo_venta_proyecto.fup_tipo_proyecto_id = fup_tipo_proyectos.ftpr_id "+
                    "WHERE(fup_tipo_proyectos.ftpr_id = "+ proyecto +") AND(fup_tipo_venta_proyecto.activo = 1) "+
                    "ORDER BY fup_tipo_venta_proyecto.descripcion";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA TIPO PROYECTO
        public SqlDataReader PoblarTipoCotizacion()
        {
            string sql;

            sql = "SELECT        ftco_id, ftco_nombre  FROM      fup_tipo_cotizacion  WHERE        (ftco_estado = 1) and   (ftco_id <> 0)";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA TIPO PROYECTO
        public SqlDataReader PoblarPlantasPv(int fup)
        {
            string sql;

            sql = "SELECT        pedido_venta.pv_id, planta_forsa.planta_descripcion "+
                    "FROM            pedido_venta AS pedido_venta INNER JOIN "+
                    " planta_forsa AS planta_forsa ON pedido_venta.planta_id = planta_forsa.planta_id "+
                    " WHERE        (pedido_venta.pv_fup_id = " + fup + ") " +
                    " ORDER BY planta_forsa.planta_id";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA TIPO ANEXO
        public SqlDataReader PoblarTipoAnexo(int comercial, int cotizador,int spt = 0)
        {
            string sql;

            sql = "SELECT        tan_id, tan_desc_esp, tan_cotizacion, tan_comercial " +
                    " FROM            fup_tipo_anexo " +
                    " WHERE        (tan_comercial = " + comercial + ") AND (tan_cotizacion = " + cotizador + ") "; 

            //sql = "SELECT tan_id ,tan_desc_esp ,tan_desc_eng ,tan_desc_por " +
            //      "FROM fup_tipo_anexo " +
            //      "WHERE tan_id <> CASE WHEN " + spt + " = 1 THEN -1 ELSE 5 END" +
            //      "  AND tan_id <> CASE WHEN " + spt + " = 1 THEN -1 ELSE 7 END";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA RECOTIZACIÓN
        public SqlDataReader PoblarRecotizacion()
        {
            string sql;

            sql = "SELECT trc_id, trc_descripcion FROM fup_tipo_recotizacion ORDER BY trc_id ASC";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA TEMA RECHAZO
        public SqlDataReader PoblarTemaRechazo()
        {
            string sql;

            sql = "SELECT tre_id, tre_descripcion FROM fup_tipo_rechazo WHERE  (tre_tipo = 1) ORDER BY tre_id ASC";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA TEMA RECHAZO de comercial
        public SqlDataReader PoblarTemaRechazoComerc()
        {
            string sql;

            sql = "SELECT tre_id, tre_descripcion FROM fup_tipo_rechazo WHERE  (tre_tipo = 2) ORDER BY tre_id ASC";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA los datos de la parte
        public SqlDataReader PoblarDatosParte(int sf_id)
        {
            string sql;

            sql = " SELECT        CAST(solicitud_facturacion.sf_m2 AS varchar) + ' M2' AS n, '$' + ' ' + FORMAT(solicitud_facturacion.Sf_vlr_comercial, 'N', 'en-us') AS Expr1, Tipos_Sol.T_Sol_Tipo " +
                    " FROM            Tipos_Sol INNER JOIN  " +
                        " fup_tipo_cotizacion INNER JOIN  " +
                        " tipo_orden_proyecto ON fup_tipo_cotizacion.ftco_grupo_orden_id = tipo_orden_proyecto.tipo_cotizacion_grupo_id INNER JOIN  " +
                        " fup_tipo_venta_proyecto ON tipo_orden_proyecto.tipo_venta_proy_id = fup_tipo_venta_proyecto.fup_tipo_venta_proy_id ON Tipos_Sol.T_Sol_Id = tipo_orden_proyecto.tipo_sol_orden_id INNER JOIN  " +
                        " fup_enc_entrada_cotizacion ON fup_tipo_cotizacion.ftco_id = fup_enc_entrada_cotizacion.eect_tipo_cotizacion AND   " +
                        " fup_tipo_venta_proyecto.fup_tipo_venta_proy_id = fup_enc_entrada_cotizacion.eect_tipo_venta_proy_id INNER JOIN  " +
                        " solicitud_facturacion ON fup_enc_entrada_cotizacion.eect_fup_id = solicitud_facturacion.Sf_fup_id AND fup_enc_entrada_cotizacion.eect_vercot_id = solicitud_facturacion.Sf_version  " +
                    " WHERE        (solicitud_facturacion.Sf_id = " + sf_id + ")";

            //sql = "SELECT  CAST(sf_m2 AS varchar) + ' M2' AS n, '$' + ' ' + FORMAT(Sf_vlr_comercial, 'N', 'en-us') AS Expr1 FROM solicitud_facturacion  WHERE (Sf_id = "+sf_id+")";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA los datos de la parte
        public SqlDataReader ConsultarFupSalida(int fup, string version )
        {
            string sql;

            sql = "SELECT        fup_enc_entrada_cotizacion.eect_clase_cot, ISNULL(fup_salida_cotizacion.sct_m2, 0) AS Expr1 ,  fup_clase_cotizacion_ent.clase_cot_sigla + ' : ' + fup_clase_cotizacion_ent.clase_cot_descripcion AS claseDesc" +
                   " FROM  fup_enc_entrada_cotizacion INNER JOIN  fup_clase_cotizacion_ent ON fup_enc_entrada_cotizacion.eect_clase_cot = fup_clase_cotizacion_ent.clase_cot_id LEFT OUTER JOIN "+
                   " fup_salida_cotizacion ON fup_enc_entrada_cotizacion.eect_id = fup_salida_cotizacion.sct_enc_entrada_cot_id " +
                   " WHERE  (fup_enc_entrada_cotizacion.eect_fup_id ="+ fup+") AND (fup_enc_entrada_cotizacion.eect_vercot_id ='"+ version+"') ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA PARTES DE LA OF
        public SqlDataReader PoblarPartesOF(int FUP, string Vers, int pv_id)
        {
            string sql;

            sql = "SELECT Sf_id,ISNULL(sf_parte,1)   FROM solicitud_facturacion sf " +
                  "WHERE Sf_confirma= 1 AND sf_ingresada =1 and sf_fup_id = " + FUP.ToString() + " AND sf_version = '" + Vers + "' AND pv_id =" + pv_id + " " +
                  "  AND Sf_parte NOT IN (SELECT O.ord_parte AS Expr1 FROM  Orden AS O INNER JOIN  Orden_Seg ON O.ordenseg_id = Orden_Seg.Id_Seg_Of INNER JOIN solicitud_facturacion  " +
                  " ON Orden_Seg.sf_id = solicitud_facturacion.Sf_id "+
                  " WHERE solicitud_facturacion.sf_tipo_id = 1 and  O.Yale_Cotiza = sf_fup_id and o.ord_version = sf_version AND solicitud_facturacion.pv_id =" + pv_id + ") " +
                  //SELECT 1 AS Expr1 FROM  Orden AS O INNER JOIN  Orden_Seg ON O.ordenseg_id = Orden_Seg.Id_Seg_Of INNER JOIN solicitud_facturacion ON Orden_Seg.sf_id = solicitud_facturacion.Sf_id
                  " ORDER BY sf_parte ";

            return BdDatos.ConsultarConDataReader(sql);
        }
        //CONSULTAR FUP
        public SqlDataReader ConsultarFUP(int FUP)
        {
            string sql;
            sql = "SELECT fup_id, pai_id, fup_ch_accesorios " +
                  "FROM formato_unico INNER JOIN cliente ON formato_unico.fup_cli_id = cliente.cli_id " +
                  "INNER JOIN pais ON cliente.cli_pai_id = pais.pai_id " +
                  "WHERE fup_id = " + FUP + " ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //VALIDAR PAIS REPRESENTANTE
        public SqlDataReader ValidarPaisRepresentante(int pais, int rep)
        {
            string sql;
            sql = "SELECT rc.rc_id FROM representantes_comerciales rc INNER JOIN pais_representante pr " +
                "ON pr.pr_id_representante = rc.rc_id " +
                  "WHERE pr.pr_id_pais = " + pais + " AND pr.pr_id_representante = " + rep + " ";
            return BdDatos.ConsultarConDataReader(sql);
        }
                
        //CREACION DEL DETALLE DE LA ENTRADA DE COTIZACION
        public int IngDET(int fup, string ver, int tipoproy, bool Espec_Muros, bool Espec_Losa,
            bool Espec_UML, string AlturaLibre, string EspesorMuro, string EspesorLosa, int TipoUnion,
            string EnrasePuertas, string EnraseVentana, bool Plano_Planta, bool Plano_Cortes_Fachada,
            bool Plano_Azotea, bool Plano_Urbanistico, bool Plano_Estructural, bool PF_Muro, 
            bool PF_Losa, bool PF_Azotea, bool PF_Ascensor, bool PF_Escalera, int FormaConst,
            bool Dilata_Muro, string EspesorJuntas, bool C_DesnivelAsc, bool C_DesnivelDesc, 
            bool Culata_Perim, bool Culata_Interna, bool D_Antepecho, bool D_Columna, bool Escalera_Monolotica,
            bool Escalera_Posterior, bool Base_Tinaco, bool Losa_Inclinada, bool D_Domo, bool Muros_Patio,
            bool Negativos_Acero, bool D_Pretiles, bool D_Gargolas, bool Muro_Formaleta, bool Neg_Carriola,
            bool Ventana_Especial, bool Cuarto_Maquinas, bool Vigas_Descolgadas, bool D_Torreon, 
            bool D_Rebordes, bool D_Reservatorios, bool Dilatacion_Fachada, bool Junta_Int_Antepecho,
            bool Junta_Ext_Antepecho, bool D_Canes, bool D_Porticos, bool D_Otros, int rol)
        {
            int EspecMuros = 0, EspecLosa = 0, EspecUML = 0, PlanoPlanta = 0, PlanoCortesFachada = 0,
                PlanoAzotea = 0, PlanoUrbanistico = 0, PlanoEstructural = 0, PFMuro = 0, PFLosa = 0,
                PFAzotea = 0, PFAscensor = 0, PFEscalera = 0, DilataMuro = 0, CDesnivelAsc = 0, CDesnivelDesc = 0, CulataPerim = 0,
                CulataInterna = 0, DAntepecho = 0, DColumna = 0, EscaleraMonolotica = 0, EscaleraPosterior = 0,
                BaseTinaco = 0, LosaInclinada = 0, DDomo = 0, MurosPatio = 0, NegativosAcero = 0, DPretiles = 0,
                DGargolas = 0, MuroFormaleta = 0, NegCarriola = 0, VentanaEspecial = 0, CuartoMaquinas = 0,
                VigasDescolgadas = 0, DTorreon = 0, DRebordes = 0, DReservatorios = 0, DilatacionFachada = 0,
                JuntaIntAntepecho = 0, JuntaExtAntepecho = 0, DCanes = 0, DPorticos = 0, DOtros = 0;

            if (Espec_Muros == true)
                EspecMuros = 1;
            if (Espec_Losa == true)
                EspecLosa = 1;
            if (Espec_UML == true)
                EspecUML = 1;
            if (Plano_Planta == true)
                PlanoPlanta = 1;
            if (Plano_Cortes_Fachada == true)
                PlanoCortesFachada = 1;
            if (Plano_Azotea == true)
                PlanoAzotea = 1;
            if (Plano_Urbanistico == true)
                PlanoUrbanistico = 1;
            if (Plano_Estructural == true)
                PlanoEstructural = 1;
            if (PF_Muro == true)
                PFMuro = 1;
            if (PF_Losa == true)
                PFLosa = 1;
            if (PF_Azotea == true)
                PFAzotea = 1;
            if (PF_Ascensor == true)
                PFAscensor = 1;
            if (PF_Escalera == true)
                PFEscalera = 1;
            if (Dilata_Muro == true)
                DilataMuro = 1;
            if (C_DesnivelAsc == true)
                CDesnivelAsc = 1;
            if (C_DesnivelDesc == true)
                CDesnivelDesc = 1;
            if (Culata_Perim == true)
                CulataPerim = 1;
            if (Culata_Interna == true)
                CulataInterna = 1;
            if (D_Antepecho == true)
                DAntepecho = 1;
            if (D_Columna == true)
                DColumna = 1;
            if (Escalera_Monolotica == true)
                EscaleraMonolotica = 1;
            if (Escalera_Posterior == true)
                EscaleraPosterior = 1;
            if (Base_Tinaco == true)
                BaseTinaco = 1;
            if (Losa_Inclinada == true)
                LosaInclinada = 1;
            if (D_Domo == true)
                DDomo = 1;
            if (Muros_Patio == true)
                MurosPatio = 1;
            if (Negativos_Acero == true)
                NegativosAcero = 1;
            if (D_Pretiles == true)
                DPretiles = 1;
            if (D_Gargolas == true)
                DGargolas = 1;
            if (Muro_Formaleta == true)
                MuroFormaleta = 1;
            if (Neg_Carriola == true)
                NegCarriola = 1;
            if (Ventana_Especial == true)
                VentanaEspecial = 1;
            if (Cuarto_Maquinas == true)
                CuartoMaquinas = 1;
            if (Vigas_Descolgadas == true)
                VigasDescolgadas = 1;
            if (D_Torreon == true)
                DTorreon = 1;
            if (D_Rebordes == true)
                DRebordes = 1;
            if (D_Reservatorios == true)
                DReservatorios = 1;
            if (Dilatacion_Fachada == true)
                DilatacionFachada = 1;
            if (Junta_Int_Antepecho == true)
                JuntaIntAntepecho = 1;
            if (Junta_Ext_Antepecho == true)
                JuntaExtAntepecho = 1;
            if (D_Canes == true)
                DCanes = 1;
            if (D_Porticos == true)
                DPorticos = 1;
            if (D_Otros == true)
                DOtros = 1;

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[55];
            sqls[0] = new SqlParameter("pFupID ", fup);
            sqls[1] = new SqlParameter("pVersion", ver);
            sqls[2] = new SqlParameter("tipo_proyecto", tipoproy);
            sqls[3] = new SqlParameter("espec_Muros", EspecMuros);
            sqls[4] = new SqlParameter("espec_Losa", EspecLosa);
            sqls[5] = new SqlParameter("espec_Union_ml", EspecUML);
            sqls[6] = new SqlParameter("esptec_Altura_Libre", AlturaLibre);
            sqls[7] = new SqlParameter("esptec_Espesor_muro", EspesorMuro);
            sqls[8] = new SqlParameter("esptec_Espesor_losa", EspesorLosa);
            sqls[9] = new SqlParameter("esptec_Tipo_union", TipoUnion);
            sqls[10] = new SqlParameter("esptec_Enrase_puerta", EnrasePuertas);
            sqls[11] = new SqlParameter("esptec_Enrase_ventana", EnraseVentana);
            sqls[12] = new SqlParameter("plano_Planta", PlanoPlanta);
            sqls[13] = new SqlParameter("plano_Cortes_Fachada", PlanoCortesFachada);
            sqls[14] = new SqlParameter("plano_Azotea", PlanoAzotea);
            sqls[15] = new SqlParameter("plano_Urbanistico", PlanoUrbanistico);
            sqls[16] = new SqlParameter("plano_estructural", PlanoEstructural);
            sqls[17] = new SqlParameter("ptofijo_Muro", PFMuro);
            sqls[18] = new SqlParameter("ptofijo_Losa", PFLosa);
            sqls[19] = new SqlParameter("ptofijo_Azotea", PFAzotea);
            sqls[20] = new SqlParameter("ptofijo_foso_ascensor", PFAscensor);
            sqls[21] = new SqlParameter("ptofijo_foso_escalera", PFEscalera);
            sqls[22] = new SqlParameter("dconst_forma", FormaConst);
            sqls[23] = new SqlParameter("dconst_dilata_muro", DilataMuro);
            sqls[24] = new SqlParameter("dconst_espesor_juntas", EspesorJuntas);
            sqls[25] = new SqlParameter("dconst_desnivel_asc", CDesnivelAsc);
            sqls[26] = new SqlParameter("dconst_desnivel_des", CDesnivelDesc);
            sqls[27] = new SqlParameter("detal_culata_perim", CulataPerim);
            sqls[28] = new SqlParameter("detal_culata_interna", CulataInterna);
            sqls[29] = new SqlParameter("detal_antepecho", DAntepecho);
            sqls[30] = new SqlParameter("detal_columna", DColumna);
            sqls[31] = new SqlParameter("detal_escalera_monolitica", EscaleraMonolotica);
            sqls[32] = new SqlParameter("detal_escalera_posterior", EscaleraPosterior);
            sqls[33] = new SqlParameter("detal_base_tinaco", BaseTinaco);
            sqls[34] = new SqlParameter("detal_losa_inclinada", LosaInclinada);
            sqls[35] = new SqlParameter("detal_domo", DDomo);
            sqls[36] = new SqlParameter("detal_muros_patio", MurosPatio);
            sqls[37] = new SqlParameter("detal_Neg_acero", NegativosAcero);
            sqls[38] = new SqlParameter("detal_pretiles", DPretiles);
            sqls[39] = new SqlParameter("detal_gargolas", DGargolas);
            sqls[40] = new SqlParameter("detal_muro_formaleta", MuroFormaleta);
            sqls[41] = new SqlParameter("detal_neg_carriola", NegCarriola);
            sqls[42] = new SqlParameter("detal_ventana_especial", VentanaEspecial);
            sqls[43] = new SqlParameter("detal_cuarto_maquinas", CuartoMaquinas);
            sqls[44] = new SqlParameter("detal_vigas_descolgadas", VigasDescolgadas);
            sqls[45] = new SqlParameter("detal_torreon", DTorreon);
            sqls[46] = new SqlParameter("detal_rebordes", DRebordes);
            sqls[47] = new SqlParameter("detal_reservatorios", DReservatorios);
            sqls[48] = new SqlParameter("detal_dilatacion_fachada", DilatacionFachada);
            sqls[49] = new SqlParameter("detal_junta_int_antep", JuntaIntAntepecho);
            sqls[50] = new SqlParameter("detal_junta_ext_antep", JuntaExtAntepecho);
            sqls[51] = new SqlParameter("detal_canes", DCanes);
            sqls[52] = new SqlParameter("detal_porticos", DPorticos);
            sqls[53] = new SqlParameter("detal_otros", DOtros);
            sqls[54] = new SqlParameter("rol", rol);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_det_entrada_cot", con))
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

        //CREACION DE LA SALIDA DE COTIZACIÓN
        public int IngSalida(int fup, string ver, string nom, string M2_Alum, 
            string Alum_Valor, string Alum_Valor_Acc, string M2_Plast,
            string Plast_Valor, string Plast_Valor_Acc, string M2_Acero,
            string Acero_Valor, string Acero_Valor_Accesorio,string conten20,string conten40)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;
            if (M2_Alum.Length == 0) M2_Alum = "0";
            if (Alum_Valor.Length == 0) Alum_Valor = "0";
            if (Alum_Valor_Acc.Length == 0) Alum_Valor_Acc = "0";
            if (M2_Plast.Length == 0) M2_Plast = "0";
            if (Plast_Valor.Length == 0) Plast_Valor = "0";
            if (Plast_Valor_Acc.Length == 0) Plast_Valor_Acc = "0";
            if (M2_Acero.Length == 0) M2_Acero = "0";
            if (Acero_Valor.Length == 0) Acero_Valor = "0";
            if (Acero_Valor_Accesorio.Length == 0) Acero_Valor_Accesorio = "0";
            if (conten20.Length == 0) conten20 = "0";
            if (conten40.Length == 0) conten40 = "0";

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[14];
            sqls[0] = new SqlParameter("pFupID ", fup);
            sqls[1] = new SqlParameter("pVersion", ver);
            sqls[2] = new SqlParameter("pUsuario", nom);
            sqls[3] = new SqlParameter("pAlum_m2", M2_Alum.Replace(",", ""));
            sqls[4] = new SqlParameter("pAlum_valor", Alum_Valor.Replace(",", ""));
            sqls[5] = new SqlParameter("pAlum_vr_acc", Alum_Valor_Acc.Replace(",", ""));
            sqls[6] = new SqlParameter("pPlast_m2", M2_Plast.Replace(",", ""));
            sqls[7] = new SqlParameter("pPlast_valor", Plast_Valor.Replace(",", ""));
            sqls[8] = new SqlParameter("pPlast_vr_acc", Plast_Valor_Acc.Replace(",", ""));
            sqls[9] = new SqlParameter("pAcero_m2", M2_Acero.Replace(",", ""));
            sqls[10] = new SqlParameter("pAcero_valor", Acero_Valor.Replace(",", ""));
            sqls[11] = new SqlParameter("pAcero_vr_acc", Acero_Valor_Accesorio.Replace(",", ""));
            sqls[12] = new SqlParameter("@pCont20", conten20.Replace(",", ""));
            sqls[13] = new SqlParameter("@pCont40", conten40.Replace(",", ""));


            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {//cambiaraproduccion
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_salida_cotizacion", con))
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

        ////INSERTAR RESPUESTA RECHAZO
        //public int insertarRespuestaRechazo(int fup, string version)
        //{
        //    string sql;
        //    sql = "INSERT INTO LOG_pv (logpv_fup_id, logpv_tipo, usu_crea) " +
        //        "VALUES(" + fup + ",'Desconfirmación', '" + usuario + "')";

        //    return BdDatos.Actualizar(sql);
        //}
        

        //INGRESO DE RECHAZO
        public int ActRECHA(int numfup, string ver, string nom, int tiporec, 
            string observa)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[5];
            sqls[0] = new SqlParameter("pFupID", numfup);
            sqls[1] = new SqlParameter("pVersion", ver);
            sqls[2] = new SqlParameter("pUsuario", nom);
            sqls[3] = new SqlParameter("ptipo_rechazo_id", tiporec);
            sqls[4] = new SqlParameter("pobservacion", observa);
            
            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_rechazo_cot", con))
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

        //INGRESO DE RECHAZO DE COMERCIAL
        public int ActRECHACOM(int id,  string nom,  
            string observa)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[3];
            sqls[0] = new SqlParameter("pID", id);
            sqls[1] = new SqlParameter("pUsuario", nom);
            sqls[2] = new SqlParameter("pobservacion", observa);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_rechazo_Comercial", con))
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

        //INGRESO DE CIERRE COMERCIAL
        public int ActCIERRE(int numfup, string ver, string nom, string fecfir, string fecplano,
            int plazo, string fecContr, int tiempoase, string fecanticip, int piezas, string pcom,
            string alcance, int cantof)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            if (pcom.Length == 0) pcom = "0";

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[13];
            sqls[0]  = new SqlParameter("pFupID", numfup);
            sqls[1]  = new SqlParameter("pVersion", ver);
            sqls[2]  = new SqlParameter("pUsuario", nom);
            sqls[3]  = new SqlParameter("pfecha_firma_contrato", fecfir);
            sqls[4]  = new SqlParameter("pfecha_planos_aprobados", fecplano);
            sqls[5]  = new SqlParameter("pplazo", plazo);
            sqls[6]  = new SqlParameter("pfecha_contractual", fecContr);
            sqls[7]  = new SqlParameter("ptiempo_asetec", tiempoase);
            sqls[8]  = new SqlParameter("pfecha_anticipado", fecanticip);
            sqls[9]  = new SqlParameter("ppiezas", piezas);
            sqls[10] = new SqlParameter("pporc_com", pcom);
            sqls[11] = new SqlParameter("pAlcance_Final", alcance);
            sqls[12] = new SqlParameter("pCantidadOF", cantof);
          
            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_Cierre_Comercial", con))
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

        //INGRESO DE SOLICITUD DE FACTURACIÓN
        public int IngSF(int numfup, string ver, string nom, string param)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[4];
            sqls[0] = new SqlParameter("pFupID", numfup);
            sqls[1] = new SqlParameter("pVersion", ver);
            sqls[2] = new SqlParameter("pUsuario", nom);
            sqls[3] = new SqlParameter("pTipo", param);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_Generar_SF", con))
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
                    con.Close();
                }
            }
            return Id;
        }

        //INGRESO Planos Forsa
        public int ActPLANOFORSA(int numfup, string ver, string nom, long resp, long even,
            string observa, bool env, string fecha)
        {
            int enviado = 0;
            if (env == true)
            {
                enviado = 1;
            }
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[8];
            sqls[0] = new SqlParameter("pFupID", numfup);
            sqls[1] = new SqlParameter("pVersion", ver);
            sqls[2] = new SqlParameter("pUsuario", nom);
            sqls[3] = new SqlParameter("pResponsable_id", resp);
            sqls[4] = new SqlParameter("pEvento_id", even);
            sqls[5] = new SqlParameter("pObservacion", observa);
            sqls[6] = new SqlParameter("pEnviado", enviado);
            sqls[7] = new SqlParameter("pFecCierre", fecha);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_seg_plano_tf", con))
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

        //INGRESO RECOTIZACION
        public int ActRECOTIZA(int numfup, string ver, string nom, int tipo,
            string observa, int clase)
        {            
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[6];
            sqls[0] = new SqlParameter("pFupID", numfup);
            sqls[1] = new SqlParameter("pVersion", ver);
            sqls[2] = new SqlParameter("pUsuario", nom);
            sqls[3] = new SqlParameter("ptipo_recotiza_id", tipo);
            sqls[4] = new SqlParameter("pobservacion", observa);
            sqls[5] = new SqlParameter("pClase", clase);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_recotiza_ent", con))
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

        //INGRESO CONTROL DE CAMBIOS
        public int ActCAMBIOS(int numfup, string ver, string nom, long tipo,
            string observa, int area)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[6];
            sqls[0] = new SqlParameter("pFupID", numfup);
            sqls[1] = new SqlParameter("pVersion", ver);
            sqls[2] = new SqlParameter("pUsuario", nom);
            sqls[3] = new SqlParameter("ptipo_registro_id", tipo);
            sqls[4] = new SqlParameter("pobservacion", observa);
            sqls[5] = new SqlParameter("pArea_id", area);            

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_registro_ing", con))
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

        //INGRESO Documentos Planos
        public int IngDOCPLAN(int numfup, string ver, string nom, string nomarch,
            string rutarc, int tipoanex, int tipoproy, string OFRef)
        {
            
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[8];
            sqls[0] = new SqlParameter("pFupID", numfup);
            sqls[1] = new SqlParameter("pVersion", ver);
            sqls[2] = new SqlParameter("pUsuario", nom);
            sqls[3] = new SqlParameter("pNombreArch", nomarch);
            sqls[4] = new SqlParameter("pRutaArch", rutarc);
            sqls[5] = new SqlParameter("pTipo_anexo_id", tipoanex);
            sqls[6] = new SqlParameter("pTipo_proyecto_id", tipoproy);
            sqls[7] = new SqlParameter("pOfref", OFRef);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_archivos_plano", con))
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

        //CREACION DE OFS
        public int ActOFS(int numfup, string ver, string nom, string proden, int parte, int sf_id)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[6];
            sqls[0] = new SqlParameter("pFupID", numfup);
            sqls[1] = new SqlParameter("pVersion", ver);
            sqls[2] = new SqlParameter("pUsuario", nom);
            sqls[3] = new SqlParameter("pFabricadoEn", proden);
            sqls[4] = new SqlParameter("pParte", parte);
            sqls[5] = new SqlParameter("sf_id", sf_id);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_INS_Orden_Fab", con))
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

        public DataSet ConsultarControlCambioGrilla(int fup, string ver, int rol)
        {
            string sql;
            sql = "SELECT CONVERT(varchar(10),rin_fecha,120) FECHA, ct.Nombre RESPONSABLE, ISNULL(a.are_nombre,'SIN AREA') [AREA RESPONSABLE],rin_observacion OBSERVACION  " +
                "FROM fup_registro_ingenieria ri INNER JOIN fup_enc_entrada_cotizacion ec ON " +
                "ri.rin_enc_entrada_cot_id = ec.eect_id " +
                "INNER JOIN (SELECT u.usu_emp_usu_num_id, e.emp_nombre + ' ' + e.emp_apellidos AS Nombre " +
                "FROM usuario AS u INNER JOIN empleado AS e ON u.usu_emp_usu_num_id = e.emp_usu_num_id " +
                "WHERE (u.usu_rap_id IN (" + rol + "))) AS ct ON ct.usu_emp_usu_num_id = ri.rin_tipo_registro_id " +
                "LEFT OUTER JOIN area a ON a.are_id = ri.rin_area_resp " +
                "WHERE eect_fup_id = " + fup + " AND eect_vercot_id = '" + ver + "'";
            DataSet dsFUP = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return dsFUP;
        }

        //BORRAR PLANO
        public SqlDataReader BorrarPlano(int plano, string usuario)
        {
            string sql, fecha;
            fecha= System.DateTime.Now.ToString("dd/MM/yyyy");

            sql = "UPDATE PLANO SET anulado = 1,usu_anula = '" + usuario + "', fecha_anulado = '"+fecha +"' WHERE id_plano = " + plano + "";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //ACTUALIZAR VALIDACIÓN
        public SqlDataReader ValidarRechazo(int rechazo)
        {
            string sql;

            sql = "UPDATE fup_rechazo_entrada SET rct_validado = 1 WHERE rct_id = " + rechazo + "";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //ACTUALIZAR ESTADO DE SALIDA DE COTIZACION
        public SqlDataReader actualizarSalidaCotizacion(int fup, string  version, string usuario)
        {
            string sql;

            sql = "UPDATE fup_enc_entrada_cotizacion SET   eect_estado_proc = 5,  eect_fecha_salida_cot =   getDate(),	" +
                 " eect_fecha_salida =  getDate() WHERE  eect_fup_id = " + fup + "  AND eect_vercot_id = '" + version + "' ; " +

                 "update fup_salida_cotizacion set fup_salida_cotizacion.sct_fecha_salida_carta = getDate(), fup_salida_cotizacion.sct_usu_crea_carta = '" + usuario + "' " +
                 " FROM    fup_salida_cotizacion INNER JOIN   fup_enc_entrada_cotizacion ON " +
                 " fup_salida_cotizacion.sct_enc_entrada_cot_id = fup_enc_entrada_cotizacion.eect_id " +
                 " WHERE        (fup_enc_entrada_cotizacion.eect_fup_id = " + fup + " ) AND (fup_enc_entrada_cotizacion.eect_vercot_id = '" + version + "') ";            

            return BdDatos.ConsultarConDataReader(sql);
        }

        //ACTUALIZAR    QUE SE ENVIO EL PLANO
        public SqlDataReader actualizarEnvioPlano(int fup, string  version)
        {
            string sql;

            sql = "update plano set envio = 1, fecha_envio = getDate() "+
                    " FROM            Plano "+
                    " WHERE        (id_fup_plano = " + fup + ") AND (plano_version = '" + version + "')    "+
                    "  AND (plano_tipo_anexo_id = 6 or plano_tipo_anexo_id = 8) ";
                    // " AND (plano_nombre_real = '" + nombreCarta + "') AND (plano_tipo_anexo_id = 6) "; 
             
            return BdDatos.ConsultarConDataReader(sql);
        }

        

        //ELIMINAR RECHAZO
        public SqlDataReader EliminarRechazo(int rechazo)
        {
            string sql;

            sql = "DELETE FROM fup_rechazo_entrada WHERE rct_id = " + rechazo + "";

            return BdDatos.ConsultarConDataReader(sql);
        }

        /// <summary>
        /// Metodo encargado de consultar los datos del FUP en el formato unico
        /// Tambien se encarga de verificar si el representante esta habilitado para el pais del FUP
        /// Autor: Global BI
        /// Fecha: 01-10-2018
        /// </summary>
        /// <param name="idFUP"></param>
        /// <param name="idRepresentante"></param>
        /// <returns></returns>
        public formato_unico obtenerFUPporID(int idFUP, int idRepresentante)
        {
            System.Text.StringBuilder stBuilder = new System.Text.StringBuilder();
            stBuilder.Append("SELECT fup_id, pai_id, fup_ch_accesorios,ISNULL((");
            stBuilder.Append(" SELECT max(rc.rc_id) rc_id FROM representantes_comerciales rc ");
            stBuilder.Append(" INNER JOIN pais_representante pr ON pr.pr_id_representante = rc.rc_id ");
            stBuilder.Append(" WHERE pr.pr_id_pais = pais.pai_id AND pr.pr_id_representante = " + idRepresentante + "),0) representanteId ");
            stBuilder.Append("FROM formato_unico ");
            stBuilder.Append("INNER JOIN cliente ON formato_unico.fup_cli_id = cliente.cli_id ");
            stBuilder.Append("INNER JOIN pais ON cliente.cli_pai_id = pais.pai_id ");
            stBuilder.Append("WHERE fup_id = " + idFUP);

            DataTable dt = BdDatos.CargarTabla(stBuilder.ToString());
            List<formato_unico> lst = dt.AsEnumerable()
                .Select(row => new formato_unico
                {
                    fup_id = (int)row["fup_id"],
                    pai_id = (int)row["pai_id"],
                    fup_ch_accesorios = (bool)row["fup_ch_accesorios"],
                    representanteId = (int)row["representanteId"]
                }).ToList();
            dt.Clear();
            dt.Dispose();

            formato_unico formatoUnico = null;
            if (lst!=null && lst.Count > 0)
            {
                formatoUnico = lst.FirstOrDefault();
            }
            return formatoUnico;
        }

        /// <summary>
        /// Metodo encargado de consultar las diferentes versiones de un FUP
        /// Autor: Global BI
        /// Fecha: 01-10-2018
        /// </summary>
        /// <param name="idFUP"></param>
        /// <returns></returns>
        public List<VersionFup> obtenerVersionesFUP(int idFUP)
        {
            string sql;
            sql = "SELECT eect_id, eect_vercot_id FROM fup_enc_entrada_cotizacion " +
                "WHERE eect_fup_id = " + idFUP + " ORDER BY eect_id DESC";

            DataTable dt = BdDatos.CargarTabla(sql);
            List<VersionFup> lst = dt.AsEnumerable()
                .Select(row => new VersionFup
                {
                    eect_id = (int)row["eect_id"],
                    eect_vercot_id = (string)row["eect_vercot_id"],
                    eect_fup_id = idFUP
                }).ToList();
            dt.Clear();
            dt.Dispose();

            return lst;
        }

        /// <summary>
        /// Metodo encargado de consultar la orden de fabricación por el idfup y la version
        /// Autor: Global BI
        /// Fecha: 01-10-2018
        /// </summary>
        /// <param name="idFup"></param>
        /// <param name="idVersion"></param>
        /// <returns></returns>
        public string obtenerOrdenFabricacionColombiaFUP(int idFup, string idVersion)
        {
            string sql;

            sql = "SELECT o.Numero, o.ano FROM Orden O inner join Orden_seg s on o.id_ofa = s.Id_ofa WHERE (o.Yale_Cotiza = " + idFup + ") " +
                "AND (o.ord_version = '" + idVersion + "') AND (o.letra = '1') AND (o.Anulada = 0) AND s.cotizacion = 0 ";
            DataTable dt = BdDatos.CargarTabla(sql);
            var lst = dt.AsEnumerable()
                .Select(row => string.Concat((string)row["Numero"],"-", (string)row["ano"])
                ).ToList();
            return lst.FirstOrDefault();
        }

        public string obtenerOrdenCotizacionFUP(int idFup, string idVersion, string TipoOf = "CT")
        {
            string sql, valor;

            sql = "SELECT o.Numero, o.ano, CONVERT(VARCHAR(20),s.Id_ofa) Id_ofa FROM Orden O inner join Orden_seg s on o.id_ofa = s.Id_ofa WHERE (o.Yale_Cotiza = " + idFup + ") " +
                "AND (o.ord_version = '" + idVersion + "') AND (o.letra = '1') AND (o.Anulada = 0) AND s.cotizacion = 1 AND s.Tipo_Of = '" + TipoOf + "'";
            DataTable dt = BdDatos.CargarTabla(sql);
            if (TipoOf == "CT")
            {
                var lst = dt.AsEnumerable()
                    .Select(row => string.Concat((string)row["Numero"], "-", (string)row["ano"])
                    ).ToList();
                valor = lst.FirstOrDefault();
            }
            else
            {
                var lst = dt.AsEnumerable()
                    .Select(row => (string)row["Id_ofa"]
                    ).ToList();
                valor = lst.FirstOrDefault();
            }

            return valor;
        }


        public List<ClaseCotizacion> obtenerClaseCotizacion()
        {
            string sql;
            sql = "SELECT [clase_cot_id], [clase_cot_sigla] + ' - ' + [clase_cot_descripcion]  texto " +
                "FROM fup_clase_cotizacion_ent where [clase_cot_activo] = 1 ";

            DataTable dt = BdDatos.CargarTabla(sql);
            List<ClaseCotizacion> lst = dt.AsEnumerable()
                .Select(row => new ClaseCotizacion
                {
                    clase_cot_id = (int)row["clase_cot_id"],
                    texto = (string)row["texto"]
                }).ToList();
            dt.Clear();
            dt.Dispose();

            return lst;
        }

        public List<Dominios> obtenerMotivoRechazoFUP()
        {
            string sql;
            sql = "SELECT [tre_id] fdom_CodDominio, [tre_descripcion] fdom_Descripcion " +
                "FROM fup_tipo_rechazo where tre_tipo < 9 ";

            DataTable dt = BdDatos.CargarTabla(sql);
            List<Dominios> lst = dt.AsEnumerable()
                .Select(row => new Entity.Dominios
                {
                    fdom_CodDominio = ((int)row["fdom_CodDominio"]).ToString(),
                    fdom_Descripcion = (string)row["fdom_Descripcion"]
                }).ToList();
            dt.Clear();
            dt.Dispose();

            return lst;
        }

        /// <summary>
        /// Metodo encargado de consultar las diferentes versiones de un FUP
        /// mediante la orden de fabricacion.
        /// Autor: Global BI
        /// Fecha: 01-10-2018
        /// </summary>
        /// <param name="idOF"></param>
        /// <returns></returns>
        public List<VersionFup> obtenerFUPporOrdenFabricacion(string idOF)
        {
            string sql;
            sql = @"SELECT 0 eect_id, o.Yale_Cotiza FUP, max(o.ord_version) version
                   from orden o
                where numero+'-'+ano = '" + idOF +
                "' group by o.Yale_Cotiza";

            DataTable dt = BdDatos.CargarTabla(sql);
            List<VersionFup> lst = dt.AsEnumerable()
                .Select(row => new VersionFup
                {
                    eect_id = (int)row["eect_id"],
                    eect_vercot_id = (string)row["version"],
                    eect_fup_id = (int)row["FUP"]
                }).ToList();
            dt.Clear();
            dt.Dispose();

            return lst;
        }

        public List<VersionFup> obtenerFUPporOrdenCliente(string idOrdenCliente)
        {
            string sql;
            sql = @"select eect_id,  b.eect_fup_id FUP, b.eect_vercot_id version 
                      from fup_enc_ent_OrdenCliente a 
	                   inner join fup_enc_entrada_cotizacion b on a.eecoc_enc_entrada_cot_id = b.eect_id 
                     where eecoc_OrdenCliente = '" + idOrdenCliente + "'";

            DataTable dt = BdDatos.CargarTabla(sql);
            List<VersionFup> lst = dt.AsEnumerable()
                .Select(row => new VersionFup
                {
                    eect_id = (int)row["eect_id"],
                    eect_vercot_id = (string)row["version"],
                    eect_fup_id = (int)row["FUP"]
                }).ToList();
            dt.Clear();
            dt.Dispose();

            return lst;
        }

        public List<Dominios> obtenerTipoRecotizacionFUP()
        {
            string sql;
            sql = "SELECT [trc_id] fdom_CodDominio, [trc_descripcion] fdom_Descripcion " +
                "FROM fup_tipo_recotizacion";

            DataTable dt = BdDatos.CargarTabla(sql);
            List<Dominios> lst = dt.AsEnumerable()
                .Select(row => new Entity.Dominios
                {
                    fdom_CodDominio = ((int)row["fdom_CodDominio"]).ToString(),
                    fdom_Descripcion = (string)row["fdom_Descripcion"],
                    fdom_DescripcionEN = (string)row["fdom_Descripcion"],
                    fdom_DescripcionPO = (string)row["fdom_Descripcion"]
                }).ToList();
            dt.Clear();
            dt.Dispose();

            return lst;
        }

        public List<Dominios> obtenerEventoPlanoTipoForsa()
        {
            string sql;
            sql = "SELECT [tes_id] fdom_CodDominio, [tes_descripcion] fdom_Descripcion " +
                "FROM fup_tipo_evento_segpf";

            DataTable dt = BdDatos.CargarTabla(sql);
            List<Dominios> lst = dt.AsEnumerable()
                .Select(row => new Entity.Dominios
                {
                    fdom_CodDominio = ((int)row["fdom_CodDominio"]).ToString(),
                    fdom_Descripcion = (string)row["fdom_Descripcion"],
                    fdom_DescripcionEN = (string)row["fdom_Descripcion"],
                    fdom_DescripcionPO = (string)row["fdom_Descripcion"]
                }).ToList();
            dt.Clear();
            dt.Dispose();

            return lst;
        }

        public List<Dominios> obtenerResponsablePlanoTipoForsa()
        {
            string sql;
            sql = "SELECT u.usu_emp_usu_num_id fdom_CodDominio, e.emp_nombre + ' ' + e.emp_apellidos fdom_Descripcion " +
                " FROM usuario u INNER JOIN empleado e ON u.usu_emp_usu_num_id = e.emp_usu_num_id " +
                " WHERE u.usu_rap_id in (24, 25,26) and u.usu_activo = 1";

            DataTable dt = BdDatos.CargarTabla(sql);
            List<Dominios> lst = dt.AsEnumerable()
                .Select(row => new Entity.Dominios
                {
                    fdom_CodDominio = ((int)row["fdom_CodDominio"]).ToString(),
                    fdom_Descripcion = (string)row["fdom_Descripcion"],
                    fdom_DescripcionEN = (string)row["fdom_Descripcion"],
                    fdom_DescripcionPO = (string)row["fdom_Descripcion"]
                }).ToList();
            dt.Clear();
            dt.Dispose();

            return lst;
        }

        public List<datosCombo2> CargarPuerto(int pais)
        {
            string sql;

            sql = "SELECT pue_id, pue_descripcion , pue_descripcion descripcionEn, pue_descripcion descripcionPO FROM sie.puerto " +
                  "WHERE pue_tipo_puerto_id = 1 AND pue_pais_id = '" + pais + "' and (1 = case when pue_pais_id = 8 and pue_ciudad_id = 0 THEN 0 ELSE 1 END ) ORDER BY pue_descripcion";

            DataTable dt = BdDatos.CargarTabla(sql);
            List<datosCombo2> lst = dt.AsEnumerable()
                .Select(row => new Entity.datosCombo2
                {
                    id = (row["pue_id"].ToString()),
                    descripcion = (string)row["pue_descripcion"]
                }).ToList();
            dt.Clear();
            dt.Dispose();

            return lst;
        }

        public int obtenerPaisCliente(string IdFup)
        {
            string sql;
            sql = @"select c.Cli_pai_id 
                      from formato_unico a 
	                   inner join Cliente c on a.fup_cli_id = c.cli_id
                     where a.fup_id = " + IdFup;

            DataTable dt = BdDatos.CargarTabla(sql);
            List<PaisFup> lst = dt.AsEnumerable()
                .Select(row => new PaisFup
                {
                    PaisId = (int)row["Cli_pai_id"]
                }).ToList();
            dt.Clear();
            dt.Dispose();

            return lst.FirstOrDefault().PaisId; 
        }
    }    
}
