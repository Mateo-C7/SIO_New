using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using CapaDatos;
using System.Diagnostics;
using System.Threading;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
namespace CapaControl
{
    public class ControlCasino
    {
        //--------------------------------------------------------------------------REALIZAR PEDIDO-------------------------------------------------------------------------------//
        //Trae los datos del usuario que ingreso al SIO
        public InfoCasino usuarioActual(String usuario)
        {
            InfoCasino infoCas = null;
            String sql = "SELECT TOP(1) area.are_nombre AS nomArea, empleado.emp_usu_num_id AS cedula, "
                          + "  empleado.emp_nombre_mayusculas + ' ' + empleado.emp_apellidos_mayusculas AS nomEmp, area.are_id AS codArea,  V_CCOST_EMP.ID_CCOSTO AS centroCostos "
                          + "  FROM  usuario INNER JOIN "
                          + "  empleado ON usuario.usu_emp_usu_num_id = empleado.emp_usu_num_id INNER JOIN "
                          + "  area ON empleado.emp_area_id = area.are_id INNER JOIN  V_CCOST_EMP ON empleado.emp_usu_num_id = V_CCOST_EMP.EMPLEADO"
                          + "  WHERE (usuario.usu_login = '" + usuario + "')";
            DataTable consulta = BdDatos.CargarTabla(sql);
            foreach (DataRow row in consulta.Rows)
            {
                infoCas = new InfoCasino();
                infoCas.CedulaEmp = int.Parse(row["cedula"].ToString());
                infoCas.NomEmp = row["nomEmp"].ToString();
                infoCas.CodArea = int.Parse(row["codArea"].ToString());
                infoCas.NomArea = row["nomArea"].ToString();
                infoCas.CentroCosto = int.Parse(row["centroCostos"].ToString());
            }
            return infoCas;
        }
        //Carga el combo de los tipos de servicios
        public DataTable cargarComboTipoSer(String filtro)
        {
            String sql = "SELECT castiposerv_id AS idServicio, castiposerv_desc AS nomServicio FROM  Casino_Tipo_Servicio WHERE   (castiposerv_Principal = 1) AND (castiposerv_activo = 1) " + filtro + "";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga el combo del los servicios especiales
        public DataTable cargarComboNomServicio()
        {
            String sql = "SELECT  castiposerv_desc AS nomServicio, castiposerv_id AS idServicio FROM Casino_Tipo_Servicio WHERE (castiposerv_activo = 1) AND (id_serv_subserv = 5)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga la tabla de los pedidos,para selecionar uno solo, dependiendo del usuarioop;, b
        public DataTable cargarGridVerPedidos(int cedula)
        {
            String sql = "SELECT Casino_Pedidos.casped_id AS numPedido, Casino_Tipo_Servicio.castiposerv_desc AS tipoSer, area.are_nombre AS area,  "
                      + "  CONVERT(char(10),  Casino_Pedidos.casped_fecha, 103) AS fechaCrea, CONVERT(CHAR(8), Casino_Pedidos.casped_hora) AS horaCrea, CONVERT(char(10), Casino_Pedidos.casped_fecha_aten, 103) AS fechaAten, "
                      + "  Casino_Pedidos.casped_cantidad AS cantidad, Casino_Pedidos.casped_Descripcion AS descripcion,  "
                      + "  (CASE WHEN Casino_Pedidos.casped_lugar_atencion IS NULL  THEN Casino_lugar_p.cas_lug_p_nombre ELSE Casino_Pedidos.casped_lugar_atencion END) AS lugar "
                      + "  FROM   Casino_Pedidos INNER JOIN "
                      + "  Casino_Tipo_Servicio ON Casino_Pedidos.casped_tipo_id = Casino_Tipo_Servicio.castiposerv_id INNER JOIN "
                      + "  empleado ON Casino_Pedidos.casped_id_emp_carne = empleado.emp_usu_num_id INNER JOIN "
                      + "  area ON empleado.emp_area_id = area.are_id LEFT OUTER JOIN Casino_lugar_p ON Casino_Pedidos.casped_id_lugar = Casino_lugar_p.cas_lug_p_id "
                      + "  WHERE (Casino_Pedidos.casped_estado = 'Abierto') AND (Casino_Pedidos.casped_emp_id = " + cedula + ") AND (Casino_Pedidos.casped_fecha_aten >= CONVERT (char(10), SYSDATETIME(), 103)) ORDER BY numPedido DESC";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga la tabla de todos los pedidos dependiendo si es un super usuario
        public DataTable cargarGridVerPedidosTodos()
        {
            String sql = "SELECT Casino_Pedidos.casped_id AS numPedido, Casino_Tipo_Servicio.castiposerv_desc AS tipoSer, area.are_nombre AS area,  "
                      + "  CONVERT(char(10),  Casino_Pedidos.casped_fecha, 103) AS fechaCrea, CONVERT(CHAR(8), Casino_Pedidos.casped_hora) AS horaCrea, CONVERT(char(10), Casino_Pedidos.casped_fecha_aten, 103) AS fechaAten, "
                      + "  Casino_Pedidos.casped_cantidad AS cantidad, Casino_Pedidos.casped_Descripcion AS descripcion,  "
                      + "  (CASE WHEN Casino_Pedidos.casped_lugar_atencion IS NULL THEN Casino_lugar_p.cas_lug_p_nombre ELSE Casino_Pedidos.casped_lugar_atencion END) AS lugar "
                      + "  FROM   Casino_Pedidos INNER JOIN "
                      + "  Casino_Tipo_Servicio ON Casino_Pedidos.casped_tipo_id = Casino_Tipo_Servicio.castiposerv_id INNER JOIN "
                      + "  empleado ON Casino_Pedidos.casped_id_emp_carne = empleado.emp_usu_num_id INNER JOIN "
                      + "  area ON empleado.emp_area_id = area.are_id LEFT OUTER JOIN Casino_lugar_p ON Casino_Pedidos.casped_id_lugar = Casino_lugar_p.cas_lug_p_id "
                      + "  WHERE (Casino_Pedidos.casped_estado = 'Abierto') AND (Casino_Pedidos.casped_fecha_aten >= CONVERT (char(10), SYSDATETIME(), 103)) ORDER BY numPedido DESC";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga el consecutivo del pedido
        public int cargarConsecutivoPedido()
        {
            String sql = "SELECT TOP (1) casped_id + 1 AS numPedido FROM Casino_Pedidos ORDER BY numPedido DESC";
            DataTable consulta = BdDatos.CargarTabla(sql);
            int numPedido = 0;
            foreach (DataRow row in consulta.Rows)
            {
                numPedido = int.Parse(row["numPedido"].ToString());
            }
            return numPedido;
        }
        //Realiza la busqueda del pedido normal y trae los datos para poder modificar
        public DataTable buscarPedidoNormal(int numPedido)
        {
            String sql = "SELECT  CONVERT(char(10),Casino_Pedidos.casped_fecha_aten, 103) AS fechaAten, Casino_Pedidos.casped_cantidad AS cantidad,  Casino_Pedidos.casped_lugar_atencion AS lugar, Casino_Tipo_Servicio.castiposerv_desc AS tipoSer, Casino_Pedidos.casped_menu_id AS idMenu, casped_tipo_id AS idServicio "
            + " FROM Casino_Pedidos INNER JOIN Casino_Tipo_Servicio ON Casino_Pedidos.casped_tipo_id = Casino_Tipo_Servicio.castiposerv_id LEFT OUTER JOIN Casino_menu ON Casino_Pedidos.casped_menu_id = Casino_menu.cas_menu_id"
            + " WHERE (Casino_Pedidos.casped_id = " + numPedido + ") ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Realiza la busqueda del pedido especial y trae los datos para poder modificar
        public DataTable buscarPedidoESP(int numPedido)
        {
            String sql = "SELECT  CONVERT(char(10),Casino_Pedidos.casped_fecha_aten, 103) AS fechaAten, Casino_Pedidos.casped_cantidad AS cantidad, Casino_Pedidos.casped_lugar_atencion AS lugar, Casino_Pedidos.casped_id_lugar AS idLugar, "
                      + "  Casino_Pedidos.casped_Descripcion AS descrip, Casino_Pedidos.casped_valor_unit AS vUnitario, Casino_Pedidos.casped_total AS vTotal, Casino_Tipo_Servicio.castiposerv_desc AS tipoSer, casped_hora_atencion AS horaAten, casped_menu_especial AS menu "
                      + "  FROM  Casino_Pedidos INNER JOIN "
                      + "  Casino_Tipo_Servicio ON Casino_Pedidos.casped_tipo_id = Casino_Tipo_Servicio.castiposerv_id "
                      + "  WHERE (Casino_Pedidos.casped_id = " + numPedido + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Insert a la tabla Casino_Pedidos dependiendo de la variable "tipo" que es el que dice si es normal o especial el pedido
        public String insertarPedido(int tipoPedido, int empleado, String fechaAntecion, int cantidad, String lugar, int area, String tipo, String descr, int valorUni, int total, String horaAten, String menu)
        {
            //Se toma la cedula del invitado
            String sql1 = "SELECT emp_usu_num_id AS invitado FROM empleado WHERE (emp_area_id = " + area + ") AND (emp_nombre_mayusculas = 'INVITADOS')";
            DataTable consulta = BdDatos.CargarTabla(sql1);
            int invitado = 0;
            foreach (DataRow row in consulta.Rows)
            {
                invitado = int.Parse(row["invitado"].ToString());
            }
            //Empieza el insert a la tabla Casino_Pedidos
            DataTable consulta2 = null;
            String mensaje = "";
            if (tipo == "normal")
            {//para pedido normal
                try
                {
                    String sql = "DECLARE @ultimoId INT;"
                    + "INSERT INTO Casino_Pedidos (casped_tipo_id, casped_fecha, casped_hora, casped_emp_id, casped_fecha_aten, casped_cantidad, casped_estado, casped_id_emp_carne, casped_lugar_atencion, casped_menu_id) "
                    + " VALUES(" + tipoPedido + ", CONVERT (char(10), SYSDATETIME(), 103), CONVERT (char(10), SYSDATETIME(), 108), " + empleado + ", '" + fechaAntecion + "', " + cantidad + ", 'Abierto', " + invitado + ", 'Casino', " + menu + ") " //'" + lugar + "') ";
                    + " SET @ultimoId = SCOPE_IDENTITY(); SELECT @ultimoId AS id;";
                    consulta2 = BdDatos.CargarTabla(sql);
                    foreach (DataRow row in consulta2.Rows)
                    {
                        mensaje = row["id"].ToString();
                    }
                }
                catch
                {
                    mensaje = "ERROR EN EL QUERY (Casino_Pedidos)";
                }
            }
            else if (tipo == "especial")//para pedido especial
            {
                try
                {
                    String sql = "DECLARE @ultimoId INT;"
                    + "INSERT INTO Casino_Pedidos (casped_tipo_id, casped_fecha, casped_hora, casped_emp_id, casped_fecha_aten, casped_cantidad, casped_Descripcion, casped_valor_unit, casped_total, casped_estado, casped_id_emp_carne, casped_id_lugar, casped_hora_atencion, casped_menu_especial) " //casped_lugar_atencion
                    + " VALUES(" + tipoPedido + ", CONVERT (char(10), SYSDATETIME(), 103), CONVERT(TIME, SYSDATETIME(),108), " + empleado + ", '" + fechaAntecion + "', " + cantidad + ", "//CONVERT (char(10), SYSDATETIME(), 108)
                    + " '" + descr + "', " + valorUni + ", " + total + ", 'Abierto', " + invitado + ", " + lugar + ", '" + horaAten + "','" + menu + "') "
                    + " SET @ultimoId = SCOPE_IDENTITY(); SELECT @ultimoId AS id;";
                    consulta2 = BdDatos.CargarTabla(sql);
                    foreach (DataRow row in consulta2.Rows)
                    {
                        mensaje = row["id"].ToString();
                    }
                }
                catch
                {
                    mensaje = "ERROR EN EL QUERY (Casino_Pedidos)";
                }
            }
            return mensaje;
        }
        //Update a la tabla Casino_Pedidos dependiendo de la variable "tipo" que es el que dice si es normal o especial el pedido y tambien del numero del pedido de la variable "numPedido"
        public String editarPedido(int numPedido, String fechaAntecion, int cantidad, String lugar, String tipo, String descr, int valorUni, int total, String horaAten, String menu)
        {
            String mensaje = "";
            if (tipo == "normal")
            {//para pedido normal
                try
                {
                    String sql = "UPDATE Casino_Pedidos SET casped_fecha = CONVERT (char(10), SYSDATETIME(), 103), casped_hora = CONVERT (char(10), SYSDATETIME(), 108), "
                    + "  casped_fecha_aten = '" + fechaAntecion + "', casped_cantidad = " + cantidad +  menu + "  WHERE casped_id = " + numPedido + ""; //, casped_lugar_atencion = '" + lugar + "'
                    BdDatos.Actualizar(sql);
                    mensaje = "OK";
                }
                catch
                {
                    mensaje = "ERROR EN EL QUERY (Casino_Pedidos)";
                }
            }
            else if (tipo == "especial")//para pedido especial
            {
                try
                {
                    String sql = "UPDATE Casino_Pedidos SET casped_fecha = CONVERT (char(10), SYSDATETIME(), 103), casped_hora = CONVERT(TIME, SYSDATETIME(),108), "//CONVERT (char(10), SYSDATETIME(), 108)
                    + "  casped_fecha_aten = '" + fechaAntecion + "', casped_cantidad = " + cantidad + " , casped_Descripcion = '" + descr + "', "
                    + "  casped_valor_unit = " + valorUni + ", casped_total = " + total + ", casped_id_lugar = " + lugar + ", casped_hora_atencion = '" + horaAten + "', casped_menu_especial = '" + menu + "' WHERE casped_id = " + numPedido + "";//casped_lugar_atencion = '" + lugar + "'
                    BdDatos.Actualizar(sql);
                    mensaje = "OK";
                }
                catch
                {
                    mensaje = "ERROR EN EL QUERY (Casino_Pedidos)";
                }
            }
            return mensaje;
        }
        //Consulta el servicio para ver que pantalla y que hora tiene
        public DataTable consultarServicio(int idServicio)
        {
            String sql = "SELECT   castiposerv_id AS idServicio, castiposerv_desc AS nombreSer, id_serv_subserv AS idSubServicio, castiposerv_pantalla AS pantSer, castiposerv_hcrea AS hcrea, castiposerv_hmod AS hmod, castiposerv_hanula AS hanula, castiposerv_actmenu AS actMenu "
            + " FROM Casino_Tipo_Servicio WHERE  (castiposerv_id = " + idServicio + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Consulta el sub servicio para ver que pantalla y que hora tiene
        public DataTable consultarSubServicio(int idServicio)
        {
            String sql = "SELECT   castiposerv_id AS idServicio, castiposerv_desc AS nombreSer, id_serv_subserv AS idSubServicio, castiposerv_pantalla AS pantSer, castiposerv_hcrea AS hcrea, castiposerv_hmod AS hmod, castiposerv_hanula AS hanula "
            + " FROM Casino_Tipo_Servicio WHERE  (id_serv_subserv = " + idServicio + ") AND (castiposerv_activo = 1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Consulta la hora actual
        public DateTime horaLocal()
        {
            String sql = "SELECT CONVERT (varchar, SYSDATETIME(), 108) AS hora";
            DataTable consulta = BdDatos.CargarTabla(sql);
            DateTime hora = new DateTime();
            foreach (DataRow row in consulta.Rows)
            {
                hora = DateTime.Parse(row["hora"].ToString());
            }
            return hora;
        }
        //Consulta la fecha actual
        public DateTime fechaLocal()
        {
            String sql = "SELECT CONVERT (varchar, SYSDATETIME(), 103) AS fecha";
            DataTable consulta = BdDatos.CargarTabla(sql);
            DateTime hora = new DateTime();
            foreach (DataRow row in consulta.Rows)
            {
                hora = DateTime.Parse(row["fecha"].ToString());
            }
            return hora;
        }
        //Busca la hora y la fecha para editar el pedido
        public DataTable buscarTipoServiEditar(int numPedido)
        {
            String sql = "SELECT castiposerv_desc AS nomSer, Casino_Tipo_Servicio.id_serv_subserv AS tipoSubSer, Casino_Tipo_Servicio.castiposerv_id AS tipoSer, Casino_Tipo_Servicio.castiposerv_hmod AS hora, Casino_Pedidos.casped_fecha_aten AS fecha "
            + " FROM Casino_Pedidos INNER JOIN Casino_Tipo_Servicio ON Casino_Pedidos.casped_tipo_id = Casino_Tipo_Servicio.castiposerv_id "
            + " WHERE (Casino_Pedidos.casped_id = " + numPedido + ")";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Me trae los datos para llenar el correo
        private DataTable llenarCorreoInf(int numPedido)
        {
            String sql = "SELECT empleado.emp_nombre + ' ' + empleado.emp_apellidos AS nombre, Casino_Tipo_Servicio.castiposerv_desc AS nombreServicio, "
            + " CONVERT(CHAR(8), Casino_Pedidos.casped_hora) AS hora, Casino_Pedidos.casped_fecha AS fechaCrea, CONVERT(char(10), Casino_Pedidos.casped_fecha_aten, 103) AS fechaAtencio, "
            + " Casino_Pedidos.casped_cantidad AS cantidad, Casino_Pedidos.casped_Descripcion AS Descripcion, Casino_menu.cas_menu_descrip AS menuNor,  Casino_Pedidos.casped_menu_especial AS menuESP, "
            + " (CASE WHEN Casino_Pedidos.casped_lugar_atencion IS NULL THEN Casino_lugar_p.cas_lug_p_nombre ELSE Casino_Pedidos.casped_lugar_atencion END) AS lugar, Casino_Pedidos.casped_valor_unit AS valorUnit, Casino_Pedidos.casped_total AS valorTotal, Casino_Pedidos.casped_hora_atencion AS horaAten "
            + " FROM  Casino_Pedidos INNER JOIN "
            + " Casino_Tipo_Servicio ON Casino_Pedidos.casped_tipo_id = Casino_Tipo_Servicio.castiposerv_id INNER JOIN "
            + " empleado ON Casino_Pedidos.casped_emp_id = empleado.emp_usu_num_id  LEFT OUTER JOIN  Casino_menu ON  Casino_Pedidos.casped_menu_id = Casino_menu.cas_menu_id "
            //+ " usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id LEFT OUTER JOIN Casino_menu ON Casino_Pedidos.casped_menu_id = Casino_menu.cas_menu_id"
            + " LEFT OUTER JOIN Casino_lugar_p ON Casino_Pedidos.casped_id_lugar = Casino_lugar_p.cas_lug_p_id "
            + " WHERE (Casino_Pedidos.casped_id = " + numPedido + ") "
            + " GROUP BY empleado.emp_nombre + ' ' + empleado.emp_apellidos, Casino_Tipo_Servicio.castiposerv_desc, Casino_Pedidos.casped_hora, Casino_lugar_p.cas_lug_p_nombre,"
            + " Casino_Pedidos.casped_fecha, Casino_Pedidos.casped_fecha_aten, Casino_Pedidos.casped_cantidad, Casino_Pedidos.casped_Descripcion, Casino_Pedidos.casped_menu_especial, "
            + " Casino_Pedidos.casped_lugar_atencion, Casino_Pedidos.casped_valor_unit, Casino_Pedidos.casped_total, Casino_Pedidos.casped_hora_atencion, Casino_menu.cas_menu_descrip ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Me permite saber si es un super usuario
        public Boolean superUsuarioCasino(String usuario)
        {
            Boolean tiene = false;
            String sql = "SELECT usu_login  FROM  usuario WHERE  (usu_login = '" + usuario + "') AND (usu_admin_casino = 1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            if (consulta.Rows.Count > 0)
            {
                tiene = true;
            }
            else
            {
                tiene = false;
            }
            return tiene;
        }
        //Me permite saber si tiene permiso para realizar el pedido
        public Boolean permisosCasino(String usuario)
        {
            Boolean tiene = false;
            String sql = "SELECT  TOP (1) empleado.emp_autorizado_pedido_cas, usuario.usu_login FROM  empleado INNER JOIN "
            + " usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id  WHERE   (usuario.usu_login = '" + usuario + "') AND (empleado.emp_activo = 1) AND (empleado.emp_autorizado_pedido_cas = 1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            if (consulta.Rows.Count > 0)
            {
                tiene = true;
            }
            else
            {
                tiene = false;
            }
            return tiene;
        }
        //Carga el combo de costos
        public DataTable cargarComboCostos() {
            String sql = "SELECT  empleado.emp_area_id AS idArea, NmUno.dbo.Nm_Equi_CC.Equi_Cen_Cos_DESC AS nomCentro "
            +" FROM empleado INNER JOIN "
            + " NmUno.dbo.Nm_Equi_CC ON empleado.emp_cen_cos_NmUno = NmUno.dbo.Nm_Equi_CC.Equi_Cen_Cos_CgUno "
            + " WHERE (empleado.emp_nombre = 'Invitados') AND (empleado.emp_activo = 1) ORDER BY nomCentro";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga lugar de pedidos
        public DataTable cargarLugarP()
        {
            String sql = "SELECT cas_lug_p_id AS idLugarP, cas_lug_p_nombre AS nomLugarP FROM Casino_lugar_p WHERE (cas_lug_p_activo = 1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga menus de tipo de servicio
        public DataTable cargarMenus(String idTipoS, String filtro)
        {
            String sql = "SELECT Casino_menu.cas_menu_id AS idMenu, Casino_menu.cas_menu_descrip AS nomMenu, (CASE WHEN Casino_menu.cas_menu_activo = 1 THEN 'SI' ELSE 'NO' END) AS actMenu FROM Casino_mserv INNER JOIN "
            + " Casino_menu ON Casino_mserv.cas_mserv_menuid = Casino_menu.cas_menu_id INNER JOIN  Casino_Tipo_Servicio ON Casino_mserv.cas_mserv_tipoid = Casino_Tipo_Servicio.castiposerv_id "
            + " WHERE  (Casino_mserv.cas_mserv_tipoid = " + idTipoS + ") " + filtro + " ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Inserta datos Menu
        public String insertarMenu(String nomMenu, String actMenu)
        {
            String mensaje = "";
            DataTable consulta = null;
            try
            {
                String sql = "DECLARE @ultimoId INT;"
                + " INSERT INTO Casino_menu (cas_menu_descrip, cas_menu_activo) VALUES ('" + nomMenu + "', '" + actMenu + "');"
                + " SET @ultimoId = SCOPE_IDENTITY(); SELECT @ultimoId AS id;";
                consulta = BdDatos.CargarTabla(sql);
                foreach (DataRow row in consulta.Rows)
                {
                    mensaje = row["id"].ToString();
                }
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT Casino_menu)";
            }
            return mensaje;
        }
        //Inserta datos Menu con Servicio Tecnico
        public String insertarMenuServ(String idSer, String idMenu)
        {
            String mensaje = "";
            try
            {
                String sql = "INSERT INTO Casino_mserv (cas_mserv_tipoid, cas_mserv_menuid) VALUES (" + idSer + ", " + idMenu + ");";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (INSERT Casino_mserv)";
            }
            return mensaje;
        }
        //Inserta datos Menu con Servicio Tecnico
        public String editarMenu(String idMenu, String nomMenu, String actMenu)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE Casino_menu SET Casino_menu.cas_menu_descrip = '" + nomMenu + "' , cas_menu_activo = '" + actMenu + "' WHERE Casino_menu.cas_menu_id = " + idMenu + "";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY (UPDATE Casino_menu)";
            }
            return mensaje;
        }
        //--------------------------------------------------------------------------REALIZAR PEDIDO-------------------------------------------------------------------------------//
        //--------------------------------------------------------------------------CONSULAR PEDIDO-------------------------------------------------------------------------------//
        //carga la tabla para pedidos especiales
        public DataTable llenarTablaEspecial(String condicion)
        {
            String sql = "SELECT  Casino_Pedidos.casped_id AS numPedido, CONVERT(char(10), Casino_Pedidos.casped_fecha, 103) AS fechaCrea, "
                      + " CONVERT(CHAR(10), Casino_Pedidos.casped_hora ,108) AS horaCrea, CONVERT(char(10), Casino_Pedidos.casped_fecha_aten, 103) AS fechaAten,  "
                      + " Casino_Pedidos.casped_cantidad AS cantidad, Casino_Pedidos.casped_Descripcion AS descripcion, Casino_Pedidos.casped_valor_unit AS valorUni, "
                      + " Casino_Pedidos.casped_total AS valorTotal, Casino_lugar_p.cas_lug_p_nombre AS lugar, "
                      + " Casino_Tipo_Servicio.castiposerv_desc AS tipoServicio, Casino_Pedidos.casped_estado AS estado, "
                      + " Casino_Pedidos.casped_hora_atencion AS horaAten, empleado.emp_nombre + ' ' + empleado.emp_apellidos AS usuario "
                      + " FROM  Casino_Pedidos INNER JOIN "
                      + " Casino_Tipo_Servicio ON Casino_Pedidos.casped_tipo_id = Casino_Tipo_Servicio.castiposerv_id INNER JOIN "
                      + " empleado ON Casino_Pedidos.casped_emp_id = empleado.emp_usu_num_id INNER JOIN"
                      + " usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id LEFT OUTER JOIN Casino_lugar_p ON Casino_Pedidos.casped_id_lugar = Casino_lugar_p.cas_lug_p_id"
                      + " WHERE (Casino_Tipo_Servicio.id_serv_subserv > 0) AND (Casino_Pedidos.casped_estado = 'Abierto')  "
                      + " " + condicion + ""
                      + "  GROUP BY Casino_Pedidos.casped_id , Casino_Pedidos.casped_fecha, Casino_Pedidos.casped_fecha_aten, Casino_Pedidos.casped_cantidad, Casino_Pedidos.casped_Descripcion, "
                      + " Casino_Pedidos.casped_valor_unit, Casino_Pedidos.casped_total , Casino_Pedidos.casped_lugar_atencion, Casino_Tipo_Servicio.castiposerv_desc, Casino_Pedidos.casped_estado, "
                      + " Casino_Pedidos.casped_hora_atencion, empleado.emp_nombre + ' ' + empleado.emp_apellidos, Casino_Pedidos.casped_hora, Casino_lugar_p.cas_lug_p_nombre ";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Carga la tabla de pedidos normales
        public DataTable llenarTablaNormal(String condicion)
        {
            String sql = "SELECT TOP (30) Casino_Pedidos.casped_id AS numPedido, CONVERT(char(10), Casino_Pedidos.casped_fecha, 103) AS fechaCrea, "
                      + " Casino_Pedidos.casped_hora AS horaCrea, CONVERT(char(10), Casino_Pedidos.casped_fecha_aten, 103) AS fechaAten, "
                      + " Casino_Pedidos.casped_cantidad AS cantidad, Casino_Pedidos.casped_lugar_atencion AS lugar, "
                      + " Casino_Tipo_Servicio.castiposerv_desc AS tipoServicio, Casino_Pedidos.casped_estado AS estado, "
                      + " empleado.emp_nombre + ' ' + empleado.emp_apellidos AS usuario "
                      + " FROM Casino_Pedidos INNER JOIN "
                      + " Casino_Tipo_Servicio ON Casino_Pedidos.casped_tipo_id = Casino_Tipo_Servicio.castiposerv_id INNER JOIN "
                      + " empleado ON Casino_Pedidos.casped_emp_id = empleado.emp_usu_num_id INNER JOIN"
                      + " usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id "
                      + " WHERE (Casino_Tipo_Servicio.id_serv_subserv = 0) AND (Casino_Pedidos.casped_estado = 'Abierto') "
                      + " " + condicion + "";
            DataTable consulta = BdDatos.CargarTabla(sql);
            return consulta;
        }
        //Edita el estado del pedido
        public String editarEstadoPedido(int numPedido, String estadoNew)
        {
            String mensaje = "";
            try
            {
                String sql = "UPDATE  Casino_Pedidos SET casped_estado = '" + estadoNew + "' WHERE (casped_id = " + numPedido + ")";
                BdDatos.Actualizar(sql);
                mensaje = "OK";
            }
            catch
            {
                mensaje = "ERROR EN EL QUERY AL MODIFICAR (Casino_Pedidos)";
            }
            return mensaje;
        }
        //Consulta la hora actual pero la manda en string
        public String horaLocalString()
        {
            String sql = "SELECT CONVERT (varchar, SYSDATETIME(), 108) AS hora";
            DataTable consulta = BdDatos.CargarTabla(sql);
            String hora = "";
            foreach (DataRow row in consulta.Rows)
            {
                hora = row["hora"].ToString();
            }
            return hora;
        }
        //Consulta la fecha actual pero la manda en string
        public String fechaLocalString()
        {
            String sql = "SELECT CONVERT (varchar, SYSDATETIME(), 103) AS fecha";
            DataTable consulta = BdDatos.CargarTabla(sql);
            String hora = "";
            foreach (DataRow row in consulta.Rows)
            {
                hora = row["fecha"].ToString();
            }
            return hora;
        }
        //--------------------------------------------------------------------------CONSULAR PEDIDO-------------------------------------------------------------------------------//
        //Se crea el correo
        //Este metodo crea el correo SIN archivo adjunto y con un metodo donde se crea el cuerpo HTML
        public Boolean correoCasino(String tipo, int numPedido, String operacion, String tipoCorreo)
        {
            String correoF = "";
            String contraF = "";
            String host = "";
            DataTable correForsa = cosultarCorreos();
            foreach (DataRow row2 in correForsa.Rows)
            {
                correoF = row2["correo"].ToString();
                contraF = row2["contra"].ToString();
                host = row2["host"].ToString();
            }
            // String correos = "jcardona@metrolinkeu.com, mastudillo@metrolinkeu.com, jandres5151@gmail.com,<";
            String titulo = "";
            String correos = "";
            String asunto = "";
            DataTable tbCorreos = cosultarCorreos(tipoCorreo);
            foreach (DataRow row in tbCorreos.Rows)
            {
                correos = correos + row["correoEmp"].ToString() + ",";
                asunto = row["asunto"].ToString();
                titulo = row["titulo"].ToString();
            }
            correos = correos + "<";
            //Configuración del Mensaje
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient();//forsa : smtp.office365.com // gmail : smtp.gmail.com
            //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
            mail.From = new MailAddress(correoF, titulo, Encoding.UTF8);
            //Aquí ponemos el asunto del correo
            mail.Subject = asunto + operacion.ToUpper();
            //Aquí ponemos el mensaje que incluirá el correo
            mail.Body = cuerpoCorreo(tipo, numPedido, operacion);
            mail.IsBodyHtml = true;
            //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
            mail.To.Add((correos.Replace(",<", " ")));
            //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran
            // mail.Attachments.Add(new Attachment(@"C:\Documentos\carta.docx"));
            //Configuracion del SMTP
            SmtpServer.Host = host;
            //Especificamos las credenciales con las que enviaremos el mail
            SmtpServer.Credentials = new System.Net.NetworkCredential(correoF, contraF);
            SmtpServer.Port = 25; //Puerto que utiliza Gmail para sus servicios // gmail : 587 // forsa : 25
            SmtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            try
            {
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false; 
            }
        }
        //Se construye el cuerpo en HTML por completo para el correoCasino
        private String cuerpoCorreo(String tipo, int numPedido, String operacion)
        {
            DataTable datosCorreo = llenarCorreoInf(numPedido);
            String pedido = "";
            if (tipo == "normal")
            {
                foreach (DataRow row in datosCorreo.Rows)
                {
                    String menu = "";
                    if(row["menuNor"].ToString() != "")
                    { menu = " Menu: <strong>" + row["menuNor"].ToString() + " </strong><br /> "; }
                    String camposNormal = "Nombre del Sevicio: <strong>" + row["nombreServicio"].ToString().ToUpper() + " </strong><br /> "
                    + " Cantidad de personas: <strong>" + row["cantidad"].ToString().ToUpper() + " </strong><br /> "
                    + " Fecha de Atencion: <strong>" + row["fechaAtencio"].ToString().ToUpper() + " </strong><br /> "
                    + " Lugar de Atencion: <strong>" + row["lugar"].ToString() + " </strong><br /> "
                    + menu
                    + " <br /> "
                    + " <br />";
                    pedido = camposNormal;
                }
            }
            else if (tipo == "especial")
            {
                foreach (DataRow row in datosCorreo.Rows)
                {
                    String menu = "";
                    if (row["menuESP"].ToString() != "")
                    { menu = " Menu: <strong>" + row["menuESP"].ToString() + " </strong><br /> "; }
                    String camposEspecial = "Nombre del Sevicio: <strong>" + row["nombreServicio"].ToString().ToUpper() + " </strong><br /> "
                    + " Cantidad de personas: <strong>" + row["cantidad"].ToString().ToUpper() + " </strong><br /> "
                    + " Fecha de Atencion: <strong>" + row["fechaAtencio"].ToString().ToUpper() + " </strong><br /> "
                    + " Lugar de Atencion: <strong>" + row["lugar"].ToString().ToUpper() + " </strong><br /> "
                    + " Hora de Atencion: <strong>" + row["horaAten"].ToString().ToUpper() + " </strong><br /> "
                    + " Valor Unitario: <strong>" + Convert.ToDecimal(row["valorUnit"].ToString()) + " </strong><br /> "
                    + " Valor Total: <strong>" + Convert.ToDecimal(row["valorTotal"].ToString()) + " </strong><br /> "
                    + " Descripcion: <strong>" + row["Descripcion"].ToString().ToUpper() + " </strong><br /> "
                    + menu
                    + " <br />"
                    + " <br />";
                    pedido = camposEspecial;
                }
            }
            String cuerpo = "";
            foreach (DataRow row in datosCorreo.Rows)
            {
                cuerpo = "<html> "
                + " <head> "
                + " <title></title> "
                + " </head> "
                + " <body style='width: 740px; height: 264px'> "
                + " <br /> "
                + " Buen día Sr.C&J Casinos, "
                + " <br /> "
                + " <br /> "
                + " El usuario<strong> " + row["nombre"].ToString().ToUpper() + " </strong> ha <strong>" + operacion.ToUpper() + "</strong> el pedido #<strong>" + numPedido + "</strong> el día <strong> " + row["fechaCrea"].ToString().Substring(0, 10) + " </strong> a las "
                + " <strong> " + row["hora"].ToString() + " </strong> con los siguientes datos: "
                + " <br /> "
                + " <br /> "
                + pedido
                + " Muchas gracias por su atención. "
                + " <br /> "
                + " </body> "
                + " </html>";
            }
            return cuerpo;
        }
        //--------------------------------------------------------------------------REALIZAR PEDIDO-------------------------------------------------------------------------------//
        //--------------------------------------------------------------------------REPORTE PEDIDO-------------------------------------------------------------------------------//
        //Este metodo crea el correo CON archivo adjunto y con un metodo donde se crea el cuerpo HTML
        public Boolean correoCasino2(String path, String fechaInicial, String fechaFin, String usuario, String tipoArchivo, String tipoCorreo)
        {
                String correoF ="";
                String contraF = "";
                String host = "";
                DataTable correForsa = cosultarCorreos();
                foreach (DataRow row2 in correForsa.Rows)
                {
                    correoF = row2["correo"].ToString();
                    contraF = row2["contra"].ToString();
                    host = row2["host"].ToString();
                }
                // String correos = "jcardona@metrolinkeu.com, mastudillo@metrolinkeu.com, jandres5151@gmail.com,<";
                String titulo = "";
                String correos = "";
                String asunto = "";
                DataTable tbCorreos = cosultarCorreos(tipoCorreo);
                foreach (DataRow row in tbCorreos.Rows)
                {
                    correos = correos + row["correoEmp"].ToString() + ",";
                    asunto = row["asunto"].ToString();
                    titulo = row["titulo"].ToString();
                }
                correos = correos + "<";
                //Configuración del Mensaje
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();//forsa : smtp.office365.com // gmail : smtp.gmail.com
                //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                mail.From = new MailAddress(correoF, titulo, Encoding.UTF8);
                //Aquí ponemos el asunto del correo
                mail.Subject = asunto + tipoArchivo;
                //Aquí ponemos el mensaje que incluirá el correo
                mail.Body = cuerpoCorreo2(fechaInicial, fechaFin, usuario, tipoArchivo);
                mail.IsBodyHtml = true;
                //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                mail.To.Add((correos.Replace(",<", " ")));
                SmtpServer.Host = host;
                //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran
                Attachment data = new Attachment(path, System.Net.Mime.MediaTypeNames.Application.Octet);
                mail.Attachments.Add(data);
                //Configuracion del SMTP
                //Especificamos las credenciales con las que enviaremos el mail
                SmtpServer.Credentials = new System.Net.NetworkCredential(correoF, contraF);
                SmtpServer.Port = 25; //Puerto que utiliza Gmail para sus servicios // gmail : 587 // forsa : 25
                SmtpServer.EnableSsl = true; 
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                try
                {
                    SmtpServer.Send(mail);
                    data.Dispose();
                    return true;
                }
                catch (Exception ex)
                {
                    data.Dispose();
                    return false;
                }
        }
        //Se construye el cuerpo en HTML por completo para el correoCasino2
        private String cuerpoCorreo2(String fechaInicial , String fechaFin, String usuario, String tipoArchivo)
        {
            String cuerpo = "";
                cuerpo = "<html> "
                + " <head> "
                + " <title></title> "
                + " </head> "
                + " <body style='width: 740px; height: 264px'> "
                + " <br /> "
                + " Buen día, "
                + " <br /> "
                + " <br /> "
                + " Para su información y gestión se adjunta el archivo plano " + tipoArchivo.ToUpper() + " que contiene el registro de los consumos del casino correspondiente al periodo " + fechaInicial + " -- " + fechaFin + " "
                + " <br /> "
                + " Realizado por el usuario: " + usuario + ""
                + " <br /> "
                + " <br /> "
                + " Muchas gracias por su atención. "
                + " <br /> "
                + " </body> "
                + " </html>";
            return cuerpo;
        }
        //Consulta el procedimiento para crear los nmuno
        public DataTable crearNMUNO(String fechaInicial, String fechaFinal, String empresa)
        {
            DataTable consulta = null;
            String proc = "EXEC ReporteNMUNO '" + fechaInicial + "', '" + fechaFinal + "', '" + empresa + "'";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Consulta el procedimiento para crear el arp fr
        public DataTable crearERPFR(String fechaInicial, String fechaFinal, String campo1, int compa)
        {
            DataTable consulta = null;
            String proc = "EXEC ReporteERPtodos '" + fechaInicial + "', '" + fechaFinal + "', '" + campo1 + "', " + compa + "";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Consulta el procedimiento para crear el arp as
        public DataTable crearERPAS(String fechaInicial, String fechaFinal, String campo1, String empresa, int compa, int idLinea)
        {
            DataTable consulta = null;
            String proc = "EXEC ReporteERPempresa '" + fechaInicial + "', '" + fechaFinal + "', '" + campo1 + "', '" + empresa + "', " + compa + ", " + idLinea + "";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Me permite saber si es el usuario tiene permisos para descargar archivos planos
        public Boolean usuarioCasinoArchPlano(String usuario)
        {
            Boolean tiene = false;
            String sql = "SELECT usu_archPlano_nmuno FROM  usuario WHERE  (usu_login = '" + usuario + "') AND (usu_archPlano_nmuno = 1)";
            DataTable consulta = BdDatos.CargarTabla(sql);
            if (consulta.Rows.Count > 0)
            {
                tiene = true;
            }
            else
            {
                tiene = false;
            }
            return tiene;
        }
        //Consulta los correos
        public DataTable cosultarCorreos(String tipo)
        {
            DataTable consulta = null;
            String proc = "SELECT     empleado.emp_nombre AS nomEmp, empleado.emp_correo_electronico AS correoEmp, mensaje.men_asunto AS asunto, mensaje.men_mensaje AS titulo "
                + " FROM         mail_proceso INNER JOIN "
                + " mensaje ON mail_proceso.mail_proc_mensaje_id = mensaje.men_id INNER JOIN "
                + " empleado ON mail_proceso.mail_proc_empleado_id = empleado.emp_usu_num_id "
                + " WHERE     (mail_proceso.mail_proc_activo = 1) AND (mensaje.men_tipo = '" + tipo + "')";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }
        //Consulta el correo forsa
        public DataTable cosultarCorreos()
        {
            DataTable consulta = null;
            String proc = "SELECT TOP(1) par_correo AS correo, par_correo_contra AS contra, par_host AS host FROM  Parametros";
            consulta = BdDatos.CargarTabla(proc);
            return consulta;
        }

        //--------------------------------------------------------------------------REPORTE PEDIDO-------------------------------------------------------------------------------//
    }
}