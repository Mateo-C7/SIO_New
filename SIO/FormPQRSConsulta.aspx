﻿<%@ Page Title="Consulta PQRS" Language="C#"  MasterPageFile="~/General2HamburgerSideBar.Master" AutoEventWireup="true" CodeBehind="FormPQRSConsulta.aspx.cs" Inherits="SIO.FormPQRSConsulta" Culture="en-US" UICulture="en-US" %>

	<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
		<script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
		<script type="text/javascript" src="Scripts/umd/popper.min.js"></script>
		<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
		<script type="importmap">
			{ "imports": { "vue": "./Scripts/vue.esm-browser.js", 
                           "vue3-easy-data-table": "./Scripts/vue3-easy-data-table.umd.js", 
                            "vue-i18n" : "./Scripts/vue-i18n.esm-browser.js",
                            "TradEsp" : "./Scripts/translation/es.json",
                            "TradEng" : "./Scripts/translation/en.json",
                            "TradPor" : "./Scripts/translation/pt.json"
                         } 
            }
		</script>

		<script type="module" src="Scripts/formpqrsConsulta.js?v=20230809A"></script>
		<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
		<script type="text/javascript" src="Scripts/select2.min.js"></script>
		<script type="text/javascript" src="Scripts/toastr.min.js"></script>
		<script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
		<script type="text/javascript" src="Scripts/moment.js?v=20200629"></script>

		<link rel="Stylesheet" href="Content/bootstrap.min.css" />
		<link rel="Stylesheet" href="Content/SIO.css" />
		<link rel="Stylesheet" href="Scripts/TableVue/style.css" />
		<link rel="stylesheet" href="Content/font-awesome.css" />
		<link rel="Stylesheet" href="Content/css/select2.min.css" />
		<link href="Content/toastr.min.css" rel="stylesheet" />
	</asp:Content>
	<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
		<div id="loader" style="        display: none">
			<h3>Procesando...</h3>
		</div>
		<div id="ohsnap"></div>

	 <div class="container-fluid contenedor_fup" id="app">
            <div class="card p-4">
             <div class="row">
                 <div class="col-10">
                     <div class="btn-group col align-self-end" role="group" aria-label="Basic example">
				        <button @click="ChangeLanguage('es')" type="button" class="btn btn-secondary langes">
					        <img alt="español" src="Imagenes/colombia.png" /></button>
				        <button @click="ChangeLanguage('en')" type="button" class="btn btn-secondary langen">
					        <img alt="ingles" src="Imagenes/united-states.png" /></button>
				        <button @click="ChangeLanguage('pt')" type="button" class="btn btn-secondary langbr">
					        <img alt="portugues" src="Imagenes/brazil.png" /></button>
                         <img class="menu-bar mx-3" src="Imagenes/HMIcon.png" style="width: 38px; height: 38px;"/>
                         <h2>¡Atención Con5entidos!</h2>
			        </div>
                 </div>
                 <div class="col-2">
                     <a class="btn btn-primary" href="FormPQRS.aspx" data-toggle="tooltip" ><i class="fa fa-plus-square" style="font-size:14px;"></i>&nbsp;&nbsp;&nbsp;&nbsp;{{ $t("create") }}</a>
                 </div>
             </div>
             <div class="card-body" v-if="permiso.SinPermiso == false">
                    <h5 class="card-title alert alert-primary">{{ $t("consultar") }}</h5>
                    <br />
                
                
                         <div class="row">
                            <div class="col-2"><label> {{ $t("filters.orden") }} </label></div>
                            <div class="col-4"><input type="text" v-model="filters.orden" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" /></div>
                            <div class="col-2"><label>{{ $t("filters.nombre") }}</label></div>
                            <div class="col-4"><input type="text" v-model="filters.nombre" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" /></div>
                         </div>
                         <div class="row">
                            <div class="col-2"><label>{{ $t("filters.desde") }}</label></div>
                            <div class="col-4"><input type="date" v-model="filters.desde" min="1900-01-01" max="2118-12-31" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" /></div>
                            <div class="col-2"><label>{{ $t("filters.hasta") }}</label></div>
                            <div class="col-4"><input type="date" v-model="filters.hasta" min="1900-01-01" max="2118-12-31" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" /></div>
                          </div>
                         <div class="row">
                            <div class="col-2 "><label>{{ $t("filters.fuente") }}</label></div>
                            <div class="col-4">
                                <select  class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" v-model="filters.fuente">
                                    <option value="-1">Seleccionar Todos</option>
                                    <option v-for="(item, index) in fuentes" :value="item.PQRSFuenteID" :key="index">
                                        {{ item.Descripcion }}
                                    </option>
                                </select>
						    </div>
                              <div class="col-2">{{ $t("filters.tipo") }} </div>
                            <div class="col-4">
                                 <select  class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" v-model="filters.TipoPQRSId">
                                           <option value="-1">Seleccionar Tipo</option>
                                            <option v-for="(item, index) in tipos" :value="item.Id" :key="index">
                                                {{ item.Descripcion }}
                                            </option>
                                        </select>
                            </div>
						   
					    </div>
                         <div class="row">
                                <div class="col-2"><label> {{ $t("filters.estado") }} </label></div>
                            <div class="col-4">
                                <select  class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" v-model="filters.estado">
                                           <option value="-1">Seleccionar Estado</option>
                                            <option v-for="(item, index) in estados" :value="item.PQRSEstadosID" :key="index">
                                                {{ item.Descripcion }}
                                            </option>
                                    </select>
                                

                            </div>
                        </div>
                        <div class="row">
                                <div class="col-2 font-weight-bold"><label> {{ $t("filters.idpqrs") }} </label></div>
                            <div class="col-4">
                                <input type="text" v-model="filters.idpqrs" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" />
                            </div>
                            <div class="col-4"></div>
                             <div class="col-2">
							    <button id="btnBusOf" type="button" class="btn btn-success" @click="ObtenerPQRSpor()" data-toggle="tooltip"><i class="fa fa-search"></i>&nbsp;&nbsp;&nbsp;&nbsp;{{ $t("consultarbtn") }}</button>
						    </div>
                        </div>


					<br />
					<h5 class="alert alert-primary mt-2">{{ $t("resultado") }}</h5>
					<easy-data-table :headers="headers" :items="listpqrss" :body-row-class-name="bodyRowClassNameFunction" :theme-color="colorTheme" :filter-options="filterOptions" table-class-name="customize-table" border-cell :rows-per-page="50">
						<template #item-IdPQRS="item">
                            <div class="IdPQRS-wrapper">
                                {{item.IdPQRS}}  <i class="fa fa-circle ml-1" :style="{color: item.Semaforo }" ></i>
                            </div>
                            
						</template>
                        <template #item-operation="item">
                              <div class="operation-wrapper"
                                  style="display: flex; flex-wrap: wrap; gap: 5px; padding-top: 5px; padding-bottom: 5px;">
                                  <button class="btn btn-sm btn-success" data-toggle="tooltip"  title="Historico" @click="MostrarHistoricoPQRS(item.IdPQRS)" style="padding-right: 16px !important;" type="button"><i class="fa fa-list" ></i></button>
                                  <button class="btn btn-sm btn-success" data-toggle="tooltip" title="Ver Resumen" @click="MostrarResumen(item.IdPQRS)" style="padding-right: 16px !important;" type="button"><i class="fa fa-pencil" aria-hidden="true"></i></button>
                                  <button data-toggle="tooltip" v-if="(item.EstadoID >= 3 || (item.EstadoID >= 1 && item.tipoPQRS == 5)) && item.EstadoID != 13 && calculedRolId == 1" class="btn btn-sm btn-success" title="Enviar Correo" @click="MostrarEnviarComunicadoCliente(item)" style="padding-right: 16px !important;" type="button"><i class="fa fa-paper-plane-o pr-3" aria-hidden="true"></i><span style="font-size: 10px !important;">({{item.CantidadComunicados}})</span></button>                             
                                  <button v-if="item.EstadoID == 0 && calculedRolId == 1"  data-toggle="tooltip"  title="Radicacion" style="padding-right: 16px !important;"  class="btn btn-sm btn-success" @click="RadicarPQRS(item.IdPQRS)" type="button"><i class="fa fa-play" aria-hidden="true"></i> </button> 
                                  <button v-if="item.EstadoID == 0 && calculedRolId == 1"  data-toggle="tooltip"  title="Anulación" style="padding-right: 16px !important;"  class="btn btn-sm btn-danger" @click="AnularPQRS(item.IdPQRS)" type="button"><i class="fa fa-times" aria-hidden="true"></i> </button>
                                  <button v-if="item.PuedeSerCerrada == 1 && calculedRolId == 1 && item.EstadoID != 9 && item.EstadoID != 11"  data-toggle="tooltip"  title="Cerrar" style="padding-right: 16px !important;"  class="btn btn-sm btn-warning" @click="AskClosePQRS(item.IdPQRS)" type="button"><i class="fa fa-archive" aria-hidden="true"></i> </button>
                                  <span  class="btn btn-sm btn-success" data-toggle="tooltip" tooltip="Encuestas Enviadas" v-if="item.SeEnvioEncuesta > 0">
                                      <i class="fa fa-check-square-o pr-2" style="margin-right: 8px; margin-top: 1px;" aria-hidden="true"></i>
                                      <span style="font-size: 10px !important;">({{ item.SeEnvioEncuesta }})</span>
                                  </span>
                              </div>
                            </template>
                        <template #item-tag="item">
                              <div class="tags-wrapper">
                                  <button class="btn btn-sm btn-success" data-toggle="tooltip"  title="Historico" @click="MostrarHistoricoPQRS(item.IdPQRS)" style="padding-right: 16px !important;" type="button"><i class="fa fa-list" ></i></button>
                              </div>
                            </template>
						<template #header-NroOrden="header">
                                <div class="filter-column">
                                    <button type="button" class="filter-icon"   @click.stop="showNameFilter=!showNameFilter"><i class="fa fa-filter" aria-hidden="true"></i></button>
                                {{ header.text }}
                                <div class="filter-menu" v-if="showNameFilter">
                                    <input v-model="nameCriteria"/>
                                </div>
                                </div>
                            </template>
                        <template #header-Fuente="header">
                                <div class="filter-column">
                                    <button type="button" class="filter-icon"   @click.stop="showFuenteFilter=!showFuenteFilter"><i class="fa fa-filter" aria-hidden="true"></i></button>
                                {{ header.text }}
                                <div class="filter-menu" v-if="showFuenteFilter">
                                    <input v-model="fuenteCriteria"/>
                                </div>
                                </div>
                            </template>
                        <template #header-Estado="header">
                                <div class="filter-column">
                                    <button type="button" class="filter-icon"   @click.stop="showEstadoFilter=!showEstadoFilter"><i class="fa fa-filter" aria-hidden="true"></i></button>
                                {{ header.text }}
                                <div class="filter-menu" v-if="showEstadoFilter">
                                    <input v-model="estadoCriteria"/>
                                </div>
                                </div>
                            </template>
                          <template #header-TipoPQRS="header">
                                <div class="filter-column">
                                    <button type="button" class="filter-icon"   @click.stop="showTipoPQRSFilter=!showTipoPQRSFilter"><i class="fa fa-filter" aria-hidden="true"></i></button>
                                {{ header.text }}
                                <div class="filter-menu" v-if="showTipoPQRSFilter">
                                    <input v-model="tipoPQRSCriteria"/>
                                </div>
                                </div>
                            </template>
                         <template #header-Cliente="header">
                                <div class="filter-column">
                                    <button type="button" class="filter-icon"   @click.stop="showClienteFilter=!showClienteFilter"><i class="fa fa-filter" aria-hidden="true"></i></button>
                                {{ header.text }}
                                <div class="filter-menu" v-if="showClienteFilter">
                                    <input v-model="nombreClienteCriteria"/>
                                </div>
                                </div>
                            </template>
                        <template #header-Pais="header">
                                <div class="filter-column">
                                    <button type="button" class="filter-icon"   @click.stop="showPaisFilter=!showPaisFilter"><i class="fa fa-filter" aria-hidden="true"></i></button>
                                {{ header.text }}
                                <div class="filter-menu" v-if="showPaisFilter">
                                    <input v-model="nombrePaisCriteria"/>
                                </div>
                                </div>
                            </template>
                        <template #header-DireccionRespuesta="header">
                                <div class="filter-column">
                                    <button type="button" class="filter-icon"   @click.stop="showDireccionRespuestaFilter=!showDireccionRespuestaFilter"><i class="fa fa-filter" aria-hidden="true"></i></button>
                                {{ header.text }}
                                <div class="filter-menu" v-if="showDireccionRespuestaFilter">
                                    <input v-model="direccionRespuestaCriteria"/>
                                </div>
                                </div>
                            </template>
						<template #header-EmailRespuesta="header">
                                <div class="filter-column">
                                    <button type="button" class="filter-icon"  @click.stop="showEmailFilter=!showEmailFilter"><i class="fa fa-filter" aria-hidden="true"></i></button>
                                {{ header.text }}
                                <div class="filter-menu" v-if="showEmailFilter">
                                    <input v-model="emailCriteria"/>
                                </div>
                                </div>
                            </template>
                        <template #header-TelefonoRespuesta="header">
                                <div class="filter-column">
                                    <button type="button" class="filter-icon"  @click.stop="showTelefonoRespuestaFilter=!showTelefonoRespuestaFilter"><i class="fa fa-filter" aria-hidden="true"></i></button>
                                {{ header.text }}
                                <div class="filter-menu" v-if="showTelefonoRespuestaFilter">
                                    <input v-model="telefonoRespuestaCriteria"/>
                                </div>
                                </div>
                            </template>
                        <template #header-OrdenProcedente="header">
                                <div class="filter-column">
                                    <button type="button" class="filter-icon"  @click.stop="showOrdenProcFilter=!showOrdenProcFilter"><i class="fa fa-filter" aria-hidden="true"></i></button>
                                {{ header.text }}
                                <div class="filter-menu" v-if="showOrdenProcFilter">
                                    <input v-model="ordenPrecedenteCriteria"/>
                                </div>
                                </div>
                            </template>
					</easy-data-table>
				</div>
				<div class="card-body" v-if="permiso.SinPermiso == true">
					<h2>No posee permisos para este perfil, por favor contacte al administrador</h2>
				</div>
			</div>
         
			<div class="modal fade" id="ModalPQRSHistorico" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content modal-lg">
						<div class="modal-header">
							<h5 class="modal-title">Timeline PQRS</h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
						</div>
						<div class="modal-body p-3">
							<div class="container-timeline">
								<div class="leftbox">
									<div class="rb-container">
										<ul class="rb">
											<li class="rb-item" @click="MostrarDetalleHistorico(item.PQRS, item.EstadoDespuesID, item.Id)" v-for="(item, index) in pqrsHistorico">
												<div class="timestamp">
													<b>  {{ item.EstadoDespues}} </b> <br/> {{item.FechaFormat}}
												</div>
												<div class="item-title">{{item.Usuario}}</div>

											</li>
										</ul>

									</div>
								</div>
								<div class="rightbox pl-3 pt-2" :class="{ statusShown: isStatusShown }">
									<div style="padding:10px !important; width:450px !important" v-if="EstadoIDHistorico == 1">
										<h5 style="width:300px">Archivos Cargados</h5>
										<div class="mb-1" style="width:100%" class="ml-3" v-for="(itemF, index) in pqrsArchivo">
                                            <button class="btn btn-sm btn-success ml-2" data-toggle="tooltip" title="Descargar" @click="DescargarArchivo(itemF.FileName, itemF.FilePATH)" style="padding-right: 16px !important;" type="button">
                                                <i style="color:#50d890" class="fa fa-file" aria-hidden="true"></i>
                                            </button>
											<span>{{ itemF.FileName }}</span> 
										</div>
									</div>
									<div style="padding:10px !important; width:450px !important" v-if="EstadoIDHistorico == 2">
										<h5 style="width:300px">Procesos Asignados</h5>
										<table class="table table-striped table-bordered table-hover">
											<thead>
												<tr class="table-info">
													<th style="width:40%">Proceso</th>
													<th style="width:60%">Email</th>
												</tr>
											</thead>
											<tbody>
												<tr v-for="(itemF, index) in procesosPQRS">
													<td>{{itemF.Proceso}}</td>
													<td>{{itemF.EmailProceso}}</td>
												</tr>
											</tbody>
										</table>
									</div>
                                    <div style="padding:10px !important; width:450px !important" v-if="isAnswerShown == true">
										<h5 style="width:300px">Respuesta Proceso</h5>
                                        <div class="row mb-2"><div class="col-sm-12"><b>Proceso: {{pqrsRespuestaHistorico.Proceso}}</b></div></div>
                                        <div class="row">
                                             <div class="col-sm-12">{{ pqrsRespuestaHistorico.Mensaje }}</div>
                                        </div><hr />
										<div class="mb-1" style="width:100%" class="ml-3" v-for="(itemF, index) in pqrsRespuestaHistorico.archivos">
											<a target="_blank" style="font-size:1.1rem; color:black !important; text-decoration:none !important;" :href="itemF.FilePATH"> <i style="color:#50d890" class="fa fa-2x fa-file"> </i>&nbsp; {{ itemF.FileName }}</a>
										</div>
									</div>
                                    <div style="padding:10px !important; width:450px !important" v-if="EstadoIDHistorico == 4">
										<h5 style="width:300px">Reclamo Procedente</h5>
										    <div class="row">
                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-6"><label>  <b>&nbsp;Es Procedente ?&nbsp; {{pqrsProcedenteHistorico.SEsProcedente}}</b></label> </div>
							                </div>

                                            <div class="row">
                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-6"><b>&nbsp;Descripción: &nbsp;</b> {{pqrsProcedenteHistorico.Descripcion}}</div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-6">{{pqrsProcedenteHistorico.DescripcionOrden}}</div>
                                            </div>

							                <div class="row" v-if="pqrsProcedenteHistorico.EsProcedente">
								                <div class="col-sm-1"></div>
								                <div class="col-sm-11">
									                <table class="wfull">
										                <thead>
											                <tr>
												                <th class="text-center" style="width:40%">Proceso</th>
												                <th class="text-center" style="width:60%">Tipo NC</th>
											                </tr>
										                </thead>
										                <tbody>
											                <tr v-for="(item, index) in pqrsProcedenteHistorico.procesos">
												                <td>{{item.Proceso}}</td>
												                <td>
                                                                   {{item.TipoNC}}
												                </td>
											                </tr>
										                </tbody>
									                </table>
								                </div>
							                </div>
									</div>
                                    <div style="padding:10px !important; width:450px !important" v-if="EstadoIDHistorico == 6">
										<h5 style="width:300px">Respuesta</h5>
                                         <div class="row mb-2">
                                             <div class="col-sm-2"><b>Para:</b></div>
                                             <div class="col-sm-10"><b>{{pqrsRespuestaCliente.MessageTo}}</b></div>
                                         </div>
                                         <div class="row">
                                             <div class="col-sm-2"><b>Asunto:</b></div>
                                             <div class="col-sm-10"><b>{{pqrsRespuestaCliente.Asunto}}</b></div>
                                         </div>
                                         <div class="row">
                                             <div class="col-sm-2"><b>Mensaje:</b></div>
                                             <div class="col-sm-10"><b>{{pqrsRespuestaCliente.Mensaje}}</b></div>
                                         </div>
                                        <div class="row">
                                        <span class="col-sm-3"><b>Se envió encuesta de satisfacción: </b>
                                            <span v-if="pqrsRespuestaCliente.SeEnvioEncuesta == null || pqrsRespuestaCliente.SeEnvioEncuesta == undefined || pqrsRespuestaCliente.SeEnvioEncuesta == 0">No</span>
                                            <span v-else>Si</span>
                                        </span>
                                    </div>
                                    </div>
                                    <div style="padding:10px !important; width:450px !important" v-if="EstadoIDHistorico == 10">
										<h5 style="width:300px">Producción</h5>
                                         <div class="row mb-2">
                                             <div class="col-sm-3"><b>Fecha Plan Alum:</b></div>
                                             <div class="col-sm-2"><b>{{pqrsProduccionHistorico.FechaPlanAlum}}</b></div>
                                              <div class="col-sm-3"><b>Fecha Plan Acero:</b></div>
                                             <div class="col-sm-2"><b>{{pqrsProduccionHistorico.FechaPlanAcero}}</b></div>
                                         </div>
                                         <div class="row">
                                            <div class="col-sm-3"><b>Fecha Req. Alum:</b></div>
                                             <div class="col-sm-2"><b>{{pqrsProduccionHistorico.FechaReqAlum}}</b></div>
                                              <div class="col-sm-3"><b>Fecha Req. Acero:</b></div>
                                             <div class="col-sm-2"><b>{{pqrsProduccionHistorico.FechaReqAcero}}</b></div>
                                         </div>
                                        <div class="row">
                                            <div class="col-sm-3"><b>Fecha Desp. Alum:</b></div>
                                             <div class="col-sm-2"><b>{{pqrsProduccionHistorico.FechaDespAlum}}</b></div>
                                              <div class="col-sm-3"><b>Fecha Desp. Acero:</b></div>
                                             <div class="col-sm-2"><b>{{pqrsProduccionHistorico.FechaDespAcero}}</b></div>
                                         </div>
                                         <div class="row">
                                             <div class="col-sm-3"><b>Fecha Ent. Obra:</b></div>
                                             <div class="col-sm-2"><b>{{pqrsProduccionHistorico.FechaEntObra}}</b></div>
                                         </div>
                                    </div>

								<%-- radicado elavorado --%>
                                     <div style="padding:10px !important; width:450px !important" v-if="EstadoIDHistorico == 0">
										<h5 style="width:300px">Elaboraciòn</h5>
                                         <div class="row mb-2">
                                             <div><b>Nombre Respuesta: </b></div>
                                             <div> &nbsp; &nbsp;{{PQRSDTOConsulta.NombreRespuesta}}</div>
                                             
                                         </div>
                                         <div class="row">
                                             <div><b>Usuario: </b></div>
                                             <div> &nbsp; &nbsp;{{PQRSDTOConsulta.UsuarioCreacion}}</div>
                                             
                                         </div>
                                          <div class="row">
                                            <div><b>Fecha:</b></div>
                                             <div> &nbsp; &nbsp;{{PQRSDTOConsulta.FechaCreacion}}</div>
                                             
                                         </div>
                                          <div class="row">
                                             <div><b>Detalle pqrs:</b></div>
                                             <div> &nbsp; &nbsp;{{PQRSDTOConsulta.Detalle}}</div>
                                         </div>
                                        
                                    </div>
                                
                                </div>
							</div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
						</div>
					</div>
				</div>
			</div>

			<div class="modal fade" id="ModalPQRSAsignacionProceso" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content modal-lg">
						<div class="modal-header">
							<h5 class="modal-title">Asignación Procesos</h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
						</div>
						<div class="modal-body p-3">
                            <div class="row p-2"><p>Si van dirigidos a mas de un usuario, se debes colocar los correos separados de punto y coma (;)</p></div>
							<div class="row">
								<div class="col-sm-2"></div>
								<div class="col-sm-8">
									<table class="wfull">
										<thead>
											<tr>
												<th style="width:20%">Seleccionado</th>
												<th style="width:40%">Proceso</th>
												<th style="width:40%">Emails</th>
											</tr>
										</thead>
										<tbody>
											<tr v-for="(item, index) in procesos">
												<td><input v-model="item.Seleccionado" type="checkbox" /></td>
												<td>{{item.Proceso}}</td>
												<td><textarea v-model="item.EmailProceso" class="wfull" rows="2" cols="5"></textarea></td>
											</tr>
										</tbody>
									</table>
								</div>
							</div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-danger " data-dismiss="modal">Cerrar</button>
							<button type="button" class="btn btn-success  ml-1" @click="PreguntaAsignarProceso()">Guardar</button>
						</div>
					</div>
				</div>
			</div>

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
                                <div class="col-sm-4"><label><b>Es Procedente ? &nbsp;</b></label></div>
                                <div class="col-sm-4"><label><input  type="radio"  value='true' v-model="pqrsProcedente.EsProcedente"  />  &nbsp;Si</label> &nbsp;
                                <label><input  type="radio" value='false' v-model="pqrsProcedente.EsProcedente"/>  &nbsp;No</label>  </div>

							</div>

                            <div class="row mx-3 pb-2"> 
                                <textarea v-model="pqrsProcedente.DescripcionNoProcedente" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" rows="5" placeholder=" Ingrese una razon porque es o no procedente" ></textarea>
                           </div>    

							<div class="row" v-if="pqrsProcedente.EsProcedente == 'true'">
								<div class="col-sm-2"></div>
								<div class="col-sm-8">
									<table class="wfull">
										<thead>
											<tr>
												<th class="text-center" style="width:20%">Seleccionar</th>
												<th class="text-center" style="width:40%">Proceso</th>
												<th class="text-center" style="width:40%">Tipo NC</th>
											</tr>
										</thead>
										<tbody>
											<tr v-for="(item, index) in procesos">
												<td class="text-center"><input v-model="item.Seleccionado" type="checkbox" /></td>
												<td>{{item.Proceso}}</td>
												<td>
                                                    <select v-model="item.TipoNC"  class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" >
                                                         <option v-for="(itemTNC, indexTNC) in tipoNC" :value="itemTNC.Id" :key="index">
                                                            {{ itemTNC.Descripcion }}
                                                         </option>
                                                    </select>
												</td>
											</tr>
										</tbody>
									</table>
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
                        <div class="row" v-if="pqrsGenerarOrden.solucionadoEnObra == 'false'">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-4"><label><b>Desea seleccionar una orden existente? &nbsp;</b></label></div>
                                <div class="col-sm-4"><label><input  type="radio" id="update" value="true" v-model="pqrsGenerarOrden.existeOrden" @change="ExisteOrdenChange"/>  &nbsp;Si</label> &nbsp;
                                <label><input  type="radio" id="New" value="false" v-model="pqrsGenerarOrden.existeOrden" @change="noExisteOrdenChange"/>  &nbsp;No</label>  </div>
                            </div>
                            <div class="row" v-if="pqrsGenerarOrden.existeOrden == 'true' && pqrsGenerarOrden.solucionadoEnObra == 'false'">
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
                            <div class="row" v-if="pqrsGenerarOrden.existeOrden == 'false' && pqrsGenerarOrden.solucionadoEnObra == 'false'">
                                 <div class="col-sm-1"></div>
                                   <div class="col-sm-4">
                                       <b>Seleccionar un tipo de orden</b>
                                    <select  class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" v-model="pqrsGenerarOrden.OrdenGarantiaOMejora">
                                        <option value="-1">Seleccionar Opción</option>
                                        <option value="OG">Orden de Garantía</option>
                                        <option value="OG">Orden de Mejora</option>
                                    </select> </div>
                                <div class="col-sm-4">
                                    <b>Seleccionar Planta</b>
                                    <select class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" v-model="pqrsGenerarOrden.IdPlanta">
                                        <option value="-1">Seleccionar Planta</option>
                                        <option v-for="planta in plantas" :value="planta.Id">{{planta.Descripcion}}</option>
                                    </select>
                                </div>
                             </div>                                  
                            <div class="row" v-if="pqrsGenerarOrden.solucionadoEnObra == 'false'">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-4"><label><b>Requiere listados? &nbsp;</b></label></div>
                                    <div class="col-sm-4">
                                        <label><input  type="radio" id="pedirCorreo" value="true"  v-model="pqrsGenerarOrden.requierelistados"  />  &nbsp;Si</label> &nbsp;
                                        <label><input  type="radio" id="pedirDescripcion" value="false" v-model="pqrsGenerarOrden.requierelistados" />  &nbsp;No</label>                       
                                   </div>							    
                            </div>
                            <div class="row" v-if="pqrsGenerarOrden.requierelistados == 'true' && pqrsGenerarOrden.solucionadoEnObra == 'false'">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-1"><label><b>Acero</b></label></div>
                                    <input type="checkbox" v-model="pqrsGenerarOrden.RequierelistadosAcero" class="form-control col-1"/>
                                    <textarea :disabled="!pqrsGenerarOrden.RequierelistadosAcero" v-model="pqrsGenerarOrden.RequierelistadosAceroCorreos" class ="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline col-5" id="requierelistadosCorreos" rows="2" placeholder=" Ingrese los correos a notificar separados por ; " ></textarea>
                                </div>  
                            <div class="row" v-if="pqrsGenerarOrden.requierelistados == 'true' && pqrsGenerarOrden.solucionadoEnObra == 'false'">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-1"><label><b>Aluminio</b></label></div>
                                    <input type="checkbox" v-model="pqrsGenerarOrden.RequierelistadosAluminio" class="form-control col-1"/>
                                    <textarea :disabled="!pqrsGenerarOrden.RequierelistadosAluminio" v-model="pqrsGenerarOrden.RequierelistadosAluminioCorreos" class ="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline col-5" id="requierelistadosCorreos" rows="2" placeholder=" Ingrese los correos a notificar separados por ; " ></textarea>
                            </div> 
                            <div class="row" v-if="pqrsGenerarOrden.requierelistados == 'false' && pqrsGenerarOrden.solucionadoEnObra == 'false'"> 
                                <div class="col-sm-2"></div>
                                <textarea v-model="pqrsGenerarOrden.requierelistadosDescripcion" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline col-5" id="requierelistadosDescripcion" rows="3" placeholder=" Ingrese una descripción " ></textarea>
                            </div>
                            <div class="row" v-if="pqrsGenerarOrden.solucionadoEnObra == 'false'">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-4"><label><b>Requiere planos? &nbsp;</b></label></div>
                                    <div class="col-sm-4">
                                        <label><input  type="radio" value="true"  v-model="pqrsGenerarOrden.requiereplanos"  />  &nbsp;Si</label> &nbsp;
                                        <label><input  type="radio" value="false" v-model="pqrsGenerarOrden.requiereplanos" />  &nbsp;No</label>                       
                                    </div>							    
                            </div>
                            <div class="row" v-if="pqrsGenerarOrden.requiereplanos == 'true' && pqrsGenerarOrden.solucionadoEnObra == 'false'">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-1"><label><b>Acero</b></label></div>
                                    <input type="checkbox" v-model="pqrsGenerarOrden.RequiereplanosAcero" class="form-control col-1"/>
                                    <textarea :disabled="!pqrsGenerarOrden.RequiereplanosAcero" v-model="pqrsGenerarOrden.RequiereplanosAceroCorreos" class ="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline col-5" rows="2" placeholder=" Ingrese los correos a notificar separados por ;" ></textarea>
                                </div>  
                            <div class="row" v-if="pqrsGenerarOrden.requiereplanos == 'true' && pqrsGenerarOrden.solucionadoEnObra == 'false'">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-1"><label><b>Aluminio</b></label></div>
                                    <input type="checkbox" v-model="pqrsGenerarOrden.RequiereplanosAluminio" class="form-control col-1"/>
                                    <textarea :disabled="!pqrsGenerarOrden.RequiereplanosAluminio" v-model="pqrsGenerarOrden.RequiereplanosAluminioCorreos" class ="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline col-5" rows="2" placeholder=" Ingrese los correos a notificar separados por ;" ></textarea>
                            </div>  
                            <div class="row" v-if="pqrsGenerarOrden.requiereplanos == 'false' && pqrsGenerarOrden.solucionadoEnObra == 'false'"> 
                                <div class="col-sm-2"></div>
                                <textarea v-model="pqrsGenerarOrden.RequierePlanosDescripcion" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline col-5" id="requierelistadosDescripcion" rows="3" placeholder=" Ingrese una descripción" ></textarea>
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
                              <li class="nav-item"  v-if="pqrs.EsProcedenteDescripcion != ''">
                                <a style="font-size: inherit" class="nav-link" id="details-tab" data-toggle="tab" href="#detailsComunicado" role="tab" aria-controls="profile" aria-selected="false">Detalles</a>
                              </li>
                                <li class="nav-item"  v-if="pqrs.NroOrden != ''">
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
                                                <tbody v-for="(itemF, index) in pqrsArchivo">
                                                    <tr>
                                                        <td>{{itemF.FileName}}</td>
                                                        <td><button 
                                                                type="button" 
                                                                class="btn fa fa-download" 
                                                                @click="DescargarArchivo(rad.FileName, rad.Path)"
                                                                >
                                                            </button>
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" v-model="archivosRadicadoComunicado[itemF.Id]" class="form-control" />
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
                                            <input disabled class="form-control" type="text" id="txtEsProcedenteComunicado" :value="pqrs.EsProcedenteDescripcion" />
                                        </div>
                                    </div>
                                    <div v-if="pqrs.OrdenGarantiaOMejora != '' || pqrs.OrdenProcedente !== ''" class="form-group row mx-1">
                                        <label v-if="pqrs.OrdenGarantiaOMejora != ''" style="margin-top: 0px !important" for="txtTipoOrdenComunicado" class="col-2 col-form-label">Tipo de Orden Generada</label>
                                        <div v-if="pqrs.OrdenGarantiaOMejora != ''" class="col-4 mt-2">
                                            <input disabled class="form-control" type="text" id="txtTipoOrdenComunicado" v-model="pqrs.OrdenGarantiaOMejora" />
                                        </div>
                                        <label v-if="pqrs.OrdenProcedente !== ''" style="margin-top: 0px !important" for="txtNumOrdenComunicado" class="col-2 col-form-label"># de Orden</label>
                                        <div class="col-4 mt-2" v-if="pqrs.OrdenProcedente !== ''">
                                            <input disabled class="form-control" type="text" id="txtNumOrdenComunicado" v-model="pqrs.OrdenProcedente" />
                                        </div>
                                    </div>
                                    <div class="form-group row mx-1">
                                        <label style="margin-top: 0px !important" for="txtDescripcionProcedenciaComunicado" class="col-2 col-form-label">Descripción Procedencia</label>
                                        <div class="col-10 mt-2">
                                            <textarea disabled class="form-control" id="txtDescripcionProcedenciaComunicado" v-model="pqrs.DescripcionProcedencia"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div v-if="pqrs.NroOrden != ''" class="tab-pane fade p-3" id="asociatedPQRSComunicado" role="tabpanel" aria-labelledby="details-tab">
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
		
            <div class="modal fade " id="ModalAgregarListadosRequerido" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content modal-lg">
						<div class="modal-header">
							<h5 class="modal-title">Agregar Listados Requeridos </h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
						</div>
						<div class="modal-body p-3">

                            <div class="row" >
								<div class="col-sm-2"></div>
								<div class="col-sm-8">
									<table class="wfull">
										<thead>
											<tr>
												<th class="text-center" style="width:20%">Mail </th>
												<th class="text-center" style="width:40%">Usuario </th>
												<th class="text-center" style="width:40%">Estado </th>
											</tr>
										</thead>
										<tbody>
											<tr v-for="(item, index) in listadosCargados">
												<td class="text-left">{{item.mail}}</td>
												<td class="text-center">{{item.usuario}} </td>
												<td class="text-center"><i v-if="item.valido ==1" class="fa fa-thumbs-up fa-2x text-info" aria-hidden="true"></i> <i v-if="item.valido==0" class="fa fa-thumbs-o-down fa-2x text-danger" aria-hidden="true"></i> </td>
											</tr>
										</tbody>
									</table>

                                   

								</div>
                            </div>

						    <div class="row">
                                <div class="main">
								        <div class="dropzone-container" @dragover="dragover" @dragleave="dragleave" @drop="dropListados">
									        <input type="file" multiple name="fileInputListados" id="fileInputListados" class="hidden-input" @change="onChangeListados" ref="fileInputListados" accept=".pdf,.jpg,.jpeg,.png" />

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

            <div class="modal fade " id="ModalImplementacionObra" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content modal-lg">
						<div class="modal-header">
							<h5 class="modal-title">Cargar Estado de Implementacion de Obra </h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
						</div>

						<div class="modal-body p-3">
						    <div class="row">
                              
                                
                                    <div class="main">
								        <div class="dropzone-container" @dragover="dragover" @dragleave="dragleave" @drop="dropImpl">
									        <input type="file" multiple name="fileInputImpl" id="fileInputImpl" class="hidden-input" @change="onChangeImpl" ref="fileInputImpl" accept=".pdf,.jpg,.jpeg,.png" />

									        <label for="fileInputImpl" class="file-label">
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
							<button type="button" id="btnAgregarImplementacionObra" @click="ValidarGuardarImplementacionObra(files)" class="btn btn-success">Guardar</button>
						</div>
					</div>
				</div>
			</div>

            <div class="modal fade " id="ModalCierreObra" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content modal-lg">
						<div class="modal-header">
							<h5 class="modal-title">Cierre Reclamacion </h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					        <span aria-hidden="true">&times;</span>
				        </button>
						</div>
						<div class="modal-body p-3">
			            
                        <div class="row">
                             <div class="col-2"><label>Cierre</label></div>
                              <div class="col-4"><input type="date" v-model="cierre.fechaCierrePlan" min="1900-01-01" max="2118-12-31" class="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline" /></div>
                        </div>
						<div class="row">
                                <div class="col-sm-2"><b>Plan de Accion</b></div>
                                <div class="col-10">
								    <input v-model="cierre.planAccion" class="shadow leading-tight w-full bg-white border rounded w-full py-2 px-3 text-gray-500 leading-tight focus:outline-none focus:shadow-outline" id="cierreReclamacionPlanAccion" placeholder="Indicar Plan de Accion" required v-model="planAccion"/>
                                </div>
						</div>
                        <div class="row">
                            <div class="col-sm-2">
                                    <b>Descripción</b>
                                </div>
                                <div class="col-sm-10">
                            <textarea class="form-control" rows="2" placeholder="Descripción" v-model="cierre.planAccionDescripcion"></textarea>
                                </div>
                            </div>
						</div>
                           
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
							<button type="button" id="btnEnviarCierreReclamacion" @click="validarEnviarCierreReclamacion(cierre)" class="btn btn-success">Guardar</button>
						</div>
					</div>
				</div>
			

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
                                <div class="col-sm-2"><b>Mensaje de información adicional solicitada:</b></div>
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

		<script type="text/javascript">
            function doOnLoad() { }

            $(document).on('inserted.bs.tooltip', function (e) {
                var tooltip = $(e.target).data('bs.tooltip');
                $(tooltip.tip).addClass($(e.target).data('tooltip-custom-classes'));
            });

        </script>
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

        .elaboracion-bg { --easy-table-body-row-background-color: #617fba !important }
        .radicado-bg { --easy-table-body-row-background-color: #878b91 !important }
        .asignacion-bg { --easy-table-body-row-background-color: #bac8e3 !important }
        .analisis-bg { --easy-table-body-row-background-color: #9dadcc !important }
        .reclamo-bg { --easy-table-body-row-background-color: #b5c7eb !important }
        .garantia-bg { --easy-table-body-row-background-color: #dbc8a4 !important }
        .respuesta-bg { --easy-table-body-row-background-color: #deb464 !important }
        .listados-bg { --easy-table-body-row-background-color: #deb568 !important }
        .comprobante-bg { --easy-table-body-row-background-color: #c48a1a !important }
        .cierre-bg { --easy-table-body-row-background-color: #e6cfa5 !important }
        .produccion-bg { --easy-table-body-row-background-color: #e0bf80 !important }
        .archivada-bg { --easy-table-body-row-background-color: #78de68 !important }
        .anulada-bg { --easy-table-body-row-background-color: #f05656 !important }

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