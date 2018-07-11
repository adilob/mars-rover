using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MarsRovers
{
	/// <summary>
	/// Internal eventArgs for the rover's location change
	/// </summary>
	internal class RoverLocationEventArgs : EventArgs
	{
		public Point OldLocation { get; set; }
		public Point NewLocation { get; set; }
	}
}
