using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Generator.ServiceClasses
{
    public class ReactiveProperty<T>
    {
        private T _value;
        public Action<T> OnChange;

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnChange?.Invoke(value);
            }
        }
    }
}
