using Dungeon_Generator.MapClasses;
using Dungeon_Generator.ServiceClasses.Enums;
using Dungeon_Generator.ServiceClasses.Interfaces;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Dungeon_Generator
{
    public class Room : IMapObject
    {
        private int _x;
        private int _y;
        private int _width;
        private int _height;

        private Marker _lastMarker;
        public Marker Marker 
        { 
            get 
            {
                int _markerX = this._x + _width / 2;
                int _markerY = this._y + _height / 2;

                return new Marker(_markerX, _markerY);
            } 
        }

        public int X
        {
            get { return this._x; }
            set { this._x = value; }
        }

        public int Y
        {
            get { return this._y; }
            set { this._y = value; }
        }

        public int Width
        {
            get { return this._width; }
            set { this._width = value; }
        }

        public int Height
        {
            get { return this._height; }
            set { this._height = value; }
        }

        public MapObjectType Type => MapObjectType.Room;

        public Room(int x, int y, int width, int height)
        {
            this._x = x;
            this._y = y;
            this._width = width;
            this._height = height;
        }

        public void DrawYourself(MapController map)
        {
            /*Данный алгоритм берет за основу изначальные координаты комнаты и ее ширину и высоту. Чтобы
            обозначить границы комнаты, нам нужно встать на изначальные координаты и, чтобы найти нижнюю
            и правую границы, сложить высоту с у и ширину с х: так мы гарантируем, что комната будет отрисована
            даже тогда, когда координата превосходит по значению ширину или высоту.*/
            for (int i = this._y; i < this._y + this._height; i++)
            {
                for (int j = this._x; j < this._x + this._width; j++)
                {
                    if ((i <= this._height + this._y - 1 && i >= this._y) || (j <= this._width + this._x - 1 && j >= this._x))
                    {
                        map._map[i, j].Fill = Brushes.Silver;
                    }
                }
            }
        }

        public void DrawYourMarker(Rectangle[,] map)
        {
            if(_lastMarker != null)
            {
				map[_lastMarker.x, _lastMarker.y].Fill = Brushes.Silver;
			}

            var marker = this.Marker;
            map[marker.y, marker.x].Fill = Brushes.Red;

            _lastMarker = marker;
        }
    }
}
