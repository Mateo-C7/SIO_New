using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace CapaControl
{
    public class ControlSeguimientoDespachos
    {
        public void Listar_Mes(DropDownList myListaMes)
        {
            DataTable dTable = new DataTable();
            string sql = "SELECT mes_id, mes_nombre FROM Mes WHERE(mes_activo = 1)";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["mes_nombre"].ToString(),
                                            row["mes_id"].ToString());
                myListaMes.Items.Insert(myListaMes.Items.Count, lst);
            }
        }
        public void Listar_MedioTrasnporte(DropDownList myListaTrasnporte)
        {
            DataTable dTable = new DataTable();
            string sql = "SELECT Tipo_Tra_Id,Tipo_Nombre from Despa_Tip_Trans WHERE Tipo_Anula=0 AND Tipo_Tra_Id<>0";
            dTable = BdDatos.CargarTabla(sql);

            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["Tipo_Nombre"].ToString(),
                                            row["Tipo_Tra_Id"].ToString());
                myListaTrasnporte.Items.Insert(myListaTrasnporte.Items.Count, lst);
            }
        }

        public void Listar_TipoVehiculo(DropDownList myListaVehiculo, int Trasn_id)
        {
            DataTable dTable = new DataTable();
            string sql = "SELECT DesTipVeh_Id, DesTipVeh_Descripcion " +
                        " FROM Despa_Tipo_Vehiculo " +
                        " WHERE(DesTipVeh_Grp_Id_Trasn = " + Trasn_id + ") AND(DesTipVeh_Activo = 1)";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["DesTipVeh_Descripcion"].ToString(),
                                            row["DesTipVeh_Id"].ToString());
                myListaVehiculo.Items.Insert(myListaVehiculo.Items.Count, lst);
            }
        }

        public void Listar_EstatusCarga(DropDownList myListaEstCarga)
        {
            DataTable dTable = new DataTable();
            string sql = "SELECT  DesEstc_Id, DesEstc_Descripcion " +
                          " FROM Despa_EstatusCarga" +
                          " WHERE DesEstc_Activo=1 " +
                          " ORDER BY DesEstc_Orden ASC";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["DesEstc_Descripcion"].ToString(),
                                            row["DesEstc_Id"].ToString());
                myListaEstCarga.Items.Insert(myListaEstCarga.Items.Count, lst);
            }
        }

        public void Listar_Tdn(DropDownList myListaTdn)
        {
            DataTable dTable = new DataTable();
            string sql = "SELECT ternog_id,ternog_sigla " +
                          " FROM termino_negociacion";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["ternog_sigla"].ToString(),
                                            row["ternog_id"].ToString());
                myListaTdn.Items.Insert(myListaTdn.Items.Count, lst);
            }
        }

        public SqlDataReader Cargar_GridView(int plantaDespacho)
        {
            SqlDataReader reader = null;
            string filtro = "";

            string sql = "";
            sql = " SELECT    CASE WHEN YEAR(Despa_Cliente.DesC_Fecha) IS NULL THEN 0 ELSE YEAR(Despa_Cliente.DesC_Fecha) END AS Año_Despacho, CASE WHEN UPPER(DATENAME(mm, " +
                            " Despa_Cliente.DesC_Fecha)) IS NULL THEN 'N/A' ELSE UPPER(DATENAME(mm, Despa_Cliente.DesC_Fecha)) END AS Mes_Despacho, Despa_Cliente.DesC_Id, " +
                            " Despa_Cliente.DesC_DespNo AS NumDespacho, Despa_Cliente.DesC_Fec_crea AS FechaCreaDespacho, Despa_Cliente.DesC_Fecha AS FechaDespacho, " +
                            " cliente.cli_nombre AS Cliente, RTRIM(Orden_Seg.Tipo_Of) + ' ' + RTRIM(Orden_Seg.Num_Of) + '-' + RTRIM(Orden_Seg.Ano_Of) AS Orden, " +
                            " isnull(stuff((SELECT DISTINCT  ' / Orden: ' + cast(vista_ordenes_despacho.Ordenes  AS varchar(max)),' Obra: ' + Vista_Ordenes_Despacho.obr_nombre " +
                            " FROM  vista_ordenes_despacho WHERE(vista_ordenes_despacho.DesC_Id = Despa_Cliente.DesC_Id) FOR xml path('')), 1, 1, ''), '') AS Ordenes, " +
                            " isnull(stuff((SELECT DISTINCT  '  / ' + cast(vista_ordenes_despacho.DesC_Fact_No   AS varchar(max))" +
                            " FROM  vista_ordenes_despacho  WHERE(vista_ordenes_despacho.DesC_Id = Despa_Cliente.DesC_Id) " +
                            " FOR xml path('')), 1, 1, ''), '') AS Facturas, " +
                            " fup_grupo_pais.grpa_gp1_nombre AS ZonaCliente, pais.pai_nombre AS PaisCliente, ciudad.ciu_nombre AS CiudadCliente,Despa_Cliente.DesC_Nal, " +
                            " Despa_Detalle.DesDt_Estatus_Carga,Despa_Cliente.planta_id, " +
                            " CASE WHEN YEAR( Despa_Cliente.DesC_Fec_crea) IS NULL THEN 0 ELSE YEAR( Despa_Cliente.DesC_Fec_crea) END AS año_Crea," +
                            " CASE WHEN UPPER(DATENAME(mm, Despa_Cliente.DesC_Fec_crea)) IS NULL THEN 'N/A' ELSE UPPER(DATENAME(mm,Despa_Cliente.DesC_Fec_crea))" +
                            " END AS mes_Crea " +
                    "FROM            ciudad INNER JOIN Despa_Cliente INNER JOIN fup_grupo_pais INNER JOIN pais ON fup_grupo_pais.grpa_id = pais.pai_grupopais_id " +
                                   " ON Despa_Cliente.DesC_PaisId = pais.pai_id ON ciudad.ciu_id = Despa_Cliente.DesC_CiudadId INNER JOIN" +
                                   " Orden_Seg ON Despa_Cliente.DesC_OfPId = Orden_Seg.Id_Ofa INNER JOIN cliente INNER JOIN " +
                                   " formato_unico ON cliente.cli_id = formato_unico.fup_cli_id ON Orden_Seg.fup = formato_unico.fup_id LEFT OUTER JOIN " +
                                   " Despa_Detalle ON Despa_Cliente.DesC_Id = Despa_Detalle.DesC_Id LEFT OUTER JOIN termino_negociacion INNER JOIN " +
                                   " solicitud_facturacion ON termino_negociacion.ternog_id = solicitud_facturacion.Sf_termino_negociacion ON Orden_Seg.sf_id = solicitud_facturacion.Sf_id" +
                   " WHERE   (Despa_Cliente.DesC_Fec_crea > CONVERT(DATETIME, '2018-01-01 00:00:00', 102)) AND (Despa_Cliente.DesC_Id = Despa_Cliente.DesC_Grp_Id) AND (Despa_Cliente.planta_id=" + plantaDespacho + ")" +
                   " ORDER   BY Despa_Cliente.DesC_Id DESC";

            reader = BdDatos.ConsultarConDataReader(sql);

            return reader;
        }

        public int Actualizar_NoFactura_DespachoNal(int OfPId, string Factura)
        {
            string sql = "UPDATE Despa_Cliente SET DesC_Fact_No ='" + Factura + "' WHERE DesC_OfPId =" + OfPId + "";
            return BdDatos.ejecutarSql(sql);
        }

        public int Actualizar_NoGuia_DespachoNal(int OfPId, string Guias)
        {
            string sql = "UPDATE Despa_Cliente SET DesC_Guias ='" + Guias + "' WHERE DesC_OfPId =" + OfPId + "";
            return BdDatos.ejecutarSql(sql);
        }


        public DataTable Cargar_DtsOrden_DespaNal(int desc_Id)
        {
            string sql = " SELECT    cliente.cli_nombre AS Cliente, RTRIM(Orden_Seg.Tipo_Of) + ' ' + RTRIM(Orden_Seg.Num_Of) + '-' + RTRIM(Orden_Seg.Ano_Of) AS Orden, obra.obr_nombre AS Obra, " +
                                   " ISNULL(solicitud_facturacion.sf_m2, 0) AS M2, ISNULL(SUM(solicitud_facturacion.Sf_vlr_comercial), 0) AS Valor_Comercial, " +
                                   " ISNULL(termino_negociacion.ternog_sigla, termino_negociacion_1.ternog_sigla) AS Tdn, ISNULL(solicitud_facturacion.Sf_termino_negociacion, " +
                                   " Despa_Cliente.DesC_Tdn) AS idTdn, ciudad.ciu_nombre AS Destino, ISNULL(solicitud_facturacion.Sf_transporte, 0) AS Flete_Cotizado, " +
                                   " Despa_Cliente_1.DesC_Fact_No AS Factura, " +
                                   " ciudad.ciu_id, cliente.cli_id, Orden_Seg.Id_Ofa,cliente.cli_tipo,Despa_Cliente_1.DesC_Guias AS No_Guia, " +
                                   " CASE WHEN Convert(date,Despa_Cliente_1.DesC_Fecha,103) IS NULL THEN '' ELSE Convert(date,Despa_Cliente_1.DesC_Fecha,103) END AS DesC_Fecha " +
                         " FROM      cliente INNER JOIN formato_unico ON cliente.cli_id = formato_unico.fup_cli_id INNER JOIN obra ON formato_unico.fup_obr_id = obra.obr_id " +
                                   " INNER JOIN Orden_Seg INNER JOIN Despa_Cliente INNER JOIN Despa_Cliente AS Despa_Cliente_1 ON " +
                                   " Despa_Cliente.DesC_Id = Despa_Cliente_1.DesC_Grp_Id ON Orden_Seg.Id_Ofa = Despa_Cliente_1.DesC_OfPId ON formato_unico.fup_id = Orden_Seg.fup INNER JOIN " +
                                   " fup_grupo_pais INNER JOIN pais ON fup_grupo_pais.grpa_id = pais.pai_grupopais_id ON cliente.cli_pai_id = pais.pai_id INNER JOIN " +
                                   " ciudad ON Despa_Cliente_1.DesC_CiudadId = ciudad.ciu_id LEFT OUTER JOIN termino_negociacion AS termino_negociacion_1 ON " +
                                   " Despa_Cliente.DesC_Tdn = termino_negociacion_1.ternog_id LEFT OUTER JOIN termino_negociacion INNER JOIN " +
                                   " solicitud_facturacion ON termino_negociacion.ternog_id = solicitud_facturacion.Sf_termino_negociacion ON Orden_Seg.sf_id = solicitud_facturacion.Sf_id " +
                        " WHERE     (Despa_Cliente.DesC_Fec_crea > CONVERT(DATETIME, '2018-01-01 00:00:00', 102)) AND(Despa_Cliente_1.DesC_Grp_Id = " + desc_Id + ")  " +
                     " GROUP BY     fup_grupo_pais.grpa_gp1_nombre, pais.pai_nombre, ciudad.ciu_nombre, cliente.cli_nombre, RTRIM(Orden_Seg.Tipo_Of) + ' ' + RTRIM(Orden_Seg.Num_Of) " +
                                    " + '-' + RTRIM(Orden_Seg.Ano_Of), obra.obr_nombre, solicitud_facturacion.sf_m2, termino_negociacion.ternog_sigla, solicitud_facturacion.Sf_termino_negociacion, " +
                                  " Despa_Cliente.DesC_Tdn, termino_negociacion_1.ternog_sigla, ciudad.ciu_nombre, solicitud_facturacion.Sf_transporte, Despa_Cliente_1.DesC_Fact_No, " +
                                  " ciudad.ciu_id, cliente.cli_id, Orden_Seg.Id_Ofa,cliente.cli_tipo,Despa_Cliente_1.DesC_Guias,Despa_Cliente_1.DesC_Fecha ";
            return BdDatos.CargarTabla(sql);
        }

        public SqlDataReader Cargar_DtsOrden(int desc_Id)
        {
            SqlDataReader reader = null;

            string sql = "SELECT    cliente.cli_nombre AS Cliente, RTRIM(Orden_Seg.Tipo_Of) + ' ' + RTRIM(Orden_Seg.Num_Of) + '-' + RTRIM(Orden_Seg.Ano_Of) AS Orden," +
                                  " obra.obr_nombre AS Obra,   ISNULL(solicitud_facturacion.sf_m2, 0) AS M2,ISNULL(SUM(solicitud_facturacion.Sf_vlr_comercial), 0) AS Valor_Comercial, " +
                                  " ISNULL(termino_negociacion.ternog_sigla, termino_negociacion_1.ternog_sigla) AS Tdn, ISNULL(solicitud_facturacion.Sf_termino_negociacion,  " +
                                  " Despa_Cliente.DesC_Tdn) AS idTdn,ciudad.ciu_nombre AS Destino, solicitud_facturacion.Sf_transporte AS Flete_Cotizado, Despa_Cliente_1.DesC_Total_Peso AS Peso, " +
                                  " ciudad.ciu_id, cliente.cli_id" +
                         " FROM     termino_negociacion AS termino_negociacion_1 RIGHT OUTER JOIN cliente INNER JOIN formato_unico ON cliente.cli_id = formato_unico.fup_cli_id INNER JOIN " +
                                  " obra ON formato_unico.fup_obr_id = obra.obr_id INNER JOIN Orden_Seg INNER JOIN Despa_Cliente INNER JOIN Despa_Cliente AS Despa_Cliente_1 " +
                                  " ON Despa_Cliente.DesC_Id = Despa_Cliente_1.DesC_Grp_Id ON Orden_Seg.Id_Ofa = Despa_Cliente_1.DesC_OfPId ON formato_unico.fup_id = Orden_Seg.fup INNER JOIN " +
                                  " fup_grupo_pais INNER JOIN pais ON fup_grupo_pais.grpa_id = pais.pai_grupopais_id ON cliente.cli_pai_id = pais.pai_id INNER JOIN " +
                                  " ciudad ON Despa_Cliente_1.DesC_CiudadId = ciudad.ciu_id ON termino_negociacion_1.ternog_id = Despa_Cliente.DesC_Tdn LEFT OUTER JOIN " +
                                  " termino_negociacion INNER JOIN solicitud_facturacion ON termino_negociacion.ternog_id = solicitud_facturacion.Sf_termino_negociacion " +
                                  " ON Orden_Seg.sf_id = solicitud_facturacion.Sf_id " +
                       " WHERE(Despa_Cliente.DesC_Fec_crea > CONVERT(DATETIME, '2018-01-01 00:00:00', 102)) AND (Despa_Cliente_1.DesC_Grp_Id = " + desc_Id + ")" +
                       " GROUP BY fup_grupo_pais.grpa_gp1_nombre, pais.pai_nombre, ciudad.ciu_nombre, cliente.cli_nombre," +
                              " RTRIM(Orden_Seg.Tipo_Of) + ' ' + RTRIM(Orden_Seg.Num_Of) + '-' + RTRIM(Orden_Seg.Ano_Of), obra.obr_nombre," +
                              " solicitud_facturacion.sf_m2, termino_negociacion.ternog_sigla,solicitud_facturacion.Sf_termino_negociacion, " +
                              " Despa_Cliente.DesC_Tdn, termino_negociacion_1.ternog_sigla,ciudad.ciu_nombre, solicitud_facturacion.Sf_transporte," +
                              " Despa_Cliente_1.DesC_Total_Peso,ciudad.ciu_id,cliente.cli_id";

            reader = BdDatos.ConsultarConDataReader(sql);
            return reader;
        }


        public DataSet Cargar_DtsOrden_(int desc_Id)
        {
            string sql = "SELECT    cliente.cli_nombre AS Cliente, RTRIM(Orden_Seg.Tipo_Of) + ' ' + RTRIM(Orden_Seg.Num_Of) + '-' + RTRIM(Orden_Seg.Ano_Of) AS Orden," +
                                " obra.obr_nombre AS Obra,   ISNULL(solicitud_facturacion.sf_m2, 0) AS M2,ISNULL(SUM(solicitud_facturacion.Sf_vlr_comercial), 0) AS Valor_Comercial, " +
                                " ISNULL(termino_negociacion.ternog_sigla,'SIN') AS Tdn, ISNULL(solicitud_facturacion.Sf_termino_negociacion, 0) AS idTdn, " +
                                " ciudad.ciu_nombre AS Destino, solicitud_facturacion.Sf_transporte AS Flete_Cotizado, Despa_Cliente_1.DesC_Total_Peso AS Peso, " +
                                " ciudad.ciu_id, cliente.cli_id" +
                       " FROM     termino_negociacion AS termino_negociacion_1 RIGHT OUTER JOIN cliente INNER JOIN formato_unico ON cliente.cli_id = formato_unico.fup_cli_id INNER JOIN " +
                                " obra ON formato_unico.fup_obr_id = obra.obr_id INNER JOIN Orden_Seg INNER JOIN Despa_Cliente INNER JOIN Despa_Cliente AS Despa_Cliente_1 " +
                                " ON Despa_Cliente.DesC_Id = Despa_Cliente_1.DesC_Grp_Id ON Orden_Seg.Id_Ofa = Despa_Cliente_1.DesC_OfPId ON formato_unico.fup_id = Orden_Seg.fup INNER JOIN " +
                                " fup_grupo_pais INNER JOIN pais ON fup_grupo_pais.grpa_id = pais.pai_grupopais_id ON cliente.cli_pai_id = pais.pai_id INNER JOIN " +
                                " ciudad ON Despa_Cliente_1.DesC_CiudadId = ciudad.ciu_id ON termino_negociacion_1.ternog_id = Despa_Cliente.DesC_Tdn LEFT OUTER JOIN " +
                                " termino_negociacion INNER JOIN solicitud_facturacion ON termino_negociacion.ternog_id = solicitud_facturacion.Sf_termino_negociacion " +
                                " ON Orden_Seg.sf_id = solicitud_facturacion.Sf_id " +
                     " WHERE(Despa_Cliente.DesC_Fec_crea > CONVERT(DATETIME, '2018-01-01 00:00:00', 102)) AND (Despa_Cliente_1.DesC_Grp_Id = " + desc_Id + ")" +
                     " GROUP BY fup_grupo_pais.grpa_gp1_nombre, pais.pai_nombre, ciudad.ciu_nombre, cliente.cli_nombre," +
                            " RTRIM(Orden_Seg.Tipo_Of) + ' ' + RTRIM(Orden_Seg.Num_Of) + '-' + RTRIM(Orden_Seg.Ano_Of), obra.obr_nombre," +
                            " solicitud_facturacion.sf_m2, termino_negociacion.ternog_sigla,solicitud_facturacion.Sf_termino_negociacion, " +
                            " Despa_Cliente.DesC_Tdn, termino_negociacion_1.ternog_sigla,ciudad.ciu_nombre, solicitud_facturacion.Sf_transporte," +
                            " Despa_Cliente_1.DesC_Total_Peso,ciudad.ciu_id,cliente.cli_id";

            return BdDatos.consultarConDataset(sql);
        }


        public int CerrarConexion()
        {
            return BdDatos.desconectar();
        }
        public SqlDataReader PoblarDatosGenerales(int desc_Id)
        {
            string sql;
     
            sql = "SELECT DISTINCT Despa_Cliente.DesC_Id AS IdDespacho, CASE WHEN UPPER(DATENAME(mm, Despa_Cliente.DesC_Fecha)) IS NULL THEN 'N/A' ELSE UPPER(DATENAME(mm, Despa_Cliente.DesC_Fecha)) END AS Mes," +
                                 " fup_grupo_pais.grpa_gp1_nombre AS ZonaDespacho, pais.pai_nombre AS PaisDespacho, ciudad.ciu_nombre AS CiudadDespacho,SUM(isnull(solicitud_facturacion.Sf_vlr_comercial, 0))" +
                                 " AS Valor_Comercial,isnull(stuff((SELECT DISTINCT  ' *' + cast(vista_ordenes_despacho.DesC_Fact_No   AS varchar(max)) FROM  vista_ordenes_despacho WHERE " +
                                 " (vista_ordenes_despacho.DesC_Id = Despa_Cliente.DesC_Id) FOR xml path('')), 1, 1, ''), '') AS Facturas, ISNULL(Despa_Cliente.DesC_Medio_Trasnsporte, 0) AS DesC_Medio_Trasnsporte, " +
                                 " ISNULL(Despa_Cliente.DesC_Tipo_Vehiculo, '') AS DesC_Tipo_Vehiculo,CASE WHEN isnull(Despa_Cliente.DesC_ValorEXW,0) = 0 THEN  SUM(isnull(solicitud_facturacion.Sf_vlr_comercial, 0)) ELSE Despa_Cliente.DesC_ValorEXW  END AS Valor_Exw, ISNULL(Despa_Cliente.DesC_ValorFob, 0) AS DesC_ValorFob, " +
                                 " ISNULL(Despa_Tipo_Vehiculo.DesTipVeh_Descripcion, '') AS DesTipVeh_Descripcion, ISNULL(Despa_Tip_Trans.Tipo_Nombre, '') AS Tipo_Nombre, SUM(ISNULL(solicitud_facturacion.sf_m2, 0)) AS M2, " +
                                 " pais.pai_id, ISNULL(Despa_Cliente.DesC_Tdn, 0) AS DesC_Tdn, SUM(isnull(solicitud_facturacion.sf_subtotal, 0)) AS sf_subtotal,Despa_Cliente.DesC_Nal, Despa_Cliente.DesC_Fecha, " +
                                 " CASE WHEN isnull(Despa_Cliente.DesC_Valor_Total_Factura,0) = 0 THEN   SUM(isnull(solicitud_facturacion.sf_subtotal, 0)) ELSE Despa_Cliente.DesC_Valor_Total_Factura END AS DesC_Valor_Total_Factura, CASE WHEN CONVERT(DATE, MAX(fup_acta_seguimiento.actseg_fecEntregaCliente), 103) IS NULL THEN '1900-01-01' " +
                                 " ELSE MAX(fup_acta_seguimiento.actseg_fecEntregaCliente) END AS actseg_fecEntregaCliente, Despa_Cliente.DesC_TSol AS Tipo_Orden, puertos.pto_nombre AS Puerto_Origen,puertos_1.pto_nombre AS Puerto_Destino " +
                          " FROM   solicitud_facturacion WITH (NOLOCK) RIGHT OUTER JOIN cliente WITH (NOLOCK)INNER JOIN formato_unico WITH(NOLOCK) ON cliente.cli_id = formato_unico.fup_cli_id INNER JOIN " +
                                 " obra WITH(NOLOCK) ON formato_unico.fup_obr_id = obra.obr_id INNER JOIN Orden_Seg WITH(NOLOCK) INNER JOIN Despa_Cliente WITH(NOLOCK) INNER JOIN Despa_Cliente AS Despa_Cliente_1 WITH(NOLOCK) ON " +
                                 " Despa_Cliente.DesC_Id = Despa_Cliente_1.DesC_Grp_Id ON Orden_Seg.Id_Ofa = Despa_Cliente_1.DesC_OfPId ON formato_unico.fup_id = Orden_Seg.fup INNER JOIN ciudad WITH(NOLOCK) ON Despa_Cliente.DesC_CiudadId " +
                                 " = ciudad.ciu_id INNER JOIN fup_grupo_pais WITH(NOLOCK) INNER JOIN pais WITH(NOLOCK) ON fup_grupo_pais.grpa_id = pais.pai_grupopais_id ON Despa_Cliente.DesC_PaisId = pais.pai_id INNER JOIN " +
                                 " Despa_Grupo ON Despa_Cliente.DesC_Id = Despa_Grupo.Id_Grupo INNER JOIN puertos ON Despa_Grupo.Puerto_Embarque = puertos.pto_id INNER JOIN puertos AS puertos_1 ON Despa_Grupo.Puerto_Destino = puertos_1.pto_id LEFT OUTER JOIN " +
                                 " Despa_Tip_Trans WITH(NOLOCK) ON Despa_Cliente.DesC_Medio_Trasnsporte = Despa_Tip_Trans.Tipo_Tra_Id LEFT OUTER JOIN fup_acta_seguimiento ON Orden_Seg.Id_Ofa = fup_acta_seguimiento.actseg_idofp AND formato_unico.fup_id =  " +
                                 " fup_acta_seguimiento.actseg_fup_id ON solicitud_facturacion.Sf_id = Orden_Seg.sf_id LEFT OUTER JOIN Despa_Tipo_Vehiculo WITH(NOLOCK) ON Despa_Cliente.DesC_Tipo_Vehiculo = Despa_Tipo_Vehiculo.DesTipVeh_Id " +
                  " WHERE  (Despa_Cliente.DesC_Fec_crea > CONVERT(DATETIME, '2018-01-01 00:00:00', 102)) AND (Despa_Cliente_1.DesC_Grp_Id = " + desc_Id + ") " +
               " GROUP BY   Despa_Cliente.DesC_Id, Despa_Cliente.DesC_Fecha, fup_grupo_pais.grpa_gp1_nombre, pais.pai_nombre, ciudad.ciu_nombre, Despa_Cliente.DesC_Medio_Trasnsporte, Despa_Cliente.DesC_Tipo_Vehiculo, " +
                          " Despa_Cliente.DesC_ValorEXW, Despa_Cliente.DesC_ValorFob, Despa_Tipo_Vehiculo.DesTipVeh_Descripcion, Despa_Tip_Trans.Tipo_Nombre, pais.pai_id, Despa_Cliente.DesC_Tdn, Despa_Cliente.DesC_Nal, " +
                          " Despa_Cliente.DesC_Fecha, Despa_Cliente.DesC_Valor_Total_Factura,Despa_Cliente.DesC_TSol,puertos.pto_nombre,puertos_1.pto_nombre " +
               " ORDER BY Valor_Comercial DESC";

   

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader Poblar_GastosOperacionales(int desc_id)
        {
            string sql;

            sql = "   SELECT     DesC_Id,ISNULL(DesGst_Prov_Flete_Nal,0) AS DesGst_Prov_Flete_Nal, ISNULL(DesGst_Real_Flete_Nal,0) AS DesGst_Real_Flete_Nal, ISNULL(DesGst_Prov_Aduanero,0) AS DesGst_Prov_Aduanero, " +
                               " ISNULL(DesGst_Real_Aduanero,0) AS DesGst_Real_Aduanero, ISNULL(DesGst_Prov_Puerto,0) AS DesGst_Prov_Puerto, ISNULL(DesGst_Real_Puerto,0) AS DesGst_Real_Puerto, " +
                               " ISNULL(DesGst_Prov_Flete_Int,0)AS DesGst_Prov_Flete_Int, ISNULL(DesGst_Real_Flete_Int,0) AS DesGst_Real_Flete_Int, ISNULL(DesGst_Prov_Destino,0) AS DesGst_Prov_Destino,  " +
                               " ISNULL(DesGst_Real_Destino,0) AS DesGst_Real_Destino, ISNULL(DesGst_Prov_Sello_Sateli,0) AS DesGst_Prov_Sello_Sateli, DesGst_Real_Sello_Sateli,  " +
                               " ISNULL(DesGst_Prov_Tot_GastFlete,0) AS DesGst_Prov_Tot_GastFlete, ISNULL(DesGst_Real_Tot_GastFlete,0) AS DesGst_Real_Tot_GastFlete, " +
                               " ISNULL(DesGst_Dif_Provisiones,0) AS DesGst_Dif_Provisiones, ISNULL(DesGst_Ahorro,0) AS DesGst_Ahorro, ISNULL(DesGst_StandBy,0) AS DesGst_StandBy, " +
                               " ISNULL(DesGst_Trm_Fecha_Fact,0) AS DesGst_Trm_Fecha_Fact, ISNULL(DesGst_Bodegaje,0) AS DesGst_Bodegaje, ISNULL(DesGst_Peso_Fact,0) AS DesGst_Peso_Fact, " +
                               " ISNULL(DesGst_Envio_Ctrl_Empaque, '')  AS DesGst_Envio_Ctrl_Empaque , DesGst_Concatenar, DesGst_Cerrado,ISNULL(DesGst_GastFletesFactu,0) AS DesGst_GastFletesFactu, " +
                               " ISNULL(DesGst_Seguro,0) AS DesGst_Seguro, ISNULL(DesGst_Inspe_AntiNarcotico,0) AS DesGst_Inspe_AntiNarcotico, ISNULL(DesGst_RollOver,0) AS DesGst_RollOver, " +
                               " ISNULL(DesGst_Manejo_Flete,0) AS DesGst_Manejo_Flete, ISNULL(DesGst_Aduana_Destino,0) AS DesGst_Aduana_Destino, ISNULL(DesGst_Bodegaje_Destino,0) AS DesGst_Bodegaje_Destino, " +
                               " ISNULL(DesGst_Transporte_Destino,0) AS DesGst_Transporte_Destino, ISNULL(DesGst_Demora_Destino,0) AS DesGst_Demora_Destino, ISNULL(DesGst_Impuesto_Destino,0) AS DesGst_Impuesto_Destino " +
                     " FROM      Despa_Gastos_Operacion " +
                    " WHERE(DesC_Id = " + desc_id + ")";

            return BdDatos.ConsultarConDataReader(sql);
        }



        public SqlDataReader Poblar_DetalleDespaNacional(int desc_id, string orden)
        {
            string sql = "SELECT DISTINCT   Despa_Detalle_Nacional.DesDtNal_Orden, " +
                                         " CASE WHEN DesDtNal_Factura IS NULL THEN Despa_Cliente.DesC_Fact_No ELSE DesDtNal_Factura END AS DesDtNal_Factura, " +
                                         " Despa_Detalle_Nacional.DesDtNal_Cliente, Despa_Detalle_Nacional.DesDtNal_Ciud_Destino," +
                                         " Despa_Detalle_Nacional.DesDtNal_EmpresaTransp, Despa_Detalle_Nacional.DesDtNal_Fecha_Entrega, " +
                                         " CASE WHEN  Despa_Detalle_Nacional.DesDtNal_NoGuia IS NULL THEN Despa_Cliente.DesC_Guias ELSE Despa_Detalle_Nacional.DesDtNal_NoGuia END AS DesDtNal_NoGuia," +
                                         " Despa_Detalle_Nacional.DesDtNal_Resp_Trasnp, Despa_Detalle_Nacional.DesDtNal_ValorExw, " +
                                         " Despa_Detalle_Nacional.DesDtNal_Flete_Cotiza, Despa_Detalle_Nacional.DesDtNal_Flete_real,  " +
                                         " Despa_Detalle_Nacional.DesDtNal_RelFlete_Valor, Despa_Detalle_Nacional.DesDtNal_Peso, Despa_Detalle_Nacional.DesDtNal_StanBy, " +
                                         " Despa_Detalle_Nacional.DesDtNal_Indicador, Despa_Detalle_Nacional.DesDtNal_Cumple_Entrega, Despa_Detalle_Nacional.DesDtNal_UsuCrea, " +
                                         " Despa_Detalle_Nacional.DesDtNal_FechaCrea, " +
                                         " CASE WHEN (SELECT  CONVERT(DATETIME,DesC_Fecha,103) FROM Despa_Cliente WHERE DesC_NoSolP = SUBSTRING(Despa_Detalle_Nacional.DesDtNal_Orden, 4, 10)) " +
                                         " IS NULL THEN '' ELSE (SELECT  CONVERT(DATETIME,DesC_Fecha,103) FROM Despa_Cliente WHERE DesC_NoSolP = SUBSTRING(Despa_Detalle_Nacional.DesDtNal_Orden, 4, 10)) " +
                                         " END  AS Fecha_Despacho " +
                                   " FROM Despa_Detalle_Nacional INNER JOIN Despa_Cliente ON Despa_Detalle_Nacional.DesC_Id = Despa_Cliente.DesC_Id " +
                          " WHERE    (Despa_Detalle_Nacional.DesC_Id = " + desc_id + ") AND (DesDtNal_Orden LIKE '%" + orden + "%')";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader Poblar_Detalle_Despacho(int desc_id)
        {
            string sql;

            sql = " SELECT    DesDt_Documento_Trasnp,DesDt_Dex_FMM,DesDt_No_Guia_Dtos,DesDt_Estatus_Carga,DesDt_Embarcador," +
                                  " ISNULL(DesDt_Fecha_Envio_Dtos, '')  AS DesDt_Fecha_Envio_Dtos," +
                                  " CASE WHEN DesDt_Tiempo_Envio_Dtos = 255 THEN NULL ELSE DesDt_Tiempo_Envio_Dtos  END AS DesDt_Tiempo_Envio_Dtos, " +
                                  " CASE WHEN DesDt_Efecti_Envio_Dtos = 255 THEN NULL ELSE DesDt_Efecti_Envio_Dtos END AS DesDt_Efecti_Envio_Dtos," +
                                  " DesDt_Inspe_Narcoticos," +
                                  " ISNULL(DesDt_Estima_Despacho, '')  AS DesDt_Estima_Despacho," +
                                  " ISNULL(DesDt_Real_Despacho, '')   AS DesDt_Real_Despacho," +
                                  " ISNULL(DesDt_Estima_Zarpe, '')  AS DesDt_Estima_Zarpe," +
                                  " ISNULL(DesDt_Real_Zarpe, '')  AS DesDt_Real_Zarpe, " +
                                  " ISNULL(DesDt_Estima_Arribo, '')  AS DesDt_Estima_Arribo," +
                                  " ISNULL(DesDt_ConfirmaArribo , '')  AS DesDt_ConfirmaArribo," +
                                  " ISNULL(DesDt_Estima_Llega_Obra, '')  AS DesDt_Estima_Llega_Obra," +
                                  " ISNULL(DesDt_Real_Llega_Obra,'') AS DesDt_Real_Llega_Obra," +
                                  " CASE WHEN DesDt_Tt_Internacional = 255 THEN NULL ELSE DesDt_Tt_Internacional END AS DesDt_Tt_Internacional," +
                                  " CASE WHEN DesDt_Tt_PuertDest_Cli = 255 THEN NULL ELSE DesDt_Tt_PuertDest_Cli END AS DesDt_Tt_PuertDest_Cli," +
                                  " CASE WHEN DesDt_Tt_Planta_Cli = 255 THEN NULL ELSE DesDt_Tt_Planta_Cli END AS DesDt_Tt_Planta_Cli," +
                                  " CASE WHEN DesDt_Efecti_Entrega = 255 THEN NULL ELSE DesDt_Efecti_Entrega END AS DesDt_Efecti_Entrega," +
                                  " DesDt_Usu_Crea,DesDt_Fecha_Crea,DesDt_Cerrado," +
                                  " ISNULL( DesDt_Arribo_Finaliza, '')  AS  DesDt_Arribo_Finaliza, " +
                                  " CASE WHEN DesDt_Dias_Lib_Demora = 99 THEN NULL ELSE DesDt_Dias_Lib_Demora END AS DesDt_Dias_Lib_Demora," +
                                  " ISNULL( DesDt_F_Inspeccion, '')  AS  DesDt_F_Inspeccion, " +
                                  " DesDt_Canal, " +
                                  " ISNULL( DesDt_CI_Nacionalizacion, '')  AS  DesDt_CI_Nacionalizacion, " +
                                  " ISNULL( DesDt_Fact_Provee_Forsa, '')  AS  DesDt_Fact_Provee_Forsa, " +
                                  " ISNULL(  DesDt_CI_Impuestos, '')  AS   DesDt_CI_Impuestos, " +
                                  " ISNULL(  DesDt_Retiro_Conten, '')  AS   DesDt_Retiro_Conten, " +
                                  " ISNULL(  DesDt_Desove_Conten, '')  AS   DesDt_Desove_Conten, " +
                                  " ISNULL(  DesDt_Almacena_Cargo, '')  AS   DesDt_Almacena_Cargo, " +
                                  " ISNULL(  DesDt_Notifica_Cliente, '')  AS   DesDt_Notifica_Cliente, " +
                                  " ISNULL(  DesDt_Devolu_Cliente, '')  AS   DesDt_Devolu_Cliente, " +
                                  " ISNULL(  DesDt_Fact_Forsa_Cliente, '')  AS   DesDt_Fact_Forsa_Cliente, " +
                                  " ISNULL(  DesDt_Carga_Cliente, '')  AS   DesDt_Carga_Cliente, " +
                                  " ISNULL(  DesDt_Entrega_Obra, '')  AS   DesDt_Entrega_Obra, " +
                                  " DesDt_Cerrado_Tramite," +
                                  " ISNULL(  DesDt_Fech_Documentado, '')  AS   DesDt_Fech_Documentado, " +
                                 " ISNULL(  DesDt_Modo_Trans_Despacho, '')  AS   DesDt_Modo_Trans_Despacho, " +
                                 " ISNULL(DesDt_Estima_ArriboMod, '')  AS DesDt_Estima_ArriboMod," +
                                 " ISNULL(DesDt_Estima_Llega_ObraMod, '')  AS DesDt_Estima_Llega_ObraMod " +
                       " FROM Despa_Detalle" +
                       " WHERE DesC_Id = " + desc_id + "";

            return BdDatos.ConsultarConDataReader(sql);
        }


        public DataSet Cargar_DtsTrasnporte(int desc_Id)
        {
            string sql = "SELECT DISTINCT " +
                                          " Desp_Transporte.Desp_Vehi_Placa AS Transporte, " +
                                          " Desp_Container.Cont_L + '' + Desp_Container.Cont_No + '-' + Desp_Container.Cont_V AS Contenedor," +
                                          " Desp_Container.Cont_Capacidad AS Cap_Contenedor, " +
                                          " Despacho_Estibas.Pallet_Ofa AS Orden, Trans_Carga.Trans_Nombre AS Empresa_Transportadora " +
                          " FROM            Trans_Carga INNER JOIN Desp_Transporte ON Trans_Carga.Id_Trans = Desp_Transporte.Desp_Trans_TransId " +
                                          " INNER JOIN Despa_Cliente ON Desp_Transporte.Desp_Trans_DespId = Despa_Cliente.DesC_Id " +
                                          " RIGHT OUTER JOIN Despacho_Estibas LEFT OUTER JOIN Desp_Container " +
                                          " ON Despacho_Estibas.idcontainer = Desp_Container.Id_Contene ON " +
                                          " Desp_Transporte.Desp_Trans_Id = Despacho_Estibas.Pallet_Trans_Id " +
                          " WHERE     (Despa_Cliente.DesC_Grp_Id =" + desc_Id + ") AND (Desp_Transporte.Desp_Trans_Anula = 0) " +
                                  " AND (Desp_Container.Cont_Anula = 0) AND (Trans_Carga.Tras_InActiva = 0)";
            return BdDatos.consultarConDataset(sql);
        }
        public DataTable ObtenerOrdenObra(int desc_Id)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT Ordenes + ' / ' + obr_nombre AS OrdenObra FROM Vista_Ordenes_Despacho WHERE desc_id = " + desc_Id + "";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public DataTable ObtenerFacturas(int desc_Id)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT DesC_Fact_No AS Facturas  FROM Vista_Ordenes_Despacho WHERE desc_id = " + desc_Id + "";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public int Registrar_Detalle_Despacho(int descId, string documenTrans, string dexFmm, string noGuia, int estatusCarga, string embarcador, string envioDtos,
                                              int tiempoEnvDtos, int efectiEnvioDtos, bool inspnarcoti, string estimaDespa, string realDespa, string estimaZarpe,
                                              string RealZarpe, string estimaArribo, string confirmaArribo, string estimaLlegaObra, string realLlegaObra,
                                              int ttInternal, int ttPuertDestinoCli, int ttPlantaCli, int efectiEntrega, string usuCrea, string fechaCrea, bool cerrado,
                                              string fechDocumentado,string modoTransDespacho, string estimaArriboMod, string estimaLlegaObraMod)
        {
            string sql = " INSERT INTO DESPA_DETALLE (DesC_Id, DesDt_Documento_Trasnp, DesDt_Dex_FMM, DesDt_No_Guia_Dtos, DesDt_Estatus_Carga, DesDt_Embarcador, DesDt_Fecha_Envio_Dtos, " +
                                                    " DesDt_Tiempo_Envio_Dtos, DesDt_Efecti_Envio_Dtos,DesDt_Inspe_Narcoticos, DesDt_Estima_Despacho, DesDt_Real_Despacho, DesDt_Estima_Zarpe, " +
                                                    " DesDt_Real_Zarpe, DesDt_Estima_Arribo, DesDt_ConfirmaArribo, DesDt_Estima_Llega_Obra, DesDt_Real_Llega_Obra, " +
                                                    " DesDt_Tt_Internacional, DesDt_Tt_PuertDest_Cli, DesDt_Tt_Planta_Cli, DesDt_Efecti_Entrega, DesDt_Usu_Crea," +
                                                    " DesDt_Fecha_Crea, DesDt_Cerrado,DesDt_Fech_Documentado,DesDt_Modo_Trans_Despacho,DesDt_Estima_ArriboMod,DesDt_Estima_Llega_ObraMod) " +
                                           " VALUES(" + descId.ToString() + ",'" + documenTrans + "','" + dexFmm + "','" + noGuia + "', " +
                                                    " " + estatusCarga.ToString() + ",'" + embarcador + "',CONVERT(DATE,'" + envioDtos + "',103), " +
                                                   " " + tiempoEnvDtos.ToString() + "," + efectiEnvioDtos.ToString() + ",'" + inspnarcoti + "', " +
                                                   " CONVERT(DATE,'" + estimaDespa + "',103),CONVERT(DATE,'" + realDespa + "',103),CONVERT(DATE,'" + estimaZarpe + "',103), " +
                                                   " CONVERT(DATE,'" + RealZarpe + "',103),CONVERT(DATE,'" + estimaArribo + "',103),CONVERT(DATE,'" + confirmaArribo + "',103), " +
                                                   " CONVERT(DATE,'" + estimaLlegaObra + "',103),CONVERT(DATE,'" + realLlegaObra + "',103)," +
                                                   " " + ttInternal.ToString() + "," + ttPuertDestinoCli.ToString() + "," + ttPlantaCli.ToString() + "," +
                                                   " " + efectiEntrega.ToString() + ",'" + usuCrea + "','" + fechaCrea + "','" + cerrado + "',CONVERT(DATE,'" + fechDocumentado + "',103),'"+ modoTransDespacho+ "',"+
                                                   " CONVERT(DATE,'" + estimaArriboMod + "',103),CONVERT(DATE,'" + estimaLlegaObraMod + "',103)  )";
            //(Desc_Id, DesDt_Documento_Trasnp, DesDt_Dex_FMM, DesDt_No_Guia_Dtos, DesDt_Estatus_Carga, " +
            //                                       " DesDt_Embarcador,DesDt_Fecha_Envio_Dtos,DesDt_Tiempo_Envio_Dtos, DesDt_Efecti_Envio_Dtos, " +
            //                                       " DesDt_Inspe_Narcoticos,DesDt_Estima_Despacho,DesDt_Real_Despacho,DesDt_Estima_Zarpe, " +
            //                                       " DesDt_Real_Zarpe,DesDt_Estima_Arribo,DesDt_ConfirmaArribo,DesDt_Estima_Llega_Obra, " +
            //                                       " DesDt_Real_Llega_Obra,DesDt_Tt_Internacional,DesDt_Tt_PuertDest_Cli,DesDt_Tt_Planta_Cli, " +
            //                                       " DesDt_Efecti_Entrega,DesDt_LeadTimeEspera,DesDt_Usu_Crea, DesDt_Fecha_Crea) " +

            return BdDatos.ejecutarSql(sql);
        }

        public int Registrar_Detalle_Despacho_Nacional(int descId, string orden, string factura, int cliente, int destino, string empresaTransp, string fechaEntrega, string numeroGuia, string resptrasnp,
                                          decimal valorExw, decimal fleteCotiza, decimal fleteReal, string realFleteValor, decimal peso, decimal stanby,
                                         int indicador, bool cumpleEntrega, string usuCrea, string fechaCrea, string fechaDespacho)
        {
            string sql = " INSERT INTO Despa_Detalle_Nacional VALUES(" + descId.ToString() + ",'" + orden + "','" + factura + "'," + cliente + "," + destino + ",'" + empresaTransp + "', CONVERT(DATE, '" + fechaEntrega + "', 103), " +
                                                                     " '" + numeroGuia + "','" + resptrasnp + "'," + valorExw + "," + fleteCotiza + "," + fleteReal + ",'" + realFleteValor + "'," + peso + "," + stanby + "," +
                                                                     " " + indicador + ",'" + cumpleEntrega + "','" + usuCrea + "','" + fechaCrea + "','" + fechaDespacho + "')";

            return BdDatos.ejecutarSql(sql);
        }


        public int Actualizar_Detalle_Despacho_Nacional(string orden, string factura, int cliente, int destino, string empresaTransp, string fechaEntrega, string numeroGuia, string resptrasnp,
                                          decimal valorExw, decimal fleteCotiza, decimal fleteReal, string realFleteValor, decimal peso, decimal stanby,
                                         int indicador, bool cumpleEntrega, string fechaDespacho, int descId)
        {
            string sql = "UPDATE  Despa_Detalle_Nacional SET  DesDtNal_Orden='" + orden + "', DesDtNal_Factura='" + factura + "', DesDtNal_Cliente=" + cliente + ", " +
                                " DesDtNal_Ciud_Destino=" + destino + ", DesDtNal_EmpresaTransp='" + empresaTransp + "', DesDtNal_Fecha_Entrega='" + fechaEntrega + "', " +
                                " DesDtNal_NoGuia='" + numeroGuia + "', DesDtNal_Resp_Trasnp='" + resptrasnp + "', DesDtNal_ValorExw=" + valorExw + ", DesDtNal_Flete_Cotiza=" + fleteCotiza + ", " +
                                " DesDtNal_Flete_real=" + fleteReal + ", DesDtNal_RelFlete_Valor='" + realFleteValor + "', DesDtNal_Peso=" + peso + ", DesDtNal_StanBy=" + stanby + ", " +
                                " DesDtNal_Indicador=" + indicador + ", DesDtNal_Cumple_Entrega='" + cumpleEntrega + "', DesDtNal_FechaDespacho='" + fechaDespacho + "' " +
                         " WHERE DesC_Id =" + descId + " AND DesDtNal_Orden = '" + orden + "'";

            return BdDatos.ejecutarSql(sql);
        }



        public int Actualizar_Detalle_Despacho(string documenTrans, string dexFmm, string noGuia, int estatusCarga, string embarcador, string envioDtos,
                                              int tiempoEnvDtos, int efectiEnvioDtos, bool inspnarcoti, string estimaDespa, string realDespa, string estimaZarpe,
                                              string RealZarpe, string estimaArribo, string confirmaArribo, string estimaLlegaObra, string realLlegaObra,
                                              int ttInternal, int ttPuertDestinoCli, int ttPlantaCli, int efectiEntrega, bool cerrado, string fechDocumentado,string modoTransDespacho,int descId,
                                              string estimaArriboMod, string estimaLlegaObraMod)
        {
            string sql = "UPDATE Despa_Detalle SET DesDt_Documento_Trasnp='" + documenTrans + "',DesDt_Dex_FMM='" + dexFmm + "',DesDt_No_Guia_Dtos='" + noGuia + "', DesDt_Estatus_Carga=" + estatusCarga + ", " +
                                                " DesDt_Embarcador='" + embarcador + "',DesDt_Fecha_Envio_Dtos='" + envioDtos + "',DesDt_Tiempo_Envio_Dtos=" + tiempoEnvDtos + ", " +
                                                " DesDt_Efecti_Envio_Dtos = " + efectiEnvioDtos + ",DesDt_Inspe_Narcoticos ='" + inspnarcoti + "',DesDt_Estima_Despacho = '" + estimaDespa + "', " +
                                                " DesDt_Real_Despacho = '" + realDespa + "',DesDt_Estima_Zarpe = '" + estimaZarpe + "',DesDt_Real_Zarpe = '" + RealZarpe + "',DesDt_Estima_Arribo = '" + estimaArribo + "', " +
                                                " DesDt_ConfirmaArribo = '" + confirmaArribo + "',DesDt_Estima_Llega_Obra = '" + estimaLlegaObra + "',DesDt_Real_Llega_Obra = '" + realLlegaObra + "', " +
                                                " DesDt_Tt_Internacional = " + ttInternal + ",DesDt_Tt_PuertDest_Cli = " + ttPuertDestinoCli + ",DesDt_Tt_Planta_Cli = " + ttPlantaCli + ", " +
                                                " DesDt_Efecti_Entrega = " + efectiEntrega + ",DesDt_Cerrado='" + cerrado + "',DesDt_Fech_Documentado= '" + fechDocumentado + "',DesDt_Modo_Trans_Despacho= '" + modoTransDespacho + "', " +
                                                " DesDt_Estima_ArriboMod = '" + estimaArriboMod + "',DesDt_Estima_Llega_ObraMod = '" + estimaLlegaObraMod + "' " +
                      " WHERE DesC_Id = " + descId + "";

            return BdDatos.ejecutarSql(sql);
        }


        public int Actualizar_Tramites_Despacho(string Arribo_Finaliza, int Dias_Lib_Demora, string F_Inspeccion,
                         int Canal, string CI_Nacionalizacion, string Fact_Provee_Forsa, string CI_Impuestos,
                         string Retiro_Conten, string Desove_Conten, string Almacena_Cargo, string Notifica_Cliente, string Devolu_Cliente,
                         string Fact_Forsa_Cliente, string Carga_Cliente, string Entrega_Obra, bool Cerrado_Tramite, int descId)
        {
            string sql = "UPDATE Despa_Detalle SET DesDt_Arribo_Finaliza=Convert(date,'" + Arribo_Finaliza + "',103), DesDt_Dias_Lib_Demora=" + Dias_Lib_Demora + ", DesDt_F_Inspeccion=Convert(date,'" + F_Inspeccion + "',103), " +
                                                 " DesDt_Canal=" + Canal + ", DesDt_CI_Nacionalizacion=Convert(date,'" + CI_Nacionalizacion + "',103), DesDt_Fact_Provee_Forsa=Convert(date,'" + Fact_Provee_Forsa + "',103), " +
                                                 " DesDt_CI_Impuestos=Convert(date,'" + CI_Impuestos + "',103), DesDt_Retiro_Conten=Convert(date,'" + Retiro_Conten + "',103), DesDt_Desove_Conten=Convert(date,'" + Desove_Conten + "',103), " +
                                                 " DesDt_Almacena_Cargo=Convert(date,'" + Almacena_Cargo + "',103), DesDt_Notifica_Cliente=Convert(date,'" + Notifica_Cliente + "',103), DesDt_Devolu_Cliente=Convert(date,'" + Devolu_Cliente + "',103), " +
                                                 " DesDt_Fact_Forsa_Cliente=Convert(date,'" + Fact_Forsa_Cliente + "',103), DesDt_Carga_Cliente=Convert(date,'" + Carga_Cliente + "',103), DesDt_Entrega_Obra=Convert(date,'" + Entrega_Obra + "',103), " +
                                                 " DesDt_Cerrado_Tramite='" + Cerrado_Tramite + "',DesDt_ConfirmaArribo=Convert(date,'" + Arribo_Finaliza + "',103),DesDt_Real_Llega_Obra=Convert(date,'" + Entrega_Obra + "',103)" +
                               " WHERE DesC_Id = " + descId + "";

            return BdDatos.ejecutarSql(sql);
        }


        public int Actualizar_Datos_Generales(int medioTrasnp, int tipoVehiculo, decimal valorExw, decimal valorFob, int tdn, int descId, decimal valortotalfac)
        {
            string sql = " UPDATE Despa_Cliente SET DesC_Medio_Trasnsporte=" + medioTrasnp + ",DesC_Tipo_Vehiculo=" + tipoVehiculo + ", " +
                                                  " DesC_ValorEXW=" + valorExw + ",DesC_ValorFob=" + valorFob + ",DesC_Tdn=" + tdn + ",DesC_Valor_Total_Factura=" + valortotalfac + " " +
                         " WHERE DesC_Id =" + descId + "";
            return BdDatos.ejecutarSql(sql);
        }

        public int Actualizar_Dtos_GastosOperacion(decimal pvFletNal, decimal rlFletNal, decimal pvAduanero, decimal rlAduanero, decimal pvPuerto, decimal rlPuerto,
                                                decimal pvFletInt, decimal rlFletInt, decimal pvDestino, decimal rlDestino, decimal pvSelloSatel, decimal rlSelloSatel,
                                                decimal pvTolGastFlet, decimal rlTolGastFlet, decimal difprovisiones, string ahorro, decimal stanby, decimal trmFechFact,
                                                decimal bodegaje, decimal pesoFact, string envioCtrlEmpaq, string concatenar, bool cerrado, decimal gastflefact,
                                                decimal seguro, decimal inspNarc, decimal rollOver, decimal manejoFlet, decimal aduanDest, decimal bodegajeDest, 
                                                decimal transDest, decimal demoraDest, decimal impuestoDest, int descId)
        {
            string sql = " UPDATE     Despa_Gastos_Operacion SET DesGst_Prov_Flete_Nal =" + pvFletNal + ", DesGst_Real_Flete_Nal=" + rlFletNal + ", " +
                                                               " DesGst_Prov_Aduanero =" + pvAduanero + ", DesGst_Real_Aduanero=" + rlAduanero + ", " +
                                                               " DesGst_Prov_Puerto=" + pvPuerto + ", DesGst_Real_Puerto=" + rlPuerto + ", " +
                                                               " DesGst_Prov_Flete_Int=" + pvFletInt + ", DesGst_Real_Flete_Int=" + rlFletInt + ", " +
                                                               " DesGst_Prov_Destino=" + pvDestino + ", DesGst_Real_Destino=" + rlDestino + ", " +
                                                               " DesGst_Prov_Sello_Sateli=" + pvSelloSatel + ", DesGst_Real_Sello_Sateli=" + rlSelloSatel + "," +
                                                               " DesGst_Prov_Tot_GastFlete=" + pvTolGastFlet + ", DesGst_Real_Tot_GastFlete=" + rlTolGastFlet + ", " +
                                                               " DesGst_Dif_Provisiones=" + difprovisiones + ", DesGst_Ahorro='" + ahorro + "', DesGst_StandBy=" + stanby + ", " +
                                                               " DesGst_Trm_Fecha_Fact=" + trmFechFact + ", DesGst_Bodegaje=" + bodegaje + ", DesGst_Peso_Fact=" + pesoFact + ", " +
                                                               " DesGst_Envio_Ctrl_Empaque='" + envioCtrlEmpaq + "', DesGst_Concatenar='" + concatenar + "', " +
                                                               " DesGst_Cerrado='" + cerrado + "',DesGst_GastFletesFactu=" + gastflefact + ", " +
                                                               " DesGst_Seguro= " + seguro + ",  DesGst_Inspe_AntiNarcotico= " + inspNarc + ", DesGst_RollOver= " + rollOver + ", " +
                                                               " DesGst_Manejo_Flete= " + manejoFlet + ", DesGst_Aduana_Destino= " + aduanDest + ", DesGst_Bodegaje_Destino= " + bodegajeDest + ", " +
                                                               " DesGst_Transporte_Destino= " + transDest + ", DesGst_Demora_Destino= " + demoraDest + ", DesGst_Impuesto_Destino= " + impuestoDest + " " +
                                              " WHERE DesC_Id =" + descId + "";
            return BdDatos.ejecutarSql(sql);
        }

        public int Guardar_Dtos_GastosOperacion(int descId, decimal pvFletNal, decimal rlFletNal, decimal pvAduanero, decimal rlAduanero, decimal pvPuerto, decimal rlPuerto,
                                              decimal pvFletInt, decimal rlFletInt, decimal pvDestino, decimal rlDestino, decimal pvSelloSatel, decimal rlSelloSatel,
                                              decimal pvTolGastFlet, decimal rlTolGastFlet, decimal difprovisiones, string ahorro, decimal stanby, decimal trmFechFact,
                                              decimal bodegaje, decimal pesoFact, string envioCtrlEmpaq, string concatenar, bool cerrado, string usuCrea, string fechaCrea,
                                              decimal gastfletfact, decimal seguro, decimal inspNarc, decimal rollOver, decimal manejoFlet, decimal aduanDest,
                                              decimal bodegajeDest, decimal transDest, decimal demoraDest, decimal impuestoDest)
        {
            string sql = "INSERT  INTO Despa_Gastos_Operacion (DesC_Id, DesGst_Prov_Flete_Nal, DesGst_Real_Flete_Nal, DesGst_Prov_Aduanero, DesGst_Real_Aduanero, DesGst_Prov_Puerto,  " +
                                    " DesGst_Real_Puerto, DesGst_Prov_Flete_Int, DesGst_Real_Flete_Int, DesGst_Prov_Destino, DesGst_Real_Destino, DesGst_Prov_Sello_Sateli,  " +
                                    " DesGst_Real_Sello_Sateli, DesGst_Prov_Tot_GastFlete, DesGst_Real_Tot_GastFlete, DesGst_Dif_Provisiones, DesGst_Ahorro, DesGst_StandBy,  " +
                                    " DesGst_Trm_Fecha_Fact, DesGst_Bodegaje, DesGst_Peso_Fact, DesGst_Envio_Ctrl_Empaque, DesGst_Concatenar, DesGst_Cerrado, DesGst_Usu_Crea, " +
                                    " DesGst_Fecha_Crea,DesGst_GastFletesFactu,DesGst_Seguro, DesGst_Inspe_AntiNarcotico, DesGst_RollOver, DesGst_Manejo_Flete, DesGst_Aduana_Destino, " +
                                    " DesGst_Bodegaje_Destino, DesGst_Transporte_Destino, DesGst_Demora_Destino, DesGst_Impuesto_Destino)  " +
                            " VALUES(" + descId + "," + pvFletNal + "," + rlFletNal + "," + pvAduanero + "," + rlAduanero + "," + pvPuerto + "," + rlPuerto + "," + pvFletInt + ", " +
                            " " + rlFletInt + "," + pvDestino + "," + rlDestino + ", " + pvSelloSatel + "," + rlSelloSatel + "," + pvTolGastFlet + "," + rlTolGastFlet + ", " +
                            " " + difprovisiones + ",'" + ahorro + "'," + stanby + "," + trmFechFact + "," + bodegaje + "," + pesoFact + ", '" + envioCtrlEmpaq + "','" + concatenar + "', " +
                            " '" + cerrado + "','" + usuCrea + "','" + fechaCrea + "'," + gastfletfact + "," + seguro + " ," + inspNarc + "," + rollOver + "," + manejoFlet + ", " +
                            " " + aduanDest + "," + bodegajeDest + "," + transDest + " ," + demoraDest + "," + impuestoDest + " )";

            return BdDatos.ejecutarSql(sql);
        }

        public int Actualizar_Campo_Concatenar(string concatenar, int descId)
        {
            string sql = " UPDATE     Despa_Gastos_Operacion SET  DesGst_Concatenar='" + concatenar + "' " +
                                              " WHERE DesC_Id =" + descId + "";
            return BdDatos.ejecutarSql(sql);
        }

        public int Actualizar_Campo_GastFletesFactu(decimal gastFletFact, int descId)
        {
            string sql = " UPDATE     Despa_Gastos_Operacion SET  DesGst_GastFletesFactu=" + gastFletFact + " " +
                                              " WHERE DesC_Id =" + descId + "";
            return BdDatos.ejecutarSql(sql);
        }



        public int Actualizar_Campo_EfectiEntrega(int efectiEntrega, int descId)
        {
            string sql = " UPDATE  Despa_Detalle SET  DesDt_Efecti_Entrega =" + efectiEntrega + " " +
                                              " WHERE DesC_Id =" + descId + "";
            return BdDatos.ejecutarSql(sql);
        }

      
        public int Guardar_Observacion_Despa(int descId, string detalle, string usuCrea, string fechaCrea, string orden, int estatus)
        {
            string sql = "INSERT INTO Despa_Observaciones (DesC_Id, DesObs_Detalle, DesObs_Usu_Crea, DesObs_Fecha_Crea,DesObs_Orden,DesObs_Estatus) " +
                                                " VALUES(" + descId + ",'" + detalle + "','" + usuCrea + "','" + fechaCrea + "','" + orden + "'," + estatus + ")";
            return BdDatos.ejecutarSql(sql);
        }

        public DataSet Poblar_observaciones_Despa(int descid, string orden)
        {
            string sql;

            if (String.IsNullOrEmpty(orden))
            {
                sql = "SELECT   DesObs_Id,DesObs_Detalle AS Observacion, DesObs_Usu_Crea AS Usu_crea, DesObs_Fecha_Crea AS Fecha, " +
                              " DesObs_Orden AS Orden, 	CASE WHEN Despa_Cliente.DesC_Nal=1 THEN 'N/A' ELSE " +
                              " Despa_EstatusCarga.DesEstc_Descripcion END AS Estatus " +
                      " FROM    Despa_Observaciones INNER JOIN Despa_Cliente ON Despa_Observaciones.DesC_Id =  " +
                              " Despa_Cliente.DesC_Id LEFT OUTER JOIN Despa_EstatusCarga ON Despa_Observaciones.DesObs_Estatus " +
                              " = Despa_EstatusCarga.DesEstc_Id" +
                      " WHERE  (Despa_Observaciones.DesC_Id =" + descid + " AND DesObs_Anulada=0)";
            }
            else
            {
                sql = "SELECT   DesObs_Id,DesObs_Detalle AS Observacion, DesObs_Usu_Crea AS Usu_crea, DesObs_Fecha_Crea AS Fecha, " +
                              " DesObs_Orden AS Orden, 	CASE WHEN Despa_Cliente.DesC_Nal=1 THEN 'N/A' ELSE " +
                              " Despa_EstatusCarga.DesEstc_Descripcion END AS Estatus " +
                      " FROM    Despa_Observaciones INNER JOIN Despa_Cliente ON Despa_Observaciones.DesC_Id =  " +
                          " Despa_Cliente.DesC_Id LEFT OUTER JOIN Despa_EstatusCarga ON Despa_Observaciones.DesObs_Estatus " +
                          " = Despa_EstatusCarga.DesEstc_Id" +
                      " WHERE (Despa_Observaciones.DesC_Id =" + descid + " AND DesObs_Anulada=0 AND DesObs_Orden='" + orden + "' )  OR " +
                            " (Despa_Observaciones.DesC_Id =" + descid + " AND DesObs_Anulada = 0 AND DesObs_Orden = '')";
            }

            return BdDatos.consultarConDataset(sql);
        }

        public int Anular_Observacion_Despacho(int idObserva)
        {
            string sql = "UPDATE Despa_Observaciones SET DesObs_Anulada=1 WHERE  DesObs_Id=" + idObserva + "";
            return BdDatos.ejecutarSql(sql);
        }


        public int Anular_Observacion(int DesObs_Id)
        {
            string sql = "UPDATE Despa_Observaciones " +
                          " SET DesObs_Anulada = 1 " +
                        " WHERE(DesObs_Id = " + DesObs_Id + ")";
            return BdDatos.ejecutarSql(sql);
        }

        public SqlDataReader Consultar_LeadTime(int pais, int tdn, int medioTrasnp)
        {
            string sql;

            sql = "  SELECT LeadTime_X_Destino.LeaTimDes_Total " +
                    " FROM LeadTime_X_Destino WITH(NOLOCK) " +
                   " WHERE LeaTimDes_Pais_Destino =" + pais + " AND LeaTimDes_tdn = " + tdn + " AND LeaTimDes_Mediotrans = " + medioTrasnp + " ";

            return BdDatos.ConsultarConDataReader(sql);
        }


        public SqlDataReader Consultar_LeadTime_Nal(int ciudad)
        {
            string sql;

            sql = "SELECT LeaTimDesNal_Total FROM LeadTime_X_Destino_Nal WHERE LeaTimDesNal_Ciudad=" + ciudad + "";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //SUBIR DOCUMENTOS DESPACHO
        public int Subir_Doc_Despacho(string nombreArchi, int despachoid, string rutaArchi, string usuCrea, int tipoanex)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[5];
            sqls[0] = new SqlParameter("dNombreArch", nombreArchi);
            sqls[1] = new SqlParameter("dDespadhoid", despachoid);
            sqls[2] = new SqlParameter("dRutaArch", rutaArchi);
            sqls[3] = new SqlParameter("dUsuario", usuCrea);
            sqls[4] = new SqlParameter("dTipoAnexo_id", tipoanex);
            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_archivos_despachos", con))
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
        //CONSULTAR DOCUMENTOS
        public DataTable Consultar_Doc_Despacho(int descId, int rol)
        {
            string sql;
            if (rol == 15)
            {
                sql = "SELECT  DesDocAnx_Id, DesDocAnx_Arc_Nombre AS Nombre_Archivo, DesDocAnx_DesC_Id AS Despacho, " +
                            " DesDocAnx_Arc_Ruta AS Ruta_Archivo, DesDocAnx_Usu_Crea AS UsuCrea, " +
                            " CONVERT(date, DesDocAnx_Fecha_Crea,103) AS FechaCrea, " +
                            " Despa_Document_Anexo.DesDocAnx_Tipo_Anexo, CASE WHEN Despa_Tipo_Anexo.destipane_descripcion IS NULL " +
                            " THEN 'Sin tipo' ELSE Despa_Tipo_Anexo.destipane_descripcion END AS TipoAnexo " +
                     " FROM Despa_Document_Anexo LEFT OUTER JOIN " +
                          " Despa_Tipo_Anexo ON Despa_Document_Anexo.DesDocAnx_Tipo_Anexo = Despa_Tipo_Anexo.destipane_id " +
                    " WHERE DesDocAnx_DesC_Id =" + descId + " AND Despa_Document_Anexo.DesDocAnx_Tipo_Anexo IN (1,2) " +
                    " ORDER BY DesDocAnx_Id DESC";
            }
            else
            {
                sql = "SELECT  DesDocAnx_Id, DesDocAnx_Arc_Nombre AS Nombre_Archivo, DesDocAnx_DesC_Id AS Despacho, " +
                         " DesDocAnx_Arc_Ruta AS Ruta_Archivo, DesDocAnx_Usu_Crea AS UsuCrea, " +
                         " CONVERT(date, DesDocAnx_Fecha_Crea,103) AS FechaCrea, " +
                         " Despa_Document_Anexo.DesDocAnx_Tipo_Anexo, CASE WHEN Despa_Tipo_Anexo.destipane_descripcion IS NULL " +
                         " THEN 'Sin tipo' ELSE Despa_Tipo_Anexo.destipane_descripcion END AS TipoAnexo " +
                  " FROM Despa_Document_Anexo LEFT OUTER JOIN " +
                       " Despa_Tipo_Anexo ON Despa_Document_Anexo.DesDocAnx_Tipo_Anexo = Despa_Tipo_Anexo.destipane_id " +
                 " WHERE DesDocAnx_DesC_Id =" + descId + " " +
                 " ORDER BY DesDocAnx_Id DESC";
            }

            return BdDatos.CargarTabla(sql);
        }


        //Se usa para generar las plantas que estan relacionadas al usuario
        public SqlDataReader PoblarPlanta(string login)
        {
            SqlDataReader reader = null;
            string sql1, sql2;
            sql1 = "SELECT   usu_siif_id AS usuId  FROM usuario  WHERE ( Ltrim(rtrim(usu_login)) =   Ltrim(rtrim('" + login + "')))";
            reader = BdDatos.ConsultarConDataReader(sql1);
            reader.Read();
            int y = reader.GetInt32(0);
            sql2 = "SELECT        c.planta_id, c.planta_descripcion"
                + " FROM planta_usuario  as  a INNER JOIN usuario  as b"
                + " ON a.plantausu_idusuario = b.usu_siif_id "
                + " INNER JOIN planta_forsa as c"
                + " ON a.plantausu_idplanta = c.planta_id"
                + "  WHERE (b.usu_siif_id = " + y + ") AND (a.plantausu_activo = (1))";
            reader.Close();
            reader.Dispose();
            return BdDatos.ConsultarConDataReader(sql2);
        }

        public DataTable Recuperar_PesoPallet_Acce(int idofa)
        {
            string sql = "SELECT  ISNULL (SUM(pallet_acc_peso),0) FROM pallet_acc WHERE pallet_acc_id_of_p=" + idofa + "";
            return BdDatos.CargarTabla(sql);
        }

        public DataTable Recuperar_PesoPallet_Alum(int idofa)
        {
            string sql = "SELECT  ISNULL (SUM(pallet_al_peso),0) FROM pallet_aluminio  WHERE pallet_al_Id_ofa =" + idofa + "";
            return BdDatos.CargarTabla(sql);
        }

        //CONSULTA TIPO ANEXO
        public SqlDataReader PoblarTipoAnexo()
        {
            string sql;

            sql = "SELECT       destipane_id,destipane_descripcion " +
                    " FROM       Despa_Tipo_Anexo ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public DataTable Obtener_IdDespacho_X_Orden(int idofa)
        {
            string sql = "SELECT  DesC_Grp_Id  FROM Despa_Cliente WHERE DesC_OfPId=" + idofa + "";

            return BdDatos.CargarTabla(sql);
        }

        public DataTable Valida_Existencia_DetaDespacho(int Descid)
        {
            string sql = " SELECT  DesC_Id  FROM Despa_Detalle WHERE DesC_Id = " + Descid + "";
            return BdDatos.CargarTabla(sql);
        }



        public int Guardar_Gastos_Destino_Brasil(int desC_id, string no_proceso, string fech_planilla, decimal trm, decimal liberaHbl_prov,
                                                 decimal liberaHbl_real, decimal dropOff_prov, decimal dropoff_real, decimal taxa_admin_prov,
                                                 decimal taxa_admin_real, decimal isps_prov, decimal isps_real, decimal otros_gastos_prov,
                                                 decimal otros_gastos_real, decimal prim_periodo_prov, decimal prim_periodo_real,
                                                 decimal segun_periodo_prov, decimal segun_periodo_real, decimal terce_periodo_prov,
                                                 decimal terce_periodo_real, decimal escan_conten_prov, decimal escan_conten_real,
                                                 decimal insope_mapa_prov, decimal insope_mapa_real, decimal taxa_corretag_prov,
                                                 decimal taxa_corretag_real, decimal demurrage_prov, decimal demurrage_real,
                                                 decimal despa_honora_prov, decimal despa_honora_real, decimal margen_2porcent_prov,
                                                 decimal margen_2porcent_real, decimal flete_terrestre_prov, decimal flete_terrestre_real,
                                                 bool cerrado, string usu_crea, string fech_crea)
        {
            string sql = "INSERT INTO Despa_Gastos_DestinoBrasil(desC_id,desGstBr_no_proceso,desGstBr_fech_planilla,desGstBr_trm, " +
                                                               " desGstBr_liberaHbl_prov,desGstBr_liberaHbl_real,desGstBr_dropOff_prov, " +
                                                               " desGstBr_dropoff_real,desGstBr_taxa_admin_prov,desGstBr_taxa_admin_real, " +
                                                               " desGstBr_isps_prov,desGstBr_isps_real,desGstBr_otros_gastos_prov," +
                                                               " desGstBr_otros_gastos_real,desGstBr_1er_periodo_prov,desGstBr_1er_periodo_real, " +
                                                               " desGstBr_2do_periodo_prov,desGstBr_2do_periodo_real,desGstBr_3er_periodo_prov, " +
                                                               " desGstBr_3er_periodo_real,desGstBr_escan_conten_prov,desGstBr_escan_conten_real, " +
                                                               " desGstBr_insope_mapa_prov,desGstBr_insope_mapa_real,desGstBr_taxa_corretag_prov, " +
                                                               " desGstBr_taxa_corretag_real,desGstBr_demurrage_prov,desGstBr_demurrage_real, " +
                                                               " desGstBr_despa_honora_prov,desGstBr_despa_honora_real,desGstBr_margen_2porcent_prov, " +
                                                               " desGstBr_margen_2porcent_real,desGstBr_flete_terrestre_prov,desGstBr_flete_terrestre_real, " +
                                                               " desGstBr_cerrado,desGstBr_usu_crea,desGstBr_fech_crea) " +
                                                     " VALUES   (" + desC_id + ",'" + no_proceso + "',Convert(date,'" + fech_planilla + "',103)," + trm + "," + liberaHbl_prov + ", " +
                                                              " " + liberaHbl_real + "," + dropOff_prov + "," + dropoff_real + "," + taxa_admin_prov + ", " +
                                                              " " + taxa_admin_real + "," + isps_prov + "," + isps_real + "," + otros_gastos_prov + ", " +
                                                              " " + otros_gastos_real + "," + prim_periodo_prov + "," + prim_periodo_real + ", " +
                                                              " " + segun_periodo_prov + "," + segun_periodo_real + "," + terce_periodo_prov + ", " +
                                                              " " + terce_periodo_real + "," + escan_conten_prov + "," + escan_conten_real + ", " +
                                                              " " + insope_mapa_prov + "," + insope_mapa_real + "," + taxa_corretag_prov + ", " +
                                                              " " + taxa_corretag_real + "," + demurrage_prov + "," + demurrage_real + ", " +
                                                              " " + despa_honora_prov + "," + despa_honora_real + "," + margen_2porcent_prov + ", " +
                                                              " " + margen_2porcent_real + "," + flete_terrestre_prov + "," + flete_terrestre_real + ", " +
                                                              " '" + cerrado + "','" + usu_crea + "',Convert(date,'" + fech_crea + "',103))";

            return BdDatos.ejecutarSql(sql);
        }


        public int Actualizar_Gastos_Destino_Brasil(string no_proceso, string fech_planilla, decimal trm, decimal liberaHbl_prov,
                                                 decimal liberaHbl_real, decimal dropOff_prov, decimal dropoff_real, decimal taxa_admin_prov,
                                                 decimal taxa_admin_real, decimal isps_prov, decimal isps_real, decimal otros_gastos_prov,
                                                 decimal otros_gastos_real, decimal prim_periodo_prov, decimal prim_periodo_real,
                                                 decimal segun_periodo_prov, decimal segun_periodo_real, decimal terce_periodo_prov,
                                                 decimal terce_periodo_real, decimal escan_conten_prov, decimal escan_conten_real,
                                                 decimal insope_mapa_prov, decimal insope_mapa_real, decimal taxa_corretag_prov,
                                                 decimal taxa_corretag_real, decimal demurrage_prov, decimal demurrage_real,
                                                 decimal despa_honora_prov, decimal despa_honora_real, decimal margen_2porcent_prov,
                                                 decimal margen_2porcent_real, decimal flete_terrestre_prov, decimal flete_terrestre_real,
                                                 bool cerrado, int desC_id)
        { 
            string sql = " UPDATE Despa_Gastos_DestinoBrasil SET desGstBr_no_proceso='" + no_proceso + "',desGstBr_fech_planilla='"+fech_planilla + "' ,desGstBr_trm= " +trm +", " +
                                                               " desGstBr_liberaHbl_prov = " +liberaHbl_prov + ",desGstBr_liberaHbl_real=" +liberaHbl_real + ",desGstBr_dropOff_prov=" +dropOff_prov +", " +
                                                               " desGstBr_dropoff_real=" + dropoff_real+ ",desGstBr_taxa_admin_prov=" +taxa_admin_prov + ",desGstBr_taxa_admin_real=" + taxa_admin_real+", " +
                                                               " desGstBr_isps_prov=" + isps_prov+ ",  desGstBr_isps_real=" + isps_real+ ",desGstBr_otros_gastos_prov=" +otros_gastos_prov +", " +
                                                               " desGstBr_otros_gastos_real=" +otros_gastos_real + ",desGstBr_1er_periodo_prov=" + prim_periodo_prov+ ", " +
                                                               " desGstBr_1er_periodo_real=" + prim_periodo_real+ ",desGstBr_2do_periodo_prov=" +segun_periodo_prov +", " +
                                                               " desGstBr_2do_periodo_real=" + segun_periodo_real+ ",desGstBr_3er_periodo_prov=" + terce_periodo_prov+", " +
                                                               " desGstBr_3er_periodo_real=" +terce_periodo_real + ",desGstBr_escan_conten_prov=" + escan_conten_prov+", " +
                                                               " desGstBr_escan_conten_real=" + escan_conten_real+ ",desGstBr_insope_mapa_prov=" +insope_mapa_prov +", " +
                                                               " desGstBr_insope_mapa_real=" +insope_mapa_real + ",desGstBr_taxa_corretag_prov=" +taxa_corretag_prov +", " +
                                                               " desGstBr_taxa_corretag_real=" +taxa_corretag_real + ",desGstBr_demurrage_prov=" +demurrage_prov +", " +
                                                               " desGstBr_demurrage_real=" +demurrage_real + ",desGstBr_despa_honora_prov=" + despa_honora_prov+", " +
                                                               " desGstBr_despa_honora_real=" +despa_honora_real + ",desGstBr_margen_2porcent_prov=" +margen_2porcent_prov +", " +
                                                               " desGstBr_margen_2porcent_real=" +margen_2porcent_real + ",desGstBr_flete_terrestre_prov=" +flete_terrestre_prov +", " +
                                                               " desGstBr_flete_terrestre_real=" +flete_terrestre_real + ",desGstBr_cerrado='" +cerrado +"' " +
                                              " WHERE desC_id =" + desC_id + "";
            return BdDatos.ejecutarSql(sql);
        }

    

    public SqlDataReader Poblar_Gastos_Destino_Brasil(int desc_id)
        {
            string sql;

            sql = "   SELECT    UPPER(desGstBr_no_proceso)  AS  desGstBr_no_proceso ,ISNULL(desGstBr_fech_planilla,''),desGstBr_trm,desGstBr_liberaHbl_prov,desGstBr_liberaHbl_real, " +
                              " desGstBr_dropOff_prov,desGstBr_dropoff_real,desGstBr_taxa_admin_prov,desGstBr_taxa_admin_real,desGstBr_isps_prov, " +
                              " desGstBr_isps_real,desGstBr_otros_gastos_prov,desGstBr_otros_gastos_real,desGstBr_1er_periodo_prov, " +
                              " desGstBr_1er_periodo_real,desGstBr_2do_periodo_prov,desGstBr_2do_periodo_real,desGstBr_3er_periodo_prov, " +
                              " desGstBr_3er_periodo_real,desGstBr_escan_conten_prov,desGstBr_escan_conten_real,desGstBr_insope_mapa_prov, " +
                              " desGstBr_insope_mapa_real,desGstBr_taxa_corretag_prov,desGstBr_taxa_corretag_real,desGstBr_demurrage_prov, " +
                              " desGstBr_demurrage_real,desGstBr_despa_honora_prov,desGstBr_despa_honora_real,desGstBr_margen_2porcent_prov, " +
                              " desGstBr_margen_2porcent_real,desGstBr_flete_terrestre_prov,desGstBr_flete_terrestre_real,desGstBr_cerrado " +
                    " FROM      Despa_Gastos_DestinoBrasil " +
                    " WHERE(desC_id = " + desc_id + ")";

            return BdDatos.ConsultarConDataReader(sql);
        }



    }
}
