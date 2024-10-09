using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CapaDatos;
using System.Data.OracleClient;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace CapaControl
{
   public  class ControlMaestroItemPlanta
    {
      
       //Se usa para obtener los perfiles que tiene acceso el rol con que ingreso el usuario
        public Dictionary<int,string> PoblarPerfil(string login)
        {
            SqlDataReader reader = null;
            string sql;
            Dictionary<int, string> perfil = new Dictionary<int, string> ();

            sql = "SELECT  isnull(rolapp.aprobador_item,0), isnull( rolapp.solicitante_item,0),isnull( rolapp.preaprobador_item,0)"
                +" FROM usuario INNER JOIN"
                +" rolapp ON usuario.usu_rap_id = rolapp.rap_id"
                +" WHERE        (usuario.usu_login = '"+login+"')";
            reader = BdDatos.ConsultarConDataReader(sql);
            perfil.Clear();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    if (reader.GetBoolean(0) == true && reader.GetBoolean(1) == true && reader.GetBoolean(2) == true)
                    {
                        perfil.Add(1, "Aprobador");
                        perfil.Add(2, "Solicitante");
                        perfil.Add(3, "Preaprobador");
                    }
                    if (reader.GetBoolean(0) == true && reader.GetBoolean(1) == false && reader.GetBoolean(2) == false)
                    {
                        perfil.Add(1, "Aprobador");
                    }
                    if (reader.GetBoolean(0) == false && reader.GetBoolean(1) == true && reader.GetBoolean(2) == false)
                    {
                        perfil.Clear();
                        perfil.Add(2, "Solicitante");
                    }
                    if (reader.GetBoolean(0) == false && reader.GetBoolean(1) == false && reader.GetBoolean(2) == true)
                    {
                        perfil.Clear();
                        perfil.Add(3, "Preaprobador");
                    }
                    if (reader.GetBoolean(0) == true && reader.GetBoolean(1) == false && reader.GetBoolean(2) == true)
                    {
                        perfil.Add(1, "Aprobador");
                        perfil.Add(3, "Preaprobador");
                    }
                    if (reader.GetBoolean(0) == true && reader.GetBoolean(1) == true && reader.GetBoolean(2) == false)
                    {
                        perfil.Add(1, "Aprobador");
                        perfil.Add(2, "Solicitante");
                    }
                    if (reader.GetBoolean(0) == false && reader.GetBoolean(1) == true && reader.GetBoolean(2) == true)
                    {
                        perfil.Add(2, "Solicitante");
                        perfil.Add(3, "Preaprobador");
                    }
                    if (reader.GetBoolean(0) == false && reader.GetBoolean(1) == false && reader.GetBoolean(2) == false)
                    {
                        perfil.Add(0, " ");
                    }
                }

            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
            return perfil;
        }


        //Se usa para obtener los grupos de item que existen
        public SqlDataReader PoblarGrupo()
        {
            string sql;
            sql = "select item_grupo_id,descripcion  from item_grupo where activo = (1) order by descripcion asc";
            return BdDatos.ConsultarConDataReader(sql);
        }
        
         //Se usa para cargar la clase de item 
         public SqlDataReader PoblarClaseItem()
        {
            string sql;
            sql = "SELECT ClsItm_Id,ClsItm_Descripcion  FROM Clase_ItemPlanta";
            return BdDatos.ConsultarConDataReader(sql);

        }

        //Se usa para obtener los estados del item que existen
        public SqlDataReader PoblarEstado()
        {
            string sql;
            sql = "SELECT item_estado_id, descripcion FROM item_estado WHERE (activo = 1) order by descripcion asc";
            return BdDatos.ConsultarConDataReader(sql);
        }
        //Se usa para obtener los grupos de item que existen
        public SqlDataReader PoblarItemplanta(Int64 item_planta_id)
        {
            string sql;
            sql = "SELECT item_planta.item_planta_id, item_planta.descripcion"
            +" FROM  item_planta INNER JOIN"
            +" item ON item_planta.item_id = item.item_id"
            +" WHERE (item_planta.activo = 1) AND (item.item_id = "+item_planta_id+")"
            +" ORDER BY item_planta.descripcion";
            return BdDatos.ConsultarConDataReader(sql);
        }



        //Se usa para obtener los item agrupador que  pertencen al grupo seleccionado
        public SqlDataReader PoblarItemAgrupador(int  x)
        {
            string sql;
            sql = "select a.item_id,a.descripcion from item as a"
                  +"  inner join  item_grupo as b"
                   +" on a.item_grupo_id = b.item_grupo_id"
                   +" INNER JOIN    item_rel_estado as c"
                   + " ON a.item_id = c.item_id"
                   + "  WHERE b.item_grupo_id = " + x + " AND (c.item_estado_id = 5) AND a.activo=1 ORDER BY a.descripcion ASC ";
            return BdDatos.ConsultarConDataReader(sql);
        }

       //Se usa para generar las plantas que estan relacionadas al usuario
        public SqlDataReader PoblarPlanta(string login)
        {
            SqlDataReader reader = null;
            string sql1,sql2;
            int y = 0;
            sql1 = "SELECT   usu_siif_id AS usuId  FROM usuario  WHERE ( Ltrim(rtrim(usu_login)) =   Ltrim(rtrim('" + login + "')))";
            reader= BdDatos.ConsultarConDataReader(sql1);
            if (reader.HasRows == true)
            {
                reader.Read();
                y = reader.GetInt32(0);
                
            }
            sql2 = "SELECT        c.planta_id, c.planta_descripcion"
                    + " FROM planta_usuario  as  a INNER JOIN usuario  as b"
                    + " ON a.plantausu_idusuario = b.usu_siif_id "
                    + " INNER JOIN planta_forsa as c"
                    + " ON a.plantausu_idplanta = c.planta_id"
                    + "  WHERE (b.usu_siif_id = " + y + ") AND (a.plantausu_activo = (1))";
            
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
            return BdDatos.ConsultarConDataReader(sql2);
        }

        //Se usa para obtener los grupos que estan habilitados para la planta seleccionada, todos ellos se traen en idioma español
        public SqlDataReader PoblarGrupoPlanta(int x,int y)
        {
            string sql;
            sql = "SELECT    igp.item_grupo_planta_id,igpi.descripcion"
                   + " FROM    item_grupo  as ig "
                   + " INNER JOIN item_grupo_planta as igp ON ig.item_grupo_id = igp.item_grupo_id "
                   + " INNER JOIN item_grupo_planta_idioma  as igpi ON igp.item_grupo_planta_id = igpi.item_grupo_planta_id"
                   + " WHERE    (igpi.activo = 1) AND (igpi.idioma_id = 1) AND (igp.planta_id = "+ x +") AND (igp.item_grupo_id = "+ y +")";
            return BdDatos.ConsultarConDataReader(sql);
        }
        //Se usa para obtener los idiomas que existen en la base de datos
        public DataTable  PoblarIdioma()
        {
            string sql;
            sql = "select  '' as planta_idioma_id,idioma_id,descripcion as idioma,'' as descripcion from idioma where activo = (1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Se usa para obtener las monedas  que existen en la base de datos
        public SqlDataReader PoblarMoneda(int planta_id)
        {
            string sql;
            sql = "SELECT  moneda.mon_descripcion"
                    +" FROM  moneda RIGHT OUTER JOIN"
                    + " planta_forsa ON moneda.mon_id = planta_forsa.planta_moneda_nal"
                    +" WHERE  (planta_forsa.planta_id = "+planta_id+") AND (moneda.mon_activo = 1)";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader obtenerMonedaPlanta(int planta_id)
        {
            string sql;
            sql = "SELECT  isnull(moneda.mon_id, 0)"
                    + " FROM  moneda INNER JOIN"
                    + " planta_forsa ON moneda.mon_id = planta_forsa.planta_moneda_id"
                    + " WHERE  (planta_forsa.planta_id = " + planta_id + ") AND (moneda.mon_activo = 1)";
            return BdDatos.ConsultarConDataReader(sql);
        }
        //Se usa para obtener los lciente_tipo que existan en la base de datos
        public DataTable PoblarClientetipoNal(int planta)
        {
            string sql;
            sql = "SELECT cliente_tipo_planta.cliente_tipo_planta_id, cliente_tipo.descripcion, cliente_tipo_planta.porcentaje, planta_forsa.planta_moneda_nal AS nal, "
                  +" moneda.mon_descripcion AS dsc1"
                 +" FROM  cliente_tipo INNER JOIN"
                       +  " cliente_tipo_planta ON cliente_tipo.cliente_tipo_id = cliente_tipo_planta.cliente_tipo_id INNER JOIN"
                        + " planta_forsa ON cliente_tipo_planta.planta_id = planta_forsa.planta_id INNER JOIN"
                         + " moneda ON planta_forsa.planta_moneda_nal = moneda.mon_id "
                   + " WHERE  (cliente_tipo_planta.activo = 1) AND (cliente_tipo_planta.planta_id = "+planta+")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Se usa para obtener los lciente_tipo que existan en la base de datos
        public DataTable PoblarClientetipoExt(int planta)
        {
            string sql;
            sql = "SELECT cliente_tipo_planta.cliente_tipo_planta_id, cliente_tipo.descripcion, "
                  + " planta_forsa.planta_moneda_ext AS ext, moneda.mon_descripcion AS dsc2"
                 + " FROM  cliente_tipo INNER JOIN"
                       + " cliente_tipo_planta ON cliente_tipo.cliente_tipo_id = cliente_tipo_planta.cliente_tipo_id INNER JOIN"
                        + " planta_forsa ON cliente_tipo_planta.planta_id = planta_forsa.planta_id INNER JOIN"
                         + " moneda ON planta_forsa.planta_moneda_ext = moneda.mon_id "
                   + " WHERE  (cliente_tipo_planta.activo = 1) AND (cliente_tipo_planta.planta_id = " + planta + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Se usa para sugerir los tipos de inventarios relacionados al origen seleccionado
        public SqlDataReader SugerirTipoInv(int planta)
        {
            string sql;
            //sql = "SELECT tipo_inventario"
            //+" FROM  item_rel_tipo_inv"
            //+ " WHERE   (planta_id = " + planta + ") AND (item_origen_id = " + origen_id + ") AND (activo = 1) order by tipo_inventario ASC ";
            sql = "SELECT LTRIM(RTRIM(tipo_inventario)) AS tipo_inventario, tipo_inventario + '/' + descripcion AS Expr1"
           + " FROM  item_rel_tipo_inv"
           + " WHERE   (planta_id = " + planta + ") AND (activo = 1) order by tipo_inventario ASC ";
            
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader SugerirBodega(int planta,string tipoinv)
        {
            string sql = "SELECT MAX(isnull(Bodega_Desc,'SIN')) AS Bodega_Desc"
                     + " FROM  item_rel_tipo_inv"
                     + " WHERE   (planta_id = " + planta + ") AND (tipo_inventario = '" + tipoinv + "') AND (activo = 1)";          
            return BdDatos.ConsultarConDataReader(sql);
        }
        //Consulta la descripcion del tipo de inventario 
        public String DescpTipoInv1E(String tipoinv, String cia)
        {
            string sql; String descripcion = "";
            SqlDataReader reader = null;
            sql = "select F149_DESCRIPCION from  T149_MC_TIPO_INV_SERV WHERE  F149_ID_CIA = " + cia + " AND  (Ltrim(rtrim(F149_ID)) = Ltrim(rtrim('"+tipoinv.ToUpper()+"')))";
            reader= BdDatos.consultarConDataReaderOracle(sql);
            if (reader.HasRows == true)
            {
                reader.Read();
                descripcion = reader["F149_DESCRIPCION"].ToString();
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();

            return descripcion;
        }


        public DataTable SugerirDescripcionTipoInv(int planta, string tipoinv)
        {                
            string sql = "SELECT descripcion"
           + " FROM  item_rel_tipo_inv"
           + " WHERE   (planta_id = " + planta + ") AND (tipo_inventario = '"+tipoinv+"') AND (activo = 1)";

            return  BdDatos.CargarTabla(sql);
        }

        //Se usa para sugerir los grupos impositivos relacionados al origen seleccionado
        public SqlDataReader SugerirGrupoImp(int planta)
        {
            string sql;
            //sql = "SELECT  grupo_impositivo"
            //+ " FROM  item_rel_tipo_inv"
            //+ " WHERE   (planta_id = " + planta + ") AND (item_origen_id = " + origen_id + ") AND (activo = 1) AND (tipo_inventario = '"+ tipoinv +"')";
            sql = "SELECT  grupo_impositivo"
            + " FROM  item_rel_tipo_inv"
            + " WHERE   (planta_id = " + planta + ") AND (activo = 1)";
            return BdDatos.ConsultarConDataReader(sql);
        }
        //Se usa para sugerir los grupos impositivos relacionados al origen seleccionado desde oracle
        public SqlDataReader SugerirGrupoImp1E(int planta)
        {
            string sql;
            sql = "SELECT ID, RTRIM(ID +' / '+ DESCRIPCION) FROM VW_GRUPIMP_1E WHERE  planta_id = " + planta +" ORDER BY ID ASC";
            return BdDatos.ConsultarConDataReader(sql);
        }
        //Consulta la descripcion del grupo impositivo
        public String DescpGrupImp1E(int planta,String grupimp)
        {
            SqlDataReader reader = null;
            String descripcion = "";
            string sql;
            sql = "select DESCRIPCION from VW_GRUPIMP_1E where planta_id = " + planta + " AND   Ltrim(rtrim(ID)) =   Ltrim(rtrim('" + grupimp + "'))";
            reader = BdDatos.ConsultarConDataReader(sql);
            if (reader.HasRows == true)
            {
                reader.Read();
                descripcion = reader.GetString(0);
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
            return descripcion;
        }

        //Se usa para sugerir la unidad principal de oracle
        public SqlDataReader SugerirUP1E(int planta)
        {
            string sql;
            sql = "select Ltrim(rtrim(ID)) from VW_UM_1E where planta_id = " + planta + " ORDER BY ID ASC";
            return BdDatos.ConsultarConDataReader(sql);
        }
        //Consulta la descripcion del grupo impositivo
        public String DescpUP1E(int planta,String UP)
        {
            SqlDataReader reader = null;
            String descripcion = "";
            string sql;
            sql = "select DESCRIPCION from VW_UM_1E where planta_id = " + planta + " AND  Ltrim(rtrim(ID)) =  Ltrim(rtrim('" + UP + "'))";
            reader = BdDatos.ConsultarConDataReader(sql);
            if (reader.HasRows == true)
            {
                reader.Read();
                descripcion = reader.GetString(0);
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
            return descripcion;
        }
        
        //Se usa para sugerir el criterio de clasificaciòn 1
        public SqlDataReader SugerirPlan1(int planta, String tipoinv)
        {
            string sql;
            sql = "SELECT DISTINCT item_criterio_clas.codigo, item_criterio_clas.descripcion"
                    +" FROM            item_rel_criterio_clas INNER JOIN"
                    +" item_criterio_clas ON item_rel_criterio_clas.cod_plan1 = item_criterio_clas.codigo"
                        + " WHERE        (item_rel_criterio_clas.activo = 1) AND (item_rel_criterio_clas.planta_id = " + planta + ") " +
                                   " AND ( Ltrim(rtrim(item_rel_criterio_clas.tipo_inventario)) =  Ltrim(rtrim('" + tipoinv + "'))) AND  filtro_Plan4=0";
            return BdDatos.ConsultarConDataReader(sql);
        }
        //Se usa para sugerir el criterio de clasificaciòn 2
        public SqlDataReader SugerirPlan2(int planta, String tipoinv,String plan1)
        {
            string sql;
            sql = "SELECT DISTINCT item_criterio_clas.codigo, item_criterio_clas.descripcion"
                    +" FROM            item_rel_criterio_clas INNER JOIN"
                       +" item_criterio_clas ON item_rel_criterio_clas.cod_plan2 = item_criterio_clas.codigo"
                        + " WHERE        (item_rel_criterio_clas.activo = 1) AND (item_rel_criterio_clas.planta_id = " + planta + ") AND ( Ltrim(rtrim(item_rel_criterio_clas.tipo_inventario)) = Ltrim(rtrim('" + tipoinv + "'))) AND  (Ltrim(rtrim(item_rel_criterio_clas.cod_plan1)) = Ltrim(rtrim('" + plan1 + "')))";
            return BdDatos.ConsultarConDataReader(sql);
        }
        //Se usa para sugerir el criterio de clasificaciòn 3
        public SqlDataReader SugerirPlan3(int planta, String tipoinv, String plan1,String plan2)
        {
            string sql;
            sql = "SELECT DISTINCT item_criterio_clas.codigo, item_criterio_clas.descripcion"
                +" FROM            item_rel_criterio_clas INNER JOIN"
                 +" item_criterio_clas ON item_rel_criterio_clas.cod_plan3 = item_criterio_clas.codigo"
                + " WHERE        (item_rel_criterio_clas.activo = 1) AND (item_rel_criterio_clas.planta_id = " + planta + ") AND (Ltrim(rtrim(item_rel_criterio_clas.tipo_inventario)) = Ltrim(rtrim('" + tipoinv + "'))) AND  (Ltrim(rtrim(item_rel_criterio_clas.cod_plan1)) = Ltrim(rtrim('" + plan1 + "'))) AND (Ltrim(rtrim(item_rel_criterio_clas.cod_plan2)) = Ltrim(rtrim('" + plan2 + "')))";
            return BdDatos.ConsultarConDataReader(sql);
        }

        //Se usa para sugerir el criterio de clasificaciòn 4
        public SqlDataReader SugerirPlan4(int planta, String tipoinv)
        {
            string sql;
            sql = "SELECT DISTINCT item_criterio_clas.codigo, item_criterio_clas.descripcion"
                    + " FROM            item_rel_criterio_clas INNER JOIN"
                    + " item_criterio_clas ON item_rel_criterio_clas.cod_plan1 = item_criterio_clas.codigo"
                        + " WHERE        (item_rel_criterio_clas.activo = 1) AND (item_rel_criterio_clas.planta_id = " + planta + ") " +
                                   " AND ( Ltrim(rtrim(item_rel_criterio_clas.tipo_inventario)) =  Ltrim(rtrim('" + tipoinv + "'))) AND  filtro_Plan4=1  " +

                   " Union All   " +
                   "    SELECT DISTINCT item_criterio_clas.codigo, item_criterio_clas.descripcion " +
                     " FROM item_rel_criterio_clas INNER JOIN " +
                       " item_criterio_clas ON item_rel_criterio_clas.cod_plan1 = item_criterio_clas.codigo "+
                       " WHERE        (item_rel_criterio_clas.activo = 1) AND (item_rel_criterio_clas.planta_id = " + planta + ") " +
                       " OR  (item_rel_criterio_clas.filtro_Gpc = 1)";
                                  
            return BdDatos.ConsultarConDataReader(sql);
        }

        //Se usa para sugerir el criterio de clasificaciòn 5
        // Adicion de gpc agosto 2023
        public SqlDataReader SugerirPlan5(int planta, String tipoinv, String plan4)
        {
            string sql;
            sql = "SELECT DISTINCT rtrim(item_criterio_clas.codigo), item_criterio_clas.descripcion"
                    + " FROM            item_rel_criterio_clas INNER JOIN"
                       + " item_criterio_clas ON item_rel_criterio_clas.cod_plan2 = item_criterio_clas.codigo"
                        + " WHERE        (item_rel_criterio_clas.activo = 1) AND (item_rel_criterio_clas.planta_id = " + planta + ") AND ( Ltrim(rtrim(item_rel_criterio_clas.tipo_inventario)) = Ltrim(rtrim('" + tipoinv + "'))) AND  (Ltrim(rtrim(item_rel_criterio_clas.cod_plan1)) = Ltrim(rtrim('" + plan4 + "')))"+
                         " Union All   " +
                   "SELECT DISTINCT rtrim(item_criterio_clas.codigo), item_criterio_clas.descripcion"
                    + " FROM            item_rel_criterio_clas INNER JOIN"
                       + " item_criterio_clas ON item_rel_criterio_clas.cod_plan2 = item_criterio_clas.codigo"
                        + " WHERE        (item_rel_criterio_clas.activo = 1) AND (item_rel_criterio_clas.planta_id = " + planta + ") AND  (Ltrim(rtrim(item_rel_criterio_clas.cod_plan1)) = Ltrim(rtrim('" + plan4 + "')))" ;
            return BdDatos.ConsultarConDataReader(sql);
        }
        //Consulta la posicion arancelaria
        public String PoscArancelaria(int planta, String tipoinv, String plan1, String plan2,String plan3)
        {
            SqlDataReader reader = null;
            String poscA = "";
            string sql;
            sql = "SELECT    pos_arancelaria"
                   +" FROM   item_rel_criterio_clas"
                    + " WHERE  (activo = 1) AND (planta_id = " + planta + ") AND (Ltrim(rtrim(tipo_inventario)) = Ltrim(rtrim('" + tipoinv + "'))) AND (Ltrim(rtrim(cod_plan1)) = Ltrim(rtrim('" + plan1 + "'))) AND (Ltrim(rtrim(cod_plan2 )) = Ltrim(rtrim('" + plan2 + "'))) AND (Ltrim(rtrim(cod_plan3)) = Ltrim(rtrim('" + plan3 + "')))";
            reader = BdDatos.ConsultarConDataReader(sql);
            if (reader.HasRows == true)
            {
                reader.Read();
                poscA = reader.GetString(0);
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
            return poscA;
        }
       
        //Inserta en la tabla item_planta
        public String insertarItemPlanta(Int64 item_id, int planta_id, int item_grupo_planta_id, string descripcion, 
                                         Boolean activo, String descripcion_corta, Boolean uso_compra, Boolean uso_venta, 
                                         Boolean uso_manufactura, int item_origen_id, string tipo_inventario, String grupo_impositivo,
                                         string und_medida_principal, string und_medida_adicional, string und_medida_orden,
                                         decimal peso_unitario, decimal factor_orden, decimal factor_adicional, string usu, 
                                         int num_perfil, bool ctrl_piciz, decimal peso_empaque, int cant_empaque, decimal largo,
                                         decimal ancho1, decimal ancho2, decimal alto1, decimal alto2, bool tipo_kamban, 
                                         bool disp_cotizacion, bool disp_comercial, bool disp_ingenieria, bool disp_almacen,
                                         bool disp_produccion, bool req_plano, bool req_tipo, bool req_modelo, int tipo_orden_prod_id,
                                         bool Insp_Calidad, bool Insp_Obligatoria, int ClsItm_Id, string bodega)
        {
            String mensaje = "";
            DataTable consulta = null;
            try
            {
                String sql = "INSERT INTO item_planta (item_id, planta_id, item_grupo_planta_id, descripcion,activo, " +
                                                     " descripcion_corta,uso_compra,uso_venta,uso_manufactura,item_origen_id, " +
                                                     " tipo_inventario,grupo_impositivo,und_medida_principal,und_medida_adicional, " +
                                                     " und_medida_orden,peso_unitario,factor_orden,factor_adicional,crea_usu, " +
                                                     " num_perfil,ctrl_piciz,peso_empaque,cant_empaque,largo,ancho1,ancho2,alto1, " +
                                                     " alto2,tipo_kamban,disp_cotizacion,disp_comercial,disp_ingenieria,disp_almacen, " +
                                                     " disp_produccion,req_plano,req_tipo,req_modelo,tipo_orden_prod_id, vista_reporte," +
                                                     " item_InspCal,item_InspObligatoria, item_ClasItem,item_Bodega_Desc) "
                + " VALUES (" + item_id + "," + planta_id + "," + item_grupo_planta_id + ",'" + descripcion + "','" + activo + "', " +
                         " '" + descripcion_corta + "','" + uso_compra + "','" + uso_venta + "','" + uso_manufactura + "', " +
                          " " + item_origen_id + ",'" + tipo_inventario + "','" + grupo_impositivo + "','" + und_medida_principal + "', " +
                         " '" + und_medida_adicional + "','" + und_medida_orden + "'," + peso_unitario + "," + factor_orden + ", " +
                          " " + factor_adicional +",'"+usu+"',"+num_perfil+",'"+ctrl_piciz+"',"+peso_empaque+", "+cant_empaque+", " +
                          " " +largo+","+ancho1+","+ancho2+","+alto1+","+alto2+",'"+tipo_kamban+"','"+disp_cotizacion+"', " +
                         " '" +disp_comercial+"','"+disp_ingenieria+"','"+disp_almacen+"','"+disp_produccion+"','"+req_plano+"'," +
                         " '" +req_tipo+"','"+req_modelo+"',"+tipo_orden_prod_id+", 1,'"+Insp_Calidad+"','"+Insp_Obligatoria+"', "+ ClsItm_Id + ",'"+bodega+"');"
                + " SELECT  MAX(item_planta_id) AS idItemPlanta FROM item_planta ";
                consulta = BdDatos.CargarTabla(sql);
                foreach (DataRow row in consulta.Rows)
                {
                    mensaje = row["idItemPlanta"].ToString();
                }
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT item_planta)";
            }
            return mensaje;
        }
        //Inserta en la tabla item_planta
        public String insertarBitacoraItemPlanta(Int64 item_planta_id, Int64 item_id, int planta_id, int item_grupo_planta_id, String descripcion, Boolean activo, 
                                                 String descripcion_corta, Boolean uso_compra, Boolean uso_venta, Boolean uso_manufactura, int item_origen_id,
                                                 String tipo_inventario, String grupo_impositivo, String und_medida_principal, String und_medida_adicional, 
                                                 String und_medida_orden, decimal peso_unitario, Decimal factor_orden, Decimal factor_adicional, string usu,
                                                 int num_perfil, bool ctrl_piciz, decimal peso_empaque, int cant_empaque, decimal largo, decimal ancho1,
                                                 decimal ancho2, decimal alto1, decimal alto2, bool tipo_kamban, bool disp_cotizacion, bool disp_comercial,
                                                 bool disp_ingenieria, bool disp_almacen, bool disp_produccion, bool req_plano, bool req_tipo, bool req_modelo,
                                                 int tipo_orden_prod_id)
        {
            String mensaje = "";
            DataTable consulta = null;
            try
            {
                String sql = "INSERT INTO bitacora_item_planta (item_planta_id,item_id, planta_id, item_grupo_planta_id, descripcion,activo,  " +
                                                              " descripcion_corta,uso_compra,uso_venta,uso_manufactura,item_origen_id,tipo_inventario," +
                                                              " grupo_impositivo,und_medida_principal,und_medida_adicional,und_medida_orden,peso_unitario, " +
                                                              " factor_orden,factor_adicional,crea_usu,num_perfil,ctrl_piciz,peso_empaque,cant_empaque,largo, " +
                                                              " ancho1,ancho2,alto1,alto2,tipo_kamban,disp_cotizacion,disp_comercial,disp_ingenieria,disp_almacen," +
                                                              " disp_produccion,req_plano,req_tipo,req_modelo,tipo_orden_prod_id) "
                + " VALUES (" + item_planta_id + "," + item_id + "," + planta_id + "," + item_grupo_planta_id + ",'" + descripcion + "','" + activo + "', " +
                         " '" + descripcion_corta + "','" + uso_compra + "','" + uso_venta + "','" + uso_manufactura + "'," + item_origen_id + "," +
                         " '" + tipo_inventario + "','" + grupo_impositivo + "','" + und_medida_principal + "','" + und_medida_adicional + "'," +
                         " '" + und_medida_orden + "'," + peso_unitario + "," + factor_orden + "," + factor_adicional + ",'" + usu + "'," +
                         " " + num_perfil + ",'" + ctrl_piciz + "'," + peso_empaque + "," + cant_empaque + "," + largo + "," + ancho1 + "," +
                         " " + ancho2 + "," + alto1 + "," + alto2 + ",'" + tipo_kamban + "','" + disp_cotizacion + "','" + disp_comercial + "'," +
                         " '" + disp_ingenieria + "','" + disp_almacen + "','" + disp_produccion + "','" + req_plano + "','" + req_tipo + "'," +
                         " '" + req_modelo + "'," + tipo_orden_prod_id + ");"
                + " SELECT  MAX(item_planta_id) AS idItemPlanta FROM item_planta ";
                consulta = BdDatos.CargarTabla(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT bitacora_item_planta)";
            }
            return mensaje;
        }
        //Inserta en item_planta_rel_estado
        public String InsertaRelEstado(Int64 item_planta,String usuario)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO item_planta_rel_estado (item_planta_id ,item_estado_id,usuario) VALUES(" + item_planta + " ,1,'"+usuario+"');";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT item_planta_rel_estado)";
            }
            return mensaje;
        }
        //Inserta en bitacora_itemplanta_rel_estado
        public String InsertarBitacoraEstado(Int64 item_planta, int estado_id,String login,String observ)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO bitacora_itemplanta_rel_estado (item_planta_id ,item_estado_id, usu_login,observacion) VALUES(" + item_planta + " ,"+estado_id+",'" + login + "','"+observ+"');";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT bitacora_itemplanta_rel_estado)";
            }
            return mensaje;
        }
        //Inserta en item_planta_idioma
        public String InsertarItemPlantaIdioma(Int64 item_planta_id, int idioma_id, String descripcion,Boolean activo)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO item_planta_idioma (item_planta_id ,idioma_id, descripcion,activo) VALUES(" +item_planta_id+","+idioma_id+",'"+descripcion+"','"+activo+"');";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT item_planta_idioma)";
            }
            return mensaje;
        }
        //Inserta en bitacora_item_planta_idioma
        public String insertarBitacoraItemPlantaIdioma(Int64 item_planta_id, int idioma_id, String descripcion, Boolean activo, string usu)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO bitacora_item_planta_idioma (item_planta_id ,idioma_id, descripcion,activo,usu) VALUES(" + item_planta_id + "," + idioma_id + ",'" + descripcion + "','" + activo + "','"+usu+"');";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT bitacora_item_planta_idioma)";
            }
            return mensaje;
        }
        //Actualizar en item_planta_idioma
        public String ActualizarItemPlantaIdioma(Int64 item_planta_idioma_id, String descripcion, Boolean activo)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE item_planta_idioma SET  descripcion = '" + descripcion + "', activo = '" + activo + "' WHERE item_planta_idio_id="+item_planta_idioma_id+";";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE item_planta_idioma)";
            }
            return mensaje;
        }
        //Inserta en item_planta_precio trm y costo
        public String InsertarItemPlantaPrecio(Int64 item_planta_id, int moneda_id, int cliente_tipo_id, Decimal valor, Boolean activo, decimal margen, decimal Costo, decimal TRM,string usuario)
        {
            String mensaje = "";
            try
            {
               
                   String sql = "INSERT INTO item_planta_precio (item_planta_id ,moneda_id, cliente_tipo_planta_id,valor,activo,margen,Costo,TRM,usuario) VALUES(" + item_planta_id + "," + moneda_id + "," + cliente_tipo_id + "," + valor + ",'"+activo+"',"+margen+ "," + Costo + "," + TRM + ",'"+usuario+"');";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT item_planta_precio)";
            }
            return mensaje;
        }
        //Inserta en bitacora_item_planta_precio
        public String insertarBitacoraItemPlantaPrecio(Int64 item_planta_id, int moneda_id, int cliente_tipo_id, Decimal valor, Boolean activo,String usu, decimal margen, decimal Costo, decimal TRM)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO bitacora_item_planta_precio (item_planta_id ,moneda_id, cliente_tipo_planta_id,valor,activo,usu, margen, fecha ,Costo,TRM) VALUES(" + item_planta_id + "," + moneda_id + "," + cliente_tipo_id + "," + valor + ",'" + activo + "','" + usu + "'," + margen + "," + Costo + "," + TRM + ", GETDate());";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT bitacora_item_planta_precio)";
            }
            return mensaje;
        }
        //actualizar en item_planta_precio
        public String ActualizarItemPlantaPrecio(Int64 item_planta_precio_id, Boolean activo, decimal valor, decimal margen, decimal Costo, decimal TRM)
        {
            String mensaje = "";
            try
            { 
                String sql = "UPDATE item_planta_precio SET  activo= '" + activo + "',valor=" + valor + ",margen = " + margen + "',Costo=" + Costo + ",TRM = " + TRM + " WHERE item_planta_precio_id =" + item_planta_precio_id + "";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT item_planta_precio)";
            }
            return mensaje;
        }

       //solo actualiza estado de precio
        public String ActualizarestadoIpPrecio(Int64 item_planta_precio_id, Boolean activo)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE item_planta_precio SET  activo= '" + activo + "' WHERE item_planta_precio_id =" + item_planta_precio_id + "";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT item_planta_precio)";
            }
            return mensaje;
        }

        //Inserta en iitem_rel_criterio_iplanta
        public String InsertarRelCriterio(Int64 item_planta_id, String cod_plan1, String cod_plan2, String cod_plan3, String pos_arancelaria,Boolean activo, String cod_plan4, String cod_plan5)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO item_rel_criterio_iplanta (item_planta_id ,cod_plan1, cod_plan2,cod_plan3,pos_arancelaria,activo,cod_plan4,cod_plan5) VALUES(" + item_planta_id + ",'" + cod_plan1 + "','" + cod_plan2 + "','" + cod_plan3 + "','" + pos_arancelaria + "','"+activo+ "','" + cod_plan4 + "','" + cod_plan5 + "');";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT item_rel_criterio_iplanta)";
            }
            return mensaje;
        }
        //Inserta en bitacor_item_rel_criterio_iplanta
        public String insertarBitacoraRelCriterio(Int64 item_planta_id, String cod_plan1, String cod_plan2, String cod_plan3, String pos_arancelaria, Boolean activo,string usu, String cod_plan4, String cod_plan5)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO bitacora_criterio_iplanta (item_planta_id ,cod_plan1, cod_plan2,cod_plan3,pos_arancelaria,activo,usu,cod_plan4,cod_plan5) VALUES(" + item_planta_id + ",'" + cod_plan1 + "','" + cod_plan2 + "','" + cod_plan3 + "','" + pos_arancelaria + "','" + activo + "','"+usu+ "','" + cod_plan4 + "','" + cod_plan5 + "');";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT bitacora_criterio_iplanta)";
            }
            return mensaje;
        }


        //Actualizar en item_rel_criterio_iplanta
        public String ActualizarRelCriterio(Int64 item_planta_id, String cod_plan1, String cod_plan2, String cod_plan3, String cod_plan4, String cod_plan5, String pos_arancelaria, Boolean activo)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE  item_rel_criterio_iplanta  SET  cod_plan1='" + cod_plan1 + "', cod_plan2='" + cod_plan2 + "',cod_plan3='" + cod_plan3 + "',cod_plan4='" + cod_plan4 + "',cod_plan5='" + cod_plan5 + "',pos_arancelaria='" + pos_arancelaria + "',activo='" + activo + "' WHERE  item_planta_id=" + item_planta_id + ";";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE item_rel_criterio_iplanta)";
            }
            return mensaje;
        }
        //Se usa para consultar que el item no exista en la base de datos
        public Boolean ConsultarItem(String descripcion, Int64 item_planta ,String planta,String cia,String coderp)
        {
            Boolean existe = false;

            string sql;
            sql = "SELECT  item_planta_id"
            + " FROM  item_planta"
            + " WHERE (Ltrim(rtrim(descripcion)) = Ltrim(rtrim('" + descripcion + "'))) AND (activo = 1) AND item_planta_id = " + item_planta + "AND planta_id <> "+ planta;
            DataTable consulta = BdDatos.CargarTabla(sql);
            if(item_planta == 0)
            {
                if (!consulta.Rows.Count.Equals(0) || ConsultarItemOracle(descripcion,cia))
                {
                    existe = true;
                }
            }
            else
            {
                if(coderp.Equals("") || coderp.Equals(null))
                {
                    if (ConsultarItemOracle(descripcion, cia))
                    {
                        existe = true;
                    }
                }
                else
                {
                    if (ConsultarItemOracleERp(descripcion, planta,coderp))
                    {
                        existe = true;
                    }
                }

                if (!consulta.Rows.Count.Equals(0))
                {
                    existe = true;

                }
            }
            return existe;
        }
        //Consulta la descripcion del item_planta existe en oracle
        public Boolean ConsultarItemOracle(String descripcion,String cia)
        {
            string sql; Boolean existe_item = false;
            SqlDataReader reader = null;
            sql = "select F120_DESCRIPCION from  T120_MC_ITEMS INNER JOIN T121_MC_ITEMS_EXTENSIONES ON T120_MC_ITEMS.F120_ROWID =  T121_MC_ITEMS_EXTENSIONES.F121_ROWID_ITEM"
            + " WHERE  Ltrim(rtrim(F120_DESCRIPCION)) = Ltrim(rtrim('" + descripcion + "')) AND F120_ID_CIA =" + Convert.ToInt32(cia) + " AND T121_MC_ITEMS_EXTENSIONES.F121_IND_ESTADO = 1";
            reader = BdDatos.consultarConDataReaderOracle(sql);
           if(reader.HasRows)
           {
               existe_item = true;
           }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
            return existe_item;
        }
        //Consulta la descripcion del item_planta existe en oracle con el codigo erp
        public Boolean ConsultarItemOracleERp(String descripcion, String cia,String coderp)
        {
            string sql; Boolean existe_item = false;
            SqlDataReader reader = null;
            sql = "select F120_DESCRIPCION from  T120_MC_ITEMS INNER JOIN T121_MC_ITEMS_EXTENSIONES ON T120_MC_ITEMS.F120_ROWID =  T121_MC_ITEMS_EXTENSIONES.F121_ROWID_ITEM"
            + " WHERE  Ltrim(rtrim(F120_DESCRIPCION)) = Ltrim(rtrim('" + descripcion + "')) AND F120_ID_CIA =" + Convert.ToInt32(cia) + " AND T121_MC_ITEMS_EXTENSIONES.F121_IND_ESTADO = 1 AND F120_ID != "+coderp+"";
            reader = BdDatos.consultarConDataReaderOracle(sql);
            if (reader.HasRows)
            {
                existe_item = true;
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
            return existe_item;
        }

       // se cargar las descripciones existentes
        public DataTable CargarDesc1(String Descripc , int planta)
        {
            string sql;
            sql = "SELECT distinct  item_planta.item_planta_id,item_planta.descripcion  FROM  item_planta "
                    + " WHERE (Ltrim(rtrim(UPPER(descripcion))) LIKE N'%" + Descripc + "%') AND item_planta.planta_id = "+planta+" order by  descripcion ASC;";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        //Se usa para obtener item_planta del usuario logueado
        public DataTable PoblarReporteUsuario(String usuario,int num_perfil)
        {
            string sql;
            sql = "SELECT DISTINCT"
                         + "   item_planta.item_planta_id, item_planta.num_perfil, CONVERT(varchar(20), item_planta.cod_erp) AS cod_erp, item_planta.descripcion AS itemplanta_desc,  "
                         + "  ISNULL(item_grupo.descripcion, '') AS grupo_des, planta_forsa.planta_descripcion, item_planta.planta_id,  "
                         + "  CASE WHEN item_planta.disp_cotizacion = 1 THEN 'SI' ELSE 'NO' END AS disp_cotizacion,  "
                         +"  CASE WHEN item_planta.disp_comercial = 1 THEN 'SI' ELSE 'NO' END AS disp_comercial, "
                         + "  CASE WHEN item_planta.disp_ingenieria = 1 THEN 'SI' ELSE 'NO' END AS disp_ingenieria, "
                         + "   CASE WHEN item_planta.disp_almacen = 1 THEN 'SI' ELSE 'NO' END AS disp_almacen, "
                         + " CASE WHEN item_planta.disp_produccion = 1 THEN 'SI' ELSE 'NO' END AS disp_produccion,"
                         + " item_origen.item_origen_id AS origen,"
                         +" item_origen.descripcion AS origen_desc, '' AS Pleno, '' AS Distribuidor, '' AS Filial1, '' AS Filial2, item_estado.item_estado_id AS estado_id, "
                         + " item_estado.descripcion AS estado_desc, item_planta.crea_usu AS usuario, CASE WHEN item_planta.activo = 1 THEN 'SI' ELSE 'NO' END AS 'activo'"
                       +" FROM  item_grupo_planta INNER JOIN"
                         +" planta_forsa ON item_grupo_planta.planta_id = planta_forsa.planta_id INNER JOIN"
                         +" item_grupo ON item_grupo_planta.item_grupo_id = item_grupo.item_grupo_id RIGHT OUTER JOIN"
                         +" item_planta INNER JOIN"
                         +" item_origen ON item_planta.item_origen_id = item_origen.item_origen_id INNER JOIN"
                         +" item_estado INNER JOIN"
                         +" item_planta_rel_estado ON item_estado.item_estado_id = item_planta_rel_estado.item_estado_id ON "
                         + " item_planta.item_planta_id = item_planta_rel_estado.item_planta_id ON item_grupo_planta.item_grupo_planta_id = item_planta.item_grupo_planta_id AND "
                         +" planta_forsa.planta_id = item_planta.planta_id"
                         +" WHERE (item_planta.num_perfil = "+num_perfil+") AND (item_planta.crea_usu = N'"+usuario+"')";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Se usa para obtener precios principales relacionados al item planta del usuario logueado
        public DataTable PoblarUsuarioPrecio(String usuario, int num_perfil)
        {
            string sql;
            sql = "select   isnull(Pleno,0) AS Pleno, isnull(Distribuidor,0) AS Distribuidor , isnull(Filial1,0) AS Filial1, isnull(Filial2,0) AS Filial2"
                +" from (select   distinct item_planta.item_planta_id,item_planta.num_perfil,CONVERT(varchar(20), cod_erp)  as cod_erp,item_planta.descripcion AS itemplanta_desc,item_estado.item_estado_id AS estado_id,item_estado.descripcion AS estado_desc, item_planta.crea_usu AS usuario, item_planta.activo,"
                    +" item_planta_precio.valor,isnull(cliente_tipo.descripcion,0) AS descripcion"
                    +" from    item_planta_precio INNER JOIN"
                        +" cliente_tipo_planta ON item_planta_precio.cliente_tipo_planta_id = cliente_tipo_planta.cliente_tipo_planta_id INNER JOIN"
                          +" cliente_tipo ON cliente_tipo_planta.cliente_tipo_id = cliente_tipo.cliente_tipo_id full JOIN"
                       + " item_planta ON item_planta_precio.item_planta_id = item_planta.item_planta_id "
                            +" INNER JOIN item_estado"
						+" INNER JOIN item_planta_rel_estado ON item_estado.item_estado_id = item_planta_rel_estado.item_estado_id ON item_planta.item_planta_id = item_planta_rel_estado.item_planta_id"
                         + " WHERE  (item_planta.crea_usu = '" + usuario + "') AND (item_planta.num_perfil = " + num_perfil + "))d"
                    +" pivot(max(valor)"
                +" for descripcion in (Pleno, Distribuidor, Filial1, Filial2)) piv;";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Se usa para obtener item_planta de los demas usuarios con perfil aprobador
        public DataTable PoblarReporteUsuarioAprobador()
        {
            string sql;
            sql = "SELECT DISTINCT"
                         + "   item_planta.item_planta_id, item_planta.num_perfil, CONVERT(varchar(20), item_planta.cod_erp) AS cod_erp, item_planta.descripcion AS itemplanta_desc,  "
                         + "  ISNULL(item_grupo.descripcion, '') AS grupo_des, planta_forsa.planta_descripcion, item_planta.planta_id,  "
                         + "  CASE WHEN item_planta.disp_cotizacion = 1 THEN 'SI' ELSE 'NO' END AS disp_cotizacion,  "
                         + "  CASE WHEN item_planta.disp_comercial = 1 THEN 'SI' ELSE 'NO' END AS disp_comercial, "
                         + "  CASE WHEN item_planta.disp_ingenieria = 1 THEN 'SI' ELSE 'NO' END AS disp_ingenieria, "
                         + "   CASE WHEN item_planta.disp_almacen = 1 THEN 'SI' ELSE 'NO' END AS disp_almacen, "
                         + " CASE WHEN item_planta.disp_produccion = 1 THEN 'SI' ELSE 'NO' END AS disp_produccion,"
                         + " item_origen.item_origen_id AS origen,"
                         + " item_origen.descripcion AS origen_desc, '' AS Pleno, '' AS Distribuidor, '' AS Filial1, '' AS Filial2, item_estado.item_estado_id AS estado_id, "
                         + " item_estado.descripcion AS estado_desc, item_planta.crea_usu AS usuario, CASE WHEN item_planta.activo = 1 THEN 'SI' ELSE 'NO' END AS 'activo'"
                       + " FROM  item_grupo_planta INNER JOIN"
                         + " planta_forsa ON item_grupo_planta.planta_id = planta_forsa.planta_id INNER JOIN"
                         + " item_grupo ON item_grupo_planta.item_grupo_id = item_grupo.item_grupo_id RIGHT OUTER JOIN"
                         + " item_planta INNER JOIN"
                         + " item_origen ON item_planta.item_origen_id = item_origen.item_origen_id INNER JOIN"
                         + " item_estado INNER JOIN"
                         + " item_planta_rel_estado ON item_estado.item_estado_id = item_planta_rel_estado.item_estado_id ON "
                         + " item_planta.item_planta_id = item_planta_rel_estado.item_planta_id ON item_grupo_planta.item_grupo_planta_id = item_planta.item_grupo_planta_id AND "
                         + " planta_forsa.planta_id = item_planta.planta_id"
                            +" WHERE  "
                         +" (item_planta_rel_estado.item_estado_id = 4) OR"
                         +" (item_planta_rel_estado.item_estado_id = 5) OR"
                         +" (item_planta_rel_estado.item_estado_id = 3) OR "
                         + " (item_planta_rel_estado.item_estado_id = 9)"; ;
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Se usa para obtener item_planta de los demas usuarios con perfil aprobador sus precios
        public DataTable PoblarAprobadorPrecio()
        {
            string sql;
            sql = "select"
                +" isnull(Pleno,0) AS Pleno, isnull(Distribuidor,0) AS Distribuidor , isnull(Filial1,0) AS Filial1, isnull(Filial2,0) AS Filial2"
                +" from( select  distinct"
                +" item_planta.item_planta_id,"
                    +" item_planta.num_perfil,"
                +" CONVERT(varchar(20), cod_erp)  as cod_erp,"
                +" item_planta.descripcion AS itemplanta_desc,"
                +" item_estado.item_estado_id AS estado_id,"
                +" item_estado.descripcion AS estado_desc,"
                +" item_planta.crea_usu AS usuario, "
                +" item_planta.activo, "
                +" isnull(item_planta_precio.valor,0) as valor,"
                +" isnull(cliente_tipo.descripcion,0) AS descripcion "
                +" from    item_planta_precio "
                +" INNER JOIN cliente_tipo_planta ON item_planta_precio.cliente_tipo_planta_id = cliente_tipo_planta.cliente_tipo_planta_id "
            +" INNER JOIN cliente_tipo ON cliente_tipo_planta.cliente_tipo_id = cliente_tipo.cliente_tipo_id "
            +" full join item_planta ON item_planta_precio.item_planta_id = item_planta.item_planta_id "
                +" INNER JOIN item_estado "
                +" INNER JOIN item_planta_rel_estado ON item_estado.item_estado_id = item_planta_rel_estado.item_estado_id ON item_planta.item_planta_id = item_planta_rel_estado.item_planta_id  "
                +" WHERE"
                + " (  item_planta_rel_estado.item_estado_id = 4 OR   item_planta_rel_estado.item_estado_id = 5 OR   item_planta_rel_estado.item_estado_id = 3 OR   item_planta_rel_estado.item_estado_id = 9) )d  "
                +" pivot( max(valor)for descripcion in (Pleno, Distribuidor, Filial1, Filial2)) piv ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Se usa para obtener item_planta de los demas usuarios con perfil pre-aprobador
        public DataTable PoblarReporteUsuarioPreAprobador()
        {
            string sql;
            sql = "SELECT DISTINCT"
                         + "   item_planta.item_planta_id, item_planta.num_perfil, CONVERT(varchar(20), item_planta.cod_erp) AS cod_erp, item_planta.descripcion AS itemplanta_desc,  "
                         + "  ISNULL(item_grupo.descripcion, '') AS grupo_des, planta_forsa.planta_descripcion, item_planta.planta_id,  "
                         + "  CASE WHEN item_planta.disp_cotizacion = 1 THEN 'SI' ELSE 'NO' END AS disp_cotizacion,  "
                         + "  CASE WHEN item_planta.disp_comercial = 1 THEN 'SI' ELSE 'NO' END AS disp_comercial, "
                         + "  CASE WHEN item_planta.disp_ingenieria = 1 THEN 'SI' ELSE 'NO' END AS disp_ingenieria, "
                         + "   CASE WHEN item_planta.disp_almacen = 1 THEN 'SI' ELSE 'NO' END AS disp_almacen, "
                         + " CASE WHEN item_planta.disp_produccion = 1 THEN 'SI' ELSE 'NO' END AS disp_produccion,"
                         + " item_origen.item_origen_id AS origen,"
                         + " item_origen.descripcion AS origen_desc, '' AS Pleno, '' AS Distribuidor, '' AS Filial1, '' AS Filial2, item_estado.item_estado_id AS estado_id, "
                         + " item_estado.descripcion AS estado_desc, item_planta.crea_usu AS usuario, CASE WHEN item_planta.activo = 1 THEN 'SI' ELSE 'NO' END AS 'activo'"
                       + " FROM  item_grupo_planta INNER JOIN"
                         + " planta_forsa ON item_grupo_planta.planta_id = planta_forsa.planta_id INNER JOIN"
                         + " item_grupo ON item_grupo_planta.item_grupo_id = item_grupo.item_grupo_id RIGHT OUTER JOIN"
                         + " item_planta INNER JOIN"
                         + " item_origen ON item_planta.item_origen_id = item_origen.item_origen_id INNER JOIN"
                         + " item_estado INNER JOIN"
                         + " item_planta_rel_estado ON item_estado.item_estado_id = item_planta_rel_estado.item_estado_id ON "
                         + " item_planta.item_planta_id = item_planta_rel_estado.item_planta_id ON item_grupo_planta.item_grupo_planta_id = item_planta.item_grupo_planta_id AND "
                         + " planta_forsa.planta_id = item_planta.planta_id"
                            + " WHERE        (item_planta_rel_estado.item_estado_id = 2) OR"
                          + " (item_planta_rel_estado.item_estado_id = 9)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //actualizar estado de un item planta
        public String EstadoItemPlanta(Int64 item_planta_id,String observ,bool estado)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE item_planta SET   activo = '"+estado+"',observacion = '"+observ+"'   WHERE (item_planta_id = " + item_planta_id + ")  ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE item_planta)";
            }
            return mensaje;
        }
 
        //Actualizar en item_planta_rel_estado
        public String ActualizarRelEstado(Int64 item_planta, int estado,String observ,String usu)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE item_planta_rel_estado  SET  item_estado_id = " + estado + ",observacion='"+observ+"',fecha= GETDate(), usuario='"+usu+"' WHERE (item_planta_id = " + item_planta + ") ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE item_planta_rel_estado)";
            }
            return mensaje;
        }

     
        //Se usa consultar si el usuario logueado es autoaprobador
        public SqlDataReader ConsultarAutoAprobador(String usuario)
        {
            string sql;
            sql = "SELECT ISNULL(usu_auto_aprobador, 0) AS auto_aprobador FROM  usuario WHERE (usu_login = N'"+usuario+"')";
            return BdDatos.ConsultarConDataReader(sql);
        }
        //Se usa para consultar de la tabla item_planta_idioma la descripcion del item planta
        public DataTable ConsultarIdioma(Int64 item_planta)
        {
            string sql;
            sql = "SELECT  CONVERT(varchar(20), item_planta_idioma.item_planta_idio_id)  as planta_idioma_id, item_planta_idioma.idioma_id, idioma.descripcion AS idioma, item_planta_idioma.descripcion"
                    + " FROM  item_planta_idioma INNER JOIN"
                    + " idioma ON item_planta_idioma.idioma_id = idioma.idioma_id"
                    + " WHERE  (item_planta_idioma.item_planta_id = "+item_planta+") AND (item_planta_idioma.activo = 1) AND idioma.activo=1";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Se usa para consultar de la tabla item_planta_precio
        public DataTable ConsultarPrecio(Int64 item_planta)
        {
            string sql; 
             sql = "SELECT     '' as 'N°', CONVERT(varchar(20),item_planta_precio.item_planta_precio_id) AS item_planta_precio_id,CONVERT(varchar(20),item_planta_precio.moneda_id) as moneda_id, moneda.mon_descripcion AS moneda,CONVERT(varchar(20), isnull(item_planta_precio.valor,0)) AS valor,"
                + " item_planta_precio.cliente_tipo_planta_id, isnull(item_planta_precio.margen,1) as margen, cliente_tipo.descripcion AS cliente_tipo, item_planta_precio.Costo AS Costo, item_planta_precio.TRM AS TRM"
                + " FROM  item_planta_precio INNER JOIN"
                +" cliente_tipo_planta ON item_planta_precio.cliente_tipo_planta_id = cliente_tipo_planta.cliente_tipo_planta_id INNER JOIN"
                + " cliente_tipo ON cliente_tipo_planta.cliente_tipo_id = cliente_tipo.cliente_tipo_id INNER JOIN"
                +" moneda ON item_planta_precio.moneda_id = moneda.mon_id"
                +" WHERE  (item_planta_precio.item_planta_id = "+item_planta+") AND (item_planta_precio.activo = 1) AND moneda.mon_activo=1";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Se usa para consultar los datos principales del item a editar
        public DataTable ConsultarItemPrincipal(Int64 item_planta)
        {
            string sql="";
            if (ConsultarItem(item_planta))
            {
                if (Consultargrupoplanta(item_planta))
                {
                    //sql = "SELECT     item_grupo_planta.item_grupo_id AS grupo,item_grupo.descripcion AS grupo_desc,"
                    //    + "item_planta.item_id, item.descripcion, item_planta.planta_id,item_planta.item_grupo_planta_id, item_grupo_planta_idioma.descripcion AS grupoplanta, item_planta.cod_erp, item_planta.referencia,"
                    //    + " LTRIM(RTRIM(item_planta.descripcion)) AS detalle, LTRIM(RTRIM(item_planta.descripcion_corta)) AS descripcion_corta, isnull(item_planta.uso_compra,0) as uso_compra ,"
                    //    + " isnull(item_planta.uso_venta,0) as uso_venta , isnull(item_planta.uso_manufactura,0) as uso_manufactura, item_planta.item_origen_id, item_planta.tipo_inventario, item_planta.grupo_impositivo,"
                    //    + " item_planta.und_medida_principal, item_planta.und_medida_adicional, item_planta.und_medida_orden, item_planta.peso_unitario, isnull(item_planta.factor_orden,0) as factor_orden,"
                    //    + " isnull(item_planta.factor_adicional,0) as factor_adicional, item_planta.activo,item_planta.peso_empaque,item_planta.cant_empaque,item_planta.largo, item_planta.ancho1,item_planta.ancho2,item_planta.alto1,item_planta.alto2,isnull(item_planta.tipo_kamban,0)  as tipo_kamban,isnull(item_planta.disp_cotizacion,0) as disp_cotizacion,isnull( item_planta.disp_comercial,0) as disp_comercial,isnull(item_planta.disp_ingenieria,0) as disp_ingenieria,isnull(item_planta.disp_almacen,0) as disp_almacen,isnull(item_planta.disp_produccion,0) as disp_produccion,isnull( item_planta.req_plano,0) as req_plano,isnull(item_planta.req_tipo,0) as req_tipo,isnull(item_planta.req_modelo,0) as req_modelo,item_planta.tipo_orden_prod_id, tp.abreviatura"
                    //    + " FROM            item_grupo_planta RIGHT OUTER JOIN"
                    //    + " item ON item_grupo_planta.item_grupo_id = item.item_grupo_id INNER JOIN"
                    //    + " item_grupo_planta_idioma ON item_grupo_planta.item_grupo_planta_id = item_grupo_planta_idioma.item_grupo_planta_id INNER JOIN"
                    //   + "  item_grupo ON item_grupo_planta.item_grupo_id = item_grupo.item_grupo_id AND item.item_grupo_id = item_grupo.item_grupo_id RIGHT OUTER JOIN"
                    //   + "  item_planta ON item_grupo_planta_idioma.item_grupo_planta_id = item_planta.item_grupo_planta_id AND item.item_id = item_planta.item_id AND +"
                    //     + " item_grupo_planta.item_grupo_planta_id = item_planta.item_grupo_planta_id"
                    //     + " LEFT OUTER JOIN  tipo_orden_prod as tp ON item_planta.tipo_orden_prod_id = tp.tipo_orden_prod_id"
                    //    + " WHERE  (item_planta.item_planta_id = " + item_planta + ") AND (item_grupo_planta_idioma.idioma_id = 1)";

                    sql = "SELECT        item_grupo_planta.item_grupo_id AS grupo, item_grupo.descripcion AS grupo_desc, item_planta.item_id, item.descripcion, item_planta.planta_id, item_planta.item_grupo_planta_id, " +
                         " item_grupo_planta_idioma.descripcion AS grupoplanta, item_planta.cod_erp, item_planta.referencia, LTRIM(RTRIM(item_planta.descripcion)) AS detalle, LTRIM(RTRIM(item_planta.descripcion_corta)) " +
                         "AS descripcion_corta, ISNULL(item_planta.uso_compra, 0) AS uso_compra, ISNULL(item_planta.uso_venta, 0) AS uso_venta, ISNULL(item_planta.uso_manufactura, 0) AS uso_manufactura, " +
                         "item_planta.item_origen_id, item_planta.tipo_inventario, item_planta.grupo_impositivo, item_planta.und_medida_principal, item_planta.und_medida_adicional, item_planta.und_medida_orden, " +
                         "item_planta.peso_unitario, ISNULL(item_planta.factor_orden, 0) AS factor_orden, ISNULL(item_planta.factor_adicional, 0) AS factor_adicional, item_planta.activo, item_planta.peso_empaque, " +
                         "item_planta.cant_empaque, item_planta.largo, item_planta.ancho1, item_planta.ancho2, item_planta.alto1, item_planta.alto2, item_planta.item_ClasItem, ISNULL(item_planta.tipo_kamban, 0) AS tipo_kamban, " +
                         "ISNULL(item_planta.disp_cotizacion, 0) AS disp_cotizacion, ISNULL(item_planta.disp_comercial, 0) AS disp_comercial, ISNULL(item_planta.disp_ingenieria, 0) AS disp_ingenieria, " +
                         "ISNULL(item_planta.disp_almacen, 0) AS disp_almacen, ISNULL(item_planta.disp_produccion, 0) AS disp_produccion, ISNULL(item_planta.req_plano, 0) AS req_plano, ISNULL(item_planta.req_tipo, 0) AS req_tipo, " +
                         "ISNULL(item_planta.req_modelo, 0) AS req_modelo, item_planta.tipo_orden_prod_id, tp.abreviatura,ISNULL(item_planta.item_InspCal, 0) AS item_InspCal, ISNULL(item_planta.item_InspObligatoria, 0) AS item_InspObligatoria,item_planta.item_Bodega_Desc " +
                         "FROM            item_grupo_planta RIGHT OUTER JOIN " +
                         "item ON item_grupo_planta.item_grupo_id = item.item_grupo_id INNER JOIN " +
                         "item_grupo_planta_idioma ON item_grupo_planta.item_grupo_planta_id = item_grupo_planta_idioma.item_grupo_planta_id INNER JOIN " +
                         "item_grupo ON item_grupo_planta.item_grupo_id = item_grupo.item_grupo_id AND item.item_grupo_id = item_grupo.item_grupo_id RIGHT OUTER JOIN " +
                         "item_planta ON item.item_id = item_planta.item_id LEFT OUTER JOIN " +
                         "tipo_orden_prod AS tp ON item_planta.tipo_orden_prod_id = tp.tipo_orden_prod_id " +
                         "WHERE        (item_planta.item_planta_id = " + item_planta + ") AND (item_grupo_planta_idioma.idioma_id = 1)";
                }
                else
                {
                    sql = "SELECT  '' AS grupo,''  AS grupo_desc,item_planta.item_id, item.descripcion, item_planta.planta_id, '' AS item_grupo_planta_id,'' AS grupoplanta, item_planta.cod_erp, item_planta.referencia, " +
                        " LTRIM(RTRIM(item_planta.descripcion)) AS detalle,Ltrim(rtrim( item_planta.descripcion_corta)) as descripcion_corta,isnull(item_planta.uso_compra,0) as uso_compra, isnull(item_planta.uso_venta,0) as uso_venta , "+ 
                        " isnull(item_planta.uso_manufactura,0) as uso_manufactura, item_planta.item_origen_id,  item_planta.tipo_inventario, item_planta.grupo_impositivo, item_planta.und_medida_principal, item_planta.und_medida_adicional, " + 
                        " item_planta.und_medida_orden,  item_planta.peso_unitario, isnull(item_planta.factor_orden,0) as factor_orden,isnull(item_planta.factor_adicional,0) as factor_adicional,item_planta.activo,item_planta.peso_empaque, " + 
                        " item_planta.cant_empaque,item_planta.largo, item_planta.ancho1,item_planta.ancho2,item_planta.alto1,item_planta.alto2, item_planta.item_ClasItem, isnull(item_planta.tipo_kamban,0)  as tipo_kamban, " + 
                        " isnull(item_planta.disp_cotizacion,0) as disp_cotizacion,isnull( item_planta.disp_comercial,0) as disp_comercial,isnull(item_planta.disp_ingenieria,0) as disp_ingenieria,isnull(item_planta.disp_almacen,0) as disp_almacen, " + 
                        " isnull(item_planta.disp_produccion,0) as disp_produccion,isnull( item_planta.req_plano,0) as req_plano,isnull(item_planta.req_tipo,0) as req_tipo,isnull(item_planta.req_modelo,0) as req_modelo,item_planta.tipo_orden_prod_id, " +
                        " tp.abreviatura,ISNULL(item_planta.item_InspCal, 0) AS item_InspCal, ISNULL(item_planta.item_InspObligatoria, 0) AS item_InspObligatoria,item_planta.item_Bodega_Desc "
                    + " FROM item_planta INNER JOIN  item ON item_planta.item_id = item.item_id"
                     + " LEFT OUTER JOIN  tipo_orden_prod as tp ON item_planta.tipo_orden_prod_id = tp.tipo_orden_prod_id"
                    + " WHERE (item_planta.item_planta_id = " + item_planta + ")";
                }

            }
            else
            {
                if (Consultargrupoplanta(item_planta))
                {
                    sql = "SELECT  item_grupo_planta.item_grupo_id AS grupo,item_grupo_planta_idioma.descripcion AS grupo_desc,' ' AS item_id,' ' AS descripcion, item_planta.planta_id, item_planta.item_grupo_planta_id,"
                         + " item_grupo_planta_idioma.descripcion AS grupoplanta, item_planta.cod_erp, item_planta.referencia, LTRIM(RTRIM(item_planta.descripcion)) AS detalle,"
                        + " Ltrim(rtrim( item_planta.descripcion_corta)) as descripcion_corta,isnull(item_planta.uso_compra,0) as uso_compra, isnull(item_planta.uso_venta,0) as uso_venta , isnull(item_planta.uso_manufactura,0) as uso_manufactura, item_planta.item_origen_id, "
                      + " item_planta.tipo_inventario, item_planta.grupo_impositivo, item_planta.und_medida_principal, item_planta.und_medida_adicional, item_planta.und_medida_orden, "
                         + " item_planta.peso_unitario,isnull(item_planta.factor_orden,0) as factor_orden , isnull(item_planta.factor_adicional,0) as factor_adicional,item_planta.activo, " 
                         + " item_planta.peso_empaque,item_planta.cant_empaque,item_planta.largo, item_planta.ancho1,item_planta.ancho2,item_planta.alto1,item_planta.alto2,  item_planta.item_ClasItem, isnull(item_planta.tipo_kamban,0)  as tipo_kamban, "
                         + " isnull(item_planta.disp_cotizacion,0) as disp_cotizacion,isnull( item_planta.disp_comercial,0) as disp_comercial,isnull(item_planta.disp_ingenieria,0) as disp_ingenieria,isnull(item_planta.disp_almacen,0) as disp_almacen, " 
                         + " isnull(item_planta.disp_produccion,0) as disp_produccion,isnull( item_planta.req_plano,0) as req_plano,isnull(item_planta.req_tipo,0) as req_tipo,isnull(item_planta.req_modelo,0) as req_modelo, " 
                         + " item_planta.tipo_orden_prod_id,tp.abreviatura,ISNULL(item_planta.item_InspCal, 0) AS item_InspCal, ISNULL(item_planta.item_InspObligatoria, 0) AS item_InspObligatoria,item_planta.item_Bodega_Desc "
                           + " FROM item_planta INNER JOIN"
                         + " item_grupo_planta ON item_planta.item_grupo_planta_id = item_grupo_planta.item_grupo_planta_id INNER JOIN"
                         + " item_grupo ON item_grupo_planta.item_grupo_id = item_grupo.item_grupo_id INNER JOIN"
                         + " item_grupo_planta_idioma ON item_grupo_planta.item_grupo_planta_id = item_grupo_planta_idioma.item_grupo_planta_id"
                         +" LEFT OUTER JOIN  tipo_orden_prod as tp ON item_planta.tipo_orden_prod_id = tp.tipo_orden_prod_id"
                         + " WHERE (item_planta.item_planta_id = " + item_planta + ") AND (item_grupo_planta_idioma.idioma_id = 1)";

                }
                else
                {
                    sql = "SELECT  '' AS grupo,''  AS grupo_desc,'' AS item_id,'' AS descripcion, item_planta.planta_id, '' AS item_grupo_planta_id,"
                        + "'' AS grupoplanta, item_planta.cod_erp, item_planta.referencia, LTRIM(RTRIM(item_planta.descripcion)) AS detalle,"
                        + "Ltrim(rtrim( item_planta.descripcion_corta)) as descripcion_corta, isnull(item_planta.uso_compra,0) as uso_compra, isnull(item_planta.uso_venta,0) as uso_venta ," +
                          " isnull(item_planta.uso_manufactura,0) as uso_manufactura, item_planta.item_origen_id, "
                        + " item_planta.tipo_inventario, item_planta.grupo_impositivo, item_planta.und_medida_principal, item_planta.und_medida_adicional, item_planta.und_medida_orden, "
                        + " item_planta.peso_unitario, isnull(item_planta.factor_orden,0) as factor_orden, isnull(item_planta.factor_adicional,0) as factor_adicional,item_planta.activo, " +
                        " item_planta.peso_empaque,item_planta.cant_empaque,item_planta.largo, item_planta.ancho1,item_planta.ancho2,item_planta.alto1,item_planta.alto2,  item_planta.item_ClasItem,  "
                        + " isnull(item_planta.tipo_kamban,0)  as tipo_kamban,isnull(item_planta.disp_cotizacion,0) as disp_cotizacion,isnull( item_planta.disp_comercial,0) as disp_comercial, " +
                        " isnull(item_planta.disp_ingenieria,0) as disp_ingenieria,isnull(item_planta.disp_almacen,0) as disp_almacen,isnull(item_planta.disp_produccion,0) as disp_produccion, " +
                        " isnull( item_planta.req_plano,0) as req_plano,isnull(item_planta.req_tipo,0) as req_tipo,isnull(item_planta.req_modelo,0) as req_modelo,item_planta.tipo_orden_prod_id, " +
                        " tp.abreviatura ,ISNULL(item_planta.item_InspCal, 0) AS item_InspCal, ISNULL(item_planta.item_InspObligatoria, 0) AS item_InspObligatoria,item_planta.item_Bodega_Desc "
                        + " FROM item_planta "
                        +" LEFT OUTER JOIN  tipo_orden_prod as tp ON item_planta.tipo_orden_prod_id = tp.tipo_orden_prod_id"
                   + " WHERE (item_planta.item_planta_id = " + item_planta + ")";
                }
            }

            //sql = "SELECT item_grupo_planta.item_grupo_id AS grupo, item_grupo.descripcion AS grupo_desc, item_planta.item_id, item.descripcion, item_planta.planta_id,"
            //             +" item_planta.item_grupo_planta_id, item_grupo_planta_idioma.descripcion AS grupoplanta, item_planta.cod_erp, item_planta.referencia, "
            //             +" LTRIM(RTRIM(item_planta.descripcion)) AS detalle, LTRIM(RTRIM(item_planta.descripcion_corta)) AS descripcion_corta, item_planta.uso_compra, "
            //             +" item_planta.uso_venta, item_planta.uso_manufactura, item_planta.item_origen_id, item_planta.tipo_inventario, item_planta.grupo_impositivo, "
            //             +" item_planta.und_medida_principal, item_planta.und_medida_adicional, item_planta.und_medida_orden, item_planta.peso, item_planta.factor_orden, "
            //             +" item_planta.factor_adicional, item_planta.activo"
            //            +" FROM item_grupo_planta RIGHT OUTER JOIN"
            //             +" item ON item_grupo_planta.item_grupo_id = item.item_grupo_id LEFT OUTER JOIN"
            //             +" item_grupo_planta_idioma ON item_grupo_planta.item_grupo_planta_id = item_grupo_planta_idioma.item_grupo_planta_id LEFT OUTER JOIN"
            //             +" item_grupo ON item_grupo_planta.item_grupo_id = item_grupo.item_grupo_id AND item.item_grupo_id = item_grupo.item_grupo_id RIGHT OUTER JOIN"
            //             +" item_planta ON item_grupo_planta_idioma.item_grupo_planta_id = item_planta.item_grupo_planta_id AND item.item_id = item_planta.item_id AND "
            //             +" item_grupo_planta.item_grupo_planta_id = item_planta.item_grupo_planta_id"
            //            +" WHERE (item_planta.item_planta_id = "+item_planta+") AND (item_grupo_planta_idioma.idioma_id = 1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        private bool Consultargrupoplanta(long item_planta)
        {
            SqlDataReader reader = null;
            Boolean grupoPlanta = false;
            String result;
            string sql;
            sql = "SELECT  item_grupo_planta_id FROM item_planta WHERE  (item_planta_id = " + item_planta + ")";
            reader = BdDatos.ConsultarConDataReader(sql);
            if (reader.HasRows == true)
            {
                reader.Read();
                result = reader.GetInt32(0).ToString();
                if (!result.Equals("0"))
                {
                    grupoPlanta = true;
                }
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();

            return grupoPlanta;
        }

        private bool ConsultarItem(long item_planta)
        {
            SqlDataReader reader = null;
            Boolean item = false;
            String result;
            string sql;
            sql = "SELECT  item_id FROM item_planta WHERE  (item_planta_id = "+item_planta+")";
            reader = BdDatos.ConsultarConDataReader(sql);
            if (reader.HasRows == true)
            {
                reader.Read();
                result = reader.GetInt64(0).ToString();
                if (!result.Equals("0"))
                {
                    item = true;
                }
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();

            return item;
        }

       
        //Se usa para consultar los datos  de criterio de clasificacion del item a editar
        public DataTable ConsultarItemcriterios(Int64 item_planta)
        {
            string sql;
            sql = "SELECT cod_plan1, cod_plan2, cod_plan3, pos_arancelaria,cod_plan4,cod_plan5 FROM item_rel_criterio_iplanta WHERE (activo = 1) AND (item_planta_id = " + item_planta+")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Consulta la cia que pertenece la planta en oracle
        public String ConsultarCia(Int32 planta_id)
        {
            SqlDataReader reader = null;
            String cia = "";
            string sql;
            sql = "SELECT planta_cia FROM   planta_forsa WHERE (planta_id = "+planta_id+")";
            reader = BdDatos.ConsultarConDataReader(sql);
            if (reader.HasRows == true)
            {
                reader.Read();
                cia = reader.GetInt32(0).ToString();
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
            return cia;
        }

        //Consulta la planta  que pertenece el item_planta seleccionado
        //public int Consultarplanta(Int64 item_planta_id)
        //{
        //    SqlDataReader reader = null;
        //    int planta;
        //    string sql;
        //    sql = "SELECT  planta_id FROM item_planta WHERE (item_planta_id = "+item_planta_id+")";
        //    reader = BdDatos.ConsultarConDataReader(sql);
        //    reader.Read();
        //    planta = reader.GetInt32(0);
        //    reader.Close();
        //    reader.Dispose();
        //    return planta;
        //}

        //Se usa para  consultar  el ROw_idid del ultimo item 
        public SqlDataReader ConsultarIDOracle()
        {
            string sql;
            sql = " Select MAX(T120_MC_ITEMS.F120_ROWID)F120_ROWID " +
                    " From T120_MC_ITEMS";
            return BdDatos.consultarConDataReaderOracle(sql);
        }
        //Se usa para  consultar  el codigo del item y referencia
        public SqlDataReader ConsultarItemOracle(Int64 row_id)
        {
            string sql;
            sql = "Select T120_MC_ITEMS.F120_ID, T120_MC_ITEMS.F120_REFERENCIA  from T120_MC_ITEMS where T120_MC_ITEMS.F120_ROWID = " + row_id + "";
            return BdDatos.consultarConDataReaderOracle(sql);
        }

        //actualizar cod_erp y referencia
        public String ActualizarCodErp(Int64 item_planta_id,String CodErp, String Ref)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE item_planta SET   cod_erp = '"+CodErp+"',referencia = '" + Ref + "'   WHERE (item_planta_id = " + item_planta_id + ")  ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE item_planta)";
            }
            return mensaje;
        }


        //Actualizar en la tabla item_planta
        public String ActualizarItemPlanta(Int64 iplanta, Int64 item_id, int planta_id, int item_grupo_planta_id, String descripcion, 
                                           String descripcion_corta, Boolean uso_compra, Boolean uso_venta, Boolean uso_manufactura, 
                                           int item_origen_id, String tipo_inventario, String grupo_impositivo, String und_medida_principal,
                                           String und_medida_adicional, String und_medida_orden, decimal peso_unitario, decimal factor_orden, 
                                           decimal factor_adicional, bool ctrl_piciz, decimal peso_empaque, int cant_empaque, decimal largo,
                                           decimal ancho1, decimal ancho2,decimal alto1, decimal alto2, bool tipo_kamban, bool disp_cotizacion,
                                           bool disp_comercial,bool disp_ingenieria,bool disp_almacen, bool disp_produccion, bool req_plano,
                                           bool req_tipo, bool req_modelo, int tipo_orden_prod_id, bool Insp_Calidad, bool Insp_Obligatoria, int ClsItm_Id, string bodega)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE item_planta SET item_id =" + item_id + ", planta_id=" + planta_id + ", item_grupo_planta_id=" + item_grupo_planta_id + ", " +
                                                              " descripcion='" + descripcion + "', descripcion_corta='" + descripcion_corta + "', " +
                                                              " uso_compra='" + uso_compra + "',uso_venta='" + uso_venta + "',uso_manufactura='" + uso_manufactura + "', " +
                                                              " item_origen_id=" + item_origen_id + ",tipo_inventario='" + tipo_inventario + "', " +
                                                              " grupo_impositivo='" + grupo_impositivo + "',und_medida_principal='" + und_medida_principal + "', " +
                                                              " und_medida_adicional='" + und_medida_adicional + "',und_medida_orden='" + und_medida_orden + "'," + 
                                                              "  peso_unitario =" + peso_unitario + ",factor_orden=" + factor_orden + ",factor_adicional=" + factor_adicional + ", " +
                                                              " ctrl_piciz='" + ctrl_piciz + "', peso_empaque= " + peso_empaque + ", cant_empaque= " + cant_empaque + "," +
                                                              " largo =" + largo + ", ancho1 =" + ancho1 + ", ancho2=" + ancho2 + ", alto1=" + alto1 + ",alto2=" + alto2 + ", " +
                                                              " tipo_kamban= '" + tipo_kamban + "',disp_cotizacion='" + disp_cotizacion + "',disp_comercial='" + disp_comercial + "', " +
                                                              " disp_ingenieria= '" + disp_ingenieria + "',disp_almacen='" + disp_almacen + "',disp_produccion='" + disp_produccion + "', " +
                                                              " req_plano='" + req_plano + "',req_tipo='" + req_tipo + "',req_modelo='" + req_modelo + "', " +
                                                              " tipo_orden_prod_id=" + tipo_orden_prod_id + ",item_InspCal='"+Insp_Calidad+"',item_InspObligatoria='"+Insp_Obligatoria+"', " +
                                                              " item_ClasItem=" + ClsItm_Id + ",item_Bodega_Desc='"+bodega+"' " +
                                                              " WHERE  item_planta_id = " + iplanta + ";";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE item_planta)";
            }
            return mensaje;
        }

       /*CONSULTAR CRITERIO*/
        public Int64 Consultarcriterio(long item_planta)
        {
            SqlDataReader reader = null;
            Int64 result=0;
            string sql;
            sql = "SELECT  isnull(item_rel_criterio_iplanta_id,0) FROM item_rel_criterio_iplanta WHERE (activo = 1) AND (item_planta_id = "+item_planta+")";
            reader = BdDatos.ConsultarConDataReader(sql);
            if(reader.HasRows)
            {
                reader.Read();
                result = reader.GetInt64(0);
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();

            return result;
        }
        //Se usa para Consultar la observacion realizada al item cuando se almaceno su nuevo estado
        public String BuscarEstadoItem(Int64 item_planta_id)
        {
            SqlDataReader reader = null;
            string sql, sql1 = " ";
            sql = "SELECT  ISNULL(observacion, ' ')  FROM  item_planta_rel_estado WHERE  (item_planta_id = "+item_planta_id+")";
            reader = BdDatos.ConsultarConDataReader(sql);
            if (reader.HasRows)
            {
                reader.Read();
                sql1 = reader.GetString(0);
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
            return sql1;

        }
        //Se usa para obtener el nombre dle usuario y el estado del item_planta a editar
        public SqlDataReader ConsultarUsuEditar(Int64 item_planta)
        {
            string sql;
            sql = "SELECT  item_planta.crea_usu, item_planta_rel_estado.item_estado_id,item_planta.activo FROM  item_planta INNER JOIN"
               +" item_planta_rel_estado ON item_planta.item_planta_id = item_planta_rel_estado.item_planta_id"
                +" WHERE  (item_planta.item_planta_id = "+item_planta+")";
            return BdDatos.ConsultarConDataReader(sql);
        }
       
       /************ITEM FORSA **************/
        public DataTable cargarDescripcion(String texto)
        {
            DataTable consulta = null;
            String sql = "SELECT  item_id AS itemid, descripcion FROM item WHERE (Ltrim(rtrim(UPPER(descripcion))) LIKE N'%" + texto + "%') AND activo = 1 ORDER BY descripcion ASC";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        public SqlDataReader CargarTipoOrden()
        {
            string sql;
            sql = " SELECT tipo_orden_prod_id, abreviatura"
                   + " FROM tipo_orden_prod"
                   + " WHERE activo = (1)";
            return BdDatos.ConsultarConDataReader(sql);
        }
        public SqlDataReader CargarParametro()
        {
            string sql;
            sql = " SELECT item_tipo_parametro_id, descripcion"
                  + " FROM item_tipo_parametro"
                  + " WHERE activo = (1)";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public DataTable CargarTablaParametro(String id)
        {
            string sql;
            sql = "  SELECT item_planta_parametro.item_tipo_parametro_id,CONVERT(varchar(20),item_planta_parametro.item_planta_parametro_id) as item_parametro_id, item_tipo_parametro.descripcion as Descripcion"
                   + " FROM item_planta_parametro INNER JOIN"
                   + " item_tipo_parametro ON item_planta_parametro.item_tipo_parametro_id = item_tipo_parametro.item_tipo_parametro_id"
                   + " WHERE (item_planta_parametro.item_planta_id = " + id + ") and (item_planta_parametro.activo = (1))";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public SqlDataReader buscarDescripcion(String descripcion)
        {
            string sql;
            sql = " SELECT item_id, descripcion"
                + " FROM item"
                + " WHERE Ltrim(rtrim(UPPER(descripcion))) =  N'"+ descripcion +"' AND (activo = 1)";
            return BdDatos.ConsultarConDataReader(sql);
        }
        public SqlDataReader buscarDescripcionID(String descripcion,Int64 id)
        {
            string sql;
            sql = " SELECT item_id, descripcion"
                + " FROM item"
                + " WHERE  Ltrim(rtrim(UPPER(descripcion))) = N'"+ descripcion +"' AND (activo = 1) AND item_id !=" + id + ";";
            return BdDatos.ConsultarConDataReader(sql);
        }
        public String InsertarItemAgrupador(String idGrupo, String descripcion)
        {
            String mensaje = "";
            DataTable consulta = null;
            try
            {
                String sql = "INSERT INTO item (item_grupo_id,  descripcion, activo) VALUES(" + idGrupo + ",'" + descripcion + "',1); "
                    + " SELECT  MAX(item_id) AS item_id FROM item";
                consulta = BdDatos.CargarTabla(sql);
                foreach (DataRow row in consulta.Rows)
                {
                    mensaje = row["item_id"].ToString();
                }
                
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT item)";
            }
            return mensaje;
        }
        public String editarItem(Int64 item_id,String idGrupo, String descripcion)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE item SET  item_grupo_id = " + idGrupo + ",  descripcion = '" + descripcion + "'"
                + " WHERE (item_id = " + item_id + ");";
                //String sql = "update item_grupo_planta set  item_grupo_planta.item_grupo_id= item.item_grupo_id "+
                //             "FROM            item_planta INNER JOIN "+
                //             "item_grupo_planta ON item_planta.item_grupo_planta_id = item_grupo_planta.item_grupo_planta_id INNER JOIN "+
                //             "item ON item_planta.item_id = item.item_id "+
                //             "WHERE        (item.item_id = "+item_id+")";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE item)";
            }
            return mensaje;
        }

        public String editarActivoItem(Int64 item_id,bool estado)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE item SET  activo = '"+estado+"'"
                + " WHERE (item_id = " + item_id + ")  ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE item)";
            }
            return mensaje;
        }

        public String InsertarBitacoraItem(Int64 item_id, String idGrupo, String descripcion, String usu,int activo)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO bitacora_item (item_id ,item_grupo_id,  descripcion, usu, activo) VALUES(" + item_id+ "," + idGrupo + ",'" + descripcion + "','"+usu+"',"+activo+");";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT bitacora_item)";
            }
            return mensaje;
        }
        public String InsertarBitacoraItemEstado(Int64 item_id, int estado,String usuario)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO bitacora_item_rel_estado (item_id ,item_estado_id, usu) VALUES( "+item_id+","+estado+",'"+usuario + "');";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT bitacora_item_rel_estado)";
            }
            return mensaje;
        }
        public String InsertarItemParametro(Int64 item_planta_id,int  itemparametroid)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO item_planta_parametro (item_tipo_parametro_id, item_planta_id, activo) VALUES(" + itemparametroid + "," + item_planta_id + ",'true');";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT item_parametro)";
            }
            return mensaje;
        }
        public String BitacoraItemParametro(Int64 item_planta_id, int itemparametroid,String usuario,bool activo)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO bitacora_item_planta_parametro (item_tipo_parametro_id, item_planta_id, activo,usu) VALUES(" + itemparametroid + "," + item_planta_id + ",'" + activo + "','" + usuario + "');";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT item_parametro)";
            }
            return mensaje;
        }
        public String InsertarItemEstado(Int64 item_id, String usuario)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO item_rel_estado (item_id ,item_estado_id, usuario) VALUES("+item_id+ "," + 5 + ",'" + usuario + "');";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT item_rel_estado)";
            }
            return mensaje;
        }
        public String EditarItemEstado(Int64 item_id, String usuario,int estado)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE  item_rel_estado  SET  item_estado_id=" + estado + ", usuario ='" + usuario + "' WHERE item_id =" + item_id +";";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE item_rel_estado)";
            }
            return mensaje;
        }
        public DataTable buscarItemAgrupador(Int64 itemid)
        {
            DataTable consulta = null;
            String sql = " SELECT i.item_grupo_id, i.descripcion,  item_grupo.descripcion AS grupo_desc, i.activo  FROM item as i  FULL OUTER JOIN   item_grupo ON i.item_grupo_id = item_grupo.item_grupo_id WHERE (i.item_id = " + itemid + ")";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        public DataTable CargarTablaParametro(Int64 id)
        {
            string sql;
            sql = "  SELECT CAST(item_parametro.item_parametro_id AS char(10)) as item_parametro_id,item_parametro.item_tipo_parametro_id, item_tipo_parametro.descripcion as Descripcion"
                   + " FROM item_parametro INNER JOIN"
                   + " item_tipo_parametro ON item_parametro.item_tipo_parametro_id = item_tipo_parametro.item_tipo_parametro_id"
                   + " WHERE (item_parametro.item_id = " + id + ") and (item_parametro.activo = (1))";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        public String editarItemParametroActivo(string id)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE item_planta_parametro SET activo = 'False' "
                + " WHERE (item_planta_parametro_id = " + id + ")  ";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE item_parametro)";
            }
            return mensaje;
        }

        //Consulta el perfil de usuario
        public String ConsultarPerfil(String usuario)
        {
            SqlDataReader reader = null;
            String numperfil = "";
            string sql;
            sql = "SELECT num_perfil FROM  item_planta WHERE  (crea_usu LIKE '% " + usuario + "%')";
            reader = BdDatos.ConsultarConDataReader(sql);
            if (reader.HasRows == true)
            {
                reader.Read();
                numperfil = reader.GetInt32(0).ToString();
            }
            reader.Close();            
            reader.Dispose();
            BdDatos.desconectar();
            return numperfil;
        }
        public DataTable reporteItem()
        {
            DataTable consulta = null;
            String sql = "SELECT item.item_id,item.descripcion as Descripcion,item_grupo.descripcion as nombre_grupo,CASE WHEN item.activo = 1 THEN 'SI' ELSE 'NO' END AS 'activo'  FROM item INNER JOIN item_grupo ON item_grupo.item_grupo_id = item.item_grupo_id  order by item_id desc;";
            consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }

        public SqlDataReader consultarNombreERP(string cod_erp, int planta)
        {
            SqlDataReader reader = null;
            string sql ="";
            //if (planta == 3)
            //{ sql = "SELECT        planta_descripcion FROM            planta_forsa  WHERE        (planta_id = 3 @)";  }
            //else
            //{
             sql = "SELECT top 1 VW_Item_Saldos_ERP_TOTAL.NOMBRE_ITEM FROM VW_Item_Saldos_ERP_TOTAL INNER JOIN "+
                         "planta_forsa ON VW_Item_Saldos_ERP_TOTAL.COMPANIA = planta_forsa.planta_cia "+
                         "WHERE(VW_Item_Saldos_ERP_TOTAL.COD_ITEM = '"+cod_erp+"') AND(planta_forsa.planta_activo = 1) AND(planta_forsa.planta_id = "+planta+")";
            //}
            reader = BdDatos.ConsultarConDataReader(sql);
            return reader;
        }

        public string actualizarDeERP(int item_planta_id, string erp)
        {
            string mensaje = "";
            string sql = "";
            try {
                sql = "UPDATE item_planta SET item_planta.cod_erp = '"+erp+"' WHERE item_planta.item_planta_id = "+item_planta_id+"";
                BdDatos.Actualizar(sql);
                sql = "UPDATE item_planta SET item_planta.uso_compra = vista.IND_COMPRA, " +
                        "item_planta.uso_manufactura = vista.IND_MANUF, " +
                        "item_planta.grupo_impositivo = vista.GRUPO_IMPOS, " +
                        "item_planta.und_medida_principal = vista.UND_INV, " +
                        "item_planta.und_medida_adicional = vista.IND_UNIDAD_ADIC, " +
                        "item_planta.und_medida_orden = vista.ID_UND_ORDEN, " +
                        "item_planta.factor_orden = vista.FACTOR, " +
                        "item_planta.tipo_inventario = vista.TIPO_INV, " +
                        "item_planta.referencia = '"+erp+"', " +
                        "item_planta.descripcion = vista.NOMBRE_ITEM, " +
                        "item_planta.descripcion_corta = SUBSTRING(vista.NOMBRE_ITEM, 1, 20) " +
                        "FROM            item_planta " +
                        " INNER JOIN                             VW_Item_Saldos_ERP_TOTAL as vista " +
                        " ON                             item_planta.cod_erp = vista.COD_ITEM " +
                        "WHERE  item_planta.item_planta_id = "+ item_planta_id + "";
                BdDatos.Actualizar(sql);
                sql = "UPDATE       item_planta_idioma " +
                       " SET                descripcion = item_planta.descripcion " +
                       " FROM            item_planta inner join item_planta_idioma on item_planta.item_planta_id = item_planta_idioma.item_planta_id " +
                       " WHERE        (item_planta.item_planta_id = " + item_planta_id + ") and item_planta_idioma.idioma_id = 1;";
                mensaje = "OK";
            } 
            catch { mensaje = "ERROR ACTUALIZANDO EL ITEM PLANTA"; }
            return mensaje;
        }

        public SqlDataReader poblarReporteItemPlanta(int moneda, int perfil, int planta) 
        {
            SqlDataReader reader = null;
            string filtro = "";
            //Aprobador
            if (perfil.Equals(1)) 
            {
                filtro = " AND (Vista_Item.vista_reporte= 1)AND ((Vista_Item.item_estado_id = 4) OR"
                         + " (Vista_Item.item_estado_id = 5) OR"
                         + " (Vista_Item.item_estado_id = 6) OR"
                         + " (Vista_Item.item_estado_id = 3) OR "
                         + " (Vista_Item.item_estado_id = 1) OR "
                         + " (Vista_Item.item_estado_id = 2) OR "
                         + " (Vista_Item.item_estado_id = 9))";            
            }
            //Solicitante
            else if (perfil.Equals(2) || perfil.Equals(0)) 
            {
                filtro = " AND ((Vista_Item.item_estado_id = 2) OR"
                    + " (Vista_Item.item_estado_id = 1) OR "
                    + " (Vista_Item.item_estado_id = 5) OR"
                    + " (Vista_Item.item_estado_id = 9))";
            }
            //Pre Aprobador
            else if (perfil.Equals(3))
            {
                filtro = " AND ((Vista_Item.item_estado_id = 2) OR"
                    + " (Vista_Item.item_estado_id = 1) OR "
                    + " (Vista_Item.item_estado_id = 5) OR"
                    + " (Vista_Item.item_estado_id = 9))";
            }

            string sql = "";
            sql = "SELECT Vista_Item.item_planta_id, Vista_Item.num_perfil, Vista_Item.grupoEspanol AS grupo_des, Vista_Item.codErp AS cod_erp, Vista_Item.nombreEspanol AS itemplanta_desc, "+
                  "Vista_Item.origenDescripcion AS origen_desc, Vista_Item.plantaId AS planta_id, Vista_Item.planta_descripcion, "+
                  "  CASE WHEN ISNULL(Vista_Item.dispCotizacion, 0) = 1 THEN 'SI' ELSE 'NO' END AS disp_cotizacion,  "
                  +"  CASE WHEN ISNULL(Vista_Item.dispCormercial, 0) = 1 THEN 'SI' ELSE 'NO' END AS disp_comercial, "
                  + "  CASE WHEN ISNULL(Vista_Item.despIngenieria, 0) = 1 THEN 'SI' ELSE 'NO' END AS disp_ingenieria, "
                  + "   CASE WHEN ISNULL(Vista_Item.dispAlmacen, 0) = 1 THEN 'SI' ELSE 'NO' END AS disp_almacen, "
                  + " CASE WHEN ISNULL(Vista_Item.dispProduccion, 0) = 1 THEN 'SI' ELSE 'NO' END AS disp_produccion, " +
                  " Vista_Item.item_origen_id AS origen, "+
                  "CASE WHEN planta_forsa.planta_moneda_id = 1 THEN ISNULL(Vista_Item.precioPesosColPleno, 0) ELSE ISNULL(Vista_Item.precioDolarColPleno, 0) END AS Pleno, "+
                  "CASE WHEN planta_forsa.planta_moneda_id = 1 THEN ISNULL(Vista_Item.precioPesosColDistribuidor, 0) ELSE ISNULL(Vista_Item.precioDolarDistribuidor, 0) "+
                  "END AS Distribuidor, CASE WHEN planta_forsa.planta_moneda_id = 1 THEN ISNULL(Vista_Item.precioPesosColFilial1, 0) ELSE ISNULL(Vista_Item.precioDolarFilial1, 0) "+
                  "END AS Filial1, CASE WHEN planta_forsa.planta_moneda_id = 1 THEN ISNULL(Vista_Item.precioPesosColFilial2, 0) ELSE ISNULL(Vista_Item.precioDolarlFilial2, 0) "+
                  "END AS Filial2, ISNULL(Vista_Item.item_estado_id, 0) AS estado_id, Vista_Item.estadoDescripcion AS estado_desc, Vista_Item.crea_usu AS usuario, "+
                  "CASE WHEN ISNULL(Vista_Item.activo, 0) = 1 THEN 'SI' ELSE 'NO' END AS activo,Vista_Item.fechaSolicitud,Vista_Item.fechaCreacion, " +
                  "CASE WHEN Vista_Item.fechaSolicitud IS NULL  OR  Vista_Item.fechaCreacion IS NULL THEN  NULL ELSE  DATEDIFF(mi,Vista_Item.fechaSolicitud,Vista_Item.fechaCreacion) / 60  END horas " +
                  "FROM     Vista_Item_TODOS as Vista_Item INNER JOIN " +
                  "planta_forsa ON Vista_Item.plantaId = planta_forsa.planta_id "+
                  "WHERE  (planta_forsa.planta_moneda_id = " + moneda + " AND Vista_Item.plantaId = " + planta + ") " + filtro + ""; 

            reader = BdDatos.ConsultarConDataReader(sql);

            return reader;
        
        }

     
        // cierra conexiones existentes con la base de datos
        public int CerrarConexion()
        {
            return BdDatos.desconectar();
        }
        private string buscarespeciales(string p)
        {

            if (p.Contains("'"))
            {
                p = p.Replace("'", "''");
            }
            return p;
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

                        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
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


        public void enviarCorreoPrueba(int actseg_id, out string mensaje, string fecEntrega_Fin_Acc, string fecEntrega_Fin_Alum)
        {
            //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[3];
            sqls[0] = new SqlParameter("@actseg_id", actseg_id);
            sqls[1] = new SqlParameter("@actseg_fecEntrega_Fin_Acc", fecEntrega_Fin_Acc);
            sqls[2] = new SqlParameter("@actseg_fecEntrega_Fin_Alum", fecEntrega_Fin_Alum);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_Planeador_notificaciones", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlParameter Asunto = new SqlParameter("@pAsun_mail", SqlDbType.VarChar, 200);
                    SqlParameter Destinatarios = new SqlParameter("@pLista", SqlDbType.VarChar, 12500);
                    SqlParameter Mensaje = new SqlParameter("@pMsg", SqlDbType.VarChar, 20500);


                    Asunto.Direction = ParameterDirection.Output;
                    Destinatarios.Direction = ParameterDirection.Output;
                    Mensaje.Direction = ParameterDirection.Output;


                    cmd.Parameters.Add(Asunto);
                    cmd.Parameters.Add(Destinatarios);
                    cmd.Parameters.Add(Mensaje);


                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //VALORES DEL ENCABEZADO 
                        string AsuntoMail = Convert.ToString(Asunto.Value);
                        string DestinatariosMail = Convert.ToString(Destinatarios.Value);
                        if (String.IsNullOrEmpty(DestinatariosMail))
                        {
                            /*Este correo no esta habilitado, solo sirve para que no se estrelle cuando el destinatario sea vacio,
                              ya que el metodo se ejecuta por cada acción realizada en la grilla*/
                            DestinatariosMail = "stiven@forsa.net.co";
                        }
                        string MensajeMail = Convert.ToString(Mensaje.Value);


                        WebClient clienteWeb = new WebClient();
                        clienteWeb.Dispose();
                        clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "forsa", "FORSA");
                        // Adjunto
                        //DEFINIMOS LA CLASE DE MAILMESSAGE
                        MailMessage mail = new MailMessage();
                        //INDICAMOS EL EMAIL DE ORIGEN
                        mail.From = new MailAddress("informes@forsa.net.co");
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
        //kp
        public int Crear_Accesorio(int id_UnoE,string nombAcc,string descAbre, float dim1min, float dim1max, float dim2min, float dim2max,
                                   float dim3min, float dim3max, float dim4min, float dim4max, float dim5min, float dim5max,
                                   float dim6min, float dim6max, float dim7min, float dim7max, int plant,int anulado, int idItemPlan)
        {
            string sql = "INSERT INTO Accesorios_Codigos (Id_UnoE, Nomenclatura, Des_Aux, Valor_1_Min, Valor_1_Max, Valor_2_Min, Valor_2_Max, Valor_3_Min, Valor_3_Max, Valor_4_Min, Valor_4_Max, Valor_5_Min, Valor_5_Max,  " +
                                                        " Valor_6_Min, Valor_6_Max,Valor_7_Min, Valor_7_Max, planta_id, Acc_Anulado, Acc_Id_ItemPlanta) " +
                                                 " VALUES(" +  id_UnoE.ToString() + ",'" + nombAcc + "','" + descAbre + "'," + dim1min.ToString() + "," + dim1max.ToString() + "," +
                                                              " " + dim2min.ToString() + "," + dim2max.ToString() + "," + dim3min.ToString() + "," + dim3max.ToString() + "," + dim4min.ToString() + "," + dim4max.ToString() + "," +
                                                              " " + dim5min.ToString() + "," + dim5max.ToString() + "," + dim6min.ToString() + "," + dim6max.ToString() + "," + dim7min.ToString() + "," + dim7max.ToString() + ", " +
                                                              " " + plant.ToString() + ","+anulado+ "," + idItemPlan.ToString() + ")";
            return BdDatos.ejecutarSql(sql);
        }

        //KP
        public DataTable Consultar_Detalle_Accesorio(int idItemPlanta, int planta)

        {
            string sql = " SELECT  Nomenclatura,Des_Aux,Valor_1_Min,Valor_1_Max,Valor_2_Min,Valor_2_Max,Valor_3_Min,Valor_3_Max, " +
                        " Valor_4_Min, Valor_4_Max, Valor_5_Min, Valor_5_Max, Valor_6_Min, Valor_6_Max, Valor_7_Min, Valor_7_Max,planta_id,Codigos_Id,Acc_Anulado " +
                        " FROM Accesorios_Codigos " +
                        " WHERE Acc_Id_ItemPlanta=" + idItemPlanta.ToString()+"  AND planta_id="+planta.ToString()+ " ";
            return BdDatos.CargarTabla(sql);
        }

        //KP

        public int Actualizar_Detalle_Accesorio(string nombAcc, string descAbre, float dim1min, float dim1max, float dim2min, float dim2max,
                                  float dim3min, float dim3max, float dim4min, float dim4max, float dim5min, float dim5max,
                                  float dim6min, float dim6max, float dim7min, float dim7max, int idItemPlanta, int planta, int idAcce)
        {
            string sql = "UPDATE Accesorios_Codigos SET Nomenclatura = '" + nombAcc + "', Des_Aux = '" + descAbre + "', Valor_1_Min =" + dim1min.ToString() + "," +
                                                      " Valor_1_Max = " + dim1max + ", Valor_2_Min =" + dim2min + "," +
                                                      " Valor_2_Max = " + dim2max + ", Valor_3_Min = " + dim3min.ToString() + "," +
                                                      " Valor_3_Max = " + dim3max.ToString() + ", Valor_4_Min = " + dim4min.ToString() + ", " +
                                                      " Valor_4_Max = " + dim4max.ToString() + ", Valor_5_Min = " + dim5min.ToString() + "," +
                                                      " Valor_5_Max = " + dim5max.ToString() + ", Valor_6_Min = " + dim6min.ToString() + "," +
                                                      " Valor_6_Max = " + dim6max.ToString() + ", Valor_7_Min = " + dim7min.ToString() + ", " +
                                                      " Valor_7_Max = " + dim7max.ToString() + ", planta_id = " + planta.ToString() + " " +
                        " WHERE Acc_Id_ItemPlanta =" + idItemPlanta.ToString() + "  AND planta_id =" + planta.ToString() + " AND Codigos_Id=" + idAcce.ToString() + "";
            return BdDatos.ejecutarSql(sql);
        }

        public int Actualizar_Estado_Accesorio(int estado, int idItemPlanta, int planta, int idAcce)
        {
            string sql = "UPDATE Accesorios_Codigos SET Acc_Anulado="+estado+" " +
                        " WHERE Acc_Id_ItemPlanta =" + idItemPlanta.ToString() + "  AND planta_id =" + planta.ToString() + " AND Codigos_Id=" + idAcce.ToString() + "";
            return BdDatos.ejecutarSql(sql);
        }


        public int Activar_ConfiguracionAccesorio(int activo,int idItemPlanta, int planta, int idAcce)
        {
            string sql = "UPDATE Accesorios_Codigos SET Acc_Anulado="+activo +" " +
                        " WHERE Acc_Id_ItemPlanta =" + idItemPlanta.ToString() + "  AND planta_id =" + planta.ToString() + " AND Codigos_Id=" + idAcce.ToString() + "";
            return BdDatos.ejecutarSql(sql);
        }
        //kp
        public DataTable Consultar_Validacion_Dimensiones_Accesorio(string nombAcce, string descAbre, int planta,int id_unoe)
        {
            string sql = " SELECT Codigos_Id, Nomenclatura,Des_Aux,Valor_1_Min,Valor_1_Max,Valor_2_Min,Valor_2_Max,Valor_3_Min,Valor_3_Max, " +
                                " Valor_4_Min, Valor_4_Max, Valor_5_Min, Valor_5_Max, Valor_6_Min, Valor_6_Max, Valor_7_Min, Valor_7_Max, planta_id " +
                         " FROM Accesorios_Codigos  " +
                         " WHERE Nomenclatura ='" + nombAcce + "' AND planta_id =" + planta.ToString() + " AND Des_Aux ='" + descAbre + "' AND Id_UnoE <>"+id_unoe.ToString()+" ";
            return BdDatos.CargarTabla(sql);
        }

        //kp
        public DataSet Consultar_Accesorios(string nombAcce, string descAbre, int planta)
        {
            string sql = " SELECT    Accesorios_Codigos.Id_UnoE, Accesorios_Codigos.Nomenclatura, Accesorios_Codigos.Des_Aux,Accesorios_Codigos.Valor_1_Min , " +
                                   " Accesorios_Codigos.Valor_1_Max , Accesorios_Codigos.Valor_2_Min, Accesorios_Codigos.Valor_2_Max, Accesorios_Codigos.Valor_3_Min, " +
                                   " Accesorios_Codigos.Valor_3_Max, Accesorios_Codigos.Valor_4_Min, Accesorios_Codigos.Valor_4_Max, Accesorios_Codigos.Valor_5_Min, " +
                                   " Accesorios_Codigos.Valor_5_Max, Accesorios_Codigos.Valor_6_Min, Accesorios_Codigos.Valor_6_Max, Accesorios_Codigos.Valor_7_Min, " +
                                   " Accesorios_Codigos.Valor_7_Max, planta_forsa.planta_descripcion " +
                          " FROM   Accesorios_Codigos INNER JOIN planta_forsa ON Accesorios_Codigos.planta_id = planta_forsa.planta_id " +
                          " WHERE  (Accesorios_Codigos.Nomenclatura ='" + nombAcce + "') AND (Accesorios_Codigos.planta_id =" + planta.ToString() + ")  AND (Accesorios_Codigos.Des_Aux ='" + descAbre + "' AND( Acc_Anulado=0))";
            return BdDatos.consultarConDataset(sql);
        }

        public DataTable Consultar_AccesoriosDtt(string nombAcce, string descAbre, int planta,int Id_ItemPlanta)
        {
            string sql = " SELECT     Accesorios_Codigos.Id_UnoE, Accesorios_Codigos.Nomenclatura, Accesorios_Codigos.Des_Aux,Accesorios_Codigos.Valor_1_Min , " +
                                   " Accesorios_Codigos.Valor_1_Max , Accesorios_Codigos.Valor_2_Min, Accesorios_Codigos.Valor_2_Max, Accesorios_Codigos.Valor_3_Min, " +
                                   " Accesorios_Codigos.Valor_3_Max, Accesorios_Codigos.Valor_4_Min, Accesorios_Codigos.Valor_4_Max, Accesorios_Codigos.Valor_5_Min, " +
                                   " Accesorios_Codigos.Valor_5_Max, Accesorios_Codigos.Valor_6_Min, Accesorios_Codigos.Valor_6_Max, Accesorios_Codigos.Valor_7_Min, " +
                                   " Accesorios_Codigos.Valor_7_Max, planta_forsa.planta_descripcion,Codigos_Id " +
                          " FROM   Accesorios_Codigos INNER JOIN planta_forsa ON Accesorios_Codigos.planta_id = planta_forsa.planta_id " +
                          " WHERE  (Accesorios_Codigos.Nomenclatura ='" + nombAcce + "') AND (Accesorios_Codigos.planta_id =" + planta.ToString() + ")  AND (Accesorios_Codigos.Des_Aux ='" + descAbre + "') AND (Acc_Id_ItemPlanta<>" + Id_ItemPlanta.ToString() + ") AND( Acc_Anulado=0)";
            return BdDatos.CargarTabla(sql);
        }

        public DataTable Consultar_AccesoriosValidaReg(string nombAcce, string descAbre, int planta)
        {
            string sql = " SELECT     Accesorios_Codigos.Id_UnoE, Accesorios_Codigos.Nomenclatura, Accesorios_Codigos.Des_Aux,Accesorios_Codigos.Valor_1_Min , " +
                                   " Accesorios_Codigos.Valor_1_Max , Accesorios_Codigos.Valor_2_Min, Accesorios_Codigos.Valor_2_Max, Accesorios_Codigos.Valor_3_Min, " +
                                   " Accesorios_Codigos.Valor_3_Max, Accesorios_Codigos.Valor_4_Min, Accesorios_Codigos.Valor_4_Max, Accesorios_Codigos.Valor_5_Min, " +
                                   " Accesorios_Codigos.Valor_5_Max, Accesorios_Codigos.Valor_6_Min, Accesorios_Codigos.Valor_6_Max, Accesorios_Codigos.Valor_7_Min, " +
                                   " Accesorios_Codigos.Valor_7_Max, planta_forsa.planta_descripcion,Codigos_Id " +
                          " FROM   Accesorios_Codigos INNER JOIN planta_forsa ON Accesorios_Codigos.planta_id = planta_forsa.planta_id " +
                          " WHERE  (Accesorios_Codigos.Nomenclatura ='" + nombAcce + "') AND (Accesorios_Codigos.planta_id =" + planta.ToString() + ")  AND (Accesorios_Codigos.Des_Aux ='" + descAbre + "') AND( Acc_Anulado=0)";
            return BdDatos.CargarTabla(sql);
        }


        public DataTable Recuperar_IdItemPlanta(string descri,string descort,int planta)
        {
            string sql = "SELECT top (1) item_planta_id FROM item_planta " +
                       " WHERE  descripcion='" + descri + "' AND descripcion_corta='" + descort + "' AND planta_id=" + planta + " " +
                       " ORDER BY item_planta_id desc ";
            return BdDatos.CargarTabla(sql);
        }


        //kp
        public DataTable Validacion_Rango_Medidad_Accerosio(string nombAcce, string descAbre, int planta, float valocampo, float valomin,float valomax)
        {
            string sql= " SELECT top (1) IIf("+valocampo.ToString()+" Between "+valomin.ToString()+" AND "+valomax.ToString()+ " AND " + valocampo.ToString() + " <>0 , 'VERDADERO', 'FALSO')  " +
                        " FROM Accesorios_Codigos " +
                        " WHERE Nomenclatura ='" + nombAcce + "' AND planta_id =" + planta.ToString() + " AND Des_Aux ='" + descAbre + "' ";
            return BdDatos.CargarTabla(sql);
        }

        public DataTable Validacion_Rango_Medidad_Accerosio_Si_Existen_Acc(string nombAcce, string descAbre, int planta, float valocampo, float valomin, float valomax)
        {
            string sql = " SELECT top (1) IIf(" + valocampo.ToString() + " = " + valomin.ToString() + " OR " + valocampo.ToString() + " =  " + valomax.ToString() + ", 'VERDADERO', 'FALSO')  " +
                        " FROM Accesorios_Codigos " +
                        " WHERE Nomenclatura ='" + nombAcce + "' AND planta_id =" + planta.ToString() + " AND Des_Aux ='" + descAbre + "' ";
            return BdDatos.CargarTabla(sql);
        }

        //kp
        public DataTable Validacion_Rango_Medidad_Accerosio2(string nombAcce, string descAbre, int planta, int id_unoe)
        {
            string sql = "SELECT MIN(Valor_1_Min ) AS Valor_1_Min_B, MAX(Valor_1_Min ) AS Valor_1_Min_A,MIN(Valor_1_Max ) AS Valor_1_Max_B , MAX(Valor_1_Max ) AS Valor_1_Max_A ," +
                    " MIN(Valor_2_Min) AS Valor_2_Min_B, MAX(Valor_2_Min)AS Valor_2_Min_A, MIN(Valor_2_Max) AS Valor_2_Max_B, MAX(Valor_2_Max) AS Valor_2_Max_A, " +
                    " MIN(Valor_3_Min) AS Valor_3_Min_B, MAX(Valor_3_Min) AS Valor_3_Min_A, MIN(Valor_3_Max) AS Valor_3_Max_B, MAX(Valor_3_Max) AS Valor_3_Max_A, " +
                    " MIN(Valor_4_Min) AS Valor_4_Min_B, MAX(Valor_4_Min) AS Valor_4_Min_A, MIN(Valor_4_Max) AS Valor_4_Max_B, MAX(Valor_4_Max) AS Valor_4_Max_A, " +
                    " MIN(Valor_5_Min) AS Valor_5_Min_B, MAX(Valor_5_Min) AS Valor_5_Min_A, MIN(Valor_5_Max) AS Valor_5_Max_B, MAX(Valor_5_Max) AS Valor_5_Max_A, " +
                    " MIN(Valor_6_Min) AS Valor_6_Min_B, MAX(Valor_6_Min) AS Valor_6_Min_A, MIN(Valor_6_Max) AS Valor_6_Max_B, MAX(Valor_6_Max) AS Valor_6_Max_A," +
                    " MIN(Valor_7_Min) AS Valor_7_Min_B, MAX(Valor_7_Min) AS Valor_7_Min_A, MIN(Valor_7_Max) AS Valor_7_Max_B, MAX(Valor_7_Max) AS Valor_7_Max_A " +
                    " FROM Accesorios_Codigos " +
                    " WHERE Nomenclatura ='" + nombAcce + "' AND planta_id =" + planta.ToString() + " AND Des_Aux ='" + descAbre + "' AND Id_UnoE <> " + id_unoe.ToString() + " ";
            return BdDatos.CargarTabla(sql);
        }

        public int Anular_Accesorio(int planta, int idAcce, int anulado)
        {
            string sql = "UPDATE Accesorios_Codigos SET Acc_Anulado = " + anulado.ToString() + " " +
                           " WHERE planta_id =" + planta.ToString() + " AND Codigos_Id=" + idAcce.ToString() + "";
            return BdDatos.ejecutarSql(sql);
        }
        //200618
        public DataTable Validar_Existencia_ItemForsa(string descripcion)
        {
            string sql = "SELECT descripcion FROM item WHERE descripcion='" + descripcion + "'";
            return BdDatos.CargarTabla2(sql);
        }
        public DataTable Consultar_CodErp_ItemAnular(int item_planta_id)
        {
            string sql = "SELECT cod_erp FROM item_planta WHERE item_planta_id=" + item_planta_id + "";
            return BdDatos.CargarTabla(sql);
        }
        public DataTable Validacion_Anular__Item_NoEmbalado(int item_planta_id, int planta)
        {
            string sql = "SELECT  DISTINCT Orden_Seg.Id_Seg_Of, Orden_Seg.T_Pry, Orden_Seg.Id_Ofa, Orden_Seg.Id_Emp_Crea, Orden_Seg.Id_Emp_Proy, " +
                                 "isnull(stuff ((SELECT DISTINCT ' * ' + LTRIM(RTRIM(Orden_Seg.Tipo_Of)), ': ' + Orden.Ofa" +
                                                  " FROM Orden_Seg INNER JOIN Orden ON Orden_Seg.Id_Seg_Of = Orden.ordenseg_id INNER JOIN " +
                                                       " Of_Accesorios ON Orden.Id_Ofa = Of_Accesorios.Id_Ofa " +
                                                 " WHERE (Orden_Seg.fecha_despacho IS NULL) AND (YEAR(Orden_Seg.fecha_crea) > 2017) AND (Orden_Seg.Anulado = 0) AND (Orden_Seg.Tipo_Of <> 'OK') AND" +
                                                       " (Orden_Seg.Tipo_Of <> 'OM') AND (Orden_Seg.Tipo_Of <> 'OK') AND (Orden_Seg.Tipo_Of <> 'ID') AND (Orden_Seg.Tipo_Of <> 'RC') AND (Orden_Seg.Tipo_Of <> 'OG')" +
                                                       " AND (Orden_Seg.planta_id = 1) AND (Orden_Seg.Num_Of <> '0') AND (Of_Accesorios.Id_UnoE =" + item_planta_id + ")" +
                                                       " FOR xml path('')), 1, 1, ''), '') AS Orden, " +
                                                       " Orden_Seg.CostoMo, Orden_Seg.CostoMp, Orden_Seg.CostoCif, Orden_Seg.CostoAcc, Orden_Seg.CostoEnv," +
                         " Orden_Seg.CostoTotal, Orden_Seg.Fec_Plan_Mail, Orden_Seg.Fec_Plan_SFac, Orden_Seg.Fec_Plan_Com, Orden_Seg.Fec_Real_Com, Orden_Seg.Fec_Real_Com2," +
                         " Orden_Seg.Fec_Plan_Ing, Orden_Seg.Fec_Real_Ing, Orden_Seg.Fec_Plan_Prod, Orden_Seg.Fec_Real_Prod, Orden_Seg.Fec_Plan_Arm, Orden_Seg.Fec_Real_Arm," +
                         " Orden_Seg.Fec_Plan_Val, Orden_Seg.Fec_Real_Val, Orden_Seg.Fec_Plan_Vis, Orden_Seg.Fec_Real_Vis, Orden_Seg.Fec_Visita, Orden_Seg.No_Cam_Fec," +
                         " Orden_Seg.Obs_seg, Orden_Seg.Pla_Armado, Orden_Seg.Pla_Mod, Orden_Seg.Cumplido, Orden_Seg.Anulado, Orden_Seg.usu_crea, Orden_Seg.fecha_crea," +
                         " Orden_Seg.fecha_crea_of, Orden_Seg.fecha_contractual, Orden_Seg.m2_cotizados, Orden_Seg.und_cotizados, Orden_Seg.fecha_despacho," +
                         " Orden_Seg.Despacho_Id, Orden_Seg.Control_Baliza, Orden_Seg.TD_1EE, Orden_Seg.ND_1EE, Orden_Seg.ND_2EE, Orden_Seg.Mt2_Esti_Ing," +
                         " Orden_Seg.Cont_Form, Orden_Seg.Cont_Acc_Alm, Orden_Seg.Cont_Acc_Fab, Orden_Seg.OP_1EE, Orden_Seg.Item_Op_ParOper, Orden_Seg.Item_Ac_Com," +
                         " Orden_Seg.Item_Ac_Al, Orden_Seg.OP_Ac_Com, Orden_Seg.OP_Ac_Al, Orden_Seg.Unidad_Neg_Tipos, Orden_Seg.Hibrido, Orden_Seg.enc_entrada_cot_id," +
                         " Orden_Seg.Item_1EE_Abuelo, Orden_Seg.OP_1EE_Abuelo, Orden_Seg.PVC_1EE, Orden_Seg.Para_Tipo_Id, Orden_Seg.embalado_formaleta," +
                         " Orden_Seg.embalado_accesorio, Orden_Seg.planta_id, Orden_Seg.Avance_Ing, Orden_Seg.sf_id, Orden_Seg.fup, Orden_Seg.vers, Orden_Seg.tipo_pv_id," +
                         " Orden_Seg.Tip_Ord_Abu, Orden_Seg.Tip_Ord_Pap_Al, Orden_Seg.Tip_Ord_Pap_Com, Orden_Seg.Tip_Ord_Pap_Alum, Orden_Seg.Mod_Ver," +
                         " Of_Accesorios.Id_UnoE" +
" FROM            Orden_Seg INNER JOIN" +
                         " Orden ON Orden_Seg.Id_Seg_Of = Orden.ordenseg_id INNER JOIN" +
                         " Of_Accesorios ON Orden.Id_Ofa = Of_Accesorios.Id_Ofa" +
" WHERE        (Orden_Seg.fecha_despacho IS NULL) AND (YEAR(Orden_Seg.fecha_crea) > 2017) AND (Orden_Seg.Anulado = 0) AND (Orden_Seg.Tipo_Of <> 'OK') AND" +
                         " (Orden_Seg.Tipo_Of <> 'OM') AND (Orden_Seg.Tipo_Of <> 'OK') AND (Orden_Seg.Tipo_Of <> 'ID') AND (Orden_Seg.Tipo_Of <> 'RC') AND (Orden_Seg.Tipo_Of <> 'OG')" +
                         " AND (Orden_Seg.planta_id =" + planta + ") AND (Orden_Seg.Num_Of <> '0') AND (Of_Accesorios.Id_UnoE =" + item_planta_id + ")";
            return BdDatos.CargarTabla(sql);
        }
        //LLAMAMOS EL PROCEDIMIENTO PARA CREAR ITEM EN BRASIL
        public int CrearItemBrasil(int item_planta_id)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            // Parametros de la BBDD              
            SqlParameter[] sqls = new SqlParameter[1];

            sqls[0] = new SqlParameter("Item_Planta_Id", item_planta_id);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarItemBrasil", con))
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

        public int RegistraError_Envio_WebServices(string origen, string detalle)
        {
            string sql = " INSERT INTO LOGSIIF  (Origen,Usuario,Fecha,Detalle ,Sistema) " +
                                        " VALUES ( '" + origen + "' ,'Sistemas1', SYSDATETIME(),'" + detalle + "','SIO')";
            return BdDatos.ejecutarSql(sql);
        }

    }
}
