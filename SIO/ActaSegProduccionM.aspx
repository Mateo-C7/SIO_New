<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralAmplia.Master" AutoEventWireup="true" 
    CodeBehind="ActaSegProduccion.aspx.cs" Inherits="SIO.ActaSegProduccion" %>

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
        input#txtFecIni, input#txtFecFin, input#a12f
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
        .auto-style1 {
            width: 255px;
            text-align: right;
        }
        .auto-style2 {
            width: 40px;
        }
        .auto-style4 {
            width: 44px;
        }
        .auto-style5 {
            width: 50px;
        }
        .auto-style6 {
            width: 27px;
        }
        .auto-style7 {
            width: 41px;
        }
        .auto-style8 {
            width: 58px;
        }
        .auto-style9 {
            width: 25px;
        }
        .auto-style10 {
            width: 26px;
        }
        .auto-style11 {
            width: 39px;
        }
        .auto-style12 {
            width: 36px;
        }
        .auto-style13 {
            width: 61px;
        }
        .auto-style14 {
        }
        .auto-style16 {
            width: 11px;
        }
        .auto-style17 {
            width: 693px;
        }
        #AnioMaestro {
            width: 44px;
        }
        #MesMaestro {
            width: 40px;
        }
        .auto-style19 {
            width: 172px;
        }
        .auto-style22 {
            width: 13px;
        }
        .auto-style23 {
            width: 30px;
            text-align: center;
        }
        .auto-style25 {
            width: 16px;
            text-align: right;
        }
        .auto-style27 {
            width: 6px;
            text-align: center;
        }
        .auto-style28 {
            width: 1px;
        }
        .auto-style29 {
            width: 5px;
            text-align: right;
        }
    </style>
   
    
        <input id="txtFecIni" name="txtFecIni" type="text" style="display:none ; font-family: Arial, Helvetica, sans-serif;
            font-size: 1pt; width: 55px;" onclick="setSens('txtFecFin', 'max');"  />
       
        <input id="txtFecFin" name="txtFecFin" type="text" style="display:none ; font-family: Arial, Helvetica, sans-serif;
            font-size: 1pt; width: 55px;" onclick="setSens('txtFecIni', 'min');"  />
        
        <label id='LTrm' style="display:none ; font-family: Arial, Helvetica, sans-serif; font-size: 1pt"> TRM</label>
        
        <input type="text" name="txtTRM" value="2900"  id="txtTRM" size="1" style="display:none ; font-family: Arial, Helvetica, sans-serif;
            font-size: 1pt;"  /> 
      
        
    
 
      <label> <input type="hidden" id="chkPerdido" onclick="valuecheck(this)"/> </label>
  
        <input type="hidden" name="a14" value="Cargar" id="a14" onclick='grActSeg.clearAll(); LoadData(byId("txtFecIni").value,
            byId("txtFecFin").value, byId("txtTRM").value, byId("chkPerdido").value);'
            style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; color: #FFFFFF;
            background-color: #1C5AB6;  width: 54px;" />    
      
        
        <input type="hidden" id="Usu" name="Usu" runat="server" />
        <input type="hidden" id="Rol" name="Rol" runat="server" />
    
   
    
    
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
            cmbPais = new dhtmlXCombo("a12c", "combo2", 120);
            cmbProb = new dhtmlXCombo("a12d", "combo3", 80);
            //cmbZonaCiu = new dhtmlXCombo("a12g", "combo4", 110);
            cmbEstatus = new dhtmlXCombo("estatus", "combo5", 100);
            cmbPlanta = new dhtmlXCombo("planta", "combo6", 100);
            cmbPlantaPlan = new dhtmlXCombo("plantaPlan", "combo7", 100);
            //cmbTipoNegocio = new dhtmlXCombo("a12abc", "combo8", 100);

            cmbAnioMaestro = new dhtmlXCombo("AnioMaestro", "combo51", 80);
            cmbMesMaestro = new dhtmlXCombo("MesMaestro", "combo52", 80);
            cmbEstadoPlan = new dhtmlXCombo("a12d1", "combo53", 80);
           

            cmbZona.loadXML("DatosFiltro.ashx?Tipo=Zona");
            cmbZona.attachEvent("onChange", function (value) {
                cmbPais.clearAll();
                var z = cmbZona.getSelectedValue();
                cmbPais.setComboValue(null);
                cmbPais.setComboText("");
                cmbPais.loadXML("DatosFiltro.ashx?Tipo=Pais&grupo_Id=" + z);
            });            

            //cmbPais.attachEvent("onChange", function (value) {
            //    //cmbZonaCiu.clearAll();
            //    var p = cmbPais.getSelectedValue();
            //    cmbZonaCiu.setComboValue(null);
            //    cmbZonaCiu.setComboText("");
            //    cmbZonaCiu.loadXML("DatosFiltro.ashx?Tipo=ZonaCiu&pai_Id=" + p);
            //});
            cmbZona.enableFilteringMode(false);
            cmbPais.enableFilteringMode(false);
            //cmbZonaCiu.enableFilteringMode(false);
            cmbEstatus.enableFilteringMode(false);
            cmbProb.loadXML("DatosFiltro.ashx?Tipo=ProbCi");
            cmbEstatus.loadXML("DatosFiltro.ashx?Tipo=Estatus");
            cmbPlanta.loadXML("DatosFiltro.ashx?Tipo=Planta"); 
            cmbPlantaPlan.loadXML("DatosFiltro.ashx?Tipo=PlantaPlan"); 
            //cmbTipoNegocio.loadXML("DatosFiltro.ashx?Tipo=TipoNegociox");

            cmbAnioMaestro.enableFilteringMode(false);
            cmbAnioMaestro.loadXML("DatosFiltro.ashx?Tipo=AnioPlanProd");            
            cmbMesMaestro.loadXML("DatosFiltro.ashx?Tipo=MesPlanProd");
            cmbEstadoPlan.loadXML("DatosFiltro.ashx?Tipo=EstadoPlan");
      


            //            from.setDate(to.getDate() - 365);
            from.setDate(to.getDate());

            myCalendar = new dhtmlXCalendarObject(["txtFecIni", "txtFecFin", "a12f","a13g"]);
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
        

    </script>
    
    <fieldset style="border-style: none; border-width: 0px; width: 1153px">
        <table style="width: 1152px; height: 1px; margin-left: 0px;">
            <tr>
                <td class="auto-style12" >
                    <label id='a10a' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">FUP/OF</label>
                </td>
                <td class="auto-style13">
                    <input type="text" name="a12a" value="" id="a12a" size="4"/>
                </td>
                 <td class="auto-style4">
                     <label id='a10g' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">MesProd</label>
                </td>
                <td class="auto-style5">
                    <input id="a13g" name="txtMesProd" type="text" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 40px;"  />
                        <input id="a13g1" name="txtMesProd" type="hidden" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 48px;"  />
                </td> 
                 <td style="text-align:right" class="auto-style6">
                     <label id='a13a' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt" >
                       Planta
                    </label>
                </td>
                <td>
                    <div id="planta" name="planta">
                    </div>
                </td>
                <td class="auto-style7">
                    <label id='a10f' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">MesFact</label></td>
                <td class="auto-style8">
                    <input id="a12f" name="txtMes" type="text" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 40px;"  />
                        <input id="a12f1" name="txtMes" type="hidden" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 48px;"  />
                </td>  
                <td class="auto-style9">
                    <label id='a10g' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">Prob
                    </label>
                </td>
                <td>
                    <div id="a12d" name="a12d" >
                    </div>
                </td>   
                 <td style="text-align:right" class="auto-style2">
                     <label id='a13ab' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt" >
                       PlantaPlan
                    </label>
                </td>
                <td>
                    <div id="plantaPlan" name="plantaPlan">
                    </div>
                </td>  
                <td class="auto-style10">
                    <label id='a10b' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; text-align: right">
                        Zona
                    </label>
                </td>
                <td>
                    <div id="a12b" name="a12b" >
                    </div>
                </td>
                <td class="auto-style9">
                    <label id='a10c' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; text-align: center">
                        Pais
                    </label>
                </td>
                <td>
                    <div id="a12c" name="a12c">
                    </div>
                </td>
                 <td class="auto-style11">
                     <label id='Label1' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        Estatus
                    </label>
                </td>
                <td>
                    <div id="estatus" name="estatus">
                    </div>
                </td>   
                &nbsp;
                
                <%--<td>
                    &nbsp;<label id='a10d' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        SubZ
                    </label>
                </td>
                <td>
                    <div id="a12g" name="a12g">
                    </div>
                </td>--%>       
                  
                 <%--<td style="text-align:right">
                    &nbsp;<label id='a13abc' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt" >
                       TipoNegociacion
                    </label>
                </td>
                <td>
                     <div id="a12abc" name="a12abc" >
                    </div>
                </td>     --%>               
                
                </tr>
            <tr>
                <td class="auto-style9">
                    <label id='a10g1' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">EstadoPlan
                    </label>
                </td>
                <td>
                    <div id="a12d1" name="a12d1" >
                    </div>
                </td>  
                <td></td>
                <td class="auto-style1">                
                    <input type="button" name="a11" value="Filtrar" id="a11" onclick='
                    tomarMes("a12f");
                    tomarMesProd("a13g");
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
                        grActSeg.filterBy(12,cmbEstatus.getComboText(),true);
                        }
                        //if (document.getElementById("a12e").value != ""){            
                        //grActSeg.filterBy(27,document.getElementById("a12e").value,true);
                        //}
                        //if (cmbTipoNegocio.getComboText() != ""){    
                        //    grActSeg.filterBy(49,cmbTipoNegocio.getComboText(),true);
                        //} 
                        //if (cmbZonaCiu.getComboText() != ""){    
                        //grActSeg.filterBy(2,cmbZonaCiu.getComboText(),true);                        
                        //} 
                        if (document.getElementById("a12f1").value != ""){            
                        grActSeg.filterBy(8,document.getElementById("a12f1").value,true);
                        }
                        if (document.getElementById("a13g1").value != ""){            
                            grActSeg.filterBy(13,document.getElementById("a13g1").value,true);
                        }                                            
                        if (cmbPlanta.getComboText() != ""){    
                            grActSeg.filterBy(42,cmbPlanta.getComboText(),true);
                        }      
                        if (cmbPlantaPlan.getComboText() != ""){    
                            grActSeg.filterBy(43,cmbPlantaPlan.getComboText(),true);
                        }                        
                        if (cmbEstadoPlan.getComboText() != ""){    
                            grActSeg.filterBy(51,cmbEstadoPlan.getComboText(),true);
                        }
                        calculateFooterValues();   '
                      
                        style="font-family: Arial, Helvetica, sans-serif;  font-size: 9pt; color: #FFFFFF;
                        background-color: #1C5AB6; width: 55px; height: 19px;"/>
                    

                    
                </td>
                
                <td class="auto-style16">
                    <asp:LinkButton ID="lkActualizar" runat="server" 
                        Font-Names="Arial" Font-Size="8pt" onclick="lkActualizar_Click" ForeColor="#333333"
                                                         >Refresh</asp:LinkButton>
                </td>
            </tr>

          
        </table>
    </fieldset>
  
    <div id="gridbox" 
        style="width: 1290px; height: 340px; background-color: white;">
    </div>
    <table>
        <tr>
            <td class="auto-style17">
                                  
            </td>         
            <td class="auto-style14" colspan="6">
                <div id="gridbox5" style="width: 640px; height: 140px; background-color: white;">
                </div> 
            </td>  
                        
        </tr>
        <tr>
            <td class="auto-style17">
            <input type="button" name="add" id="add" runat="server" value="Asignar Semana"
                    onclick="AddObservacion()" style="font-family: Arial, Helvetica, sans-serif;
                    font-size: 9pt; color: #FFFFFF; background-color: #1C5AB6; 
                    width: 141px;" />  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;                
                                 
             
            <input type="button" name="a91" value="CalcularPendiente" id="a91" onclick='calculateFooterValuesProd();'
                      
                        style="font-family: Arial, Helvetica, sans-serif;  font-size: 8pt; color: #000000;
                        background-color: #FFFFFF; width: 100px;"/>
                </td>
            <td class="auto-style19">
                <input type="button" name="add1" id="add1" runat="server" value="Configurar Semana"
                    onclick="AddObservacion1()" style="font-family: Arial, Helvetica, sans-serif;
                    font-size: 9pt; color: #FFFFFF; background-color: #1C5AB6; 
                    width: 141px;" />&nbsp;
                </td>
             

            

            <td class="auto-style29">
               <label id='a10g' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">Año</label></td>
             

            

            <td class="auto-style28">
                 <div id="AnioMaestro" name="AnioMaestro" class="auto-style22" >
                    </div>
                </td>
             

            

            <td class="auto-style25">
                 <label id='a10xg' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">Mes</label></td>
             

            

            <td class="auto-style28">
                <div id="MesMaestro" name="MesMaestro" class="auto-style23" >
                    </div>
                </td>
             

            

            <td class="auto-style27">
                    <input type="button" name="a92" value="FiltrarMes" id="a92" onclick='
                   
                        grActSeg5.filterBy(0,"");
                        
                        if (cmbAnioMaestro.getComboText() != ""){
                            grActSeg5.filterBy(0,cmbAnioMaestro.getComboText(),true);
                        }
                        if (cmbMesMaestro.getComboText() != ""){
                            grActSeg5.filterBy(1,cmbMesMaestro.getComboText(),true);
                        }
                              '
                      
                        style="font-family: Arial, Helvetica, sans-serif;  font-size: 9pt; color: #FFFFFF;
                        background-color: #1C5AB6; width: 65px; height: 19px;"/></td>
             

            

        </tr>
    </table>
    <table>
        <tr>
            <td>
                <div id="gridbox4" style="width: 600px; height: 120px; background-color: white;">
                </div>                
            </td>
         </tr>
         <tr>
            <td>
                <div id="gridbox2" style="width: 570px; height: 120px; background-color: white; display:none;">
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
        grActSeg.attachEvent("onRowSelect", function (id) {
            var vRol = <%=Session["Rol"] %>;        
                      
            grActSeg2.clearAll();
            grActSeg2.loadXML("GrillaObservacion.ashx?IdActa=" + id + "&Rol=" + vRol);
            var trm = byId("txtTRM").value;
            
            grActSeg4.clearAll();
            grActSeg4.loadXML("GrillaActalog1.ashx?IdActa=" + id + "&Rol=" + vRol);  
            
            grActSeg5.clearAll()
            grActSeg5.loadXML("GrillaProdSemana.ashx?Usu=" + vUsu+ "&Rol=" + vRol);
            
            //document.getElementById("a91").click();
            //darClick ();
            calculateFooterValuesProdCero();      
        });        

        //PLANTA
        grActSeg.getCombo(22).put("COLOMBIA", "COLOMBIA");
        grActSeg.getCombo(22).put("MEXICO", "MEXICO");
        //PAGO
        grActSeg.getCombo(14).put("SI", "SI");
        grActSeg.getCombo(14).put("NO", "NO");
        //PLANOS
        grActSeg.getCombo(15).put("SI", "SI");
        grActSeg.getCombo(15).put("NO", "NO");
        grActSeg.getCombo(15).put("N/A", "N/A");
        //CONTRATO
        grActSeg.getCombo(16).put("SI", "SI");
        grActSeg.getCombo(16).put("NO", "NO");
        grActSeg.getCombo(16).put("OC", "OC");
        grActSeg.getCombo(16).put("N/A", "N/A");
        //APROBADO DESPACHO
        grActSeg.getCombo(25).put("SI", "SI");
        grActSeg.getCombo(25).put("NO", "NO");

        grActSeg.setDateFormat("%Y-%m-%d");

        grActSeg.init();
        grActSeg.setSkin("dhx_skyblue")
        grActSeg.splitAt(10);
        grActSeg.enableSmartRendering(true, 50);

        

        // cargue de grilla maestro semana
        dhx_globalImgPath = "codebase/imgs/";
        grActSeg5 = new dhtmlXGridObject('gridbox5');
        grActSeg5.setImagePath("codebase/imgs/");
        grActSeg5.setSkin("dhx_skyblue"); 
        grActSeg5.setDateFormat("%Y-%m-%d");
        //grActSeg5.setDateFormat("%Y-%m-%d");
       
        grActSeg5.init();           

        LoadData(null, null, 2000,0);

        var vRol = <%=Session["Rol"] %>;
        var vUsu = '<%=Session["Usuario"] %>';
        var dp = new dataProcessor("GrillaContenidoPlanProd.ashx?Rol=" + vRol + "&Usu=" + vUsu);
        dp.init(grActSeg);


       

        var dp5 = new dataProcessor("GrillaProdSemana.ashx?Usu=" + vUsu+ "&Rol=" + vRol);
        dp5.init(grActSeg5);        


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

            grActSeg.attachFooter("Cantidad Total,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,<div id='nr_q' class='divcolor' ;>0</div>,<div id='sr_q' class='divcolor'>0</div>",["text-align:left; font-size:12pt; color:black;"]);

            if (document.getElementById('chkPerdido').checked)  { vperdido = 1; }                     
            
            grActSeg.loadXML("GrillaContenidoPlanProd.ashx?fecDesde=" + fDesde + "&fecHasta=" + fHasta+ "&Rol=" + vRol + 
                "&Usu=" + vUsu + "&TRM=" + trm + "&Perd=" + vperdido, function () {
                
                    /* solo se agregan estas lineas cuando se van a cargar los combos dentro de la grilla,
                   cuando los combos sean editables, de lo contrario no. */
                cmbProbabilidad = grActSeg.getColumnCombo(7);
                cmbProbabilidad.loadXML("DatosFiltro.ashx?Tipo=ProbCi");
                cmbTipoObservacion = grActSeg2.getColumnCombo(1);
                cmbTipoObservacion.loadXML("DatosFiltro.ashx?Tipo=TipoObs");
                cmbEstado = grActSeg.getColumnCombo(12);
                cmbEstado.loadXML("DatosFiltro.ashx");
                cmbPerdida = grActSeg.getColumnCombo(19);
                cmbPerdida.loadXML("DatosFiltro.ashx?Tipo=MotPer"); 
                cmbEmpresaCompre = grActSeg.getColumnCombo(21);
                cmbEmpresaCompre.loadXML("DatosFiltro.ashx?Tipo=EmpresaCompe");  
                cmbPlantaP = grActSeg.getColumnCombo(43);
                cmbPlantaP.loadXML("DatosFiltro.ashx?Tipo=PlantaPlan");    
                cmbEstadoDt = grActSeg.getColumnCombo(24);
                cmbEstadoDt.loadXML("DatosFiltro.ashx?Tipo=EstatusDft");

                          
               
                    
                var nrQ = document.getElementById("nr_q");
                nrQ.innerHTML = sumColumn(10);  
                var srQ = document.getElementById("sr_q");
                srQ.innerHTML = sumColumn(11);  

                //grActSeg5.clearAll()
                //grActSeg5.loadXML("GrillaProdSemana.ashx?Usu=" + vUsu+ "&Rol=" + vRol);

                grActSeg5.loadXML("GrillaProdSemana.ashx?Usu=" + vUsu+ "&Rol=" + vRol, function () {
                
                    /* solo se agregan estas lineas cuando se van a cargar los combos dentro de la grilla,
                   cuando los combos sean editables, de lo contrario no. */
                    
                    //combos planeacion produccion maestro semana
                    cmbAnioPlanProdSem = grActSeg5.getColumnCombo(0);
                    cmbAnioPlanProdSem.loadXML("DatosFiltro.ashx?Tipo=AnioPlanProd");

                    cmbAnioPlanProdSem = grActSeg5.getColumnCombo(1);
                    cmbAnioPlanProdSem.loadXML("DatosFiltro.ashx?Tipo=MesPlanProd");

                    cmbAnioPlanProdSem = grActSeg5.getColumnCombo(2);
                    cmbAnioPlanProdSem.loadXML("DatosFiltro.ashx?Tipo=SemPlanProd");    
                                 
                });   
                return true;

                //grActSeg5.clearAll()
                    //grActSeg5.loadXML("GrillaProdSemana.ashx?Usu=" + vUsu+ "&Rol=" + vRol);
                    // carga datos de grilla maestro semana
                
                          

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

        //realiza llamado a sumatoria
        function calculateFooterValues(){        
            var nrQ = document.getElementById("nr_q");
            nrQ.innerHTML = sumColumn(10);           
            var srQ = document.getElementById("sr_q");
            srQ.innerHTML = sumColumn(11);           
            return true;
        }

        //realiza llamado a sumatoria xxx
        function calculateFooterValuesProd(){        

            //var nrM = document.getElementById("nr_m");
            //var m2Planeados = sumColumnM2PlanProd(4);
            //var m2Total = grActSeg.cells(grActSeg.getSelectedId(), 50).getValue();
            //var M2Pend = parseFloat( m2Total)- parseFloat(m2Planeados); 
            //nrM.innerHTML = Math.round(Number(parseFloat(M2Pend)) *100) / 100 

            var m2Planeados = sumColumnM2PlanProd(4);
            var m2Total = grActSeg.cells(grActSeg.getSelectedId(), 10).getValue();
            var M2Pend = parseFloat( m2Total)- parseFloat(m2Planeados);
            
            var nrM = document.getElementById("nr_m");
            nrM.innerHTML =  Math.round(Number(parseFloat(M2Pend)) *100) / 100 ;

            //var nrM = document.getElementById("nr_m");
            //var m2asig = sumColumnM2PlanProd(4);                 
                    
            return true;
        }

        //realiza llamado a sumatoria xxx
        function calculateFooterValuesProdCero(){        
                       
            
            var nrM = document.getElementById("nr_m");
            nrM.innerHTML =  0.0 ;           
                    
            return true;
        }

        function FiltrarMaestroMes(){        

            //var nrM = document.getElementById("nr_m");
            //var m2Planeados = sumColumnM2PlanProd(4);
            //var m2Total = grActSeg.cells(grActSeg.getSelectedId(), 50).getValue();
            //var M2Pend = parseFloat( m2Total)- parseFloat(m2Planeados); 
            //nrM.innerHTML = Math.round(Number(parseFloat(M2Pend)) *100) / 100 

            var m2Planeados = sumColumnM2PlanProd(4);
            var m2Total = grActSeg.cells(grActSeg.getSelectedId(), 10).getValue();
            var M2Pend = parseFloat( m2Total)- parseFloat(m2Planeados);
            
            var nrM = document.getElementById("nr_m");
            nrM.innerHTML =  Math.round(Number(parseFloat(M2Pend)) *100) / 100 ;

            //var nrM = document.getElementById("nr_m");
            //var m2asig = sumColumnM2PlanProd(4);                 
                    
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
      

        

        function AddObservacion1() {
            
            var id = grActSeg5.uid();
            var today = new Date();
            var fila = ',,,,,,,,';
            //var fila = grActSeg.cells(grActSeg.getSelectedId(), 3).getValue() + ',1,' + today.yyyymmdd() + ', ,' + grActSeg.getSelectedId();
            grActSeg5.addRow(id, fila, 0);
            grActSeg5.showRow(id);  
            
        }
    </script>
     
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server"> 

    
</asp:Content>

