using System;
using System.Windows.Forms;

namespace CCTools.CCDesign
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
#if DEBUG
			Application.EnableVisualStyles();
#endif
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(args.Length > 0 ? new Form1(args[0]) : new Form1());
		}
	}
}
