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
    public partial class FormFleteInternacional : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.EnablePageMethods = true;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        [WebMethod]
        public static string ObtenerAgentes()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<Agentes_FleteInternacional> data = ControlDatos.EjecutarStoreProcedureConParametros<Agentes_FleteInternacional>("USP_fup_SEL_AgenteCarga_Flete_Internacional", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static string ObtenerTiposContenedores()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<Contenedores_FleteInternacional> data = ControlDatos.EjecutarStoreProcedureConParametros<Contenedores_FleteInternacional>("USP_fup_SEL_Contenedores_Fletes_AgenteCarga", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static string ObtenerPuertos()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<Puertos_FleteInternacional> data = ControlDatos.EjecutarStoreProcedureConParametros<Puertos_FleteInternacional>("USP_fup_SEL_Puertos_Fletes_AgenteCarga", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static string ObtenerFletes()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<FleteInternacional> data = ControlDatos.EjecutarStoreProcedureConParametros<FleteInternacional>("USP_fup_SEL_Fletes_AgenteCarga", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static void CrearActualizarFleteNacional(FleteInternacional fleteInternacional)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pImportar", fleteInternacional.Importar);
            parametros.Add("@pRegistroId", fleteInternacional.RegistroId);
            parametros.Add("@pAgenteCargaId", fleteInternacional.AgenteCargaId);
            parametros.Add("@pAgenteCargaNombre", fleteInternacional.AgenteCargaNombre);
            parametros.Add("@pPuertoOrigenId", fleteInternacional.PuertoOrigenId);
            parametros.Add("@pPuertoOrigenNombre", fleteInternacional.PuertoOrigenNombre);
            parametros.Add("@pPuertoDestinoId", fleteInternacional.PuertoDestinoId);
            parametros.Add("@pPuertoDestinoNombre", fleteInternacional.PuertoDestinoNombre);
            parametros.Add("@pTipoContenedorId", fleteInternacional.TipoContenedorId);
            parametros.Add("@pTipoContenedorNombre", fleteInternacional.TipoContenedorNombre);
            parametros.Add("@pDespachoAduanal", fleteInternacional.DespachoAduanal);
            parametros.Add("@pGastosPuertoOrigen", fleteInternacional.GastosPuertoOrigen);
            parametros.Add("@pFleteInternacionalValor", fleteInternacional.FleteInternacionalValor);
            parametros.Add("@pLeadTimeCIF", fleteInternacional.LeadTimeCIF);
            parametros.Add("@pEstado", fleteInternacional.Estado);
            parametros.Add("@pUsuario", (string)HttpContext.Current.Session["Usuario"]);
            List<RespuestaFilasAfectadas> data = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_UPD_Fletes_Internacionales", parametros);
        }

        [WebMethod]
        public static void BorrarFleteNacional(int RegistroId)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pRegistroId", RegistroId);
            List<RespuestaFilasAfectadas> data = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_DEL_Fletes_Internacionales", parametros);
        }

        private static SheetData CrearEncabezadoExportar(SheetData sheetData)
        {
            Row row = new Row();
            Cell cell = new Cell { CellValue = new CellValue("Agente Carga"), DataType = CellValues.String };
            row.Append(cell);
            cell = new Cell { CellValue = new CellValue("Puerto Origen"), DataType = CellValues.String };
            row.Append(cell);
            cell = new Cell { CellValue = new CellValue("Puerto Destino"), DataType = CellValues.String };
            row.Append(cell);
            cell = new Cell { CellValue = new CellValue("Contenedor"), DataType = CellValues.String };
            row.Append(cell);
            cell = new Cell { CellValue = new CellValue("Despacho Aduanal"), DataType = CellValues.String };
            row.Append(cell);
            cell = new Cell { CellValue = new CellValue("Gastos Puerto Origen"), DataType = CellValues.String };
            row.Append(cell);
            cell = new Cell { CellValue = new CellValue("Valor Flete"), DataType = CellValues.String };
            row.Append(cell);
            cell = new Cell { CellValue = new CellValue("Lead Time CIF"), DataType = CellValues.String };
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
            List<FleteInternacional> data = ControlDatos.EjecutarStoreProcedureConParametros<FleteInternacional>("USP_fup_SEL_Fletes_AgenteCarga", parametros);


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
                foreach (FleteInternacional flete in data)
                {
                    Row row = new Row();
                    Cell cell = new Cell { CellValue = new CellValue(flete.AgenteCargaNombre), DataType = CellValues.String };
                    row.Append(cell);
                    cell = new Cell { CellValue = new CellValue(flete.PuertoOrigenNombre), DataType = CellValues.String };
                    row.Append(cell);
                    cell = new Cell { CellValue = new CellValue(flete.PuertoDestinoNombre), DataType = CellValues.String };
                    row.Append(cell);
                    cell = new Cell { CellValue = new CellValue(flete.TipoContenedorNombre), DataType = CellValues.String };
                    row.Append(cell);
                    cell = new Cell { CellValue = new CellValue(flete.DespachoAduanal), DataType = CellValues.Number };
                    row.Append(cell);
                    cell = new Cell { CellValue = new CellValue(flete.GastosPuertoOrigen), DataType = CellValues.Number };
                    row.Append(cell);
                    cell = new Cell { CellValue = new CellValue(flete.FleteInternacionalValor), DataType = CellValues.Number };
                    row.Append(cell);
                    cell = new Cell { CellValue = new CellValue(flete.LeadTimeCIF), DataType = CellValues.Number };
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
                    Name = "FletesInternacionales"
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