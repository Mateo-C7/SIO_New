<%@ Page Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="MaestroFleteInternacional.aspx.cs"
    Inherits="GrillaExample.MaestroFleteInternacional" %>

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
      <%--  <script src="codebase/ext/dhtmlxgrid_math.js"></script>--%>
        <script>

            var cmbTransportador;
            var cmbOrigen;
            var cmbDestino;
            function doOnLoad() {
                // init values
                cmbPais = new dhtmlXCombo("a12b", "combo", 180);
                cmbPais.loadXML("ComboFleteInternacional.ashx");
                cmbPais.enableFilteringMode(true);
                cmbDestino = new dhtmlXCombo("a12d", "combo", 180);

                cmbPais.attachEvent("onChange", function (value) {
                    cmbDestino.clearAll();
                    var z = cmbPais.getSelectedValue();

                    cmbDestino.setComboValue(null);
                    cmbDestino.setComboText("");
                    cmbDestino.loadXML("ComboFleteInternacional.ashx?Tipo=Dest&Pa_Id=" + z);
                    cmbDestino.enableFilteringMode(true);

                });
                cmbOrigen = new dhtmlXCombo("a12c", "combo", 180);
                cmbOrigen.loadXML("ComboFleteInternacional.ashx?Tipo=Orig");
                cmbOrigen.enableFilteringMode(true);

                cmbAgente = new dhtmlXCombo("a12e", "combo", 180);
                cmbAgente.loadXML("ComboFleteInternacional.ashx?Tipo=Agente");
                cmbAgente.enableFilteringMode(true);

            }

            function byId(id) {
                return document.getElementById(id);
            }

        </script>
        <fieldset style="width: 800px">
            <table style="width: 283px">
                <tr>
                    <td>
                        <label id='a10e' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                            Agente.Carga
                        </label>
                    </td>
                    <td>
                        <div id="a12e" name="a12e">
                        </div>
                    </td>
                    <td>
                        <label id='a10c' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                            Puerto.Origen
                        </label>
                    </td>
                    <td>
                        <div id="a12c" name="a12c">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label id='a10b' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                            Pais.Destino
                        </label>
                    </td>
                    <td>
                        <div id="a12b" name="a12b">
                        </div>
                    </td>
                    <td>
                        <label id='a10d' style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                            Puerto.Destino
                        </label>
                    </td>
                    <td>
                        <div id="a12d" name="a12d">
                        </div>
                    </td>
                    <td>
                        <input type="button" name="a11" value="Filtrar" id="a11" onclick='
                        grFlete.clearAll();
                        grFlete.loadXML("GrillaFleteInternacional.ashx?Pais=" + cmbPais.getSelectedValue()
                        + "&Origen=" + cmbOrigen.getSelectedValue() + "&Destino=" + cmbDestino.getSelectedValue()
                        + "&Agente=" + cmbAgente.getSelectedValue()); 
                        gridObs.clearAll();
                        gridObs.loadXML("GrillaFleteInternacional.ashx?Tipo=2&Pais=" + cmbPais.getSelectedValue()
                        + "&Origen=" + cmbOrigen.getSelectedValue() + "&Destino=" + cmbDestino.getSelectedValue()
                        + "&Agente=" + cmbAgente.getSelectedValue());                        
                        ' style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #FFFFFF;
                            background-color: #1C5AB6; font-weight: bold;"/>
                    </td>
                </tr>
            </table>
            <br />
        </fieldset>

        <div id="gridbox" style="width: 1000px; height: 120px; background-color: white;">
        </div>

        <div id="gridbox2" style="width: 1000px; height: 400px; background-color: white;">
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
            grFlete.setNumberFormat("0,000.00", 3, ".", ",");
            grFlete.setNumberFormat("0,000.00", 4, ".", ",");
            grFlete.setNumberFormat("0,000.00", 5, ".", ",");
            grFlete.setNumberFormat("0,000.00", 6, ".", ",");
            grFlete.setNumberFormat("0,000.00", 7, ".", ",");

            //grTema.splitAt(2);
            grFlete.enableSmartRendering(true, 50);
            var dp = new dataProcessor("GrillaFleteInternacional.ashx");
            dp.init(grFlete);
           
            
            gridObs = new dhtmlXGridObject('gridbox2');
            gridObs.setImagePath("codebase/imgs/");
            //grFlete.setDateFormat("%Y-%m-%d");
            gridObs.init();
            gridObs.setSkin("dhx_skyblue")           
            gridObs.setNumberFormat("0,000.00", 2, ".", ",");
            gridObs.setNumberFormat("0,000.00", 3, ".", ",");
            gridObs.setNumberFormat("0,000.00", 4, ".", ",");
            gridObs.setNumberFormat("0,000.00", 5, ".", ",");
            gridObs.setNumberFormat("0,000.00", 6, ".", ",");
            gridObs.setNumberFormat("0,000.00", 7, ".", ",");
            gridObs.setNumberFormat("0,000.00", 8, ".", ",");

            //grTema.splitAt(2);
            gridObs.enableSmartRendering(true, 50);
        </script>      
</asp:Content>