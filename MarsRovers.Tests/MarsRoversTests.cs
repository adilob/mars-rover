using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MarsRovers;

namespace MarsRovers.Tests
{
	[TestFixture]
	public class MarsRoversTests
	{
		[Test]
		public void Create_Board_5Columns_5Lines_Expects_36_Squares()
		{
			RoverBoard board = new RoverBoard(5, 5);
			Assert.IsTrue(board.Squares.Count == 36);
		}

		[Test]
		public void Create_Board_5X5_And_Set_Rover_On_1_2_Facing_North()
		{
			RoverBoard board = new RoverBoard(5, 5);
			board.SetRoverOnBoard(new System.Drawing.Point(1, 2), RoverDirectionHelper.GetDirectionByChar('N'));

			RoverBoardSquare square = board.GetSquareByLocation(new System.Drawing.Point(1, 2));

			Assert.IsNotNull(square);
			Assert.IsNotNull(square.Rover);
			Assert.AreEqual(RoverDirection.North, square.Rover.Direction);
		}

		[Test]
		public void Create_Board_5X5_Set_Rover_1_2_N_RotateLeft_Expects_1_2_W()
		{
			RoverBoard board = new RoverBoard(5, 5);
			board.SetRoverOnBoard(new System.Drawing.Point(1, 2), RoverDirectionHelper.GetDirectionByChar('N'));

			RoverBoardSquare square = board.GetSquareByLocation(new System.Drawing.Point(1, 2));
			square.Rover.Rotate(Rotation.Left);

			Assert.IsNotNull(square);
			Assert.IsNotNull(square.Rover);
			Assert.AreEqual(RoverDirection.West, square.Rover.Direction);
			Assert.AreEqual("1 2 W", square.Rover.ToString());
		}

