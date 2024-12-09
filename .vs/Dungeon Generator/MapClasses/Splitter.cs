using System;
using System.Collections.Generic;

namespace Dungeon_Generator
{
    public class Splitter
    {
        private MapController _Map;
        public int _maxLeafSize;
        public int _minLeafSize;

        private Haller _haller;
        public Haller Haller => _haller;

        public Splitter(MapController map, int maxLeafSize, int minLeafSize)
        {
            this._maxLeafSize = maxLeafSize;
            this._Map = map;
            this._minLeafSize = minLeafSize;
        }

        public void Split()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            Queue<Leaf> _leafes = new Queue<Leaf>();
            Leaf l;

            Leaf _root = new Leaf(0, 0, this._Map._width, this._Map._height, this._minLeafSize);
            _root.DrawYourself(this._Map);
            _leafes.Enqueue(_root);
            int _iter = 1;

            while (_leafes.Count > 0)
            {
                //_counter.Content = $"{_iter}";
                Console.WriteLine($"Число комнат согласно счетчику: {_iter}");
                l = _leafes.Dequeue();
                if (l.LeftLeafs == null && l.RightLeafs == null)
                {
                    if (l.Width > _maxLeafSize || l.Height > _maxLeafSize || rnd.NextDouble() > 0.75)
                    {
                        if (l.Split(rnd) == true)
                        {
                            _leafes.Enqueue(l.LeftLeafs);
                            l.LeftLeafs.DrawYourself(this._Map);

                            _leafes.Enqueue(l.RightLeafs);
                            l.RightLeafs.DrawYourself(this._Map);

                            _iter++;
                        }
                    }
                }
            }

            Haller haller = new Haller(_iter);
            haller.ListFiller(_root, this._Map, rnd);
            haller.HallWatcher(this._Map, rnd, this._maxLeafSize);

            this._haller = haller;
        }
    }
}
