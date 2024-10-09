using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaDatos;

namespace CapaControl
{
    public class ControlFacturaProforma
    {
        public string NumeroALetras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }

            catch
            {
                return "";
            }
            
            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));

            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "CENTAVOS";
            }

            res = CapaControl.ControlFacturaProforma.NumeroALetras(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private static string NumeroALetras(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);

            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + NumeroALetras(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + NumeroALetras(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = NumeroALetras(Math.Truncate(value / 10) * 10) + " Y " + NumeroALetras(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + NumeroALetras(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = NumeroALetras(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = NumeroALetras(Math.Truncate(value / 100) * 100) + " " + NumeroALetras(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + NumeroALetras(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value % 1000);
            }
            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + NumeroALetras(value % 1000000);
            else if (value < 1000000000000)
            {

                Num2Text = NumeroALetras(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000) * 1000000);
            }
            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }

            return Num2Text;
        }

        public DataSet ConsultarIdiomaFacturaProforma()
        {
            string sql;
            sql = "SELECT idioma_factura_proforma.* FROM idioma_factura_proforma";
            DataSet ds_idiomaObra = BdDatos.consultarConDataset(sql);
            return ds_idiomaObra;
        }

        public DataSet ConsultarIdiomaFacturaEquivalente()
        {
            string sql;
            sql = "SELECT idioma_factura_equivalente.* FROM idioma_factura_equivalente";
            DataSet ds_idiomaObra = BdDatos.consultarConDataset(sql);
            return ds_idiomaObra;
        }

        public SqlDataReader ConsultarConsecutivoBogota()
        {
            string sql;
            sql = "SELECT confe_bogota FROM consecutivo_equivalente";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarConsecutivoCandelaria()
        {
            string sql;
            sql = "SELECT confe_candelaria FROM consecutivo_equivalente";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarConsecutivoForsa()
        {
            string sql;
            sql = "SELECT confe_forsa FROM consecutivo_equivalente";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarConsecutivoFactura()
        {
            string sql;
            sql = "SELECT fac_numero FROM consecutivo";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarSumaFactura(int IDFactura)
        {
            string sql;
            sql = "SELECT SUM(detfac_precio_total) AS Suma FROM detalle_factura WHERE detfac_fac_id = " + IDFactura + " ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarImpuesto()
        {
            string sql;
            sql = "SELECT imp_tipo, imp_id FROM impuesto";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public SqlDataReader ConsultarFacturaProforma(int numfac)
        {
            string sql;
            sql = "SELECT fac_nombre, fac_direccion, fac_nit, fac_telefono, fac_pais, fac_ciudad, fac_obra, " + 
                "fac_fecha, fac_subtotal, fac_expedicion, fac_descuento, fac_valor_descuento, fac_iva, fac_total, fac_moneda, " + 
                "fac_subpartida, fac_tdn, fac_valor_letras, fac_notas, fac_porcentaje, fac_anho, fac_id " +
                "FROM factura WHERE fac_numero = " + numfac + " ";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public int FacturaID(string Pais, string Ciudad, string Cliente, string Obra, string NIT, string Direccion, 
            string Telefono, string Fec, string Representante, string Fecha)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;               

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[10];
            sqls[0] = new SqlParameter("Pais", Pais);
            sqls[1] = new SqlParameter("Ciudad", Ciudad);
            sqls[2] = new SqlParameter("Nombre", Cliente);
            sqls[3] = new SqlParameter("Obra", Obra);
            sqls[4] = new SqlParameter("Nit", NIT);
            sqls[5] = new SqlParameter("Direccion", Direccion);
            sqls[6] = new SqlParameter("Telefono", Telefono);
            sqls[7] = new SqlParameter("Fecha", Fec);
            sqls[8] = new SqlParameter("Usuario", Representante);
            sqls[9] = new SqlParameter("FechaCrea", Fecha);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarFACTURA", con))
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

        public int DetalleFactura(int idfactura, int cant, string descrip, string preuni, string pretot, string usu_crea, 
            string fecha_crea, string usu_actualiza, string fecha_actualiza)
        {
            // Valor de retorno. Sabemos que si acaba valiendo -1 ha habido un error
            int Id = -1;

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[7];
            sqls[0] = new SqlParameter("@FacID", idfactura);
            sqls[1] = new SqlParameter("Cantidad", cant);
            sqls[2] = new SqlParameter("Descripcion", descrip);
            sqls[3] = new SqlParameter("PrecioUnitario", preuni);
            sqls[4] = new SqlParameter("PrecioTotal", pretot);
            sqls[5] = new SqlParameter("Usuario", usu_crea);
            sqls[6] = new SqlParameter("FechaCrea", fecha_crea);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("InsertarDETALLEFACTURA", con))
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

        public int ActualizarFactura(int idFactura, string subpartida, bool desc, string subtotal, string porc, string valor, 
            string iva, string totalfin, string moneda, string notas, string precio, int consec, string tdn, string anho)
        {
            int ckDesc = 0;

            if (desc == true)
            {
                ckDesc = 1;
            }

            string sql;

            sql = "UPDATE factura SET fac_subpartida = '" + subpartida + "', fac_descuento = " + ckDesc +
                ",  fac_subtotal = '" + subtotal + "', fac_porcentaje = '" + porc + "', fac_valor_descuento = '" + valor +
                "', fac_iva = '" + iva + "', fac_total = '" + totalfin + "', fac_moneda = '" + moneda +
                "', fac_notas = '" + notas + "', fac_valor_letras = '" + precio + "', fac_numero = " + consec +
                ", fac_tdn = '" + tdn + "', fac_anho = '" + anho + "' " + 
                "WHERE fac_id = " + idFactura;

            return BdDatos.Actualizar(sql);
        }

        //ACTUALIZA ENCABEZADO FACTURA
        public int ActualizarFacturaProforma(int idFactura, string pais, string ciudad, string cliente,
            string obra, string nit, string direccion, string telefono, string fecha, string subpartida,
            string tdn, bool desc, string subtotal, string porc, string moneda, string valdesc, string iva, string total,
            string notas, string precio)
        {
            int ckDesc = 0;

            if (desc == true)
            {
                ckDesc = 1;
            }

            string sql;

            sql = "UPDATE factura SET fac_pais = '" + pais + "', fac_ciudad = '" + ciudad + "',  fac_nombre = '" + cliente +
                "', fac_obra = '" + obra + "', fac_nit = '" + nit + "', fac_direccion = '" + direccion +
                "', fac_telefono = '" + telefono + "', fac_fecha = '" + fecha + "', fac_subpartida = '" + subpartida +
                "', fac_tdn = '" + tdn + "', fac_descuento = " + ckDesc + ", fac_subtotal = '" + subtotal +
                "', fac_porcentaje = '" + porc + "', fac_moneda = '" + moneda + "', fac_valor_descuento = '" + valdesc + 
                "', fac_iva = '" + iva + "', fac_total = '" + total + "', fac_notas = '" + notas + "', fac_valor_letras = '" + precio + "' " +
                "WHERE fac_numero = " + idFactura;

            return BdDatos.Actualizar(sql);
        }

        public int ActualizarConsecutivoBogota(int consec)
        {
            string sql;

            sql = "UPDATE consecutivo_equivalente SET confe_bogota = " + consec + " ";

            return BdDatos.Actualizar(sql);
        }

        public int ActualizarConsecutivoCandelaria(int consec)
        {
            string sql;

            sql = "UPDATE consecutivo_equivalente SET confe_candelaria = " + consec + " ";

            return BdDatos.Actualizar(sql);
        }

        public int ActualizarConsecutivoForsa(int consec)
        {
            string sql;

            sql = "UPDATE consecutivo_equivalente SET confe_forsa = " + consec + " ";

            return BdDatos.Actualizar(sql);
        }
    }
}
