using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Debug = System.Console;

namespace Dungeon_Generator.MapClasses
{
    public class MapObjectsManager
    {
        private Rectangle[,] _map;
        private List<Room> _rooms;

        public Func<Rectangle[,]> OnGetMap;
        public Func<List<Room>> OnGetRooms;
        public Func<Marker, Room> OnGetRoom;
        public Action<Room> OnAddRoom;
        public Action<Marker> OnRemoveRoom;

        public MapObjectsManager(Rectangle[,] map, Room[] rooms)
        {
            _map = map;
            _rooms = rooms.ToList();

            OnGetRoom += GetRoom;
            OnGetMap += GetMap;
            OnGetRooms += GetRooms;
            OnAddRoom += AddRoom;
            OnRemoveRoom += RemoveRoom;

            Debug.WriteLine($"{_rooms.Count} получено из Haller");
            SetMarkers();
        }

        private Room GetRoom(Marker marker)
        {
            return _rooms.Find(r => (r.Marker.x == marker.x) && (r.Marker.y == marker.y));
        }

        private List<Room> GetRooms() 
        {
            return _rooms;
        }

        private Rectangle[,] GetMap() 
        {
            return _map;
        }

        private void AddRoom(Room room)
        {
            room.DrawYourMarker(_map);
            _rooms.Add(room);
        }

        private void RemoveRoom(Marker marker)
        {
            _rooms.Remove(_rooms.Find(r => (r.Marker.x == marker.x) && (r.Marker.y == marker.y)));
        }

        private void SetMarkers()
        {
            foreach (Room room in _rooms) 
            {
                room.DrawYourMarker(_map);
            }
        }
    }
}
