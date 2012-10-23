#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace ConveyourDefence
{
	static class Program
	{
		private static ConveyourDefence game;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main ()
		{
			game = new ConveyourDefence();
			game.Run ();
		}
	}
}
