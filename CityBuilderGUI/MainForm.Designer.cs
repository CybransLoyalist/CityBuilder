
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CityBuilding;

namespace CityBuilderGUI
{
    public class BrushForTileCreator
    {
        public virtual Brush Create(ITile tile, IMap map)
        {
            System.Drawing.Color color;
            switch (tile.TileState)
            {
                case TileState.Blocked:
                    color = Color.Gray;
                    break;
                case TileState.Empty:
                    color = Color.LightYellow;
                    break;
                case TileState.Full:
                    var guid = map.Buildings.First(b => b.Tiles.Any(t => t == tile)).Guid;
                    color = Color.FromArgb(guid.GetHashCode());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new System.Drawing.SolidBrush(color);
        }
    }
    partial class MainForm
    {

        const int TileSize = 20;

        private Dictionary<Rectangle, ITile> _rectangleTilePairs = new Dictionary<Rectangle, ITile>();
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            
                for (int i = 0; i < _map.Width; i++)
                {
                    for (int j = 0; j < _map.Height; j++)
                    {
                        var tile = _map[i, j];
                        var brush = new BrushForTileCreator().Create(tile, _map);
                        var rectangle = _rectangleTilePairs.Where(a => a.Value == tile).First().Key; 
                    
                        g.FillRectangle(brush, rectangle);

                    brush.Dispose();
                    }
                }
        }


        private void OnClick(object sender, MouseEventArgs e)
        {
            var rectangleTilePairs = _rectangleTilePairs.Where(a => a.Key.Contains(e.Location));
            if (rectangleTilePairs.Any())
            {
                var rectangleTilePair = rectangleTilePairs.First();
                if (rectangleTilePair.Value.TileState == TileState.Blocked)
                {
                    rectangleTilePair.Value.TileState = TileState.Empty;
                }
                else if (rectangleTilePair.Value.TileState == TileState.Empty)
                {
                    rectangleTilePair.Value.TileState = TileState.Blocked;
                }
                this.Refresh();
            }
        }

        private System.ComponentModel.IContainer components = null;

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
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(197, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.FillMapWithBuildings);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button1);
            this.Name = "MainForm";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnClick);
            this.ResumeLayout(false);

        }

        #endregion

        private Button button1;
    }
}

