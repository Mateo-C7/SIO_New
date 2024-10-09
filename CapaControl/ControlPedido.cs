using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CapaDatos;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace CapaControl
{
    public class ControlPedido
    {
        public int CerrarConexion()
        {
            return BdDatos.desconectar();            
        }

        //CONSULTA IDIOMA 
        public DataSet ConsultarIdiomaPedido()
        {
            string sql;
            sql = "SELECT idioma_pedido_venta.* FROM idioma_pedido_venta";
            DataSet ds_idiomaCliente = BdDatos.consultarConDataset(sql);
            return ds_idiomaCliente;
        }

        public SqlDataReader ConsultarNumeroPedidoVenta(string NumPV)
        {
            string sql;
            sql = "SELECT Orden.Yale_Cotiza, Orden.Anulada, Id_Ofa FROM Orden WHERE (Orden.Ofa = '" + NumPV + "') AND (Tipo_Of = 'PV' OR Tipo_Of = 'PC') ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarNumeroPedidoVentaConFUP(int FUP)
        {
            string sql;
            sql = "SELECT  RTRIM(Num_Of) + '-' + RTRIM(Ano_Of) AS Ofa FROM Orden_Seg " +
                "WHERE ( Orden_Seg.fup  = " + FUP + ") AND ( Orden_Seg.Tipo_Of = 'PV' OR Orden_Seg.Tipo_Of = 'PC')";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarIdRecompra(string IdRecompra)
        {
            string sql;            

            sql = "SELECT fup_hvp_Observaciones.hvpo_id AS IdRecompra, fup_hvp_Observaciones.hvpo_UsuaActualiza + ' - ' + CONVERT(varchar(10), fup_hvp_Observaciones.hvpo_FecActualiza, 103) " +
                  " + ' : ' + fup_hvp_Observaciones.hvpo_Comentario AS Recompra  FROM fup_hvp_Observaciones INNER JOIN " +
                  " fup_enc_entrada_cotizacion ON fup_hvp_Observaciones.hvpo_enc_entrada_cot_id = fup_enc_entrada_cotizacion.eect_id " +
                  " WHERE (fup_hvp_Observaciones.hvpo_TipoEntrada = 2) AND(fup_hvp_Observaciones.hvpo_id =" + IdRecompra + ")";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarOrdenAsociada(string orden)
        {
            string sql;
            sql = "SELECT     TOP (1) Numero + '-' + ano AS orden, Id_Ofa " +
                  "FROM         Orden " +
                  "WHERE     (Numero + '-' + ano ='" + @orden + "')";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarFUP(int FUP)
        {
            string sql;
            sql = "SELECT fup_id, fup_cli_id, fup_ccl_id, fup_obr_id, fup_usu_crea, cli_nombre, obr_nombre, pai_id, pai_nombre, "+
                  " ciu_zona, fup_ch_accesorios, ciu_nombre, " +
                    " ciu_id, ccl_nombre + ' ' + contacto_cliente.ccl_nombre2 + ' ' + contacto_cliente.ccl_apellido + ' ' + contacto_cliente.ccl_apellido2 AS Contacto,  " +
                    " formato_unico.planta_id, pf.planta_descripcion,  " +
                    " cast (ISNULL(fup_hvp_Observaciones.hvpo_id, '') as varchar(10)) AS IdRecompra, ISNULL(fup_hvp_Observaciones.hvpo_UsuaActualiza + ' - ' + CONVERT(varchar(10), fup_hvp_Observaciones.hvpo_FecActualiza, 103) " +
                    "     + ' : ' + fup_hvp_Observaciones.hvpo_Comentario, '') AS Recompra  " +
                    " FROM formato_unico INNER JOIN " +
                        "                       cliente ON formato_unico.fup_cli_id = cliente.cli_id INNER JOIN " +
                        "                      obra ON formato_unico.fup_obr_id = obra.obr_id INNER JOIN " +
                        "                     pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN " +
                        "                     ciudad ON cliente.cli_ciu_id = ciudad.ciu_id INNER JOIN " +
                        "                     contacto_cliente ON formato_unico.fup_ccl_id = contacto_cliente.ccl_id INNER JOIN " +
                        "                     planta_forsa AS pf ON pf.planta_id = formato_unico.planta_id LEFT OUTER JOIN " +
                        "                     fup_hvp_Observaciones ON formato_unico.IdRecompra = fup_hvp_Observaciones.hvpo_id " +
                      " WHERE fup_id =" +FUP+";";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarNumOP(int FUP)
        {
            string sql;
            sql = "SELECT OS.ND_1EE FROM Orden O INNER JOIN Orden_Seg OS ON OS.Id_Ofa = O.Id_Ofa " +
                "WHERE (O.Yale_Cotiza = " + FUP + ") AND (O.Tipo_Of = 'PV' OR Tipo_Of = 'PC')";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarContenidoPvFUP(int FUP)
        {
            string sql;
            //sql = "SELECT isnull(pv_contenido, 0)  from pedido_venta " +
            //    "WHERE (pv_fup_id = " + FUP + ") ";
            sql = "SELECT SeguimientoPV.pv_contenido_id, SeguimientoPV.log_almacen, SeguimientoPV.log_accesorios FROM     pv_contenido INNER JOIN " +
                  "SeguimientoPV ON pv_contenido.pv_contenido_id = SeguimientoPV.pv_contenido_id " +
                   "WHERE  (pv_contenido.activo = 1) AND (SeguimientoPV.fup = "+FUP+");";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarPvEspecial(int FUP)
        {
            string sql;
            //sql = "SELECT isnull(pv_contenido, 0)  from pedido_venta " +
            //    "WHERE (pv_fup_id = " + FUP + ") ";
            sql = "SELECT        CASE WHEN SUM(CAST(item_planta.especial AS int)) > 0 THEN 'ESPECIAL' ELSE 'ESTANDAR' END AS tipo, COUNT(item_planta.especial) AS todos, SUM(CAST(item_planta.especial AS int)) AS especiales,  "+
                     "    SUM(CAST(CASE WHEN cotizacion_accesorios.estado_item = 1 AND item_planta.especial = 1 THEN 1 ELSE 0 END AS int)) AS completados, CASE WHEN SUM(CAST(item_planta.especial AS int))  "+
                     "    - SUM(CAST(cotizacion_accesorios.estado_item AS int)) > 0 THEN 'PEDIDO PARADO Falta Informacion de ' + CAST(SUM(CAST(item_planta.especial AS int)) "+
                     "    - SUM(CASE WHEN cotizacion_accesorios.estado_item = 1 AND item_planta.especial = 1 THEN 1 ELSE 0 END) AS varchar(MAX)) + ' Items ' ELSE '' END AS mensaje, SUM(CAST(item_planta.especial AS int)) " +
                     "    - SUM(CAST(CASE WHEN cotizacion_accesorios.estado_item = 1 AND item_planta.especial = 1 THEN 1 ELSE 0 END AS int)) AS pendientes "+
                  "  FROM            cotizacion_accesorios INNER JOIN "+
                  "       formato_unico ON cotizacion_accesorios.cot_acc_fup_id = formato_unico.fup_id INNER JOIN "+
                  "       item_planta ON cotizacion_accesorios.cot_acc_id_acc = item_planta.cod_erp AND formato_unico.planta_id = item_planta.planta_id "+
                  "  WHERE        (cotizacion_accesorios.cot_acc_fup_id = " + FUP + ") AND (cotizacion_accesorios.cot_acc_activo = 1) " +
                  "  GROUP BY formato_unico.fup_id;";
            return BdDatos.ConsultarConDataReader(sql);
        }


        public SqlDataReader ValidarPaisRepresentante(int pais, int rep)
        {
            string sql;
            sql = "SELECT rc.rc_id FROM representantes_comerciales rc INNER JOIN pais_representante pr " + 
                "ON pr.pr_id_representante = rc.rc_id " +
                  "WHERE pr.pr_id_pais = " + pais + " AND pr.pr_id_representante = " + rep + " ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTO SI SE CONFIRMO LA VENTA, VERIFICANDO SI ESTA EN LA TABLA ORDEN
        public SqlDataReader ConsultarConfirmacionVenta(int FUP)
        {
            string sql;
            //sql = "SELECT     isnull(Orden.Yale_Cotiza, 0),isnull(pedido_venta.pv_confirma_com, 0) "+
            //      " FROM         Orden LEFT OUTER JOIN " +
            //      " pedido_venta ON Orden.Yale_Cotiza = pedido_venta.pv_fup_id WHERE (Orden.Yale_Cotiza =" + FUP + ") ";
            sql = "SELECT seguimientopv_id FROM     SeguimientoPV WHERE  (fup = "+FUP+");";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarValidacionAlmacenLogistica(int FUP)
        {
            string sql;
            sql = "SELECT isnull(PV.pv_entrega_almacen,0), isnull(PV.pv_logistica,0) FROM pedido_venta PV " + 
                "WHERE PV.pv_fup_id = " + FUP + " ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarLogisticaAlmAcc(int FUP)
        {
            string sql;
            //sql = "SELECT isnull(PV.pv_logistica_almacen,0),isnull( PV.pv_logistica_accesorios,0) FROM pedido_venta PV " +
            //    "WHERE PV.pv_fup_id = " + FUP + " ";
            sql = "SELECT SeguimientoPV.pv_contenido_id FROM     pv_contenido INNER JOIN " +
                  "SeguimientoPV ON pv_contenido.pv_contenido_id = SeguimientoPV.pv_contenido_id " + 
                  "WHERE  (pv_contenido.activo = 1) AND (SeguimientoPV.fup ="+FUP+");";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //public SqlDataReader ConsultarValidacionAlmacen(int FUP)
        //{
        //    string sql;
        //    //sql = "SELECT isnull(PV.pv_entrega_almacen,0), isnull(pv_logistica_almacen,0) FROM pedido_venta PV " +
        //    //    "WHERE PV.pv_fup_id = " + FUP + " ";
            
        //    return BdDatos.ConsultarConDataReader(sql);
        //}

        public SqlDataReader ConsultarValidacionAccesorios(int FUP)
        {
            string sql;
            sql = "SELECT isnull(PV.pv_entrega_accesorios,0), isnull(pv_logistica_accesorios,0) FROM pedido_venta PV " +
                "WHERE PV.pv_fup_id = " + FUP + " ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //VALIDACIÓN COMERCIAL
        public SqlDataReader ConsultarValidacionComercial(int FUP)
        {
            string sql;
            sql = "SELECT isnull(PV.pv_confirma_com,0) FROM pedido_venta PV " +
                "WHERE PV.pv_fup_id = " + FUP + " ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //TRAEMOS LOS DATOS DE FLETE DE LA TABLA PV
        public SqlDataReader ConsultarFleteTotal(int FUP)
        {
            string sql;
            sql = "SELECT  isnull(fup_total_flete,0)  FROM   formato_unico WHERE fup_id = "+ FUP ;
            return BdDatos.ConsultarConDataReader(sql);
        }

        //BUSQUEDA ACCESORIO POR NOMBRE ESPAÑOL
        public SqlDataReader ConsultarAccesorioNomEsp(string nombre, int planta, int idioma)
        {
            string sql;

            //sql = "SELECT     Accesorios_Genericos.Acc_Gen_Id, Accesorios_Genericos.Acc_Gen_Nom_Esp "+
            //       " FROM         Accesorios_Genericos INNER JOIN "+
            //        "Accesorio_Detalle ON Accesorios_Genericos.Acc_Gen_Id = Accesorio_Detalle.Acc_Det_GenId "+
            //        "WHERE     (Accesorios_Genericos.Acc_Gen_Inactivo = 0) "+
            //        "GROUP BY Accesorios_Genericos.Acc_Gen_Id, Accesorios_Genericos.Acc_Gen_Nom_Esp "+
            //        "HAVING      (Accesorios_Genericos.Acc_Gen_Nom_Esp LIKE '%" + nombre + "%') " +
            //        "ORDER BY Accesorios_Genericos.Acc_Gen_Nom_Esp";1

            //sql = "SELECT        item_planta.cod_erp, item_planta_idioma.descripcion AS Expr1, item_planta.activo, item_planta_idioma.activo AS Expr2, item_planta.planta_id, item_planta.item_grupo_planta_id, "+
            //             "item_planta_idioma.idioma_id             FROM            item_planta INNER JOIN "+
            //             "item_planta_idioma ON item_planta.item_planta_id = item_planta_idioma.item_planta_id INNER JOIN "+
            //             "item ON item_planta.item_id = item.item_id "+
            //             "WHERE        (item_planta.cod_erp IS NOT NULL) AND (item_planta_idioma.idioma_id = "+idioma+") AND (item_planta.activo = 1) AND (item.disp_comercial = 1) AND (item.activo = 1) AND (item_planta_idioma.activo = 1) AND "+
            //             "(item_planta_idioma.descripcion LIKE '%"+nombre+"%') and item_planta.planta_id = "+planta+";";

            sql = "SELECT item_planta.cod_erp, item_planta_idioma.descripcion AS Expr1, item_planta.activo, item_planta_idioma.activo AS Expr2, item_planta.planta_id, " +
                  "item_planta.item_grupo_planta_id, item_planta_idioma.idioma_id " +
                  "FROM     item_planta INNER JOIN " +
                  "item_planta_idioma ON item_planta.item_planta_id = item_planta_idioma.item_planta_id INNER JOIN " +
                  "item ON item_planta.item_id = item.item_id " +
                  "WHERE  (item_planta.cod_erp IS NOT NULL) AND (item_planta_idioma.idioma_id = "+idioma+") AND (item_planta.activo = 1) AND (item_planta_idioma.activo = 1) AND (item_planta.planta_id = "+planta+") " +
                  "AND (item_planta_idioma.descripcion LIKE '%" + nombre + "%') AND (item_planta.disp_comercial = 1) AND (item.activo = 1) order by item_planta_idioma.descripcion ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //BUSQUEDA ACCESORIO POR NOMBRE INGLES
        public SqlDataReader ConsultarAccesorioNomIng(string nombre)
        {
            string sql;

            sql = "SELECT acc_id, acc_desc_ing FROM accesorios " +
                  "WHERE (acc_desc_ing LIKE '%" + nombre + "%') AND (acc_activo = 1) AND (acc_pv = 1) " +
                  "ORDER BY acc_desc_ing ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //BUSQUEDA ACCESORIO POR NOMBRE PORTUGUES
        public SqlDataReader ConsultarAccesorioNomPort(string nombre)
        {
            string sql;

            sql = "SELECT acc_id, acc_desc_port FROM accesorios " +
                  "WHERE (acc_desc_port LIKE '%" + nombre + "%')AND (acc_activo = 1) AND (acc_pv = 1) " +
                  "ORDER BY acc_desc_port ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTO LOS GRUPOS DE LOS ACCESORIO 
        public SqlDataReader ConsultarGrupoAcc(int planta, int idioma)
        {
            string sql;

            //sql = "SELECT     Accesorios_Genericos.Acc_Gen_Id, Accesorios_Genericos.Acc_Gen_Nom_Esp "+
            //       " FROM         Accesorios_Genericos INNER JOIN "+
            //       " Accesorio_Detalle ON Accesorios_Genericos.Acc_Gen_Id = Accesorio_Detalle.Acc_Det_GenId "+
            //       " WHERE     (Accesorios_Genericos.Acc_Gen_Inactivo = 0) AND (Accesorio_Detalle.Acc_Det_Inactivo = 0) AND (Accesorio_Detalle.Acc_Det_Com = 1)  "+
            //       " GROUP BY Accesorios_Genericos.Acc_Gen_Id, Accesorios_Genericos.Acc_Gen_Nom_Esp " +
            //       " ORDER BY Accesorios_Genericos.Acc_Gen_Nom_Esp";

            //sql = "SELECT DISTINCT "+
            //      "item_grupo.descripcion, item_grupo.ruta_imagen, item_grupo.activo, item_grupo_planta_idioma.descripcion AS Expr1, item_grupo_planta_idioma.idioma_id, "+
            //      "item_grupo_planta.planta_id, item_grupo_planta.item_grupo_planta_id, item.activo AS Expr2, item_grupo_planta_idioma.activo AS Expr3 "+
            //      "FROM     item_grupo INNER JOIN " +
            //      "item_grupo_planta ON item_grupo.item_grupo_id = item_grupo_planta.item_grupo_id INNER JOIN " +
            //      "item_grupo_planta_idioma ON item_grupo_planta.item_grupo_planta_id = item_grupo_planta_idioma.item_grupo_planta_id INNER JOIN " +
            //      "item ON item_grupo.item_grupo_id = item.item_grupo_id " +
            //      "WHERE  (item_grupo_planta_idioma.idioma_id = "+idioma+") AND (item_grupo_planta.planta_id = "+planta+") AND (item_grupo_planta.activo = 1) AND (item.disp_comercial = 1) AND (item.activo = 1) AND " +
            //      "(item_grupo_planta_idioma.activo = 1) AND (item_grupo.activo = 1)";

            sql = "SELECT DISTINCT " +
                  "item_grupo.descripcion, item_grupo.ruta_imagen, item_grupo.activo, item_grupo_planta_idioma.descripcion AS Expr1, item_grupo_planta_idioma.idioma_id, " +
                  "item_grupo_planta.planta_id, item_grupo_planta.item_grupo_planta_id, item_grupo_planta_idioma.activo AS Expr3 " +
                  " FROM            item_grupo INNER JOIN " +
                  " item_grupo_planta ON item_grupo.item_grupo_id = item_grupo_planta.item_grupo_id INNER JOIN " +
                  "       item_grupo_planta_idioma ON item_grupo_planta.item_grupo_planta_id = item_grupo_planta_idioma.item_grupo_planta_id INNER JOIN " +
                  "      item ON item_grupo.item_grupo_id = item.item_grupo_id INNER JOIN " +
                  "       item_planta ON item.item_id = item_planta.item_id " +
                  "WHERE  (item_grupo_planta_idioma.idioma_id = "+idioma+") AND (item_grupo_planta.planta_id = "+planta+") AND (item_planta.activo = 1) AND (item_grupo_planta.activo = 1) AND " +
                  "(item_grupo_planta_idioma.activo = 1) AND (item_grupo.activo = 1) AND (item_planta.disp_comercial = 1) AND (item.activo = 1) order by item_grupo_planta_idioma.descripcion ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTO EL DETALLE DE LOS GRUPOS 
        public SqlDataReader ConsultarDetalleAcc(int idGrupo, int idioma)
        {
            string sql;

            //sql = "SELECT     Accesorio_Detalle.Acc_Det_Cod_1E, Accesorio_Detalle.Acc_Det_Desc "+
            //        "FROM         Accesorios_Genericos INNER JOIN "+
            //        "  Accesorio_Detalle ON Accesorios_Genericos.Acc_Gen_Id = Accesorio_Detalle.Acc_Det_GenId "+
            //        "WHERE     (Accesorios_Genericos.Acc_Gen_Inactivo = 0) AND (Accesorio_Detalle.Acc_Det_Inactivo = 0) AND (Accesorio_Detalle.Acc_Det_Com = 1) AND   "+
            //        "  (Accesorios_Genericos.Acc_Gen_Id  =" + idGrupo + ") " +
            //        "GROUP BY Accesorios_Genericos.Acc_Gen_Id, Accesorios_Genericos.Acc_Gen_Nom_Esp, Accesorio_Detalle.Acc_Det_Cod_1E,  "+
            //        "  Accesorio_Detalle.Acc_Det_Desc " +
            //        "ORDER BY Accesorio_Detalle.Acc_Det_Desc ";

            //sql = "SELECT        item_planta.cod_erp, item_planta_idioma.descripcion AS Expr1, item_planta.activo, item_planta_idioma.activo AS Expr2, item_planta.planta_id, item_planta.item_grupo_planta_id, " +
            //             "item_planta_idioma.idioma_id FROM            item_planta INNER JOIN " +
            //             "item_planta_idioma ON item_planta.item_planta_id = item_planta_idioma.item_planta_id INNER JOIN "+
            //             "item ON item_planta.item_id = item.item_id "+
            //             "WHERE        (item_planta.cod_erp IS NOT NULL) AND (item_planta_idioma.idioma_id = "+idioma+") AND (item_planta.item_grupo_planta_id = "+idGrupo+") AND (item_planta.activo = 1) AND (item.disp_comercial = 1) AND "+
            //             "(item.activo = 1) AND (item_planta_idioma.activo = 1);";

            //sql = "SELECT item_planta.cod_erp, item_planta_idioma.descripcion AS Expr1, item_planta.planta_id, item_planta.item_grupo_planta_id, item_planta_idioma.idioma_id "+
            //      "FROM     item_planta INNER JOIN "+
            //      "item_planta_idioma ON item_planta.item_planta_id = item_planta_idioma.item_planta_id INNER JOIN "+
            //      "item ON item_planta.item_id = item.item_id "+
            //      "WHERE  (item_planta.cod_erp IS NOT NULL) AND (item_planta_idioma.idioma_id = "+idioma+") AND (item_planta.item_grupo_planta_id = "+idGrupo+") AND (item_planta.activo = 1) AND (item.activo = 1) AND "+
            //      "(item_planta_idioma.activo = 1) AND (item_planta.disp_comercial = 1) order by item_planta_idioma.descripcion ";

            sql =  " SELECT        item_planta.cod_erp, item_planta_idioma.descripcion AS Expr1, item_planta.planta_id, item_grupo_planta.item_grupo_planta_id, item_planta_idioma.idioma_id " +
            " FROM            item_planta INNER JOIN "+
             "            item_planta_idioma ON item_planta.item_planta_id = item_planta_idioma.item_planta_id INNER JOIN  "+
             "            item ON item_planta.item_id = item.item_id INNER JOIN  "+
             "            item_grupo ON item.item_grupo_id = item_grupo.item_grupo_id INNER JOIN  "+
             "            item_grupo_planta ON item_grupo.item_grupo_id = item_grupo_planta.item_grupo_id AND item_planta.planta_id = item_grupo_planta.planta_id  "+             
            " WHERE        (item_planta.cod_erp IS NOT NULL) AND (item_planta_idioma.idioma_id = " + idioma + ") AND (item_planta.activo = 1) AND (item.activo = 1) AND (item_planta_idioma.activo = 1) AND (item_planta.disp_comercial = 1) AND " +
            "              (item_grupo_planta.item_grupo_planta_id = " + idGrupo + ") " +
            " ORDER BY Expr1 ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //BUSQUEDA ACCESORIO POR CODIGO 
        public SqlDataReader ConsultarAccesorioCodigo(int erp, int planta)
        {
            string sql;

            //sql = "SELECT     Accesorios_Genericos.Acc_Gen_Id, Accesorios_Genericos.Acc_Gen_Nom_Esp, Accesorio_Detalle.Acc_Det_Cod_1E, "+
            //       " Accesorio_Detalle.Acc_Det_Desc "+
            //        "FROM         Accesorios_Genericos INNER JOIN "+
            //        " Accesorio_Detalle ON Accesorios_Genericos.Acc_Gen_Id = Accesorio_Detalle.Acc_Det_GenId "+
            //        "WHERE     (Accesorios_Genericos.Acc_Gen_Inactivo = 0) AND (Accesorio_Detalle.Acc_Det_Inactivo = 0) AND (Accesorio_Detalle.Acc_Det_Com = 1) AND "+
            //        " (Accesorio_Detalle.Acc_Det_Cod_1E =" + cod + ") " +
            //        "GROUP BY Accesorios_Genericos.Acc_Gen_Id, Accesorios_Genericos.Acc_Gen_Nom_Esp, Accesorio_Detalle.Acc_Det_Cod_1E, "+
            //        "  Accesorio_Detalle.Acc_Det_Desc  ORDER BY Accesorios_Genericos.Acc_Gen_Nom_Esp ";

            //sql = "SELECT        item_planta.cod_erp, item_planta.planta_id, item_planta.item_id, item_grupo_planta_idioma.descripcion, item_planta_idioma.descripcion AS Expr1, item_planta.item_planta_id, "+
            //       "item_grupo_planta.item_grupo_planta_id FROM            item INNER JOIN   item_planta ON item.item_id = item_planta.item_id INNER JOIN "+
            //       "item_grupo_planta ON item_planta.item_grupo_planta_id = item_grupo_planta.item_grupo_planta_id INNER JOIN "+
            //       "item_grupo_planta_idioma ON item_grupo_planta.item_grupo_planta_id = item_grupo_planta_idioma.item_grupo_planta_id INNER JOIN "+
            //       "item_planta_idioma ON item_planta.item_planta_id = item_planta_idioma.item_planta_id " +
            //       "WHERE              (item_planta.cod_erp = "+erp+") AND (item_planta_idioma.idioma_id = 1) AND (item_planta.activo = 1) AND (item_planta_idioma.activo = 1) AND (item_planta.planta_id = "+ planta + ") AND "+
            //       "(item_grupo_planta_idioma.idioma_id = 1) AND (item_grupo_planta_idioma.activo = 1) AND (item_grupo_planta.activo = 1) AND (item.disp_comercial = 1);";

            //sql = "SELECT item_planta.cod_erp, item_planta.planta_id, item_planta.item_id, item_grupo_planta_idioma.descripcion, item_planta_idioma.descripcion AS Expr1, item_planta.item_planta_id, " +
            //      "item_grupo_planta.item_grupo_planta_id " +
            //      "FROM            item_grupo INNER JOIN " +
            //          "   item_grupo_planta_idioma INNER JOIN " +
            //          "   item_grupo_planta ON item_grupo_planta_idioma.item_grupo_planta_id = item_grupo_planta.item_grupo_planta_id ON item_grupo.item_grupo_id = item_grupo_planta.item_grupo_id INNER JOIN " +
            //           "  item_planta_idioma INNER JOIN " +
            //            " item_planta ON item_planta_idioma.item_planta_id = item_planta.item_planta_id INNER JOIN " +
            //             " item ON item_planta.item_id = item.item_id ON item_grupo.item_grupo_id = item.item_grupo_id " +
            //    "WHERE  (item_planta.cod_erp = "+erp+") AND (item_planta_idioma.idioma_id = 1) AND (item_planta.activo = 1) AND (item_planta_idioma.activo = 1) AND (item_planta.planta_id = "+planta+") AND "+
            //      "(item_grupo_planta_idioma.idioma_id = 1) AND (item_grupo_planta_idioma.activo = 1) AND (item_grupo_planta.activo = 1) AND (item_planta.disp_comercial = 1) AND (item.activo = 1)";

            sql = "SELECT item_planta.cod_erp, item_planta.planta_id, item_planta.item_id, item_grupo_planta_idioma.descripcion, item_planta_idioma.descripcion AS Expr1, item_planta.item_planta_id, " +
                  "item_grupo_planta.item_grupo_planta_id " +
                  "FROM            item_grupo INNER JOIN " +
                     "    item_grupo_planta_idioma INNER JOIN " +
                      "   item_grupo_planta ON item_grupo_planta_idioma.item_grupo_planta_id = item_grupo_planta.item_grupo_planta_id ON item_grupo.item_grupo_id = item_grupo_planta.item_grupo_id INNER JOIN " +
                      "   item_planta_idioma INNER JOIN " +
                      "   item_planta ON item_planta_idioma.item_planta_id = item_planta.item_planta_id INNER JOIN " +
                      "   item ON item_planta.item_id = item.item_id ON item_grupo.item_grupo_id = item.item_grupo_id AND item_grupo_planta.planta_id = item_planta.planta_id " +
                "WHERE  (item_planta.cod_erp = " + erp + ") AND (item_planta_idioma.idioma_id = 1) AND (item_planta.activo = 1) AND (item_planta_idioma.activo = 1) AND (item_planta.planta_id = " + planta + ") AND " +
                  "(item_grupo_planta_idioma.idioma_id = 1) AND (item_grupo_planta_idioma.activo = 1) AND (item_grupo_planta.activo = 1) AND (item_planta.disp_comercial = 1) AND (item.activo = 1)";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTAR ACCESORIO EN COTIZACION
        public SqlDataReader ConsultarAccesorio(int id)
        {
            string sql;

            sql = "SELECT     cotizacion_accesorios.cot_cantidad, cotizacion_accesorios.cot_acc_precio_unitario, cotizacion_accesorios.cot_acc_peso_total,  " +
                     " cotizacion_accesorios.cot_acc_volumen_total, cotizacion_accesorios.cot_observacion, cotizacion_accesorios.cot_embalaje,   " +
                     " cotizacion_accesorios.cot_acc_id_acc, Accesorios_Genericos.Acc_Gen_Nom_Esp + ' ' + Accesorio_Detalle.Acc_Det_Desc AS Expr1  " +
                     " FROM         Accesorios_Genericos INNER JOIN " +
                     " Accesorio_Detalle ON Accesorios_Genericos.Acc_Gen_Id = Accesorio_Detalle.Acc_Det_GenId INNER JOIN " +
                     "cotizacion_accesorios ON Accesorio_Detalle.Acc_Det_Cod_1E = cotizacion_accesorios.cot_acc_id_acc " +
                     "WHERE (cot_acc_id = " + id + ") ";            

            return BdDatos.ConsultarConDataReader(sql);
        }

        //BUSQUEDA VALOR ACCESORIO
        //public DataSet ConsultarValorAccesorio(int erp , int planta)
        //{
        //    string sql;
            
        //    //sql = "SELECT     Acc_Det_Cod_1E, Acc_Det_Pre_Cop_Pleno, Acc_Det_Pre_Cop_Distri, 0 AS Expr1, Acc_Det_Pre_US_Pleno, Acc_Det_Pre_US_Distri, 0 AS Expr2, "+
        //    //       "Acc_Det_Pre_Cop_Filial1, Acc_Det_Pre_Cop_Filial2, Acc_Det_Pre_US_Filial1, Acc_Det_Pre_US_Filial2, Acc_Det_Peso, acc_cant_emp,  acc_peso_empaque " +
        //    //        "FROM         Accesorio_Detalle "+
        //    //        "WHERE     (Acc_Det_Cod_1E = " + cod + ") ";
        //    sql = "select ip.descripcion_corta, ip.item_id, i.peso_unitario, i.cant_empaque, i.peso_empaque from item_planta ip inner join item i on ip.item_id=i.item_id where ip.planta_id = '" + planta + "' and ip.cod_erp = '" + erp + "';";    
        //    DataSet ds_valoraccesorio = BdDatos.consultarConDataset(sql);
        //    return ds_valoraccesorio;
        //}

        //BUSQUEDA VALOR ACCESORIO
        public SqlDataReader ConsultarValorAccesorio(int erp, int planta)
        {
            string sql;
            //sql = "select ip.descripcion, ip.item_planta_id, i.peso_unitario, i.cant_empaque, i.peso_empaque, i.item_id, i.disp_produccion, ip.item_grupo_planta_id from item_planta ip inner join item i on ip.item_id=i.item_id where ip.planta_id = '" + planta + "' and ip.cod_erp = '" + erp + "';";  
            //sql = "SELECT ip.descripcion, ip.item_planta_id, ip.peso_unitario, ip.cant_empaque, ip.peso_empaque, i.item_id , isnull(ip.disp_produccion, 0), ip.item_grupo_planta_id "+
            //      "FROM     item_planta AS ip INNER JOIN "+
            //      "item AS i ON ip.item_id = i.item_id "+
            //      "WHERE  (ip.planta_id = "+planta+") AND (ip.cod_erp = "+erp+") AND (i.activo = 1) AND (ip.activo = 1)";

            sql = " SELECT        ip.descripcion, ip.item_planta_id, ip.peso_unitario, ip.cant_empaque, ip.peso_empaque, i.item_id, ISNULL(ip.disp_produccion, 0) AS Expr1, item_grupo_planta.item_grupo_planta_id " +
            " FROM            item_planta AS ip INNER JOIN " +
            "             item AS i ON ip.item_id = i.item_id INNER JOIN " +
            "             item_grupo ON i.item_grupo_id = item_grupo.item_grupo_id INNER JOIN " +
            "             item_grupo_planta ON item_grupo.item_grupo_id = item_grupo_planta.item_grupo_id AND ip.planta_id = item_grupo_planta.planta_id " +
            " WHERE        (ip.planta_id = " + planta + ") AND (ip.cod_erp = " + erp + ") AND (i.activo = 1) AND (ip.activo = 1)";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarPesoVolumenAccesorio(int cod)
        {           
            string sql;
            sql = "SELECT     Accesorios_Genericos.Acc_Gen_Nom_Esp + ' ' + Accesorio_Detalle.Acc_Det_Desc " +
            "FROM         Accesorios_Genericos INNER JOIN " +
            "  Accesorio_Detalle ON Accesorios_Genericos.Acc_Gen_Id = Accesorio_Detalle.Acc_Det_GenId " +
            " WHERE     (Accesorio_Detalle.Acc_Det_Cod_1E = " + cod + ") ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTO EL TIPO DE CLIENTE
        public SqlDataReader ConsultarTipoCliente(int cliente, int planta)
        {
            string sql;
            //sql = "SELECT cli_tipo FROM cliente WHERE (cli_id =" + cliente + ")";
            //sql = "select ct.descripcion, ct.cliente_tipo_id from cliente_tipo ct join cliente_tipo_planta ctp on ctp.cliente_tipo_id = ct.cliente_tipo_id where ct.planta_id = ctp.planta_id and ctp.cliente_id = '" + cliente + "';";
            //sql = "SELECT ct.descripcion, ct.cliente_tipo_id, ctp.planta_id FROM cliente_tipo AS ct INNER JOIN cliente_tipo_planta AS ctp ON ctp.cliente_tipo_id = ct.cliente_tipo_id INNER JOIN cliente_planta AS cp ON cp.cliente_tipo_planta_id = ctp.cliente_tipo_planta_id WHERE  (cp.cliente_id = "+cliente+") AND (cp.activo = 1) AND (ctp.planta_id = "+planta+");";
            sql = " SELECT        ct.descripcion, ct.cliente_tipo_id, ctp.planta_id, " +
                  " CASE WHEN planta_forsa.planta_activar_mon_ext <> 1 THEN CASE WHEN cliente.cli_pai_id = planta_forsa.planta_pais_id THEN planta_forsa.planta_moneda_nal ELSE planta_forsa.planta_moneda_ext END ELSE " +
                  " planta_forsa.planta_moneda_ext END AS Expr1 " +
                  " FROM              cliente_tipo AS ct INNER JOIN " +
                  " cliente_tipo_planta AS ctp ON ctp.cliente_tipo_id = ct.cliente_tipo_id INNER JOIN " +
                  " cliente_planta AS cp ON cp.cliente_tipo_planta_id = ctp.cliente_tipo_planta_id INNER JOIN " +
                  " planta_forsa ON ctp.planta_id = planta_forsa.planta_id INNER JOIN " +
                  " cliente ON cp.cliente_id = cliente.cli_id " +
                  " WHERE        (cp.cliente_id = "+cliente+") AND (cp.activo = 1) AND (ctp.planta_id = "+planta+") AND (ct.activo = 1) AND (ctp.activo = 1);";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTO LA CIUDAD DE LA OBRA
        public SqlDataReader ConsultarCiudadObra(int obra)
        {
            string sql;
            sql = "SELECT ciu_nombre FROM obra INNER JOIN ciudad ON obr_ciu_id = ciu_id WHERE (obr_id =" + obra + ")";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CREACION DEL FORMATO UNICO DE PROYECTOS (FUP)
        public int FUP(string fecha_crea, int cli_id, int unm_id, int ccl_id, int obra_id, string menu, string usu_crea, int planta)
        {

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[8];
            sqls[0] = new SqlParameter("F_Creacion", fecha_crea);
            sqls[1] = new SqlParameter("ID_Cliente", cli_id);
            sqls[2] = new SqlParameter("ID_Moneda", unm_id);
            sqls[3] = new SqlParameter("ID_Contacto", ccl_id);
            sqls[4] = new SqlParameter("ID_Obra", obra_id);
            sqls[5] = new SqlParameter("ID_Menu", menu);
            sqls[6] = new SqlParameter("Usuario", usu_crea);
            sqls[7] = new SqlParameter("planta_id", planta);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarFUPNew", con))
                //using (SqlCommand cmd = new SqlCommand("InsertarFUP", con))
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

        //CREACION DE LA COTIZACION DE ACCESORIOS
        public int CotAcc(int acc, int cant, string preuni, string pesototal, string voltotal, int idfup, string observa, 
            string embalaje, int item, string usuario, string fecha, int tipo, int modelo, int estado_item)
        {

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            Int32 newId = -1;
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[14];
            sqls[0] = new SqlParameter("ACCESORIO ", acc);
            sqls[1] = new SqlParameter("CANTIDAD", cant);
            sqls[2] = new SqlParameter("PRECIOUNITARIO", preuni);
            sqls[3] = new SqlParameter("PESOTOTAL", pesototal);
            sqls[4] = new SqlParameter("VOLUMENTOTAL", voltotal);
            sqls[5] = new SqlParameter("FUP", idfup);
            sqls[6] = new SqlParameter("OBSERVACION", observa);
            sqls[7] = new SqlParameter("EMBALAJE", embalaje);
            sqls[8] = new SqlParameter("ITEM", item);
            sqls[9] = new SqlParameter("USUARIO", usuario);
            sqls[10] = new SqlParameter("FECHA", fecha);            
            sqls[11] = new SqlParameter("tipo_id", tipo);
            sqls[12] = new SqlParameter("modelo_id", modelo);
            sqls[13] = new SqlParameter("estado_item", estado_item);
            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarCOTIZACIONACCESORIOSNew", con))
                //using (SqlCommand cmd = new SqlCommand("InsertarCOTIZACIONACCESORIOS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);
                    cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int, 0, "cot_acc_id");
                    cmd.Parameters["@RETURN_VALUE"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    newId = (int)cmd.Parameters["@RETURN_VALUE"].Value;
                }
            }
            return newId;
        }

        //ACTUALIZACION DE LA COTIZACION DE ACCESORIOS
        public int ActAcc(int acc, int cant, string preuni, string pesototal, string voltotal, string observa,
            string embalaje, string usuario, string fecha, int fup, string hora, string estado, int codacc, int tipo, int modelo, int estado_item)
        {

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;


            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[16];
            sqls[0] = new SqlParameter("ACCESORIO ", acc);
            sqls[1] = new SqlParameter("CANT", cant);
            sqls[2] = new SqlParameter("PRECIOUNITARIO", preuni);
            sqls[3] = new SqlParameter("PESOTOTAL", pesototal);
            sqls[4] = new SqlParameter("VOLUMENTOTAL", voltotal);
            sqls[5] = new SqlParameter("OBSERVACION", observa);
            sqls[6] = new SqlParameter("EMBALAJE", embalaje);
            sqls[7] = new SqlParameter("USUARIO", usuario);
            sqls[8] = new SqlParameter("FECHA", fecha);
            sqls[9] = new SqlParameter("IDFUP", fup);
            sqls[10] = new SqlParameter("HORA", hora);
            sqls[11] = new SqlParameter("ESTADO", estado);
            sqls[12] = new SqlParameter("CODACC", codacc);
            sqls[13] = new SqlParameter("tipo_id", tipo);
            sqls[14] = new SqlParameter("modelo_id", modelo);
            sqls[15] = new SqlParameter("estado_item", estado_item);
            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("ActualizarCOTIZACIONACCESORIOSNew", con))
                //using (SqlCommand cmd = new SqlCommand("ActualizarCOTIZACIONACCESORIOS", con))
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

        public int ActualizarEstadoFUPAccesorio(int fup, string OF)
        {
            string sql;
            sql = "UPDATE formato_unico SET fup_ch_accesorios = 1, fup_ofa_pv = '" + OF + "' WHERE fup_id = " + fup;

            return BdDatos.Actualizar(sql);
        }

        //QUITAR CONFIRMACION PEDIDO DE VENTA
        public int QuitarConfirmacionComercial(int fup)
        {
            string sql;
            sql = "UPDATE pedido_venta SET pv_confirma_com = 0 WHERE pv_fup_id = " + fup;
            sql = "UPDATE solicitud_facturacion SET Sf_confirma = 0 WHERE Sf_fup_id = " + fup;

            return BdDatos.Actualizar(sql);
        }

        //QUITAR CONFIRMACION PEDIDO DE VENTA
        public int EjecutarJob()
        {
            string sql;
            sql = "EXEC msdb.dbo.sp_start_job 'Actualiza Clientes Sf Erps' ;" ;

            return BdDatos.Actualizar(sql);
        }

        //CONSULTAR ITEM ACCESORIOS
        public SqlDataReader ConsultarItemAccesorio(int fup)
        {
            string sql;
            sql = "SELECT cot_item FROM cotizacion_accesorios WHERE (cot_acc_fup_id = " + fup + ") ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTAR MÁXIMO ITEM ACCESORIO
        public SqlDataReader ConsultarMaximoItemAccesorio(int fup)
        {
            string sql;
            sql = "SELECT MAX (cot_item) FROM cotizacion_accesorios WHERE (cot_acc_fup_id = " + fup + ") ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //OBTENER EMAIL PV
        public SqlDataReader ObtenerMailPV()
        {
            string sql = "SELECT emp_correo_electronico FROM empleado WHERE emp_mail_pv = 1 AND emp_activo = 1 ";
            return BdDatos.ConsultarConDataReader(sql);
        }        

        //OBTENER EMAIL ADMIN
        public SqlDataReader ObtenerMailAdmin()
        {
            string sql = "SELECT emp_correo_electronico FROM empleado WHERE emp_mail_admin = 1 AND emp_activo = 1 ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //OBTENER EMAIL ADMIN
        public SqlDataReader ObtenerMailArrendadora()
        {
            string sql = "SELECT emp_correo_electronico FROM empleado WHERE  emp_activo = 1 AND emp_mail_pv_arrendadora = 1 ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //OBTENER EMAIL GERENTES
        public SqlDataReader ObtenerMailGerentes(int pais)
        {
            string sql = "SELECT rc_email FROM representantes_comerciales rc INNER JOIN pais_representante pa ON " + 
            "pa.pr_id_representante = rc.rc_id INNER JOIN usuario u ON u.usu_siif_id = rc.rc_usu_siif_id " +
            "WHERE pa.pr_id_pais = " + pais + " AND u.usu_rap_id = 2 AND rc.rc_activo = 1 AND pa.pr_activo = 1 ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CREACION DEL PEDIDO DE VENTA
        public int IngPV(string anho, string tipo, int fup, string solic, string ver, int tipopv, int sf, int planta)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[8];
            sqls[0] = new SqlParameter("ANHO ", anho);
            sqls[1] = new SqlParameter("TIPO", tipo);
            sqls[2] = new SqlParameter("FUP", fup);
            sqls[3] = new SqlParameter("SOLICITUD", solic);
            sqls[4] = new SqlParameter("VERSION", ver);
            sqls[5] = new SqlParameter("TIPOPV", tipopv);
            sqls[6] = new SqlParameter("SF", sf);
            sqls[7] = new SqlParameter("PLANTA", planta);
            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarORDENNew", con))
                //using (SqlCommand cmd = new SqlCommand("InsertarORDEN", con))
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

        //ANULAMOS EL FUP EN LA TABLA ORDEN 
        public int AnularPV(int fup)
        {
            string sql;
            sql = "UPDATE Orden SET Anulada = 1" +
                  "WHERE Yale_Cotiza = " + fup + " ;" +
                  "UPDATE Orden_Seg set  Anulado = 1 WHERE fup = " + fup + " ; ";
                  ;
            return BdDatos.Actualizar(sql);
        }

        public SqlDataReader ConsultarOFID(string OFA)
        {
            string sql;
            sql = "SELECT Orden.Id_Ofa FROM Orden WHERE (Orden.Ofa LIKE '%" + OFA + "%' AND Letra = '1') ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONFIRMACION PEDIDO DE VENTA ALMACEN
        public int ConfirmacionAlmacen(int fup, string fecha)
        {
            string sql;
            sql = "UPDATE pedido_venta SET pv_entrega_almacen = 1, pv_fecha_almacen = '" + fecha + "' WHERE pv_fup_id = " + fup;

            return BdDatos.Actualizar(sql);
        }

        //CONFIRMACION PEDIDO DE VENTA LOGISTICA
        public int ConfirmacionLogistica(int fup, string fecha)
        {
            string sql;
            sql = "UPDATE pedido_venta SET pv_logistica = 1, pv_fecha_logistica = '" + fecha + "' WHERE pv_fup_id = " + fup;

            return BdDatos.Actualizar(sql);
        }

        //CONFIRMACION PEDIDO DE VENTA LOGISTICA almacen
        public int ConfirmacionLogisticaAlmacen(int fup, string fecha,string usuario)
        {
            string sql;
            sql = "UPDATE pedido_venta SET pv_logistica_almacen = 1, pv_fecha_logistica_alm = '" + fecha + "', "+
                  " pv_usu_logistica_alm = '" +  usuario + "' WHERE pv_fup_id = " + fup;

            return BdDatos.Actualizar(sql);
        }

        //CONFIRMACION PEDIDO DE VENTA LOGISTICA accesorios
        public int ConfirmacionLogisticaAccesorios(int fup, string fecha, string usuario)
        {
            string sql;
            sql = "UPDATE pedido_venta SET pv_logistica_accesorios = 1, pv_fecha_logistica_acc = '" + fecha + "', " +
                  " pv_usu_logistica_acc = '" + usuario + "' WHERE pv_fup_id = " + fup;

            return BdDatos.Actualizar(sql);
        }

        
        //NUMERO OP
        public int IngresarNumeroOP(string numpv, int num)
        {
            string sql = "UPDATE Orden_Seg SET TD_1EE = 'OP', ND_1EE = " + num + " " +
                  "WHERE Num_Of = '" + numpv + "' ";

            return BdDatos.Actualizar(sql);
        }

        //INSERTAR LOG PV DESCONFIRMACIÓN
        public int LogQuitarConfirmacionComercial(int fup, string usuario)
        {
            string sql;
            sql = "INSERT INTO LOG_pv (logpv_fup_id, logpv_tipo, usu_crea) " +
                "VALUES(" + fup + ",'Desconfirmación', '" + usuario + "')";

            return BdDatos.Actualizar(sql);
        }

        //Consulta los modelos relacionados a un item
        public SqlDataReader poblarModeloItem(Int64 item)
        {
            //string sql = "select im.descripcion, im.item_modelo_id from item_modelo im where (select i.req_modelo from item i where i.item_id =" + item + ") = 1;";
            string sql;
            //sql = "SELECT descripcion, item_modelo_id " +
            //      "FROM     item_modelo AS im " +
            //      "WHERE  ((SELECT isnull(req_modelo, 0) " +
            //      "FROM      item_planta AS ip " +
            //      "WHERE   (item_planta_id = " + item + ")) = 1)";

            sql = "SELECT        item_modelo.descripcion as descripcion, item_modelo.item_modelo_id as item_modelo_id " +
                  "FROM            item_planta INNER JOIN " +
                  "item_modelo ON item_planta.item_modelo_codigo = item_modelo.modelo_codigo  " +
                  "WHERE        (item_planta.item_planta_id = " + item + ")";


            return BdDatos.ConsultarConDataReader(sql);
        }

        //Consulta la descripcion de un modelo
        public SqlDataReader consultarModelo(int item_planta_id)
        {
            string sql ="SELECT        item_modelo.item_modelo_id, item_modelo.descripcion FROM item_modelo INNER JOIN "+
             " item_planta ON item_modelo.modelo_codigo = item_planta.item_modelo_codigo WHERE (item_planta.item_planta_id = "+item_planta_id+")";
            //string sql = "select item_modelo_id, descripcion from item_modelo where item_modelo_id = "+modelo+";";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //Consulta los tipos relacionados a un item
        public SqlDataReader poblarTipoItem(Int64 item)
        {
            //string sql = "select it.descripcion, it.item_tipo_id from item_tipo it where (select i.req_tipo from item i where i.item_id =" + item + ") = 1;";
            string sql;
            sql = "SELECT descripcion, item_tipo_id " +
                  "FROM     item_tipo AS it " +
                  "WHERE  ((SELECT isnull(req_tipo, 0) " +
                  "FROM      item_planta AS ip " +
                  "WHERE   (item_planta_id = "+item+")) = 1)";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //Consulta la descripcion de un tipo
        public SqlDataReader consultarTipo(int tipo)
        {
            string sql = "select item_tipo_id, descripcion from item_tipo where item_tipo_id =  "+tipo+";";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader CargarImagenItem(Int64 item)
        {
            string sql = "select i.ruta_imagen from item_grupo ig inner join item i on i.item_grupo_id=ig.item_grupo_id where i.item_id=" + item + ";";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader MostrarTipoItem(Int64 item) 
        {
            string sql = "select isnull(especial,0) from item_planta where item_planta_id="+ item +";";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarParametrosItem(Int64 item)
        {
            string sql;
            sql = "SELECT itp.descripcion, itp.item_tipo_parametro_id " +
                  "FROM     item_tipo_parametro AS itp INNER JOIN " +
                  "item_planta_parametro AS ipp ON ipp.item_tipo_parametro_id = itp.item_tipo_parametro_id INNER JOIN " +
                  "item_planta AS ip ON ipp.item_planta_id = ip.item_planta_id " +
                  "WHERE  (itp.activo = 1) AND (ipp.activo = 1) AND (ip.activo = 1) AND (ipp.activo = 1) AND (ip.item_planta_id = "+item+")";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarPrecioItem(Int64 item , int cliente, int planta, int cliente_tipo, int moneda) 
        {
            string sql = "SELECT cliente_tipo_planta.porcentaje, cliente_tipo_planta.planta_id, cliente_tipo_planta.cliente_tipo_id, cliente_tipo.descripcion, planta_forsa.planta_descripcion, moneda.mon_descripcion, moneda.mon_id, item_planta_precio.item_planta_id, item_planta_precio.valor, cliente_planta.cliente_id, cliente_tipo.cliente_tipo_id AS Expr1,  cliente_planta.activo FROM     cliente_tipo_planta INNER JOIN    cliente_tipo ON cliente_tipo_planta.cliente_tipo_id = cliente_tipo.cliente_tipo_id INNER JOIN planta_forsa ON cliente_tipo_planta.planta_id = planta_forsa.planta_id INNER JOIN item_planta_precio ON cliente_tipo_planta.cliente_tipo_planta_id = item_planta_precio.cliente_tipo_planta_id INNER JOIN  moneda ON item_planta_precio.moneda_id = moneda.mon_id INNER JOIN cliente_planta ON cliente_tipo_planta.cliente_tipo_planta_id = cliente_planta.cliente_tipo_planta_id WHERE  (cliente_tipo_planta.planta_id = "+planta+") AND (cliente_planta.cliente_id = "+cliente+") AND (cliente_tipo.cliente_tipo_id = "+cliente_tipo+") AND (item_planta_precio.item_planta_id = "+item+") AND (moneda.mon_id = "+moneda+") and item_planta_precio.activo = 1;";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader cantidadDisponibleItem(int erp, int planta) 
        {
            string sql = "";
            int cia = 0;
            if (planta == 1) cia = 6;
            if (planta == 2) cia = 12;

            sql = "SELECT    t120_mc_items.f120_id AS CodErp, t120_mc_items.f120_descripcion AS Descripcion, SUM(BI_T400.f_cant_disponible_1) AS CantDisponible " +
                " FROM BI_T400 INNER JOIN  t121_mc_items_extensiones ON t121_mc_items_extensiones.f121_rowid = BI_T400.f_rowid_item_ext INNER JOIN " +
                " t120_mc_items ON t121_mc_items_extensiones.f121_rowid_item = t120_mc_items.f120_rowid " +
                " WHERE(t120_mc_items.f120_id_cia =" + cia + " ) AND(BI_T400.f_cant_existencia_1 > 0)  " +
                " AND(BI_T400.f_parametro_biable = " + cia + ") AND(t120_mc_items.f120_id = " + erp + ") GROUP BY t120_mc_items.f120_id, t120_mc_items.f120_descripcion";

            if (planta == 11)
            { 
                cia = 20;

                sql = "SELECT    t120_mc_items.f120_id AS CodErp, t120_mc_items.f120_descripcion AS Descripcion, SUM(BI_T400.f_cant_disponible_1) AS CantDisponible "+
                " FROM BI_T400 INNER JOIN  t121_mc_items_extensiones ON t121_mc_items_extensiones.f121_rowid = BI_T400.f_rowid_item_ext INNER JOIN " +
                " t120_mc_items ON t121_mc_items_extensiones.f121_rowid_item = t120_mc_items.f120_rowid " +
                " WHERE(t120_mc_items.f120_id_cia ="+ cia + " ) AND(BI_T400.f_cant_existencia_1 > 0)  AND (BI_T400.f_id_bodega = '001') " +
                " AND(BI_T400.f_parametro_biable = " + cia + ") AND(t120_mc_items.f120_id = " + erp + ") GROUP BY t120_mc_items.f120_id, t120_mc_items.f120_descripcion";
            }

            
            return BdDatos.ConsultarConexionBases(sql,"unoee");
        }

        public SqlDataReader requerirPlanos(Int64 item)
        {
            string sql = "select isnull(req_plano, 0) from item_planta where item_planta_id = "+ item +" and req_plano = 1 ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarDetalleFUP(int fup, int planta)
        {
            string sql = "SELECT        cotizacion_accesorios.cot_acc_id, cotizacion_accesorios.cot_acc_id_acc, cotizacion_accesorios.cot_cantidad, cotizacion_accesorios.cot_acc_precio_unitario, cotizacion_accesorios.cot_acc_precio_total, "+
                         "cotizacion_accesorios.cot_observacion, cotizacion_accesorios.cot_item, item_planta_idioma.descripcion AS descripcion1, item_planta.planta_id, isnull(item_planta.especial,0), "+
                         " CASE WHEN ISNULL(item_planta.especial, 0) = 1 AND cotizacion_accesorios.estado_item = 0 THEN 0 WHEN ISNULL(item_planta.especial, 0) = 1 AND  " +
                         " cotizacion_accesorios.estado_item = 1 THEN 1 ELSE - 1 END AS estadoEspecial "+
                            "FROM            cotizacion_accesorios INNER JOIN "+
                         "item_planta ON cotizacion_accesorios.cot_acc_id_acc = item_planta.cod_erp INNER JOIN "+
                         "item_planta_idioma ON item_planta.item_planta_id = item_planta_idioma.item_planta_id INNER JOIN "+
                         "item ON item_planta.item_id = item.item_id "+
                         "WHERE        (cotizacion_accesorios.cot_acc_fup_id = " + fup + ") AND (cotizacion_accesorios.cot_acc_activo = 1) AND (item_planta_idioma.idioma_id = 1) AND (item_planta.planta_id = " + planta + ") AND (item_planta.activo = 1) AND (item.activo = 1) order by cotizacion_accesorios.cot_acc_id";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader CambiarEstadoAccesorio(int fup)
        {
            string sql = "update cotizacion_accesorios set cot_acc_activo = 0 where cot_acc_id = " + fup + ";"+
                " exec FletesPV " + fup + " ;"; 
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ActulizarAccesorioFUP(int id, string observacion, double cantidad, double total, int fup)
        {
            string sql = "update cotizacion_accesorios set cot_observacion = '" + observacion + "', cot_cantidad = " + cantidad + ", cot_acc_precio_total = "+total+" where cot_acc_id = " + id + ";" +
                         " exec FletesPV "+ fup +" ;";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader cargarCotizacionAccesorio(int id_cot)
        {
            //string sql = "select ca.cot_acc_id_acc, ca.cot_cantidad, ca.cot_acc_precio_unitario, ca.cot_acc_precio_total, ca.cot_observacion, isnull(ca.tipo_id,0), isnull(ca.modelo_id,0), i.cant_empaque, i.peso_empaque, i.especial ,m.mon_id, m.mon_descripcion, ip.item_planta_id, i.peso_unitario, ip.item_id, ip.cod_erp, i.disp_produccion from cotizacion_accesorios ca inner join item_planta ip on ip.cod_erp=ca.cot_acc_id_acc inner join item i on i.item_id=ip.item_id inner join formato_unico fu on fu.fup_id=ca.cot_acc_fup_id inner join moneda m on m.mon_id=fu.fup_unm_id where cot_acc_id = " + id_cot + ";";
            string sql;
            sql = "SELECT ca.cot_acc_id_acc, ca.cot_cantidad, ca.cot_acc_precio_unitario, ca.cot_acc_precio_total, ca.cot_observacion, ISNULL(ca.tipo_id, 0) AS Expr1, ISNULL(ca.modelo_id, 0) " +
                   "AS Expr2, ip.cant_empaque, ip.peso_empaque, ip.especial, m.mon_id, m.mon_descripcion, ip.item_planta_id, ip.peso_unitario, ip.item_id, ip.cod_erp, isnull(ip.disp_produccion, 0) " +
                   "FROM     cotizacion_accesorios AS ca INNER JOIN " +
                   "item_planta AS ip ON ip.cod_erp = ca.cot_acc_id_acc INNER JOIN " +
                   "formato_unico AS fu ON fu.fup_id = ca.cot_acc_fup_id INNER JOIN " +
                   "moneda AS m ON m.mon_id = fu.fup_unm_id " +
                   "WHERE  (ca.cot_acc_id = "+id_cot+")";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public void insertarParametros(int idAcc, List<int> idParametros, List<string> parametros)
        {
            for (int i = 0; i < parametros.Count; i++) 
            {
                string sql = "INSERT INTO cotizacion_accesorios_param(item_tipo_parametro_id, cotizacion_accesorios_id, descripcion) VALUES (" + idParametros.ElementAt(i) + " , " + idAcc + ", '" + parametros.ElementAt(i) + "')";
                BdDatos.ConsultarConDataReader(sql);            
            }            
        }

        public SqlDataReader cargarParametrosItem(int idAcc) 
        {
            string sql = "select cap.item_tipo_parametro_id, cap.descripcion, itp.descripcion from cotizacion_accesorios_param  cap inner join item_tipo_parametro itp on itp.item_tipo_parametro_id=cap.item_tipo_parametro_id where cotizacion_accesorios_id = "+idAcc+";";
            return BdDatos.ConsultarConDataReader(sql);        
        }

        public void actualizarParametrosAccesorios(int idAcc, List<int> idParametro, List<string> descripcion) 
        {
            for (int i = 0; i < idParametro.Count; i++)
            {
                string sql = "update cotizacion_accesorios_param set descripcion = '" + descripcion.ElementAt(i) + "' where item_tipo_parametro_id = " + idParametro.ElementAt(i) + " and cotizacion_accesorios_id = " + idAcc + ";";
                BdDatos.ConsultarConDataReader(sql);
            }     
        }

        public SqlDataReader consultarSolicitudFacturacion(int fup) 
        {
            string sql = "SELECT sf_ingresada, SF_id, Sf_vlr_comercial, Sf_tltiva FROM solicitud_facturacion WHERE (Sf_fup_id =" + fup + ") AND (Sf_vlr_comercial <> 0) and sf_ingresada = 1 and Sf_confirma = 1;";
            return BdDatos.ConsultarConDataReader(sql);        
        }   
     
        public SqlDataReader consultarTipoItem(int item_cotizado) 
        {
            string sql = "SELECT        ISNULL(ip.especial, 0) AS Expr1, ISNULL(i.disp_produccion, 0) AS Expr2 FROM            cotizacion_accesorios AS ca INNER JOIN                         item_planta AS ip ON ip.cod_erp = ca.cot_acc_id_acc INNER JOIN                          item AS i ON i.item_id = ip.item_id INNER JOIN                          formato_unico ON ca.cot_acc_fup_id = formato_unico.fup_id AND ip.planta_id = formato_unico.planta_id where ca.cot_acc_fup_id = " + item_cotizado + "  and ca.cot_acc_activo = 1;";
            return BdDatos.ConsultarConDataReader(sql);        
        }

        public SqlDataReader consultarEstadoItem(int fup)
        {
            string sql = "select isnull(estado_item , 0) from cotizacion_accesorios where cot_acc_fup_id = "+fup+" and cot_acc_activo = 1;";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader consultarAccesoriosPorConfirmar(int fup)
        {
            string sql = "SELECT isnull(ca.estado_item,0), isnull(ca.tipo_id,0), isnull(ca.modelo_id,0), ca.cot_acc_id_acc, ca.cot_acc_id, ca.cot_item FROM  cotizacion_accesorios AS ca WHERE  (ca.cot_acc_fup_id = "+fup+") AND (ca.cot_acc_activo = 1);";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader consultarParametrosPorConfirmar(int fup)
        {
            string sql = "SELECT ca.estado_item, cap.descripcion, itp.descripcion AS Exp, ca.cot_acc_id_acc, cap.cotizacion_accesorios_id, ca.cot_item FROM  cotizacion_accesorios AS ca INNER JOIN cotizacion_accesorios_param AS cap ON cap.cotizacion_accesorios_id = ca.cot_acc_id INNER JOIN item_tipo_parametro AS itp ON itp.item_tipo_parametro_id = cap.item_tipo_parametro_id WHERE  (ca.cot_acc_fup_id = " + fup + ") AND (ca.cot_acc_activo = 1)";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader consultarMonedaPais(int pais)
        {
            string sql = "SELECT        pais.pai_moneda, moneda.mon_descripcion FROM            pais INNER JOIN                          moneda ON pais.pai_moneda = moneda.mon_id WHERE        (pais.pai_id = "+pais+");";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader consultarRequiereTipoModelo(int erp, int planta)
        {
            string sql = "SELECT isnull(item_planta.req_tipo, 0), isnull(item_planta.req_modelo,0), item_planta.planta_id, item_planta.cod_erp, isnull(item_planta.req_plano,0) FROM     item_planta WHERE  (item_planta.planta_id = "+planta+") AND (item_planta.cod_erp = "+erp+");";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader consultarEstadoPV(int fup)
        {
            //string sql = "SELECT pv_estado.pv_descripcion, pv_estado.pv_estado_id FROM formato_unico INNER JOIN pv_estado ON formato_unico.fup_Estado_pv = pv_estado.pv_estado_id WHERE  (pv_estado.pv_activo = 1) AND (formato_unico.fup_id = "+fup+");";
            string sql = "";
            sql = "SELECT SeguimientoPV.pv_estado_id, pv_estado.pv_descripcion "+
                    "FROM     SeguimientoPV INNER JOIN "+
                    "pv_estado ON SeguimientoPV.pv_estado_id = pv_estado.pv_estado_id "+
                    "WHERE  (SeguimientoPV.fup = "+fup+");";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader insertarSeguimientoPV(int fup, int estado, int conf_comercial, string fecha, string usuario, int tipoContenido)
        {
            string sql = "INSERT INTO SeguimientoPV(fup, pv_estado_id, conf_comercial, conf_comercial_fecha, conf_comercial_usuario, pv_contenido_id) VALUES ("+ fup +", "+ estado +" ,"+ conf_comercial +",'"+ fecha +"', '"+ usuario +"', "+tipoContenido+")";
            return BdDatos.ConsultarConDataReader(sql);
        }
        
        public SqlDataReader actualizarFechaSeguimientoPV(string fecha,string fechaPlan, int fup, string usuario)
        {
            string sql = "UPDATE SeguimientoPV SET fecha_planeada_despacho = '" + fechaPlan + "', fecha_planeada_dsp_usu = '" + usuario + "' WHERE fup = " + fup + "; "+
                         "INSERT INTO LOG_pv (logpv_fup_id, logpv_tipo, usu_crea, logpv_descripcion) " +
                                      "VALUES(" + fup + ",'Fecha Plan Despacho', '" + usuario + "' , '" + fechaPlan + "')";
                ; 
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader actualizarIdRecompra(int fup ,int IdRecompra, string usuario)
        {
            string sql = "UPDATE  formato_unico SET IdRecompra = " + IdRecompra + ", RecompraUsuario = '" + usuario + "' , " +
                            " RecompraFecha = getdate()  WHERE fup_id = " + fup + "; ";                            
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader seleccionarCantidadComprometer(int fup, int planta)
        {
            string sql = "SELECT SUM(cotizacion_accesorios.cot_cantidad) as sumc , item_planta.cod_erp, item_planta.item_planta_id, formato_unico.fup_id, item_planta.planta_id FROM item_planta INNER JOIN item ON item_planta.item_id = item.item_id INNER JOIN formato_unico INNER JOIN cotizacion_accesorios ON formato_unico.fup_id = cotizacion_accesorios.cot_acc_fup_id ON item_planta.cod_erp = cotizacion_accesorios.cot_acc_id_acc WHERE  (formato_unico.planta_id = "+planta+") AND (formato_unico.fup_id = "+fup+") AND item_planta.planta_id = 1 AND (cotizacion_accesorios.cot_acc_activo = 1) group by item_planta.cod_erp, item_planta.item_planta_id, formato_unico.fup_id, item_planta.planta_id;"; 
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader insertarCantidadComprometida(int item, int cantidad ,int fup, string fecha, int activo, int erp, int planta)
        {
            string sql = "INSERT INTO item_planta_compromiso(item_planta_id, cant_comprometida, fup_id, fecha, activo, cod_erp, planta_id) VALUES("+item+", "+cantidad+", "+fup+", '"+fecha+"', "+activo+", "+erp+", "+planta+");";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader seleccionarCantidadComprometida(int erp, int planta)
        {
            string sql = "SELECT SUM(isnull(cant_comprometida, 0)) AS Expr1 FROM     item_planta_compromiso WHERE  (activo = 1) AND (cod_erp = "+erp+") AND (planta_id = "+planta+");";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader actualizarSeguimientoPVComercial(int fup, int conf_comercial, string fecha_comercial, string usuario_comercial, int estado)
        {
            string sql = "UPDATE SeguimientoPV SET conf_comercial = " + conf_comercial + ", conf_comercial_fecha = '" + fecha_comercial + "', conf_comercial_usuario = '" + usuario_comercial + "', pv_estado_id = "+estado+" WHERE fup = " + fup + ";";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader actualizarSeguimientoPVAsistente(int fup, int conf_asistente, string fecha_asistente, string usuario_asistente, int rechaza_asistente, int estado)
        {
            string sql = "UPDATE SeguimientoPV SET conf_asistente = " + conf_asistente + ", conf_asistente_fecha = '" + fecha_asistente + "', conf_asistente_usuario = '" + usuario_asistente + "', rechaza_asistente = '" + rechaza_asistente + "', pv_estado_id = " + estado + " WHERE fup = " + fup + ";";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader actualizarSeguimientoPVIngenieria(int fup, int conf_ingenieria, string fecha_ingenieria, string usuario_ingenieria, int rechaza_ingenieria, int estado)
        {
            string sql = "UPDATE SeguimientoPV SET conf_ingenieria = " + conf_ingenieria + ", conf_ingenieria_fecha = '" + fecha_ingenieria + "', conf_ingenieria_usuario = '" + usuario_ingenieria + "', rechaza_ingenieria = '" + rechaza_ingenieria + "', pv_estado_id = "+estado+" WHERE fup = " + fup + ";";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader actualizarSeguimientoPVRechazoAsistente(int fup, int rechaza_asistente, string fecha_rezhazo, string usuario, string motivo, int conf_comercial, int estado)
        {
            string sql = "UPDATE SeguimientoPV SET rechaza_asistente = " + rechaza_asistente + ", rechaza_asistente_fecha  = '" + fecha_rezhazo + "', rechaza_asistente_usuario = '" + usuario + "', rechaza_asistente_motivo = '" + motivo + "', conf_comercial = " + conf_comercial + ", pv_estado_id = "+estado+" WHERE fup = " + fup + ";";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader actualizarSeguimientoPVRechazoIngenieria(int fup, string fecha_rezhazo, string usuario, string motivo, int conf_asistente, int rechaza_ingenieria, int estado)
        {
            string sql = "UPDATE SeguimientoPV SET rechaza_ingenieria_fecha  = '" + fecha_rezhazo + "', rechaza_ingenieria_usuario = '" + usuario + "', rechaza_ingenieria_motivo = '" + motivo + "', conf_asistente = " + conf_asistente + ", rechaza_ingenieria = " + rechaza_ingenieria + ", pv_estado_id = "+ estado +" WHERE fup = " + fup + ";";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader seleccionarConfirmaciones(int fup)
        {
            string sql = "SELECT isnull(conf_comercial, 0), isnull(conf_asistente, 0), isnull(conf_ingenieria, 0), isnull(rechaza_asistente, 0), isnull(rechaza_ingenieria, 0) FROM SeguimientoPV WHERE  (fup = "+fup+")";
            return BdDatos.ConsultarConDataReader(sql);
        } 

        public SqlDataReader consultarTipoPV(int tipo)
        {
            string sql = "SELECT pv_tipo_id, descripcion, activo FROM pv_tipo WHERE  (pv_tipo_id = "+tipo+") and pv_tipo.activo = 1;";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader consultarContenidoPV(int contenido)
        {
            string sql = "SELECT pv_contenido_id, descripcion, activo FROM pv_contenido WHERE  (pv_contenido_id = "+contenido+") and pv_contenido.activo = 1;";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader calcularMonedaDefectoPais(int planta)
        {
            string sql = "SELECT        planta_forsa.planta_moneda_id, moneda.mon_descripcion " +
                    "FROM            planta_forsa INNER JOIN        moneda ON planta_forsa.planta_moneda_id = moneda.mon_id "+
                    "WHERE        (planta_forsa.planta_id = "+planta+") AND (planta_forsa.planta_activo = 1) AND (moneda.mon_activo = 1);";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader descripcionMoneda(int moneda)
        {
            string sql = "SELECT mon_descripcion FROM     moneda WHERE  (mon_id = "+moneda+")";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader requierePlanoItem(int planta, int erp)
        {
            string sql = "SELECT isnull(item_planta.req_plano,0) FROM     item_planta WHERE  (item_planta.cod_erp = "+erp+") AND (item_planta.planta_id = "+planta+");";
            return BdDatos.ConsultarConDataReader(sql);
        } 

        public SqlDataReader actualizarSeguimientoPVLogisticaAlmacen(int log_almacen, string fecha,  string usuario ,int fup, int estado)
        {
            string sql = "UPDATE SeguimientoPV SET log_almacen  = '" + log_almacen + "', log_almacen_fecha = '" + fecha + "', log_almacen_usuario = '" + usuario + "', pv_estado_id = "+estado+" WHERE fup = " + fup + ";";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader actualizarSeguimientoPVLogisticaAccesorios(int log_accesorios, string fecha, string usuario, int fup, int estado)
        {
            string sql = "UPDATE SeguimientoPV SET log_accesorios  = '" + log_accesorios + "', log_accesorios_fecha = '" + fecha + "', log_accesorios_usuario = '" + usuario + "', pv_estado_id = "+estado+" WHERE fup = " + fup + ";";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader seleccionarFechaPlaneadaDespacho(int fup)
        {
            string sql = "SELECT isnull(fecha_planeada_despacho, ''), isnull(fecha_planeada_dsp_usu, '') FROM     SeguimientoPV WHERE  (fup = " + fup + ");";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader consultarCirreVenta(int fup)
        {
            string sql = "SELECT Id_Orden_Acce FROM     Of_Accesorios WHERE  (Yale_Cotiza = "+fup+");";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public void enviarCorreo(int evento, int fup, string user, string remitente, out string mensaje, string nombrer, string emailr, string paisr, int idSF)
        { 
            //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[4];
            sqls[0] = new SqlParameter("@pEvento ", evento);
            sqls[1] = new SqlParameter("@pFupID", fup);
            sqls[2] = new SqlParameter("@pUsuario", user);
            sqls[3] = new SqlParameter("@pRemitente", emailr);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_PV_notificaciones", con))
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
                        mail.From = new MailAddress(remitente);
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
 
                        MemoryStream ms = new MemoryStream();
                        //ADJUNTAMOS EL ARCHIVO                        
                        if (llevaAnexo)
                        {       
                            string enlace1 = "", enlace2 = "";
                            string fecha = System.DateTime.Today.ToLongDateString();

                            if(!String.IsNullOrEmpty(EnlaceAnexo1.ToString()))
                                enlace1 = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo1.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + fup +"&nombrer=" + nombrer + "&paisr=" + paisr + "&correor=" + emailr + "&fecha=" + fecha ;
                            if(!String.IsNullOrEmpty(EnlaceAnexo2.ToString()))
                                enlace2 = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo2.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + fup + "&version=A &parte=1" + "&sf_id=" + idSF;
 
                            Byte[] correo = new Byte[0];

                            if (!String.IsNullOrEmpty(enlace1)) 
                            {
                                correo = clienteWeb.DownloadData(enlace1);
                                ms = new MemoryStream(correo);
                                mail.Attachments.Add(new Attachment(ms, "FUP " + fup + ".pdf"));
                            }
                            if (!String.IsNullOrEmpty(enlace2))
                            {
                                correo = (clienteWeb.DownloadData(enlace2));
                                ms = new MemoryStream(correo);
                                mail.Attachments.Add(new Attachment(ms, "SF " + fup + ".pdf"));
                            }                          
                        }
                        //DEFINIMOS NUESTRO SERVIDOR SMTP
                        //DECLARAMOS LA CLASE SMTPCLIENT
                        SmtpClient smtp = new SmtpClient();
                        //DEFINIMOS NUESTRO SERVIDOR SMTP
                        smtp.Host = "smtp.office365.com";
                        //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
                        smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
                        smtp.Port = 25;
                        smtp.EnableSsl = true;

                        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
                            SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };
                        try
                        {
                            smtp.Send(mail);
                            mensaje = "";
                            
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

        public SqlDataReader consultarSfId(int fup)
        {
            string sql = " SELECT        Sf_id FROM            solicitud_facturacion WHERE        (Sf_fup_id = "+fup+");";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public DataTable consultarRutaPlano(int idAcc) 
        {
            DataTable dt = new DataTable();
            string sql = "SELECT isnull(ruta_planos, '') as planos FROM cotizacion_accesorios WHERE cot_acc_id = "+idAcc+"";
            dt = BdDatos.CargarTabla(sql);
            return dt;        
        }

        public string actualizarEstadoPlano(int idAcc, int estado, string directorio)
        {
            string msj = "";
            string sql = "";
            if (String.IsNullOrEmpty(directorio))
            {
                sql = "UPDATE cotizacion_accesorios SET tiene_planos = " + estado + " WHERE cot_acc_id = " + idAcc + "";
            }
            else
            {
                sql = "UPDATE cotizacion_accesorios SET tiene_planos = " + estado + " , ruta_planos = '" + directorio + "' WHERE cot_acc_id = " + idAcc + "";
            }
            try
            {
                BdDatos.Actualizar(sql);
                msj = "OK";
            }
            catch 
            {
                msj = "Error en el Query ControlPedido -> actualizarEstadoPlano";            
            }

            return msj;
        }
    }
}
