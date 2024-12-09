using Dungeon_Generator.ServiceClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Generator.ServiceClasses.Interfaces
{
    public interface IMapObject
    {
        MapObjectType Type { get; }
    }
}
