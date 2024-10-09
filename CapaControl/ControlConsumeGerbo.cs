using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaDatos;

namespace CapaControl
{
    public class ControlConsumeGerbo
    {
        public int CerrarConexion()
        {
            return BdDatos.desconectar();
        }

        //CONSULTAR nombre de item por el id de la raya y nivel
        public SqlDataReader ConsultarNombreItemForsa(int IdRaya, int nivel)
        {
            string sql;
            sql = " SELECT  RTRIM(Orden.Tipo_Of) + ' ' + CASE WHEN Gerbo_Estructura_Ordenes.Nivel = 3 THEN rtrim(orden.ofa) ELSE RTRIM(Orden.Numero) + '-' + RTRIM(Orden.ano) END + ' ' + SUBSTRING(cliente.cli_nombre, 0, 23) " +
                  " + ' ' + Gerbo_Estructura_Ordenes.Nomenclatura +' '+ case when Gerbo_Estructura_Ordenes.Nivel = 3 then Tipos_Sol.NomenclaturaItem else '' end   AS Descripcion, " +
                  "  Gerbo_Estructura_Ordenes.Establecimiento, Gerbo_Estructura_Ordenes.TipoProducto, Gerbo_Estructura_Ordenes.Linha,  Gerbo_Estructura_Ordenes.Familia, " +
                  "  Gerbo_Estructura_Ordenes.SubFamilia, Gerbo_Estructura_Ordenes.UnidadMedida, Gerbo_Estructura_Ordenes.Procedencia, " +
                  "  Gerbo_Estructura_Ordenes.NumCuentaEstoque, Gerbo_Estructura_Ordenes.NumCuentaProd, " +
                  " Gerbo_Estructura_Ordenes.OrigenMercadoria, Gerbo_Estructura_Ordenes.ClasificacionFiscal, " +
                  " Gerbo_Estructura_Ordenes.ControlNumSerie, Gerbo_Estructura_Ordenes.ControlNumLote, " +
                  " Gerbo_Estructura_Ordenes.OrdenProdNatural, Gerbo_Estructura_Ordenes.PesoBruto, Gerbo_Estructura_Ordenes.PesoLiquido, Gerbo_Estructura_Ordenes.TipoDocumento,TipoDocumentoPadre, Orden.Ord_Unidad_Neg," +
                  "  NumCtaICMS, NumCtaIPI, NumCtaVenda, NumCtaCustoVenda, CodTipoItem, isnull(Orden.Op_UnoEE,0), RTRIM(Orden.Tipo_Of), formato_unico.fup_id " +
                  " FROM cliente INNER JOIN " +
                  " formato_unico ON cliente.cli_id = formato_unico.fup_cli_id INNER JOIN "+
                  " Orden ON formato_unico.fup_id = Orden.Yale_Cotiza INNER JOIN "+
                  " Gerbo_Estructura_Ordenes ON Orden.Ord_Unidad_Neg = Gerbo_Estructura_Ordenes.IdUnidadNegocio "+
                  " INNER JOIN Tipos_Sol ON Orden.Tipo_Of = Tipos_Sol.T_Sol_Tipo  "+
                  " WHERE(Orden.Id_Ofa ="+ IdRaya+") AND(Gerbo_Estructura_Ordenes.Nivel = "+nivel+")" +
                  "  ORDER BY Orden.Id_Ofa DESC";           
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTAR nombre de item por el id de la raya y nivel
        public SqlDataReader ConsultarItemsDatosBd(int @IdItemPlanta)
        {
            string sql;
            sql = " SELECT UPPER ( item_planta.descripcion) AS Descripcion, Gerbo_Estructura_Ordenes.Establecimiento, Gerbo_Estructura_Ordenes.TipoProducto, " +
                   " Gerbo_Estructura_Ordenes.Linha, Gerbo_Estructura_Ordenes.Familia,  ISNULL(Accesorios_Nivel.CodSubFamiliaGerbo, Gerbo_Estructura_Ordenes.SubFamilia) AS SubFamilia, " +
                    " Gerbo_Estructura_Ordenes.UnidadMedida, Gerbo_Estructura_Ordenes.Procedencia, Gerbo_Estructura_Ordenes.NumCuentaEstoque, " +
                    " Gerbo_Estructura_Ordenes.NumCuentaProd, Gerbo_Estructura_Ordenes.OrigenMercadoria, Gerbo_Estructura_Ordenes.ClasificacionFiscal, " +
                    " Gerbo_Estructura_Ordenes.ControlNumSerie,  Gerbo_Estructura_Ordenes.ControlNumLote, Gerbo_Estructura_Ordenes.OrdenProdNatural," +
                    " Gerbo_Estructura_Ordenes.PesoBruto, Gerbo_Estructura_Ordenes.PesoLiquido,  Gerbo_Estructura_Ordenes.TipoDocumento, " +
                    " Gerbo_Estructura_Ordenes.TipoDocumentoPadre, Gerbo_Estructura_Ordenes.IdUnidadNegocio,Gerbo_Estructura_Ordenes.NumCtaICMS, Gerbo_Estructura_Ordenes.NumCtaIPI,  " +
                    " Gerbo_Estructura_Ordenes.NumCtaVenda, Gerbo_Estructura_Ordenes.NumCtaCustoVenda, Gerbo_Estructura_Ordenes.CodTipoItem," +
                    "  item_planta.item_planta_id "+
                    " FROM            Gerbo_Estructura_Ordenes CROSS JOIN " +
                    "      Accesorios_Nivel RIGHT OUTER JOIN " +
                    "      Accesorios_Maestro_Nomenclaturas ON Accesorios_Nivel.Nivel = Accesorios_Maestro_Nomenclaturas.Nivel RIGHT OUTER JOIN " +
                    "      Accesorios_Codigos ON Accesorios_Maestro_Nomenclaturas.Nomenclatura = Accesorios_Codigos.Nomenclatura RIGHT OUTER JOIN " +
                    "      item_planta ON Accesorios_Codigos.Acc_Id_ItemPlanta = item_planta.item_planta_id " +
                    " WHERE(Gerbo_Estructura_Ordenes.IdUnidadNegocio = 3) " +
                    " AND(item_planta.item_planta_id =" + @IdItemPlanta + " ) " +
                    " AND(Gerbo_Estructura_Ordenes.Procedencia = 'C')";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTAR SolMp en forsa 
        public SqlDataReader consultarSolicMpForsa(int IdMpSol)
        {
            string sql;
            sql = " SELECT       1 AS CodEmpresa, Orden.Ord_Tipo_Op, isnull(Orden.Op_UnoEE,0) , Mp_sol_Det.Sol_Mp_Det_MpId AS IdItemGerbo, "+
                  " 300 AS Ccostos, GETDATE() AS Fecha, CAST(CASE WHEN Materia_Prima.UnidadMedidad = 'UND' THEN 1 ELSE Materia_Prima.Mp_Peso_UnoEE END * Mp_sol_Det.Sol_Mp_Det_Cant_E AS decimal(18, 2)) AS CantPeso, " +
                  " Mp_Sol.Mp_Sol_Id, Mp_sol_Det.Sol_Mp_Det_Id , Materia_Prima.UnidadMedidad FROM            Mp_Sol INNER JOIN   Mp_sol_Det ON Mp_Sol.Mp_Sol_Id = Mp_sol_Det.Sol_Mp_Det_SolMp_Id " +
                  " INNER JOIN  Orden ON Mp_Sol.Mp_Sol_IdOfa = Orden.Id_Ofa INNER JOIN "+
                  "  Materia_Prima ON Mp_sol_Det.Sol_Mp_Det_MpId = Materia_Prima.Mp_Cod_UnoEE AND Mp_Sol.planta_id = Materia_Prima.Planta_Id "+
                  " WHERE(Mp_sol_Det.Sol_Mp_Det_Anula = 0) AND (Materia_Prima.Mp_InActivo = 0) AND(Mp_Sol.Mp_Sol_Id ="+ IdMpSol + ") AND (Mp_sol_Det.IdRequisicion = 0)  order by Mp_sol_Det.Sol_Mp_Det_Id DESC ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTAR items Acc de la raya
        public SqlDataReader consultarAccRaya(int IdRaya)
        {
            string sql;
            sql = " SELECT   1 AS CodEmpresa, Orden.Ord_Tipo_Op, ISNULL(Orden.Op_UnoEE, 0) AS Op, Of_Accesorios.Id_UnoE AS IdItemGerbo, "+
                    " 300 AS Ccostos, GETDATE() AS Fecha, Of_Accesorios.Cant_Req AS CantPeso, Of_Accesorios.Id_Ofa AS IdRaya, "+
                    " Of_Accesorios.Id_Orden_Acce AS IdofAcc, 'UND' AS UnidadMedida FROM Orden INNER JOIN "+
                    " Of_Accesorios ON Orden.Id_Ofa = Of_Accesorios.Id_Ofa "+
                    " WHERE(Orden.planta_id = 3) AND(Of_Accesorios.Id_Ofa ="+ IdRaya+ ") AND(Of_Accesorios.Anula = 0) AND (Of_Accesorios.IdRequisicion = 0) " +
                    " ORDER BY Orden.Id_Ofa DESC ";
            
            return BdDatos.ConsultarConDataReader(sql);
        }


        public DataSet consultarSolicMpForsaDst(int IdMpSol)
        {
            string sql;
            sql = " SELECT        1 AS CodEmpresa, Orden.Ord_Tipo_Op, isnull(Orden.Op_UnoEE,0) , Mp_sol_Det.Sol_Mp_Det_MpId AS IdItemGerbo, " +
                  " 300 AS Ccostos, GETDATE() AS Fecha,  CAST(Materia_Prima.Mp_Peso_UnoEE * Mp_sol_Det.Sol_Mp_Det_Cant_E AS decimal(18, 2)) AS CantPeso, " +
                  " Mp_Sol.Mp_Sol_Id FROM            Mp_Sol INNER JOIN   Mp_sol_Det ON Mp_Sol.Mp_Sol_Id = Mp_sol_Det.Sol_Mp_Det_SolMp_Id " +
                  " INNER JOIN  Orden ON Mp_Sol.Mp_Sol_IdOfa = Orden.Id_Ofa INNER JOIN " +
                  "  Materia_Prima ON Mp_sol_Det.Sol_Mp_Det_MpId = Materia_Prima.Mp_Cod_UnoEE AND Mp_Sol.planta_id = Materia_Prima.Planta_Id " +
                  " WHERE(Mp_sol_Det.Sol_Mp_Det_Anula = 0) AND(Materia_Prima.Mp_InActivo = 0) AND(Mp_Sol.Mp_Sol_Id =" + IdMpSol + ")";

            DataSet SolMp = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return SolMp ;
        }

        //CONSULTAR cantidad de componentes de la sol de materia prima
        public SqlDataReader consultarCantCompSolMp(int IdMpSol)
        {
            string sql;
            sql = " SELECT  COUNT(1) AS Cant , max( isnull(Orden.Op_UnoEE,0)), max(Orden.Ofa) as Ofa FROM Mp_Sol INNER JOIN Mp_sol_Det ON Mp_Sol.Mp_Sol_Id = " +
                    " Mp_sol_Det.Sol_Mp_Det_SolMp_Id INNER JOIN  Orden ON Mp_Sol.Mp_Sol_IdOfa = Orden.Id_Ofa INNER JOIN " +
                    " Materia_Prima ON Mp_sol_Det.Sol_Mp_Det_MpId = Materia_Prima.Mp_Cod_UnoEE AND Mp_Sol.planta_id = " +
                    " Materia_Prima.Planta_Id WHERE(Mp_sol_Det.Sol_Mp_Det_Anula = 0) AND(Materia_Prima.Mp_InActivo = 0) AND (Mp_Sol.Mp_Sol_Anula = 0) " +
                    " AND(Mp_Sol.Mp_Sol_Id =" + IdMpSol + ")";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTAR cantidad de componentes solicitada
        public SqlDataReader consultarCantSolicitadaCompSolMp(int IdMpSol)
        {
            string sql;
            sql = " SELECT  COUNT(1) AS Cant , max( isnull(Orden.Op_UnoEE,0)) ,max( Orden.ofa) as Raya FROM Mp_Sol INNER JOIN Mp_sol_Det ON Mp_Sol.Mp_Sol_Id = " +
                    " Mp_sol_Det.Sol_Mp_Det_SolMp_Id INNER JOIN  Orden ON Mp_Sol.Mp_Sol_IdOfa = Orden.Id_Ofa INNER JOIN " +
                    " Materia_Prima ON Mp_sol_Det.Sol_Mp_Det_MpId = Materia_Prima.Mp_Cod_UnoEE AND Mp_Sol.planta_id = " +
                    " Materia_Prima.Planta_Id WHERE(Mp_sol_Det.Sol_Mp_Det_Anula = 0) AND(Materia_Prima.Mp_InActivo = 0) " +
                    " AND (Mp_Sol.Mp_Sol_Id =" + IdMpSol + ") AND (IdRequisicion <> 0) ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTAR cantidad de componentes solicitada
        public SqlDataReader consultarFaltAccReq(int IdRayaAcc)
        {
            string sql;
            sql = " SELECT  SUM(1) - SUM(CASE WHEN Of_Accesorios.IdRequisicion > 0 THEN 1 ELSE 0 END) AS Pendiente, Orden.Id_Ofa "+
                    " FROM Orden INNER JOIN Of_Accesorios ON Orden.Id_Ofa = Of_Accesorios.Id_Ofa WHERE(Of_Accesorios.Anula = 0) "+
                    " AND (Of_Accesorios.Id_Ofa = "+IdRayaAcc+" ) GROUP BY Orden.Id_Ofa ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTAR cantidad de items a crear
        public SqlDataReader consultarCantItemsCrearBd()
        {
            string sql;
            sql = " SELECT  COUNT(Codigos_Id) AS Cantidad FROM Accesorios_Codigos WHERE(Id_UnoE = -1) AND(planta_id = 3) AND (Acc_Anulado = 0)";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTAR cantidad de items a crear
        public SqlDataReader consultarItemsItemPlantaBd()
        {
            string sql;
            sql = " SELECT  Acc_Id_ItemPlanta FROM   Accesorios_Codigos WHERE  (Id_UnoE = - 1) AND (planta_id = 3) AND (Acc_Anulado = 0)";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //CONSULTAR item de gerbo
        public SqlDataReader ConsultarItemGerbo(string nombreItem)
        {
            string sql;
            sql = "SELECT rtrim(COD_PRODUTO) AS IdItem, rtrim(DES_PRODUTO) AS Item FROM TPRODUTO "+
                    " where rtrim(DES_PRODUTO) = UPPER('" + nombreItem+ "') or rtrim(DES_PRODUTO) = '" + nombreItem + "' ";
            
            return BdDatos.ConsultarConDataReaderGerbo(sql);
        }

        //CONSULTAR op con el id de itemgerbo en gerbo
        public SqlDataReader ConsultarIdOp(string IdItemGerbo)
        {
            string sql;
            sql = "SELECT  TORDEMPROD.NUM_CHAVE, TORDEMPROD.COD_DOCOP, ltrim(TORDEMPROD.NUM_ORDEMPROD), TORDEMPROD.COD_PRODUTO "+
                  " FROM TORDEMPROD INNER JOIN  TPRODUTO ON TORDEMPROD.COD_EMPRESA = TPRODUTO.COD_EMPRESA AND " +
                  " TORDEMPROD.COD_EMPRESA = TPRODUTO.COD_EMPRESA AND TORDEMPROD.TIP_PRODUTO = TPRODUTO.TIP_PRODUTO AND " +
                  " TORDEMPROD.COD_PRODUTO = TPRODUTO.COD_PRODUTO  WHERE(RTRIM(TORDEMPROD.COD_PRODUTO) ='"+ IdItemGerbo+"')";
            return BdDatos.ConsultarConDataReaderGerbo(sql);
        }

        //CONSULTAR op Padre con nombre de nivel anterior 
        public SqlDataReader ConsultarIdOpPdre(string nombreItemPadre)
        {
            string sql;
            sql = " SELECT        TORDEMPROD.NUM_CHAVE, TORDEMPROD.COD_DOCOP, TORDEMPROD.NUM_ORDEMPROD "+
                  " FROM            TPRODUTO INNER JOIN TORDEMPROD ON TPRODUTO.COD_EMPRESA = TORDEMPROD.COD_EMPRESA "+
                  " AND TPRODUTO.COD_EMPRESA = TORDEMPROD.COD_EMPRESA AND TPRODUTO.TIP_PRODUTO = TORDEMPROD.TIP_PRODUTO AND "+
                  " TPRODUTO.COD_PRODUTO = TORDEMPROD.COD_PRODUTO WHERE(rtrim(TPRODUTO.DES_PRODUTO) = '" + nombreItemPadre+"')";
            return BdDatos.ConsultarConDataReaderGerbo(sql);
        }

        //actualizo los id de item abuelo de gerbo en la bd de forsa
        public int actualizarItemAbueloEnForsa(int idRaya, int IditemAbuelo)
        {
            string sql;
            string fecha = System.DateTime.Now.ToShortDateString();

            sql = "update Orden_Seg set Orden_Seg.Item_1EE_Abuelo =" + IditemAbuelo +
                  " FROM Orden_Seg INNER JOIN  Orden ON Orden_Seg.Id_Seg_Of = Orden.ordenseg_id " +
                  " WHERE(Orden.Id_Ofa = "+ idRaya+")";

            return BdDatos.Actualizar(sql);
        }

        //actualizo los id de item abuelo de gerbo en la bd de forsa
        public int actualizarItemPlantaAccCodBd(int IdItemPlanta, int CodErp)
        {
            string sql;
            string fecha = System.DateTime.Now.ToShortDateString();

            sql = "update item_planta set cod_erp = " + CodErp + " where  item_planta_id = " + @IdItemPlanta + " ;  " +
                  "update Accesorios_Codigos set Id_UnoE = " + CodErp + " where Acc_Id_ItemPlanta = " + @IdItemPlanta + "  ;  ";

            return BdDatos.Actualizar(sql);
        }

        //actualizo los el detalle de solmp
        public int actualizarReqEnMpSolDet(int IdDetalle, int IdRequisision, decimal valorEnviado, int idMpSol)
        {
            string sql;
            string fecha = System.DateTime.Now.ToShortDateString();

            sql = " update Mp_sol_Det set Sol_Mp_Det_Cant_R = Sol_Mp_Det_Cant_E,  ValorEnviado = " + valorEnviado + ", IdRequisicion =" + IdRequisision + ",  Sol_Mp_Det_Sci = "+ IdRequisision + " WHERE (Sol_Mp_Det_Id = " + IdDetalle + ") ; ";
                 
            return BdDatos.Actualizar(sql);
        }

        //actualizo  el detalle de OfAcc
        public int actualizarReqEnOfAcc(int IdDetalle, int IdRequisision)
        {
            string sql;
            string fecha = System.DateTime.Now.ToShortDateString();

            sql = " update Of_Accesorios set IdRequisicion =" + IdRequisision + " where Id_Orden_Acce = " + IdDetalle ;            

            return BdDatos.Actualizar(sql);
        }

        //actualizo el id en mpsol
        public int actualizarReqEnMpSol( int IdRequisision , int idMpSol)
        {
            string sql;
            string fecha = System.DateTime.Now.ToShortDateString();

            sql = " update Mp_sol set Mp_Sol_Sci = " + IdRequisision + " WHERE (Mp_Sol_Id = " + idMpSol + ") ; ";

            return BdDatos.Actualizar(sql);
        }

        //actualizo el id en mpsol
        public int actualizarFechaAprobaEnMpSol(int IdRequisision, int idMpSol)
        {
            string sql;
            string fecha = System.DateTime.Now.ToShortDateString();

            sql = " update orden set Orden.Op_Sol_Apro_Fecha = '" + fecha + "' FROM Mp_Sol INNER JOIN Orden " +
                  " ON Mp_Sol.Mp_Sol_IdOfa = Orden.Id_Ofa WHERE(Orden.planta_id = 3) AND(Mp_Sol.Mp_Sol_Id = " + idMpSol + ") ; ";

            return BdDatos.Actualizar(sql);
        }

        //actualizo el log de respuesta ws gerbo en la bd de forsa
        public int actualizarLogWsGerboEnForsa(string log, int idRaya,string metodo)
        {
            string sql;
            string fecha = System.DateTime.Now.ToShortDateString();

            sql = "INSERT INTO Gerbo_resulWs (ResultadoWs , IdRaya, Metodo) VALUES('"+log+ "', "+idRaya+" , '"+ metodo +"' )";           

            return BdDatos.Actualizar(sql);
        }
        //actualizo los id de item abuelo de gerbo en la bd de forsa
        public int actualizarOpAbueloEnForsa(int idRaya, int IdOpAbuelo)
        {
            string sql;
            string fecha = System.DateTime.Now.ToShortDateString();

            sql = "update Orden_Seg set Orden_Seg.OP_1EE_Abuelo =" + IdOpAbuelo +
                  " FROM Orden_Seg INNER JOIN  Orden ON Orden_Seg.Id_Seg_Of = Orden.ordenseg_id " +
                  " WHERE(Orden.Id_Ofa = " + idRaya + ")";

            return BdDatos.Actualizar(sql);
        }

        //actualizo los id de item Od de gerbo en forsa
        public int actualizarItemOdEnForsa(int idRaya, int IditemOd, int unidadNegocio)         
        {
            string sql, campo= "";
            string fecha = System.DateTime.Now.ToShortDateString();

            if (unidadNegocio == 2) campo = "ND_1EE";
            if (unidadNegocio == 3) campo = "Item_Ac_Com";            

            sql = "update Orden_Seg set " + campo + " = " + IditemOd +
                    " FROM Orden_Seg INNER JOIN  Orden ON Orden_Seg.Id_Seg_Of = Orden.ordenseg_id " +
                    " WHERE(Orden.Id_Ofa = " + idRaya + ")";

            return BdDatos.Actualizar(sql);
        }

        //actualizo los id de Op Od de gerbo en forsa
        public int actualizarOpOdEnForsa(int idRaya, int IdOpOd, int unidadNegocio)
        {
            string sql, campo = "";
            string fecha = System.DateTime.Now.ToShortDateString();

            if (unidadNegocio == 2) campo = "OP_1EE";
            if (unidadNegocio == 3) campo = "OP_Ac_Com";

            sql = "update Orden_Seg set " + campo + " = " + IdOpOd +
                    " FROM Orden_Seg INNER JOIN  Orden ON Orden_Seg.Id_Seg_Of = Orden.ordenseg_id " +
                    " WHERE(Orden.Id_Ofa = " + idRaya + ")";

            return BdDatos.Actualizar(sql);
        }

        //actualizo los id de item OP de gerbo en forsa
        public int actualizarItemOpEnForsa(int idRaya, int IditemOp)
        {
            string sql;
            string fecha = System.DateTime.Now.ToShortDateString();            

            sql = "update Orden set Ord_item_1ee = " + IditemOp + " WHERE (Orden.Id_Ofa = " + idRaya + ")";

            return BdDatos.Actualizar(sql);
        }

        //actualizo los id de  OP de gerbo en forsa
        public int actualizarOpEnForsa(int idRaya, int IditemOp)
        {
            string sql;
            string fecha = System.DateTime.Now.ToShortDateString();

            sql = "update Orden set Op_UnoEE = " + IditemOp + " WHERE (Orden.Id_Ofa = " + idRaya + ")";

            return BdDatos.Actualizar(sql);
        }

    }
}
