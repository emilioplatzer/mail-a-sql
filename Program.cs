/*
 * Creado por SharpDevelop.
 * Usuario: Administrador
 * Fecha: 16/02/2008
 * Hora: 12:08 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificaci�n | Editar Encabezados Est�ndar
 */
using System;

namespace Mail2Access
{
	class Program
	{
		public static void Main(string[] args)
		{
			// 
			new Pruebas().Proceso(); /*
			string dirBase=System.Environment.GetEnvironmentVariable("MAIL2ACCESS_DIR");
			new MailASql(dirBase+@"\ServEsp.mdb"
			             ,"MOCs"
			             ,dirBase+@"\MailsAProcesar")
				.LoQueSeaNecesario();
			// */
			Console.WriteLine("Procesado!");
			
			// TODO: Implement Functionality Here
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}