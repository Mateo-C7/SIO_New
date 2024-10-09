using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl.Enumeradores
{
    public enum TIPO_NEGOCIACION
    {
        venta = 1,
        reparacion = 2
    }

    public enum TIPO_COTIZACION
    {
        equipo_nuevo = 1,
        adaptaciones = 2,
        listados = 3
    }

    public enum Dominios
    {
        SI_NO,
        TIPO_VACIADO,
        SIS_SEGURIDAD,
        ALIN_VERTICAL,
        TIPO_FM_FACHADA,
        DET_UNION,
        FORMA_CONSTRUCCION,
        TIPO_EQUIPO,
        VIGA,
        ESCALERA,
        MUROS_NO_ESTRUC,
        CULATAS,
        PRETILES,
        CAP_DILATACION,
        ANTEPECHOS,
        JUNTA_ANTEPECHO,
        TENSOR_MURO,
        TIPO_VIVIENDA,
        APROBACION,
        ESCALERA_COT_RAPIDA
    }
}
