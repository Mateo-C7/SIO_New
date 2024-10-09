<%@ Page Title="Resumen PQRS" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormPQRSResumen.aspx.cs" Inherits="SIO.FormPQRSResumen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
		<script type="text/javascript" src="Scripts/PopperRefactored/Popper14.js"></script>
		<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
		<script type="importmap">
			{ "imports": { "vue": "./Scripts/vue.esm-browser.js", 
                            "vue3-easy-data-table": "./Scripts/vue3-easy-data-table.umd.js" } 
            }
		</script>

		<script type="module" src="Scripts/formpqrsResumen.js?v=20240408A"></script>

		<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
		<script type="text/javascript" src="Scripts/select2.min.js"></script>
        <script type="module" src="Scripts/bootstrap-select.min.js"></script>
		<script type="text/javascript" src="Scripts/toastr.min.js"></script>
		<script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
		<script type="text/javascript" src="Scripts/moment.js?v=20200629"></script>

		<link rel="Stylesheet" href="Content/bootstrap.min.css" />
		<link rel="Stylesheet" href="Content/SIO.css" />
		<link rel="Stylesheet" href="Scripts/TableVue/style.css" />
		<link rel="stylesheet" href="Content/font-awesome.css" />
		<link rel="Stylesheet" href="Content/css/select2.min.css" />
        <link rel="Stylesheet" href="Content/bootstrap-select.css" />
		<link href="Content/toastr.min.css" rel="stylesheet" />

        <script>
            function Collapse(object) {
                if ($(object).children().attr("class").search("down") == -1) {
                    $(object).children().removeClass();
                    $(object).children().addClass("fa fa-angle-double-down");
                    $("#body" + object.id.replace("collapse", "")).attr("style", "display: none; padding-top: 20px;margin-left: 15px;margin-right: 15px; ");
                }
                else {
                    $(object).children().removeClass();
                    $(object).children().addClass("fa fa-angle-double-up");
                    $("#body" + object.id.replace("collapse", "")).attr("style", "display: normal; padding-top: 20px;margin-left: 15px;margin-right: 15px; ");
                }
            }
            function CollapseHallazgosNoProcedentes(object) {
                if ($(object).children().attr("class").search("down") == -1) {
                    $(object).children().removeClass();
                    $(object).children().addClass("fa fa-angle-double-down");
                    $("#bodyHallazgosNoProcedentes" + object.id.replace("collapseHallazgosNoProcedentes", "")).attr("style", "display: none;margin-left: 15px;margin-right: 15px; ");
                }
                else {
                    $(object).children().removeClass();
                    $(object).children().addClass("fa fa-angle-double-up");
                    $("#bodyHallazgosNoProcedentes" + object.id.replace("collapseHallazgosNoProcedentes", "")).attr("style", "display: normal;margin-left: 15px;margin-right: 15px; ");
                }
            }
            function CollapseHallazgosMarcadosNoProcedentes(object) {
                if ($(object).children().attr("class").search("down") == -1) {
                    $(object).children().removeClass();
                    $(object).children().addClass("fa fa-angle-double-down");
                    $("#bodyHallazgosMarcadosNoProcedentes" + object.id.replace("collapseHallazgosMarcadosNoProcedentes", "")).attr("style", "display: none;margin-left: 15px;margin-right: 15px; ");
                }
                else {
                    $(object).children().removeClass();
                    $(object).children().addClass("fa fa-angle-double-up");
                    $("#bodyHallazgosMarcadosNoProcedentes" + object.id.replace("collapseHallazgosMarcadosNoProcedentes", "")).attr("style", "display: normal;margin-left: 15px;margin-right: 15px; ");
                }
            }
        </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">

    <div id="loader" style="        display: none">
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
                 <h6 class="ml-4 mt-2" style="font-size: 20px;"><b>¡Atención Con5entidos!</b></h6>
			</div>
         </div>

         <!-- Modal para generar orden -->
         <div class="modal fade " id="ModalGenerarOrden" tabindex="-1" role="dialog" aria-hidden="true">
		    <div class="modal-dialog" role="document">
			    <div class="modal-content modal-lg">
				    <div class="modal-header">
                        <h5 class="modal-title">Generar Orden</h5>
						<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
                    </div>
                    <div class="modal-body p-3">
                        <div class="row">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-4"><label><b>Solucionado en Obra? &nbsp;</b></label></div>
                                <div class="col-sm-4"><label><input  type="radio" value=true v-model="pqrsGenerarOrden.solucionadoEnObra"/>  &nbsp;Si</label> &nbsp;
                                <label><input  type="radio" value=false v-model="pqrsGenerarOrden.solucionadoEnObra"/>  &nbsp;No</label>  </div>
                            </div>
                        <div class="row">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-4"><label><b>Desea seleccionar una orden existente? &nbsp;</b></label></div>
                                <div class="col-sm-4"><label><input  type="radio" id="update" value="true" v-model="pqrsGenerarOrden.existeOrden" @change="ExisteOrdenChange"/>  &nbsp;Si</label> &nbsp;
                                <label><input  type="radio" id="New" value="false" v-model="pqrsGenerarOrden.existeOrden" @change="noExisteOrdenChange"/>  &nbsp;No</label>  </div>
                            </div>
                            <div class="row" v-if="pqrsGenerarOrden.existeOrden == 'true'">
                                 <div class="col-sm-1"></div>
                                   <div class="col-sm-4">
                                       Seleccionar orden
                                    <select  class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" v-model="pqrsGenerarOrden.Idordenprocedente">
                                       <option value="-1">Seleccionar Orden</option>
                                        <option v-for="(itemq, indexOrden) in numordenes" :value="itemq.IdOfa" :key="indexOrden">
                                            {{ itemq.Orden }}
                                        </option>
                                    </select> </div>
                             </div>       
                            <div class="row" v-if="pqrsGenerarOrden.existeOrden == 'false'">
                                 <div class="col-sm-1"></div>
                                   <div class="col-sm-4">
                                       <b>Seleccionar un tipo de orden</b>
                                    <select  class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" v-model="pqrsGenerarOrden.OrdenGarantiaOMejora">
                                        <option value="-1">Seleccionar Opción</option>
                                        <option value="OG">Orden de Garantía</option>
                                        <option value="OM">Orden de Mejora</option>
                                    </select> </div>
                                <div class="col-sm-4">
                                    <b>Seleccionar Planta</b>
                                    <select class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" v-model="pqrsGenerarOrden.IdPlanta">
                                        <option value="-1">Seleccionar Planta</option>
                                        <option v-for="planta in plantas" :value="planta.Id">{{planta.Descripcion}}</option>
                                    </select>
                                </div>
                             </div>                                  
                            <div class="row">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-4"><label><b>Requiere listados? &nbsp;</b></label></div>
                                    <div class="col-sm-4">
                                        <label><input  type="radio" id="pedirCorreo" value="true"  v-model="pqrsGenerarOrden.requierelistados"  />  &nbsp;Si</label> &nbsp;
                                        <label><input  type="radio" id="pedirDescripcion" value="false" v-model="pqrsGenerarOrden.requierelistados" />  &nbsp;No</label>                       
                                   </div>							    
                            </div>
                            <div class="row" v-if="pqrsGenerarOrden.requierelistados == 'true'">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-1"><label><b>Acero</b></label></div>
                                    <input type="checkbox" v-model="pqrsGenerarOrden.RequierelistadosAcero" class="form-control col-1"/>
                                    <textarea :disabled="!pqrsGenerarOrden.RequierelistadosAcero" v-model="pqrsGenerarOrden.RequierelistadosAceroCorreos" class ="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline col-5" id="requierelistadosCorreos" rows="2" placeholder=" Ingrese los correos a notificar separados por ; " ></textarea>
                                </div>  
                            <div class="row" v-if="pqrsGenerarOrden.requierelistados == 'true'">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-1"><label><b>Aluminio</b></label></div>
                                    <input type="checkbox" v-model="pqrsGenerarOrden.RequierelistadosAluminio" class="form-control col-1"/>
                                    <textarea :disabled="!pqrsGenerarOrden.RequierelistadosAluminio" v-model="pqrsGenerarOrden.RequierelistadosAluminioCorreos" class ="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline col-5" id="requierelistadosCorreos" rows="2" placeholder=" Ingrese los correos a notificar separados por ; " ></textarea>
                            </div> 
                            <div class="row" v-if="pqrsGenerarOrden.requierelistados == 'false'"> 
                                <div class="col-sm-2"></div>
                                <textarea v-model="pqrsGenerarOrden.requierelistadosDescripcion" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline col-5" id="requierelistadosDescripcion" rows="3" placeholder=" Ingrese una descripción " ></textarea>
                            </div>
                            <div class="row">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-4"><label><b>Requiere planos? &nbsp;</b></label></div>
                                    <div class="col-sm-4">
                                        <label><input  type="radio" value="true"  v-model="pqrsGenerarOrden.requiereplanos"  />  &nbsp;Si</label> &nbsp;
                                        <label><input  type="radio" value="false" v-model="pqrsGenerarOrden.requiereplanos" />  &nbsp;No</label>                       
                                    </div>							    
                            </div>
                            <div class="row" v-if="pqrsGenerarOrden.requiereplanos == 'true'">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-1"><label><b>Acero</b></label></div>
                                    <input type="checkbox" v-model="pqrsGenerarOrden.RequiereplanosAcero" class="form-control col-1"/>
                                    <textarea :disabled="!pqrsGenerarOrden.RequiereplanosAcero" v-model="pqrsGenerarOrden.RequiereplanosAceroCorreos" class ="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline col-5" rows="2" placeholder=" Ingrese los correos a notificar separados por ;" ></textarea>
                                </div>  
                            <div class="row" v-if="pqrsGenerarOrden.requiereplanos == 'true'">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-1"><label><b>Aluminio</b></label></div>
                                    <input type="checkbox" v-model="pqrsGenerarOrden.RequiereplanosAluminio" class="form-control col-1"/>
                                    <textarea :disabled="!pqrsGenerarOrden.RequiereplanosAluminio" v-model="pqrsGenerarOrden.RequiereplanosAluminioCorreos" class ="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline col-5" rows="2" placeholder=" Ingrese los correos a notificar separados por ;" ></textarea>
                            </div>  
                            <div class="row" v-if="pqrsGenerarOrden.requiereplanos == 'false'"> 
                                <div class="col-sm-2"></div>
                                <textarea v-model="pqrsGenerarOrden.RequierePlanosDescripcion" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline col-5" id="requierelistadosDescripcion" rows="3" placeholder=" Ingrese una descripción" ></textarea>
                            </div>


                        <div class="row">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-4"><label><b>Requiere Plano de Armado Nuevo o Actualizado &nbsp;</b></label></div>
                                    <div class="col-sm-4">
                                        <label><input  type="radio" value="true"  v-model="pqrsGenerarOrden.requierearmado"  />  &nbsp;Si</label> &nbsp;
                                        <label><input  type="radio" value="false" v-model="pqrsGenerarOrden.requierearmado" />  &nbsp;No</label>                       
                                    </div>							    
                            </div>
                            <div class="row" v-if="pqrsGenerarOrden.requierearmado == 'true'">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-1"><label><b>Acero</b></label></div>
                                    <input type="checkbox" v-model="pqrsGenerarOrden.RequierearmadoAcero" class="form-control col-1"/>
                                    <textarea :disabled="!pqrsGenerarOrden.RequierearmadoAcero" v-model="pqrsGenerarOrden.RequierearmadoAceroCorreos" class ="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline col-5" rows="2" placeholder=" Ingrese los correos a notificar separados por ;" ></textarea>
                                </div>  
                            <div class="row" v-if="pqrsGenerarOrden.requierearmado == 'true'">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-1"><label><b>Aluminio</b></label></div>
                                    <input type="checkbox" v-model="pqrsGenerarOrden.RequierearmadoAluminio" class="form-control col-1"/>
                                    <textarea :disabled="!pqrsGenerarOrden.RequierearmadoAluminio" v-model="pqrsGenerarOrden.RequierearmadoAluminioCorreos" class ="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline col-5" rows="2" placeholder=" Ingrese los correos a notificar separados por ;" ></textarea>
                            </div>  
                            <div class="row" v-if="pqrsGenerarOrden.requierearmado == 'false'"> 
                                <div class="col-sm-2"></div>
                                <textarea v-model="pqrsGenerarOrden.RequiereArmadoDescripcion" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline col-5" id="requierearmadoDescripcion" rows="3" placeholder=" Ingrese una descripción" ></textarea>
                            </div>

                    </div>
                    <div class="modal-footer">
					    <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
						<button type="button" class="btn btn-success ml-1 " @click="ValidarGenerarOrden()">Generar</button>
					</div>
                </div>
            </div>
         </div>
         <!-- Fin modal para generar orden -->

         <!-- Modal para procedencia -->
         <div class="modal fade " id="ModalReclamoProcedente" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content modal-lg">
						<div class="modal-header">
							<h5 class="modal-title">Reclamo Procedente </h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
						</div>
						<div class="modal-body p-3">
						
							<div class="row">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-3"><label style="margin-top: 0px !important;"><b>Es Procedente ? &nbsp;</b></label></div>
                                <div class="col-sm-4">
                                    <select v-model="pqrsProcedente.EsProcedente" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline">
                                        <option value='null'>Seleccione</option>
                                        <option value='true'>Si</option>
                                        <option value='false'>No</option>
                                    </select>
                                </div>
							</div>

                            <div class="row mx-3 pb-2 mt-3"> 
                                <textarea v-model="pqrsProcedente.DescripcionNoProcedente" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" rows="5" placeholder=" Ingrese una razon porque es o no procedente" ></textarea>
                           </div>

                            <div class="row" v-if="pqrsProcedente.EsProcedente == 'true'">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-8">
                                    <select @change="AddProcesoToPNC" class="form-control" v-model="ProcesoAddProcesoToPNCTemp">
                                        <option value="">Seleccionar para añadir procesos</option>
                                        <option v-for="(item, index) in procesos" :value="item.Proceso" :key="index">
                                            {{ item.Proceso }}
                                        </option>
                                    </select>
                                </div>
                            </div>

							<div class="row" v-if="pqrsProcedente.EsProcedente == 'true'">
								<div class="col-sm-12">
									<table class="wfull">
										<thead>
											<tr>
												<th class="text-center" style="width:20%">Proceso</th>
												<th class="text-center" style="width:20%">Tipo NC</th>
                                                <th class="text-center" style="width:20%">Email</th>
                                                <th class="text-center" style="width:35%">Comentario</th>
                                                <th class="text-center" style="width: 5%"></th>
											</tr>
										</thead>
										<tbody>
											<tr v-for="(item, index) in procesosToAdd">
												<td class="text-center">{{item.Proceso}}</td>
												<td class="text-center">
                                                    <select v-model="item.TipoNC"  class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" >
                                                         <option v-for="(itemTNC, indexTNC) in tipoNC" :value="itemTNC.Id" :key="index">
                                                            {{ itemTNC.Descripcion }}
                                                         </option>
                                                    </select>
												</td>
                                                <td class="text-center">
                                                    <textarea class="form-control" v-model="item.EmailProceso" cols="1"></textarea>
                                                </td>
                                                <td class="text-center">
                                                    <textarea class="form-control" v-model="item.Comentario" cols="1"></textarea>
                                                </td>
                                                <td class="text-center">
                                                    <button @click="DeleteProcesoToPNC(index)" class="btn btn-danger btn-sm">
                                                        <i class="fa fa-trash-o" style="margin-left: -200%"></i>
                                                    </button>
                                                </td>
											</tr>
										</tbody>
									</table>
								</div>

                           </div>  
                            <div id="DinamycChangeHallazgos" class="col-md-12 mt-3 border p-3" v-if="pqrsProcedente.EsProcedente == 'true' &&infoPQRSIdFuenteReclamo == 5">
                         <div class="box-header border-bottom border-primary Comentario" style="z-index: 2;">
                                <span>Seleccione cuales hallazgos <b>NO</b> son procedentes</span>
                                <div class="col-md-12 mt-3">
                                    <table class="col-md-12">
                                        <thead>
                                            <tr>
                                                <th width="5%"></th>
                                                <th width="20%">Fecha</th>
                                                <th width="65%">Título</th>
                                                <th width="5%"></th>
                                                <td width="5%"></td>
                                            </tr>
                                        </thead>
                                    </table></div>
                                 </div>
                                    <div v-for="hallazgo in lisHallazgos" v-if="lisHallazgos.length > 0">
                             <div v-if="hallazgo.Padre == '0'" class="col-md-12 Comentario" style="padding-top: 6x;padding-left: 0px;padding-right: 0px;" :id="'ParteHallazgoObra' + hallazgo.Nivel"><div :id="'headerHallazgoObra' + hallazgo.Nivel" class="box box-primary"><div class="box-header border-bottom border-primary" style="z-index: 2;">
                                    <table class="col-md-12 table-sm">
                                        <tr>
                                            <td width="5%" class="text-center">
                                                <input type="checkbox" class="" v-model="hallazgosNoProcedentes[hallazgo.Id]"/>
                                            </td>
                                            <td width="20%">
                                                {{hallazgo.Fecha.substring(0, 10)}}
                                            </td>
                                            <td width="65%">
                                                {{hallazgo.Titulo}}
                                            </td>
                                            <td width="3%">
                                                <div class="col-md-12" style="padding-bottom: 4px;">
                                                    <button :id="'collapseHallazgosNoProcedentes' + hallazgo.Nivel" onclick="CollapseHallazgosNoProcedentes(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 2px;">
                                                        <span class="fa fa-angle-double-down"></span>
                                                    </button>
                                                 </div>
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                    <div :id="'bodyHallazgosNoProcedentes' + hallazgo.Nivel" class="box-body" style="display: none;margin-left: 10px;margin-right: 10px; ">
                                     <div class="row item" :id="hallazgo.Nivel"><span style="font-size: 10px;" class="mt-2">Observacion</span><textarea class="form-control col-sm-12 SegLogistico" rows="2" disabled v-model="hallazgo.Comentario"></textarea></div>
                            
                                            <!-- Aqui van los anexos -->
                                        <div class="row item" v-if="hallazgo.Anexos.length > 0"><table class="col-md-12 table-sm table-bordered">
                                    <tr v-for="ane in hallazgo.Anexos">
                                        <td width="10%" >Anexo</td>
                                        <td width="80%">{{ane.nombre}}</td>
                                        <td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" @click="DescargarArchivo('', ane.ruta+ane.nombre)"> </button></td>
                                    </tr>
                                </table></div>

                                        <div v-if="hallazgo.Padre != '0'" class="row item" :id="hallazgo.Nivel"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">{{hallazgo.Fecha.substring(0, 10)}}</td><td width="15%"><span v-if="hallazgo.TipoConsideracionObservacion">Correctivo para la orden</span><span v-else>Mejora en el proceso</span></td><td width="33%">{{hallazgo.Comentario}}</td><td width="10%">{{hallazgo.FecDespacho.substring(0, 10)}}</td><td width="10%">{{hallazgo.Estado}}</td><td colspan = "2" width="20%">{{hallazgo.Usuario}}</td></tbody></table></div></div>
                                    </div>

                                    </div>
                             </div>
                           </div>
                                </div>
                            
						<div class="modal-footer">
							<button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
							<button type="button" class="btn btn-success ml-1 " @click="ValidarPQRSProcedencia()">Guardar</button>
                            
							
						</div>
					</div>
				</div>
			</div>
            </div>
         <!-- Fin modal procedencia --> 

         <!-- Modal para procedencia -->
         <div class="modal fade " id="ModalAdicionarProcesos" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content modal-lg">
						<div class="modal-header">
							<h5 class="modal-title">Añadir procesos adicionales</h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
						</div>
						<div class="modal-body p-3">
                            <div class="row">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-8">
                                    <select @change="AddProcesoToPNC" class="form-control" v-model="ProcesoAddProcesoToPNCTemp">
                                        <option value="">Seleccionar para añadir procesos</option>
                                        <option v-for="(item, index) in procesos" :value="item.Proceso" :key="index">
                                            {{ item.Proceso }}
                                        </option>
                                    </select>
                                </div>
                            </div>
							<div class="row">
								<div class="col-sm-12">
									<table class="wfull">
										<thead>
											<tr>
												<th class="text-center" style="width:20%">Proceso</th>
												<th class="text-center" style="width:20%">Tipo NC</th>
                                                <th class="text-center" style="width:20%">Email</th>
                                                <th class="text-center" style="width:35%">Comentario</th>
                                                <th class="text-center" style="width: 5%"></th>
											</tr>
										</thead>
										<tbody>
											<tr v-for="(item, index) in procesosToAdd">
												<td class="text-center">{{item.Proceso}}</td>
												<td class="text-center">
                                                    <select v-model="item.TipoNC"  class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" >
                                                         <option v-for="(itemTNC, indexTNC) in tipoNC" :value="itemTNC.Id" :key="index">
                                                            {{ itemTNC.Descripcion }}
                                                         </option>
                                                    </select>
												</td>
                                                <td class="text-center">
                                                    <textarea class="form-control" v-model="item.EmailProceso" cols="1"></textarea>
                                                </td>
                                                <td class="text-center">
                                                    <textarea class="form-control" v-model="item.Comentario" cols="1"></textarea>
                                                </td>
                                                <td class="text-center">
                                                    <button @click="DeleteProcesoToPNC(index)" class="btn btn-danger btn-sm">
                                                        <i class="fa fa-trash-o" style="margin-left: -200%"></i>
                                                    </button>
                                                </td>
											</tr>
										</tbody>
									</table>
								</div>
                           </div>  
						<div class="modal-footer">
							<button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
							<button type="button" class="btn btn-success ml-1 " @click="ValidarAdicionarProcesosProcedencia()">Guardar</button>
                            
							
						</div>
					</div>
				</div>
			</div>
            </div>
         <!-- Fin modal adicionar pncs --> 

         <!-- Modal para enviar comunicados -->
         <div class="modal fade " id="ModalEnviarComunicado" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content modal-lg">
						<div class="modal-header">
							<h5 class="modal-title">Enviar Comunicado al Cliente </h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
						</div>
						<div class="modal-body p-3">
			            <div class="row">
                            <div class="col-2"><label>Para:</label></div>
                            <div class="col-4"><input type="text" v-model="enviarComunicado.para" class="block appearance-none w-full bg-white border border-gray-400 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" /></div>                                    
                            <div class="col-1"><label>CC:</label></div>
                            <div class="col-5"><input type="text" v-model="enviarComunicado.Cc" class="block appearance-none w-full bg-white border border-gray-400 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" />  Ingrese los correos a notificar separados por ; </div>                                    
                          
                        </div>
                 
                          <div class="row">
                             <div class="col-2"><label>Asunto:</label></div>
                             <div class="col-10"><input type="text" v-model="enviarComunicado.Asunto"  class="block appearance-none w-full bg-white border border-gray-400 px-4 py-2 pr-8  rounded shadow leading-tight focus:outline-none focus:shadow-outline" /></div>
                        </div>
                           
                            
							<div class="row">
                                <div class="col-sm-2"><b>Mensaje</b></div>
                                <div class="col-10">
								    <textarea  v-model="enviarComunicado.mensaje" class="shadow leading-tight w-full bg-white border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outline" id="mensajeCli" rows="5" placeholder=".... comunicado aqui" required v-model="mensajeCliente"></textarea>
                                </div>
							</div>
                            
                            <ul class="nav nav-tabs mt-3" id="enviarComunicadosTab" role="tablist">
                                <li class="nav-item">
                                    <a style="font-size: inherit" class="nav-link active" id="varios-tab" data-toggle="tab" href="#variosComunicado" role="tab" aria-controls="home" aria-selected="true">Varios</a>
                                </li>
                                <li class="nav-item"  v-if="esProcedente != null">
                                    <a style="font-size: inherit" class="nav-link" id="details-tab" data-toggle="tab" href="#detailsComunicado" role="tab" aria-controls="profile" aria-selected="false">Detalles</a>
                                </li>
                                <li class="nav-item"  v-if="pqrsAsociadosOrden != null">
                                    <a style="font-size: inherit" class="nav-link" id="asociated-pqrs-tab" data-toggle="tab" href="#asociatedPQRSComunicado" role="tab" aria-controls="pqrs" aria-selected="false">PQRS Asociados</a>
                                </li>
                            </ul>

                            <div class="tab-content" id="myTabContent" style="border: 1px solid transparent; border-color: #fff #dee2e6 #dee2e6 #dee2e6;">
                                <div class="tab-pane fade show active" id="variosComunicado" role="tabpanel" aria-labelledby="varios-tab">
                                    <div class="row mt-3 mx-3" v-if="tipoPQRS != 5 && tipoPQRS != 6">
                                        <div class="col-sm-2" style="padding: 0px;"><b>Incluir encuesta de satisfacción </b></div>
                                        <div class="col-1">
                                            <input type="checkbox" class="form-control" v-model="enviarComunicado.incluirEncuesta"/>
                                        </div>
                                    </div>

                                    <div class="row border p-4 mx-1 mt-3">
                                        ¿Cuales archivos del radicado desea enviar adicionales?
                                        <table class="col-12 table table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>Archivo</th>
                                                        <th>Ver</th>
                                                        <th>Enviar?</th>
                                                    </tr>
                                                </thead>
                                                <tbody v-for="rad in radicado">
                                                    <tr>
                                                        <td>{{rad.FileName}}</td>
                                                        <td><button 
                                                                type="button" 
                                                                class="btn fa fa-download" 
                                                                @click="DescargarArchivo(rad.FileName, rad.Path)"
                                                                >
                                                            </button>
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" v-model="archivosRadicadoComunicado[rad.IdArchivo]" class="form-control" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                    </div>

                                    <hr />

                                    <div class="row border p-4 mx-1 mt-3">
                                        ¿Cuales archivos del comprobante de entrega en obra desea enviar?
                                        <table class="col-12 table table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>Archivo</th>
                                                        <th>Ver</th>
                                                        <th>Enviar?</th>
                                                    </tr>
                                                </thead>
                                                <tbody v-for="archivoProduccion in archivosProduccion">
                                                    <tr>
                                                        <td>{{archivoProduccion.FileName}}</td>
                                                        <td><button 
                                                                type="button" 
                                                                class="btn fa fa-download" 
                                                                @click="DescargarArchivo(archivoProduccion.FileName, archivoProduccion.Path)"
                                                                >
                                                            </button>
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" v-model="archivosComprobanteComunicado[archivoProduccion.IdArchivo]" class="form-control" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                    </div>

                                    <div class="row mx-1 border pt-2">
                                        <label class="col-2" style="margin-top: 0px !important">Archivos para cargar al comunicado</label>
                                        <div class="col-10">
                                            <input type="file" class="form-control" id="fileInputComunicado" ref="fileComunicado" @change="onChangeComunicado" multiple="multiple" accept=".doc,.docx,.pdf,.jpg,.jpeg,.png" />
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="detailsComunicado" role="tabpanel" aria-labelledby="details-tab">
                                    <div class="form-group row mt-2 mx-1">
                                        <label style="margin-top: 0px !important" for="txtEsProcedenteComunicado" class="col-2 col-form-label">Es procedente?</label>
                                        <div class="col-4 mt-2">
                                            <input disabled class="form-control" type="text" id="txtEsProcedenteComunicado" :value="(esProcedente ? 'Si': 'No')" />
                                        </div>
                                    </div>
                                    <div v-if="infoPQRSOrdenGarantiaOMejora != '' || infoPQRSOrdenProcedente !== ''" class="form-group row mx-1">
                                        <label v-if="infoPQRSOrdenGarantiaOMejora != ''" style="margin-top: 0px !important" for="txtTipoOrdenComunicado" class="col-2 col-form-label">Tipo de Orden Generada</label>
                                        <div v-if="infoPQRSOrdenGarantiaOMejora != ''" class="col-4 mt-2">
                                            <input disabled class="form-control" type="text" id="txtTipoOrdenComunicado" v-model="infoPQRSOrdenGarantiaOMejora" />
                                        </div>
                                        <label v-if="infoPQRSOrdenProcedente !== ''" style="margin-top: 0px !important" for="txtNumOrdenComunicado" class="col-2 col-form-label"># de Orden</label>
                                        <div class="col-4 mt-2" v-if="infoPQRSOrdenProcedente !== ''">
                                            <input disabled class="form-control" type="text" id="txtNumOrdenComunicado" v-model="infoPQRSOrdenProcedente" />
                                        </div>
                                    </div>
                                    <div class="form-group row mx-1">
                                        <label style="margin-top: 0px !important" for="txtDescripcionProcedenciaComunicado" class="col-2 col-form-label">Descripción Procedencia</label>
                                        <div class="col-10 mt-2">
                                            <textarea disabled class="form-control" id="txtDescripcionProcedenciaComunicado" v-model="infoPQRSDescripcionProcedencia"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div v-if="pqrsAsociadosOrden != null" class="tab-pane fade p-3" id="asociatedPQRSComunicado" role="tabpanel" aria-labelledby="details-tab">
                                    <table class="table table-striped table-sm">
                                        <thead>
                                            <tr>
                                                <th style="width: 20%">Id PQRS</th>
                                                <th style="width: 80%">Detalles</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="asociatedPQRS in pqrsAsociadosOrden">
                                                <td>{{asociatedPQRS.IdPQRS}}</td>
                                                <td>{{asociatedPQRS.Detalle}}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
							<button type="button" id="btnEnviarMensajeCliente" @click="validarEnviarMensajeCliente(enviarComunicado)" class="btn btn-success">Enviar</button>
						</div>
					</div>
				</div>
			</div>
         <!-- Fin del modal para enviar comunicados -->

         <!-- Modal para cargar listados requeridos -->
         <div class="modal fade " id="ModalAgregarListadosRequerido" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content modal-lg">
						<div class="modal-header">
							<h5 class="modal-title">Administrar Listados/Planos Requeridos </h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
						</div>
						<div class="modal-body p-3">

                            <div v-for="listadoRequerido in listadosRequeridos.listadosPlanos">
                                <div class="row mt-2">
								    <div class="col-1 offset-1">
                                        <input type="checkbox" v-model="listadoRequerido.Completos" class="form-control" :disabled="!listadoRequerido.PuedeEditar"/>
								    </div>
                                    <div class="col-3">
                                        <b>{{ listadoRequerido.Correo }} </b>
                                    </div>
                                    <div class="col-1">
                                        {{ listadoRequerido.TipoCargue }}
                                    </div>
                                    <div class="col-1">
                                        {{ listadoRequerido.Tipo }}
                                    </div>
                                    <div class="col-4">
                                        <textarea v-model="listadoRequerido.Comentario" class="form-control" :disabled="!listadoRequerido.PuedeEditar"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-3 mx-1">
                                <span class="col-2">Tipo Archivo</span>
                                <select v-model="listadosRequeridos.TablaCargueArchivos" class="form-control col-4">
                                    <option value="-1" selected="selected">Seleccionar Tipo Archivo</option>
                                    <option value="1" v-if="debeCargarListadosOPlanosOArmado.listados.acero == true || debeCargarListadosOPlanosOArmado.listados.aluminio == true">Listados</option>
                                    <option value="2" v-if="debeCargarListadosOPlanosOArmado.planos.acero == true || debeCargarListadosOPlanosOArmado.planos.aluminio == true">Planos</option>
                                    <option value="3" v-if="debeCargarListadosOPlanosOArmado.armado.acero == true || debeCargarListadosOPlanosOArmado.armado.aluminio == true">Plano de Armado</option>
                                </select>
                                <span class="col-2">Tipo Material</span>
                                <select v-model="listadosRequeridos.TipoCargueArchivos" class="form-control col-4">
                                    <option value="-1" selected="selected">Seleccionar Tipo Archivo</option>
                                    <option value="Acero" v-if="debeCargarListadosOPlanosOArmado.listados.acero == true || debeCargarListadosOPlanosOArmado.planos.acero == true || debeCargarListadosOPlanosOArmado.armado.acero == true">Acero</option>
                                    <option value="Aluminio" v-if="debeCargarListadosOPlanosOArmado.listados.aluminio == true || debeCargarListadosOPlanosOArmado.planos.aluminio == true || debeCargarListadosOPlanosOArmado.armado.aluminio == true">Aluminio</option>
                                </select>
                                
                            </div>
						    <div class="row">
                                <div class="main">
								        <div class="dropzone-container" @dragover="dragover" @dragleave="dragleave" @drop="dropListados">
									        <input type="file" multiple name="fileInputListados" id="fileInputListados" class="hidden-input" @change="onChangeListados" ref="fileInputListados" accept=".pdf,.jpg,.jpeg,.png,.xls,.xlsx" />

									        <label for="fileInputListados" class="file-label">
                                                <div v-if="isDragging">Arrastra los archivos aqui.</div>
                                                <div v-else>Arrastra los archivos aqui  o <u>click aqui</u> para cargar.</div>
                                            </label>

									        <div class="preview-container mt-2" v-if="files.length">
										        <div v-for="file in files" :key="file.name" class="preview-card">
											        <div>
												        <img class="preview-img" :src="generateThumbnail(file)" />
												        <p :title="file.name">
													        {{ makeName(file.name) }}
												        </p>
											        </div>
											        <div>
												        <button class="ml-2" type="button" @click="remove(files.indexOf(file))" title="Remove file">
                                                         <b>&times;</b>
                                                        </button>
											        </div>
										        </div>
									        </div>
								        </div>
							        </div>                         
						    </div>
                            
                        
				     	</div>
                        <div class="modal-footer">
							<button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
							<button type="button" id="btnAgregarListadosRequeridos" @click="ValidarGuardarListadosRequeridos()" class="btn btn-success">Guardar</button>
						</div>
				   </div>
			    </div>
            </div>
         <!-- Fin modal listados requeridos -->

        <!-- Modal -->
        <div class="modal fade" id="editResumenDataModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="detModalLabel">Información PQRS</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div>
                    <div class="row">
                        <div class="col-12">Descripción</div>
                    </div>
                    <div class="row">
                        <textarea class="form-control mx-3" rows="3" v-model="infoPQRSDetalle"></textarea>
                    </div>
                    <div class="row" v-if="infoPQRSIdCliente == -1">
							<div class="col-3" >
								Pais
							</div>
							<div class="col-9">
							    <select id="cmbPaises" data-width="auto" 
                                    data-live-search="true" @change="onChangePais" v-model="infoPQRSIdPais" class="" >
                                 </select>
                                
							</div>
							<div class="col-3" >
								Ciudad
							</div>
							<div class="col-9">
                                <select id="cmbCiudad" 
                                    data-width="auto" 
                                    data-live-search="true" 
                                    v-model="infoPQRSIdCiudad" 
                                    @change="onChangeCiudad"
                                    class="" >
                                 </select>
							</div>                 
						</div>
                    <div class="row" v-if="infoPQRSIdCliente == -1">
                        <div class="col-3">Cliente</div>
                        <div class="col-9"><select id="cmbEmpresa" 
                            data-width="fit" 
                            data-live-search="true" v-model="infoPQRSIdClienteTemp" class="" >
                                 </select>
                        </div>
                        <div class="col-9 offset-3" v-if="infoPQRSIdClienteTemp == -1"><input type="text" v-model="infoPQRSOtroClienteNombre" class="form-control" /></div>
                    </div>
                    <div class="row">
                        <div class="col-3">Nombre Respuesta</div>
                        <div class="col-9">
                            <input class="form-control" v-model="infoPQRSNombreRespuesta"/>
                        </div>
                    </div>
                     <div class="row">
                        <div class="col-3">Dirección Respuesta</div>
                        <div class="col-9">
                            <input class="form-control" v-model="infoPQRSDireccionRespuesta"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">Email Respuesta</div>
                        <div class="col-9">
                            <input class="form-control" v-model="infoPQRSEmailRespuesta"/>
                        </div>
                     </div>
                    <div class="row">
                        <div class="col-3">Teléfono Respuesta</div>
                        <div class="col-9">
                            <input class="form-control" v-model="infoPQRSTelefonoRespuesta"/>
                        </div>
                    </div>
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal" ref="cerrarModificarDatosGeneralesPQRS">Cancelar</button>
                <button type="button" class="btn btn-primary" v-on:click="GuardarPQRSDatosGenerales()">Guardar</button>
              </div>
            </div>
          </div>
        </div>

         <!-- Modal Editar No Conformidad -->
         <div class="modal fade" id="editNoConformidad" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="detModalLabel">Editar No Conformidad</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div>
                    <div class="row">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-8">
                                    
                                </div>
                            </div>
                    <div class="form-group">
                        <label for="exampleInputEmail1">Proceso</label>
                        <select @change="AddProcesoToPNC" class="form-control" v-model="noConformidadEditNC.Proceso">
                                        <option v-for="(item, index) in procesos" :value="item.Proceso" :key="index">
                                            {{ item.Proceso }}
                                        </option>
                                    </select>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputEmail1">Descripción No Conformidad</label>
                        <input type="text" class="form-control" id="inputDescNoConformidadEditNC" v-model="noConformidadEditNC.DescripcionNoConformidad" aria-describedby="emailHelp" disabled="disabled" />
                    </div>
                    <div class="form-group">
                        <label for="exampleInputEmail1">Email</label>
                        <input type="text" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" v-model="noConformidadEditNC.Email" placeholder="Ingresar emails separados por punto y coma" />
                    </div>
                    <div class="form-group">
                        <label for="comentarioNoConformidadEdit">Comentario</label>
                        <textarea class="form-control" id="comentarioNoConformidadEdit" v-model="noConformidadEditNC.Comentario"></textarea>
                    </div>
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal" ref="cerrarModificarDatosGeneralesPQRS">Cancelar</button>
                <button type="button" class="btn btn-primary" v-on:click="PreguntarValidarNoConformidadEditNC()">Guardar</button>
              </div>
            </div>
          </div>
        </div>
         <!-- Fin Modal Editar No Conformidad -->

         <!-- Modal to show action plan details -->
        <div class="modal fade" id="modalActionPlanDetails" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Detalles Plan de Accion / {{ noConformidadDetails.Proceso }} {{ noConformidadDetails.DescripcionNoConformidad }}</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group row">
                            <label style="margin-top: 0px !important" for="txtFechaCierreDetails" class="col-2 col-form-label">Fecha Cierre</label>
                            <div class="col-4">
                                <input disabled class="form-control" type="text" id="txtFechaCierreDetails" :value="noConformidadDetails.PlanAccionFecha ? noConformidadDetails.PlanAccionFecha.substring(0, 10) : ''" />
                            </div>
                            <label style="margin-top: 0px !important" for="txtPlanAccionDetails" class="col-2 col-form-label">Plan Accion</label>
                            <div class="col-4">
                                <input disabled class="form-control" type="text" id="txtPlanAccionDetails" v-model="noConformidadDetails.PlanAccion" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <label style="margin-top: 0px !important" for="txtPlanAccionDescripcionDetails" class="col-2 col-form-label">Descripcion</label>
                            <div class="col-10">
                                <textarea disabled class="form-control" id="txtPlanAccionDescripcionDetails" v-model="noConformidadDetails.PlanAccionDescripcion"></textarea>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label style="margin-top: 0px !important" for="txtClasificacionDetails" class="col-2 col-form-label">Clasificacion</label>
                            <div class="col-4">
                                <input disabled class="form-control" type="text" id="txtClasificacionDetails" v-model="noConformidadDetails.Clasificacion" />
                            </div>
                            <label style="margin-top: 0px !important" for="txtUsuarioResponsableDetails" class="col-2 col-form-label">Usuario Responsable</label>
                            <div class="col-4">
                                <input disabled class="form-control" type="text" id="txtUsuarioResponsableDetails" v-model="noConformidadDetails.UsuarioResponsable" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <label style="margin-top: 0px !important" for="txtFamiliaGarantiaDetails" class="col-2 col-form-label">Familia Garantia</label>
                            <div class="col-4">
                                <input disabled class="form-control" type="text" id="txtFamiliaGarantiaDetails" v-model="noConformidadDetails.FamiliaGarantia" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <label style="margin-top: 0px !important" for="tableProductosGarantiaDetails" class="col-2 col-form-label">Productos</label>
                            <div class="col-10">
                                <table id="tableProductosGarantiaDetails" class="table table-sm">
                                    <tbody>
                                        <tr v-for="producto in noConformidadDetails.ProductosGarantias">
                                            <td v-if="producto.Nombre == 'Otro'">Otro: {{producto.TextoOpcional}}</td>
                                            <td v-else>{{producto.Nombre}}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

         <!-- Modal adicionar procesos -->
        <div class="modal fade" id="modalAdicionarProcesos" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title">Adicionar Procesos</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div class="row">
                    <div class="col-6">
                        <label style="margin-top: 0px !important" for="#selectAdminProcesses">
                            Proceso para añadir
                        </label>
                        <select class="form-control" ref="selectAdminProcesses">
                            <option v-for="(procesoAdmin, index) in procesosAdmin" :value="procesoAdmin.Proceso" :key="index">
                                {{ procesoAdmin.Proceso }}
                            </option>
                        </select>
                        <button class="btn btn-primary form-control mt-2" @click="adicionarProcesoTemporal">
                            Añadir
                        </button>
                    </div>
                    <div class="col-12 mt-3">
                        <table class="table table-stripped table-sm">
							<thead>
								<tr>
									<th class="text-center" style="width:20%">Proceso</th>
									<th class="text-center" style="width:30%">Email</th>
                                    <th class="text-center" style="width:45%">Observacion</th>
                                     <th class="text-center" style="width: 5%"></th>
								</tr>
							</thead>
								<tbody>
									<tr v-for="(asignacionProceso, index) in asignacionProcesos">
										<td class="text-center">{{asignacionProceso.Proceso}}</td>
                                        <td class="text-center">
                                            <textarea class="form-control" rows="1" v-model="asignacionProceso.Email" :disabled="!asignacionProceso.IsNew"></textarea>
                                        </td>
                                        <td class="text-center">
                                            <textarea class="form-control" v-model="asignacionProceso.Observacion" rows="1" :disabled="!asignacionProceso.IsNew"></textarea>
                                        </td>
                                        <td class="text-center">
                                            <button v-if="asignacionProceso.IsNew" @click="DeleteStagedAssignedProcess(index)" class="btn btn-danger btn-sm">
                                                <i class="fa fa-trash-o pr-2"></i>
                                            </button>
                                        </td>
									</tr>
                                </tbody>
						</table>
                    </div>
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                <button type="button" class="btn btn-primary" v-on:click="ValidarGuardarProcesosAsignados()">Guardar</button>
              </div>
            </div>
          </div>
        </div>
         <!-- Fin modal adicionar procesos-->

         <!-- Modales para cargar mas archivos -->
        <div class="modal fade" id="adicionarArchivosRadicacionModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title"> Adicionar archivos</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div>
                    <input type="file" class="" id="addFilesRadicado" ref="addFilesRadicado" multiple="multiple"/>
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal" ref="CerrarModalAdicionarArchivos">Cancelar</button>
                <button type="button" class="btn btn-primary" v-on:click="AdicionarArchivosRadicado()">Añadir</button>
              </div>
            </div>
          </div>
        </div>

        <div class="modal fade" id="adicionarArchivosComprobacionModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title"> Adicionar archivos</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div>
                    <input type="file" class="" id="addFilesImplementacion" ref="addFilesImplementacion" multiple="multiple"/>
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal" ref="CerrarModalAdicionarArchivosImplementacion">Cancelar</button>
                <button type="button" class="btn btn-primary" v-on:click="AdicionarArchivosImplementacion()">Añadir</button>
              </div>
            </div>
          </div>
        </div>

         <div class="modal fade" id="adicionarArchivosCierreModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title"> Adicionar archivos</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div>
                    <input type="file" class="" id="addFilesCierre" ref="addFilesCierre" multiple="multiple"/>
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal" ref="CerrarModalAdicionarArchivosCierre">Cancelar</button>
                <button type="button" class="btn btn-primary" v-on:click="AdicionarArchivosCierre()">Añadir</button>
              </div>
            </div>
          </div>
        </div>

         <div class="modal fade" id="adicionarArchivosListadosModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title"> Adicionar archivos</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div>
                    <input type="file" class="" id="addFilesListados" ref="addFilesListados" multiple="multiple"/>
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal" ref="CerrarModalAdicionarArchivosListados">Cancelar</button>
                <button type="button" class="btn btn-primary" v-on:click="AdicionarArchivosListados()">Añadir</button>
              </div>
            </div>
          </div>
        </div>
         <!-- Fin modales para cargar mas archivos -->

         <!-- Modal para solicitar mas informacion a procesos -->
         <div class="modal fade" id="modalSolicitarInformacionProcesos" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title">Solicitar información proceso - {{nombrePQRSProcesoTemp}}</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div>
                    <textarea class="form-control" rows="3" placeholder="Informacion requerida" ref="mensajeAdicionalSolicitarInformacionProcesos"></textarea>
                    <span class="mt-3">Archivos</span>
                    <input type="file" class="form-control" id="addFilesSolicitarInformacionProcesos" ref="addFilesSolicitarInformacionProcesos" multiple="multiple"/>
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal" ref="CerrarModalSolicitarInformacionProcesos">Cancelar</button>
                <button type="button" class="btn btn-primary" v-on:click="SolicitarInformacionProcesos()">Solicitar</button>
              </div>
            </div>
          </div>
        </div>
         <!-- Fin modal solicitar informacion a procesos -->

         <!-- Modal para adicionar respuestas procesos -->
         <div class="modal fade " id="ModalAgregarRespuestaProceso" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content modal-lg">
						<div class="modal-header">
							<h5 class="modal-title">Agregar Respuesta </h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
						</div>

						<div class="modal-body p-3">
						    <div class="row">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-2"><b>  Proceso</b></div>
                                 <div class="col-sm-6">
                                    <select  class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" v-model="pqrsRespuesta.PQRSIdproceso" @change="BuscarAclaracionProceso()">
                                       <option value="-1">Seleccionar Todos</option>
                                        <option v-for="(item, index) in procesosAsignadosPQRS" :value="item.PQRSProcesoId" :key="index">
                                            {{ item.Proceso }}
                                        </option>
                                    </select>
                                </div>
                                
						    </div>
                            <div class="row" v-if="pqrsProcesoAclaracionTemp != null">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-2"><b>Observación:</b></div>
                                <div class="col-sm-6">
                                    <textarea class="shadow leading-tight border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outline" rows="2" disabled="disabled" v-model="pqrsProcesoAclaracionTemp"></textarea>
                                </div>
                            </div>
							<div class="row mt-2">
                                <div class="col-sm-2"></div>
                               <div class="col-sm-2"><b>  Respuesta</b></div>
                                <div class="col-sm-6">
								    <textarea  v-model="pqrsRespuesta.Mensaje" class="shadow leading-tight border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outline" id="mensaje" rows="5" placeholder="registra tu respuesta aqui" required ></textarea>
                                </div>
							</div>

							<div class="main">
								<div class="dropzone-container" @dragover="dragover" @dragleave="dragleave" @drop="drop">
									<input type="file" multiple name="file" id="fileInputRes" class="hidden-input" @change="onChange" ref="file" accept=".pdf,.jpg,.jpeg,.png" />

									<label for="fileInputRes" class="file-label">
                                        <div v-if="isDragging">Arrastra los archivos aqui.</div>
                                        <div v-else>Arrastra los archivos aqui  o <u>click aqui</u> para cargar.</div>
                                    </label>

									<div class="preview-container mt-2" v-if="files.length">
										<div v-for="file in files" :key="file.name" class="preview-card">
											<div>
												<img class="preview-img" :src="generateThumbnail(file)" />
												<p :title="file.name">
													{{ makeName(file.name) }}
												</p>
											</div>
											<div>
												<button class="ml-2" type="button" @click="remove(files.indexOf(file))" title="Remove file">
                                                 <b>&times;</b>
                                                </button>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
							<button type="button"  @click="ValidarGuardarPQRSRespuesta()" class="btn btn-success ml-1 ">Guardar</button>
						</div>
					</div>
				</div>
			</div>
         <!-- Fin modal adicionar respuestas procesos -->

         <div class="modal fade " id="ModalCierreObra" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content modal-lg">
						<div class="modal-header">
							<h5 class="modal-title">Plan de Accion / {{ noConformidadEdit.Proceso }} {{ noConformidadEdit.DescripcionNoConformidad }}</h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
						</div>
						<div class="modal-body p-3">

			            <div class="row">
                            <div class="col-2"><label>Clasificación</label></div>
                            <div class="col-4">
                                <select v-model="noConformidadEdit.IdClasificacion" class="shadow leading-tight w-full bg-white border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outline">
                                    <option value="-1">Sin Clasificacion</option>
                                    <option v-for="clasificacion in clasificacionesPlanAccion" :value="clasificacion.Id">
                                        {{clasificacion.Nombre}}
                                    </option>
                                </select>
                            </div>
                            <div class="col-2"><label>Usuario Responsable</label></div>
                            <div class="col-4">
                                <input type="text" v-model="noConformidadEdit.UsuarioResponsable" class="shadow leading-tight w-full bg-white border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outline" />
                            </div>
                        </div>

                        <div class="row">
                             <div class="col-2"><label>Familia Garantia</label></div>
                                <div class="col-10">
                                    <select @change="onChangeFamiliaGarantia()" v-model="noConformidadEdit.IdFamiliaGarantia" class="shadow leading-tight w-full bg-white border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outlineshadow leading-tight w-full bg-white border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outline">
                                        <option value="-1" selected="selected">Sin Familia de Garantia</option>
                                        <option v-for="familiaGarantia in familiasGarantias" :value="familiaGarantia.Id">
                                            {{familiaGarantia.Nombre}}
                                        </option>
                                    </select>
                                </div>
                        </div>

                        <div class="row" v-if="noConformidadEdit.IdFamiliaGarantia != -1 && noConformidadEdit.IdFamiliaGarantia != undefined && noConformidadEdit.IdFamiliaGarantia != null && currentFamiliaGarantia.Productos != null">
                            <div class="col-2">
                                <label>Productos</label>
                            </div>
                            <div class="col-10">
                                
                                    <div class="form-check col-2" v-for="producto in currentFamiliaGarantia.Productos">
                                        <input class="form-check-input" type="checkbox" v-model="producto.Selected" :id="'producto-' + producto.Id" />
                                        <label class="form-check-label" :for="'producto-' + producto.Id" v-if="producto.Nombre !== 'Otro'">
                                            {{ producto.Nombre }}
                                        </label>
                                        <input class="form-control" type="text" v-else v-model="textoOtroProductosGarantia" placeholder="Otro"/>
                                    </div> 
                                </div>

                        </div>

                        <div class="row">
                             <div class="col-2"><label>Cierre</label></div>
                              <div class="col-4"><input type="date" v-model="noConformidadEdit.PlanAccionFecha" min="1900-01-01" max="2118-12-31" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" /></div>
                        </div>
						<div class="row">
                                <div class="col-sm-2"><b>Plan de Accion</b></div>
                                <div class="col-10">
								    <input v-model="noConformidadEdit.PlanAccion" class="shadow leading-tight w-full bg-white border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outline" id="cierreReclamacionPlanAccion" placeholder="Indicar Plan de Accion" required v-model="planAccion"/>
                                </div>
						</div>
                        <div class="row">
                            <div class="col-sm-2">
                                    <b>Descripción</b>
                                </div>
                                <div class="col-sm-10">
                            <textarea class="form-control" rows="4" placeholder="Descripción" v-model="noConformidadEdit.PlanAccionDescripcion"></textarea>
                                </div>
                            </div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
							<button type="button" id="btnEnviarCierreReclamacion" @click="validarEnviarCierreReclamacion()" class="btn btn-success">Guardar</button>
						</div>
					</div>
				</div>
			</div>

         <div class="modal fade " id="ModalCrearPlanAccionQuejas" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content modal-lg">
						<div class="modal-header">
							<h5 class="modal-title">Plan de Accion</h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
						</div>
						<div class="modal-body p-3">

			            <div class="row">
                            <div class="col-2"><label>Clasificación</label></div>
                            <div class="col-4">
                                <select v-model="noConformidadEdit.IdClasificacion" class="shadow leading-tight w-full bg-white border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outline">
                                    <option value="-1">Sin Clasificacion</option>
                                    <option v-for="clasificacion in clasificacionesPlanAccion" :value="clasificacion.Id">
                                        {{clasificacion.Nombre}}
                                    </option>
                                </select>
                            </div>
                            <div class="col-2"><label>Usuario Responsable</label></div>
                            <div class="col-4">
                                <input type="text" v-model="noConformidadEdit.UsuarioResponsable" class="shadow leading-tight w-full bg-white border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outline" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-2"><label>Email</label></div>
                            <div class="col-10">
                                <input type="text" v-model="noConformidadEdit.Email" class="shadow leading-tight w-full bg-white border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outline" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-2"><label>Comentario</label></div>
                            <div class="col-10">
                                <input type="text" v-model="noConformidadEdit.Comentario" class="shadow leading-tight w-full bg-white border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outline" />
                            </div>
                        </div>

                        <div class="row">
                             <div class="col-2"><label>Familia Garantia</label></div>
                                <div class="col-10">
                                    <select @change="onChangeFamiliaGarantia()" v-model="noConformidadEdit.IdFamiliaGarantia" class="shadow leading-tight w-full bg-white border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outlineshadow leading-tight w-full bg-white border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outline">
                                        <option value="-1" selected="selected">Sin Familia de Garantia</option>
                                        <option v-for="familiaGarantia in familiasGarantias" :value="familiaGarantia.Id">
                                            {{familiaGarantia.Nombre}}
                                        </option>
                                    </select>
                                </div>
                        </div>

                        <div class="row" v-if="noConformidadEdit.IdFamiliaGarantia != -1 && noConformidadEdit.IdFamiliaGarantia != undefined && noConformidadEdit.IdFamiliaGarantia != null && currentFamiliaGarantia.Productos != null">
                            <div class="col-2">
                                <label>Productos</label>
                            </div>
                            <div class="col-10">
                                
                                    <div class="form-check col-2" v-for="producto in currentFamiliaGarantia.Productos">
                                        <input class="form-check-input" type="checkbox" v-model="producto.Selected" :id="'producto-' + producto.Id" />
                                        <label class="form-check-label" :for="'producto-' + producto.Id" v-if="producto.Nombre !== 'Otro'">
                                            {{ producto.Nombre }}
                                        </label>
                                        <input class="form-control" type="text" v-else v-model="textoOtroProductosGarantia" placeholder="Otro"/>
                                    </div> 
                                </div>

                        </div>

                        <div class="row">
                             <div class="col-2"><label>Cierre</label></div>
                              <div class="col-4"><input type="date" v-model="noConformidadEdit.PlanAccionFecha" min="1900-01-01" max="2118-12-31" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" /></div>
                        </div>
						<div class="row">
                                <div class="col-sm-2"><b>Plan de Accion</b></div>
                                <div class="col-10">
								    <input v-model="noConformidadEdit.PlanAccion" class="shadow leading-tight w-full bg-white border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outline" id="cierreReclamacionPlanAccion" placeholder="Indicar Plan de Accion" required v-model="planAccion"/>
                                </div>
						</div>
                        <div class="row">
                            <div class="col-sm-2">
                                    <b>Descripción</b>
                                </div>
                                <div class="col-sm-10">
                            <textarea class="form-control" rows="4" placeholder="Descripción" v-model="noConformidadEdit.PlanAccionDescripcion"></textarea>
                                </div>
                            </div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
							<button type="button" @click="validarCrearPlanAccionQueja()" class="btn btn-success">Guardar</button>
						</div>
					</div>
				</div>
			</div>

         <!-- Modal para editar las fechas planeadas en produccion -->
         <div class="modal fade " id="ModalEditarProduccion" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content modal-lg">
						<div class="modal-header">
							<h5 class="modal-title">Editar fechas Produccion </h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
						</div>
						<div class="modal-body p-3">
                            <div class="row">
                                 <div class="col-4"><label></label></div>
                                 <div class="col-4"><label>Producción Aluminio</label></div>
                                 <div class="col-4"><label>Producción Acero</label></div>
                            </div>
                            <div class="row">
                                <div class="col-4"><label>Fecha Despacho Real</label></div>
                                    <div class="col-4">
                                        <input type="date" ref="editFechaPlanAlum" min="1980-01-01" max="2100-12-31" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" />
                                    </div>
                                    <div class="col-4">
                                        <input type="date" ref="editFechaPlanAcero" min="1980-01-01" max="2100-12-31" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" />
                                    </div>
		                     </div>
						<div class="modal-footer">
							<button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
							<button type="button" @click="ValidarEditarFechasProduccion()" class="btn btn-success">Guardar</button>
						</div>
					</div>
				</div>
			</div>
         </div>
         <!-- Fin modal edicion fechas -->

         <div class="modal fade " id="ModalProduccion" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content modal-lg">
						<div class="modal-header">
							<h5 class="modal-title">Producción </h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
						</div>
						<div class="modal-body p-3">
			            
                            <div class="row">
                                 <div class="col-4"><label></label></div>
                                 <div class="col-4"><label>Producción Aluminio</label></div>
                                  <div class="col-4"><label>Producción Acero</label></div>
                            </div>
						    <div class="row">
                                    <div class="col-4"><label>Fecha Planeada</label></div>
                                    <div class="col-4">
                                        <input disabled="disabled" type="date" v-model="pqrsprod.fecha_plan_alum" min="1980-01-01" max="2100-12-31" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" />
                                    </div>
                                    <div class="col-4">
                                        <input disabled="disabled" type="date" v-model="pqrsprod.fecha_plan_acero" min="1980-01-01" max="2100-12-31" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" />
                                    </div>
						    </div>
                            <div class="row">
                                    <div class="col-4"><label>Fecha Requerida</label></div>
                                    <div class="col-4">
                                        <input type="date" v-model="pqrsprod.fecha_req_alum" min="1980-01-01" max="2100-12-31" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" />
                                    </div>
                                    <div class="col-4">
                                        <input type="date" v-model="pqrsprod.fecha_req_acero" min="1980-01-01" max="2100-12-31" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" />
                                    </div>
						    </div>
                          <div class="row">
                                    <div class="col-4"><label>Fecha Despacho Real</label></div>
                                    <div class="col-4">
                                        <input disabled="disabled" type="date" v-model="pqrsprod.fecha_desp_alum" min="1980-01-01" max="2100-12-31" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" />
                                    </div>
                                    <div class="col-4">
                                        <input disabled="disabled" type="date" v-model="pqrsprod.fecha_desp_acero" min="1980-01-01" max="2100-12-31" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" />
                                    </div>
						    </div>
                              <%--  <div class="row">
                                    <div class="col-4"><label>Fecha Entr. Obra</label></div>
                                    <div class="col-4">
                                         {{pqrsprod.fecha_ent_obra}}
                                    </div>
						    </div>--%>                           
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
							<button type="button" id="btnFinalizarPRoceso" @click="cerrarProduccion()" class="btn btn-success">Guardar</button>
						</div>
					</div>
				</div>
			</div>

                     <div class="modal fade " id="ModalComprobanteObra" tabindex="-1" role="dialog" aria-hidden="true">
				        <div class="modal-dialog" role="document">
					        <div class="modal-content modal-lg">
						        <div class="modal-header">
							        <h5 class="modal-title">Comprobante Obra </h5>
							        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
					                <span aria-hidden="true">&times;</span>
				                </button>
						        </div>
						        <div class="modal-body p-3">
                                    <div class="row">
                                            <div class="main">
								                <div class="dropzone-container" @dragover="dragover" @dragleave="dragleave" @drop="dropProd">
									                <input type="file" multiple name="fileProd" id="fileProd" class="hidden-input" @change="onChangeProd" ref="fileProd" accept=".pdf,.jpg,.jpeg,.png" />

									                <label for="fileProd" class="file-label">
                                                         <div v-if="isDragging">Release to drop files here.</div>
                                                         <div v-else>Drop files here or <u>click here</u> to upload.</div>
                                                    </label>

									                <div class="preview-container mt-2" v-if="files.length">
										                <div v-for="file in files" :key="file.name" class="preview-card">
											                <div>
												                <img class="preview-img" :src="generateThumbnail(file)" />
												                <p :title="file.name">
													                {{ makeName(file.name) }}
												                </p>
											                </div>
											                <div>
												                <button class="ml-2" type="button" @click="remove(files.indexOf(file))" title="Remove file">
                                                                 <b>&times;</b>
                                                                </button>
											                </div>
										                </div>
									                </div>
								                </div>
							                </div>                         
						            </div>
						        </div>
						        <div class="modal-footer">
							        <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
							        <button type="button" @click="CerrarComprobanteObra()" class="btn btn-success">Guardar</button>
						        </div>
					        </div>
				        </div>
			        </div>

         <div class="modal fade" id="ModControlCambios" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Adicionar Control de Cambio</h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">&times;</span>
							</button>
						</div>
						<div class="modal-body">
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input type="text" class="form-control" id="AreaControl" disabled />
								<input type="text" class="form-control" id="padreCambio" disabled hidden/>
							</div>
                            <div class="row">
								<div class="col-12">Titulo</div>
							</div>
                            <div class="row">
								<div class="col-12">
    								<input type="text" class="form-control" id="txtTituloObs" />
								</div>
							</div>
                            <div class="row">
								<div class="col-12">Evento</div>
							</div>
                            <div class="row">
                                <div class="col-8">
                                    <select id="cmbBitacoraEvento" class="form-control">
                                        <option value="-1">Seleccionar</option>
                                        <option v-for="evento in bitacoraEventos" :value="evento.EventoId">{{evento.EventoDescripcion}}</option>
                                    </select>
                                </div>
                            </div>
                            <div class="row">
								<div class="col-12">Notas y Observaciones</div>
							</div>
                            <div class="row">
								<div class="col-12">
    								<textarea id="txtObsCntrCm" class="form-control" rows="3"></textarea>
								</div>
							</div>
                            <div class="row">
								<div class="col-12">Anexos</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input type="file" class="form-control" id="rutaArchivo2" multiple />
							</div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
							<button type="button" @click="AgregarControlCambios()" class="btn btn-primary">Enviar</button>
						</div>
					</div>
				</div>
			</div>


         <div class="row mx-1 bg-white py-2 border pr-3">
             <div class="col-1 d-flex" style="font-weight: bold; font-size: 16px;">
                 <span class="align-self-center">PQRS #</span> 
             </div>
             <div class="col-1 d-flex">
                 <input type="number" step="1" class="form-control align-self-center" v-model="idPQRS"/> 
             </div>
             <div class="col-1 d-flex">
                 <button type="button" class="btn btn-primary align-self-center" v-on:click="CargarIdPQRS()"><i class="fa fa-search" style="margin-left: -200%"></i></button>
             </div>
             <div class="col-2">
                 <div class="btn-group" role="group" aria-label="Basic example">
                      <button type="button" class="btn btn-primary" v-on:click="redirectCrear()" data-toggle="tooltip" title="Crear PQRS"><i class="fa fa-file pr-3"></i>Crear</button>
                      <button type="button" class="btn btn-secondary" v-on:click="redirectConsultar()"data-toggle="tooltip" title="Ir a consulta"><i class="fa fa-list pr-3"></i>Consultar</button>
                    </div>
             </div>
             <div class="col-3 border py-1 d-flex justify-content-between" style="font-size: 16px !important" ref="statusColorBar">
                <span class="align-self-center"><i class="fa fa-circle pr-4" ref="semaforo" style="color: forestgreen;" ></i> Estado actual: {{infoPQRSEstado}}</span> 
                
            </div>
             <div class="col-4" v-if="calculedRolId == 1">
                 <button type="button" style="height: 100%" class="btn btn-primary align-self-center" v-if="mostrarBtnSiguienteEstado" v-on:click="ActualizarEstadoPQRS()">
                    Proceder a {{pqrsSiguienteEstadoDesc}}
                </button>
                 <button type="button" class="btn btn-danger ml-2" v-if="mostrarBtnAnularPQRS" v-on:click="AnularPQRS()">
                    Anular PQRS
                </button>
                 <button type="button" class="btn btn-warning ml-2" v-if="puedeSerCerrada && infoPQRSEstadoID != 9 && infoPQRSEstadoID != 11" v-on:click="AskClosePQRS()">
                     <i class="fa fa-archive pr-4" aria-hidden="true"></i>
                     <span>Cerrar PQRS</span>
                 </button>
             </div>
         </div>

         <div class="row mx-1 bg-white py-2 border pr-3" v-if="canSkipStates">
                 <div class="col-2">
                     <label for="saltarEstadoCmb" style="font-size: small; font-weight: bolder;">Cambiar estados de forma manual</label>
                 </div>
                 <div class="col-4 d-flex">
                     <select id="saltarEstadoCmb" class="form-control" style="height: 100%">
                         <option value="-1" disabled selected>Estado a saltar</option>
                         <option v-for="estado in pqrsPosiblesEstados" :value="estado.PQRSEstadosID">{{ estado.Descripcion }}</option>
                     </select>
                     <button style="height: 100%" type="button" class="btn ml-2 btn-primary btn-sm" @click="confirmarSaltarEstado()">
                         Saltar a estado
                     </button>
                 </div>                     
                 </div>
         

         <div id="accordion">
             <div class="card" id="DatosGenerales" >
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseDatosGenerales" data-i18n="FUP_datos_generales">Datos Generales</a>
				</div>
				<div id="collapseDatosGenerales" class="collapse show" data-parent="#accordion">
                    <div class="container-fluid" style="font-size: 14px !important">
                        <div class="row alert alert-primary mx-1 mt-3">
                            <h6 style="margin-bottom: 0px;">Datos de Contacto PQRS</h6>
                        </div>
                        <div class="row mt-4">
                            <span class="col-1 font-weight-bold">Fuente: </span>
                            <span class="col-4"><input type="text" disabled="disabled" v-model="infoPQRSFuente" class="form-control"/></span>
                            <span class="col-1 font-weight-bold">Tipo: </span>
                            <span class="col-2"><input type="text" disabled="disabled" v-model="infoPQRSTipo" class="form-control"/></span>
                            <span class="col-1 font-weight-bold" v-if="infoPQRSSubTipo !== ''">Subtipo: </span>
                            <span class="col-3" v-if="infoPQRSSubTipo !== ''"><input type="text" disabled="disabled" v-model="infoPQRSSubTipo" class="form-control"/></span>
                        </div>
                        <div class="row mt-2">
                            <span class="col-1 font-weight-bold">Cliente: </span>
                            <span class="col-4" v-if="infoPQRSOtroClienteNombre != ''"><input type="text" disabled="disabled" v-model="infoPQRSOtroClienteNombre" class="form-control"/></span>
                            <span class="col-4" v-else><input type="text" disabled="disabled" v-model="infoPQRSClienteNombre" class="form-control"/></span>
                            <span class="col-1 font-weight-bold">NroOrden: </span>
                            <span class="col-2"><input type="text" disabled="disabled" v-model="infoPQRSNroOrden" class="form-control"/></span>
                            <span class="col-1 font-weight-bold">Estado: </span>
                            <span class="col-3"><input type="text" disabled="disabled" v-model="infoPQRSEstado" class="form-control"/></span>
                        </div>
                        <div class="row mt-2">
                            <span class="col-1 font-weight-bold">Nombre: </span>
                            <span class="col-4"><input type="text" disabled="disabled" v-model="infoPQRSNombreRespuesta" class="form-control"/></span>
                            <span class="col-1 font-weight-bold">Usuario: </span>
                            <span class="col-2"><input type="text" disabled="disabled" v-model="infoPQRSUsuarioCreacion" class="form-control"/></span>
                            <span class="col-1 font-weight-bold">Fecha: </span>
                            <span class="col-3"><input type="text" disabled="disabled" v-model="infoPQRSFechaCreacion" class="form-control"/></span>
                        </div>
                        <div class="row mt-2">
                            <span class="col-1 font-weight-bold">Direccion: </span>
                            <span class="col-4"><input type="text" disabled="disabled" v-model="infoPQRSDireccionRespuesta" class="form-control"/></span>               
                            <span class="col-1 font-weight-bold">Telefono: </span>
                            <span class="col-2"><input type="text" disabled="disabled" v-model="infoPQRSTelefonoRespuesta" class="form-control"/></span>
                            <span class="col-1 font-weight-bold">Email: </span>
                            <span class="col-3"><input type="text" disabled="disabled" v-model="infoPQRSEmailRespuesta" class="form-control"/></span>
                        </div>
                        <div class="row mt-2">
                            <span class="col-1 font-weight-bold">Pais: </span>
                            <span class="col-4"><input type="text" disabled="disabled" v-model="infoFupPais" class="form-control"/></span>               
                            <span class="col-1 font-weight-bold">Ciudad: </span>
                            <span class="col-2"><input type="text" disabled="disabled" v-model="infoFupCiudad" class="form-control"/></span>               
                        </div>
                        <div class="row mt-2 pb-3">
                            <span class="col-1 font-weight-bold">Descripción: </span>
                            <span class="col-11">
                                <textarea rows="4" disabled="disabled" v-model="infoPQRSDetalle" class="form-control"></textarea>
                            </span>
                            
                        </div>
                        <div class="row mt-3" style="margin-bottom: 15px !important" v-if="funcionalidadesBotones.editarInformacionPQRS && calculedRolId == 1">
                            <div class="col-3">
                                <button type="button" class="btn btn-primary" @click="MostrarEditResumenDataModal()">
                                    Editar información
                                </button>
                            </div>
                        </div>
                        <div v-if="mostrarDatosFup">
                            <div class="row alert alert-primary mx-1 mt-4">
                                <h6 style="margin-bottom: 0px;">Informacion Fup</h6>
                            </div>
                            <div class="row mt-4">
                                <!--<span class="col-1 font-weight-bold">Pais: </span>
                                <span class="col-2"><input type="text" disabled="disabled" v-model="infoFupPais" class="form-control"/></span>
                                <span class="col-1 font-weight-bold">Ciudad: </span>
                                <span class="col-3"><input type="text" disabled="disabled" v-model="infoFupCiudad" class="form-control"/></span>-->
                                <span class="col-1 font-weight-bold">Cliente: </span>
                                <span class="col-6"><input type="text" disabled="disabled" v-model="infoPQRSClienteNombre" class="form-control"/></span>
                                <span class="col-1 font-weight-bold">Obra: </span>
                                <span class="col-4"><input type="text" disabled="disabled" v-model="infoFupObra" class="form-control"/></span>
