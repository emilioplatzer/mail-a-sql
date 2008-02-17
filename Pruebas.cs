/*
 * Creado por SharpDevelop.
 * Usuario: Administrador
 * Fecha: 16/02/2008
 * Hora: 02:09 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificaci�n | Editar Encabezados Est�ndar
 */

using System;
using System.Data.OleDb;
using NUnit.Framework;
using ADOX;

namespace Mail2Access
{
	/// <summary>
	/// Description of Pruebas.
	/// </summary>
	[TestFixture]
	public class Pruebas
	{
		public Pruebas()
		{
		}
		[Test]
		public void signoIgual(){
			Assert.AreEqual("hola che",Otras.expandirSignoIgual("hola=20che"));
			Assert.AreEqual("Regi�n",Otras.expandirSignoIgual("Regi=F3n"));
			Assert.AreEqual("L�nea",Otras.expandirSignoIgual("L=EDnea"));
			Assert.AreEqual("el \nsalto",Otras.expandirSignoIgual("el =\nsalto"));
			Assert.AreEqual("lang=ES",Otras.expandirSignoIgual("lang=3DES"));
		}
		[Test]
		public void CreacionMdb(){
			string nombreArchivo="tempAccesABorrar.mdb";
			Otras.borrarArchivo(nombreArchivo);
			Assert.IsTrue(!Otras.existeArchivo(nombreArchivo),"no deber�a existir");
			Catalog cat=BaseDatos.crearMDB(nombreArchivo);
			Assert.IsTrue(Otras.existeArchivo(nombreArchivo),"deber�a existir");
		}
		[Test]
		public void Proceso(){
			string nombreArchivo="tempAccesABorrar2.mdb";
			string directorio="temp_borrar";
			Otras.borrarArchivo(nombreArchivo);
			Assert.IsTrue(!Otras.existeArchivo(nombreArchivo),"no deber�a existir");
			BaseDatos.crearMDB(nombreArchivo);
			Assert.IsTrue(Otras.existeArchivo(nombreArchivo),"deber�a existir");
			OleDbConnection con=BaseDatos.abrirMDB(nombreArchivo);
			string sentencia=@"
				CREATE TABLE Receptor(
				   id number,
				   Numero varchar(250),
				   Nombre varchar(250),
				   [Tipo de documento] varchar(250),
				   [N�mero de documento] varchar(250),
				   Observaciones varchar(250)
				   )";
			OleDbCommand com=new OleDbCommand(sentencia,con);
			com.ExecuteNonQuery();
			con.Close();
			System.IO.Directory.CreateDirectory(directorio);
			Otras.escribirArchivo(directorio+@"\unmail.eml",@"
				lleno de basura
				To:Numero@nombre.com
				From:<Tipo de documento>
				Subject: No encuentro mi n�mero de documento
				
				content:quoted-printable
				Numero: 123 :: Nombre: Carlos Perez
				Tipo de documento: DNI N�mero de documento: 12.333.123.
				Observaciones: condicional");
			Otras.escribirArchivo(directorio+@"\otro mail.eml",@"
				lleno de basura
				To:Numero@nombre.com
				From:<Tipo de documento>
				Subject: No encuentro mi n�mero de documento
				
				content:quoted-printable
				Numero: 124
				Nombre: Mar�a de las Mercedes
				Tipo de documento: DNI - N�mero de documento: 12345678
				Observaciones: condicional");
			MailASql procesador=new MailASql(nombreArchivo,"receptor",directorio);
			procesador.LoQueSeaNecesario();
			procesador.Close();
			con=BaseDatos.abrirMDB(nombreArchivo);
			sentencia="SELECT count(*) FROM Receptor";
			com=new OleDbCommand(sentencia,con);
			string cantidadRegistros=com.ExecuteScalar().ToString();
			Assert.AreEqual("2",cantidadRegistros);
			sentencia="SELECT * FROM Receptor ORDER BY Numero";
			com=new OleDbCommand(sentencia,con);
			OleDbDataReader rdr=com.ExecuteReader();
			rdr.Read();
			Assert.AreEqual("123",rdr.GetValue(1));
		}
	}
}
