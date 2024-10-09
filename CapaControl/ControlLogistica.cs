using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using CapaDatos;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace CapaControl
{
    public class ControlLogistica
    {
        InfoAyudantes ObjInfoAyud = new InfoAyudantes();
        //CONSUTAL DE LA TABLA LOGICA---------------------------------------------------------
        public ELogcappeso buscar_Tamaño(String campo, String valor)
        {
            ELogcappeso log = null;
            string sql;
            sql = "SELECT * FROM log_cap_peso where " + campo + " = " + valor + " ";
            DataTable Table = BdDatos.CargarTabla(sql);
            foreach (DataRow row in Table.Rows)
            {
                log = new ELogcappeso();
                log.tamano = int.Parse(row["log_cap_tamano"].ToString());
                log.tipo = int.Parse(row["log_cap_tipo"].ToString());
                log.validar = row["log_cap_validar"].ToString();
                log.llave = int.Parse(row["log_cap_llave"].ToString());
                log.tabla = row["log_cap_tabla"].ToString();
                log.strCarga = row["log_str_carga"].ToString();
            }
            return log;
        }
        //CONSUTAL DE LA TABLA LOGICA---------------------------------------------------------
        /*-------------------------------------SELECT DE ACCESORIOS------------------------ */
        public InfoEstiba buscarAccesorios(long llave)
        {
            InfoEstiba InfoEst = null;
            string sql;
            sql = " SELECT     Orden.Numero + '-' + Orden.ano AS orden, pais.pai_nombre AS pais, ciudad.ciu_nombre AS ciudad, pallet_acc_1.pallet_acc_numero AS numpallet, solicitud_facturacion.Sf_cliente AS nombrecliente, solicitud_facturacion.Sf_dir_obra_desp AS direccion, pallet_acc_1.pallet_acc_cant AS cantidad, pallet_acc_1.pallet_acc_EtiqM AS codigoBarr, pallet_acc_1.pallet_acc_EtiqH AS codigoBarrC, pallet_acc_1.pallet_acc_Peso_pallet AS pesoPallet, " +
                  " (SELECT     COUNT(pallet_acc_id_of_p) AS Expr1 " +
                  " FROM          pallet_acc " +
                  " WHERE      (pallet_acc_id_of_p = Orden.Id_Ofa)) AS cantAccesorios, " +
                  " (SELECT     COUNT(pallet_al_Id_ofa) AS Expr1 " +
                  " FROM          pallet_aluminio " +
                  " WHERE      (pallet_al_Id_ofa = Orden.Id_Ofa)) AS cantAluminio, pallet_acc_1.pallet_acc_peso AS peso, pallet_acc_1.pallet_acc_ancho AS ancho, " +
                  " pallet_acc_1.pallet_acc_largo AS largo, pallet_acc_1.pallet_acc_alto AS alto, pallet_acc_1.pallet_acc_Vol AS volumen, " +
                  " pallet_acc_1.pallet_acc_id AS idpallet , CASE WHEN pallet_acc_1.pallet_acc_PS = 1 THEN 'S' + '' + RIGHT(cast (pallet_acc_1.pallet_acc_Desc as varchar(max)), 2) ELSE cast (pallet_acc_1.pallet_acc_numero as varchar(max))  END  AS numero, rtrim(orden.Tipo_Of) as tipoOrden " +
                  " FROM            Acc_Remision INNER JOIN  " +
                     "    pallet_acc AS pallet_acc_1 INNER JOIN " +
                     "    formato_unico INNER JOIN " +
                     "    Orden ON formato_unico.fup_id = Orden.Yale_Cotiza INNER JOIN " +
                     "    cliente ON formato_unico.fup_cli_id = cliente.cli_id ON pallet_acc_1.pallet_acc_id_of_p = Orden.Id_Ofa ON Acc_Remision.Rem_Acc_Pallet_Id = pallet_acc_1.pallet_acc_id INNER JOIN " +
                     "    solicitud_facturacion ON Orden.Yale_Cotiza = solicitud_facturacion.Sf_fup_id AND Orden.ord_version = solicitud_facturacion.Sf_version AND Orden.ord_parte = solicitud_facturacion.Sf_parte INNER JOIN " +
                     "    obra ON formato_unico.fup_obr_id = obra.obr_id INNER JOIN " +
                     "    ciudad ON obra.obr_ciu_id = ciudad.ciu_id INNER JOIN " +
                     "    pais ON obra.obr_pai_id = pais.pai_id" +
                  " WHERE     (Acc_Remision.Rem_Acc_id = " + llave + ") ";
            //sql = " SELECT     Orden.Numero + '-' + Orden.ano AS orden, pais.pai_nombre AS pais, ciudad.ciu_nombre AS ciudad, pallet_acc_1.pallet_acc_numero AS numpallet, solicitud_facturacion.Sf_cliente AS nombrecliente, solicitud_facturacion.Sf_dir_obra_desp AS direccion, pallet_acc_1.pallet_acc_cant AS cantidad, pallet_acc_1.pallet_acc_EtiqM AS codigoBarr, pallet_acc_1.pallet_acc_EtiqH AS codigoBarrC, pallet_acc_1.pallet_acc_Peso_pallet AS pesoPallet, " +
            //      " (SELECT     COUNT(pallet_acc_id_of_p) AS Expr1 " +
            //      " FROM          pallet_acc " +
            //      " WHERE      (pallet_acc_id_of_p = Orden.Id_Ofa)) AS cantAccesorios, " +
            //      " (SELECT     COUNT(pallet_al_Id_ofa) AS Expr1 " +
            //      " FROM          pallet_aluminio " +
            //      " WHERE      (pallet_al_Id_ofa = Orden.Id_Ofa)) AS cantAluminio, pallet_acc_1.pallet_acc_peso AS peso, pallet_acc_1.pallet_acc_ancho AS ancho, " +
            //      " pallet_acc_1.pallet_acc_largo AS largo, pallet_acc_1.pallet_acc_alto AS alto, pallet_acc_1.pallet_acc_Vol AS volumen, " +
            //      " pallet_acc_1.pallet_acc_id AS idpallet , CASE WHEN pallet_acc_1.pallet_acc_PS = 1 THEN 'S' + '' + RIGHT(cast (pallet_acc_1.pallet_acc_Desc as varchar(max)), 2) ELSE cast (pallet_acc_1.pallet_acc_numero as varchar(max))  END  AS numero " +
            //      " FROM         ciudad INNER JOIN " +
            //      "     pais INNER JOIN " +
            //      "     formato_unico INNER JOIN " +
            //      "     Orden ON formato_unico.fup_id = Orden.Yale_Cotiza INNER JOIN " +
            //      "     cliente ON formato_unico.fup_cli_id = cliente.cli_id ON pais.pai_id = cliente.cli_pai_id ON ciudad.ciu_id = cliente.cli_ciu_id INNER JOIN " +
            //      "     pallet_acc AS pallet_acc_1 ON Orden.Id_Ofa = pallet_acc_1.pallet_acc_id_of_p INNER JOIN " +
            //      "     Acc_Remision ON pallet_acc_1.pallet_acc_id = Acc_Remision.Rem_Acc_Pallet_Id INNER JOIN " +
            //      "     solicitud_facturacion ON Orden.Yale_Cotiza = solicitud_facturacion.Sf_fup_id AND Orden.ord_version = solicitud_facturacion.Sf_version AND " +
            //      "     Orden.ord_parte = solicitud_facturacion.Sf_parte" +
            //      " WHERE     (Acc_Remision.Rem_Acc_id = " + llave + ") ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                InfoEst = new InfoEstiba();
                InfoEst.NumP = row["numero"].ToString();
                InfoEst.Orden = row["orden"].ToString();
                InfoEst.Pais = row["pais"].ToString();
                InfoEst.Ciudad = row["ciudad"].ToString();
                InfoEst.Numpallet = int.Parse(row["numpallet"].ToString());
                InfoEst.Peso = float.Parse(row["peso"].ToString());
                InfoEst.Ancho = float.Parse(row["ancho"].ToString());
                InfoEst.Largo = float.Parse(row["largo"].ToString());
                InfoEst.Alto = float.Parse(row["alto"].ToString());
                InfoEst.Volumen = float.Parse(row["volumen"].ToString());
                InfoEst.Direccion = row["direccion"].ToString();
                InfoEst.Nombrecliente = row["nombrecliente"].ToString();
                InfoEst.CantiP = int.Parse(row["cantidad"].ToString());
                InfoEst.CodBarra = row["codigoBarr"].ToString();
                InfoEst.CodBarraC = row["codigoBarrC"].ToString();
                InfoEst.PesoPallet = int.Parse(row["pesoPallet"].ToString());
                InfoEst.Material = "ACCESORIOS";
                InfoEst.Idpallet = int.Parse(row["idpallet"].ToString());
                InfoEst.TipoOrden =row["tipoOrden"].ToString(); 
            }
            return InfoEst;
        }
        /*-------------------------------------SELECT DE ACCESORIOS------------------------ */
        /*-------------------------------------SELECT DE ALUMINIO------------------------ */
        public InfoEstiba buscarAluminio(int idOfpa, int numPallet)
        {
            InfoEstiba InfoEst = null;
            string sql;
            sql = "SELECT   Orden.Numero + '-' + Orden.ano AS orden, pais.pai_nombre AS pais, ciudad.ciu_nombre AS ciudad, pallet_aluminio_1.pallet_al_numero AS numpallet, solicitud_facturacion.Sf_cliente AS nombrecliente,  solicitud_facturacion.Sf_dir_obra_desp AS direccion, pallet_aluminio_1.pallet_al_cant AS cantidad, pallet_aluminio_1.pallet_al_BarEtiM AS codigoBarr, pallet_aluminio_1.pallet_al_BarEtiH AS codigoBarrC, pallet_aluminio_1.pallet_al_peso_pallet  AS pesoPallet, " +
                    " case when pallet_aluminio_1.pallet_tipo_pieza = 'FA' then 'ACERO MADERA' " +
		            " when pallet_aluminio_1.pallet_tipo_pieza = 'FP' then 'ALUMINIO' else 'ALUMINIO' end as Material ," +
                  "          (SELECT     COUNT(pallet_acc_id_of_p) AS Expr1 " +
                  "          FROM          pallet_acc  " +
                  "          WHERE      (pallet_acc_id_of_p =" + idOfpa + ")) AS cantAccesorios, " +
                  "          (SELECT     COUNT(pallet_al_Id_ofa) AS Expr1 " +
                  "          FROM          pallet_aluminio " +
                  "          WHERE      (pallet_al_Id_ofa = " + idOfpa + ")) AS cantAluminio, pallet_aluminio_1.pallet_al_peso AS peso, pallet_aluminio_1.pallet_al_Anc AS ancho, " +
                  "          pallet_aluminio_1.pallet_al_Largo AS largo, pallet_aluminio_1.pallet_al_Alto AS alto, pallet_aluminio_1.pallet_al_Vol AS volumen,  " +
                  "          pallet_aluminio_1.pallet_al_id AS idpallet , rtrim(orden.Tipo_Of) as tipoOrden " +
                  " FROM            obra INNER JOIN "+
                      "   solicitud_facturacion INNER JOIN "+
                      "  formato_unico INNER JOIN "+
                      "   Orden ON formato_unico.fup_id = Orden.Yale_Cotiza INNER JOIN "+
                      "   cliente ON formato_unico.fup_cli_id = cliente.cli_id ON solicitud_facturacion.Sf_fup_id = Orden.Yale_Cotiza AND solicitud_facturacion.Sf_version = Orden.ord_version AND  "+
                      "   solicitud_facturacion.Sf_parte = Orden.ord_parte ON obra.obr_id = formato_unico.fup_obr_id INNER JOIN "+
                      "   ciudad ON obra.obr_ciu_id = ciudad.ciu_id INNER JOIN "+
                      "   pais ON obra.obr_pai_id = pais.pai_id LEFT OUTER JOIN " +
                      "   pallet_aluminio AS pallet_aluminio_1 ON Orden.Id_Ofa = pallet_aluminio_1.pallet_al_Id_ofa" +
                  " WHERE     (Orden.Id_Ofa =" + idOfpa + ") AND (pallet_aluminio_1.pallet_al_numero = " + numPallet + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                InfoEst = new InfoEstiba();
                InfoEst.NumP = row["numpallet"].ToString();
                InfoEst.Orden = row["orden"].ToString();
                InfoEst.Pais = row["pais"].ToString();
                InfoEst.Ciudad = row["ciudad"].ToString();
                InfoEst.Numpallet = int.Parse(row["numpallet"].ToString());
                InfoEst.Peso = float.Parse(row["peso"].ToString());
                InfoEst.Ancho = float.Parse(row["ancho"].ToString());
                InfoEst.Largo = float.Parse(row["largo"].ToString());
                InfoEst.Alto = float.Parse(row["alto"].ToString());
                InfoEst.Volumen = float.Parse(row["volumen"].ToString());
                InfoEst.Direccion = row["direccion"].ToString();
                InfoEst.Nombrecliente = row["nombrecliente"].ToString();
                InfoEst.CantiP = int.Parse(row["cantidad"].ToString());
                InfoEst.CodBarra = row["codigoBarr"].ToString();
                InfoEst.CodBarraC = row["codigoBarrC"].ToString();
                InfoEst.PesoPallet = int.Parse(row["pesoPallet"].ToString());
                InfoEst.Material = row["Material"].ToString();
                InfoEst.TipoOrden = row["tipoOrden"].ToString(); 
            }
            return InfoEst;
        }
        
        /*-------------------------------------SELECT DE ALUMINIO------------------------ */
        /*-------------------------------------UPDATE DE ALUMINIO------------------------ */
        public int actualizarDatosAluminio(long IdOfpallet, int numPallet, string peso, string ancho, string alto, string largo, string vol, int Estado, String usuario, string codigo)
        {
            String sql = "UPDATE pallet_aluminio SET pallet_al_peso = " + peso + ",  pallet_al_Anc = " + ancho + " ,  pallet_al_Largo = " + largo + " , pallet_al_Alto = " + alto + " , pallet_al_Vol = " + vol + " , fecha_actualiza = SYSDATETIME() ,  pallet_al_estado_id = " + Estado + ", usu_actualiza = '" + usuario + "', pallet_al_cod_usu_peso = '"+codigo+"'" +
                        "WHERE pallet_al_numero = " + numPallet + " and pallet_al_Id_ofa = '" + IdOfpallet + "' ";
            return BdDatos.Actualizar(sql);
        }
        /*-------------------------------------UPDATE DE ALUMINIO------------------------ */
        /*-------------------------------------UPDATE DE ACCESORIOS------------------------ */
        public int actualizarDatosAccesorios(String idOrden, int numPallet, string peso, string ancho, string alto, string largo, string vol, int Estado, String usuario, int idpallet, string codigo)
        {
            String sql = "UPDATE pallet_acc SET pallet_acc_peso = " + peso + ", pallet_acc_pesoB = " + peso + ",  pallet_acc_ancho = " + ancho + " ,  pallet_acc_largo = " + largo + " , pallet_acc_alto = " + alto + " , pallet_acc_Vol = " + vol + ", fecha_actualiza = SYSDATETIME(),  pallet_acc_estado_id = " + Estado + ", usu_actualiza = '" + usuario + "', pallet_acc_cod_usu_peso = '" + codigo + "' " +
                        "WHERE pallet_acc_id = "+ idpallet+" and pallet_acc_numero = " + numPallet + "";
            return BdDatos.Actualizar(sql);
        }
        /*-------------------------------------UPDATE DE ACCESORIOS------------------------ */
        /*-------------------------------------BUSCAR DESPACHOS------------------------ */
        public List<InfoOrden> buscarDespachos(String numOrden)
        {
            List<InfoOrden> lInfoOrden = new List<InfoOrden>();
            String sql = " SELECT     Orden.Numero + '-' + Orden.ano AS orden, Orden.Id_Of_P AS idofa, cliente.cli_nombre AS cliente, pais.pai_nombre AS pais, " +
                         "            ciudad.ciu_nombre AS ciudad " +
                         " FROM       Orden INNER JOIN " +
                         "            cliente INNER JOIN " +
                         "            formato_unico ON cliente.cli_id = formato_unico.fup_cli_id ON Orden.Yale_Cotiza = formato_unico.fup_id INNER JOIN " +
                         "            ciudad INNER JOIN " +
                         "            pais INNER JOIN " +
                         "            Despa_Cliente ON pais.pai_id = Despa_Cliente.DesC_PaisId ON ciudad.ciu_id = Despa_Cliente.DesC_CiudadId ON  " +
                         "            Orden.Id_Ofa = Despa_Cliente.DesC_OfPId " +
                         " WHERE      (Orden.Ofa LIKE '%" + numOrden.ToString() + "%') ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            //Obtener idofa (ID Orden)
            InfoOrden infoO = null;
            foreach (DataRow row in consulta.Rows)
            {
                infoO = new InfoOrden();
                infoO.Orden = row["orden"].ToString();
                infoO.Pais = row["pais"].ToString();
                infoO.Ciudad = row["ciudad"].ToString();
                infoO.Idofa = int.Parse(row["idofa"].ToString());
                infoO.Cliente = row["cliente"].ToString();
            }
            if (infoO != null)
            {
                sql = " SELECT     Orden.Numero + '-' + Orden.ano AS orden, Despa_Cliente.DesC_DespNo AS NoDespacho, Despa_Cliente.DesC_Id AS idDespacho " +
                            " FROM       Orden INNER JOIN " +
                            "            Despa_Cliente ON Orden.Id_Ofa = Despa_Cliente.DesC_OfPId " +
                            " WHERE     (Orden.Id_Ofa = " + infoO.Idofa + ") ";
                DataTable consulta_2 = BdDatos.CargarTabla(sql);
                foreach (DataRow row in consulta_2.Rows)
                {
                    InfoOrden info2 = infoO;
                    info2.Despacho = row["NoDespacho"].ToString();
                    info2.IdDespacho = int.Parse(row["idDespacho"].ToString());
                    lInfoOrden.Add(info2);
                }
            }
            return lInfoOrden;
        }
        /*-------------------------------------BUSCAR DESPACHOS------------------------ */
        /*-------------------------------------BUSCAR CONTENEDORES------------------------ */
        public List<InfoContenedor> buscarContenedores(String idDespacho)
        {
            List<InfoContenedor> LInfoConte = new List<InfoContenedor>();
            //InfoContenedor InfoContenedor = null;
            String sql = "SELECT Desp_Transporte.Desp_Trans_Id AS idTrans, Desp_Transporte.Desp_Vehi_Placa AS transPlaca, Desp_Transporte.Desp_Contenedor_Id AS idContenedor, Desp_Transporte.Desp_Abierto AS estadoAC"
                        + " FROM Despa_Cliente LEFT OUTER JOIN"
                        + " Desp_Transporte ON Despa_Cliente.DesC_Id = Desp_Transporte.Desp_Trans_DespId"
                        + " WHERE (Despa_Cliente.DesC_Id =" + idDespacho + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                InfoContenedor infoc = new InfoContenedor();
                infoc.Desp_Trans_id = int.Parse(row["idTrans"].ToString());
                infoc.TransPlaca = row["transPlaca"].ToString();
                infoc.Trans_idContenedor = int.Parse(row["idContenedor"].ToString());
                infoc.EstadoAC = row["estadoAC"].ToString();
                LInfoConte.Add(infoc);
            }
            return LInfoConte;
        }
        /*-------------------------------------BUSCAR CONTENEDORES------------------------ */
        /*-------------------------------------------------VERIFICAR LA PALLET ALUMINIO CON LA OFA COINCIDA------------------------------------------*/
        //Esta funcio verifica si encontro el pallet asociado a la orden de fabricacion en la tabla ALUMINIO ************////////
        public InfoEstiba verificaPalletvsOfaAL(int pallet, int ofa)
        {
            InfoEstiba InfoEst = null;
            string sql;
            sql = "Select * from pallet_aluminio where pallet_al_id_ofa =" + ofa + " and pallet_al_id = " + pallet + "";
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                InfoEst = new InfoEstiba();
                InfoEst.TrasnId = int.Parse(row["Pallet_Trans_Id"].ToString());
                InfoEst.Estado = int.Parse(row["pallet_al_estado_id"].ToString());
                InfoEst.Ofa = int.Parse(row["pallet_al_id_ofa"].ToString());
                InfoEst.Peso = float.Parse(row["pallet_al_peso"].ToString());
            }
            return InfoEst;
        }
        /*-------------------------------------------------VERIFICAR LA PALLET ALUMINIO CON LA OFA COINCIDA------------------------------------------*/
        //----------------------------------------------------VERIFICAR LA PALLET ACCESORIOS CON LA OFA COINCIDA--------------------------------------------//
        public InfoEstiba verificaPalletvsOfaACC(int pallet, int ofa)
        {
            InfoEstiba InfoEst = null;
            string sql;
            sql = "Select * from pallet_acc where pallet_acc_id_of_p = " + ofa + " and pallet_acc_id = " + pallet + "";
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                InfoEst = new InfoEstiba();
                InfoEst.TrasnId = int.Parse(row["pallet_acc_id_trans"].ToString());
                InfoEst.Estado = int.Parse(row["pallet_acc_estado_id"].ToString());
                InfoEst.Ofa = int.Parse(row["pallet_acc_id_of_p"].ToString());
                InfoEst.Peso = int.Parse(row["pallet_acc_pesoB"].ToString()); 
            }
            return InfoEst;
        }
        /*----------------------------------------------------VERIFICAR LA PALLET ACCESORIOS CON LA OFA COINCIDA------------------------------------------*/
        public void actualizaContenedor(int idTrans, int numEstado)
        {
            String sql = "update Desp_Transporte set Desp_Abierto = " + numEstado + " where Desp_Trans_Id = " + idTrans + "";
            BdDatos.Actualizar(sql);
        }
        public void actualizaSoloContenedor(int idTrans, int numEstado)
        {
            String sql = "update Desp_Transporte set Desp_Abierto = " + numEstado + " where Desp_Trans_Id = '" + idTrans + "'";
            BdDatos.Actualizar(sql);
        }
        /*-------------------------UPDATE DE ALUMINIO PARA ACTUALIZAR EL ID DEL TRASNPORTADOR ----------------*/
        public void actualizarContenedorAluminio(int pallet, int trans, int transIdCont)
        {
            String sql = "update pallet_aluminio set Pallet_Trans_Id = " + trans + ", pallet_al_id_container = " + transIdCont + " where  pallet_al_id = " + pallet + "";
            BdDatos.Actualizar(sql);
        }

        /*-------------------------INSERT PARA EL LOG DE CARGUE A TRANSPORTE ----------------*/
        public void actualizarLogCarguePallet(int pallet, string tipoPallet, int trans, int transIdCont,string usuario, int desasocia)
        {           
            String sql = "INSERT INTO pallet_log_cargue   ( LogTrans_IdPallet ,LogTrans_TipoPallet ,LogTrans_IdTransporte,LogTrans_IdContainer,LogTrans_Usuario,LogDesasocia)" +
                            " VALUES("+ pallet + ", '"+ tipoPallet + "',"+ trans + " ,"+ transIdCont  + ",'"+ usuario + "', "+ desasocia + ")";
            BdDatos.Actualizar(sql);
        }

        /*-------------------------UPDATE DE ALUMINIO PARA ACTUALIZAR EL ID DEL TRASNPORTADOR ----------------*/
        /*-------------------------UPDATE DE ACCESORIOS PARA ACTUALIZAR EL ID DEL TRASNPORTADOR ----------------*/
        public void actualizarContenedorAccesorios(int pallet, int trans, int transIdCont)
        {
            String sql = "update pallet_acc set pallet_acc_id_trans = " + trans + ", pallet_acc_id_container = " + transIdCont + " where  pallet_acc_id = " + pallet + "";
            BdDatos.Actualizar(sql);
        }
        /*------------------------ACTUALIZAR HORAS DEL CAMION---------------------*/
        public void actualizarHorasInicio(int idtrans)
        {
            String sql1 = "update Desp_Transporte set Desp_Trans_Ini_Cargue = SYSDATETIME() where Desp_Trans_Id =  " + idtrans + ";";
            BdDatos.Actualizar(sql1);
        }
        public void actualizarHorasFin(int idtrans)
        {
            String sql1 = "update Desp_Transporte set Desp_Trans_Fin_Cargue = SYSDATETIME() where Desp_Trans_Id =  " + idtrans + ";";
            BdDatos.Actualizar(sql1);
        }
        /*------------------------ACTUALIZAR HORAS DEL CAMION---------------------*/
        /*-------------------------UPDATE DE ACCESORIOS PARA ACTUALIZAR EL ID DEL TRASNPORTADOR ----------------*/
        public void desasociarContenedorAluminio(int pallet)
        {
            String sql = "update pallet_aluminio set Pallet_Trans_Id = 0 where pallet_al_id = " + pallet + "";
            BdDatos.Actualizar(sql);
        }
        public void desasociarContenedorAccesorios(int pallet)
        {
            String sql = "update pallet_acc set pallet_acc_id_trans = 0 , pallet_acc_id_container = 0 where pallet_acc_id = " + pallet + "";
            BdDatos.Actualizar(sql);
        }
        public DataTable cargaGriedViewAlYACC(int trans)
        {
            String sql = "SELECT pallet_al_id AS Estiba, pallet_al_peso AS Peso ,cast (pallet_al_numero as varchar(max))  AS Pallet, pallet_al_Vol AS Volumen, Pallet_al_Tipo +' '+ Orden_1.Tipo_Of +' '+pallet_al_ofa AS Piezas "
                     + " FROM pallet_aluminio INNER JOIN "
                     + " Orden AS Orden_1 INNER JOIN "
                     + " Despa_Cliente AS Despa_Cliente_1 LEFT OUTER JOIN "
                     + " Desp_Transporte AS Desp_Transporte_1 ON Despa_Cliente_1.DesC_Grp_Id = Desp_Transporte_1.Desp_Grp_Id ON  "
                     + " Orden_1.Id_Ofa = Despa_Cliente_1.DesC_OfPId ON pallet_aluminio.pallet_al_Id_ofa = Orden_1.Id_Ofa AND  "
                     + " pallet_aluminio.Pallet_Trans_Id = Desp_Transporte_1.Desp_Trans_Id "
                     + " WHERE (Orden_1.Despachada = 0) AND (Orden_1.Fec_Desp IS NULL) AND (Orden_1.Anulada = 0) AND (pallet_aluminio.pallet_al_cant > 0) AND  "
                     + " (pallet_aluminio.Pallet_Trans_Id <> 0) AND (Desp_Transporte_1.Desp_Trans_Id = " + trans + ") "
                     + " union all "
                     + " SELECT pallet_acc_id AS Estiba, pallet_acc_peso AS Peso ,cast(pallet_acc_numero  as varchar(max)) AS Pallet, pallet_acc_Vol AS Volumen, pallet_acc_tipo+' '+  Orden_1.Tipo_Of +' '+ pallet_acc_SolNo AS Piezas "
                     + " FROM Orden AS Orden_1 INNER JOIN "
                     + " Despa_Cliente AS Despa_Cliente_1 LEFT OUTER JOIN "
                     + " Desp_Transporte AS Desp_Transporte_1 ON Despa_Cliente_1.DesC_Grp_Id = Desp_Transporte_1.Desp_Grp_Id ON  "
                     + " Orden_1.Id_Ofa = Despa_Cliente_1.DesC_OfPId INNER JOIN "
                     + " pallet_acc ON Desp_Transporte_1.Desp_Trans_Id = pallet_acc.pallet_acc_id_trans AND Orden_1.Id_Ofa = pallet_acc.pallet_acc_id_of_p "
                     + " WHERE (Orden_1.Despachada = 0) AND (Orden_1.Fec_Desp IS NULL) AND (Orden_1.Anulada = 0) AND (Desp_Transporte_1.Desp_Trans_Id = " + trans + ") AND  "
                     + " (pallet_acc.pallet_acc_id_trans <> 0) AND (pallet_acc.pallet_acc_cant > 0) ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        public DataTable sumaPesoAlYACC(int trans)
        {
            String sql = "select  sum(pallet_al_peso) AS Peso"
                + " from pallet_aluminio where Pallet_Trans_Id =" + trans
                + " UNION "
                + " select  sum(pallet_acc_peso) AS Peso"
                + " from pallet_acc where pallet_acc_id_trans=" + trans;
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        public DataTable sumaPesoParaNeto(int trans)
        {
            String sql = "SELECT  sum(pallet_al_pesoN) AS Peso"
            + " FROM pallet_aluminio"
            + " WHERE (Pallet_Trans_Id =" + trans + ")"
            + " UNION"
            + " SELECT sum(pallet_acc_pesoN) AS Peso"
            + " FROM   pallet_acc"
            + " WHERE (pallet_acc_id_trans =" + trans + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public DataTable sumaVolumen(int trans)
        {
            String sql = "SELECT  sum(pallet_al_Vol) AS vol"
            + " FROM pallet_aluminio"
            + " WHERE (Pallet_Trans_Id =" + trans + ")"
            + " UNION"
            + " SELECT sum(pallet_acc_Vol) AS vol"
            + " FROM   pallet_acc"
            + " WHERE (pallet_acc_id_trans =" + trans + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }



        /*-----------------------------------CONTEO DE LOS DATOS-------------------------------------*/
        public int conteo(String sql)
        {
            int nconteo = 0;
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                nconteo = nconteo + int.Parse(row["CANT"].ToString());
            }
            return nconteo;
        }
        /*-----------------------------------CONTEO DE LOS DATOS-------------------------------------*/
        public List<InfoContenedor> estadosId(int idTrans)
        {
            List<InfoContenedor> LInfoConte2 = new List<InfoContenedor>();
            String sql = "SELECT Desp_Transporte.Desp_Abierto AS estadoAC, Desp_Transporte.Desp_Trans_Id AS idTrans, Desp_Transporte.Desp_Vehi_Placa AS transPlaca, Desp_Transporte.Desp_Contenedor_Id AS idContenedor "
                        + " FROM Despa_Cliente LEFT OUTER JOIN"
                        + " Desp_Transporte ON Despa_Cliente.DesC_Id = Desp_Transporte.Desp_Trans_DespId"
                        + " WHERE (Desp_Transporte.Desp_Trans_Id =" + idTrans + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                InfoContenedor infoc = new InfoContenedor();
                infoc.Desp_Trans_id = int.Parse(row["idTrans"].ToString());
                infoc.TransPlaca = row["transPlaca"].ToString();
                infoc.Trans_idContenedor = int.Parse(row["idContenedor"].ToString());
                infoc.EstadoAC = row["estadoAC"].ToString();
                LInfoConte2.Add(infoc);
            }
            return LInfoConte2;
        }

        /*------------------------------------------------------DESPACHO DE CARGA CONTENEDOR----------------------------------------------------------------------------------------------*/
        /*----------*/
        //busco el id del grupo
        public List<InfoContenedor> buscarDatosTrans(String placaNumcont)
        {
            List<InfoContenedor> LInfoConte = new List<InfoContenedor>();
            String sql = "SELECT Desp_Transporte.Desp_Trans_Id AS idTrans, Desp_Container.Cont_L + ' ' + Desp_Container.Cont_No + '-' + Desp_Container.Cont_V AS Numero, Desp_Transporte.Desp_Vehi_Placa AS transPlaca, Desp_Transporte.Desp_Contenedor_Id AS idContenedor, Desp_Transporte.Desp_Abierto AS estadoAC , Desp_Grp_Id AS idGrupo, Desp_Trans_Ini_Cargue AS horaInicio,CASE WHEN Despa_Cliente.DesC_Nal=1 THEN Desp_Transporte.Desp_Trans_Obs ELSE "
                            + "  Desp_Container.Cont_Observ END AS observacion "
                 + " FROM  Despa_Cliente INNER JOIN Desp_Transporte ON Despa_Cliente.DesC_Id = Desp_Transporte.Desp_Trans_DespId " +
                         " RIGHT OUTER JOIN Despa_Grupo ON Desp_Transporte.Desp_Grp_Id = Despa_Grupo.Id_Grupo " +
                         " LEFT OUTER JOIN  Desp_Container ON Desp_Transporte.Desp_Contenedor_Id = Desp_Container.Id_Contene "
                 + "  WHERE  (  Desp_Transporte.Desp_Vehi_Placa = '" + placaNumcont + "' OR"
                 + "  Desp_Container.Cont_L + ' ' + Desp_Container.Cont_No + '-' + Desp_Container.Cont_V = '" + placaNumcont + "')"
                 + "  AND(Desp_Transporte.Desp_Abierto = 1)";              
            DataTable consulta = BdDatos.CargarTabla(sql);
            if (consulta.Rows.Count > 0)
            {
                
                    foreach (DataRow row in consulta.Rows)
                    {
                        InfoContenedor infoC = new InfoContenedor();
                        infoC.Desp_Trans_id = int.Parse(row["idTrans"].ToString());
                        infoC.TransPlaca = row["transPlaca"].ToString();
                        infoC.Trans_idContenedor = int.Parse(row["idContenedor"].ToString());
                        infoC.EstadoAC = row["estadoAC"].ToString();
                        infoC.Desp_idGrupo = int.Parse(row["idGrupo"].ToString());
                        infoC.NumeroCont = row["Numero"].ToString();
                        infoC.FechaInicio = row["horaInicio"].ToString();
                        infoC.Cont_Observ = row["observacion"].ToString();
                    LInfoConte.Add(infoC);
                    }
                
            }
            return LInfoConte;
        }
        public List<InfoContenedor> buscarDatosTransRecargar(int idTrans)
        {
            List<InfoContenedor> LInfoConte = new List<InfoContenedor>();
            String sql = "SELECT Desp_Transporte.Desp_Trans_Id AS idTrans, Desp_Container.Cont_L + ' ' + Desp_Container.Cont_No + '-' + Desp_Container.Cont_V AS Numero, Desp_Transporte.Desp_Vehi_Placa AS transPlaca, Desp_Transporte.Desp_Contenedor_Id AS idContenedor, Desp_Transporte.Desp_Abierto AS estadoAC , Desp_Grp_Id AS idGrupo, Desp_Transporte.Desp_Trans_Ini_Cargue AS horaInicio"
                 + "  FROM Despa_Grupo LEFT OUTER JOIN"
                 + "  Desp_Transporte LEFT OUTER JOIN "
                 + "  Desp_Container ON Desp_Transporte.Desp_Contenedor_Id = Desp_Container.Id_Contene ON Despa_Grupo.Id_Grupo = Desp_Transporte.Desp_Grp_Id"
                 + "  WHERE (Desp_Transporte.Desp_Trans_Id = '" + idTrans + "')";
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                InfoContenedor infoC = new InfoContenedor();
                infoC.Desp_Trans_id = int.Parse(row["idTrans"].ToString());
                infoC.TransPlaca = row["transPlaca"].ToString();
                infoC.Trans_idContenedor = int.Parse(row["idContenedor"].ToString());
                infoC.EstadoAC = row["estadoAC"].ToString();
                infoC.Desp_idGrupo = int.Parse(row["idGrupo"].ToString());
                infoC.NumeroCont = row["Numero"].ToString();
                infoC.FechaInicio = row["horaInicio"].ToString();
                LInfoConte.Add(infoC);
            }
            return LInfoConte;
        }
        //busco las ordenes para cargar al combo
        public List<InfoOrden> buscarOrdenes(String idgrupo, int idTrans)
        {
            List<InfoOrden> lInfoContC = new List<InfoOrden>();
            String sql = "SELECT  Orden.Numero + '-' + Orden.ano AS orden, Orden.Id_Ofa AS idofa , orden.Tipo_Of as Tipo_Of "
                     + "  FROM Orden INNER JOIN "
                     + "  Despa_Cliente INNER JOIN "
                     + "  Despa_Grupo INNER JOIN "
                     + "  Desp_Transporte ON Despa_Grupo.Id_Grupo = Desp_Transporte.Desp_Grp_Id ON Despa_Grupo.Id_Grupo = Despa_Cliente.DesC_Grp_Id "
                     + "  ON Orden.Id_Ofa = Despa_Cliente.DesC_OfPId "
                     + "  WHERE (Desp_Transporte.Desp_Grp_Id = " + idgrupo + ") AND (Orden.Despachada = 0) AND (Orden.Fec_Desp IS NULL) AND (Orden.Anulada = 0) AND (Desp_Transporte.Desp_Trans_Id = " + idTrans + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            InfoOrden InfoClienteCont = null;
            foreach (DataRow row in consulta.Rows)
            {
                InfoClienteCont = new InfoOrden();
                InfoClienteCont.Orden = row["orden"].ToString();
                InfoClienteCont.Idofa = int.Parse(row["idofa"].ToString());
                InfoClienteCont.TipoOf = row["Tipo_Of"].ToString();
                lInfoContC.Add(InfoClienteCont);

            }
            return lInfoContC;
        }
        //busco el despacho y los demas datos
        public List<InfoOrden> buscarDespachosOr(String idofa)
        {
            List<InfoOrden> lInfoOrden = new List<InfoOrden>();
            String sql = "SELECT     Orden.Numero + '-' + Orden.ano AS orden, Orden.Id_Ofa AS idofa, cliente.cli_nombre AS cliente, pais.pai_nombre AS pais, ciudad.ciu_nombre AS ciudad,"
             + " Despa_Cliente.DesC_DespNo AS NoDespacho, Despa_Cliente.DesC_Id AS idDespacho "
             + " FROM       Orden INNER JOIN  "
             + " cliente INNER JOIN  "
             + " formato_unico ON cliente.cli_id = formato_unico.fup_cli_id ON Orden.Yale_Cotiza = formato_unico.fup_id INNER JOIN  "
             + " ciudad INNER JOIN  "
             + " pais INNER JOIN  "
             + " Despa_Cliente ON pais.pai_id = Despa_Cliente.DesC_PaisId ON ciudad.ciu_id = Despa_Cliente.DesC_CiudadId ON   "
             + " Orden.Id_Ofa = Despa_Cliente.DesC_OfPId  "
             + " WHERE      (Orden.Id_Ofa = " + idofa + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            InfoOrden infoO = null;
            foreach (DataRow row in consulta.Rows)
            {
                infoO = new InfoOrden();
                infoO.Orden = row["orden"].ToString();
                infoO.Pais = row["pais"].ToString();
                infoO.Ciudad = row["ciudad"].ToString();
                infoO.Idofa = int.Parse(row["idofa"].ToString());
                infoO.Cliente = row["cliente"].ToString();
                infoO.Despacho = row["NoDespacho"].ToString();
                infoO.IdDespacho = int.Parse(row["idDespacho"].ToString());
                lInfoOrden.Add(infoO);
            }
            return lInfoOrden;
        }
        /*-------------------------------------------------------DESPACHO DE CARGA CONTENEDOR----------------------------------------------------------------------------------------------*/

        //cosnsulto los faltantes de la orden accesorios y aluminio
        public SqlDataReader consultarFaltantesOrden(string of)
        {
            string sql;

            sql = "SELECT     'Accesorios' AS tipo, SUM(Of_Accesorios.Cant_Req) - SUM(Of_Accesorios.Cant_Enviada) AS faltantes "+
                  "  FROM         Orden INNER JOIN "+
                   "                       Of_Accesorios ON Orden.Id_Ofa = Of_Accesorios.Id_Ofa "+
                   "WHERE     (Orden.Ofa LIKE '%" + of + "%') AND (Of_Accesorios.Anula = 0)  and  (Orden.Anulada = 0) " +
                   " UNION ALL "+
                   " SELECT     'Aluminio' AS tipo,ISNULL(SUM(Cant_Final_Req) - SUM(Cant_Desp) ,0) AS faltantes " +
                   " FROM            Saldos INNER JOIN  Orden AS Orden_1 ON Saldos.Id_Ofa = Orden_1.Id_Ofa"+
                   " WHERE     (Saldos.Ofa  LIKE '" + of + "%') and  (Orden_1.Anulada = 0) AND Saldos.Anula = 0 ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //-----------


        /*---------------------------------------------AYUDANTES----------------------------------------------------------------------------------------------*/
        public DataTable cargaGriedViewAyudantes(int idCont)
        {
            String sql = "SELECT desp_ayu_id AS id, desp_ayu_cedula AS cedula, empleado.emp_nombre_mayusculas + ' ' + empleado.emp_apellidos_mayusculas AS nombre, desp_ayu_fecha AS fecha,  desp_ayu_placa AS placa"
                      + " FROM Desp_Ayudante INNER JOIN"
                      + " empleado ON empleado.emp_usu_num_id = desp_ayudante.desp_ayu_cedula"
                      + " WHERE desp_ayu_activo = 1 AND desp_ayu_idContenedor = " + idCont + "";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        public void anularAyudantes(int idAyud)
        {
            String sql = "UPDATE Desp_Ayudante SET desp_ayu_activo = 0 WHERE desp_ayu_id = " + idAyud + "";
            BdDatos.Actualizar(sql);
        }
        public void activarAyudantes(int cedula)
        {
            String sql = "UPDATE Desp_Ayudante SET desp_ayu_activo = 1 WHERE desp_ayu_cedula = " + cedula + "";
            BdDatos.Actualizar(sql);
        }
        public void eliminarAyudantes(int idAyud)
        {
            String sql = "DELETE from Desp_Ayudante where desp_ayu_id = " + idAyud + "";
            BdDatos.Actualizar(sql);
        }
        public void insertarAyudantes(String placa, int cedula, int activo, int idConte)
        {
            String sql = "INSERT INTO Desp_Ayudante VALUES(" + idConte + ", " + cedula + ",SYSDATETIME(),'" + placa + "'," + activo + ")";
            BdDatos.Actualizar(sql);
        }
        public void buscarUsuario()
        {
            String sql = "Select ";
            BdDatos.Actualizar(sql);
        }
        public InfoAyudantes buscarDatosEmpleados(int cedula)
        {
            InfoAyudantes InfoA = null;
            String sql = "SELECT emp_nombre_mayusculas + ' ' + emp_apellidos_mayusculas AS nombre, emp_usu_num_id AS cedula FROM empleado where emp_usu_num_id = " + cedula + "";
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                InfoA = new InfoAyudantes();
                InfoA.AyuNombreEmp = row["nombre"].ToString();
                InfoA.AyuCedula = int.Parse(row["cedula"].ToString());
            }
            return InfoA;
        }
        public InfoAyudantes buscarDatosEmpleados2(int cedula, int idTrans)
        {
            InfoAyudantes InfoA2 = null;
            String sql = "SELECT desp_ayu_id AS idAyudante, desp_ayu_placa AS placa, desp_ayu_cedula AS cedula, desp_ayu_fecha AS fecha, desp_ayu_activo AS activo, desp_ayu_idContenedor AS idContenedor"
                    + " FROM Desp_Ayudante "
                    + " WHERE desp_ayu_cedula = " + cedula + " AND desp_ayu_idContenedor = " + idTrans + "";
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                InfoA2 = new InfoAyudantes();
                InfoA2.AyuId = int.Parse(row["idAyudante"].ToString());
                InfoA2.AyuPlaca = row["placa"].ToString();
                InfoA2.AyuCedula = int.Parse(row["cedula"].ToString());
                InfoA2.AyuFecha = row["fecha"].ToString();
                InfoA2.AyuActivo = row["activo"].ToString();
                InfoA2.AyuIdContenedor = int.Parse(row["idContenedor"].ToString());
            }
            return InfoA2;
        }
        public List<InfoAyudantes> buscarAyudantes(String placa)
        {
            List<InfoAyudantes> InfoAyudantes = new List<InfoAyudantes>();
            InfoAyudantes infoA = null;
            String sql = "SELECT desp_ayu_id AS idAyudante, desp_ayu_placa AS placa, desp_ayu_cedula AS cedula, desp_ayu_fecha AS fecha, desp_ayu_activo AS activo, desp_ayu_idgeneral AS idgeneral"
                       + " FROM Desp_Ayudante"
                       + " WHERE desp_ayu_placa = '" + placa + "'";
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                infoA = new InfoAyudantes();
                infoA.AyuId = int.Parse(row["idAyudante"].ToString());
                infoA.AyuPlaca = row["placa"].ToString();
                infoA.AyuCedula = int.Parse(row["cedula"].ToString());
                infoA.AyuFecha = row["fecha"].ToString();
                infoA.AyuActivo = row["activo"].ToString();
                InfoAyudantes.Add(infoA);
            }
            return InfoAyudantes;
        }
        /*---------------------------------------------AYUDANTES----------------------------------------------------------------------------------------------*/

        /*---------------------------------------------REPORTES----------------------------------------------------------------------------------------------*/
        /**-------------------------------------------ACTA TRAZA------------------------------------------**/
        public List<InfoContenedor> buscarPlacaIdTrasn(String orden)
        {
            List<InfoContenedor> LInfoConte = new List<InfoContenedor>();
            String sql = " select Desp_Transporte.Desp_Vehi_Placa AS placa,Desp_Transporte.Desp_Trans_Id AS idTrans"
                        + " FROM Orden INNER JOIN "
                        + " Despa_Cliente INNER JOIN"
                        + " Despa_Grupo INNER JOIN"
                        + " Desp_Transporte ON Despa_Grupo.Id_Grupo = Desp_Transporte.Desp_Grp_Id ON Despa_Grupo.Id_Grupo = Despa_Cliente.DesC_Grp_Id"
                        + " ON Orden.Id_Ofa = Despa_Cliente.DesC_OfPId"
                        + " WHERE (Orden.Ofa LIKE '%" + orden + "%') ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                InfoContenedor infoC = new InfoContenedor();
                infoC.Desp_Trans_id = int.Parse(row["idTrans"].ToString());
                infoC.TransPlaca = row["placa"].ToString();
                LInfoConte.Add(infoC);
            }
            return LInfoConte;
        }

        public int CerrarConexion()
        {
            return BdDatos.desconectar();
        }

        public List<InfoActaTraza> buscarDatosTraza(int IdTrans)
        {
            List<InfoActaTraza> LInfoTraza = new List<InfoActaTraza>();
            String sql = "SELECT     Desp_Transporte.Desp_Trans_Id AS idTrans, Desp_Transporte.Desp_Vehi_Placa AS placa, Desp_Transporte.Desp_Trailer_No AS numTrailer,  "
                      + " Desp_Transporte.Desp_Trans_Ini_Cargue AS horaInicio, Desp_Transporte.Desp_Trans_Fin_Cargue AS horaFin, "
                      + " Desp_Transporte.Desp_Trans_fecha AS horaLLegada, Desp_Transporte.Desp_Trans_Sale AS horaSalida,  "
                      + " Desp_Container.Cont_L + ' ' + Desp_Container.Cont_No + '-' + Desp_Container.Cont_V AS numContenedor, Despa_Cliente.DesC_Nal AS tipoExpo, "
                      + " Despa_Grupo.Id_Grupo, Despa_Tip_Trans.Tipo_Nombre AS tipoTrans, Desp_Precinto_Tipo.Prec_Tipo_Nombre AS nombreLlegada, "
                      + " Desp_Precinto_Tipo_1.Prec_Tipo_Nombre AS nombreSalida, Desp_Container.Cont_Pre_Ent AS precintoL, "
                      + " Desp_Container.Cont_Pre_Sal AS precintoS, Despa_Conductor.Desp_Cond_Nombre AS nomCond, Despa_Conductor.Desp_Cond_Doc AS ccCond, "
                      + " Trans_Carga.Trans_Nombre AS nomEmp, Desp_Transporte.Desp_Cond_Tel AS telefono, Desp_Container.Cont_Pres_Otros AS otroPre "
                      + " FROM         Despa_Grupo INNER JOIN "
                      + " Despa_Tip_Trans ON Despa_Grupo.Tipo_Trans_Id = Despa_Tip_Trans.Tipo_Tra_Id INNER JOIN "
                      + " Desp_Transporte INNER JOIN "
                      + " Desp_Container ON Desp_Transporte.Desp_Contenedor_Id = Desp_Container.Id_Contene INNER JOIN "
                      + " Despa_Cliente ON Desp_Transporte.Desp_Trans_DespId = Despa_Cliente.DesC_Id ON "
                      + " Despa_Grupo.Id_Grupo = Desp_Transporte.Desp_Grp_Id INNER JOIN "
                      + " Desp_Precinto_Tipo ON Desp_Transporte.Desp_Tip_Prec_Llega = Desp_Precinto_Tipo.Prec_Tipo_Id INNER JOIN "
                      + " Desp_Precinto_Tipo AS Desp_Precinto_Tipo_1 ON Desp_Transporte.Desp_Tip_Prec_Sale = Desp_Precinto_Tipo_1.Prec_Tipo_Id INNER JOIN "
                      + " Despa_Conductor ON Desp_Transporte.Desp_Cond_Id = Despa_Conductor.Desp_Cond_Id INNER JOIN "
                      + " Trans_Carga ON Despa_Conductor.Desp_Cond_Idtrans = Trans_Carga.Id_Trans "
                      + " WHERE     (Desp_Transporte.Desp_Trans_Id = " + IdTrans + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                InfoActaTraza infoActa = new InfoActaTraza();
                infoActa.IdDespaTrans = int.Parse(row["idTrans"].ToString());
                infoActa.Placa = row["placa"].ToString();
                infoActa.NumContenedor = row["numContenedor"].ToString();
                infoActa.InicioCargue = row["horaInicio"].ToString();
                infoActa.FinCargue = row["horaFin"].ToString();
                infoActa.LlegadaCamion = row["horaLLegada"].ToString();
                infoActa.SalidaCamion = row["horaSalida"].ToString();
                infoActa.NumTrailer = row["numTrailer"].ToString();
                infoActa.TipoExpo = row["tipoExpo"].ToString();
                infoActa.NombreLlegada = row["nombreLlegada"].ToString();
                infoActa.NombreSalida = row["nombreSalida"].ToString();
                infoActa.TipoTrans = row["tipoTrans"].ToString();
                infoActa.PrecintoLlegada = row["precintoL"].ToString();
                infoActa.PrecintoSalida = row["precintoS"].ToString();
                infoActa.NomCond = row["nomCond"].ToString();
                infoActa.CcCond = row["ccCond"].ToString();
                infoActa.NomEmp = row["nomEmp"].ToString();
                infoActa.Tel = row["telefono"].ToString();
                infoActa.OtroPre = row["otroPre"].ToString();
                LInfoTraza.Add(infoActa);
            }

            return LInfoTraza;
        }
        public InfoActaTraza buscarDatosTraza2(int IdTrans)
        {
            InfoActaTraza infoT = new InfoActaTraza();
            String sql = "SELECT     pais.pai_nombre AS pais"
                      + " FROM         Despa_Grupo INNER JOIN"
                      + " Desp_Transporte ON Despa_Grupo.Id_Grupo = Desp_Transporte.Desp_Grp_Id INNER JOIN"
                      + " Orden INNER JOIN"
                      + " ciudad INNER JOIN"
                      + " pais INNER JOIN"
                      + " Despa_Cliente ON pais.pai_id = Despa_Cliente.DesC_PaisId ON ciudad.ciu_id = Despa_Cliente.DesC_CiudadId ON "
                      + " Orden.Id_Ofa = Despa_Cliente.DesC_OfPId INNER JOIN"
                      + " cliente ON Despa_Cliente.DesC_clieId = cliente.cli_id ON Despa_Grupo.Id_Grupo = Despa_Cliente.DesC_Grp_Id"
                      + " WHERE     (Desp_Transporte.Desp_Trans_Id = " + IdTrans + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                infoT = new InfoActaTraza();
                infoT.Pais = row["pais"].ToString();
            }
            return infoT;
        }
        public DataTable cargarGridTraza1(int IdTrans) //CARGA GRID
        {
            String sql = "SELECT     Orden.Numero + '-' + Orden.ano AS orden, cliente.cli_nombre AS cliente "
                      + "  FROM         Desp_Transporte INNER JOIN "
                      + "  Despa_Grupo ON Desp_Transporte.Desp_Grp_Id = Despa_Grupo.Id_Grupo INNER JOIN "
                      + "  Despa_Cliente ON Despa_Grupo.Id_Grupo = Despa_Cliente.DesC_Grp_Id INNER JOIN "
                      + "  Orden INNER JOIN "
                      + "  cliente INNER JOIN "
                      + "  formato_unico ON cliente.cli_id = formato_unico.fup_cli_id ON Orden.Yale_Cotiza = formato_unico.fup_id ON "
                      + "  Despa_Cliente.DesC_OfPId = Orden.Id_Ofa "
                      + " WHERE     (Desp_Transporte.Desp_Trans_Id = " + IdTrans + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        public DataTable cargaGridTraza2(int IdTrans)
        {
            String sql = "SELECT Desp_Ayudante.desp_ayu_cedula AS cedula, empleado.emp_nombre_mayusculas + ' ' + empleado.emp_apellidos_mayusculas AS nombre, "
                       + "  area.are_nombre AS area, '..........................................' AS firma, '..........................................' AS obser"
                       + "  FROM empleado INNER JOIN"
                       + "  Desp_Ayudante ON empleado.emp_usu_num_id = Desp_Ayudante.desp_ayu_cedula INNER JOIN"
                       + "  area ON empleado.emp_area_id = area.are_id"
                       + "  WHERE (Desp_Ayudante.desp_ayu_activo = 1) AND (Desp_Ayudante.desp_ayu_idContenedor =  " + IdTrans + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        /**-------------------------------------------ACTA TRAZA------------------------------------------**/
        /**-------------------------------------------PACKING------------------------------------------**/
        public DataTable cargarGridPacking1(int idTrans) //CARGA GRID
        {
            String sql = " SELECT pallet_aluminio.pallet_numero_desc AS palletN,'FORMALETAS' AS contenido,pallet_aluminio.pallet_al_Largo AS largo, pallet_aluminio.pallet_al_Anc AS ancho, pallet_aluminio.pallet_al_Alto AS alto,  "
                  + "  pallet_aluminio.pallet_al_Vol AS m3, pallet_aluminio.pallet_al_peso AS pesoB,   "
                  + "  pallet_aluminio.pallet_al_pesoN AS pesoN  "
                  + "  FROM pallet_aluminio INNER JOIN  "
                  + "  Orden AS Orden_1 INNER JOIN  "
                  + "  Despa_Cliente AS Despa_Cliente_1 LEFT OUTER JOIN  "
                  + "  Desp_Transporte AS Desp_Transporte_1 ON Despa_Cliente_1.DesC_Grp_Id = Desp_Transporte_1.Desp_Grp_Id ON   "
                  + "  Orden_1.Id_Ofa = Despa_Cliente_1.DesC_OfPId ON pallet_aluminio.pallet_al_Id_ofa = Orden_1.Id_Ofa AND   "
                  + "  pallet_aluminio.Pallet_Trans_Id = Desp_Transporte_1.Desp_Trans_Id  "
                  + "  WHERE(Orden_1.Anulada = 0) AND (pallet_aluminio.pallet_al_cant > 0) AND   "
                  + "  (pallet_aluminio.Pallet_Trans_Id <> 0) AND (Desp_Transporte_1.Desp_Trans_Id = " + idTrans + ")  "
                  + "  union all  "
                  + "  SELECT pallet_acc.pallet_acc_Desc AS palletN, 'ACCESORIOS FORMALETAS' AS contenido,pallet_acc.pallet_acc_largo AS largo, pallet_acc.pallet_acc_ancho AS ancho,  "
                  + "  pallet_acc.pallet_acc_alto AS alto,  pallet_acc.pallet_acc_Vol AS m3, pallet_acc_peso AS pesoB, pallet_acc.pallet_acc_pesoN AS pesoN "
                  + "  FROM Orden AS Orden_1 INNER JOIN  "
                  + "  Despa_Cliente AS Despa_Cliente_1 LEFT OUTER JOIN  "
                  + "  Desp_Transporte AS Desp_Transporte_1 ON Despa_Cliente_1.DesC_Grp_Id = Desp_Transporte_1.Desp_Grp_Id ON   "
                  + "  Orden_1.Id_Ofa = Despa_Cliente_1.DesC_OfPId INNER JOIN  "
                  + "  pallet_acc ON Desp_Transporte_1.Desp_Trans_Id = pallet_acc.pallet_acc_id_trans AND Orden_1.Id_Ofa = pallet_acc.pallet_acc_id_of_p  "
                  + "  WHERE (Orden_1.Anulada = 0) AND (Desp_Transporte_1.Desp_Trans_Id = " + idTrans + ") AND   "
                  + "  (pallet_acc.pallet_acc_id_trans <> 0) AND (pallet_acc.pallet_acc_cant > 0)  ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        public DataTable cargarGridPacking2(int idTrans) //CARGA GRID2
        {
            String sql = "SELECT Desp_Container.Cont_L + ' ' + Desp_Container.Cont_No + '-' + Desp_Container.Cont_V AS contenedor, Desp_Transporte.Desp_Vehi_Placa AS placa,  "
            + "  Desp_Container.Cont_Pre_Sal AS precinto"
            + "  FROM Orden INNER JOIN "
            + "  Despa_Cliente ON Orden.Id_Ofa = Despa_Cliente.DesC_OfPId INNER JOIN "
            + "  Despa_Grupo ON Despa_Cliente.DesC_Grp_Id = Despa_Grupo.Id_Grupo INNER JOIN "
            + "  Desp_Transporte ON Despa_Cliente.DesC_Id = Desp_Transporte.Desp_Trans_DespId LEFT OUTER JOIN "
            + "  Desp_Container ON Desp_Transporte.Desp_Trans_Id = Desp_Container.Cont_Desp_TransId "
            + "  WHERE (Desp_Transporte.Desp_Trans_Id = " + idTrans + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        public InfoPacking buscarDatosPacking(String orden)
        {
            InfoPacking infoP = new InfoPacking();
            String sql = "SELECT     Orden.Numero + '-' + Orden.ano AS orden, cliente.cli_nombre AS cliente, cliente.cli_direccion AS direccion, cliente.cli_telefono AS telefono,  "
                        + "  cliente.cli_nit AS nit, pais.pai_nombre AS pais, Despa_Cliente.DesC_Fecha AS fecha,  "
                        + "  empleado.emp_nombre_mayusculas + ' ' + empleado.emp_apellidos_mayusculas AS nomUsuCrea, Despa_Cliente.DesC_Obs AS encomendante,  "
                        + "  puertos_1.pto_nombre AS puertoEmbarque, puertos.pto_nombre AS puertoDestino, Despa_Cliente.DesC_Fact_No AS factura,  "
                        + "  termino_negociacion.ternog_sigla AS tdn, Orden.Id_Ofa "
                        + "  FROM         termino_negociacion INNER JOIN "
                        + "  solicitud_facturacion ON termino_negociacion.ternog_id = solicitud_facturacion.Sf_termino_negociacion RIGHT OUTER JOIN "
                        + "  Despa_Grupo INNER JOIN "
                        + "  Despa_Cliente ON Despa_Grupo.Id_Grupo = Despa_Cliente.DesC_Grp_Id INNER JOIN "
                        + "  Orden INNER JOIN "
                        + "  cliente INNER JOIN "
                        + "  formato_unico ON cliente.cli_id = formato_unico.fup_cli_id ON Orden.Yale_Cotiza = formato_unico.fup_id ON  "
                        + "  Despa_Cliente.DesC_OfPId = Orden.Id_Ofa INNER JOIN "
                        + "  pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN "
                        + "  empleado ON Despa_Cliente.DesC_EmpId = empleado.emp_usu_num_id INNER JOIN "
                        + "  puertos AS puertos_1 ON Despa_Grupo.Puerto_Embarque = puertos_1.pto_id INNER JOIN "
                        + "  puertos ON Despa_Grupo.Puerto_Destino = puertos.pto_id ON solicitud_facturacion.Sf_fup_id = Orden.Yale_Cotiza AND  "
                        + "  solicitud_facturacion.Sf_version = Orden.ord_version AND solicitud_facturacion.Sf_parte = Orden.ord_parte "
                        + "  WHERE     (Orden.Numero + '-' + Orden.ano = '" + orden + "') ";

            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                infoP = new InfoPacking();
                infoP.Cliente = row["cliente"].ToString();
                infoP.Direccion = row["direccion"].ToString();
                infoP.Telefono = row["telefono"].ToString();
                infoP.Nit = row["nit"].ToString();
                infoP.Pais = row["pais"].ToString();
                infoP.UsuarioCreaDesp = row["nomUsuCrea"].ToString();
                infoP.Fecha = row["fecha"].ToString();
                infoP.Encomendante = row["encomendante"].ToString();
                infoP.PuertoEmbarque = row["puertoEmbarque"].ToString();
                infoP.PuertoDestino = row["puertoDestino"].ToString();
                infoP.Factura = row["factura"].ToString();
                infoP.Tdn = row["tdn"].ToString();
            }
            return infoP;
        }
        /**-------------------------------------------PACKING------------------------------------------**/
        /*---------------------------------------------REPORTES----------------------------------------------------------------------------------------------*/

        public DataTable consultarUsuario(string cedula)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT        empleado.emp_nombre + ' ' + empleado.emp_apellidos AS nombre, usuario.usu_siif_id as codigo, isnull(empleado.emp_correo_electronico, '') as correo " +
                         " FROM empleado INNER JOIN " +
                         " usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id " +
                         " WHERE empleado.emp_usu_num_id = '"+cedula+"'";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        //Insertar Log peso
        public int insertarLogPeso(int idPallet, int tipoPallet, double peso, string usuario_sesion, int codigo_usuario_peso, int devolucion, double pesoAnterior)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[8];
            sqls[0] = new SqlParameter("idPallet", idPallet);
            sqls[1] = new SqlParameter("tipoPallet", tipoPallet); 
            sqls[2] = new SqlParameter("peso", peso);
            sqls[3] = new SqlParameter("usuario_sesion", usuario_sesion);
            sqls[4] = new SqlParameter("codigo_usuario_peso", codigo_usuario_peso);
            sqls[5] = new SqlParameter("devolucion", devolucion);
            sqls[6] = new SqlParameter("peso_anterior", pesoAnterior);
            sqls[7] = new SqlParameter("tipoPieza", 1);
            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarLogLogisticaPeso", con))
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

        //Insertar Solicitud Logistica
        public int insertarSolicitudLogistica(int id_area, int id_ofa, int tipo_pallet_id, int motivo_id, string comentario, string usuario)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[6];
            sqls[0] = new SqlParameter("id_area", id_area);
            sqls[1] = new SqlParameter("id_ofa", id_ofa);
            sqls[2] = new SqlParameter("tipo_pallet_id", tipo_pallet_id);
            sqls[3] = new SqlParameter("motivo_id", motivo_id);
            sqls[4] = new SqlParameter("comentario", comentario);
            sqls[5] = new SqlParameter("usuario", usuario);
            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarSolicitudLogistica", con))
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

        public DataTable consultarMotivosSolicitud()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT logistica_motivo_id, descripcion FROM logistica_motivo WHERE activo = 1 order by descripcion ";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public string consultarNombreArea(int idArea)
        {
            string nombre = "";
            string sql = "SELECT are_nombre as nombre FROM area WHERE are_id = "+idArea+"";
            DataTable dt = new DataTable();
            dt = BdDatos.CargarTabla(sql);
            if (dt.Rows.Count > 0)
            {
                nombre = dt.Rows[0]["nombre"].ToString();
            }
            return nombre;
        }

        public DataTable consultarAreaUsuarios()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT  are_id, are_nombre as nombre " +
                          " FROM area " +
                              " WHERE      are_id = 10 OR are_id = 17 " +
                                      " OR are_id = 4  OR are_id = 11  " +
                                      " OR are_id = 26 OR are_id = 15 " +
                                      " OR are_id = 29 OR are_id = 139 " +
                                      " OR are_id = 35 OR are_id = 18 " +
                           " ORDER BY   are_nombre ASC";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }



        public DataTable consultarTipoPallet()
        {
            string sql;
            sql = "SELECT log_cap_tipo, UPPER(log_cap_tabla) as descripcion FROM log_cap_peso";
            DataTable Table = BdDatos.CargarTabla(sql);            
            return Table;
        }
        public DataTable consultarPalletAccOrden(string orden, int idArea)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT        pallet_acc.pallet_acc_id, pallet_acc.pallet_acc_numero, pallet_acc.pallet_acc_id_of_p, pallet_acc.pallet_acc_Desc " +
                          " FROM pallet_acc INNER JOIN " +
                         " Orden ON pallet_acc.pallet_acc_id_of_p = Orden.Id_Ofa " +
                         " WHERE(Orden.Numero + '-' + Orden.ano LIKE '%" + orden + "%') AND (Orden.Fec_Desp IS NULL) AND(pallet_acc.pallet_acc_cant > 0) AND (pallet_acc.pallet_acc_peso > 0) AND (isnull(pallet_acc.pallet_acc_solicitado, 0) = 0) AND (pallet_acc.pallet_acc_id_trans = 0)  AND (pallet_acc.pallet_acc_OrigenId = " + idArea + " )" +
                         " ORDER BY pallet_acc.pallet_acc_numero";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public DataTable consultarPalletAluminioOrden(string orden, int idArea)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT        pallet_aluminio.pallet_al_id, pallet_aluminio.pallet_al_numero, pallet_aluminio.pallet_al_Id_ofa, pallet_aluminio.pallet_numero_desc " +
                         " FROM pallet_aluminio INNER JOIN " +
                         " Orden ON pallet_aluminio.pallet_al_Id_ofa = Orden.Id_Ofa " +
                         " WHERE(pallet_aluminio.pallet_al_cant > 0) AND (Orden.Fec_Desp IS NULL) AND  (pallet_aluminio.pallet_al_peso > 0) AND(Orden.Numero + '-' + Orden.ano LIKE '%" + orden + "%') AND (isnull(pallet_aluminio.pallet_al_solicitado, 0) = 0) AND (pallet_aluminio.Pallet_Trans_Id = 0) " +
                         " ORDER BY pallet_aluminio.pallet_al_numero ";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public void insertarSolicitudPallets(int solicitud_id, ListBox lb)
        {
            string val = "";
            String sql = "INSERT INTO solicitud_logistica_pallet (solicitud_logistica_id, pallet_id, activo) VALUES";

            foreach (ListItem lis in lb.Items)
            {
                val += " ( " + solicitud_id + ", " + Convert.ToInt32(lis.Value) + ", 1), ";
            }

            val = val.Substring(0, val.Length - 2);

            sql += val;

            try
            {
                BdDatos.Actualizar(sql);
            }
            catch { }            
        }

        public void actualizarEstadoPallet(int tipo, ListBox lb, int idArea)
        {
            string val = "";
            string sql = "";
            string query = "";
            if (tipo == 2)
            {
                foreach (ListItem lis in lb.Items)
                {
                    sql = "UPDATE PALLET_ACC SET pallet_acc_solicitado = 1, pallet_Acc_Cerrado = 0 WHERE ";
                    val = " ( pallet_acc.pallet_acc_id = " + Convert.ToInt32(lis.Value) + "); ";
                    query += sql + val;
                }
            }
            else if (tipo == 9)
            {
                foreach (ListItem lis in lb.Items)
                {
                    sql = "UPDATE PALLET_ALUMINIO SET pallet_al_solicitado = 1, pallet_cerrado = 0 WHERE ";
                    val = " ( pallet_al_id = " + Convert.ToInt32(lis.Value) + "); ";
                    query += sql + val;
                }
            }
            try
            {
                BdDatos.Actualizar(query);
            }
            catch { }
        }

        //insertar el log de devolucion de pallet
        public int InsertarLogDevolucionPalletLogistica(int id_area, int id_ofa, int pallet_id, int tipo_pallet_id, string usuario, string codigo)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[6];
            sqls[0] = new SqlParameter("id_area", id_area);
            sqls[1] = new SqlParameter("id_ofa", id_ofa);
            sqls[2] = new SqlParameter("pallet_id", pallet_id);
            sqls[3] = new SqlParameter("tipo_pallet_id", tipo_pallet_id);
            sqls[4] = new SqlParameter("usuario", usuario);
            sqls[5] = new SqlParameter("codigo_usuario_devolucion", codigo);
            
            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarLogDevolucionPalletLogistica", con))
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

        public bool consultarSolicitudPalletAl(int pallet)
        {
            bool solicitado = false;
            DataTable dt = new DataTable();
            string sql = "SELECT        isnull(pallet_al_solicitado, 0) as solicitado FROM pallet_aluminio WHERE(pallet_al_id = "+pallet+")";
            dt = BdDatos.CargarTabla(sql);
            if (dt.Rows.Count > 0)
            {
                solicitado = Convert.ToBoolean(dt.Rows[0]["solicitado"]);
            }
            return solicitado;
        }

        public bool consultarSolicitudPalletAcc(int pallet)
        {
            bool solicitado = false;
            DataTable dt = new DataTable();
            string sql = "SELECT        pallet_acc_solicitado AS solicitado FROM pallet_acc WHERE(pallet_acc_id = "+pallet+")";
            dt = BdDatos.CargarTabla(sql);
            if (dt.Rows.Count > 0)
            {
                solicitado = Convert.ToBoolean(dt.Rows[0]["solicitado"]);
            }
            return solicitado;
        }

        public DataTable consultarIdPalletAluminio(long IdOfpallet, int numPallet)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT pallet_al_id as id, pallet_al_peso as peso FROM pallet_aluminio WHERE pallet_al_numero = " + numPallet + " and pallet_al_Id_ofa = '" + IdOfpallet + "'";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public DataTable consultarIdPalletAccesorio(int idpallet)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT pallet_acc_id as id, pallet_acc_peso as peso FROM pallet_acc WHERE pallet_acc_id = " + idpallet + "";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public void CorreosLogistica(int evento, int idPallet, int tipo, int area, string usuario, int planta, string remitente, string correoSistema, string motivodesc, string comentario, int idSolicitud, string of, string correosolicita, string usuariosolicita, string areasolicita, int area_id_solicita)
        {
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[15];
            sqls[0] = new SqlParameter("@pEvento ", evento);
            sqls[1] = new SqlParameter("@idPallet", idPallet);
            sqls[2] = new SqlParameter("@tipo", tipo);
            sqls[3] = new SqlParameter("@area", area);
            sqls[4] = new SqlParameter("@pUsuario", usuario);
            sqls[5] = new SqlParameter("@planta", planta);
            sqls[6] = new SqlParameter("@pRemitente", remitente);
            sqls[7] = new SqlParameter("@motivodesc", motivodesc);
            sqls[8] = new SqlParameter("@comentario", comentario);
            sqls[9] = new SqlParameter("@idSolicitud", idSolicitud);
            sqls[10] = new SqlParameter("@of", of);
            sqls[11] = new SqlParameter("@correosolicita", correosolicita);
            sqls[12] = new SqlParameter("@usuariosolicita", usuariosolicita);
            sqls[13] = new SqlParameter("@areasolicita", areasolicita);
            sqls[14] = new SqlParameter("@areaidsolicita", area_id_solicita);
            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_LOGISTICA_notificaciones", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlParameter Asunto = new SqlParameter("@pAsun_mail", SqlDbType.VarChar, 200);
                    SqlParameter Destinatarios = new SqlParameter("@pLista", SqlDbType.VarChar, 12500);
                    SqlParameter Mensaje = new SqlParameter("@pMsg", SqlDbType.VarChar, 12500);
                    SqlParameter Anexo = new SqlParameter("@pAnexo", SqlDbType.Bit);
                    SqlParameter LinkAnexo = new SqlParameter("@pLink_anexo", SqlDbType.VarChar, 250);

                    Asunto.Direction = ParameterDirection.Output;
                    Destinatarios.Direction = ParameterDirection.Output;
                    Mensaje.Direction = ParameterDirection.Output;
                    Anexo.Direction = ParameterDirection.Output;
                    LinkAnexo.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(Asunto);
                    cmd.Parameters.Add(Destinatarios);
                    cmd.Parameters.Add(Mensaje);
                    cmd.Parameters.Add(Anexo);
                    cmd.Parameters.Add(LinkAnexo);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //VALORES DEL ENCABEZADO 
                        string AsuntoMail = Convert.ToString(Asunto.Value);
                        string DestinatariosMail = Convert.ToString(Destinatarios.Value);
                        string MensajeMail = Convert.ToString(Mensaje.Value);
                        bool llevaAnexo = Convert.ToBoolean(Anexo.Value);
                        string EnlaceAnexo = Convert.ToString(LinkAnexo.Value);
                        string tipoAdjunto = "";

                        Byte[] correo = new Byte[0];
                        WebClient clienteWeb = new WebClient();
                        clienteWeb.Dispose();
                        clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "forsa", "FORSA");
                        // Adjunto
                        //DEFINIMOS LA CLASE DE MAILMESSAGE
                        MailMessage mail = new MailMessage();
                        //INDICAMOS EL EMAIL DE ORIGEN
                        mail.From = new MailAddress(correoSistema);

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
                        //ADJUNTAMOS EL ARCHIVO
                        MemoryStream ms = new MemoryStream();

                        //DEFINIMOS NUESTRO SERVIDOR SMTP
                        //DECLARAMOS LA CLASE SMTPCLIENT
                        SmtpClient smtp = new SmtpClient();
                        //DEFINIMOS NUESTRO SERVIDOR SMTP
                        smtp.Host = "smtp.office365.com";
                        //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
                        smtp.Credentials = new System.Net.NetworkCredential("monitoreo@forsa.net.co", "Those7953");
                        smtp.Port = 25;
                        smtp.EnableSsl = true;
                        //smtp.Timeout =

                        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
                            SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };
                        try
                        {
                            smtp.Send(mail);
                        }
                        catch (Exception ex)
                        {
                            string mensaje = "ERROR: " + ex.Message;
                        }
                        ms.Close();
                    }
                }
            }
        }

        
        public int consultarPlantaUsuario(string usuario)
        {
            DataTable dt = new DataTable();
            int planta = 0;
            string sql = "SELECT        usu_planta_Id as planta FROM usuario WHERE(usu_login = '"+usuario+"')";
            dt = BdDatos.CargarTabla(sql);
            if (dt.Rows.Count > 0)
            {
                planta = Convert.ToInt32(dt.Rows[0]["planta"]);
            }
            return planta;            
        }

        public DataTable getDatosSolicitante(int idPallet, int tipo)
        {
            DataTable dt = new DataTable();
            string sql = "";
            if (tipo == 2)
            {
                sql = "SELECT  TOP(1) area.are_nombre area , area.are_id , solicitud_logistica.usuario usuario, empleado.emp_correo_electronico correo " +
                      " FROM solicitud_logistica INNER JOIN " +
                      " solicitud_logistica_pallet ON solicitud_logistica.solicitud_logistica_id = solicitud_logistica_pallet.solicitud_logistica_id INNER JOIN " +
                      " area ON solicitud_logistica.id_area = area.are_id INNER JOIN " +
                      " pallet_acc ON solicitud_logistica_pallet.pallet_id = pallet_acc.pallet_acc_id INNER JOIN " +
                      " usuario ON solicitud_logistica.usuario = usuario.usu_login INNER JOIN " +
                      " empleado ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id " +
                      " WHERE(solicitud_logistica_pallet.pallet_id = " + idPallet + ") AND(solicitud_logistica_pallet.activo = 1) ORDER BY solicitud_logistica.fecha desc ";
            }

            else if (tipo == 9)
            {
                sql = "SELECT TOP(1) area.are_nombre area, area.are_id, solicitud_logistica.usuario usuario, empleado.emp_correo_electronico correo " +
                       " FROM solicitud_logistica INNER JOIN " +
                       " solicitud_logistica_pallet ON solicitud_logistica.solicitud_logistica_id = solicitud_logistica_pallet.solicitud_logistica_id INNER JOIN " +
                       " area ON solicitud_logistica.id_area = area.are_id INNER JOIN " +
                       " pallet_aluminio ON solicitud_logistica_pallet.pallet_id = pallet_aluminio.pallet_al_id INNER JOIN " +
                       " usuario ON solicitud_logistica.usuario = usuario.usu_login INNER JOIN " +
                       " empleado ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id " +
                       " WHERE(solicitud_logistica_pallet.pallet_id = " + idPallet + ") AND(solicitud_logistica_pallet.activo = 1) ORDER BY solicitud_logistica.fecha desc ";
            }
            dt = BdDatos.CargarTabla(sql);
            return dt;
            /*
             if @tipo = 2
			begin
				SELECT   @areasolicita = area.are_nombre, @usuariosolicita = solicitud_logistica.usuario, @correosolicita = empleado.emp_correo_electronico
				FROM            solicitud_logistica INNER JOIN
                         solicitud_logistica_pallet ON solicitud_logistica.solicitud_logistica_id = solicitud_logistica_pallet.solicitud_logistica_id INNER JOIN
                         area ON solicitud_logistica.id_area = area.are_id INNER JOIN
                         pallet_acc ON solicitud_logistica_pallet.pallet_id = pallet_acc.pallet_acc_id INNER JOIN
											 usuario ON solicitud_logistica.usuario = usuario.usu_login INNER JOIN
											 empleado ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id
				WHERE        (solicitud_logistica_pallet.pallet_id = @idPallet) AND (solicitud_logistica_pallet.activo = 1)
			end
			else if @tipo = 9
			begin
					SELECT     @areasolicita = area.are_nombre, @usuariosolicita = solicitud_logistica.usuario, @correosolicita = empleado.emp_correo_electronico
					FROM            solicitud_logistica INNER JOIN
											 solicitud_logistica_pallet ON solicitud_logistica.solicitud_logistica_id = solicitud_logistica_pallet.solicitud_logistica_id INNER JOIN
											 area ON solicitud_logistica.id_area = area.are_id INNER JOIN
											 pallet_aluminio ON solicitud_logistica_pallet.pallet_id = pallet_aluminio.pallet_al_id INNER JOIN
											 usuario ON solicitud_logistica.usuario = usuario.usu_login INNER JOIN
											 empleado ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id
					WHERE        (solicitud_logistica_pallet.pallet_id = @idPallet) AND (solicitud_logistica_pallet.activo = 1)
			end
					
             */
        }

        public string getFechaLogPeso(int id)
        {
            string fecha = "";
            string sql = "SELECT fecha FROM log_logistica_peso WHERE log_logistica_peso_id = " + id + "";
            DataTable dt = new DataTable();
            dt = BdDatos.CargarTabla(sql);
            if (dt.Rows.Count > 0)
            {
                fecha = dt.Rows[0]["fecha"].ToString();
            }
            return fecha;
        }

        public DataTable consultarSolicitudes()
        {
            DataTable dt = new DataTable();
            string sql = " SELECT        CONVERT(varchar, " +
                         "ISNULL((CASE WHEN solicitud_logistica.tipo_pallet_id = 9 THEN pallet_aluminio.pallet_numero_desc WHEN solicitud_logistica.tipo_pallet_id = 2 THEN pallet_acc.pallet_acc_Desc " +
                         " ELSE '' END), '')) AS pallet, CONVERT(varchar, " +
                         " ISNULL((CASE WHEN solicitud_logistica.tipo_pallet_id = 9 THEN(CASE WHEN pallet_aluminio.pallet_al_solicitado = 1 AND " +
                         " solicitud_logistica_pallet.activo = 1 THEN 'NO' ELSE 'SI' END) " +
                         " WHEN solicitud_logistica.tipo_pallet_id = 2 THEN(CASE WHEN pallet_acc.pallet_acc_solicitado = 1 AND " +
                         " solicitud_logistica_pallet.activo = 1 THEN 'NO' ELSE 'SI' END) ELSE 'SI' END), 'SI')) AS devuelto, Orden.Numero + '-' + Orden.ano AS orden " +
                         " FROM solicitud_logistica INNER JOIN " +
                         " solicitud_logistica_pallet ON solicitud_logistica.solicitud_logistica_id = solicitud_logistica_pallet.solicitud_logistica_id INNER JOIN " +
                         " Orden ON solicitud_logistica.id_ofa = Orden.Id_Ofa INNER JOIN " +
                         " log_cap_peso ON solicitud_logistica.tipo_pallet_id = log_cap_peso.log_cap_tipo LEFT OUTER JOIN " +
                         " pallet_aluminio ON solicitud_logistica_pallet.pallet_id = pallet_aluminio.pallet_al_id LEFT OUTER JOIN " +
                         " pallet_acc ON solicitud_logistica_pallet.pallet_id = pallet_acc.pallet_acc_id " +
                         " WHERE(CONVERT(varchar, ISNULL((CASE WHEN solicitud_logistica.tipo_pallet_id = 9 THEN(CASE WHEN pallet_aluminio.pallet_al_solicitado = 1 AND " +
                         " solicitud_logistica_pallet.activo = 1 THEN 'NO' ELSE 'SI' END) " +
                         " WHEN solicitud_logistica.tipo_pallet_id = 2 THEN(CASE WHEN pallet_acc.pallet_acc_solicitado = 1 AND " +
                         " solicitud_logistica_pallet.activo = 1 THEN 'NO' ELSE 'SI' END) ELSE 'SI' END), 'SI')) = 'NO') " +
                         " ORDER BY solicitud_logistica.fecha DESC";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }


        public DataTable Obtener_pallet_Solicitado(int idofa)
        {
            //PRUEBAS
            //string sql = "SELECT CONVERT(VARCHAR,ISNULL((CASE WHEN SL.tipo_pallet_id = 9 THEN  PAL.pallet_numero_desc WHEN SL.tipo_pallet_id = 2 THEN PAC.pallet_acc_Desc ELSE '' END), '')) AS Pallet, " +
            //    " CASE WHEN PAL.Pallet_Al_Id IS NULL THEN 'ACERO' ELSE 'ALUMINIO' END Tipo  FROM   SOLICITUD_LOGISTICA  SL INNER JOIN SOLICITUD_LOGISTICA_PALLET  SLP ON SL.Solicitud_Logistica_Id =  " +
            //    " SLP.Solicitud_Logistica_Id INNER JOIN   ORDEN O    ON SL.Id_Ofa = O.Id_Ofa INNER JOIN  LOG_CAP_PESO LCP ON SL.Tipo_Pallet_Id = LCP.Log_Cap_Tipo  LEFT OUTER JOIN PALLET_ALUMINIO  PAL " +
            //    " ON SLP.Pallet_Id = PAL.Pallet_Al_Id LEFT OUTER JOIN PALLET_ACC PAC ON SLP.Pallet_Id = PAC.Pallet_Acc_Id " +
            //    " WHERE o.Id_Ofa = " + idofa + "";

            string sql = "SELECT CONVERT(VARCHAR,ISNULL((CASE WHEN SL.tipo_pallet_id = 9 THEN  PAL.pallet_numero_desc WHEN SL.tipo_pallet_id = 2 THEN PAC.pallet_acc_Desc ELSE '' END), '')) AS Pallet, " +
                         " CASE WHEN PAL.Pallet_Al_Id IS NULL THEN 'ACERO' ELSE 'ALUMINIO' END Tipo" +
                         " FROM   SOLICITUD_LOGISTICA  SL WITH(NOLOCK) INNER JOIN SOLICITUD_LOGISTICA_PALLET  SLP WITH(NOLOCK) ON SL.Solicitud_Logistica_Id = SLP.Solicitud_Logistica_Id" +
                                " INNER JOIN   ORDEN O WITH(NOLOCK)   ON SL.Id_Ofa = O.Id_Ofa INNER JOIN  LOG_CAP_PESO LCP WITH(NOLOCK) ON SL.Tipo_Pallet_Id = LCP.Log_Cap_Tipo " +
                                " LEFT OUTER JOIN PALLET_ALUMINIO  PAL WITH(NOLOCK) ON SLP.Pallet_Id = PAL.Pallet_Al_Id LEFT OUTER JOIN PALLET_ACC PAC WITH(NOLOCK) ON SLP.Pallet_Id = PAC.Pallet_Acc_Id" +
                        " WHERE o.Id_Ofa = " + idofa + " AND CONVERT(VARCHAR, ISNULL((CASE WHEN SL.tipo_pallet_id = 9 THEN(CASE WHEN PAL.pallet_al_solicitado = 1 AND" +
                                " SLP.activo = 1 THEN 'NO' ELSE 'SI' END) WHEN SL.tipo_pallet_id = 2 THEN(CASE WHEN PAC.pallet_acc_solicitado = 1 " +
                                " AND SLP.activo = 1 THEN 'NO' ELSE 'SI' END) ELSE 'SI' END ) ,'SI')) = 'NO' " +
                                " ORDER BY SL.fecha DESC";
            return BdDatos.CargarTabla(sql);
        }
    }
}