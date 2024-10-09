using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using CapaDatos;
using Microsoft.Reporting.WebForms;
using System.Data.Common;

namespace SIO
{
    public partial class Prueba : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            //String strConnection = "Data Source=MySystem;Initial Catalog=MySamplesDB;Integrated Security=True";
            String conexion = @"Provider=sqloledb; data source=172.21.0.5;persist security info=False;initial catalog=Forsa; user id=forsa; password=forsa2006";//REALA
            //string conexion = BdDatos.conexionScope();                 

            //file upload path
            string ruta = @"C:\Documents and Settings\ivanvidal\Desktop\Lite Subida\" ;
            string path = fileuploadExcel.PostedFile.FileName;

            //Create connection string to Excel work book

            string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;Persist Security Info=False";

            //Create Connection to Excel work book

            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);


            //Create OleDbCommand to fetch data from Excel

            OleDbCommand cmd = new OleDbCommand("Select [idPais],[IdCiudad],[EMPRESA/CLIENTE],[DIRECCION EMPRESA] ," +
            			"[NOMBRE PROYECTO],[DIRECCION PROYECTO],[idTipoproyecto],[UNIDADES],[MTS2] ,"+
            " [INVERSION],[idproducto],[idModalidad],[CONTACTO],[CARGO],[TELEFONO],[CELULAR],[CORREO] ,"+
           "[OBSERVACIONES/COMENTARIOS],[Periodo],[idOrigen],[idFuente] from [Lite$]", excelConnection);

            excelConnection.Open();

            OleDbDataReader dReader;

            OleDbDataAdapter adaptador = new OleDbDataAdapter();
            adaptador.SelectCommand = cmd;

            SqlDataAdapter adaptersql = new SqlDataAdapter();
            

            DataSet ds = new DataSet();
            adaptador.Fill(ds);
            //GridView1.DataSource = ds.Tables[0];
            //GridView1.DataBind();
            //GridView1.Visible= true;

            int posicion = -1;
            int id;
            string name;
            string designation;

            int id_pais,id_ciudad,id_tipoproyecto,unidades,id_tipoproducto,id_tipomodalidad,tipoorigen,tipofuente;
            decimal metros,inversion;
            string direcc_proyect,contacto, cargo, telefono, celular, correo, direccion, empresa, proyecto, observaciones, fecha_periodo;        

             try
             {

            //SqlConnection con = new SqlConnection(conexion);
            OleDbConnection con = new OleDbConnection(conexion);  
            string sql="";
            con.Open();

            foreach (DataRow fila in ds.Tables[0].Rows)
            {
                posicion = posicion + 1;                

                id_pais = Convert.ToInt32( fila[0].ToString());
                id_ciudad = Convert.ToInt32( fila[1].ToString());
                empresa = fila[2].ToString();
                direccion = fila[3].ToString(); 
                proyecto = fila[4].ToString();
                direcc_proyect = fila[5].ToString();               
                id_tipoproyecto = Convert.ToInt32( fila[6].ToString());
                unidades = Convert.ToInt32( fila[7].ToString());
                metros = Convert.ToDecimal( fila[8].ToString());
                inversion = Convert.ToDecimal( fila[9].ToString());
                id_tipoproducto = Convert.ToInt32( fila[10].ToString());
                id_tipomodalidad = Convert.ToInt32( fila[11].ToString());
                contacto= fila[12].ToString();
                cargo= fila[13].ToString();
                telefono= fila[14].ToString();
                celular= fila[15].ToString();
                correo= fila[16].ToString();
                observaciones= fila[17].ToString();
                fecha_periodo = Convert.ToDateTime(fila[18]).ToString("dd/MM/yyyy");
                tipoorigen =  Convert.ToInt32( fila[19].ToString());
                tipofuente = Convert.ToInt32(fila[20].ToString());

                System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();

                sql = "INSERT INTO cliente_lite ( clite_id_pais,clite_id_ciudad,clite_empresa,clite_proyecto,clite_id_tipoproyecto,clite_unidades,clite_metros,clite_inversion, " +
                "clite_id_tipoproducto,clite_id_tipomodalidad,clite_contacto,clite_cargo,clite_telefono,clite_celular,clite_correo,clite_direccion, " +
                "clite_observaciones,clite_id_tipoorigen,clite_id_tipofuente,clite_fecha_periodo,clite_direccion_proy) " +
                    "VALUES ( " +
               id_pais + "," + id_ciudad + ",'" + empresa + "','" + proyecto + "'," + id_tipoproyecto + "," + unidades + "," + metros + "," + inversion + "," +
               id_tipoproducto + "," + id_tipomodalidad + ",'" + contacto + "','" + cargo + "','" + telefono + "','" + celular + "','" + correo + "','" + direccion + "','" +
               observaciones + "'," + tipoorigen + "," + tipofuente + ",'" + fecha_periodo + "','"+direcc_proyect+"')";

                myCommand.Connection = con;
                myCommand.CommandText = sql;
                myCommand.ExecuteNonQuery();
               
            }
            con.Close();           
                
            ds.Tables.Remove("Table");
            ds.Dispose();
            ds.Clear();

            Label1.Text = "se han subido "+ posicion.ToString() + " Registros ";

            //dReader = cmd.ExecuteReader();

            //SqlBulkCopy sqlBulk = new SqlBulkCopy(conexion);

            ////Give your Destination table name

            //sqlBulk.DestinationTableName = "Excel_table";

            //sqlBulk.WriteToServer(dReader);

            //excelConnection.Close();

             }
             catch (Exception ex)
             {
                 string mensaje= "Se han subido " + posicion.ToString() + " Registros";
                 //Label1.Text = "Error in execution " + ex.ToString();
                 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
             }
        }

        protected void lkrapidisimo_Click1(object sender, EventArgs e)
        {
            // Limpiamos la salida
            Response.Clear();
            // Con esto le decimos al browser que la salida sera descargable
            Response.ContentType = "application/octet-stream";
            // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
            Response.AddHeader("Content-Disposition", "attachment; filename=Imagenes/1.gif");
            // Escribimos el fichero a enviar 
            Response.WriteFile("Imagenes/1.gif");
            // volcamos el stream 
            Response.Flush();
            // Enviamos todo el encabezado ahora
            Response.End();

            //Byte[] correo = new Byte[0];
            //WebClient clienteWeb = new WebClient();
            //clienteWeb.Dispose();
            //clienteWeb.DownloadFil("http://app.forsa.com.co/siomaestros/imagenes/1.gif", "1.gif");
            //System.Diagnostics.Process.Start("1.gif");
        }

       

        
    }
}