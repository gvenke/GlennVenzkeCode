namespace GameOfLife
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.generation = new System.Windows.Forms.Timer(this.components);
			this.label2 = new System.Windows.Forms.Label();
			this.CurrentGeneration = new System.Windows.Forms.Label();
			this.canvas = new System.Windows.Forms.PictureBox();
			this.start = new System.Windows.Forms.Button();
			this.stop = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
			this.SuspendLayout();
			// 
			// generation
			// 
			this.generation.Interval = 50;
			this.generation.Tick += new System.EventHandler(this.generation_Tick);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(1135, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(178, 25);
			this.label2.TabIndex = 3;
			this.label2.Text = "Current Generation";
			// 
			// CurrentGeneration
			// 
			this.CurrentGeneration.AutoSize = true;
			this.CurrentGeneration.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CurrentGeneration.Location = new System.Drawing.Point(1319, 54);
			this.CurrentGeneration.Name = "CurrentGeneration";
			this.CurrentGeneration.Size = new System.Drawing.Size(0, 25);
			this.CurrentGeneration.TabIndex = 4;
			// 
			// canvas
			// 
			this.canvas.Location = new System.Drawing.Point(38, 14);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(1068, 956);
			this.canvas.TabIndex = 5;
			this.canvas.TabStop = false;
			this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
			// 
			// start
			// 
			this.start.Location = new System.Drawing.Point(1162, 146);
			this.start.Name = "start";
			this.start.Size = new System.Drawing.Size(75, 23);
			this.start.TabIndex = 6;
			this.start.Text = "Start";
			this.start.UseVisualStyleBackColor = true;
			this.start.Click += new System.EventHandler(this.start_Click);
			// 
			// stop
			// 
			this.stop.Location = new System.Drawing.Point(1162, 208);
			this.stop.Name = "stop";
			this.stop.Size = new System.Drawing.Size(75, 23);
			this.stop.TabIndex = 7;
			this.stop.Text = "Stop";
			this.stop.UseVisualStyleBackColor = true;
			this.stop.Click += new System.EventHandler(this.stop_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1521, 982);
			this.Controls.Add(this.stop);
			this.Controls.Add(this.start);
			this.Controls.Add(this.canvas);
			this.Controls.Add(this.CurrentGeneration);
			this.Controls.Add(this.label2);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Timer generation;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label CurrentGeneration;
		private System.Windows.Forms.PictureBox canvas;
		private System.Windows.Forms.Button start;
		private System.Windows.Forms.Button stop;
	}
}

