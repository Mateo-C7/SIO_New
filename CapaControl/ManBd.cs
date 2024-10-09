using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADODB;
using System.Data;
using System.Data.SqlClient;
namespace CapaControl
{
    public class ManBd
    {
        private static SqlConnection sqlConexion;
        private static string strConSql;
      
       public static Boolean ConectarSql()
       {
           Boolean objBool;
           objBool = false;
            strConSql = @"Password=forsa2006;Persist Security Info=True;User ID=forsa;Initial Catalog=Forsa;Data Source=172.21.0.5";
            try
           {
               sqlConexion = new SqlConnection();

               if (sqlConexion.State == 0)
               {
                   sqlConexion.ConnectionString = strConSql;
                   sqlConexion.Open();
               }
               objBool = true;
           }
           catch (Exception ex)
           {
               
               Console.WriteLine("In Main catch block. Caught: {0}", ex.Message);
               Console.WriteLine("Inner Exception is {0}", ex.InnerException);
           }


           return objBool;
       }

       public static void DescSql()
       {
           try
           {
               sqlConexion.Close();
            }
           catch (Exception e)
           {
               Console.Write(e.Message);
           }

       }

       public static SqlDataReader CargarReader(String sql)
       {

           SqlDataReader dataRead = null;

           ConectarSql();
           SqlCommand sqlComando = new SqlCommand(sql, sqlConexion);
           dataRead = sqlComando.ExecuteReader();

           return dataRead;

       }

       public static DataTable CargarTabla(String sql)
       {

           DataTable tabla = new DataTable();
           SqlDataAdapter adapter = new SqlDataAdapter();
           
           ConectarSql();
           SqlCommand sqlComando = new SqlCommand(sql, sqlConexion);
           adapter.SelectCommand = sqlComando;
           adapter.FillSchema(tabla, SchemaType.Source);
           try { adapter.Fill(tabla); }
           catch { }
           DescSql();
           return tabla;

       }

        public static int InsertaDatos(String Datos, String Tabla, String Valores)
        {
            int numRow  = -1;
            try {
                String strConsulta;
                strConsulta = "INSERT INTO " + Tabla + " (" + Datos + ") VALUES (" + Valores + ")";
               
                ConectarSql();
                SqlCommand sqlComando = new SqlCommand(strConsulta, sqlConexion);
                numRow = sqlComando.ExecuteNonQuery();
            }
            catch { }
                DescSql();
           
            return numRow;
           
        }


        public static int MaximoID(String sql)
        {
            int numGrup = -1;
            try {
                DataTable tabla = new DataTable();
               
                ConectarSql();
                SqlCommand sqlComando = new SqlCommand(sql, sqlConexion);
                SqlDataReader reader = sqlComando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        numGrup= reader.GetInt32(0);
                    }
                }
                
                reader.Close();

            }
            catch { }
                DescSql();

                return numGrup;

        }


        public static DataTable ActualizaTabla(String sql)
        {

            DataTable tabla = new DataTable();
            ConectarSql();
           
            try {
                SqlCommand sqlComando = new SqlCommand(sql, sqlConexion);
                int numRec = sqlComando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }
            DescSql();
            return tabla;

        }

        public static DataTable ActualizaItemKanban(DataTable dt, object[] arItem)
        {
            DataTable actTabla = dt;

            try
            {              
                string strCons;
                string strIni="";
                string strMed="";
                
                strIni = "SELECT CIA, ITEM_KANBAN, CODIGO_MP, UND, REQUERIDA, MATERIA_PRIMA, FACTOR, "
                        + "GRP_EQUI_ID, PCZ_UND FROM VPZ_COMPO_KANBAN WHERE (CIA = 6) AND (UND > N'0') AND (";


                foreach (String[] arDatKan in arItem)
                {
                     String codItem = arDatKan[0];
                    if (strMed == "")
                    {
                        strMed = "(ITEM_KANBAN = " + codItem + " )";
                    }
                    else
                    {
                        strMed = strMed + " OR (ITEM_KANBAN = " + codItem + " )";
                    }

                    strCons = "SELECT CIA, ITEM_KANBAN, CODIGO_MP, UND, REQUERIDA, MATERIA_PRIMA, 1 / FACTOR AS FACTOR, "
                            + "GRP_EQUI_ID, PCZ_UND FROM VPZ_COMPO_KANBAN  WHERE (CIA = 6) AND (UND > N'0') AND "
                            + "(ITEM_KANBAN = " + codItem + " )";
                }

                strCons = strIni + strMed + ")";

                    DataTable tabla = CargarTabla(strCons);

                    if (tabla.Rows.Count > 0)
                    {
                        int i = 0;

                        string oldCod = "";
                        decimal oldCant = 1;

                        while (i < tabla.Rows.Count)
                        {
                            DataRow oldRow = tabla.Rows[i];
                            DataRow row = actTabla.NewRow();
                            decimal cantReq = decimal.Round(Convert.ToDecimal(oldRow["REQUERIDA"]), 2);
                            decimal valfactor = decimal.Round(Convert.ToDecimal(oldRow["FACTOR"]), 2);
                        
                            row["UND"] = oldRow["UND"];
                            row["COD_ITEM_MP"] = oldRow["CODIGO_MP"];
                            string newCod = Convert.ToString(oldRow["ITEM_KANBAN"]);
                            
                            if (newCod != oldCod)
                            {
                                foreach (String[] arDatKan in arItem)
                                {
                                    String codItem = arDatKan[0];
                                    if (newCod == codItem) 
                                    {
                                        oldCant = Convert.ToDecimal(arDatKan[1]);
                                        oldCod = codItem;
                           
                                    }
                                }
                            }

                            row["CANT_MP"] = decimal.Round(oldCant * cantReq);
                            row["DES_ITEM_MP"] = oldRow["MATERIA_PRIMA"];
                            row["FACTOR"] = valfactor;
                            row["ES_KANBAN"] = "NO";
                            row["GRP_EQUI_ID"] = oldRow["GRP_EQUI_ID"];
                            row["UND_PZ"] = oldRow["PCZ_UND"];
                            row["CANT_P"] = decimal.Round((oldCant * cantReq) / valfactor, 2);
                            
                            string strDesc;
                            strDesc = Convert.ToString(row["DES_ITEM_MP"]);
                            strDesc = strDesc.ToUpper();
                            if (!strDesc.Contains("-PN"))
                            {
                                actTabla.Rows.Add(row);
                            }
                           
                            i++;
                        }

                    }
               
                DescSql();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return actTabla;
        }
    }
}
