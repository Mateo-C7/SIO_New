<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="PrecioItemsPv.aspx.cs" Inherits="SIO.PrecioItemsPv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
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
            width: 50px;
            text-align: right;
        }
        .auto-style4 {
            width: 84px;
        }
        .auto-style5 {
            width: 119px;
        }
        .auto-style16 {
            width: 42px;
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
        .auto-style20 {
            width: 44px;
        }
        .auto-style21 {
            width: 155px;
        }
        .auto-style23 {
            width: 1041px;
            height: 1px;
        }
        .auto-style25 {
            width: 181px;
        }
        .auto-style30 {
            width: 100px;
        }
        .auto-style31 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 8pt;
        }
        .auto-style33 {
            font-size: small;
        }
        .auto-style34 {
            width: 71px;
        }
        .auto-style35 {
            width: 216px;
        }
        .auto-style36 {
            width: 148px;
            height: 29px;
        }
        .auto-style37 {
            width: 65px;
        }
        .auto-style38 {
            width: 65px;
            text-align: right;
        }
        .auto-style39 {
            color: #000000;
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
           
            cmbPlanta = new dhtmlXCombo("planta", "combo6", 100);
            
                     
             
            cmbPlanta.loadXML("DatosFiltro.ashx?Tipo=PlantaPv");  

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
        
        function Confirmacion() {

            var seleccion = confirm("acepta el mensaje ?");

            if (seleccion)
                alert("se acepto el mensaje");
            else
                alert("NO se acepto el mensaje");

            //usado para que no haga postback el boton de asp.net cuando 
            //no se acepte el confirm
            return seleccion;
        
        }

    </script>
    
    <fieldset style="border-style: none; border-width: 0px; width: 1153px">
        <table style="margin-left: 0px;" class="auto-style23">
            <tr>
                <td class="auto-style31" colspan="3" >
                    <h5>
                    <asp:Label ID="Label1" runat="server" Text="Label" CssClass="auto-style33">Proceso Automatico Ejecutado:</asp:Label>
                    </h5>
                </td>
                <td class="auto-style5">
                    <asp:Label ID="lblFechaProc" runat="server" Text="Label"></asp:Label>
                </td> 
                 <td style="text-align:right" class="auto-style34">
                     &nbsp;</td>
                <td class="auto-style21">
                    &nbsp;</td> 
                
               
                <td class="auto-style1">                
                    &nbsp;</td>
                
                <td class="auto-style37">
                    &nbsp;</td>
                
                <td class="auto-style35">
                    &nbsp;</td>
                
                <td>
                     <%--<asp:Button ID="btnConfirmacion2" runat="server" Text="Confirmacion 2" BackColor="#1C5AB6" ForeColor="White" OnClick="btnConfirmacion2_Click"  />--%>
                    <input type="button" name="a15" value="ACTUALIZAR PRECIOS" id="a15" onclick='Confirmacion()'
                      
                        style="font-family: Arial, Helvetica, sans-serif;  font-size: 9pt; color: #FFFFFF;
                        background-color: #1C5AB6; " class="auto-style36"/>
                    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" /></td>
                
                <td class="auto-style16">
                    &nbsp;</td>
            </tr>

          
            <tr>
                <td class="auto-style31" >
                    Codigo</td>
                <td class="auto-style5">
                   <%-- <input type="text" name="a12a" value="" id="a12a"  size="4" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; " class="auto-style29"  />--%>
                     &nbsp;<input id="a12a" name="a12a" type="text" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; " class="auto-style30"  /></td>
                 <td class="auto-style4">
                     <label id='a10g' class="auto-style31">Nombre Item</label>
                </td>
                <td class="auto-style5">
                    <input id="nombre" name="nombre" type="text" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; " class="auto-style25"  />
                       
                </td> 
                 <td style="text-align:right" class="auto-style34">
                     <label id='a13a' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt" >
                       Planta
                    </label>
                </td>
                <td class="auto-style21">
                    <div id="planta" name="planta" class="auto-style20">
                    </div>
                </td> 
                
               
                <td class="auto-style1">                
                    <input type="button" name="a11" value="Filtrar" id="a11" onclick='
                     
                        grActSeg.filterBy(0,"");
                        if (document.getElementById("a12a").value != ""){
                          grActSeg.filterBy(0,document.getElementById("a12a").value,true);
                        }
                        if (document.getElementById("nombre").value != ""){
                            grActSeg.filterBy(1,document.getElementById("nombre").value,true);
                        }                               
                        if (cmbPlanta.getComboText() != ""){    
                            grActSeg.filterBy(12,cmbPlanta.getComboText(),true);
                        }   
                            '
                      
                        style="font-family: Arial, Helvetica, sans-serif;  font-size: 9pt; background-color: #CEE4FF; width: 59px; height: 22px;" class="auto-style39"/>
                    

                    
                </td>
                
                <td class="auto-style38">
                    <asp:LinkButton ID="lkActualizar" runat="server" 
                        Font-Names="Arial" Font-Size="8pt" onclick="lkActualizar_Click" ForeColor="#333333"
                                                         >Refresh</asp:LinkButton>
                </td>
                
                <td class="auto-style35">
            
                    &nbsp;</td>
                
                <td>
                    &nbsp;</td>
                
                <td class="auto-style16">
                    &nbsp;</td>
            </tr>

          
        </table>

       
        
    </fieldset>
    
 
    <div id="gridbox" 
        style="width: 1135px; height: 500px; background-color: white;">
    </div>
    <table>
        <tr>
            <td class="auto-style17">
            <input type="button" name="add" id="add" runat="server" value="Asignar Semana"
                    onclick="AddObservacion()" style="font-family: Arial, Helvetica, sans-serif;
                    font-size: 9pt; color: #FFFFFF; background-color: #1C5AB6; 
                    width: 141px;" visible ="False"/>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;                
                
                </td>        
            </tr>
    </table>
    <table>
        <tr>
            <td>
                <div id="gridbox4" style="width: 600px; height: 120px; background-color: white; display:none;">
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
        grActSeg.setNumberFormat("0,000.00", 2, ".", ",");
        grActSeg.setNumberFormat("0,000.00", 3, ".", ",");
        grActSeg.setNumberFormat("0,000.00", 4, ".", ",");
        grActSeg.setNumberFormat("0,000.00", 5, ".", ",");
        grActSeg.setNumberFormat("0,000.00", 6, ".", ",");
        grActSeg.setNumberFormat("0,000.00", 7, ".", ",");
        grActSeg.setNumberFormat("0,000.00", 8, ".", ",");
        grActSeg.setNumberFormat("0,000.00", 9, ".", ",");
        grActSeg.setNumberFormat("0,000.00", 10, ".", ",");
        grActSeg.setNumberFormat("0,000.00", 11, ".", ",");
        //mygrid.setNumberFormat("0,000.00",0,".",","); 
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

        LoadData(null, null, 2000,0);

        var vRol = <%=Session["Rol"] %>;
        var vUsu = '<%=Session["Usuario"] %>';
        var dp = new dataProcessor("GrillaContenidoPrecioItems.ashx?Rol=" + vRol + "&Usu=" + vUsu);
        dp.init(grActSeg);


       
         

        dhx_globalImgPath = "codebase/imgs/";
        grActSeg2 = new dhtmlXGridObject('gridbox2');
        grActSeg2.setImagePath("codebase/imgs/");
        grActSeg2.setDateFormat("%Y-%m-%d");
        grActSeg2.setNumberFormat("0,000.00", 2, ".", ",");
        

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
        grActSeg4.setNumberFormat("0,000.00", 3, ".", ",");

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

            //grActSeg.attachFooter("Cantidad Total,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,<div id='nr_q' class='divcolor' ;>0</div>,<div id='sr_q' class='divcolor'>0</div>",["text-align:left; font-size:12pt; color:black;"]);

            if (document.getElementById('chkPerdido').checked)  { vperdido = 1; }                     
            
            grActSeg.loadXML("GrillaContenidoPrecioItems.ashx?fecDesde=" + fDesde + "&fecHasta=" + fHasta+ "&Rol=" + vRol + 
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


                  
                return true;
                     

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
      

       

         
    </script>
     
</asp:Content>
