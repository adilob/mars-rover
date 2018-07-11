using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MarsRovers
{
	/// <summary>
	/// Represents the plateau of the suggested problem as a simple board
	/// </summary>
	public class RoverBoard
	{
		/// <summary>
		/// The number of board's columns
		/// </summary>
		public int Columns { get; private set; }

		/// <summary>
		/// The number of board's lines
		/// </summary>
		public int Lines { get; private set; }

		/// <summary>
		/// A list of squares representing each possible location on the board
		/// </summary>
		public List<RoverBoardSquare> Squares { get; private set; }

		/// <summary>
		/// Creates an instance of RoverBoard with the provided columns and lines
		/// </summary>
		/// <param name="columns">The number of columns to create</param>
		/// <param name="lines">The number of lines to create</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public RoverBoard(int columns, int lines)
		{
			this.Columns = columns;
			this.Lines = lines;

			if (columns <= 0 || lines <= 0)
				throw new ArgumentOutOfRangeException();

			InitializeSquares();
		}

		/// <summary>
		/// Initializes the list of squares
		/// </summary>
		private void InitializeSquares()
		{
			/*
			 * The board should have a bottom left corner (the 0,0 location) and
			 * should have too, the upper right corner that is defined by the number of
			 * columns and lines provided by the user on the application.
			 * So, that's the why the code below initializes the Squares list with the "+ 1" beside
			 * the coluns and lines.
			 * */
			Squares = new List<RoverBoardSquare>();
			for (int i = 0; i < this.Columns + 1; i++)
				for (int j = 0; j < this.Lines + 1; j++)
					Squares.Add(new RoverBoardSquare 
					{ 
						IsSet = false,
						Location = new Point(i, j)
					});
		}

		/// <summary>
		/// Gets a RoverBoardSquare object based on its location
		/// </summary>
		/// <param name="location">The required square location</param>
		/// <returns>A RoverBoardSquare object</returns>
		public RoverBoardSquare GetSquareByLocation(Point location)
		{
			int index = (location.X * (this.Columns + 1)) + location.Y;
			return Squares[index];
		}

		/// <summary>
		/// Sets a Rover object on the board
		/// </summary>
		/// <param name="location">The required location for the Rover</param>
		/// <param name="direction">The initial direction of the Rover</param>
		/// <returns>The Rover object set on the board</returns>
		public Rover SetRoverOnBoard(Point location, RoverDirection direction = RoverDirection.North)
		{
			RoverBoardSquare square = GetSquareByLocation(location);

			square.IsSet = true;
			square.Rover = new Rover 
			{ 
				Direction = direction,
				Location = location
			};
			square.Rover.OnLocationChanged += new EventHandler<RoverLocationEventArgs>(Rover_OnLocationChanged);

			return square.Rover;
		}

		/// <summary>
		/// Computes the provided command literal.
		/// If valid, performs all the commands for the Rover object. Otherwise throws a exception.
		/// </summary>
		/// <param name="inputLiteral">The command literal to compute</param>
		/// <param name="rover">The current Rover that will be affected</param>
		public void ComputeMovements(string inputLiteral, Rover rover)
		{
			RoverBoardFacade.ValidateInput(this, inputLiteral, rover);
			RoverOperationParser.GetParser(rover).ParseOperations(inputLiteral);
		}

		/// <summary>
		/// Observes a rover's location change and sets the appropriated squares that were affected by the operation
		/// </summary>
		void Rover_OnLocationChanged(object sender, RoverLocationEventArgs e)
		{
			Rover rover = sender as Rover;

			RoverBoardSquare oldSquare = GetSquareByLocation(e.OldLocation);
			RoverBoardSquare newSquare = GetSquareByLocation(e.NewLocation);

			oldSquare.IsSet = false;
			oldSquare.Rover = null;

			newSquare.IsSet = true;
			newSquare.Rover = rover;
		}

		/// <summary>
		/// Gets a list of squares by the provided points on the grid
		/// </summary>
		/// <param name="points">A list of points to compute</param>
		/// <returns>A list of squares</returns>
		public List<RoverBoardSquare> GetSquaresByPoints(List<Point> points)
		{
			List<RoverBoardSquare> list = new List<RoverBoardSquare>();
			points.ForEach((p) => list.Add(GetSquareByLocation(p)));
			return list;
		}

		/// <summary>
		/// Overrides a base.ToString() method
		/// </summary>
		/// <returns>A formatted string which represents only the Rovers found on the board</returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			Squares.FindAll((s) => s.IsSet).ForEach((s) => sb.AppendLine(s.Rover.ToString()));
			return sb.ToString();
		}
	}
}
