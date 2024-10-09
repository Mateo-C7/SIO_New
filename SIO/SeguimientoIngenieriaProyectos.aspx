<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralAmplia.Master" AutoEventWireup="true" 
    CodeBehind="SeguimientoIngenieriaProyectos.aspx.cs" Inherits="SIO.SeguimientoIngenieriaProyectos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="STYLESHEET" type="text/css" href="codebase/dhtmlxgrid.css">
    <link rel="stylesheet" type="text/css" href="codebase/skins/dhtmlxgrid_dhx_skyblue.css">
    <link rel="STYLESHEET" type="text/css" href="codebase/dhtmlxcombo.css">
    <link rel="STYLESHEET" type="text/css" href="codebase/dhtmlxcalendar.css">
    <link rel="STYLESHEET" type="text/css" href="codebase/skins/dhtmlxcalendar_dhx_skyblue.css"> 
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
          .divcolor
        {
            color:black;
            font-size:11pt;
        }
        .auto-style1 {
            width: 40px;
        }
        .auto-style2 {
            margin-bottom: 0px;
        }
    .auto-style3 {
        height: 27px;
    }
    </style>
   
    <fieldset style="border: thin solid #C0C0C0; width: 1280px" class="fondoazul" >
                             
        
     <asp:Label ID="Label7"  runat="server" Font-Names="Arial" Font-Size="12pt"
                    ForeColor="White"  Text="SEGUIMIENTO PLANEACIÓN INGENIERIA" ></asp:Label>

                       
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button"  name="a14" value="Cargar" id="a14" onclick='grActSeg.clearAll(); LoadData(); '
            style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; color: #FFFFFF;
            background-color: #1C5AB6;  width: 54px; visibility:hidden" />    
        &nbsp;   
        
        <input type="hidden" id="Usu" name="Usu" runat="server" />
        <input type="hidden" id="Rol" name="Rol" runat="server" />
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
            
            cmbEstatus = new dhtmlXCombo("estatus", "combo", 130);              
            cmbProb = new dhtmlXCombo("prob", "combo2", 80);
            cmbPais = new dhtmlXCombo("pais", "combo3", 120);       
            cmbPlanta = new dhtmlXCombo("planta", "combo4", 120);
            cmbMaterial = new dhtmlXCombo("material", "combo5", 80); 
            cmbCiudad = new dhtmlXCombo("ciudad", "combo6", 120);  
            cmbZona = new dhtmlXCombo("a12b", "combo7", 110);
            cmbZonaCiuIng = new dhtmlXCombo("ZonaCiuIng", "combo8", 110);
            cmbCoordinadore = new dhtmlXCombo("coordinadores", "combo9", 110);
           

            cmbZona.loadXML("DatosFiltroIngenieria.ashx?Tipo=Zona");
            cmbZona.attachEvent("onChange", function (value) {
                cmbPais.clearAll();
                cmbZonaCiuIng.clearAll();
                cmbZonaCiuIng.setComboValue(null);
                cmbCiudad.clearAll();
                cmbCiudad.setComboValue(null);
                var z = cmbZona.getSelectedValue();
                cmbPais.setComboValue(null);
                cmbPais.setComboText("");           
                cmbPais.loadXML("DatosFiltroIngenieria.ashx?Tipo=Pais&grupo_Id=" + z);
            });            

            cmbEstatus.loadXML("DatosFiltroIngenieria.ashx?Tipo=EstIngenieria");             
            cmbProb.loadXML("DatosFiltroIngenieria.ashx?Tipo=ProbCi");
            //cmbPais.loadXML("DatosFiltroIngenieria.ashx?Tipo=PaisIng");
            cmbPlanta.loadXML("DatosFiltroIngenieria.ashx?Tipo=PlantaIng");  
            cmbCoordinadore.loadXML("DatosFiltroIngenieria.ashx?Tipo=Coordinadores");  
            cmbMaterial.loadXML("DatosFiltroIngenieria.ashx?Tipo=Material");  
     

   
            //Permite autocompletar en el combo
            //cmbPais.enableFilteringMode(true);
            //cmbCiudad.enableFilteringMode(true);
                  
            cmbPais.attachEvent("onChange", function (value) {               
                cmbZonaCiuIng.clearAll();
                cmbCiudad.clearAll();
                cmbCiudad.setComboValue(null);
                var p = cmbPais.getSelectedValue();
                cmbZonaCiuIng.setComboValue(null);
                cmbZonaCiuIng.setComboText("");
                cmbZonaCiuIng.loadXML("DatosFiltroIngenieria.ashx?Tipo=ZonaCiu&pai_Id=" + p);
            });  

                          
            cmbZonaCiuIng.attachEvent("onChange", function (value) {               
                cmbCiudad.clearAll();
                var zc = cmbZonaCiuIng.getSelectedValue();
                cmbCiudad.setComboValue(null);
                cmbCiudad.setComboText("");
                cmbCiudad.loadXML("DatosFiltro.ashx?Tipo=CiudadIng&SubZ=" + zc);  
                cmbCiudad.openSelect();
            });  
   
            from.setDate(to.getDate());

            myCalendar = new dhtmlXCalendarObject(["a12f","a12f2","a12f4","a12f6","a12f8","a12f12",]);
            myCalendar.setSkin('dhx_skyblue');
            myCalendar.setDate(from.yyyymmdd());
            myCalendar.hideTime();


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
        function tomarMesFact(id) {
    
            var mes = document.getElementById(id).value;
            mes = mes.substring(0,7);
            return document.getElementById("a12f1").value = mes;                      
        }
        function tomarMesDespa(id) {
    
            var mes = document.getElementById(id).value;
            mes = mes.substring(0,7);
            return document.getElementById("a12f3").value = mes;                      
        }
        function tomarMesEntrePlan(id) {
    
            var mes = document.getElementById(id).value;
            mes = mes.substring(0,7);
            return document.getElementById("a12f5").value = mes;                      
        }
        function tomarMesProduccion(id) {
    
            var mes = document.getElementById(id).value;
            mes = mes.substring(0,7);
            return document.getElementById("a12f7").value = mes;                      
        }
        function tomarMesIngenieria(id) {
    
            var mes = document.getElementById(id).value;
            mes = mes.substring(0,7);
            return document.getElementById("a12f9").value = mes;                      
        }
        function tomarMesEntregaCoor(id) {
    
            var mes = document.getElementById(id).value;
            mes = mes.substring(0,7);
            return document.getElementById("a12f13").value = mes;                      
        }
    </script>
    
    <fieldset style="border-style: none; border-width: 0px; align-content:flex-start; width: 1280px">
        <table >
            <tr>
                <td style="text-align:right">
                    &nbsp;<label id='a10a' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        OF</label>
                </td>
                <td>
                    <input type="text" name="a12a" value="" id="a12a" size="4"/>
                </td>
              
                <td style="text-align:right">
                    &nbsp;<label id='Label1' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt" >
                        Estatus
                    </label>
                </td>
                <td>
                    <div id="estatus"  name="estatus" class=""  >
                    </div>
                </td> 
                   <td style="text-align:right">
                    &nbsp;<label id='a10b' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; text-align: right">
                        Zona
                    </label>
                </td>
                <td>
                    <div id="a12b" name="a12b" >
                    </div>
                </td>
                 <td  style="text-align:right">
                    &nbsp;<label id='a11a'style="font-family: Arial, Helvetica, sans-serif;  font-size: 8pt">
                       Pais
                    </label>
                </td>
                <td>
                    <div id="pais"  mode="checkbox" name="pais" class="auto-style1">
                    </div>
                </td> 
                <td></td>  
                 <td style="text-align:right">
                    &nbsp;<label id='a10d' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        SubZona
                    </label>
                </td>
                <td>
                    <div id="ZonaCiuIng"  name="a12g">
                    </div>
                </td>
                 <td style="text-align:right">
                    &nbsp;<label id='a11z'style="font-family: Arial, Helvetica, sans-serif;  font-size: 8pt">
                       Ciudad
                    </label>
                </td>
                <td>
                    <div id="ciudad"  name="ciudad">
                    </div>
                </td>   
                <td></td>                  
                  <td>                
                    <input type="button" name="a11" value="Filtrar" id="a11" onclick=' 
    tomarMesFact("a12f");
    tomarMesDespa("a12f2");
    tomarMesEntrePlan("a12f4"); 
    tomarMesProduccion("a12f6"); 
    tomarMesIngenieria("a12f8"); 
    tomarMesEntregaCoor("a12f12");
                            grActSeg.filterBy(0,"");
                        if (document.getElementById("a12a").value != ""){
                            grActSeg.filterBy(1,document.getElementById("a12a").value,true);
                        }  
                        if (cmbProb.getComboText() != ""){    
                            grActSeg.filterBy(8,cmbProb.getComboText(),true);
                        }
                        if (cmbPais.getComboText() != ""){    
                            grActSeg.filterBy(4,cmbPais.getComboText(),true);
                        }
                        if (cmbCoordinadore.getComboText() != ""){    
                            grActSeg.filterBy(47,cmbCoordinadore.getComboText(),true);
                        }
                        if (cmbPlanta.getComboText() != ""){    
                            grActSeg.filterBy(0,cmbPlanta.getComboText(),true);
                        }
                        if (cmbCiudad.getComboText() != ""){    
                            grActSeg.filterBy(5,cmbCiudad.getComboText(),true);
                        }
                        if (cmbMaterial.getComboText() != ""){    
                            grActSeg.filterBy(7,cmbMaterial.getComboText(),true);
                        }
                        if (document.getElementById("a12f1").value != ""){            
                            grActSeg.filterBy(9,document.getElementById("a12f1").value,true);
                        }
                        if (document.getElementById("a12f3").value != ""){            
                            grActSeg.filterBy(40,document.getElementById("a12f3").value,true);
                        }
                        if (document.getElementById("a12f5").value != ""){            
                            grActSeg.filterBy(39,document.getElementById("a12f5").value,true);
                        }
                        if (document.getElementById("a12f7").value != ""){            
                            grActSeg.filterBy(43,document.getElementById("a12f7").value,true);
                        }
                        if (document.getElementById("a12f9").value != ""){            
                            grActSeg.filterBy(46,document.getElementById("a12f9").value,true);
                        }
                        if (document.getElementById("a12f13").value != ""){            
                            grActSeg.filterBy(48,document.getElementById("a12f13").value,true);
                        }
                        if (cmbZona.getComboText() != ""){    
                            grActSeg.filterBy(44,cmbZona.getComboText(),true);
                        }
                        if (cmbZonaCiuIng.getComboText() != ""){    
                            grActSeg.filterBy(45,cmbZonaCiuIng.getComboText(),true);
                        }
                        if (cmbEstatus.getComboText() != ""){    
                        grActSeg.filterBy(42,cmbEstatus.getComboText(),true);
                        }
                        calculateFooterValues();'
                         style="font-family: Arial, Helvetica, sans-serif;  font-size: 9pt; color: #FFFFFF;
                        background-color: #1C5AB6; width: 65px;"/>                                           
                  </td>
                  <td>
                  <asp:LinkButton ID="LinkButton1" runat="server" 
                        Font-Names="Arial" Font-Size="8pt" onclick="lkActualizar_Click"
                                                         >REFRESCAR</asp:LinkButton>
                  </td>                         
                         <tr>   
                              <td style="text-align:right" class="auto-style3">
                    &nbsp;<label id='a13a' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt" >
                       Planta
                    </label>
                </td>
                <td class="auto-style3">
                    <div id="planta" name="planta">
                    </div>
                </td> 
                 <td style="text-align:right" class="auto-style3">
                    &nbsp;<label id='a14a' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        Prob
                    </label>
                </td>
                <td class="auto-style3">
                    <div id="prob" name="prob">
                    </div>
                </td>  
                 <td style="text-align:right" class="auto-style3">
                    &nbsp;<label id='a15a' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        Material
                    </label>
                </td>
                <td class="auto-style3">
                    <div id="material" name="material">
                    </div>
                </td>  
                
                                          
                  <td style="text-align:right" class="auto-style3">
                    &nbsp;<label id='a10f' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">MesFact</label>
                </td>                  
                <td class="auto-style3">
                    <input id="a12f" name="txtMes" type="text" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 40px;"  />
                        <input id="a12f1" name="txtMes" type="hidden" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 48px;"  />
                </td> 
                   <td style="text-align:right" class="auto-style3">
                    &nbsp;<label id='a11f' style="font-family: Arial, Helvetica, sans-serif;  font-size: 8pt">MesPlanDespa</label>
                </td>
                <td class="auto-style3">
                    <input id="a12f2" name="txtMesDespacho" type="text" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 40px;"  />
                        <input id="a12f3" name="txtMesDespacho" type="hidden" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 48px;"  />
                </td> 
                   <td style="text-align:right" class="auto-style3">
                    &nbsp;<label id='a13f' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">Entrega_Plan_Ing</label>
                </td>
                <td class="auto-style3">
                    <input id="a12f4" name="txtEntrePlan" type="text" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 40px;"  />
                        <input id="a12f5" name="txtEntrePlan" type="hidden" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 48px;"  />
                </td> 
                  <td style="text-align:right" class="auto-style3">
                    &nbsp;<label id='a11f6' style="font-family: Arial, Helvetica, sans-serif;  font-size: 8pt">MesProduccion</label>
                </td>
                <td class="auto-style3">
                    <input id="a12f6" name="txtMesProduccion" type="text" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 40px;"  />
                        <input id="a12f7" name="txtMesProduccion" type="hidden" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 48px;"  />
                </td> 
                                            <td style="text-align:right" class="auto-style3">
                    &nbsp;<label id='a11f7' style="font-family: Arial, Helvetica, sans-serif;  font-size: 8pt">MesIngenieria</label>
                </td>
                <td class="auto-style3">
                    <input id="a12f8" name="txtMesIngenieria" type="text" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 40px;"  />
                        <input id="a12f9" name="txtMesIngenieria" type="hidden" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 48px;"  />
                </td> 
                             </tr>
            <tr>

                 <td style="text-align:right">
                    &nbsp;<label id='a11f8' style="font-family: Arial, Helvetica, sans-serif;  font-size: 8pt">MesEntregaCoor</label>
                </td>
                <td>
                    <input id="a12f12" name="txtMesEntregaCoor" type="text" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 40px;"  />
                        <input id="a12f13" name="txtMesEntregaCoor" type="hidden" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 8pt; width: 48px;"  />
                </td> 


                              <td style="text-align:right">
                    &nbsp;<label id='a12f11' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt" >
                     coordinadores
                    </label>
                </td>
                <td>
                    <div id="coordinadores" name="coordinadores" class="auto-style2">
                    </div>
                </td> 
            </tr>
          
        </table>
    </fieldset>
    &nbsp;
    <div id="gridbox" 
        style="width: 1290px; height: 340px; background-color: white;">
    </div>
    <table>
        <tr>
            <td>
                <div id="gridbox2" style="width: 1290px; height: 120px;  background-color: white;">
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
                <div id="gridbox3" style="width: 570px; height: 120px; background-color: white;">
                </div>
            </td>
        </tr>               
    </table>
    <%--Variable De Session--%>
    <script>
        dhx_globalImgPath = "codebase/imgs/";
        grActSeg = new dhtmlXGridObject('gridbox');
        grActSeg.setImagePath("codebase/imgs/");   
                      
        grActSeg.attachEvent("onRowSelect", function (id) {
            var vRol = <%=Session["Rol"] %>;
            
            grActSeg2.clearAll();
            grActSeg2.loadXML("GrillaObservacionIngenieria.ashx?IdActa=" + id + "&Rol=" + vRol);
            
            grActSeg4.clearAll();
            grActSeg4.loadXML("GrillaActalog1.ashx?IdActa=" + id + "&Rol=" + vRol);             
        });


   
        grActSeg.setDateFormat("%Y-%m-%d");

        grActSeg.init();
        grActSeg.setSkin("dhx_skyblue")
        grActSeg.splitAt(11);
        grActSeg.enableSmartRendering(true, 50);

        dhx_globalImgPath = "codebase/imgs/";
        grActSeg3 = new dhtmlXGridObject('gridbox3');
        grActSeg3.setImagePath("codebase/imgs/");
        grActSeg3.setSkin("dhx_skyblue"); 
        grActSeg3.setNumberFormat("0,000.00", 1, ".", ",");
        grActSeg3.setNumberFormat("0,000.00", 2, ".", ",");
        grActSeg3.setNumberFormat("0,000.00", 3, ".", ",");
        grActSeg3.setNumberFormat("0,000.00", 4, ".", ",");
        grActSeg3.setNumberFormat("0,000.00", 5, ".", ",");
        grActSeg3.setNumberFormat("0,000.00", 6, ".", ",");
     
        grActSeg3.init();                   

        LoadData();

        var vRol = <%=Session["Rol"] %>;
        var vUsu = '<%=Session["Usuario"] %>';
        var dp = new dataProcessor("GrillaContenidoIngenieria.ashx?Rol=" + vRol + "&Usu=" + vUsu);
        dp.init(grActSeg);



        dhx_globalImgPath = "codebase/imgs/";
        grActSeg2 = new dhtmlXGridObject('gridbox2');
        grActSeg2.setImagePath("codebase/imgs/");
        grActSeg2.setDateFormat("%Y-%m-%d");

        grActSeg2.init();
        grActSeg2.splitAt(1);
        grActSeg2.setSkin("dhx_skyblue")
        grActSeg2.loadXML("GrillaObservacionIngenieria.ashx?IdActa=-1&Rol=" + vRol + "&Usu=" + vUsu);

        var dp2 = new dataProcessor("GrillaObservacionIngenieria.ashx?IdActa=-2&Rol=" + vRol + "&Usu=" + vUsu);
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
       
         
        function LoadData() {

            var vRol = <%=Session["Rol"] %>;
            var vUsu = '<%=Session["Usuario"] %>';   
                                             
            grActSeg.attachFooter("Cantidad Total,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,,,,,,,<div id='nr_q' class='divcolor' ;>0</div>,<div id='sr_q' class='divcolor'>0</div>,<div id='or_q' class='divcolor'>0</div>,<div id='pr_q' class='divcolor'>0</div>,<div id='qr_q' class='divcolor'>0</div,<div id='rr_q' class='divcolor'>0</div,<div id='tr_q' class='divcolor'>0</div",["text-align:left; font-size:12pt; color:black;"]);
 

            grActSeg.loadXML("GrillaContenidoIngenieria.ashx?Rol=" + vRol + 
                "&Usu=" + vUsu + "" , function (){
       
                cmbTipoObservacion = grActSeg2.getColumnCombo(1);
                cmbTipoObservacion.loadXML("DatosFiltroIngenieria.ashx?Tipo=TipoObsIng");
                cmbEstado = grActSeg.getColumnCombo(42);
                cmbEstado.loadXML("DatosFiltroIngenieria.ashx?Tipo=EstIngenieria");

                var nrQ = document.getElementById("nr_q");
                nrQ.innerHTML = sumColumn(17);  
                var srQ = document.getElementById("sr_q");
                srQ.innerHTML = sumColumn(18);  
                var orQ = document.getElementById("or_q");
                orQ.innerHTML = sumColumn(19);  
                var prQ = document.getElementById("pr_q");
                prQ.innerHTML = sumColumn(20);  
                var qrQ = document.getElementById("qr_q");
                qrQ.innerHTML = sumColumn(21);
                var rrQ = document.getElementById("rr_q");
                rrQ.innerHTML = sumColumn(22); 
                var trQ = document.getElementById("tr_q");
                trQ.innerHTML = sumColumn(23);  
                return true;                                                                   
            });          
        }
        function calculateFooterValues(){        
            var nrQ = document.getElementById("nr_q");
            nrQ.innerHTML = sumColumn(17);           
            var srQ = document.getElementById("sr_q");
            srQ.innerHTML = sumColumn(18);  
            var orQ = document.getElementById("or_q");
            orQ.innerHTML = sumColumn(19);  
            var prQ = document.getElementById("pr_q");
            prQ.innerHTML = sumColumn(20);
            var qrQ = document.getElementById("qr_q");
            qrQ.innerHTML = sumColumn(21);
            var rrQ = document.getElementById("rr_q");
            rrQ.innerHTML = sumColumn(22); 
            var trQ = document.getElementById("tr_q");
            trQ.innerHTML = sumColumn(23);  
            return true;
        }

        //Suma todos los valores de una columna
        function sumColumn(ind){
            var out = 0;
            for(var i=0;i<grActSeg.getRowsNum();i++){
                out+=parseFloat(grActSeg.cells2(i,ind).getValue());
            }          
            //parsea el resultado a un digito decimal
            result=out.toLocaleString(undefined, {minimumFractionDigits: 1, maximumFractionDigits: 1})
            console.log(result);
            console.log();   
            return result;                       
        }
        function AddObservacion() {
            var id = grActSeg2.uid();
            var today = new Date();
            var fila = grActSeg.cells(grActSeg.getSelectedId(), 1).getValue() + ',Seguimiento Ingenieria,' + today.yyyymmdd() + ', ,' + grActSeg.getSelectedId();
            grActSeg2.addRow(id, fila, 0);
            grActSeg2.showRow(id);             
        }
    </script>     
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
