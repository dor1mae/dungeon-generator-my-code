using Dungeon_Generator.ServiceClasses.Enums;
using Dungeon_Generator.ServiceClasses.Interfaces;
using System.Windows.Media;

namespace Dungeon_Generator
{
    internal class Hall : IMapObject
    {
        private int _x;
        private int _y;
        private int _width;
        private int _height;
        private bool _direction;

        public Hall(int x, int y, int width, int height, bool direction)
        {
            this._x = x;
            this._y = y;
            this._width = width;
            this._height = height;
            this._direction = direction;
        }

        public MapObjectType Type => MapObjectType.Hall;

        public void DrawYourself(MapController map)
        {
            /*Данный алгоритм берет за основу изначальные координаты комнаты и ее ширину и высоту. Чтобы
            обозначить границы комнаты, нам нужно встать на изначальные координаты и, чтобы найти нижнюю
            и правую границы, сложить высоту с у и ширину с х: так мы гарантируем, что комната будет отрисована
            даже тогда, когда координата превосходит по значению ширину или высоту.*/

            //Вверх или вниз
            if (this._direction == true)
            {
                for (int i = this._y; i < this._height + this._y; i++)
                {
                    try
                    {
                        map._map[i, this._x].Fill = Brushes.Silver;
                    }
                    catch { System.Console.WriteLine($"Исключение в координате [{i},{this._x}]"); }
                }
            }

            //Вбок
            else if (this._direction == false)
            {
                for (int j = this._x; j < this._width + this._x; j++)
                {
                    try
                    {
                        map._map[this._y, j].Fill = Brushes.Silver;
                    }
                    catch { System.Console.WriteLine($"Исключение в координате [{this._y}, {j}]"); }
                }
            }
        }
    }
}
