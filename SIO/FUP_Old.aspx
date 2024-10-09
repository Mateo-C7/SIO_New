<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="FUP_Old.aspx.cs" Inherits="SIO.FUP" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script runat="server">
    protected void FormViewAnexos_PageIndexChanging(object sender, FormViewPageEventArgs e)
    {

    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Navigate() {
            javascript: window.open("SolicitudFacturacionNew.aspx");
            //javascript: window.open("SolicitudFacturacion.aspx");
        }
    </script>
    <script type="text/javascript">
        function Lista() {
            javascript: window.open("ReporteListaChequeo.aspx");
        }
    </script>
    <%--<script type="text/javascript">
        //Postback is necessary for updating the status text if you don't want to implement it in javascript
//        function UploadComplete(sender, args) {
//            __doPostBack('<%=UpdatePanel1.ClientID%>', '');
//        }

    </script>--%>
    <style type="text/css">
        /* Accordion */
        .accordionHeader
        {
            border: 2px Outset #EBEBEB;
            color: white;
            background-color: #1C5AB6;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            border-left: 1px solid #1C5AB6;
            border-right: 1px solid #1C5AB6;
            border-top: 1px solid #1C5AB6;
            cursor: pointer;
            z-index: 2;
            font-weight: bold;
            text-decoration: none;
            color: #ffffff;
            text-shadow: 0 1px 1px rgba(0, 0, 0, 0.35);
            background: #1C5AB6;
            background: -webkit-linear-gradient(#1c5ab6, #1fa0e4);
            background: -moz-linear-gradient(#1c5ab6, #1fa0e4);
            background: -o-linear-gradient(#1c5ab6, #1fa0e4);
            background: -ms-linear-gradient(#1c5ab6, #1fa0e4);
            text-align: center;
        }
        
        #master_content .accordionHeader a
        {
            color: #FFFFFF;
            background: none;
            text-decoration: none;
        }
        
        #master_content .accordionHeader a:hover
        {
            background: none;
            text-decoration: underline;
        }
        
        .accordionHeaderSelected
        {
            border: 2px Outset #EBEBEB;
            color: white;
            background-color: #1C5AB6;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            border-left: 1px solid #1C5AB6;
            border-right: 1px solid #1C5AB6;
            border-top: 1px solid #1C5AB6;
            cursor: pointer;
            z-index: 2;
            font-weight: bold;
            text-decoration: none;
            color: #ffffff;
            text-shadow: 0 1px 1px rgba(0, 0, 0, 0.35);
            background: #1C5AB6;
            background: -webkit-linear-gradient(#1c5ab6, #1fa0e4);
            background: -moz-linear-gradient(#1c5ab6, #1fa0e4);
            background: -o-linear-gradient(#1c5ab6, #1fa0e4);
            background: -ms-linear-gradient(#1c5ab6, #1fa0e4);
            text-align: center;
        }
        
        #master_content .accordionHeaderSelected a
        {
            color: #FFFFFF;
            background: none;
            text-decoration: none;
        }
        
        #master_content .accordionHeaderSelected a:hover
        {
            background: none;
            text-decoration: underline;
        }
        
        .accordionContent
        {
            border: 0px outset #2F4F4F;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
        }
        .CustomComboBoxStyle .ajax__combobox_buttoncontainer button
        {
            background-image: url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-arrow.gif');
            border-style: none;
        }
        .CustomComboBoxStyle .ajax__combobox_textboxcontainer input
        {
            background-image: url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-bg.gif');
            border-style: none;
        }
        .CustomComboBoxStyle .ajax__combobox_itemlist li
        {
            color: Black;
            font-size: 8pt;
            font-family: Arial;
            background-color: #EBEBEB;
        }
        .izquierda
        {
            width: 108px;
            text-align: izquierda;
        }
        .derecha
        {
            width: 108px;
            text-align: derecha;
        }
        .centrado
        {
            width: 108px;
            text-align: centrado;
        }
        .Detalle
        {
            width: 749px;
            height: 145px;
        }
        .style26
        {
            text-align: right;
            margin-left: 40px;
        }
        .style81
        {
            height: 18px;
            text-align: right;
        }
        .style94
        {
            width: 130px;
            text-align: justify;
        }
        .style98
        {
            text-align: right;
            margin-left: 40px;
            height: 18px;
        }
        .style104
        {
            width: 243px;
            text-align: center;
        }
        .style110
        {
            width: 123px;
        }
        .style111
        {
            width: 120px;
        }
        .style114
        {
            width: 55%;
        }
        .style119
        {
            text-align: center;
            width: 185px;
        }
        .style121
        {
            width: 142px;
            text-align: right;
        }
        .style123
        {
            width: 18px;
            text-align: right;
        }
        .style124
        {
            width: 20px;
        }
        .style126
        {
            width: 81px;
        }
        
        .style127
        {
            width: 130px;
        }
        
        .style128
        {
            width: 18px;
        }
        .style130
        {
            text-align: center;
        }
        
        .style137
        {
            width: 116px;
        }
        
        .style138
        {
            width: 300px;
        }
        
        .style152
        {
            width: 900px;
        }
        .style154
        {
            width: 66px;
        }
        .style155
        {
            width: 24px;
        }
        .style156
        {
            width: 72px;
        }
        
        .CustomComboBoxStyle
        {
            text-align: right;
        }
        .CustomComboBoxStyle
        {
            text-align: left;
        }
        
        .style157
        {
            width: 100%;
        }
        
        .modalPopup
        {
            background-color: #FFFFC0;
            border-width: 1px;
            border-style: Solid;
            border-color: Gray;
            padding: 1px;
            width: 250px;
        }
        
        .overlay
        {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: #aaa;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }
        .overlayContent
        {
            z-index: 99;
            margin: 250px auto;
            width: 80px;
            height: 80px;
        }
        .overlayContent h2
        {
            font-size: 18px;
            font-weight: bold;
            color: #000;
        }
        .overlayContent img
        {
            width: 80px;
            height: 80px;
        }
        .Grid td
        {
            background-color: #A1DCF2;
            color: black;
            font-size: 7pt;
            border-style:none;
        }
        .Grid th
        {
            background-color: #3AC0F2;
            color: White;
            font-size: 7pt;
        }
        .ChildGrid td
        {
            background-color: #eee !important;
            color: black;
            font-size: 7pt;
            line-height: 200%;
        }
        .ChildGrid th
        {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 7pt;
            line-height: 200%;
        }
        .Nested_ChildGrid td
        {
            background-color: #fff !important;
            color: black;
            font-size: 7pt;
            line-height: 200%;
        }
        .Nested_ChildGrid th
        {
            background-color: #2B579A !important;
            color: White;
            font-size: 7pt;
            line-height: 200%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
            <asp:PostBackTrigger ControlID="lkrapidisimo"  /> 
                         
        </Triggers>       
        

        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <table style="width: 895px;">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlDatosGenerales" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        GroupingText="Datos Generales" Width="900px" ForeColor="Black">
                                        <table style="height: 94px; width: 647px;">
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblPais" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Pais"
                                                        Width="45px" ForeColor="Black"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:ComboBox ID="cboPais" runat="server" AutoCompleteMode="Append" AutoPostBack="True"
                                                        DropDownStyle="DropDownList" Font-Names="Arial" Font-Size="8pt" OnSelectedIndexChanged="cboPais_SelectedIndexChanged"
                                                        Width="200px">
                                                    </asp:ComboBox>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblCiudad" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Ciudad"
                                                        Width="60px" ForeColor="Black"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:ComboBox ID="cboCiudad" runat="server" AutoCompleteMode="Append" AutoPostBack="True"
                                                        DropDownStyle="DropDownList" Font-Names="Arial" Font-Size="8pt" OnSelectedIndexChanged="cboCiudad_SelectedIndexChanged"
                                                        Width="195px">
                                                    </asp:ComboBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" Width="30px"></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblFUP1" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                        Text="FUP" Width="30px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFUP" runat="server" AutoPostBack="True" BackColor="#FFFF66" Font-Names="Arial"
                                                        Font-Size="8pt" OnTextChanged="txtFUP_TextChanged" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblCliente" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Empresa"
                                                        Width="70px" ForeColor="Black"></asp:Label>
                                                </td>
                                                <td colspan="3" style="text-align: left">
                                                    <asp:ComboBox ID="cboCliente" runat="server" AutoCompleteMode="Suggest" AutoPostBack="True"
                                                        DropDownStyle="DropDownList" Font-Names="Arial" Font-Size="8pt" OnSelectedIndexChanged="cboCliente_SelectedIndexChanged"
                                                        Width="484px">
                                                    </asp:ComboBox>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:LinkButton ID="lkCliente" runat="server" Enabled="False" OnClick="lkCliente_Click">&gt;&gt;</asp:LinkButton>
                                                </td>
                                                <td style="text-align: left">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblProducido" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                        Text="Producido En " Width="70px"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:ComboBox ID="cboProducido" runat="server" AutoCompleteMode="Append" AutoPostBack="True"
                                                        DropDownStyle="DropDownList" Font-Names="Arial" Font-Size="8pt" Width="85px">
                                                        <asp:ListItem>Colombia</asp:ListItem>
                                                    </asp:ComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblContacto" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Contacto"
                                                        Width="70px" ForeColor="Black"></asp:Label>
                                                </td>
                                                <td colspan="3" style="text-align: left">
                                                    <asp:ComboBox ID="cboContacto" runat="server" AutoCompleteMode="Suggest" AutoPostBack="True"
                                                        DropDownStyle="DropDownList" Font-Names="Arial" Font-Size="8pt" Width="484px">
                                                    </asp:ComboBox>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:LinkButton ID="lkContacto" runat="server" Enabled="False" OnClick="lkContacto_Click">&gt;&gt;</asp:LinkButton>
                                                </td>
                                                <td style="text-align: left">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblOF0" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                        Text="Orden" Width="40px"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtOF" runat="server" AutoPostBack="True" BackColor="#FFFF66" Font-Names="Arial"
                                                        Font-Size="8pt" OnTextChanged="txtOF_TextChanged" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblObra" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                        Text="Obra" Width="50px" ForeColor="Black"></asp:Label>
                                                </td>
                                                <td colspan="3" style="text-align: left">
                                                    <asp:ComboBox ID="cboObra" runat="server" AutoCompleteMode="Suggest" AutoPostBack="True"
                                                        DropDownStyle="DropDownList" Font-Names="Arial" Font-Size="8pt" Width="484px"
                                                        OnSelectedIndexChanged="cboObra_SelectedIndexChanged">
                                                    </asp:ComboBox>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:LinkButton ID="lkObra" runat="server" Enabled="False" OnClick="lkObra_Click">&gt;&gt;</asp:LinkButton>
                                                </td>
                                                <td style="text-align: left">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Arrow back123.png"
                                                        OnClick="ImageButton1_Click" Style="text-align: right" ToolTip="Volver a Empresa"
                                                        Visible="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblUnd" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="Black" Style="text-align: right" Text="Unds Construir*" Width="90px"></asp:Label>
                                              
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtUnidades" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: center"
                                                        Width="40px"></asp:TextBox>
                                                         <asp:Label ID="lblM3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="Black" Style="text-align: right" Text="M² Vivienda *" Width="80px"></asp:Label>
                                                <asp:TextBox ID="txtM2" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: center"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right">
                                                  <asp:Label ID="lblMoneda" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Moneda*"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                   <asp:ComboBox ID="cboMoneda" runat="server" Font-Names="Arial" Font-Size="8pt" Width="90px">
                                                      <asp:ListItem Value="1">
                                                        <div align="right">Pesos</div>
                                                      </asp:ListItem>
                                                    </asp:ComboBox>
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblEstado0" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="Black" Text="Estado Cliente" Width="90px"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="LEstadoCli" runat="server" Font-Italic="True" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="#0000CC" Style="text-align: left; margin-bottom: 0px;" Width="100px"></asp:Label>
                                                    .
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                  <asp:Label ID="lblEstrato0" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="Black" Style="text-align: right" Text="Estrato *" Width="60px"></asp:Label>
                                                
                                                   </td>
                                                <td style="text-align: left">
                                                     <asp:ComboBox ID="cboEstrato" runat="server" Font-Names="Arial" Font-Size="8pt" Width="150px">
                                                    </asp:ComboBox>
                                                </td>
                                                <td style="text-align: left">
                                                
                                                      </td>
                                                <td style="text-align: left">
                                                   
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblEstado" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="black" Text="Estado Fup" Width="90px"></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label  ID="LEstado" runat="server" Font-Italic="True" Font-Names="Arial" Font-Size="10pt"
                                                       BackColor = "Yellow" ForeColor="Black" Style="text-align: center" Font-Bold="True"
                                                        Width="110px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblVivienda" runat="server" Font-Bold="False" Font-Names="Arial" Font-Overline="False"
                                                        Font-Size="8pt" ForeColor="Black" Height="16px" Style="text-align: right; margin-right: 0px;"
                                                        Text="Tipo Vivienda *" Width="90px"></asp:Label>
                                                <td style="text-align: left">
                                                   <asp:ComboBox ID="cboVivienda" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                        Width="150px">
                                                    </asp:ComboBox>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="Label30" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Probabilidad"></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblProbabilidad" runat="server" Font-Names="Arial" Font-Size="8pt" Text="P"></asp:Label>
                                                    <asp:Label ID="lblFechaFac" runat="server" Font-Names="Arial" Font-Size="8pt" Text="P"></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td colspan="2" style="text-align: left">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                        OnClick="LinkButton1_Click">Fup Blanco</asp:LinkButton>
                                                          &nbsp;&nbsp;<asp:LinkButton ID="lkrapidisimo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                        OnClick="lkrapidisimo_Click">Rapidisimo</asp:LinkButton>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Button ID="btnNuevo" runat="server" BackColor="#1C5AB6" BorderColor="#999999"
                                                        Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" OnClick="btnNuevo_Click"
                                                        Text="Nuevo" Width="70px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlMercadeo" runat="server" Font-Names="Arial" Font-Size="8pt" GroupingText="Mercadeo"
                                        Visible="False" Width="800px" ForeColor="Black">
                                        <table class="style114">
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblEmailContacto0" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                        Text="E-Mail Contacto" Width="80px" ForeColor="Black"></asp:Label>
                                                </td>
                                                <td style="text-align: justify">
                                                    <asp:Label ID="lblEmailCont" runat="server" Font-Italic="True" Font-Names="Arial"
                                                        Font-Size="8pt" ForeColor="#0000CC" Width="180px"></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblTipoViv" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo De Vivienda"
                                                        Width="100px" ForeColor="Black"></asp:Label>
                                                </td>
                                                <td style="text-align: justify">
                                                    <asp:Label ID="LTipoViv" runat="server" Font-Bold="False" Font-Italic="True" Font-Names="Arial"
                                                        Font-Size="8pt" ForeColor="#0000CC" Width="100px"></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblAreaViv1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="M2 Vivienda"
                                                        Width="100px" ForeColor="Black"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LAreaViv" runat="server" Font-Italic="True" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="#0000CC" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: justify">
                                                    <asp:Label ID="lblCargo" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cargo/Profesión"
                                                        Width="60px" ForeColor="Black"></asp:Label>
                                                </td>
                                                <td style="text-align: justify">
                                                    <asp:Label ID="LCargo" runat="server" Font-Italic="True" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="#0000CC" Width="180px"></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblEstrato" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Estrato"
                                                        Width="70px" ForeColor="Black"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LEstrato" runat="server" Font-Italic="True" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="#0000CC" Width="100px"></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblNumViv" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Total Unidades A Construir"
                                                        Width="120px" ForeColor="Black"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LNumViv" runat="server" Font-Italic="True" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="#0000CC" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblProf0" runat="server" Font-Names="Arial" Font-Size="8pt" Text=" "
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td style="text-align: justify">
                                                    <asp:Label ID="LProf" runat="server" Font-Italic="True" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="#0000CC" Width="180px"></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp; &nbsp;
                        <br />
                        <span>
                            <asp:Label ID="lblCreoFup" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Guardó El FUP."
                                Width="85px" ForeColor="Black"></asp:Label>
                            <asp:Label ID="LCreoFup" runat="server" Font-Bold="False" Font-Italic="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="#0000CC" Width="300px"></asp:Label>
                            <asp:Label ID="lblCreoSalida" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cotizado por"
                                Width="70px" ForeColor="Black"></asp:Label>
                            <asp:Label ID="Lcotizadopor" runat="server" Font-Italic="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="#0000CC" Style="text-align: left" Width="350px"></asp:Label>
                                <asp:Label ID="lblcotizRechazada" Visible= "false" runat="server" Font-Italic="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="#000000" Style="text-align: left" Width="150px"></asp:Label>
                        </span>
                        <asp:Accordion ID="Accordion1" runat="server" ContentCssClass="accordionContent"
                            HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            Width="900px" Height="8000px" Font-Names="Arial" Font-Size="8pt" 
                            ForeColor="Black" style="text-align: right">
                            <Panes>
                                <asp:AccordionPane ID="AcorInfoGeneral" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="lblEncabGeneral" runat="server" Text="Informacion General"></asp:Label>
                                    </Header>
                                    <Content>
                                   
                                        <table width="830"     >
                                         
    <div></div>                                            
                             <tr>
                             <td align="left" >
                                </td>
    <td width="150" align="right" ><div align="right">
     <asp:ComboBox ID="cboTipoCotizacion" runat="server" AutoCompleteMode="SuggestAppend" style="text-align: right"
                                                      AutoPostBack="True" DropDownStyle="DropDownList" Font-Names="Arial" Font-Size="8pt"
                                                        Width="100px" OnSelectedIndexChanged="cboTipoCotizacion_SelectedIndexChanged">
      <asp:ListItem Value="0">Tipo De Cotización</asp:ListItem>
                                                        <asp:ListItem Value="1">Equipo Nuevo</asp:ListItem>
                                                        <asp:ListItem Value="2">Adaptacion</asp:ListItem>
                                                        <asp:ListItem Value="3">Listado De Piezas</asp:ListItem>
    </asp:ComboBox>
    </div> 
   </div></td>
                               
    <td width="130"><div align="center">
       <asp:CheckBox ID="chkAccesorios" runat="server" CssClass="derecha" Font-Names="Arial"
                                                        Font-Size="8pt" Text="Accesorios" TextAlign="Left" Width="80px" />  
   </td>
  
    <td ><div align="right">
    <span style="text-align: right">
     
    </td>
    <td align="left">
      <span style="text-align: right">
    <asp:Label ID="lblClase" ForeColor="#0000CC" Width="60px" runat="server" Font-Names="Arial" Font-Size="8pt" Text="."></asp:Label>
    </span></div></td>
    <td width="150"><div align="right">
    <span style="text-align: right">
    </span><span style="text-align: right"> 
      <asp:Label ID="lblNumEquipos" runat="server" Font-Names="Arial" Font-Size="8pt" Text="No De Equipos"
                                                        Width="80px"></asp:Label>
        </td>
         <td align="left">
      <asp:TextBox ID="txtNumEquipos" runat="server" Font-Names="Arial" AutoPostBack="true"
                                                        OnTextChanged="txtNumEquipos_TextChanged" Font-Size="8pt" style="text-align: right"
                                                        Width="20px">
       
      </asp:TextBox>
    </span></div></td>
   <td width="180" align="right"><div align="right">
    <span style="text-align: right"> 
      <asp:Label ID="lblVersion" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Versión"
                                                        Width="40px"></asp:Label> 
         
   </td>
   <td>
       <asp:ComboBox ID="cboVersion" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                        Font-Size="8pt" OnSelectedIndexChanged="cboVersion_SelectedIndexChanged" Width="15px"> </asp:ComboBox>
    
   </td>
  </tr>
  <tr>
  <td></td>
    <td><div align="right">
      <asp:CheckBox ID="chkAluminio" runat="server" Font-Names="Arial" Font-Size="8pt" AutoPostBack= "True"
                                               Text="ALUMINIO *" TextAlign="Left" Width="110px"  OnCheckedChanged="chkAluminio_CheckedChanged1"  />      
    </div></td>

    <td><div align="left">
    <asp:ComboBox ID="cboTipoAluminio" runat="server" AutoPostBack="True" Font-Names="Arial" 
                                                      Font-Size="8pt" Width="80px" Visible ="false"> </asp:ComboBox>
   
     </div> </td>

    <td><div align="right"><span style="text-align: right">
        <asp:Label ID="Label25" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Clase"
                                                        Width="70px"></asp:Label>
   </div></td>
    <td>
      <asp:ComboBox ID="cboClaseCot" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                        Font-Size="8pt" OnSelectedIndexChanged="cboClaseCot_SelectedIndexChanged" Width="60px"> </asp:ComboBox>
   
    
    </td>
    <td><div align="right"><span style="text-align: right">
    </span><span style="text-align: right">
    <asp:CheckBox ID="chkRecotiza" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                        Font-Size="8pt" OnCheckedChanged="chkRecotiza_CheckedChanged" Text="Recotización"
                                                        TextAlign="Left" Visible="False" Width="100px" />    
    </span></div></td>
  </tr>
  <tr>
  <td></td>
    <td><div align="right">
      <asp:CheckBox ID="chkAcero" runat="server" Font-Names="Arial" Font-Size="8pt" Text="ACERO *" AutoPostBack= "True"
         TextAlign="Left" Width="120px"  OnCheckedChanged="chkAcero_CheckedChanged1"   />      
    </div></td>
     <td><div align="left">
    <asp:ComboBox ID="cboTipoAcero" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                        Font-Size="8pt" Width="80px" Visible ="false"> </asp:ComboBox>
   </div>
   </td>
    <td width="80">  </td>
    <td align="left">
   </td>
    <td><div align="right"><span style="text-align: right">
    </span><span style="text-align: right">
    <asp:CheckBox ID="chkPlanoForsa" runat="server" Font-Names="Arial" AutoPostBack="True"
                                                        Font-Size="8pt" Text="Planos TipoForsa" TextAlign="Left" Width="120px"
                                                        Visible="False" OnCheckedChanged="chkPlanoForsa_CheckedChanged" />    
    </span></div></td>
    <td>
        <asp:Label ID="Label27" runat="server" Font-Names="Arial" Font-Size="8pt" Text="."
                                                        Width="5px"></asp:Label>
    </td>
    <td><div align="right"><span style="text-align: right">
    </span><span style="text-align: right">
    <asp:CheckBox ID="chkRechComer" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                        Font-Size="8pt"  Text="Rechazo Cotizacion" OnCheckedChanged="chkRechComer_CheckedChanged" 
                                                      Enabled= "false"  TextAlign="Left" Visible="True" Width="120px" />    
    </span></div></td>
  </tr>
  <tr>
  <td></td>
    <td><div align="right">
      <asp:CheckBox ID="chkPlastico" runat="server" Font-Names="Arial" Font-Size="8pt"
                   OnCheckedChanged="chkPlastico_CheckedChanged1"  AutoPostBack= "True" Text="PLASTICO *" TextAlign="Left" Width="120px" />      
    </div></td>
     <td>
    
   </td>
    <td><div align="right"><span style="text-align: right">
    </span><span style="text-align: right">
      
    </span></div></td>
    <td><div align="right"><span style="text-align: right">
    <asp:Label ID="Label26" runat="server" Font-Names="Arial" Font-Size="8pt" Text="."
                                                        Width="10px"></asp:Label>
    <asp:CheckBox ID="chkCotRapida" runat="server" Font-Names="Arial" AutoPostBack="True"
                                                        Font-Size="8pt" Text="C" TextAlign="Left" Width="40px" Visible="false"
                                                        OnCheckedChanged="chkCotRapida_CheckedChanged" /></span></div></td>
    <td><div align="right"><span style="text-align: right">
    </span><span style="text-align: right">
    <asp:CheckBox ID="chkVB2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Visto Bueno"
                                                        TextAlign="Left" Width="80px" />    
    </span></div></td>
  </tr>
  </table>
  <table>
                               <tr>
                                                <td colspan="10">
                                                    <asp:Panel ID="pnlAlcance" runat="server" Font-Names="Arial" Font-Size="8pt" GroupingText="Alcance de la Oferta *"
                                                        Height="100px" Style="text-align: center" Width="830px" >
                                                        <asp:TextBox ID="txtAlcance" runat="server" Font-Names="Arial" Font-Size="8pt" Height="80px"
                                                            TextMode="MultiLine" Width="800px"></asp:TextBox>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="10">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="10" style="text-align: justify">
                                                    <asp:Panel ID="pnlObservaciones" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                        GroupingText="Descripcion del Proyecto *" Height="70px" Style="text-align: center"
                                                        Width="830px">
                                                        <asp:TextBox ID="txtObserva" runat="server" Font-Names="Arial" Font-Size="8pt" Height="50px"
                                                            TextMode="MultiLine" Width="800px"></asp:TextBox>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: justify">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right; margin-left: 160px">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td colspan="2" style="text-align: center">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                
                                                <td colspan="2" style="text-align: right">
                                                    <asp:Button ID="btnGuardar" runat="server" BackColor="#1C5AB6" BorderColor="#999999"
                                                        CssClass="derecha" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btnGuardar_Click"
                                                        Text="Guardar" ForeColor="White" Width="70px" />
                                                </td>
                                                <td colspan="2" style="text-align: center">
                                                    <asp:Button ID="btnSubirListado" runat="server" Text="Subir Listado" OnClick="btnSubirListado_Click"
                                                        BorderColor="#999999" BackColor="#1C5AB6" Font-Bold="True" Font-Names="Arial"
                                                        Font-Size="8pt" ForeColor="White" Width="100px" Visible="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AccorDetEspecif" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="Label1" runat="server" Text="Detalle de Cotizacion"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Panel ID="pnlTitDetalle" runat="server" BackColor="White" Font-Names="Arial"
                                                        Font-Size="8pt" Height="20px" Style="text-align: justify" Width="615px">
                                                        <table width="610px">
                                                            <tr>
                                                                <td width="210px">
                                                                    &nbsp;<asp:Label ID="lblDeta" runat="server" Text=" " Width="200px"></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="lblForAlum" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                                        Style="text-align: center" Text="FORSAALUM"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="lblForPlast" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                                        Style="text-align: center" Text="FORSAPLAST"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="lblForAcero" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                                        Style="text-align: center" Text="FORSAACERO"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 80px">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td colspan="1">
                                                                <asp:Panel ID="pnlEspecificaciones" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                    GroupingText="Especificaciones" Height="90px" Style="text-align: justify" Width="615px">
                                                                    <table style="width: 222px">
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblMuros" runat="server" Font-Names="Arial" Font-Size="8pt" Height="16px"
                                                                                    Text="Muros" Width="40px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: right">
                                                                                <asp:CheckBox ID="chkEMuroAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Style="text-align: center" Text=" " Width="130px" />
                                                                            </td>
                                                                            <td style="text-align: right">
                                                                                <asp:CheckBox ID="chkEMuroPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Style="text-align: center" Text=" " Width="130px" />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkEMuroAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblLosa" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Losa"
                                                                                    Width="40px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkELosaAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Height="21px" Text=" " Width="120px" />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkELosaPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: right">
                                                                                <asp:CheckBox ID="chkELosaAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Style="text-align: center" Text=" " Width="130px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblUML" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Unión muro losa"
                                                                                    Width="200px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkEUMLAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkEUMLPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkEUMLAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="1">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="1">
                                                                <asp:Panel ID="pnlEspecTec" runat="server" Font-Names="Arial" Font-Size="8pt" GroupingText="Especificaciones Técnicas"
                                                                    Height="170px" Style="text-align: right" Width="615px">
                                                                    <table style="width: 596px; height: 155px; margin-right: 0px;">
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblAlturaLibre" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text="Altura libre" Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td class="style127" style="text-align: justify">
                                                                                <asp:TextBox ID="txtALAlum" runat="server" Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:TextBox>
                                                                            </td>
                                                                            <td style="text-align: justify">
                                                                                <asp:TextBox ID="txtALPlast" runat="server" Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:TextBox>
                                                                            </td>
                                                                            <td style="text-align: justify">
                                                                                <asp:TextBox ID="txtALAcero" runat="server" Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblEspMuro" runat="server" Font-Names="Arial" Font-Size="8pt" Height="22px"
                                                                                    Text="Espesor de muro" Width="200px"></asp:Label>
                                                                            </td>
                                                                            <td class="style94">
                                                                                <asp:TextBox ID="txtEMAlum" runat="server" Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:TextBox>
                                                                            </td>
                                                                            <td class="style127" style="text-align: justify">
                                                                                <asp:TextBox ID="txtEMPlast" runat="server" Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:TextBox>
                                                                            </td>
                                                                            <td style="text-align: justify">
                                                                                <asp:TextBox ID="txtEMAcero" runat="server" Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="22">
                                                                                <asp:Label ID="lblEspLosa" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Espesor de losa"
                                                                                    Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td class="style127" height="22" style="text-align: justify">
                                                                                <asp:TextBox ID="txtELAlum" runat="server" Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:TextBox>
                                                                            </td>
                                                                            <td class="style127" height="22" style="text-align: justify">
                                                                                <asp:TextBox ID="txtELPlast" runat="server" Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:TextBox>
                                                                            </td>
                                                                            <td height="22" style="text-align: justify">
                                                                                <asp:TextBox ID="txtELAcero" runat="server" Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="22" style="text-align: right">
                                                                                <asp:Label ID="lblTipoUnion" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo de unión"
                                                                                    Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td class="style127" height="22" style="text-align: justify">
                                                                                <asp:DropDownList ID="cboTUAlum" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                                    Font-Size="8pt" Width="130px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="style127" height="22" style="text-align: justify">
                                                                                <asp:DropDownList ID="cboTUPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Width="130px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td height="22" style="text-align: justify">
                                                                                <asp:DropDownList ID="cboTUAcero" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                                    Font-Size="8pt" Width="130px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="22" style="text-align: right">
                                                                                <asp:Label ID="lblEnrPtas" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Enrase de puertas"
                                                                                    Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td class="style127" height="22" style="text-align: justify">
                                                                                <asp:TextBox ID="txtEPAlum" runat="server" Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:TextBox>
                                                                            </td>
                                                                            <td class="style127" height="22" style="text-align: justify">
                                                                                <asp:TextBox ID="txtEPPlast" runat="server" Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:TextBox>
                                                                            </td>
                                                                            <td height="22" style="text-align: justify">
                                                                                <asp:TextBox ID="txtEPAcero" runat="server" Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblEnrVent" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Enrase de ventanas"
                                                                                    Width="200px"></asp:Label>
                                                                            </td>
                                                                            <td class="style127" style="text-align: justify">
                                                                                <asp:TextBox ID="txtEVAlum" runat="server" Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:TextBox>
                                                                            </td>
                                                                            <td class="style127" style="text-align: justify">
                                                                                <asp:TextBox ID="txtEVPlast" runat="server" Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:TextBox>
                                                                            </td>
                                                                            <td style="text-align: justify">
                                                                                <asp:TextBox ID="txtEVAcero" runat="server" Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="pnlPlanos" runat="server" Font-Names="Arial" Font-Size="8pt" GroupingText="Planos"
                                                                    Height="135px" Style="text-align: justify" Width="615px">
                                                                    <table style="width: 222px">
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblPlanoPlanta" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text="Plano planta" Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: right">
                                                                                <asp:CheckBox ID="chkPlantaAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Style="text-align: center" Text=" " Width="130px" />
                                                                            </td>
                                                                            <td style="text-align: right">
                                                                                <asp:CheckBox ID="chkPlantaPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Style="text-align: center" Text=" " Width="130px" />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkPlantaAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " Width="130px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblPlanFachada" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text="Plano de cortes y fachada " Width="200px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkCorteFachadaAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkCorteFachadaPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkCorteFachadaAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " Width="130px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblPlanAzot" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Plano de azotea"
                                                                                    Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkAzotAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkAzotPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkAzotAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblPlanoUrba" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Plano urbanistico"
                                                                                    Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkUrbaAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkUrbaPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkUrbaAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblPlanEstruct" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text="Plano estructural" Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkEstructuralAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkEstructuralPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkEstructuralAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="pnlPuntoFijo" runat="server" Font-Names="Arial" Font-Size="8pt" GroupingText="Punto Fijo"
                                                                    Height="135px" Width="615px">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblLosaPF" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Losa de punto fijo"
                                                                                    Width="140px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chklosaAlum" runat="server" Text=" " Width="130px" />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chklosaPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " Width="130px" />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chklosaAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " Width="130px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblMurosPF" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Muros de punto fijo"
                                                                                    Width="140px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkMuroAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkMuroPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkMuroAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                                    Text="Losa para cubrir escalera en azotea" Width="200px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkLosaEscaleraAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkLosaEscaleraPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkLosaEscaleraAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblFosoAsorsce" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text="Foso de ascensor" Width="140px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkFosoAscensorAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkFosoAscensorPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkFosoAscensorAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblFosoEscalera" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text="Foso de escalera" Width="140px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkFosoEscaleraAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkFosoEscaleraPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkFosoEscaleraAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="pnlDatosConstructivos" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                    GroupingText="Datos Constructivos" Height="175px" Width="615px">
                                                                    <table style="width: 222px; height: 150px;">
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblForma" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Forma"
                                                                                    Width="80px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: justify" class="style127">
                                                                                <asp:DropDownList ID="cboFormaAlum" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                                    Font-Size="8pt" Width="130px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="text-align: justify" class="style127">
                                                                                <asp:DropDownList ID="cboFormaPlast" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                                    Font-Size="8pt" Width="130px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="text-align: justify" class="style127">
                                                                                <asp:DropDownList ID="cboFormaAcero" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                                    Font-Size="8pt" Width="130px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblPresJuntaDila" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text="Presenta junta de dilatación entre muros" Width="200px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center" class="style127">
                                                                                <asp:CheckBox ID="chkJuntaDilataAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center" class="style127">
                                                                                <asp:CheckBox ID="chkJuntaDilataPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center" class="style127">
                                                                                <asp:CheckBox ID="chkJuntaDilataAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblEspesorJuntas" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text="Espesor entre juntas(Indicar en cm)" Width="200px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: justify" class="style127">
                                                                                <asp:TextBox ID="txtEspJunAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Width="122px"></asp:TextBox>
                                                                            </td>
                                                                            <td style="text-align: justify" class="style127">
                                                                                <asp:TextBox ID="txtEspJunPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Width="122px"></asp:TextBox>
                                                                            </td>
                                                                            <td style="text-align: justify">
                                                                                <asp:TextBox ID="txtEspJunAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Width="122px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblAlturaDesAsc" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text="Presenta desniveles ascendente" Width="200px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkDesnAscAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkDesnAscPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkDesnAscAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblAlturaDesDesc" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text="Presenta desniveles descendentes" Width="200px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkDesnDescAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkDesnDescPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkDesnDescAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="pnlDetalles" runat="server" Font-Names="Arial" Font-Size="8pt" GroupingText="Detalles"
                                                                    Height="660px" Width="615px">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblCulPerim" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Culatas perimetrales"
                                                                                    Width="180px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkCulatsPerimAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkCulatsPeriPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkCulatsPerimAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblCulInt" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Culatas Internas"
                                                                                    Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkCulatasInternasAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkCulatasInternasPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " Width="130px" />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkCulatasInternasAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " Width="130px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblAntep0" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Antepechos"
                                                                                    Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: right">
                                                                                <asp:CheckBox ID="chkAntepechosAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Style="text-align: center" Text=" " Width="130px" />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkAntepechosPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkAntepechosAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style81">
                                                                                <asp:Label ID="lblColum" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Columnas"
                                                                                    Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkColumnasAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkColumnasPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkColumnasAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style81">
                                                                                <asp:Label ID="lblEscVacMon" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Escalera para vaciado monolitica"
                                                                                    Width="180px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkEscMonAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkEscMonPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkEscMonAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style98">
                                                                                <asp:Label ID="lblEscVacPost" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Escalera para vaciado posterior"
                                                                                    Width="160px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkEscPostAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkEscPostPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkEscPostAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblBaseTinaco" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Base para tinaco"
                                                                                    Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkBaseAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkBasePlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkBaseAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblLosaInc" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Losa inclinada"
                                                                                    Width="100px" Style="text-align: right"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkLosaInclinadaAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkLosaInclinadaPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkLosaInclinadaAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblDomo" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Domo"
                                                                                    Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkDomoAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkDomoPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkDomoAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblMurPat" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Muros de patio"
                                                                                    Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkMPAlum" runat="server" Font-Names="Arial" Font-Size="8pt" Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkMPPlast" runat="server" Font-Names="Arial" Font-Size="8pt" Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkMPAcero" runat="server" Font-Names="Arial" Font-Size="8pt" Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style26">
                                                                                <asp:Label ID="lblNegAce" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Negativos en acero"
                                                                                    Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkNegAceroAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkNegAceroPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkNegAceroAce" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblPretiles" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Pretiles"
                                                                                    Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkPretilesAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkPretilesPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkPretilesAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblGargolas" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Gargolas (Mexico)"
                                                                                    Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkGargolasAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkGargolasPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkGargolasAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style26">
                                                                                <asp:Label ID="lblMurFormTex" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Muros con formaleta texturada"
                                                                                    Width="180px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkMFTAlum" runat="server" Font-Names="Arial" Font-Size="8pt" Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkMFTPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkMFTAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblNegCarriolas" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text="Negativos para carriolas" Width="160px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkNegCarriolasAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkNegCarriolasPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkNegCarriolasAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblVentEsp" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Ventanas especiales"
                                                                                    Width="160px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkVEAlum" runat="server" Font-Names="Arial" Font-Size="8pt" Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkVEPlast" runat="server" Font-Names="Arial" Font-Size="8pt" Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkVEAcero" runat="server" Font-Names="Arial" Font-Size="8pt" Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style26">
                                                                                <asp:Label ID="lblFormCtoMaq" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Formaleta para cuarto de maquinas"
                                                                                    Width="180px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkFCMqAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkFCMqPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkFCMqAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblVigDesc" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Vigas descolgadas"
                                                                                    Width="120px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkVigasAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkVigasPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkVigasAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblTorreon" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Torreon (Mexico)"
                                                                                    Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkTorreonAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkTorreonPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkTorreonAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblRebordes" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Rebordes en fachada"
                                                                                    Width="140px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkRebordesAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkRebordesPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkRebordesAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblReservatorios" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text="Reservatorios " Width="140px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkReservatoriosAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkReservatoriosPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkReservatoriosAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblDilatacion" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Dilatación en fachada"
                                                                                    Width="140px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkDilFacAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkDilFacPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkDilFacAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblJuntContInt" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text="Junta de control interna en antepechos" Width="200px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkJCAIAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkJCAIPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkJCAIAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblJuntContExt" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text="Junta de control externa en antepechos" Width="200px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkJCAEAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkJCAEPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkJCAEcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblCanes0" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Canes"
                                                                                    Width="80px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkCanesAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkCanesPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkCanesAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblPorticos" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Porticos"
                                                                                    Width="80px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkPortAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkPortPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Style="text-align: center" Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkPortAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Label ID="lblOtros" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Otros"
                                                                                    Width="80px"></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkOtrosAlum" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkOtrosPlast" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox ID="chkOtrosAcero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                            <table width="610px">
                                                            <tr>
                                                            <td></td>
                                                                      <td></td>                                                
                                                            <td style="text-align: right">
                                                            <asp:Label ID="lblModulaciones" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cant Modulaciones"
                                                                                    Width="120px"></asp:Label>
                                                                                    &nbsp;  
                                                             <asp:TextBox style="text-align: center" Text="0" ID="txtModulaciones" runat="server" Font-Names="Arial" Font-Size="8pt" Width="30px"></asp:TextBox> 
                                                            
                                                               &nbsp;
                                                                        <asp:Button ID="btnVB" runat="server" BackColor="#1C5AB6" BorderColor="#999999" Font-Bold="True"
                                                                            Font-Names="Arial" Font-Size="8pt" ForeColor="White" OnClick="btnVB_Click" Text="Visto Bueno"
                                                                            Width="90px" />
                                                                    </td>
                                                                     <td></td>
                                                             <td>
                                                                 </td>
                                                            </tr>
                                                                <tr>
                                                                    <td class="style119">
                                                                        &nbsp;
                                                                        <asp:Button ID="btnRechazo" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Text="Rechazo"
                                                                            Width="70px" OnClick="btnRechazo_Click" />
                                                                        &nbsp;
                                                                    </td>
                                                                    
                                                                    <td style="text-align: left">
                                                                        <asp:Button ID="btnDetalle" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btnDetalle_Click"
                                                                            Text="Enviar" Width="70px" />
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <asp:Button ID="btnSubir" runat="server" BackColor="#1C5AB6" ForeColor="White" BorderColor="#099999"
                                                                            Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="text-align: center"
                                                                            Text="Subir Planos/Listado/Carta" Width="165px" OnClick="btnSubir_Click" ToolTip="Tamaño Máximo Del Archivo 10 MB." />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AccorSubirArch" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="LblSubPlanListDoc" runat="server" Text="Subir Planos -Listado - Documentacion"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table class="style157">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:LinkButton ID="lkSubirPlanosDoc" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                        OnClick="lkSubirPlanosDoc_Click" Width="220px" ForeColor="White" BackColor="#1C5AB6"
                                                        BorderColor="White" Font-Bold="True" Font-Italic="False" Height="20px" Style="text-align: center">Subir Planos/Listado/Documentos</asp:LinkButton>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="60px">
                                                    &nbsp;
                                                </td>
                                                <td colspan="2" style="text-align: center">
                                                    <asp:GridView ID="grvArchivo" runat="server" CellPadding="1" CellSpacing="4" ForeColor="#333333"
                                                        GridLines="None" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt"
                                                        DataKeyNames="id_plano" OnRowCommand="grvArchivo_RowCommand">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-Width="15px" ControlStyle-Width="15px" Visible="True">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnDeleteFile" runat="server" ImageUrl="~/Imagenes/editdelete.gif"
                                                                        Width="15px" OnClientClick="return confirm('Esta seguro de eliminar el archivo?')"
                                                                        CommandArgument="<%#((GridViewRow) Container).RowIndex %>" CommandName="Borrar" />
                                                                </ItemTemplate>
                                                                <ControlStyle Width="15px" />
                                                                <HeaderStyle Width="15px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="idPlano" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPlano" runat="server" Text='<%# Bind("ID_PLANO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtPlano" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Version">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVer" runat="server" Text='<%# Bind("VERS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtVer" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Consecutivo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("CONS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tipo Anexo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("TIPO_DOC") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="OF Referencia">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOF" runat="server" Text='<%# Bind("OF_REFERENCIA") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ruta Archivo">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="simpa_anexoEditLink" runat="server" Text='<%# Eval("RUTA") %>'
                                                                         />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Fecha">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblfecha" runat="server" Text='<%# Bind("Fecha") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" />
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" />
                                                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AccorSalidaCot" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="Label2" runat="server" Text="Salida de Cotizacion"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblSalidadCotizacion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                        Text="Dio salida de cotizacion" Width="120px" Visible="false"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="LSalidadCotizacion" runat="server" Font-Bold="False" Font-Italic="True"
                                                        Font-Names="Arial" Font-Size="8pt" ForeColor="#0000CC" Width="200px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td style="border-color: #1C5AB6; text-align: center; background-color: #1C5AB6;">
                                                    <asp:Label ID="lblm2" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="White"
                                                        Font-Size="8pt" Text="M2" Width="20px"></asp:Label>
                                                </td>
                                                <td class="style111" style="border-color: #1C5AB6; text-align: center; background-color: #1C5AB6;">
                                                    <asp:Label ID="lblvalor" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="White"
                                                        Font-Size="8pt" Style="text-align: right" Text="Valor" Width="80px"></asp:Label>
                                                </td>
                                                <td>
                                                <asp:Label ID="lblMonedaFup" runat="server" ForeColor="#1C5AB6" Font-Bold = "True" Font-Names="Arial" Font-Size="8pt" Text="Moneda"
                                                      style="text-align: left"  Width="120px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblAlum" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Aluminio"
                                                        Width="60px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtm2Alum" runat="server" Font-Names="Arial" Font-Size="8pt" Width="120px" style="text-align: right" 
                                                        AutoPostBack="true" OnTextChanged="txtm2Alum_TextChanged">0</asp:TextBox>
                                                </td>
                                                <td style="text-align: right" >
                                                    <asp:TextBox ID="txtValAlum" runat="server" Font-Names="Arial" Font-Size="8pt" Width="155px" style="text-align: right" 
                                                        AutoPostBack="True" OnTextChanged="txtValAlum_TextChanged">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblAlumAcc" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Accesorios"
                                                        Width="60px"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtValAccAlum" runat="server" Font-Names="Arial" Font-Size="8pt" style="text-align: right" 
                                                        Width="155px" AutoPostBack="True" OnTextChanged="txtValAccAlum_TextChanged">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style121">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td class="style110">
                                                    &nbsp;
                                                </td>
                                                <td class="style111">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblPlast" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Plastico"
                                                        Width="60px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtm2Plast" runat="server" Font-Names="Arial" Font-Size="8pt" Width="120px" style="text-align: right" 
                                                        AutoPostBack="True" OnTextChanged="txtm2Plast_TextChanged">0</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtValPlast" runat="server" Font-Names="Arial" Font-Size="8pt" Width="155px" style="text-align: right" 
                                                        AutoPostBack="True" OnTextChanged="txtValPlast_TextChanged">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblPlastAcc" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Accesorios"
                                                        Width="60px"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtValAccPlast" runat="server" Font-Names="Arial" Font-Size="8pt" style="text-align: right" 
                                                        Width="155px" AutoPostBack="True" OnTextChanged="txtValAccPlast_TextChanged">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblAcero" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Acero"
                                                        Width="60px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtm2Acero" runat="server" Font-Names="Arial" Font-Size="8pt" Width="120px" style="text-align: right" 
                                                        AutoPostBack="True" OnTextChanged="txtm2Acero_TextChanged">0</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtValAcero" runat="server" Font-Names="Arial" Font-Size="8pt" Width="155px" style="text-align: right" 
                                                        AutoPostBack="True" OnTextChanged="txtValAcero_TextChanged">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblAceroAcc" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Accesorios"
                                                        Width="60px"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtValAccAcero" runat="server" Font-Names="Arial" Font-Size="8pt" style="text-align: right" 
                                                        Width="155px" AutoPostBack="True" OnTextChanged="txtValAccAcero_TextChanged">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Arial"
                                                        Font-Size="8pt" Text="TOTAL" Width="60px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTotalm2" runat="server" Font-Names="Arial" Font-Size="8pt" Width="120px" style="text-align: right" 
                                                        Enabled="False">0</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTotalValor" runat="server" Font-Names="Arial" Font-Size="8pt" style="text-align: right" 
                                                        Width="155px" Enabled="False">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                          <tr>
                                            <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                            <td style="text-align: right">
                                                    <asp:Label ID="Label31" runat="server"  Font-Names="Arial"
                                                        Font-Size="8pt" Text="Descuento" Width="80px"></asp:Label>
                                                </td>
                                                <td>
                                                  <asp:Label ID="lblPorcDscto" runat="server"   Font-Names="Arial"
                                                        Font-Size="8pt" Text="Cant SF" Width="100px"></asp:Label>
                                                      
                                                </td>
                                           
                                            <td>
                                            <asp:Label ID="lblValorDscto" runat="server"   style="text-align: right"  Font-Names="Arial"
                                                        Font-Size="8pt" Text="Valor SF" Width="155px"></asp:Label>
                                                    </td>
                                                     <td>
                                                <asp:Label ID="lblRazonDscto" runat="server" style="text-align: center"  ForeColor="#1C5AB6" Font-Names="Arial" Font-Size="8pt" Text=""
                                                        Width="220px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                            <td style="text-align: right">
                                                    <asp:Label ID="Label29" runat="server"  Font-Names="Arial" Font-Bold="true"
                                                        Font-Size="8pt" Text="Total Sol Facturacion" Width="120px"></asp:Label>
                                                </td>
                                                <td>
                                                  <asp:Label ID="lblCantSf" runat="server" Font-Bold="false" Font-Italic="False" Font-Names="Arial"
                                                        Font-Size="8pt" Text="Cant SF" Width="50px"></asp:Label>
                                                       
                                                </td>
                                           
                                            <td>
                                            <asp:Label ID="lblValorSf" runat="server"  style="text-align: right" Font-Names="Arial"  
                                                        Font-Size="10pt" Text="Valor SF" Width="155px"></asp:Label>
                                                    </td>
                                                     <td>
                                                <asp:Label ID="lblUsuarioSf" runat="server" style="text-align: center"  ForeColor="#1C5AB6" Font-Names="Arial" Font-Size="8pt" Text=""
                                                        Width="220px"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            </table>
                                            <table>
                                             <tr>
                                             <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                              </tr>
                                            <tr>
                                                <td style="text-align: right" class="style121">
                                                  
                                                </td>
                                                <td style="text-align: right">
                                                    
                                                </td>
                                                <td class="style121" style="text-align: right">
                                                    <asp:Label ID="lblCont20" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Contenedores 20 Ton"
                                                        Width="140px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCont20" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                        Width="40px" class="style121" style="text-align: center" AutoPostBack="True" OnTextChanged="txtCont20_TextChanged">0</asp:TextBox>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    
                                                </td>
                                                <td class="style121" style="text-align: right">
                                                    <asp:Label ID="lblCont40" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Contenedores 40 Ton"
                                                        Width="140px" ></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCont40" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                        Width="40px" class="style121" style="text-align: center"  AutoPostBack="True" OnTextChanged="txtCont40_TextChanged">0</asp:TextBox>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    
                                                </td>
                                                <td class="style121" style="text-align: right">
                                                    <asp:Label ID="Label20" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Pais Obra"
                                                        Width="140px" ></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPaisObra" runat="server" Font-Names="Arial" Font-Size="8pt" Text="."
                                                        Width="180px"></asp:Label>
                                                </td>
                                               

                                            </tr>
                                            <tr>
                                                <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    
                                                </td>
                                                </table>
                                                <table>
                                                 <tr>

                                                <td class="style121" style="text-align: left">
                                                    <asp:Label ID="Label22" runat="server" Font-Names="Arial" Font-Size="8pt" Text="TemaRechazo"
                                                      Visible = "false"  Width="140px" ></asp:Label>
                                                        <asp:DropDownList ID="cboTemarespRechazo" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                Visible = "false"     OnSelectedIndexChanged = "cboTemarespRechazo_SelectedIndexChanged"                Font-Size="8pt" Width="130px">
                                                                                </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRechazo" runat="server" Font-Names="Arial" Font-Size="8pt" TextMode = "MultiLine"
                                                    Visible = "false"  Enabled ="false"   Width="300px" class="style121" style="text-align: left"  ></asp:TextBox>
                                                
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="style121" style="text-align: right">
                                                    <asp:Label ID="Label23" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Respuesta Rechazo"
                                                        Width="140px" ></asp:Label>                                                        
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRespRechazo" runat="server" Font-Names="Arial" Font-Size="8pt" TextMode = "MultiLine"
                                                     Visible = "false"    Width="300px" class="style121" style="text-align: left"  ></asp:TextBox>
                                                
                                                </td>
                                                <td>
                                                </td>
                                                 <td style="text-align: right">
                                                    <asp:Button ID="btnGuardarSalida" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Text="Guardar"
                                                        Width="70px" OnClick="btnGuardarSalida_Click" />
                                                </td>
                                                <td>
                                                 <asp:Button ID="btnSubirCarta" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                        BorderColor="#099999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="text-align: center"
                                                     visible = "False"   Text="Subir Carta" Width="80px" OnClick="btnSubirList_Click" />
                                               
                                                </td>

                                                <td style="text-align: right">
                                                   <asp:Button ID="btnEnviarCotiz" runat="server" BackColor="#1C5AB6" ForeColor="White" Visible = "false"
                                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Text="Enviar Cotizacion"
                                                        Width="110px" OnClick = "btnEnviarCotiz_Click"  />
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td class="style121" style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                   <asp:Label ID="lblRutaCarta" runat="server" Font-Names="Arial" Font-Size="8pt" Text="" Visible="false"
                                                        Width="30px" ></asp:Label>  
                                                     <asp:Label ID="LblRutaArchivo" runat="server" Width="30px" Visible="false" ></asp:Label> 
                                                </td>
                                                
                                                <td style="text-align: right">
                                                 
                                                    </td>
                                                    <td>
                                                </td>
                                                

                                            </tr>
                                            <tr>
                                                <td style="text-align: right" class="style121">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                       <table>
                                            <tr>
                                            <td>

                                                <asp:GridView ID="gridCartas" runat="server" CellPadding="1" CellSpacing="4" ForeColor="#333333"
                                                        GridLines="None" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt"
                                                        DataKeyNames="id_plano" OnRowCommand="gridCartas_RowCommand">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-Width="15px" ControlStyle-Width="15px" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnDeleteFile" runat="server" ImageUrl="~/Imagenes/editdelete.gif"
                                                                        Width="15px" OnClientClick="return confirm('Esta seguro de eliminar el archivo?')"
                                                                        CommandArgument="<%#((GridViewRow) Container).RowIndex %>" CommandName="Borrar" />
                                                                </ItemTemplate>
                                                                <ControlStyle Width="15px" />
                                                                <HeaderStyle Width="15px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="idPlano" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPlano" runat="server" Text='<%# Bind("ID_PLANO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtPlano" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Version">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVer" runat="server" Text='<%# Bind("VERS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtVer" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Consecutivo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("CONS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tipo Anexo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("TIPO_DOC") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="OF Referencia">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOF" runat="server" Text='<%# Bind("OF_REFERENCIA") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>                                                           
                                                             <asp:TemplateField HeaderText="Fecha">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblfecha" runat="server" Text='<%# Bind("Fecha") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" />
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" />
                                                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                    </asp:GridView>
                                                </td>
                                                <td>
                                        <asp:GridView ID="GridDetalleCarta" runat="server" AutoGenerateColumns="False"
                                            OnRowCommand="GridDetalleCarta_RowCommand" BackColor="White"
                                            CellPadding="1" CellSpacing="4" ForeColor="#333333"
                                            GridLines="None" Font-Names="Arial" Font-Size="8pt">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nombre_Archivo" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server"
                                                            CausesValidation="False"
                                                            CommandArgument='<%# Eval("NOMBRE_ARCHIVO") %>' PostBackUrl="~/FUP.aspx"
                                                            CommandName="Download" Text='<%# Eval("NOMBRE_ARCHIVO") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" />
                                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                            </td>
                                            </tr>
                                        </table>                                     
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AccFletes" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="LFletes" runat="server" Text="Fletes"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table class="style157">
                                            <tr>
                                                <td class="style158">
                                                    &nbsp;
                                                </td>
                                                <tr>
                                                    <td class="style158">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style163" colspan="2">
                                                        <asp:Panel ID="pnlExportacion" runat="server"  Font-Names="Arial"
                                                            Font-Size="8pt" GroupingText="Exportacion" Visible="false" Width="450px">
                                                            <table>
                                                                <tr>
                                                                    <td class="style165">
                                                                        <asp:Label ID="LTransp" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Transportador"
                                                                            Width="100px"></asp:Label>
                                                                    </td>
                                                                    <td class="style161">
                                                                        <asp:Label ID="lblTransp" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LAgente" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                            Width="150px">Agente De Carga Internacional</asp:Label>
                                                                    </td>
                                                                    <td class="style161">
                                                                        <asp:Label ID="lblAgentCarga" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: left"
                                                                            Width="200px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LTipoNeg0" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                            Text="Termino De Negociación" Width="150px"></asp:Label>
                                                                    </td>
                                                                    <td class="style161">
                                                                        <asp:DropDownList ID="cboTDNFlete" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                            Font-Size="8pt" Width="100px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LPuertoCargue" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                            Width="150px">Puerto De Cargue</asp:Label>
                                                                    </td>
                                                                    <td class="style161">
                                                                        <asp:DropDownList ID="cboPtoCargue" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                            Font-Size="8pt" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LPuertoDescargue" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                            Style="text-align: right" Width="150px">Puerto De Descargue</asp:Label>
                                                                    </td>
                                                                    <td class="style161">
                                                                        <asp:DropDownList ID="cboPtoDescargue" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                            Font-Size="8pt" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                </tr>
                                            </tr>
                                        </table>
                                        </asp:Panel> </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        </tr>
                                        <tr>
                                            <td class="style158">
                                                &nbsp;
                                            </td>
                                            <td class="style163" colspan="2">
                                                &nbsp;<asp:Panel ID="PnlCotizacionFlete" runat="server"   Font-Names="Arial"
                                                    Font-Size="8pt" GroupingText="Cotización Flete" Width="400px">
                                                    <table class="style164">
                                                        <tr>
                                                            <td class="style163">
                                                                <asp:Label ID="LVrEXW" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                    Width="150px">Valor Cotizado</asp:Label>
                                                            </td>
                                                            <td class="style161">
                                                                <asp:Label ID="lblVrEXW" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: left"
                                                                    Width="150px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LVehic" runat="server" Font-Names="Arial" Font-Size="9pt" Style="text-align: center"
                                                                    Width="150px" ForeColor="Navy" Font-Overline="true" Font-Underline="true">VEHICULOS</asp:Label>
                                                            </td>
                                                            <td class="style163">
                                                                <asp:Label ID="LCiuObr" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                    Width="150px" Visible="false">Ciudad De Obra</asp:Label>
                                                            </td>
                                                            <td class="style161">
                                                                <asp:Label ID="LblCiuObraFlete" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                    Style="text-align: left" Width="150px" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style163">
                                                                <asp:Label ID="LTipoTurbo" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                    Width="150px">Turbo</asp:Label>
                                                            </td>
                                                            <td class="style161">
                                                                <asp:TextBox ID="txtCant3" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: center"
                                                                    Width="20px" Enabled="False">0</asp:TextBox>
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="lblVrTipo3" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: left"
                                                                    Width="80"></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style163">
                                                                <asp:Label ID="LTipo1" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                    Width="150px"></asp:Label>
                                                            </td>
                                                            <td class="style161">
                                                                <asp:TextBox ID="txtCant1" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: center"
                                                                    Width="20px">0</asp:TextBox>&nbsp;&nbsp;
                                                                <asp:Label ID="lblVrTipo1" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: left"
                                                                    Width="80"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style163">
                                                                <asp:Label ID="LMinimula" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                    Width="150px">Minimula</asp:Label>
                                                            </td>
                                                            <td class="style161">
                                                                <asp:TextBox ID="txtCant4" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: center"
                                                                    Width="20px">0</asp:TextBox>
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="lblVrTipo4" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: left"
                                                                    Width="80"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style163">
                                                                <asp:Label ID="LTipo2" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                    Width="150px"></asp:Label>
                                                            </td>
                                                            <td class="style161">
                                                                <asp:TextBox ID="txtCant2" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: center"
                                                                    Width="20px">0</asp:TextBox>
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="lblVrTipo2" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: left"
                                                                    Width="80"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style163">
                                                                <asp:Label ID="LTFPD" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                    Width="180px" Visible="false">Lead Time</asp:Label>
                                                            </td>
                                                            <td class="style161">
                                                                <asp:Label ID="lblLTF" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: left"
                                                                    Width="150px" Height="16px" Visible="false">Dias</asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style163">
                                                                <asp:Label ID="lblVrFlete" runat="server" Font-Names="Arial" Font-Size="9pt" Style="text-align: right"
                                                                    Width="150px" ForeColor="Navy" Font-Overline="true" Font-Underline="true">Valor Flete</asp:Label>
                                                            </td>
                                                            <td class="style161">
                                                                <asp:Label ID="LVrFlete" runat="server" Font-Names="Arial" Font-Size="9pt" Style="text-align: left"
                                                                    Width="80" Font-Bold="true">0</asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style163">
                                                                <asp:Label ID="lblVrTotalFlete" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                                    Style="text-align: right" Width="150px" ForeColor="Navy" Font-Overline="true"
                                                                    Font-Underline="true">Valor Total</asp:Label>
                                                            </td>
                                                            <td class="style161">
                                                                <asp:Label ID="LVrTotalFlete" runat="server" Font-Names="Arial" Font-Size="9pt" Style="text-align: left"
                                                                    Width="80" Font-Bold="true">0</asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style163">
                                                                &nbsp;
                                                            </td>
                                                            <td style="text-align: center">
                                                                <asp:Button ID="btnCalcularFlete" runat="server" Text="Calcular" BackColor="#1C5AB6"
                                                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                                                                    OnClick="btnCalcularFlete_Click" />
                                                                &nbsp;&nbsp;
                                                                <asp:Button ID="btnGuardarFlete" runat="server" Text="Guardar" BackColor="#1C5AB6"
                                                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                                                                    OnClick="btnGuardarFlete_Click" />
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style163">
                                                                <asp:Label ID="LTransInterno" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                    Width="150px" Visible="false">Transporte Interno</asp:Label>
                                                            </td>
                                                            <td class="style161">
                                                                <asp:Label ID="VrTransInterno" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                    Width="80px" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style163">
                                                                <asp:Label ID="LGastPtoOrig" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                    Width="150px" Visible="false">Gastos En Puerto Origen</asp:Label>
                                                            </td>
                                                            <td class="style161">
                                                                <asp:Label ID="VrGastPtoOrig" runat="server" Font-Names="Arial" Font-Size="8pt" Width="80px"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style163">
                                                                <asp:Label ID="LDespAduanal" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                    Width="150px" Visible="false">Despacho Aduanal</asp:Label>
                                                            </td>
                                                            <td class="style161">
                                                                <asp:Label ID="VrDespAduana" runat="server" Font-Names="Arial" Font-Size="8pt" Width="150px"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style163">
                                                                <asp:Label ID="LSeguro" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                    Width="150px" Visible="false">Seguro</asp:Label>
                                                            </td>
                                                            <td class="style161">
                                                                <asp:Label ID="VrSeguro" runat="server" Font-Names="Arial" Font-Size="8pt" Width="150px"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style163">
                                                                <asp:Label ID="LTotalFleteIntern" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                    Style="text-align: right" Width="170px" Visible="false">Total Flete Internacional</asp:Label>
                                                            </td>
                                                            <td class="style161">
                                                                <asp:Label ID="VrFleteInt" runat="server" Font-Names="Arial" Font-Size="8pt" Width="80px"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                            <caption>
                                                &nbsp;
                                            </td>
                                            </caption>
                                        </tr>
                                        <tr>
                                            <td class="style163">
                                                <asp:Label ID="LGasPtoDest" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                    Width="170px" Visible="false">Gastos En Puerto Destino</asp:Label>
                                            </td>
                                            <td class="style161">
                                                <asp:Label ID="VrGastosPtoDest" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Style="text-align: right" Width="80px" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style163">
                                                <asp:Label ID="LDADest" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                    Width="170px" Visible="false">Despacho Aduanal Destino</asp:Label>
                                            </td>
                                            <td class="style161">
                                                <asp:Label ID="VrDespAduanalDest" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Style="text-align: right" Width="80px" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style163">
                                                <asp:Label ID="LTranspADest" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                    Width="170px" Visible="false">Transporte Aduanal Destino</asp:Label>
                                            </td>
                                            <td class="style161">
                                                <asp:Label ID="vrTranspAduaDest" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Style="text-align: right" Width="80px" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style163">
                                                <asp:Label ID="LTipo3" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                    Width="150px" Visible="false"></asp:Label>
                                            </td>
                                            <td class="style161">
                                                <asp:Label ID="vrTipo3" runat="server" Font-Names="Arial" Font-Size="8pt" Width="80px"
                                                    Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style163">
                                                <asp:Label ID="LTipo4" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                    Width="150px" Visible="false"></asp:Label>
                                            </td>
                                            <td class="style161">
                                                <asp:Label ID="VrTipo4" runat="server" Font-Names="Arial" Font-Size="8pt" Width="80px"
                                                    Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        </td> </tr> </table> </asp:Panel> </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        </tr>
                                        <tr>
                                            <td class="style158">
                                                &nbsp;
                                            </td>
                                            <td class="style163" colspan="2">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        </table>
                                    </Content>
                                </asp:AccordionPane>
                               <asp:AccordionPane ID="AccorRechazo" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="lblRechazo" runat="server" Text="Rechazo De Entrada"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="pnlRechazo" runat="server"  Font-Names="Arial" Font-Size="8pt">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnlTemas" runat="server" Font-Names="Arial" Font-Size="8pt" GroupingText="Temas"
                                                                        Height="50px">
                                                                        <asp:DropDownList ID="cboTemaRechazo" runat="server"  Font-Names="Arial"
                                                                            Font-Size="8pt" Width="200px">
                                                                        </asp:DropDownList>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td>
                                                                    <asp:Panel ID="pnlObservRechazo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        GroupingText="Observacion" Height="50px" Width="520px">
                                                                        <asp:TextBox ID="txtObservaRechazo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                            Height="30px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td style="text-align: right">
                                                                    <asp:Button ID="btnGuardarRechazo" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btnGuardarRechazo_Click"
                                                                        Text="Guardar" Width="70px" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="btnNewRechazo" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btnNewRechazo_Click"
                                                                        Style="text-align: center" Text="Nuevo" Width="70px" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="btnEnviarCorreoRechazo" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btnEnviarCorreoRechazo_Click"
                                                                        Text="Enviar Correo" Width="100px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td colspan="1" style="text-align: justify">
                                                    <asp:GridView ID="gvRechazo" runat="server" CellPadding="1" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="#333333" GridLines="None" CellSpacing="4" OnPageIndexChanged="gvRechazo_PageIndexChanged"
                                                        AutoGenerateColumns="False" DataKeyNames="rct_id" OnRowCommand="gvRechazo_RowCommand"
                                                        Width="683px">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="idRechazo" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDRechazo" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("rct_id") %>'
                                                                        Width="10px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnValidar" runat="server" CommandArgument="<%#((GridViewRow) Container).RowIndex %>"
                                                                        CommandName="Validar" Height="15px" ImageUrl="~/Imagenes/editar.gif" Width="15px"
                                                                        ToolTip="Validar" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEliminarRechazo" runat="server" CommandArgument="<%#((GridViewRow) Container).RowIndex %>"
                                                                        CommandName="EliminarRechazo" Height="15px" ImageUrl="~/Imagenes/editdelete.gif"
                                                                        Width="15px" ToolTip="Eliminar" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Validado">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblValidado" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("VALIDADO") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Version">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LVersRechazo" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("VERS") %>'
                                                                        Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fecha">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LFechaRechazo" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("FECHA") %>'
                                                                        Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tema">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LTemaRechazo" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("TEMA") %>'
                                                                        Width="180px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Observacion">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LObservación" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("OBSERVACION") %>'
                                                                        Width="280px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rechazado Por">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lrechazadopor" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("RECHAZADO_POR") %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" />
                                                        <PagerStyle BackColor="#2461BF" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style124">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </asp:AccordionPane>
                                  <asp:AccordionPane ID="AccorRechazoComerc" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="Label21" runat="server" Text="Rechazo De Cotizacion"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="Panel2" runat="server"  Font-Names="Arial" Font-Size="8pt">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Panel3" runat="server" Font-Names="Arial" Font-Size="8pt" GroupingText="Temas"
                                                                        Height="50px">
                                                                        <asp:DropDownList ID="cboTemaRechCom" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                           Enabled = "false" Font-Size="8pt" Width="200px">
                                                                        </asp:DropDownList>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td>
                                                                    <asp:Panel ID="Panel4" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        GroupingText="Observacion" Height="50px" Width="520px">
                                                                        <asp:TextBox ID="txtObservRecCom" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                          Enabled = "false"  Height="45px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td style="text-align: right">
                                                                    <asp:Button ID="btnGuardarRechCom" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                                      OnClick="btnGuardarRechCom_Click"  BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                                       Enabled = "false" Text="Guardar" Width="70px" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                   
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td colspan="1" style="text-align: justify">
                                                    <asp:GridView ID="gridRechCom" runat="server" CellPadding="1" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="#333333" GridLines="None" CellSpacing="4" OnPageIndexChanged="gvRechazo_PageIndexChanged"
                                                        AutoGenerateColumns="False" DataKeyNames="rct_id" OnRowCommand="gvRechazo_RowCommand"
                                                        Width="683px">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="idRechazo" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDRechazo" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("rct_id") %>'
                                                                        Width="10px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnValidar" runat="server" CommandArgument="<%#((GridViewRow) Container).RowIndex %>"
                                                                      Visible = "false"  CommandName="Validar" Height="15px" ImageUrl="~/Imagenes/editar.gif" Width="15px"
                                                                        ToolTip="Validar" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEliminarRechazo" runat="server" CommandArgument="<%#((GridViewRow) Container).RowIndex %>"
                                                                       Visible = "false"  CommandName="EliminarRechazo" Height="15px" ImageUrl="~/Imagenes/editdelete.gif"
                                                                        Width="15px" ToolTip="Eliminar" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblValidado" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("VALIDADO") %>'
                                                                       visible = "false" Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Version">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LVersRechazo" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("VERS") %>'
                                                                        Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fecha">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LFechaRechazo" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("FECHA") %>'
                                                                        Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tema">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LTemaRechazo" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("TEMA") %>'
                                                                        Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Observacion">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LObservación" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("OBSERVACION") %>'
                                                                        Width="220px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rechazado Por">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lrechazadopor" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("RECHAZADO_POR") %>'
                                                                        Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Respuesta">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lrespuesta" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("RESPUESTA") %>'
                                                                        Width="180px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" />
                                                        <PagerStyle BackColor="#2461BF" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style124">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </asp:AccordionPane>
                               
                                
                                <asp:AccordionPane ID="AccorSegPFT" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="LblPlanTipForsa" runat="server" Text="Seguimiento Solicitud Planos Tipo Forsa"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Panel ID="pnlSegPlanosTF" runat="server" Font-Names="Arial" Font-Size="8pt">
                                                        <table class="style157">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnlResponsable" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        GroupingText="Responsable *" Width="185px">
                                                                        <asp:DropDownList ID="cboResponsable" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                            Font-Size="8pt" Width="180px">
                                                                        </asp:DropDownList>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td>
                                                                    <asp:Panel ID="pnlEvento" runat="server" Font-Names="Arial" Font-Size="8pt" GroupingText="Evento *"
                                                                        Width="200px">
                                                                        <asp:DropDownList ID="cboEvento" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                            Font-Size="8pt" Width="190px">
                                                                        </asp:DropDownList>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td style="text-align: right">
                                                                    <asp:Label ID="lblFecSalPlano" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        Style="text-align: right" Text="Fecha De Salida *" Visible="False" Width="100px"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFecSalida" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        Visible="false" Width="80px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtFecSalida_CalendarExtender" runat="server" Enabled="True"
                                                                        Format="yyyy-MM-dd" TargetControlID="txtFecSalida">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkEnvPlano" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        Height="20px" Text="Enviado" TextAlign="Left" Width="70px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="pnlObservPlanoForsa" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        GroupingText="Observaciones" Width="645px">
                                                                        <asp:TextBox ID="txtObservaPlano" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                            Width="630px"></asp:TextBox>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnGuardarPlanoForsa" runat="server" BackColor="#1C5AB6" BorderColor="#999999"
                                                                        Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btnGuardarPlanoForsa_Click"
                                                                        Text="Guardar" ForeColor="White" Width="70px" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnNuevoTipoForsa" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btnNuevoTipoForsa_Click"
                                                                        Text="Nuevo" Width="70px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: center">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gvTipoForsa" runat="server" CellPadding="1" CellSpacing="4" Font-Names="Arial"
                                                        Font-Size="8pt" ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" />
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" />
                                                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                    </asp:GridView>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AccorCierre" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="Label6" runat="server" Text="Venta - Cierre Comercial"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table style="width: 673px">
                                            <tr>
                                                <td style="text-align: right" colspan="6">
                                                    <asp:Panel ID="pnlAlcanceCierre" runat="server" Font-Bold="True" Font-Names="Arial"
                                                        Font-Size="8pt" GroupingText="Alcance De La Oferta" Height="94px" Width="100%"
                                                        Style="text-align: justify">
                                                        <asp:TextBox ID="txtAlcanceCierre" runat="server" Height="74px" Style="margin-left: 0px"
                                                            TextMode="MultiLine" Width="635px"></asp:TextBox>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" colspan="2">
                                                    <asp:Button ID="btnSubirImagen" runat="server" Text="Subir Documento" ForeColor="White"
                                                        OnClick="btnSubirImagen_Click" Font-Names="Arial" Font-Size="8pt" BackColor="#1C5AB6"
                                                        Width="120px" Font-Bold="True" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="text-align: right">
                                                     <asp:Label ID="lblCierre" runat="server" Font-Names="Arial" Font-Size="9" Text="" Font-Bold = "True"
                                                                    Font-Italic="True"   ForeColor="#0000CC"     Width="490px" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="text-align: justify">
                                                    <asp:Panel ID="pnlDetCierre" runat="server" width = "700px">
                                                        <table  width = "700px">
                                                            <tr>
                                                                <td style="text-align: right">
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td style="text-align: right">
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td colspan="2" style="text-align: right">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="text-align: right">
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                
                                                                <td style="text-align: right">
                                                                    <asp:Label ID="lblFContrato" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Firma De Contrato *"
                                                                        Width="100px"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFContrato" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        Width="60px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtFContrato_CalendarExtender" runat="server" Enabled="True"
                                                                        Format="yyyy-MM-dd" TargetControlID="txtFContrato">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                                <td style="text-align: right">
                                                                    <asp:Label ID="lblFContractual" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        Text="Fecha Contractual *" Width="100px"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFContractual" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        Width="60px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtFContractual_CalendarExtender" runat="server" Enabled="True"
                                                                        Format="yyyy-MM-dd" TargetControlID="txtFContractual">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                                <td style="text-align: right">
                                                                    <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="M2 Cerrados"
                                                                        Width="80px"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LM2Cerrados" runat="server" Font-Bold="False" Font-Italic="True" Font-Names="Arial"
                                                                        Font-Size="8pt" ForeColor="#0000CC" Style="text-align: right" Text="0" Width="100%" ></asp:Label>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                
                                                                <td style="text-align: right">
                                                                    <asp:Label ID="lblFPago" runat="server" Text="Formalización De Pago *" Font-Names="Arial"
                                                                        Font-Size="8pt" Width="130px"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFPago" runat="server" Font-Names="Arial" Font-Size="8pt" Width="60px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtFPago_CalendarExtender" runat="server" Enabled="True"
                                                                        TargetControlID="txtFPago" Format="yyyy-MM-dd">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                                <td style="text-align: right">
                                                                    <asp:Label ID="lblPAprobados" runat="server" Text="Planos Aprobados *" Font-Names="Arial"
                                                                        Font-Size="8pt" Width="100px"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPAprob" runat="server" Font-Names="Arial" Font-Size="8pt" Width="60px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtPAprob_CalendarExtender" runat="server" Enabled="True"
                                                                        TargetControlID="txtPAprob" Format="yyyy-MM-dd">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                                <td style="text-align: right">
                                                                    <asp:Label ID="lblTotalValor" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Vlr Total De Cierre"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblVlrTC" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                        Text="0" Width="100%" Font-Italic="True" ForeColor="#0000CC"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">
                                                                    <asp:Label ID="lblPlazo" runat="server" Text="Plazo" Font-Names="Arial" Font-Size="8pt"
                                                                        Width="60px"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPlazo" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: center"
                                                                        Width="60px" AutoPostBack="true" OnTextChanged="txtPlazo_TextChanged">0</asp:TextBox>
                                                                    &nbsp;
                                                                </td>
                                                                <td style="text-align: right">
                                                                    <asp:Label ID="LCantOF" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cant OF A Fabricar" Visible="false"
                                                                        Width="110px"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCantOF" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: center" Visible="false"
                                                                        Width="60px" Enabled="false" AutoPostBack="True" OnTextChanged="txtCantOF_TextChanged">1</asp:TextBox>
                                                                </td>
                                                                <td style="text-align: right">
                                                                    <asp:Label ID="Label28" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Vlr Total Facturacion"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblSf" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right" Font-Bold = "True"
                                                                        Text="0" Width="100%" Font-Italic="True" ForeColor="#0000CC"></asp:Label>
                                                                </td>
                                                               
                                                            </tr>
                                                            <tr>
                                                                 <td style="text-align: right">
                                                                    <asp:Label ID="lblTotPiezas" runat="server" Text="Total De Piezas Cerradas" Font-Names="Arial"
                                                                        Font-Size="8pt" Width="130px" Visible="false"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTPiezas" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: center"
                                                                        Width="40px" AutoPostBack="True" OnTextChanged="txtTPiezas_TextChanged" Visible="false">0</asp:TextBox>
                                                                </td>
                                                                <td style="text-align: right">
                                                                    <asp:Label ID="lblTAsesoria" runat="server" Text="Dias Asisist Tecnica"
                                                                        Font-Names="Arial" Font-Size="8pt" Width="100px" Visible="true"></asp:Label>
                                                                </td>
                                                                <td>
                                                                 <asp:Label ID="lblDiasDisponibles" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: center" Font-Bold = "True"
                                                                        Text="0" Width="100%" Font-Italic="True" ForeColor="#0000CC"></asp:Label>
                                                                    <%--<asp:TextBox ID="txtTAsesoria" runat="server" Font-Names="Arial" Font-Size="8pt" Enabled = "false"
                                                                        Style="text-align: center" Width="60px" Visible="true" OnTextChanged="txtTAsesoria_TextChanged">0</asp:TextBox>--%>
                                                                </td>
                                                                <td style="text-align: right">
                                                                    <asp:Label ID="lblPorcCom" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Dias Consumidos"
                                                                        Width="90px" Visible="true"></asp:Label>
                                                                </td>
                                                                <td>
                                                                <asp:Label ID="lblDiasConsu" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: center" Font-Bold = "True"
                                                                        Text="0" Width="100%" Font-Italic="True" ForeColor="#0000CC"></asp:Label>

                                                                    <%--<asp:TextBox ID="txtPCom" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="true"
                                                                        Style="text-align: center" Width="60px">0</asp:TextBox>--%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td class="style126">
                                                   
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Button ID="btnGuardaCierre" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                        Font-Names="Arial" Font-Size="8pt" Height="21px" OnClick="btnGuardaCierre_Click"
                                                        Text="Guardar" Width="70px" Font-Bold="True" Visible= "False" />
                                                </td>
                                                <td colspan="2">
                                                    <asp:Button ID="btnSolicitud" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                        Font-Names="Arial" Font-Size="8pt" Text="Ir a Solic Facturacion" Width="140px"
                                                     Visible = "False"   Font-Bold="True" OnClientClick="Navigate()" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AccorOF" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="lblTemaOF" runat="server" Text="Orden de Fabricacion"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table style="width: 500px">
                                            <tr>
                                                <td class="style155">
                                                    &nbsp;
                                                </td>
                                                <td  style="text-align: right" colspan="4">
                                                    <asp:Panel ID="pnlOF" runat="server" Width="805px" >
                                                        <table>
                                                            <tr>
                                                                <td style="text-align: right">
                                                                   
                                                                </td>
                                                                <td>
                                                                 <td style="text-align: left">
                                                   <asp:LinkButton ID="lkActualizarOrden" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                        OnClick="lkActualizarOrden_Click">Actualizar</asp:LinkButton>
                                                </td>
                                                                </td>
                                                                <td style="text-align: right">
                                                                <asp:Label ID="LProducido" runat="server" Font-Names="Arial" Font-Size="8pt" Height="16px"
                                                                        Text="Planta" Width="50px"></asp:Label>
                                                                    <asp:DropDownList ID="cboProdOF" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                        Font-Size="8pt" Width="150px" OnSelectedIndexChanged= "cboProdOF_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="text-align: right">
                                                                    
                                                                        <asp:Label ID="LParte" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Parte"
                                                                        Width="40px"></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:DropDownList ID="cboParte" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                        Font-Size="8pt" OnSelectedIndexChanged= "cboParte_SelectedIndexChanged" Width="85px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="text-align: right">
                                                                    
                                                                        <asp:Label ID="lblm2Parte" runat="server" Font-Names="Arial" Font-Size="8pt" Text="M2"
                                                                        Width="60px"></asp:Label>
                                                                </td>
                                                                <td style="text-align: right">
                                                                    
                                                                        <asp:Label ID="lblPrecioParte" runat="server" Font-Names="Arial" Font-Size="8pt" Text="$"
                                                                        Width="100px"></asp:Label>
                                                                </td>
                                                                 <td style="text-align: right">
                                                                    
                                                                        <asp:Label ID="lblTipo" runat="server" Font-Names="Arial" Font-Size="8pt" Text=""
                                                                        Width="100px"></asp:Label>
                                                                </td>
                                                                <td>
                                                    <asp:Button ID="btnGenerar" runat="server" BackColor="#1C5AB6" Font-Bold="True" Font-Names="Arial" Visible="false"
                                                        Font-Size="8pt" OnClick="btnGenerar_Click" ForeColor="White" Text="Generar" Width="70px" />
                                                        
                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                                
                                               
                                            </tr>
                                            <tr>
                                                <td class="style155">
                                                    &nbsp;
                                                </td>
                                                <td class="style152">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td class="style156">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td class="style154">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td  >
                                                    &nbsp;<td colspan="4">
                                                        <asp:GridView ID="grvOF" runat="server" CellPadding="1" CellSpacing="4" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"
                                                            OnRowCommand="grvOF_RowCommand" Width="805px" DataKeyNames="Id_Ofa">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="IdOFA" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOFA" runat="server" Text='<%# Bind("Id_Ofa") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtOFA" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tipo">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTipo" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("TIPO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtTipo" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="OFA" HeaderStyle-Width="140px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNumero" runat="server" Text='<%# Bind("ORDEN") %>'></asp:Label>
                                                                        
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtOFA" Width="90px" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Producido En" HeaderStyle-Width="150px" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProd" Width = "90px" runat="server" Text='<%# Bind("PRODUCIDO_EN") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtProd" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="M2" HeaderStyle-Width="60px" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblM2Of" Width = "70px" runat="server" Text='<%# Bind("M2") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtM2Of" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Precio" HeaderStyle-Width="100px" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrecioof" Width = "70px" runat="server" Text='<%# Bind("VALOR") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtPrecioOf" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ver">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVer" runat="server" Text='<%# Bind("VERSION") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtVer" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Parte">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblParte" runat="server" Text='<%# Bind("PARTE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtParte" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Fecha">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFecha" Width="100px" runat="server" Text='<%# Bind("FECHA") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Responsable" HeaderStyle-Width="350px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblResp" runat="server" Width = "110px" Text='<%# Bind("RESPONSABLE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtResp" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="M2Prod" HeaderStyle-Width="60px" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblM2Prod" Width = "70px" runat="server" Text='<%# Bind("m2Prod") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtM2Prod" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="M2Dif" HeaderStyle-Width="60px" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblM2Dif" Width = "70px" runat="server" Text='<%# Bind("m2Diferencia") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtM2Dif" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnListaChequeo" runat="server" ImageUrl="~/Imagenes/editar.gif"
                                                                            Width="15px" ToolTip="Lista De Chequeo" CommandArgument="<%#((GridViewRow) Container).RowIndex %>"
                                                                            CommandName="OFA" OnClientClick="Lista()" />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Width="15px" />
                                                                    <HeaderStyle Width="15px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" />
                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" />
                                                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                        </asp:GridView>
                                                        <td class="style154">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                            </tr>
                                            <tr>
                                                <td class="style155">
                                                    &nbsp;
                                                </td>
                                                <td class="style152">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td class="style156">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td class="style154">
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AccFUP" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected" Visible="false">
                                    <Header>
                                        <asp:Label ID="Label5" runat="server" Text="Formato Unico de Proyecto"></asp:Label>
                                    </Header>
                                    <Content>
                                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="800px" Height="600px">
                                        </rsweb:ReportViewer>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AcordSubirDoc" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected" Visible="true">
                                    <Header>
                                        <asp:Label ID="Label24" runat="server" Text="Subir Archivos"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table style="width: 738px">
                <tr>
                    <td style="text-align: center" class="style128">
                        &nbsp;
                    </td>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style128">
                        &nbsp;
                    </td>
                    <td style="text-align: right" class="style137">
                        <asp:Label ID="lblTitOFTec" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Font-Strikeout="False" Font-Underline="False" Text="Indicar OF De Referencia"
                            Width="140px" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtOFRef" runat="server" Font-Names="Arial" Font-Size="8pt" Width="130px"
                            Visible="False"></asp:TextBox>
                    </td>
                    <td class="style138">
                        <asp:Label ID="lblDetOF1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="En caso de que no se indique la OF las perforaciones se enviaran  estandar. "
                            Width="374px" Font-Bold="False" Font-Italic="True" ForeColor="#0000CC" Visible="False"></asp:Label>
                        <asp:Label ID="lblDetOF2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="En caso de no haber coincidencia entre perforaciones estas se realizarán en obra."
                            Width="335px" Font-Italic="True" ForeColor="#0000CC" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style128">
                        &nbsp;
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblTipoAnexo" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Text="Tipo Anexo"
                            Width="65px" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="cboTipoAnexo" runat="server" Width="140px" Font-Names="Arial"
                            Font-Size="8pt" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style123">
                        &nbsp;
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblTipoProyecto" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Text="Tipo Proyecto"
                            Width="90px" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboTipoProy" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="140px" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style128">
                        &nbsp;
                    </td>
                    <td style="text-align: right" align="right">
                        &nbsp;
                    </td>
                    <td style="text-align: right">
                        <asp:FileUpload ID="FDocument" runat="server" />
                    </td>
                    
                    <td class="style138">
                        <asp:Label ID="lblArchivo" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Plano / Listado / Documentación"
                            Width="374px" Font-Italic="True" ForeColor="#0000CC" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style123">
                        &nbsp;
                    </td>
                    <td class="style130">
                        &nbsp;
                    </td>
                    <td style="text-align: right">
                        <asp:Button ID="btnSubirPlano" runat="server" Font-Names="Arial" ForeColor="White"
                            Font-Size="8pt" OnClick="btnSubirPlano_Click" Text="Subir" Width="70px" BackColor="#1C5AB6"
                            Font-Bold="True" Visible="False" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" BackColor="#1C5AB6" ForeColor="White"
                            Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Text="Cancelar" Width="70px"
                            OnClick="btnCancelar_Click" Visible="False" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style123">
                        &nbsp;
                    </td>
                    <td class="style130">
                        &nbsp;
                    </td>
                    <td class="style104">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AccRecotiza" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected" Visible="true">
                                    <Header>
                                        <asp:Label ID="Label8" runat="server" Text="Motivos de Recotizacion"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LMotiRec" runat="server" Font-Names="Arial" Font-Size="8pt" Text=" "
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Panel ID="pnlMotivo" runat="server" Font-Names="Arial" Font-Size="8pt" GroupingText="Motivo"
                                                        Width="230px" Visible="false">
                                                        <asp:DropDownList ID="cboRecotizacion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Width="195px">
                                                        </asp:DropDownList>
                                                    </asp:Panel>
                                                </td>
                                                <td>
                                                    <asp:Panel ID="pnlComRecotiza" runat="server" Width="400px" Font-Names="Arial" Font-Size="8pt"
                                                        GroupingText="Comentario" Style="text-align: center" Visible="False">
                                                        <asp:TextBox ID="txtComRec" runat="server" Font-Names="Arial" Font-Size="8pt" Height="50px"
                                                            TextMode="MultiLine" Width="380px"></asp:TextBox>
                                                    </asp:Panel>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Button ID="btnGuardarRecotiza" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                        Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btnGuardarRecotiza_Click"
                                                        Text="Adicionar Motivo" Width="120px" Visible="False" />
                                                    &nbsp;
                                                    <asp:Button ID="btnRecotizar" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                        Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btnRecotizar_Click"
                                                        Text="Recotizar" Width="70px" Visible="False" />
                                                    &nbsp;
                                                    <asp:Button ID="btnNewRecot" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                        Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btnNewRecot_Click"
                                                        Text="Nuevo" Width="70px" Visible="False" />
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td colspan="2">
                                                    <asp:GridView ID="grvRecotiza" runat="server" CellPadding="1" CellSpacing="4" Font-Names="Arial"
                                                        Font-Size="8pt" ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" />
                                                        <PagerStyle BackColor="#2461BF" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    </asp:GridView>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </asp:AccordionPane>
                               <asp:AccordionPane ID="AccControlCambios" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected" Visible="true">
                                    <Header>
                                        <asp:Label ID="Label9" runat="server" Text="Control de Cambios"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table class="style1">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td colspan="4">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td colspan="4">
                                                    <asp:Panel ID="pnlTema" runat="server" BackColor="White" Font-Names="Arial" Font-Size="8pt"
                                                        GroupingText="Tema">
                                                        <table class="style1">
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:TextBox ID="txtTema" runat="server" Font-Names="Arial" Font-Size="8pt" Width="600pt"></asp:TextBox>
                                                                    <%-- <asp:RoundedCornersExtender ID="txtTema_RoundedCornersExtender" runat="server" BorderColor="Silver"
                                                                        Enabled="True" Radius="6" TargetControlID="txtTema">
                                                                    </asp:RoundedCornersExtender>--%>
                                                                    <asp:TextBoxWatermarkExtender ID="txtTema_TextBoxWatermarkExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtTema" WatermarkCssClass="watermarked" WatermarkText="Descripción Del Tema">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="cboAreaResp" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                        Font-Size="8pt" Width="150pt" OnSelectedIndexChanged="cboAreaResp_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <%-- <asp:RoundedCornersExtender ID="cboAreaResp_RoundedCornersExtender" runat="server"
                                                                        BorderColor="Silver" Enabled="True" Radius="6" TargetControlID="cboAreaResp">
                                                                    </asp:RoundedCornersExtender>--%>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="cboResponTema" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                        Font-Size="8pt" Width="150pt">
                                                                    </asp:DropDownList>
                                                                    <%--  <asp:RoundedCornersExtender ID="cboResponTema_RoundedCornersExtender" runat="server"
                                                                        BorderColor="Silver" Enabled="True" Radius="6" TargetControlID="cboResponTema">
                                                                    </asp:RoundedCornersExtender>--%>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFecCierre" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        Width="60pt" Visible="false"></asp:TextBox>
                                                                    <%--  <asp:RoundedCornersExtender ID="txtFecCierre_RoundedCornersExtender" runat="server"
                                                                        BorderColor="Silver" Enabled="True" Radius="6" TargetControlID="txtFecCierre">
                                                                    </asp:RoundedCornersExtender>--%>
                                                                    <asp:TextBoxWatermarkExtender ID="txtFecCierre_TextBoxWatermarkExtender2" runat="server"
                                                                        Enabled="True" TargetControlID="txtFecCierre" WatermarkCssClass="watermarked"
                                                                        WatermarkText="Fecha Cierre">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="txtFecCierre_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                                                        TargetControlID="txtFecCierre">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="cboEstadoTema" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                        Font-Size="8pt" Width="80pt" Visible="false">
                                                                    </asp:DropDownList>
                                                                    <%-- <asp:RoundedCornersExtender ID="cboEstadoTema_RoundedCornersExtender" runat="server"
                                                                        BorderColor="Silver" Enabled="True" Radius="6" TargetControlID="cboEstadoTema">
                                                                    </asp:RoundedCornersExtender>--%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnIngresarTema" runat="server" Text="Ingresar Tema" BackColor="#1C5AB6"
                                                                        BorderColor="#1C5AB6" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                        Font-Names="Arial" Font-Size="10pt" ForeColor="White" OnClick="btnIngresarTema_Click"
                                                                        Width="120px" />
                                                                    <%--   <asp:RoundedCornersExtender ID="btnIngresarTema_RoundedCornersExtender" runat="server"
                                                                        BorderColor="Silver" Enabled="True" Radius="6" TargetControlID="btnIngresarTema">
                                                                    </asp:RoundedCornersExtender>--%>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnNuevoTema" runat="server" Text="Nuevo Tema" BackColor="#1C5AB6"
                                                                        BorderColor="#1C5AB6" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                        Font-Names="Arial" Font-Size="10pt" ForeColor="White" OnClick="btnNuevoTema_Click"
                                                                        Width="120px" />
                                                                    <%-- <asp:RoundedCornersExtender ID="btnNuevoTema_RoundedCornersExtender" runat="server"
                                                                        BorderColor="Silver" Enabled="True" Radius="6" TargetControlID="btnNuevoTema">
                                                                    </asp:RoundedCornersExtender>--%>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td colspan="4">
                                                    <hr />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:GridView ID="grvTema" runat="server" CellSpacing="1" ForeColor="White" GridLines="Both"
                                                        AutoGenerateColumns="False" DataKeyNames="coc_id" CssClass="Grid" OnRowCommand="grvTema_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgOrdersShow" runat="server" ImageUrl="~/imagenes/toolkit-arrow.png"
                                                                        Width="15px" OnClick="Show_Hide_OrdersGrid" ToolTip="Ver Comentarios" CommandArgument="Show" />
                                                                   
                                                                </ItemTemplate>
                                                                <ControlStyle Width="15px" />
                                                                <HeaderStyle Width="15px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-Width="15px" ControlStyle-Width="15px">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnComentarioTema" runat="server" ImageUrl="~/Imagenes/editar.gif"
                                                                        Width="15px" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" CommandName="Comentario"
                                                                        ToolTip="Ingresar Comentarios" />
                                                                </ItemTemplate>
                                                                <ControlStyle Width="15px" />
                                                                <HeaderStyle Width="15px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEstado" runat="server" ImageUrl="~/Imagenes/Close.png" Width="15px"
                                                                        OnClientClick="return confirm('Esta seguro de cerrar el tema?')" CommandArgument="<%#((GridViewRow) Container).RowIndex %>"
                                                                        CommandName="Estado" ToolTip="Cerrar Tema" />
                                                                </ItemTemplate>
                                                                <ControlStyle Width="15px" />
                                                                <HeaderStyle Width="15px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="idTema" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDTema" runat="server" Text='<%# Bind("coc_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtIDTema" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tema">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTema" Width="200px" runat="server" Text='<%# Bind("coc_tema") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtTema" Width="200px" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="F.Apertura">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LFecApe" Width="60px" runat="server" Text='<%# Bind("FecApertura") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtFecApe" Width="60px" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="F.Cierre">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LFecCierre" Width="60px" runat="server" Text='<%# Bind("FecCierre") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtFecCierre" Width="60px" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Solicitante">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSolic" Width="160px" runat="server" Text='<%# Bind("Solicitante") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtSolic" Width="160px" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Area Responsable">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAreaResp" Width="90px" runat="server" Text='<%# Bind("AreaResponsable") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtAreaResp" Width="90px" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Responsable">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRespTema" Width="160px" runat="server" Text='<%# Bind("Responsable") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtRespTema" Width="160px" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Estado">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEstadoTema" Width="50px" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtEstadoTema" Width="50px" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Creado Por" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCreaTema" runat="server" Text='<%# Bind("UsuCrea") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtCreaTema" runat="server" Visible="false"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                           
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                 <tr style="display: inherit;">
                                                                    <td align="left">
                                                                        <asp:Panel ID="pnlOrders" runat="server" Visible="false" Style="position: relative">
                                                                            <asp:GridView ID="grvCom" runat="server" CellPadding="1" ForeColor="White" GridLines="None"
                                                                                AutoGenerateColumns="False" CssClass="ChildGrid">
                                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="idComTema" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblccc_id" runat="server" Text='<%# Bind("ccc_id") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtccc_id" Width="10px" runat="server"></asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Fecha">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblfechaComTema" Width="70px" runat="server" Text='<%# Bind("fecha") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtfechaComTema" Width="70px" runat="server"></asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AreaResponsable">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="LAreaResponsable" Width="100px" runat="server" Text='<%# Bind("AreaResponsable") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="TAreaResponsable" Width="100px" runat="server"></asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Solicitante">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="LSolicitante" Width="220px" runat="server" Text='<%# Bind("Solicitante") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="TSolicitante" Width="220px" runat="server"></asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Comentario">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="LComentario" Width="300px" runat="server" Text='<%# Bind("ccc_comentario") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="TComentario" Width="300px" runat="server"></asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <EditRowStyle BackColor="#999999" />
                                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" />
                                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" />
                                                                                <PagerStyle BackColor="#284775" HorizontalAlign="Center" />
                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                        <%--  <a href="javascript:expandcollapse('div<%# Eval("coc_id") %>', 'one');">
                                                                        <img id="imgdiv<%# Eval("coc_id") %>" border="0" src="~/Imagenes/editar.gif" title="Ver Comentarios" />
                                                                    </a>--%>
                                                                    </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <ControlStyle Width="15px" />
                                                                <HeaderStyle Width="15px" />
                                                            </asp:TemplateField>                                                            
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td colspan="4">
                                                    <hr />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td colspan="4">
                                                    <asp:Panel ID="pnlComen" runat="server" BackColor="White" Font-Names="Arial" Font-Size="8pt"
                                                        GroupingText="Comentarios" Visible="false">
                                                        <table class="style1">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:TextBox ID="txtComTema" runat="server" Font-Names="Arial" Font-Size="8pt" Width="600pt"
                                                                        Height="30pt" TextMode="MultiLine"></asp:TextBox>
                                                                    <asp:RoundedCornersExtender ID="txtComTema_RoundedCornersExtender" runat="server"
                                                                        BorderColor="Silver" Enabled="True" Radius="6" TargetControlID="txtComTema">
                                                                    </asp:RoundedCornersExtender>
                                                                    <asp:TextBoxWatermarkExtender ID="txtComTema_TextBoxWatermarkExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtComTema" WatermarkCssClass="watermarked" WatermarkText="Comentario Del Tema">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnGuardarComTema" runat="server" Text="Ingresar Comentario" BackColor="#1C5AB6"
                                                                        BorderColor="#1C5AB6" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                        Font-Names="Arial" Font-Size="10pt" ForeColor="White" OnClick="btnGuardarComTema_Click"
                                                                        Width="150px" />
                                                                    <%--<asp:RoundedCornersExtender ID="btnGuardarComTema_RoundedCornersExtender" runat="server"
                                                                        BorderColor="Silver" Enabled="True" Radius="6" TargetControlID="btnGuardarComTema">
                                                                    </asp:RoundedCornersExtender>--%>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnNuevoComTema" runat="server" Text="Nuevo Comentario" BackColor="#1C5AB6"
                                                                        BorderColor="#1C5AB6" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                        Font-Names="Arial" Font-Size="10pt" ForeColor="White" OnClick="btnNuevoComTema_Click"
                                                                        Width="150px" />
                                                                    <%--<asp:RoundedCornersExtender ID="btnNuevoComTema_RoundedCornersExtender" runat="server"
                                                                        BorderColor="Silver" Enabled="True" Radius="6" TargetControlID="btnNuevoComTema">
                                                                    </asp:RoundedCornersExtender>--%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td colspan="4">
                                                    <hr />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td colspan="4">
                                                    <asp:GridView ID="grvComentario" runat="server" CellPadding="1" CellSpacing="4" ForeColor="White"
                                                        GridLines="None" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="idComTema" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblccc_id" runat="server" Text='<%# Bind("ccc_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtccc_id" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fecha">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblfechaComTema" runat="server" Text='<%# Bind("fecha") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtfechaComTema" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AreaResponsable">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LAreaResponsable" runat="server" Text='<%# Bind("AreaResponsable") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TAreaResponsable" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Solicitante">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LSolicitante" runat="server" Text='<%# Bind("Solicitante") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TSolicitante" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Comentario">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LComentario" runat="server" Text='<%# Bind("ccc_comentario") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TComentario" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" />
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" />
                                                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                    </asp:GridView>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td colspan="4">
                                                    <hr />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AccordionPane1" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected" Visible="true">
                                    <Header>
                                        <asp:Label ID="Label11" runat="server" Text="Acta de Entrega Equipo"></asp:Label>
                                    </Header>
                                    <Content>
                                        <asp:UpdatePanel ID="UpdatePanel700" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="panelComboIdOfa" runat="server" GroupingText="Ordenes" Width="785px"
                                                    Height="50px">
                                                    <table style="width: 777px">
                                                        <tr>
                                                            <td class="style5">
                                                                <asp:Label ID="Label16" runat="server" Text="Seleccione la Orden: "></asp:Label>
                                                                &nbsp;&nbsp;
                                                                <asp:DropDownList ID="cboComboOrden" runat="server" AutoPostBack="True" Height="16px"
                                                                    OnSelectedIndexChanged="cboComboOrden_SelectedIndexChanged" Width="131px" Style="font-size: 8pt;
                                                                    font-family: Arial">
                                                                </asp:DropDownList>
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="Label19" runat="server" Text="Digite las Ordenes: "></asp:Label>
                                                               
                                                                <asp:TextBox ID="txtOrdenes" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Width="130px"></asp:TextBox>
                                                                                    &nbsp;&nbsp;&nbsp;
                                                                                    <asp:Label ID="lblMens" runat="server" Visible="False" Style="font-size: 8pt;
                                                                    font-family: Arial; color: Blue"></asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;
                                                                    &nbsp;&nbsp;&nbsp;
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" Enabled="true"  OnClick="lkContacto_Click">Crear Contacto</asp:LinkButton>                                                
                                                                    &nbsp;&nbsp;&nbsp;
                                                                    &nbsp;&nbsp;&nbsp;
                                                                    <asp:LinkButton ID="LinkButton3"  OnClick="LinkButton3_Click" runat="server">Actualizar</asp:LinkButton>                                                                   
                                                                    
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel400" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="panelActaEntrega" runat="server" GroupingText="Acta de Entrega de Equipo" Width="830px"
                                                    Height="880px">
                                                    <asp:UpdatePanel ID="UpdatePanel100" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="panelContantos" runat="server" GroupingText="Por el Cliente" Width="800px">
                                                                <table style="margin-bottom: 0px; height: 139px;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ListBox ID="listaCont" runat="server" Font-Names="Arial" Font-Size="8pt" Height="120px"
                                                                                Width="330px"></asp:ListBox>
                                                                        </td>
                                                                        <td style="width: 54px; text-align: center">
                                                                            <asp:Button ID="btnAgregarCont" runat="server"  Text=">" Font-Bold= "true" OnClick="btnAgregarCont_Click"   Width="30px" Height="25px" />
                                                                            <br />
                                                                            <asp:Button ID="btnEliminarCont" runat="server" OnClick="btnEliminarCont_Click" Text="<" Font-Bold= "true" Width="30px" Height="28px" />
                                                                            <br />
                                                                            <br />
                                                                            <asp:Button ID="btnElimTodosCont" runat="server" OnClick="btnElimTodosCont_Click"
                                                                               Text="<<" Font-Bold= "true" 
                                                                                Width="30px" Height="28px" />
                                                                            <br />
                                                                            <br />
                                                                        </td>
                                                                        <td style="width: 7px; text-align: left;">
                                                                            <asp:ListBox ID="listaContAgg" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                Height="120px" Width="330px" SelectionMode="Multiple"></asp:ListBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdatePanel ID="UpdatePanel200" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="panelForsa" runat="server" GroupingText="Por Forsa" Width="800px">
                                                                <table>
                                                                    <tr>
                                                                        <td class="style3">
                                                                            <asp:Label ID="Label12" runat="server" Text="Area Personal(FORSA): "></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="cboAreaForsa" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboAreaForsa_SelectedIndexChanged"
                                                                                Height="20px" Width="161px" Style="font-size: 8pt; font-family: Arial">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table style="margin-bottom: 0px">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ListBox ID="listaForsa" runat="server" Font-Names="Arial" Font-Size="8pt" Height="120px"
                                                                                Width="330px"></asp:ListBox>
                                                                        </td>
                                                                        <td style="width: 54px; text-align: center">
                                                                            <asp:Button ID="btnAgregarForsa" runat="server" OnClick="btnAgregarForsa_Click" Text=">" Font-Bold= "true"  Width="30px" Height="28px" />
                                                                            <br />
                                                                            <asp:Button ID="btnEliminarForsa" runat="server" OnClick="btnEliminarForsa_Click"
                                                                                Text="<" Font-Bold= "true" 
                                                                                Width="30px" Height="28px" />
                                                                            <br />
                                                                            <br />
                                                                            <asp:Button ID="btnElimTodosForsa" runat="server" OnClick="btnElimTodosForsa_Click"
                                                                                Text="<<" Font-Bold= "true" 
                                                                                Width="30px" Height="28px" />
                                                                            <br />
                                                                            <br />
                                                                        </td>
                                                                        <td style="width: 7px; text-align: left;">
                                                                            <asp:ListBox ID="listaForsaAgg" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                Height="120px" Width="330px" SelectionMode="Multiple"></asp:ListBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdatePanel ID="UpdatePanel300" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="panelDatos" runat="server" GroupingText="Registro" Width="800px">
                                                                <table style="width: 773px">
                                                                    <tr>
                                                                        <td class="styleTextoDer">
                                                                            <asp:Label ID="Label13" runat="server" Text="Tecnicas del Equipo: "></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtTecnicasEq" TextMode="MultiLine" runat="server" Wrap="true" Height="72px"
                                                                                Width="564px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="styleTextoDer">
                                                                            <asp:Label ID="Label14" runat="server" Text="Cronograma de Despacho: "></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtCronoDespacho" TextMode="MultiLine" runat="server" Wrap="true"
                                                                                Height="72px" Width="564px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="styleTextoDer">
                                                                            <asp:Label ID="Label15" runat="server" Text="Condicion de Embalaje: "></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtConEmbalaje" TextMode="MultiLine" runat="server" Wrap="true"
                                                                                Height="72px" Width="564px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="styleTextoDer">
                                                                            <asp:Label ID="Label3" runat="server" Text="Condicion Financiera: "></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtConFinanciera" TextMode="MultiLine" runat="server" Wrap="true"
                                                                                Height="72px" Width="564px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdatePanel ID="UpdatePanel500" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="panelObra" runat="server" GroupingText="Persona Responsable" Width="800px">
                                                                <table style="margin-bottom: 0px">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ListBox ID="listaPerRes" runat="server" Font-Names="Arial" Font-Size="8pt" Height="120px"
                                                                                Width="330px"></asp:ListBox>
                                                                        </td>
                                                                        <td style="width: 54px; text-align: center">
                                                                            <br />
                                                                            <asp:Button ID="btnAgregarResp" runat="server" OnClick="btnAgregarResp_Click" Text=">" Font-Bold= "true" Width="35px" Height="35px" />
                                                                            <br />
                                                                            <br />
                                                                        </td>
                                                                        <td style="width: 7px; text-align: left;">
                                                                            <asp:ListBox ID="listaPerResAgg" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                Height="28px" Width="330px"></asp:ListBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdatePanel ID="UpdatePanel600" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="panelBotones" runat="server" Width="782px">
                                                                <table>
                                                                    <tr>
                                                                        <td class="style4">
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="btnGuardarActa" runat="server" Text="Guardar" AutoPostBack="False"
                                                                                BorderColor="#999999" BackColor="#1C5AB6" Font-Bold="True" Font-Names="Arial"
                                                                                Font-Size="8pt" ForeColor="White" Width="100px" OnClick="btnGuardarActa_Click" Visible="False" />
                                                                            &nbsp;&nbsp;
                                                                            <asp:Button ID="btnReporte" runat="server" Text="Ver Acta Entrega" AutoPostBack="False"
                                                                                BorderColor="#999999" BackColor="#1C5AB6" Font-Bold="True" Font-Names="Arial"
                                                                                Font-Size="8pt" ForeColor="White" Width="110px" OnClick="btnReporte_Click" Visible="true" />
                                                                            &nbsp;&nbsp;
                                                                            <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar" BorderColor="#999999"
                                                                                BackColor="#1C5AB6" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                                                                                Width="100px" OnClick="btnFinalizar_Click" Visible="False" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="PaneActaEntrega" runat="server"
                                    ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial" 
                                    Font-Size="8pt" HeaderCssClass="accordionHeader" 
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="Label18" runat="server" Text="Formato Acta de Entrega Equipo"></asp:Label>
                                    </Header>
                                    <Content>
                                        <rsweb:ReportViewer ID="ReportActaEntrega" runat="server" Width="800px" Height="600px">
                                        </rsweb:ReportViewer>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AccordionPane2" runat="server" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected" Visible="true">
                                    <Header>
                                        <asp:Label ID="Label17" runat="server" Text="Cambio de Estado del Proyecto"></asp:Label>
                                    </Header>
                                    <Content>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="panelCambioEstado" runat="server" GroupingText="Cambiar Estado" Width="890px">
                                                    <table>
                                                        <tr>
                                                            <td class="style2"> 
                                                                <asp:GridView ID="grdCambiarEstado" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                    BorderColor="#1C5AB6" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical"
                                                                    Width="870px" Font-Names="Arial" Font-Size="8pt" ForeColor="#1C5AB6">
                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="fup" HeaderText="FUP"></asp:BoundField>
                                                                        <asp:BoundField DataField="version" HeaderText="Version" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="estado" HeaderText="Estado Actual">
                                                                            <ItemStyle Width="100pt"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Cambiar a:">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="cboEstado" DataSource='<%# estados() %>' DataTextField="estado"
                                                                                    DataValueField="idEstado" runat="server" Height="23px" Width="140px">
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Motivo:">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="cboMotivo" DataSource='<%# motivos() %>' DataTextField="motivo"
                                                                                    DataValueField="idMotivo" runat="server" Height="23px" Width="140px">
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Descripcion">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="340px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#1C5AB6" ForeColor="Black" />
                                                                    <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#1C5AB6" ForeColor="Black" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right">
                                                                <br />
                                                                <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" OnClick="btnActualizar_Click"
                                                                    AutoPostBack="True" BorderColor="#999999" BackColor="#1C5AB6" Font-Bold="True"
                                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="White" Width="100px" />
                                                            </td>
                                                        </tr>
                                                   </table>

                                                   <asp:Panel ID="panel1" runat="server" GroupingText="Cambios Realizados" Width="880px">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="grdCambios" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Font-Names="Arial"
                                                                    Font-Size="8pt" ForeColor="#1C5AB6" Width="870px" Height="60px">
                                                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="fup" HeaderText="FUP" />
                                                                        <asp:BoundField DataField="version" HeaderText="Version" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="EstIncial" HeaderText="Estado Inicial" />
                                                                        <asp:BoundField DataField="EstFinal" HeaderText="Estado Final" />
                                                                        <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                                                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                                                        <asp:BoundField DataField="motivo" HeaderText="Motivo">
                                                                         <ItemStyle Width="80pt"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Observacion">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="obser" Text='<%# Eval("obser") %>' runat="server" Enabled="false" TextMode="MultiLine" Width="330px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                    <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                </asp:Panel>
                                                
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </Content>
                                </asp:AccordionPane>
                            </Panes>
                        </asp:Accordion>
                        <br />
                        <br />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            
            <br />
            </td> </tr> </table> </td> </tr> </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">
                <asp:Label ID="lblEnviando" runat="server" Text="Cargando..." Font-Names="Arial"
                    Font-Size="14pt"></asp:Label>
                <img src="Imagenes/ajax-loader.gif" alt="Loading" height="30" style="text-align: center"
                    width="30" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
