<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralAmplia.Master" AutoEventWireup="true" 
    CodeBehind="PlaneadorPlanosFabricacion.aspx.cs" Inherits="SIO.PlaneadorPlanosFabricacion" %>

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
        .auto-style3 {
            width: 40px;
            height: 15px;
        }
    </style>
   
    <fieldset style="border: thin solid #C0C0C0; width: 1280px" class="fondoazul" >
                             
        
     <asp:Label ID="Label7"  runat="server" Font-Names="Arial" Font-Size="12pt"
                    ForeColor="White"  Text="PLANEACIÓN PLANOS DE FABRICACIÓN" ></asp:Label>

                               
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

            cmbZona = new dhtmlXCombo("a12b", "combo", 120);
            cmbPais = new dhtmlXCombo("pais", "combo2", 120);
            cmbTipoCotizacion  = new dhtmlXCombo("tipoCotizacion", "combo3", 120);
            cmbExplosionado  = new dhtmlXCombo("explosionado", "combo4", 45);
            cmbEstado  = new dhtmlXCombo("estado", "combo5", 120);
        


            cmbZona.loadXML("DatosFiltro.ashx?Tipo=Zona");
            cmbZona.attachEvent("onChange", function (value) {
                cmbPais.clearAll();               
                var z = cmbZona.getSelectedValue();
                cmbPais.setComboValue(null);
                cmbPais.setComboText("");           
                cmbPais.loadXML("DatosFiltro.ashx?Tipo=Pais&grupo_Id=" + z);
            });  

            cmbTipoCotizacion.loadXML("DatosFiltro.ashx?Tipo=TipoCotizacionPlanos");  
            cmbExplosionado.loadXML("DatosFiltro.ashx?Tipo=Explosionado");  
            cmbEstado.loadXML("DatosFiltro.ashx?Tipo=EstIngenieria");  


            //cmbZonaCiu = new dhtmlXCombo("cbo_ZonaCiu", "combo3", 110);
            //cmbCiudad = new dhtmlXCombo("cbo_Ciudad", "combo4", 120);
            //cmbRaya = new dhtmlXCombo("cbo_Raya", "combo5", 120);
            //cmbTipoCotiza = new dhtmlXCombo("cbo_Cotiza", "combo6", 120);
            //cmbExplosionado = new dhtmlXCombo("cbo_Explosionado", "combo7", 120);
            //cmbEstado = new dhtmlXCombo("cbo_Estado", "combo8", 120);                   
                                            
            //Permite autocompletar en el combo
            //cmbPais.enableFilteringMode(true);
            //cmbCiudad.enableFilteringMode(true);

            //cmbZona.loadXML("DatosFiltroPlaneadorPlanos.ashx?Tipo=Zona");

            //cmbZona.attachEvent("onChange", function (value) {
            //    cmbPais.clearAll();
            //    cmbZonaCiu.clearAll();
            //    cmbZonaCiu.setComboValue(null);
            //    cmbCiudad.clearAll();
            //    cmbCiudad.setComboValue(null);
            //    var z = cmbZona.getSelectedValue();
            //    cmbPais.setComboValue(null);
            //    cmbPais.setComboText("");
            //    cmbPais.loadXML("DatosFiltroPlaneadorPlanos.ashx?Tipo=Pais&grupo_Id=" + z);
            //});

            //cmbEstado.loadXML("DatosFiltroPlaneadorPlanos.ashx?Tipo=Estado");
                  
            //cmbPais.attachEvent("onChange", function (value) {               
            //    cmbZonaCiuIng.clearAll();
            //    cmbCiudad.clearAll();
            //    cmbCiudad.setComboValue(null);
            //    var p = cmbPais.getSelectedValue();
            //    cmbZonaCiu.setComboValue(null);
            //    cmbZonaCiu.setComboText("");
            //    cmbZonaCiu.loadXML("DatosFiltroPlaneadorPlanos.ashx?Tipo=ZonaCiu&pai_Id=" + p);
            //});  

                          
            //cmbZonaCiu.attachEvent("onChange", function (value) {               
            //    cmbCiudad.clearAll();
            //    var zc = cmbZonaCiu.getSelectedValue();
            //    cmbCiudad.setComboValue(null);
            //    cmbCiudad.setComboText("");
            //    cmbCiudad.loadXML("DatosFiltroPlaneadorPlanos.ashx?Tipo=CiudadIng&SubZ=" + zc);
            //    cmbCiudad.openSelect();
            //});

      
   
            from.setDate(to.getDate());

            myCalendar = new dhtmlXCalendarObject(["a12f","a12f2","a12f4","a12f6",]);
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
        function tomarMesEntrePlaneada(id) {
    
            var mes = document.getElementById(id).value;
            mes = mes.substring(0,7);
            return document.getElementById("a12f1").value = mes;                      
        }
        function tomarMesEntrega(id) {
    
            var mes = document.getElementById(id).value;
            mes = mes.substring(0,7);
            return document.getElementById("a12f3").value = mes;                      
        }
        function tomarMesValidacion(id) {
    
            var mes = document.getElementById(id).value;
            mes = mes.substring(0,7);
            return document.getElementById("a12f5").value = mes;                      
        }
        function tomarMesDespacho(id) {
    
            var mes = document.getElementById(id).value;
            mes = mes.substring(0,7);
            return document.getElementById("a12f7").value = mes;                      
        }        
    </script>
    <fieldset style="border-style: none; border-width: 0px; align-content: flex-start; width: 1280px">
        <table>
            <tr>
                <td style="text-align: right">&nbsp;<label id='lbl_Of' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                    OF-FP</label>
                </td>
                <td>
                    <input type="text" name="txt_Of" value="" id="txt_Of" size="10" />
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
                <td>
                       &nbsp;  &nbsp;  &nbsp;&nbsp;  &nbsp;  &nbsp;
                    &nbsp;  &nbsp;  &nbsp;&nbsp;  &nbsp;  &nbsp;
                    &nbsp;  &nbsp;  
                </td>
                  <td style="text-align:right">
                    &nbsp;<label id='a15a' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                      Tipo_Cotizacíon
                    </label>
                </td>
                <td>
                    <div id="tipoCotizacion" name="tipoCotizacion">
                    </div>
                </td> 
                <td style="text-align: right">&nbsp;<label id='a11f' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">Fech Entrega</label>
                </td>
                <td>
                    <input id="a12f2" name="txt_FechEntrega" type="text" style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; width: 40px;" />
                    <input id="a12f3" name="txt_FechEntrega" type="hidden" style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; width: 48px;" />
                </td>
                <td style="text-align: right">&nbsp;<label id='a13f' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">Fech Validación</label>
                </td>
                <td>
                    <input id="a12f4" name="txt_FechValidacion" type="text" style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; width: 40px;" />
                    <input id="a12f5" name="txt_FechValidacion" type="hidden" style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; width: 48px;" />
                </td>
                <td style="text-align: left">&nbsp;<label id='a11f6' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">Fech Despacho</label>
                </td>
                <td>
                    <input id="a12f6" name="txt_FechDespacho" type="text" style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; width: 40px;" />
                    <input id="a12f7" name="txt_FechDespacho" type="hidden" style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; width: 48px;" />
                </td>
                
            </tr>
            <tr>
               <td style="text-align:right">
                    &nbsp;<label id='a16a' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                      Explosionado
                    </label>
                </td>

                <td>
                    <div id="explosionado" name="explosionado">
                    </div>
                </td>
                <td style="text-align:right">
                    &nbsp;<label id='a17a' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                      Estado
                    </label>
                </td>

                <td>
                    <div id="estado" name="estado">
                    </div>
                </td>
                

                <td></td>
                <td>
                    <input type="button" name="a11" value="Filtrar" id="a11" onclick='  
    tomarMesEntrega("a12f2");
    tomarMesValidacion("a12f4");
    tomarMesDespacho("a12f6");                                      
    grActSeg.filterBy(0, "");
    if (document.getElementById("txt_Of").value != "") {
        grActSeg.filterBy(0, document.getElementById("txt_Of").value, true);
    }  
    if (cmbPais.getComboText() != ""){    
        grActSeg.filterBy(2,cmbPais.getComboText(),true);
    }
    if (cmbTipoCotizacion.getComboText() != ""){    
        grActSeg.filterBy(4,cmbTipoCotizacion.getComboText(),true);
    }
    if (document.getElementById("a12f3").value != "") {
        grActSeg.filterBy(17, document.getElementById("a12f3").value, true);
    }
    if (document.getElementById("a12f5").value != "") {
        grActSeg.filterBy(14, document.getElementById("a12f5").value, true);
    }
    if (document.getElementById("a12f7").value != "") {
        grActSeg.filterBy(15, document.getElementById("a12f7").value, true);
    }   
    if (cmbExplosionado.getComboText() != ""){    
        grActSeg.filterBy(19,cmbExplosionado.getComboText(),true);
    }
    if (cmbEstado.getComboText() != ""){    
        grActSeg.filterBy(18,cmbEstado.getComboText(),true);
    }
    '
                        style="font-family: Arial, Helvetica, sans-serif; font-size: 9pt; color: #FFFFFF; background-color: #1C5AB6; width: 65px;" />
                </td>
                <td></td>
                <td>
                    <asp:LinkButton ID="LinkButton1" runat="server"
                        Font-Names="Arial" Font-Size="8pt" OnClick="lkActualizar_Click">REFRESCAR</asp:LinkButton>
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
            grActSeg2.loadXML("GrillaObservacionesSegPlanosFabri.ashx?IdOfa=" + id + "&Rol=" + vRol);
            
            grActSeg4.clearAll();
            grActSeg4.loadXML("GrillaPlanosFabricacionLog.ashx?IdOfa=" + id + "&Rol=" + vRol);             
        });


   
        grActSeg.setDateFormat("%Y-%m-%d");

        grActSeg.init();
        grActSeg.setSkin("dhx_skyblue")
        grActSeg.splitAt(7);
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
        var dp = new dataProcessor("GrillaContenPlanosFabricacion.ashx?Rol=" + vRol + "&Usu=" + vUsu);
        dp.init(grActSeg);



        dhx_globalImgPath = "codebase/imgs/";
        grActSeg2 = new dhtmlXGridObject('gridbox2');
        grActSeg2.setImagePath("codebase/imgs/");
        grActSeg2.setDateFormat("%Y-%m-%d");

        grActSeg2.init();
        grActSeg2.splitAt(1);
        grActSeg2.setSkin("dhx_skyblue")
        grActSeg2.loadXML("GrillaObservacionesSegPlanosFabri.ashx?IdOfa=-1&Rol=" + vRol + "&Usu=" + vUsu);

        var dp2 = new dataProcessor("GrillaObservacionesSegPlanosFabri.ashx?IdOfa=-2&Rol=" + vRol + "&Usu=" + vUsu);
        dp2.init(grActSeg2);

        dhx_globalImgPath = "codebase/imgs/";
        grActSeg4 = new dhtmlXGridObject('gridbox4');
        grActSeg4.setImagePath("codebase/imgs/");
        grActSeg4.setDateFormat("%Y-%m-%d");

        grActSeg4.init();
        grActSeg4.splitAt(1);
        grActSeg4.setSkin("dhx_skyblue")
        grActSeg4.loadXML("GrillaPlanosFabricacionLog.ashx?IdOfa=-1&Rol=" + vRol + "&Usu=" + vUsu);

        var dp4 = new dataProcessor("GrillaPlanosFabricacionLog.ashx?IdOfa=-2&Rol=" + vRol + "&Usu=" + vUsu);
        dp4.init(grActSeg4);


        dhx_globalImgPath = "codebase/imgs/";       
                       
    function LoadData() {

            var vRol = <%=Session["Rol"] %>;
            var vUsu = '<%=Session["Usuario"] %>';   
                                             
            //grActSeg.attachFooter("Cantidad Total,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,#cspan,,,,,,<div id='nr_q' class='divcolor' ;>0</div>,<div id='sr_q' class='divcolor'>0</div>,<div id='or_q' class='divcolor'>0</div>,<div id='pr_q' class='divcolor'>0</div>,<div id='qr_q' class='divcolor'>0</div,<div id='rr_q' class='divcolor'>0</div,<div id='tr_q' class='divcolor'>0</div",["text-align:left; font-size:12pt; color:black;"]);
 

        grActSeg.loadXML("GrillaContenPlanosFabricacion.ashx?Rol=" + vRol + 
                "&Usu=" + vUsu + "" , function (){
                   
                    cmbEstado = grActSeg.getColumnCombo(18);
                    cmbEstado.loadXML("DatosFiltro.ashx?Tipo=EstIngenieria");

                    cmbComplejidad = grActSeg.getColumnCombo(12);
                    cmbComplejidad.loadXML("DatosFiltro.ashx?Tipo=Complejidad");
               

                    return true;       
                                                                                  
            });          
    }

        function AddObservacion() {
            var id = grActSeg2.uid();
            var today = new Date();
            var fila = grActSeg.cells(grActSeg.getSelectedId(), 0).getValue() + ',Seguimiento planos de fabricación,' + today.yyyymmdd() + ', ,' + grActSeg.getSelectedId();
            grActSeg2.addRow(id, fila, 0);
            grActSeg2.showRow(id);             
        }
    </script>                                    
</asp:Content>

