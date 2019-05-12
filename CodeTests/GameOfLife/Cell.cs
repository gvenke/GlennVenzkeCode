using System.Windows;

namespace GameOfLife
{
	public class Cell
	{
		public Cell(double xCoord, double yCoord, int generation) {
			Coords = new Point(xCoord, yCoord);
			Generation = generation;
			IsAlive = false;
		}

		public Point Coords { get; }
		public int Generation {get; private set; }
		public bool IsAlive {get; set; }

	}
}
