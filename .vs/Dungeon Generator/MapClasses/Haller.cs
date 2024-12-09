using System;

namespace Dungeon_Generator
{
    public class Haller
    {
        private Room[] _roomList;
        private bool[] _hallFlag;
        private int _roomNumber;
        private int[] _hallCounter;

        public Haller(int _iter)
        {
            this._roomNumber = _iter;
            this._roomList = new Room[_roomNumber];
            this._hallFlag = new bool[_roomNumber];
            this._hallCounter = new int[_roomNumber];
        }

        public int GetRoomNumber
        {
            get { return this._roomNumber; }
            set { this._roomNumber = value; }
        }

        public int[] GetRoomCounter
        {
            get { return this._hallCounter; }
            set { this._hallCounter = value; }
        }

        public Room[] GetRoomList
        {
            get { return this._roomList; }
            set { this._roomList = value; }
        }

        public bool[] GetHallFlag
        {
            get { return this._hallFlag; }
            set { this._hallFlag = value; }
        }

        /*Используется сортировка Шелла. Дважды. Делается это для того, чтобы расположить в рабочей матрице
         комнаты так, чтобы алгоритм соединения комнат работал с соседними комнатами, а не проводил пути между помещениями в двух
        противоположных углах подземелья*/
        public void ListFiller(Leaf _root, MapController map, Random rnd)
        {
            _root.CreateRooms(map, rnd, this);

            int _size = this._roomList.Length;

            //Сперва сортируем по высоте
            for (int s = _size / 2; s > 0; s /= 2)
            {
                for (int i = s; i < _size; ++i)
                {
                    for (int j = i - s; (j >= 0 && (this._roomList[j].X >= this._roomList[j + s].X)); j -= s)
                    {
                        Room temp = this._roomList[j];
                        this._roomList[j] = this._roomList[j + s];
                        this._roomList[j + s] = temp;
                    }
                }
            }

            /*for (int s = _size / 2; s > 0; s /= 2)
            {
                for (int i = s; i < _size; ++i)
                {
                    for (int j = i - s; (j >= 0 && (this._roomList[j].X >= this._roomList[j + s].X && (this._roomList[j].Y >= this._roomList[j + s].Y - map._Splitter._maxLeafSize  && this._roomList[j].Y <= this._roomList[j + s].Y + map._Splitter._maxLeafSize))); j -= s)
                    {
                        Room temp = this._roomList[j];
                        this._roomList[j] = this._roomList[j + s];
                        this._roomList[j + s] = temp;
                    }
                }
            }*/

            for (int i = 0; i < _size; i++)
            {
                System.Console.WriteLine($"Комната {i + 1} имеет координаты [{this._roomList[i].Y},{this._roomList[i].X}]");
            }
        }

        public void HallWatcher(MapController map, Random rnd, int max)
        {
            int i = 0;
            while (i < this._roomList.Length - 1)
            {
                HallCreator(this._roomList[i], this._roomList[i + 1], map, rnd);
                i++;
            }
        }

        public void HallCreator(Room l, Room r, MapController map, Random rnd)
        {
            Hall temp;

            //Высчитываются случайные точки в пределах площади двух комнат
            int point1_x, point1_y, point2_x, point2_y;
            point1_x = rnd.Next(l.X + 1, l.X + l.Width - 2);
            point1_y = rnd.Next(l.Y + 1, l.Y + l.Height - 2);
            point2_x = rnd.Next(r.X + 1, r.X + r.Width - 2);
            point2_y = rnd.Next(r.Y + 1, r.Y + r.Height - 2);

            //Вычисляется расстояние между точками первой комнаты и второй. Разность даст ширину и высоту коридора
            int w = point2_x - point1_x;
            int h = point2_y - point1_y;

            //левая правее
            if (w < 0)
            {
                //ниже
                if (h < 0)
                {
                    if (rnd.NextDouble() > 0.5)
                    {
                        temp = new Hall(point2_x, point2_y, Math.Abs(w) + 1, 1, false);
                        temp.DrawYourself(map);

                        temp = new Hall(point1_x, point2_y, 1, Math.Abs(h) + 1, true);
                        temp.DrawYourself(map);
                    }
                    else
                    {
                        temp = new Hall(point2_x, point1_y, Math.Abs(w) + 1, 1, false);
                        temp.DrawYourself(map);

                        temp = new Hall(point2_x, point2_y, 1, Math.Abs(h) + 1, true);
                        temp.DrawYourself(map);
                    }
                }
                //выше
                else if (h > 0)
                {
                    if (rnd.NextDouble() > 0.5)
                    {
                        temp = new Hall(point2_x, point2_y, Math.Abs(w) + 1, 1, false);
                        temp.DrawYourself(map);

                        temp = new Hall(point1_x, point1_y, 1, Math.Abs(h) + 1, true);
                        temp.DrawYourself(map);
                    }
                    else
                    {
                        temp = new Hall(point2_x, point1_y, Math.Abs(w) + 1, 1, false);
                        temp.DrawYourself(map);

                        temp = new Hall(point2_x, point1_y, 1, Math.Abs(h) + 1, true);
                        temp.DrawYourself(map);
                    }
                }
                else
                {
                    temp = new Hall(point2_x, point1_y, Math.Abs(w) + 1, 1, false);
                    temp.DrawYourself(map);
                }
            }
            //левее
            else if (w > 0)
            {
                //ниже
                if (h < 0)
                {
                    if (rnd.NextDouble() > 0.5)
                    {
                        temp = new Hall(point1_x, point1_y, Math.Abs(w) + 1, 1, false);
                        temp.DrawYourself(map);

                        temp = new Hall(point2_x, point2_y, 1, Math.Abs(h) + 1, true);
                        temp.DrawYourself(map);
                    }
                    else
                    {
                        temp = new Hall(point1_x, point2_y, Math.Abs(w) + 1, 1, false);
                        temp.DrawYourself(map);

                        temp = new Hall(point1_x, point2_y, 1, Math.Abs(h) + 1, true);
                        temp.DrawYourself(map);
                    }
                }
                //выше
                else if (h > 0)
                {
                    if (rnd.NextDouble() > 0.5)
                    {
                        temp = new Hall(point1_x, point1_y, Math.Abs(w) + 1, 1, false);
                        temp.DrawYourself(map);

                        temp = new Hall(point2_x, point1_y, 1, Math.Abs(h) + 1, true);
                        temp.DrawYourself(map);
                    }
                    else
                    {
                        temp = new Hall(point1_x, point2_y, Math.Abs(w) + 1, 1, false);
                        temp.DrawYourself(map);

                        temp = new Hall(point1_x, point1_y, 1, Math.Abs(h) + 1, true);
                        temp.DrawYourself(map);
                    }
                }
                else
                {
                    temp = new Hall(point1_x, point1_y, Math.Abs(w) + 1, 1, false);
                    temp.DrawYourself(map);
                }
            }
            else
            {
                //ниже
                if (h < 0)
                {
                    temp = new Hall(point2_x, point2_y, 1, Math.Abs(h) + 1, true);
                    temp.DrawYourself(map);
                }
                //выше
                else if (h > 0)
                {
                    temp = new Hall(point1_x, point1_y, 1, Math.Abs(h) + 1, true);
                    temp.DrawYourself(map);
                }
            }
        }
    }
}
