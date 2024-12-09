using System.Windows.Shapes;

namespace Dungeon_Generator
{
    public class MapController
    {
        public Rectangle[,] _map;
        public int _height;
        public int _width;
        public Splitter _Splitter;

        public MapController(int height, int width, int maxLeafSize, int minLeafSize)
        {
            this._height = height;
            this._width = width;
            this._Splitter = new Splitter(this, maxLeafSize, minLeafSize);

            EventBus.onGetMap += GetMap;
        }

        public Rectangle[,] GetMap()
        {
            return _map;
        }
    }
}
