using System;
using System.Data;
using System.Data.SqlClient;
using CapaDatos;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace CapaControl
{
	public class ControlSolicitudFacturacion
	{
		public int CerrarConexion()
		{
			return BdDatos.desconectar();
		}

		//CONSULTAR CONDICION DE PAGO
		public SqlDataReader ConsultarCondicionPago(int ccia)
		{
			string sql;
			sql = " select idCondPago, CondPago  from  Erp_Condicion_Pago where Planta_Id =" + ccia + " and activo = 1 order by 2 ";
			//sql = "SELECT ID, DESCRIPCION FROM VW_CONDICION_PAGO_1E where ID_CIA=" + ccia +
			//      " ORDER BY DESCRIPCION ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR LAS PLANTAS ACTIVAS
		public SqlDataReader ConsultarPlanta()
		{
			string sql;
			sql = "SELECT        planta_id, planta_descripcion, planta_ruta_imagen " +
					" FROM            planta_forsa "+
					" WHERE        (planta_activo = 1)";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR LAS PLANTAS ACTIVAS
		public SqlDataReader ConsultarPlantaProd(int plantafact)
		{
			string sql;
			sql = "SELECT        planta_forsa_fact_prod.planta_id_produccion, planta_forsa.planta_descripcion "+
					" FROM            planta_forsa_fact_prod INNER JOIN "+
					" planta_forsa ON planta_forsa_fact_prod.planta_id_produccion = planta_forsa.planta_id "+
					" WHERE        (planta_forsa_fact_prod.activo = 1) AND (planta_forsa_fact_prod.planta_id_facturar ="+ plantafact+")";
			return BdDatos.ConsultarConDataReader(sql);
		}

        //CONSULTAR LAS PLANTAS ACTIVAS
        public SqlDataReader ConsultarVendedorDw(int plantafact)
        {
            string sql;
            sql = "SELECT        IdVendedor, RazonSocial "+
                  "  FROM Vista_VendedoresErp_Dw "+
                  "  WHERE (IdPlanta = " + plantafact + ") ORDER BY RazonSocial";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTAR LAS PLANTAS ACTIVAS
        public SqlDataReader ConsultarFacturarPlanta()
		{
			string sql;
			sql = "SELECT        planta_id, planta_descripcion, planta_ruta_imagen " +
					" FROM            planta_forsa " +
					" WHERE            (planta_disp_facturar = 1)";
			return BdDatos.ConsultarConDataReader(sql);
		}

		public SqlDataReader ConsultarTipoSf()
		{
			string sql;
			sql = "SELECT        sf_tipo_id, sf_tipo_descripcion "+
					" FROM            solicitud_fact_tipo " +
					" WHERE        (sf_tipo_activo = 1)";
			return BdDatos.ConsultarConDataReader(sql);
		}

        public SqlDataReader ConsultarPuerto()
        {
            string sql;
            sql = "SELECT  pto_id, pto_nombre FROM puertos WHERE (pto_activo = 1) AND(pto_pais_id = 6) "+
                    " ORDER BY pto_nombre";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTAR LAS PLANTAS ACTIVAS     QUE NO ESTEN RELACIONADAS AL FUP
        public SqlDataReader ConsultarPlantaParte(int fup)
		{
			string sql;
			sql = "SELECT        planta_forsa.planta_id, planta_forsa.planta_descripcion "+
					"FROM            planta_forsa "+                        
					" WHERE        (planta_forsa.planta_id NOT IN "+
						   "  (SELECT        planta_forsa_1.planta_id "+
							"   FROM            pedido_venta AS pedido_venta_1 INNER JOIN "+
							 " planta_forsa AS planta_forsa_1 ON pedido_venta_1.planta_id = planta_forsa_1.planta_id "+
							  " WHERE        (pedido_venta_1.pv_fup_id = "+fup+"))) AND (planta_forsa.planta_activo = 1) ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR LAS PLANTAS ACTIVAS
		public SqlDataReader ConsultarCantPlantas(int fup)
		{
			string sql;
			sql =  "SELECT        COUNT(planta_id) AS Expr1 " +
				" FROM  planta_forsa WHERE        (planta_id NOT IN " +
				" (SELECT        planta_forsa_1.planta_id   FROM   pedido_venta AS pedido_venta_1 INNER JOIN " +
				"  planta_forsa AS planta_forsa_1 ON pedido_venta_1.planta_id = planta_forsa_1.planta_id " +
				" WHERE        (pedido_venta_1.pv_fup_id = " + fup + "))) AND (planta_activo = 1)";
		   
			return BdDatos.ConsultarConDataReader(sql);
		}


		//CONSULTAR LAS PARTES DE PEDIDO DE VENTA
		public SqlDataReader ConsultarPartesPv(int fup)
		{
			string sql;
			sql = "SELECT        pedido_venta.pv_id,pedido_venta.parte, planta_forsa.planta_descripcion + ' ' + CASE WHEN pedido_venta.parte = 1 THEN 'Principal' ELSE '' END AS Expr1," +
			"  pedido_venta.pv_id FROM            pedido_venta INNER JOIN " +
					 "    planta_forsa ON pedido_venta.planta_id = planta_forsa.planta_id"+
					" WHERE        (pedido_venta.pv_fup_id = "+@fup +") order by 2";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR LAS PARTES DE PEDIDO DE VENTA
		public SqlDataReader ConsultarPartesPvParte(int fup, int parte)
		{
			string sql;
			sql = "SELECT        pedido_venta.pv_id,pedido_venta.parte, planta_forsa.planta_descripcion + ' ' + CASE WHEN pedido_venta.parte = 1 THEN 'Principal' ELSE '' END AS Expr1," +
			"  pedido_venta.pv_id FROM            pedido_venta INNER JOIN " +
					 "    planta_forsa ON pedido_venta.planta_id = planta_forsa.planta_id" +
					" WHERE        (pedido_venta.pv_fup_id = " + @fup + ") and (pedido_venta.parte ="+parte+")";
			return BdDatos.ConsultarConDataReader(sql);
		}


		//CONSULTAR COMPAÑIA DE LA PLANTA
		public SqlDataReader ConsultarCompaniaPlanta(int planta)
		{
			string sql;

			sql = "SELECT  planta_id AS planta_cia, planta_forsa.planta_descripcion, ISNULL(planta_forsa.planta_ruta_imagen, '') AS Expr1, pais.pai_id,CAST(pais.pai_impuesto AS decimal(18, 2)) AS Expr2 " +
					" FROM planta_forsa INNER JOIN  pais ON planta_forsa.planta_pais_id = pais.pai_id "+
					" WHERE  planta_forsa.planta_id = " + planta;

			//sql = "SELECT        planta_cia, planta_descripcion, isnull(planta_ruta_imagen,'') FROM planta_forsa " +
			//        " WHERE  planta_id = "+ planta ;
			return BdDatos.ConsultarConDataReader(sql);
		}

		////CONSULTAR CONDICION DE PAGO
		//public SqlDataReader ConsultarCondicionPagoNew()
		//{
		//    string sql;
		//    sql = "SELECT ID, DESCRIPCION FROM VW_CONDICION_PAGO_1E WHERE     (ID = N'000') OR "+
		//             " (ID = N'008') OR (ID = N'030') OR (ID = N'180') OR (ID = N'999') OR " +
		//             " (ID = N'AC1') OR (ID = N'AC2')  "+
		//          " ORDER BY DESCRIPCION";
		//    return BdDatos.ConsultarConDataReader(sql);
		//}

		//CONSULTAR MOTIVO
		public SqlDataReader ConsultarMotivo(int cia)
		{
			string sql;
			sql = "select IdMotivoVenta, MotivoVenta from Erp_Motivo_Venta where Activo = 1 and Planta_Id = " + cia;
			//sql = "SELECT ID, DESCRIPCION FROM vw_motivo_venta_1E where ID_CIA=" + cia;
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR CENTRO DE OPERACION
		public SqlDataReader ConsultarCentroOperacion(int cia)
		{
			string sql;
			sql = " select  IdCentroOperacion , CentroOperacion from Erp_Centro_Operacion where activo = 1 and Planta = " + cia;
			//sql = "SELECT ID, DESCRIPCION FROM vw_centro_operacion_1E where  ID_CIA=" + cia;
			return BdDatos.ConsultarConDataReader(sql);
		}
		

		//CONSULTAR TIPO DE CLIENTE
		public SqlDataReader ConsultarTipoCliente(int cia)
		{
			string sql;
			sql = " select IdTipoCliente, TipoCliente from Erp_Tipo_Cliente where Activo = 1 and Planta_Id =" + cia;
			//sql = "SELECT ID, DESCRIPCION FROM vw_tipo_cliente_1E where ID_CIA=" + cia;
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR los datos en la tabla pedido de venta
		public SqlDataReader ConsultarPedidoVenta(int fup)
		{
			string sql;
			sql = "SELECT PV.pv_cli_cond_pago, cp1e.DESCRIPCION AS condpago, PV.pv_centro_operacion, "+
					  "  CO.DESCRIPCION AS centrooperacion, PV.pv_motivo, PV.pv_pais_cli_fact, "+
					  "  PA1E.DESCRIPCION AS paisfact, PV.pv_dpto_cli_fact, DP1E.DESCRIPCION AS departafact, PV.pv_ciu_cli_fact, "+
					  "  CI1E.DESCRIPCION AS ciudadfact, PV.pv_cli_fact, CL1E.DESCRIPCION_SUCURSAL, CL1E.ID AS nit, "+
					  "  PV.pv_pais_cli_desp, PA1E_1.DESCRIPCION AS paisdesp, PV.pv_dpto_cli_desp, "+
					  "  DP1E_1.DESCRIPCION AS departadesp, PV.pv_ciu_cli_desp, CI1E_1.DESCRIPCION AS ciudaddesp, "+
					  "  PV.pv_cli_despacho, CL1E_1.DESCRIPCION_SUCURSAL AS clientedesp, PV.pv_num_dias, "+
					  "  PV.pv_fecha_entrega, TC.DESCRIPCION, TC.ID "+
					  "FROM pedido_venta PV  "+
					  "	INNER JOIN VW_CONDICION_PAGO_1E cp1e ON cp1e.ID COLLATE Modern_Spanish_CI_AS = PV.pv_cli_cond_pago  "+
					  "	INNER JOIN dbo.VW_CENTRO_OPERACION_1E CO ON CO.ID COLLATE Modern_Spanish_CI_AS = PV.pv_centro_operacion  "+
					  "	INNER JOIN VW_TIPO_CLIENTE_1E TC ON TC.ID COLLATE Modern_Spanish_CI_AS = PV.pv_cli_tipo " +
					  " INNER JOIN VW_CLIENTES_ERP_1E CL1E ON CL1E.ID COLLATE Modern_Spanish_CI_AS = PV.pv_cli_fact   "+
					  "         AND CL1E.ID_SUCURSAL COLLATE Modern_Spanish_CI_AS = pv.pv_cli_fact_suc "+
					  "			AND CL1E.ID_CIA = PV_ID_CIA1E "+
					  " INNER JOIN VW_PAISES_1E PA1E ON PA1E.ID = CL1E.COD_DANE_PAIS "+
					  " INNER JOIN VW_DEPARTAMENTOS_1E DP1E ON DP1E.ID = CL1E.COD_DANE_DPTO AND PA1E.ID = DP1E.ID_PAIS  "+
					  " INNER JOIN VW_CIUDADES_1E CI1E ON CI1E.ID = CL1E.COD_DANE_CIUDAD AND PA1E.ID = CI1E.ID_PAIS AND  DP1E.ID = CI1E.ID_DEPTO  "+
					  "	INNER JOIN VW_CLIENTES_ERP_1E CL1E_1 ON CL1E_1.ID COLLATE Modern_Spanish_CI_AS = pv_cli_despacho "+
					  "			AND CL1E_1.ID_SUCURSAL COLLATE Modern_Spanish_CI_AS = pv.pv_cli_despacho_suc "+
					  "			AND CL1E_1.ID_CIA = PV_ID_CIA1E "+
					  "  INNER JOIN VW_PAISES_1E PA1E_1 ON PA1E_1.ID = CL1E_1.COD_DANE_PAIS "+
					  "  INNER JOIN VW_DEPARTAMENTOS_1E DP1E_1 ON DP1E_1.ID = CL1E_1.COD_DANE_DPTO AND PA1E_1.ID = DP1E_1.ID_PAIS  "+
					  "  INNER JOIN VW_CIUDADES_1E CI1E_1 ON CI1E_1.ID = CL1E_1.COD_DANE_CIUDAD AND PA1E_1.ID = CI1E_1.ID_PAIS AND DP1E_1.ID = CI1E_1.ID_DEPTO  " +
					  "WHERE (PV.pv_fup_id ="+ @fup+") ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR DIRECTOR DE OFICINA COLOMBIA
		public SqlDataReader ConsultarDirectorOficinaColombia(int pais, int ciudad)
		{
			string sql;
			sql = "SELECT DISTINCT rc_id, ltrim(rtrim(rc_descripcion)) rc_descripcion FROM representantes_comerciales " +
				"INNER JOIN pais_representante ON representantes_comerciales.rc_id = pais_representante.pr_id_representante " +
				"INNER JOIN ciudad_representante ON pais_representante.pr_id_pais =  ciudad_representante.cr_pai_id " + 
				"AND ciudad_representante.cr_rc_id = representantes_comerciales.rc_id " +
				"WHERE representantes_comerciales.rc_activo = 1 AND pais_representante.pr_id_pais = " + pais + " AND rc_director_ofic = 1 " +
				"AND ciudad_representante.cr_ciu_id = " + ciudad + " " +
				"ORDER BY 2 "; 
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR TIPO DE FLETE
		public SqlDataReader ConsultarTipoFlete()
		{
			string sql;
			sql = "SELECT tipofle_id, tipofle_descripcion  FROM  tipo_flete_sf  WHERE (tipofle_activo = 1)";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR MODO FACTURA DE FLETE
		public SqlDataReader ConsultarFacturarFlete()
		{
			string sql;
			sql = "SELECT     facflete_id, facflete_descripcion   FROM   tipo_factura_flete  WHERE     (facflete_activo = 1)";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR DIRECTOR DE OFICINA
		public SqlDataReader ConsultarDirectorOficina(int pais)
		{
			string sql;
			sql = "SELECT rc_id, rc_descripcion "+
                " FROM representantes_comerciales INNER JOIN  "+
                " pais_representante ON representantes_comerciales.rc_id = pais_representante.pr_id_representante INNER JOIN  "+
                " usuario ON representantes_comerciales.rc_usu_siif_id = usuario.usu_siif_id " +
                "WHERE representantes_comerciales.rc_activo = 1 AND (usuario.usu_activo = 1) AND pais_representante.pr_id_pais = " + pais + " " +
                "AND rc_director_ofic = 1   AND  pais_representante.pr_activo = 1 " +
				"ORDER BY representantes_comerciales.rc_descripcion ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR  la solicitud de facturacion
		public SqlDataReader ConsultarSolicitudFacturacion(int fup, string ver, int parte)
		{
			string sql;
			sql = "SELECT Sf_vlr_venta, Sf_vlr_comercial, Sf_porcdesc, Sf_vlr_dscto, Sf_razondesc, Sf_iva, Sf_transporte, " +
				"Sf_tltiva, Sf_comentarios, Sf_director_oficina, Sf_asesor_comercial, Sf_instrumento, Sf_forma_pago, " +
				"ternog_sigla, Sf_termino_negociacion, Sf_dir_obra_desp, sf_valor_alum, sf_valor_plast, sf_valor_acero " +
			 "FROM solicitud_facturacion INNER JOIN  termino_negociacion ON solicitud_facturacion.Sf_termino_negociacion = termino_negociacion.ternog_id " +
			 "WHERE (Sf_fup_id = " + fup + " AND Sf_version = '" + ver + "' AND Sf_parte = " + parte + ") ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//OBTENER MAIL PEDIDO DE VENTA
		public SqlDataReader ObtenerMailPV(string nRol, string pRol)
		{
			string sql = "SELECT emp_correo_electronico FROM empleado WHERE emp_mail_pv = 1 AND emp_activo = 1 ";

			return BdDatos.ConsultarConDataReader(sql);
		}

        //CONSULTA BOTON CONFIRMACION DE SF
        public SqlDataReader ConsultarBotonConfirmacionSF(int fup_id, string ver, int parte)
        {
            string sql;
            sql = "Select  isnull(cieseg_AutorizaJur, 0) cieseg_AutorizaJur, isnull(cieseg_AutorizaJurDesp, 0) cieseg_AutorizaJurDesp," +
                " isnull(cieseg_AutorizaTes, 0) cieseg_AutorizaTes, isnull(cieseg_AutorizaTesDesp, 0) cieseg_AutorizaTesDesp, " +

                " isnull(cieseg_AutorizaGerco, 0) cieseg_AutorizaGerco, isnull(cieseg_AutorizaGercoDesp, 0) cieseg_AutorizaGercoDesp, " +

                " isnull(cieseg_AutorizaViceco, 0) cieseg_AutorizaViceco, isnull(cieseg_AutorizaVicecoDesp, 0) cieseg_AutorizaVicecoDesp " +
                " from cierre_comercial" +
                " WHERE (cieseg_fup_id = " + fup_id + " AND cierre_version = '" + ver + "' )";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarPaisFup(int fup)
        {
            string sql;
            sql = "SELECT        cliente.cli_pai_id AS IdPais  FROM formato_unico INNER JOIN "+
                     "    cliente ON formato_unico.fup_cli_id = cliente.cli_id   "+
                     " WHERE (formato_unico.fup_id = "+ fup +")";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTA CONFIRMACION DE CIERRE DE SF
        public SqlDataReader ConsultarConfirmacionSF(int fup_id, string ver, int parte, int pvid)
		{
			string sql;
			sql = " SELECT  solicitud_facturacion.Sf_confirma, isnull(cliente.cli_arrendadora,0), isnull(solicitud_facturacion.confirma_financiero,0), " +
					" isnull(solicitud_facturacion.fecha_financiero,''), isnull(solicitud_facturacion.usu_financiero,''),ISNULL(solicitud_facturacion.notifica_financiero, 0) AS notifica " +
					" FROM   cliente INNER JOIN  formato_unico ON cliente.cli_id = formato_unico.fup_cli_id INNER JOIN " +
					" solicitud_facturacion ON formato_unico.fup_id = solicitud_facturacion.Sf_fup_id" +
				  " WHERE (Sf_fup_id = " + fup_id + " AND Sf_version = '" + ver + "' AND Sf_parte = " + parte + " AND pv_id= " + pvid + " )";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTA CONFIRMACION DE CIERRE DE SF
		public SqlDataReader ConsultarPedidoERPSF(int fup_id, string ver, int parte, int pvid)
		{
			string sql;
			sql = " SELECT  ISNULL([sf_id_pvc_erp],0) PedidoERP, ISNULL(cc.cieseg_m2_modulados,0) m2_modulados " +
				  " FROM solicitud_facturacion sf left outer join cierre_comercial cc on cc.cieseg_fup_id = sf.Sf_fup_id and cc.cierre_version = sf.Sf_version " +
				  " WHERE (Sf_fup_id = " + fup_id + " AND Sf_version = '" + ver + "' AND Sf_parte = " + parte + " AND pv_id= " + pvid + " )";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//actualizar Pedido ERP en sf
		public int actualizarPedidoERP(int pedidoERP, int fup_id, string ver, int parte, int pvId)
		{
			string sql;

			sql = "UPDATE solicitud_facturacion SET sf_id_pvc_erp = " + pedidoERP + " " +
				  "WHERE Sf_fup_id = " + fup_id + " AND Sf_version = '" + ver + "' AND Sf_parte = " + parte + "  AND pv_id = " + pvId;

			return BdDatos.Actualizar(sql);
		}

		//CONSULTO EL CLIENTE INTERNO A FACTURAR
		public SqlDataReader ConsultarClienteFacInterno(int plantaFact, int plantaProd)
		{
			string sql;
			sql = " SELECT        planta_forsa_fact_prod.ErpCliente_interno_id, Erp_Cliente.Cliente "+
					" FROM            planta_forsa_fact_prod INNER JOIN "+
					"  Erp_Cliente ON planta_forsa_fact_prod.ErpCliente_interno_id = Erp_Cliente.IdCliente "+
					" WHERE        (planta_forsa_fact_prod.activo = 1) AND (planta_forsa_fact_prod.planta_id_facturar ="+ plantaFact+") "+
					" AND (planta_forsa_fact_prod.planta_id_produccion ="+ @plantaProd+") AND (Erp_Cliente.Planta_Id ="+ plantaProd+")"+ 
					 "  AND (Erp_Cliente.Activo = 1) AND (planta_forsa_fact_prod.activo = 1)" ;
			return BdDatos.ConsultarConDataReader(sql);
		}

		public SqlDataReader ConsultarNotificaFinanciero(int fup_id, string ver)
		{
			string sql;
			sql = " SELECT ISNULL(notifica_financiero, 0) AS notifica "+
					" FROM   solicitud_facturacion  WHERE  (Sf_fup_id = "+fup_id+") AND (Sf_version = "+ver+")  ";
			 return BdDatos.ConsultarConDataReader(sql);
		}

		//actualizar la confirmacion de Financiero para arrendadoras en sf
		public int actualizarConfirmarSFAgente(string usu_confirma, int fup_id, string ver, int parte, int pvId)
		{
			string sql;
			string fecha_confirma = DateTime.Now.ToString("dd/MM/yyyy");

			sql = "UPDATE solicitud_facturacion SET Sf_confirma = 1, usu_actualiza = '" + usu_confirma + "', fecha_actualiza = '" + fecha_confirma + "' " +
				  "WHERE Sf_fup_id = " + fup_id + " AND Sf_version = '" + ver + "' AND Sf_parte = " + parte + "  AND pv_id = " +pvId ; 

			return BdDatos.Actualizar(sql);
		}

        //guardar el puerto en la sf
        public int guardarPuerto(string usu_confirma, int fup_id, string ver, int parte, int pvId, int puerto)
        {
            string sql;
            string fecha_confirma = DateTime.Now.ToString("dd/MM/yyyy");

            sql = "UPDATE solicitud_facturacion SET sf_id_puerto = "+puerto + " , sf_log_puerto = '" + usu_confirma +" - "+ fecha_confirma + "' "+
                  " WHERE Sf_fup_id = " + fup_id + " AND Sf_version = '" + ver + "' AND Sf_parte = " + parte + "  AND pv_id = " + pvId;

            return BdDatos.Actualizar(sql);
        }

        public int actualizarConfirmarFinanciero(string usu_confirma, int fup_id, string ver, int parte, int pvId)
		{
			string sql;
			string fecha_confirma = DateTime.Now.ToString("dd/MM/yyyy");

			sql = "UPDATE solicitud_facturacion SET confirma_financiero = 1, usu_financiero = '" + usu_confirma + "', fecha_financiero = '" + fecha_confirma + "' " +
				  "WHERE Sf_fup_id = " + fup_id + " AND Sf_version = '" + ver + "' AND Sf_parte = " + parte + "  AND pv_id = " + pvId;

			return BdDatos.Actualizar(sql);
		}

		//actualizar la confirmacion en sf
		public int actualizarNotificacionFinanciero(int fup_id, string ver)
		{
			string sql;
			string fecha_confirma = DateTime.Now.ToString("dd/MM/yyyy");

			sql = "update solicitud_facturacion set notifica_financiero = 1 where Sf_fup_id = "+fup_id+" and Sf_version = '"+ver+ "'";

			return BdDatos.Actualizar(sql);
		}


		//actualizar la confirmacion en sf
		public int desconfirmarSF(string usu_confirma, int fup_id, string ver, int parte)
		{
			string sql;
			string fecha_confirma = DateTime.Now.ToString("dd/MM/yyyy");

			sql = "UPDATE solicitud_facturacion SET Sf_confirma = 0, usu_actualiza = '" + usu_confirma + "', fecha_actualiza = '" + fecha_confirma + "' " +
				  "WHERE Sf_fup_id = " + fup_id;

			return BdDatos.Actualizar(sql);
		}

		//CONSULTAR GERENTE COMERCIAL
		public SqlDataReader ConsultarGerenteComercial(int pais)
		{
			string sql;
			sql = "SELECT rc_id, rc_descripcion FROM representantes_comerciales INNER JOIN pais_representante ON " + 
				"representantes_comerciales.rc_id = pais_representante.pr_id_representante INNER JOIN usuario ON " + 
				"representantes_comerciales.rc_usu_siif_id = usuario.usu_siif_id INNER JOIN rolapp ON " + 
				"usuario.usu_rap_id = rolapp.rap_id " + 
				"WHERE representantes_comerciales.rc_activo = 1 AND pais_representante.pr_id_pais = " + pais + " " +
                " AND (rolapp.rap_id = 2 OR rolapp.rap_id = 30)  AND (usuario.usu_activo = 1) " + 
				"ORDER BY representantes_comerciales.rc_descripcion ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR INSTRUMENTO DE PAGO
		public SqlDataReader ConsultarInstrumentoPago()
		{
			string sql;
			sql = "SELECT instpag_id, instpag_descripcion FROM instrumento_pago ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR FORMA DE PAGO
		public SqlDataReader ConsultarFormaPago()
		{
			string sql;
			sql = "SELECT formpag_id, upper(formpag_concepto) formpag_concepto FROM forma_pago ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR LA CIUDAD DELCLIENTE A FACTURAR
		public SqlDataReader ConsultarDepartamentoFacturar(int cia,string idpaisfact)
		{
			string sql;
			sql = "select IdDepartamento, Departamento from Erp_Departamento where activo = 1 and IdPais ='" + idpaisfact + "' and Planta_Id =" + cia+" order by 2 " ;
			//sql = "SELECT ID, DESCRIPCION FROM VW_DEPARTAMENTOS_1E " +
			//      "WHERE  (ID_PAIS ='" + idpaisfact + "') ORDER BY DESCRIPCION ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR LOS DATOS DEL CLIENTE
		public SqlDataReader ConsultarDatosClienteFact(string idclifact)
		{
			string sql;
			sql = "SELECT     ID_TIPO_CLI, ID_MONEDA, ID_VENDEDOR, ROWID_CONTACTO "+
				   "FROM         vw_Clientes_ERP_1E  WHERE     (ID = '"+idclifact+"')" ;
			return BdDatos.ConsultarConDataReader(sql);
		}


		public SqlDataReader ConsultarCiudadFacturar(int cia, string iddepfact, string idpaisfact)
		{
			string sql;
			sql = "select IdCiudad, ciudad from Erp_Ciudad where activo = 1 and " +
					" Planta_Id = " + cia + " and IdPais =  '" + idpaisfact + "' and IdDepartamento = '" + iddepfact + "' order by 2";
			//sql = "SELECT ID, DESCRIPCION FROM  VW_CIUDADES_1E " +
			//      "WHERE (ID_PAIS = '"+idpaisfact+"') AND (ID_DEPTO = '"+iddepfact+"') ORDER BY DESCRIPCION ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR EL CLIENTE A FACTURAR
		public SqlDataReader ConsultarClienteFacturar(string idciudfact, string idpaisfact, string iddepfact, int cia)
		{
			string sql;
			sql = " select IdCliente, Planta_Id, case when Planta_Id = 3 then  cliente +' CNPJ: '+ isnull(Nit_1,'') else cliente end as cliente,IdCliente , * from Erp_Cliente where Activo = 1 " +
					" and  idpais = '"+idpaisfact+"' and IdCiudad = '"+idciudfact+"' and IdDepartamento = '"+iddepfact+"' and Planta_Id ="+ cia+" order by 3";

			//sql = "  SELECT DISTINCT ID, ID_CIA, DESCRIPCION_SUCURSAL, ROWID_TERCERO FROM  vw_Clientes_ERP_1E " +
			//      "WHERE (COD_DANE_PAIS = " + idpaisfact + ") AND (COD_DANE_CIUDAD ='" + idciudfact + "') " +
			//      "AND (COD_DANE_DPTO ='" + iddepfact + "')AND (ID_CIA = "+ cia+ " )"+
			//      " ORDER BY DESCRIPCION_SUCURSAL";
			return BdDatos.ConsultarConDataReader(sql);
		}

		////CONSULTAR ESTADO DEL CLIENTE A FACTURAR
		//public SqlDataReader ConsultarEstadoClienteFact(int planta_Id, string idciudfact, string idpaisfact, string iddepfact, string idCliente)
		//{
		//    string sql;
		//    sql = "SELECT ID_CIA,ROWID_TERCERO,ID_SUCURSAL  FROM  vw_Clientes_ERP_1E " +
		//          "WHERE (COD_DANE_PAIS = " + idpaisfact + ") AND (COD_DANE_CIUDAD ='" + idciudfact + "') " +
		//          "AND (COD_DANE_DPTO =" + iddepfact + ")AND (ID ='" + idCliente + "') AND ID_CIA = 6 ORDER BY DESCRIPCION_SUCURSAL";
		//    return BdDatos.ConsultarConDataReader(sql);
		//}

		//PROCEDIMIENTO A ORACLE ESTADO CLIENTE
		public SqlDataReader ConsultarEstadoClienteFact(int planta_Id, string idciudfact, string idpaisfact, string iddepfact, string idCliente)
		{
			string sql;
			sql = "SELECT        Bloqueado, BloqCupo, BloqMora ,isnull( nit_1,'') , Direccion, isnull(nit_2,''), isnull(nit_3,'') FROM  Erp_Cliente  WHERE  " +
			"(Planta_Id = " + planta_Id + ") AND (Activo = 1) AND (IdPais = '" + idpaisfact + "') AND (IdDepartamento = '" + iddepfact + "') AND (IdCiudad = '" + idciudfact + "') AND ( IdCliente = '"+idCliente+"')";
		   // sql = "exec EstadoTercero "+compania+ "," +rowTercero+",'"+sucursal+"'";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR EL CLIENTE A FACTURAR
		public SqlDataReader ConsultarClienteFacCia1(string idCliente, int cia)
		{
			string sql;
			sql = "SELECT ID, ID_CIA, DESCRIPCION_SUCURSAL, ROWID_TERCERO FROM  vw_Clientes_ERP_1E " +
				  "WHERE ID = '" + idCliente.Trim() + "' AND ID_CIA ="+cia+
			"  ORDER BY DESCRIPCION_SUCURSAL";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR DIRECCION CLIENTE A FACTURAR
		public SqlDataReader ConsultarDireccionCliente(string idcliente)
		{
			string sql;
			sql = "SELECT ID, DIRECCION FROM VW_CLIENTES_ERP_1E " +
				  "WHERE (VW_CLIENTES_ERP_1E.ID = '" + idcliente + "')";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR DIRECCION CLIENTE A FACTURAR
		public SqlDataReader ConsultarFechaOfac(string idcliente)
		{
			string sql;
			sql = "SELECT     listofac_cli_facturar, listofac_fecha_revision, listofac_estado  " +
				  " FROM lista_ofac   WHERE     (listofac_cli_facturar ='" + idcliente + "') AND (listofac_activo = 1)";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR ESTADO DEL CLIENTE EN OFAC
		public SqlDataReader ConsultarSFingresada(int fup, string version , int parte)
		{
			string sql;
			sql = "SELECT     sf_ingresada FROM    solicitud_facturacion "+
				  " WHERE     (Sf_version ='" +@version+"') AND (Sf_parte = '"+@parte+"') AND (Sf_fup_id ="+ fup+")"; 
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR ENCABEZADO EN SF
		public SFEncabezado ConsultarEncabezadoSF(int sfId, ref string compania, ref string listaprecios)
		{
			SFEncabezado RegSF = null;
			string Recibe = "";
			StringBuilder sqlString = new StringBuilder();
			sqlString.Append("with ");
			sqlString.Append("infCia as");
			sqlString.Append(" (select *");
			sqlString.Append(" from openquery(FORSA1E,'select * from UNOEE2.T010_MM_COMPANIAS ') )");
			sqlString.Append(", tasa as");
			sqlString.Append("(select *");
			sqlString.Append("from openquery(FORSA1E,'select f018_id_cia, f018_id_moneda, f018_fecha, f018_tasa, ROW_NUMBER ( ) ");
			sqlString.Append("                OVER (PARTITION BY f018_id_cia, f018_id_moneda ORDER BY f018_id_cia, f018_fecha ) fila ");
			sqlString.Append("                from UNOEE2.T018_MM_TASAS_CAMBIO where f018_fecha >= trunc(sysdate) order by f018_id_cia, f018_fecha')");
			sqlString.Append(")");
			sqlString.Append(", cli as");
			sqlString.Append("(select *");

			sqlString.Append(" from openquery(FORSA1E,'select T.f200_id_cia, T.f200_rowid, T.f200_id, T.f200_nit, T.f200_razon_social, f201_id_sucursal, f201_id_vendedor, f201_id_cond_pago, F215_ID, f215_id_vendedor , V.F200_ID IDVENDEDOR, F201_id_lista_precio lista_precio");
			sqlString.Append("                from UNOEE2.T200_MM_TERCEROS T inner");
			sqlString.Append("                join UNOEE2.T201_MM_CLIENTES on f201_rowid_tercero = T.F200_ROWID left outer join UNOEE2.T215_MM_PUNTOS_ENVIO_CLIENTE on f215_rowid_tercero = f201_rowid_tercero and f215_id_sucursal = f201_id_sucursal");
			sqlString.Append("                LEFT OUTER JOIN UNOEE2.T210_MM_VENDEDORES ON F210_ID = f201_id_vendedor  AND F210_ID_CIA = T.f200_id_cia LEFT OUTER JOIN UNOEE2.T200_MM_TERCEROS V ON F210_ROWID_TERCERO = V.f200_rowid ')");

			sqlString.Append("     )");
			sqlString.Append(" select sf.Sf_id, planta_cia Compania, ISNULL(CONVERT(VARCHAR(200), cli.f200_nit), cl.Nit_1) tercero, ISNULL(f201_id_sucursal, '001') sucursal");
			sqlString.Append("    , convert(varchar(10), SF.fecha_crea, 120) FecHoy");
			sqlString.Append("    , convert(varchar(10), SF.fecha_crea, 120) fecEntrega");
			sqlString.Append("    , DATEDIFF(DAY, SF.fecha_crea, SF.fecha_crea) diasEntrega");
			sqlString.Append("    , pv.pv_fup_id FUP, mo.mon_erp MonedaDocum");
			sqlString.Append("    , ci.f010_id_moneda_conversion MonedaConversion");
			sqlString.Append("     , ci.f010_id_moneda_local MonedaLocal");
			sqlString.Append("      , isnull(ta.f018_tasa, 1) TasaConversion");
			sqlString.Append("    , isnull(ta.f018_tasa, 1) TasaLocal");
			sqlString.Append("    , ISNULL(f201_id_cond_pago, sf.Sf_condicion_pago) CondPago");
			sqlString.Append("    , sf.Sf_comentarios");
			sqlString.Append("    , fu.fup_usu_crea VendFup");
			sqlString.Append("    ,idVendedor TerceroVendedor");
			sqlString.Append("    , F215_ID PuntoEnvio");
			sqlString.Append("    , lista_precio");
			sqlString.Append("    , 'TDN : ' + tn.ternog_sigla + '       COMENTARIOS : ' + ISNULL(sf.Sf_comentarios, '') + ' DIRECCIÓN OBRA DESPACHO : ' + ISNULL(sf.Sf_dir_obra_desp, '') + ' DIRECCIÓN ENVÍO DOCUMENTOS : ' + ISNULL(sf.sf_dir_documentos, '') Nota");
			sqlString.Append(" from pedido_venta pv");
			sqlString.Append("    inner join solicitud_facturacion sf on sf.pv_id = pv.pv_id");
			sqlString.Append("    left outer join planta_forsa pf on pv.planta_id_fact = pf.planta_id");
			sqlString.Append("    left outer join Erp_Cliente cl on cl.IdCliente = CASE WHEN ISNULL(PV.ErpCliente_interno_id,'0') <> '0' THEN PV.ErpCliente_interno_id ELSE pv.pv_cli_fact END ");
			sqlString.Append("    left outer join formato_unico fu on fu.fup_id = pv.pv_fup_id");
			sqlString.Append("    left outer join moneda mo on mo.mon_id = fu.fup_unm_id");
			sqlString.Append("    left outer join infCia ci on ci.f010_id = planta_cia");
			sqlString.Append("    left outer join tasa ta on ta.f018_id_cia = planta_cia and fila = 1 and ta.f018_id_moneda = mo.mon_erp collate sql_Latin1_General_CP1_CI_AS");
			sqlString.Append("    left outer join cli on cli.f200_nit = cl.Nit_1 collate sql_Latin1_General_CP1_CI_AS AND ci.f010_id = f200_id_cia");
			sqlString.Append("    LEFT OUTER JOIN dbo.termino_negociacion tn ON tn.ternog_id = sf.Sf_termino_negociacion");
			sqlString.Append(" where sf.Sf_id = " + sfId + "");
			string sqlCadena = sqlString.ToString();
			sqlCadena.Replace("\r\n", " ");
			DataTable consulta = BdDatos.CargarTabla(sqlCadena);
			foreach (DataRow row in consulta.Rows)
			{
				RegSF = new SFEncabezado();
				//RegSF.ay = int.Parse(row["cedula"].ToString());
				//Consecutivo
				Recibe = "2";
				Recibe = Recibe.PadLeft(7,'0');
				Recibe = LimitLength(Recibe, 7);
				RegSF.Consecutivo = Recibe;
				//Tipo Registro
				Recibe = "0430";
				Recibe = Recibe.PadRight(4);
				Recibe = LimitLength(Recibe, 4);
				RegSF.TipoReg = Recibe;
				//Subtipo Registro
				Recibe = "00";
				Recibe = Recibe.PadLeft(2);
				Recibe = LimitLength(Recibe, 2);
				RegSF.SubTipoReg= Recibe;
				//Version Registro
				Recibe = "02";
				Recibe = Recibe.PadRight(2);
				Recibe = LimitLength(Recibe, 2);
				RegSF.VersionReg = Recibe;
				//Compañia
				Recibe = row["compania"].ToString();
				Recibe = Recibe.Trim();
				Recibe = Recibe.PadLeft(3,'0');
				Recibe = LimitLength(Recibe, 3);
				RegSF.Compania = Recibe;
				compania = row["compania"].ToString();
				//LiquidaImpuesto
				Recibe = "1";
				RegSF.LiquidaImpuesto = Convert.ToInt32(Recibe);
				//AutoRegistro
				Recibe = "1";
				RegSF.ConsecutivoAutoreg = Convert.ToInt32(Recibe);
				//IndicadorContacto
				Recibe = "1";
				RegSF.IndicadorContacto = Convert.ToInt32(Recibe);
				//Centro de Operacion
				Recibe = "001";
				Recibe = Recibe.PadLeft(3);
				Recibe = LimitLength(Recibe, 3);
				RegSF.CentroOperacionDocumento = Recibe;
				//TipoDocumento
				Recibe = "PVC";
				Recibe = Recibe.PadLeft(3);
				Recibe = LimitLength(Recibe, 3);
				RegSF.TipoDocumento = Recibe;
				//Numero Documento
				Recibe = "0";
				Recibe = Recibe.PadLeft(8,'0');
				Recibe = LimitLength(Recibe, 8);
				RegSF.NumeroDocumento = Recibe;
				//FecHoy
				Recibe = row["FecHoy"].ToString();
				Recibe = Recibe.PadLeft(10);
				Recibe = DateTime.Parse(Recibe).ToString("yyyyMMdd");
				Recibe = LimitLength(Recibe, 10);
				RegSF.FechaDocumento = Recibe;
				//Clase Interna
				Recibe = "502";
				Recibe = Recibe.PadLeft(3,'0');
				Recibe = LimitLength(Recibe, 3);
				RegSF.ClaseInternaDocumento = Recibe;
				//Estado Documento
				Recibe = "0";
				RegSF.EstadoDocumento = Convert.ToInt32(Recibe);
				//Backorder Documento
				Recibe = "0";
				RegSF.BackorderDocumento = Convert.ToInt32(Recibe);
				//Tercero
				Recibe = row["tercero"].ToString();
				Recibe = Recibe.PadRight(15);
				Recibe = LimitLength(Recibe, 15);
				RegSF.TerceroFacturar=Recibe;
				//Sucursal
				Recibe = row["sucursal"].ToString();
				Recibe = Recibe.PadLeft(3);
				Recibe = LimitLength(Recibe, 3);
				RegSF.SucursalFacturar=Recibe;
				//Tercero REM
				Recibe = row["tercero"].ToString();
				Recibe = Recibe.PadRight(15);
				Recibe = LimitLength(Recibe, 15);
				RegSF.TerceroDespachar = Recibe;
				//Sucursal REM
				Recibe = row["sucursal"].ToString();
				Recibe = Recibe.PadLeft(3);
				Recibe = LimitLength(Recibe, 3);
				RegSF.SucursalDespachar = Recibe;
				//Tipo Cliente
				Recibe = " ";
				Recibe = Recibe.PadLeft(4);
				Recibe = LimitLength(Recibe, 4);
				RegSF.TipoTercero = Recibe;
				//Centro Operacion Factura
				Recibe = "001";
				Recibe = Recibe.PadLeft(3);
				Recibe = LimitLength(Recibe, 3);
				RegSF.CentroOperacionFactura = Recibe;
				//FecEntrega
				Recibe = row["fecEntrega"].ToString();
				Recibe = Recibe.PadLeft(10);
				Recibe = DateTime.Parse(Recibe).ToString("yyyyMMdd");
				Recibe = LimitLength(Recibe, 10);
				RegSF.FechaEntregaPedido = Recibe;
				//DiasEntrega
				Recibe = row["diasEntrega"].ToString();
				Recibe = Recibe.PadLeft(3,'0');
				Recibe = LimitLength(Recibe, 3);
				RegSF.DiasEntregaPedido = Recibe;
				//Orden de Compra - num_docto_referencia
				Recibe = sfId.ToString();
				Recibe = Recibe.PadRight(15);
				Recibe = LimitLength(Recibe, 15);
				RegSF.OrdenCompraDocumento = Recibe;
				//FUP - Referencia Documento
				Recibe = row["FUP"].ToString();
				Recibe = Recibe.PadRight(10);
				Recibe = LimitLength(Recibe, 10);
				RegSF.ReferenciaDocumento = Recibe;
				//Codigo Cargue
				Recibe = " ";
				Recibe = Recibe.PadRight(10);
				Recibe = LimitLength(Recibe, 10);
				RegSF.CodigoCargueDocumento = Recibe;
				//MonedaDocum
				Recibe = row["MonedaDocum"].ToString();
				Recibe = Recibe.PadLeft(3);
				Recibe = LimitLength(Recibe, 3);
				RegSF.MonedaDocumento = Recibe;
				//MonedaConversion
				Recibe = row["MonedaConversion"].ToString();
				Recibe = Recibe.PadLeft(3);
				Recibe = LimitLength(Recibe, 3);
				RegSF.MonedaConversion = Recibe;
				//MonedaLocal
				Recibe = row["MonedaLocal"].ToString();
				Recibe = Recibe.PadLeft(3);
				Recibe = LimitLength(Recibe, 3);
				RegSF.MonedaLocal= Recibe;
				//TasaConversion
				Recibe = row["TasaConversion"].ToString();
				Recibe = Recibe.PadLeft(13,'0');
				Recibe = LimitLength(Recibe, 13);
				RegSF.TasaConversion = Recibe;
				//TasaLocal
				Recibe = row["TasaLocal"].ToString();
				Recibe = Recibe.PadLeft(13,'0');
				Recibe = LimitLength(Recibe, 13);
				RegSF.TasaLocal = Recibe;
				//CondPago
				Recibe = row["CondPago"].ToString();
				Recibe = Recibe.PadLeft(3);
				Recibe = LimitLength(Recibe, 3);
				RegSF.CondicionPago = Recibe;
				//Estado de Impresión
				Recibe = "0";
				RegSF.EstadoImpresion = Convert.ToInt32(Recibe);
				//Sf_comentarios - Observaciones Documento
				Recibe = row["Nota"].ToString();
				if (Recibe.Length > 2000)
				{
					Recibe = Recibe.Substring(1, 2000);
				};
				Recibe = Recibe.PadRight(2000);
				Recibe = LimitLength(Recibe, 2000);
				RegSF.Observaciones = Recibe;
				//Cliente de Contado
				Recibe = " ";
				Recibe = Recibe.PadLeft(15);
				Recibe = LimitLength(Recibe, 15);
				RegSF.ClientedeContado = Recibe;
				//PuntoEnvio
				Recibe = row["PuntoEnvio"].ToString();
				Recibe = Recibe.PadLeft(3);
				Recibe = LimitLength(Recibe, 3);
				RegSF.PuntodeEnvio = Recibe;

				//VendFup
				//Recibe = row["VendFup"].ToString();
				//if (Recibe.Length > 15)
				//{
				//    Recibe = Recibe.Substring(1, 15);
				//};
				//Recibe = Recibe.PadLeft(15);
				//Recibe = LimitLength(Recibe, 15);
				//RegSF.VendedorPedido = Recibe;

				//TerceroVendedor
				Recibe = row["TerceroVendedor"].ToString();
				if (Recibe.Length > 15)
				{
					Recibe = Recibe.Substring(1, 15);
				};
				Recibe = Recibe.PadLeft(15);
				Recibe = LimitLength(Recibe, 15);
				RegSF.VendedorPedido = Recibe;
				//Contacto
				Recibe = " ";
				Recibe = Recibe.PadLeft(50);
				Recibe = LimitLength(Recibe, 50);
				RegSF.Contacto = Recibe;
				//Direccion1
				Recibe = " ";
				Recibe = Recibe.PadLeft(40);
				Recibe = LimitLength(Recibe, 40);
				RegSF.Direccion1 = Recibe;
				//Direccion2
				Recibe = " ";
				Recibe = Recibe.PadLeft(40);
				Recibe = LimitLength(Recibe, 40);
				RegSF.Direccion2 = Recibe;
				//Direccion3
				Recibe = " ";
				Recibe = Recibe.PadLeft(40);
				Recibe = LimitLength(Recibe, 40);
				RegSF.Direccion3 = Recibe;
				//Pais
				Recibe = " ";
				Recibe = Recibe.PadLeft(2);
				Recibe = LimitLength(Recibe, 2);
				RegSF.Pais = Recibe;
				//Departemento - Estado
				Recibe = " ";
				Recibe = Recibe.PadLeft(3);
				Recibe = LimitLength(Recibe, 3);
				RegSF.DepEstado = Recibe;
				//Ciudad
				Recibe = " ";
				Recibe = Recibe.PadLeft(3);
				Recibe = LimitLength(Recibe, 3);
				RegSF.Ciudad = Recibe;
				//Barrio
				Recibe = " ";
				Recibe = Recibe.PadLeft(40);
				Recibe = LimitLength(Recibe, 40);
				RegSF.Barrio = Recibe;
				//Telefono
				Recibe = " ";
				Recibe = Recibe.PadLeft(20);
				Recibe = LimitLength(Recibe, 20);
				RegSF.Telefono = Recibe;
				//Fax
				Recibe = " ";
				Recibe = Recibe.PadLeft(20);
				Recibe = LimitLength(Recibe, 20);
				RegSF.Fax = Recibe;
				//Codigo Postal
				Recibe = " ";
				Recibe = Recibe.PadLeft(10);
				Recibe = LimitLength(Recibe, 10);
				RegSF.CodigoPostal = Recibe;
				//Email
				Recibe = " ";
				Recibe = Recibe.PadLeft(50);
				Recibe = LimitLength(Recibe, 50);
				RegSF.email = Recibe;
				//Indicador de Descuento
				Recibe = "0";
				RegSF.Descuento = Convert.ToInt32(Recibe);
				//lista de precios
				Recibe = row["lista_precio"].ToString();
				Recibe = Recibe.PadLeft(3);
				Recibe = LimitLength(Recibe, 3);
				listaprecios = Recibe;
			}
			return RegSF;
		}

		//CONSULTAR DETALLES EN SF
		public SFDetalle ConsultarDetallesSF(int sfId, ref string compania, ref string listaprecios)
		{
			SFDetalle RegSF = null;
			string Recibe = "";
			StringBuilder sqlString = new StringBuilder();
			sqlString.Append("with ");
			sqlString.Append(" DatAdicional AS (");
			sqlString.Append(" SELECT * FROM OPENQUERY(FORSA1E,'select * from UNOEE2.T120_MC_ITEMS')");
			sqlString.Append(" )");
			sqlString.Append("Select Item_1EE_Abuelo Item , pv.pv_centro_operacion CentroOperacion, pv.pv_unidad_negocio ");
			sqlString.Append(",convert(varchar(10), SF.fecha_crea, 120) FecHoy");
			sqlString.Append(",convert(varchar(10), SF.fecha_crea, 120) fecEntrega");
			sqlString.Append(",DATEDIFF(DAY, SF.fecha_crea, SF.fecha_crea) diasEntrega");
			//sqlString.Append("--,s f.sf_m2 Cantidad");
			sqlString.Append(", FORMAT(1,'000000000000000.0000') Cantidad");
			sqlString.Append(", FORMAT(sf.Sf_vlr_comercial,'000000000000000.0000') Valor");
			sqlString.Append(", i.F120_ID_Unidad_Inventario UMedida");
			sqlString.Append(" from pedido_venta pv");
			sqlString.Append(" inner join solicitud_facturacion sf on sf.pv_id = pv.pv_id");
			sqlString.Append(" inner join Orden_Seg os on os.sf_id = sf.Sf_id");
			sqlString.Append(" LEFT outer JOIN DatAdicional i ON i.F120_Id = Item_1EE_Abuelo AND i.F120_Id_Cia = " + compania + "");
			sqlString.Append(" where sf.Sf_id = " + sfId + "");
			string sqlCadena = sqlString.ToString();
			sqlCadena.Replace("\r\n", " ");
			DataTable consulta = BdDatos.CargarTabla(sqlCadena);
			foreach (DataRow row in consulta.Rows)
			{
				RegSF = new SFDetalle();
				//RegSF.ay = int.Parse(row["cedula"].ToString());
				//Consecutivo
				Recibe = "3";
				Recibe = Recibe.PadLeft(7, '0');
				Recibe = LimitLength(Recibe, 7);
				RegSF.Consecutivo = Recibe;
				//Tipo Registro
				Recibe = "0431";
				Recibe = Recibe.PadRight(4);
				Recibe = LimitLength(Recibe, 4);
				RegSF.TipoReg = Recibe;
				//Subtipo Registro
				Recibe = "00";
				Recibe = Recibe.PadLeft(2);
				Recibe = LimitLength(Recibe, 2);
				RegSF.SubTipoReg = Recibe;
				//Version Registro
				Recibe = "02";
				Recibe = Recibe.PadRight(2);
				Recibe = LimitLength(Recibe, 2);
				RegSF.VersionReg = Recibe;
				//Compañia
				Recibe = compania;
				Recibe = Recibe.Trim();
				Recibe = Recibe.PadLeft(3, '0');
				Recibe = LimitLength(Recibe, 3);
				RegSF.Compania = Recibe;
				//Centro de Operación
				Recibe = "001";
				Recibe = Recibe.PadLeft(3, '0');
				Recibe = LimitLength(Recibe, 3);
				RegSF.CentroOperacion = Recibe;
				//Tipo de Documento
				Recibe = "PVC";
				RegSF.TipoDocumento = Recibe;
				//ConsecutivoDocumento
				Recibe = "0";
				Recibe = Recibe.PadLeft(8,'0');
				Recibe = LimitLength(Recibe, 8);
				RegSF.ConsecDocumento = Recibe;
				//Número de Registro
				Recibe = "1";
				Recibe = Recibe.PadLeft(10, '0');
				Recibe = LimitLength(Recibe, 10);
				RegSF.NumeroReg = Recibe;
				//IdItem
				Recibe = row["Item"].ToString(); ;
				Recibe = Recibe.PadLeft(7,'0');
				Recibe = LimitLength(Recibe, 7);
				RegSF.IdItem = Recibe;
				//Referencia Item
				Recibe = " ";
				Recibe = Recibe.PadLeft(50);
				Recibe = LimitLength(Recibe, 50);
				RegSF.ReferenciaItem = Recibe;
				//Código de Barras
				Recibe = " ";
				Recibe = Recibe.PadLeft(20, '0');
				Recibe = LimitLength(Recibe, 20);
				RegSF.CodigoBarras = Recibe;
				//Extensión 1
				Recibe = " ";
				Recibe = Recibe.PadLeft(20);
				Recibe = LimitLength(Recibe, 20);
				RegSF.Extension1 = Recibe;
				//Extensión 2
				Recibe = " ";
				Recibe = Recibe.PadLeft(20);
				Recibe = LimitLength(Recibe, 20);
				RegSF.Extension2 = Recibe;
				//IdBodega
				Recibe = "B11";
				Recibe = Recibe.PadRight(5);
				Recibe = LimitLength(Recibe, 5);
				RegSF.Bodega = Recibe;
				//IdConcepto
				Recibe = "501";
				Recibe = Recibe.PadRight(3);
				Recibe = LimitLength(Recibe, 3);
				RegSF.Concepto = Recibe;
				//Motivo
				Recibe = "01";
				Recibe = Recibe.PadRight(2);
				Recibe = LimitLength(Recibe, 2);
				RegSF.Motivo= Recibe;
				//Indicador de Obsequio
				Recibe = "0";
				Recibe = Recibe.PadLeft(1);
				Recibe = LimitLength(Recibe, 1);
				RegSF.IndicadorObsequio = Convert.ToInt32(Recibe);
				//Centro Operacion Movimiento
				Recibe = row["CentroOperacion"].ToString(); ;
				Recibe = Recibe.PadLeft(3);
				Recibe = LimitLength(Recibe, 3);
				RegSF.CentroOperacionMovimiento = Recibe;
				//Unidad de Negocio Movimiento
				Recibe = row["pv_unidad_negocio"].ToString();
				Recibe = Recibe.PadRight(20);
				Recibe = LimitLength(Recibe, 20);
				RegSF.UnidadNegocioMovimiento = Recibe;
				//Centro de Costo Movimiento
				Recibe = " ";
				Recibe = Recibe.PadLeft(15);
				Recibe = LimitLength(Recibe, 15);
				RegSF.CentroCostoMovimiento = Recibe;
				//Proyecto
				Recibe = " ";
				Recibe = Recibe.PadLeft(15);
				Recibe = LimitLength(Recibe, 15);
				RegSF.Proyecto = Recibe;
				//FecEntrega
				Recibe = row["fecEntrega"].ToString();
				Recibe = Recibe.PadLeft(8);
				Recibe = DateTime.Parse(Recibe).ToString("yyyyMMdd");
				Recibe = LimitLength(Recibe, 8);
				RegSF.FechaEntregaPedido = Recibe;
				//Dias Entrega
				Recibe = row["diasEntrega"].ToString();
				Recibe = Recibe.PadLeft(3, '0');
				Recibe = LimitLength(Recibe, 3);
				RegSF.DiasEntregaPedido = Recibe;
				//Id Lista Precio
				Recibe = listaprecios;
				Recibe = Recibe.PadLeft(3);
				Recibe = LimitLength(Recibe, 3);
				RegSF.ListaPrecio = Recibe;
				//Unidad de Medida
				Recibe = row["UMedida"].ToString();
				Recibe = Recibe.PadLeft(4);
				Recibe = LimitLength(Recibe, 4);
				RegSF.UnidadMedida = Recibe;
				//Cantidad Base
				Recibe = row["Cantidad"].ToString();
				Recibe = Recibe.Replace(",", ".");
				Recibe = Recibe.PadLeft(20,'0');
				Recibe = LimitLength(Recibe, 20);
				RegSF.CantidadBase = Recibe;
				//Cantidad Adicional
				Recibe = "000000000000000.0000";
				Recibe = Recibe.PadRight(20, '0');
				Recibe = LimitLength(Recibe, 20);
				RegSF.CantidadAdicional = Recibe;
				//Valor Unitario
				Recibe = row["Valor"].ToString();
				Recibe = Recibe.Replace(",", ".");
				Recibe = Recibe.PadLeft(20, '0');
				Recibe = LimitLength(Recibe, 20);
				RegSF.PrecioUnitario = Recibe;
				//Impuestos Asumidos
				Recibe = "0";
				Recibe = Recibe.PadRight(1, '0');
				Recibe = LimitLength(Recibe, 1);
				RegSF.Impuestos = Recibe;
				//Notas
				Recibe = " ";
				Recibe = Recibe.PadLeft(255);
				Recibe = LimitLength(Recibe, 255);
				RegSF.Notas = Recibe;
				//Detalle
				Recibe = " ";
				Recibe = Recibe.PadLeft(2000);
				Recibe = LimitLength(Recibe, 2000);
				RegSF.Detalle = Recibe;
				//Backorder
				Recibe = "5";
				RegSF.Backorder = Convert.ToInt32(Recibe);
				//Indicador de Precio
				Recibe = "2";
				RegSF.IndicadorPrecio = Convert.ToInt32(Recibe);
			}
			return RegSF;
		}

		//CONSULTAR RESULTADO SF POR WS
		public String ResultadoSFxWS(int sfId, ref string compania, ref string CentroOpera, ref string tipodocto)
		{
			string Resultado = "";
			StringBuilder sqlString = new StringBuilder();
			sqlString.Append("SELECT F022_CONS_PROXIMO - 1 UltimoRegistro FROM ");
			sqlString.Append(" OPENQUERY(FORSA1E,'select * from UNOEE2.T022_MM_Consecutivos ");
			sqlString.Append(" where F022_ID_CIA = ''" + compania + "'' and F022_ID_CO = ''" + CentroOpera + "'' AND F022_ID_TIPO_DOCTO = ''" + tipodocto + "''')");
			string sqlCadena = sqlString.ToString();
			sqlCadena.Replace("\r\n", " ");
			DataTable consulta = BdDatos.CargarTabla(sqlCadena);
			foreach (DataRow row in consulta.Rows)
			{
				string resulta = row[0].ToString();
				//Resultado = "Consecutivo Asignado = " + resulta;
				Resultado =  resulta;
			}
			return Resultado;
		}
		//OBTENER EMAIL OFAC
		public SqlDataReader ObtenerMailOfac()
		{
			string sql = "SELECT emp_correo_electronico FROM empleado WHERE emp_mail_ofac = 1 AND emp_activo = 1 ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//OBTENER EMAIL OFAC
		public SqlDataReader ObtenerMailBloqueCli()
		{
			string sql = "SELECT emp_correo_electronico FROM empleado WHERE emp_mail_bloqueocli = 1 AND emp_activo = 1 ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//OBTENER EMAIL OFAC
		public SqlDataReader ObtenerMailAdmin()
		{
			string sql = "SELECT emp_correo_electronico FROM empleado WHERE emp_mail_admin = 1 AND emp_activo = 1 ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR TDN
		public SqlDataReader ConsultarTDN()
		{
			string sql;
			sql = "SELECT ternog_id, ternog_sigla FROM termino_negociacion ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTAR PAIS DE CLIENTE A FACTURAR xx
		public SqlDataReader ConsultarPaisFactura(int cia)
		{
			string sql;
			sql = "select IdPais, Pais from Erp_Pais where Activo = 1 and Planta_Id =" + cia +" order by 2 ";
			//sql = "SELECT ID, DESCRIPCION FROM  VW_PAISES_1E ORDER BY DESCRIPCION";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//SUMO LOS VALORES DE LOS ACCESORIOS PARA EL VALOR VENTA
		public SqlDataReader ObtnerSuma_accesorios(int fup_id, string nRol, string pRol)
		{
			string sql;
			sql = "SELECT SUM(cot_acc_precio_total) AS Suma " +
				  "FROM cotizacion_accesorios " +
				  "WHERE cot_acc_fup_id = " + fup_id + " AND cot_acc_activo = 1 " +
				  "GROUP BY cot_acc_fup_id ";
			return BdDatos.ConsultarConDataReader(sql);
		}               

		//CONSULTO EL VALOR DE FLETE DESDE FUP
		public SqlDataReader consultarFletepv(int fup_id)
		{
		   
			string sql;
			sql = "SELECT  formato_unico.fup_id, ISNULL(solicitud_facturacion.Sf_transporte, formato_unico.fup_total_flete) AS Expr1 " +
			" FROM   formato_unico LEFT OUTER JOIN  solicitud_facturacion ON formato_unico.fup_id = solicitud_facturacion.Sf_fup_id " +
			" WHERE   (formato_unico.fup_id =" + fup_id + ")";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTO EL VALOR DEL FLETE

		//CALCULO PARA EL IVA A TRAVEZ DE FUNCION EN LA BD
		public decimal calculoIVA(decimal valorventa)
		{
			// Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
			decimal Id = -1;

			// Parametros de la BBDD
			SqlParameter[] sqls = new SqlParameter[1];
			sqls[0] = new SqlParameter("valorventa", valorventa);

			string conexion = BdDatos.conexionScope();

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand("CalculoIVA", con))
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


		//INSERCION EN LA TABLA PEDIDO VENTA
		public int InsertarPv(int FUP, string CLIENTE_FACTURA ,string FACTURA_SUCURSAL,string CLIENTE_DESPACHO,
		string DESPACHO_SUCURSAL ,string @CO_FACTURA ,string TIPO_CLIENTE,string FECHA_ENTREGA ,string CLIENTE_MONEDA,
		string CLIENTE_CONDICION_PAGO ,string CLIENTE_NOTAS,string VENDEDOR,string CONTACTO,string DIRECCION  ,
		string MOTIVO,string CENTRO_OPERACION ,int NUM_DIAS,string COTIZACION,string NUMERO_PV ,string UNIDAD_NEGOCIO ,
		string PAIS_CLIENTE_FACTURA , string PAIS_CLIENTE_DESPACHO ,string DPTO_CLIENTE_FACTURA ,
		string DPTO_CLIENTE_DESPACHO ,string CIUDAD_CLIENTE_FACTURA,string CIUDAD_CLIENTE_DESPACHO ,
		string USUARIO , string @FECHA )
		{

			// Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
			int Id = -1;

			// Parametros de la BBDD
			SqlParameter[] sqls = new SqlParameter[28];
			sqls[0] = new SqlParameter("FUP ", FUP);
			sqls[1] = new SqlParameter("CLIENTE_FACTURA", CLIENTE_FACTURA);
			sqls[2] = new SqlParameter("FACTURA_SUCURSAL", FACTURA_SUCURSAL);
			sqls[3] = new SqlParameter("CLIENTE_DESPACHO", CLIENTE_DESPACHO);
			sqls[4] = new SqlParameter("DESPACHO_SUCURSAL", DESPACHO_SUCURSAL);
			sqls[5] = new SqlParameter("@CO_FACTURA", @CO_FACTURA);
			sqls[6] = new SqlParameter("TIPO_CLIENTE", TIPO_CLIENTE);
			sqls[7] = new SqlParameter("FECHA_ENTREGA", FECHA_ENTREGA);
			sqls[8] = new SqlParameter("CLIENTE_MONEDA", CLIENTE_MONEDA);
			sqls[9] = new SqlParameter("CLIENTE_CONDICION_PAGO", CLIENTE_CONDICION_PAGO);
			sqls[10] = new SqlParameter("CLIENTE_NOTAS", CLIENTE_NOTAS);
			sqls[11] = new SqlParameter("VENDEDOR", VENDEDOR);
			sqls[12] = new SqlParameter("CONTACTO", CONTACTO);
			sqls[13] = new SqlParameter("DIRECCION ", DIRECCION);
			sqls[14] = new SqlParameter("MOTIVO", MOTIVO);
			sqls[15] = new SqlParameter("CENTRO_OPERACION", CENTRO_OPERACION);
			sqls[16] = new SqlParameter("NUM_DIAS", NUM_DIAS);
			sqls[17] = new SqlParameter("COTIZACION", COTIZACION);
			sqls[18] = new SqlParameter("NUMERO_PV", NUMERO_PV);
			sqls[19] = new SqlParameter("UNIDAD_NEGOCIO", UNIDAD_NEGOCIO);
			sqls[20] = new SqlParameter("PAIS_CLIENTE_FACTURA", PAIS_CLIENTE_FACTURA);
			sqls[21] = new SqlParameter("PAIS_CLIENTE_DESPACHO", PAIS_CLIENTE_DESPACHO);
			sqls[22] = new SqlParameter("DPTO_CLIENTE_FACTURA", DPTO_CLIENTE_FACTURA);
			sqls[23] = new SqlParameter("DPTO_CLIENTE_DESPACHO", DPTO_CLIENTE_DESPACHO);
			sqls[24] = new SqlParameter("CIUDAD_CLIENTE_FACTURA", CIUDAD_CLIENTE_FACTURA);
			sqls[25] = new SqlParameter("CIUDAD_CLIENTE_DESPACHO", CIUDAD_CLIENTE_DESPACHO);
			sqls[26] = new SqlParameter("USUARIO", USUARIO);
			sqls[27] = new SqlParameter("@FECHA", @FECHA);
		  

			string conexion = BdDatos.conexionScope();

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand("InsertarPEDIDOVENTA", con))
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

		//ACTUALIZACION EN LA TABLA PEDIDO VENTA
		public int ActualizarPv(int FUP, string CLIENTE_FACTURA, string FACTURA_SUCURSAL, string CLIENTE_DESPACHO,
		string DESPACHO_SUCURSAL, string @CO_FACTURA, string TIPO_CLIENTE, string FECHA_ENTREGA, string CLIENTE_MONEDA,
		string CLIENTE_CONDICION_PAGO, string CLIENTE_NOTAS, string VENDEDOR, string CONTACTO, string DIRECCION,
		string MOTIVO, string CENTRO_OPERACION, int NUM_DIAS, string COTIZACION, string NUMERO_PV, string UNIDAD_NEGOCIO,
		string PAIS_CLIENTE_FACTURA, string PAIS_CLIENTE_DESPACHO, string DPTO_CLIENTE_FACTURA,
		string DPTO_CLIENTE_DESPACHO, string CIUDAD_CLIENTE_FACTURA, string CIUDAD_CLIENTE_DESPACHO,
		string USUARIO, string @FECHA)
		{

			// Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
			int Id = -1;

			// Parametros de la BBDD
			SqlParameter[] sqls = new SqlParameter[28];
			sqls[0] = new SqlParameter("FUP ", FUP);
			sqls[1] = new SqlParameter("CLIENTE_FACTURA", CLIENTE_FACTURA);
			sqls[2] = new SqlParameter("FACTURA_SUCURSAL", FACTURA_SUCURSAL);
			sqls[3] = new SqlParameter("CLIENTE_DESPACHO", CLIENTE_DESPACHO);
			sqls[4] = new SqlParameter("DESPACHO_SUCURSAL", DESPACHO_SUCURSAL);
			sqls[5] = new SqlParameter("@CO_FACTURA", @CO_FACTURA);
			sqls[6] = new SqlParameter("TIPO_CLIENTE", TIPO_CLIENTE);
			sqls[7] = new SqlParameter("FECHA_ENTREGA", FECHA_ENTREGA);
			sqls[8] = new SqlParameter("CLIENTE_MONEDA", CLIENTE_MONEDA);
			sqls[9] = new SqlParameter("CLIENTE_CONDICION_PAGO", CLIENTE_CONDICION_PAGO);
			sqls[10] = new SqlParameter("CLIENTE_NOTAS", CLIENTE_NOTAS);
			sqls[11] = new SqlParameter("VENDEDOR", VENDEDOR);
			sqls[12] = new SqlParameter("CONTACTO", CONTACTO);
			sqls[13] = new SqlParameter("DIRECCION ", DIRECCION);
			sqls[14] = new SqlParameter("MOTIVO", MOTIVO);
			sqls[15] = new SqlParameter("CENTRO_OPERACION", CENTRO_OPERACION);
			sqls[16] = new SqlParameter("NUM_DIAS", NUM_DIAS);
			sqls[17] = new SqlParameter("COTIZACION", COTIZACION);
			sqls[18] = new SqlParameter("NUMERO_PV", NUMERO_PV);
			sqls[19] = new SqlParameter("UNIDAD_NEGOCIO", UNIDAD_NEGOCIO);
			sqls[20] = new SqlParameter("PAIS_CLIENTE_FACTURA", PAIS_CLIENTE_FACTURA);
			sqls[21] = new SqlParameter("PAIS_CLIENTE_DESPACHO", PAIS_CLIENTE_DESPACHO);
			sqls[22] = new SqlParameter("DPTO_CLIENTE_FACTURA", DPTO_CLIENTE_FACTURA);
			sqls[23] = new SqlParameter("DPTO_CLIENTE_DESPACHO", DPTO_CLIENTE_DESPACHO);
			sqls[24] = new SqlParameter("CIUDAD_CLIENTE_FACTURA", CIUDAD_CLIENTE_FACTURA);
			sqls[25] = new SqlParameter("CIUDAD_CLIENTE_DESPACHO", CIUDAD_CLIENTE_DESPACHO);
			sqls[26] = new SqlParameter("USUARIO", USUARIO);
			sqls[27] = new SqlParameter("@FECHA", @FECHA);


			string conexion = BdDatos.conexionScope();

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand("ActualizarPEDIDOVENTA", con))
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

		//INSERCION EN LA TABLA Solicitud de facturacion
		public int InsertarSF(string Sf_cliente, string Sf_cliente_facturar, string Sf_director_oficina, string Sf_asesor_comercial,
		   string Sf_cotizacion, string Sf_solicitud_No, int Sf_termino_negociacion, string Sf_condicion_pago,
		   decimal Sf_vlr_venta, decimal Sf_transporte, decimal Sf_iva, decimal Sf_tltiva, decimal Sf_vlr_comercial,
		   string Sf_forma_pago, string Sf_instrumento, string Sf_comentarios, int Sf_fup_id, string Sf_dir_obra_desp,
		   string Sf_porcdesc, string Sf_razondesc, string usu_crea, string fecha_crea, decimal Sf_vlr_dscto)
		{          

			// Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
			int Id = -1;
		   
			// Parametros de la BBDD
			SqlParameter[] sqls = new SqlParameter[23];
			sqls[0] = new SqlParameter("Sf_cliente ", Sf_cliente);
			sqls[1] = new SqlParameter("Sf_cliente_facturar", Sf_cliente_facturar);
			sqls[2] = new SqlParameter("Sf_director_oficina", Sf_director_oficina);
			sqls[3] = new SqlParameter("Sf_asesor_comercial", Sf_asesor_comercial);
			sqls[4] = new SqlParameter("Sf_cotizacion", Sf_cotizacion);
			sqls[5] = new SqlParameter("Sf_solicitud_No", Sf_solicitud_No);
			sqls[6] = new SqlParameter("Sf_termino_negociacion", Sf_termino_negociacion);
			sqls[7] = new SqlParameter("Sf_condicion_pago", Sf_condicion_pago);
			sqls[8] = new SqlParameter("Sf_vlr_venta", Sf_vlr_venta);
			sqls[9] = new SqlParameter("Sf_transporte", Sf_transporte);
			sqls[10] = new SqlParameter("Sf_iva", Sf_iva);
			sqls[11] = new SqlParameter("Sf_tltiva", Sf_tltiva);
			sqls[12] = new SqlParameter("Sf_vlr_comercial", Sf_vlr_comercial);
			sqls[13] = new SqlParameter("Sf_forma_pago ", Sf_forma_pago);
			sqls[14] = new SqlParameter("Sf_instrumento", Sf_instrumento);
			sqls[15] = new SqlParameter("Sf_comentarios", Sf_comentarios);
			sqls[16] = new SqlParameter("Sf_fup_id", Sf_fup_id);
			sqls[17] = new SqlParameter("Sf_dir_obra_desp", Sf_dir_obra_desp);
			sqls[18] = new SqlParameter("Sf_porcdesc", Sf_porcdesc);
			sqls[19] = new SqlParameter("Sf_razondesc", Sf_razondesc);
			sqls[20] = new SqlParameter("usu_crea", usu_crea);
			sqls[21] = new SqlParameter("fecha_crea", fecha_crea);
			sqls[22] = new SqlParameter("Sf_vlr_dscto", Sf_vlr_dscto);
		   


			string conexion = BdDatos.conexionScope();

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand("InsertarSOLICITUDFACTURACION", con))
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

		//ACTUALIZACION EN LA TABLA Solicitud de facturacion
		public int ActualizarSF(string Sf_cliente, string Sf_cliente_facturar, string Sf_director_oficina, string Sf_asesor_comercial,
		   string Sf_cotizacion, string Sf_solicitud_No, int Sf_termino_negociacion, string Sf_condicion_pago,
		   decimal Sf_vlr_venta, decimal Sf_transporte, decimal Sf_iva, decimal Sf_tltiva, decimal Sf_vlr_comercial,
		   string Sf_forma_pago, string Sf_instrumento, string Sf_comentarios, int Sf_fup_id, string Sf_dir_obra_desp,
		   string Sf_porcdesc, string Sf_razondesc, string usu_crea, string fecha_crea, decimal Sf_vlr_dscto)
		{

			// Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
			int Id = -1;

			// Parametros de la BBDD
			SqlParameter[] sqls = new SqlParameter[23];
			sqls[0] = new SqlParameter("Sf_cliente ", Sf_cliente);
			sqls[1] = new SqlParameter("Sf_cliente_facturar", Sf_cliente_facturar);
			sqls[2] = new SqlParameter("Sf_director_oficina", Sf_director_oficina);
			sqls[3] = new SqlParameter("Sf_asesor_comercial", Sf_asesor_comercial);
			sqls[4] = new SqlParameter("Sf_cotizacion", Sf_cotizacion);
			sqls[5] = new SqlParameter("Sf_solicitud_No", Sf_solicitud_No);
			sqls[6] = new SqlParameter("Sf_termino_negociacion", Sf_termino_negociacion);
			sqls[7] = new SqlParameter("Sf_condicion_pago", Sf_condicion_pago);
			sqls[8] = new SqlParameter("Sf_vlr_venta", Sf_vlr_venta);
			sqls[9] = new SqlParameter("Sf_transporte", Sf_transporte);
			sqls[10] = new SqlParameter("Sf_iva", Sf_iva);
			sqls[11] = new SqlParameter("Sf_tltiva", Sf_tltiva);
			sqls[12] = new SqlParameter("Sf_vlr_comercial", Sf_vlr_comercial);
			sqls[13] = new SqlParameter("Sf_forma_pago ", Sf_forma_pago);
			sqls[14] = new SqlParameter("Sf_instrumento", Sf_instrumento);
			sqls[15] = new SqlParameter("Sf_comentarios", Sf_comentarios);
			sqls[16] = new SqlParameter("Sf_fup_id", Sf_fup_id);
			sqls[17] = new SqlParameter("Sf_dir_obra_desp", Sf_dir_obra_desp);
			sqls[18] = new SqlParameter("Sf_porcdesc", Sf_porcdesc);
			sqls[19] = new SqlParameter("Sf_razondesc", Sf_razondesc);
			sqls[20] = new SqlParameter("usu_crea", usu_crea);
			sqls[21] = new SqlParameter("fecha_crea", fecha_crea);
			sqls[22] = new SqlParameter("Sf_vlr_dscto", Sf_vlr_dscto);



			string conexion = BdDatos.conexionScope();

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand("ActualizarSOLICITUDFACTURACION", con))
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
		public SqlDataReader ObtenerAsesorComercial(int seleccion)
		{
			string sql;

			sql = "SELECT representantes_comerciales.rc_id, representantes_comerciales.rc_descripcion, representantes_comerciales.rc_email " +
				"FROM representantes_comerciales INNER JOIN usuario ON representantes_comerciales.rc_usu_siif_id = usuario.usu_siif_id INNER JOIN " +
				 "pais_representante ON representantes_comerciales.rc_id = pais_representante.pr_id_representante INNER JOIN rolapp ON usuario.usu_rap_id = rolapp.rap_id " +
				 "WHERE (representantes_comerciales.rc_activo = 1) AND (pais_representante.pr_id_pais = " + seleccion + ") AND (rolapp.rap_id = 2) AND " +
				 "(pais_representante.pr_activo = 1) OR (representantes_comerciales.rc_activo = 1) AND (pais_representante.pr_id_pais = " + seleccion + ") AND " +
				 "(rolapp.rap_id = 9) AND (pais_representante.pr_activo = 1) OR (representantes_comerciales.rc_activo = 1) AND (pais_representante.pr_id_pais = " + seleccion + ")  " +
				 "AND (rolapp.rap_id = 29) AND (pais_representante.pr_activo = 1) OR (representantes_comerciales.rc_activo = 1) AND (pais_representante.pr_id_pais = " + seleccion + ") " +
				 "AND (rolapp.rap_id = 30) AND (pais_representante.pr_activo = 1)";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//OBTENER sfid
		public SqlDataReader ObtenerSfId(int fup, string version, int parte,int Idpv)
		{
			string sql;

			sql = "SELECT  Sf_id FROM solicitud_facturacion WHERE "+
					" (Sf_fup_id = "+ fup + ") AND(Sf_version ='"+ version + "') AND(Sf_parte ="+ parte + ") AND(pv_id ="+ Idpv + ")";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//OBTENER GERENTES COMERCIALES
		public SqlDataReader ObtenerFinancieroAprobador()
		{
			string sql;

			sql = "SELECT   emp_correo_electronico FROM empleado WHERE (mail_conf_financiero = 1)";
			return BdDatos.ConsultarConDataReader(sql);
		}

		public SqlDataReader ConsultarSF(int fup, string version)
		{
			string sql;

			sql = "SELECT Sf_id, Sf_cliente, Sf_cliente_facturar, Sf_obra, Sf_dir_obra_desp, Sf_comentarios, " +
			"Sf_director_oficina, Sf_asesor_comercial, CAST(Sf_vlr_venta AS decimal(18, 2)) AS VrVenta,  " +
			"Sf_porcdesc, Sf_razondesc, CAST(Sf_iva AS decimal(18, 2)) AS IVA, CAST(Sf_transporte AS decimal(18, 2)) AS Transporte, " +
			"CAST(Sf_tltiva AS decimal(18, 2)) AS VlrTotalVenta, solicitud_facturacion.Sf_termino_negociacion, " +
			"termino_negociacion.ternog_sigla, Sf_condicion_pago, Sf_instrumento, CAST(Sf_vlr_comercial AS decimal(18, 2)) AS VlrComercial " +
			"FROM solicitud_facturacion  INNER JOIN termino_negociacion ON " +
			"solicitud_facturacion.Sf_termino_negociacion = termino_negociacion.ternog_id " +
			"WHERE Sf_fup_id = " + fup + " AND Sf_version = '" + version + "' ";

			return BdDatos.ConsultarConDataReader(sql);
		}

		//OBTENER SUMA PORCENTAJE
		public SqlDataReader ObtenerSumaPorcentaje(int fup_id, string nRol, string pRol)
		{
			string sql;
			sql = "SELECT SUM(CAST(cuo_porcentaje AS Decimal(18, 2))) AS Suma " +
				  "FROM cuota " +
				  "WHERE cuo_fup_id = " + fup_id + " " +
				  "GROUP BY cuo_fup_id ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//OBTENER SUMA PORCENTAJE
		public SqlDataReader ObtenerPorcentajecuota(int fup_id,int cuota, string nRol, string pRol)
		{
			string sql;
			sql = "SELECT SUM(CAST(cuo_porcentaje AS Decimal(18, 2))) AS Suma " +
				  "FROM cuota " +
				  "WHERE cuo_fup_id = " + fup_id + "  and cuo_num =" +cuota +" "+
				  "GROUP BY cuo_fup_id ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		public SqlDataReader consultarOrden(int fup, string ver, int parte)
		{
			string sql;

			sql = "SELECT Ofa, Id_Ofa, Numero FROM Orden " +
				"WHERE Yale_Cotiza = " + fup + " AND ord_version = '" + ver + "' AND ord_parte = " + parte + "";

			return BdDatos.ConsultarConDataReader(sql);
		}

		public int actualizarOFAccesorio(int Orden, int pv_numero, int fup_acc)
		{
			string sql;

			sql = "UPDATE Of_Accesorios SET Id_Ofa = " + Orden + ", pv_numero = " + pv_numero + ", cant_act = 1, Cant_Enviada = 0, Despachado = 0 " +
				  "WHERE Yale_Cotiza = " + fup_acc;

			return BdDatos.Actualizar(sql);
		}

		public int actualizarIdAccesorio(int fup_id)
		{
			string sql;

			sql = "SELECT Id_Accesorio From Of_Accesorios UPDATE Of_Accesorios SET Id_UnoE = Id_Accesorio " +
				  "WHERE Yale_Cotiza = " + fup_id;

			return BdDatos.Actualizar(sql);
		}

		public int IngresarDatosCierre(int txtfup, int moneda, string txtvalortot, string txtIVApv, int cli, int Orden, int contacto, string rcNombre, int tdn)
		{

			string sql;
			string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
			sql = "INSERT INTO cierre_venta (cierre_id_fup, cierre_moneda_id, cierre_valor_total_cotizado, cierre_iva_valor, cierre_id_clif, cierre_id_ofp, " +
				"cierre_id_ccl, usu_modifica, cierre_id_tdn, fecha_modifica, cierre_cotizacion_final) " +
				  "VALUES (" + txtfup + "," + moneda + ",'" + txtvalortot.Replace(",", ".") + "','" + txtIVApv.Replace(",", ".") + "'," + cli + "," + Orden + "," + contacto + ",'" + rcNombre +
				  "'," + tdn + ",'" + fecha + "'," + txtfup + ") ";

			return BdDatos.Actualizar(sql);
		}

		//OBTENER MAIL SOLICITUD DE FACTURACION
		public SqlDataReader ObtenerMailSF(string nRol, string pRol)
		{
			string sql = "SELECT emp_correo_electronico FROM empleado WHERE emp_mail_sf = 1 AND emp_activo = 1 ";

			return BdDatos.ConsultarConDataReader(sql);
		}

		public int ActualizarDatosConfirma(string usu_confirma, int fup_id, int estado)
		{
			string sql;

			string fecha_confirma = DateTime.Now.ToString("dd/MM/yyyy");

			sql = "UPDATE pedido_venta SET pv_confirma_com = "+ estado+" , pv_usu_confirma = '" + usu_confirma + "', pv_fecha_confirma = '" + fecha_confirma + "' " +
				  "WHERE pv_fup_id = " + fup_id;

			return BdDatos.Actualizar(sql);
		}

		//ACA TRAIGO EL CONSECUTIVO DE PEDIDO DE VENTA
		public SqlDataReader consultarConsecutivoPV()
		{
			string sql;
			sql = "SELECT pv_numero FROM consecutivo ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		public SqlDataReader ConsultarDetallePV(int fup)
		{
			string sql;

			sql = "SELECT pv_fup_id, pv_entrega_almacen FROM pedido_venta " +
				"WHERE pv_fup_id = " + fup + " ";

			return BdDatos.ConsultarConDataReader(sql);
		}

		public SqlDataReader ConsultarConfirmacion(int fup)
		{
			string sql;

			sql = "SELECT pv_confirma_com FROM pedido_venta " +
				"WHERE pv_fup_id = " + fup + " ";

			return BdDatos.ConsultarConDataReader(sql);
		}

		public SqlDataReader ConsultarRechazoSf(int fup, string nRol, string pRol)
		{
			string sql;

			sql = "SELECT     sf_rech_fup, sf_rech_razon_social, sf_rech_nit, sf_rech_direccion, "+
				  " sf_rech_telefono, sf_rech_cond_pago, sf_rech_tdn, sf_rech_vlr_comercial, "+
				  " sf_rech_observaciones, sf_rech_usu_crea, sf_rech_fecha_crea FROM  solicitud_fact_rechazo "+
				  " WHERE     (sf_rech_fup =" + fup + ") ";

			return BdDatos.ConsultarConDataReader(sql);
		}

		public int IngresarDatosOrdenAcc(int fup)
		{
			string sql;

			sql = "INSERT INTO Of_Accesorios "+
			" (Id_UnoE,			Id_Ofa,			Id_Accesorio,	id_cot_accesorios,	Yale_Cotiza,	 Cant_Req,     fecha_crea, usu_crea, No_Item,  Observa,			pv_numero, Saldo ) " +
			" SELECT    cot_acc_id_acc,	Orden.Id_Ofa,	cot_acc_id_acc,	cot_acc_id,			cot_acc_fup_id,	 cot_cantidad, fecha_crea, usu_crea, cot_item, cot_observacion,	Orden.Numero , cot_cantidad  " +
			" FROM            cotizacion_accesorios INNER JOIN " +
			" Orden ON cotizacion_accesorios.cot_acc_fup_id = Orden.Yale_Cotiza " +
				  "WHERE cot_acc_fup_id = " + fup + " AND cot_acc_activo = 1 ";

			return BdDatos.Actualizar(sql);
		}

		public int IngresarRechazoSf(int fup,string version,bool razonsocial,bool nit,bool direccion,bool telefono,
									bool condpago,bool tdn,bool vlrcomercial,string observaciones,string usuario)
		{
			string sql;
			int razonsocial1 = 0, nit1 = 0, direccion1 = 0, telefono1 = 0, condpago1 = 0, tdn1 = 0, vlrcomercial1 = 0;
			if (razonsocial == true)
				razonsocial1 = 1;
			if (nit == true)
				nit1 = 1;
			if (direccion == true)
				direccion1 = 1;
			if (telefono == true)
				telefono1 = 1;
			if (condpago == true)
				condpago1 = 1;
			if (tdn == true)
				tdn1 = 1;
			if (vlrcomercial == true)
				vlrcomercial1 = 1;

			sql = "INSERT INTO solicitud_fact_rechazo(sf_rech_fup, sf_rech_version, sf_rech_razon_social, sf_rech_nit, sf_rech_direccion, sf_rech_telefono, " +
				" sf_rech_cond_pago,sf_rech_tdn,sf_rech_vlr_comercial,sf_rech_observaciones, sf_rech_usu_crea )" +
				" VALUES ("+fup+",'"+version+"',"+razonsocial1+","+nit1+","+direccion1+","+telefono1+","+condpago1+","+
				tdn1+","+vlrcomercial1+",'"+observaciones+"','"+usuario+"')";

			return BdDatos.Actualizar(sql);

		}



		public int actualizarCuota(int txtReferencia, int cuotanum, string valorpagar, string porcentaje, string valorpagado, string fechareal, string estado,
			string comentario, string nombre)
		{
			string sql;
			string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

			sql = "UPDATE cuota SET cuo_valor_pagar = '" + valorpagar.Replace(",", ".") + "', cuo_porcentaje = '" + porcentaje + "', cuo_valor_pagado = '" + valorpagado.Replace(",", ".") +
				"', cuo_fecha_real = '" + fechareal + "', cuo_estado = '" + estado + "', cuo_comentarios = '" + comentario + "', usu_actualiza = '" + nombre +
				"', fecha_actualiza = '" + fecha + "' " +
				  "WHERE (cuo_fup_id = " + txtReferencia + ") AND (cuo_num = " + cuotanum + " )";

			return BdDatos.Actualizar(sql);
		}

		public int IngresarDatosCuota(int txtfup, int cuota, string txtapagar, string txtpagado, string txtporpagar,
			string txtfechreal, string txtestado, string com, string sfnum, string NombreRep, string version, int parte)
		{

			string sql;
			string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

			sql = "INSERT INTO cuota (cuo_fup_id, cuo_num, cuo_valor_pagar, cuo_valor_pagado, cuo_porcentaje, cuo_fecha_esperada, cuo_fecha_real, cuo_estado, cuo_comentarios, cuo_sf_id, usu_crea, fecha_crea, " +
				  "usu_actualiza, fecha_actualiza, cuo_version, cuo_parte) " +
				  "VALUES (" + txtfup + "," + cuota + ",'" + txtapagar.Replace(",", ".") + "','" + txtpagado.Replace(",", ".") + "','" + txtporpagar + "','" + txtfechreal + "','" + fecha +
				  "','" + txtestado + "','" + com + "','" + sfnum + "','" + NombreRep + "','" + fecha + "','" + NombreRep + "','" + fecha + "','" + version + "'," + parte + ") ";

			return BdDatos.Actualizar(sql);
		}

		//OBTENER SUMA CUOTA
		public SqlDataReader ObtenerSumaCuota(int fup_id)
		{
			string sql;
			sql = "SELECT SUM(CAST(cuo_valor_pagar AS Decimal(18, 2))) AS Suma " +
				  "FROM cuota " +
				  "WHERE cuo_fup_id = " + fup_id + " " +
				  "GROUP BY cuo_fup_id ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		public int EliminarCuota(int fup, int cuota, string ver)
		{
			string sql;

			sql = "DELETE FROM cuota WHERE (cuo_fup_id = " + fup + ") AND (cuo_num = " + cuota + " ) " +
				"AND (cuo_version = '" + ver + "') ";

			return BdDatos.Actualizar(sql);
		}


		public SqlDataReader ObtnerMonedaFup(int fup_id, string nRol, string pRol)
		{
			string sql;
			sql = "SELECT formato_unico.fup_id, unidad_moneda.unm_nombre, unidad_moneda.unm_simbolo " +
				  "FROM formato_unico INNER JOIN " +
				  "unidad_moneda ON formato_unico.fup_unm_id = unidad_moneda.unm_id " +
				  "WHERE (formato_unico.fup_id = " + fup_id + ")";
			return BdDatos.ConsultarConDataReader(sql);
		}

		public DataSet cuotaAcc(int fup_id, string ver)
		{
			string sql;

			sql = "SELECT cuo_num CUOTA, CONVERT(VARCHAR(40),CAST (cuo_valor_pagar AS MONEY),1) [VALOR A PAGAR] , " +
			"cuo_porcentaje [%], CONVERT(VARCHAR(40),CAST (cuo_valor_pagado AS MONEY),1) [VALOR PAGADO], " +
			"CONVERT(VARCHAR(10),cuo_fecha_real,120) [FECHA REAL], cuo_estado [ESTADO], cuo_comentarios [COMENTARIOS] " +
			"FROM cuota " +
			"WHERE (cuo_fup_id = " + fup_id + ") AND (cuo_version = '" + ver + "') ";

			return BdDatos.consultarConDataset(sql);
		}

		//OBTENER MAIL PORCENTAJE
		public SqlDataReader ObtenerMailPorcentaje(string nRol, string pRol)
		{
			string sql = "SELECT emp_correo_electronico FROM empleado WHERE emp_mail_porc = 1 AND emp_activo = 1 ";

			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTA PARA POBLAR LAS PARTES DE LA SF
		public SqlDataReader PoblarParte(int FUP, string ver,int pv_id)
		{
			string sql;

			sql = "SELECT Sf_id, Sf_parte, Sf_confirma  FROM solicitud_facturacion " +
				  "WHERE Sf_fup_id = " + FUP + " AND Sf_version = '" + ver + "' AND (pv_id =" + pv_id +") "+
				  "ORDER BY Sf_parte Asc";

			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTA SI EXISTE LA PARTE INDICADA DE LA SF
		public SqlDataReader ConsultarParte(int FUP, string ver, int parte, int pv_id)
		{
			string sql;

			sql = "SELECT sf.Sf_id, Sf_parte,  RTRIM(ISNULL(Orden_Seg.Tipo_Of, '')) + ' ' + RTRIM(ISNULL(Orden_Seg.Num_Of, '')) + '-' + RTRIM(ISNULL(Orden_Seg.Ano_Of, '')) AS Orden, isnull(sf.sf_id_puerto,0) as IdPuerto " +
                   " FROM solicitud_facturacion sf LEFT OUTER JOIN "+
                  " Orden_Seg ON sf.Sf_id = Orden_Seg.sf_id " +
                  "WHERE sf.Sf_fup_id = " + FUP + " AND sf.Sf_version = '" + ver + "' AND sf.Sf_parte = " + parte + " AND sf.pv_id =  " + pv_id;

			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTA MAXIMO PARTES DE LA SF
		public SqlDataReader  MaximoParte(int FUP, string ver, int pv_id)
		{
			string sql;

			sql = "SELECT MAX(Sf_parte) FROM solicitud_facturacion " +
				  "WHERE Sf_fup_id = " + FUP + " AND Sf_version = '" + ver + "' and pv_id="+pv_id;

			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTA MAXIMO PARTES DE LA SF
		public SqlDataReader MaximoPartePv(int FUP)
		{
			string sql;

			sql = "SELECT MAX(parte) FROM pedido_venta " +
				  "WHERE pv_fup_id = " + FUP ;

			return BdDatos.ConsultarConDataReader(sql);
		}

		//INGRESO DE SOLICITUD DE FACTURACIÓN
		public int IngSF(int numfup, string ver, int parte, string nom, string client,
			string client_fact, string obra, string dirofic, string asescom, string solnum,
			int tdn, string condpag, string vr_vta, string transp, string iva, string vr_total,
			string vr_com, string formpag, string instpag, int cuotas, string coment, 
			string dirobra, string prod, string porcdesc, string vr_dcto, string razdesc,
			string clidesp, string vr_alum, string vr_plast, string vr_acces, string fecpv,
			int solcli, string client_fact_suc, string clidesp_suc,
			string fact_co, string tipocliente, string fec_entrega, string cli_moneda, string vendedor, 
			string clicont, string motivo, string centope, int numdias, string undneg,
			string cfpais, string cfdpto, string cfciud, string cdpais, string cddpto,
			string cdciu, string codconpag, string codclifact, string codclidesp)
		{
			// Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
			int Id = -1;

			// Parametros de la BBDD
			SqlParameter[] sqls = new SqlParameter[59];
			sqls[0] = new SqlParameter("pFupID", numfup);
			sqls[1] = new SqlParameter("pVersion", ver);
			sqls[2] = new SqlParameter("pparte", parte);
			sqls[3] = new SqlParameter("pcliente", client);
			sqls[4] = new SqlParameter("pcliente_facturar", client_fact);
			sqls[5] = new SqlParameter("pdirector_oficina", dirofic);
			sqls[6] = new SqlParameter("pasesor_comercial", asescom);
			sqls[7] = new SqlParameter("pcotizacion", numfup);
			sqls[8] = new SqlParameter("psolicitud_No", solnum);
			sqls[9] = new SqlParameter("ptermino_negociacion", tdn);
			sqls[10] = new SqlParameter("pcondicion_pago", condpag);
			sqls[11] = new SqlParameter("pvlr_venta", vr_vta);
			sqls[12] = new SqlParameter("ptransporte", transp);
			sqls[13] = new SqlParameter("piva", iva);
			sqls[14] = new SqlParameter("ptltiva", vr_total);
			sqls[15] = new SqlParameter("pvlr_comercial", vr_com);
			sqls[16] = new SqlParameter("pforma_pago", formpag);
			sqls[17] = new SqlParameter("pinstrumento", instpag);
			sqls[18] = new SqlParameter("pcuotas", cuotas);
			sqls[19] = new SqlParameter("pcomentarios", coment);
			sqls[20] = new SqlParameter("pdir_obra_desp", dirobra);
			sqls[21] = new SqlParameter("pproducido", prod);
			sqls[22] = new SqlParameter("pnum", solnum);
			sqls[23] = new SqlParameter("pporcdesc", porcdesc);
			sqls[24] = new SqlParameter("pvlr_dscto", vr_dcto);
			sqls[25] = new SqlParameter("prazondesc", razdesc);
			sqls[26] = new SqlParameter("pcliente_despacho", clidesp);
			sqls[27] = new SqlParameter("pvalor_alum", vr_alum);
			sqls[28] = new SqlParameter("pvalor_plast", vr_plast);
			sqls[29] = new SqlParameter("pvalor_acero", vr_acces);
			sqls[30] = new SqlParameter("pfecha", fecpv);
			sqls[31] = new SqlParameter("psol_cli", solcli);
			sqls[32] = new SqlParameter("ptipo_solicitud", "502");
			sqls[33] = new SqlParameter("pcli_fact", codclifact);
			sqls[34] = new SqlParameter("pcli_fact_suc", client_fact_suc);
			sqls[35] = new SqlParameter("pcli_despacho", codclidesp);
			sqls[36] = new SqlParameter("pcli_despacho_suc", clidesp_suc);
			sqls[37] = new SqlParameter("pcli_fact_co", fact_co);
			sqls[38] = new SqlParameter("pcli_tipo", tipocliente);
			sqls[39] = new SqlParameter("pfecha_entrega", fec_entrega);
			sqls[40] = new SqlParameter("pcli_moneda", cli_moneda);
			sqls[41] = new SqlParameter("pcli_cond_pago", codconpag);
			sqls[42] = new SqlParameter("pcli_notas", coment);
			sqls[43] = new SqlParameter("pvendedor", vendedor);
			sqls[44] = new SqlParameter("pcli_contacto", clicont);
			sqls[45] = new SqlParameter("pmotivo", motivo);
			sqls[46] = new SqlParameter("pcentro_operacion", centope);
			sqls[47] = new SqlParameter("pnum_dias", numdias);
			sqls[48] = new SqlParameter("pnumero", solnum);
			sqls[49] = new SqlParameter("punidad_negocio", undneg);
			sqls[50] = new SqlParameter("psolicitud", "Orden De Fabricación");
			sqls[51] = new SqlParameter("ppais_cli_fact", cfpais);
			sqls[52] = new SqlParameter("ppais_cli_desp", cdpais);
			sqls[53] = new SqlParameter("pdpto_cli_fact", cfdpto);
			sqls[54] = new SqlParameter("pdpto_cli_desp", cddpto);
			sqls[55] = new SqlParameter("pciu_cli_fact", cfciud);
			sqls[56] = new SqlParameter("pciu_cli_desp", cdciu);
			sqls[57] = new SqlParameter("pusuario", nom);
			sqls[58] = new SqlParameter("pobra", obra);

			string conexion = BdDatos.conexionScope();

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_salida_facturacion", con))
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

		public SqlDataReader obtenerCuotas(int fup_id, string ver)
		{
			string sql;
			sql = "SELECT cuo_num,cuo_fup_id " +
				  "FROM cuota " +
				  "WHERE (cuo_fup_id = " + fup_id + ") AND (cuo_version = '" + ver + "') ";
			return BdDatos.ConsultarConDataReader(sql);
		}

		//OBTENER CANTIDAD DE CUOTAS
		public SqlDataReader obtenerCantCuotas(int fup_id, string ver, int parte)
		{
			string sql;
			sql = "SELECT SUM(cuo_num) " +
				  "FROM cuota " +
				  "WHERE (cuo_fup_id = " + fup_id + ") AND (cuo_version = '" + ver + "') AND " +
				  "cuo_parte = " + parte + "";
			return BdDatos.ConsultarConDataReader(sql);
		}

		public SqlDataReader ConsultarNombrePais(int pais)
		{
			string sql;
			sql = "SELECT  pai_nombre  FROM  pais  WHERE  (pai_id = " + pais + ")";
			return BdDatos.ConsultarConDataReader(sql);
		}

		public SqlDataReader ConsultarCuota(int fup, string ver, int cuota)
		{
			string sql;

			sql = "SELECT cuo_num, CAST(cuo_valor_pagar AS decimal(18, 2)) AS vlrPagar, cuo_porcentaje, " +
				"CAST(cuo_valor_pagado AS decimal(18, 2)) AS vlrPagado, cuo_fecha_real, cuo_estado, cuo_comentarios FROM cuota " +
				  "WHERE (cuo_fup_id = " + fup + ") AND (cuo_num = " + cuota + " ) " + 
				  "AND (cuo_version = '" + ver + "') ";

			return BdDatos.ConsultarConDataReader(sql);
		}


		//ingreso los datos al log del pv
		public int IngresarDatosLOGpv(int txtfup, int cboacc_id, int txtcantidad, string txtprecio, string precio_total, string tipo,
		  string lusuconect)
		{

			string sql;
			string fecha_ing = System.DateTime.Now.ToString("dd/MM/yyyy");
			string hora_crea = System.DateTime.Now.ToShortTimeString();

			sql = "INSERT INTO LOG_pv (logpv_fup_id, logpv_id_acc, logpv_cantidad, logpv_precio_unitario, logpv_precio_total, logpv_tipo, usu_crea, fecha_crea, hora_crea) " +
				  "VALUES (" + txtfup + "," + cboacc_id + "," + txtcantidad + ",'" + txtprecio.Replace(",", ".") + "','" + precio_total.Replace(",", ".") +
					   "','" + tipo + "','" + lusuconect + "','" + fecha_ing + "','" + hora_crea + "') ";

			return BdDatos.ejecutarSql(sql);
		}

		//INGRESO DE PEDIDO DE VENTA
		public int IngPV(int fup, string usu, string clifact, string clidesp, string tipocli, string fecent,
			string condpago, string motivo, string co, int numdia, string numpv, string desctipo, 
			string paiclifac, string paiclidesp, string dptoclifac, string dptoclidesp, string ciuclifac,
			string ciuclidesp, int planta, int parte, string idClienteInterno, string plantaProduc
			, string obra_desp, string dir_documentos, string vendedor
			)
		{
			// Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
			int Id = -1;


			// Parametros de la BBDD
			SqlParameter[] sqls = new SqlParameter[28];
			sqls[0] = new SqlParameter("pFupID ", fup);
			sqls[1] = new SqlParameter("pusuario", usu);
			sqls[2] = new SqlParameter("pcotizacion", fup);
			sqls[3] = new SqlParameter("ptipo_solicitud", "502");
			sqls[4] = new SqlParameter("pcli_fact", clifact);
			sqls[5] = new SqlParameter("pcli_despacho", clidesp);
			sqls[6] = new SqlParameter("pcli_tipo", tipocli);
			sqls[7] = new SqlParameter("pfecha_entrega", fecent);
			sqls[8] = new SqlParameter("pcli_cond_pago", condpago);
			sqls[9] = new SqlParameter("pmotivo", motivo);
			sqls[10] = new SqlParameter("pcentro_operacion", co);
			sqls[11] = new SqlParameter("pnum_dias", numdia);
			sqls[12] = new SqlParameter("pnumero", numpv);
			sqls[13] = new SqlParameter("punidad_negocio", "002");          
			sqls[14] = new SqlParameter("psolicitud", desctipo);
			sqls[15] = new SqlParameter("ppais_cli_fact", paiclifac);
			sqls[16] = new SqlParameter("ppais_cli_desp", paiclidesp);
			sqls[17] = new SqlParameter("pdpto_cli_fact", dptoclifac);
			sqls[18] = new SqlParameter("pdpto_cli_desp", dptoclidesp);
			sqls[19] = new SqlParameter("pciu_cli_fact", ciuclifac);
			sqls[20] = new SqlParameter("pciu_cli_desp", ciuclidesp);
			sqls[21] = new SqlParameter("pplantafact", planta);
			sqls[22] = new SqlParameter("pparte", parte);
			sqls[23] = new SqlParameter("pcliente_interior_id", idClienteInterno);
			sqls[24] = new SqlParameter("pplanta", plantaProduc);
			sqls[25] = new SqlParameter("pdirdespacho", obra_desp);
			sqls[26] = new SqlParameter("pdirdocumentos", dir_documentos);
            sqls[27] = new SqlParameter("pvendedor", vendedor);

            string conexion = BdDatos.conexionScope();

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_pedido_venta_V2", con))
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

		//INGRESO DE SOLICITUD DE FACTURACIÓN POR PARTE
		public int IngSFParte(int fup, string ver, int parte, string usuario, string cliente, string clifact,
			string obra, string dirofi, string asecom, int termnon, string formpago, string vrVenta, 
			string vrTransporte, string vrIVA, string vrTotal, string vrComercial, string condpago, 
			string instrumento, string coment, string dirobra, string porc, string vrDesc, string razdesc,
			string clidesp, decimal m2, string dirEnvio, int tipoflete, int tipofactflete, string subtotal, 
			int pv_id, int tipoSf, int Compania, int PlantaProd, string ObservaFactura            )
		{
			// Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
			int Id = -1;


			// Parametros de la BBDD
			SqlParameter[] sqls = new SqlParameter[38];
			sqls[0] = new SqlParameter("pFupID ", fup);
			sqls[1] = new SqlParameter("pversion", ver);
			sqls[2] = new SqlParameter("pparte", parte);
			sqls[3] = new SqlParameter("pusuario", usuario);
			sqls[4] = new SqlParameter("pcliente", cliente);
			sqls[5] = new SqlParameter("pcliente_facturar", clifact);
			sqls[6] = new SqlParameter("pobra", obra);
			sqls[7] = new SqlParameter("pdirector_oficina", dirofi);
			sqls[8] = new SqlParameter("pasesor_comercial", asecom);
			sqls[9] = new SqlParameter("pcotizacion", fup);
			sqls[10] = new SqlParameter("psolicitud_No", " ");
			sqls[11] = new SqlParameter("ptermino_negociacion", termnon);
			sqls[12] = new SqlParameter("pcondicion_pago", condpago);
			sqls[13] = new SqlParameter("pvlr_venta", vrVenta);
			sqls[14] = new SqlParameter("ptransporte", vrTransporte);
			sqls[15] = new SqlParameter("piva", vrIVA);
			sqls[16] = new SqlParameter("ptltiva", vrTotal);
			sqls[17] = new SqlParameter("pvlr_comercial", vrComercial);
			sqls[18] = new SqlParameter("pforma_pago", formpago);
			sqls[19] = new SqlParameter("pinstrumento", instrumento);
			sqls[20] = new SqlParameter("pcomentarios", coment);
			sqls[21] = new SqlParameter("pdir_obra_desp", dirobra);
			sqls[22] = new SqlParameter("pproducido", "");
			sqls[23] = new SqlParameter("pnum", "");
			sqls[24] = new SqlParameter("pporcdesc", porc);
			sqls[25] = new SqlParameter("pvlr_dscto", vrDesc);
			sqls[26] = new SqlParameter("prazondesc", razdesc);
			sqls[27] = new SqlParameter("pcliente_despacho", clidesp);
			sqls[28] = new SqlParameter("pm2", m2);
			sqls[29] = new SqlParameter("pdir_documentos", dirEnvio);
			sqls[30] = new SqlParameter("ptipoflete", tipoflete);
			sqls[31] = new SqlParameter("ptipofactflete", tipofactflete);
			sqls[32] = new SqlParameter("psubtotal", subtotal);
			sqls[33] = new SqlParameter("ppv_id", pv_id);
			sqls[34] = new SqlParameter("ptipo_sf", tipoSf);
			sqls[35] = new SqlParameter("pfac_compania", Compania);
			sqls[36] = new SqlParameter("pPlantaProd", PlantaProd);
            sqls[37] = new SqlParameter("pObservaFactura", ObservaFactura);

            string conexion = BdDatos.conexionScope();

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_solicitud_facturacion_V2", con))
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

		//CONSULTA DE GRILLA PARTE
		public DataSet ConsultarGrillaParte(int fup, string ver)
		{
			string sql;
			sql = "SELECT Sf_num, planta_forsa.planta_descripcion as planta,Sf_parte, CONVERT(decimal(18,2),sf_m2) sf_m2, " +
				"CONVERT(VarChar(50),cast(Sf_vlr_venta as money), 1) Sf_vlr_venta, " +
				"CONVERT(VarChar(50),cast(Sf_vlr_comercial as money), 1) Sf_vlr_comercial, " +
				"CONVERT(VarChar(50),cast(Sf_porcdesc as money), 1) Sf_porcdesc, " +
				"CONVERT(VarChar(50),cast(Sf_vlr_dscto as money), 1) Sf_vlr_dscto, " +
				"CONVERT(VarChar(50),cast(Sf_transporte as money), 1) Sf_transporte, " +
				"CONVERT(VarChar(50),cast(Sf_iva as money), 1) Sf_iva, " +
				"CONVERT(VarChar(50),cast(Sf_tltiva as money), 1) Sf_tltiva ,"+
				"solicitud_fact_tipo.sf_tipo_descripcion as TipoSf " + 
				"FROM            solicitud_facturacion INNER JOIN "+
				"         pedido_venta ON solicitud_facturacion.pv_id = pedido_venta.pv_id INNER JOIN "+
				"         planta_forsa ON pedido_venta.planta_id = planta_forsa.planta_id INNER JOIN "+
				"         solicitud_fact_tipo ON solicitud_facturacion.sf_tipo_id = solicitud_fact_tipo.sf_tipo_id "+ 
				"WHERE (Sf_fup_id = " + fup + ") AND (Sf_version = '" + ver + "') ORDER BY planta_forsa.planta_id";
			DataSet ds = BdDatos.consultarConDataset(sql);
			// DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
			return ds;
		}

		//CONSULTA DE SUMA DE TOTALES
		public SqlDataReader ConsultarTotales(int fup, string ver)
		{
			string sql;

			sql = "SELECT CONVERT(decimal(18,2),ISNULL(SUM(sf.sf_m2),0)) M2_TOTAL " +
				"	 , CONVERT(decimal(18,2),ISNULL(AVG(CONVERT(decimal(18,2),cc.cieseg_m2_cerrados)),0)) M2_CIERRE " +
				"	 , CONVERT(decimal(18,2),ISNULL(SUM(Sf_tltiva),0)) VALOR_TOTAL  " +
				"	 , CONVERT(decimal(18,2),ISNULL(AVG(cierre_valor_final),0)) VALOR_CIERRE " +
				"	 , CONVERT(decimal(18,2),ISNULL(SUM(Sf_vlr_venta),0)) VALOR_VENTA " +
				"	 , CONVERT(decimal(18,2),ISNULL(AVG(CONVERT(decimal(18,2),cc.cieseg_m2_modulados)),0)) M2_Modulados " +
				"	 , CASE WHEN SUM(cieseg_m2_modulados) > ISNULL(AVG(CONVERT(decimal(18, 2), cc.cieseg_m2_cerrados)), 0) "+
				"     THEN SUM(cieseg_m2_modulados) ELSE ISNULL(AVG(CONVERT(decimal(18, 2), cc.cieseg_m2_cerrados)), 0)  END AS M2Actual  " +
				" FROM solicitud_facturacion sf INNER JOIN cierre_comercial cc  " +
				"  ON sf.Sf_fup_id = cc.cieseg_fup_id AND sf.Sf_version = cc.cierre_version  " +
				"WHERE (Sf_fup_id = " + fup + ") AND (Sf_version = '" + ver + "')";

			return BdDatos.ConsultarConDataReader(sql);
		}

		//CONSULTA DE SUMA DE TOTALES PEDIDO DE 
		public SqlDataReader ConsultarTotalesPV(int fup, string ver)
		{
			string sql;

			//sql = "SELECT 1 as M2_TOTAL " +
			//", 1 as M2_CIERRE " +
			//", CONVERT(decimal(18,2),ISNULL(SUM(Sf_tltiva),0)) VALOR_TOTAL " +
			//", CONVERT(decimal(18,2),ISNULL(AVG(cc.cot_acc_precio_total),0)) VALOR_CIERRE " +
			//", CONVERT(decimal(18,2),ISNULL(SUM(Sf_vlr_venta),0)) VALOR_VENTA " +
			//", 1 as M2_Modulados " +
			//", 1 as M2Actual " +
			//"FROM solicitud_facturacion sf INNER JOIN  (SELECT cot_acc_fup_id, " +
			//"SUM(cot_acc_precio_total) cot_acc_precio_total, SUM(cot_cantidad) cot_cantidad " +
			//"FROM cotizacion_accesorios WHERE cot_acc_activo = 1 and cot_acc_fup_id = " + fup + " " +
			//"GROUP BY cot_acc_fup_id) cc ON sf.Sf_fup_id = cc.cot_acc_fup_id " +
			//"WHERE (Sf_fup_id = " + fup + ") AND (Sf_version = '" + ver + "')";

			sql = "SELECT CONVERT(decimal(18,2),ISNULL(SUM(sf.sf_m2),0)) M2_TOTAL " +
		   ", CONVERT(decimal(18,2),ISNULL(AVG(CONVERT(decimal(18,2),cc.cot_cantidad)),0)) M2_CIERRE " +
		   ", CONVERT(decimal(18,2),ISNULL(SUM(Sf_tltiva),0)) VALOR_TOTAL " +
		   ", CONVERT(decimal(18,2),ISNULL(AVG(cc.cot_acc_precio_total),0)) VALOR_CIERRE " +
		   ", CONVERT(decimal(18,2),ISNULL(SUM(Sf_vlr_venta),0)) VALOR_VENTA " +
		   ", CONVERT(decimal(18,2),ISNULL(SUM(cc.cot_cantidad),0)) M2_Modulados " +
		   ", CONVERT(decimal(18,2),ISNULL(SUM(cc.cot_cantidad),0)) M2Actual " +
		   "FROM solicitud_facturacion sf INNER JOIN  (SELECT cot_acc_fup_id, " +
		   "SUM(cot_acc_precio_total) cot_acc_precio_total, SUM(cot_cantidad) cot_cantidad " +
		   "FROM cotizacion_accesorios WHERE cot_acc_activo = 1 and cot_acc_fup_id = " + fup + " " +
		   "GROUP BY cot_acc_fup_id) cc ON sf.Sf_fup_id = cc.cot_acc_fup_id " +
		   "WHERE (Sf_fup_id = " + fup + ") AND (Sf_version = '" + ver + "')";

			return BdDatos.ConsultarConDataReader(sql);
		}

		//INGRESO DE CUOTAS
		public int IngCuota(int fup, string ver, string usuario, int numcuota, string vpag, string vpagado, 
			string fecreal, string coment, string salpar, string porc, string und)
		{
			// Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
			int Id = -1;
			string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

			// Parametros de la BBDD
			SqlParameter[] sqls = new SqlParameter[14];
			sqls[0] = new SqlParameter("pFupID ", fup);
			sqls[1] = new SqlParameter("pVersion", ver);
			sqls[2] = new SqlParameter("pusuario", usuario);
			sqls[3] = new SqlParameter("num", numcuota);
			sqls[4] = new SqlParameter("valor_pagar", vpag);
			sqls[5] = new SqlParameter("valor_pagado", vpagado);
			sqls[6] = new SqlParameter("fecha_esperada", fecreal);
			sqls[7] = new SqlParameter("fecha_real", fecha);
			sqls[8] = new SqlParameter("estado", "Debe");
			sqls[9] = new SqlParameter("comentarios", coment);
			sqls[10] = new SqlParameter("saldo_parcial", salpar);
			sqls[11] = new SqlParameter("tasa_cambio", "0");
			sqls[12] = new SqlParameter("porcentaje", porc);
			sqls[13] = new SqlParameter("unm_id", und);

			string conexion = BdDatos.conexionScope();

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_cuotas_sf", con))
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

		//INGRESO DE CUOTAS
		public int InsertarListaOfac(string cliente, string fecha, string usuario, string estado, string clientenombre)
		{
			// Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
			int Id = -1;

			// Parametros de la BBDD
			SqlParameter[] sqls = new SqlParameter[5];
			sqls[0] = new SqlParameter("cliente", cliente);
			sqls[1] = new SqlParameter("fecharevision", fecha);
			sqls[2] = new SqlParameter("usuario", usuario);
			sqls[3] = new SqlParameter("estado", estado);
			sqls[4] = new SqlParameter("nombrecliente", clientenombre); 

			string conexion = BdDatos.conexionScope();

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand("USP_lista_ofac", con))
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


		//MÁXIMOO CUOTAS
		public SqlDataReader MaximoCuota(int FUP, string ver)
		{
			string sql;

			sql = "SELECT ISNULL(MAX(cuo_num),0) FROM cuota " +
				  "WHERE cuo_fup_id = " + FUP + " AND cuo_version = '" + ver + "' ";

			return BdDatos.ConsultarConDataReader(sql);
		}

		//INGRESO DE RECHAZO
		public int IngRechazo(int fup, string ver, string usuario, int parte, bool razsoc, bool bnit, 
			bool bdirec, bool btele, bool bcondpago, bool btdn, bool bvrcom, string obs)
		{
			int raz_soc = 0, nit = 0, direc = 0, tele = 0, condpago = 0, tdn = 0, vrcom = 0;

			if(razsoc == true)
			{
				raz_soc = 1;
			}
			if(bnit == true)
			{
				nit = 1;
			}
			if(bdirec == true)
			{
				direc = 1;
			}
			if(btele == true)
			{
				tele = 1;
			}
			if(bcondpago == true)
			{
				condpago = 1;
			}
			if(btdn == true)
			{
				tdn = 1;
			}
			if(bvrcom == true)
			{
				vrcom = 1;
			}
			// Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
			int Id = -1;
			string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

			// Parametros de la BBDD
			SqlParameter[] sqls = new SqlParameter[12];
			sqls[0] = new SqlParameter("pFupID ", fup);
			sqls[1] = new SqlParameter("pVersion", ver);
			sqls[2] = new SqlParameter("pUsuario", usuario);
			sqls[3] = new SqlParameter("pparte", parte);
			sqls[4] = new SqlParameter("prazon_social", raz_soc);
			sqls[5] = new SqlParameter("pnit", nit);
			sqls[6] = new SqlParameter("pdireccion", direc);
			sqls[7] = new SqlParameter("ptelefono", tele);
			sqls[8] = new SqlParameter("pcond_pago", condpago);
			sqls[9] = new SqlParameter("ptdn", tdn);
			sqls[10] = new SqlParameter("pvlr_comercial", vrcom);
			sqls[11] = new SqlParameter("pobservaciones", obs);

			string conexion = BdDatos.conexionScope();

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand("USP_fup_UPD_rechazos_sf", con))
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

		//CONSULTA GRILLA DE RECHAZO
		public DataSet GrillaRechazo(int fup_id, string ver)
		{
			string sql;

			sql = "SELECT sf_rech_parte [Parte], sf_rech_razon_social [Razón Social] , sf_rech_nit [NIT], " + 
				"sf_rech_direccion [Dirección], sf_rech_telefono [Telefono], sf_rech_cond_pago [Cond Pago], " +
				"sf_rech_tdn [TDN], sf_rech_vlr_comercial [Valor Comercial], sf_rech_observaciones [Observaciones], " +
				"CASE WHEN o.ano IS NULL THEN 'PENDIENTE' ELSE CONVERT(varchar,o.Numero) +'-'+ CONVERT(varchar,o.ano) END [OFA] " +
				"FROM solicitud_fact_rechazo r LEFT OUTER JOIN Orden o on r.sf_rech_fup = o.Yale_Cotiza and r.sf_rech_version = o.ord_version " +
				"and r.sf_rech_parte = o.ord_parte " +
				"WHERE (sf_rech_fup = " + fup_id + ") AND (sf_rech_version = '" + ver + "') ";

			return BdDatos.consultarConDataset(sql);
		}

		//ingreso los datos al log del pv
		public int IngresarDatosLOGpv(int txtfup, int cboacc_id, int txtcantidad, string txtprecio, string precio_total, string tipo,
		string lusuconect, string observacion, int item_tipo, int item_modelo, string parametros)
		{
			string sql;

			string FechaCrea = DateTime.Now.Date.ToString();
			FechaCrea = FechaCrea.Substring(0, 10);
			string Hora = DateTime.Now.ToShortTimeString();

			sql = "INSERT INTO LOG_pv (logpv_fup_id, logpv_id_acc, logpv_cantidad, logpv_precio_unitario, logpv_precio_total, logpv_tipo, usu_crea, fecha_crea, hora_crea2, logpv_descripcion, parametros, tipo, modelo) " +
				  "VALUES (" + txtfup + "," + cboacc_id + "," + txtcantidad + ",'" + txtprecio.Replace(",", ".") + "','" + precio_total.Replace(",", ".") +
					   "','" + tipo + "','" + lusuconect + "','" + FechaCrea + "','" + Hora + "', '" + observacion + "', '" + parametros + "', " + item_tipo + ", " + item_modelo + ") ";

			return BdDatos.ejecutarSql(sql);
		}

		private string LimitLength(string source, int maxLength)
		{
			if (source.Length <= maxLength)
			{
				return source;
			}

			return source.Substring(0, maxLength);
		}


		public void enviarCorreo(int evento, int item_planta_id, string usuario, string remitente, out string mensaje, string emailr)
		{
			//int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
			// Parametros de la BBDD
			SqlParameter[] sqls = new SqlParameter[4];
			sqls[0] = new SqlParameter("@pEvento ", evento);
			sqls[1] = new SqlParameter("@pItemPlantaID", item_planta_id);
			sqls[2] = new SqlParameter("@pUsuario", usuario);
			sqls[3] = new SqlParameter("@pRemitente", emailr);

			string conexion = BdDatos.conexionScope();

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand("USP_Item_notificaciones", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddRange(sqls);

					SqlParameter Asunto = new SqlParameter("@pAsun_mail", SqlDbType.VarChar, 200);
					SqlParameter Destinatarios = new SqlParameter("@pLista", SqlDbType.VarChar, 12500);
					SqlParameter Mensaje = new SqlParameter("@pMsg", SqlDbType.VarChar, 12500);
					SqlParameter Anexo = new SqlParameter("@pAnexo", SqlDbType.Bit);
					SqlParameter LinkAnexo1 = new SqlParameter("@pLink_anexo1", SqlDbType.VarChar, 300);
					SqlParameter LinkAnexo2 = new SqlParameter("@pLink_anexo2", SqlDbType.VarChar, 300);

					Asunto.Direction = ParameterDirection.Output;
					Destinatarios.Direction = ParameterDirection.Output;
					Mensaje.Direction = ParameterDirection.Output;
					Anexo.Direction = ParameterDirection.Output;
					LinkAnexo1.Direction = ParameterDirection.Output;
					LinkAnexo2.Direction = ParameterDirection.Output;

					cmd.Parameters.Add(Asunto);
					cmd.Parameters.Add(Destinatarios);
					cmd.Parameters.Add(Mensaje);
					cmd.Parameters.Add(Anexo);
					cmd.Parameters.Add(LinkAnexo1);
					cmd.Parameters.Add(LinkAnexo2);

					// Abrimos la conexión y ejecutamos el ExecuteReader
					con.Open();
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						//VALORES DEL ENCABEZADO 
						string AsuntoMail = Convert.ToString(Asunto.Value);
						string DestinatariosMail = Convert.ToString(Destinatarios.Value);
						string MensajeMail = Convert.ToString(Mensaje.Value);
						bool llevaAnexo = Convert.ToBoolean(Anexo.Value);
						string EnlaceAnexo1 = Convert.ToString(LinkAnexo1.Value);
						string EnlaceAnexo2 = Convert.ToString(LinkAnexo2.Value);

						WebClient clienteWeb = new WebClient();
						clienteWeb.Dispose();
						clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "forsa", "FORSA");
						// Adjunto
						//DEFINIMOS LA CLASE DE MAILMESSAGE
						MailMessage mail = new MailMessage();
						//INDICAMOS EL EMAIL DE ORIGEN
						MailAddress emailremite = new MailAddress(remitente);
						mail.From = emailremite;
						//AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
						mail.To.Add(DestinatariosMail);
						//INCLUIMOS EL ASUNTO DEL MENSAJE
						mail.Subject = AsuntoMail;
						//AÑADIMOS EL CUERPO DEL MENSAJE
						mail.Body = MensajeMail;
						//INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
						mail.BodyEncoding = System.Text.Encoding.UTF8;
						//DEFINIMOS LA PRIORIDAD DEL MENSAJE
						mail.Priority = System.Net.Mail.MailPriority.Normal;
						//INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
						mail.IsBodyHtml = true;
						//List<Byte[]> listCorreo = new List<Byte[]>();
						MemoryStream ms = new MemoryStream();
						//ADJUNTAMOS EL ARCHIVO                        
						//if (llevaAnexo)
						//{
						//    //string enlace1 = "", enlace2 = "";
						//    //string fecha = System.DateTime.Today.ToLongDateString();

						//    //if (!String.IsNullOrEmpty(EnlaceAnexo1.ToString()))
						//    //    enlace1 = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo1.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + fup + "&nombrer=" + nombrer + "&paisr=" + paisr + "&correor=" + emailr + "&fecha=" + fecha;
						//    //if (!String.IsNullOrEmpty(EnlaceAnexo2.ToString()))
						//    //    enlace2 = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo2.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + fup + "&version=A &parte=1" + "&sf_id=" + idSF;


						//    Byte[] correo = new Byte[0];

						//    if (!String.IsNullOrEmpty(enlace1))
						//    {
						//        correo = clienteWeb.DownloadData(enlace1);
						//        listCorreo.Add(correo);
						//    }
						//    if (!String.IsNullOrEmpty(enlace2))
						//    {
						//        correo = (clienteWeb.DownloadData(enlace2));
						//        listCorreo.Add(correo);
						//    }

						//    //for (int i = 0; i < listCorreo.Count(); i++)
						//    //{
						//    //    ms = new MemoryStream(listCorreo[i]);
						//    //    mail.Attachments.Add(new Attachment(ms, "FUP " + fup + i + ".pdf"));
						//    //}
						//}
						//DEFINIMOS NUESTRO SERVIDOR SMTP
						//DECLARAMOS LA CLASE SMTPCLIENT
						SmtpClient smtp = new SmtpClient();
						//DEFINIMOS NUESTRO SERVIDOR SMTP
						smtp.Host = "smtp.office365.com";
						//INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
						smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
						smtp.Port = 25;
						smtp.EnableSsl = true;

						ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain,
							SslPolicyErrors sslPolicyErrors)
						{
							return true;
						};
						try
						{
							smtp.Send(mail);
							mensaje = "";
							//listCorreo.Clear();
						}
						catch (Exception ex)
						{
							mensaje = "ERROR: " + ex.Message;
						}
						ms.Close();
					}
				}
			}
		}
	}
}
