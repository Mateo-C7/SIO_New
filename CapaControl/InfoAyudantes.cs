using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl
{
    public class InfoAyudantes
    {
      public InfoAyudantes()
      {}
      private int ayuId;
      public int AyuId
      {
          get { return ayuId; }
          set { ayuId = value; }
      }

      private String ayuPlaca;
      public String AyuPlaca
      {
          get { return ayuPlaca; }
          set { ayuPlaca = value; }
      }

      private int ayuCedula;
      public int AyuCedula
      {
          get { return ayuCedula; }
          set { ayuCedula = value; }
      }

      private String ayuFecha;
      public String AyuFecha
      {
          get { return ayuFecha; }
          set { ayuFecha = value; }
      }

      private String ayuActivo;
      public String AyuActivo
      {
          get { return ayuActivo; }
          set { ayuActivo = value; }
      }

      private int ayuIdContenedor;
      public int AyuIdContenedor
      {
          get { return ayuIdContenedor; }
          set { ayuIdContenedor = value; }
      }

      private String ayuNombreEmp;
      public String AyuNombreEmp
      {
          get { return ayuNombreEmp; }
          set { ayuNombreEmp = value; }
      }

      private int cedulaEmp;
      public int CedulaEmp
      {
          get { return cedulaEmp; }
          set { cedulaEmp = value; }
      }
    }
}
