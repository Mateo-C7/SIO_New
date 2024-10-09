using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using Microsoft.Reporting.WebForms;
using System.Linq;
using System.Web;

using static SIO.wsFUP;

namespace SIO
{
    public partial class wsConsumeGerbo : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public SqlDataReader readerItem = null;
        public ControlConsumeGerbo controlgerbo = new ControlConsumeGerbo();
        private DataSet dsPlan = new DataSet();

        private wsGerbo.AuthHeader wsAuth = new wsGerbo.AuthHeader();
        //Cria instância de acesso ao webservice
        private wsGerbo.WSInterfaceApp meuWS = new wsGerbo.WSInterfaceApp();
        //Cria instância de acesso ao webservice de pruebas
        //private WsGerboPRU.WSInterfaceApp meuWSPRU = new WsGerboPRU.WSInterfaceApp();
        string RespuestaGlobal = "", detalle= "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmensaje.Text =" ";
                string opcion = string.Empty;
                string respuesta = string.Empty;

                

                //ESTRUCTURA DE ITEMS Y ORDENES

                // identifico si es un llamado para creacion de item de ordenes y ordenes de produccion
                if (Request.QueryString["IdRayaxx"] != null)
                {
                    string IdRaya = Request.QueryString["IdRaya"];
                    Session["IdRaya"] = IdRaya;

                    // realizo el llamado de los metodos para crear items y ordenes
                    this.ejecutaNivelItemOrden(Convert.ToInt32( IdRaya));
                    Session["Metodo"] = "ItemOrden";

                    if (detalle == "" && Request.QueryString["IdRaya"] != null)
                    {
                        lblMensajePrincipal.BackColor = Color.Red;
                        lblMensajePrincipal.Text = "Alerta Estructura sin Crear Verifique !! ";
                    }
                    else
                    {
                        if (detalle != "" && Request.QueryString["IdRaya"] != null)
                        {
                            lblMensajePrincipal.BackColor = Color.Green;
                            lblMensajePrincipal.Text = "Estructura Creada Exitosamente !!";
                        }
                    }

                    int UnidadNegocio = Convert.ToInt32( Session["UnidadNegocioRaya"]);
                    int OpRayaAcc = Convert.ToInt32(Session["OpRayaAcc"]);
                    string TipoOrden = Session["TipoOrden"].ToString();

                    // evaluo si es raya de accesorios y si falta por enviar a crear requisiciones
                    
                    if (UnidadNegocio == 3 && OpRayaAcc > 0 )
                        {
                        //consulto Accesorios pendientes por requisicion
                        int cantFaltante = consultarCantFaltaAccReq(Convert.ToInt32(IdRaya));

                        if(cantFaltante > 0)
                        { 
                            // EJECUTO EL PROCESO para la creacion de la requisiones de accesorios faltantes
                           ejecutaRequesicionAcc(Convert.ToInt32( IdRaya));
                        }
                    }

                }

                // ESTRUCTURA DE CREAR REQUISICIONES

                //Creacion de requesiciones por solicitud de materia prima
                if (Request.QueryString["IdMpSolxx"] != null)
                {
                    string IdMpSol = Request.QueryString["IdMpSol"];
                    int cantCompSolMp = 0, fila= 0, cantEnviada = 0, faltante = -1;

                    Session["IdMpSol"] = IdMpSol;
                    Session["Metodo"] = "Requisicion";

                    // consulto la cantidad de componentes de la solmp
                    cantCompSolMp = consultarCantSolMpForsa(Convert.ToInt32(IdMpSol));

                    if (Convert.ToInt32(Session["OpMpSol"]) != 0)
                    {
                        //consulto la cantidad enviadas con requisicion
                        cantEnviada = consultarCantEnviadaSolMpForsa(Convert.ToInt32(IdMpSol));

                        faltante = cantCompSolMp - cantEnviada;
                           
                        if (faltante > 0)
                        {
                            // EJECUTO EL METODO con los items pendientes por requerir
                            consultarSolMpForsa(Convert.ToInt32(IdMpSol));                                                          

                            //consulto la cantidad enviadas con requisicion
                            cantEnviada = consultarCantEnviadaSolMpForsa(Convert.ToInt32(IdMpSol));

                            faltante = cantCompSolMp - cantEnviada;

                            //actualizo si esta en cero en mpsol
                            if (faltante == 0)

                            {
                                actualizaReqMpSol(Convert.ToInt32(Session["IdRequisicion1"]), Convert.ToInt32(IdMpSol));
                                actualizarFechaAprobaEnMpSol(Convert.ToInt32(Session["IdRequisicion1"]), Convert.ToInt32(IdMpSol));
                            }                                                               
                        }                                
                    }
                    else
                    {
                        RespuestaGlobal = RespuestaGlobal + "No existe OP Verifique !! ";
                    }

                    string DatosAd = " Raya :" + Session["Ofa"].ToString() + " OP: " + Session["OpMpSol"].ToString() + "  Cant Componentes: "+cantCompSolMp.ToString()+" </br>  </br> "; 
                    RespuestaGlobal = DatosAd + RespuestaGlobal + " </ br > </br> " + " FALTAN " + faltante.ToString() + " COMPONENTES POR REQUISICION ";
                }

                // ESTRUCTURA PARA CREAR ITEMS DE ACCESORIOS
                 
