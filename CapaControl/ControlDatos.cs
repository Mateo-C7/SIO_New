using CapaControl.Entity;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CapaControl
{
    public class ControlDatos
    {
        public DataTable getTipoSolicitud()
        {
            DataTable dt = null;
            string sql = "EXEC getTipoSolicitud";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public DataTable getPlanta()
        {
            DataTable dt = null;
            string sql = "EXEC getPlanta";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public DataTable getOrdenesTipoSolicitud(string tipoSol, int planta)
        {
            DataTable dt = null;
            string sql = "EXEC getOrdenesTipoSolicitud '" + tipoSol + "'," + planta;
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public DataTable getRayaOrden(string tipoSol, int idOfaPadre)
        {
            DataTable dt = null;
            string sql = "EXEC getRayaOrden '" + tipoSol + "'," + idOfaPadre;
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public DataTable getFamiliaOrdenRaya(int idOfaHijo, int idOfaPadre)
        {
            DataTable dt = null;
            string sql = "EXEC getFamiliaOrdenRaya " + idOfaHijo + ',' + idOfaPadre;
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        // consulta a la bd para cargar los items de la grilla
        public DataTable getItemsPlaneador(int idOfaHijo, string filtro, string kambam, int idEstado)
        {
            DataTable dt = new DataTable();
            string sql = "";
            string filtroKambam = "";
            if (kambam == "0")
            {
                filtroKambam = "   AND(CONVERT(varchar, Saldos.Tipo + ' ' + CONVERT(varchar, Saldos.Anc1) + 'x' + " +
                    "       CONVERT(varchar, Saldos.alto1) + 'x' + CONVERT(varchar, Saldos.Alto2)" +
                         "   + 'x' + CONVERT(varchar, Saldos.Anc2)) in " +
                             "  (Select  CONVERT(varchar, T1.Tipo + ' ' + CONVERT(varchar, T1.Anc1) + 'x' + " +
                             "  CONVERT(varchar, T1.alto1) + 'x' + CONVERT(varchar, T1.Alto2)" +
                             "  + 'x' + CONVERT(varchar, T1.Anc2))" +
                            "   From saldos  T1" +
                            "   Left Outer Join" +
                             "  Kanbam  T2" +

                            "   ON   CONVERT(varchar, T1.Tipo + ' ' + CONVERT(varchar, T1.Anc1) + 'x' + " +
                            "   CONVERT(varchar, T1.alto1) + 'x' + CONVERT(varchar, T1.Alto2)" +
                            "   + 'x' + CONVERT(varchar, T1.Anc2)) = (T2.[Desc] + ' ' + T2.MedX)" +

                             "  where T2.[Desc] is null" +

                             "  and T1.Id_Ofa = " + idOfaHijo + "))";
            }

            //si estado es nuevo o guardado se muestran todos los items
            if (idEstado == 0 || idEstado == 1)
            {
                sql = "SELECT        Saldos.Id_Ofa, Saldos.GNo, Saldos.Grupo AS familia, Saldos.Cant_Final_Req AS cantidad, Saldos.item as item, " +
                      " Saldos.Identificador, Orden.Ofa, Orden.Id_Of_P, " +
                      " CONVERT(varchar, Saldos.Tipo) + ' ' + CONVERT(varchar, Saldos.Anc1) " +
                      " + 'x' + CONVERT(varchar, Saldos.alto1) AS nombre, Saldos.Id_Piezas_Forsa AS idPieza, Saldos.Explosion " +
                      " FROM            Saldos INNER JOIN " +
                      " Orden ON Saldos.Id_Ofa = Orden.Id_Ofa " +
                      " WHERE(Orden.Id_Ofa = " + idOfaHijo + ") AND(Orden.Anulada = 0) AND(Saldos.Anula = 0) " +
                      filtroKambam + filtro +
                          " ORDER BY Saldos.item, Saldos.Identificador DESC";
                dt = BdDatos.CargarTabla(sql);
            }

            else
            {
                //Consultamos si existe en explo mp
                sql = " SELECT  DISTINCT Saldos.Id_Ofa, Saldos.Gno, Saldos.Grupo as familia, Saldos.Cant_Final_Req as cantidad,Saldos.item as item, Saldos.Identificador, " +
                      " Orden.Ofa, Orden.Id_Of_P, PF.[Desc] + ' ' + CONVERT(varchar, Saldos.Anc1) + 'x' + " +
                      " CONVERT(varchar, Saldos.alto1) AS nombre, Saldos.Id_Piezas_Forsa as idPieza, Saldos.Explosion " +
                      " FROM Saldos INNER JOIN " +
                      " Orden ON Saldos.Id_Ofa = Orden.Id_Ofa INNER JOIN " +
                      " Piezas_Forsa AS PF ON PF.Id_Piezas = Saldos.Id_Piezas_Forsa INNER JOIN " +
                      " Explo_Mp ON Orden.Id_Ofa = Explo_Mp.Explo_OfaId AND Saldos.Identificador = Explo_Mp.Explo_SalId " +
                      " WHERE (Orden.Id_Ofa = " + idOfaHijo + ") AND(Orden.Anulada = 0) " + filtro + filtroKambam +
                      " AND saldos.Anula = 0 " +
                        " ORDER BY Saldos.item, Saldos.Identificador DESC";
                dt = BdDatos.CargarTabla(sql);

                if (dt.Rows.Count == 0)
                {
                    //Si no existe buscamos en saldos
                    sql = "SELECT        Saldos.Id_Ofa, Saldos.GNo, Saldos.Grupo AS familia, Saldos.Cant_Final_Req AS cantidad, Saldos.item as item, " +
                          " Saldos.Identificador, Orden.Ofa, Orden.Id_Of_P, " +
                          " CONVERT(varchar, Saldos.Tipo) + ' ' + CONVERT(varchar, Saldos.Anc1) " +
                          " + 'x' + CONVERT(varchar, Saldos.alto1) AS nombre, Saldos.Id_Piezas_Forsa AS idPieza, Saldos.Explosion " +
                          " FROM            Saldos INNER JOIN " +
                          " Orden ON Saldos.Id_Ofa = Orden.Id_Ofa " +
                          " WHERE(Orden.Id_Ofa = " + idOfaHijo + ") AND(Orden.Anulada = 0) AND(Saldos.Anula = 0) " +
                          filtroKambam + filtro +
                          " ORDER BY Saldos.item, Saldos.Identificador DESC";
                    dt = BdDatos.CargarTabla(sql);
                }
            }
            return dt;
        }

        public DataTable getMateriaPrimaItems(int idOfaHijo, string filtro, string kambam)
        {
            DataTable dt = new DataTable();
            string sql = "";

            string filtroKambam = "";
            if (kambam == "0")
            {
                filtroKambam = "   AND(CONVERT(varchar, Saldos.Tipo + ' ' + CONVERT(varchar, Saldos.Anc1) + 'x' + " +
               "       CONVERT(varchar, Saldos.alto1) + 'x' + CONVERT(varchar, Saldos.Alto2)" +
                    "   + 'x' + CONVERT(varchar, Saldos.Anc2)) in " +
                        "  (Select  CONVERT(varchar, T1.Tipo + ' ' + CONVERT(varchar, T1.Anc1) + 'x' + " +
                        "  CONVERT(varchar, T1.alto1) + 'x' + CONVERT(varchar, T1.Alto2)" +
                        "  + 'x' + CONVERT(varchar, T1.Anc2))" +
                       "   From saldos  T1" +
                       "   Left Outer Join" +
                        "  Kanbam  T2" +

                       "   ON   CONVERT(varchar, T1.Tipo + ' ' + CONVERT(varchar, T1.Anc1) + 'x' + " +
                       "   CONVERT(varchar, T1.alto1) + 'x' + CONVERT(varchar, T1.Alto2)" +
                       "   + 'x' + CONVERT(varchar, T1.Anc2)) = (T2.[Desc] + ' ' + T2.MedX)" +

                        "  where T2.[Desc] is null" +

                        "  and T1.Id_Ofa = " + idOfaHijo + "))";
            }


            //Consultamos primero en explo mp
            sql = " SELECT        Saldos.Ofa, Explo_Mp.Explo_OfaId AS id_Ofa, Saldos.Grupo, Saldos.Identificador, Saldos.Grupo + ' ' + CONVERT(varchar, Explo_Mp.Explo_Med1) + 'x' + CONVERT(varchar, Explo_Mp.Explo_Med2) AS nombre, " +
                  " (CASE WHEN Materia_Prima.Mp_Desc IS NULL THEN Explo_Mp_Desc + ' XX' WHEN Materia_Prima.Id_Mp = 0 THEN Explo_Mp_Desc + ' XX' ELSE Materia_Prima.Mp_Desc END) AS materiaPrima, " +
                  " Explo_Mp.Explo_Cant AS cantidad,(CASE WHEN Explo_Mp.Explo_Med3 <> 0 AND Explo_Mp.Explo_Med3 IS NOT NULL THEN CONVERT(varchar, Explo_Mp.Explo_Med1) +'x' + CONVERT(varchar, Explo_Mp.Explo_Med2) + 'x' + CONVERT(varchar, Explo_Mp.Explo_Med3) ELSE " +
                  " CASE WHEN Explo_Mp.Explo_Med2 <> 0 AND Explo_Mp.Explo_Med2 IS NOT NULL THEN CONVERT(varchar, Explo_Mp.Explo_Med1) +'x' +CONVERT(varchar, Explo_Mp.Explo_Med2) ELSE CONVERT(varchar, Explo_Mp.Explo_Med1) END END )  AS medidas, Explo_Mp.Explo_CanKit, Explo_Mp.Id_Explo, Explo_Mp.Explo_MpId,0 as metodo " +
                  " FROM            Explo_Mp INNER JOIN " +
                  " Saldos ON Explo_Mp.Explo_SalId = Saldos.Identificador INNER JOIN " +
                  " Orden ON Saldos.Id_Ofa = Orden.Id_Ofa LEFT OUTER JOIN " +
                  " Materia_Prima ON Explo_Mp.Explo_MpId = Materia_Prima.Mp_Cod_UnoEE " +
                  " WHERE(Explo_Mp.Explo_OfaId = " + idOfaHijo + ") AND(Saldos.Anula = 0) " + filtro + filtroKambam +
         " ORDER BY Saldos.item, Saldos.Identificador DESC";
            dt = BdDatos.CargarTabla(sql);
            if (dt.Rows.Count == 0)
            {             
                //Si no existe en explo mp consultamos en inventor
                sql = "SELECT        Orden.Ofa, Orden.Id_Ofa, Saldos.Grupo, Saldos.Identificador, " +
                      " Formaleta_Lib_Inv.Formaleta_Lib_Inv_Nom + ' ' + CONVERT(varchar, Formaleta_Lib_Inv.Formaleta_Lib_Inv_Med1) +" +
                      " 'x' + CONVERT(varchar, Formaleta_Lib_Inv.Formaleta_Lib_Inv_Med2) AS nombre, " +
                      " Explo_Lib_Inv.Explo_Lib_Desc_Mp AS materiaPrima, Explo_Lib_Inv.Explo_Lib_Cant_Kit * Saldos.Cant_Final_Req AS cantidad, " +
                      " (CASE WHEN Explo_Lib_Inv.Explo_Lib_Med2 <> 0 AND Explo_Lib_Inv.Explo_Lib_Med2 IS NOT NULL " +
                      " THEN CONVERT(varchar, Explo_Lib_Inv.Explo_Lib_Med1) +'x' + CONVERT(varchar, " +
                      " Explo_Lib_Inv.Explo_Lib_Med2) ELSE CONVERT(varchar, Explo_Lib_Inv.Explo_Lib_Med1) END) AS medidas,isnull( Explo_Saldos.Explo_Sal_Metodo,1) as metodo " +
                      " FROM Explo_Lib_Inv INNER JOIN " +
                      " Formaleta_Lib_Inv ON Explo_Lib_Inv.Formaleta_Lib_Inv_Id = Formaleta_Lib_Inv.Formaleta_Lib_Inv_Id " +
                      " LEFT OUTER JOIN Orden INNER JOIN Saldos ON Orden.Id_Ofa = Saldos.Id_Ofa LEFT OUTER JOIN " +
                      " Explo_Saldos ON Saldos.Identificador = Explo_Saldos.Saldos_Id ON Formaleta_Lib_Inv.Formaleta_Lib_Inv_Id = " +
                      " Explo_Saldos.Explo_Lib_Id " +
                      " WHERE(Orden.Id_Ofa = " + idOfaHijo + ") AND(Orden.Anulada = 0) AND(Saldos.Anula = 0) " +
                      filtroKambam + filtro +
                   " ORDER BY Saldos.item, Saldos.Identificador DESC";
                dt = BdDatos.CargarTabla(sql);
            }
            return dt;
        }

        public DataTable getMateriaPrimaCombo(int plantaId)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC getMateriaPrima " + plantaId.ToString();
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public DataTable getInventarioMateriaPrima(string desc_mp, int plantaId)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC getMateriaPrimaInventario '" + desc_mp + "',"+plantaId+" ";
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public int insertLogExploPrincipal(int id_Ofa, int id_estado_explosionador, string usuario_crea, int id_familia)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[4];
            sqls[0] = new SqlParameter("id_ofa", id_Ofa);
            sqls[1] = new SqlParameter("id_estado_explosionador", id_estado_explosionador);
            sqls[2] = new SqlParameter("usuario_crea", usuario_crea);
            sqls[3] = new SqlParameter("id_familia", id_familia);
            

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertExploMpPrincipal", con))
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

        public void insertLogExploMp(int id_explo_mp_principal, int id_Estado_explosionador, string usuario)
        {
            string sql = "EXEC InsertLogExploMp " + id_explo_mp_principal + ", " + id_Estado_explosionador + ", '" + usuario + "'";
            BdDatos.Actualizar(sql);
        }

        public void exectQuery(string sql)
        {
            BdDatos.Actualizar(sql);
        }

        public void updateEstadoExplo(int idExploMpPrincipal, int estado, string usuario)
        {
            string sql = "EXEC updateEstadoExploMp " + idExploMpPrincipal + "," + estado + ", '" + usuario + "'";
            BdDatos.Actualizar(sql);
        }

        public void actualizaExploSugiere(int idOfa)
        {
            string sql = "EXEC USP_Simulador_SugiereFabricacion " + idOfa + " ;" +
                         " UPDATE orden SET ExploWebSugiere = 1 WHERE (Id_Ofa = " + idOfa + ") ;";
            BdDatos.Actualizar(sql);
        }

        public int getIdExploPrincipal(int idOfa,int idFamilia)
        {
            int id = 0;
            DataTable dt = new DataTable();
            string sql = "EXEC getIdExploPrincipal " + idOfa.ToString() + "," + idFamilia.ToString();
            dt = BdDatos.CargarTabla(sql);

            if (dt.Rows.Count > 0)
            {
                id = Convert.ToInt32(dt.Rows[0]["id_explo_mp_principal"]);
            }
            return id;
        }

        public bool consultaExplosugiereRaya(int idOfa)
        {
            bool id = false;
            DataTable dt = new DataTable();
            string sql = "SELECT ISNULL(ExploWebSugiere, 0) AS ExploWebSugiere FROM  Orden WHERE (Id_Ofa = " + idOfa.ToString() + ")";
            dt = BdDatos.CargarTabla(sql);

            if (dt.Rows.Count > 0)
            {
                id = Convert.ToBoolean(dt.Rows[0]["ExploWebSugiere"]);
            }
            return id;
        }

        public DataTable getEstadoExplosionador(int idOfa, int idFamilia)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC getEstadoExploPrincipal " + idOfa.ToString() + "," + idFamilia.ToString();
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public DataTable getParametrosMP(string codigo, int idPlanta)
        {
            string sql = "EXEC getParametrosMateriaPrima " + codigo + "," + idPlanta; 
            DataTable dt = new DataTable();
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        public int validarCodigoMateriaPrima(string materiaPrima, string codigo)
        {
            int result = -1;
            DataTable dt = new DataTable();
            //string sql = " SELECT        Id_Mp, Mp_Nombre " +
            //             " FROM Materia_Prima " +
            //             " WHERE(Id_Mp = " + codigo + ") AND(Mp_InActivo = 0) AND(Mp_Desc = '" + materiaPrima + "')";

            string sql = " SELECT Mp_Cod_UnoEE, Mp_Nombre " +
                        " FROM Materia_Prima " +
                        " WHERE(Mp_Cod_UnoEE= " + codigo + ") AND(Mp_InActivo = 0) AND(Mp_Desc = '" + materiaPrima + "') AND Mp_Cod_UnoEE <> 0";

            dt = BdDatos.CargarTabla(sql);
            if (dt.Rows.Count > 0)
            {
                result = 0;
            }
            return result;
        }

     
        public int getConsecutivoSolMp()
        {
            int consecutivo = 0;
            string sql = "SELECT Sol_Mp + 1 AS consecutivo FROM consecutivo";
            DataTable dt = new DataTable();
            dt = BdDatos.CargarTabla(sql);
            if (dt.Rows.Count > 0)
            {
                consecutivo = Convert.ToInt32(dt.Rows[0]["consecutivo"]);
            }
            return consecutivo;
        }

        public int getIdMpSol()
        {
            int id = 0;
            string sql = "SELECT TOP(1) Mp_Sol_Id + 1 as id FROM Mp_Sol ORDER BY Mp_Sol_Id DESC";
            DataTable dt = new DataTable();
            dt = BdDatos.CargarTabla(sql);
            if (dt.Rows.Count > 0)
            {
                id = Convert.ToInt32(dt.Rows[0]["id"]);
            }
            return id;
        }

        public void updateConsecutivo(int consecutivo)
        {
            string sql = "UPDATE consecutivo SET Sol_Mp = " + consecutivo;
            BdDatos.Actualizar(sql);
        }

        public DataTable getListaSolMp(int idOfa, int id_familia)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC getListaSolMp " + idOfa + "," + id_familia;
            dt = BdDatos.CargarTabla(sql);
            return dt;
        }

        //Valida que todos los saldos del IdOfa esten explosionados para poder confirmar la raya
        public DataTable ValidarSaldosExplosionados(int idOfa)
        {
            String sql = "SELECT Saldos.Explo_Inventor " +
                         " FROM Saldos" +
                        " WHERE(Saldos.Id_Ofa = " + idOfa + ") AND(Saldos.Anula = 0) AND(Saldos.Explo_Inventor = 0)";
            return BdDatos.CargarTabla(sql);            
        }

        public static List<planos> ObtenerArchivos(int idfup, int idtipo)
        {
            string consulta = @"SELECT      id_plano , plano_nombre_real, id_fup_plano, plano_ruta_archivo, plano_descripcion, 
			                                                fecha_crea, usu_crea, plano_tipo_anexo_id
                                                FROM            Plano
                                                where id_fup_plano = @idfup and plano_tipo_anexo_id = @tipo";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@idfup", idfup);
            parametros.Add("@tipo", idtipo);
            List<planos> listaplanos = ControlDatos.EjecutarConsulta<planos>(consulta, parametros);
            return listaplanos;
        }

        public static void BorrarArchivos(int idfup, int idplano, string usuario)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", idfup);
            parametros.Add("@pIdPlano", idplano);
            parametros.Add("@pUsuario", usuario);
            BdDatos.EjecutarStoreProcedureConParametros("USP_fup_DEL_Anexo", parametros);
        }

        public static void GuardarAnexo(int idfup, string nombre_archivo, string nombre_ruta, int idtipo, string usuario, string Version, string descripcion, int idtipoEvento)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", idfup);
            parametros.Add("@pVersion", Version);
            parametros.Add("@pTipoAnexo", idtipo);
            parametros.Add("@pNombrePlano", nombre_archivo);
            parametros.Add("@pRuta", nombre_ruta);
            parametros.Add("@pDescripcion", descripcion);
            parametros.Add("@pusuario", usuario);
            parametros.Add("@pEventoId", idtipoEvento);
            BdDatos.EjecutarStoreProcedureConParametros("USP_fup_UPD_Anexo", parametros);
        }

        public static void GuardarDebugLog(string modulo, string mensaje, string usuario)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pModulo", modulo);
            parametros.Add("@pMensaje", mensaje);
            parametros.Add("@pUsuario", usuario);
            BdDatos.EjecutarStoreProcedureConParametros("USP_fup_Debug_App", parametros);
        }


        public static int GuardarStoreProcedureConParametros(string storeprocedure, Dictionary<string, object> parametros)
        {
            int id = 0;
            id = BdDatos.GuardarStoreProcedureConParametros(storeprocedure, parametros);
            return id;
        }

        public static void GuardarPlano(int idfup, string nombre_archivo, string nombre_ruta, int idtipo, string usuario, string Version, string descripcion)
        {
            string consulta = @"INSERT INTO Plano ( plano_nombre_real, id_fup_plano, plano_ruta_archivo, plano_descripcion, fecha_crea, usu_crea, plano_tipo_anexo_id, plano_version)
                                VALUES        (@nombre_real,@idfup,@ruta_archivo,@descripcion,getDate(),@usuario,@tipo_anexo, @Version)";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@nombre_real", nombre_archivo);
            parametros.Add("@idfup", idfup);
            parametros.Add("@ruta_archivo", nombre_ruta);
            parametros.Add("@descripcion", descripcion);
            parametros.Add("@usuario", usuario);
            parametros.Add("@tipo_anexo", idtipo);
            parametros.Add("@Version", Version);

            BdDatos.GuardarConsultaConParametros(consulta, parametros);
        }

        public static List<T> EjecutarStoreProcedureConParametros<T>(string storeprocedure, Dictionary<string, object> parametros) where T : new()
        {
            return ConvertToEntity<T>(BdDatos.EjecutarStoreProcedureConParametros(storeprocedure, parametros));
        }

        public static List<T> EjecutarConsulta<T>(string consulta, Dictionary<string, object> parametros) where T : new()
        {
            return ConvertToEntity<T>(BdDatos.EjecutarConsultaConParametros(consulta, parametros));
        }

        public static List<T> ConvertToEntity<T>(DataTable table) where T : new()
        {
            List<T> list = new List<T>();
            foreach (DataRow tableRow in table.Rows)
            {
                Type t = typeof(T);
                T returnObject = new T();

                foreach (DataColumn col in tableRow.Table.Columns)
                {
                    string colName = col.ColumnName;

                    PropertyInfo pInfo = t.GetProperty(colName.ToLower(),
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    bool IsNullable = false;

                    if (pInfo != null)
                        IsNullable = (Nullable.GetUnderlyingType(pInfo.PropertyType) != null);

                    if (pInfo != null)
                    {
                        object val = tableRow[colName];

                        if (IsNullable)
                        {

                            if (val is System.DBNull)
                            {
                                val = null;
                            }
                            //else
                            //{
                            //    val = Convert.ChangeType(val, pInfo.PropertyType);
                            //}
                            pInfo.SetValue(returnObject, val, null);
                        }
                        else
                        {
                            try
                            {
                                // Convert the db type into the type of the property in our entity
                                val = Convert.ChangeType(val, pInfo.PropertyType);
                            }
                            catch { }
                        }
                        pInfo.SetValue(returnObject, val, null);
                    }

                }
                list.Add(returnObject);
            }
            return list;

        }
    }
}

