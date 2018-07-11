using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MarsRovers
{
	/// <summary>
	/// Façade for the RoverBoard object
	/// </summary>
	internal static class RoverBoardFacade
	{
		/// <summary>
		/// Validates the command literal provided
		/// </summary>
		/// <param name="board">The RoverBoard object</param>
		/// <param name="inputLiteral">The command literal to validate</param>
		/// <param name="rover">The current Rover object</param>
		public static void ValidateInput(RoverBoard board, string inputLiteral, Rover rover)
		{
			if (string.IsNullOrEmpty(inputLiteral))
				throw new ArgumentNullException("inputLiteral");

			if (!IsInputLiteralValid(inputLiteral))
				throw new ArgumentException("An invalid command was found!");

			if (rover == null)
				throw new ArgumentNullException("rover");

			List<Point> movements = ParseMovements(inputLiteral, rover.Direction, rover.Location);
			List<RoverBoardSquare> squares = board.GetSquaresByPoints(movements);

			CheckForInvalidMovements(squares);

			var roverConflicts = from x in squares
								 where x.Location != rover.Location && x.IsSet
								 select x;

			if (roverConflicts.Count() > 0)
				throw new Exception("A conflict with another rover was found!");
		}

		private static void CheckForInvalidMovements(List<RoverBoardSquare> squares)
		{
			for (int i = 0; i < squares.Count - 1; i++)
			{
				for (int j = i + 1; j < squares.Count; j++)
				{
					int offsetX = Math.Abs(squares[i].Location.X - squares[j].Location.X);
					int offsetY = Math.Abs(squares[i].Location.Y - squares[j].Location.Y);

					if (offsetX > 1 || offsetY > 1)
						throw new ArgumentOutOfRangeException();

					break;
				}
			}
		}

		/// <summary>
		/// Computes a rover roation
		/// </summary>
		/// <param name="rotation">The required rotation</param>
		/// <param name="direction">The required direction</param>
		/// <returns>The final RoverDirection calculated</returns>
		private static RoverDirection Rotate(Rotation rotation, RoverDirection direction)
		{
			RoverDirection directionToRet = RoverDirection.North;

			switch (direction)
			{
				case RoverDirection.North:
					directionToRet = (rotation == Rotation.Left ? RoverDirection.West : RoverDirection.East);
					break;
				case RoverDirection.East:
					directionToRet = (rotation == Rotation.Left ? RoverDirection.North : RoverDirection.South);
					break;
				case RoverDirection.South:
					directionToRet = (rotation == Rotation.Left ? RoverDirection.East : RoverDirection.West);
					break;
				case RoverDirection.West:
					directionToRet = (rotation == Rotation.Left ? RoverDirection.South : RoverDirection.North);
					break;
				default:
					break;
			}

			return directionToRet;
		}

		/// <summary>
		/// Computes a rover movement
		/// </summary>
		/// <param name="direction">The required direction</param>
		/// <param name="location">The required location</param>
		/// <returns>The new location calculated</returns>
		private static Point Move(RoverDirection direction, Point location)
		{
			Point newPoint = new Point();

			switch (direction)
			{
				case RoverDirection.North:
					newPoint = new Point(location.X, location.Y + 1);
					break;
				case RoverDirection.East:
					newPoint = new Point(location.X + 1, location.Y);
					break;
				case RoverDirection.South:
					newPoint = new Point(location.X, location.Y - 1);
					break;
				case RoverDirection.West:
					newPoint = new Point(location.X - 1, location.Y);
					break;
				default:
					break;
			}

			return newPoint;
		}

		/// <summary>
		/// Parses all the commands from the inputLiteral
		/// </summary>
		/// <param name="inputLiteral">The required command literal</param>
		/// <param name="roverDirection">The required direction</param>
		/// <param name="roverLocation">The required location</param>
		/// <returns>A list of calculated points</returns>
		private static List<Point> ParseMovements(string inputLiteral, RoverDirection roverDirection, Point roverLocation)
		{
			List<Point> pointList = new List<Point>();
			Point location = new Point(roverLocation.X, roverLocation.Y);

			foreach (char c in inputLiteral.ToCharArray())
			{
				switch (c)
				{
					case 'L':
						roverDirection = Rotate(Rotation.Left, roverDirection);
						break;
					case 'R':
						roverDirection = Rotate(Rotation.Right, roverDirection);
						break;
					case 'M':
						location = Move(roverDirection, location);
						pointList.Add(location);
						break;
					default:
						break;
				}
			}

			return pointList;
		}

		/// <summary>
		/// Validates if the literal has only valid commands
		/// </summary>
		/// <param name="inputLiteral">The required command literal</param>
		/// <returns>True if valid. Otherwise, False</returns>
		private static bool IsInputLiteralValid(string inputLiteral)
		{
			Func<string, bool> isValid =
						 (s) =>
						 {
							 var data = from x in s.ToCharArray().ToList()
										where x != 'L' && x != 'R' && x != 'M'
										select x;

							 return (data != null && data.Count() <= 0);
						 };

			return isValid(inputLiteral);
		}
	}
}