                // creacion de items de accesorios disponibles en accesorios codigo con cod erp -1
                if (Request.QueryString["ItemPlantillaxx"] != null)
                {
                    string ItemPlantilla = Request.QueryString["ItemPlantilla"];
                    int cantAccDisp = 0, idItemPlanta = 0;

                    if(ItemPlantilla == "1")
                    {
                        //consulto la cantidad de items en acc codigo disponibles para crear
                        cantAccDisp = consultarCantItemsCrear();

                        if (cantAccDisp > 0 )
                        {
                            ejecutaCreaItemAcc(cantAccDisp);
                        }
                        else
                        {
                            detalle = detalle + " NO EXISTEN ACCESORIOS CONFIGURADOS PARA CREAR EN GERBO BRASIL ";
                        }
                    
                    }
                 }

                // inserto el log en bd forsa
                string Log =  RespuestaGlobal;
                if(Log !="")    actualizaLogGerboEnForsa(Log);                

                lblmensaje.Text = RespuestaGlobal;
                lbldetalle.Text = detalle;

                lblmensaje.Text = "FORA DE USO";
            }
        }

        public void ejecutaNivelItemOrden(int IdRaya)
        {
            int nivel = 1;
            string nombreItem = "", IdItemGerbo = "" , IdOpGerbo = "";

            while (nivel <= 3)
            {
                // consulto la raya que esta entregando y armo el nombre de acuerdo a la estructura en la bd forsa
                nombreItem = consultarNombreItemNivel(IdRaya, nivel);

                // consulto el id del item con base al nombre en bd de gerbo consulta directa gerbo
                IdItemGerbo = consultarItemxRayaGerbo(nombreItem);
                //IdItemGerbo = "";
                
                // evaluo si el item no existe para proceder a crearlo
                if (IdItemGerbo == "")
                {
                    //llamo al metodo para ejecutar el metodo de crear item de wsgerbo con el id 2
                    RespuestaGlobal = RespuestaGlobal + " / " + ejecutaMetodoWsGerbo("2");
                    //// consulto el id del item con base al nombre en bd de gerbo
                    //IdItemGerbo = consultarItemxRayaGerbo(nombreItem);
                    //actualizaItemEnForsa(nivel, IdRaya,Convert.ToInt32( IdItemGerbo));
                }

                if (RespuestaGlobal.Contains("#ERRO#"))
                {
                    detalle = "Error " + detalle + "OP Nivel: " + nivel + " Num: " + IdOpGerbo + " / </br> ";
                }
                else
                {
                    // consulto el id del item con base al nombre en bd de gerbo
                    IdItemGerbo = consultarItemxRayaGerbo(nombreItem);
                    actualizaItemEnForsa(nivel, IdRaya, Convert.ToInt32(IdItemGerbo));

                    detalle = detalle + "Item Nivel: " +nivel+" Cod: " + IdItemGerbo+ " / ";

                    // consulto el id de la op de acuerdo al iditemgerbo en gerbo
                    IdOpGerbo = consultarOpxRayaGerbo(IdItemGerbo);

                    // evaluo si existe la op para proceder a crearlo
                    if(IdOpGerbo == "")
                    {
                        int nivelAnterior = 0;
                        nivelAnterior = nivel - 1;

                        // verifico si es a nivel if para que no reste el nivel
                        if (nivel == 1)
                        { 
                            nivelAnterior = 1;
                            Session["IdOpPadre"] = "";
                            Session["TipoDocPadre"] = "";                        
                        }
                        else
                        {
                            string NombreItemAnterior = consultarNombreItemNivelAnterior(IdRaya, nivelAnterior);
                            String opPadre = consultarOpPadrexNombreItem(NombreItemAnterior);
                        }

                        Session["IdItemGerbo"] = IdItemGerbo;

                        // consulto el id del item con base al nombre en bd de gerbo
                        RespuestaGlobal = RespuestaGlobal + " / " + ejecutaMetodoWsGerbo("3") ;

                        //IdOpGerbo = consultarOpxRayaGerbo(IdItemGerbo);
                        //actualizaOpEnForsa(nivel, IdRaya, Convert.ToInt32(IdOpGerbo));
                    }

                }
                

                if(RespuestaGlobal.Contains("#ERRO#"))
                {
                    detalle = "Error "+ detalle + "OP Nivel: " + nivel + " Num: " + IdOpGerbo + " / </br> " ;
                }
                else
                { 

                    IdOpGerbo = consultarOpxRayaGerbo(IdItemGerbo);

                    actualizaOpEnForsa(nivel, IdRaya, Convert.ToInt32(IdOpGerbo));

                    detalle = detalle + "OP Nivel: " + nivel + " Num: " + IdOpGerbo + " / </br>";

                    nivel = nivel + 1;
                 }

            }

        }

        // creacion de item accesorio en gerbo
        public void ejecutaCreaItemAcc(int cantAcc)
        {
            int Cont = 0, IdItemPlanta = 0;
            string nombreItem = "", IdItemGerbo = "";
             
            // consulto los ids de itemplanta
            readerItem = controlgerbo.consultarItemsItemPlantaBd();

            if (readerItem.HasRows == true)
            {
                while (readerItem.Read())
                {
                    IdItemPlanta = readerItem.GetInt32(0);

                    if (IdItemPlanta > 0)
                    {
                        // consulto los datos del item
                        nombreItem = consultarItemsDatos(IdItemPlanta);

                        // consulto el id del item con base al nombre en bd de gerbo
                         IdItemGerbo = consultarItemxRayaGerbo(nombreItem);
                        //IdItemGerbo = "";

                        // evaluo si el item no existe para proceder a crearlo
                        if (IdItemGerbo == "")
                        {
                            //llamo al metodo para ejecutar el metodo de crear item de wsgerbo con el id 2
                            RespuestaGlobal = RespuestaGlobal + " / " + ejecutaMetodoWsGerbo("2");
                            
                            Session["Metodo"] = "Item Accesorios";
                            int CodErp = Convert.ToInt32(Session["CodErp"]);
                            // consulto el id del item con base al nombre en bd de gerbo
                            //IdItemGerbo = CodErp.ToString();
                            IdItemGerbo = consultarItemxRayaGerbo(nombreItem);                            
                        }
                        if (IdItemGerbo != "")
                        {
                            actualizaItemPlantaAccCodigo(IdItemPlanta, Convert.ToInt32(IdItemGerbo));
                            detalle = detalle + "ITEM DE ACCESORIO CREADO EN GERBO CODIGO: " + IdItemGerbo + " / " + nombreItem + " </br> ";
                        }


                    }                        
                }
                
                readerItem.Close();
                readerItem.Dispose();
                controlgerbo.CerrarConexion();               
            }
        }  

        public string ejecutaMetodoWsGerbo ( string opcion)
        {
            String respuesta = "";
                
                //opcion = Request.QueryString["Opcion"];
                
                if (opcion != null)
                {
                    //Chave de autenticação do método
                    wsAuth.key = "9D2B3DD54594492E97C9FDDA1F5F3FD8";

                    //URL para webservice
                    //meuWS.Url = "http://172.21.0.1/wsGerbo/wsGerbo.asmx";
                    //URL para webservice pruebas nube
                    meuWS.Url = "http://177.185.11.24:8081/wsGerbo/wsGerbo.asmx";

                    //Define para o webservice a autenticação das chamadas aos métodos
                    meuWS.AuthHeaderValue = wsAuth;

                    switch (opcion)
                    {
                        case "1":
                            respuesta = TestWS();
                            break;
                        case "2":
                            respuesta = IncluirMaterial();
                            break;
                        case "3":
                            respuesta = IncluirOP();
                            break;
                        case "4":
                            respuesta = IncluirReqPlanej();
                            break;
                        default:
                            respuesta = "Metodo no definido";
                            break;
                    }
                }
                else
                {
                    respuesta = "Metodo no definido";
                }
               // Response.Write(respuesta);

            return respuesta;
        }

        private string TestWS()
        {
            string mensaje = string.Empty;
            try
            {
                //Invoca o método
                var wsResult = meuWS.TestarWebService().ToList();

                //Exibe o resultado
                foreach (var itemResult in wsResult)
                {
                    mensaje = mensaje + itemResult.SIG_UF.Trim() + "-" + itemResult.DES_UF.Trim() + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                //Em caso de erro - mostra o erro
                mensaje = ex.Message;
            }
            return mensaje;
        }       

        private string IncluirMaterial()
        {
            //Limpa o resultado antes de iniciar            

            string mensaje = string.Empty;
            try
            {
                //Cria a instância do parâmetro que será enviado para ser incluído na base de dados
                wsGerbo.RegProduto LmyRegPROD = new wsGerbo.RegProduto();

                //Traigo datos de sesiones y asigno a las variables             

                //Define - chave
                LmyRegPROD.CodEmpresa = Session["Establecimiento"].ToString();
                LmyRegPROD.TipProduto = Session["TipoProducto"].ToString();
                LmyRegPROD.CodProduto = string.Empty;

                LmyRegPROD.DesProduto =  Session["NombreItem"].ToString();
                LmyRegPROD.DesComplMat = Session["Complementaria"].ToString();

                LmyRegPROD.CodLinha = Session["linea"].ToString();
                LmyRegPROD.CodFamilia = Session["Familia"].ToString();
                LmyRegPROD.CodSubFamilia = Session["Subfamilia"].ToString();

                //Unidade de Estoque
                LmyRegPROD.SigUnidade = Session["UnidaMedida"].ToString();

                //Procedência: Comprado/Fabricado
                LmyRegPROD.TipProcedencia = Session["Procedencia"].ToString();  //C  -  F

                //Contas contábeis - Estoque e Producao
                LmyRegPROD.NumCtaEstoque = Session["NumCuentaEstoque"].ToString();
                LmyRegPROD.NumCtaProducao = Session["NumCuentaProd"].ToString();

                //@12479 09/09/2019 ES: Seta as contas
                LmyRegPROD.NumCtaICMS = Session["NumCtaICMS"].ToString(); //M3
                LmyRegPROD.NumCtaIPI = Session["NumCtaIPI"].ToString(); //M4
                LmyRegPROD.NumCtaVenda = Session["NumCtaVenda"].ToString(); //M2
                LmyRegPROD.NumCtaCustoVenda = Session["NumCtaCustoVenda"].ToString(); //M5

                //@12479 09/09/2019 ES: Código do tipo do item
                LmyRegPROD.CodTipoItem = Session["CodTipoItem"].ToString();
                
                LmyRegPROD.CodOriMercad = Session["OrigenMercadoria"].ToString();
                LmyRegPROD.NumClaFiscal = Session["ClasFiscal"].ToString();

                LmyRegPROD.FlgSeriado = Session["ControlNumSerie"].ToString();
                LmyRegPROD.FlgNumLote = Session["ControlNumLote"].ToString();
                LmyRegPROD.FlgNatureza = Session["OrdenProdNatural"].ToString();
                

                try
                {
                    //Peso - Bruto Unitário
                    decimal LValPBrutoUni = Convert.ToDecimal(Session["PesoBruto"].ToString().Replace('.', ','));
                    LmyRegPROD.ValPBrutoUni = LValPBrutoUni;
                }
                catch (Exception ex)
                {
                    mensaje = "Informe um valor válido (somente números, vírgula para casa decimal)";
                    return mensaje;
                }

                try
                {
                    //Peso - Liquido
                    decimal LValPLiqUni = Convert.ToDecimal(Session["PesoLiquido"].ToString()); //.Replace('.', ','));
                    LmyRegPROD.ValPliqUni = LValPLiqUni;
                }
                catch (Exception ex)
                {
                    mensaje = ("Informe um valor válido (somente números, vírgula para casa decimal)");
                    return mensaje;
                }

                //Invoca o método para inserir o produto
                var wsResult = meuWS.IncluirMaterial(LmyRegPROD);

                //Verifica se ocorreu erro
                if ((wsResult.DES_MSGRETORNO != null) && (wsResult.DES_MSGRETORNO.Contains("#ERRO#")))
                {
                    mensaje = "Ocorreu o erro: " + Environment.NewLine + wsResult.DES_MSGRETORNO;
                }
                else
                {
                    //Exibe o retorno do método
                    mensaje = "Produto: " + Environment.NewLine +
                                     "Estabel:" + wsResult.COD_EMPRESA.Trim() + ", Tipo:" + wsResult.TIP_PRODUTO.Trim() + ", Código:" + wsResult.COD_PRODUTO.Trim() + Environment.NewLine +
                                     "Mensagem de Retorno: " + wsResult.DES_MSGRETORNO + " </br> ";

                    Session["CodErp"] = wsResult.COD_PRODUTO.Trim();
                }
            }
            catch (Exception ex)
            {
                //Em caso de erro - mostra o erro
                mensaje = ex.Message;
            }
            //lblmensaje.Text = mensaje;
            return mensaje;
        }

        private string IncluirOP()
        {
            //Limpa o resultado antes de iniciar
            string mensaje = string.Empty;

            try
            {
                //Cria a instância do parâmetro que será enviado para ser incluído na base de dados
                wsGerbo.RegOrdemProd LmyRegOP = new wsGerbo.RegOrdemProd();

                //Seta os valores a serem enviados ao método -------

                LmyRegOP.CodEmpresa = Session["Establecimiento"].ToString();
                LmyRegOP.CodDocOP = Session["TipoDocumento"].ToString();
                LmyRegOP.TipProduto = Session["TipoProducto"].ToString();
                LmyRegOP.CodProduto = Session["IdItemGerbo"].ToString();

                try
                {
                    //Quantidade a produzir
                    decimal LQtdProduzir = Convert.ToDecimal("1".Replace('.', ','));
                    LmyRegOP.QtdProduzir = LQtdProduzir;
                }
                catch (Exception ex)
                {
                    mensaje = "Informe um valor válido (somente números, vírgula para casa decimal)";
                    return mensaje;
                }

                LmyRegOP.DatInicio = Convert.ToDateTime(DateTime.Today);
                LmyRegOP.DatNecessaria = Convert.ToDateTime(DateTime.Today);

               
                //OP Pai
                LmyRegOP.CodDocOPPai = Session["TipoDocPadre"].ToString();

                
                LmyRegOP.NumOrdemProdPai = Session["IdOpPadre"].ToString();


                //---------------------------------------------------

                //Campo informativo - nome do usuário responsável
                LmyRegOP.NomUsuarioResp = string.Empty;

                //Invoca o método para inserir a Ordem de Produção
                var wsResult = meuWS.IncluirOrdemProdMat(LmyRegOP);

                //Verifica se ocorreu erro
                if ((wsResult.DES_MSGRETORNO != null) && (wsResult.DES_MSGRETORNO.Contains("#ERRO#")))
                {
                    mensaje = "Ocorreu o erro: " + Environment.NewLine + wsResult.DES_MSGRETORNO;
                }
                else
                {
                    //Exibe o retorno do método
                    mensaje = "Ordem de Produção: " + Environment.NewLine +
                                     "Estabel:" + wsResult.COD_EMPRESA.Trim() + ", DocOP:" + wsResult.COD_DOCOP.Trim() + ", Num.OP:" + wsResult.NUM_ORDEMPROD.Trim() + Environment.NewLine +
                                     "Mensagem de Retorno: " + wsResult.DES_MSGRETORNO;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
        }

        private string IncluirReqPlanej()
        {
            //Limpa o resultado antes de iniciar
            string mensaje = string.Empty;
            
            try
            {
                //Cria a instância do parâmetro que será enviado para ser incluído na base de dados
                wsGerbo.RegRequisicao LmyRegReqPlan = new wsGerbo.RegRequisicao();

                //Seta os valores a serem enviados ao método -------
                LmyRegReqPlan.CodEmpresa = Session["Establecimiento"].ToString();
                LmyRegReqPlan.CodDocOP = Session["TipoOp"].ToString();
                LmyRegReqPlan.NumOrdemProd = Session["Op"].ToString();
                LmyRegReqPlan.CodMaterial = Session["IdItemGerbo"].ToString();
                LmyRegReqPlan.CodCCustoReq = Session["Ccostos"].ToString();
                LmyRegReqPlan.DatNecessaria = Convert.ToDateTime(DateTime.Today);
                string unidadMedida = Session["UnidadMedida"].ToString();               

                try
                {
                    decimal LQtdNecessaria = 0;

                    if (Session["OpRayaAcc"] == null)

                        //Quantidade - necessária
                        LQtdNecessaria = Convert.ToDecimal(Session["CantPeso"].ToString().Trim());   //.Replace('.', ','));
                    //decimal LQtdNecessaria = Convert.ToDecimal(edtReqPlan_QtdNecessaria.Text.Trim().Replace('.', ','));
                    if (unidadMedida == "UND")
                    {

                        decimal cant = Convert.ToDecimal(Session["CantPeso"].ToString().Trim().Replace('.', ',')) / 100;
                        LmyRegReqPlan.QtdNecessaria = cant;
                    }
                    else
                    {
                        LQtdNecessaria = Convert.ToDecimal(Session["CantPeso"].ToString().Trim());
                        LmyRegReqPlan.QtdNecessaria = LQtdNecessaria;
                    }

                    decimal a  = LQtdNecessaria;


                    //if (Session["OpRayaAcc"] == null)

                    //    LQtdNecessaria = Convert.ToDecimal(Session["CantPeso"].ToString().Trim());

                    //    //Quantidade - necessária
                    //    //decimal LQtdNecessaria = Convert.ToDecimal(edtReqPlan_QtdNecessaria.Text.Trim().Replace('.', ','));
                    //    //if (unidadMedida == "UND")
                    //    //    LmyRegReqPlan.QtdNecessaria = pesoInt; 
                    //    //else
                    //    //else
                    //    //LQtdNecessaria = Convert.ToDecimal(Session["CantPeso"].ToString().Trim().Replace('.', ',')) ;

                    //LmyRegReqPlan.QtdNecessaria = LQtdNecessaria;
                }
                catch (Exception ex)
                {
                    mensaje = "Informe um valor válido (somente números, vírgula para casa decimal)";
                    return (mensaje);
                }
                //---------------------------------------------------


                Session["DatosEnviados"]="Empresa=" + Session["Establecimiento"].ToString() + " CodDocOP=" + Session["TipoOp"].ToString()
                + " NumOrdemProd=" + Session["Op"].ToString() 
                +"CodMaterial=" + Session["IdItemGerbo"].ToString() 
                +" CodCCustoReq=" + Session["Ccostos"].ToString() 
                +" DatNecessaria=" + Convert.ToDateTime(DateTime.Today) 
                +" QtdNecessaria=" + Convert.ToDecimal(Session["CantPeso"].ToString());

                //Invoca o método para inserir a Ordem de Produção
                var wsResult = meuWS.IncluirReqPlanejada(LmyRegReqPlan);

                //Verifica se ocorreu erro
                if ((wsResult.DES_MSGRETORNO != null) && (wsResult.DES_MSGRETORNO.Contains("#ERRO#")))
                {
                   mensaje = "Ocorreu o erro: " + Environment.NewLine + wsResult.DES_MSGRETORNO + " Cod: " + Session["IdItemGerbo"].ToString(); ;
                }
                else
                {
                        //Exibe o retorno do método
                        mensaje = "Ordem de Produção: " + Environment.NewLine +
                                     "Estabel:" + wsResult.COD_EMPRESA.Trim() + ", DocReq:" + wsResult.COD_DOCRQ.Trim() + ", Num.Req:" + wsResult.NUM_REQMATERIAL.Trim() + Environment.NewLine +
                                     "Mensagem de Retorno: " + wsResult.DES_MSGRETORNO + " </br> ";
                       int  IdRequisicion = Convert.ToInt32(wsResult.NUM_REQMATERIAL.Trim());

                    if (IdRequisicion > 0) Session["IdRequisicion1"] = IdRequisicion; else Session["IdRequisicion1"] = 0;
                }
            }
            catch (Exception ex)
            {
                    mensaje = ex.Message;
            }
            return (mensaje);
        }

        // consulto la raya para construir el nombre del item en forsa
        public string consultarNombreItemNivel(int IdRaya, int nivel)
        {
            string NombreItemNivel = "";
            reader = controlgerbo.ConsultarNombreItemForsa(IdRaya, nivel);

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    NombreItemNivel = reader.GetString(0).Trim();
                    Session["NombreItem"] = reader.GetString(0).Trim();
                    Session["Establecimiento"] = reader.GetString(1);
                    Session["TipoProducto"] = reader.GetString(2).Trim();
                    Session["linea"] = reader.GetInt32(3);
                    Session["Familia"] = reader.GetInt32(4);
                    Session["Subfamilia"] = reader.GetInt32(5);
                    Session["UnidaMedida"] = reader.GetString(6).Trim();
                    Session["Procedencia"] = reader.GetString(7).Trim();
                    Session["NumCuentaEstoque"] = reader.GetString(8).Trim();
                    Session["NumCuentaProd"] = reader.GetString(9).Trim();
                    Session["OrigenMercadoria"] = reader.GetString(10).Trim();
                    Session["ClasFiscal"] = reader.GetString(11).Trim();
                    Session["ControlNumSerie"] = reader.GetString(12).Trim();
                    Session["ControlNumLote"] = reader.GetString(13).Trim();
                    Session["OrdenProdNatural"] = reader.GetString(14).Trim();
                    Session["PesoBruto"] = reader.GetString(15).Trim();
                    Session["PesoLiquido"] = reader.GetString(16).Trim();
                    Session["TipoDocumento"] = reader.GetString(17).Trim();
                    Session["TipoDocumentoPadre"] = reader.GetString(18).Trim();
                    Session["UnidadNegocio"] = reader.GetInt32(19);

                    Session["NumCtaICMS"] = reader.GetString(20).Trim();
                    Session["NumCtaIPI"] = reader.GetString(21).Trim();
                    Session["NumCtaVenda"] = reader.GetString(22).Trim();
                    Session["NumCtaCustoVenda"] = reader.GetString(23).Trim();
                    Session["CodTipoItem"] = reader.GetString(24).Trim();
                    

                    // evaluo la raya en nivel 3 que es la de orden para capturarla unidad de negocio para accesorios
                    if (nivel == 3)
                    { 
                        Session["UnidadNegocioRaya"] = reader.GetInt32(19);
                        Session["OpRayaAcc"] =  reader.GetInt32(25);
                        Session["TipoOrden"] = reader.GetString(26).Trim();
                        Session["Complementaria"] = "";
                    }

                    if (nivel == 1)
                    {
                        Session["Complementaria"] = "Fup: " + reader.GetInt32(27).ToString();
                    }
                    else
                    {
                        Session["Complementaria"] = ".";
                    }

                }
            }
            reader.Close();
            reader.Dispose();
            controlgerbo.CerrarConexion();

            return NombreItemNivel;
        }

        // consulto la raya para construir el nombre del item en forsa
        public string consultarItemsDatos(int IdItemPlanta)
        {
            string NombreItemNivel = "";
            reader = controlgerbo.ConsultarItemsDatosBd(IdItemPlanta);

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    NombreItemNivel = reader.GetString(0).Trim();
                    Session["NombreItem"] = reader.GetString(0).Trim();
                    Session["Establecimiento"] = reader.GetString(1);
                    Session["TipoProducto"] = reader.GetString(2).Trim();
                    Session["linea"] = reader.GetInt32(3);
                    Session["Familia"] = reader.GetInt32(4);
                    Session["Subfamilia"] = reader.GetInt32(5);
                    Session["UnidaMedida"] = reader.GetString(6).Trim();
                    Session["Procedencia"] = reader.GetString(7).Trim();
                    Session["NumCuentaEstoque"] = reader.GetString(8).Trim();
                    Session["NumCuentaProd"] = reader.GetString(9).Trim();
                    Session["OrigenMercadoria"] = reader.GetString(10).Trim();
                    Session["ClasFiscal"] = reader.GetString(11).Trim();
                    Session["ControlNumSerie"] = reader.GetString(12).Trim();
                    Session["ControlNumLote"] = reader.GetString(13).Trim();
                    Session["OrdenProdNatural"] = reader.GetString(14).Trim();
                    Session["PesoBruto"] = reader.GetString(15).Trim();
                    Session["PesoLiquido"] = reader.GetString(16).Trim();
                    Session["TipoDocumento"] = reader.GetString(17).Trim();
                    Session["TipoDocumentoPadre"] = reader.GetString(18).Trim();
                    Session["UnidadNegocio"] = reader.GetInt32(19);

                    Session["NumCtaICMS"] = reader.GetString(20).Trim();
                    Session["NumCtaIPI"] = reader.GetString(21).Trim();
                    Session["NumCtaVenda"] = reader.GetString(22).Trim();
                    Session["NumCtaCustoVenda"] = reader.GetString(23).Trim();
                    Session["CodTipoItem"] = reader.GetString(24).Trim();

                    Session["Complementaria"] = "Creado desde SIO";                   

                }
            }
            reader.Close();
            reader.Dispose();
            controlgerbo.CerrarConexion();

            return NombreItemNivel;
        }

        // consulto la raya para construir el nombre del item en forsa
        public string consultarNombreItemNivelAnterior(int IdRaya, int nivel)
        {
            string NombreItemNivel = "", NombreItemAnterior = "";
            reader = controlgerbo.ConsultarNombreItemForsa(IdRaya, nivel);

            if (reader.HasRows == true)
            {
                while (reader.Read())
                { 
                    NombreItemAnterior = reader.GetString(0).Trim();
                }
            }
            reader.Close();
            reader.Dispose();
            controlgerbo.CerrarConexion();

            return NombreItemAnterior;
        }

        // consulto el item en gerbo con el nombre construido
        public string consultarItemxRayaGerbo(string nombreItem)
        {
            string IdItem = "";
            reader = controlgerbo.ConsultarItemGerbo(nombreItem);
            
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    IdItem =  reader.GetString(0).Trim();
                }
            }
            reader.Close();
            reader.Dispose();
            controlgerbo.CerrarConexion();

            return IdItem;
        }

        // consulto la op en gerbo con el Iditemgerbo
        public string consultarOpxRayaGerbo(string idItemGerbo)
        {
            string IdOp = "";
            reader = controlgerbo.ConsultarIdOp(idItemGerbo);

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    IdOp = reader.GetString(2).Trim();
                }
            }
            reader.Close();
            reader.Dispose();
            controlgerbo.CerrarConexion();

            return IdOp;
        }

        // consulto la op en gerbo con el nombre del item del nivel anterior
        public string consultarOpPadrexNombreItem(string nombreItemGerbo)
        {
            string IdOp = "", TipoDocPadre = "";
            reader = controlgerbo.ConsultarIdOpPdre(nombreItemGerbo);

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    IdOp = reader.GetString(2).Trim();
                    TipoDocPadre = reader.GetString(1).Trim();
                    Session["IdOpPadre"] = IdOp;
                    Session["TipoDocPadre"] = TipoDocPadre;
                }
            }
            reader.Close();
            reader.Dispose();
            controlgerbo.CerrarConexion();

            return IdOp;
        }

        // actualizo en orden seg el itemabuelo y op abuelo
        public int actualizaItemEnForsa(int nivel,int idRaya, int IdItemgerbo)
        {
            int unidadNegocio = Convert.ToInt32( Session["UnidadNegocio"].ToString());

            int actualizar = 0;

            // evaluo si es item abuelo
            if (nivel == 1)
            {
                actualizar = controlgerbo.actualizarItemAbueloEnForsa(idRaya, IdItemgerbo);
            }
            // evaluo si es item od
            if (nivel == 2)
            {
                actualizar = controlgerbo.actualizarItemOdEnForsa(idRaya, IdItemgerbo, unidadNegocio);
            }
            // evaluo si es item OP
            if (nivel == 3)
            {
                actualizar = controlgerbo.actualizarItemOpEnForsa(idRaya, IdItemgerbo);
            }

            return actualizar;
        }

        // actualizo en itemplanta y accesorioscodigo
        public int actualizaItemPlantaAccCodigo(int IdItemPlanta, int CodErp)
        {
            int actualizar = 0;
            
            actualizar = controlgerbo.actualizarItemPlantaAccCodBd(IdItemPlanta, CodErp);           

            return actualizar;
        }

        // actualizo en mpsoldet el id de la requsicion
        public int actualizaReqMpSoldet(int IdDetalle, int IdReq, decimal valorEnviado, int IdMpSol)
        {
            int actualizar = 0;      
                 
            actualizar = controlgerbo.actualizarReqEnMpSolDet(IdDetalle, IdReq, valorEnviado, IdMpSol);           

            return actualizar;
        }

        // actualizo en ofacc el id de la requsicion
        public int actualizaReqOfAcc(int IdDetalle, int IdReq)
        {
            int actualizar = 0;

            actualizar = controlgerbo.actualizarReqEnOfAcc(IdDetalle, IdReq);

            return actualizar;
        }

        // actualizo en mpsol el id de la requsicion
        public int actualizaReqMpSol( int IdReq, int IdMpSol)
        {
            int actualizar = 0;

            actualizar = controlgerbo.actualizarReqEnMpSol( IdReq,  IdMpSol);

            return actualizar;
        }

        // actualizo FechaApro en mpsol el id de la requsicion
        public int actualizarFechaAprobaEnMpSol(int IdReq, int IdMpSol)
        {
            int actualizar = 0;

            actualizar = controlgerbo.actualizarFechaAprobaEnMpSol(IdReq, IdMpSol);

            return actualizar;
        }

        // actualizo en orden seg el itemabuelo y op abuelo
        public int actualizaOpEnForsa(int nivel, int idRaya, int IdOp)
        {
            int unidadNegocio = Convert.ToInt32(Session["UnidadNegocio"].ToString());

            int actualizar = 0;

            // evaluo si es item abuelo
            if (nivel == 1)
            {
                actualizar = controlgerbo.actualizarOpAbueloEnForsa(idRaya, IdOp);
            }
            // evaluo si es item od
            if (nivel == 2)
            {
                actualizar = controlgerbo.actualizarOpOdEnForsa(idRaya, IdOp, unidadNegocio);
            }
            // evaluo si es item OP
            if (nivel == 3)
            {
                actualizar = controlgerbo.actualizarOpEnForsa(idRaya, IdOp);
            }

            return actualizar;
        }

        // actualizo en gerbolog elresultado del webservices
        public int actualizaLogGerboEnForsa(String datosLog)
        {
            int actualizar = 0;
            string Id = "0";
            string Metodo = Session["Metodo"].ToString();


            if (Request.QueryString["IdMpSol"] != null) Id = Request.QueryString["IdMpSol"].ToString();
            else
                if (Request.QueryString["IdRaya"] != null) Id = Request.QueryString["IdRaya"].ToString();

            actualizar = controlgerbo.actualizarLogWsGerboEnForsa(datosLog,Convert.ToInt32( Id), Metodo);
            
            return actualizar;
        }

        // consulto la raya para construir la requesision 
        public string consultarSolMpForsa(int IdMpSol)
        {
            string Resultado = "";
            reader = controlgerbo.consultarSolicMpForsa(IdMpSol);
           
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    Session["Establecimiento"] = reader.GetInt32(0);
                    Session["TipoOp"] = reader.GetString(1).Trim();
                    Session["Op"] = reader.GetInt32(2);
                    Session["IdItemGerbo"] = reader.GetInt32(3);
                    Session["Ccostos"] = reader.GetInt32(4);
                    Session["CantPeso"] = reader.GetSqlDecimal(6);
                    Session["IdDetalle"] = reader.GetSqlInt32(8);
                    Session["UnidadMedida"] = reader.GetString(9);
                    Session["CantPeso"] = reader.GetSqlDecimal(6);                   

                    int Iddetalle = Convert.ToInt32(reader.GetValue(8));

                    // ejecuto el metodo de incluir mp
                    RespuestaGlobal = RespuestaGlobal + ejecutaMetodoWsGerbo("4");                   
                    int IdReq1 = Convert.ToInt32(Session["IdRequisicion1"]);
                    decimal valorEnviado = 0;
                    valorEnviado = Convert.ToDecimal(Session["CantPeso"].ToString());
                    
                    actualizaReqMpSoldet(Iddetalle, IdReq1, valorEnviado, IdMpSol);
                }
            }           

            reader.Close();
            reader.Dispose();
            controlgerbo.CerrarConexion();

            return RespuestaGlobal;
        }

        // consulto la raya para construir la requesision 
        public string ejecutaRequesicionAcc(int Idraya)
        {
            string Resultado = "";
            reader = controlgerbo.consultarAccRaya(Idraya);

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    Session["Establecimiento"] = reader.GetInt32(0);
                    Session["TipoOp"] = reader.GetString(1).Trim();
                    Session["Op"] = reader.GetInt32(2);
                    Session["IdItemGerbo"] = reader.GetInt32(3);
                    Session["Ccostos"] = reader.GetInt32(4);
                    Session["CantPeso"] = reader.GetInt32(6);
                    Session["IdDetalle"] = reader.GetSqlInt32(8);
                    //Session["UnidadMedida"] = reader.GetString(9);
                    Session["CantPeso"] = reader.GetInt32(6);

                    int Iddetalle = Convert.ToInt32(reader.GetValue(8));

                    // ejecuto el metodo de incluir mp
                    RespuestaGlobal = RespuestaGlobal + ejecutaMetodoWsGerbo("4");
                    int IdReq1 = Convert.ToInt32(Session["IdRequisicion1"]);
                    decimal valorEnviado = 0;
                    valorEnviado = Convert.ToDecimal(Session["CantPeso"].ToString());

                    actualizaReqOfAcc(Iddetalle, IdReq1);
                }
            }

            reader.Close();
            reader.Dispose();
            controlgerbo.CerrarConexion();

            return RespuestaGlobal;
        }

        // consulto la raya para construir el nombre del item en forsa
        public int consultarCantSolMpForsa(int IdMpSol)
        {
            int cantComponentes = 0;
            reader = controlgerbo.consultarCantCompSolMp(IdMpSol);

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cantComponentes = reader.GetInt32(0);
                    Session["OpMpSol"] = reader.GetInt32(1);
                    Session["Ofa"] = reader.GetSqlString(2);
                }
            }
            reader.Close();
            reader.Dispose();
            controlgerbo.CerrarConexion();

            return cantComponentes;
        }

        // consulto los componentes que se han solicitado con requisicion
        public int consultarCantEnviadaSolMpForsa(int IdMpSol)
        {
            int cantComponentesEnviado = 0;
            reader = controlgerbo.consultarCantSolicitadaCompSolMp(IdMpSol);

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cantComponentesEnviado = reader.GetInt32(0);                    
                }
            }
            else
            {
                cantComponentesEnviado = 0;
            }
            reader.Close();
            reader.Dispose();
            controlgerbo.CerrarConexion();

            return cantComponentesEnviado;
        }

        // consulto los faltantes de acceosrios para requiesicion
        public int consultarCantFaltaAccReq(int IdRayaAcc)
        {
            int cantComponentesEnviado = 0;
            reader = controlgerbo.consultarFaltAccReq(IdRayaAcc);

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cantComponentesEnviado = reader.GetInt32(0);
                }
            }
            else
            {
                cantComponentesEnviado = 0;
            }
            reader.Close();
            reader.Dispose();
            controlgerbo.CerrarConexion();

            return cantComponentesEnviado;
        }


        // consulto la cantidad de items a crear en acc codigo con codigo erp en -1
        public int consultarCantItemsCrear()
        {
            int cantItemsCrear = 0;
            reader = controlgerbo.consultarCantItemsCrearBd();

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cantItemsCrear = reader.GetInt32(0);
                }
            }
            else
            {
                cantItemsCrear = 0;
            }
            reader.Close();
            reader.Dispose();
            controlgerbo.CerrarConexion();

            return cantItemsCrear;
        }

        // consulto la cantidad de items a crear en acc codigo con codigo erp en -1
        public int consultarItemsItemPlantaCrear()
        {
            int cantItemsCrear = 0;
            reader = controlgerbo.consultarItemsItemPlantaBd();

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cantItemsCrear = reader.GetInt32(0);
                }
            }
            else
            {
                cantItemsCrear = 0;
            }
            reader.Close();
            reader.Dispose();
            controlgerbo.CerrarConexion();

            return cantItemsCrear;
        }

    }

}
