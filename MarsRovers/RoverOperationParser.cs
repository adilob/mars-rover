using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MarsRovers
{
	/// <summary>
	/// Internal Rover operation parser object
	/// </summary>
	internal class RoverOperationParser
	{
		/// <summary>
		/// Gets or sets the Rover object
		/// </summary>
		public Rover Rover { get; set; }

		private static RoverOperationParser _singleton = null;
		/// <summary>
		/// Gets a singleton instance for the parser
		/// </summary>
		/// <param name="rover">A valid Rover object</param>
		/// <returns>The RoverOperationParser object</returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static RoverOperationParser GetParser(Rover rover)
		{
			if (_singleton == null)
				_singleton = new RoverOperationParser();

			if (rover == null)
				throw new ArgumentNullException("rover");

			_singleton.Rover = rover;
			return _singleton;
		}

		/// <summary>
		/// Private constructor
		/// </summary>
		private RoverOperationParser() { }

		/// <summary>
		/// Gets the rotate method for the current Rover object
		/// </summary>
		/// <returns></returns>
		private MethodInfo RoverRotate()
		{
			MethodInfo method = this.Rover.GetType().GetMethod("Rotate");
			return method;
		}

		/// <summary>
		/// Gets the move method for the current Rover object
		/// </summary>
		/// <returns></returns>
		private MethodInfo RoverMove()
		{
			MethodInfo method = this.Rover.GetType().GetMethod("Move");
			return method;
		}

		/// <summary>
		/// Parses a command literal and executes the operations on the current Rover object
		/// </summary>
		/// <param name="inputLiteral">A command literal</param>
		public void ParseOperations(string inputLiteral)
		{
			foreach (char c in inputLiteral.ToCharArray())
			{
				switch (c)
				{
					case 'L':
						RoverRotate().Invoke(this.Rover, new object[] { Rotation.Left });
						break;
					case 'R':
						RoverRotate().Invoke(this.Rover, new object[] { Rotation.Right });
						break;
					case 'M':
						RoverMove().Invoke(this.Rover, null);
						break;
					default:
						break;
				}
			}
		}
	}
}
