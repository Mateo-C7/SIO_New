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

namespace CapaControl
{
    public class ControlBalizas
    {
        //----------------------------------------------------------------- CARGA PANTALLA DE LA BALIZA-----------------------------------------------------------------//
      //-------------------------------------------Cargar Tabla Validacion-------------------------------------//

        public DataTable cargarTablaVal(int idBal, int ofa, int val)
        {
            String proc = "EXEC ConsultaBaliza_Metro " + idBal + "," + ofa + "," + val + "," + 1 + "";
            DataTable consulta = BdDatos.CargarTablaProccedure(proc);
            return consulta;
        }

        //-------------------------------------------Cargar Tabla NO Validacion-------------------------------------//

        //-------------------------Totales--------------///

        public int totalP(int idBal, int ofa, int val)
        {
            String proc = "EXEC ConsultaBaliza_Metro " + idBal + "," + ofa + "," + val + "," + 3 + "";
            int totalP = conteo(proc);
            return totalP;
        }

        public int totalF(int idBal, int ofa, int val)
        {
            String proc = "EXEC ConsultaBaliza_Metro " + idBal + "," + ofa + "," + val + "," + 4 + "";
            int totalP = conteo(proc);
            return totalP;
        }

        public int totalOrden(int ofa)
        {
            String proc = "EXEC ConsultaBaliza_Metro 0," + ofa + ",0," + 5 + "";
            int totalP = conteo(proc);
            return totalP;
        }

        public String sumasPesos(int idBal, int ofa, int val)
        {
            String peso = "0";
            String proc = "EXEC ConsultaBaliza_Metro " + idBal + "," + ofa + "," + val + "," + 6 + "";
            DataTable consulta = BdDatos.CargarTablaProccedure(proc);
            foreach (DataRow row in consulta.Rows)
            {
                peso = row["peso"].ToString();
            }
            if (peso == null || peso == "")
            {
                peso = "0";
            }
            return peso;
        }
        //-------------------------Totales--------------///
        /*-----------------------------------Conteo de los datos-------------------------------------*/
        public int conteo(String sql)
        {
            int nconteo = 0;
            DataTable consulta = BdDatos.CargarTablaProccedure(sql);
            foreach (DataRow row in consulta.Rows)
            {
                nconteo = nconteo + int.Parse(row["CANT"].ToString());
            }
            return nconteo;
        }
        /*-----------------------------------Conteo de los datos-------------------------------------*/

        //----------------------------------------------------------------- CARGA PANTALLA DE LA BALIZA-----------------------------------------------------------------//

        //----------------------------------------------------------------- VALIDACION DE LA PIEZA-----------------------------------------------------------------//

       
        public InfoBaliza verificarBal(int idBal, String filtro)
        {
            InfoBaliza infoB = null;
            String proc = "EXEC ValidacionBaliza_Metro 0," + idBal + ",0,0,0,0,'" + filtro + "'," + 3 + "";
            DataTable consulta = BdDatos.CargarTablaProccedure(proc);
            foreach (DataRow row in consulta.Rows)
            {
                infoB = new InfoBaliza();
                infoB.BalTipoVal = row["tipoVal"].ToString();
                infoB.BalNo = int.Parse(row["noBal"].ToString());
            }
            return infoB;
        }

        public void verificado(int idPieza, int pno, int idSaldos, int cedula)
        {
            String proc = "EXEC ValidacionBaliza_Metro 0,0," + idSaldos + "," + pno + "," + idPieza + "," + cedula + ",0," + 4 + "";
            BdDatos.Actualizar(proc);
        }

        public void grabarBita(int idSaldos, int pno, int cedula, int idBal)
        {
            String proc = "EXEC ValidacionBaliza_Metro 0," + idBal + "," + idSaldos + "," + pno + ",0," + cedula + ",0," + 5 + "";
            BdDatos.Actualizar(proc);
        }
        //----------------------------------------------------------------- VALIDACION DE LA PIEZA-----------------------------------------------------------------//
    }
}