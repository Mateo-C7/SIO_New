<%@ Page Title="Creacion PQRS" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormPQRS.aspx.cs" Inherits="SIO.FormPQRS" Culture="en-US" UICulture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
    <script type="text/javascript" src="Scripts/PopperRefactored/Popper14.js"></script>

    <script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
    <%--	<script type="text/javascript" src="Scripts/jquery.i18n.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.messagestore.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.fallbacks.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.parser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.emitter.js"></script>--%>
    <%--<script src="https://unpkg.com/vue@3/dist/vue.global.js" type="text/javascript"></script>--%>

    <script type="importmap">
			{ "imports": { "vue": "./Scripts/vue.esm-browser.js", 
                           "vue3-easy-data-table": "./Scripts/vue3-easy-data-table.umd.js", 
                            "vue-i18n" : "./Scripts/vue-i18n.esm-browser.js"

                         } 
            }
		</script>

    <script type="module" src="Scripts/formpqrs.js?v=20230809A"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="Scripts/select2.min.js"></script>
    <script type="module" src="Scripts/bootstrap-select.min.js"></script>
    <script type="text/javascript" src="Scripts/toastr.min.js"></script>
    <script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
    <script type="text/javascript" src="Scripts/moment.js?v=20200629"></script>

    <link rel="Stylesheet" href="Content/bootstrap.min.css" />
    <link rel="Stylesheet" href="Content/SIO.css" />
    <link rel="stylesheet" href="Content/font-awesome.css" />
    <link rel="Stylesheet" href="Content/bootstrap-select.css" />
    <link rel="Stylesheet" href="Content/css/select2.min.css" />
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
        </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server" >
    <div id="loader" style="display: none">
        <h3>Procesando...</h3>
    </div>
    <div id="ohsnap"></div>

    <div class="container-fluid contenedor_fup" id="app">
        <div class="card p-4">
             <div class="card-body">
                  <div class="row">
                           <div class="btn-group col align-self-end" role="group" aria-label="Basic example">
				            <button @click="ChangeLanguage('es')" type="button" class="btn btn-secondary langes">
					            <img alt="español" src="Imagenes/colombia.png" /></button>
				            <button @click="ChangeLanguage('en')" type="button" class="btn btn-secondary langen">
					            <img alt="ingles" src="Imagenes/united-states.png" /></button>
				            <button @click="ChangeLanguage('pt')" type="button" class="btn btn-secondary langbr">
					            <img alt="portugues" src="Imagenes/brazil.png" /></button><h2>&nbsp;&nbsp;&nbsp;&nbsp;¡Atención Con5entidos!</h2>
			            </div>
                     </div>
                    <h5 class="card-title alert alert-primary">Crear PQRS</h5>
                    <br />                                  
                    <div class="row">
                        <div class="col-2 text-left"><label>{{ $t("tipo") }}</label> </div>
                        <div class="col-4">
                             <select @change="onChangeTipo"  class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" v-model="pqrs.TipoPQRSId">
                                       <option value="-1">Seleccionar Tipo</option>
                                        <option v-for="(item, index) in tipos" :value="item.Id" :key="index">
                                            {{ item.Descripcion }}
                                        </option>
                                    </select>
                        </div>
                        <div class="col-2 text-left" v-if="pqrs.TipoPQRSId == 2 || pqrs.TipoPQRSId == 1 || pqrs.TipoPQRSId == 3 || pqrs.TipoPQRSId == 5"><label>Orden Fabricación - FUP</label></div>
                        <div class="col-2"  v-if="pqrs.TipoPQRSId == 2  || pqrs.TipoPQRSId == 1 || pqrs.TipoPQRSId == 3 || pqrs.TipoPQRSId == 5 ">
                            <input id="txtIdOrden" type="text" v-model="selectedOrden" class="form-control  bg-warning text-dark" />
                        </div>
                        <div class="col-2"  v-if="pqrs.TipoPQRSId == 2  || pqrs.TipoPQRSId == 1 || pqrs.TipoPQRSId == 3 || pqrs.TipoPQRSId == 5 ">
                            <button v-if="fup.IdFup == undefined" id="btnBusOf" type="button" class="btn btn-primary" @click="ObtenerFUP"  data-toggle="tooltip" ><i class="fa fa-search"></i></button>
                            <button v-if="fup.IdFup !== undefined" type="button" class="btn btn-danger" @click="BorrarFup" data-toggle="tooltip"><i class="fa fa-times"></i></button>
                        </div>
                     
                    </div>
                    <div class="row" v-if="pqrs.TipoPQRSId == 1">
                         <div class="col-2 text-left"><label>Tipo de Queja</label> </div>
                        <div class="col-4">
                             <select   class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" v-model="pqrs.TipoSubPQRSId">
                                       <option value="-1">Seleccionar Tipo</option>
                                        <option v-for="(item, index) in subTipoQuejas" :value="item.Id" :key="index">
                                            {{ item.Descripcion }}
                                        </option>
                                    </select>
                        </div>

                    </div>
                 <div class="row" v-if="pqrs.TipoPQRSId == 3 || pqrs.TipoPQRSId == 6" >
                         <div class="col-2 text-left"><label>Tipo de Solicitud</label> </div>
                        <div class="col-4">
                             <select  class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" v-model="pqrs.TipoSubPQRSId">
                                       <option value="-1">Seleccionar Tipo</option>
                                        <option v-if="pqrs.TipoPQRSId == 3" v-for="(item, index) in subTipoSolicitudes" :value="item.Id" :key="index">
                                            {{ item.Descripcion }}
                                        </option>
                                        <option v-if="pqrs.TipoPQRSId == 6" v-for="(item, index) in subTipoSolicitudesTH" :value="item.Id" :key="index">
                                            {{ item.Descripcion }}
                                        </option>
                                    </select>
                        </div>

                    </div>
                    <br />
           
                   <h4 class="alert alert-primary" v-if="pqrs.TipoPQRSId != 4 && fup.IdFup != undefined && fup.IdFup != null" >Información FUP</h4>
                    <div class="row mt-4" v-if="pqrs.TipoPQRSId != 4 && fup.IdFup != undefined && fup.IdFup != null">
                
                        <div class="col-1">
                           <b>Pais </b> 
                        </div>
                        <div class="col-2">
                            <input class="form-control" disabled="disabled" v-model="fup.Pais" type="text" />
                        </div>
                        <div class="col-1">
                           <b> Ciudad</b>
                        </div>
                        <div class="col-2">
                            <input class="form-control" disabled="disabled" v-model="fup.Ciudad" type="text" />
                        </div>
                        <div class="col-1">
                           <b> Empresa</b>
                        </div>
                        <div class="col-5">
                            <input class="form-control" disabled="disabled" v-model="fup.Cliente" type="text" />
                        </div>
                    </div>
                    <div class="row" v-if="(pqrs.TipoPQRSId == 2 || pqrs.TipoPQRSId == 1 || pqrs.TipoPQRSId == 3 || pqrs.TipoPQRSId == 5) && fup.IdFup != undefined && fup.IdFup != null">
                        <div class="col-1">
                            <b>Contacto</b>
                        </div>
                        <div class="col-5">
                            <input class="form-control" disabled="disabled" v-model="fup.Contacto" type="text" />
                        </div>
                        <div class="col-1">
                            <b>Obra</b>
                        </div>
                        <div class="col-5">
                            <input class="form-control" disabled="disabled" v-model="fup.Obra" type="text" />
                        </div>
                    </div>
                 
                 
                 <!-- Modal -->
                <div class="modal fade" id="ModalPQRSCreado" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                  <div class="modal-dialog modal-sm" role="document">
                    <div class="modal-content">
                      <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Creación PQRS</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                        </button>
                      </div>
                      <div class="modal-body">
                          <div class="row mx-1">
                              <span class="col-12" style="font-size: 20px !important;">
                                  PQRS Creada exitosamente con Id: {{ idCreado }}
                              </span>
                          </div>
                        <div class="row mx-1 mt-3">
                            <a href="FormPQRSConsulta.aspx" class="btn btn-primary">Consultar PQRS</a>
                        </div>
                      </div>
                      <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                      </div>
                    </div>
                  </div>
                </div>
                 <div class="mt-4" v-if="(pqrs.TipoPQRSId == 2 && fup.IdFup != undefined && fup.IdFup != null) || (pqrs.TipoPQRSId != 2 && pqrs.TipoPQRSId != -1)">
                       <h5 class="card-title alert alert-primary">Otra Info</h5>
                    <br />
                       <div class="row">
                            <div class="col-2 text-left"><label>Fuente</label></div>
                             <div class="col-4">
                                    <select required class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline"
                                            v-model="pqrs.IdFuenteReclamo" >
                                        <option v-for="(item, index) in fuentes"
                                                :value="item.PQRSFuenteID"
                                                :key="index">
                                            {{ item.Descripcion }}
                                        </option>
                                    </select>
                            </div>
                           
                            <div class="col-2 text-left"><label>Tipo Contacto</label></div>
                             <div class="col-4">
                                    <select  class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline"
                                            v-model="pqrs.IdTipoFuenteReclamo" >
                                       
                                        <option value="-1" >SELECCIONAR</option>
                                        <option v-for="(itemTipo, index) in tipofuentes"
                                                :value="itemTipo.PQRSTipoFuenteID"
                                                :key="index">
                                            {{ itemTipo.Descripcion }}
                                        </option>
                                    </select>
                            </div>
                           
                           
                           <div class="col-2 text-left" v-if="(pqrs.IdFuenteReclamo == 6)"><label>Email Colaborador</label></div>
                            <div class="col-4" v-if="(pqrs.IdFuenteReclamo == 6)"><input type="text" v-model="pqrs.Colaborador" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" /></div>
                        </div>
                     
                       <div class="row">
                            <div class="col-2 text-left"><label>Descripción</label></div>
                            <div class="col-10" >
                                <textarea rows ="4" v-model="pqrs.Detalle" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" required v-model="pqrs.Detalle"></textarea>
                            </div>
                        </div>
                     <div class="row" v-if="pqrs.TipoPQRSId != 4 && fup.IdFup != undefined && fup.IdFup != null && pqrs.IdFuenteReclamo == 5">
                     <div id="DinamycChangeHallazgos" class="col-md-12 mt-3">
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

                         <div v-for="hallazgo in lisHallazgos">
                             <div v-if="hallazgo.Padre == '0'" class="col-md-12 Comentario" style="padding-top: 6x;padding-left: 0px;padding-right: 0px;" :id="'ParteHallazgoObra' + hallazgo.Nivel"><div :id="'headerHallazgoObra' + hallazgo.Nivel" class="box box-primary"><div class="box-header border-bottom border-primary" style="z-index: 2;">
                            <table class="col-md-12 table-sm"><tr><td width="5%"><input type="checkbox" class="form-control" v-model="hallazgo.Asociar"/></td><td width="12%">{{hallazgo.Fecha.substring(0, 10)}}</td><td width="27%">{{hallazgo.Titulo}}</td><td width="10%"><span v-if="hallazgo.SolucionadoEnObra">Sí</span><span v-else>No</span></td><td width="10%"><span v-if="hallazgo.GeneroCosto">Sí</span><span v-else>No</span></td><td width="15%">{{hallazgo.Estado}}</td><td width="12%">{{hallazgo.Usuario}}</td><td width="8%" class=text-right>{{hallazgo.HallazgoOrdenFabricacion}}</td>
                            <td width="3%"><div class="col-md-12" style="padding-bottom: 4px;"><button :id="'collapse' + hallazgo.Nivel" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 2px;"><span class="fa fa-angle-double-down"></span></button></div></td></tr></table></div>
                            <div :id="'body' + hallazgo.Nivel" class="box-body" style="display: none;padding-top: 8px;margin-left: 10px;margin-right: 10px; ">
                             <div class="row item" :id="hallazgo.Nivel"><label style="font-size: 10px;">Observacion</label><textarea class="form-control col-sm-12 SegLogistico" rows="2" disabled v-model="hallazgo.Comentario"></textarea></div>
                            
                                    <!-- Aqui van los anexos -->
                                <div class="row item" v-if="hallazgo.Anexos.length > 0"><table class="col-md-12 table-sm table-bordered">
                                    <tr v-for="ane in hallazgo.Anexos">
                                        <td width="10%" >Anexo</td>
                                        <td width="80%">{{ane.nombre}}</td>
                                        <td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" @click="DescargarArchivo(ane.nombre, ane.ruta)"> </button></td>
                                    </tr>
                                </table></div>
                                
                                <div v-if="hallazgo.Padre != '0'" class="row item" :id="hallazgo.Nivel"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">{{hallazgo.Fecha.substring(0, 10)}}</td><td width="15%"><span v-if="hallazgo.TipoConsideracionObservacion">Correctivo para la orden</span><span v-else>Mejora en el proceso</span></td><td width="33%">{{hallazgo.Comentario}}</td><td width="10%">{{hallazgo.FecDespacho.substring(0, 10)}}</td><td width="10%">{{hallazgo.Estado}}</td><td colspan = "2" width="20%">{{hallazgo.Usuario}}</td></tbody></table></div></div>
                            </div>

                            </div>
                     </div>
                             </div>
                         </div>
                     </div>
                        <hr />
                        
                          <h5 class="card-title alert alert-primary">Datos del Contacto para PQRS</h5>
                        
                       
                                       
                      <div class="row" v-if="pqrs.TipoPQRSId == 4 || ((pqrs.TipoPQRSId == 2 || pqrs.TipoPQRSId == 1 || pqrs.TipoPQRSId == 3 || pqrs.TipoPQRSId == 5)  && (fup.IdFup == undefined && fup.IdFup == null))">
							<div class="col-1" >
								Pais *
							</div>
							<div class="col-2">
							    <select id="cmbPaises" data-width="fit" data-live-search="true" @change="onChangePais" v-model="pqrs.IdPais" class="" >
                                                         <!--<option v-for="(pais, indexpais) in listapaises" :value="pais.Id" :key="indexpais">
                                                            {{ pais.Nombre }}
                                                         </option>-->
                                 </select>
                                
							</div>
							<div class="col-1" >
								Ciudad *
							</div>
							<div class="col-2">
								
                                <select id="cmbCiudad" data-width="fit" data-live-search="true" @change="onChangeCiudad" v-model="pqrs.IdCiudad" class="" >
                                                                                               
                                                         <!--<option v-for="(ciudad, indexciudad) in listaCiudades" :value="ciudad.Id" :key="indexciudad">
                                                            {{ ciudad.Nombre }}
                                                         </option>-->
                                 </select>
							</div>
							<div class="col-1">
								Empresa *
							</div>
							<div class="col-5">
								
                                  <select id="cmbEmpresa" data-width="fit" data-live-search="true" v-model="pqrs.IdCliente" class="" >
                                                        <!--<option v-for="(Cliente, indexc) in listaClientes" :value="Cliente.Id" :key="indexc">
                                                            {{ Cliente.Nombre }}
                                                         </option>-->
                                 </select>
                                 <div class="col-9" v-if="(pqrs.IdCliente == -1)"><input type="text" v-model="pqrs.otroCliente" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" /></div>
							</div>                        
						</div>

                        <div class="row mt-2">
                            <div class="col-1"><label>Nombre</label></div>
                            <div class="col-5"><input type="text" v-model="pqrs.NombreRespuesta" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" /></div>
                            <div class="col-1"><label>Dirección Respuesta</label></div>
                            <div class="col-5"><input type="text" v-model="pqrs.DireccionRespuesta" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" /></div>
                        </div>
                            <div class="row">
                            <div class="col-1"><label>Email</label></div>
                            <div class="col-5"><input type="email" @blur="validateEmail" v-model="pqrs.EmailRespuesta" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" /></div>
                    
                            <div class="col-1"><label>Teléfono</label></div>
                            <div class="col-5"><input type="text" v-model="pqrs.TelefonoRespuesta" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline"></input></div>
                        </div>
                        <div class="main">
                            <div  class="dropzone-container"  @dragover="dragover" @dragleave="dragleave" @drop="drop"  >
                              <input type="file" multiple name="file" id="fileInput" class="hidden-input" @change="onChange" ref="file" accept=".pdf,.jpg,.jpeg,.png"  />

                              <label for="fileInput" class="file-label">
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
                                    <button class="ml-2" type="button" @click="remove(files.indexOf(file))" title="Remove file" >
                                      <b>&times;</b>
                                    </button>
                                  </div>
                                </div>
                              </div>
                            </div>
                        </div>
                     <div class="row" v-if="calculedRolId == 1">
                         <div class="offset-10">
                           <button type="button" id="btnSavePQRS" @click="GuardarPQRS" class="btn btn-primary">Guardar</button>
                         </div>
                      </div>
                 </div>
            </div>
        </div>


        <div class="modal fade" id="ModalCreatePQRS" tabindex="-1" role="dialog" aria-hidden="true">
	        <div class="modal-dialog" role="document">
		        <div class="modal-content modal-lg">
			        <div class="modal-header">
				        <h5 class="modal-title">Crear PQRS</h5>
				        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
			        </div>
			        <div class="modal-body p-3">
				      
			        </div>
			        <div class="modal-footer">
				       <%-- <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>--%>
                        <button type="button" id="btnSavePQRS" @click="GuardarPQRS" class="btn btn-primary">Guardar</button>
			        </div>
		        </div>
	        </div>
        </div>

   
        <div class="modal fade" id="MostrarModalCliente" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content modal-lg">
						<div class="modal-header">
							<h5 class="modal-title">Validar Cliente </h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
						</div>
						<div class="modal-body p-5">
			            
                        <div class="row">
							<div class="col-1" >
								Pais *
							</div>
							<div class="col-2">
							    <select @change="onChangePais" v-model="pqrs.IdPais" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" >
                                                         <option v-for="(pais, indexpais) in listapaises" :value="pais.Id" :key="indexpais">
                                                            {{ pais.Nombre }}
                                                         </option>
                                 </select>
                                
							</div>
							<div class="col-1" >
								Ciudad *
							</div>
							<div class="col-2">
								
                                <select @change="onChangeCiudad" v-model="pqrs.IdCiudad" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" >
                                                                                               
                                                         <option v-for="(ciudad, indexciudad) in listaCiudades" :value="ciudad.Id" :key="indexciudad">
                                                            {{ ciudad.Nombre }}
                                                         </option>
                                 </select>
							</div>
							<div class="col-1">
								Empresa *
							</div>
							<div class="col-5">
								
                                  <select v-model="pqrs.IdCliente" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" >
                                                         <option v-for="(Cliente, indexc) in listaClientes" :value="Cliente.Id" :key="indexc">
                                                            {{ Cliente.Nombre }}
                                                         </option>
                                 </select>
							</div>
						</div>
                            
						</div>
						
                        
                        <div class="modal-footer">
							<button type="button"  @click="AceptarCliente()" class="btn btn-primary">Aceptar</button>
						</div>
					</div>
				</div>
			</div>        
    
    
    </div>
  
   <script type="text/javascript">
        function doOnLoad() { }
    </script>

    <style >
        .filter-option-inner-inner {
            font-size: 0.8rem !important;
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
    </style>

</asp:Content>

