using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameOfLife
{
	public class Sector
	{
		private static Random rnd = new Random();
		public Point TopLeft {get;}
		public Point BottomRight {get;}
		public Point Center {get; }

		private List<Cell> _cells;

		public Sector(Point center)
		{
			_cells = new List<Cell>();
			Center = center;
			TopLeft = new Point(Center.X-1, Center.Y-1);
			BottomRight = new Point(Center.X+1, Center.Y+1);

		}

		public IEnumerable<Cell> GetNeighbors(Cell cell)
		{
			return _cells.Where(o => o.IsAlive && !o.Equals(cell));
		}

		public IList<Cell> Cells => _cells;
    }
}
