using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CapaControl
{
    public static class AdminService
    {
        private static string CDCIAUSUARIA = "500";
        private static string CDZONAFRANCA = "969";
        private static string DSUSUARIO = "LUISDU500";
        private static string PWDUSUARIO = "FORSA1";
       // private static string CDTIPO = "ZF";
        private static string _erCrea ="";
        //private static decimal CDSUBPARTIDA = 848060000;

        public static decimal ConsultaSaldo(string codItem)
        {
            decimal itSaldo = PicizSaldoItem(codItem);
           
            return itSaldo;
        }

        public static ConsultasParametricas.RESULTADODATOSITEM DatosItem(string codItem)
        {
            ConsultasParametricas.RESULTADODATOSITEM itSaldo = null;
            try
            {
                ConsultasParametricas.Administrador_AdministradorParametricas_ConsultasParametricas wsConsulta =
            new ConsultasParametricas.Administrador_AdministradorParametricas_ConsultasParametricas();

                 ConsultasParametricas.inHeader clHeader =
                   new ConsultasParametricas.inHeader();

                DataTable dtAutentica = Consulta.ConsultaDatosAutentic();

                if (dtAutentica !=  null)
                {
                    CDCIAUSUARIA = Convert.ToString(dtAutentica.Rows[0]["Param_CDCIAUSUARIA"]);
                    CDZONAFRANCA = Convert.ToString(dtAutentica.Rows[0]["Param_CDZONAFRANCA"]);
                    DSUSUARIO = Convert.ToString(dtAutentica.Rows[0]["Param_DSUSUARIO"]);
                    PWDUSUARIO = Convert.ToString(dtAutentica.Rows[0]["Param_PWDUSUARIO"]);
                }

                clHeader.CDCIAUSUARIA = CDCIAUSUARIA;
                clHeader.CDZONAFRANCA = CDZONAFRANCA;
                clHeader.DSUSUARIO = DSUSUARIO;
                clHeader.PWDUSUARIO = PWDUSUARIO;

                wsConsulta.inHeaderValue = clHeader;

                ConsultasParametricas.DATOSITEM itDato = new ConsultasParametricas.DATOSITEM();

                itDato.CDITEM = codItem;
                ConsultasParametricas.RESULTADODATOSITEM resConsulta = wsConsulta.DatosPorItem(itDato);

                if (resConsulta != null)
                {
                    itSaldo = resConsulta;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("In Main catch block. Caught: {0}", ex.Message);
                Console.WriteLine("Inner Exception is {0}", ex.InnerException);
            }
            
            return itSaldo;
        }


        public static decimal PicizSaldoItem(string codItem)
        {
            decimal itSaldo = 0;
            try
            {
                ConsultasParametricas.RESULTADODATOSITEM resConsulta = DatosItem(codItem);

                if (resConsulta != null)
                {
                    itSaldo = Convert.ToDecimal(resConsulta.SALDO_REAL);
                    itSaldo = Math.Round(itSaldo, 3);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("In Main catch block. Caught: {0}", ex.Message);
                Console.WriteLine("Inner Exception is {0}", ex.InnerException);
            }

            return itSaldo;
        }

        public static bool ItemCreado(string codItem)
        {
            
            bool itCreado = false;
            try
            {
                ConsultasParametricas.RESULTADODATOSITEM resConsulta = DatosItem(codItem);

                if (resConsulta != null)
                {
                    if (resConsulta.CDITEM != "0")
                    {
                        itCreado = true;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return itCreado;
        }

        public static bool InsertaItemPiciz(string codItem, string cdTipoItem, 
                              decimal tipoMerc, string descItem,
                            string itUndCom, string itUndMed, 
                            string SComp, decimal itSubP)
        {
            
            bool boolCrea=false;

            try
            {
                MatricesItems.Administrador_AdministradorMatricesItems_MatricesItems wsMatriz =
               new MatricesItems.Administrador_AdministradorMatricesItems_MatricesItems();

                MatricesItems.ArrayOfITEMCOMPANIAINSTITEMCOMPANIAINST valItem =
                    new MatricesItems.ArrayOfITEMCOMPANIAINSTITEMCOMPANIAINST();

                valItem.CDITEM = codItem;
                valItem.CDTIPO = cdTipoItem;
                valItem.CDTIPOMERCANCIA = tipoMerc;
                valItem.DSITEM = descItem;
                valItem.CDSUBPARTIDA = itSubP;
                valItem.CDUNIDADCOMERCIAL = itUndCom;
                valItem.CDUNIDADMEDIDA = itUndMed;
                valItem.NMCONVERSION = 1;
                valItem.SNCOMPONENTES = SComp;

                MatricesItems.ArrayOfITEMCOMPANIAINSTITEMCOMPANIAINST[] arItem = null;

                arItem = new MatricesItems.ArrayOfITEMCOMPANIAINSTITEMCOMPANIAINST[1];

                arItem[0] = valItem;

                MatricesItems.INSERTARITEMSCOMPANIA msInserta
                    = new MatricesItems.INSERTARITEMSCOMPANIA();

                msInserta.ITEMSCOMPANIA = arItem;

                MatricesItems.inHeader clHeader =
                       new MatricesItems.inHeader();

                DataTable dtAutentica = Consulta.ConsultaDatosAutentic();

                if (dtAutentica != null)
                {
                    CDCIAUSUARIA = Convert.ToString(dtAutentica.Rows[0]["Param_CDCIAUSUARIA"]);
                    CDZONAFRANCA = Convert.ToString(dtAutentica.Rows[0]["Param_CDZONAFRANCA"]);
                    DSUSUARIO = Convert.ToString(dtAutentica.Rows[0]["Param_DSUSUARIO"]);
                    PWDUSUARIO = Convert.ToString(dtAutentica.Rows[0]["Param_PWDUSUARIO"]);
                }

                clHeader.CDCIAUSUARIA = CDCIAUSUARIA;
                clHeader.CDZONAFRANCA = CDZONAFRANCA;
                clHeader.DSUSUARIO = DSUSUARIO;
                clHeader.PWDUSUARIO = PWDUSUARIO;

                wsMatriz.inHeaderValue = clHeader;

                MatricesItems.typeResultadoMatricesItems rsInserta =
                    wsMatriz.InsertarItem(msInserta);

                boolCrea = rsInserta.FGRESULTADO;
                MsgCrea = rsInserta.DSMENSAJE;
                if (!boolCrea)
                {
                    int numErr;
                    int i=0;
                    numErr=rsInserta.ERRORES.Length;
                    if (numErr > 0)
                    {
                        while (i < numErr)
                        {
                            MatricesItems.typeErrorMatricesItems mtErr=rsInserta.ERRORES[i];
                            MsgCrea = MsgCrea + "\r\n" + mtErr.DSMENSAJE + " Cod. Error: " + mtErr.CDREGISTRO + " Cod. Campo: " + mtErr.DSCAMPO;
                            i++;
                        }
                    }
                }
                
            }
            catch(Exception ex)
            {
                MsgCrea = ex.Message;
            }
           

            return boolCrea;
        }

        public static bool InsertaCompoPiciz(string codItem, DataTable dtComp)
        {

            bool boolCrea = false;

            try
            {
                if (dtComp != null)
                  {
                      int numRows = dtComp.Rows.Count;
                      if (numRows  > 0)
                      {
                          MatricesItems.Administrador_AdministradorMatricesItems_MatricesItems wsMatriz =
                            new MatricesItems.Administrador_AdministradorMatricesItems_MatricesItems();

                          MatricesItems.MATRIZ matComp = null;

                          matComp = new MatricesItems.MATRIZ();

                          matComp.CDITEM = codItem;

                          MatricesItems.MATRIZCOMPONENTE[] arComp = null;

                          int ct = 0;
                          string strMsg = "";
                          
                          while (ct < numRows)
                          {
                              DataRow itRow = dtComp.Rows[ct];
                              MatricesItems.MATRIZCOMPONENTE mtComp= 
                                  new MatricesItems.MATRIZCOMPONENTE();

                              mtComp.CDITEM = Convert.ToString(itRow["COD_ITEM_MP"]);
                              mtComp.NMCANTIDADCONS = Convert.ToDecimal(itRow["CANT_P"]);
                              mtComp.NMDESPERDICIO = 0;

                              Array.Resize<MatricesItems.MATRIZCOMPONENTE>(ref arComp, ct + 1);
                              arComp[ct] = mtComp;
                              ct++;
                           }

                          MsgCrea += strMsg;

                          matComp.COMPONENTE = arComp;
                          
                          MatricesItems.inHeader clHeader =
                                 new MatricesItems.inHeader();

                          DataTable dtAutentica = Consulta.ConsultaDatosAutentic();

                          if (dtAutentica != null)
                          {
                              CDCIAUSUARIA = Convert.ToString(dtAutentica.Rows[0]["Param_CDCIAUSUARIA"]);
                              CDZONAFRANCA = Convert.ToString(dtAutentica.Rows[0]["Param_CDZONAFRANCA"]);
                              DSUSUARIO = Convert.ToString(dtAutentica.Rows[0]["Param_DSUSUARIO"]);
                              PWDUSUARIO = Convert.ToString(dtAutentica.Rows[0]["Param_PWDUSUARIO"]);
                          }

                          clHeader.CDCIAUSUARIA = CDCIAUSUARIA;
                          clHeader.CDZONAFRANCA = CDZONAFRANCA;
                          clHeader.DSUSUARIO = DSUSUARIO;
                          clHeader.PWDUSUARIO = PWDUSUARIO;

                          wsMatriz.inHeaderValue = clHeader;

                          MatricesItems.typeResultadoMatricesItems rsInserta =
                              wsMatriz.InsertarComponente(matComp);

                          boolCrea = rsInserta.FGRESULTADO;
                          MsgCrea = rsInserta.DSMENSAJE;

                          if (!boolCrea)
                          {
                              int numErr;
                              int i = 0;
                              numErr = rsInserta.ERRORES.Length;
                              if (numErr > 0)
                              {
                                  while (i < numErr)
                                  {
                                      MatricesItems.typeErrorMatricesItems mtErr = rsInserta.ERRORES[i];
                                      MsgCrea = MsgCrea + "\r\n" + mtErr.DSMENSAJE + " Cod. Error: " + mtErr.CDREGISTRO + " Cod. Campo: " + mtErr.DSCAMPO; 
                                      i++;
                                  }
                              }
                          }

                      }
                   

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MsgCrea = ex.Message;
            }

           

            return boolCrea;
        }

        public static string MsgCrea
        {
            get { return _erCrea; }
           set { _erCrea = value; }
        }

        public static string PzUser
        {
            get { return DSUSUARIO; }
            set { DSUSUARIO = value; }
        }
    }

}
