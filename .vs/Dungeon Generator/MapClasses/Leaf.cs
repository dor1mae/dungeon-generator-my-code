using System;
using System.Windows.Media;


namespace Dungeon_Generator
{
    public class Leaf
    {
        private int _minSizeLeaf;

        private int x;
        private int y;
        private int _height;
        private int _width;
        private Leaf _rightLeafs;
        private Leaf _leftLeafs;
        private Room _room;

        public int MaxSizeLeaf
        {
            get { return this._minSizeLeaf; }
            set { this._minSizeLeaf = value; }
        }

        public int Height
        {
            get { return this._height; }
            set { this._height = value; }
        }

        public int Width
        {
            get { return this._width; }
            set { this._width = value; }
        }

        public Leaf RightLeafs
        {
            get { return this._rightLeafs; }
            set { this._rightLeafs = value; }
        }

        public Leaf LeftLeafs
        {
            get { return this._leftLeafs; }
            set { this._leftLeafs = value; }
        }

        public int GetX
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public int GetY
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public Leaf(int x, int y, int width, int height, int minSizeLeaf)
        {
            this.x = x;
            this.y = y;
            this._width = width;
            this._height = height;
            this._minSizeLeaf = minSizeLeaf;

        }

        public bool Split(Random rnd)
        {
            //Проверяем, есть ли у класса дочерние классы
            if (this._leftLeafs != null || this._rightLeafs != null)
            {
                return false;
            }

            //Определяем направление разреза
            bool _splitH;
            if (rnd.NextDouble() > 0.5)
            {
                _splitH = true;
            }
            else
            {
                _splitH = false;
            }

            //Проверяем, какая из сторон - ширина или высота - наибольшая
            if (this._width > this._height && (double)(this._width) / (double)(this._height) >= 1.25)
            {
                _splitH = false;
            }
            else if (this._height > this._width && (double)(this._height) / (double)(this._width) >= 1.25)
            {
                _splitH = true;
            }

            // Проверка размера сторон будущей комнаты: чтобы они не были меньше минимального размера
            int _max = (_splitH ? this._height : this._width) - this._minSizeLeaf;
            if (_max < this._minSizeLeaf)
            {
                return false;
            }

            //Поиск координаты линии размера
            //Горизонтальный разрез
            int split = rnd.Next(this._minSizeLeaf, _max);
            if (_splitH)
            {
                this._leftLeafs = new Leaf(this.x, this.y, this._width, split, this._minSizeLeaf);
                this._rightLeafs = new Leaf(this.x, this.y + split, this._width, this._height - split, this._minSizeLeaf);
                return true;
            }
            // Вертикальный разрез
            else
            {
                this._leftLeafs = new Leaf(this.x, this.y, split, this._height, this._minSizeLeaf);
                this._rightLeafs = new Leaf(this.x + split, this.y, this._width - split, this._height, this._minSizeLeaf);
                return true;
            }
        }

        public void DrawYourself(MapController map)
        {
            /*Данный алгоритм берет за основу изначальные координаты комнаты и ее ширину и высоту. Чтобы
            обозначить границы комнаты, нам нужно встать на изначальные координаты и, чтобы найти нижнюю
            и правую границы, сложить высоту с у и ширину с х: так мы гарантируем, что комната будет отрисована
            даже тогда, когда координата превосходит по значению ширину или высоту.*/
            for (int i = this.y; i < this.y + this._height; i++)
            {
                for (int j = this.x; j < this.x + this._width; j++)
                {
                    if (i == this._height + this.y - 1 || j == this._width + this.x - 1 || i == this.y || j == this.x)
                    {
                        map._map[i, j].Fill = Brushes.Gray;
                    }
                }
            }
        }

        public void CreateRooms(MapController map, Random rnd, Haller haller)
        {
            if (this._leftLeafs != null || this._rightLeafs != null)
            {
                if (this._leftLeafs != null)
                {
                    this._leftLeafs.CreateRooms(map, rnd, haller);
                }
                if (this._rightLeafs != null)
                {
                    this._rightLeafs.CreateRooms(map, rnd, haller);
                }

                if (this._leftLeafs != null && this._rightLeafs != null)
                {
                    //CreateHall(this._leftLeafs.GetRoom(rnd), this._rightLeafs.GetRoom(rnd), rnd, map);
                }
            }
            else
            {
                int x, y, width, height;
                width = rnd.Next(5, this._width - 2);
                height = rnd.Next(5, this._height - 2);
                x = rnd.Next(1, this._width - width - 1);
                y = rnd.Next(1, this._height - height - 1);

                this._room = new Room(this.x + x, this.y + y, width, height);
                this._room.DrawYourself(map);

                //Заполняет массив внутри Haller'a, чтобы потом построить дороги между комнатами
                for (int i = 0; i < haller.GetRoomNumber; i++)
                {
                    if (haller.GetRoomList[i] == null)
                    {
                        haller.GetRoomList[i] = this._room;
                        haller.GetHallFlag[i] = false;
                        haller.GetRoomCounter[i] = 0;
                        break;
                    }
                }
            }
        }

        //Функция поиска комнаты, которая используется для создания коридоров
        public Room GetRoom(Random rnd)
        {
            if (this._room != null)
            {
                return this._room;
            }
            else
            {
                //Создание переменных под комнаты левого и правого листа
                Room _lRoom = null;
                Room _rRoom = null;

                //Проходим по листьям справа и слева до тех пор, пока не встретим комнату, иначе возвращаем null
                if (this._leftLeafs != null)
                {
                    _lRoom = this._leftLeafs.GetRoom(rnd);
                }
                if (this._rightLeafs != null)
                {
                    _rRoom = this._rightLeafs.GetRoom(rnd);
                }

                if (_lRoom == null && _rRoom == null)
                {
                    return null;
                }
                else if (_lRoom == null)
                {
                    return _rRoom;
                }
                else if (_rRoom == null)
                {
                    return _lRoom;
                }
                else if (rnd.NextDouble() > 0.5)
                {
                    return _lRoom;
                }
                else { return _rRoom; }
            }
        }
    }
}
