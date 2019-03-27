using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace GameOfLife
{
	public partial class Form1 : Form
	{
		private const int generationSpan = 5;
		private Font gridUnitFont  = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);
		private Pen grayPen;
		SolidBrush blackBrush;
		private List<GridUnit> _gridContents;
		private Bitmap _grid;
		private EventHandler _canvasClickHandler;

		public Form1()
		{
			_canvasClickHandler = new EventHandler(canvas_Click);
			grayPen = new Pen(Color.Gray);
			blackBrush = new SolidBrush(Color.Black);
			InitializeComponent();
			_gridContents = new List<GridUnit>();
			
			
			//Load += new EventHandler(Form1_Load);  
			PreInit();
		}

		private  void Init()
		{	
			PetriDish.CurrentGeneration = 0;
			canvas.Click -= _canvasClickHandler;			
			//start.Click -= start_Click;
			start.Enabled = false;
			generation.Start();
			CurrentGeneration.Text = PetriDish.CurrentGeneration.ToString();
			stop.Enabled = true;
		}

		private void PreInit()
		{
			PetriDish.Genesis();
			initGrid();
			InitInterface();
			canvas.Refresh();
		}

		private void InitInterface()
		{
			start.Enabled = true;
			canvas.Click += _canvasClickHandler;
			stop.Enabled = false;
		}

		private void Terminate()
		{
			generation.Stop();
			PetriDish.ResetColony();
			canvas.Refresh();
			InitInterface();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//PreInit();

		}

		private void initGrid()
		{
			int gridUnitSize = Math.Min(canvas.Width, canvas.Height) / PetriDish.HorizontalThreshold;
			_grid = new Bitmap(canvas.Width, canvas.Height);
			var graphics = Graphics.FromImage(_grid);
			foreach(var curCell in PetriDish.Colony) {
				var newGridUnit = new GridUnit(curCell, gridUnitSize);
				graphics.DrawRectangle(grayPen, newGridUnit.Rect);
				_gridContents.Add(newGridUnit);
			}			
		}

		private void DrawLiveCells(Graphics graphics)
		{
			var blackGridUnits = _gridContents.Where(o => o.Cell.IsAlive);
			foreach(var curGridUnit in blackGridUnits) {
				graphics.DrawRectangle(grayPen, curGridUnit.Rect);
				graphics.FillRectangle(blackBrush, curGridUnit.Rect);
			}
		}
			

		private void canvas_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawImage(_grid, new Point(0,0));
			DrawLiveCells(e.Graphics);
		}

		private void generation_Tick(object sender, EventArgs e)
		{
			PetriDish.SpawnNextGeneration();	
			CurrentGeneration.Text = PetriDish.CurrentGeneration.ToString();
			canvas.Refresh();
		}

		~Form1()
		{
			gridUnitFont.Dispose();
			grayPen.Dispose();
			blackBrush.Dispose();
		}

		private void canvas_Click(object sender, EventArgs e)
		{
			MouseEventArgs e2 = (MouseEventArgs) e;
			int xCoord = e2.X;
			int yCoord = e2.Y;

			var gridUnitClicked = _gridContents.FirstOrDefault(o => xCoord >= o.Rect.Left && xCoord <= o.Rect.Right && yCoord >= o.Rect.Top && yCoord <= o.Rect.Bottom);
			if (gridUnitClicked != null) {
				gridUnitClicked.Cell.IsAlive = true ;
				canvas.Refresh();
			}
			
		}

		private void start_Click(object sender, EventArgs e)
		{
			Init();
		}

		private void stop_Click(object sender, EventArgs e)
		{
			Terminate();
		}
	}
}
