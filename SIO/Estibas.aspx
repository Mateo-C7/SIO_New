<%@ Page Title="Estibas" Language="C#" MasterPageFile="~/BalizasMaster.Master" AutoEventWireup="true"
    CodeBehind="Estibas.aspx.cs" Inherits="SIO.Estibas" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style19
        {
            color: red;
            font-size: 12.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
        }
        .style21
        {
            color: #00B050;
            font-size: 16.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
        }
        .style22
        {
            color: white;
            font-size: 16.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            height: 11px;
        }
        .style23
        {
            color: red;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
        }
        .style89
        {
            color: red;
            font-size: 42.0pt;
            font-weight: 700;
            font-family: Arial Black;
            text-align: center;
            width: 1443px;
            height: 27px;
        }
        .style169
        {
            color: #003399;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: left;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            width: 3px;
        }
        .style181
        {
            color: #003399;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            height: 87px;
        }
        .style184
        {
            color: #003399;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: right;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            width: 245px;
        }
        .style197
        {
            width: 475px;
        }
        .style198
        {
            width: 10px;
        }
        .style200
        {
            color: #003399;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: left;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            width: 199px;
        }
        .style201
        {
            color: #003399;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: right;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            width: 3px;
        }
        .style204
        {
            color: #244062;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            width: 137px;
        }
        .style210
        {
            color: #00B050;
            font-size: 12.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: right;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            width: 6px;
        }
        .style212
        {
            width: 11px;
        }
        .style213
        {
            color: white;
            font-size: 11.0pt;
            font-weight: bold;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            height: 21px;
        }
        .style215
        {
            color: #003399;
            font-size: 12.0pt;
            font-weight: bold;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: right;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            width: 6px;
        }
        .style217
        {
            color: #00B050;
            font-size: 12.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: left;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            width: 113px;
        }
        .style218
        {
            color: #003399;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: left;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            width: 6px;
        }
        .style221
        {
            color: #244062;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            width: 3px;
        }
        .style229
        {
            width: 3px;
        }
        .style230
        {
            color: #244062;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            width: 6px;
        }
        .style242
        {
            font-weight: bold;
            color: White;
            font-size: 28.0pt;
            font-style: normal;
            font-family: Arial Black;
            text-align: center;
            padding: 0px;
            height: 7px;
        }
        .style243
        {
            font-weight: bolder;
            color: White;
            font-size: 77.0pt;
            font-style: normal;
            font-family: Arial Black;
            text-align: center;
            padding: 0px;
            height: 66px;
        }
        .style246
        {
            color: white;
            font-size: 16.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            height: 8px;
        }
        .style249
        {
            background-color: Yellow;
            height: 335px;
            width: 4px;
        }
        .style251
        {
            width: 417px;
            height: 152px;
        }
        .style252
        {
            width: 8px;
        }
        .style253
        {
            color: white;
            font-size: 16.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            height: 19px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <div>
        <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="12000">
        </asp:Timer>
    </div>
    <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
            <table style="height: 490px; width: 981px">
                <tr>
                    <td class="style62">
                        <table style="width: 981px; height: 164px;">
                            <tr>
                                <td class="style242">
                                    BALIZA No.&nbsp;
                                    <asp:Label ID="lblNumBal" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style243">
                                    OF
                                    <asp:Label ID="lblOrden" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="height: 340px; width: 728pt; margin-top: 12px;">
                            <tr style="text-align: center">
                                <td class="style251">
                                    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                                        height: 337px;" class="style197">
                                        <tr>
                                            <td class="style246" colspan="4">
                                                <strong>Estiba No.
                                                    <asp:Label ID="lblEstibaV" runat="server"></asp:Label>
                                                </strong>
                                            </td>
                                        </tr>
                                        <tr height="21">
                                            <td class="style21" colspan="4">
                                                <strong>PARA VALIDACION</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style253" colspan="4">
                                                <strong>Piezas</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style181" colspan="4">
                                                <asp:GridView ID="grdV" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="3" GridLines="Vertical" Height="37px" HorizontalAlign="Right"
                                                    Width="464px" Font-Size="9px" Font-Names="Arial" Style="margin-top: 0px; text-align: left;">
                                                    <AlternatingRowStyle BackColor="white" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Cons.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ofa" HeaderText="No.Sol" ItemStyle-Width="110pt">
                                                            <ItemStyle Width="350pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="grupo" HeaderText="Grupo" />
                                                        <asp:BoundField DataField="item" HeaderText="Item" />
                                                        <asp:BoundField DataField="pno" HeaderText="PNo." ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="65pt">
                                                            <ItemStyle HorizontalAlign="Center" Width="80pt"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="pieza" HeaderText="Pieza" ItemStyle-Width="460pt">
                                                            <ItemStyle Width="240pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="medidas" HeaderText="Medidas" />
                                                        <asp:BoundField DataField="area" HeaderText="Area(Mt2)" ItemStyle-Width="400pt" ItemStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" Width="400pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Peso" ItemStyle-Width="120pt" ItemStyle-HorizontalAlign="Right"
                                                            DataField="peso">
                                                            <ItemStyle HorizontalAlign="Right" Width="120pt" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
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
                                        <tr height="21">
                                            <td class="style200">
                                            </td>
                                            <td class="style201">
                                            </td>
                                            <td class="style218">
                                            </td>
                                            <td class="style184">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style213" colspan="5">
                                                Datos de la estiba:<br />
                                            </td>
                                        </tr>
                                        <tr height="21">
                                            <td class="style213">
                                                Cantidad de piezas =<b>
                                                    <asp:Label ID="lblCantV" runat="server"></asp:Label>
                                                </b>&nbsp;&nbsp;
                                            </td>
                                            <td class="style169">
                                            </td>
                                            <td class="style215">
                                            </td>
                                            <td class="style213">
                                                <asp:Label ID="Label1" runat="server" Text="Peso de estiba(Kg)" Style="font-weight: 700"></asp:Label>
                                                &nbsp;=
                                                <asp:Label ID="lblPesoV" runat="server" Style="font-weight: 700"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr height="21">
                                            <td class="style204">
                                            </td>
                                            <td class="style221">
                                            </td>
                                            <td class="style230">
                                            </td>
                                            <td class="style198">
                                            </td>
                                        </tr>
                                        <tr height="21">
                                            <td class="style19" colspan="4">
                                                Piezas por ubicar =
                                                <asp:Label ID="lblCantFV" runat="server"></asp:Label>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr height="21">
                                            <td class="style212">
                                            </td>
                                            <td class="style229">
                                            </td>
                                            <td class="style210">
                                                <span style="mso-spacerun: yes">&nbsp;&nbsp;</span>
                                            </td>
                                            <td class="style217">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="style252">
                                    <table>
                                        <tr>
                                            <td class="style249">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="style251">
                                    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                                        height: 336px;" class="style197">
                                        <tr>
                                            <td class="style246" colspan="4">
                                                <strong>Estiba No.
                                                    <asp:Label ID="lblEstibaNV" runat="server"></asp:Label>
                                                </strong>
                                            </td>
                                        </tr>
                                        <tr height="21">
                                            <td class="style23" colspan="4">
                                                <strong>NO VALIDACION</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style22" colspan="4">
                                                <strong>Piezas</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style181" colspan="4">
                                                <asp:GridView ID="grdNoV" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="3" GridLines="Vertical" Height="37px" HorizontalAlign="Right"
                                                    Width="464px" Font-Size="9px" Font-Names="Arial" Style="margin-top: 0px; text-align: left;">
                                                    <AlternatingRowStyle BackColor="white" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Cons.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ofa" HeaderText="No.Sol" ItemStyle-Width="110pt">
                                                            <ItemStyle Width="350pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="grupo" HeaderText="Grupo" />
                                                        <asp:BoundField DataField="item" HeaderText="Item" />
                                                        <asp:BoundField DataField="pno" HeaderText="PNo." ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="65pt">
                                                            <ItemStyle HorizontalAlign="Center" Width="80pt"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="pieza" HeaderText="Pieza" ItemStyle-Width="460pt">
                                                            <ItemStyle Width="240pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="medidas" HeaderText="Medidas" />
                                                        <asp:BoundField DataField="area" HeaderText="Area(Mt2)" ItemStyle-Width="400pt" ItemStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" Width="400pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Peso" ItemStyle-Width="120pt" ItemStyle-HorizontalAlign="Right"
                                                            DataField="peso">
                                                            <ItemStyle HorizontalAlign="Right" Width="120pt" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
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
                                        <tr height="21">
                                            <td class="style200">
                                            </td>
                                            <td class="style201">
                                            </td>
                                            <td class="style218">
                                            </td>
                                            <td class="style184">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style213" colspan="5">
                                                Datos de la estiba:<br />
                                            </td>
                                        </tr>
                                        <tr height="21">
                                            <td class="style213">
                                                Cantidad de piezas =<b>
                                                    <asp:Label ID="lblCantNV" runat="server"></asp:Label>
                                                </b>&nbsp;&nbsp;
                                            </td>
                                            <td class="style169">
                                            </td>
                                            <td class="style215">
                                            </td>
                                            <td class="style213">
                                                <asp:Label ID="Label5" runat="server" Text="Peso de estiba(Kg)" Style="font-weight: 700"></asp:Label>
                                                &nbsp;=
                                                <asp:Label ID="lblPesoNV" runat="server" Style="font-weight: 700"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr height="21">
                                            <td class="style204">
                                            </td>
                                            <td class="style221">
                                            </td>
                                            <td class="style230">
                                            </td>
                                            <td class="style198">
                                            </td>
                                        </tr>
                                        <tr height="21">
                                            <td class="style19" colspan="4">
                                                Piezas por ubicar =
                                                <asp:Label ID="lblCantFNV" runat="server"></asp:Label>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr height="21">
                                            <td class="style212">
                                                <asp:Label ID="lblBorrar2" runat="server" style="color: #66FF66"></asp:Label>
                                            </td>
                                            <td class="style229">
                                            </td>
                                            <td class="style210">
                                                <span style="mso-spacerun: yes">&nbsp;&nbsp;</span>
                                            </td>
                                            <td class="style217">
                                                &nbsp;
                                                <asp:Label ID="lblBorrar" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                            width: 734pt; height: 40px;">
                            <tr>
                                <td class="style89" colspan="15">
                                    Faltan
                                    <asp:Label ID="lblTotalF" runat="server"></asp:Label>
                                    &nbsp;Piezas
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>