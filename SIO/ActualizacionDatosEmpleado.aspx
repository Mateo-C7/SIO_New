<%@ Page Title="" Language="C#" MasterPageFile="~/ReporteEmpleado.Master" AutoEventWireup="true" CodeBehind="ActualizacionDatosEmpleado.aspx.cs" Inherits="SIO.ActualizacionDatosEmpleado" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
      <script type="text/javascript">

            function confirmarActulizacionEmpleado() {

                if (confirm("¿Desea actualizar los datos del empleado?")) {

                    return true;
                }
                else {
                    return false;
                }
            }      
    </script>
     <script type="text/javascript">

            function confirmarActulizacionRegistro() {

                if (confirm("¿Desea actualizar este registro?")) {

                    return true;
                }
                else {
                    return false;
                }
            }      
    </script>
     <script type="text/javascript">

            function confirmarEliminacion() {

                if (confirm("¿Desea Eliminar Este Registro?")) {

                    return true;
                }
                else {
                    return false;
                }
            }      
    </script>

    <script type="text/javascript" src="Scripts/jsMaetsroItemPlanta.js"></script>


    <style type="text/css">
        .auto-style2 {
            margin-top: 0px;
        }
        .auto-style3 {
            margin-left: 18px;
        }
        .auto-style6 {
            width: 160px;
        }

         .accordionHeader {
            border: 2px Outset #EBEBEB;
            color: white;
            background-color: #1C5AB6;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
        }

        #master_content .accordionHeader a {
            color: #FFFFFF;
            background: none;
            text-decoration: none;
        }

            #master_content .accordionHeader a:hover {
                background: none;
                text-decoration: underline;
            }

        .accordionHeaderSelected {
            border: 2px Outset #EBEBEB;
            color: white;
            background-color: #1C5AB6;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
        }

        #master_content .accordionHeaderSelected a {
            color: #FFFFFF;
            background: none;
            text-decoration: none;
        }

            #master_content .accordionHeaderSelected a:hover {
                background: none;
                text-decoration: underline;
            }

        .accordionContent {
            border: 0px outset #2F4F4F;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
        }
        .auto-style8 {
            width: 934px;
            height: 15px;
        }
        .auto-style9 {
            height: 26px;
        }
        .auto-style10 {
            width: 153px;
            height: 61px;
        }
        .auto-style11 {
            width: 78px;
            height: 61px;
        }
        .auto-style12 {
            width: 71px;
            height: 61px;
        }
        .auto-style13 {
            width: 144px;
            height: 61px;
        }
        .auto-style14 {
            width: 201px;
            height: 61px;
        }
        .auto-style15 {
            width: 308px;
        }
        .auto-style17 {
            width: 293px;
        }
        .auto-style18 {
            width: 326px;
        }
        .auto-style21 {
            width: 1105px;
        }
        .auto-style24 {
            height: 61px;
        }

        .fondoazul
        { 
              border-left: 1px solid #1C5AB6;
              border-right: 1px solid #1C5AB6;
              border-top: 1px solid #1C5AB6;
              cursor: pointer;
              z-index: 2;
              font-size: 12px;
              font-weight: bold;
              text-decoration: none;
              color: #1c5ab6;
              text-shadow: 0 1px 1px rgba(0, 0, 0, 0.35);
              background: #1C5AB6;
              background: -webkit-linear-gradient(#1c5ab6, #1fa0e4);
              background: -moz-linear-gradient(#1c5ab6, #1fa0e4);
              background: -o-linear-gradient(#1c5ab6, #1fa0e4);
              background: -ms-linear-gradient(#1c5ab6, #1fa0e4);
}
        .auto-style31 {
            height: 26px;
            width: 128px;
        }
        .auto-style33 {
            margin-bottom: 0px;
        }
        .auto-style34 {
            height: 36px;
            width: 80px;
        }
        .auto-style36 {
            width: 160px;
            height: 6px;
        }
        .auto-style37 {
            width: 293px;
            height: 6px;
        }
        .auto-style38 {
            width: 308px;
            height: 6px;
        }
        .auto-style39 {
            width: 326px;
            height: 6px;
        }
        .auto-style40 {
            height: 16px;
            width: 101px;
        }
        .auto-style41 {
            height: 36px;
            width: 128px;
        }
        .auto-style42 {
            width: 19px;
        }
        .auto-style43 {
            height: 26px;
            width: 80px;
        }
        </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
       <table id="TblDatosPers" class="fondoazul" width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Text="ACTUALIZACIÓN INFORMACIÓN EMPLEADO"></asp:Label>
            </td>
        </tr>
    </table>

    <asp:UpdatePanel UpdateMode="Conditional" ID="updpnlDatos" runat="server">
        <ContentTemplate>
           <asp:Panel runat="server" Width="100%" Height="100%">
                 <table class="auto-style8">
                    <tr>
                        <td style="text-align: right" class="auto-style24">
                            <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Text="Tipo Documento:" Width="100px"></asp:Label>
                        </td>
                        <td class="auto-style10">
                            <asp:DropDownList ID="cboCedula" runat="server" AutoPostBack="false"
                                Width="200px">
                                <asp:ListItem>CÉDULA DE CIUDADANÍA</asp:ListItem>
                                <asp:ListItem>CÉDULA DE EXTRANJERÍA</asp:ListItem>
                                <asp:ListItem>TARJETA DE IDENTIDAD</asp:ListItem>
                            </asp:DropDownList>
                        </td>                                    
                           <td style="text-align: right" class="auto-style11">
                            <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"  Text="Identificacion:" Width="70px"></asp:Label>
                        </td>
                        <td class="auto-style13">
                            <asp:TextBox ID="txt_Identificacion" MaxLength="15" AutoPostBack="true" runat="server" Width="120px" OnTextChanged="txt_Identificacion_TextChanged" TabIndex="1" ></asp:TextBox>
                        </td>
                        <td class="auto-style12">
                            <asp:Button ID="btn_BuscarEmple" runat="server" Text="Buscar" BackColor="#1C5AB6" ForeColor="White"
                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_BuscarEmple_Click1" TabIndex="2"/>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                           <td class="auto-style14">
                            <asp:Button ID="btn_CancelarBuscar" runat="server" Text="Cancelar" BackColor="#1C5AB6" ForeColor="White"
                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_CancelarBuscar_Click"/>
                        </td>
                        <td class="auto-style24"><asp:Image ID="FotoEmple" runat="server" Width="50px"/></td>
                     </tr>
                         </table>
           </asp:Panel>
              
            <asp:Label ID="lbl_MsgExisteEmple" runat="server" ForeColor="Red" ></asp:Label>
            <asp:Label ID="lblUsuario" runat="server" Text="." ></asp:Label>
                <asp:Label ID="lblnombreUsu" runat="server" Text="." ></asp:Label>
            <asp:Label ID="lblobtenercargo" runat="server" Text="." ></asp:Label>
            <asp:Label ID="lblcargoemc_id" runat="server" Text="." ></asp:Label>
              <asp:Panel ID="pnlContent" runat="server"
               Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                Width="100%" Height="100%" CssClass="auto-style2">
                  <asp:Panel runat="server" ID="PnlDatosGeneral" GroupingText="DATOS GENERALES"
                       Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                Width="100%" Height="100%" CssClass="auto-style2">
 <table>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nombres:" Width="50px"></asp:Label>
                        </td>
                        <td style="text-align: left" class="auto-style17">
                            <asp:TextBox ID="txt_Nombres" MaxLength="300" AutoPostBack="false" runat="server" Width="205px" CssClass="auto-style3"> </asp:TextBox>                      
                        </td>
                         <td style="text-align: right">
                            <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Apellidos:" Width="50px"></asp:Label>
                        </td>
                        <td class="auto-style15">
                            <asp:TextBox ID="txt_Apellidos" MaxLength="300" AutoPostBack="false" runat="server" Width="200px" CssClass="auto-style3"> </asp:TextBox>                      
                        </td>                                     
                    </tr>
                    <tr>
                        <td style="text-align: right" class="auto-style36">
                            <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Direccion De Residencia:" Width="120px"></asp:Label>
                        </td>
                        <td class="auto-style37">
                            <asp:TextBox ID="txt_DireccResidencia" MaxLength="300" AutoPostBack="false" runat="server" Width="267px" CssClass="auto-style3"></asp:TextBox>
                        </td>
                         <td style="text-align: right" class="auto-style36">
                            <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Barrio:" Width="120px"></asp:Label>
                        </td>
                        <td class="auto-style38">
                            <asp:TextBox ID="txt_Barrio" MaxLength="300" AutoPostBack="false" runat="server" Width="200px" CssClass="auto-style3"> </asp:TextBox>
                        </td>
                           <td style="text-align: right" class="auto-style39">
                            <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Telefono Fijo:" Width="120px"></asp:Label>
                        </td>
                        <td class="auto-style37">
                            <asp:TextBox ID="txt_TeleFijo" MaxLength="15" AutoPostBack="false" runat="server" Width="100px" CssClass="auto-style3"> </asp:TextBox>
                        </td>                                                                           
                    </tr>
                      <tr>
                          <td style="text-align: right">
                              <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Ciudad/Vereda:" Width="90px"></asp:Label>
                          </td>
                          <td class="auto-style17">
                              <asp:DropDownList ID="cboCiudadVereda" runat="server" Width="205px" AutoPostBack="false" CssClass="auto-style3"></asp:DropDownList>
                          </td>
                          <td style="text-align: right" class="auto-style6">
                              <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Celular:" Width="120px"></asp:Label>
                          </td>
                          <td class="auto-style15">
                              <asp:TextBox  ID="txt_Celular" MaxLength="15" AutoPostBack="false" runat="server" Width="100px" CssClass="auto-style3"> </asp:TextBox>
                          </td>
                          <td style="text-align: right" class="auto-style18">
                              <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo De Sangre:" Width="100px"></asp:Label>
                          </td>
                          <td>
                              <asp:DropDownList ID="cbo_TipoSangre" runat="server" AutoPostBack="false"
                                  Width="60px" CssClass="auto-style3">
                                  <asp:ListItem>A+</asp:ListItem>
                                  <asp:ListItem>A-</asp:ListItem>
                                  <asp:ListItem>B+</asp:ListItem>
                                  <asp:ListItem>B-</asp:ListItem>
                                  <asp:ListItem>O-</asp:ListItem>
                                  <asp:ListItem>O+</asp:ListItem>
                                  <asp:ListItem>AB+</asp:ListItem>
                                  <asp:ListItem>AB-</asp:ListItem>
                                  <asp:ListItem>N</asp:ListItem>
                                  <asp:ListItem></asp:ListItem>
                              </asp:DropDownList>
                          </td>
                      </tr>
                    <tr>
                        <td style="text-align: right" >
                            <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Nacimiento:" Width="120px"></asp:Label>
                        </td>
                        <td class="auto-style17">
                            <asp:TextBox ID="txt_FechNaci" MaxLength="10" AutoPostBack="false" runat="server" Width="100px" CssClass="auto-style3"> </asp:TextBox>
                                     <asp:CalendarExtender ID="calFechNacimiento" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txt_FechNaci"></asp:CalendarExtender>
                        </td>
                        <td style="text-align: right" >
                            <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cargo Actual:" Width="82px"></asp:Label>
                        </td>
                        <td class="auto-style15">
                            <asp:DropDownList ID="cbo_CargoActual" runat="server"  AutoPostBack="false" Width="225px" CssClass="auto-style3" OnSelectedIndexChanged="Txt_CargoActual1_SelectedIndexChanged" ></asp:DropDownList>
                        </td>               
                        <td style="text-align: right" class="auto-style18" >  
                            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;                       
                            <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Est Civil:" Width="58px"></asp:Label>
                        </td>

                        <td>
                            <asp:DropDownList ID="cbo_EstadoCivil" runat="server" AutoPostBack="false"
                                Width="115px" CssClass="auto-style3">
                                <asp:ListItem>SOLTERO(A)</asp:ListItem>
                                <asp:ListItem>CASADO(A)</asp:ListItem>
                                <asp:ListItem>DIVORCIADO(A)</asp:ListItem>
                                <asp:ListItem>UNIÓN LIBRE</asp:ListItem>
                                <asp:ListItem>VIUDO(A)</asp:ListItem>
                                <asp:ListItem>ESTADO CIVIL</asp:ListItem>
                                <asp:ListItem>N</asp:ListItem>
                                  <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr> 
                    <tr>
                         <td style="text-align: right">
                            <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Empresa Contratante:" Width="120px"></asp:Label>
                        </td>
                        <td class="auto-style17">
                            <asp:DropDownList ID="cbo_EmpresaContra" runat="server"  AutoPostBack="false" Width="225px" CssClass="auto-style3" ></asp:DropDownList>
                        </td>
                          <td style="text-align: right" >
                            <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Correo Electronico:" Width="101px"></asp:Label>
                        </td>
                        <td class="auto-style15">
                            <asp:TextBox ID="txt_Correo" MaxLength="200" AutoPostBack="false" runat="server" Width="200px" CssClass="auto-style3"></asp:TextBox>
                        </td>
                         <td style="text-align: right" class="auto-style18" >
                            <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="¿Tiene Carnet De La Empresa?:" Width="200px"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCarnetSi" Text="Si" runat="server" AutoPostBack="true" CssClass="auto-style3" OnCheckedChanged="chkCarnetSi_CheckedChanged" />
                            &nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox ID="chkCarnetNo" Text="No" runat="server" AutoPostBack="true" OnCheckedChanged="chkCarnetNo_CheckedChanged" />
                        </td>
                        <td> 
                            <asp:Label ID="LblCarnet" runat="server"></asp:Label>
                        </td>
                        <tr>
                        <td style="text-align: right" class="auto-style6">
                            <asp:Label ID="lbl_Estracto" Font-Names="Arial" runat="server" Text="Estrato:" Font-Size="8pt" ></asp:Label>
                            <td class="auto-style15">
                            <asp:TextBox ID="txt_Estracto" MaxLength="200" AutoPostBack="false" runat="server" Width="30px" CssClass="auto-style3" > </asp:TextBox>                      
                        </td>   

                        </td>
                         </tr>  

                        </table>
                  </asp:Panel>

                        <br />
                      <table class="fondoazul" width="100%" >
        <tr>
            <td align="center">
                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Text="INFORMACIÓN PERSONAL"></asp:Label>
            </td>
        </tr>
    </table>
                  <table>
                      <tr>
                          <td >
                              <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Vive En Casa:" Width="70px"></asp:Label>
                          </td>
                          <td>
                              <asp:CheckBox ID="chk_Familiar" Text="Familiar" runat="server" AutoPostBack="true" OnCheckedChanged="chk_Familiar_CheckedChanged" />
                              &nbsp&nbsp&nbsp&nbsp&nbsp
                              <asp:CheckBox ID="chk_Propia" Text="Propia" runat="server" AutoPostBack="true" OnCheckedChanged="chk_Propia_CheckedChanged" />
                              &nbsp&nbsp&nbsp&nbsp&nbsp
                              <asp:CheckBox ID="chk_Alquilada" Text="Alquilada" runat="server" AutoPostBack="true" OnCheckedChanged="chk_Alquilada_CheckedChanged" />
                              &nbsp&nbsp&nbsp&nbsp&nbsp
                              <asp:CheckBox ID="chk_Otro" Text="Otro" runat="server" AutoPostBack="true" OnCheckedChanged="chk_Otro_CheckedChanged" />
                              <asp:Label ID="lblTipoVivienda" runat="server"></asp:Label>
                          </td>
                      </tr>
                </table>
               <br />
                  <asp:panel ID="pnlPersConviven" runat="server"
               Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GroupingText="PERSONAS CON LAS QUE CONVIVE ACTUALMENTE"
                Width="100%" Height="100%" CssClass="auto-style2">
                      <table>
                      <tr>
                          <td style="text-align: right">
                            <asp:Label ID="lblnomape_persconvi" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nombres y Apellidos:" Width="106px"></asp:Label>
                        </td>
                        <td style="text-align: left" class="altoRengInicio">
                            <asp:TextBox ID="txt_NombApellConv" MaxLength="400" AutoPostBack="false" runat="server" Width="195px"></asp:TextBox>                      
                        </td>
                               <td style="text-align: right">
                            <asp:Label ID="lblparents_persconvi" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Parentesco:" Width="60px"></asp:Label>
                        </td>
                        <td style="text-align: left" class="altoRengInicio">
                            <asp:TextBox ID="txt_ParentescoConv" MaxLength="50" AutoPostBack="false" runat="server" Width="195px"></asp:TextBox>                      
                        </td>
                              <td style="text-align: right">
                            <asp:Label ID="lbledad_persconvi" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Edad:" Width="30px"></asp:Label>
                        </td>
                        <td style="text-align: left" class="altoRengInicio">
                            <asp:TextBox ID="txt_EdadConv" MaxLength="4" AutoPostBack="false" runat="server" Width="27px"></asp:TextBox>                      
                        </td> 
                      </tr>
                      <tr>
                          <td style="text-align: right">
                              <asp:Label ID="lblniveleduca_persconvi" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nivel Educativo:" Width="80px"></asp:Label>
                          </td>
                          <td style="text-align: left" class="altoRengInicio">
                              <asp:DropDownList ID="cbo_NivenEducaConv" runat="server" MaxLength="100" AutoPostBack="false" Width="195px">                                                                                           
                                   <asp:ListItem>ELEGIR UNA OPCION</asp:ListItem>
                                  <asp:ListItem>BASICA PRIMARIA</asp:ListItem>
                                  <asp:ListItem>BACHILLERATO</asp:ListItem>
                                  <asp:ListItem>TÉCNICO</asp:ListItem>
                                  <asp:ListItem>TECNOLÓGICO</asp:ListItem>
                                  <asp:ListItem>PROFESIONAL</asp:ListItem>
                                  <asp:ListItem>ESPECIALIZACIÓN</asp:ListItem>
                                  <asp:ListItem>MAESTRIA</asp:ListItem>
                                  <asp:ListItem>DOCTORADO</asp:ListItem>
                                  <asp:ListItem>NO APLICA</asp:ListItem>
                              </asp:DropDownList>
                          </td>
                            <td style="text-align: right">
                              <asp:Label ID="lblocupa_persconvi" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Ocupacion:" Width="80px"></asp:Label>
                          </td>
                          <td style="text-align: left" class="altoRengInicio">
                              <asp:TextBox ID="txt_OcupacionConv" MaxLength="200" AutoPostBack="false" runat="server" Width="195px"></asp:TextBox>
                          </td>                                                             
                      </tr>                                            
                  </table>

                        <table>
                      <tr>
                          <td>
                              <asp:Button ID="btn_AgregarPersConvi" runat="server" Text="Guardar" BackColor="#1C5AB6" ForeColor="White"
                                  BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_AgregarPersConvi_Click" />
                          </td>               
                          <td>
                              <asp:Button ID="btn_HabiliNuevaPersConvi" runat="server"  Text="Agregar" BackColor="#1C5AB6" ForeColor="White"
                                  BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_HabiliNuevaPersConvi_Click"/>
                          </td>
                              <td>
                              <asp:Button ID="btn_CancelaNuevaPersConvi" runat="server" Text="Cancelar" BackColor="#1C5AB6" ForeColor="White"
                                  BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_CancelaNuevaPersConvi_Click"/>
                          </td>
                             <td>
                              <asp:Label ID="lblMsgPersCOnvive" runat="server" ForeColor="Red"></asp:Label>
                          </td>
                      </tr>
                  </table>                   
                      <table>
                      <tr>
                          <td>
                              <asp:GridView ID="GridConvivePers" runat="server" BackColor="White" BorderColor="#999999"
                                  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="700px" AllowPaging="True"
                                  AutoGenerateColumns="False" Font-Size="8pt" DataKeyNames="covemp_id" Font-Names="arial" Height="16px" PageSize="7"
                                  OnRowEditing="GridConvivePers_RowEditing" OnRowCancelingEdit="GridConvivePers_RowCancelingEdit" OnPageIndexChanging="GridConvivePers_PageIndexChanging"
                                  OnRowUpdating="GridConvivePers_RowUpdating" OnRowDeleting="GridConvivePers_RowDeleting" ShowHeaderWhenEmpty="True">
                                  <Columns>
                                      <asp:TemplateField HeaderText="id" Visible="False">
                                          <EditItemTemplate>
                                              <asp:TextBox ID="txt_covemp_id" runat="server" Text='<%# Bind("covemp_id") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label6" runat="server" Text='<%# Bind("covemp_id") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Nombres y Apellidos" HeaderStyle-Width="200px" >
                                          <EditItemTemplate>
                                              <asp:TextBox ID="txtNombApel" Width="200px"  runat="server" MaxLength="400" Text='<%# Bind("covemp_Nomb_Apell") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("covemp_Nomb_Apell") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Parentesco" HeaderStyle-Width="100px">
                                          <EditItemTemplate>
                                              <asp:TextBox ID="txtParentesco" Width="100px" MaxLength="50" runat="server" Text='<%# Bind("covemp_Parentesco") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("covemp_Parentesco") %>'></asp:Label>
                                          </ItemTemplate>                                  
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Edad"  HeaderStyle-Width="40px">
                                          <EditItemTemplate>
                                              <asp:TextBox ID="txtEdad" ToolTip= "Para especificar la edad en meses, seguir este ejemplo:(0.1 a 0.11) meses" Width="40px" MaxLength="4" runat="server" Text='<%# Bind("covemp_Edad") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label3" runat="server" Text='<%# Bind("covemp_Edad") %>'></asp:Label>
                                          </ItemTemplate>
                                           <HeaderStyle Width="40px"></HeaderStyle>
                                             <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Nivel Educativo"  HeaderStyle-Width="110px">
                                          <EditItemTemplate>
                                              <asp:DropDownList ID="cboNivelEduca" runat="server" AppendDataBoundItems="false"  Width="110px" Text='<%# Bind("covemp_Nivel_Educativo") %>'>
                                                  <asp:ListItem>ELEGIR UNA OPCION</asp:ListItem>
                                                  <asp:ListItem>BASICA PRIMARIA</asp:ListItem>
                                                  <asp:ListItem>BACHILLERATO</asp:ListItem>
                                                  <asp:ListItem>TÉCNICO</asp:ListItem>
                                                  <asp:ListItem>TECNOLÓGICO</asp:ListItem>
                                                  <asp:ListItem>PROFESIONAL</asp:ListItem>
                                                  <asp:ListItem>ESPECIALIZACIÓN</asp:ListItem>
                                                  <asp:ListItem>MAESTRIA</asp:ListItem>
                                                  <asp:ListItem>DOCTORADO</asp:ListItem>
                                                  <asp:ListItem>NO APLICA</asp:ListItem>
                                              </asp:DropDownList>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label4" runat="server" Text='<%# Bind("covemp_Nivel_Educativo") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Ocupacion" HeaderStyle-Width="130px">
                                          <EditItemTemplate>
                                              <asp:TextBox ID="txtOcupacio" Width="130px" MaxLength="200" runat="server" Text='<%# Bind("covemp_Ocupacion") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label5" runat="server" Text='<%# Bind("covemp_Ocupacion") %>'></asp:Label>
                                          </ItemTemplate>                                      
                                      </asp:TemplateField>
                                      <asp:CommandField ShowEditButton="True"  
                                       HeaderStyle-Width="20px" >
                                      <HeaderStyle Width="20px" />
                                      </asp:CommandField>
                                      <asp:TemplateField ShowHeader="False">
                                          <ItemTemplate>
                                              <asp:LinkButton ID="btn_EliminarPersConvive" Text="Eliminar"  runat="server" CommandName="Delete" OnClientClick="return confirmarEliminacion();" ForeColor="Black"></asp:LinkButton>
                                          </ItemTemplate>
                                          <HeaderStyle Width="20px"></HeaderStyle>
                                      </asp:TemplateField>
                                    
                                  </Columns>
                                  <EditRowStyle HorizontalAlign="Right" />
                                  <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                  <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                  <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                  <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Left" />
                                  <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                  <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                  <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                  <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                  <SortedDescendingHeaderStyle BackColor="#000065" />
                              </asp:GridView>
                          </td>
                      </tr>
                  </table>
                  </asp:panel>            
                  <br />
                    <asp:panel ID="pnlHijoNoConvi" runat="server"
               Font-Names="Arial" Font-Size="8pt" ForeColor="Black"  GroupingText="HIJOS QUE NO VIVEN CON USTED"
                Width="100%" Height="100%" CssClass="auto-style2">
                      <table>
                      <tr>
                          <td>
                            <asp:Label ID="lblnomape_HiNoConvi" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nombres y Apellidos:" Width="106px"></asp:Label>
                        </td>
                        <td style="text-align: left" class="altoRengInicio">
                            <asp:TextBox ID="txt_NombApellHiNoConvi" MaxLength="400" AutoPostBack="false" runat="server" Width="196px"></asp:TextBox>                      
                        </td>                               
                            <td>
                              <asp:Label ID="lblniveleduca_HiNoConvi" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nivel Educativo:" Width="80px"></asp:Label>
                          </td>
                          <td style="text-align: left" class="altoRengInicio">
                              <asp:DropDownList ID="cbo_NivelEducHiNoConvi" runat="server" MaxLength="100" AutoPostBack="false" Width="195px">                                               
                                  <asp:ListItem>ELEGIR UNA OPCION</asp:ListItem>
                                  <asp:ListItem>BASICA PRIMARIA</asp:ListItem>
                                  <asp:ListItem>BACHILLERATO</asp:ListItem>
                                  <asp:ListItem>TÉCNICO</asp:ListItem>
                                  <asp:ListItem>TECNOLÓGICO</asp:ListItem>                  
                                  <asp:ListItem>PROFESIONAL</asp:ListItem>
                                    <asp:ListItem>ESPECIALIZACIÓN</asp:ListItem>
                                  <asp:ListItem>MAESTRIA</asp:ListItem>
                                  <asp:ListItem>DOCTORADO</asp:ListItem>
                                  <asp:ListItem>NO APLICA</asp:ListItem>
                              </asp:DropDownList>
                          </td>   
                            <td style="text-align: right">
                            <asp:Label id="lblEdad_HiNoConvi" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Edad:" Width="30px"></asp:Label>
                        </td>
                        <td style="text-align: left" class="altoRengInicio">
                            <asp:TextBox ID="txt_EdadHiNoConvi" MaxLength="4" AutoPostBack="false" runat="server" Width="27px"></asp:TextBox>                      
                        </td>                         
                      </tr>                                                                                                                                                                                                               
                  </table>
                  <table>
                      <tr>
                          <td class="auto-style9">
                              <asp:Button ID="btn_AgregarHijosNoConvi" runat="server" Text="Guardar" BackColor="#1C5AB6" ForeColor="White"
                                  BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_AgregarHijosNoConvi_Click"/>
                          </td>
                           <td>
                              <asp:Button ID="btn_HabiliAgregarHijosNoConvi" runat="server" Text="Agregar" BackColor="#1C5AB6" ForeColor="White"
                                  BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_HabiliAgregarHijosNoConvi_Click"/>
                          </td>
                              <td>
                              <asp:Button ID="btn_CancelarAgregarHijosNoConvi" runat="server" Text="Cancelar" BackColor="#1C5AB6" ForeColor="White"
                                  BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_CancelarAgregarHijosNoConvi_Click"/>
                          </td>
                          <td>
                              <asp:Label ID="lblMsgHijosNoConvi" runat="server" ForeColor="Red"></asp:Label>
                          </td>
                      </tr>
                  </table>                 
                  <table>
                      <tr>
                          <td>
                              <asp:GridView ID="GridNoConvivePers" runat="server" BackColor="White" BorderColor="#999999"
                                  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="520px" DataKeyNames="hjnocov_id" AllowPaging="True"
                                  AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px" PageSize="8" 
                                  OnRowEditing="GridNoConvivePers_RowEditing" OnRowCancelingEdit="GridNoConvivePers_RowCancelingEdit" 
                                  OnRowUpdating="GridNoConvivePers_RowUpdating" OnRowDeleting="GridNoConvivePers_RowDeleting" ShowHeaderWhenEmpty="True">
                                  <Columns>
                                      <asp:TemplateField HeaderText="hjnocov" Visible="False">
                                          <EditItemTemplate>
                                              <asp:TextBox ID="txt_hjnocov_id" runat="server" Text='<%# Bind("hjnocov_id") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label4" runat="server" Text='<%# Bind("hjnocov_id") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Nombres y Apellidos"  HeaderStyle-Width="200px">
                                          <EditItemTemplate>
                                              <asp:TextBox ID="txtNombApelNoConvi" Width="250px" runat="server" Text='<%# Bind("hjnocov_Nomb_Apell") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("hjnocov_Nomb_Apell") %>'></asp:Label>
                                          </ItemTemplate>
                                            <HeaderStyle Width="200px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="left" />
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Edad"  HeaderStyle-Width="40px"> 
                                          <EditItemTemplate>
                                              <asp:TextBox ID="txtEdadNoConvi" ToolTip= "Para especificar la edad en meses, seguir este ejemplo:(0.1 a 0.11) meses" Width="40PX" MaxLength="4" runat="server" Text='<%# Bind("hjnocov_Edad") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("hjnocov_Edad") %>'></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="40px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Nivel Educativo"  HeaderStyle-Width="110px">
                                          <EditItemTemplate>
                                              <asp:DropDownList ID="cboNIvelEducaNoConvi" AppendDataBoundItems="false" Width="150px" runat="server" Text='<%# Bind("hjnocov_Nivel_Educativo") %>'>
                               <asp:ListItem>ELEGIR UNA OPCION</asp:ListItem>
                                  <asp:ListItem>BASICA PRIMARIA</asp:ListItem>
                                  <asp:ListItem>BACHILLERATO</asp:ListItem>
                                  <asp:ListItem>TÉCNICO</asp:ListItem>
                                  <asp:ListItem>TECNOLÓGICO</asp:ListItem>
                                  <asp:ListItem>PROFESIONAL</asp:ListItem>
                                  <asp:ListItem>ESPECIALIZACIÓN</asp:ListItem>
                                  <asp:ListItem>MAESTRIA</asp:ListItem>
                                  <asp:ListItem>DOCTORADO</asp:ListItem>
                                  <asp:ListItem>NO APLICA</asp:ListItem>
                                              </asp:DropDownList>
                                          </EditItemTemplate>
                                          <HeaderStyle Width="110px" />
                                          <ItemStyle HorizontalAlign="left" />
                                          <ItemTemplate>
                                              <asp:Label ID="Label3" runat="server" Text='<%# Bind("hjnocov_Nivel_Educativo") %>'></asp:Label>
                                          </ItemTemplate>                                
                                      </asp:TemplateField>
                                      <asp:CommandField ShowEditButton="True"  HeaderStyle-Width="20px" >
                                      <HeaderStyle Width="20px" />
                                      </asp:CommandField>
                                      <asp:TemplateField ShowHeader="False">
                                          <ItemTemplate>
                                              <asp:LinkButton ID="btn_EliminarHijoNoConvi"  Text="Eliminar"  runat="server" CommandName="Delete" OnClientClick="return confirmarEliminacion();" ForeColor="Black"></asp:LinkButton>
                                          </ItemTemplate>
                                          <HeaderStyle Width="30px" />
                                      </asp:TemplateField>
                                  </Columns>
                                  <EditRowStyle HorizontalAlign="Right" />
                                  <EmptyDataRowStyle BorderStyle="Solid" />
                                  <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                  <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                  <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                  <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Left" />
                                  <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                  <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                  <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                  <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                  <SortedDescendingHeaderStyle BackColor="#000065" />
                              </asp:GridView>
                          </td>
                      </tr>
                  </table>
                  </asp:panel>              
                    <br />
                      <asp:Panel runat="server" ID="pnlEmergencia"
                      Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                      GroupingText="ESPECIFICAR DOS CONTACTOS EN CASO DE EMERGENCIA" Width="100%" Height="100%" CssClass="auto-style2">
                      <table>
                          <tr>
                              <td style="text-align: right">
                                  <asp:Label ID="lblnomape_Emergencia" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nombres y Apellidos:" Width="106px"></asp:Label>
                              </td>
                              <td style="text-align: left" class="altoRengInicio">
                                  <asp:TextBox ID="Txt_NombApelEmr" MaxLength="400" AutoPostBack="false" runat="server" Width="195px"></asp:TextBox>
                              </td>
                              <td style="text-align: right">
                                  <asp:Label ID="lblparent_Emergencia" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Parentesco:" Width="60px"></asp:Label>
                              </td>
                              <td style="text-align: left" class="altoRengInicio">
                                  <asp:TextBox ID="Txt_ParentescoEmr" MaxLength="50" AutoPostBack="false" runat="server" Width="195px"></asp:TextBox>
                              </td>
                          </tr>
                          <tr>
                              <td style="text-align: right">
                                  <asp:Label id="lbldirecci_Emergencia" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Direccion:" Width="80px"></asp:Label>
                              </td>
                              <td style="text-align: left" class="altoRengInicio">
                                  <asp:TextBox ID="Txt_UbicacionEmr" MaxLength="400" AutoPostBack="false" runat="server" Width="195px"></asp:TextBox>
                              </td>
                              <td style="text-align: right">
                                  <asp:Label ID="lbltelefo_Emergencia" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Telefono:" Width="80px"></asp:Label>
                              </td>
                              <td style="text-align: left" class="altoRengInicio">
                                  <asp:TextBox ID="Txt_TelefonoEmr" MaxLength="10" AutoPostBack="false" runat="server" Width="80px"></asp:TextBox>
                              </td>
                          </tr>
                      </table>

                        <table>
                      <tr>
                          <td>
                              <asp:Button ID="btn_AgregarPersEmergen" runat="server" Text="Guardar" BackColor="#1C5AB6" ForeColor="White"
                                  BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_AgregarPersEmergen_Click"/>
                          </td>
                           <td>
                              <asp:Button ID="btn_HabiliAgregarPersEmergen" runat="server" Text="Agregar" BackColor="#1C5AB6" ForeColor="White"
                                  BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_HabiliAgregarPersEmergen_Click"/>
                          </td>
                              <td>
                              <asp:Button ID="btn_CancelarAgregarPersEmergen" runat="server" Text="Cancelar" BackColor="#1C5AB6" ForeColor="White"
                                  BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_CancelarAgregarPersEmergen_Click"/>
                          </td>
                          <td>
                              <asp:Label ID="lblEmergencia" runat="server" ForeColor="Red"></asp:Label>
                          </td>
                      </tr>
                  </table>                      
                  <table>
                      <tr>
                          <td>
                              <asp:GridView ID="GridEmergencia" runat="server" BackColor="White" BorderColor="#999999"
                                  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="696px" DataKeyNames="peremr_id" AllowPaging="True"
                                  AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px" PageSize="2"
                                  OnRowEditing="GridEmergencia_RowEditing" OnRowCancelingEdit="GridEmergencia_RowCancelingEdit" 
                                  OnRowUpdating="GridEmergencia_RowUpdating" OnRowDeleting="GridEmergencia_RowDeleting" ShowHeaderWhenEmpty="True">

                                  <Columns>
                                      <asp:TemplateField HeaderText="peremrid" Visible="False">
                                          <EditItemTemplate>
                                              <asp:TextBox ID="txt_PerEmr_Id" runat="server" Text='<%# Bind("peremr_id") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label5" runat="server" Text='<%# Bind("peremr_id") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Nombre y Apellidos">
                                          <EditItemTemplate>
                                              <asp:TextBox ID="txtNombApelEmer" Width="200px" MaxLength="400" runat="server" Text='<%# Bind("peremr_Nomb_Apell") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("peremr_Nomb_Apell") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Parentesco">
                                          <EditItemTemplate>
                                              <asp:TextBox ID="txtParentescoEmer" MaxLength="50" runat="server" Text='<%# Bind("peremr_Parentesco") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("peremr_Parentesco") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Direccion De Ubicacion">
                                          <EditItemTemplate>
                                              <asp:TextBox ID="txt_DireccionEmer" MaxLength="400" runat="server" Text='<%# Bind("peremr_DireccionUbica") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label3" runat="server" Text='<%# Bind("peremr_DireccionUbica") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Telefono" HeaderStyle-Width="80px">
                                          <EditItemTemplate>
                                              <asp:TextBox ID="txtTelefonoEmer" MaxLength="10"  Width="80px" runat="server" Text='<%# Bind("peremr_Telefono") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label4" runat="server" Text='<%# Bind("peremr_Telefono") %>'></asp:Label>
                                          </ItemTemplate>
                                         <HeaderStyle Width="80px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                      <asp:CommandField ShowEditButton="True" />
                                      <asp:TemplateField ShowHeader="False">
                                          <ItemTemplate>
                                              <asp:LinkButton ID="btn_Eliminar_PersEmergencia" Text="Eliminar"  runat="server" CommandName="Delete" OnClientClick="return confirmarEliminacion();" ForeColor="Black"></asp:LinkButton>
                                          </ItemTemplate>
                                             <HeaderStyle Width="30px" />
                                      </asp:TemplateField>
                                  </Columns>
                                  <EditRowStyle HorizontalAlign="Right" />
                                  <EmptyDataRowStyle BorderStyle="Solid" />
                                  <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                  <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                  <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                  <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Left" />
                                  <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                  <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                  <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                  <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                  <SortedDescendingHeaderStyle BackColor="#000065" />
                              </asp:GridView>
                          </td>
                      </tr>
                  </table> 
                  </asp:panel>  
                  <br />
                             <table class="fondoazul" width="100%">
                              <tr>
                                  <td align="center" class="auto-style21">
                                      <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Text="INFORMACIÓN ACADEMICA"></asp:Label>
                                  </td>
                              </tr>
                          </table>
             
                 
                       <asp:panel ID="pnlEstudios" runat="server"
               Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GroupingText="¿QUE ESTUDIOS TIENE?"
                Width="1140px" Height="100%" CssClass="auto-style2">
                           <table>
                               <tr>
                                   <td style="text-align: right" class="auto-style9">
                                       <asp:Label ID="lbltitulo_Estudios" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nivel Educativo:" Width="90px"></asp:Label>
                                   </td>
                                   <td style="text-align: left" class="auto-style9">
                                       <asp:DropDownList ID="txt_Titulo" MaxLength="50" AutoPostBack="true" runat="server" Width="196px" ToolTip="Titulo obtenido" OnTextChanged="txt_Titulo_TextChanged" ></asp:DropDownList>
                                   </td>
                                   <td style="text-align: right" class="auto-style9">
                                       <asp:Label ID="lblprograma_Estudios" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Titulo:" Width="50px"></asp:Label>
                                   </td>
                                   <td style="text-align: left" class="auto-style9">
                                       <asp:TextBox ID="txt_Programa" MaxLength="50" AutoPostBack="false" runat="server" Width="196px" OnTextChanged="txt_Programa_TextChanged"></asp:TextBox>
                                   </td>
                                    <td style="text-align: right" class="auto-style9">
                                       <asp:Label ID="lblinstitu_Estudios" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Institucion:" Width="50px"></asp:Label>
                                   </td>
                                   <td style="text-align: left" class="auto-style9">
                                       <asp:TextBox ID="txt_Entidad" MaxLength="100" AutoPostBack="false" runat="server" Width="196px"></asp:TextBox>
                                   </td>
                                      <td style="text-align: right" class="auto-style9">
                                       <asp:Label ID="lblcursa_Estudios" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cursando:" Width="50px"></asp:Label>
                                   </td>
                                   <td style="text-align: left" class="auto-style9">
                                       <asp:DropDownList ID="cbo_Cursando" runat="server" AutoPostBack="true" OnTextChanged="cbo_Cursando_TextChanged" OnSelectedIndexChanged="cbo_Cursando_SelectedIndexChanged">
                                           <asp:ListItem Value="1">SI</asp:ListItem>
                                           <asp:ListItem Value="0">NO</asp:ListItem>
                                       </asp:DropDownList>
                                   </td>
                               </tr>
                               <tr>
                                   <td style="text-align: right">
                                       <asp:Label ID="lblfechini_Estudios" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Inicio:" Width="90px"></asp:Label>
                                   </td>
                                   <td style="text-align: left" class="altoRengInicio">
                                       <asp:TextBox ID="txt_FechaInicio" MaxLength="10" AutoPostBack="true" runat="server" Width="90px"></asp:TextBox>
                                       <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                           TargetControlID="txt_FechaInicio" Format="dd/MM/yyyy">
                                       </asp:CalendarExtender>
                                   </td>
                                   <td style="text-align: right">
                                       <asp:Label ID="lblsemetre_Estudios" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Semestre Actual:" Width="90px"></asp:Label>
                                   </td>
                                   <td style="text-align: left" class="altoRengInicio">
                                       <asp:TextBox ID="txt_Semestre" MaxLength="2" AutoPostBack="false" runat="server" Width="30px"></asp:TextBox>
                                   </td>
                                   <td style="text-align: right">
                                       <asp:Label ID="lbllugar_Estudios" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Lugar Realizado:" Width="90px"></asp:Label>
                                   </td>
                                   <td style="text-align: left" class="altoRengInicio">
                                       <asp:TextBox ID="txt_Lugar" MaxLength="50" AutoPostBack="false" runat="server" Width="196px"></asp:TextBox>
                                   </td>
                                   <td style="text-align: right">
                                       <asp:Label ID="lblcompleta_Estudios" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Completado:" Width="90px"></asp:Label>
                                   </td>
                                   <td style="text-align: left" class="altoRengInicio">
                                       <asp:DropDownList ID="cbo_Completado" runat="server" AutoPostBack="true" OnTextChanged="cbo_Completado_TextChanged">
                                           <asp:ListItem Value="0">NO</asp:ListItem>
                                           <asp:ListItem Value="1">SI</asp:ListItem>
                                       </asp:DropDownList>
                                   </td>
                               </tr>
                               <tr>
                                       <td style="text-align: right">
                                       <asp:Label ID="lblañogradu_Estudios" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Terminacion:" Width="110px"></asp:Label>
                                   </td>
                                   <td style="text-align: left" class="altoRengInicio">
                                       <asp:TextBox ID="txt_AñoGradua" MaxLength="10" AutoPostBack="false" runat="server" Width="90px"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                         TargetControlID="txt_AñoGradua"></asp:CalendarExtender>
                                   </td>                               
                                        <td style="text-align: right">
                                       <asp:Label ID="lblestuexter_Estudios" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Estudio Externo:" Width="90px"></asp:Label>
                                   </td>
                                   <td style="text-align: left" class="altoRengInicio">
                                        <asp:DropDownList ID="cbo_Estu_Externo" runat="server" AutoPostBack="false" ToolTip="Estudio realizado por fuera de  la empresa">
                                            <asp:ListItem Value="1">SI</asp:ListItem>   
                                             <asp:ListItem Value="0">NO</asp:ListItem>                                                                         
                                       </asp:DropDownList>                        
                                   </td>
                                          <td style="text-align: right">
                                       <asp:Label ID="lblcostea_Estudios" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Costeado por la empresa:" Width="130px"></asp:Label>
                                   </td>
                                   <td style="text-align: left" class="altoRengInicio">
                                        <asp:DropDownList ID="cbo_Costeado" runat="server" AutoPostBack="false" ToolTip="Costeado por la empresa">
                                            <asp:ListItem Value="0">NO</asp:ListItem> 
                                             <asp:ListItem Value="1">SI</asp:ListItem>                                                                                                                      
                                       </asp:DropDownList>                        
                                   </td>
                               </tr>
                           </table>
                            <table>
                      <tr>  
                          <td>
                              <asp:Button ID="btn_AgregarEstudio" runat="server"  BackColor="#1C5AB6" ForeColor="White"
                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"  Text="Guardar" OnClick="btn_AgregarEstudio_Click1" />
                          </td>
                           <td>
                              <asp:Button ID="btn_Habili_AgregarEstudio" runat="server" Text="Agregar" BackColor="#1C5AB6" ForeColor="White"
                                  BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_Habili_AgregarEstudio_Click"/>
                          </td>
                              <td>
                              <asp:Button ID="btn_Cancelar_AgregarEstudio" runat="server" Text="Cancelar" BackColor="#1C5AB6" ForeColor="White"
                                  BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_Cancelar_AgregarEstudio_Click"/>
                          </td>                     
                          <td class="auto-style9">
                              <asp:Label ID="lblMsgEstudio" runat="server" ForeColor="Red"></asp:Label>
                          </td>
                      </tr>
                  </table>    
                        <table>                    
                             <tr>
                          <asp:GridView ID="GridEstudios" runat="server" BackColor="White" BorderColor="#999999"
                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="1054px" DataKeyNames="est_id" AllowPaging="True"
                AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px" 
                OnRowEditing="GridEstudios_RowEditing" OnRowCancelingEdit="GridEstudios_RowCancelingEdit"
                OnRowUpdating="GridEstudios_RowUpdating" OnRowDeleting="GridEstudios_RowDeleting" OnPageIndexChanging="GridEstudios_PageIndexChanging" ShowHeaderWhenEmpty="True">
                              <Columns>
                                  <asp:TemplateField HeaderText="est_id" Visible="False">
                                      <EditItemTemplate>
                                          <asp:TextBox ID="txt_Est_Id" runat="server" Text='<%# Bind("est_id") %>'></asp:TextBox>
                                      </EditItemTemplate>
                                      <ItemTemplate>
                                          <asp:Label ID="Label8" runat="server" Text='<%# Bind("est_id") %>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Nivel Educativo" HeaderStyle-Width="210px">
                                      <EditItemTemplate>                                 
                                          <asp:DropDownList ID="txt_titulo" Width="210px" Text='<%# Bind("est_tipo") %>' DataSource="<%# Obtener_TipEstudio() %>" DataTextField="tipest_nombre" DataValueField="tipest_nombre" AutoPostBack="false" runat="server">
                                          </asp:DropDownList>                                  
                                      </EditItemTemplate>
                                      <ItemTemplate>
                                          <asp:Label ID="Label3" runat="server" Text='<%# Bind("est_tipo") %>'></asp:Label>
                                      </ItemTemplate>
                                      <HeaderStyle Width="210px" />                           
                                      <ItemStyle HorizontalAlign="left" />
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Titulo" HeaderStyle-Width="210px">
                                      <EditItemTemplate>
                                          <asp:TextBox ID="txt_Programa" Width="210px" runat="server" Text='<%# Bind("est_titulo") %>'></asp:TextBox>
                                      </EditItemTemplate>
                                      <ItemTemplate>
                                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("est_titulo") %>'></asp:Label>
                                      </ItemTemplate>
                                      <HeaderStyle Width="210px" />
                                      <ItemStyle HorizontalAlign="Left" />
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Entidad Educativa" HeaderStyle-Width="210px">
                                      <EditItemTemplate>
                                          <asp:TextBox ID="txt_EntidadEduca" Width="210px" runat="server" Text='<%# Bind("est_entidad_educativa") %>'></asp:TextBox>
                                      </EditItemTemplate>
                                      <ItemTemplate>
                                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("est_entidad_educativa") %>'></asp:Label>
                                      </ItemTemplate>
                                      <HeaderStyle Width="210px" />
                                      <ItemStyle HorizontalAlign="Left" />
                                  </asp:TemplateField>              
                                  <asp:TemplateField HeaderText="Fecha Terminacion" HeaderStyle-Width="120px" >                              
                                      <EditItemTemplate>
                                          <asp:CalendarExtender ID="CalendarExtender12" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_Año_Gradua">
                                          </asp:CalendarExtender>
                                          <asp:TextBox ID="txt_Año_Gradua" Width="100px" runat="server" Text='<%# Bind("est_fecha_fin","{0:d}") %>'></asp:TextBox>
                                      </EditItemTemplate>                                
                                      <ItemTemplate>
                                          <asp:Label ID="Label4" runat="server" Text='<%# Bind("est_fecha_fin","{0:d}") %>'></asp:Label>
                                      </ItemTemplate>
                                      <HeaderStyle Width="120px" />
                                      <ItemStyle HorizontalAlign="Left" />
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Completado" HeaderStyle-Width="50px" >
                                      <EditItemTemplate>
                                          <asp:DropDownList ID="cbo_completa" Width="50px" runat="server" Text='<%# Bind("est_tiene_certificado")%>'>
                                                    <asp:ListItem>SI</asp:ListItem>
                                               <asp:ListItem >NO</asp:ListItem>
                                          </asp:DropDownList>
                                      </EditItemTemplate>
                                      <ItemTemplate>
                                          <asp:Label ID="Label5" runat="server" Text='<%# Bind("est_tiene_certificado") %>'></asp:Label>
                                      </ItemTemplate>
                                       <HeaderStyle Width="50px" />
                                      <ItemStyle HorizontalAlign="Center"/>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Semestre Actual" HeaderStyle-Width="110px">
                                      <EditItemTemplate>
                                          <asp:TextBox ID="txt_semestre" Width="90px" MaxLength="2" AutoPostBack="false" runat="server" Text='<%# Bind("est_semetre_Actual") %>' ></asp:TextBox>
                                      </EditItemTemplate>
                                      <ItemTemplate>
                                          <asp:Label ID="Label6" runat="server" Text='<%# Bind("est_semetre_Actual") %>'></asp:Label>
                                      </ItemTemplate>
                                      <HeaderStyle Width="110px" />
                                      <ItemStyle HorizontalAlign="Center" />
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Cursando" HeaderStyle-Width="50px">
                                      <EditItemTemplate>
                                          <asp:DropDownList ID="cbo_cursando" Width="50px" runat="server" OnTextChanged="cbo_cursando_TextChanged1" AutoPostBack="true" Text='<%# Bind("est_cursando")%>' >
                                               <asp:ListItem >SI</asp:ListItem>
                                               <asp:ListItem >NO</asp:ListItem>
                                          </asp:DropDownList>
                                      </EditItemTemplate>
                                      <ItemTemplate>
                                          <asp:Label ID="Label7" runat="server" Text='<%# Bind("est_cursando") %>'></asp:Label>
                                      </ItemTemplate>
                                      <HeaderStyle Width="50px" />
                                      <ItemStyle HorizontalAlign="center" />
                                  </asp:TemplateField>
                                  <asp:CommandField HeaderText="Editar" ShowEditButton="True" />
                                  <asp:TemplateField ShowHeader="False">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="btn_EliminarEstudio"  Text="Eliminar"  runat="server" CommandName="Delete" OnClientClick="return confirmarEliminacion();" ForeColor="Black"></asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                              </Columns>
                <EditRowStyle HorizontalAlign="Right" />
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
                          </asp:GridView>
                                 </table>
                  </asp:panel> 
                  <br />
                  <table id="lblinfoseguridad" class="fondoazul" width="100%">
                      <tr>
                          <td align="center">
                              <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Text=" INFORMACIÓN SEGURIDAD"></asp:Label>
                          </td>
                      </tr>
                  </table><asp:Panel runat="server" ID="pnlinfoseguridad"
                      Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                      Width="704px" Height="100%" CssClass="auto-style2">
                       <tr>
                              <td class="auto-style42">
                                  <asp:Label ID="lblRdesociales" runat="server" Text="¿Tiene actualmente paginas sociales?(redes Sociales)" Width="300px"></asp:Label>
                              </td>
                              <td class="auto-style31">
                                  <asp:CheckBox ID="chkRdesocialeSi" Text="Si" runat="server" AutoPostBack="true" OnCheckedChanged="chkRdesocialeSi_CheckedChanged"  />
                                  &nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox ID="chRdesocialesNo" Text="No" runat="server" AutoPostBack="true" OnCheckedChanged="chRdesocialesNo_CheckedChanged" />
                                  &nbsp&nbsp&nbsp&nbsp&nbsp
                              </td>
                          </tr>
                     <br />
                      <br />
                       <tr>
                              <td class="auto-style42">
                                  <asp:Label ID="lblpublicaFotos" runat="server" Text="¿Si la respuesta es afirmativa  usted ha publicado fotos de las  instalaciones o maquinaria de la compañía en esas redes sociales?" Width="300px"></asp:Label>
                              </td>
                              <td class="auto-style31">
                                  <asp:CheckBox ID="chkpublicaFotoSi" Text="Si" runat="server" AutoPostBack="true" OnCheckedChanged="chkpublicaFotoSi_CheckedChanged"  />
                                  &nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox ID="chkpublicaFotoNo" Text="No" runat="server" AutoPostBack="true" OnCheckedChanged="chkpublicaFotoNo_CheckedChanged" />
                                  &nbsp&nbsp&nbsp&nbsp&nbsp
                              </td>
                          </tr>
                      <br />
                      <br />
                      <tr>
                           <td class="auto-style42">
                                  <asp:Label ID="lblrecibeDotac" runat="server" Text="¿Usted  recibe dotación por parte de la compañía para la realización de sus labores:?" Width="300px"></asp:Label>
                              </td>
                              <td class="auto-style31">
                                  <asp:CheckBox ID="chkrecibeDotacSi" Text="Si" runat="server" AutoPostBack="true" OnCheckedChanged="chkrecibeDotacSi_CheckedChanged"  />
                                  &nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox ID="chkrecibeDotacNo" Text="No" runat="server" AutoPostBack="true" OnCheckedChanged="chkrecibeDotacNo_CheckedChanged" />
                                  &nbsp&nbsp&nbsp&nbsp&nbsp
                              </td>
                      </tr>
                      <br />
                      <br />
                       <tr>
                           <td class="auto-style42">
                                  <asp:Label ID="lblConReglaDota" runat="server" Text="¿ Usted sabe que por efectos legales la dotación de la compañía no se debe donar ni regalar:?" Width="300px"></asp:Label>
                              </td>
                              <td class="auto-style31">
                                  <asp:CheckBox ID="chkConReglaDotaSi" Text="Si" runat="server" AutoPostBack="true" OnCheckedChanged="chkConReglaDotaSi_CheckedChanged"  />
                                  &nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox ID="chkConReglaDotaNo" Text="No" runat="server" AutoPostBack="true" OnCheckedChanged="chkConReglaDotaNo_CheckedChanged" />
                                  &nbsp&nbsp&nbsp&nbsp&nbsp
                              </td>
                      </tr>

                          </asp:Panel>

                  <br />                
                      <table class="fondoazul" width="100%">
                          <tr>
                              <td align="center">
                                  <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Text="INFORMACIÓN VEHICULAR"></asp:Label>
                              </td>
                          </tr>                       
                      </table>
                  <br />
                  <asp:Panel runat="server" ID="pnlVehiculo"
                      Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                      Width="704px" Height="100%" CssClass="auto-style2">
                      <table>
                          <tr>
                              <td>
                                  <asp:Label ID="lbllicencia" runat="server" Text="¿Tiene licencia de conducción Vigente:?" Width="200px"></asp:Label>
                              </td>
                              <td class="auto-style31">
                                  <asp:CheckBox ID="ChklicenciaSi" Text="Si" runat="server" AutoPostBack="true" OnCheckedChanged="ChklicenciaSi_CheckedChanged" />
                                  &nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox ID="ChklicenciaNo" Text="No" runat="server" AutoPostBack="true" OnCheckedChanged="ChklicenciaNo_CheckedChanged" />
                                  &nbsp&nbsp&nbsp&nbsp&nbsp
                              </td>
                          </tr>
                          </table>
                      <table>
                          <tr>
                              <td class="auto-style42">
                                  <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="¿Posee Carro?" Width="80px"></asp:Label>
                              </td>
                              <td class="auto-style31">
                                  <asp:CheckBox ID="chkSiPlacaCarro" Text="Si" runat="server" AutoPostBack="true" OnCheckedChanged="chkSiPlacaCarro_CheckedChanged" />
                                  &nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox ID="chkNoPlacaCarro" Text="No" runat="server" AutoPostBack="true" OnCheckedChanged="chkNoPlacaCarro_CheckedChanged" />
                                  &nbsp&nbsp&nbsp&nbsp&nbsp
                              </td>

                              <%--/evento del vehiculo/--%>
                              <tr>
                                  <td class="TexDer" >
                                      <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Placa1:" Width="34px"></asp:Label>
                                  </td>
                                  <td class="auto-style31">
                                      <asp:TextBox ID="txtPlacaCarro" runat="server" MaxLength="6" Width="70px" AutoPostBack="false" OnTextChanged="txtPlacaCarro_TextChanged"></asp:TextBox>
                                  </td>
                                  <td style="text-align: right">
                                      <asp:Label ID="lblModelo" runat="server" Text="Modelo:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                  </td>
                                  <td class="auto-style31">
                                      <asp:TextBox ID="txtModelo1" runat="server" MaxLength="6" Width="70px" AutoPostBack="false" OnTextChanged="txtModelo1_Textchanged"></asp:TextBox>
                                  </td>
                                  <td style="text-align: right">
                                      <asp:Label ID="lblMarca" runat="server" Text="Marca:"></asp:Label>
                                  </td>
                                  <td>
                                      <asp:TextBox ID="txtMarca1" runat="server" MaxLength="6" Width="70px" AutoPostBack="false" OnTextChanged="txtMarca1_Textchanged"></asp:TextBox>
                                  </td>
                              </tr>
                              <tr>
                                  <td class="TexDer" >
                                      <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Placa2:" Width="34px"></asp:Label>
                                  </td>
                                  <td class="auto-style31">
                                      <asp:TextBox ID="txtPlacaCarro2" runat="server" MaxLength="6" Width="70px" AutoPostBack="false" CssClass="auto-style33" OnTextChanged="txtPlacaCarro2_TextChanged"></asp:TextBox>
                                  </td>
                                  <td style="text-align: right">
                                      <asp:Label ID="lblModelo2" runat="server" Text="Modelo:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                  </td>
                                  <td class="auto-style31">
                                      <asp:TextBox ID="txtModelo2" runat="server" MaxLength="6" Width="70px" AutoPostBack="false" OnTextChanged="txtModelo2_Textchanged"></asp:TextBox>
                                  </td>
                                  <td style="text-align: right">
                                      <asp:Label ID="lblMarca2" runat="server" Text="Marca:"></asp:Label>
                                  </td>
                                  <td>
                                      <asp:TextBox ID="txtMarca2" runat="server" MaxLength="6" Width="70px" AutoPostBack="false" OnTextChanged="txtMarca2_Textchanged"></asp:TextBox>
                                  </td>

                                  <td class="auto-style40">
                                      <asp:Label ID="lbl_TieneCarro" runat="server"></asp:Label>
                                  </td>
                              </tr>
                              <tr>
                                  <td class="auto-style34">
                                      <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="¿Posee Moto?" Width="80px"></asp:Label>
                                  </td>
                                  <td class="auto-style41">
                                      <asp:CheckBox ID="chkSiPlacaMoto" runat="server" AutoPostBack="true" OnCheckedChanged="chkSiPlacaMoto_CheckedChanged" Text="Si" />
                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                      <asp:CheckBox ID="chkNoPlacaMoto" runat="server" AutoPostBack="true" OnCheckedChanged="chkNoPlacaMoto_CheckedChanged" Text="No" />
                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                  <%-- Datos DE la Moto 27/08/2019 Cristian Sanchez Alias= "Menor" --%>
                                  <tr>
                                      <td class="TexDer">
                                          <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Placa1:" Width="34px"></asp:Label>
                                      </td>
                                      <td class="auto-style31">
                                          <asp:TextBox ID="txtPlacaMoto" runat="server" AutoPostBack="false" MaxLength="6" OnTextChanged="txtPlacaMoto_TextChanged" Width="70px"></asp:TextBox>
                                      </td>
                                      <td style="text-align: right">
                                          <asp:Label ID="lblModelMoto1" runat="server" Text="Modelo:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txtModelMoto1" runat="server" MaxLength="6" Width="70px" AutoPostBack="false" OnTextChanged="txtModelMoto1_Textchanged"></asp:TextBox>
                                      </td>
                                      <td style="text-align: right">
                                          <asp:Label ID="lblMarMoto1" runat="server" Text="Marca:"></asp:Label>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txtMarMoto1" runat="server" MaxLength="6" Width="70px" AutoPostBack="false" OnTextChanged="txtMarMoto1_Textchanged"></asp:TextBox>
                                      </td>
                                  </tr>
                                   <tr>
                                        <td class="TexDer">
                                          <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Placa2:" Width="34px"></asp:Label>
                                      </td>
                                        <td class="auto-style31">
                                          <asp:TextBox ID="txtPlacaMoto2" runat="server" AutoPostBack="false" MaxLength="6" OnTextChanged="txtPlacaMoto2_TextChanged" Width="70px"></asp:TextBox>
                                      </td>
                                      <td style="text-align: right">
                                          <asp:Label ID="lblModelMoto2" runat="server" Text="Modelo:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txtModelMoto2" runat="server" MaxLength="6" Width="70px" AutoPostBack="false" OnTextChanged="txtModelMoto2_Textchanged"></asp:TextBox>
                                      </td>
                                      <td style="text-align: right">
                                          <asp:Label ID="lblMarMoto2" runat="server" Text="Marca:"></asp:Label>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txtMarMoto2" runat="server" MaxLength="6" Width="70px" AutoPostBack="false" OnTextChanged="txtMarMoto2_Textchanged"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style43">
                                          <asp:Label ID="lblTieneMoto" runat="server"></asp:Label>
                                      </td>
                                  </tr>
                              </tr>
                          </tr>
                      </table>
                  </asp:Panel>
                     <asp:Label ID="lblMsgVehiculo" runat="server" ForeColor="Red"></asp:Label>            
                  <br /><br />    
                      <asp:Button ID="btn_ActualizarRegistro" runat="server" Text="Terminar Actualizacion"  BackColor="#1C5AB6" ForeColor="White"
                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_ActualizarRegistro_Click" OnClientClick=" return confirmarActulizacionEmpleado();" CssClass="auto-style2"/>                           
              </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">


    </asp:Content>

<%-- <asp:Accordion runat="server" ID="Accordion1"  ContentCssClass="accordionContent" HeaderCssClass="accordionHeader" 
                            HeaderSelectedCssClass="accordionHeaderSelected" Width="1140px" 
                            SelectedIndex="0" AutoPostBack="True" Height="285px"> 
                            <Panes>
                                <asp:AccordionPane runat="server" ID="AccordPaneDetalle" ContentCssClass="accordionContent"
                                    Font-Bold="False"  Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected" Width="100%" class="fondoazul" >
                                    <Header>
                                        <asp:Label runat="server" Text="INFORMACIÓN PERSONAL" ID="AccordDetalle"></asp:Label>
                                    </Header>                                   
                                    <Content> 
                                         <asp:Panel ID="Panel1" runat="server"
               Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GroupingText=".."
                Width="100%" Height="100%" CssClass="auto-style2">
                                             <table>
                                                 <tr>
                                                     <td style="text-align: right" class="auto-style6">
                                                         <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" Text="Vive En Casa:" Width="120px"></asp:Label>
                                                     </td>
                                                     <td>
                                                         <asp:CheckBox ID="chk_Familiar" Text="Familiar" runat="server" />
                                                         &nbsp&nbsp&nbsp&nbsp&nbsp
                              <asp:CheckBox ID="chk_Propia" Text="Propia" runat="server" />
                                                         &nbsp&nbsp&nbsp&nbsp&nbsp
                              <asp:CheckBox ID="chk_Alquilada" Text="Alquilada" runat="server" />
                                                         &nbsp&nbsp&nbsp&nbsp&nbsp
                              <asp:CheckBox ID="chk_Otro" Text="Otro" runat="server" />
                                                     </td>
                                                 </tr>

                                                 <tr>
                                                     <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server"></asp:GridView>

                                                 </tr>
                                             </table>
                                             
                                     
                                             

                                         </asp:Panel>                                        
                                    </Content>
                                </asp:AccordionPane>
                                  </Panes>
                       </asp:Accordion>     --%>
