using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Security;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using CapaControl;

namespace SIO
{
    public partial class Eventos : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public SqlDataReader readerevento = null;
        private DataSet dsPedido = new DataSet();
        public ControlPedido contpv = new ControlPedido();
        public ControlUbicacion contubi = new ControlUbicacion();
        public ControlCliente concli = new ControlCliente();
        public ControlEvento controlEvento = new ControlEvento();
        public ControlVisitaComercial CVC = new ControlVisitaComercial();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PoblarOrigen();
                this.cargarPaisesRol();
                cargargrilla();
            }
        }

        private void PoblarOrigen()
        {
            cboOrigen.Items.Clear();
            cboOrigen.Items.Add(new ListItem("Seleccione El Origen", "0"));

            reader = contubi.poblarOrigen();
            if (reader != null)
            {
                while (reader.Read())
                {
                    cboOrigen.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            contubi.cerrarConexion();
        }

        protected void cargargrilla()
        {
            dsPedido.Reset();
            dsPedido = controlEvento.ConsultarEventos();
            if (dsPedido != null)
            {
                GridView1.DataSource = dsPedido.Tables[0];
                GridView1.DataBind();
                GridView1.Visible = true;
            }
            else
            {
                GridView1.Dispose();
                GridView1.Visible = false;
            }
            //dsPedido.Reset();
            contubi.cerrarConexion();
        }
        private void cargarPaisesRol()
        {
            int arRol = (int)Session["Rol"];
            if ((arRol == 3) || (arRol == 28) || (arRol == 29))
            {
                this.PoblarListaPais();
            }
            else
            {
                this.PoblarListaPais2();
            }
        }
        private void PoblarListaPais()
        {
            string idioma = (string)Session["Idioma"];
            string rcID = (string)Session["rcID"];
            //cboPais.Items.Clear();

            cboPais.Items.Add(new ListItem("Seleccione El Pais", "0"));


            reader = contubi.poblarListaPaisRepresentante(Convert.ToInt32(rcID));
            if (reader != null)
            {
                while (reader.Read())
                {
                    cboPais.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(1).ToString()));
                }
            }
            else
            {
                string mensaje = "";
                if (idioma == "Español")
                {
                    mensaje = "Usted no posee paises asociados.";
                }
                if (idioma == "Ingles")
                {
                    mensaje = "You have no partner countries.";
                }
                if (idioma == "Portugues")
                {
                    mensaje = "Você não tem países parceiros.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            reader.Close();
            contubi.cerrarConexion();
            // Session["Pais"] = cboPais.SelectedValue;
        }
        private void PoblarListaPais2()
        {
            string idioma = (string)Session["Idioma"];
            //cboPais.Items.Clear();

            cboPais.Items.Add(new ListItem("Seleccione El Pais", "0"));


            reader = contubi.poblarListaPais();

            if (reader != null)
            {
                while (reader.Read())
                {
                    cboPais.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            else
            {
                string mensaje = "";
                if (idioma == "Español")
                {
                    mensaje = "Usted no posee paises asociados.";
                }
                if (idioma == "Ingles")
                {
                    mensaje = "You have no partner countries.";
                }
                if (idioma == "Portugues")
                {
                    mensaje = "Você não tem países parceiros.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            reader.Close();
            contubi.cerrarConexion();
        }

        protected void cboCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PoblarCiudad(0);
        }

        private void PoblarCiudad(int estado)
        {
            int rol = (int)Session["Rol"];
            string rcID = (string)Session["rcID"];
            string idioma = (string)Session["Idioma"];

            if (estado == 0) cboCiudad.Items.Clear();

            cboCiudad.Items.Add(new ListItem("Seleccione La Ciudad", "0"));

            if ((rol == 3) && (Convert.ToInt32(cboPais.SelectedItem.Value) == 8))
            {
                reader = contubi.poblarCiudadesRepresentantesColombia(Convert.ToInt32(rcID));

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        cboCiudad.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                }
                reader.Close();
                contubi.cerrarConexion();
            }
            else
            {
                reader = contubi.poblarListaCiudades(Convert.ToInt32(cboPais.SelectedItem.Value));
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        cboCiudad.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                }
                reader.Close();
                contubi.cerrarConexion();
            }
            Session["Ciudad"] = cboCiudad.SelectedValue;
        }

        protected void btnGuardarsf_Click(object sender, EventArgs e)
        {
            string usuario = (string)Session["Usuario"];
            string mensaje = "";

            if (txtFechaIni.Text == "" || txtFechaFin.Text == "")
            {
                mensaje = "Faltan las fechas del evento";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                if (DateTime.Parse(txtFechaFin.Text) < DateTime.Parse(txtFechaIni.Text))
                {
                    mensaje = "La fecha fin no puede se menor que la de inicio";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {

                    if (cboPais.SelectedItem.Value == "0")
                    {
                        mensaje = "Seleccione el pais";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }
                    else
                    {
                        if (cboCiudad.SelectedItem.Value == "0")
                        {
                            mensaje = "Seleccione la ciudad";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        }
                        else
                        {
                            if (cboOrigen.SelectedValue == "0")
                            {
                                mensaje = "Seleccione el Origen";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                            }
                            else
                            {
                                if (txtNombre.Text == "")
                                {
                                    mensaje = "Digite el nombre";
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                                }
                                else
                                {
                                    if (listPart.Items.Count == 0)
                                    {
                                        mensajeVentana("Seleccione al menos un participante");
                                    }
                                    else
                                    {
                                        if (btnGuardar.Text == "Guardar")
                                        {
                                            txtNombre.Text = txtNombre.Text.ToUpperInvariant();
                                            String menId = controlEvento.guardarDatosEvento(Convert.ToInt32(cboPais.SelectedItem.Value), Convert.ToInt32(cboCiudad.SelectedItem.Value), txtNombre.Text, usuario, txtFechaIni.Text, txtFechaFin.Text, Convert.ToInt32(cboOrigen.SelectedValue),txtObjetivo.Text,txtConclusion.Text);
                                            if (menId.Substring(0, 1) != "E")//E <- de ERROR
                                            {
                                                String listParts = listaString(listPart);
                                                Boolean confi = controlEvento.insertParti(listParts, menId);
                                                if (confi == false)
                                                {
                                                    mensajeVentana("Evento guardado exitosamente!! Pero hubo un error al guardar los participantes"); btnGuardar.Text = "Actualizar";
                                                }
                                                else { mensajeVentana("Evento guardado exitosamente!!"); btnGuardar.Text = "Actualizar"; }
                                                cargargrilla();
                                                Session["Idevento"] = menId;
                                                enviarICS(menId);
                                            }
                                            else { mensajeVentana(menId); btnGuardar.Text = "Guardar"; }
                                        }
                                        else
                                        {
                                            if (btnGuardar.Text == "Actualizar")
                                            {
                                                txtNombre.Text = txtNombre.Text.ToUpperInvariant();
                                                String idevento = Session["Idevento"].ToString();
                                                int actualizar = controlEvento.actualizarDatosFeria(Convert.ToInt32(cboPais.SelectedItem.Value), Convert.ToInt32(cboCiudad.SelectedItem.Value), txtNombre.Text, usuario, int.Parse(idevento), txtFechaIni.Text, txtFechaFin.Text, Convert.ToInt32(cboOrigen.SelectedValue),txtObjetivo.Text,txtConclusion.Text);
                                                if (actualizar != -1)
                                                {
                                                    String listParts = listaString(listPart);
                                                    Boolean confi = controlEvento.insertParti(listParts, idevento.ToString());
                                                    if (confi == false)
                                                    {
                                                        mensajeVentana("Evento actualizado exitosamente!! Pero hubo un error al actualizar los participantes");
                                                    }
                                                    else { mensajeVentana("Evento actualizado exitosamente!!"); }
                                                    cargargrilla();
                                                    enviarICS(idevento);
                                                }
                                                else
                                                {
                                                    mensajeVentana("No fue posible actualizar el evento");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fila = GridView1.SelectedRow.RowIndex;
            this.limpiarcampos();
            btnGuardar.Enabled = true;
            btnGuardar.Text = "Actualizar";
            String idEvento = GridView1.SelectedRow.Cells[1].Text;
            llenarList(controlEvento.cargarPartiEvento(" AND (part.part_tifuente_id = " + idEvento + ")"), listPart, 1, 0);
            readerevento = controlEvento.consultarEvento(Convert.ToInt32(idEvento));
            if (readerevento.HasRows == true)
            {
                readerevento.Read();
                cboPais.Items.Clear();
                cboPais.Items.Add(new ListItem(readerevento.GetString(2), readerevento.GetInt32(1).ToString()));
                cboPais.Items.Add(new ListItem("------", "0"));
                cargarPaisesRol();
                cboCiudad.Items.Clear();
                cboCiudad.Items.Add(new ListItem(readerevento.GetString(4), readerevento.GetInt32(3).ToString()));
                cboCiudad.Items.Add(new ListItem("------", "0"));
                this.PoblarCiudad(1);
                txtNombre.Text = readerevento.GetString(6);
                Session["Idevento"] = readerevento.GetInt32(0);
                txtFechaIni.Text = readerevento.GetString(7);
                txtFechaFin.Text = readerevento.GetString(8);
                cboOrigen.SelectedValue = readerevento.GetInt32(9).ToString();
                txtObjetivo.Text = readerevento.GetString(11);
                txtConclusion.Text = readerevento.GetString(12);
            }
            readerevento.Close();
            
        }

        protected void limpiarcampos()
        {
            cboPais.Items.Clear();
            cboCiudad.Items.Clear();
            txtNombre.Text = "";
            listPart.Items.Clear();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Eventos.aspx");
        }

        protected void btnAggPart_Click(object sender, EventArgs e)
        {
            if (txtPart.Text.Trim() == "")
            {
                mensajeVentana("Por favor seleccione el participante");
            }
            else
            {
                agregarLista(listPart, txtPart.Text, lblIdPart.Value);
                txtPart.Text = "";
                lblIdPart.Value = "";
            }
        }
        protected void btnEliPart_Click(object sender, EventArgs e)
        {
            eliminarLista(listPart);
        }

        //Crea los datos para el correo
        public void enviarICS(String idEveFer)
        {
            String pais = "";
            String ciudad = "";
            String uid = "";
            String fechaIni = "";
            String fechaFin = "";
            String evento = txtNombre.Text;
            DataTable datosEveFer = CVC.consultaDatosEveFer(idEveFer);
            foreach (DataRow row in datosEveFer.Rows)
            {
                pais = row["paisFeria"].ToString();
                ciudad = row["ciudadFeria"].ToString();
                uid = row["fechaAct"].ToString();
                fechaIni = row["fechaIni"].ToString();
                fechaFin = row["fechaFin"].ToString();
            }
            crearArchivoICS("EventoFeriaCal", Session["usuario"].ToString(), txtNombre.Text, txtNombre.Text, String.Format("{0:yyyyMMdd}", DateTime.Parse(uid)).Replace("/", "").Replace("-", ""), String.Format("{0:yyyyMMdd}", DateTime.Parse(fechaIni)).Replace("/", "").Replace("-", ""), String.Format("{0:yyyyMMdd}", DateTime.Parse(fechaFin)).Replace("/", "").Replace("-", ""), ciudad, pais, uid.Trim().Replace("/", "").Replace("-", "").Replace(":", "").Replace(".", ""), idEveFer);
        }
        //elimina el item selecionado
        private void eliminarLista(ListBox list)
        {
            for (int i = 0; i < list.Items.Count; i++)
            {
                if (list.Items[i].Selected)
                {
                    list.Items.Remove(list.Items[i]);
                }
            }
        }
        //metodo general para agregar las listas limitada (No permite repeticion)
        private void agregarLista(ListBox list, String item, String value)
        {
            if (value != "")//verifico si hay algo en el id del combo
            {
                if (list.Items.Count >= 1)//verifico si hay algo en la lista que ingresa en la variable list
                {
                    String existe = "NO";
                    foreach (ListItem li in list.Items)//lleno y recorro un list por medio del la lista que esta entrando
                    {
                        if (li.Value == value.ToString())//verifico si el id ya esta en la lista
                        {
                            existe = "SI";
                        }
                    }
                    if (existe == "NO")//agrego a la lista el item y el value si no existe en lista
                    {
                        list.Items.Add(new ListItem(item.ToString(), value.ToString()));
                    }
                }
                else//si no hay agrego inmediatamente
                {
                    list.Items.Add(new ListItem(item.ToString(), value.ToString()));
                }
            }
        }
        //envia los mensajes en los alert
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        //guarda los valores de la lista y la guarda en una lista string
        private String listaString(ListBox listaAgg)
        {
            String listaString = "";
            int conList = 0;
            foreach (ListItem lis in listaAgg.Items)
            {
                if (conList == 1)
                {
                    listaString = listaString + ",'" + lis.Value + "'";
                }
                else
                {
                    listaString = listaString + "'" + lis.Value + "'";
                    conList = 1;
                }
            }
            return listaString;
        }
        //llena las listas
        private void llenarList(DataTable tabla, ListBox list, int text, int value)
        {   // 1/0  -- nom/id
            list.Items.Clear();
            foreach (DataRow row in tabla.Rows)
            {
                list.Items.Add(new ListItem(row[text].ToString(), row[value].ToString()));
            }
        }
        //Crea archivos ics para el calendario
        public void crearArchivoICS(String nomArchi, String usuPlan, String titulo, String descr, String fecha, String fechaIni, String fechaFin, String ciu, String pais, String uid, String idEveFer)
        {
            // Se crea el archivo nuevo  2
            string filename = "" + nomArchi + ".ics"; //nombre de tu arhivo con su extención, ej: archivo.txt
            String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
            rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
            String dlDir = @"/ArchivosICS\";
            String path = rutaAplicacion + dlDir + filename;//Crea la ruta completa
            using (FileStream flujoArchivo = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (StreamWriter escritor = new StreamWriter(flujoArchivo))
                {
                    escritor.WriteLine("BEGIN:VCALENDAR");
                    escritor.WriteLine("VERSION:2.0");
                    escritor.WriteLine("PRODID:-//Microsoft Corporation//Outlook 12.0 MIMEDIR//EN");
                    escritor.WriteLine("BEGIN:VEVENT");
                    escritor.WriteLine("DTSTART:" + fechaIni + "T000000");
                    escritor.WriteLine("DTEND:" + fechaFin + "T000000");
                    escritor.WriteLine("UID:" + uid + "");
                    escritor.WriteLine("STATUS:CONFIRMED");
                    escritor.WriteLine("CATEGORIES:APPOINTMENT");
                    escritor.WriteLine("DTSTAMP:" + fecha + "T000000");
                    escritor.WriteLine("LOCATION:" + ciu + "," + pais + "");
                    escritor.WriteLine("DESCRIPTION: " + descr + "");
                    escritor.WriteLine("SUMMARY;LANGUAGE=es-co: Evento: " + titulo + "");
                    escritor.WriteLine("END:VEVENT");
                    escritor.Write("END:VCALENDAR");
                }
            }
            controlEvento.correoArchiICS(usuPlan, " AND (part.part_tifuente_id = " + idEveFer + ") ", path);
        }

    }
}