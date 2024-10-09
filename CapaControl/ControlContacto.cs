using System;
using System.Data.SqlClient;
using CapaDatos;
using System.Data;
using System.Collections.Generic;
using CapaControl.Entity;
using System.Linq;

namespace CapaControl
{
    public class ControlContacto
    {
        public  DataSet ConsultarIdiomaContacto()
        {
            string sql;           
            sql = "SELECT     idioma_contacto.* FROM idioma_contacto";
            DataSet ds_idiomaContacto = BdDatos.consultarConDataset(sql);
            // DataTable dt_temp = cambiarIdioma(ds_idioma.Tables[0]);
            return ds_idiomaContacto;     
        }

        public SqlDataReader obtenerCargo()
        {            
            string sql;
            sql = "SELECT     tipo_cargo_id, tipo_cargo_descripcion "+
                   " FROM         tipo_cargo "+
                   " WHERE     (tipo_cargo_activo = 1) " +
                   " ORDER BY tipo_cargo_idtipocontacto, tipo_cargo_nivel";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader obtenerCargoId(int idTipo)
        {
            string sql;
            sql = "SELECT     tipo_cargo_id, tipo_cargo_descripcion " +
                   " FROM         tipo_cargo " +
                   " WHERE     (tipo_cargo_activo = 1) AND (tipo_cargo_idtipocontacto = "+idTipo+" ) " +
                   " ORDER BY tipo_cargo_idtipocontacto, tipo_cargo_nivel";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader obtenerProfesion()
        {            
            string sql;
            sql = "SELECT     tipo_prof_id, tipo_prof_nombre "+
                    "FROM         tipo_profesion "+
                    "WHERE     (tipo_prof_activo = 1)";
            return BdDatos.ConsultarConDataReader(sql);
        }

        

        public SqlDataReader ConsultarTipoContacto()
        {
            string sql;
            sql = "SELECT tcc_id, tcc_espanol, tcc_ingles, tcc_portugues " +
                  "FROM tipo_contacto ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarTipoContactoID(int tipo)
        {
            string sql;
            sql = "SELECT tcc_id, tcc_espanol, tcc_ingles, tcc_portugues " +
                  "FROM tipo_contacto " +
                  "WHERE tcc_id = " + tipo + " ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarProfesion()
        {
            string sql;
            sql = "SELECT prof_id, prof_espanol, prog_ingles, prof_portugues " +
                  "FROM profesiones ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarProfesionID(int prof)
        {
            string sql;
            sql = "SELECT prof_id, prof_espanol, prog_ingles, prof_portugues " +
                  "FROM profesiones " +
                  "WHERE prof_id = " + prof + " ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public int cerrarConexion()
        {
            return BdDatos.desconectar();
        }

        public SqlDataReader consultarFeria()
        {
            string sql;            

            sql = " SELECT        lite_tipo_fuente.tifuente_id, lite_tipo_fuente.tifuente_descripcion " +
            " FROM            lite_tipo_fuente INNER JOIN "+
            "             lite_tipo_origen ON lite_tipo_fuente.tifuente_origen_id = lite_tipo_origen.tiorigen_id "+
            " WHERE        (lite_tipo_fuente.tifuente_activo = 1) AND (lite_tipo_origen.tiorigen_maestro = 1) " +
            " ORDER BY lite_tipo_fuente.tifuente_descripcion ";
            //sql = "SELECT     fer_id, fer_nombre FROM   ferias WHERE     (fer_id <> 5)ORDER BY fer_nombre";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //Consultar Nombre Representante
        public SqlDataReader consultarNombre(string nombre, string nRol, string pRol)
        {
            string sql;
            sql = "SELECT empleado.emp_nombre + ' ' + empleado.emp_apellidos AS Nombre, emp_correo_electronico, emp_area_id " +
                "FROM empleado INNER JOIN usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id " +
                "WHERE (usuario.usu_login = '" + nombre + "')";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader poblarListaPais()
        {
            string sql;
            sql = "SELECT pai_id, pai_nombre " +
                  "FROM pais " +
                  "ORDER BY pai_nombre";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader poblarListaPaisRepresentante(int representante)
        {
            string sql;

            sql = "SELECT pais.pai_nombre, pais.pai_id, representantes_comerciales.rc_id " +
                "FROM pais_representante INNER JOIN representantes_comerciales ON " +
                "pais_representante.pr_id_representante = representantes_comerciales.rc_id INNER JOIN " +
                "pais ON pais_representante.pr_id_pais = pais.pai_id " +
                "WHERE (representantes_comerciales.rc_id = " + representante + ") AND (pais_representante.pr_activo = 1) ";

            return BdDatos.ConsultarConDataReader(sql);

        }

        public SqlDataReader poblarZona(string nRol, string pRol)
        {
            string sql;

            sql = "SELECT tipzon_id, tipzon_nombre FROM tipo_zona ORDER BY tipzon_nombre";

            return BdDatos.ConsultarConDataReader(sql);

        }

        public SqlDataReader poblarCiudadesRepresentantesColombia(int pai_id, int rc_id)
        {
            string sql;

            sql = "SELECT cr_ciu_id, ciudad.ciu_nombre " +
                "FROM ciudad_representante INNER JOIN pais ON ciudad_representante.cr_pai_id = pais.pai_id INNER JOIN " +
                "ciudad ON ciudad_representante.cr_ciu_id = ciudad.ciu_id AND pais.pai_id = ciudad.ciu_pai_id " +
                "WHERE (ciudad_representante.cr_pai_id = " + pai_id + ") AND (ciudad_representante.cr_rc_id = " + rc_id + ")" +
                "ORDER BY ciudad.ciu_nombre";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader poblarListaCiudades(int pai_id)
        {
            string sql;

            sql = "SELECT ciu_id, ciu_nombre FROM ciudad WHERE ciu_pai_id = " + pai_id + " ORDER BY ciu_nombre";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader consultarDatosClientexCiudad(int ciu_id)
        {
            string sql;

            sql = "SELECT cli_id, cli_nombre, pai_nombre, ciu_nombre " +
                    "FROM  cliente INNER JOIN pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN ciudad ON cliente.cli_ciu_id =                     ciudad.ciu_id AND pais.pai_id = ciudad.ciu_pai_id " +
                    "WHERE cli_ciu_id = " + ciu_id + " AND cli_activo = 1 ORDER BY cli_nombre";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ObtnObrasDistPV(int cliente)
        {
            string sql;
            sql = " SELECT DISTINCT obra.obr_id, obra.obr_nombre, cliente.cli_id, obra.obr_seguimiento " +
                  "FROM obra INNER JOIN " +
                  "cliente ON obra.obr_cli_id = cliente.cli_id " +
                  "WHERE (cliente.cli_id = " + cliente + ") AND (obra.obr_perdido = 0) OR " +
                  "(cliente.cli_id = " + cliente + ") AND (obra.obr_seguimiento = 0) " +
                  "ORDER BY obr_nombre ASC ";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ObtnObrasDistFUP(int cliente)
        {
            string sql;
            sql = "SELECT DISTINCT obra.obr_id, obra.obr_nombre, obra.obr_cli_id, obra.obr_seguimiento " +
                     " FROM         obra INNER JOIN " +
                     " cliente ON obra.obr_cli_id = cliente.cli_id LEFT OUTER JOIN " +
                     " formato_unico ON obra.obr_id = formato_unico.fup_obr_id " +
                     " WHERE     (formato_unico.fup_obr_id IS NULL) AND (obra.obr_cli_id = " + cliente + " )";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //LLAMAMOS EL PROCEDIMIENTO DE INGRESO DE CONTACTO CLIENTE
        public int guardarDatosContactoCliente(int clienteId, int obraId, int tipoCargoId, string nombre1, string nombre2, string apellido1, string apellido2,
                                               string telefono1, string telefono2, string email1, string email2, string movil, bool contCliente, bool contObra,
                                               bool contTecnico, string hobby, string fechacumple, int feriaId, string usucrea, string fechacrea,
                                               bool chtelefono, bool chemail,bool chreferencia,bool chferia,bool chtrabcampo,bool chvisita, bool chmedicomunic,
                                               bool chcharlas, bool chconferencia,bool chweb,bool chseminarios,bool chpublicidad,bool chpersonal,
                                               string comentarios, int profesion, string nombrecargo,string prefijo1, string prefijo2, string prefijo3, bool noNotificar, string linkedin)
        {
            int contCli = 0, contObr = 0, contTec = 0 , no_notificar = 0;
            if (contCliente == true)
                contCli = 1;
            if (contObra == true)
                contObr = 1;
            if (contTecnico == true)
                contTec = 1;

            if (noNotificar == true) no_notificar = 1;

            int telefono = 0, email = 0, referencia = 0, feria = 0, trabcampo = 0, visita = 0, medicomunic = 0, charlas = 0
                , conferencia = 0, web = 0, seminarios = 0, publicidad = 0, personal = 0;

            if (chtelefono == true)
                telefono = 1;
            if (chemail == true)
                email = 1;
            if (chreferencia == true)
                referencia = 1;
            if (chferia == true)
                feria = 1;
            if (chtrabcampo == true)
                trabcampo = 1;
            if (chvisita == true)
                visita = 1;
            if (chmedicomunic == true)
                medicomunic = 1;
            if (chcharlas == true)
                charlas = 1;
            if (chconferencia == true)
                conferencia = 1;
            if (chweb == true)
                web = 1;
            if (chseminarios == true)
                seminarios = 1;
            if (chpublicidad == true)
                publicidad = 1;
            if (chpersonal == true)
                personal = 1;

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[41];
            sqls[0] = new SqlParameter("clienteId", clienteId);
            sqls[1] = new SqlParameter("obraId", obraId);
            sqls[2] = new SqlParameter("tipoCargoId", tipoCargoId);
            sqls[3] = new SqlParameter("nombre1", nombre1);
            sqls[4] = new SqlParameter("nombre2", nombre2);
            sqls[5] = new SqlParameter("apellido1", apellido1);
            sqls[6] = new SqlParameter("apellido2", apellido2);
            sqls[7] = new SqlParameter("telefono1", telefono1);
            sqls[8] = new SqlParameter("telefono2", telefono2);
            sqls[9] = new SqlParameter("email1", email1);
            sqls[10] = new SqlParameter("email2", email2);
            sqls[11] = new SqlParameter("movil", movil);
            sqls[12] = new SqlParameter("contCli ", contCli);
            sqls[13] = new SqlParameter("contObr ", contObr);
            sqls[14] = new SqlParameter("contTec", contTec);
            sqls[15] = new SqlParameter("hobby", hobby);
            sqls[16] = new SqlParameter("fechacumple", fechacumple);
            sqls[17] = new SqlParameter("feriaId", feriaId);
            sqls[18] = new SqlParameter("usucrea", usucrea);
            sqls[19] = new SqlParameter("fechacrea", fechacrea);
            
            sqls[20] = new SqlParameter("telefono", telefono);
            sqls[21] = new SqlParameter("email", email);
            sqls[22] = new SqlParameter("referencia", referencia);
            sqls[23] = new SqlParameter("feria", feria);
            sqls[24] = new SqlParameter("trabcampo", trabcampo);
            sqls[25] = new SqlParameter("visita", visita);
            sqls[26] = new SqlParameter("medicomunic", medicomunic);
            sqls[27] = new SqlParameter("charlas ", charlas);
            sqls[28] = new SqlParameter("conferencia ", conferencia);
            sqls[29] = new SqlParameter("web", web);
            sqls[30] = new SqlParameter("seminarios", seminarios);
            sqls[31] = new SqlParameter("publicidad", publicidad);
            sqls[32] = new SqlParameter("personal", personal);
            sqls[33] = new SqlParameter("comentarios", comentarios);
            sqls[34] = new SqlParameter("profesion", profesion);
            sqls[35] = new SqlParameter("nombrecargo", nombrecargo);
            sqls[36] = new SqlParameter("prefijo1", prefijo1);
            sqls[37] = new SqlParameter("prefijo2", prefijo2);
            sqls[38] = new SqlParameter("prefijo3", prefijo3);
            sqls[39] = new SqlParameter("noNotificar", no_notificar);
            sqls[40] = new SqlParameter("linkedIn", linkedin);


            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))            
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarCONTACTOnew", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    // Esta es la clave, hemos de añadir un parámetro que recogerá el valor de retorno
                    SqlParameter retValue = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
                    retValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(retValue);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        Id = Convert.ToInt32(retValue.Value);
                    }
                }
            }

            return Id;
        }

        //LLAMAMOS EL PROCEDIMIENTO DE INGRESO DE CONTACTO CLIENTE LITE
        public int guardarDatosContactoClienteLite(int clienteId, int obraId, int tipoCargoId, string nombre1, string nombre2, string apellido1, string apellido2,
                                               string telefono1, string telefono2, string email1, string email2, string movil, bool contCliente, bool contObra,
                                               bool contTecnico, string hobby, string fechacumple, int feriaId, string usucrea, string fechacrea,
                                               bool chtelefono, bool chemail, bool chreferencia, bool chferia, bool chtrabcampo, bool chvisita, bool chmedicomunic,
                                               bool chcharlas, bool chconferencia, bool chweb, bool chseminarios, bool chpublicidad, bool chpersonal,
                                               string comentarios, int profesion, string nombrecargo, string prefijo1, string prefijo2, string prefijo3,
                                                int fila, string archivo, int idContacto, string idCodCliSim, int pais, int ciudad, string linkedin)
        {
            int contCli = 0, contObr = 0, contTec = 0;
            if (contCliente == true)
                contCli = 1;
            if (contObra == true)
                contObr = 1;
            if (contTecnico == true)
                contTec = 1;

            int telefono = 0, email = 0, referencia = 0, feria = 0, trabcampo = 0, visita = 0, medicomunic = 0, charlas = 0
                , conferencia = 0, web = 0, seminarios = 0, publicidad = 0, personal = 0;

            if (chtelefono == true)
                telefono = 1;
            if (chemail == true)
                email = 1;
            if (chreferencia == true)
                referencia = 1;
            if (chferia == true)
                feria = 1;
            if (chtrabcampo == true)
                trabcampo = 1;
            if (chvisita == true)
                visita = 1;
            if (chmedicomunic == true)
                medicomunic = 1;
            if (chcharlas == true)
                charlas = 1;
            if (chconferencia == true)
                conferencia = 1;
            if (chweb == true)
                web = 1;
            if (chseminarios == true)
                seminarios = 1;
            if (chpublicidad == true)
                publicidad = 1;
            if (chpersonal == true)
                personal = 1;

            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[46];
            sqls[0] = new SqlParameter("clienteId", clienteId);
            sqls[1] = new SqlParameter("obraId", obraId);
            sqls[2] = new SqlParameter("tipoCargoId", tipoCargoId);
            sqls[3] = new SqlParameter("nombre1", nombre1);
            sqls[4] = new SqlParameter("nombre2", nombre2);
            sqls[5] = new SqlParameter("apellido1", apellido1);
            sqls[6] = new SqlParameter("apellido2", apellido2);
            sqls[7] = new SqlParameter("telefono1", telefono1);
            sqls[8] = new SqlParameter("telefono2", telefono2);
            sqls[9] = new SqlParameter("email1", email1);
            sqls[10] = new SqlParameter("email2", email2);
            sqls[11] = new SqlParameter("movil", movil);
            sqls[12] = new SqlParameter("contCli ", contCli);
            sqls[13] = new SqlParameter("contObr ", contObr);
            sqls[14] = new SqlParameter("contTec", contTec);
            sqls[15] = new SqlParameter("hobby", hobby);
            sqls[16] = new SqlParameter("fechacumple", fechacumple);
            sqls[17] = new SqlParameter("feriaId", feriaId);
            sqls[18] = new SqlParameter("usucrea", usucrea);
            sqls[19] = new SqlParameter("fechacrea", fechacrea);

            sqls[20] = new SqlParameter("telefono", telefono);
            sqls[21] = new SqlParameter("email", email);
            sqls[22] = new SqlParameter("referencia", referencia);
            sqls[23] = new SqlParameter("feria", feria);
            sqls[24] = new SqlParameter("trabcampo", trabcampo);
            sqls[25] = new SqlParameter("visita", visita);
            sqls[26] = new SqlParameter("medicomunic", medicomunic);
            sqls[27] = new SqlParameter("charlas ", charlas);
            sqls[28] = new SqlParameter("conferencia ", conferencia);
            sqls[29] = new SqlParameter("web", web);
            sqls[30] = new SqlParameter("seminarios", seminarios);
            sqls[31] = new SqlParameter("publicidad", publicidad);
            sqls[32] = new SqlParameter("personal", personal);
            sqls[33] = new SqlParameter("comentarios", comentarios);
            sqls[34] = new SqlParameter("profesion", profesion);
            sqls[35] = new SqlParameter("nombrecargo", nombrecargo);
            sqls[36] = new SqlParameter("prefijo1", prefijo1);
            sqls[37] = new SqlParameter("prefijo2", prefijo2);
            sqls[38] = new SqlParameter("prefijo3", prefijo3);
            sqls[39] = new SqlParameter("fila", fila);
            sqls[40] = new SqlParameter("archivo", archivo);
            sqls[41] = new SqlParameter("id", idContacto);
            sqls[42] = new SqlParameter("IdCliSIM", idCodCliSim);
            sqls[43] = new SqlParameter("idPais", pais);
            sqls[44] = new SqlParameter("idCiudad", ciudad);
            sqls[45] = new SqlParameter("linkedIn", linkedin);
            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarContactoLite", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    // Esta es la clave, hemos de añadir un parámetro que recogerá el valor de retorno
                    SqlParameter retValue = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
                    retValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(retValue);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        Id = Convert.ToInt32(retValue.Value);
                    }
                }
            }

            return Id;
        }

        public SqlDataReader consultarCliente(int cliente)
        {
            string sql;

            sql = "SELECT cli_nombre, cli_pai_id, pai_nombre, cli_ciu_id, ciu_nombre, cli_id " +
                  "FROM cliente INNER JOIN pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN ciudad ON " +
                  "cliente.cli_ciu_id = ciudad.ciu_id INNER JOIN tipo_contribuyente ON " +
                  "cliente.cli_tco_id = tipo_contribuyente.tco_id " +
                  "WHERE (cli_id = " + cliente + ")";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader consultarEmpresa(int cliente)
        {
            string sql;

            sql = "SELECT     cli_apoyo FROM         cliente "+
                  "WHERE (cli_id = " + cliente + ")";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader consultarContactoCliente(int idContCliente)
        {
            string sql;

            sql = " SELECT        contacto_cliente.ccl_cli_id, contacto_cliente.ccl_obr_id, contacto_cliente.ccl_tipo_cargo, tipo_cargo.tipo_cargo_descripcion, contacto_cliente.ccl_nombre, contacto_cliente.ccl_nombre2, " +
             "            contacto_cliente.ccl_apellido, contacto_cliente.ccl_apellido2, contacto_cliente.ccl_telefono, contacto_cliente.ccl_telefono2, contacto_cliente.ccl_email, contacto_cliente.ccl_email2, contacto_cliente.ccl_cel,  " +
             "           contacto_cliente.ccl_cliente, contacto_cliente.ccl_obra, contacto_cliente.ccl_tecnico, contacto_cliente.ccl_hobby, contacto_cliente.ccl_fecha_cumpleaños, ISNULL(contacto_cliente.ccl_feria, 0) AS Expr9,  " +
             "          ISNULL(lite_tipo_fuente.tifuente_descripcion, 'N/A') AS Expr10, obra.usu_crea, obra.fecha_crea, obra.usu_actualiza, obra.fecha_actualiza, contacto_cliente.cc_telefono, contacto_cliente.cc_mail,   " +
             "            contacto_cliente.cc_referencia, contacto_cliente.cc_feria, contacto_cliente.cc_trabajo_campo, contacto_cliente.cc_visita, contacto_cliente.cc_comunicacion, contacto_cliente.cc_charlas,   " +
             "            contacto_cliente.cc_conferencias, contacto_cliente.cc_web, contacto_cliente.cc_seminarios, contacto_cliente.cc_publicidad, contacto_cliente.cc_personal, contacto_cliente.cc_comentarios, pais.pai_id,   " +
             "            pais.pai_nombre, ciudad.ciu_id, ciudad.ciu_nombre, cliente.cli_id, cliente.cli_nombre, ISNULL(obra.obr_id, 0) AS Expr1, ISNULL(obra.obr_nombre, '') AS Expr2, contacto_cliente.ccl_tipo_contacto,   " +
             "            ISNULL(tipo_profesion.tipo_prof_id,0 ) AS tipo_prof_id , isnull(tipo_profesion.tipo_prof_nombre,'') as tipo_prof_nombre , ISNULL(contacto_cliente.usu_actualiza, '') AS Expr3, ISNULL(contacto_cliente.fecha_actualiza, '') AS Expr4,   " +
             "            ISNULL(contacto_cliente.ccl_cargo_nombre, '') AS Expr5, ISNULL(contacto_cliente.ccl_prefijo, '0') AS Expr6, ISNULL(contacto_cliente.ccl_prefijo2, '0') AS Expr7, ISNULL(contacto_cliente.ccl_prefijo_fax, '0')  AS Expr8 , isnull(no_notificar,0),isnull(contacto_cliente.ccl_linkedIn,'') " +
             " FROM            pais INNER JOIN  " +
             "             cliente ON cliente.cli_pai_id = pais.pai_id INNER JOIN " +
             "             ciudad ON ciudad.ciu_id = cliente.cli_ciu_id INNER JOIN " +
             "             contacto_cliente ON cliente.cli_id = contacto_cliente.ccl_cli_id INNER JOIN " +
             "             tipo_cargo ON contacto_cliente.ccl_tipo_cargo = tipo_cargo.tipo_cargo_id LEFT OUTER JOIN " +
             "             tipo_profesion ON contacto_cliente.ccl_prof_id = tipo_profesion.tipo_prof_id LEFT OUTER JOIN " +
             "             lite_tipo_fuente ON contacto_cliente.ccl_feria = lite_tipo_fuente.tifuente_id LEFT OUTER JOIN " +
             "             obra ON contacto_cliente.ccl_obr_id = obra.obr_id FULL OUTER JOIN " +
             "             tipo_contacto ON contacto_cliente.ccl_tipo_contacto = tipo_contacto.tcc_id " +
             " WHERE (contacto_cliente.ccl_id =" + idContCliente + ")";

            //sql = "SELECT contacto_cliente.ccl_cli_id, contacto_cliente.ccl_obr_id, contacto_cliente.ccl_tipo_cargo, tipo_cargo.tipo_cargo_descripcion, " +
            //          "contacto_cliente.ccl_nombre, contacto_cliente.ccl_nombre2, contacto_cliente.ccl_apellido, contacto_cliente.ccl_apellido2, contacto_cliente.ccl_telefono," +
            //          "contacto_cliente.ccl_telefono2, contacto_cliente.ccl_email, contacto_cliente.ccl_email2, contacto_cliente.ccl_cel, contacto_cliente.ccl_cliente, " +
            //          "contacto_cliente.ccl_obra, contacto_cliente.ccl_tecnico, contacto_cliente.ccl_hobby, contacto_cliente.ccl_fecha_cumpleaños, contacto_cliente.ccl_feria, " +
            //          "ferias.fer_nombre, obra.usu_crea, obra.fecha_crea, obra.usu_actualiza, obra.fecha_actualiza, contacto_cliente.cc_telefono, contacto_cliente.cc_mail, " +
            //          "contacto_cliente.cc_referencia, contacto_cliente.cc_feria, contacto_cliente.cc_trabajo_campo, contacto_cliente.cc_visita, " +
            //          "contacto_cliente.cc_comunicacion, contacto_cliente.cc_charlas, contacto_cliente.cc_conferencias, contacto_cliente.cc_web, " +
            //          "contacto_cliente.cc_seminarios, contacto_cliente.cc_publicidad, contacto_cliente.cc_personal, contacto_cliente.cc_comentarios, pais.pai_id, " +
            //          "pais.pai_nombre, ciudad.ciu_id, ciudad.ciu_nombre, cliente.cli_id, cliente.cli_nombre, ISNULL(obra.obr_id,0),ISNULL(obra.obr_nombre,''), " +
            //          "contacto_cliente.ccl_tipo_contacto, tipo_profesion.tipo_prof_id, tipo_profesion.tipo_prof_nombre, ISNULL(contacto_cliente.usu_actualiza,''), ISNULL(contacto_cliente.fecha_actualiza,''),  " +
            //          "ISNULL(ccl_cargo_nombre,''),ISNULL(ccl_prefijo,'0'),ISNULL(ccl_prefijo2,'0'),ISNULL(ccl_prefijo_fax,'0') " +
            //          "FROM         pais INNER JOIN " +
            //          "cliente ON cliente.cli_pai_id = pais.pai_id INNER JOIN " +
            //          "ciudad ON ciudad.ciu_id = cliente.cli_ciu_id INNER JOIN " +
            //          "contacto_cliente INNER JOIN " +
            //          "ferias ON contacto_cliente.ccl_feria = ferias.fer_id ON cliente.cli_id = contacto_cliente.ccl_cli_id INNER JOIN " +
            //          "tipo_cargo ON contacto_cliente.ccl_tipo_cargo = tipo_cargo.tipo_cargo_id INNER JOIN " +
            //          "tipo_profesion ON contacto_cliente.ccl_prof_id = tipo_profesion.tipo_prof_id LEFT OUTER JOIN " +
            //          "obra ON contacto_cliente.ccl_obr_id = obra.obr_id FULL OUTER JOIN " +
            //          "tipo_contacto ON contacto_cliente.ccl_tipo_contacto = tipo_contacto.tcc_id " +
            //          " WHERE (contacto_cliente.ccl_id =" + idContCliente + ")";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //LLAMAMOS PARA ACTUALIZAR EL CONTACTO CLIENTE
        public int actualizarDatosContactoCliente(int clienteId, int obraId, int tipoCargoId, string nombre1, string nombre2, string apellido1, string apellido2,
                                               string telefono1, string telefono2, string email1, string email2, string movil, bool contCliente, bool contObra,
                                               bool contTecnico, string hobby, string fechacumple, int feriaId, string usucrea, string fechacrea,
                                               bool chtelefono, bool chemail, bool chreferencia, bool chferia, bool chtrabcampo, bool chvisita, bool chmedicomunic,
                                               bool chcharlas, bool chconferencia, bool chweb, bool chseminarios, bool chpublicidad, bool chpersonal, string comentarios,
                                               int idContCliente, int profesion, string nombrecargo, string prefijo1, string prefijo2, string prefijo3,bool noNotificar, string linkedin)
        {
            int contCli = 0, contObr = 0, contTec = 0, no_notificar = 0;
            if (contCliente == true)
                contCli = 1;
            if (contObra == true)
                contObr = 1;
            if (contTecnico == true)
                contTec = 1;

            if (noNotificar == true) no_notificar = 1;

            int telefono = 0, email = 0, referencia = 0, feria = 0, trabcampo = 0, visita = 0, medicomunic = 0, charlas = 0
                , conferencia = 0, web = 0, seminarios = 0, publicidad = 0, personal = 0;

            if (chtelefono == true)
                telefono = 1;
            if (chemail == true)
                email = 1;
            if (chreferencia == true)
                referencia = 1;
            if (chferia == true)
                feria = 1;
            if (chtrabcampo == true)
                trabcampo = 1;
            if (chvisita == true)
                visita = 1;
            if (chmedicomunic == true)
                medicomunic = 1;
            if (chcharlas == true)
                charlas = 1;
            if (chconferencia == true)
                conferencia = 1;
            if (chweb == true)
                web = 1;
            if (chseminarios == true)
                seminarios = 1;
            if (chpublicidad == true)
                publicidad = 1;
            if (chpersonal == true)
                personal = 1;

            string sql = "UPDATE contacto_cliente SET  ccl_cli_id = " + clienteId + ",ccl_obr_id= " + obraId + ", ccl_tipo_cargo= " + tipoCargoId + ", ccl_nombre= '" + nombre1 + "', ccl_nombre2= '" + nombre2 + "'"+
                          " , ccl_apellido= '" + apellido1 + "',ccl_apellido2= '" + apellido2 + "', ccl_telefono='" + telefono1 + "', ccl_telefono2='" + telefono2 + "', ccl_email='" + email1 + "',ccl_email2='" + email2 +"'"+
                          " ,ccl_cel='" + movil + "', ccl_cliente=" + contCli + ",ccl_obra =" + contObr + ",ccl_tecnico=" + contTec + ", ccl_hobby= '" + hobby + "',ccl_fecha_cumpleaños='" + fechacumple +"'"+
                          " ,ccl_feria=" + feriaId + ",usu_crea='" + usucrea + "', fecha_crea='" + fechacrea + "',usu_actualiza='" + usucrea + "', fecha_actualiza='" + fechacrea +"'"+
                          " ,cc_telefono = " + telefono + ",cc_mail = " + email + ",cc_referencia=" + referencia + ",cc_feria = " + feria + ",cc_trabajo_campo=" + trabcampo + ",cc_visita = " + visita + ",cc_comunicacion = " + medicomunic +
                          " ,cc_charlas = " + charlas + ", cc_conferencias = " + conferencia + ",cc_web = " + web + ",cc_seminarios =" + seminarios + ",cc_publicidad =" + publicidad + ",cc_personal=" + personal +
                          " ,cc_comentarios = '" + comentarios + "', ccl_prof_id= " + profesion + " , ccl_cargo_nombre = '" + nombrecargo + "',ccl_prefijo = '" + prefijo1 + "',ccl_prefijo2 ='" + prefijo2 + "',ccl_prefijo_fax = '" + prefijo3 + "', no_notificar = " + no_notificar +" , ccl_linkedIn='"+ linkedin +
                          "' WHERE ccl_id= " + idContCliente ;
           
            return BdDatos.Actualizar(sql);
        }

        public int eliminarContactoCliente(int idContCliente)
        {
            string sql = "UPDATE contacto_cliente SET ccl_activo= 0 WHERE ccl_id= " + idContCliente;

            return BdDatos.Actualizar(sql);
        }

        public void insertLogContactoLite(int id, int fila, string archivo, string usuario, string observacion, string nombre, int pais, int ciudad)
        {
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
            string sql = "INSERT INTO LogContactoLite(contacto_id, fila_excel, observacion, nombre_archivo, fecha, usuario, estado, nombre_contacto, pais, ciudad, ultimo_registro) VALUES(" + id + "," + fila + ", '" + observacion + "' , '" + archivo + "' , '" + fecha + "' , '" + usuario + "', 0, '" + nombre + "', " + pais + ", " + ciudad + ", 1)";
            BdDatos.Actualizar(sql);
        }

        /// <summary>
        /// Metodo encargado de consultar las obras del cliente seleccionado
        /// Autor: Global BI
        /// Fecha: 09-09-2018
        /// </summary>
        /// <returns>Lista de obras del cliente</returns>
        public List<ObraCliente> obtenerObrasDistPV(int cliente)
        {
            string sql;
            sql = " SELECT DISTINCT obra.obr_id, obra.obr_nombre, cliente.cli_id, obra.obr_seguimiento " +
                  "FROM obra INNER JOIN " +
                  "cliente ON obra.obr_cli_id = cliente.cli_id " +
                  "WHERE (cliente.cli_id = " + cliente + ") AND (obra.obr_perdido = 0) OR " +
                  "(cliente.cli_id = " + cliente + ") AND (obra.obr_seguimiento = 0) " +
                  "ORDER BY obr_nombre ASC ";

            DataTable dt = BdDatos.CargarTabla(sql);
            List<ObraCliente> lstObject = dt.AsEnumerable()
                .Select(row => new ObraCliente
                {
                    Id = (int)row["obr_id"],
                    Nombre = (string)row["obr_nombre"],
                    IdCliente = (int)row["cli_id"]
                }).ToList();
            dt.Clear();
            dt.Dispose();
            return lstObject;
        }
    }

    
}
