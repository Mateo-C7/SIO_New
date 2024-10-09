using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl
{
     public class InfoEstiba
    {
         public InfoEstiba()
         {}
         private int idEstiba;
         public int IdEstiba
         {
             get { return idEstiba; }
             set { idEstiba = value; }
         }

         private String orden;
         public String Orden
         {
             get { return orden; }
             set { orden = value; }
         }

         private String pais;
         public String Pais
         {
             get { return pais; }
             set { pais = value; }
         }

         private String numP;
         public String NumP
         {
             get { return numP; }
             set { numP = value; }
         }

         private String ciudad;
         public String Ciudad
         {
             get { return ciudad; }
             set { ciudad = value; }
         }

         private String material;
         public String Material
         {
             get { return material; }
             set { material = value; }
         }

         private int numpallet;
         public int Numpallet
         {
             get { return numpallet; }
             set { numpallet = value; }
         }

         private float peso;
         public float Peso
         {
             get { return peso; }
             set { peso = value; }
         }

         private float ancho;
         public float Ancho
         {
             get { return ancho; }
             set { ancho = value; }
         }

         private float alto;
         public float Alto
         {
             get { return alto; }
             set { alto = value; }
         }

         private float largo;
         public float Largo
         {
             get { return largo; }
             set { largo = value; }
         }

         private float volumen;
         public float Volumen
         {
             get { return volumen; }
             set { volumen = value; }
         }

         private int trasnId;
         public int TrasnId
         {
             get { return trasnId; }
             set { trasnId = value; }
         }

         private String direccion;
         public String Direccion
         {
             get { return direccion; }
             set { direccion = value; }
         }

         private String nombrecliente;
         public String Nombrecliente
         {
             get { return nombrecliente; }
             set { nombrecliente = value; }
         }

         private int cantiP;
         public int CantiP
         {
             get { return cantiP; }
             set { cantiP = value; }
         }

         private String codBarra;
         public String CodBarra
         {
             get { return codBarra; }
             set { codBarra = value; }
         }

         private String codBarraC;
         public String CodBarraC
         {
             get { return codBarraC; }
             set { codBarraC = value; }
         }

         private int pesoPallet;
         public int PesoPallet
         {
             get { return pesoPallet; }
             set { pesoPallet = value; }
         }

         private int estado;
         public int Estado
         {
             get { return estado; }
             set { estado = value; }
         }

         private int ofa;
         public int Ofa
         {
             get { return ofa; }
             set { ofa = value; }
         }

         private int idpallet;
         public int Idpallet
         {
             get { return idpallet; }
             set { idpallet = value; }
         }

         private string tipoOrden;
         public string TipoOrden
         {
             get { return tipoOrden; }
             set { tipoOrden = value; }
         }
    }
}