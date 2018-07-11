using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MarsRovers
{
	/// <summary>
	/// Represents a rover with its properties
	/// </summary>
	public class Rover
	{
		/// <summary>
		/// Gets or sets the rover's location
		/// </summary>
		public Point Location { get; set; }

		/// <summary>
		/// Gets or sets the rover's direction
		/// </summary>
		public RoverDirection Direction { get; set; }

		/// <summary>
		/// Internal event that is fired when the rover's location is changed
		/// </summary>
		internal event EventHandler<RoverLocationEventArgs> OnLocationChanged;

		/// <summary>
		/// Default constructor
		/// </summary>
		public Rover() { }

		/// <summary>
		/// Performs a rotation on the rover
		/// </summary>
		/// <param name="rotation">The required rotation's value</param>
		/// <returns>Returns the Rover object for fluent interface operations</returns>
		public Rover Rotate(Rotation rotation)
		{
			switch (this.Direction)
			{
				case RoverDirection.North:
					this.Direction = (rotation == Rotation.Left ? RoverDirection.West : RoverDirection.East);
					break;
				case RoverDirection.East:
					this.Direction = (rotation == Rotation.Left ? RoverDirection.North : RoverDirection.South);
					break;
				case RoverDirection.South:
					this.Direction = (rotation == Rotation.Left ? RoverDirection.East : RoverDirection.West);
					break;
				case RoverDirection.West:
					this.Direction = (rotation == Rotation.Left ? RoverDirection.South : RoverDirection.North);
					break;
				default:
					break;
			}

			return this;
		}

		/// <summary>
		/// Changes the current rover's location based on its currently direction
		/// </summary>
		/// <returns>Returns the Rover object for fluent interface operations</returns>
		public Rover Move()
		{
			Point oldLocation = new Point(this.Location.X, this.Location.Y);
			switch (this.Direction)
			{
				case RoverDirection.North:
					this.Location = new Point(this.Location.X, this.Location.Y + 1);
					break;
				case RoverDirection.East:
					this.Location = new Point(this.Location.X + 1, this.Location.Y);
					break;
				case RoverDirection.South:
					this.Location = new Point(this.Location.X, this.Location.Y - 1);
					break;
				case RoverDirection.West:
					this.Location = new Point(this.Location.X - 1, this.Location.Y);
					break;
				default:
					break;
			}

			if (this.OnLocationChanged != null)
			{
				this.OnLocationChanged(
					this,
					new RoverLocationEventArgs
					{
						OldLocation = oldLocation,
						NewLocation = this.Location
					});
			}

			return this;
		}

		/// <summary>
		/// Overrides the base.ToString() method
		/// </summary>
		/// <returns>A formatted string with the location and heading of the Rover object</returns>
		public override string ToString()
		{
			return string.Format("{0} {1} {2}",
				this.Location.X,
				this.Location.Y,
				this.Direction.ToString().Substring(0, 1));
		}
	}
}
