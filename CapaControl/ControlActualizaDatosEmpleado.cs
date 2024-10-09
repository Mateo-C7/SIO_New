using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls;

namespace CapaControl
{
    public class ControlActualizaDatosEmpleado
    {
        //Consulta los datos generales del empleado
        public DataTable Consultar_DatosGeneralEmpleado(int identEmpl, string tipoIdent)
        {
            string sql = "SELECT 'file://///172.21.0.5/c$/B/xampp/htdocs/FORSA/Empleado/Imagenes/Fotos/' + empleado.emp_nombre_archivo_foto AS nombfoto, " +
                         "emp_nombre,emp_apellidos,emp_direccion_residencia," +                           
                         "  emp_barrio_residencia,emp_ciu_id_residencia,emp_telefono_residencia," +
                         "  emp_telefono_movil,CASE WHEN emp_grupo_sanguineo='X' THEN 'A+' ELSE emp_grupo_sanguineo END emp_grupo_sanguineo, " +
                         "  emp_fecha_nacimiento,CASE WHEN emp_estado_civil='SIN' THEN 'UNIÓN LIBRE' ELSE emp_estado_civil END emp_estado_civil," +
                         "  emp_correo_electronico,emp_tiene_Carnet,emp_vive_En_Casa,emp_tiene_Moto,emp_tiene_Carro,emp_estracto,emp_licencia_veh " 
                       + " ,emp_redes_sociales,emp_publica_fotos,emp_recibe_dotacion,emp_con_regls_dota"
                       + " FROM empleado INNER JOIN contrato ON empleado.emp_usu_num_id = contrato.con_emp_usu_num_id" +
                       " WHERE emp_usu_num_id=" + identEmpl.ToString() ;
                       //" WHERE emp_usu_num_id=" + identEmpl.ToString() + " AND emp_tipo_id='" + tipoIdent + "'";
                //"SELECT emp_nombre,emp_apellidos,emp_direccion_residencia,emp_barrio_residencia,emp_ciu_id_residencia,emp_telefono_residencia,emp_telefono_movil,emp_grupo_sanguineo,emp_fecha_nacimiento,emp_estado_civil,emp_correo_electronico FROM empleado   WHERE emp_usu_num_id=" + identEmpl.ToString() + " AND emp_tipo_id='" + tipoIdent + "'";
            return BdDatos.CargarTabla(sql);
        }

        public DataTable Consultar_Placas(int idemple)
        {
            string sql = "select veh_placa_carro1,veh_placa_moto1,veh_placa_carro2,veh_placa_moto2,"
               + " veh_modelo_carro1,veh_modelo_carro2,veh_modelo_moto1,veh_modelo_moto2,"
		       + " veh_marca_carro1,veh_marca_carro2,veh_marca_moto1,veh_marca_moto2"
               + " from Vehiculo where veh_usu_num_id="+idemple.ToString()+"";
            return BdDatos.CargarTabla(sql);
        }


        //Bueno
        public DataTable Consultar_Cargo_Actual(int idempl)
        {
            string sql = " SELECT car_nombre,car_id,emc_id,emc_ubf_id,emc_emp_usu_num_id_jefe,emc_fecha_inicio  " +
                         "  FROM cargo INNER JOIN empleado_cargo ON empleado_cargo.emc_car_id = cargo.car_id " +
                         "  WHERE emc_emp_usu_num_id =" + idempl.ToString() + " order by emc_id desc";
            return BdDatos.CargarTabla(sql);
        }
        //==========================METODOS EMPRESA CONTRATANTE==============================================================
        public DataTable Obtener_EmpresaContratante_Empleado(int idempl)
        {
            string sql = "SELECT contrato.con_epr_id, contrato.con_num " +
                              " FROM empleado INNER JOIN contrato ON empleado.emp_usu_num_id = contrato.con_emp_usu_num_id " +
                              " INNER JOIN empleador ON contrato.con_epr_id = empleador.epr_id " +
                              " WHERE empleado.emp_usu_num_id = " + idempl.ToString() + "  " +
                              " GROUP BY contrato.con_epr_id, contrato.con_num " +
                              " ORDER BY contrato.con_num DESC";
            return BdDatos.CargarTabla(sql);
        }


