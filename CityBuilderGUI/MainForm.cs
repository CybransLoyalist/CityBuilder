using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CityBuilder;
using CityBuilder.AreaWithBuildingFilling;
using CityBuilder.MapModel;
using CityBuilder.MapModel.Tiles;
using Point = System.Drawing.Point;

namespace CityBuilderGUI
{
    public class MapWithBuildingsFillerFactory
    {
        public virtual MapWithBuildingsFiller Create(IMap map)
        {
            return new MapWithBuildingsFiller(
                map,
                new EmptyAreaGroupGetter(),
                new StreetsAppender(),
                new AreaWithBuildingFiller(
                    map,
                    new RandomBuildingLocationGenerator(),
                    new BuildingOnMapIfPossibleLocator(
                        map,
                        new BuildingOnMapLocator(),
                        new PathToNearestStreetFromBuildingFinder(
                            map,
                            new BuildingOnMapLocator(),
                            new ClosestStreetFinder())),
                    new MapFillingParametersCalculator()));
        }
    }
    public class MapDrawer
    {
        public void Draw(IMap map, Graphics graphics, int TileSize)
        {
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    var tile = map[i, j];
                    var brush = new BrushForTileCreator().Create(tile, map);
                    var rectangle = new Rectangle(i * (TileSize + 1), j * (TileSize + 1), TileSize, TileSize);

                    graphics.FillRectangle(brush, rectangle);

                    brush.Dispose();
                }
            }
        }
    }

    public partial class MainForm : Form
    {
        private Map _map;

        public MainForm()
        {
            ResetMap();
        }

        const int TileSize = 20;

        private Dictionary<Rectangle, ITile> _rectangleTilePairs;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            new MapDrawer().Draw(_map, e.Graphics, TileSize);
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

            MainForm_MouseDown(sender, e);
        }

        private void ResetMap()
        {
            _rectangleTilePairs = new Dictionary<Rectangle, ITile>();
            _map = new Map(25, 25);
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
            new MapWithBuildingsFillerFactory().Create(_map).FillMap();
            Refresh();
        }

        public void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            RectStartPoint = e.Location;
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
            var rectangleTilePairs = _rectangleTilePairs.Where(a =>
                Rect.Contains(Center(a.Key)) ||
                Rect.Contains(LeftTop(a.Key)) ||
                Rect.Contains(LeftBottom(a.Key)) ||
                Rect.Contains(RightTop(a.Key)) ||
                Rect.Contains(RightBottom(a.Key)));
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

        public static Point LeftTop(Rectangle rect)
        {
            return new Point(rect.Left, rect.Top);
        }

        public static Point LeftBottom(Rectangle rect)
        {
            return new Point(rect.Left, rect.Bottom);
        }

        public static Point RightTop(Rectangle rect)
        {
            return new Point(rect.Right, rect.Top);
        }

        public static Point RightBottom(Rectangle rect)
        {
            return new Point(rect.Right, rect.Bottom);
        }

        private void Clear(object sender, EventArgs e)
        {
            ResetMap();
            Refresh();
        }
    }
}