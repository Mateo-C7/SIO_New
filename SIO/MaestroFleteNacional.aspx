<%@ Page Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="MaestroFleteNacional.aspx.cs"
    Inherits="GrillaExample.MaestroFleteNacional" %>

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
    <script src="codebase/dhtmlxcommon.js"></script>
    <script src="codebase/dhtmlxcombo.js"></script>
    <script src="codebase/dhtmlxcalendar.js"></script>
    <script src="codebase/dhtmlxgrid.js"></script>
    <script src="codebase/dhtmlxgridcell.js"></script>
    <script src="codebase/ext/dhtmlxgrid_splt.js"></script>
    <script src="codebase/ext/dhtmlxgrid_filter.js"></script>
    <script src="codebase/ext/dhtmlxgrid_srnd.js"></script>
    <script src="codebase/excells/dhtmlxgrid_excell_dhxcalendar.js"></script>
    <script src="codebase/excells/dhtmlxgrid_excell_combo.js"></script>
    <script src="codebase/dhtmlxdataprocessor.js"></script>
    <script src="codebase/connector.js"></script>
    <style type="text/css">
        input#txtFecIni, input#txtFecFin
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
    </style>
    <script>
        Date.prototype.yyyymmdd = function () {

            var yyyy = this.getFullYear().toString();
            var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based         
            var dd = this.getDate().toString();

            return yyyy + '-' + (mm[1] ? mm : "0" + mm[0]) + '-' + (dd[1] ? dd : "0" + dd[0]);
        };
        var myCalendar;
        var cmbTransportador;
        var cmbOrigen;
        var cmbDestino;
        function doOnLoad() {
            // init values
            cmbTransportador = new dhtmlXCombo("a12b", "combo", 180);
            cmbTransportador.loadXML("ComboFleteNacional.ashx");
            cmbTransportador.enableFilteringMode(true);

            cmbTransportador2 = new dhtmlXCombo("a12g", "combo", 180);
            cmbTransportador2.loadXML("ComboFleteNacional.ashx");
            cmbTransportador2.enableFilteringMode(true);

            cmbPais = new dhtmlXCombo("a12e", "combo", 180);
            cmbPais.loadXML("ComboFleteNacional.ashx?Tipo=Pai");
            cmbPais.enableFilteringMode(true);
            
            cmbVehiculo = new dhtmlXCombo("a12f", "combo", 180);
            cmbVehiculo.loadXML("ComboFleteNacional.ashx?Tipo=Veh");
            cmbVehiculo.enableFilteringMode(true);

            cmbOrigen = new dhtmlXCombo("a12c", "combo", 180);
            cmbDestino = new dhtmlXCombo("a12d", "combo", 180);

            cmbPais.attachEvent("onChange", function (value) {
                cmbOrigen.clearAll();
                cmbDestino.clearAll();
                var z = cmbPais.getSelectedValue();

                cmbOrigen.setComboValue(null);
                cmbOrigen.setComboText("");

                cmbOrigen.loadXML("ComboFleteNacional.ashx?Tipo=Ciu&Pa_Id=" + z);
                cmbOrigen.enableFilteringMode(true);

                cmbDestino.setComboValue(null);
                cmbDestino.setComboText("");

                cmbDestino.loadXML("ComboFleteNacional.ashx?Tipo=Ciu&Pa_Id=" + z);
                cmbDestino.enableFilteringMode(true);
            });
            var from = new Date();
            var to = new Date();
            from.setDate(to.getDate() - 365);

            myCalendar = new dhtmlXCalendarObject(["txtFecIni", "txtFecFin"]);
            myCalendar.setSkin('dhx_skyblue');
            myCalendar.setDate(from.yyyymmdd());
            myCalendar.hideTime();
            byId("txtFecIni").value = from.yyyymmdd();
            alert(to.yyyymmd());
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

    </script>

    <fieldset style="width: 600px">
        <table style="width: 180px">
            <tr>
                <td>
                    <label id='a10b' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        Transportador
                    </label>
                </td>
                <td>
                    <div id="a12b" name="a12b">
                    </div>
                </td>
                <td>
                    <label id='a10e' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        Pais
                    </label>
                </td>
                <td>
                    <div id="a12e" name="a12e">
                    </div>
                </td>
                
            </tr>
            <tr>
                <td>
                    <label id='a10c' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        Ciudad.Origen
                    </label>
                </td>
                <td>
                    <div id="a12c" name="a12c">
                    </div>
                </td>
                <td>
                    <label id='a10d' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        Ciudad.Destino
                    </label>
                </td>
                <td>
                    <div id="a12d" name="a12d">
                    </div>
                </td>
                <td>
                    <input type="button" name="a11" value="Filtrar" id="a11" onclick='
                        grFlete.clearAll();
                        grFlete.loadXML("GrillaFleteNacional.ashx?Transp=" + cmbTransportador.getSelectedValue()
                        + "&Origen=" + cmbOrigen.getSelectedValue() + "&Destino=" + cmbDestino.getSelectedValue());                        
                        grObs.clearAll();
                        grObs.loadXML("GrillaFleteNacional.ashx?Tipo=2&Transp=0&Vehiculo=0" 
                        + "&Origen=" + cmbOrigen.getSelectedValue() + "&Destino=" + cmbDestino.getSelectedValue());                        
                        ' style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #FFFFFF;
                        background-color: #1C5AB6; font-weight: bold;">
                </td>
            </tr>
        </table>
        <br />
    </fieldset>
    &nbsp;
    <div id="gridbox" style="width: 600px; height: 120px; background-color: white;">
    </div>
     &nbsp;    
    <fieldset style="width: 600px">
        <table style="width: 180px">
            <tr>
              <td>
                    <label id='a10g' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        Transportador
                    </label>
                </td>
                <td>
                    <div id="a12g" name="a12g">
                    </div>
                </td>
                <td>
                    <label id='a10f' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        Tipo.Vehiculo
                    </label>
                </td>
                <td>
                    <div id="a12f" name="a12f">
                    </div>
                </td>
                <td>
                    <input type="button" name="a13" value="Filtrar" id="a13" onclick='                                             
                        grObs.clearAll();
                        grObs.loadXML("GrillaFleteNacional.ashx?Tipo=2&Transp=" + cmbTransportador2.getSelectedValue()
                        + "&Vehiculo=" + cmbVehiculo.getSelectedValue() 
                        + "&Origen=" + cmbOrigen.getSelectedValue() + "&Destino=" + cmbDestino.getSelectedValue());                        
                        ' style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #FFFFFF;
                        background-color: #1C5AB6; font-weight: bold;">
                </td>
            </tr>
        </table>
    </fieldset>
     &nbsp;
    <div id="gridbox2" style="width: 600px; height: 500px; background-color: white;">
    </div>
    <script>
        dhx_globalImgPath = "codebase/imgs/";
        grFlete = new dhtmlXGridObject('gridbox');
        grFlete.setImagePath("codebase/imgs/");
        //grFlete.setDateFormat("%Y-%m-%d");
        grFlete.init();
        grFlete.setSkin("dhx_skyblue")
        grFlete.setNumberFormat("0,000.00", 1, ".", ",");
        grFlete.setNumberFormat("0,000.00", 2, ".", ",");

        //grTema.splitAt(2);
        grFlete.enableSmartRendering(true, 50);
        //            grFlete.loadXML("GrillaFleteNacional.ashx?Tipo=1");
        var dp = new dataProcessor("GrillaFleteNacional.ashx");
        dp.init(grFlete);

        dhx_globalImgPath = "codebase/imgs/";
        grObs = new dhtmlXGridObject('gridbox2');
        grObs.setImagePath("codebase/imgs/");
        //grFlete.setDateFormat("%Y-%m-%d");
        grObs.init();
        grObs.setSkin("dhx_skyblue")
        grObs.setNumberFormat("0,000.00", 2, ".", ",");
        grObs.setNumberFormat("0,000.00", 3, ".", ",");

        //grTema.splitAt(2);
        grObs.enableSmartRendering(true, 50);
        //            grObs.loadXML("GrillaFleteNacional.ashx?Tipo=2");

    </script>
</asp:Content>