        //Llena el combo empresa_contratante
        public void ListarEmpresaContra(DropDownList myListaEmpleador)
        {   //Recuperar lista de empleador
            DataTable dTable = new DataTable();
            string sql2 = "SELECT epr_id,epr_nombre FROM empleador WHERE (epr_activo = 1) order by epr_nombre Asc";
            dTable = BdDatos.CargarTabla(sql2);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["epr_nombre"].ToString(),
                                            row["epr_id"].ToString());
                myListaEmpleador.Items.Insert(myListaEmpleador.Items.Count, lst);
            }
        }

        //Llena el combo cargo
        public void ListarCargoActual(DropDownList myListcargo)
        {   //Recuperar lista de empleador
            DataTable dTable = new DataTable();
            string sql2 = "SELECT car_id,car_nombre FROM cargo WHERE  (car_activo = 1) order by car_nombre Asc";
            dTable = BdDatos.CargarTabla(sql2);
            ListItem lst = new ListItem("Seleccione",
                                           "0");
            foreach (DataRow row in dTable.Rows)
            {
                lst = new ListItem(row["car_nombre"].ToString(),
                                            row["car_id"].ToString());
                myListcargo.Items.Insert(myListcargo.Items.Count, lst);
            }
        }


        //Recupera la informacion de la empresa anterior
        public DataTable Recuperar_EmpresaAnterior(int idemple)
        {
            string sql = "SELECT con_num,con_emp_usu_num_id,con_epr_id,con_tipo,con_fecha_inicio,con_fecha_fin,con_clausulas, " +
                               " con_sal_bas,con_sal_int,con_sal_int_mixto,con_porc_comisiones,con_sal_bas_mas_comisiones, " +
                               " con_sal_bas_mas_comp,con_sal_int_mas_comp,con_promedio_sal,con_fecha_venc_pas_judicial, " +
                               " con_fecha_venc_pasaporte,con_dias_vacaciones,con_es_de_planta,con_observaciones, " +
                               " con_fecha_modificacion,con_usuario_modificador,con_activo " +
                         " FROM contrato " +
                         " WHERE con_emp_usu_num_id=" + idemple.ToString() + " AND con_num=(SELECT MAX(con_num) " +
                         " FROM contrato where con_emp_usu_num_id=" + idemple.ToString() + ")";
            return BdDatos.CargarTabla(sql);
        }

        public int Asignar_NuevaEmpresaContratante(int con_emp_usu_num_id,int con_epr_id, string con_tipo, string con_fecha_inicio,
            string con_fecha_fin,string con_clausula,decimal con_sal_bas, decimal con_sal_int, decimal con_sal_int_mixto, 
            decimal con_porc_comisiones, decimal con_sal_bas_mas_comisiones,decimal con_sal_bas_mas_comp, decimal con_sal_int_mas_comp,
            decimal con_promedio_sal, string con_fecha_venc_pas_judicial,string con_fecha_venc_pasaporte,int con_dias_vacaciones,
            int con_es_de_planta,string con_observacion ,string con_fecha_modificacion,int con_usuario_modificador,int con_activo)
        {
            string sql = "INSERT INTO contrato VALUES("+ con_emp_usu_num_id.ToString() + "," + con_epr_id.ToString() + ",'" + con_tipo + "', " + 
                                                  " '" + con_fecha_inicio + "','"+ con_fecha_fin +"','" + con_clausula +"',"+ con_sal_bas.ToString() + ", " + 
                                                   " " + con_sal_int.ToString() + "," + con_sal_int_mixto.ToString() + "," + con_porc_comisiones.ToString() + ", " + 
                                                   " " + con_sal_bas_mas_comisiones.ToString() + ", " + con_sal_bas_mas_comp.ToString() + ", " +
                                                   " " + con_sal_int_mas_comp.ToString() + "," + con_promedio_sal.ToString() + ",'" + con_fecha_venc_pas_judicial + "', " + 
                                                   " '" + con_fecha_venc_pasaporte + "'," + con_dias_vacaciones.ToString() + "," + con_es_de_planta.ToString() + ", " +
                                                   " '"+ con_observacion +"','" + con_fecha_modificacion + "'," + con_usuario_modificador.ToString() + "," + con_activo.ToString() +")";
            return BdDatos.ejecutarSql(sql);
        }

        public int Asignar_NuevoCargo(int ubifis, int idemple,int carid, int jefe, string fechini, string fechmodif,int usumodi,int activo ) 
        {
            string sql = "INSERT INTO empleado_cargo ( emc_ubf_id,emc_emp_usu_num_id,emc_car_id,emc_emp_usu_num_id_jefe, " + 
                                                     " emc_fecha_inicio,emc_fecha_modificacion,emc_usuario_modificador,emc_activo) " +
                                " VALUES(" + ubifis.ToString() + "," + idemple.ToString() + "," + 
                                         " " + carid.ToString() + "," + jefe.ToString() + ", '" + fechini + "','" + fechmodif + "'," + usumodi+","+activo+")";
            return BdDatos.ejecutarSql(sql);
        }

        public DataTable Consultar_CargoAnterior(int idemple)
        {
            //string sql = " SELECT emc_id,car_id,emc_ubf_id,emc_emp_usu_num_id ,emc_emp_usu_num_id_jefe,emc_fecha_inicio, " +
            //                    " emc_fecha_modificacion,emc_usuario_modificador,emc_activo" +
            //                   " FROM cargo INNER JOIN empleado_cargo ON empleado_cargo.emc_car_id = cargo.car_id " +
            //                   " WHERE emc_emp_usu_num_id =" + idemple.ToString() + " ORDER BY emc_id desc";

            string sql = "SELECT emc_id, car_id, emc_ubf_id, emc_emp_usu_num_id, emc_emp_usu_num_id_jefe, emc_fecha_inicio, " +
                                " emc_fecha_modificacion, emc_usuario_modificador, emc_activo " +
                                " FROM cargo INNER JOIN empleado_cargo ON empleado_cargo.emc_car_id = cargo.car_id " +
                               "  WHERE emc_emp_usu_num_id = " + idemple.ToString() + " AND emc_id = (SELECT MAX(emc_id) " +
                               " FROM empleado_cargo WHERE emc_emp_usu_num_id = " + idemple.ToString() + ")";
                              
            return BdDatos.CargarTabla(sql);
        }

        //Recupera la informacion del cargo anterior
        public DataTable Recuperar_CargoAnterior(int idemple)
        {
            string sql = "SELECT  TOP(1) emc_id,emc_ubf_id,emc_emp_usu_num_id,emc_car_id,emc_emp_usu_num_id_jefe,emc_fecha_inicio,emc_fecha_fin,emc_fecha_modificacion,emc_usuario_modificador,emc_activo " +
                                  " FROM empleado_cargo " +
                                  " WHERE emc_emp_usu_num_id=10498321 AND emc_id=(SELECT MAX(emc_id) FROM empleado_cargo)";
            return BdDatos.CargarTabla(sql);
        }

        public int Deshabilitar_CargoAnterior(int idemple, int emc_id, string fechini)
        {
            string sql = "UPDATE empleado_cargo  SET emc_fecha_fin='" + fechini + "',emc_activo=0  " +
                         " WHERE emc_id=" + emc_id.ToString() + " AND emc_emp_usu_num_id=" + idemple.ToString() + "";
            return BdDatos.ejecutarSql(sql);
        }
        //=====================================================================================================================

        public int Deshabilitar_EmpresaAnterior(string fechaFin, int idemple, int con_num)
        {
            string sql = " UPDATE contrato SET con_fecha_fin='" + fechaFin + "',con_activo='0' " +
                                      " WHERE con_emp_usu_num_id=" + idemple.ToString() + " AND con_num=" + con_num.ToString() + "";
            return BdDatos.ejecutarSql(sql);
        }

        ////Actualizar el empresa contratante del empleado
        //public int Actualizar_EmpresaContratante_Empleado(int empresa, int idEmple)
        //{
        //    string sql = "UPDATE contrato SET con_epr_id=" + empresa.ToString() + " WHERE con_emp_usu_num_id=" + idEmple.ToString() + "";

        //    return BdDatos.ejecutarSql(sql);
        //}

        public DataTable consulta_carnet(int idemple)
        {
            string sql = "select emp_tiene_Carnet from empleado where emp_usu_num_id =" + idemple.ToString() + "";
            return BdDatos.CargarTabla(sql);
        }

        //Llena el combo ciudad/residencia
        public void ListarCiudadResideEmple(DropDownList myListaCiudad)
        {   //Recuperar lista de Ciudades
            DataTable dTable = new DataTable();
            string sql2 = "SELECT ciu_id ,ciu_nombre FROM ciudad  order by ciu_nombre Asc";
            dTable = BdDatos.CargarTabla(sql2);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["ciu_nombre"].ToString(),
                                            row["ciu_id"].ToString());
                myListaCiudad.Items.Insert(myListaCiudad.Items.Count, lst);
            }
        }
        public void ListarUbicacionFisicaEmpleado(DropDownList myListaUbicaFis)
        {   //Recuperar lista de Ciudades
            DataTable dTable = new DataTable();
            string sql2 = "SELECT ubf_id,ubf_lugar FROM ubicacion_fisica ORDER BY ubf_lugar asc";
            dTable = BdDatos.CargarTabla(sql2);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["ubf_lugar"].ToString(),
                                            row["ubf_id"].ToString());
                myListaUbicaFis.Items.Insert(myListaUbicaFis.Items.Count, lst);
            }
        }
       
  
        //Actualizar datos empleado menos el cargo y la empresa 
        public int Actualizar_Empleado(string nombre, string apellido, string direccion,
                                        string barrio, int ciudad, string teleHogar, 
                                        string teleMovil, string tipoSangre, string fechNaci, 
                                        string estadoCivil, string correo, string carnet, 
                                        string TipoVivienda,string tieneMoto, int estracto, int licencia,
                                        string tieneCarro, string fechaModifi, int sio_actualiza,
                                        int redSociales, int pubFotos, int recDota, int conRegDotacion,
                                        int ideEmpleado, string tipoDocumento
                                        )
        {
            string sql = "UPDATE empleado SET emp_nombre='" + nombre + "',emp_apellidos='" + apellido + "', " +
                " emp_direccion_residencia='" + direccion + "',emp_barrio_residencia='" + barrio + "', " +
                " emp_ciu_id_residencia=" + ciudad.ToString() + ",emp_telefono_residencia='" + teleHogar + "', " +
                " emp_telefono_movil='" + teleMovil + "',emp_grupo_sanguineo='" + tipoSangre + "', " +
                " emp_fecha_nacimiento= CONVERT(DATE,'" + fechNaci + "',103),emp_estado_civil='" + estadoCivil + "', " +
                " emp_correo_electronico='" + correo + "',emp_tiene_carnet='" + carnet + "', " +
                " emp_vive_En_Casa='" + TipoVivienda + "',emp_tiene_Moto='" + tieneMoto + "',emp_estracto=" + estracto + ", emp_licencia_veh= " + licencia + ", " +
                " emp_tiene_Carro='" + tieneCarro + "',emp_fecha_modificacion='" + fechaModifi +
                "',emp_sio_actualiza=" + sio_actualiza.ToString() + " "
               + ",emp_redes_sociales= " + redSociales + ",emp_publica_fotos = " + pubFotos + ",emp_recibe_dotacion= " + recDota + "";

            if (conRegDotacion == -1)
            {
                sql += ",emp_con_regls_dota= NULL";
            }
            else {
                sql += ",emp_con_regls_dota= " + conRegDotacion.ToString();
            }
            sql +=" WHERE emp_usu_num_id=" + ideEmpleado.ToString();
            //sql += " WHERE emp_usu_num_id=" + ideEmpleado.ToString() + " AND emp_tipo_id='" + tipoDocumento + "'";

            return BdDatos.ejecutarSql(sql);
        }
  
        public DataTable Recuperar_ID_Usuario(string nomLogin)
        {
            string sql = "select usu_emp_usu_num_id from usuario where usu_login='" + nomLogin + "'";
            return BdDatos.CargarTabla(sql);
        }

        //====================ESTUDIOS EMPLEADO=============================================================================================

        //Consultar estudios del empleado
        public DataSet Consultar_Estudios_Empleado(int idEmple)
        {
            string sql = "SELECT est_id, ISNULL(Tipo_Estudio.tipest_Nombre,'NO APLICA') AS est_tipo,est_titulo,est_entidad_educativa, " +
                         " est_fecha_fin,case when est_tiene_certificado = 1 then 'SI' else 'NO' end as est_tiene_certificado, " +
                         " est_semetre_Actual,case when est_cursando  = 1 then 'SI' else 'NO' end as est_cursando " +
                         "FROM estudio LEFT OUTER JOIN Tipo_Estudio ON estudio.est_tipo = Tipo_Estudio.tipest_Nombre " +
                         " WHERE est_emp_usu_num_id=" + idEmple.ToString() + " order by est_id Desc" ;

            return BdDatos.consultarConDataset(sql);
        }


        public DataTable Consultar_Estudios_Empleado2(int idEmple)
        {
            string sql = "SELECT est_id,ISNULL(Tipo_Estudio.tipest_Nombre,'NO APLICA') AS est_tipo,est_titulo,est_entidad_educativa, " +
                         " est_fecha_fin,case when est_tiene_certificado = 1 then 'SI' else 'NO' end as est_tiene_certificado, " +
                         " est_semetre_Actual,case when est_cursando  = 1 then 'SI' else 'NO' end as est_cursando " +
                         "FROM estudio LEFT OUTER JOIN Tipo_Estudio ON estudio.est_tipo = Tipo_Estudio.tipest_Nombre " +
                         " WHERE est_emp_usu_num_id=" + idEmple.ToString() + " order by est_id Desc";

            return BdDatos.CargarTabla(sql);
        }

        // Agregar un nuevo estudio cursado o cursando por el empleado con fecha de graduacion
        public int Agregar_Estudio(string titulo, string programa, string entidad, string fecha, int completo, int semestre, int cursando, int idEmp, string lugar, string fechaIni, int externo, int costeado, string fechaModi, int usuModi, int Estaacti)
        {
            string sql = "INSERT INTO estudio (est_tipo,est_titulo,est_entidad_educativa,est_fecha_fin,est_tiene_certificado,est_semetre_Actual,est_cursando,est_emp_usu_num_id,est_lugar,est_fecha_inicio,est_es_externo,est_estudio_costeado,est_fecha_modificacion,est_usuario_modificador,est_activo) VALUES('" + titulo + "','" + programa + "','" + entidad + "','" + fecha + "'," + completo.ToString() + "," + semestre.ToString() + "," + cursando.ToString() + "," + idEmp.ToString() + ",'" + lugar + "','" + fechaIni + "'," + externo.ToString() + "," + costeado.ToString() + ",'" + fechaModi + "'," + usuModi.ToString() + "," + Estaacti.ToString() + ")";

            return BdDatos.ejecutarSql(sql);
        }


        // Agregar un nuevo estudio cursado o cursando por el empleado sin fecha de graduacion
        public int Agregar_EstudioSinFechaFinal(string titulo, string programa, string entidad, int completo, int semestre, int cursando, int idEmp, string lugar, string fechaIni, int externo, int costeado, string fechaModi, int usuModi, int Estaacti)
        {
            string sql = "INSERT INTO estudio (est_tipo,est_titulo,est_entidad_educativa,est_tiene_certificado,est_semetre_Actual,est_cursando,est_emp_usu_num_id,est_lugar,est_fecha_inicio,est_es_externo,est_estudio_costeado,est_fecha_modificacion,est_usuario_modificador,est_activo) VALUES('" + titulo + "','" + programa + "','" + entidad + "'," + completo.ToString() + "," + semestre.ToString() + "," + cursando.ToString() + "," + idEmp.ToString() + ",'" + lugar + "','" + fechaIni + "'," + externo.ToString() + "," + costeado.ToString() + ",'" + fechaModi + "'," + usuModi.ToString() + "," + Estaacti.ToString() + ")";

            return BdDatos.ejecutarSql(sql);
        }

        //Actualizar estudios empleado 
        public int Actualizar_Estudios_Empleado(string tipo,string programa,string entidad, string añogradua,int completo,int semestre,int cursa,int estid)
        {
            string sql = "UPDATE estudio SET est_tipo='" + tipo + "',est_titulo='" + programa + "',est_entidad_educativa='" + entidad + "',est_fecha_fin='" + añogradua + "',est_tiene_certificado=" + completo.ToString() + ",est_semetre_Actual=" + semestre.ToString() + ",est_cursando=" + cursa.ToString() + " where est_id=" + estid.ToString() + "";

            return BdDatos.ejecutarSql(sql);
        }

        //Actualizar estudios empleado  sin fecha grado
        public int Actualizar_Estudios_Empleado_SinFecGradua(string tipo, string programa, string entidad, int completo, int semestre, int cursa, int estid)
        {
            string sql = "UPDATE estudio SET est_tipo='" + tipo + "',est_titulo='" + programa + "',est_entidad_educativa='" + entidad + "',est_tiene_certificado=" + completo.ToString() + ",est_semetre_Actual=" + semestre.ToString() + ",est_cursando=" + cursa.ToString() + " where est_id=" + estid.ToString() + "";

            return BdDatos.ejecutarSql(sql);
        }

        //Elimina estudios del empleado
        public int Eliminar_Estudio_Empleado(int estid)
        {
            string sql = "DELETE FROM estudio  WHERE est_id=" + estid.ToString() + "";

            return BdDatos.ejecutarSql(sql);
        }

        //===========================================================================================================================================

        //====================PERSONAS CONVIVEN EMPLEADO=============================================================================================
        //Actualizar personas conviven con el empleado
        public int Actualizar_PersonasConviven_Empleado(string nombapell,string parent, float edad, string niveleduc, string ocupa,int covempid)
        {
            string sql = "UPDATE Personas_Conviven_Empleado SET covemp_Nomb_Apell='" + nombapell + "',covemp_Parentesco='" + parent + "',covemp_Edad=" + edad.ToString() + ",covemp_Nivel_Educativo='" + niveleduc + "',covemp_Ocupacion='" + ocupa + "'  WHERE covemp_id=" +covempid.ToString() + "";

            return BdDatos.ejecutarSql(sql);
        }
        //Elimina personas conviven con el empleado
        public int Eliminar_PersonasConviven_Empleado(int covempid)
        {
            string sql= "DELETE FROM Personas_Conviven_Empleado WHERE covemp_id=" + covempid.ToString() + "";

            return BdDatos.ejecutarSql(sql);
        }
        //Consultar personas que coviven con un determinado empleado
        public DataSet Consultar_PersConvivenEmpleado(int idEmple)
        {
            string sql = "SELECT covemp_id,covemp_Nomb_Apell ,covemp_Parentesco,covemp_Edad,covemp_Nivel_Educativo,covemp_Ocupacion FROM Personas_Conviven_Empleado WHERE covemp_usu_num_id=" + idEmple.ToString() + "";
            return BdDatos.consultarConDataset(sql);
        }
        //Agregar Personas que conviven con el empleado
        public int Agregar_Pers_ConvivenEmpleado(int idEmple, string nombApell, string parent, float edad, string NivelEduc, string Ocupa)
        {
            string sql = "INSERT INTO Personas_Conviven_Empleado VALUES(" + idEmple.ToString() + ",'" + nombApell + "','" + parent + "'," + edad.ToString() + ",'" + NivelEduc + "','" + Ocupa + "')";

            return BdDatos.ejecutarSql(sql);
        }
        //===========================================================================================================================================

        //====================HIJOS NO CONVIVEN EMPLEADO=============================================================================================
        //Agregar hijos que no conviven con el empleado
        public int Agregar_Hijos_NoConvivenEmpleado(int idEmple, string nombApell, float edad, string NivelEduc)
        {
            string sql = "INSERT INTO Hijos_NoConviven_Empleado  VALUES(" + idEmple.ToString() + ",'" + nombApell + "'," + edad.ToString() + ",'" + NivelEduc + "')";

            return BdDatos.ejecutarSql(sql);
        }
        //Consultar hijos que no conviven con el empleado
        public DataSet Consultar_Hijos_NoConvivenEmpleado(int idEmple)
        {
            string sql = "SELECT  hjnocov_id,hjnocov_Nomb_Apell ,hjnocov_Edad ,hjnocov_Nivel_Educativo FROM Hijos_NoConviven_Empleado WHERE hjnocov_usu_num_id=" + idEmple.ToString() + "";
            return BdDatos.consultarConDataset(sql);
        }
        //Actualizar hijos que no conviven con el empleado
        public int Actualizar_HijosNoConviven_Empleado(string nombapel, float edad,string niveeduca,int id)
        {
            string sql = " UPDATE Hijos_NoConviven_Empleado SET hjnocov_Nomb_Apell='" + nombapel + "',hjnocov_Edad=" + edad.ToString() + ",hjnocov_Nivel_Educativo ='" + niveeduca + "' WHERE hjnocov_id=" + id.ToString() + "";

            return BdDatos.ejecutarSql(sql);
        }

        //Elimina hijos que no conviven con el empleado
        public int Eliminar_HijosNoConviven_Empleado(int id)
        {
            string sql = "DELETE FROM Hijos_NoConviven_Empleado WHERE hjnocov_id=" + id.ToString() + "";

            return BdDatos.ejecutarSql(sql);
        }
        //==========================================================================================================================================

        //====================PERSONA EN CASO DE EMERGENCIA EMPLEADO=============================================================================================
        //Agregar persona de emergencia empleado
        public int Agregar_Persona_Emergencia_Empleado(string nombre,string parentesco, string direccion,string telefono, int idempl)
        {
            string sql = "INSERT INTO  Persona_Emergencia_Empleado (peremr_Nomb_Apell,peremr_Parentesco,peremr_DireccionUbica,peremr_Telefono,peremr_usu_num_id)VALUES('" + nombre + "','" + parentesco + "','" + direccion + "','" + telefono + "'," + idempl.ToString()+")";

            return BdDatos.ejecutarSql(sql);
        }
        //Consultar persona de emergencia empleado
        public DataSet Consultar_PersonaEmergencia_Empleado(int idempl)
        {
           string sql = "SELECT peremr_id,peremr_Nomb_Apell,peremr_Parentesco,peremr_DireccionUbica,peremr_Telefono  FROM  Persona_Emergencia_Empleado WHERE peremr_usu_num_id=" + idempl.ToString() + "";
           return BdDatos.consultarConDataset(sql);
        }
        //Actualizar persona de emergencia empleado
        public int Actualizar_Persona_Emergencia_Empleado(string nombre,string parents,string direcci, string tele,int peremrid)
        {
            string sql = "UPDATE Persona_Emergencia_Empleado SET peremr_Nomb_Apell='" + nombre + "',peremr_Parentesco='" + parents + "',peremr_DireccionUbica='" + direcci + "',peremr_Telefono='" + tele + "' WHERE peremr_id=" + peremrid.ToString() + "";

            return BdDatos.ejecutarSql(sql);
        }
        //Eliminar persona de emergencia empleado
        public int Eliminar_Persona_Emergencia_Empleado(int peremrid)
        {
            string sql = "DELETE FROM Persona_Emergencia_Empleado WHERE peremr_id=" + peremrid.ToString() + "";

            return BdDatos.ejecutarSql(sql);
        }
        //==========================================================================================================================================

        //07/09/2017 Stiven palacios
        //Agregar numero de placas de los vheiculos asociados a un determinado empleado
        //Actualiza
        //
        public int Agregar_Placa_Vehiculo(int idemple ,string placarro1, string plamoto1, string placarro2, string plamoto2,
                                           string marcarro1,string marcarro2, int modelcarro1, int modelcarro2,
                                           int modelmoto1, int modelmoto2, string marmoto1, string marmoto2)
        {
            string sql = "insert into Vehiculo values(" + idemple.ToString() + ",'" +
                                                       placarro1 + "','" + plamoto1 + "','" + placarro2 + "','" + plamoto2 + "', '"+ marcarro1 + "',"
                                                       +" '" + marcarro2 + "', " + modelcarro1 + ", " + modelcarro2 + ""
                                                       +", " + modelmoto1 + ", " + modelmoto2 + ", '" + marmoto1 + "', '" + marmoto2 + "')";
            return BdDatos.ejecutarSql(sql);
        }

        public int Actualizar_Placa_Vehiculo(string placarro1, string plamoto1, string placarro2, string plamoto2,
                                            int idemple, string marcarro1, string marcarro2, int modelcarro1, int modelcarro2,
                                            int modelmoto1, int modelmoto2, string marmoto1, string marmoto2)
        {
            string sql = "update Vehiculo set veh_placa_carro1='" + placarro1 + "',veh_placa_moto1 = '" + plamoto1 + "',veh_placa_carro2 = '" + placarro2 + "',veh_placa_moto2 = '" + plamoto2 + "'," 
                + "  veh_marca_carro1 = '" + marcarro1 + "',veh_marca_carro2 = '" + marcarro2 + "',veh_modelo_carro1 = " + modelcarro1 + ", veh_modelo_carro2 = " + modelcarro2 + ", "
                + " veh_modelo_moto1 = " + modelmoto1 + ",veh_modelo_moto2 = " + modelmoto2 + ",veh_marca_moto1 = '" + marmoto1 + "', veh_marca_moto2 = '" + marmoto2 + "'  "
                + " where veh_usu_num_id = " + idemple.ToString() + "";

            return BdDatos.ejecutarSql(sql);
        }

        //Consulta el numero de personas para llamar en caso de emergencia por empleado
        public DataTable Consultar_Numero_Personas_Emergencia(int idemple)
        {
            string sql = "SELECT peremr_usu_num_id " +
                         " FROM persona_emergencia_empleado " +
                         " WHERE peremr_usu_num_id=" + idemple.ToString() + "";
            return BdDatos.CargarTabla(sql);
        }

        //Consulta el numero de personas para llamar en caso de emergencia por empleado
        public DataTable Verificar_Existencia_Placas(int idemple)
        {
            string sql = "SELECT veh_id " +
                         " FROM Vehiculo " +
                         " WHERE veh_usu_num_id=" + idemple.ToString() + "";
            return BdDatos.CargarTabla(sql);
        }
        public DataTable Verificar_Vehiculos_registrados(int idemple)
        {

            string sql = "SELECT veh_id FROM vehiculo WHERE veh_usu_num_id=" + idemple.ToString() + "";

            return BdDatos.CargarTabla(sql);
        }

        //Llenar el combo Tipo_Estudio
        public void ListarTipoEstudio(DropDownList myListTipEstudio)
        {   //Recuperar lista tipo eestusios
            DataTable dTable = new DataTable();
            string sql2 = "SELECT tipest_id,tipest_nombre FROM Tipo_Estudio";
            dTable = BdDatos.CargarTabla(sql2);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["tipest_nombre"].ToString(),
                                            row["tipest_nombre"].ToString());
                myListTipEstudio.Items.Insert(myListTipEstudio.Items.Count, lst);
            }
        }

        public DataSet Met_Obtener_TipEstudio()
        {
            string sql = "SELECT tipest_nombre,tipest_nombre FROM Tipo_Estudio";

            return BdDatos.consultarConDataset(sql);
        }

       public DataSet tipoestudio()
        {
            string sql = "SELECT estudio.est_id, ISNULL(Tipo_Estudio.tipest_Nombre, 'NO APLICA') AS est_tipo, estudio.est_titulo, estudio.est_entidad_educativa, estudio.est_fecha_fin, " +
                         " estudio.est_tiene_certificado, estudio.est_semetre_Actual, estudio.est_cursando " +
                         " FROM            estudio LEFT OUTER JOIN " +
                        " Tipo_Estudio ON estudio.est_tipo = Tipo_Estudio.tipest_Nombre";
            return BdDatos.consultarConDataset(sql);
        }

    }
}
