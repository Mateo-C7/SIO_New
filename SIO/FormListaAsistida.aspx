<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormListaAsistida.aspx.cs" Inherits="SIO.FormListaAsistida" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
		<script type="text/javascript" src="Scripts/PopperRefactored/Popper14.js"></script>
		<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
		<script type="importmap">
			{ "imports": { "vue": "./Scripts/vue.esm-browser.js", 
                "vue3-easy-data-table": "./Scripts/vue3-easy-data-table.umd.js" } 
            }
		</script>

		<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
		<script type="text/javascript" src="Scripts/select2.min.js"></script>
        <script type="module" src="Scripts/bootstrap-select.min.js"></script>
		<script type="text/javascript" src="Scripts/toastr.min.js"></script>
		<script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
		<script type="text/javascript" src="Scripts/moment.js?v=20200629"></script>
        <script type="text/javascript" src="Scripts/Datatables_Scripts/datatables.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.7.1/js/dataTables.buttons.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/buttons/1.7.1/js/buttons.colVis.min.js"></script>

        <script type="module" src="Scripts/formListaAsistida.js?v=20240918""></script>

		<link rel="Stylesheet" href="Content/bootstrap.min.css" />
		<link rel="Stylesheet" href="Content/SIO.css" />
		<link rel="stylesheet" href="Content/font-awesome.css" />
        <link rel="Stylesheet" href="Content/bootstrap-select.css" />
        <link rel="Stylesheet" href="Scripts/Datatables_Scripts/datatables.min.css" />
		<link href="Content/toastr.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <div id="loader" style="display: none">
        <h3>Procesando...</h3>
	</div>

	<div id="ohsnap"></div>

    <div class="container-fluid contenedor_fup" id="app">
         <div class="row">
             <div class="btn-group col align-self-end" role="group" aria-label="Basic example">
				<button type="button" class="btn btn-secondary langes">
					<img alt="español" src="Imagenes/colombia.png" /></button>
				<button type="button" class="btn btn-secondary langen">
					<img alt="ingles" src="Imagenes/united-states.png" /></button>
				<button type="button" class="btn btn-secondary langbr">
					<img alt="portugues" src="Imagenes/brazil.png" /></button>
			</div>
         </div>
        <div v-if="parametrosUrlValidos">
            <div class="row">
            <div class="col-3 border">
                <h6 class="text-center font-weight-bold mt-3" style="font-size: 16px !important">Añadir Elemento</h6>
                <hr />
                <div class="row mt-2" style="font-size: 16px !important">
                    <div class="col-3 font-weight-bold">
                        Producto
                    </div>
                    <div class="col-9">
                        <select id="cmbProductos" data-live-search="true" @change="cmbProductoOnChange()">
                            
                        </select>
                    </div>
                </div>
                <div class="row mt-2" style="font-size: 16px !important">
                    <div class="col-3 font-weight-bold">
                        Elemento
                    </div>
                    <div class="col-9">
                        <select id="cmbReferencias" data-live-search="true" @change="cmbReferenciaOnChange()">
                            
                        </select>
                    </div>
                </div>
                <div class="row mt-2" style="font-size: 16px !important">
                    <div class="col-12 text-center">
                        <img :src="imgReferencia" alt="" class="img-thumbnail" style="width: 70%"/>
                    </div>
                </div>
                <div class="row mt-2" style="font-size: 16px !important">
                    <div class="col-4 font-weight-bold">
                        Ancho
                    </div>
                    <div class="col-8">
                        <input step="0.01" type="number" class="form-control" placeholder="Cm" :disabled="!ancho1Requerido" v-model="ancho1"/>
                    </div>
                </div>
                <div class="row mt-2" style="font-size: 16px !important">
                    <div class="col-4 font-weight-bold">
                        Alto
                    </div>
                    <div class="col-8">
                        <input step="0.01" type="number" class="form-control" placeholder="Cm" :disabled="!alto1Requerido" v-model="alto1"/>
                    </div>
                </div>
                <div class="row mt-2" style="font-size: 16px !important">
                    <div class="col-4 font-weight-bold">
                        Ancho 2
                    </div>
                    <div class="col-8">
                        <input step="0.01" type="number" class="form-control" placeholder="Cm" :disabled="!ancho2Requerido" v-model="ancho2"/>
                    </div>
                </div>
                <div class="row mt-2" style="font-size: 16px !important">
                    <div class="col-4 font-weight-bold">
                        Alto 2
                    </div>
                    <div class="col-8">
                        <input step="0.01" type="number" class="form-control"  placeholder="Cm" :disabled="!alto2Requerido" v-model="alto2"/>
                    </div>
                </div>
                <div class="row mt-2" style="font-size: 16px !important" v-if="accsRequerido">
                    <div class="col-4 font-weight-bold">
                        ACCS
                    </div>
                    <div class="col-8">
                        <input step="0.01" type="number" class="form-control"  placeholder="Cm" v-model="accs"/>
                    </div>
                </div>
                <div class="row mt-2" style="font-size: 16px !important" v-if="espanRequerido">
                    <div class="col-4 font-weight-bold">
                        ESPAN
                    </div>
                    <div class="col-8">
                        <input step="0.01" type="number" class="form-control"  placeholder="Cm" v-model="espan"/>
                    </div>
                </div>
                <div class="row mt-2" style="font-size: 16px !important">
                    <div class="col-4 font-weight-bold">
                        Desc Auxiliar
                    </div>
                    <div class="col-8">
                        <input type="text" class="form-control" v-model="descAux"/>
                    </div>
                </div>
                <div class="row mt-2" style="font-size: 16px !important">
                    <div class="col-4 font-weight-bold">
                        Familia
                    </div>
                    <div class="col-8">
                        <input type="text" class="form-control" disabled="disabled" :value="familiaReferencia"/>
                    </div>
                </div>
                <div class="row mt-2" style="font-size: 16px !important">
                    <div class="col-4 font-weight-bold">
                        Cantidad
                    </div>
                    <div class="col-8">
                        <input type="text" class="form-control" v-model="itemCantidad"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12" ><!--v-if="listaSimulada != 1" -->
                        <button  type="button" class="btn btn-primary form-control" v-on:click="AdicionarItem()" :disabled="deshabilitarBtnAdicionar">Adicionar</button>
                    </div>
                </div>
            </div>
            <div class="col-9">
                <div class="mb-3" v-if="listaSimulada != 1">
                    <div class="btn-group" role="group">
                        <button class="btn btn-primary" type="button" v-on:click="GuardarListado()">Guardar Lista</button>
                        <button class="btn btn-secondary" type="button" v-on:click="LimpiarListado()">Limpiar Lista</button>
                        <button class="btn btn-primary" type="button" v-on:click="Simular()">Cotizar</button>
                    </div>
                </div>
                <div class="alert alert-primary text-center" role="alert" v-else>
                  <div class="col-6"><span style="font-size: 16px;"><b>Lista Cotizada!</b></span></div>
                  <div class="col-1"><button class="btn btn-primary" type="button" v-on:click="ImprimirCarta()">ir a Carta</button></div>
                </div>
                <div class="font-weight-bold mb-2" style="font-size: 14px">
                    <span>Cantidad total: {{ cantidadTotal }} - M2 totales: {{ m2Totales.toFixed(2) }}</span>
                </div>
                <div :key="index">
                    <table class="table table-striped text-center" id="tableReferencias">
                        <thead>
                            <tr>
                                <th>Cantidad</th>
                                <th>Descripcion</th>
                                <th>Ancho</th>
                                <th>Alto</th>
                                <th>Ancho 2</th>
                                <th>Alto 2</th>
                                <th>Desc Aux</th>
                                <th>Familia</th>
                                <th>Item m2</th>
                                <th>Total m2</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(item, index) in itemsActuales" v-bind:class = "{ 'bg-primary text-white' : item.PendienteGuardar === 1,
                                          'bg-danger  text-white': item.PendienteGuardar !== 1 && item.IdPiezaForsa === null}">
                                <td>{{item.ItemCantidad}}</td>
                                <td>{{item.Prefijo}}{{item.RefCodigo}}</td>
                                <td>{{item.ItemAncho1}}</td>
                                <td>{{item.ItemAlto1}}</td>
                                <td>{{item.ItemAncho2}}</td>
                                <td>{{item.ItemAlto2}}</td>
                                <td>{{item.ItemDescAux}}</td>
                                <td>{{item.RefGrupoDescripcion}}</td>
                                <td>{{item.ItemM2}}</td>
                                <td>{{item.ItemTotalM2}}</td>
                                <td><button v-if="listaSimulada != 1" type="button" class="btn btn-danger" v-on:click="RemoverItem(index)"><i class="fa fa-trash-o"></i></button></td>
                              </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-3 border">
                <h6 class="text-center font-weight-bold mt-3" style="font-size: 16px !important">Subir Listado</h6>
                <hr />

                <div class="row mt-2" style="font-size: 16px !important">
                    <div class="col-3 font-weight-bold">
                        Archivo:
                    </div>
                    <div class="col-9">
                        <input type="file" class="form-control" ref="inputFileListado"/>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-12">Recuerda que el archivo debe tener extensión .csv y contener el siguiente orden de columnas: Cantidad, ReferenciaId, Ancho1, Alto1, Ancho2, Alto2, Espacio, DescAux, Familia, M2, TotalM2</div>
                </div>
                <div class="row">
                    <div class="col-12"><!--v-if="listaSimulada != 1"-->
                        <button type="button"  class="btn btn-primary form-control" v-on:click="SubirListado()" >Añadir</button>
                    </div>
                </div>
            </div>
        </div>
        </div>
        <div v-else>
            <div class="row">
                <div class="col-12 font-weight-bold" style="font-size: 26px !important">Parámetros inválidos</div>
            </div>
        </div>
    </div>
</asp:Content>

