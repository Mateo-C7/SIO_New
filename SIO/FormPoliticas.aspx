<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormPoliticas.aspx.cs" Inherits="SIO.FormPoliticas" Culture="en-US" UICulture="en-US" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
	<script type="text/javascript" src="Scripts/umd/popper.min.js"></script>

	<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.messagestore.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.fallbacks.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.parser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.emitter.js"></script>
	<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
	<script type="text/javascript" src="Scripts/select2.min.js"></script>
	<script type="text/javascript" src="Scripts/toastr.min.js"></script>
	<script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
    <script type="text/javascript" src="Scripts/moment.js?v=20200629"></script>
	<link rel="Stylesheet" href="Content/bootstrap.min.css" />
	<link rel="Stylesheet" href="Content/SIO.css" />
	<link rel="stylesheet" href="Content/font-awesome.css" />
	<link rel="Stylesheet" href="Content/css/select2.min.css" />
	<link href="Content/toastr.min.css" rel="stylesheet" />

	<script type="text/javascript">

</script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">

    <div id="loader" style="display: none">
		<h3>Procesando...</h3>
	</div>
	<div id="ohsnap"></div>

	<div class="container-fluid contenedor_fup">
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

        <!-- Carousel for images about politics -->
        <div id="carouselExampleIndicators" class="carousel slide" data-interval="false" data-ride="carousel">
          <ol class="carousel-indicators">
            <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
            <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
            <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
          </ol>
          <div class="carousel-inner text-center">
            <div class="carousel-item active">
              <img class="d-block w-75" style="margin:auto"  src="Imagenes/Politics/Img1 Forsa.png" alt="First slide">
            </div>
            <div class="carousel-item">
              <img class="d-block w-75" style="margin:auto"  src="Imagenes/Politics/Img2 Forsa.png" alt="Second slide">
            </div>
            <div class="carousel-item">
              <img class="d-block w-50" style="margin:auto" src="Imagenes/Politics/Img3 Forsa.png" alt="Third slide">
            </div>
          </div>
          <a class="carousel-control-prev" style="filter:invert(100%);" href="#carouselExampleIndicators" role="button" data-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
          </a>
          <a class="carousel-control-next" style="filter:invert(100%);" href="#carouselExampleIndicators" role="button" data-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
          </a>
        </div>

	</div>

</asp:Content>
