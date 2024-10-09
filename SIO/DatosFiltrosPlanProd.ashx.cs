using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using dhtmlxConnectors;
using System.Configuration;
using CapaDatos;

namespace SIO
{
    /// <summary>
    /// Connector body
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]


    public class ComboConnector : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            string vTableName, vIdName, vDescName;

            //// 
            //vTableName = "TipoObs";
            //vIdName = "";
            //vDescName = "";

            vTableName = "SELECT      eacts_id,  eacts_descripcion   FROM  fup_actaseg_estado WHERE  (eacts_tipo = 'General') ";
            //vTableName = "fup_actaseg_probCierre  FROM    fup_actaseg_probCierre";
            vIdName = "eacts_id";
            vDescName = "eacts_descripcion";

            if (context.Request.QueryString["Tipo"] == "MotPer")
            {
                vTableName = "SELECT         asmp_id,     asmp_descripcion     FROM            fup_actaseg_motivoPerdida     WHERE        (asmp_activo = 1)      ORDER BY asmp_order  ";
                vIdName = "asmp_id";
                vDescName = "asmp_descripcion";
            }
            if (context.Request.QueryString["Tipo"] == "Coordinadores")
            {
                vTableName = "SELECT    representantes_comerciales.rc_descripcion, usuario.usu_siif_id " +
                                " FROM usuario INNER JOIN representantes_comerciales ON " +
                                     " usuario.usu_siif_id =representantes_comerciales.rc_usu_siif_id" +
                               " WHERE usuario.usu_rap_id = 26 AND usuario.usu_activo = 1 AND " +
                                     " representantes_comerciales.rc_activo = 1 AND " +
                                     " usuario.usu_siif_id <> 270 AND  (usuario.usu_siif_id <> 906)" +
                                     " AND usuario.usu_siif_id <> 1116";
                vIdName = "usu_siif_id";
                vDescName = "rc_descripcion";
            }


            if (context.Request.QueryString["Tipo"] == "TipoObs")
            {
                vTableName = "SELECT  asot_id, asot_descripcion  FROM     fup_actaseg_observac_tipo   WHERE  (asot_activo = 1) ";
                vIdName = "asot_id";
                vDescName = "asot_descripcion";
            }

            // plan produccion orden
            if (context.Request.QueryString["Tipo"] == "AnioPlanProd")
            {
                vTableName = "SELECT        anio   FROM            anio  WHERE        (activo = 1) AND (planActual = 1) ";
                vIdName = "anio";
                vDescName = "anio";
            }


            if (context.Request.QueryString["Tipo"] == "MesPlanProd")
            {
                vTableName = "SELECT mes_id, mes_nombre   FROM Mes ";
                vIdName = "mes_id";
                vDescName = "mes_nombre";
            }


            //Ingenieria
            if (context.Request.QueryString["Tipo"] == "TipoObsIng")
            {
                vTableName = "SELECT  asot_id, asot_descripcion  FROM  fup_actaseg_observac_tipo   WHERE  (asot_activo = 1)  AND (asot_id=9) ";
                vIdName = "asot_id";
                vDescName = "asot_descripcion";
            }

            if (context.Request.QueryString["Tipo"] == "ProbCi")
            {
                vTableName = "SELECT   aspc_id , aspc_descripcion FROM       fup_actaseg_probCierre  WHERE        (activo = 1)";
                //vTableName = "fup_actaseg_probCierre  FROM    fup_actaseg_probCierre";
                vIdName = "aspc_id";
                vDescName = "aspc_descripcion";
            }

            if (context.Request.QueryString["Tipo"] == "EmpresaCompe")
            {
                vTableName = "SELECT cli_id,cli_nombre FROM cliente WHERE cli_competencia=1 AND cli_activo=1 ORDER BY cli_nombre ASC";
                vIdName = "cli_id";
                vDescName = "cli_nombre";
            }

            if (context.Request.QueryString["Tipo"] == "Estatus")
            {
                vTableName = "SELECT      eacts_id,  eacts_descripcion   FROM  fup_actaseg_estado WHERE  (eacts_tipo = 'General') ";
                //vTableName = "fup_actaseg_probCierre  FROM    fup_actaseg_probCierre";
                vIdName = "eacts_id";
                vDescName = "eacts_descripcion";
            }

            if (context.Request.QueryString["Tipo"] == "EstatusDft")
            {
                vTableName = "SELECT eacts_id,  eacts_descripcion  FROM fup_actaseg_estado   WHERE (eacts_tipo = 'DefinicionTecnica') ";
                //vTableName = "fup_actaseg_probCierre  FROM    fup_actaseg_probCierre";
                vIdName = "eacts_id";
                vDescName = "eacts_descripcion";
            }

            if (context.Request.QueryString["Tipo"] == "Zona")
            {
                vTableName = "SELECT        grpa_id, grpa_gp1_nombre  FROM  fup_grupo_pais  WHERE   (activo = 1)  ORDER BY grpa_gp1_nombre";
                vIdName = "grpa_id";
                vDescName = "grpa_gp1_nombre";
            }
            if (context.Request.QueryString["Tipo"] == "Pais")
            {
                string grupid = context.Request.QueryString["grupo_Id"];
                vTableName = "select pai_id, pai_nombre  from pais where pai_grupopais_id = " + grupid + "";
                vIdName = "pai_id";
                vDescName = "pai_nombre";
            }
            if (context.Request.QueryString["Tipo"] == "ZonaCiu")
            {
                string pai_id = context.Request.QueryString["pai_Id"];
                vTableName = "SELECT zonac_id, zonac_nom FROM zonaCiudad WHERE zonac_pais = " + pai_id + " AND zonac_activo = 1";
                vIdName = "zonac_id";
                vDescName = "zonac_nom";
            }

            if (context.Request.QueryString["Tipo"] == "PlantaIng")
            {
                vTableName = "SELECT planta_id, planta_descripcion FROM planta_forsa WHERE planta_activo = 1";
                vIdName = "planta_id";
                vDescName = "planta_descripcion";
            }

            if (context.Request.QueryString["Tipo"] == "Planta")
            {
                vTableName = "SELECT planta_id, planta_descripcion FROM planta_forsa WHERE planta_activo = 1";
                vIdName = "planta_id";
                vDescName = "planta_descripcion";
            }

            if (context.Request.QueryString["Tipo"] == "PlantaPlan")
            {
                vTableName = "SELECT planta_id, planta_descripcion FROM planta_forsa WHERE planta_activo = 1 AND planta_id> 0";
                vIdName = "planta_id";
                vDescName = "planta_descripcion";
            }


            if (context.Request.QueryString["Tipo"] == "Material")
            {
                vTableName = "SELECT fup_tipo_venta_proy_id, descripcion FROM fup_tipo_venta_proyecto " +
                            " WHERE fup_tipo_venta_proyecto.activo = 1 AND fup_tipo_venta_proy_id <> 0";
                vIdName = "fup_tipo_venta_proy_id";
                vDescName = "descripcion";
            }

            if (context.Request.QueryString["Tipo"] == "EstIngenieria")
            {
                vTableName = "SELECT  estoper_id, estoper_descripcion, estoper_activo FROM Inge_Estatus_Operacion WHERE estoper_activo=1";
                vIdName = "estoper_id";
                vDescName = "estoper_descripcion";
            }

            if (context.Request.QueryString["Tipo"] == "PaisIng")
            {
                vTableName = "select pai_id, pai_nombre  from pais ";
                vIdName = "pai_id";
                vDescName = "pai_nombre";
            }
            if (context.Request.QueryString["Tipo"] == "CiudadIng")
            {
                string SubZ = context.Request.QueryString["SubZ"];
                vTableName = "SELECT ciu_id, ciu_nombre FROM CIUDAD WHERE ciu_zona= " + SubZ + " ORDER BY  ciu_nombre ASC";
                vIdName = "ciu_id";
                vDescName = "ciu_nombre";
            }

            if (context.Request.QueryString["Tipo"] == "TipoNegociox")
            {
                // ftne_nombre
                vTableName = "SELECT ftne_id AS IdTipoNegociacion, ftne_nombre AS TIpoNegociacion FROM fup_tipo_negociacion WHERE (ftne_estado = 1) AND(ftne_id = 1) OR (ftne_estado = 1) AND(ftne_id = 6)";
                vIdName = "IdTipoNegociacion";
                vDescName = "TIpoNegociacion";
            }

            if (context.Request.QueryString["Tipo"] == "TipoCotizacion")
            {
                vTableName = "SELECT tco_id,tco_nombre from tipo_cotizacion where tco_estado = 1 AND tco_id <> 0";
                vIdName = "tco_id";
                vDescName = "tco_nombre";
            }
            if (context.Request.QueryString["Tipo"] == "TipoCotizacionPlanos")
            {
                vTableName = "SELECT ftco_id,ftco_nombre FROM fup_tipo_cotizacion WHERE ftco_estado=1 AND ftco_Uso_fup=1";
                vIdName = "ftco_id";
                vDescName = "ftco_nombre";
            }
            if (context.Request.QueryString["Tipo"] == "Complejidad")
            {
                vTableName = "SELECT Id, Tipo FROM(SELECT  1 Id, 'Baja' Tipo UNION " +
                                                 " SELECT  2 Id, 'Media' Tipo UNION " +
                                                 " SELECT  3 Id, 'Alta' Tipo) AS SubSql";
                vIdName = "Id";
                vDescName = "Tipo";
            }

            if (context.Request.QueryString["Tipo"] == "Explosionado")
            {
                vTableName = "SELECT Id, Tipo FROM(SELECT  'NO' Id, 'NO' Tipo UNION " +
                                                 " SELECT  'SI' Id, 'SI' Tipo) AS SubSql";
                vIdName = "Id";
                vDescName = "Tipo";
            }

            dhtmlxComboConnector connector = new dhtmlxComboConnector(
                vTableName,
                vIdName,
                dhtmlxDatabaseAdapterType.SqlServer2005,
                BdDatos.conexionScope(),
                vDescName
            );

            if (context.Request.QueryString["dynamic"] != null)
                connector.SetDynamicLoading(2);
            return connector;
        }

    }
}