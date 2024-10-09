using CapaControl.Entity;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace CapaControl
{
	public class ControlPQRS
	{
		public FUPResumen obtenerFUPporOrdenFabricacion(string idOF)
		{
			string sql;

			if (idOF.Contains("-"))
			{
				sql = @"SELECT	Id_Of_P Id_Ofa,fup_id , ord_version ,pa.pai_nombre, ci.ciu_nombre, 
						cl.cli_nombre,   cc.ccl_nombre +' '+ cc.ccl_nombre2 +' '+ cc.ccl_apellido +' '+cc.ccl_apellido2 	as contacto
						,ob.obr_nombre 
						, o.hvpo_TelefonoResponsableObra
						, o.hvpo_ResponsableObra ResponsableObra 
						, o.hvpo_EmailResponsableObra EmailResponsableObra
						, ISNULL(o.hvpo_DireccionObra, ob.obr_direccion) DireccionObra
					FROM  formato_unico AS fu 
					INNER JOIN cliente AS cl ON fu.fup_cli_id = cl.cli_id 
					INNER JOIN contacto_cliente AS cc ON cc.ccl_id = fu.fup_ccl_id 
					INNER JOIN obra AS ob ON fu.fup_obr_id = ob.obr_id 
					INNER JOIN pais AS pa ON cl.cli_pai_id = pa.pai_id 
					INNER JOIN ciudad AS Ci ON cl.cli_ciu_id = Ci.ciu_id  
					INNER JOIN ( SELECT  o.Yale_Cotiza ,o.ord_version, o.Id_Of_P 
						FROM orden o 
						WHERE o.ord_version is not null and numero + '-' + ano = '" + idOF + 
						@"' GROUP BY o.Yale_Cotiza, o.ord_version ,Id_Of_P ) c on fup_id = Yale_Cotiza
						 LEFT OUTER JOIN 
						(SELECT EC.eect_fup_id , ec.eect_vercot_id,  min(o.hvpo_id) hvpo_id 
						 FROM dbo.fup_enc_entrada_cotizacion ec
							INNER JOIN dbo.fup_hvp_Observaciones o on ec.eect_id = hvpo_enc_entrada_cot_id and o.hvpo_TipoEntrada = 4
						 GROUP BY EC.eect_fup_id , ec.eect_vercot_id
						) e ON E.eect_fup_id = Yale_Cotiza and e.eect_vercot_id = ord_version
					LEFT OUTER JOIN  dbo.fup_hvp_Observaciones o ON e.hvpo_id = o.hvpo_id";
			}
			else
			{
				sql = @"SELECT	Id_Of_P Id_Ofa,fup_id , ord_version ,pa.pai_nombre, ci.ciu_nombre, 
									 cl.cli_nombre,   cc.ccl_nombre +' '+ cc.ccl_nombre2 +' '+ cc.ccl_apellido +' '+cc.ccl_apellido2 	as contacto,  ob.obr_nombre 
							, '' hvpo_TelefonoResponsableObra
							, '' ResponsableObra 
							, '' EmailResponsableObra
							, ob.obr_direccion DireccionObra
							FROM  formato_unico AS fu 
							INNER JOIN cliente AS cl ON fu.fup_cli_id = cl.cli_id 
							INNER JOIN contacto_cliente AS cc ON cc.ccl_id = fu.fup_ccl_id 
							INNER JOIN obra AS ob ON fu.fup_obr_id = ob.obr_id 
							INNER JOIN pais AS pa ON cl.cli_pai_id = pa.pai_id 
							INNER JOIN ciudad AS Ci ON cl.cli_ciu_id = Ci.ciu_id  
							LEFT OUTER JOIN ( SELECT TOP 1 o.Yale_Cotiza ,o.ord_version, o.Id_Of_P 
										 FROM orden o 
										 WHERE o.ord_version is not null and o.Yale_Cotiza = '" + idOF +
									   "' GROUP BY o.Yale_Cotiza, o.ord_version ,Id_Of_P ) c on fup_id = Yale_Cotiza  "
						 + " WHERE fu.fup_id = " + idOF;
			}

			DataTable dt = BdDatos.CargarTabla(sql);
			FUPResumen lst = dt.AsEnumerable()
				.Select(row => new FUPResumen
				{
					Ciudad = (string)row["ciu_nombre"],
					Cliente = (string)row["cli_nombre"],
					Contacto = (string)row["contacto"],
					IdFup = (int)row["fup_id"],
					Obra = (string)row["obr_nombre"],
					Pais = (string)row["pai_nombre"],
					IdOrden = (int)row["Id_Ofa"],
					Version = (string)row["ord_version"],
					DireccionRespuesta = row["DireccionObra"] == DBNull.Value ? "" : (string)row["DireccionObra"],
					EmailRespuesta = row["EmailResponsableObra"] == DBNull.Value ? "" : (string)row["EmailResponsableObra"],
					NombreRespuesta = row["ResponsableObra"] == DBNull.Value ? "" : (string)row["ResponsableObra"],
					TelefonoRespuesta = row["hvpo_TelefonoResponsableObra"] == DBNull.Value ? "" : (string)row["hvpo_TelefonoResponsableObra"],
				}).FirstOrDefault();
			dt.Clear();
			dt.Dispose();

			return lst;
		}

        public string ObtenerObraPorFup(int fupId)
        {
            try
            {
                string sql = "select obr_nombre from obra where obr_id = (select fup_obr_id from formato_unico where fup_id = " + fupId.ToString() + ")";
                DataTable dt = BdDatos.CargarTabla(sql);
                string lst = (string)dt.AsEnumerable().FirstOrDefault()["obr_nombre"];
                dt.Clear();
                dt.Dispose();
                return lst;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public List<PQRSFuente> ObtenerFuentesActivas()
		{
			string sql = @"SELECT [PQRSFuenteID],[Descripcion],[Activo]
							  FROM [dbo].[PQRSFuente]
							  WHERE Activo = 1";

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSFuente> lst = dt.AsEnumerable()
				.Select(row => new PQRSFuente
				{
					PQRSFuenteID = (int)row["PQRSFuenteID"],
					Descripcion = (string)row["Descripcion"],
					Activo = (bool)row["Activo"],
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}


		public List<PQRSTipo> ObtenerTiposPQRS()
		{
			string sql = @"SELECT [PQRSTipoID] ,[Descripcion]
						  FROM [PQRSTipo]";

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSTipo> lst = dt.AsEnumerable()
				.Select(row => new PQRSTipo
				{
					Id = (int)row["PQRSTipoID"],
					Descripcion = (string)row["Descripcion"]
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}

		public int[] ObtenerEstadosPermisos(int rolID, int usuarioID)
		{
			string sql = @"SELECT [RolID],[UsuarioID] ,[EstadoAntesID]
						  ,[EstadoDespuesID] ,[MostrarTodosEstados]
					  FROM [dbo].[PQRSFlujoAprobacion]
						WHERE RolID = " + rolID.ToString() + " OR UsuarioID = " + usuarioID;

			DataTable dt = BdDatos.CargarTabla(sql);

			DataRow dr = dt.AsEnumerable().FirstOrDefault();
			List<PQRSFlujoAprobacion> lst = dt.AsEnumerable()
				.Select(row => new PQRSFlujoAprobacion
				{
					EstadoAntesID = row["EstadoAntesID"] == System.DBNull.Value ? default(int?) : (int)row["EstadoAntesID"],
					EstadoDespuesID = row["EstadoDespuesID"] == System.DBNull.Value ? default(int?) : (int)row["EstadoDespuesID"],
					MostrarTodosEstados = (bool)row["MostrarTodosEstados"],
					RolID = row["RolID"] == System.DBNull.Value ? default(int?) : (int)row["RolID"],
					UsuarioID = row["UsuarioID"] == System.DBNull.Value ? default(int?) : (int)row["UsuarioID"],
				}).ToList();
			dt.Clear();
			dt.Dispose();

			bool TodosEstados = lst.Any(x => x.MostrarTodosEstados);

			if (TodosEstados)
				return null;
			else
				return lst.Where(x => x.EstadoAntesID.HasValue).Select(x => x.EstadoAntesID.Value).ToArray();

		}
		public Permiso ObtenerPermisos(int rolID, int usuarioID)
		{
			string sql = @"SELECT [RolID],[UsuarioID] ,[EstadoAntesID]
						  ,[EstadoDespuesID] ,[MostrarTodosEstados]
					  FROM [dbo].[PQRSFlujoAprobacion]
						WHERE RolID = " + rolID.ToString() + " OR UsuarioID = " + usuarioID;

			DataTable dt = BdDatos.CargarTabla(sql);

			DataRow dr = dt.AsEnumerable().FirstOrDefault();
			List<PQRSFlujoAprobacion> lst = dt.AsEnumerable()
				.Select(row => new PQRSFlujoAprobacion
				{
					EstadoAntesID = row["EstadoAntesID"] == System.DBNull.Value ? default(int?) : (int)row["EstadoAntesID"],
					EstadoDespuesID = row["EstadoDespuesID"] == System.DBNull.Value ? default(int?) : (int)row["EstadoDespuesID"],
					MostrarTodosEstados = (bool)row["MostrarTodosEstados"],
					RolID = row["RolID"] == System.DBNull.Value ? default(int?) : (int)row["RolID"],
					UsuarioID = row["UsuarioID"] == System.DBNull.Value ? default(int?) : (int)row["UsuarioID"],
				}).ToList();
			dt.Clear();
			dt.Dispose();

			Permiso permiso = new Permiso();
			permiso.SinPermiso = false;
			/*if (lst == null || lst.Count == 0)
			{
				permiso.SinPermiso = true;
			}
			else
			{
				permiso = new Permiso()
				{
					SinPermiso = false,
					VerTodos = lst.Any(x => x.MostrarTodosEstados),
					AsignaProceso = lst.Any(x => x.EstadoAntesID == (int)EstadosPQRS.Radicado),
					RepuestaProceso = lst.Any(x => x.EstadoAntesID == (int)EstadosPQRS.Asignacion),
					ReclamoProcedente = lst.Any(x => x.EstadoAntesID == (int)EstadosPQRS.RespuestaProceso),
					OrdenGarantia = lst.Any(x => x.EstadoAntesID == (int)EstadosPQRS.ReclamoProcedente),
					RespuestaCliente = lst.Any(x => x.EstadoAntesID == (int)EstadosPQRS.Ingenieria),
					Produccion = lst.Any(x => x.EstadoAntesID == (int)EstadosPQRS.RespuestaCliente),
					CargaListados = lst.Any(x => x.EstadoAntesID == (int)EstadosPQRS.Produccion),
					ImplementacionObra = lst.Any(x => x.EstadoAntesID == (int)EstadosPQRS.CargarListado),
				};
			}*/

			return permiso;
		}

		public int GuardarPQRS(PQRSDTO pqrs)
		{
			string conexion = BdDatos.conexionScope();
			int Id = 0;
			int EstadoID = (int)EstadosPQRS.Elaboracion;
			string usuario = pqrs.UsuarioCreacion;
			object nroOrden = System.DBNull.Value;

			if (!string.IsNullOrWhiteSpace(pqrs.NroOrden))
			{
				nroOrden = pqrs.NroOrden;
			}

			object idFuente = System.DBNull.Value;
			if (pqrs.IdFuenteReclamo.HasValue)
			{
				idFuente = pqrs.IdFuenteReclamo;
			}

			object idFup = System.DBNull.Value;
			if (pqrs.IdFup.HasValue)
			{
				idFup = pqrs.IdFup;
			}

			object idOrden = System.DBNull.Value;
			if (pqrs.IdOrden.HasValue)
			{
				idOrden = pqrs.IdOrden;
			}

			string version = string.Empty;
			if (!string.IsNullOrWhiteSpace(pqrs.Version))
			{
				version = pqrs.Version;
			}


			object _IdTipoFuenteReclamo = System.DBNull.Value;
			if (pqrs.IdTipoFuenteReclamo.HasValue)
			{
				_IdTipoFuenteReclamo = pqrs.IdTipoFuenteReclamo;
			}

			string _OtroCliente = string.Empty;
			if (!string.IsNullOrWhiteSpace(pqrs.OtroCliente))
			{
				_OtroCliente = pqrs.OtroCliente;
			}


			object _IdCliente = System.DBNull.Value;
			if (pqrs.IdCliente.HasValue)
			{
				_IdCliente = pqrs.IdCliente;
			}


			object _TipoSubPQRSId = System.DBNull.Value;
			if (pqrs.TipoSubPQRSId.HasValue)
			{
				_TipoSubPQRSId = pqrs.TipoSubPQRSId;
			}

			object _IdPais = pqrs.IdPais;
			object _IdCiudad = pqrs.IdCiudad;
			if(pqrs.IdCliente != -1)
			{
				_IdPais = System.DBNull.Value;
				_IdCiudad = System.DBNull.Value;
			}

			SqlParameter[] sqls = new SqlParameter[21];
			sqls[0] = new SqlParameter("@NroOrden", nroOrden);
			sqls[1] = new SqlParameter("@IdFuenteReclamo", idFuente);
			sqls[2] = new SqlParameter("@Detalle", pqrs.Detalle);
			sqls[3] = new SqlParameter("@NombreRespuesta", pqrs.NombreRespuesta);
			sqls[4] = new SqlParameter("@DireccionRespuesta", pqrs.DireccionRespuesta);
			sqls[5] = new SqlParameter("@EmailRespuesta", pqrs.EmailRespuesta);
			sqls[6] = new SqlParameter("@TelefonoRespuesta", pqrs.TelefonoRespuesta);
			sqls[7] = new SqlParameter("@FechaCreacion", DateTime.Now);
			sqls[8] = new SqlParameter("@UsuarioCreacion", usuario);
			sqls[9] = new SqlParameter("@EstadoID", EstadoID);
			sqls[10] = new SqlParameter("@IdFup", idFup);
			sqls[11] = new SqlParameter("@IdOrden", idOrden);
			sqls[12] = new SqlParameter("@Version", version);
			sqls[13] = new SqlParameter("@TipoPQRSId", pqrs.TipoPQRSId);
			sqls[14] = new SqlParameter("@Colaborador", string.IsNullOrEmpty(pqrs.Colaborador) ? string.Empty : pqrs.Colaborador);
			sqls[15] = new SqlParameter("@IdTipoFuente", _IdTipoFuenteReclamo);
			sqls[16] = new SqlParameter("@IdCliente", _IdCliente);
			sqls[17] = new SqlParameter("@OtroCliente", _OtroCliente);
			sqls[18] = new SqlParameter("@TipoSubPQRSId", _TipoSubPQRSId);
			sqls[19] = new SqlParameter("@IdPais", _IdPais);
			sqls[20] = new SqlParameter("@IdCiudad", _IdCiudad);

			string sql = @"INSERT INTO PQRS(NroOrden, IdFuenteReclamo, Detalle, NombreRespuesta, DireccionRespuesta, EmailRespuesta, TelefonoRespuesta, FechaCreacion, UsuarioCreacion, EstadoID, IdFup, IdOrden, [Version], TipoPQRSId, Colaborador,IdTipoFuente,IdCliente,OtroCliente,TipoSubPQRSId, IdPais, IdCiudad)
							 VALUES(@NroOrden, @IdFuenteReclamo, @Detalle, @NombreRespuesta, @DireccionRespuesta, @EmailRespuesta, @TelefonoRespuesta, @FechaCreacion, @UsuarioCreacion, @EstadoID, @IdFup, @IdOrden, @Version, @TipoPQRSId, @Colaborador,@IdTipoFuente,@IdCliente,@OtroCliente,@TipoSubPQRSId,@IdPais,@IdCiudad);
						   ;SELECT  SCOPE_IDENTITY()";

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddRange(sqls);

					// Abrimos la conexión y ejecutamos el ExecuteReader
					con.Open();
					SqlTransaction transaction = con.BeginTransaction();
					cmd.Transaction = transaction;

					try
					{
						Id = Convert.ToInt32(cmd.ExecuteScalar());

						if (pqrs.archivos != null && pqrs.archivos.Count > 0)
						{
							foreach (var file in pqrs.archivos)
							{
								string filePath = SaveFilePath(Id, file.base64, file.nameFile, "Pqrs");
								SaveFileBD(con, transaction, Id, file.nameFile, filePath, pqrs.UsuarioCreacion);
							}
						}

						SaveLog(con, transaction, Id, usuario, DBNull.Value, EstadoID);

						transaction.Commit();
					}
					catch (Exception e)
					{
						transaction.Rollback();
					}
				}
			}
			return Id;
		}

		private string SaveFilePath(int PQRSId, string stringBase64File, string nameFile, string namedir)
		{
			HttpContext.Current.Server.MapPath("~");

//            String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
			String rutaAplicacion = @"i:\";//Se mueve al File server 
			rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
			String dlDir = @"\ArchivosPQRS\P" + PQRSId + @"\" + namedir + @"\";
			String directorio = rutaAplicacion + dlDir;
			if (!Directory.Exists(directorio))
			{
				Directory.CreateDirectory(directorio);
			}

			string pathFile = directorio + nameFile;
			File.WriteAllBytes(pathFile, Convert.FromBase64String(stringBase64File));

			return dlDir.Replace(@"\", "/") + nameFile;
		}

		public static string DeleteFile(int PQRSId, string nameFile, string namedir, int idArchivo, int idTipoArchivo)
		{
			HttpContext.Current.Server.MapPath("~");

			//            String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
			String rutaAplicacion = @"i:\";//Se mueve al File server 
			rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
			String dlDir = namedir;
			String directorio = rutaAplicacion + dlDir;
			string pathFile = directorio;
			var objeto = new
			{
				id = 0,
				descripcion = ""
			};

			

				try
				{
					File.Delete(pathFile);

					BorrarArchivosPqrs(idArchivo, idTipoArchivo);
					objeto = new
					{
						id = 1,
						descripcion = "Archivo Borrado Correctamente"
					};

				}
				catch (Exception ex)
				{
					objeto = new
					{
						id = 2,
						descripcion = "Error Borrando Archivo : " + ex.Message
					};

				}
			
			return dlDir.Replace(@"\", "/") + nameFile;
		}

		private static void BorrarArchivosPqrs(int idArchivo, int idTipoArchivo)
		{
			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("@pIdArchivo", idArchivo);
			parametros.Add("@pIdTipoArchivo", idTipoArchivo);
			BdDatos.EjecutarStoreProcedureConParametros("USP_fup_DEL_AnexoPqrs", parametros);
		}

		private void SaveFileBD(SqlConnection con, SqlTransaction transaction, int PQRSId, string nameFile, string pathFile, string usuario)
		{
			SqlParameter[] sqls = new SqlParameter[4];
			sqls[0] = new SqlParameter("@IdPQRS ", PQRSId);
			sqls[1] = new SqlParameter("@FilePATH", pathFile);
			sqls[2] = new SqlParameter("@FileName", nameFile);
			sqls[3] = new SqlParameter("@UsuarioArchivo", usuario);

			string sql = @"INSERT INTO PQRSArchivos(IdPQRS, FilePATH, FileName, UsuarioArchivo)
							 VALUES(@IdPQRS, @FilePATH, @FileName, @UsuarioArchivo);
						   ";

			using (SqlCommand cmd = new SqlCommand(sql, con))
			{
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddRange(sqls);

				// Abrimos la conexión y ejecutamos el ExecuteReader
				cmd.Transaction = transaction;
				cmd.ExecuteNonQuery();
			}
		}

		public void SaveLog(SqlConnection con, SqlTransaction transaction, int PQRSId, string usuario, object EstadoAntesID, int EstadoDespuesID, DateTime? fechaActual = null)
		{
			fechaActual = fechaActual.HasValue ? fechaActual.Value : DateTime.Now;

			SqlParameter[] sqls = new SqlParameter[5];
			sqls[0] = new SqlParameter("@PQRSId ", PQRSId);
			sqls[1] = new SqlParameter("@EstadoAntesID", EstadoAntesID);
			sqls[2] = new SqlParameter("@EstadoDespuesID", EstadoDespuesID);
			sqls[3] = new SqlParameter("@Fecha", fechaActual);
			sqls[4] = new SqlParameter("@Usuario", usuario);

			string sql = @"INSERT INTO PQRSLog(PQRSId, EstadoAntesID, EstadoDespuesID, Fecha, Usuario)
							 VALUES(@PQRSId, @EstadoAntesID, @EstadoDespuesID, @Fecha, @Usuario);
						   ";

			using (SqlCommand cmd = new SqlCommand(sql, con))
			{
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddRange(sqls);

				// Abrimos la conexión y ejecutamos el ExecuteReader
				cmd.Transaction = transaction;
				cmd.ExecuteNonQuery();
			}
		}

		private void UpdateEstadoPQRS(SqlConnection con, SqlTransaction transaction, int PQRSId, int EstadoID)
		{

			SqlParameter[] sqls = new SqlParameter[2];
			sqls[0] = new SqlParameter("@IdPQRS ", PQRSId);
			sqls[1] = new SqlParameter("@EstadoID", EstadoID);

			string sql = @"UPDATE  PQRS SET EstadoID = @EstadoID WHERE IdPQRS =  @IdPQRS  ";

			using (SqlCommand cmd = new SqlCommand(sql, con))
			{
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddRange(sqls);

				// Abrimos la conexión y ejecutamos el ExecuteReader
				cmd.Transaction = transaction;
				cmd.ExecuteNonQuery();
			}
		}

		public List<PQRSDTO> ObtenerPRQS(PQRSDTO pqrs)
		{

			string sql = @"SELECT [IdPQRS]
							  ,[NroOrden]
							  ,[IdFuenteReclamo]
							  ,[Detalle]
							  ,[NombreRespuesta]
							  ,[DireccionRespuesta]
							  ,[EmailRespuesta]
							  ,[TelefonoRespuesta]
							  ,[FechaCreacion]
							  ,[UsuarioCreacion]
							  ,[EstadoID]
							  ,isnull([IdFup],0) IdFup
							  ,isnull([IdOrden],0) IdOrden
							  ,isnull([Version],'') Version
						  FROM [dbo].[PQRS] WHERE [EstadoID] < 11";

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSDTO> lst = dt.AsEnumerable()
				.Select(row => new PQRSDTO
				{
					IdPQRS = (int)row["IdPQRS"],
					NroOrden = (string)row["NroOrden"],
					IdFuenteReclamo = (int)row["IdFuenteReclamo"],
					Detalle = (string)row["Detalle"],
					NombreRespuesta = (string)row["NombreRespuesta"],
					DireccionRespuesta = (string)row["DireccionRespuesta"],
					EmailRespuesta = (string)row["EmailRespuesta"],
					TelefonoRespuesta = (string)row["TelefonoRespuesta"],
					//  FechaCreacion = (string)row["FechaCreacion"],
					UsuarioCreacion = (string)row["UsuarioCreacion"],
					// EstadoID = (int)row["EstadoID"],
					IdFup = (int)row["IdFup"],
					IdOrden = (int)row["IdOrden"],
					Version = (string)row["Version"],

				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}
		public List<PQRSDTOHistorico> ObtenerPRQSHistorico(string idpqrs)
		{
			string sql = @"select 
						  [dbo].[PQRSLog].PQRSLogId, 
						  [dbo].[PQRSLog].PQRSId, 
						  [dbo].[PQRSLog].Fecha, 
						  [dbo].[PQRSLog].Usuario,
						  [dbo].[PQRSLog].EstadoDespuesID,
						  ISNULL(ANTES.Descripcion,'NA')  EstadoAntes,
						  DESPUES.Descripcion EstadoDespues
						  from [dbo].[PQRSLog] 
						  LEFT JOIN [dbo].[PQRSEstados] ANTES ON ANTES.PQRSEstadosID = [dbo].[PQRSLog].EstadoAntesID
						  INNER JOIN [dbo].[PQRSEstados] DESPUES ON DESPUES.PQRSEstadosID = [dbo].[PQRSLog].EstadoDespuesID
						  where [dbo].[PQRSLog].PQRSId =" + idpqrs;

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSDTOHistorico> lst = dt.AsEnumerable()
				.Select(row => new PQRSDTOHistorico
				{
					Id = (int)row["PQRSLogId"],
					PQRS = (int)row["PQRSId"],
					EstadoDespuesID = (int)row["EstadoDespuesID"],
					EstadoDespues = (string)row["EstadoDespues"],
					EstadoAntes = (string)row["EstadoAntes"],
					Fecha = (DateTime)row["Fecha"],
					Usuario = (string)row["Usuario"],

				}).ToList();
			dt.Clear();
			dt.Dispose();

			lst.ForEach(x => x.FechaFormat = x.Fecha.ToString("dd/MM/yyyy hh:mm tt"));

			return lst;
		}

		public List<PQRSDTOArchivo> ObtenerPQRSArchivo(string idpqrs)
		{

			string sql = @"select IdPQRSArchivos, IdPQRS, FilePATH, FileName from [dbo].[PQRSArchivos] where idPQRS=" + idpqrs;

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSDTOArchivo> lst = dt.AsEnumerable()
				.Select(row => new PQRSDTOArchivo
				{
					Id = (int)row["IdPQRSArchivos"],
					PQRS = (int)row["IdPQRS"],
					FilePATH = (string)row["FilePATH"],
					FileName = (string)row["FileName"],
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}

		public List<PQRSDTOArchivo> ObtenerPQRSArchivosComprobante(string idpqrs)
		{

			string sql = @"select IdPQRSProduccionArchivos as IdPQRSArchivos, IdPQRS, FilePATH, FileName from [dbo].[PQRSProduccionArchivos] where idPQRS=" + idpqrs;

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSDTOArchivo> lst = dt.AsEnumerable()
				.Select(row => new PQRSDTOArchivo
				{
					Id = (int)row["IdPQRSArchivos"],
					PQRS = (int)row["IdPQRS"],
					FilePATH = (string)row["FilePATH"],
					FileName = (string)row["FileName"],
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}

		public PQRSDTOConsulta ObtenerPQRSId(string idpqrs, string email)
		{
			string sql = @"SELECT [IdPQRS]
							  ,[NroOrden]
							  ,b.[Descripcion] Fuente
							  ,[Detalle]
							  ,[NombreRespuesta]
							  ,[DireccionRespuesta]
							  ,[EsProcedente]
							  ,[EmailRespuesta]
							  ,[TelefonoRespuesta]
							  ,[FechaCreacion]
							  ,[UsuarioCreacion]
							  ,c.[Descripcion] Estado
							  ,a.EstadoID
							  , a.[TipoPQRSId]
							  ,pt.Descripcion TipoPQRS
							  , IIF(a.EstadoID = 2 AND EXISTS(SELECT * FROM PQRSProceso WHERE PQRSID = a.IdPQRS AND Email like '%" + email + @"%' 
								AND NOT EXISTS(SELECT * FROM [PQRSRespuesta] WHERE IdPQRS = a.IdPQRS AND Usuario like '%" + email + @"%')	), 1, 0) as Responde
							  ,isnull([IdFup],0) IdFup
							  ,isnull([IdOrden],0) IdOrden
							  ,isnull([Version],'') Version
							  ,ISNULL(CASE  
								  WHEN CLF.cli_id IS NOT NULL THEN CLf.cli_nombre
								  WHEN ISNULL(a.IdCliente,-1) <> -1 THEN cl.cli_nombre
								  ELSE  a.OtroCliente END,'') Cliente
							  ,a.IdTipoFuente
							  ,a.IdFuenteReclamo
							  ,a.Colaborador
							  ,tf.Descripcion TipoFuente
						  FROM [dbo].[PQRS] a
						  left join [dbo].[PQRSFuente] b on b.[PQRSFuenteID] = a.[IdFuenteReclamo]
						  inner join [dbo].[PQRSEstados] c on c.[PQRSEstadosID] = a.[EstadoID]
						  inner join [dbo].[PQRSTipo] pt on pt.[PQRSTipoID] = a.[TipoPQRSId]
						  inner join PQRSTipoFuente tf on tf.PQRSTipoFuenteID = a.IdTipoFuente
						  left join cliente CL on cl.cli_id = a.IdCliente
						  left join formato_unico f on f.fup_id = a.IdFup
						  left join cliente CLf on clf.cli_id = f.fup_cli_id
							WHERE a.IdPQRS = " + idpqrs;

			DataTable dt = BdDatos.CargarTabla(sql);
			PQRSDTOConsulta lst = dt.AsEnumerable()
				.Select(row => new PQRSDTOConsulta
				{
					IdPQRS = (int)row["IdPQRS"],
					NroOrden = row["NroOrden"] == System.DBNull.Value ? string.Empty : (string)row["NroOrden"],
					Fuente = row["Fuente"] == System.DBNull.Value ? string.Empty : (string)row["Fuente"],
					Detalle = (string)row["Detalle"],
					NombreRespuesta = (string)row["NombreRespuesta"],
					DireccionRespuesta = (string)row["DireccionRespuesta"],
					EmailRespuesta = (string)row["EmailRespuesta"],
					TelefonoRespuesta = (string)row["TelefonoRespuesta"],
					FechaCreacion = (DateTime)row["FechaCreacion"],
					UsuarioCreacion = (string)row["UsuarioCreacion"],
					Estado = (string)row["Estado"],
					EstadoID = (int)row["EstadoID"],
					Responde = (int)row["Responde"],
					TipoPQRS = (string)row["TipoPQRS"],
					TipoPQRSId = (int)row["TipoPQRSId"],
					IdFup = (int)row["IdFup"],
					IdOrden = (int)row["IdOrden"],
					Version = (string)row["Version"],
					Cliente = (string)row["Cliente"],
					EsProcedente = row["EsProcedente"] == System.DBNull.Value ? false : (bool)row["EsProcedente"],
					IdTipoFuente = row["IdTipofuente"] == System.DBNull.Value ? 0 : (int)row["IdTipofuente"],
					IdFuenteReclamo = (int)row["IdFuenteReclamo"],
					Colaborador = (string)row["Colaborador"],
					TipoFuente = row["TipoFuente"] == System.DBNull.Value ? string.Empty : (string)row["TipoFuente"]
				}).FirstOrDefault();
			dt.Clear();
			dt.Dispose();

			return lst;
		}

		

		public List<PQRSDTOConsulta> ObtenerPRQSpor(int RolID, int UsuarioID, string email, string NombreUsuario, PQRSFiltros pqrs)
		{
			int[] EstadosDisponibles = ObtenerEstadosPermisos(RolID, UsuarioID);

			string sql = @"SELECT a.[IdPQRS]
							  ,[NroOrden]
							  ,b.[Descripcion] Fuente
							  ,[Detalle]
							  ,[NombreRespuesta]
							  ,[DireccionRespuesta]
							  ,[EmailRespuesta]
							  ,[TelefonoRespuesta]
							  ,[FechaCreacion]
							  ,[UsuarioCreacion]
							  ,c.[Descripcion] Estado
							  ,a.EstadoID
							  , a.[TipoPQRSId]
							  , a.Idordenprocedente
							  ,pt.Descripcion TipoPQRS
							  , IIF(a.EstadoID = 2 AND EXISTS(SELECT * FROM PQRSProceso WHERE PQRSID = a.IdPQRS AND Email like '%" + email + @"%' 
								AND NOT EXISTS(SELECT * FROM [PQRSRespuesta] WHERE IdPQRS = a.IdPQRS AND Usuario like '%" + email + @"%')	), 1, 0) as Responde
						  ,ISNULL(CASE WHEN ISNULL(a.IdCliente,-1) = -1 THEN a.OtroCliente ELSE cl.cli_nombre END,'') Cliente
						  ,ISNULL(pais.pai_nombre,'') Pais
							,c.Color ColorEstado
							,c.ColorClase 
							,a.IdOrden IdOrdenOrigen
							,rtrim(og.Tipo_Of)+'-'+RTRIM(CONVERT(VARCHAR(10),og.Num_Of))+'-'+og.Ano_Of OrdenProcedente 
							,COUNT(com.IdPQRSComunicado) CantidadComunicados 
							,[dbo].[fn_PQRS_calcularSemaforo](a.IdPQRS)  Semaforo
							,(SELECT CASE WHEN EXISTS (
								SELECT 1
								FROM PQRSListados
								WHERE PQRSListados.Correo LIKE '%" + email + @"%'
							) OR EXISTS (
								SELECT 1
								FROM PQRSPlanos
								WHERE PQRSPlanos.Correo LIKE '%" + email + @"%'
							) OR EXISTS (
								SELECT 1
								FROM PQRSProceso
								WHERE PQRSProceso.Email LIKE '%" + email + @"%'
							) THEN 1 ELSE 0 END) AS IsInvolucred
							,tf.Descripcion TipoFuente
							,rtrim(og.Tipo_Of)+'-'+RTRIM(CONVERT(VARCHAR(10),og.Num_Of))+'-'+og.Ano_Of OrdenProcedente
							,CASE
								WHEN OrdenGarantiaOMejora = 'OG' THEN 'Orden de Garantía'
								WHEN OrdenGarantiaOMejora = 'OM' THEN 'Orden de Mejora'
								ELSE ''
							END AS OrdenGarantiaOMejora 
							,CASE
								WHEN EsProcedente = 1 THEN 'Si'
								WHEN EsProcedente = 0 THEN 'No'
								ELSE ''
							END AS EsProcedenteDescripcion
							,a.DescripcionNoProcedente DescripcionProcedencia 
							,SUM(com.SeEnvioEncuesta) as SeEnvioEncuesta 
							,ISNULL(a.PuedeSerCerrada, 0) PuedeSerCerrada 
						  FROM [dbo].[PQRS] a
						  left join [dbo].[PQRSFuente] b on b.[PQRSFuenteID] = a.[IdFuenteReclamo]
						  inner join [dbo].[PQRSEstados] c on c.[PQRSEstadosID] = a.[EstadoID]
						  inner join [dbo].[PQRSTipo] pt on pt.[PQRSTipoID] = a.[TipoPQRSId]
						  inner join PQRSTipoFuente tf on tf.PQRSTipoFuenteID = a.IdTipoFuente
						  left join cliente CL on cl.cli_id = a.IdCliente 
						  left join [dbo].[pais] pais on cl.cli_pai_id = pais.pai_id
						  left JOIN Orden_Seg og ON og.Id_Ofa = a.IdOrdenProcedente 
						  left JOIN [dbo].PQRSComunicado com ON com.IdPQRS = a.IdPQRS ";
			string sqlwere = " Where ";
			if (!string.IsNullOrEmpty(pqrs.nombre) && !string.IsNullOrWhiteSpace(pqrs.nombre))
				sqlwere = sqlwere + " [NombreRespuesta] like '%" + pqrs.nombre + "%' AND ";
			if (!string.IsNullOrEmpty(pqrs.orden) && !string.IsNullOrWhiteSpace(pqrs.orden))
				sqlwere = sqlwere + " [NroOrden] like '%" + pqrs.orden + "%' AND ";
			if (!string.IsNullOrEmpty(pqrs.idpqrs) && !string.IsNullOrWhiteSpace(pqrs.idpqrs))
				sqlwere = sqlwere + " a.[IdPQRS] like " + pqrs.idpqrs + " AND ";
			if (pqrs.fuente > 0)
				sqlwere = sqlwere + " b.[PQRSFuenteID] = " + pqrs.fuente.ToString() + " AND ";
			if(pqrs.estado > -1)
			{
				sqlwere = sqlwere + " a.[EstadoID] = " + pqrs.estado.ToString() + " AND ";
			} else
			{
				sqlwere = sqlwere + " a.[EstadoID] != 11 AND ";
			}
			if (pqrs.TipoPQRSId > 0)
				sqlwere = sqlwere + " a.[TipoPQRSId] = " + pqrs.TipoPQRSId.ToString() + " AND ";
			if (EstadosDisponibles != null && EstadosDisponibles.Count() > 0)
				sqlwere = sqlwere + " a.EstadoID IN (" + String.Join(",", EstadosDisponibles) + ") AND ";
			sql = sql + sqlwere + " CONVERT(DATE,[FechaCreacion]) >= CONVERT(DATE,'" + pqrs.desde.ToString("yyyy-MM-dd") + "',120) and CONVERT(DATE,[FechaCreacion]) <= CONVERT(DATE,'" + pqrs.hasta.ToString("yyyy-MM-dd") + "',120) ";
			sql += @" GROUP BY a.IdPQRS, a.NroOrden, b.Descripcion, Detalle, NombreRespuesta, DireccionRespuesta, 
						  EmailRespuesta, TelefonoRespuesta, FechaCreacion, UsuarioCreacion, c.Descripcion,
						  EstadoID, TipoPQRSId, IdOrdenProcedente, pt.Descripcion, IdCliente, OtroCliente, 
						  CL.cli_nombre, pai_nombre, c.Color, c.ColorClase, a.IdOrden, og.Tipo_Of, og.Num_Of, og.Ano_Of, tf.Descripcion, a.OrdenGarantiaOMejora, a.EsProcedente, a.DescripcionNoProcedente, a.PuedeSerCerrada ";

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSDTOConsulta> lst = dt.AsEnumerable()
				.Select(row => new PQRSDTOConsulta
				{
					IdPQRS = (int)row["IdPQRS"],
					NroOrden = row["NroOrden"] == System.DBNull.Value ? string.Empty : (string)row["NroOrden"],
					Fuente = row["Fuente"] == System.DBNull.Value ? string.Empty : (string)row["Fuente"],
					Detalle = (string)row["Detalle"],
					NombreRespuesta = (string)row["NombreRespuesta"],
					DireccionRespuesta = (string)row["DireccionRespuesta"],
					EmailRespuesta = (string)row["EmailRespuesta"],
					TelefonoRespuesta = (string)row["TelefonoRespuesta"],
					FechaCreacion = (DateTime)row["FechaCreacion"],
					UsuarioCreacion = (string)row["UsuarioCreacion"],
					Estado = (string)row["Estado"],
					EstadoID = (int)row["EstadoID"],
					Responde = (int)row["Responde"],
					TipoPQRS = (string)row["TipoPQRS"],
					TipoPQRSId = (int)row["TipoPQRSId"],
					OrdenProcedente = row["OrdenProcedente"] == System.DBNull.Value ? string.Empty : (string)row["OrdenProcedente"],
					Cliente = (string)row["Cliente"],
					Pais = (string)row["Pais"],
					ColorEstado = row["ColorEstado"] == System.DBNull.Value ? string.Empty : (string)row["ColorEstado"],
					IdOrdenOrigen = row["IdOrdenOrigen"] == System.DBNull.Value ? 0 : (int)row["IdOrdenOrigen"],
					ColorClase = (string)row["ColorClase"],
					CantidadComunicados = (int)row["CantidadComunicados"],
					Semaforo = (string)row["Semaforo"],
					IsInvolucred = (int)row["IsInvolucred"] == 1 ? true : false,
					TipoFuente = row["TipoFuente"] == System.DBNull.Value ? string.Empty : (string)row["TipoFuente"],
					OrdenGarantiaOMejora = (string)row["OrdenGarantiaOMejora"],
					EsProcedenteDescripcion = row["EsProcedenteDescripcion"] == System.DBNull.Value ? string.Empty : (string)row["EsProcedenteDescripcion"],
					DescripcionProcedencia = row["DescripcionProcedencia"] == System.DBNull.Value ? string.Empty : (string)row["DescripcionProcedencia"],
					SeEnvioEncuesta = row["SeEnvioEncuesta"] == System.DBNull.Value ? 0 : (int)row["SeEnvioEncuesta"],
					PuedeSerCerrada = (bool)row["PuedeSerCerrada"],
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}

		public List<PQRSPlanta> ObtenerPlantas()
		{
			string sql = @"SELECT planta_id, planta_descripcion
							FROM planta_forsa
							WHERE planta_activo = 1
							  AND planta_id > 0";

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSPlanta> lst = dt.AsEnumerable()
				.Select(row => new PQRSPlanta
				{
					Id = (int)row["planta_id"],
					Descripcion = (string)row["planta_descripcion"],
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}

		public List<PQRSProceso> ObtenerProcesos(string tipopqrs)
		{
			string sql = @"SELECT  [Proceso] ,[EmailProceso], [EmailProcesoCC] 
						  FROM [dbo].[PQRSProcesoAdmin]
							WHERE TipoPQRSId = '" + tipopqrs + "'";

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSProceso> lst = dt.AsEnumerable()
				.Select(row => new PQRSProceso
				{
					Proceso = (string)row["Proceso"],
					EmailProceso = (string)row["EmailProceso"],
					Seleccionado = false,
					EmailProcesoCC = row["EmailProcesoCC"] == DBNull.Value ? "" : (string)row["EmailProcesoCC"]
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}

		public List<TipoNC> ObtenerTIPONC()
		{
			string sql = @"SELECT pnc_cod_id, pnc_cod_nc, pnc_cod_desc, pnc_cod_proc
							FROM pnc_cod";

			DataTable dt = BdDatos.CargarTabla(sql);
			List<TipoNC> lst = dt.AsEnumerable()
				.Select(row => new TipoNC
				{
					Id = (int)row["pnc_cod_id"],
					Descripcion = (string)row["pnc_cod_nc"] + " - " + (string)row["pnc_cod_desc"],
					Proc = (int)row["pnc_cod_proc"],
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}

		public void GuardarEmailsListadosPlanosRequeridos(int idPQRS, string emailsString, int tipo, int tipoMaterial)
		{
			string[] emailsArray = emailsString.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

			string conexion = BdDatos.conexionScope();
			string sql = "";
			string material = "";

			if(tipo == 1)
			{
				sql = @"INSERT INTO PQRSListados(IdPQRS, Correo, Completos, Tipo)
							 VALUES(@IdPQRS, @Email, 0, @TipoMaterial)";
			} else if(tipo == 2)
			{
				sql = @"INSERT INTO PQRSPlanos(IdPQRS, Correo, Completos, Tipo)
							 VALUES(@IdPQRS, @Email, 0, @TipoMaterial)";
			} else if(tipo == 3)
            {
				sql = @"INSERT INTO PQRSArmado(IdPQRS, Correo, Completos, Tipo)
							 VALUES(@IdPQRS, @Email, 0, @TipoMaterial)";
			}

			if(tipoMaterial == 1)
			{
				material = "Acero";
			} else if (tipoMaterial == 2)
			{
				material = "Aluminio";
			}
			
			using (SqlConnection con = new SqlConnection(conexion))
			{
				con.Open();
				SqlTransaction transaction = con.BeginTransaction();
				try
				{
					// Creamos el Comando
					foreach (string email in emailsArray)
					{
						SqlParameter[] sqls = new SqlParameter[3];
						sqls[0] = new SqlParameter("@IdPQRS ", idPQRS);
						sqls[1] = new SqlParameter("@Email", email);
						sqls[2] = new SqlParameter("@TipoMaterial", material);

						using (SqlCommand cmd = new SqlCommand(sql, con))
						{
							cmd.CommandType = CommandType.Text;
							cmd.Parameters.AddRange(sqls);
							cmd.Transaction = transaction;
							cmd.ExecuteNonQuery();
						}
					}

					transaction.Commit();
				}
				catch
				{
					transaction.Rollback();
					throw;
				}
			}
		}

		public void ClosePQRS(int pqrsId, string usuario, string email)
        {
			string conexion = BdDatos.conexionScope();
			PQRSDTOConsulta pqrs = this.ObtenerPQRSId(pqrsId.ToString(), email);
			using (SqlConnection con = new SqlConnection(conexion))
			{
				con.Open();
				SqlTransaction transaction = con.BeginTransaction();
				try
				{
					this.SaveLog(con, transaction, pqrsId, usuario, pqrs.EstadoID, (int)EstadosPQRS.Final);
					this.UpdateEstadoPQRS(con, transaction, pqrsId, (int)EstadosPQRS.Final);
					transaction.Commit();
				}
				catch(Exception e)
				{
					transaction.Rollback();
					Console.WriteLine(e.Message);
				}

			}
		}

		public bool GuardarProcesos(string usuario, PQRSProcesoSave procesos)
		{
			List<PQRSProceso> ListSaveProcesos = new List<PQRSProceso>();
			foreach (var proc in procesos.procesos)
			{
				string[] emails = proc.EmailProceso.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string email in emails)
				{
					ListSaveProcesos.Add(new PQRSProceso()
					{
						EmailProceso = email.Trim(),
						Proceso = proc.Proceso,
					});
				}
			}

			string conexion = BdDatos.conexionScope();

			string sql = @"INSERT INTO PQRSProceso(PQRSId, Proceso, Email)
							 VALUES(@PQRSId, @Proceso, @Email)";

			using (SqlConnection con = new SqlConnection(conexion))
			{
				con.Open();
				SqlTransaction transaction = con.BeginTransaction();
				try
				{
					// Creamos el Comando
					foreach (var proc in ListSaveProcesos)
					{
						SqlParameter[] sqls = new SqlParameter[3];
						sqls[0] = new SqlParameter("@PQRSId ", procesos.PQRSId);
						sqls[1] = new SqlParameter("@Proceso", proc.Proceso);
						sqls[2] = new SqlParameter("@Email", proc.EmailProceso);
						using (SqlCommand cmd = new SqlCommand(sql, con))
						{
							cmd.CommandType = CommandType.Text;
							cmd.Parameters.AddRange(sqls);
							cmd.Transaction = transaction;
							cmd.ExecuteNonQuery();
						}
					}

					SaveLog(con, transaction, procesos.PQRSId, usuario, (int)EstadosPQRS.Radicado, (int)EstadosPQRS.Asignacion);
					UpdateEstadoPQRS(con, transaction, procesos.PQRSId, (int)EstadosPQRS.Asignacion);
					transaction.Commit();
				}
				catch
				{
					transaction.Rollback();
					throw;
				}
			}

			return true;
		}

		public List<PQRSListadosPlanos> ObtenerListadosPlanos(string idpqrs, string email)
		{
			string sql = @"SELECT *,
								CASE
									WHEN tabla = 'tabla1' THEN 'Listados'
									WHEN tabla = 'tabla2' THEN 'Planos'
									WHEN tabla = 'tabla3' THEN 'Armado'
									ELSE 'N/A'
								END AS TipoCargue,
								CASE
									WHEN Correo LIKE  '" + email + @"' THEN 1
									ELSE 0
								END AS PuedeEditar
							FROM (
								SELECT *, 'tabla1' AS tabla FROM PQRSListados
								UNION ALL 
								SELECT *, 'tabla2' AS tabla FROM PQRSPlanos
								UNION ALL 
								SELECT *, 'tabla3' AS tabla FROM PQRSArmado
								) AS union_tablas
							WHERE IdPQRS = " + idpqrs;

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSListadosPlanos> lst = dt.AsEnumerable()
				.Select(row => new PQRSListadosPlanos
				{
					Id = (int)row["Id"],
					IdPQRS = (int)row["IdPQRS"],
					Correo = (string)row["Correo"],
					Completos = (bool)row["Completos"],
					Tipo = (string)row["Tipo"],
					TipoCargue = (string)row["TipoCargue"],
					PuedeEditar = (int)row["PuedeEditar"] == 0 ? false : true,
					Comentario = row["Comentario"] == DBNull.Value ? "" : (string)row["Comentario"],
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}

		public List<PQRSProceso> ObtenerProcesosAsignados(string IDPQRS)
		{
			string sql = @"SELECT  [Proceso] ,[Email]
						  FROM [dbo].[PQRSProceso]
							WHERE PQRSId = " + IDPQRS;

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSProceso> lst = dt.AsEnumerable()
				.Select(row => new PQRSProceso
				{
					Proceso = (string)row["Proceso"],
					EmailProceso = (string)row["Email"],
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}

		public List<PQRSProcesoAsignado> ObtenerProcesosAsignadosEmail(string IDPQRS, string email)
		{
			string sql = @"SELECT a.[PQRSProcesoId]
							  ,a.[PQRSId]
							  ,a.[Proceso]
							  ,a.[Email]
							  ,c.InformacionAclaracion
						  FROM [dbo].[PQRSProceso] AS a
						  LEFT JOIN [dbo].[PQRSProceso] AS b ON b.PQRSProcesoId = a.PQRSProcesoIdPadre 
						  LEFT JOIN [dbo].[PQRSRespuesta] AS c ON c.IdPQRSProceso = b.PQRSProcesoId
						  WHERE a.PQRSId = " + IDPQRS + " AND  a.Email LIKE '%" + email + @"%'
							AND a.PQRSProcesoId NOT IN (
														SELECT IdPQRSProceso
														FROM PQRSRespuesta
														WHERE IdPQRS = " + IDPQRS + " )";

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSProcesoAsignado> lst = dt.AsEnumerable()
				.Select(row => new PQRSProcesoAsignado
				{
					Proceso = (string)row["Proceso"],
					EmailProceso = (string)row["Email"],
					PQRSProcesoId = (int)row["PQRSProcesoId"],
					InformacionAclaracion = row["InformacionAclaracion"] == System.DBNull.Value ? null : (string)row["InformacionAclaracion"],
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}


		public void GuardarEstadoProcesoRespuesta(int id, string email, string usuario, Comunicado comunicado)
		{
			PQRSDTOConsulta pqrs = this.ObtenerPQRSId(id.ToString(), email);
			
				string conexion = BdDatos.conexionScope();
				DateTime fechaActual = DateTime.Now;

				SqlParameter[] sqls = new SqlParameter[10];
				sqls[0] = new SqlParameter("@IdPQRS", id);
				sqls[1] = new SqlParameter("@MessageTo", comunicado.Para);
				sqls[2] = new SqlParameter("@Asunto", comunicado.Asunto);
				sqls[3] = new SqlParameter("@Mensaje", comunicado.Mensaje);
				sqls[4] = new SqlParameter("@Fecha", fechaActual);
				sqls[5] = new SqlParameter("@Usuario", usuario);
				sqls[6] = new SqlParameter("@FilePATH", string.Empty);
				sqls[7] = new SqlParameter("@FileName", string.Empty);
				sqls[8] = new SqlParameter("@MessageCc", comunicado.Cc ?? "");
				sqls[9] = new SqlParameter("@SeEnvioEncuesta", comunicado.incluirEncuesta);

				string sql = @"INSERT INTO PQRSComunicado([IdPQRS],[MessageTo],[Asunto],[Mensaje],[Fecha],[Usuario],[FilePATH],[FileName],[MessageCc],[SeEnvioEncuesta])
							 VALUES(@IdPQRS,@MessageTo,@Asunto,@Mensaje,@Fecha,@Usuario,@FilePATH,@FileName,@MessageCc,@SeEnvioEncuesta);";

				// Creamos la conexión
				using (SqlConnection con = new SqlConnection(conexion))
				{
					// Abrimos la conexión y ejecutamos el ExecuteReader
					using (SqlCommand cmd = new SqlCommand(sql, con))
					{
						cmd.CommandType = CommandType.Text;
						cmd.Parameters.AddRange(sqls);
						con.Open();
						SqlTransaction transaction = con.BeginTransaction();
						cmd.Transaction = transaction;
						try
						{
							cmd.ExecuteNonQuery();
							
							if(pqrs.TipoPQRSId != 2 || (pqrs.TipoPQRSId == 2 && !(bool)pqrs.EsProcedente && pqrs.EstadoID >= 4))
							{
								sql = "UPDATE dbo.PQRS SET PuedeSerCerrada = 1 WHERE IdPQRS = @PuedeSerCerrada";
								SqlParameter sqlParameter = new SqlParameter("@PuedeSerCerrada", id);
								using (SqlCommand cmd2 = new SqlCommand(sql, con))
								{
									cmd2.CommandType = CommandType.Text;
									cmd2.Parameters.Add(sqlParameter);
									cmd2.Transaction = transaction;
									cmd2.ExecuteNonQuery();
								}
							}
							
							transaction.Commit();
						}
						catch (Exception e)
						{
							transaction.Rollback();

						}
					}
				}
			
		}

		public void ModifyDeliveryDate(int pqrsId, DateTime deliveryDate)
		{
			string sql = @"UPDATE PQRS SET FechaEntObra = @FechaEntregaObra WHERE IdPQRS = @IdPQRS";
			SqlParameter[] sqls = new SqlParameter[2];
			sqls[0] = new SqlParameter("@IdPQRS ", pqrsId);
			sqls[1] = new SqlParameter("@FechaEntregaObra", deliveryDate);

			string conexion = BdDatos.conexionScope();
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddRange(sqls);

					// Abrimos la conexión y ejecutamos el ExecuteReader
					con.Open();
					SqlTransaction transaction = con.BeginTransaction();
					cmd.Transaction = transaction;

					try
                    {
						cmd.ExecuteScalar();
						transaction.Commit();
					}
					catch (Exception e)
					{
						transaction.Rollback();
					}
					finally
					{
						con.Close();
					}
				}

			}

		}

		public void VerifyResponsesAndProcessAmounts(int pqrsId)
        {
			string conexion = BdDatos.conexionScope();
			using (SqlConnection con = new SqlConnection(conexion))
			{
				con.Open();
				SqlTransaction transaction = con.BeginTransaction();
				try
				{
					int cuentaRespuesta = ObtenerCuentaRespuesta(con, transaction, pqrsId.ToString());
					List<PQRSProceso> listaProcesos = ObtenerProcesosAsignados(pqrsId.ToString());
					if (listaProcesos.Count == cuentaRespuesta)
					{
						int EstadoACambiar = (int)EstadosPQRS.RespuestaProceso;
						UpdateEstadoPQRS(con, transaction, pqrsId, EstadoACambiar);
					}

					transaction.Commit();
				}
				catch (Exception e)
				{
					transaction.Rollback();
				}
				finally
                {
					con.Close();
                }
			}
		}

		public Dictionary<string, int> GuadarRespuestaProceso(PQRSRespuesta respuesta, string email)
		{
			string conexion = BdDatos.conexionScope();
			int Id = 0;
			int estadoActual = 0;

			//  obtener el proceso ocorde al mail del usaurio logueado para ir a la tabla de procesos a traer el id con este mail
			List<PQRSProceso> listaProcesos = ObtenerProcesosAsignados(respuesta.PQRSId.ToString());
			PQRSDTOConsulta pqrs = this.ObtenerPQRSId(respuesta.PQRSId.ToString(), email);
			estadoActual = pqrs.EstadoID;

			DateTime fechaActual = DateTime.Now;
			SqlParameter[] sqls = new SqlParameter[6];
			sqls[0] = new SqlParameter("@IdPQRS ", respuesta.PQRSId);
			sqls[1] = new SqlParameter("@IdPQRSProceso", respuesta.PQRSIdproceso);
			sqls[2] = new SqlParameter("@Respuesta", respuesta.Mensaje);
			sqls[3] = new SqlParameter("@Usuario", respuesta.Usuario);
			sqls[4] = new SqlParameter("@Fecha", fechaActual);
			sqls[5] = new SqlParameter("@IdPQRSProcesoForSearchFather", respuesta.PQRSIdproceso);

			string sql = @"INSERT INTO PQRSRespuesta(IdPQRS, IdPQRSProceso, Respuesta, Usuario, Fecha, IdPadre)
							 VALUES(@IdPQRS, @IdPQRSProceso, @Respuesta, @Usuario,@Fecha,
							   (SELECT a.IdPQRSRespuesta
								  FROM PQRSProceso AS b
								  INNER JOIN PQRSProceso AS c ON c.PQRSProcesoId = b.PQRSProcesoId
								  INNER JOIN PQRSRespuesta AS a ON a.IdPQRSProceso = c.PQRSProcesoIdPadre
								  WHERE b.PQRSProcesoId = @IdPQRSProcesoForSearchFather));
						   ;SELECT  SCOPE_IDENTITY()";

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddRange(sqls);

					// Abrimos la conexión y ejecutamos el ExecuteReader
					con.Open();
					SqlTransaction transaction = con.BeginTransaction();
					cmd.Transaction = transaction;

					try
					{
						Id = Convert.ToInt32(cmd.ExecuteScalar());

						if (respuesta.archivos != null && respuesta.archivos.Count > 0)
						{
							foreach (var file in respuesta.archivos)
							{
								string filePath = SaveFilePath(respuesta.PQRSId, file.base64, file.nameFile, "RespuestaProceso");
								GuardarArchivosRespuesta(con, transaction, Id, file.nameFile, filePath, respuesta.Usuario);
							}
						}

						SaveLog(con, transaction, respuesta.PQRSId, respuesta.Usuario, (int)EstadosPQRS.Asignacion, (int)EstadosPQRS.RespuestaProceso, fechaActual);

						int cuentaRespuesta = ObtenerCuentaRespuesta(con, transaction, respuesta.PQRSId.ToString());

						if (listaProcesos.Count == cuentaRespuesta)
						{
							int EstadoACambiar = (int)EstadosPQRS.RespuestaProceso;
							//if (pqrs.TipoPQRSId != (int)TipoPQRS.Reclamo)
							//{
							//    EstadoACambiar = (int)EstadosPQRS.RespuestaCliente;
							//}
							UpdateEstadoPQRS(con, transaction, respuesta.PQRSId, EstadoACambiar);
						}

						transaction.Commit();
					}
					catch (Exception e)
					{
						transaction.Rollback();

					}
				}
			}
			Dictionary<string, int> response = new Dictionary<string, int>();
			response.Add("Id", Id);
			response.Add("estadoActual", estadoActual);
			return response;

		}

		private int ObtenerCuentaRespuesta(SqlConnection con, SqlTransaction transaction, string IdPQRS)
		{
			int Id = 0;
			string sql = @"SELECT COUNT(*) FROM PQRSRespuesta WHERE IdPQRS = " + IdPQRS;

			using (SqlCommand cmd = new SqlCommand(sql, con))
			{
				cmd.CommandType = CommandType.Text;

				// Abrimos la conexión y ejecutamos el ExecuteReader
				cmd.Transaction = transaction;
				Id = Convert.ToInt32(cmd.ExecuteScalar());
			}

			return Id;
		}

		private void GuardarArchivosRespuesta(SqlConnection con, SqlTransaction transaction, int PQRSId, string nameFile, string pathFile, string usuario)
		{
			SqlParameter[] sqls = new SqlParameter[4];
			sqls[0] = new SqlParameter("@IdPQRSRespuesta ", PQRSId);
			sqls[1] = new SqlParameter("@FilePATH", pathFile);
			sqls[2] = new SqlParameter("@FileName", nameFile);
			sqls[3] = new SqlParameter("@Usuario", usuario);

			string sql = @"INSERT INTO PQRSRespuestaArchivos(IdPQRSRespuesta, FilePATH, FileName, UsuarioArchivo)
							 VALUES(@IdPQRSRespuesta, @FilePATH, @FileName, @Usuario);
						   ";

			using (SqlCommand cmd = new SqlCommand(sql, con))
			{
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddRange(sqls);

				// Abrimos la conexión y ejecutamos el ExecuteReader
				cmd.Transaction = transaction;
				cmd.ExecuteNonQuery();
			}
		}

		public PQRSRespuestaHistorico ObtenerRespuestasProcesos(string IdLog)
		{
			string sql = @"SELECT res.Respuesta, res.IdPQRSRespuesta, pproc.Proceso
							FROM PQRSLog lp
							INNER JOIN PQRSRespuesta res ON res.IdPQRS = lp.PQRSId
											  AND  lp.Usuario = res.Usuario AND FORMAT( res.Fecha, 'ddMMyyyyHHmmss') =  FORMAT( lp.Fecha, 'ddMMyyyyHHmmss')
							INNER JOIN  PQRSProceso pproc on pproc.PQRSProcesoId = res.IdPQRSProceso
							WHERE lp.PQRSLogId = " + IdLog;

			DataTable dt = BdDatos.CargarTabla(sql);
			PQRSRespuestaHistorico lst = dt.AsEnumerable()
				.Select(row => new PQRSRespuestaHistorico
				{
					Mensaje = (string)row["Respuesta"],
					Id = (int)row["IdPQRSRespuesta"],
					Proceso = (string)row["Proceso"],
				}).FirstOrDefault();
			dt.Clear();
			dt.Dispose();

			return lst;
		}
		public List<PQRSDTOArchivo> ObtenerPRQSRespuestaArchivo(string idRespuesta)
		{

			string sql = @"SELECT [IdPQRSRespuestaArchivos]  ,[IdPQRSRespuesta]
							  ,[FilePATH]  ,[FileName]
						  FROM [forsa].[dbo].[PQRSRespuestaArchivos]
							where IdPQRSRespuesta = " + idRespuesta;

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSDTOArchivo> lst = dt.AsEnumerable()
				.Select(row => new PQRSDTOArchivo
				{
					Id = (int)row["IdPQRSRespuestaArchivos"],
					PQRS = (int)row["IdPQRSRespuesta"],
					FilePATH = (string)row["FilePATH"],
					FileName = (string)row["FileName"],
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}
		private void UpdateProcedencia(SqlConnection con, SqlTransaction transaction, pqrsProcedente pqrsprocedente)
		{


			SqlParameter[] sqls = new SqlParameter[3];
			sqls[0] = new SqlParameter("@IdPQRS ", pqrsprocedente.IdPQRS);
			sqls[1] = new SqlParameter("@EsProcedente", pqrsprocedente.EsProcedente);
			sqls[2] = new SqlParameter("@DescripcionNoProcedente", string.IsNullOrEmpty(pqrsprocedente.DescripcionNoProcedente) ? "" : pqrsprocedente.DescripcionNoProcedente);

			string sql = @"UPDATE  PQRS SET EsProcedente = @EsProcedente, 
											DescripcionNoProcedente = @DescripcionNoProcedente 
											WHERE IdPQRS =  @IdPQRS; ";

			using (SqlCommand cmd = new SqlCommand(sql, con))
			{

				try
				{
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddRange(sqls);

					// Abrimos la conexión y ejecutamos el ExecuteReader
					cmd.Transaction = transaction;
					cmd.ExecuteNonQuery();
				}
				catch (Exception e)
				{
					throw;
				}
			}



		}

		public int GenerarOrden(string usuario, pqrsGenerarOrden pqrsprocedente)
		{
			int idNuevoEstadoResponse = 0;
			string conexion = BdDatos.conexionScope();
			PQRSDTO pqrsOrden = new PQRSDTO();
			string sqlPQRS = @"SELECT IDFUP,IDORDEN,[VERSION] FROM PQRS WHERE IdPQRS=" + pqrsprocedente.IdPQRS;
			DataTable dt = BdDatos.CargarTabla(sqlPQRS);
			pqrsOrden = dt.AsEnumerable()
			.Select(row => new PQRSDTO
			{
				IdFup = (int)row["IDFUP"],
				IdOrden = (int)row["IDORDEN"],
				Version = (string)row["VERSION"],
			}).FirstOrDefault();
			dt.Clear();
			dt.Dispose();

			using (SqlConnection con = new SqlConnection(conexion))
			{
				con.Open();
				SqlTransaction transaction = con.BeginTransaction();
				try
				{
					
						if (pqrsprocedente.Idordenprocedente != null)
						{
							GestionOrdenPQRS(con, transaction, pqrsprocedente, pqrsOrden); // guarda o actualiza la ordenprocedencia
							
						}
						else
						{
							Dictionary<string, object> parametros = new Dictionary<string, object>();
							parametros.Add("@pFupID", pqrsOrden.IdFup);
							parametros.Add("@pversion", pqrsOrden.Version);
							parametros.Add("@pIdOrdenOrigen", pqrsOrden.IdOrden);
							parametros.Add("@pTipo", pqrsprocedente.OrdenGarantiaOMejora);
							parametros.Add("@pUsuario", usuario);
							parametros.Add("@pPlantaId", pqrsprocedente.IdPlanta);
							parametros.Add("@pPqrsId", pqrsprocedente.IdPQRS);
							int result = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_INS_Orden_GM", parametros);
							int IdOrdenProcedenteCreada = result;
							if (IdOrdenProcedenteCreada > 0)
							{
								pqrsprocedente.Idordenprocedente = IdOrdenProcedenteCreada.ToString();
								GestionOrdenPQRS(con, transaction, pqrsprocedente, pqrsOrden); // guarda o actualiza la ordenprocedencia
							}
							else
							{
								//throw new Exception("Se generó un error");
								GestionOrdenPQRS(con, transaction, pqrsprocedente, pqrsOrden); // guarda o actualiza la ordenprocedencia
							}
							parametros.Clear();
						}

						if (pqrsprocedente.RequierelistadosAcero)
						{
							GuardarEmailsListadosPlanosRequeridos(pqrsprocedente.IdPQRS, pqrsprocedente.RequierelistadosAceroCorreos, 1, 1);
						}
						if (pqrsprocedente.RequierelistadosAluminio)
						{
							GuardarEmailsListadosPlanosRequeridos(pqrsprocedente.IdPQRS, pqrsprocedente.RequierelistadosAluminioCorreos, 1, 2);
						}
						if (pqrsprocedente.RequiereplanosAcero)
						{
							GuardarEmailsListadosPlanosRequeridos(pqrsprocedente.IdPQRS, pqrsprocedente.RequiereplanosAceroCorreos, 2, 1);
						}
						if (pqrsprocedente.RequiereplanosAluminio)
						{
							GuardarEmailsListadosPlanosRequeridos(pqrsprocedente.IdPQRS, pqrsprocedente.RequiereplanosAluminioCorreos, 2, 2);
						}
						if (pqrsprocedente.RequierearmadoAcero)
						{
							GuardarEmailsListadosPlanosRequeridos(pqrsprocedente.IdPQRS, pqrsprocedente.RequierearmadoAceroCorreos, 3, 1);
						}
						if (pqrsprocedente.RequierearmadoAluminio)
						{
							GuardarEmailsListadosPlanosRequeridos(pqrsprocedente.IdPQRS, pqrsprocedente.RequierearmadoAluminioCorreos, 3, 2);
						}

					if (pqrsprocedente.solucionadoEnObra)
						{
							SaveLog(con, transaction, pqrsprocedente.IdPQRS, usuario, (int)EstadosPQRS.ReclamoProcedente, (int)EstadosPQRS.SolucionadoEnObra);
							UpdateEstadoPQRS(con, transaction, pqrsprocedente.IdPQRS, (int)EstadosPQRS.SolucionadoEnObra);
							idNuevoEstadoResponse = (int)EstadosPQRS.SolucionadoEnObra;
						} else
						{
							if (pqrsprocedente.RequierelistadosAcero || pqrsprocedente.RequierelistadosAluminio
								|| pqrsprocedente.RequiereplanosAcero || pqrsprocedente.RequiereplanosAluminio
								|| pqrsprocedente.RequierearmadoAcero || pqrsprocedente.RequierearmadoAluminio)
							{
								SaveLog(con, transaction, pqrsprocedente.IdPQRS, usuario, (int)EstadosPQRS.ReclamoProcedente, (int)EstadosPQRS.Ingenieria);
								UpdateEstadoPQRS(con, transaction, pqrsprocedente.IdPQRS, (int)EstadosPQRS.Ingenieria);
								idNuevoEstadoResponse = (int)EstadosPQRS.Ingenieria;
							}
							else
							{
								string sql = "UPDATE dbo.PQRS SET PuedeSerCerrada = 1 WHERE IdPQRS = @PuedeSerCerrada";
								SqlParameter sqlParameter = new SqlParameter("@PuedeSerCerrada", pqrsprocedente.IdPQRS);
								using (SqlCommand cmd2 = new SqlCommand(sql, con))
								{
									cmd2.CommandType = CommandType.Text;
									cmd2.Parameters.Add(sqlParameter);
									cmd2.Transaction = transaction;
									cmd2.ExecuteNonQuery();
								}
								idNuevoEstadoResponse = (int)EstadosPQRS.CargarListado;
							}
						}
						

					transaction.Commit();
				}
				catch (Exception e)
				{
					transaction.Rollback();
					throw;
				}
			}
			return idNuevoEstadoResponse;
		}

		private void ActualizarHallazgosNoProcedentes(string hallazgosNoProcedentes)
		{
			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("@pIdsHallazgos", hallazgosNoProcedentes);
			ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_PQRSHallazgosNoProcedentes", parametros);
		}

        public void GuardarProcesosAdicionalesProcedencia(int idPqrs, string usuario, PQRSProceso[] Procesos)
        {
            string conexion = BdDatos.conexionScope();
            string sql = @"INSERT INTO PQRSNoConformidades(PQRSId, Proceso, PncCodID, Email, Comentario, UsuarioCrea, FechaCrea)
							 VALUES(@PQRSId, @Proceso, @PncCodID, @Email, @Comentario, @Usuario, GETDATE())";
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    foreach (var proc in Procesos)
                    {
                        SqlParameter[] sqls = new SqlParameter[6];
                        sqls[0] = new SqlParameter("@PQRSId ", idPqrs);
                        sqls[1] = new SqlParameter("@Proceso", proc.Proceso);
                        sqls[2] = new SqlParameter("@PncCodID", proc.TipoNC);
                        sqls[3] = new SqlParameter("@Email", proc.EmailProceso);
                        sqls[4] = new SqlParameter("@Comentario", proc.Comentario);
                        sqls[5] = new SqlParameter("@Usuario", usuario);

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddRange(sqls);
                            cmd.Transaction = transaction;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

		public void GuardarReclamoProcedente(string usuario, pqrsProcedente pqrsprocedente, string hallazgosNoProcedentes)
		{
			PQRSDTO pqrsOrden = new PQRSDTO();
			string conexion = BdDatos.conexionScope();
			string sql = @"INSERT INTO PQRSNoConformidades(PQRSId, Proceso, PncCodID, Email, Comentario, UsuarioCrea, FechaCrea)
							 VALUES(@PQRSId, @Proceso, @PncCodID, @Email, @Comentario, @Usuario, GETDATE())";

			ActualizarHallazgosNoProcedentes(hallazgosNoProcedentes);

			using (SqlConnection con = new SqlConnection(conexion))
			{
				con.Open();
				SqlTransaction transaction = con.BeginTransaction();
				try
				{
					EstadosPQRS proximoEstado = pqrsprocedente.EsProcedente ? EstadosPQRS.ReclamoProcedente : EstadosPQRS.NoProcedente;
					SaveLog(con, transaction, pqrsprocedente.IdPQRS, usuario, (int)EstadosPQRS.RespuestaProceso, (int)proximoEstado);
					UpdateEstadoPQRS(con, transaction, pqrsprocedente.IdPQRS, (int)proximoEstado);
					UpdateProcedencia(con, transaction, pqrsprocedente);

					foreach (var proc in pqrsprocedente.Procesos)
					{
						SqlParameter[] sqls = new SqlParameter[6];
						sqls[0] = new SqlParameter("@PQRSId ", pqrsprocedente.IdPQRS);
						sqls[1] = new SqlParameter("@Proceso", proc.Proceso);
						sqls[2] = new SqlParameter("@PncCodID", proc.TipoNC);
						sqls[3] = new SqlParameter("@Email", proc.EmailProceso);
						sqls[4] = new SqlParameter("@Comentario", proc.Comentario);
						sqls[5] = new SqlParameter("@Usuario", usuario);

						using (SqlCommand cmd = new SqlCommand(sql, con))
						{
							cmd.CommandType = CommandType.Text;
							cmd.Parameters.AddRange(sqls);
							cmd.Transaction = transaction;
							cmd.ExecuteNonQuery();
						}
					}

					transaction.Commit();
				}
				catch (Exception e)
				{
					transaction.Rollback();
					throw;
				}
			}
		}
		public List<PQRSNoConformidades> ObtenerNCHistorico(string idPQRS)
		{
			string sql = @"SELECT pnc.Proceso,
							   pcod.pnc_cod_nc + ' - ' + pcod.pnc_cod_desc as Descripcion
						  FROM [PQRSNoConformidades] pnc
						  INNER JOIN pnc_cod pcod on pcod.pnc_cod_id = pnc.PncCodID
						  WHERE pnc.PQRSId = " + idPQRS;

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSNoConformidades> lst = dt.AsEnumerable()
				.Select(row => new PQRSNoConformidades
				{
					Proceso = (string)row["Proceso"],
					TipoNC = (string)row["Descripcion"],
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}
		public pqrsProcedenteHistorico ObtenerHistoricoProcedente(string idPQRS)
		{
			string sql = @"SELECT [EsProcedente],[IdPQRS],[DescripcionNoProcedente],
						  [IdOrdenProcedente], [OrdenGarantiaOMejora]  
						  FROM [PQRS]
						  WHERE [IdPQRS] = " + idPQRS;

			DataTable dt = BdDatos.CargarTabla(sql);
			pqrsProcedenteHistorico lst = dt.AsEnumerable()
				.Select(row => new pqrsProcedenteHistorico
				{
					IdPQRS = (int)row["IdPQRS"],
					EsProcedente = (bool)row["EsProcedente"],
					Descripcion = (string)row["DescripcionNoProcedente"],
					IdOrdenProcedente = Convert.IsDBNull(row["IdOrdenProcedente"]) ? 0 : (int)row["IdOrdenProcedente"],
					OrdenGarantiaOMejora = Convert.IsDBNull(row["OrdenGarantiaOMejora"]) ? "" : (string)row["OrdenGarantiaOMejora"]
				}).FirstOrDefault();
			dt.Clear();
			dt.Dispose();

			List<PQRSNoConformidades> listaNC = new List<PQRSNoConformidades>();

			if (lst.EsProcedente)
			{
				listaNC = this.ObtenerNCHistorico(idPQRS);
			}

			lst.SEsProcedente = lst.EsProcedente ? "SI" : "NO";
			lst.DescripcionOrden = (lst.OrdenGarantiaOMejora != "" ? " Tipo de Orden: " + (lst.OrdenGarantiaOMejora == "OG" ? "Orden de Garantia" : "Orden de Mejora") : " Id Orden Procedente: " + lst.IdOrdenProcedente.ToString());
			lst.procesos = listaNC;

			return lst;
		}
		public List<PQRSDTORDEN> ObtenerOrdenesActivas(string IdPqrs)
		{

			string sql = @"SELECT DISTINCT a.Id_Ofa ,RTRIM(a.Tipo_Of)+'-'+ RTRIM(CONVERT(VARCHAR(10),a.Numero))+'-'+a.ano orden, 
							isnull(os.fup,0) fup   
						FROM ORDEN a 
							INNER JOIN Orden_Seg os on os.Id_Ofa = a.Id_Ofa
							INNER JOIN PQRS P ON p.IdOrden = a.Id_Of_P
						WHERE a.Tipo_Of IN ('OG','OM') AND os.Anulado = 0 AND os.fecha_despacho is null 
						  AND IdPQRS = " + IdPqrs ;

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSDTORDEN> lst = dt.AsEnumerable()
				.Select(row => new PQRSDTORDEN
				{
					IdOfa = (int)row["Id_Ofa"],
					Orden = (string)row["orden"],
					Fup = row["fup"] == System.DBNull.Value ? default(int?) : (int)row["fup"],
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}
		private void GestionOrdenPQRS(SqlConnection con, SqlTransaction transaction, pqrsGenerarOrden pqrpqrsprocedentes, PQRSDTO pqrs)
		{
			SqlParameter[] sqls = new SqlParameter[7];
			sqls[0] = new SqlParameter("@IdPQRS ", pqrpqrsprocedentes.IdPQRS);
			sqls[1] = new SqlParameter("@IdOrdenProcendente", pqrpqrsprocedentes.Idordenprocedente);
			sqls[2] = new SqlParameter("@DescripcionPlanos", string.IsNullOrEmpty(pqrpqrsprocedentes.RequierePlanosDescripcion) ? "" : pqrpqrsprocedentes.RequierePlanosDescripcion);
			sqls[3] = new SqlParameter("@DescripcionListados", string.IsNullOrEmpty(pqrpqrsprocedentes.RequierelistadosDescripcion) ? "" : pqrpqrsprocedentes.RequierelistadosDescripcion);
			sqls[4] = new SqlParameter("@DescripcionArmado", string.IsNullOrEmpty(pqrpqrsprocedentes.RequiereArmadoDescripcion) ? "" : pqrpqrsprocedentes.RequiereArmadoDescripcion);
			sqls[5] = new SqlParameter("@OrdenMejoraGarantia", string.IsNullOrEmpty(pqrpqrsprocedentes.OrdenGarantiaOMejora) ? "" : pqrpqrsprocedentes.OrdenGarantiaOMejora);
			sqls[6] = new SqlParameter("@IdPlantaProcedente", pqrpqrsprocedentes.IdPlanta);

			string sql = @"UPDATE  PQRS SET Idordenprocedente = @IdOrdenProcendente,
							DescripcionPlanos = @DescripcionPlanos, 
							DescripcionListados = @DescripcionListados, 
							DescripcionPlanoArmado = @DescripcionArmado, 
							OrdenGarantiaOMejora = @OrdenMejoraGarantia,  
							IdPlantaProcedente = @IdPlantaProcedente 
							WHERE IdPQRS =  @IdPQRS;  ";

			using (SqlCommand cmd = new SqlCommand(sql, con))
			{
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddRange(sqls);
				// Abrimos la conexión y ejecutamos el ExecuteReader
				cmd.Transaction = transaction;
				cmd.ExecuteNonQuery();
			}

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("@pPqrsId", pqrpqrsprocedentes.IdPQRS);
			BdDatos.EjecutarStoreProcedureConParametros("USP_fup_INS_NC_Proceso_Orden_GM", parametros);
		}
		public int GuadarListadosRequeridos(PQRSListadosRequeridos ListadosRequeridos, string usuario, string correo)
		{
			string conexion = BdDatos.conexionScope();
			int Id = 0;

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{   // Abrimos la conexión y ejecutamos el ExecuteReader
				con.Open();
				SqlTransaction transaction = con.BeginTransaction();
				try
				{
					if (ListadosRequeridos.archivos != null && ListadosRequeridos.archivos.Count > 0)
					{
						foreach (var file in ListadosRequeridos.archivos)
						{
							string filePath = SaveFilePath(Id, file.base64, file.nameFile, "ListadosRequeridos");
							GuardarArchivosListados(con, transaction, ListadosRequeridos.IdPQRS, file.nameFile, filePath, correo, usuario, ListadosRequeridos.TipoCargueArchivos, ListadosRequeridos.TablaCargueArchivos);
						}
					}
					transaction.Commit();
				}
				catch (Exception e)
				{
					transaction.Rollback();
				}
				SqlTransaction transaction2 = con.BeginTransaction();
				try
				{
					ListadosRequeridos.listadosPlanos = ListadosRequeridos.listadosPlanos.Where(x =>
						x.PuedeEditar == true).ToList();
					foreach (PQRSListadosPlanos listadoPlano in ListadosRequeridos.listadosPlanos)
					{
						UpdateListadosPlanos(con, transaction2, listadoPlano);
					}
					transaction2.Commit();
				}
				catch (Exception e)
				{
					transaction2.Rollback();
				}
			}
			return Id;
		}
		public void UpdateListadosPlanos(SqlConnection con, SqlTransaction transaction, PQRSListadosPlanos listadoPlano)
		{
			string sql = "";
			if(listadoPlano.TipoCargue == "Listados")
			{
				sql = @"UPDATE PQRSListados SET Completos = @Completo, Comentario = @Comentario WHERE Id = @Id AND IdPQRS = @IdPQRS";
			} else if (listadoPlano.TipoCargue == "Planos")
			{
				sql = @"UPDATE PQRSPlanos SET Completos = @Completo, Comentario = @Comentario WHERE Id = @Id AND IdPQRS = @IdPQRS";
			}
            else if (listadoPlano.TipoCargue == "Armado")
            {
                sql = @"UPDATE PQRSArmado SET Completos = @Completo, Comentario = @Comentario WHERE Id = @Id AND IdPQRS = @IdPQRS";
            }
            SqlParameter[] sqls = new SqlParameter[4];
			sqls[0] = new SqlParameter("@IdPQRS", listadoPlano.IdPQRS);
			sqls[1] = new SqlParameter("@Id", listadoPlano.Id);
			sqls[2] = new SqlParameter("@Completo", listadoPlano.Completos);
			sqls[3] = new SqlParameter("@Comentario", listadoPlano.Comentario);

			using (SqlCommand cmd = new SqlCommand(sql, con))
			{
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddRange(sqls);

				// Abrimos la conexión y ejecutamos el ExecuteReader
				cmd.Transaction = transaction;
				cmd.ExecuteNonQuery();
			}
		}

		public void ListadosCompletos(string idpqrs, string usuario)
		{
			string conexion = BdDatos.conexionScope();

			DateTime fechaActual = DateTime.Now;

			using (SqlConnection con = new SqlConnection(conexion))
			{
				con.Open();
				SqlTransaction transaction = con.BeginTransaction();
				try
				{
					UpdateEstadoPQRS(con, transaction, Int32.Parse(idpqrs), (int)EstadosPQRS.CargarListado);
					SaveLog(con, transaction, Int32.Parse(idpqrs), usuario, (int)EstadosPQRS.Ingenieria, (int)EstadosPQRS.CargarListado, fechaActual);

					transaction.Commit();
				}
				catch (Exception e)
				{
					transaction.Rollback();
				}

			}
		}

		public void Produccion(string idpqrs, string usuario)
		{
				try
				{
				Dictionary<string, object> parametros = new Dictionary<string, object>();
				parametros.Add("@pIdPQRS", idpqrs);
				parametros.Add("@pIdNuevoEstado", (int)EstadosPQRS.Produccion);
				parametros.Add("@pUsuario", usuario);
				List<RespuestaFilasAfectadas> data = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_UPD_PQRSActualizarEstado", parametros);

				}
				catch (Exception e)
				{
				}

		}

		private void GuardarArchivosListados(SqlConnection con, SqlTransaction transaction, int PQRSId, string nameFile, string pathFile, string correo, string usuario, string tipoCargueArchivo, int tablaArchivo)
		{
			string sql = "";
			if (tablaArchivo == 1)
			{
				sql = @"INSERT INTO PQRSListadosArchivos(IdPQRS, FilePATH, FileName, Usuario, FechaCReacion, Mail, Tipo)
							 VALUES(@IdPQRS, @FilePATH, @FileName, @Usuario, getdate(), @Mail, @TipoCargue);
						   ";
			} else if(tablaArchivo == 2)
			{
				sql = @"INSERT INTO PQRSPlanosArchivos(IdPQRS, FilePATH, FileName, Usuario, FechaCReacion, Mail, Tipo)
							 VALUES(@IdPQRS, @FilePATH, @FileName, @Usuario, getdate(), @Mail, @TipoCargue);
						   ";
			} else if (tablaArchivo == 3)
			{
				sql = @"INSERT INTO PQRSArmadoArchivos(IdPQRS, FilePATH, FileName, Usuario, FechaCReacion, Mail, Tipo)
							 VALUES(@IdPQRS, @FilePATH, @FileName, @Usuario, getdate(), @Mail, @TipoCargue);
						   ";
			}
			SqlParameter[] sqls = new SqlParameter[6];
			sqls[0] = new SqlParameter("@IdPQRS", PQRSId);
			sqls[1] = new SqlParameter("@FilePATH", pathFile);
			sqls[2] = new SqlParameter("@FileName", nameFile);
			sqls[3] = new SqlParameter("@Mail", correo);
			sqls[4] = new SqlParameter("@Usuario", usuario);
			sqls[5] = new SqlParameter("@TipoCargue", tipoCargueArchivo);

			using (SqlCommand cmd = new SqlCommand(sql, con))
			{
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddRange(sqls);

				// Abrimos la conexión y ejecutamos el ExecuteReader
				cmd.Transaction = transaction;
				cmd.ExecuteNonQuery();
			}
		}

		public int GuardarimplmentacionObra(PQRSImplementacionObra obra, string usuario)
		{
			string conexion = BdDatos.conexionScope();
			int Id = 0;

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{   // Abrimos la conexión y ejecutamos el ExecuteReader
				con.Open();
				SqlTransaction transaction = con.BeginTransaction();
				try
				{
					if (obra.archivos != null && obra.archivos.Count > 0)
					{
						foreach (var file in obra.archivos)
						{
							string filePath = SaveFilePath(Id, file.base64, file.nameFile, "ImplementacionObra");
							GuardarArchivosObra(con, transaction, obra.IdPQRS, file.nameFile, filePath, usuario);
						}
						UpdateEstadoPQRS(con, transaction, obra.IdPQRS, (int)EstadosPQRS.ImplementacionObra);
					}

					transaction.Commit();
				}
				catch (Exception e)
				{
					transaction.Rollback();

				}

			}
			return Id;
		}
		private void GuardarArchivosObra(SqlConnection con, SqlTransaction transaction, int PQRSId, string nameFile, string pathFile, string usuario)
		{
			SqlParameter[] sqls = new SqlParameter[4];
			sqls[0] = new SqlParameter("@IdPQRS", PQRSId);
			sqls[1] = new SqlParameter("@FilePATH", pathFile);
			sqls[2] = new SqlParameter("@FileName", nameFile);
			sqls[3] = new SqlParameter("@Usuario", usuario);

			string sql = @"INSERT INTO PQRSImplementacionObra(IdPQRS, FilePATH, FileName, UsuarioArchivo)
							 VALUES(@IdPQRS, @FilePATH, @FileName, @Usuario);
						   ";

			using (SqlCommand cmd = new SqlCommand(sql, con))
			{
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddRange(sqls);

				// Abrimos la conexión y ejecutamos el ExecuteReader
				cmd.Transaction = transaction;
				cmd.ExecuteNonQuery();
			}
		}

		public int guardarCierreReclamacion(string planAccion, string fechaCierre, int PQRSId, string descripcionPlanAccion)
		{
			int Id = 0;
			DateTime dateTime = DateTime.Parse(fechaCierre);
			string conexion = BdDatos.conexionScope();
			SqlConnection con = new SqlConnection(conexion);
			con.Open();
			SqlTransaction transaction = con.BeginTransaction();

			SqlParameter[] sqls = new SqlParameter[4];
			sqls[0] = new SqlParameter("@FechaCierre", dateTime);
			sqls[1] = new SqlParameter("@IdPQRS ", PQRSId);
			sqls[2] = new SqlParameter("@PlanAccion", string.IsNullOrEmpty(planAccion) ? "" : planAccion);
			sqls[3] = new SqlParameter("@DescripcionPlanAccion", string.IsNullOrEmpty(descripcionPlanAccion) ? "" : descripcionPlanAccion);

			string sql = @"UPDATE  PQRS SET FechaCierre = @FechaCierre,
											PlanAccion = @PlanAccion, 
											DescripcionPlanAccion = @DescripcionPlanAccion 
											WHERE IdPQRS =  @IdPQRS; ";

			using (SqlCommand cmd = new SqlCommand(sql, con))
			{

				try
				{
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddRange(sqls);

					// Abrimos la conexión y ejecutamos el ExecuteReader
					cmd.Transaction = transaction;
					cmd.ExecuteNonQuery();

					transaction.Commit();

				}
				catch (Exception e)
				{
					throw;
				}
			}
			return Id;
		}

		public void guardarPQRSComprobante(PQRSPComprobante produccion, string usuario)
		{
			string conexion = BdDatos.conexionScope();

			DateTime fechaActual = DateTime.Now;

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				
					

					// Abrimos la conexión y ejecutamos el ExecuteReader
					con.Open();
					SqlTransaction transaction = con.BeginTransaction();
					

					try
					{
						

						if (produccion.archivos != null && produccion.archivos.Count > 0)
						{
							foreach (var file in produccion.archivos)
							{
								//string filePath = SaveFilePathProduccion(produccion.IdPQRS, file.base64, file.nameFile);
								string filePath = SaveFilePath(produccion.IdPQRS, file.base64, file.nameFile, "Produccion");
								GuardarArchivosProduccion(con, transaction, produccion.IdPQRS, file.nameFile, filePath, usuario);
							}
						}

						UpdateEstadoPQRS(con, transaction, produccion.IdPQRS, (int)EstadosPQRS.ImplementacionObra);
						SaveLog(con, transaction, produccion.IdPQRS, usuario, (int)EstadosPQRS.Produccion, (int)EstadosPQRS.ImplementacionObra, fechaActual);

						transaction.Commit();
					}
					catch (Exception e)
					{
						transaction.Rollback();

					}
				
			}
		}

		public void guardarPQRSProduccion(PQRSProduccion produccion, string usuario)
		{
			string conexion = BdDatos.conexionScope();

			DateTime fechaActual = DateTime.Now;
			SqlParameter[] sqls = new SqlParameter[7];
			sqls[0] = new SqlParameter("@FechaPlanAlum ", produccion.fecha_plan_alum);
			sqls[1] = new SqlParameter("@FechaPlanAcero", produccion.fecha_plan_acero);
			sqls[2] = new SqlParameter("@FechaReqAlum", produccion.fecha_req_alum);
			sqls[3] = new SqlParameter("@FechaReqAcero", produccion.fecha_req_acero);
			sqls[4] = new SqlParameter("@FechaDespAlum", produccion.fecha_desp_alum);
			sqls[5] = new SqlParameter("@FechaDespAcero", produccion.fecha_desp_acero);
			//sqls[6] = new SqlParameter("@FechaEntObra", produccion.fecha_ent_obra);
			sqls[6] = new SqlParameter("@IdPQRS", produccion.IdPQRS);

			string sql = @"UPDATE PQRS 
							SET FechaPlanAlum = @FechaPlanAlum, FechaPlanAcero = @FechaPlanAcero,
								FechaReqAlum = @FechaReqAlum, FechaReqAcero = @FechaReqAcero,
								FechaDespAlum = @FechaDespAlum, FechaDespAcero = @FechaDespAcero 
							WHERE IdPQRS = @IdPQRS";

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddRange(sqls);

					// Abrimos la conexión y ejecutamos el ExecuteReader
					con.Open();
					SqlTransaction transaction = con.BeginTransaction();
					cmd.Transaction = transaction;

					try
					{
						cmd.ExecuteNonQuery();
						UpdateEstadoPQRS(con, transaction, produccion.IdPQRS, (int)EstadosPQRS.Produccion);
						SaveLog(con, transaction, produccion.IdPQRS, usuario, (int)EstadosPQRS.RespuestaCliente, (int)EstadosPQRS.Produccion, fechaActual);

						transaction.Commit();
					}
					catch (Exception e)
					{
						transaction.Rollback();

					}
				}
			}
		}

		private void GuardarArchivosProduccion(SqlConnection con, SqlTransaction transaction, int PQRSId, string nameFile, string pathFile, string usuario)
		{
			SqlParameter[] sqls = new SqlParameter[4];
			sqls[0] = new SqlParameter("@IdPQRS ", PQRSId);
			sqls[1] = new SqlParameter("@FilePATH", pathFile);
			sqls[2] = new SqlParameter("@FileName", nameFile);
			sqls[3] = new SqlParameter("@Usuario", usuario);

			string sql = @"INSERT INTO PQRSProduccionArchivos(IdPQRS, FilePATH, FileName, UsuarioArchivo)
							 VALUES(@IdPQRS, @FilePATH, @FileName, @Usuario);
						   ";

			using (SqlCommand cmd = new SqlCommand(sql, con))
			{
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddRange(sqls);

				// Abrimos la conexión y ejecutamos el ExecuteReader
				cmd.Transaction = transaction;
				cmd.ExecuteNonQuery();
			}
		}

		private string SaveFilePathProduccion(int PQRSId, string stringBase64File, string nameFile)
		{
			HttpContext.Current.Server.MapPath("~");

			String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
			rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
			String dlDir = @"\ArchivosPQRS\P" + PQRSId + @"\Prod\";
			String directorio = rutaAplicacion + dlDir;
			if (!Directory.Exists(directorio))
			{
				Directory.CreateDirectory(directorio);
			}

			string pathFile = directorio + nameFile;
			File.WriteAllBytes(pathFile, Convert.FromBase64String(stringBase64File));

			return dlDir.Replace(@"\", "/") + nameFile;
		}


		public List<UsuarioRequiereListadoDTO> ObtenerUsuariosListados(string idprs, string usuario, string mail)
		{

			return _ObtenerUsuariosListados(idprs, usuario, mail);
		}

		private List<UsuarioRequiereListadoDTO> _ObtenerUsuariosListados(string idprs, string usuario, string mail)
		{


			string sql = @"DECLARE @CORREO NVARCHAR(4000);
							SELECT @CORREO = CORREOSLISTADO FROM PQRS WHERE IDPQRS = " + idprs + " SELECT A.VALUE AS MAIL,UPPER(B.USUARIO) USUARIO, IIF(B.USUARIO IS NULL, '0', '1') VALIDO " +
							"FROM STRING_SPLIT(@CORREO, ';') A LEFT JOIN PQRSLISTADOSARCHIVOS B ON A.VALUE = B.MAIL AND IDPQRS = " + idprs + " group by B.MAIL,B.USUARIO,A.VALUE ";

			DataTable dt = BdDatos.CargarTabla(sql);
			List<UsuarioRequiereListadoDTO> lst = dt.AsEnumerable()
				.Select(row => new UsuarioRequiereListadoDTO
				{
					usuario = string.IsNullOrEmpty(row["USUARIO"].ToString()) ? string.Empty : (string)row["USUARIO"],
					mail = string.IsNullOrEmpty(row["MAIL"].ToString()) ? string.Empty : (string)row["MAIL"],
					valido = string.IsNullOrEmpty(row["VALIDO"].ToString()) ? string.Empty : (string)row["VALIDO"],

				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}

		public void RadicarPQRS(string idpqrs,string NombreUsu)
		{
			_RadicarPQRS(idpqrs,NombreUsu);
		}

		public void AnularPQRS(string idpqrs, string NombreUsu)
		{
			_AnularPQRS(idpqrs, NombreUsu);
		}

		private void _RadicarPQRS(string idpqrs,string NombreUsu)
		{
			string conexion = BdDatos.conexionScope();
			DateTime fechaActual = DateTime.Now;
			using (SqlConnection con = new SqlConnection(conexion))
			{

				con.Open();
				SqlTransaction transaction = con.BeginTransaction();
				try
				{
					UpdateEstadoPQRS(con, transaction, int.Parse(idpqrs), (int)EstadosPQRS.Radicado);
					SaveLog(con, transaction, Int32.Parse(idpqrs), NombreUsu, (int)EstadosPQRS.Elaboracion, (int)EstadosPQRS.Radicado, fechaActual);
					transaction.Commit();
				}
				catch (Exception e)
				{
					transaction.Rollback();
				}

			}

		}

		private void _AnularPQRS(string idpqrs, string NombreUsu)
		{
			string conexion = BdDatos.conexionScope();
			DateTime fechaActual = DateTime.Now;
			using (SqlConnection con = new SqlConnection(conexion))
			{

				con.Open();
				SqlTransaction transaction = con.BeginTransaction();
				try
				{
					UpdateEstadoPQRS(con, transaction, int.Parse(idpqrs), (int)EstadosPQRS.Anulada);
					SaveLog(con, transaction, Int32.Parse(idpqrs), NombreUsu, (int)EstadosPQRS.Elaboracion, (int)EstadosPQRS.Anulada, fechaActual);
					transaction.Commit();
				}
				catch (Exception e)
				{
					transaction.Rollback();
				}

			}

		}


		public void GuardarComunicado(Comunicado pqrs, string usuario)
		{
			string conexion = BdDatos.conexionScope();
			DateTime fechaActual = DateTime.Now;

			SqlParameter[] sqls = new SqlParameter[9];
			sqls[0] = new SqlParameter("@IdPQRS", pqrs.Id);
			sqls[1] = new SqlParameter("@MessageTo", pqrs.Para);
			sqls[2] = new SqlParameter("@Asunto", pqrs.Asunto);
			sqls[3] = new SqlParameter("@Mensaje", pqrs.Mensaje);
			sqls[4] = new SqlParameter("@Fecha", fechaActual);
			sqls[5] = new SqlParameter("@Usuario", usuario);
			sqls[6] = new SqlParameter("@FilePATH", string.Empty);
			sqls[7] = new SqlParameter("@FileName", string.Empty);
			sqls[8] = new SqlParameter("@MessageCc", pqrs.Cc);

			string sql = @"INSERT INTO PQRSComunicado([IdPQRS],[MessageTo],[Asunto],[Mensaje],[Fecha],[Usuario],[FilePATH],[FileName],[MessageCc])
							 VALUES(@IdPQRS,@MessageTo,@Asunto,@Mensaje,@Fecha,@Usuario,@FilePATH,@FileName,@MessageCc);";

			// Creamos la conexión
			using (SqlConnection con = new SqlConnection(conexion))
			{
				// Creamos el Comando
				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddRange(sqls);

					// Abrimos la conexión y ejecutamos el ExecuteReader
					con.Open();
					SqlTransaction transaction = con.BeginTransaction();
					cmd.Transaction = transaction;

					try
					{
						cmd.ExecuteNonQuery();
						SaveLog(con, transaction, pqrs.Id, usuario, DBNull.Value, (int)EstadosPQRS.RespuestaCliente, fechaActual);

						transaction.Commit();
					}
					catch (Exception e)
					{
						transaction.Rollback();
					}
				}
			}
		}

		public PQRSComunicado ObtenerRespuestasClienteHistorico(string IdLog)
		{
			string sql = @"SELECT res.MessageTo, res.Asunto, res.Mensaje, res.SeEnvioEncuesta
							FROM PQRSLog lp
							INNER JOIN PQRSComunicado res ON res.IdPQRS = lp.PQRSId
											  AND  lp.Usuario = res.Usuario AND FORMAT( res.Fecha, 'ddMMyyyyHHmmss') =  FORMAT( lp.Fecha, 'ddMMyyyyHHmmss')
							WHERE lp.PQRSLogId = " + IdLog;

			DataTable dt = BdDatos.CargarTabla(sql);
			PQRSComunicado lst = dt.AsEnumerable()
				.Select(row => new PQRSComunicado
				{
					MessageTo = (string)row["MessageTo"],
					Asunto = (string)row["Asunto"],
					Mensaje = (string)row["Mensaje"],
					SeEnvioEncuesta = row["SeEnvioEncuesta"] == System.DBNull.Value ? 0 : (int)row["SeEnvioEncuesta"]
				}).FirstOrDefault();
			dt.Clear();
			dt.Dispose();

			return lst;
		}

		public PQRSProduccionHistorico ObtenerProduccionHistorico(string idPQRS)
		{
			string sql = @"SELECT [FechaPlanAlum],[FechaPlanAcero],[FechaReqAlum],[FechaReqAcero],[FechaDespAlum]
								  ,[FechaDespAcero],[FechaEntObra]
							  FROM PQRS WHERE [IdPQRS] = " + idPQRS;

			DataTable dt = BdDatos.CargarTabla(sql);
			PQRSProduccionHistorico lst = dt.AsEnumerable()
				.Select(row => new PQRSProduccionHistorico
				{
					FechaPlanAlum = row["FechaPlanAlum"] == System.DBNull.Value ? string.Empty : ((DateTime)row["FechaPlanAlum"]).ToString("dd/MM/yyyy"),
					FechaPlanAcero = row["FechaPlanAcero"] == System.DBNull.Value ? string.Empty : ((DateTime)row["FechaPlanAcero"]).ToString("dd/MM/yyyy"),
					FechaReqAlum = row["FechaReqAlum"] == System.DBNull.Value ? string.Empty : ((DateTime)row["FechaReqAlum"]).ToString("dd/MM/yyyy"),
					FechaReqAcero = row["FechaReqAcero"] == System.DBNull.Value ? string.Empty : ((DateTime)row["FechaReqAcero"]).ToString("dd/MM/yyyy"),
					FechaDespAlum = row["FechaDespAlum"] == System.DBNull.Value ? string.Empty : ((DateTime)row["FechaDespAlum"]).ToString("dd/MM/yyyy"),
					FechaDespAcero = row["FechaDespAcero"] == System.DBNull.Value ? string.Empty : ((DateTime)row["FechaDespAcero"]).ToString("dd/MM/yyyy"),
					FechaEntObra = row["FechaEntObra"] == System.DBNull.Value ? string.Empty : ((DateTime)row["FechaEntObra"]).ToString("dd/MM/yyyy"),
				}).FirstOrDefault();
			dt.Clear();
			dt.Dispose();

			return lst;
		}


		public List<PQRSTipoFuente> ObtenerTipoFuentesActivas()
		{
			string sql = @"SELECT [PQRSTipoFuenteID],[Descripcion],[Activo]
							  FROM [dbo].[PQRSTipoFuente]
							  WHERE Activo = 1";

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSTipoFuente> lst = dt.AsEnumerable()
				.Select(row => new PQRSTipoFuente
				{
					PQRSTipoFuenteID = (int)row["PQRSTipoFuenteID"],
					Descripcion = (string)row["Descripcion"],
					Activo = (bool)row["Activo"],
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}


		public List<PQRSTipo> ObtenerSubTiposPQRS(int subTipoPqrs)
		{
			string sql = @"Select PQRSubTipoID, Descripcion from [PQRSubTipo] where PQRSTipoID="+ subTipoPqrs.ToString();

			DataTable dt = BdDatos.CargarTabla(sql);
			List<PQRSTipo> lst = dt.AsEnumerable()
				.Select(row => new PQRSTipo
				{
					Id = (int)row["PQRSubTipoID"],
					Descripcion = (string)row["Descripcion"]
				}).ToList();
			dt.Clear();
			dt.Dispose();

			return lst;
		}


	}
}

