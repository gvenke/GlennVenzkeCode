using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace GameOfLife
{
	public static class PetriDish
	{
		private static Random rnd = new Random();

		private static List<Sector> _neighborHoods;
		private const int CellLives = 1;
		public const int HorizontalThreshold = 60;
		public const int VerticalThreshold = 60;
		public static int CurrentGeneration {get; set; }
		public static ConcurrentBag<Cell> Colony {get; private set; }



		public static void Genesis()
	    {
			CurrentGeneration = 0;
			Colony = new ConcurrentBag<Cell>();
			_neighborHoods = new List<Sector>();

			InitColony();
			InitSectors();
	    }

		private static void KillCell(Cell cell) {
			cell.IsAlive = false;
		}

		public static void ResetColony()
		{
			Parallel.ForEach(Colony, (curCell) => {
				curCell.IsAlive = false;
			});
		}

		public static void SpawnNextGeneration()
		{
			CurrentGeneration++;
			
			Parallel.ForEach(Colony, (curCell) => {
				var sector = _neighborHoods.First(o => o.Center.X == curCell.Coords.X && o.Center.Y == curCell.Coords.Y);
				var neighbors = sector.GetNeighbors(curCell);

				if (curCell.IsAlive) {

					// kill underpopulated cells
					if (neighbors.Count() < 2) {
						KillCell(curCell);

					// kill overpopulated cells
					} else if (neighbors.Count() > 3) {
						KillCell(curCell);					
					} 
				}  else {
					if (neighbors.Count() == 3) {
						curCell.IsAlive = true;
				    }
				}	
				    
			});
		}

		private static void InitColony()
		{
			Parallel.For(0, HorizontalThreshold, (x) => {
				Parallel.For(0, VerticalThreshold, (y) => {
					var newCell = new Cell(x,y,CurrentGeneration);
					//newCell.IsAlive = rnd.Next(1,13) == CellLives;				
					Colony.Add(newCell);
				});
			});
		}

		private static void InitSectors()
		{
			Parallel.ForEach(Colony, (curCell) => {
			//foreach(var curCell in Colony) {
				_neighborHoods.Add(GetSector(curCell));
			});
		}

		private static Sector GetSector(Cell cell)
		{
			var sector = new Sector(cell.Coords);

		// determine sector iteration bounds
			var lboundX = sector.TopLeft.X;
			var uboundX = sector.BottomRight.X;
			var lboundY = sector.TopLeft.Y;
			var uboundY = sector.BottomRight.Y;

			for (var x = lboundX; x <= uboundX; x++) {
				for (var y = lboundY; y <= uboundY; y++) {
					var curCell = Colony.FirstOrDefault(o => o.Coords.X == x && o.Coords.Y == y);
					if (curCell != null) {
						sector.Cells.Add(curCell);
					}
				}
			}
			return sector;
		}
	}
}
