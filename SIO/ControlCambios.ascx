<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlCambios.ascx.cs"
    Inherits="SIO.ControlCambios" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body onload="doOnLoad();>
    <form id="form1" runat="server">
    <div>
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
            function doOnLoad() {
                // init values
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
        <div id="gridbox" style="width: 800px; height: 300px; background-color: white;">
        </div>
        <input type="button" name="add" value="Adicionar Tema" onclick="AddTema()"
            style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #FFFFFF;
            background-color: #1C5AB6; font-weight: bold; width: 180px;">
        <div id="gridbox2" style="width: 800px; height: 120px; background-color: white;">
        </div>
        
        <input type="button" name="add" value="Adicionar Comentario" onclick="AddComentario()"
            style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #FFFFFF;
            background-color: #1C5AB6; font-weight: bold; width: 180px;">
        
        <script>
            dhx_globalImgPath = "codebase/imgs/";
            grTema = new dhtmlXGridObject('gridbox');
            grTema.setImagePath("codebase/imgs/");
            grTema.attachEvent("onRowSelect", function (id) {

                grComentario.clearAll();
                grComentario.loadXML("GrillaControlCambioComentario.ashx?IdCom=" + id);
            });

            grTema.setDateFormat("%Y-%m-%d");
            grTema.init();
            grTema.setSkin("dhx_skyblue")
            //grTema.splitAt(2);
            grTema.enableSmartRendering(true, 50);
            LoadData(null);
            var dp = new dataProcessor("GrillaControlCambioTema.ashx");
            dp.init(grTema);

            dhx_globalImgPath = "codebase/imgs/";
            grComentario = new dhtmlXGridObject('gridbox2');
            grComentario.setImagePath("codebase/imgs/");
            grComentario.setDateFormat("%Y-%m-%d");

            grComentario.init();
            //grActSeg2.splitAt(2);
            grComentario.setSkin("dhx_skyblue")
            grComentario.loadXML("GrillaControlCambioComentario.ashx?IdCom=-1");

            var dp2 = new dataProcessor("GrillaControlCambioComentario.ashx?IdCom=-2");
            dp2.init(grComentario);

            function LoadData(fup) {
                grTema.loadXML("GrillaControlCambioTema.ashx?fup=1", function Combos() {

                    cmbArea = grTema.getColumnCombo(5);
                    cmbResponsable = grTema.getColumnCombo(6);
                    cmbArea.loadXML("ComboControlCambio.ashx");
                    cmbArea.attachEvent("onChange", function (value) {
                        cmbResponsable.clearAll();
                        var z = cmbArea.getSelectedValue();

                        cmbResponsable.setComboValue(null);
                        cmbResponsable.setComboText("");
                        cmbResponsable.loadXML("ComboControlCambio.ashx?Tipo=Emp&Are_Id=" + z);
                    });
                });
            }

            function AddComentario() {
                var id = grComentario.uid();
                var today = new Date();
                var fila = today.yyyymmdd() + ',,USUprueba,,' + grTema.getSelectedId();
                alert(fila);
                grComentario.addRow(id, fila, 0);
                grComentario.showRow(id);
            }

            function AddTema() {
                var id = grTema.uid();
                var today = new Date();
                var fila = '1,,' + today.yyyymmdd() + ',,usuprueba,,,Abierto';
                grTema.addRow(id, fila, 0);
                grTema.showRow(id);
            }
        </script>
    </div>
    </form>
</body>
</html>