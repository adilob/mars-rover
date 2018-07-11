using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarsRovers;
using System.Drawing;

namespace MarsRoversConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			string command = string.Empty;
			RoverBoard board = null;
			Rover rover = null;

			while (true)
			{
				try
				{
					command = Console.ReadLine();
					if (command == string.Empty)
						break;

					string[] data = command.ToUpper().Split(' ');
					if (board == null)
					{
						board = new RoverBoard(int.Parse(data[0]), int.Parse(data[1]));
						continue;
					}

					if (rover == null)
					{
						rover = board.SetRoverOnBoard(
							new Point(int.Parse(data[0]), int.Parse(data[1])),
							RoverDirectionHelper.GetDirectionByChar(char.Parse(data[2])));
						continue;
					}

					board.ComputeMovements(command.ToUpper(), rover);
					rover = null;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}

			if (board != null)
				Console.WriteLine(board.ToString());

		}
	}
}