<%--                                <span class="col-1 font-weight-bold">Id_Ofa: </span>
                                <span class="col-1"><input type="text" disabled="disabled" v-model="infoFupId_Ofa" class="form-control"/></span>--%>
                            </div>
                            <div class="row mt-2">
                                <span class="col-1 font-weight-bold">FUP: </span>
                                <span class="col-2"><input type="text" disabled="disabled" v-model="infoFupID" class="form-control"/></span>
                                <span class="col-4"></span>
                                <span class="col-1 font-weight-bold">Contacto: </span>
                                <span class="col-4"><input type="text" disabled="disabled" v-model="infoFupContacto" class="form-control"/></span>
                            </div>
                            <div class="row mt-3 pb-2">
                                <b class="col-2 mt-3">Ordenes asociadas</b>
                     <div id="DinamycChangeHallazgos" class="col-md-12 mt-3" v-if="infoPQRSIdFuenteReclamo == 5">
                         <div class="box-header border-bottom border-primary Comentario" style="z-index: 2;">
                            <table class="col-md-12 table-sm"><thead><tr>
                            <th width:="5%">Asociar?</th>
                            <th width="12%">Fecha</th>
                            <th width="27%">Título</th>
                            <th width="10%">Sol. en Obra</th>
                            <th width="10%">Gen. Costo</th>
                            <th width="15%">Estado</th>
                            <th width="12%">Usuario</th><th width="8%">Orden Fabr</th><th width="3%">
                            </th><td width="3%"></td></tr></thead></table></div>

                         <div v-for="hallazgo in lisHallazgos" v-if="lisHallazgos.length > 0">
                             <div v-if="hallazgo.Padre == '0'" class="col-md-12 Comentario" style="padding-top: 6x;padding-left: 0px;padding-right: 0px;" :id="'ParteHallazgoObra' + hallazgo.Nivel"><div :id="'headerHallazgoObra' + hallazgo.Nivel" class="box box-primary"><div class="box-header border-bottom border-primary" style="z-index: 2;">
                                    <table class="col-md-12 table-sm"><tr><td width="5%"><input checked="checked" type="checkbox" disabled="disabled" class="form-control" v-model="hallazgo.Asociar"/></td><td width="12%">{{hallazgo.Fecha.substring(0, 10)}}</td><td width="27%">{{hallazgo.Titulo}}</td><td width="10%"><span v-if="hallazgo.SolucionadoEnObra">Sí</span><span v-else>No</span></td><td width="10%"><span v-if="hallazgo.GeneroCosto">Sí</span><span v-else>No</span></td><td width="15%">{{hallazgo.Estado}}</td><td width="12%">{{hallazgo.Usuario}}</td><td width="8%" class=text-right>{{hallazgo.HallazgoOrdenFabricacion}}</td>
                                    <td width="3%"><div class="col-md-12" style="padding-bottom: 4px;"><button :id="'collapse' + hallazgo.Nivel" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 2px;"><span class="fa fa-angle-double-down"></span></button></div></td></tr></table></div>
                                    <div :id="'body' + hallazgo.Nivel" class="box-body" style="display: none;padding-top: 8px;margin-left: 10px;margin-right: 10px; ">
                                     <div class="row item" :id="hallazgo.Nivel"><label style="font-size: 10px;">Observacion</label><textarea class="form-control col-sm-12 SegLogistico" rows="2" disabled v-model="hallazgo.Comentario"></textarea></div>
                            
                                            <!-- Aqui van los anexos -->
                                        <div class="row item" v-if="hallazgo.Anexos.length > 0"><table class="col-md-12 table-sm table-bordered">
                                    <tr v-for="ane in hallazgo.Anexos">
                                        <td width="10%" >Anexo</td>
                                        <td width="80%">{{ane.nombre}}</td>
                                        <td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" @click="DescargarArchivo('', ane.ruta+ane.nombre)"> </button></td>
                                    </tr>
                                </table></div>

                                        <div v-if="hallazgo.Padre != '0'" class="row item" :id="hallazgo.Nivel"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">{{hallazgo.Fecha.substring(0, 10)}}</td><td width="15%"><span v-if="hallazgo.TipoConsideracionObservacion">Correctivo para la orden</span><span v-else>Mejora en el proceso</span></td><td width="33%">{{hallazgo.Comentario}}</td><td width="10%">{{hallazgo.FecDespacho.substring(0, 10)}}</td><td width="10%">{{hallazgo.Estado}}</td><td colspan = "2" width="20%">{{hallazgo.Usuario}}</td></tbody></table></div></div>
                                    </div>

                                    </div>
                             </div>
                           </div>
                                 </div>
                             
                            </div>
                        </div>
                    </div>
                </div>
            </div>
             <div class="card" id="Timeline" >
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseTimeline">Detalle</a>
				</div>
				<div id="collapseTimeline" class="collapse show" data-parent="#accordion">
                    <div class="container-fluid">
                        <div v-if="infoPQRSEstadoID >= 0 && idPQRS != 0">
                            
                        <div v-if="infoPQRSEstadoID >= 0 && infoPQRSEstadoID !== 13">
                            <div class="row alert alert-primary mx-1 mt-4">
                                <h6 style="margin-bottom: 0px;">Radicado</h6>
                            </div>
                                <div class="row mx-1 mt-3">
                                    <div class="col-12">
                                    <span class="font-weight-bold">Detalle</span>: {{infoPQRSDetalle}}
                                </div>
                                </div>
                            <div class="row mx-1">
                                <div class="col-2">
                                    <b>Archivos Cargados</b>
                                </div>
                            </div>
                            <div class="row mx-1">
                                <div class="col-2" v-if="funcionalidadesBotones.añadirArchivosRadicado && calculedRolId == 1">
                                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#adicionarArchivosRadicacionModal">
                                        Añadir archivos
                                    </button>
                                </div>
                            </div>
                            <div class="mx-1">
                                <div class="col-6">
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th>Archivo</th>
                                                <th>Acciones</th>
                                            </tr>
                                        </thead>
                                        <tbody v-for="rad in radicado">
                                            <tr>
                                                <td>{{rad.FileName}}</td>
                                                <td>
                                                    <button 
                                                        type="button" 
                                                        class="btn fa fa-download" 
                                                        @click="DescargarArchivo(rad.FileName, rad.Path)"
                                                        >
                                                    </button>
                                                    <button v-if="funcionalidadesBotones.eliminarArchivosRadicado && rad.CanBeDeleted"
                                                        type="button" 
                                                        class="btn fa fa-trash ml-3" 
                                                        @click ="PreguntaBorrarArchivoFisico(rad.FileName, rad.Path, rad.IdArchivo, 1)"
                                                        >
                                                    </button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div v-if="infoPQRSEstadoID >= 1 && tipoPQRS != 5 && infoPQRSEstadoID !== 13">
                            <div class="row alert alert-primary mx-1 mt-4">
                                <h6 style="margin-bottom: 0px;">Asignacion de Procesos</h6>
                            </div>
                            <div class="row mx-1 mt-3">
                                <div class="col-3">
                                    <b>Fecha: </b>{{fechaAsignacionProcesos}} 
                                </div>
                                <div class="col-2">
                                    <b>Usuario: </b>{{usuarioAsignacionProcesos}} 
                                </div>
                            </div>
                            <div class="row mx-1">
                                <div class="col-10">
                                    <div>
                                        <table class="table text-center table-striped table-bordered table-sm">
                                            <thead>
                                                <tr>
                                                    <th width="10%"><b>Proceso</b></th>
                                                    <th width="20%"><b>Email</b></th>
                                                    <th width="50%"><b>Observacion</b></th>
                                                    <th v-if="funcionalidadesBotones.añadirProcesosAsignacion && calculedRolId == 1"></th>
                                                </tr>
                                            </thead>
                                            <tbody v-for="asignacionProceso in asignacionProcesos">
                                                <tr v-if="asignacionProceso.IsNew == false">
                                                    <td>{{asignacionProceso.Proceso}}</td>
                                                    <td>{{asignacionProceso.Email}}</td>
                                                    <td>{{asignacionProceso.Observacion}}</td>
                                                    <td v-if="funcionalidadesBotones.añadirProcesosAsignacion && calculedRolId == 1">
                                                        <button class="btn btn-danger" v-on:click="VerifyIfAssignedProcessCanBeDeleted(asignacionProceso.Id)">
                                                            <i class="fa fa-times" aria-hidden="true" style="margin-left: -200%"></i>
                                                        </button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="row mx-1" v-if="funcionalidadesBotones.añadirProcesosAsignacion && calculedRolId == 1">
                                <div class="col-6">
                                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalAdicionarProcesos">Adicionar Procesos</button>
                                </div>
                            </div>
                        </div>
                        <div v-if="(respuestasProcesos.length > 0 || infoPQRSEstadoID >= 2)  && tipoPQRS != 5 && infoPQRSEstadoID !== 13">
                            <div class="row alert alert-primary mx-1 mt-4">
                                <h6 style="margin-bottom: 0px;">Respuestas procesos</h6>
                            </div>
                            <div class="row mx-1 mt-3" v-if="funcionalidadesBotones.adicionarRespuestasProcesos">
                                <div class="col-6" v-if="infoPQRSIsInvolucred">
                                    <button type="button" class="btn btn-primary" @click="MostrarAgregarRespuestaProceso()">Añadir respuesta</button>
                                </div>
                            </div>
                            <hr />
                            <div v-for="respuestaProceso in respuestasProcesos">
                                <div class="border py-2">
                                    <div class="row mx-1">
                                        <span class="col-4"><b>Proceso respuesta: </b> {{respuestaProceso.Proceso}}</span>
                                        <span class="col-4"><b>Usuario: </b>{{respuestaProceso.Usuario}}</span>
                                        <span class="col-4"><b>Fecha: </b>{{respuestaProceso.Fecha.replace("T"," ").substring(0,16)}}</span>
                                    </div>
                                    <div class="row mx-1">
                                        <span class="col-4"><b>Respuesta: </b> {{respuestaProceso.Respuesta}}</span>
                                    </div>
                                    <div class="row mx-1">
                                        <div class="col-6">
                                            <table class="table table-bordered" v-if="respuestaProceso.Archivos !== null">
                                                <tr v-for="archivo in respuestaProceso.Archivos">
                                                    <td>{{archivo.FileName}}</td>
                                                    <td>
                                                        <button 
                                                            type="button" 
                                                            class="btn fa fa-download" 
                                                            @click="DescargarArchivo(archivo.FileName, archivo.Path)"
                                                            >
                                                        </button></a>
                                                        <button v-if="funcionalidadesBotones.eliminarArchivosRespuestas && archivo.CanBeDeleted"
                                                            type="button" 
                                                            class="btn fa fa-trash ml-3" 
                                                            @click ="PreguntaBorrarArchivoFisico(archivo.FileName, archivo.Path, archivo.IdArchivo, 6)"
                                                            >
                                                        </button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    
                                    </div>
                                    <hr v-if="respuestaProceso.Aclaracion" class="mx-3" />
                                    <div class="row mx-1" v-if="respuestaProceso.Aclaracion">
                                        <span class="col-4"><b>Solicitud de información adicional: </b>{{respuestaProceso.InformacionAclaracion}}</span>
                                        <span class="col-4"><b>Usuario: </b>{{respuestaProceso.UsuarioAclaracion}} </span>
                                        <span class="col-4"><b>Fecha: </b>{{respuestaProceso.Fecha.replace("T", " ").substring(0,16)}} </span>
                                    </div>
                                    <div class="row mx-1">
                                        <div class="col-2">
                                            <button 
                                                v-if="funcionalidadesBotones.solicitarInformacionProcesosAsignados && respuestaProceso.Aclaracion == false && calculedRolId == 1 && infoPQRSEstadoID <= 3" 
                                                type="button" class="btn btn-primary"  
                                                v-on:click="MostrarModalSolicitarInformacionProcesos(respuestaProceso.IdProceso, respuestaProceso.Proceso, respuestaProceso.Email, respuestaProceso.Id)">
                                                <i class="fa fa-hand-paper-o" aria-hidden="true"></i>
                                                <span class="ml-3">Solicitar mas información</span>
                                            </button>
                                        </div>
                                    </div>

                                    <!-- Imprimimos todos los hijos -->
                                    <div v-if="respuestaProceso.hijos != null">
                                        <div v-for="hijo in respuestaProceso.hijos" class="ml-4">
                                            <hr class="mx-3"/>
                                            <div class="border-left py-2 ml-3">
                                                <div class="row mx-1">
                                                    <span class="col-4"><b>Proceso respuesta: </b> {{hijo.Proceso}}</span>
                                                    <span class="col-4"><b>Usuario: </b>{{hijo.Usuario}}</span>
                                                    <span class="col-4"><b>Fecha: </b>{{hijo.Fecha.replace("T"," ").substring(0,16)}}</span>
                                                </div>
                                                <div class="row mx-1">
                                                    <span class="col-4"><b>Respuesta: </b> {{hijo.Respuesta}}</span>
                                                </div>
                                                <div class="row mx-1">
                                                    <div class="col-6">
                                                        <table class="table table-bordered" v-if="hijo.Archivos !== null">
                                                            <tr v-for="archivoHijo in hijo.Archivos">
                                                                <td>{{archivoHijo.FileName}}</td>
                                                                <td>
                                                                    <button 
                                                                        type="button" 
                                                                        class="btn fa fa-download" 
                                                                        @click="DescargarArchivo(archivoHijo.FileName, archivoHijo.Path)"
                                                                        >
                                                                    </button></a>
                                                                    <button v-if="funcionalidadesBotones.eliminarArchivosRespuestas && archivoHijo.CanBeDeleted"
                                                                        type="button" 
                                                                        class="btn fa fa-trash ml-3" 
                                                                        @click ="PreguntaBorrarArchivoFisico(archivoHijo.FileName, archivoHijo.Path, archivoHijo.IdArchivo, 6)"
                                                                        >
                                                                    </button>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                    
                                                </div>
                                                <div class="row mx-1" v-if="respuestaProceso.Aclaracion">
                                                    <span class="col-4"><b>Solicitud de información adicional: </b>{{hijo.InformacionAclaracion}}</span>
                                                    <span class="col-4"><b>Usuario: </b>{{hijo.UsuarioAclaracion}} </span>
                                                    <span class="col-4"><b>Fecha: </b>{{hijo.Fecha.replace("T", " ").substring(0,16)}} </span>
                                                </div>
                                                <div class="row mx-1">
                                                    <div class="col-2">
                                                        <button 
                                                            v-if="funcionalidadesBotones.solicitarInformacionProcesosAsignados && hijo.Aclaracion == false && calculedRolId == 1" 
                                                            type="button" class="btn btn-primary"  
                                                            v-on:click="MostrarModalSolicitarInformacionProcesos(hijo.IdProceso, hijo.Proceso, hijo.Email, hijo.Id)">
                                                            <i class="fa fa-hand-paper-o" aria-hidden="true"></i>
                                                            <span class="ml-3">Solicitar mas información</span>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <hr />
                            </div>
                        </div>
                        <div v-if="infoPQRSEstadoID >= 3 && tipoPQRS == 2 ">
                            <div class="row alert alert-primary mx-1 mt-4 py-2 d-flex justify-content-between">
                                <h6 style="margin-bottom: 0px;" class="align-self-center">Reclamo Procedente</h6>
                                <button v-if="(esProcedente == null || esProcedente == undefined) && calculedRolId == 1" class="btn btn-primary" style="padding-right: 16px !important;" @click="ReclamoProcedente()" type="button">Indicar procedencia</button> 
                                <button v-if="esProcedente === true" class="btn btn-primary" style="padding-right: 16px !important;" @click="MostrarAdicionarProcesosProcedencia()" type="button">Adicionar Procesos</button> 
                            </div>
                            <div v-if="esProcedente !== null">
                                <div class="row mx-1 mt-3">
                                    <div class="col-3">
                                        <span><b>Es Procedente ? </b>
                                            <span v-if="esProcedente === true">
                                                Si
                                            </span>
                                            <span v-else>
                                                No
                                            </span>
                                        </span>
                                    </div>
                                </div>
                                <div class="row mx-1 mt-3">
                                    <div class="col-10">
                                        <b>Descripcion Procedencia</b>
                                        <textarea disabled="disabled" class="form-control" rows="3" v-model="infoPQRSDescripcionProcedencia"></textarea>
                                    </div>
                                </div>
                                <div class="row mx-1 mt-3" v-if="esProcedente === true">
                                    <div class="col-12" v-if="infoPQRSOrdenGarantiaOMejora !== ''">
                                        <span><b>Tipo de orden</b>: {{ infoPQRSOrdenGarantiaOMejora }}</span>
                                    </div>
                                </div>
                                <hr />
                                <div v-if="esProcedente === true" class="mx-1">
                                    <div class="col-12">
                                        <table class="table table-striped table-bordered">
                                            <thead> 
                                                <tr>
                                                    <th width="10%">Proceso</th>
                                                    <th width="20%">Descripcion</th>
                                                    <th width="20%">Email</th>
                                                    <th width="45%">Comentario</th>
                                                    <th class="text-center" width="5%">-</th>
                                                </tr>
                                            </thead>
                                            <tbody v-for="noConformidad in noConformidades">
                                                <tr>
                                                    <td>{{noConformidad.Proceso}}</td>
                                                    <td>{{noConformidad.DescripcionNoConformidad}}</td>
                                                    <td>{{noConformidad.Email}}</td>
                                                    <td>{{noConformidad.Comentario}}</td>
                                                    <td class="text-center">
                                                        <button @click="mostrarModalEditarNC(noConformidad)" class="btn btn-sm btn-primary">Editar</button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="row mx-1 mt-3" v-if="esProcedente === true && infoPQRSIdFuenteReclamo == 5">
                                <div id="DinamycChangeHallazgos" class="col-md-12 mt-3 border p-3">
                                <div class="box-header border-bottom border-primary Comentario" style="z-index: 2;">
                                <span>Hallazgos marcados como <b>NO</b> procedentes</span>
                                <div class="col-md-12 mt-3">
                                    <table class="col-md-12">
                                        <thead>
                                            <tr>
                                                <th width="20%">Fecha</th>
                                                <th width="70%">Título</th>
                                                <th width="5%"></th>
                                                <td width="5%"></td>
                                            </tr>
                                        </thead>
                                    </table></div>
                                 </div>
                                    <div v-for="hallazgo in lisHallazgos" v-if="lisHallazgos.length > 0">
                             <div v-if="hallazgo.Padre == '0' && hallazgo.NoEsProcedente === true" class="col-md-12 Comentario" style="padding-top: 6x;padding-left: 0px;padding-right: 0px;" :id="'ParteHallazgoObra' + hallazgo.Nivel"><div :id="'headerHallazgoObra' + hallazgo.Nivel" class="box box-primary"><div class="box-header border-bottom border-primary" style="z-index: 2;">
                                    <table class="col-md-12 table-sm">
                                        <tr>
                                            <td width="20%">
                                                {{hallazgo.Fecha.substring(0, 10)}}
                                            </td>
                                            <td width="70%">
                                                {{hallazgo.Titulo}}
                                            </td>
                                            <td width="3%">
                                                <div class="col-md-12" style="padding-bottom: 4px;">
                                                    <button :id="'collapseHallazgosMarcadosNoProcedentes' + hallazgo.Nivel" onclick="CollapseHallazgosMarcadosNoProcedentes(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 2px;">
                                                        <span class="fa fa-angle-double-down"></span>
                                                    </button>
                                                 </div>
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                    <div :id="'bodyHallazgosMarcadosNoProcedentes' + hallazgo.Nivel" class="box-body" style="display: none;margin-left: 10px;margin-right: 10px; ">
                                     <div class="row item" :id="hallazgo.Nivel"><span style="font-size: 10px;" class="mt-2">Observacion</span><textarea class="form-control col-sm-12 SegLogistico" rows="2" disabled v-model="hallazgo.Comentario"></textarea></div>
                            
                                            <!-- Aqui van los anexos -->
                                        <div class="row item" v-if="hallazgo.Anexos.length > 0"><table class="col-md-12 table-sm table-bordered">
                                    <tr v-for="ane in hallazgo.Anexos">
                                        <td width="10%" >Anexo</td>
                                        <td width="80%">{{ane.nombre}}</td>
                                        <td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" @click="DescargarArchivo('', ane.ruta+ane.nombre)"> </button></td>
                                    </tr>
                                </table></div>

                                        <div v-if="hallazgo.Padre != '0'" class="row item" :id="hallazgo.Nivel"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">{{hallazgo.Fecha.substring(0, 10)}}</td><td width="15%"><span v-if="hallazgo.TipoConsideracionObservacion">Correctivo para la orden</span><span v-else>Mejora en el proceso</span></td><td width="33%">{{hallazgo.Comentario}}</td><td width="10%">{{hallazgo.FecDespacho.substring(0, 10)}}</td><td width="10%">{{hallazgo.Estado}}</td><td colspan = "2" width="20%">{{hallazgo.Usuario}}</td></tbody></table></div></div>
                                    </div>

                                    </div>
                             </div>
                           </div>
                                </div>

                                </div>
                                <div class="row mx-1 mt-3" v-if="infoPQRSOrdenProcedente !== ''">
                                    <div class="col-12">
                                        <span><b>Orden Procedente: {{ infoPQRSOrdenProcedente }} </b></span>
                                    </div>
                                </div>
                                <div class="row mx-1 mt-3" v-else>
                                    <div class="col-12">
                                        <span><b>Orden Sin Generar</b></span>
                                    </div>
                                    <div class="col-2" v-if="infoPQRSEstadoID == 4">
                                        <button v-if="calculedRolId == 1" class="btn-success btn" @click="GenerarOrdenMostrarModal()">Generar Orden</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div v-if="infoPQRSEstadoID >= 5 && esProcedente == 1 && tipoPQRS == 2 ">
                            <div class="row alert alert-primary mx-1 mt-4 py-2 d-flex justify-content-between">
                                <h6 style="margin-bottom: 0px;" class="align-self-center">Listados y Planos</h6>
                                <div>
                                    <button v-if="infoPQRSEstadoID == 5 && (debeCargarListadosOPlanosOArmado.listados.acero == true || debeCargarListadosOPlanosOArmado.listados.aluminio == true
                            || debeCargarListadosOPlanosOArmado.planos.acero == true || debeCargarListadosOPlanosOArmado.planos.aluminio == true 
                                        || debeCargarListadosOPlanosOArmado.armado.acero == true || debeCargarListadosOPlanosOArmado.armado.aluminio == true)" class="btn btn-primary mr-3" style="padding-right: 16px !important;" @click="MostrarListadoRequerido(true)" type="button">Cargar Listados/Planos</button> 
                                <button v-if="infoPQRSEstadoID == 5 && calculedRolId == 1" class="btn btn-success" style="padding-right: 16px !important;" @click="PreguntarAvanzarEstadoListadosCompletos()" type="button">Listados Completos</button> 
                                </div>
                            </div>
                            <div class="border py-3">
                                                        
                            <div v-if="descripcionListado !== ''">
                                <div class="col-12">
                                    <h6><b>No requiere listados</b></h6>
                                </div>
                                <div class="col-2">
                                    <b>Descripción</b>
                                </div>
                                <div class="col-10">
                                    <textarea disabled="disabled" class="form-control" cols="5" rows="4" v-model="descripcionListado"></textarea>
                                </div>
                            </div>
                            <div v-for="listadoRequerido in listadosRequeridos.listadosPlanos"  v-if="descripcionListado == ''">
                                        <div class="row mt-2" v-if="listadoRequerido.TipoCargue == 'Listados'">
								            <div class="col-1">
                                                <input type="checkbox" v-model="listadoRequerido.Completos" class="form-control" disabled="disabled"/>
								            </div>
                                            <div class="col-3">
                                                <b>{{ listadoRequerido.Correo }} </b>
                                            </div>
                                            <div class="col-1">
                                                {{ listadoRequerido.TipoCargue }}
                                            </div>
                                            <div class="col-1">
                                                {{ listadoRequerido.Tipo }}
                                            </div>
                                            <div class="col-4">
                                                <textarea v-model="listadoRequerido.Comentario" class="form-control" disabled="disabled"></textarea>
                                            </div>
                                        </div>
                                    </div>
                            <div class="mx-1" v-if="descripcionListado == ''">
                                <hr class="mx-2"/>
                                <b class="col-1">Listados Cargados</b>
                                <div class="col-12">
                                    <table class="table table-bordered table-striped text-center">
                                        <thead>
                                            <tr>
                                                <th width="10%">Tipo</th>
                                                <th width="30%">Archivo</th>
                                                <th width="30%">Usuario</th>
                                                <th width="10%">Mail</th>
                                                <th width="10%">Fecha</th>
                                                <th width="10%">Acciones</th>
                                            </tr>
                                        </thead>
                                        <tbody v-for="listado in listados">
                                            <tr>
                                                <td>{{listado.Tipo}}</td>
                                                <td>{{listado.FileName}}</td>
                                                <td>{{listado.UsuarioArchivo}}</td>
                                                <td>{{listado.Mail}}</td>
                                                <td>{{listado.Fecha}}</td>
                                                <td>
                                                    <button 
                                                        type="button" 
                                                        class="btn fa fa-download"
                                                        @click="DescargarArchivo(listado.FileName, listado.Path)"
                                                        >
                                                    </button></a>
                                                    <button v-if="funcionalidadesBotones.eliminarArchivosListados && listado.CanBeDeleted"
                                                        type="button" 
                                                        class="btn fa fa-trash ml-3" 
                                                        @click ="PreguntaBorrarArchivoFisico(listado.FileName, listado.Path, listado.IdArchivo, 4)"
                                                        >
                                                    </button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                                </div>
                            <hr />
                            <div class="border py-3">                            
                            <div class="" v-if="descripcionPlanos !== ''">
                                <div class="col-12">
                                    <h6><b>No requiere Planos</b></h6>
                                </div>
                                <div class="col-2">
                                    <b>Descripción</b> 
                                </div>
                                <div class="col-10">
                                    <textarea disabled="disabled" class="form-control" cols="5" rows="4" v-model="descripcionPlanos"></textarea>
                                </div>
                            </div>
                            <div v-for="listadoRequerido in listadosRequeridos.listadosPlanos" v-if="descripcionPlanos == ''">
                                    <div class="row mt-2"  v-if="listadoRequerido.TipoCargue == 'Planos'">
								        <div class="col-1">
                                            <input type="checkbox" v-model="listadoRequerido.Completos" class="form-control" disabled="disabled"/>
								        </div>
                                        <div class="col-3">
                                            <b>{{ listadoRequerido.Correo }} </b>
                                        </div>
                                        <div class="col-1">
                                            {{ listadoRequerido.TipoCargue }}
                                        </div>
                                        <div class="col-1">
                                            {{ listadoRequerido.Tipo }}
                                        </div>
                                        <div class="col-4">
                                            <textarea v-model="listadoRequerido.Comentario" class="form-control" disabled="disabled"></textarea>
                                        </div>
                                    </div>
                                </div>
                            <div class="mx-1" v-if="descripcionPlanos == ''">
                                <hr class="mx-2" />
                                <b class="col-2">Planos Cargados</b>
                                <div class="col-12">
                                    <table class="table table-bordered table-striped text-center">
                                        <thead>
                                            <tr>
                                                <th width="10%">Tipo</th>
                                                <th width="30%">Archivo</th>
                                                <th width="40%">Usuario</th>
                                                <th width="10%">Mail</th>
                                                <th width="10%">Fecha</th>
                                                <th width="10%">Acciones</th>
                                            </tr>
                                        </thead>
                                        <tbody v-for="plano in planos">
                                            <tr>
                                                <td>{{plano.Tipo}}</td>
                                                <td>{{plano.FileName}}</td>
                                                <td>{{plano.UsuarioArchivo}}</td>
                                                <td>{{plano.Mail}}</td>
                                                <td>{{plano.Fecha}}</td>
                                                <td>
                                                    <button 
                                                        type="button" 
                                                        class="btn fa fa-download"
                                                        @click="DescargarArchivo(plano.FileName, plano.Path)"
                                                        >
                                                    </button></a>
                                                    <button v-if="funcionalidadesBotones.eliminarArchivosListados && plano.CanBeDeleted"
                                                        type="button" 
                                                        class="btn fa fa-trash ml-3" 
                                                        @click ="PreguntaBorrarArchivoFisico(plano.FileName, plano.Path, plano.IdArchivo, 7)"
                                                        >
                                                    </button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                            <hr />
                            <div class="border py-3">
                                                        
                            <div v-if="descripcionPlanoArmado !== ''">
                                <div class="col-12">
                                    <h6><b>No requiere plano de armado nuevo o actualizado</b></h6>
                                </div>
                                <div class="col-2">
                                    <b>Descripción</b>
                                </div>
                                <div class="col-10">
                                    <textarea disabled="disabled" class="form-control" cols="5" rows="4" v-model="descripcionPlanoArmado"></textarea>
                                </div>
                            </div>
                            <div v-for="listadoRequerido in listadosRequeridos.listadosPlanos"  v-if="descripcionPlanoArmado == ''">
                                        <div class="row mt-2" v-if="listadoRequerido.TipoCargue == 'Armado'">
								            <div class="col-1">
                                                <input type="checkbox" v-model="listadoRequerido.Completos" class="form-control" disabled="disabled"/>
								            </div>
                                            <div class="col-3">
                                                <b>{{ listadoRequerido.Correo }} </b>
                                            </div>
                                            <div class="col-1">
                                                {{ listadoRequerido.TipoCargue }}
                                            </div>
                                            <div class="col-1">
                                                {{ listadoRequerido.Tipo }}
                                            </div>
                                            <div class="col-4">
                                                <textarea v-model="listadoRequerido.Comentario" class="form-control" disabled="disabled"></textarea>
                                            </div>
                                        </div>
                                    </div>
                            <div class="mx-1" v-if="descripcionPlanoArmado == ''">
                                <hr class="mx-2"/>
                                <b class="col-1">Planos de armado cargados</b>
                                <div class="col-12">
                                    <table class="table table-bordered table-striped text-center">
                                        <thead>
                                            <tr>
                                                <th width="10%">Tipo</th>
                                                <th width="30%">Archivo</th>
                                                <th width="30%">Usuario</th>
                                                <th width="10%">Mail</th>
                                                <th width="10%">Fecha</th>
                                                <th width="10%">Acciones</th>
                                            </tr>
                                        </thead>
                                        <tbody v-for="listado in armado">
                                            <tr>
                                                <td>{{listado.Tipo}}</td>
                                                <td>{{listado.FileName}}</td>
                                                <td>{{listado.UsuarioArchivo}}</td>
                                                <td>{{listado.Mail}}</td>
                                                <td>{{listado.Fecha}}</td>
                                                <td>
                                                    <button 
                                                        type="button" 
                                                        class="btn fa fa-download"
                                                        @click="DescargarArchivo(listado.FileName, listado.Path)"
                                                        >
                                                    </button></a>
                                                    <button v-if="funcionalidadesBotones.eliminarArchivosListados && listado.CanBeDeleted"
                                                        type="button" 
                                                        class="btn fa fa-trash ml-3" 
                                                        @click ="PreguntaBorrarArchivoFisico(listado.FileName, listado.Path, listado.IdArchivo, 4)"
                                                        >
                                                    </button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                                </div>
                        </div>

                        <div v-if="tipoPQRS == 2 && esProcedente == 1 && infoPQRSEstadoID >= 5">
                            <div class="row alert alert-primary mx-1 mt-4 py-2 d-flex justify-content-between">
                                <h6 style="margin-bottom: 0px;" class="align-self-center">Producción</h6>
                                <button v-if="calculedRolId == 1 && infoPQRSEstadoID == 7" class="btn btn-primary" style="padding-right: 16px !important;" @click="PreguntarAvanzarEstadoProduccion()" type="button">Indicar Producción</button> 
                            </div>
                            <!--<div class="row mx-1" v-if="funcionalidadesBotones.editarFechasProduccion">
                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#ModalEditarProduccion">
                                        Editar Fechas
                                    </button>
                            </div>-->
                            <div>
                                <div class="row mx-1 mt-3" style="font-size: 14px !important;">
                                    <div class="col-3">
                                        <b>Fecha Req Alum</b> 
                                        <input type="date" v-model="fechaReqAlum" disabled="disabled" class="form-control " />
                                    </div>
                                    <div class="col-3">
                                        <b>Fecha Planeada Fabricacion</b>
                                        <input type="date" v-model="fechaPlanAlum" disabled="disabled" class="form-control " />
                                    </div>
                                    <div class="col-3">
                                        <b>Fecha Despacho Planeada</b> 
                                        <input type="date" v-model="fechaDespAlum" disabled="disabled" class="form-control " />
                                    </div>
                                </div>
                                <div class="row mx-1 mt-2" style="font-size: 14px !important;">
                                    <div class="col-3">
                                        <b>Fecha Req Acero</b> 
                                        <input type="date" v-model="fechaReqAcero" disabled="disabled" class="form-control " />
                                    </div>
                                    <div class="col-3 offset-3">
                                        <b>Fecha Despacho Real</b> 
                                        <input type="date" v-model="fechaDespAcero" disabled="disabled" class="form-control " />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                         <div v-if="tipoPQRS == 2 && esProcedente == 1 && infoPQRSEstadoID >= 8">
                            <div class="row alert alert-primary mx-1 mt-4 py-2 d-flex justify-content-between">
                                <h6 style="margin-bottom: 0px;" class="align-self-center">Comprobante en Obra</h6>
                                <button v-if="infoPQRSEstadoID == 8 || infoPQRSEstadoID == 12" class="btn btn-primary" style="padding-right: 16px !important;" @click="MostrarComprobanteObra()" type="button">Indicar Comprobante</button> 
                                </div>
                                <div class="row mx-1">
                                    <div class="col-2">
                                        <b>Archivos Cargados</b>
                                    </div>
                                    <div class="col-3 offset-4">
                                        <span class="font-weight-bold">Fecha Entrega en Obra</span>
                                        <input type="date" disabled="disabled" class="form-control" v-model="FechaEntregaObra" v-if="!EditingFechaEntregaObra"/>
                                        <input type="date" class="form-control" v-model="FechaEntregaObraEdit" v-else/>
                                        <button type="button" class="btn btn-sm btn-primary mt-1" v-if="!EditingFechaEntregaObra && infoPQRSEstadoID == 8 && calculedRolId == 1" @click="EditingFechaEntregaObra = true">
                                            <i class="fa fa-pencil" style="margin-left: -200%"></i>
                                        </button>
                                        <button  type="button" class="btn btn-sm btn-success mt-1" v-if="EditingFechaEntregaObra && infoPQRSEstadoID == 8 && calculedRolId == 1" @click="AskModifyDeliveryDate()">
                                            <i class="fa fa-save" style="margin-left: -200%"></i>
                                        </button>
                                        <button  type="button" class="btn btn-sm btn-danger mt-1 ml-1" v-if="EditingFechaEntregaObra && infoPQRSEstadoID == 8 && calculedRolId == 1" @click="EditingFechaEntregaObra = false">
                                            <i class="fa fa-times" style="margin-left: -200%"></i>
                                        </button>
                                    </div>
                                </div>
                            <div class="row mx-1">
                                <div class="col-2">
                                    <button type="button" v-if="funcionalidadesBotones.añadirArchivosComprobanteObra" class="btn btn-primary" data-toggle="modal" data-target="#adicionarArchivosComprobacionModal">
                                        Añadir archivos
                                    </button>
                                </div>
                            </div>
                            <div class="mx-1">
                                <div class="col-6">
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th>Archivo</th>
                                                <th>Descarga</th>
                                            </tr>
                                        </thead>
                                        <tbody v-for="archivoProduccion in archivosProduccion">
                                            <tr>
                                                <td>{{archivoProduccion.FileName}}</td>
                                                <td>
                                                    <button 
                                                        type="button" 
                                                        class="btn fa fa-download" 
                                                        @click="DescargarArchivo(archivoProduccion.FileName, archivoProduccion.Path)"
                                                        >
                                                    </button></a>
                                                    <button v-if="funcionalidadesBotones.eliminarArchivosComprobacion && archivoProduccion.CanBeDeleted"
                                                        type="button" 
                                                        class="btn fa fa-trash ml-3" 
                                                        @click ="PreguntaBorrarArchivoFisico(archivoProduccion.FileName, archivoProduccion.Path, archivoProduccion.IdArchivo, 5)"
                                                        >
                                                    </button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                         </div>

                        <div v-if="((tipoPQRS == 2 && infoPQRSEstadoID >= 4) || (tipoPQRS == 1 && infoPQRSEstadoID >= 3)) && infoPQRSEstadoID !== 13 && infoPQRSEstadoID !== 11">
                            
                            <div class="row alert alert-primary mx-1 mt-4 py-2 d-flex justify-content-between">
                                <h6 style="margin-bottom: 0px;" class="align-self-center">Plan de Accion y Cierre</h6>
                                 <button class="btn btn-primary" style="padding-right: 16px !important;" @click="MostrarModalCrearPlanAccionQuejas()" type="button">Crear Plan de Acción</button> 
                            </div>
                            <div class="col-12">
                                <span v-if="lastDatePlanAccion != null" style="font-size: 13px;">
                                    <b>Fecha: </b> {{lastDatePlanAccion.substring(0, 10)}}
                                </span>
                                <table class="table table-striped table-sm text-center">
                                    <thead>
                                        <tr>
                                            <th>Proceso</th>
                                            <th>Descripcion</th>
                                            <th>Comentario</th>
                                            <th>Plan de Acción</th>
                                            <th>Plan de Acción Desc</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody v-for="noConformidad in noConformidades">
                                        <tr>
                                            <td>{{noConformidad.Proceso}}</td>
                                            <td>{{noConformidad.DescripcionNoConformidad}}</td>
                                            <td>{{noConformidad.Comentario}}</td>
                                            <td>{{noConformidad.PlanAccion}}</td>
                                            <td>{{noConformidad.PlanAccionDescripcion}}</td>
                                            <td class="text-center">
                                                <button v-if="(infoPQRSEstadoID >= 4 || (tipoPQRS == 1 && infoPQRSEstadoID >= 3)) && infoPQRSEstadoID !== 13 && infoPQRSEstadoID !== 11" class="btn btn-primary mr-2" @click="MostrarCierreObra(noConformidad)" type="button">
                                                    Plan
                                                </button>
                                                <button v-if="noConformidad.PlanAccion != ''" class="btn btn-primary" @click="ShowActionPlanDetails(noConformidad)" type="button">
                                                    Detalles
                                                </button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <!--<div class="row mx-1">
                                <div class="col-2">
                                    <b>Archivos Cargados</b>
                                </div>
                            </div>
                            <div class="row mx-1">
                                <div class="col-2" v-if="funcionalidadesBotones.añadirArchivosCierre">
                                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#adicionarArchivosCierreModal">
                                        Añadir archivos
                                    </button>
                                </div>
                            </div>
                            <div class="mx-1">
                                <div class="col-6">
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th>Archivo</th>
                                                <th>Descarga</th>
                                            </tr>
                                        </thead>
                                        <tbody v-for="archivoCierre in archivosCierre">
                                            <tr>
                                                <td>{{archivoCierre.FileName}}</td>
                                                <td>
                                                    <button 
                                                        type="button" 
                                                        class="btn fa fa-download" 
                                                        @click="DescargarArchivo(archivoCierre.FileName, archivoCierre.Path)"
                                                        >
                                                    </button></a>
                                                    <button v-if="funcionalidadesBotones.eliminarArchivosCierre && archivoCierre.CanBeDeleted"
                                                        type="button" 
                                                        class="btn fa fa-trash ml-3" 
                                                        @click ="PreguntaBorrarArchivoFisico(archivoCierre.FileName, archivoCierre.Path, archivoCierre.IdArchivo, 2)"
                                                        >
                                                    </button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>-->
                        </div>
                    </div>
                </div>
            </div>
            <div class="card" id="Novedades" >
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseNovedades">Novedades</a>
				</div>
				<div id="collapseNovedades" class="collapse show" data-parent="#accordion">
                    <div class="container-fluid">
                        <div class="row alert alert-primary mx-1 mt-4">
                                <h6 style="margin-bottom: 0px;">Novedades PQRS</h6>
                            </div>
                            <div class="row mx-1 mt-3">                          
							    <div class="col-4" style="display: inline-table">
									    <button v-if="calculedRolId == 1 || infoPQRSIsInvolucred" type="button"  class="btn btn-primary controlcambiosDFT_Crear" data-toggle="modal" @click="ControlCambioShow('Novedades PQRS',0,0,'')">
										    <i class="fa fa-list pr-3">  </i>
										    Añadir Novedad
									    </button>

							    </div>                            
                            </div>
                            <div class="box-header border-bottom border-primary Comentario" style="z-index: 2;">
                                <table class="col-md-12 table-sm">
                                    <thead>
                                        <tr>
                                            <th width="12%">FECHA</th>
                                            <th width="45%">TITULO</th>
                                            <th width="10%">EVENTO</th>
                                            <th width="15%">ESTADO</th>
                                            <th width="15%">USUARIO</th>
                                            <td width="3%"></td>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                            <div v-for="bitacora in bitacoraPQRS" class="col-md-12 Comentario" style="padding-top: 6px;" :id="'Parte' + bitacora.Nivel">
                                <div :id="'header' + bitacora.Nivel" class="box box-primary">
                                    <div class="box-header border-bottom border-primary" style="z-index: 2;">
                                        <table class="col-md-12 table-sm">
                                            <tr>
                                                <td width="12%">{{bitacora.Fecha.substring(0, 10)}}</td>
                                                <td width="45%">{{bitacora.Titulo}}</td>
                                                <td width="10%">{{bitacora.EventoDescripcion}}</td>
                                                <td width="15%">{{bitacora.Estado}}</td>
                                                <td width="15%">{{bitacora.Usuario}}</td>
                                                <td width="3%">
                                                    <div class="col-md-12" style="padding-bottom: 4px;">
                                                        <button :id="'collapse' + bitacora.Nivel" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 2px;">
                                                            <span class="fa fa-angle-double-down"></span>
                                                        </button>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                <div :id="'body' + bitacora.Nivel" class="box-body" style="display: none;padding-top: 8px;margin-left: 10px;margin-right: 10px; ">
                                    <div class="row item" :id="bitacora.Nivel">
                                        <label style="font-size: 10px;">Observacion</label>
                                        <textarea class="form-control col-sm-12 Observacion" rows="2" disabled v-model="bitacora.Comentario"></textarea>
                                    </div>

                                    <div class="row item" v-if="bitacora.Anexos.length > 0">
                                        <table class="col-md-12 table-sm table-bordered">
                                            <tr v-for="ane in bitacora.Anexos">
                                                <td width="10%" >Anexo</td>
                                                <td width="80%">{{ane.nombre}}</td>
                                                <td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" @click="DescargarArchivo(ane.nombre, ane.ruta)"> </button></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                                </div>
                        </div>
                    </div>
                </div>
            <div class="card" id="Comunicados" >
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseComunicados">Comunicados</a>
				</div>
				<div id="collapseComunicados" class="collapse show" data-parent="#accordion">
                    <div class="container-fluid pb-3">
                        <div class="row alert alert-primary mx-1 mt-4 py-2 d-flex justify-content-between">
                            <h6 style="margin-bottom: 0px;" class="align-self-center">Comunicación con el Cliente</h6>
                            <button v-if="((idPQRS !== 0 && infoPQRSEstadoID >= 3) || (infoPQRSEstadoID >= 1 && tipoPQRS == 5)) && infoPQRSEstadoID !== 13 && calculedRolId == 1" class="btn btn-primary" @click="MostrarEnviarComunicadoCliente()" style="padding-right: 16px !important;" type="button">Enviar Comunicado</button>
                        </div>
                        <hr />
                        <div v-for="comunicado in comunicados">
                                <div class="border py-2">
                                    <div class="row mx-1">
                                        <span class="col-12"><b>Para: </b> {{comunicado.MessageTo}}</span>
                                    </div>
                                    <div class="row mx-1">
                                        <span class="col-12"><b>Con copia: </b>{{comunicado.MessageCc}}</span>
                                    </div>
                                    <div class="row mx-1">
                                        <span class="col-12"><b>Fecha: </b>{{comunicado.Fecha}}</span>
                                    </div>
                                    <div class="row mx-1">
                                        <span class="col-4"><b>Se envió encuesta de satisfacción: </b>
                                            <span v-if="comunicado.SeEnvioEncuesta == null || comunicado.SeEnvioEncuesta == undefined || comunicado.SeEnvioEncuesta == 0">No</span>
                                            <span v-else>Si</span>
                                        </span>
                                    </div>
                                    <div class="row mx-1">
                                        <span class="col-4"><b>Mensaje:</b></span>
                                    </div>
                                    <div class="row mx-3">
                                        <textarea disabled="disabled" v-model="comunicado.Mensaje" class="form-control" rows="3"></textarea>
                                    </div>
                                    <div class="row mx-1" v-if="comunicado.Archivos !== null">
                                        <div class="col-6 ml-4">
                                            <h6 class="mt-2 font-weight-bold">Archivos Enviados</h6>
                                            <table class="table table-bordered">
                                                <tr v-for="archivo in comunicado.Archivos">
                                                    <td>{{archivo.FileName}}</td>
                                                    <td>
                                                        <a :href="archivo.Path" target="_blank">
                                                        <button 
                                                            type="button" 
                                                            class="btn fa fa-download" 
                                                            >
                                                        </button></a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    
                                    </div>
                                </div>
                                <hr />
                            </div>
                    </div>
                </div>
            </div>
         </div>
     </div>
         </div>
    <style>
        option {
            padding: 5px !important;
        }

        .wfull {
            width: 100% !important;
        }

        .filter-column {
            display: flex;
            align-items: center;
            justify-items: center;
            position: relative;
        }

        .filter-icon {
            cursor: pointer;
            display: inline-block;
            width: 15px !important;
            height: 15px !important;
            margin-right: 4px;
            background-color: transparent !important;
            border: none !important;
        }

        .filter-menu {
            padding: 15px 30px;
            z-index: 1;
            position: absolute;
            top: 30px;
            width: 200px;
            background-color: #fff;
            border: 1px solid #e0e0e0;
        }

        .customize-table {
            --easy-table-border: 1px solid #445269;
            --easy-table-row-border: 1px solid #a0afc6;
            --easy-table-header-background-color: #007bff;
        }

        .w-full {
            width: 100% !important;
        }

        .modal-body .row label {
            margin-top: 8% !important;
            font-weight: bold !important;
        }

        @media (min-width: 992px) {
            .modal-dialog {
                width: 800px !important;
                max-width: 100% !important;
            }
        }


        /*Estilos drag and drop files*/

        .main {
            display: flex;
            flex-grow: 1;
            align-items: center;
            height: 24vh;
            justify-content: center;
            text-align: center;
            margin: 1.5rem;
        }

        .dropzone-container {
            padding: 1rem;
            background: #f7fafc;
            border: 1px solid #e2e8f0;
        }

        .hidden-input {
            opacity: 0;
            overflow: hidden;
            position: absolute;
            width: 1px;
            height: 1px;
        }

        .file-label {
            font-size: 20px;
            display: block;
            cursor: pointer;
        }

        .filter-option-inner-inner {
            font-size: 0.8rem !important;
        }

        .preview-container {
            display: flex;
            margin-top: 2rem;
            max-width: 600px !important;
            justify-content: center;
        }

            .preview-container > * {
                flex: 1 1 1;
            }

        .preview-card {
            display: flex;
            border: 1px solid #a2a2a2;
            padding: 5px;
            margin-left: 5px;
            width: 94px !important;
            flex-direction: column !important;
        }

            .preview-card > div {
                display: flex !important;
                flex-direction: column !important;
                justify-content: center;
                align-items: center;
            }

                .preview-card > div > p {
                    width: 50px !important
                }

        .preview-img {
            width: 50px;
            height: 50px;
            border-radius: 5px;
            border: 1px solid #a2a2a2;
            background-color: #a2a2a2;
        }

        /*Estilos para linea de tiempo*/

        .container-timeline {
            background: #232931;
            width: 100%;
            height: 500px;
            margin: 0 auto;
            position: relative;
            box-shadow: 2px 5px 20px rgba(119, 119, 119, 0.5);
        }

        .leftbox {
            top: 0;
            left: 0;
            position: absolute;
            width: 15%;
            height: 110%;
            padding: 0em 17rem 0em 0em;
            overflow-y: auto;
            overflow-x: hidden;
            max-height: 500px !important;
        }

        .rightbox {
            padding: 0em 31rem 0em 0em;
            height: 100%;
            background-color: white !important;
            position: absolute;
            left: 35%;
            width: 10%;
            transition: opacity 2s ease-in-out;
            opacity: 0;
        }

        .statusShown {
            opacity: 1
        }

        .rb-container {
            font-family: "PT Sans", sans-serif;
            width: 50%;
            margin: auto;
            display: block;
            position: relative;
        }

            .rb-container ul.rb {
                margin: 1em 0;
                padding: 0;
                display: inline-block;
            }

                .rb-container ul.rb li {
                    list-style: none;
                    margin: auto;
                    margin-left: 5em;
                    min-height: 50px;
                    border-left: 1px dashed #fff !important;
                    padding: 0 0 40px 30px;
                    position: relative;
                    cursor: pointer !important;
                }

                    .rb-container ul.rb li:last-child {
                        border-left: 0;
                    }

                    .rb-container ul.rb li::before {
                        position: absolute;
                        left: -18px;
                        top: 0px;
                        content: " ";
                        border: 8px solid rgba(255, 255, 255, 1);
                        border-radius: 500%;
                        background: #50d890;
                        height: 34px;
                        width: 34px;
                        transition: all 500ms ease-in-out;
                    }

                    .rb-container ul.rb li:hover::before {
                        border-color: #232931;
                        transition: all 1000ms ease-in-out;
                    }

        ul.rb li .timestamp {
            color: #50d890;
            position: relative;
            width: 160px;
            font-size: 12px;
        }

        .item-title {
            color: #fff;
        }


        [data-badge]:after {
            content: attr(data-badge);
            /*position: absolute;
                 top: -10px;
                   right: -10px;*/
            font-size: 10px;
            background: red;
            color: white;
            width: 100px;
            height: 20px;
            text-align: center;
            line-height: 18px;
            border-radius: 50%;
        }
    </style>
</asp:Content>