		[Test]
		public void Create_Board_5X5_Set_Rover_1_2_N_RotateLeft_Twice_Expects_1_2_S()
		{
			RoverBoard board = new RoverBoard(5, 5);
			board.SetRoverOnBoard(new System.Drawing.Point(1, 2), RoverDirectionHelper.GetDirectionByChar('N'));

			RoverBoardSquare square = board.GetSquareByLocation(new System.Drawing.Point(1, 2));
			square.Rover.Rotate(Rotation.Left);
			square.Rover.Rotate(Rotation.Left);

			Assert.IsNotNull(square);
			Assert.IsNotNull(square.Rover);
			Assert.AreEqual(RoverDirection.South, square.Rover.Direction);
			Assert.AreEqual("1 2 S", square.Rover.ToString());
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentOutOfRangeException))]
		public void Create_Board_5X5_Set_Rover_Out_Of_Squares_Range_Expects_ArgumentOutOfRangeException()
		{
			RoverBoard board = new RoverBoard(5, 5);
			board.SetRoverOnBoard(new System.Drawing.Point(6, 6), RoverDirectionHelper.GetDirectionByChar('N'));
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentOutOfRangeException))]
		public void Create_Board_With_Negative_Indexes_Expects_ArgumentOutOfRangeException()
		{
			RoverBoard board = new RoverBoard(-5, -5);
		}

		[Test]
		public void Board5X5_RoverOn_1_2_N_Computes_LMLMLMLMM_Expects_1_3_N()
		{
			RoverBoard board = new RoverBoard(5, 5);
			Rover rover = board.SetRoverOnBoard(new System.Drawing.Point(1, 2), RoverDirectionHelper.GetDirectionByChar('N'));
			Rover expected = new Rover { Direction = RoverDirection.North, Location = new System.Drawing.Point(1, 2) };

			Assert.AreEqual(expected.Direction, rover.Direction);
			Assert.AreEqual(expected.Location, rover.Location);

			//LMLMLMLMM
			expected
				.Rotate(Rotation.Left)
				.Move()
				.Rotate(Rotation.Left)
				.Move()
				.Rotate(Rotation.Left)
				.Move()
				.Rotate(Rotation.Left)
				.Move()
				.Move();

			board.ComputeMovements("LMLMLMLMM", rover);

			Assert.AreEqual("1 3 N", rover.ToString());
			Assert.AreEqual(expected.ToString(), rover.ToString()); // 1 3 N
		}

		[Test]
		public void Board5X5_RoverOn_3_3_E_Computes_MMRMMRMRRM_Expects_5_1_E()
		{
			RoverBoard board = new RoverBoard(5, 5);
			Rover rover = board.SetRoverOnBoard(new System.Drawing.Point(3, 3), RoverDirectionHelper.GetDirectionByChar('E'));
			Rover expected = new Rover { Direction = RoverDirection.East, Location = new System.Drawing.Point(3 ,3) };

			Assert.AreEqual(expected.Direction, rover.Direction);
			Assert.AreEqual(expected.Location, rover.Location);

			//MMRMMRMRRM
			expected
				.Move()
				.Move()
				.Rotate(Rotation.Right)
				.Move()
				.Move()
				.Rotate(Rotation.Right)
				.Move()
				.Rotate(Rotation.Right)
				.Rotate(Rotation.Right)
				.Move();

			board.ComputeMovements("MMRMMRMRRM", rover);

			Assert.AreEqual("5 1 E", rover.ToString());
			Assert.AreEqual(expected.ToString(), rover.ToString()); // 5 1 E
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Create_Board_5X5_Set_RoverOn_1_2_N_PassEmptyLiteralCommand_Expects_ArgumentNullException()
		{
			RoverBoard board = new RoverBoard(5, 5);
			Rover rover = board.SetRoverOnBoard(new System.Drawing.Point(1, 2), RoverDirectionHelper.GetDirectionByChar('N'));
			board.ComputeMovements(string.Empty, rover);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Create_Board_5X5_Set_RoverOn_1_2_N_PassInvalidLiteralCommand_Expects_ArgumentException()
		{
			RoverBoard board = new RoverBoard(5, 5);
			Rover rover = board.SetRoverOnBoard(new System.Drawing.Point(1, 2), RoverDirectionHelper.GetDirectionByChar('N'));
			board.ComputeMovements("asdf", rover);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Create_Board_5X5_Set_RoverOn_1_2_N_PassRoverNull_Expects_ArgumentNullException()
		{
			RoverBoard board = new RoverBoard(5, 5);
			Rover rover = board.SetRoverOnBoard(new System.Drawing.Point(1, 2), RoverDirectionHelper.GetDirectionByChar('N'));
			board.ComputeMovements("LM", null);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(Exception))]
		public void Create_Board_5X5_Set_Two_Rovers_1_2_N_Expects_Conflict_Exception()
		{
			RoverBoard board = new RoverBoard(5, 5);
			Rover rover = board.SetRoverOnBoard(new System.Drawing.Point(1, 2), RoverDirectionHelper.GetDirectionByChar('N'));
			board.ComputeMovements("M", rover);

			Rover rover2 = board.SetRoverOnBoard(new System.Drawing.Point(1, 2), RoverDirectionHelper.GetDirectionByChar('N'));
			board.ComputeMovements("M", rover2);
		}

		[Test]
		public void Create_Board_5X5_Set_Rover_1_2_N_RotateLeft_4Times_Expects_The_Same_Heading()
		{
			RoverBoard board = new RoverBoard(5, 5);
			Rover rover = board.SetRoverOnBoard(new System.Drawing.Point(1, 2), RoverDirectionHelper.GetDirectionByChar('N'));

			rover
				.Rotate(Rotation.Left)
				.Rotate(Rotation.Left)
				.Rotate(Rotation.Left)
				.Rotate(Rotation.Left);

			Assert.AreEqual(RoverDirection.North, rover.Direction);

			board.ComputeMovements("LLLL", rover);
			Assert.AreEqual(RoverDirection.North, rover.Direction);
		}

		[Test]
		public void Create_Board_5X5_Set_Rover_1_2_N_RotateRight_4Times_Expects_The_Same_Heading()
		{
			RoverBoard board = new RoverBoard(5, 5);
			Rover rover = board.SetRoverOnBoard(new System.Drawing.Point(1, 2), RoverDirectionHelper.GetDirectionByChar('N'));

			rover
				.Rotate(Rotation.Right)
				.Rotate(Rotation.Right)
				.Rotate(Rotation.Right)
				.Rotate(Rotation.Right);

			Assert.AreEqual(RoverDirection.North, rover.Direction);

			board.ComputeMovements("RRRR", rover);
			Assert.AreEqual(RoverDirection.North, rover.Direction);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentOutOfRangeException))]
		public void Create_Board_5X5_Set_Rover_1_2_N_MovesOutOfBoard_Expects_ArgumentOutOfRangeException()
		{
			RoverBoard board = new RoverBoard(5, 5);
			Rover rover = board.SetRoverOnBoard(new System.Drawing.Point(1, 2), RoverDirectionHelper.GetDirectionByChar('N'));

			board.ComputeMovements("MMMMMMMMMM", rover);
		}
	}
}
