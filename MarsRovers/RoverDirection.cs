using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRovers
{
	/// <summary>
	/// Enum with all possibles directions for the Rover object
	/// </summary>
	public enum RoverDirection
	{
		North,
		East,
		South,
		West
	}

	/// <summary>
	/// Helper to the RoverDirection enum
	/// </summary>
	public static class RoverDirectionHelper
	{
		/// <summary>
		/// Gets the RoverDirection by a char value
		/// </summary>
		/// <param name="c">The char to parse</param>
		/// <returns>The calculated RoverDirection enum value</returns>
		public static RoverDirection GetDirectionByChar(char c)
		{
			RoverDirection directionToReturn = RoverDirection.North;
			switch (c)
			{
				case 'N':
					directionToReturn = RoverDirection.North;
					break;
				case 'E':
					directionToReturn = RoverDirection.East;
					break;
				case 'S':
					directionToReturn = RoverDirection.South;
					break;
				case 'W':
					directionToReturn = RoverDirection.West;
					break;
				default:
					break;
			}
			return directionToReturn;
		}
	}
}
