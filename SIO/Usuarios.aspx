<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="Usuarios.aspx.cs" Inherits="SIO.Usuarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function listaUsuarios(source, eventArgs) {
            document.getElementById('<%= lblIdUsu.ClientID %>').value = eventArgs.get_value();
        };
        function ValEmail() {
            var valor = document.getElementById('<%= txtCorreo.ClientID %>').value;
            if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(valor)) {
            } else {
                alert("La dirección del correo es incorrecta!!");
                document.getElementById('<%= txtCorreo.ClientID %>').value = "";
            }
        }
    </script>
    <style type="text/css">
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
        .stylebtnAggN
        {
            width: 30px;
            height: 22px;
            text-align: center;
            font-weight: bold;
        }
        .stylebtnAggTodos
        {
            background-image: url(iconosMetro/aggTodos1.png);
            background-repeat: no-repeat;
            width: 30px;
            height: 28px;
        }
        .stylebtnEliTodos
        {
            background-image: url(iconosMetro/quitarTodos1.png);
            background-repeat: no-repeat;
            width: 30px;
            height: 28px;
        }
        .stylebtnAgg
        {
            background-image: url(iconosMetro/next1.png);
            background-repeat: no-repeat;
            width: 30px;
            height: 28px;
        }
        .stylebtnEli
        {
            background-image: url(iconosMetro/quitar1.png);
            background-repeat: no-repeat;
            width: 30px;
            height: 28px;
        }
        .styleLabes
        {
            text-align: right;
            width: 121px;
            font-size: 8pt;
        }
        .sytelTexbox
        {
            width: 105px;
            font-size: 8pt;
            height: 10px;
        }
        .sytelCombo
        {
            font-size: 8pt;
        }
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
        .accordionHeaderSelectedNada
        {
            padding: 0px;
            margin-top: 0px;
            cursor: pointer;
            cursor: pointer;
            z-index: 2;
            font-weight: bold;
            text-decoration: none;
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
        .accordionContentNada
        {
            border: 0pxF;
            border-top: none;
            padding: 0px;
            padding-top: 0px;
        }
        .accordionHeaderNada
        {
            padding: 0px;
            border-left: 0px;
            border-right: 0px;
            border-top: 0px;
            cursor: pointer;
        }
        
        .accordionContent
        {
            border: 0px outset #2F4F4F;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
        }
        .style167
        {
            width: 139px;
        }
        .style176
        {
            text-align: right;
            width: 71px;
            font-size: 8pt;
            height: 2px;
        }
        .style177
        {
            width: 103px;
        }
        .style179
        {
            width: 30px;
        }
        .style187
        {
            width: 67px;
            height: 2px;
        }
        .style195
        {
            width: 188px;
        }
        .style196
        {
            width: 67px;
            height: 25px;
        }
        .style197
        {
            width: 67px;
        }
        .style208
        {
            height: 25px;
            width: 102px;
        }
        .style209
        {
            width: 559px;
        }
        .style214
        {
            text-align: right;
            width: 74px;
            font-size: 8pt;
            height: 2px;
        }
        .style215
        {
            width: 74px;
        }
        .style217
        {
            text-align: right;
            font-size: 8pt;
            height: 2px;
            width: 188px;
        }
        .style218
        {
            width: 35px;
        }
        .style223
        {
            width: 102px;
        }
        .style224
        {
            text-align: right;
            font-size: 8pt;
        }
        .style227
        {
            width: 12px;
        }
        .style229
        {
            width: 61px;
        }
        .style230
        {
            text-align: right;
            font-size: 8pt;
            height: 25px;
            width: 61px;
        }
        .style231
        {
            width: 112px;
        }
        .style235
        {
            width: 107px;
        }
        .style236
        {
            text-align: right;
            width: 78px;
            font-size: 8pt;
            height: 2px;
        }
        .style237
        {
            width: 78px;
        }
        .style240
        {
            text-align: right;
            width: 67px;
            font-size: 8pt;
            height: 2px;
        }
        .style241
        {
            text-align: right;
            width: 68px;
            font-size: 8pt;
            height: 2px;
        }
        .style242
        {
            text-align: right;
            font-size: 8pt;
            height: 2px;
            width: 59px;
        }
        .style243
        {
            width: 93px;
        }
        .style244
        {
            width: 123px;
        }
        .style245
        {
            text-align: right;
            font-size: 8pt;
            height: 25px;
            width: 101px;
        }
        .style247
        {
            text-align: right;
            font-size: 8pt;
            height: 25px;
            width: 114px;
        }
        .style248
        {
            text-align: right;
            font-size: 8pt;
            width: 134px;
        }
        .style249
        {
            width: 157px;
        }
        .style250
        {
            text-align: right;
            font-size: 8pt;
            width: 109px;
        }
        .style251
        {
            text-align: right;
            font-size: 8pt;
            width: 199px;
        }
        .style253
        {
            text-align: right;
            font-size: 8pt;
            width: 201px;
        }
        .style254
        {
            text-align: right;
            font-size: 8pt;
            width: 200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:Panel ID="PanelUsu" runat="server" Font-Names="Arial" Height="690px" ScrollBars="Auto"
        Width="818px" GroupingText="Creacion de Usuarios" Style="margin-right: 26px">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table style="width: 775px">
                    <tr>
                        <td class="style209">
                            <table style="height: 38px; width: 762px">
                                <tr>
                                    <td class="style230">
                                        Rol:
                                    </td>
                                    <td class="style187">
                                        <asp:DropDownList ID="cboRoles" AutoPostBack="true" runat="server" Width="148px"
                                            CssClass="sytelCombo">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style217">
                                        <asp:CheckBox ID="chkEmpleadoF" runat="server" AutoPostBack="true" OnCheckedChanged="chkEmpleadoF_CheckedChanged"
                                            Text="Empleado Forsa?: " TextAlign="Left" />
                                    </td>
                                    <td class="style214">
                                        Cedula:
                                    </td>
                                    <td class="style223">
                                        <asp:TextBox ID="txtCedula" runat="server" CssClass="sytelTexbox" AutoPostBack="true"
                                            OnTextChanged="txtCedula_TextChanged"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="ftNoLetraCedula" runat="server" FilterType="Numbers"
                                            TargetControlID="txtCedula">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                    <td class="style218" colspan="2">
                                        <asp:Label ID="lblNomEmp" runat="server" Text="" Font-Size="8pt" Font-Names="arial"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style230">
                                        Login:
                                    </td>
                                    <td class="style196">
                                        <asp:TextBox ID="txtLoginUsu" runat="server" CssClass="sytelTexbox" OnTextChanged="txtLoginUsu_TextChanged"
                                            AutoCompleteType="None" AutoPostBack="true"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txtLoginUsu_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                            CompletionSetCount="15" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                            ServiceMethod="GetListaUsuarios" UseContextKey="true" ServicePath="~/WSSIO.asmx"
                                            TargetControlID="txtLoginUsu" OnClientItemSelected="listaUsuarios">
                                        </asp:AutoCompleteExtender>
                                        <input id="lblIdUsu" runat="server" type="hidden" />
                                    </td>
                                    <td class="style217" rowspan="2">
                                        <asp:CheckBox ID="chkActivo" Text="Activo?: " runat="server" TextAlign="Left" OnCheckedChanged="chkActivo_CheckedChanged"
                                            AutoPostBack="true" />
                                    </td>
                                    <td class="style214" rowspan="2">
                                        Planta:
                                    </td>
                                    <td class="style208" rowspan="2">
                                        <asp:DropDownList ID="cboPlanta" runat="server" CssClass="sytelCombo" AutoPostBack="true"
                                            Width="107px">
                                        </asp:DropDownList>
                                    </td>
                                    <td rowspan="2" class="style218" style="text-align: right">
                                        <asp:Button ID="btnAgrPlanta" runat="server" Text=">" CssClass="stylebtnAggN" OnClick="btnAgrPlanta_Click" />
                                        <asp:Button ID="btnEliPlanta" runat="server" Text="<" CssClass="stylebtnAggN" OnClick="btnEliPlanta_Click" />
                                    </td>
                                    <td class="style218" rowspan="2">
                                        <asp:ListBox ID="listaPlantaAgg" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Height="50px" SelectionMode="Multiple" Width="155px"></asp:ListBox>
                                    </td>
                                    <tr>
                                        <td class="style230">
                                            Password:
                                        </td>
                                        <td class="style197"> 
                                            <asp:TextBox ID="txtPassUsu" runat="server" TextMode="Password" CssClass="sytelTexbox"
                                                AutoCompleteType="None" AutoPostBack="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style229">
                                        </td>
                                        <td class="style197">
                                        </td>
                                        <td class="style195">
                                        </td>
                                        <td class="style215">
                                        </td>
                                        <td>
                                        </td>
                                        <td style="text-align: right;" colspan="2">
                                            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" BackColor="#1C5AB6" BorderColor="#999999"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" Height="20px"
                                                Width="67px" Style="text-align: center" OnClick="btnNuevo_Click" />
                                            <asp:Button ID="btnGuardarUsu" runat="server" Text="Guardar" BackColor="#1C5AB6"
                                                BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                                                Height="20px" Width="67px" Style="text-align: center" OnClick="btnGuardarUsu_Click" />
                                        </td>
                                    </tr>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%--<tr>
                                    <td class="style209">
                                        <table style="width: 225px">
                                            <tr>
                                                <td class="style160">
                                                    <asp:CheckBox ID="chkUsuCol" Text="Colombia: " runat="server" TextAlign="Left" />
                                                </td>
                                                <td class="style135">
                                                    <asp:CheckBox ID="chkUsuMex" Text="Mexico: " runat="server" TextAlign="Left" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style160">
                                                    <asp:CheckBox ID="chkUsuUru" Text="Uruguay: " runat="server" TextAlign="Left" />
                                                </td>
                                                <td class="style135">
                                                    <asp:CheckBox ID="chkUsuBra" Text="Brazil: " runat="server" TextAlign="Left" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style160">
                                                    <asp:CheckBox ID="chkAprobarT" Text="Apro. Tablero: " runat="server" TextAlign="Left" />
                                                </td>
                                                <td class="style135">
                                                    <asp:CheckBox ID="chkUsuCasino" Text="Casino: " runat="server" TextAlign="Left" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style160">
                                                    <asp:CheckBox ID="chkUsuGasto" Text="Gasto: " runat="server" TextAlign="Left" />
                                                </td>
                                                <td class="style135">
                                                    <asp:CheckBox ID="chkUsuCierre" Text="Cierre: " runat="server" TextAlign="Left" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>--%>
                </table>
                <asp:Accordion ID="Accordion" runat="server" ContentCssClass="accordionContent" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" Width="780px" Height="7997px"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="margin-right: 0px">
                    <Panes>
                        <asp:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeaderNada"
                            ContentCssClass="accordionContentNada" Style="width: 0px; height: 0px; background: transparent;
                            border: 0;">
                        </asp:AccordionPane>
                        <asp:AccordionPane ID="accorComercial" runat="server" ContentCssClass="accordionContent"
                            Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected">
                            <Header>
                                <asp:Label ID="lblEncabGeneral" runat="server" Text="COMERCIAL"></asp:Label>
                            </Header>
                            <Content>
                                <table style="width: 760px">
                                    <tr>
                                        <td class="style230">
                                            Nombre:
                                        </td>
                                        <td class="style235">
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="sytelTexbox"></asp:TextBox>
                                        </td>
                                        <td class="style236">
                                            Correo:
                                        </td>
                                        <td class="style231">
                                            <asp:TextBox ID="txtCorreo" runat="server" CssClass="sytelTexbox" OnTextChanged="txtCorreo_TextChanged"
                                                AutoPostBack="true"></asp:TextBox>
                                            <%-- <asp:RegularExpressionValidator ID="validateEmail"  runat="server" ErrorMessage="Invalid email." ControlToValidate="txtCorreo" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />--%>
                                        </td>
                                        <td class="style240">
                                            Celular:
                                        </td>
                                        <td class="style223">
                                            <asp:TextBox ID="txtCelular" runat="server" CssClass="sytelTexbox"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="ftNoLetraCelular" runat="server" FilterType="Numbers"
                                                TargetControlID="txtCelular">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                        <td class="style241">
                                            Telefono:
                                        </td>
                                        <td class="style227">
                                            <asp:TextBox ID="txtTel" runat="server" CssClass="sytelTexbox"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="ftNoLetraTel" runat="server" FilterType="Numbers"
                                                TargetControlID="txtTel">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style230">
                                            Oficina:
                                        </td>
                                        <td class="style235">
                                            <asp:TextBox ID="txtOficina" runat="server" CssClass="sytelTexbox"></asp:TextBox>
                                        </td>
                                        <td class="style236">
                                            Pais Oficina:
                                        </td>
                                        <td class="style231">
                                            <asp:DropDownList ID="cboPaisOfi" runat="server" CssClass="sytelCombo" AutoPostBack="true"
                                                Width="109px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style240">
                                            Direccion:
                                        </td>
                                        <td class="style223">
                                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="sytelTexbox"></asp:TextBox>
                                        </td>
                                        <td colspan="2" class="style224">
                                            <asp:CheckBox ID="chkDirOfi" runat="server" Text="Director Oficina?: " TextAlign="Left" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style229">
                                        </td>
                                        <td class="style235">
                                        </td>
                                        <td class="style237">
                                        </td>
                                        <td class="style231">
                                        </td>
                                        <td class="style197">
                                        </td>
                                        <td class="style224">
                                            <asp:CheckBox ID="chkActivoRepre" runat="server" Text="Activo?: " TextAlign="Left" />
                                        </td>
                                        <td colspan="2" style="text-align: right">
                                            <asp:Button ID="btnGuardarComer" runat="server" Text="Guardar" BackColor="#1C5AB6"
                                                BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                                                Height="20px" Width="67px" Style="text-align: center" OnClick="btnGuardarComer_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <div id="dviLinea" runat="server" style="width: 758px; height: 10px; border-top: 1px solid black;">
                                </div>
                                <table style="height: 124px; width: 760px">
                                    <tr>
                                        <td class="style242">
                                            Zona Pais:
                                            <br />
                                            <br />
                                            Pais:
                                        </td>
                                        <td class="style243">
                                            <asp:DropDownList ID="cboZonaP" runat="server" AutoPostBack="true" CssClass="sytelCombo"
                                                Width="110px" OnSelectedIndexChanged="cboZonaP_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <br />
                                            <br />
                                            <asp:DropDownList ID="cboPais" runat="server" AutoPostBack="true" CssClass="sytelCombo"
                                                OnSelectedIndexChanged="cboPais_SelectedIndexChanged" Width="110px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style179">
                                            <asp:Button ID="btnAgrPais" runat="server" Text=">" CssClass="stylebtnAggN" OnClick="btnAgrPais_Click" />
                                            <br />
                                            <asp:Button ID="btnEliPais" runat="server" Text="<" CssClass="stylebtnAggN" OnClick="btnEliPais_Click" />
                                            <br />
                                            <asp:Button ID="btnAgrTodosPais" runat="server" Text=">>" CssClass="stylebtnAggN"
                                                OnClick="btnAgrTodosPais_Click" />
                                            <br />
                                            <asp:Button ID="btnEliTodosPais" runat="server" Text="<<" CssClass="stylebtnAggN"
                                                OnClick="btnEliTodosPais_Click" />
                                        </td>
                                        <td class="style244">
                                            <asp:ListBox ID="listaPaisAgg" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                Height="104px" SelectionMode="Multiple" Width="155px"></asp:ListBox>
                                        </td>
                                        <td class="style176">
                                            Zona Ciudad:
                                            <br />
                                            <br />
                                            Ciudad:
                                        </td>
                                        <td class="style177">
                                            <asp:DropDownList ID="cboZonaC" runat="server" AutoPostBack="true" CssClass="sytelCombo"
                                                Width="110px" OnSelectedIndexChanged="cboZonaC_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <br />
                                            <br />
                                            <asp:DropDownList ID="cboCiudad" runat="server" AutoPostBack="true" CssClass="sytelCombo"
                                                Width="110px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style179">
                                            <asp:Button ID="btnAgreCiu" runat="server" Text=">" CssClass="stylebtnAggN" OnClick="btnAgreCiu_Click" />
                                            <br />
                                            <asp:Button ID="btnEliCiu" runat="server" Text="<" CssClass="stylebtnAggN" OnClick="btnEliCiu_Click" />
                                            <br />
                                            <asp:Button ID="btnAgreTodosCiu" runat="server" Text=">>" CssClass="stylebtnAggN"
                                                OnClick="btnAgreTodosCiu_Click" />
                                            <br />
                                            <asp:Button ID="btnEliTodosCiu" runat="server" Text="<<" CssClass="stylebtnAggN"
                                                OnClick="btnEliTodosCiu_Click" />
                                        </td>
                                        <td class="style167">
                                            <asp:ListBox ID="listaCiudadAgg" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                Height="104px" SelectionMode="Multiple" Width="155px"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="9" style="text-align: right">
                                            <asp:Button ID="btnGuardarPC" runat="server" Text="Guardar" BackColor="#1C5AB6" BorderColor="#999999"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" Height="20px"
                                                Width="67px" Style="text-align: center" OnClick="btnGuardarPC_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </asp:AccordionPane>
                        <asp:AccordionPane ID="accorSiif" runat="server" ContentCssClass="accordionContent"
                            Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                            Enabled="false" HeaderSelectedCssClass="accordionHeaderSelected">
                            <Header>
                                <asp:Label ID="Label2" runat="server" Text="SIIF"></asp:Label>
                            </Header>
                            <Content>
                                <table style="width: 759px">
                                    <tr>
                                        <td class="style245">
                                            Proceso Perminso:
                                        </td>
                                        <td class="style249">
                                            <asp:DropDownList ID="cboProPer" runat="server" Width="156px" CssClass="sytelCombo"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style247">
                                            Aprueba Memo:
                                        </td>
                                        <td class="style248">
                                            <asp:CheckBox ID="chkFormaleta" runat="server" Text="Formaleta: " TextAlign="Left" />
                                        </td>
                                        <td class="style250">
                                            <asp:CheckBox ID="chkAccesorios" runat="server" Text="Accesorios: " TextAlign="Left" />
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Button ID="btnGuardarPlanta" runat="server" Text="Guardar" BackColor="#1C5AB6"
                                                BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                                                Height="20px" Width="67px" Style="text-align: center" OnClick="btnGuardarPlanta_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </asp:AccordionPane>
                        <asp:AccordionPane ID="accorCasino" runat="server" ContentCssClass="accordionContent"
                            Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected">
                            <Header>
                                <asp:Label ID="Label4" runat="server" Text="CASINO"></asp:Label>
                            </Header>
                            <Content>
                                <table style="width: 761px">
                                    <tr>
                                        <td class="style254">
                                            <asp:CheckBox ID="chkAdmiCas" runat="server" Text="Administrador: " TextAlign="Left" />
                                        </td>
                                        <td class="style254">
                                            <asp:CheckBox ID="chkPedArea" runat="server" Text="Pedido Area: " TextAlign="Left" />
                                        </td>
                                        <td class="style254">
                                            <asp:CheckBox ID="chkArchivoP" runat="server" Text="Archivo Plano: " TextAlign="Left" />
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Button ID="btnGuardarCas" runat="server" Text="Guardar" BackColor="#1C5AB6"
                                                BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                                                Height="20px" Width="67px" Style="text-align: center" OnClick="btnGuardarCas_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </asp:AccordionPane>
                    </Panes>
                </asp:Accordion>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="width: 780px; height: 17px; border-top: 1px solid black;">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <div style="overflow: auto; width: 770px; height: 378px">
                                <asp:GridView ID="grdUsuarios" runat="server" BackColor="White" BorderColor="#999999"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="750px"
                                    AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px">
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                    <Columns>
                                        <asp:BoundField DataField="usuId" HeaderText="No." HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                        <asp:BoundField DataField="nomUsu" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="login" HeaderText="Login" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="rol" HeaderText="Rol" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="cedula" HeaderText="Cedula" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="activo" HeaderText="Activo" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="empleadoF" HeaderText="Empleado Forsa" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <%--   <asp:BoundField DataField="usuCol" HeaderText="Us. Colombiano" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="usuMex" HeaderText="Us. Mexico" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="usuUru" HeaderText="Us. Uruguay" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="usuBra" HeaderText="Us. Brazil" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="usuSuperV" HeaderText="Us. Supervisor" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="usuAprobadorT" HeaderText="Us. Apr.Tablero" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="usuCasino" HeaderText="Us. de Casino" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="usuCalidad" HeaderText="Us. de Calidad" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="usuCierre" HeaderText="Us. de Cierre" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="usuGasto" HeaderText="Us. de Gasto" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />--%>
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
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="overlay" />
                <div class="overlayContent">
                    <asp:Label ID="lblEnviando" runat="server" Text="Enviando..." Font-Names="Arial"
                        Font-Size="14pt"></asp:Label>
                    <img src="Imagenes/ajax-loader.gif" alt="Loading" height="30" style="text-align: center"
                        width="30" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
</asp:Content>