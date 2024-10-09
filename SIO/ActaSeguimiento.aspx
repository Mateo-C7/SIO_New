<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralAmplia.Master" AutoEventWireup="true"
    CodeBehind="ActaSeguimiento.aspx.cs" Inherits="SIO.ActaSeguimiento" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="STYLESHEET" type="text/css" href="codebase/dhtmlxgrid.css">
    <link rel="stylesheet" type="text/css" href="codebase/skins/dhtmlxgrid_dhx_skyblue.css">
    <link rel="STYLESHEET" type="text/css" href="codebase/dhtmlxcombo.css">
    <link rel="STYLESHEET" type="text/css" href="codebase/dhtmlxcalendar.css">
    <link rel="STYLESHEET" type="text/css" href="codebase/skins/dhtmlxcalendar_dhx_skyblue.css">
    <script type='text/javascript' src="codebase/dhtmlx.js"></script>
    <script type='text/javascript' src="codebase/dhtmlxcommon.js"></script>
    <script type='text/javascript' src="codebase/dhtmlxcombo.js"></script>
    <script type='text/javascript' src="codebase/dhtmlxcalendar.js"></script>
    <script type='text/javascript' src="codebase/dhtmlxgrid.js"></script>
    <script type='text/javascript' src="codebase/dhtmlxgridcell.js"></script>
    <script type='text/javascript' src="codebase/ext/dhtmlxgrid_splt.js"></script>
    <script type='text/javascript' src="codebase/ext/dhtmlxgrid_filter.js"></script>
    <script type='text/javascript' src="codebase/ext/dhtmlxgrid_srnd.js"></script>
    <script type='text/javascript' src="codebase/excells/dhtmlxgrid_excell_dhxcalendar.js"></script>
    <script type='text/javascript' src="codebase/excells/dhtmlxgrid_excell_combo.js"></script>
    <script type='text/javascript' src="codebase/excells/dhtmlxgrid_excell_link.js"></script>
    <script type='text/javascript' src="codebase/dhtmlxdataprocessor.js"></script>
    <script type='text/javascript' src="codebase/connector.js"></script>
    <style  type="text/css">
        input#txtFecIni, input#txtFecFin, input#a12f, input#a12fpc
        {
            font-family: Tahoma;
            font-size: 12px;
            background-color: #fafafa;
            border: #c0c0c0 1px solid;
            width: 80px;
        }
        span.label
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        #txtFIni
        {
            width: 70px;
        }
        #a12a
        {
            width: 54px;
        }
        .auto-style2 {
            width: 26px;
        }
        .auto-style3 {
            width: 12px;
        }
        .auto-style4 {
            width: 84px;
        }
        .auto-style5 {
            width: 3px;
        }
        .auto-style6 {
            width: 70px;
        }
        .auto-style7 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 8pt;
        }
        .auto-style8 {
            width: 128%;
        }
        .auto-style9 {
            width: 513px;
            height: 25px;
        }
        .auto-style10 {
            width: 316px;
        }
        .auto-style11 {
            width: 181px;
        }
        .auto-style12 {
            height: 52px;
        }
    </style>
   
    <fieldset  class="auto-style9" style="border-style: solid; border-color: #F2F2F2; border-width: inherit"  >
       
        <table class="auto-style8">
            <tr>
                <td class="auto-style10">
         <input id="txtFecIni" name="txtFecIni" type="text" style="display:none ; font-family: Arial, Helvetica, sans-serif;
            font-size: 8pt; width: 55px;" onclick="setSens('txtFecFin', 'max');"  />
        &nbsp;
        <input id="txtFecFin" name="txtFecFin" type="text" style="display:none ; font-family: Arial, Helvetica, sans-serif;
            font-size: 8pt; width: 55px;" onclick="setSens('txtFecIni', 'min');"  />
        &nbsp;
        <label id='LTrm' style="display:none ; font-family: Arial, Helvetica, sans-serif; font-size: 8pt"> TRM</label>
        
        <input type="text" name="txtTRM" value="2900"  id="txtTRM" size="6" style="display:none ; font-family: Arial, Helvetica, sans-serif;
            font-size: 8pt;"  /> 
        
        
    
        
        <input type="hidden" id="Usu" name="Usu" runat="server" />
        <input type="hidden" id="Rol" name="Rol" runat="server" />
    
    <label>Ver Perdidos </label>
      <label><input type="checkbox" id="chkPerdido" onclick="valuecheck(this)"/></label><input type="button" name="a14" value="Cargar" id="a14" onclick='grActSeg.clearAll(); LoadData(byId("txtFecIni").value,
            byId("txtFecFin").value, byId("txtTRM").value, byId("chkPerdido").value);'
            style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; color: #FFFFFF;
            background-color: #1C5AB6;  width: 54px;" /></td>
                <td class="auto-style11">
                                  <asp:LinkButton ID="linkDespachos" runat="server" 
                        Font-Names="Arial" Font-Size="8pt"
                                                        OnClick="LinkButton1_Click">PlanDespachos</asp:LinkButton></td>
                <td>
                                      &nbsp;&nbsp;&nbsp;
                                      <asp:LinkButton ID="lkActualizar" runat="server" 
                        Font-Names="Arial" Font-Size="8pt" onclick="lkActualizar_Click"
                                                         >ACTUALIZAR</asp:LinkButton>              
                                                   </td>
            </tr>
        </table>
    </fieldset>

    <script>
        

        Date.prototype.yyyymmdd = function () {

            var yyyy = this.getFullYear().toString();
            var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based        
            var dd = this.getDate().toString();

            return yyyy + '-' + (mm[1] ? mm : "0" + mm[0]) + '-' + (dd[1] ? dd : "0" + dd[0]);
        };
        var myCalendar;
        var cmbZona;
        function doOnLoad() {
            // init values
            var from = new Date();
            var to = new Date();

            cmbZona = new dhtmlXCombo("a12b", "combo", 110);
            cmbPais = new dhtmlXCombo("a12c", "combo2", 130);
            cmbProb = new dhtmlXCombo("a12d", "combo3", 80);
            cmbZonaCiu = new dhtmlXCombo("a12g", "combo4", 110);
            cmbEstatus = new dhtmlXCombo("estatus", "combo5", 100);
            cmbPlanta = new dhtmlXCombo("planta", "combo6", 100);
            cmbPlantaPlan = new dhtmlXCombo("plantaPlan", "combo7", 100);
            cmbPlantaComercial = new dhtmlXCombo("plantaComercial", "combo9", 100);
            cmbTipoNegocio = new dhtmlXCombo("a12abc", "combo8", 100);
            cmbReserva = new dhtmlXCombo("reserva", "combo10", 50);
           

            cmbZona.loadXML("DatosFiltro.ashx?Tipo=Zona");
            //cmbZona.enableOptionAutoPositioning(true);
            //cmbZona.enableFilteringMode(true);
            
            cmbZona.attachEvent("onChange", function (value) {
                cmbPais.clearAll();
                var z = cmbZona.getSelectedValue();
                cmbPais.setComboValue(null);
                cmbPais.setComboText("");
                cmbPais.loadXML("DatosFiltro.ashx?Tipo=Pais&grupo_Id=" + z);
            });   
            
            //cmbZona.enableMultiselect(true);

            cmbPais.attachEvent("onChange", function (value) {
                cmbZonaCiu.clearAll();
                var p = cmbPais.getSelectedValue();
                cmbZonaCiu.setComboValue(null);
                cmbZonaCiu.setComboText("");
                cmbZonaCiu.loadXML("DatosFiltro.ashx?Tipo=ZonaCiu&pai_Id=" + p);
            });
            cmbZona.enableFilteringMode(false);
            cmbPais.enableFilteringMode(false);
            cmbZonaCiu.enableFilteringMode(false);
            cmbEstatus.enableFilteringMode(false);
            cmbProb.loadXML("DatosFiltro.ashx?Tipo=ProbCi");
            cmbEstatus.loadXML("DatosFiltro.ashx?Tipo=Estatus");
            cmbPlanta.loadXML("DatosFiltro.ashx?Tipo=Planta"); 
            cmbPlantaPlan.loadXML("DatosFiltro.ashx?Tipo=PlantaPlan");
            cmbPlantaComercial.loadXML("DatosFiltro.ashx?Tipo=PlantaComercial"); 
            cmbTipoNegocio.loadXML("DatosFiltro.ashx?Tipo=TipoNegociox");
            cmbReserva.loadXML("DatosFiltro.ashx?Tipo=SiNoReserva");
      


            //            from.setDate(to.getDate() - 365);
            from.setDate(to.getDate());

            myCalendar = new dhtmlXCalendarObject(["txtFecIni", "txtFecFin", "a12f","a13g", "a12fpc"]);
            myCalendar.setSkin('dhx_skyblue');
            myCalendar.setDate(from.yyyymmdd());
            myCalendar.hideTime();
//            byId("txtFecIni").value = from.yyyymmdd();
            
        }

        function setSens(id, k) {
            // update range
            if (k == "min") {
                myCalendar.setSensitiveRange(byId(id).value, null);
            } else {
                myCalendar.setSensitiveRange(null, byId(id).value);
            }
        }
        function byId(id) {
            return document.getElementById(id);
        }
        function tomarMesProd(id) {
    
            var mes = document.getElementById(id).value;
            mes = mes.substring(0,7);
            return document.getElementById("a13g1").value = mes;                      
        }
        function tomarMes(id) {
    
            var mes = document.getElementById(id).value;
            mes = mes.substring(0,7);
            return document.getElementById("a12f1").value = mes;          
            
        }
        function tomarMesProdCom(id) {
    
            var mes = document.getElementById(id).value;
            mes = mes.substring(0,7);
            return document.getElementById("a12f1pc").value = mes;          
            
        }

    </script>
    
    <fieldset style="border-style: none; border-width: 0px; width: 1153px">
        <table class="auto-style12">
            <tr>
                <td>
                    &nbsp;<label id='a10a' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        FUP/OF</label>
                </td>
                <td>
                    <input type="text" name="a12a" value="" id="a12a" size="4"/>
                </td>
                
                <td style="text-align:right">
                    <label id='a10b' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; text-align: left">
                        Zona</label></td>
                <td>
                    <div id="a12b" name="a12b" >
                    </div>
                </td>
                <td style="text-align:right" class="auto-style1">
                    <label id='a10c' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        Pais</label></td>
                <td>
                    <div id="a12c" name="a12c">
                    </div>
                </td>
                <td style="text-align:right" class="auto-style1">
                    <label id='a10d' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        SubZ</label></td>
                <td>
                    <div id="a12g" name="a12g">
                    </div>
                </td>
               <td style="text-align:right" class="auto-style1">
                    <label id='a10g' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        Prob</label></td>
                <td>
                    <div id="a12d" name="a12d" >
                    </div>
                </td>
                
                <td>
                    &nbsp;<label id='a10f' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">MesFact</label>
                </td>
                <td class="auto-style4">
                    <input id="a12f" name="txtMes" type="text" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 40px;"  />
                        <input id="a12f1" name="txtMes" type="hidden" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 48px;"  />
                </td>  
                <td>
                    <label id='Label2' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        Estatus
                    </label>
                    &nbsp;<label id='Label1' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        </label>
                </td>  
                <td>
                    <div id="estatus" name="estatus">
                    </div>
                    
                </td>  
                 <td>
                    &nbsp;<label id='a10e' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        Vendedor</label>
                </td>
                <td>
                    <input type="text" name="a12e" value="" id="a12e" size="20" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 60px;"/>
                </td>
                              <td style="text-align:right">
                                  &nbsp;<label id='a13a' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt" >
                       </label>
                </td>
                <td>
                    
                    </div>
                </td>  
                <td style="text-align:right" class="auto-style5">
                    &nbsp;</td>
                <td style="text-align:right">
                    &nbsp;</td>                          
            </tr>
            <tr>     <td>
                    &nbsp;<label id='a10g' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">MesProd</label>
                </td>
                <td>
                    <input id="a13g" name="txtMesProd" type="text" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 40px;"  />
                        <input id="a13g1" name="txtMesProd" type="hidden" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 48px;"  />
                </td> 
                    <td style="text-align:right">
                        <label id='a13a0' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt" >
                       Planta</label></td>
                    <td style="text-align:right">
                        <div id="planta" name="planta"> &nbsp;</td> 
                 <td style="text-align:right">
                     <label id='a13ab' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt" >
                       PlantaPlan
                    </label>
                </td>
                <td>
                    <div id="plantaPlan" name="plantaPlan">
                    </div>
                </td>  

                 <td style="text-align:right">
                     <label id='a13abx' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt" >
                       PlantaComer
                    </label>
                </td>
                <td>
                    <div id="plantaComercial" name="plantaComercial">
                    </div>
                </td>  
                 <td style="text-align:right" class="auto-style2">
                     <label id='a13abc' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt" >
                       TipoNegoc
                    </label>
                </td>
                <td>
                     <div id="a12abc" name="a12abc" >
                    </div>
                </td> 
                <td class="auto-style3">
                    &nbsp;<label id='a10f' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">MesProdCom</label>
                </td>
                <td>
                    <input id="a12fpc" name="txtMespc" type="text" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 40px;"  />
                        <input id="a12f1pc" name="txtMe´pc" type="hidden" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 48px;"  />
                </td>                    
                <td>                
                    <label id='a10f0' class="auto-style7">Reserva</label>
                </td>
                <td>
                     <div id="reserva" name="reserva">
                    </div>
                </td>
                <td class="auto-style4">
                                      &nbsp;</td>
                <td>                
                    <input type="button" name="a11" value="Filtrar" id="a11" onclick='
                    tomarMes("a12f");
                    tomarMesProd("a13g");
                    tomarMesProdCom("a12fpc");
                        grActSeg.filterBy(0,"");
                        if (document.getElementById("a12a").value != ""){
                          grActSeg.filterBy(3,document.getElementById("a12a").value,true);
                        }                        
                        if (cmbZona.getComboText() != ""){
                        grActSeg.filterBy(0,cmbZona.getComboText(),true);
                        }
                        if (cmbPais.getComboText() != ""){
                        grActSeg.filterBy(1,cmbPais.getComboText(),true);
                        }
                        if (cmbProb.getComboText() != ""){    
                        grActSeg.filterBy(7,cmbProb.getComboText(),true);
                        }
                        if (cmbEstatus.getComboText() != ""){    
                        grActSeg.filterBy(14,cmbEstatus.getComboText(),true);
                        }
                        if (cmbReserva.getComboText() != ""){    
                        grActSeg.filterBy(59,cmbReserva.getComboText(),true);
                        }
                        if (document.getElementById("a12e").value != ""){            
                        grActSeg.filterBy(33,document.getElementById("a12e").value,true);
                        }
                        if (document.getElementById("a12f1").value != ""){            
                        grActSeg.filterBy(8,document.getElementById("a12f1").value,true);
                        }
                        if (document.getElementById("a12f1pc").value != ""){            
                            grActSeg.filterBy(17,document.getElementById("a12f1pc").value,true);
                        }
                        if (document.getElementById("a13g1").value != ""){            
                            grActSeg.filterBy(18,document.getElementById("a13g1").value,true);
                        }
                        if (cmbZonaCiu.getComboText() != ""){    
                        grActSeg.filterBy(2,cmbZonaCiu.getComboText(),true);                        
                        }                     
                        if (cmbPlanta.getComboText() != ""){    
                            grActSeg.filterBy(48,cmbPlanta.getComboText(),true);
                        }      
                        if (cmbPlantaPlan.getComboText() != ""){    
                            grActSeg.filterBy(49,cmbPlantaPlan.getComboText(),true);
                        }   
                        if (cmbPlantaComercial.getComboText() != ""){    
                            grActSeg.filterBy(47,cmbPlantaComercial.getComboText(),true);
                        } 
                        if (cmbTipoNegocio.getComboText() != ""){    
                            grActSeg.filterBy(56,cmbTipoNegocio.getComboText(),true);
                        } 
                        calculateFooterValues();'
                        style="font-family: Arial, Helvetica, sans-serif;  font-size: 9pt; color: #FFFFFF;
                        background-color: #1C5AB6; width: 65px;"/></td>
                <td class="auto-style5">
                                      &nbsp;</td>
                <td class="auto-style6">
                                                   </td>
                <td style="text-align:right" class="auto-style5">
                    &nbsp;</td>
                <td style="text-align:right">
                    &nbsp;</td>  

            </tr>

            
        </table>
    </fieldset>
   
    <div id="gridbox" 
        style="width: 1290px; height: 340px; background-color: white;">
    </div>
    <table>
        <tr>
            <td>
                <div id="gridbox2" style="width: 1290px; height: 120px; background-color: white;">
                </div>                
            </td>
            

            
        </tr>
        <tr>
            <td>
            <input type="button" name="add" id="add" runat="server" value="Adicionar Observacion"
                    onclick="AddObservacion()" style="font-family: Arial, Helvetica, sans-serif;
                    font-size: 9pt; color: #FFFFFF; background-color: #1C5AB6; 
                    width: 141px;" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <div id="gridbox4" style="width: 700px; height: 120px; background-color: white;">
                </div>                
            </td>
            <td>
                <div id="gridbox3" style="width: 570px; height: 120px; background-color: white; display:none;">
                </div>
            </td>
        </tr>
    </table>
    <%--Variable De Session--%>
    <script>
