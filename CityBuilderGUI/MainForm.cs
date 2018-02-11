using System.Drawing;
using System.Windows.Forms;
using CityBuilding;

namespace CityBuilderGUI
{
    public partial class MainForm : Form
    {
        private Map _map;

        public MainForm()
        {

            _map = new Map(5, 10);
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

        private void FillMapWithBuildings(object sender, System.EventArgs e)
        {
            new CityBuilding.CityBuilder().FillMap(_map);
            this.Refresh();
        }
    }
}
