using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameOfLife
{
	public class GridUnit
	{
		public Cell Cell {get; }
		public Rectangle Rect {get;}

		//private const int RectSize = 16;

		public GridUnit(Cell cell, int size)
		{
			Cell = cell;
			Rect = new Rectangle((int)Cell.Coords.X * size, (int)Cell.Coords.Y * size, size, size);
		}
	}
}
