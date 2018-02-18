using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CityBuilding;
using Point = System.Drawing.Point;

namespace CityBuilderGUI
{
    public partial class MainForm : Form
    {
        private Map _map;

        public MainForm()
        {
            ResetMap();
        }

        private void ResetMap()
        {
            _rectangleTilePairs = new Dictionary<Rectangle, ITile>();
            _map = new Map(25 ,25);
            InitializeComponent();

            for (int i = 0; i < _map.Width; i++)
            {
                for (int j = 0; j < _map.Height; j++)
                {
                    var tile = _map[i, j];
                    var rectangle = new Rectangle(i * (TileSize + 1), j * (TileSize + 1), TileSize, TileSize);

                    _rectangleTilePairs.Add(rectangle, tile);
                }
            }
        }

        private void FillMapWithBuildings(object sender, EventArgs e)
        {
            new CityBuilder(
                new EmptyAreaGroupGetter()).FillMap(_map);
            Refresh();
        }

        public void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            RectStartPoint = e.Location;
           // Invalidate();
        }

        public Point RectStartPoint { get; set; }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            Point tempEndPoint = e.Location;

            Rect =
                                new Rectangle(
                                    Math.Min(RectStartPoint.X, tempEndPoint.X),
                                    Math.Min(RectStartPoint.Y, tempEndPoint.Y),
                                    Math.Abs(RectStartPoint.X - tempEndPoint.X),
                                    Math.Abs(RectStartPoint.Y - tempEndPoint.Y));
            
        }

        public Rectangle Rect { get; set; }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            var rectangleTilePairs = _rectangleTilePairs.Where(a => Rect.Contains(Center(a.Key)));
            foreach (var rectangleTilePair in rectangleTilePairs)
            {
                rectangleTilePair.Value.TileState = TileState.Empty;
            }
            Refresh();
        }

        public static Point Center(Rectangle rect)
        {
            return new Point(rect.Left + rect.Width / 2,
                rect.Top + rect.Height / 2);
        }

        private void Clear(object sender, EventArgs e)
        {
            ResetMap();
            Refresh();
        }
    }
}
