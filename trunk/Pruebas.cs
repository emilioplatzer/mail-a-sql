/*
 * Creado por SharpDevelop.
 * Usuario: Administrador
 * Fecha: 16/02/2008
 * Hora: 02:09 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
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
			Assert.AreEqual("Región",Otras.expandirSignoIgual("Regi=F3n"));
			Assert.AreEqual("Línea",Otras.expandirSignoIgual("L=EDnea"));
			Assert.AreEqual("el \nsalto",Otras.expandirSignoIgual("el =\nsalto"));
			Assert.AreEqual("lang=ES",Otras.expandirSignoIgual("lang=3DES"));
		}
		[Test]
		public void CreacionMdb(){
			string nombreArchivo="tempAccesABorrar.mdb";
			Otras.borrarArchivo(nombreArchivo);
			Assert.IsTrue(!Otras.existeArchivo(nombreArchivo),"no debería existir");
			Catalog cat=BaseDatos.crearMDB(nombreArchivo);
			Assert.IsTrue(Otras.existeArchivo(nombreArchivo),"debería existir");
		}
		[Test]
		public void Proceso(){
			string nombreArchivo="tempAccesABorrar2.mdb";
			Otras.borrarArchivo(nombreArchivo);
			Assert.IsTrue(!Otras.existeArchivo(nombreArchivo),"no debería existir");
			BaseDatos.crearMDB(nombreArchivo);
			Assert.IsTrue(Otras.existeArchivo(nombreArchivo),"debería existir");
			OleDbConnection con=BaseDatos.abrirMDB(nombreArchivo);
			string sentencia=@"
				CREATE TABLE Receptor(
				   Numero varchar(250),
				   Nombre varchar(250),
				   [Tipo de documento] varchar(250),
				   [Número de documento] varchar(250),
				   Observaciones varchar(250)
				   )";
			OleDbCommand com=new OleDbCommand(sentencia,con);
			com.ExecuteNonQuery();
			
		}
	}
}
