using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls;

namespace CapaControl
{
    public class ControlPais
    {
        BdDatos objdatos = new BdDatos();

        //obtiene los paises para establecerlos en un combo
        public DataSet ObtenerPais()
        {
            string sqlobtpais = "select pai_id,pai_nombre from pais  order by pai_nombre asc";

            return BdDatos.consultarConDataset(sqlobtpais);
        }

        /*consulta el detalle de un pais para despues
      verificar con los datos ingresados por el usuario*/
        public DataTable Met_Consultar_Detalle_Pais(int idpais)
        {
            string sqlcondetapais = " select pai_id ,pai_nombre, pai_impuesto,pai_zona_id,pai_moneda,longitud,latitud,zona_siat from pais where pai_id>0 and pai_id=" + idpais.ToString() + " order by pai_nombre asc";
            return BdDatos.CargarTabla(sqlcondetapais);
        }

        //Insertar el log update de la tabla pais
        public int Met_Insertar_Log_Pais(string tabla, int idpais, string campo, string usuario, string fecha, string valor, string valorNuevo,string evento)
        {
            string sqlinsert = "insert into Log_transacciones values('" + tabla + "'," + idpais.ToString() + ",'" + campo + "','" + usuario + "','" + fecha + "','" + valor + "','" + valorNuevo + "','"+evento+"')";

            return BdDatos.ejecutarSql(sqlinsert);
        }
        //Insertar el log delete de la tabla pais
        public int Met_Insertar_Log_Delete_Pais(string tabla, int idpais,string usuario, string fecha, string evento)
        {
            string sqlinsertlogdel = "insert into Log_transacciones (genlog_tabla,genlog_id_registro,genlog_usuario,genlog_fecha,genlog_Evento) values('" + tabla + "'," + idpais.ToString() + ",'" + usuario + "','" + fecha + "','" + evento + "')";

            return BdDatos.ejecutarSql(sqlinsertlogdel);
        }

        //Elimina un pais y su detalle
        public int Met_Eliminar_Pais(int idpai)
        {
            string sql = "delete from pais where pai_id=" + idpai.ToString() + "";

            return BdDatos.ejecutarSql(sql);
        }

        //Llena la gridview con los paises y las zonas
        public DataTable ListarPaises()
        {
           string sqlobtpaises =   " SELECT        pais.pai_id, pais.pai_nombre, pais.pai_impuesto, pais.pai_cod_erp, pais.pai_grupopais_id, pais.pai_zona_id, pais.pai_moneda, pais.longitud, f.grpa_gp1_nombre, pais.latitud, pais.zona_siat, s.nombre_zona, "+
                        " m.mon_descripcion, pais.pai_maneja_subzona  "+
            " FROM            pais INNER JOIN  "+
             "            siat_zona_pais AS s ON pais.zona_siat = s.siat_zona_id INNER JOIN  "+
               "          moneda AS m ON m.mon_id = pais.pai_moneda INNER JOIN  "+
                "         fup_grupo_pais AS f ON pais.pai_grupopais_id = f.grpa_id  "+
                " WHERE        (pais.pai_id > 0)  ORDER BY pais.pai_nombre  ";
            //string sqlobtpaises = "select pai_id ,pai_nombre, pai_impuesto,pai_cod_erp,pai_grupopais_id,pai_zona_id,pai_moneda,longitud,f.grpa_gp1_nombre,latitud,zona_siat,s.nombre_zona,m.mon_descripcion ,pai_maneja_subzona from pais inner join siat_zona_pais s on zona_siat=s.siat_zona_id inner join fup_grupo_pais f on f.grpa_id=pai_zona_id inner join moneda m on m.mon_id=pai_moneda where pai_id>0 order by pai_nombre asc";
            return BdDatos.CargarTabla(sqlobtpaises);
        }

        // Actualiza el detalle de un pais
        public int Met_Actualizar_Detalle_Pais(float impuesto, int zona, int moneda, float longitud, float latitud, int zonasiat, int paiId)
        {
            string sqlact = "update pais set pai_impuesto=" + impuesto.ToString() + ",pai_zona_id=" + zona.ToString() + ",pai_moneda=" + moneda.ToString() + ",longitud=" + longitud.ToString() + ",latitud=" + latitud.ToString() + ",zona_siat=" + zonasiat.ToString() + " where pai_id=" + paiId.ToString() + "";

            return BdDatos.ejecutarSql(sqlact);
        }

        //crea un nuevo pais con su detalle
        public int Met_Crear_Nuevo_Pais(string pais, float impuesto, int zona, int moneda, float longitud, float latitud, int zonasiat)
        {
            string sql = "insert into pais (pai_nombre,pai_impuesto,pai_zona_id,pai_moneda,longitud,latitud,zona_siat)  values('" + pais + "'," + impuesto.ToString() + ", " + zona.ToString() + "," + moneda.ToString() + "," + longitud.ToString() + "," + latitud.ToString() + "," + zonasiat.ToString() + ")";

            return BdDatos.ejecutarSql(sql);
        }

        //compara el nombre del pais a ingresar con la base de datos
        public DataTable Met_Verificar_Duplicidad_Pais(string nombrepais)
        {
            string sql = "select pai_nombre from pais where pai_nombre='" + nombrepais + "'";

            return BdDatos.CargarTabla2(sql);
        }
        public int Ejecutar_ProcAlma_Generar_Log_Pais(int pasid, string campo, string usuario, string fecha, string valorant, string valoact)
        {
            string sqlejeprocpais = " EXEC Generar_Log_Pais_prueba  " + pasid.ToString() + ",'" + campo + "','" + usuario + "','" + fecha + "','" + valorant + "','" + valoact + "'";

            return BdDatos.ejecutarSql(sqlejeprocpais);
        }
    }
}
