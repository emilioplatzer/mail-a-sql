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
			/*
			Assert.AreEqual("hola che",Otras.expandirSignoIgual("L=EDnea"));
			Assert.AreEqual("hola che",Otras.expandirSignoIgual("L=EDnea"));
			Assert.AreEqual("hola che",Otras.expandirSignoIgual("L=EDnea"));
			Assert.AreEqual("hola che",Otras.expandirSignoIgual("L=EDnea"));
			*/
		}
		[Test]
		public void CreacionMdb(){
			string nombreArchivo="tempAccesABorrar.mdb";
			Otras.borrarArchivo(nombreArchivo);
			Assert.IsTrue(!Otras.existeArchivo(nombreArchivo),"no debería existir");
			Procesar.crearMDB(nombreArchivo);
			Assert.IsTrue(Otras.existeArchivo(nombreArchivo),"debería existir");
		}
	}
}
