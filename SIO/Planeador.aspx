<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralAmpliaPlaneador1.Master" AutoEventWireup="true" CodeBehind="Planeador.aspx.cs" Inherits="SIO.Planeador" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/2.1.0/jquery.min.js" type="text/javascript"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.2.1.min.js" type="text/css" ></script>
    <script type="text/javascript" src="http://rawgit.com/vitmalina/w2ui/master/dist/w2ui-1.5.rc1.min.js"></script>
    <link rel="stylesheet" type="text/css" href="http://rawgit.com/vitmalina/w2ui/master/dist/w2ui-1.5.rc1.min.css" />   --%> 

     <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.1.0/jquery.min.js" type="text/javascript"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.2.1.min.js" type="text/css" ></script>
    <script type="text/javascript" src="Scripts/dist/w2ui-1.5.rc1.min.js"></script>
    <link rel="stylesheet" type="text/css" href="Scripts/dist/w2ui-1.5.rc1.min.css" />    
    
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />   
    <script src="js/logicaGridPlaneador.js" type="text/jscript"></script>
    <title>Planeador Web</title>
    <style type="text/css">
        .auto-style1 {
            width: 817px;
        }

        select {
            font-weight:bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="tab1" class="tab-pane active" width="100%" runat="server">
                <asp:Panel ID="Panel1" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="true" Width="100%">
                    <table width="100%">
                        <tr>
                             <td>
                                <asp:Label runat="server" ID="lblPlanta" Text="Planta:"></asp:Label>
                                <asp:DropDownList runat="server" ID="cboPlanta" OnTextChanged="cboPlanta_TextChanged"  AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblTipoSolicitud" Text="Tipo Solicitud:"></asp:Label>
                                <asp:DropDownList runat="server" ID="cboTipoSolicitud" OnSelectedIndexChanged="cboTipoSolicitud_TextChanged" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoSolicitud" Text="No. Solicitud:"></asp:Label>
                                <asp:DropDownList runat="server" ID="cboNoSolicitud" OnSelectedIndexChanged="cboNoSolicitud_TextChanged" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblRaya" Text="Raya:"></asp:Label>
                                <asp:DropDownList runat="server" ID="cboRaya" OnSelectedIndexChanged="cboRaya_TextChanged" AutoPostBack="true"></asp:DropDownList>
                            </td>

                            <td>
                                <asp:Label runat="server" ID="lblFamilia" Text="Familia:"></asp:Label>
                                <asp:DropDownList runat="server" ID="cboFamilia" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnAggFm" runat="server" visible="false" Text=">" OnClick="btnAggFm_Click"
                                    BackColor="#1C5AB6" ForeColor="White"
                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
                                <br />
                                <asp:Button ID="btnElmFm" runat="server" Visible="false" Text="<" OnClick="btnElmFm_Click"
                                    BackColor="#1C5AB6" ForeColor="White"
                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
                            </td>
                            <td>
                                <asp:ListBox ID="listFm" runat="server" Width="80px" Visible="false" Height="50px"></asp:ListBox>
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkKambam" Text="Kambam" />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnCargar" Text="Cargar Familia(s)" OnClick="btnCargar_Click"
                                    BackColor="#1C5AB6" ForeColor="White"
                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Width="119px" />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnSugiere" Text="ExploSuguiere" 
                                    BackColor="Silver" ForeColor="#666666"
                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Width="98px" OnClick="btnSugiere_Click" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <hr width="100%" align="center" color="blue" size="2" visible="false" id="hr1" runat="server" />
                                <table width="100%" id="tblMP" visible="false" runat="server">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblMP" Text="Agregar Materia Prima:"></asp:Label>
                                            <asp:DropDownList runat="server" ID="cboMP" AutoPostBack="true" OnSelectedIndexChanged="cboMP_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblColumnas" Text="Eliminar Columnas:"></asp:Label>
                                            <select id="cboColumns" name="cboColumns" runat="server" onchange="deleteColumn(this);"></select>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnEliminarRegitro" OnClientClick="eliminarRegistros()" Text="Eliminar Registros"
                                                BackColor="#1C5AB6" ForeColor="White"
                                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"></asp:Button>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblEstado" Text="" Font-Names="Arial" Font-Size="10pt" Font-Bold="true" ForeColor="DarkBlue"></asp:Label>
                                            <input id="lblIdEstado" runat="server" type="hidden" />
                                            <input id="lblIdExploPrincipal" runat="server" type="hidden" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="grid2" style="width: 100%; height: 400px;"></div>            
        </ContentTemplate>
    </asp:UpdatePanel> 
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div id="tab2" class="tab-pane active" style="width: 100%;" runat="server" visible="false">
                <hr width="100%" align="center" color="blue" size="2" visible="true" id="hr2" runat="server" /> 
                <table style="width: 100%;" >
                    <tr>
                        <td style="width: 50%;">
                            
                        </td>
                        <td style="width: 50%;" align="right">
                            <table>
                                <tr>
                                    <td align="right">
                                        <asp:Button runat="server" ID="btnNuevo" OnClick="btnNuevo_Click" Text="Nuevo"
                                            BackColor="#1C5AB6" ForeColor="White"
                                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"></asp:Button>
                                    </td>
                                    <td align="right">
                                        <asp:Button runat="server" ID="btnGuardarExplosion" OnClick="btnGuardarExplosion_Click" onclientclick="return confirm('¿Desea Guardar la orden?');" Text="Guardar"
                                            BackColor="#1C5AB6" ForeColor="White"
                                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"></asp:Button>
                                    </td>
                                    <td align="right">
                                        <asp:Button runat="server" ID="btnActualizar" OnClientClick="updateExplosion()" Text="Actualizar"
                                            BackColor="#1C5AB6" ForeColor="White"
                                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                            Visible ="false" onclick="btnActualizar_Click1"></asp:Button>
                                    </td>
                                    <td align="right">
                                        <asp:Button runat="server" ID="btnConfirmar" OnClick="btnConfirmar_Click"  onclientclick="return confirm('¿Desea Confirmar la orden?');" Text="Confirmar"
                                            BackColor="#1C5AB6" ForeColor="White"
                                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"></asp:Button>
                                    </td>
                                    <td align="right">
                                        <asp:Button runat="server" ID="btnSolicitudMateriaPrima" onclientclick="return confirm('¿Desea Confirmar la solicitud de materia prima?');" Text="Solicitud Materia Prima"
                                            BackColor="#1C5AB6" ForeColor="White"
                                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btnSolicitudMateriaPrima_Click"></asp:Button>
                                    </td>
                                     <td align="right">
                                        <asp:Button runat="server" ID="btnAnular" OnClick="btnAnular_Click" onclientclick="return confirm('¿Desea aunlar la orden?');" Text="Anular"
                                            BackColor="#1C5AB6" ForeColor="White"
                                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>            
        </ContentTemplate>
    </asp:UpdatePanel> 
    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td align="center">
                        <div id="gridMP" style="width: 30%; height: 300px;"></div>
                    </td>
                </tr>
            </table>                       
        </ContentTemplate>
    </asp:UpdatePanel> 
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
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
