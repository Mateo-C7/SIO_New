using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ADODB;
using CapaDatos;

namespace CapaControl
{
    public class Consulta
    {

        public static DataTable ConsultaTablaCompItem(DataTable tblEstItem)
        {
            DataTable datoTabla = null;
            try
            {
                String strCons;
                String numOrden = "";
               
                DataRow row = tblEstItem.Rows[0];
                numOrden = Convert.ToString(row["NO_IF"]);

                strCons = "SELECT derivedtbl_1.UND, derivedtbl_1.CANT_MP, derivedtbl_1.COD_ITEM_MP, derivedtbl_1.UND_INV, " 
                           + "derivedtbl_1.FACTOR, derivedtbl_1.DESCRIPCION, VPZ_CARACTERISTICAS_ITEMS.ES_KANBAN, "
                           + "VPZ_CARACTERISTICAS_ITEMS.GRP_EQUI_ID, VPZ_CARACTERISTICAS_ITEMS.CONTROL "
                           + "FROM OPENQUERY(FORSA1E, 'select * from VPZ_CONSUMOS_PICIZ') AS derivedtbl_1 INNER JOIN "
                           + "VPZ_CARACTERISTICAS_ITEMS ON derivedtbl_1.COD_ITEM_MP = VPZ_CARACTERISTICAS_ITEMS.ITEM "
                           + "WHERE (derivedtbl_1.CIA = 6) AND (derivedtbl_1.C_O = N'001') AND (derivedtbl_1.T_DOC_OP = N'IF') "
                           + "AND (derivedtbl_1.NO_DOC_OP =" + numOrden + ") AND (derivedtbl_1.CANT_MP > N'0')";

                 ManBd objBD = new ManBd();
                
                 SqlDataReader dataRead = ManBd.CargarReader(strCons);

                 if (dataRead != null)
                 {
                     datoTabla = FiltrarItemKanban(dataRead);
                 }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return datoTabla;
        }

      public static DataTable ConsultaOrdenMP(string valItem)
      {

          String strCons;

          strCons = "SELECT"
                    + " OP_1EE_Abuelo AS NO_IF"
                    + " FROM "
                    + " Orden_Seg"
                    + " WHERE"
                    + " (RTRIM(Tipo_Of) + ' ' + RTRIM(Num_Of) + '-' + RTRIM(Ano_Of)= '" + valItem + "')";
                  

          ManBd objBD = new ManBd();
          DataTable datoTabla = ManBd.CargarTabla(strCons);

        if (datoTabla != null)
          {
              datoTabla = ConsultaTablaCompItem(datoTabla);
          }
          return datoTabla;
        }

      public static DataTable ConsultaTablaEstOrden(string valItem)
      {

          String strCons;

          strCons = "SELECT"
                    + " T_DOC_OP, DES_ITEM, NO_DOC_OP, ID_ESTADO"
                    + " FROM "
                    + " VPZ_ESTRUCTURA_ORDEN_ERP"
                    + " WHERE"
                    + " (T_DOC_OP <> N'IF') AND (DES_ITEM LIKE N'" + valItem + "%') AND (ID_ESTADO <> 9)"
                    + " ORDER BY T_DOC_OP";

          ManBd objBD = new ManBd();
          DataTable datoTabla = ManBd.CargarTabla(strCons);

          if (datoTabla != null)
          {
              datoTabla = FiltrarTablaEstOrden(datoTabla);
          }
          return datoTabla;
      }

        public static DataTable ConsultaItemEquiv(string codItem)
        {
            DataTable datoTabla = null;
            try
            {
                String strCons;
               
                strCons = "SELECT PZ_ITEM_EQUI_ORI, PZ_ITEM_EQUI_PICIZ, PZ_ITEM_EQUI_MED1, "
                          + "PZ_ITEM_EQUI_MED2 FROM PZ_ITEM_EQUI WHERE "
                          + "(PZ_GRP_EQUI_ID = " + codItem + ") AND (PZ_ITEM_EQUI_ACTIVO = 1) ";

                ManBd objBD = new ManBd();
                datoTabla = ManBd.CargarTabla(strCons);

              }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return datoTabla;
        }

        public static DataTable ConsultaExisteItemEquiv(String itEquiv, string itOrig, string itMed1, string itMed2, int idGrup)
        {
            DataTable datoTabla = null;
            try
            {
                String strCons;

                strCons = "SELECT * FROM PZ_ITEM_EQUI WHERE "
                          + "(PZ_ITEM_EQUI_ORI = '" + itOrig + "') AND (PZ_ITEM_EQUI_PICIZ = '" + itEquiv + "') "
                          + "AND (PZ_ITEM_EQUI_MED1 = " + itMed1 + ") AND (PZ_ITEM_EQUI_MED2 = " + itMed2 + ") "
                          + "AND (PZ_GRP_EQUI_ID = " + idGrup + ")";

                ManBd objBD = new ManBd();
                datoTabla = ManBd.CargarTabla(strCons);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return datoTabla;
        }

        public static DataTable ConsultaTodoItemEquiv(string codItem)
        {
            DataTable datoTabla = null;
            try
            {
                String strCons;

                strCons = "SELECT * FROM PZ_ITEM_EQUI WHERE "
                          + "(PZ_GRP_EQUI_ID = " + codItem + ") ";

                ManBd objBD = new ManBd();
                datoTabla = ManBd.CargarTabla(strCons);
                 
                int numRow;
                int i = 0;
                numRow = datoTabla.Rows.Count;

                datoTabla.Columns.Add("ESTADO");
               
                if (numRow > 0)
                {
                    while (i < numRow)
                    {
                        int isAct;
                        
                        DataRow row = datoTabla.Rows[i];
                        isAct = Convert.ToInt32(row["PZ_ITEM_EQUI_ACTIVO"]);
                        if (isAct==1)
                        {
                            row["ESTADO"] = "SI";
                        }
                        else
                        {
                            row["ESTADO"] = "NO";
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return datoTabla;
        }

        public static DataTable ConsultaGrupoEquiv()
        {
            DataTable datoTabla = null;
            try
            {
                String strCons;

                strCons = "SELECT * FROM PZ_GRP_EQUIVALENTE ORDER BY PZ_GRP_EQUI_ID ASC";

                ManBd objBD = new ManBd();
                datoTabla = ManBd.CargarTabla(strCons);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return datoTabla;
        }

 public static DataTable ConsultaDatosAutentic()
        {
            DataTable datoTabla = null;
            try
            {
                String strCons;

                strCons = "SELECT Param_CDZONAFRANCA, Param_CDCIAUSUARIA, Param_DSUSUARIO, Param_PWDUSUARIO, Param_ACTIVO " 
                        + "FROM PZ_PARAMETROS WHERE (Param_ACTIVO = 1)";

                ManBd objBD = new ManBd();
                datoTabla = ManBd.CargarTabla(strCons);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return datoTabla;
        }

        public static bool CreaGrupoEquiv(String nuevoGrupo)
        {
            bool grCreado = false;
            try
            {
                String strCons;
                strCons = "SELECT Max(PZ_GRP_EQUI_ID) AS MAXGRP FROM PZ_GRP_EQUIVALENTE";

                int valFil = ManBd.MaximoID(strCons);
                if (valFil > 0)
                    {
                        valFil++;

                        string Datos = "PZ_GRP_EQUI_ID, PZ_GRP_EQUI_DESC";
                        string Valores = Convert.ToString(valFil) + ", '" + nuevoGrupo + "'";

                        valFil = ManBd.InsertaDatos(Datos, "PZ_GRP_EQUIVALENTE", Valores);

                        if(valFil>0)
                            grCreado = true;
                    }
              
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return grCreado;
        }

        public static bool CreaItemEquiv(int idGrupo, String itEquiv, string itOrig, string itMed1, string itMed2, int itAct)
        {
            bool itCread = false;
            try 
            {
                 String strCons;
                 strCons = "SELECT Max(PZ_ITEMS_EQUI_ID) AS MAXGRP FROM PZ_ITEM_EQUI";

                int valFil = ManBd.MaximoID(strCons);
                if (valFil > 0)
                {
                    valFil++;

                    string Datos = "PZ_ITEM_EQUI_ORI, PZ_ITEM_EQUI_PICIZ, PZ_ITEM_EQUI_MED1, PZ_ITEM_EQUI_MED2, PZ_ITEM_EQUI_ACTIVO, PZ_GRP_EQUI_ID, PZ_ITEMS_EQUI_ID";
                    string Valores = "'" + Convert.ToString(itOrig) + "', '" + Convert.ToString(itEquiv) + "', " + Convert.ToString(itMed1) + ", " +
                                    Convert.ToString(itMed2) + ", " + Convert.ToString(itAct) + ", " + Convert.ToString(idGrupo) + ", " + Convert.ToString(valFil);

                    valFil = ManBd.InsertaDatos(Datos, "PZ_ITEM_EQUI", Valores);
                    if (valFil > 0)
                        itCread = true;
                }
                 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return itCread;
        }

        public static bool ActualizaGrupoEquiv(String nuevoGrupo, int GrupoID, int grpAct)
        {
            bool grCreado = false;
            try
            {
                String strCons;
               
                strCons = "UPDATE PZ_GRP_EQUIVALENTE SET PZ_GRP_EQUI_DESC ='" +  nuevoGrupo + "', PZ_GRP_EQUI_ACTIVO =" + grpAct + 
                           " WHERE PZ_GRP_EQUI_ID = " + Convert.ToString(GrupoID);

                ManBd.ActualizaTabla(strCons);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return grCreado;
        }

        public static bool ActualizaItemEquiv(String itEquiv, string itOrig, string itMed1, string itMed2, int itID, int itAct)
        {
            bool grCreado = false;
            try
            {
                String strCons;

                strCons = "UPDATE PZ_ITEM_EQUI SET PZ_ITEM_EQUI_ORI ='" + itOrig +
                           "', PZ_ITEM_EQUI_ACTIVO =" + itAct
                          + ", PZ_ITEM_EQUI_PICIZ ='" + itEquiv
                          + "', PZ_ITEM_EQUI_MED1 =" + itMed1
                          + ", PZ_ITEM_EQUI_MED2 =" + itMed2
                          + " WHERE PZ_ITEMS_EQUI_ID = " + Convert.ToString(itID);

                ManBd.ActualizaTabla(strCons);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return grCreado;
        }

        public static DataTable FiltrarTablaEstOrden(DataTable datoTabla)
        {
            DataTable newdatoTabla = datoTabla;

            if (newdatoTabla != null)
            {
                int numRow;
                int i = 0;
                numRow = newdatoTabla.Rows.Count;

                newdatoTabla.Columns.Add("ESTADO");
                newdatoTabla.Columns.Add("TIPO");
 
                while (i < numRow)
                {
                    string tipOrden;
                    string itDesc;
                    DataRow row = newdatoTabla.Rows[i];
                    tipOrden = Convert.ToString(row["T_DOC_OP"]);
                    itDesc = Convert.ToString(row["DES_ITEM"]);

                    if (tipOrden.Contains("OD") && itDesc.Contains("(ALUM"))
                    {
                        newdatoTabla.Rows[i].Delete();
                        
                    }
                    else
                    {
                        int valEstado;
                        valEstado = Convert.ToInt32(row["ID_ESTADO"]);
                        
                        switch (valEstado)
                        {
                            case 0:
                                newdatoTabla.Rows[i]["ESTADO"] = "En elaboracion";
                               
                                break;
                            case 1:
                                newdatoTabla.Rows[i]["ESTADO"] = "Aprobada";
                               
                                break;
                            case 3:
                                newdatoTabla.Rows[i]["ESTADO"] = "Cumplida";
                               
                                break;
                            case 9:
                                newdatoTabla.Rows[i]["ESTADO"] = "Anulada";
                              
                               break;
                        }

                        if (itDesc.Contains("(ALUM"))
                        {
                            newdatoTabla.Rows[i]["TIPO"] = "ALUM";
                        }
                        else if (itDesc.Contains("(AC"))
                        {
                            newdatoTabla.Rows[i]["TIPO"] = "AC";
                        }
                        else if (itDesc.Contains("(COM"))
                        {
                            newdatoTabla.Rows[i]["TIPO"] = "COM";
                        }
                      
                    }

                    i++;
                   
                }
            }
            
            return newdatoTabla;
        }


       public static DataTable FiltrarItemKanban(SqlDataReader dataRead)
        {
            DataTable newdatoTabla = null;

            try
            {
                DataTable tmpTable;

                // Create a new DataTable.
                tmpTable = new DataTable();

                // Create DataColumn objects of data types.
                DataColumn colUND = new DataColumn("UND");
                colUND.DataType = System.Type.GetType("System.String");
                tmpTable.Columns.Add(colUND);

                DataColumn colCANT_MP = new DataColumn("CANT_MP");
                colCANT_MP.DataType = System.Type.GetType("System.Decimal");
                tmpTable.Columns.Add(colCANT_MP);

                DataColumn colCOD_ITEM_MP = new DataColumn("COD_ITEM_MP");
                colUND.DataType = System.Type.GetType("System.String");
                tmpTable.Columns.Add(colCOD_ITEM_MP);

                DataColumn colDES_ITEM_MP = new DataColumn("DES_ITEM_MP");
                colUND.DataType = System.Type.GetType("System.String");
                tmpTable.Columns.Add(colDES_ITEM_MP);

                DataColumn colFACTOR = new DataColumn("FACTOR");
                colCANT_MP.DataType = System.Type.GetType("System.Decimal");
                tmpTable.Columns.Add(colFACTOR);

                DataColumn colES_KANBAN = new DataColumn("ES_KANBAN");
                colUND.DataType = System.Type.GetType("System.String");
                tmpTable.Columns.Add(colES_KANBAN);

                DataColumn colGRP_EQUI_ID = new DataColumn("GRP_EQUI_ID");
                colUND.DataType = System.Type.GetType("System.String");
                tmpTable.Columns.Add(colGRP_EQUI_ID);

                DataColumn colUND_PZ = new DataColumn("UND_PZ");
                colUND_PZ.DataType = System.Type.GetType("System.String");
                tmpTable.Columns.Add(colUND_PZ);

                DataColumn colSal_PZ = new DataColumn("SALDO_P");
                colSal_PZ.DataType = System.Type.GetType("System.String");
                tmpTable.Columns.Add(colSal_PZ);

                DataColumn colCANT_P = new DataColumn("CANT_P");
                colCANT_MP.DataType = System.Type.GetType("System.Decimal");
                tmpTable.Columns.Add(colCANT_P);

                DataColumn colCar_PZ = new DataColumn("CARGA_P");
                colCar_PZ.DataType = System.Type.GetType("System.Boolean");
                tmpTable.Columns.Add(colCar_PZ);

                DataColumn colCONT_P = new DataColumn("CONTROL");
                colCONT_P.DataType = System.Type.GetType("System.String");
                tmpTable.Columns.Add(colCONT_P);

                if (dataRead != null)
                {
                    int i = 0;
                    int cont = 1;

                    //numRow = newdatoTabla.Rows.Count;

                    object[] arItKanban = null;
                    
                    while (dataRead.Read())
                    {
                        string esKanban;
                        string esContr;
                        string strDesc;
                        string strCodIt;

                        bool filtra = false;
                        //DataRow row = newdatoTabla.Rows[i];
                        DataRow tmpRow;
                        tmpRow = tmpTable.NewRow();

                        esKanban = Convert.ToString(dataRead["ES_KANBAN"]);
                        esKanban = esKanban.ToUpper();
                        esContr = Convert.ToString(dataRead["CONTROL"]);
                        esContr = esContr.ToUpper();

                        strDesc = Convert.ToString(dataRead["DESCRIPCION"]);
                        strDesc = strDesc.ToUpper();
                        strCodIt = Convert.ToString(dataRead["DESCRIPCION"]);
                        strCodIt = strDesc.ToUpper();

                        tmpRow["UND"] = Convert.ToString(dataRead["UND"]);
                        tmpRow["CANT_MP"] = decimal.Round(Convert.ToDecimal(dataRead["CANT_MP"]), 2);
                        tmpRow["DES_ITEM_MP"] = Convert.ToString(dataRead["DESCRIPCION"]);
                        tmpRow["COD_ITEM_MP"] = Convert.ToString(dataRead["COD_ITEM_MP"]);
                        tmpRow["GRP_EQUI_ID"] = Convert.ToString(dataRead["GRP_EQUI_ID"]);
                        tmpRow["FACTOR"] = decimal.Round(Convert.ToDecimal(dataRead["FACTOR"]), 2);
                        tmpRow["ES_KANBAN"] = Convert.ToString(dataRead["ES_KANBAN"]);
                        tmpRow["UND_PZ"] = Convert.ToString(dataRead["UND_INV"]);
                        tmpRow["SALDO_P"] = "";
                        tmpRow["CARGA_P"] = true;
                        tmpRow["CONTROL"] = Convert.ToString(dataRead["CONTROL"]);

                        if (esKanban.Contains("SI"))
                        {
                            filtra = true;
                        }
                        else if (strDesc.Contains("-PN"))
                        {
                            filtra = true;
                        }
                        else if (esContr.Contains("NO"))
                        {
                            filtra = true;
                        }

                        if (filtra == true)
                        {
                            Array.Resize<object>(ref arItKanban, cont);
                            string[] arItDat = { Convert.ToString(dataRead["COD_ITEM_MP"]), Convert.ToString(dataRead["CANT_MP"]) };
                            arItKanban[cont - 1] = arItDat;
                            cont++;

                            newdatoTabla.Rows[i].Delete();

                        }
                        else
                        {
                            decimal cantReq = Convert.ToDecimal(dataRead["CANT_MP"]);
                            decimal valfactor = Convert.ToDecimal(dataRead["FACTOR"]);

                            tmpRow["CANT_P"] = decimal.Round(cantReq / valfactor, 2);

                            tmpTable.Rows.Add(tmpRow);
                        }

                        i++;

                    }
                    newdatoTabla = tmpTable;

                    ManBd objBD = new ManBd();
                    newdatoTabla = ManBd.ActualizaItemKanban(newdatoTabla, arItKanban);
                    newdatoTabla = UnificarItem(newdatoTabla);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return newdatoTabla;
        }
        public static DataTable UnificarItem(DataTable datoTabla)
        {
            DataTable newdatoTabla = datoTabla;

            try
            {
                DataTable tmpTable;

                // Create a new DataTable.
                tmpTable = new DataTable();
                tmpTable = datoTabla.Clone();

                if (newdatoTabla != null)
                {
                    int numRow;
                    int i = 0;

                    numRow = newdatoTabla.Rows.Count;
                    System.Collections.ArrayList arCodEnTabla = new System.Collections.ArrayList();

                    while (i < numRow)
                    {
                        decimal rowCantMP;
                        decimal rowCanP;
                        string strCodIt;

                        int PosItem = 0;

                        DataRow row = newdatoTabla.Rows[i];
                        if (row != null)
                        {
                            rowCantMP = Convert.ToDecimal(row["CANT_MP"]);
                            rowCanP = Convert.ToDecimal(row["CANT_P"]);

                            strCodIt = Convert.ToString(row["COD_ITEM_MP"]);
                            strCodIt = strCodIt.ToUpper();

                            bool adItem = true;
                            adItem = Convert.ToBoolean(row["CARGA_P"]);
                            if (adItem)
                            {
                                if (i > 0)
                                {
                                    if (arCodEnTabla.Contains(strCodIt))
                                    {
                                        adItem = false;
                                        PosItem = arCodEnTabla.IndexOf(strCodIt);
                                    }

                                }


                                if (adItem)
                                {
                                    DataRow tmpRow;
                                    tmpRow = tmpTable.NewRow();
                                    tmpRow.ItemArray = row.ItemArray;
                                    tmpTable.Rows.Add(tmpRow);
                                    arCodEnTabla.Add(strCodIt);

                                }
                                else
                                {
                                    DataRow tmpRow;
                                    tmpRow = tmpTable.Rows[PosItem];
                                    decimal actCantMP;
                                    decimal actCanP;
                                    actCantMP = Convert.ToDecimal(tmpRow["CANT_MP"]);
                                    actCanP = Convert.ToDecimal(tmpRow["CANT_P"]);
                                    tmpTable.Rows[PosItem]["CANT_P"] = decimal.Round(actCanP + rowCanP, 2);
                                    tmpTable.Rows[PosItem]["CANT_MP"] = decimal.Round(actCantMP + rowCantMP, 2);
                                }
                            }
                            
                        }

                        i++;

                    }
                    newdatoTabla = tmpTable;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return newdatoTabla;
        }

     
        public static ListItemCollection ConsultaCDMerc()
        {
            ListItemCollection arTipo=new ListItemCollection();
            arTipo.Add(new ListItem("15"));
            arTipo.Add(new ListItem("16"));

            return arTipo;
        }


        public static ListItemCollection ConsultaCDSubPartida()
        {
            ListItemCollection arTipo = new ListItemCollection();
            arTipo.Add(new ListItem("7308400000"));
            arTipo.Add(new ListItem("7308909000"));
            arTipo.Add(new ListItem("7318240000"));
            arTipo.Add(new ListItem("7326909000"));
            arTipo.Add(new ListItem("7610900000"));
            arTipo.Add(new ListItem("8205599900"));
            arTipo.Add(new ListItem("8480600000"));
           
           
            return arTipo;
        }

        public static ListItemCollection ConsultaTipo()
        {
            ListItemCollection arTipo = new ListItemCollection();
            arTipo.Add(new ListItem("ZF"));
            arTipo.Add(new ListItem("LD"));

            return arTipo;
        }


        public static ListItemCollection ConsultaUndCom()
        {
            ListItemCollection arTipo=new ListItemCollection();
            arTipo.Add(new ListItem("U"));
            arTipo.Add(new ListItem("KG"));

            return arTipo;
        }

        public static ListItemCollection ConsultaUndMed()
        {
           ListItemCollection arTipo=new ListItemCollection();
            arTipo.Add(new ListItem("U"));
             arTipo.Add(new ListItem("KG"));

            return arTipo;
        }

        public static ListItem[] ConvLista(DataTable dt, string strColumDesc, string strColumVal)
        {
            ListItem[] arTipo = null;
            if (dt != null)
            {
                int cont = 1;
                foreach (DataRow row in dt.Rows)
                {

                    string codRow = Convert.ToString(row[strColumDesc]);
                    string valRow = Convert.ToString(row[strColumVal]);
                    Array.Resize<ListItem>(ref arTipo, cont);
                    arTipo[cont - 1] = new ListItem(codRow, valRow);
                    cont++;                  
                }
            }
          
            return arTipo;
        }

        public static String[] ConvArray(DataTable dt, string strColumDesc)
        {
            String[] arTipo=null;
            int cont=1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    string codRow = Convert.ToString(row[strColumDesc]);
                    if (cont == 1)
                    {
                        Array.Resize<string>(ref arTipo, cont);
                        arTipo[cont - 1] = codRow;
                        cont++;
                    }
                    else if (!arTipo.Contains(codRow))
                    {
                        Array.Resize<string>(ref arTipo, cont);
                        arTipo[cont - 1] = codRow;
                        cont++;
                    }
                }
            }

            return arTipo;
        }


        public static int ConsultaItemCreado(string CodItem)
        {
            int itCrea = -1;
            String strCons;

            strCons = "SELECT * FROM PZ_MATRIZ WHERE PZ_IP_Item = '" + CodItem + "'";
            ManBd objBD = new ManBd();
            DataTable datoTabla = ManBd.CargarTabla(strCons);

            if (datoTabla != null)
            {
                if (datoTabla.Rows.Count > 0)
                {
                    DataRow itRow = datoTabla.Rows[0];

                    itCrea = Convert.ToInt16( itRow["Id_PZ_Item_Piciz"]);
                }
                    
            }

            return itCrea;
        }

        public static int CreaItemEnBD(string itOfPapa, string CodItem, int itCreado, 
                                        string itUsuario)
        {
            int itCrea = -1;
           
                       
            ManBd objBD = new ManBd();

            String strCons;
            strCons = "SELECT Max(Id_PZ_Item_Piciz) AS MAXGRP FROM PZ_MATRIZ";

            int idItem = ManBd.MaximoID(strCons);
            if (idItem < 0)
                idItem = 0;

            string Datos = "PZ_IP_Item_Papa, PZ_IP_OF_Papa, PZ_IP_Item, PZ_IP_Usuario, "
                            + "PZ_IP_Creado";
            
            string Valores = "0, '" + itOfPapa +"', '" + CodItem + "', '" + itUsuario + "', "
                             + Convert.ToString(itCreado);

            String Tabla = "PZ_MATRIZ";

            int valFil = ManBd.InsertaDatos(Datos, Tabla, Valores);

            if (valFil > 0)
                itCrea = ConsultaItemCreado(CodItem);

            return itCrea;
        }

        public static bool CreaCompoItemEnBD(int IdItemMatriz, string codItem, DataTable dtComp)
           {
            bool itCrea = false;


            ManBd objBD = new ManBd();

            if(dtComp != null)
            {
                int cont = 0;
                String strConsulta="";
                while (cont < dtComp.Rows.Count)
                {
                   
                    DataRow row = dtComp.Rows[cont];
                    string CodPiciz = Convert.ToString(row["COD_ITEM_MP"]);
                    string CodERP = Convert.ToString(row["GRP_EQUI_ID"]);
                    string mdNombre = Convert.ToString(row["DES_ITEM_MP"]);
                    string mdUndPcz = Convert.ToString(row["UND_PZ"]);
                    string mdUndCanPcz = Convert.ToString(row["CANT_P"]);
                    string mdUndERP = Convert.ToString(row["UND"]);
                    string mdCanERP = Convert.ToString(row["CANT_MP"]);

                    string Datos = "PZ_MD_Cod_Piciz, PZ_MD_Cod_Erp, PZ_MD_Nombre, "
                           + "PZ_MD_Und_Pcz, PZ_MD_Can_Pcz, PZ_MD_Und_Erp, PZ_MD_Can_Erp, Id_PZ_Item_Piciz";

                    string Valores = "'" + CodPiciz + "', '" + CodERP + "', '" + mdNombre + "', "
                                    + "'" + mdUndPcz + "', '" + mdUndCanPcz + "'"
                                    + ", '" + mdUndERP + "', " + mdCanERP + ", " + IdItemMatriz;
                    String Tabla = "PZ_Matriz_Detalle";


                    strConsulta = strConsulta + "INSERT INTO " + Tabla + " (" + Datos + ") VALUES (" + Valores + ") ";

                    cont = cont + 1;
                }

                DataTable unTab = ManBd.ActualizaTabla(strConsulta);
            }
            return itCrea;
        }

    }
}
