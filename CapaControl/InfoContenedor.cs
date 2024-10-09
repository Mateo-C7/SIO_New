using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl
{
    public class InfoContenedor
    {
       public InfoContenedor()
       {}
       private int desp_Trans_id;

       public int Desp_Trans_id
       {
           get { return desp_Trans_id; }
           set { desp_Trans_id = value; }
       }

       private String transPlaca;
       public String TransPlaca
       {
           get { return transPlaca; }
           set { transPlaca = value; }
       }

       private int trans_idContenedor;
       public int Trans_idContenedor
       {
           get { return trans_idContenedor; }
           set { trans_idContenedor = value; }
       }

       private String estadoAC;
       public String EstadoAC
       {
           get { return estadoAC; }
           set { estadoAC = value; }
       }

       private int desp_idGrupo;
       public int Desp_idGrupo
       {
           get { return desp_idGrupo; }
           set { desp_idGrupo = value; }
       }

       private String numeroCont;
       public String NumeroCont
       {
           get { return numeroCont; }
           set { numeroCont = value; }
       }

       private String fechaInicio;
       public String FechaInicio
       {
           get { return fechaInicio; }
           set { fechaInicio = value; }
       }

       private String numDespacho;
       public String NumDespacho
       {
           get { return numDespacho; }
           set { numDespacho = value; }
       }

       private int idDespacho;
        

        public int IdDespacho
       {
           get { return idDespacho; }
           set { idDespacho = value; }
       }

        public String cont_Observ;
        public String Cont_Observ
        {
            get { return cont_Observ; }
            set { cont_Observ = value; }
        }

    }
}
