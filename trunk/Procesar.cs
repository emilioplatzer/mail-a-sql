/*
 * Creado por SharpDevelop.
 * Usuario: Administrador
 * Fecha: 16/02/2008
 * Hora: 12:10 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OleDb;

namespace Mail2Access
{
	/// <summary>
	/// Description of Procesar.
	/// </summary>
	public class Procesar
	{
		string ContenidoPlano;
		OleDbConnection ConexionABase;
		OleDbDataReader SelectAbierto;
		public Procesar()
		{
		}
		string obtenerCampo(string campo){
			// Regex r=new Regex();
			Regex r=new Regex(" *"+campo+"[ .]*:([^:]*)(:|$)");
			Match m=r.Match(ContenidoPlano);
			if(!m.Success | m.Groups.Count<=1){
				return "";
			}
			return m.Groups[1].ToString();
			/*
			System.Collections.ArrayList a=m.Captures;
			return a.;
			*/
		}
		void abrirBase(){
			ConexionABase = new System.Data.OleDb.OleDbConnection();
			ConexionABase.ConnectionString = @"PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=f:\Ceitech\Servicios Especiales\ServEsp.mdb";
			ConexionABase.Open();
			OleDbCommand cmd = new OleDbCommand("SELECT * FROM MOCs",ConexionABase);
			SelectAbierto=cmd.ExecuteReader();
		}
		void leerMail(string nombreArchivo){
			ContenidoPlano=Otras.leerArchivoCompleto(nombreArchivo);
			string campo=obtenerCampo("Nro de MOC");
			Console.WriteLine("Tengo "+campo);
		}
		string stuff(string valor){
			return valor.Replace('"',' ')
				.Replace('\n',' ')
				.Replace('\r',' ')
				.Replace('\t',' ')
				.Substring(0,250);
		}
		void guardarMailEnBase(){
			StringBuilder campos=new StringBuilder();
			StringBuilder valores=new StringBuilder();
			string separador="";
			for(int i=1;i<SelectAbierto.FieldCount;i++){
				string nombreCampo=SelectAbierto.GetName(i);
				string valorCampo=obtenerCampo(nombreCampo);
				Console.WriteLine(nombreCampo+"="+valorCampo);
				if(valorCampo.Length>0){
					campos.Append(separador+"["+nombreCampo+"]");
					valores.Append(separador+'"'+valorCampo.Replace('"',' ')+'"');
					separador=",";
				}
			}
			string sentencia="INSERT INTO MOCs ("+campos.ToString()+") VALUES ("+
					valores.ToString()+")";
			OleDbCommand cmd = new OleDbCommand(sentencia,ConexionABase);
			Otras.escribirArchivo(@"f:\Ceitech\Servicios Especiales\temp\query.sql"
			                      ,sentencia);
			Console.WriteLine("Resultado "+cmd.ExecuteNonQuery());
		}
		void Uno(string nombreArchivo){
			abrirBase();
			leerMail(nombreArchivo);
			guardarMailEnBase();
		}
		public void LoQueSeaNecesario(){
			Uno("RV_ Alta ADSL Nro 40727 - 'BsAs - GBA Norte' - Ref_ 227542.eml");
		}
		public static void crearMDB(string nombreArchivo){
			
		}
	}
}
