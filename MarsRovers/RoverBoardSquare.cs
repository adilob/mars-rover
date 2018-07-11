using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MarsRovers
{
	/// <summary>
	/// Represents each possible location on the RoverBoard
	/// </summary>
	public class RoverBoardSquare
	{
		/// <summary>
		/// Gets or sets if the square has a Rover object set
		/// </summary>
		public bool IsSet { get; set; }

		/// <summary>
		/// Gets or sets a Rover object on the square
		/// </summary>
		public Rover Rover { get; set; }

		/// <summary>
		/// Gets or sets the square's location
		/// </summary>
		public Point Location { get; set; }

		/// <summary>
		/// Default constructor
		/// </summary>
		public RoverBoardSquare() { }

		/// <summary>
		/// Overrides the base.ToString() method
		/// </summary>
		/// <returns>A formatted string with the rover's location or the square's location</returns>
		public override string ToString()
		{
			if (this.Rover != null)
				return this.Rover.ToString();

			return this.Location.ToString();
		}
	}
}
