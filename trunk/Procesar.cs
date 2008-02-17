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
			abrirBase();
		}
		string obtenerCampo(string campo,string proximoCampo){
			// Regex r=new Regex(" *"+campo+"[ .]*:([^:\n\r]*)(:|$|\n|\r|"+proximoCampo+")");
			Regex r=new Regex(" *"+campo+"[ .]*:([^`]*?)("+proximoCampo+")", RegexOptions.Multiline);
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
			ConexionABase.ConnectionString = @"PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=c:\Servicios Especiales\ServEsp.mdb";
			ConexionABase.Open();
			OleDbCommand cmd = new OleDbCommand("SELECT * FROM MOCs",ConexionABase);
			SelectAbierto=cmd.ExecuteReader();
		}
		void leerMail(string nombreArchivo){
			ContenidoPlano=Otras.expandirSignoIgual(Otras.leerArchivoCompleto(nombreArchivo));
		}
		string stuff(string valor){
			return valor.Replace('"',' ')
				.Replace('\n',' ')
				.Replace('\r',' ')
				.Replace('\t',' ')
				.Substring(0,Otras.min(250,valor.Length)).Trim();
		}
		void guardarMailEnBase(){
			StringBuilder campos=new StringBuilder();
			StringBuilder valores=new StringBuilder();
			string separador="";
			for(int i=1;i<SelectAbierto.FieldCount;i++){
				string nombreCampo=SelectAbierto.GetName(i);
				// 
				string proximoCampo=i<SelectAbierto.FieldCount-1?SelectAbierto.GetName(i+1):"----";
				/*
				string proximoCampo;
				if(i<SelectAbierto.FieldCount-1){
					proximoCampo=SelectAbierto.GetName(i+1);
				}else{
					proximoCampo="----";
				}
				*/
				string valorCampo=obtenerCampo(nombreCampo,proximoCampo);
				Console.WriteLine(nombreCampo+"="+valorCampo);
				if(valorCampo.Length>0){
					campos.Append(separador+"["+nombreCampo+"]");
					valores.Append(separador+'"'+stuff(valorCampo)+'"');
					separador=",";
				}
			}
			string sentencia="INSERT INTO MOCs ("+campos.ToString()+") VALUES ("+
					valores.ToString()+")";
			OleDbCommand cmd = new OleDbCommand(sentencia,ConexionABase);
			Otras.escribirArchivo(@"c:\Servicios Especiales\temp\query.sql"
			                      ,sentencia);
			Console.WriteLine("Resultado "+cmd.ExecuteNonQuery());
		}
		void Uno(string nombreArchivo){
			leerMail(nombreArchivo);
			guardarMailEnBase();
		}
		public void LoQueSeaNecesario(){
			Uno("RV_ Alta ADSL Nro 40727 - 'BsAs - GBA Norte' - Ref_ 227542.eml");
			Uno("RV_ Alta ADSL Nro 40579 - 'BsAs - GBA Bonaerense' - Ref_ 227669.eml");
		}
		public static void crearMDB(string nombreArchivo){
			
		}
	}
}