//    function valuecheck(check)
//    {        
//    alert('El check '+check.name+' tiene el valor '+check.value);
//    }


        dhx_globalImgPath = "codebase/imgs/";
        grActSeg = new dhtmlXGridObject('gridbox');
        grActSeg.setImagePath("codebase/imgs/");
        grActSeg.setNumberFormat("0,000", 7, ".", ",");
        grActSeg.setNumberFormat("0,000", 8, ".", ",");
        grActSeg.setNumberFormat("0,000", 9, ".", ",");
        grActSeg.setNumberFormat("0,000", 11, ".", ",");
        grActSeg.setNumberFormat("0,000", 12, ".", ",");
        grActSeg.setNumberFormat("0,000", 51, ".", ",");
        
        
        grActSeg.attachEvent("onRowSelect", function (id) {
            var vRol = <%=Session["Rol"] %>;
            
            grActSeg2.clearAll();
            grActSeg2.loadXML("GrillaObservacion.ashx?IdActa=" + id + "&Rol=" + vRol);
            var trm = byId("txtTRM").value;
            grActSeg3.clearAll()
            //grActSeg3.loadXML("GrillaSumM2.ashx?TRM=" + trm + "&Usu=" + vUsu);
            grActSeg3.loadXML("GrillaPlanProduccion1.ashx?IdActa=" + id + "&Rol=" + vRol);
       
            grActSeg4.clearAll();
            grActSeg4.loadXML("GrillaActalog1.ashx?IdActa=" + id + "&Rol=" + vRol);
             
        });

        //PLANTA
        grActSeg.getCombo(24).put("COLOMBIA", "COLOMBIA");
        grActSeg.getCombo(24).put("MEXICO", "MEXICO");
        //PAGO
        grActSeg.getCombo(16).put("SI", "SI");
        grActSeg.getCombo(16).put("NO", "NO");
        //PLANOS
        grActSeg.getCombo(17).put("SI", "SI");
        grActSeg.getCombo(17).put("NO", "NO");
        grActSeg.getCombo(17).put("N/A", "N/A");
        //CONTRATO
        grActSeg.getCombo(18).put("SI", "SI");
        grActSeg.getCombo(18).put("NO", "NO");
        grActSeg.getCombo(18).put("OC", "OC");
        grActSeg.getCombo(18).put("N/A", "N/A");
        //APROBADO DESPACHO
        grActSeg.getCombo(27).put("SI", "SI");
        grActSeg.getCombo(27).put("NO", "NO");        

        grActSeg.setDateFormat("%Y-%m-%d");

        grActSeg.init();
        grActSeg.setSkin("dhx_skyblue")
        grActSeg.splitAt(10);
        grActSeg.enableSmartRendering(true, 51);

        //dhx_globalImgPath = "codebase/imgs/";
        //grActSeg3 = new dhtmlXGridObject('gridbox3');
        //grActSeg3.setImagePath("codebase/imgs/");
        //grActSeg3.setSkin("dhx_skyblue"); 
        //grActSeg3.setNumberFormat("0,000.00", 1, ".", ",");
        //grActSeg3.setNumberFormat("0,000.00", 2, ".", ",");
        //grActSeg3.setNumberFormat("0,000.00", 3, ".", ",");
        //grActSeg3.setNumberFormat("0,000.00", 4, ".", ",");
        //grActSeg3.setNumberFormat("0,000.00", 5, ".", ",");
        //grActSeg3.setNumberFormat("0,000.00", 6, ".", ",");
     
        //grActSeg3.init();                   

        LoadData(null, null, 2000,0);

        var vRol = <%=Session["Rol"] %>;
        var vUsu = '<%=Session["Usuario"] %>';
        var dp = new dataProcessor("GrillaContenido.ashx?Rol=" + vRol + "&Usu=" + vUsu);
        dp.init(grActSeg);


        dhx_globalImgPath = "codebase/imgs/";
        grActSeg3 = new dhtmlXGridObject('gridbox3');
        grActSeg3.setImagePath("codebase/imgs/");
        grActSeg3.setDateFormat("%Y-%m-%d");

        grActSeg3.init();
        grActSeg3.splitAt(1);
        grActSeg3.setSkin("dhx_skyblue")
        grActSeg3.loadXML("GrillaPlanProduccion1.ashx?IdActa=-1&Rol=" + vRol + "&Usu=" + vUsu);

        var dp3 = new dataProcessor("GrillaPlanProduccion1.ashx?IdActa=-2&Rol=" + vRol + "&Usu=" + vUsu);
        dp3.init(grActSeg3);

        dhx_globalImgPath = "codebase/imgs/";
        grActSeg2 = new dhtmlXGridObject('gridbox2');
        grActSeg2.setImagePath("codebase/imgs/");
        grActSeg2.setDateFormat("%Y-%m-%d");

        grActSeg2.init();
        grActSeg2.splitAt(1);
        grActSeg2.setSkin("dhx_skyblue")
        grActSeg2.loadXML("GrillaObservacion.ashx?IdActa=-1&Rol=" + vRol + "&Usu=" + vUsu);

        var dp2 = new dataProcessor("GrillaObservacion.ashx?IdActa=-2&Rol=" + vRol + "&Usu=" + vUsu);
        dp2.init(grActSeg2);

        dhx_globalImgPath = "codebase/imgs/";
        grActSeg4 = new dhtmlXGridObject('gridbox4');
        grActSeg4.setImagePath("codebase/imgs/");
        grActSeg4.setDateFormat("%Y-%m-%d");

        grActSeg4.init();
        grActSeg4.splitAt(1);
        grActSeg4.setSkin("dhx_skyblue")
        grActSeg4.loadXML("GrillaActaLog1.ashx?IdActa=-1&Rol=" + vRol + "&Usu=" + vUsu);

        var dp4 = new dataProcessor("GrillaActaLog1.ashx?IdActa=-2&Rol=" + vRol + "&Usu=" + vUsu);
        dp4.init(grActSeg4);


        dhx_globalImgPath = "codebase/imgs/";       
       
         
        function LoadData(fDesde, fHasta, trm, perd) {

            var vRol = <%=Session["Rol"] %>;
            var vUsu = '<%=Session["Usuario"] %>';   
            var vperdido = 0;            

            grActSeg.attachFooter("Cantidad Total,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,<div></div>,<div></div>,<div id='nr_q' class='divcolor' ;>0</div>,<div id='sr_q' class='divcolor'>0</div>,<div></div>,"+
                "<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,"+
                "<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,"+
                "<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div></div>,<div id='kr_q' class='divcolor' ;>0</div>",["text-align:left; font-size:12pt; color:black;"]);

            if (document.getElementById('chkPerdido').checked)  { vperdido = 1; }                     
            
            grActSeg.loadXML("GrillaContenido.ashx?fecDesde=" + fDesde + "&fecHasta=" + fHasta+ "&Rol=" + vRol + 
                "&Usu=" + vUsu + "&TRM=" + trm + "&Perd=" + vperdido, function () {
                
                    /* solo se agregan estas lineas cuando se van a cargar los combos dentro de la grilla,
                   cuando los combos sean editables, de lo contrario no. */
                cmbProbabilidad = grActSeg.getColumnCombo(7);
                cmbProbabilidad.loadXML("DatosFiltro.ashx?Tipo=ProbCi");
                cmbTipoObservacion = grActSeg2.getColumnCombo(1);
                cmbTipoObservacion.loadXML("DatosFiltro.ashx?Tipo=TipoObs");
                cmbEstado = grActSeg.getColumnCombo(14);
                cmbEstado.loadXML("DatosFiltro.ashx");
                cmbPerdida = grActSeg.getColumnCombo(24);
                cmbPerdida.loadXML("DatosFiltro.ashx?Tipo=MotPer"); 
                cmbEmpresaCompre = grActSeg.getColumnCombo(26);
                cmbEmpresaCompre.loadXML("DatosFiltro.ashx?Tipo=EmpresaCompe");  
                cmbPlantaP = grActSeg.getColumnCombo(49);
                cmbPlantaP.loadXML("DatosFiltro.ashx?Tipo=PlantaPlan");  
                cmbPlantaC = grActSeg.getColumnCombo(47);
                cmbPlantaC.loadXML("DatosFiltro.ashx?Tipo=PlantaComercial"); 
                cmbEstadoDt = grActSeg.getColumnCombo(29);
                cmbEstadoDt.loadXML("DatosFiltro.ashx?Tipo=EstatusDft");
                //desmoldante
                cmbDesmoldante = grActSeg.getColumnCombo(56);
                cmbDesmoldante.loadXML("DatosFiltro.ashx?Tipo=SiNo");

                //marca Qr
                cmbMarcaQr = grActSeg.getColumnCombo(58);
                cmbMarcaQr.loadXML("DatosFiltro.ashx?Tipo=SiNoQr");

                //marca Reserva Mes
                cmbMarcaReserva = grActSeg.getColumnCombo(59);
                cmbMarcaReserva.loadXML("DatosFiltro.ashx?Tipo=SiNoReserva");

                //combos planeacion produccion orden semana
                cmbAnioPlanProd = grActSeg3.getColumnCombo(1);
                cmbAnioPlanProd.loadXML("DatosFiltro.ashx?Tipo=AnioPlanProd");

                cmbAnioPlanProd = grActSeg3.getColumnCombo(2);
                cmbAnioPlanProd.loadXML("DatosFiltro.ashx?Tipo=MesPlanProd");
                    
                    
                var nrQ = document.getElementById("nr_q");
                nrQ.innerHTML = sumColumn(12);  
                var srQ = document.getElementById("sr_q");
                srQ.innerHTML = sumColumn(13);  
                var krQ = document.getElementById("kr_q");
                krQ.innerHTML = sumColumn(52);
                return true;
                            
            //grActSeg3.clearAll()
            //grActSeg3.loadXML("GrillaSumM2.ashx?TRM=" + trm+ "&Usu=" + vUsu); 

           if ((vRol == "2") || (vRol == "9") || (vRol == "30") || (vRol == "3"))
                {
                   document.formulario.add.disabled = false;
                }
                else
                {
                    document.formulario.button(add).disabled = false;
                }               
            });
          
        }

        function calculateFooterValues(){        
            var nrQ = document.getElementById("nr_q");
            nrQ.innerHTML = sumColumn(12);           
            var srQ = document.getElementById("sr_q");
            srQ.innerHTML = sumColumn(13);     
            var krQ = document.getElementById("kr_q");
            krQ.innerHTML = sumColumn(52);
            return true;
        }

        //Suma todos los valores de una columna
        function sumColumn(ind){
            var out = 0;
            for(var i=0;i<grActSeg.getRowsNum();i++){
                out+=parseFloat(grActSeg.cells2(i,ind).getValue());
            }          
            //parsea el resultado a un digito decimal
            result=out.toLocaleString(undefined, {minimumFractionDigits: 2, maximumFractionDigits: 2})
            console.log(result);
            console.log();   
            return result;                       
        }
      

        function AddObservacion() {
            var id = grActSeg2.uid();
            var today = new Date();
            var fila = grActSeg.cells(grActSeg.getSelectedId(), 3).getValue() + ',1,' + today.yyyymmdd() + ', ,' + grActSeg.getSelectedId();
            grActSeg2.addRow(id, fila, 0);
            grActSeg2.showRow(id); 
            
        }
    </script>
     
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server"> 

    
</asp:Content>
