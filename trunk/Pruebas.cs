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
		public void CreacionMdb(){
			string nombreArchivo="tempAccesABorrar.mdb";
			Otras.borrarArchivo(nombreArchivo);
			Assert.IsTrue(!Otras.existeArchivo(nombreArchivo),"no deber�a existir");
			Procesar.crearMDB(nombreArchivo);
			Assert.IsTrue(Otras.existeArchivo(nombreArchivo),"deber�a existir");
		}
	}
}
