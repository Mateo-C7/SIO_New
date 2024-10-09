using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl.Entity;
using Newtonsoft.Json;
using System.Threading;
using CapaControl;
using System.Globalization;
using System.Web.Services;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;

namespace SIO
{
    public partial class FormFleteNacional : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.EnablePageMethods = true;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        [WebMethod]
        public static string ObtenerTransportadores()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<Transportador> data = ControlDatos.EjecutarStoreProcedureConParametros<Transportador>("USP_fup_SEL_Transportadores", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static string ObtenerFletes()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<FleteNacional> data = ControlDatos.EjecutarStoreProcedureConParametros<FleteNacional>("USP_fup_SEL_Fletes_Nacional", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }
        

       [WebMethod]
       public static string ObtenerTiposVehiculos()
       {
           Dictionary<string, object> parametros = new Dictionary<string, object>();
           List<TipoVehiculo> data = ControlDatos.EjecutarStoreProcedureConParametros<TipoVehiculo>("USP_fup_SEL_Tipo_Vehiculo", parametros);

           string response = JsonConvert.SerializeObject(data);
           return response;
       }

        [WebMethod]
        public static string ObtenerCiudades()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pais", "Colombia");
            List<CiudadesFletesNacionales> data = ControlDatos.EjecutarStoreProcedureConParametros<CiudadesFletesNacionales>("Ciudades", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static void CrearActualizarFleteNacional(FleteNacional fleteNacional)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pImportar", fleteNacional.Importar);
            parametros.Add("@pRegistroId", fleteNacional.RegistroId);
            parametros.Add("@pTransportadorId", fleteNacional.TransportadorId);
            parametros.Add("@pTransportadorNombre", fleteNacional.TransportadorNombre);
            parametros.Add("@pCiudadOrigenId", fleteNacional.CiudadOrigenId);
            parametros.Add("@pCiudadOrigenNombre", fleteNacional.CiudadOrigenNombre);
            parametros.Add("@pCiudadDestinoId", fleteNacional.CiudadDestinoId);
            parametros.Add("@pCiudadDestinoNombre", fleteNacional.CiudadDestinoNombre);
            parametros.Add("@pVehiculoId", fleteNacional.VehiculoId);
            parametros.Add("@pVehiculoDescripcion", fleteNacional.VehiculoDescripcion);
            parametros.Add("@pValorFlete", fleteNacional.ValorFlete);
            parametros.Add("@pValorPrima", fleteNacional.ValorPrima);
            parametros.Add("@pEstado", fleteNacional.Estado);
            parametros.Add("@pUsuario", (string)HttpContext.Current.Session["Usuario"]);
            List<RespuestaFilasAfectadas> data = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_UPD_Fletes_Nacional", parametros);
        }

        [WebMethod]
        public static void BorrarFleteNacional(int RegistroId)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pRegistroId", RegistroId);
            List<RespuestaFilasAfectadas> data = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_DEL_Fletes_Nacional", parametros);
        }

        private static SheetData CrearEncabezadoExportar(SheetData sheetData)
        {
            Row row = new Row();
            Cell cell = new Cell { CellValue = new CellValue("Nombre Transportador"), DataType = CellValues.String };
            row.Append(cell);
            cell = new Cell { CellValue = new CellValue("Ciudad Origen"), DataType = CellValues.String };
            row.Append(cell);
            cell = new Cell { CellValue = new CellValue("Ciudad Destino"), DataType = CellValues.String };
            row.Append(cell);
            cell = new Cell { CellValue = new CellValue("Vehiculo"), DataType = CellValues.String };
            row.Append(cell);
            cell = new Cell { CellValue = new CellValue("Valor Flete"), DataType = CellValues.String };
            row.Append(cell);
            cell = new Cell { CellValue = new CellValue("Valor Prima"), DataType = CellValues.String };
            row.Append(cell);
            cell = new Cell { CellValue = new CellValue("Estado"), DataType = CellValues.String };
            row.Append(cell);
            sheetData.Append(row);

            return sheetData;
        }

        [WebMethod]
        public static string ExportarFletesXLS()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<FleteNacional> data = ControlDatos.EjecutarStoreProcedureConParametros<FleteNacional>("USP_fup_SEL_Fletes_Nacional", parametros);

            // Generacion de un path aleatorio
            string carpetaActual = Environment.CurrentDirectory;
            string fileName = Guid.NewGuid().ToString() + ".xls";
            string filePath = Path.GetFullPath(@"I:\\TempFilesFletes\\" + fileName);

            // Crea el archivo excel
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                // Crea el libro de trabajo
                WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                // Crea una hoja de trabajo
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                // Agregamos los datos
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                sheetData = CrearEncabezadoExportar(sheetData);
                foreach (FleteNacional flete in data)
                {
                    Row row = new Row();
                    Cell cell = new Cell { CellValue = new CellValue(flete.TransportadorNombre), DataType = CellValues.String };
                    row.Append(cell);
                    cell = new Cell { CellValue = new CellValue(flete.CiudadOrigenNombre), DataType = CellValues.String };
                    row.Append(cell);
                    cell = new Cell { CellValue = new CellValue(flete.CiudadDestinoNombre), DataType = CellValues.String };
                    row.Append(cell);
                    cell = new Cell { CellValue = new CellValue(flete.VehiculoDescripcion), DataType = CellValues.String };
                    row.Append(cell);
                    cell = new Cell { CellValue = new CellValue(flete.ValorFlete), DataType = CellValues.Number };
                    row.Append(cell);
                    cell = new Cell { CellValue = new CellValue(flete.ValorPrima), DataType = CellValues.Number };
                    row.Append(cell);
                    cell = new Cell { CellValue = new CellValue(flete.Estado == 1 ? "Activo" : "Inactivo"), DataType = CellValues.String };
                    row.Append(cell);
                    sheetData.Append(row);
                }

                // Agrego la hoja de trabajo al libro
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "FletesNacionales"
                };
                sheets.Append(sheet);

                workbookPart.Workbook.Save();
            }

            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("path", "/TempFilesFletes/");
            response.Add("fileName", fileName);
            return JsonConvert.SerializeObject(response);
        }

    }
}