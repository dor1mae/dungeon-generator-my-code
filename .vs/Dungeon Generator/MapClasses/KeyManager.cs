using System.Windows;
using System.Windows.Input;

namespace Dungeon_Generator.MapClasses
{
    public class KeyManager
    {
        private Window _window;
        private Key _key;
        private bool _isKeyDown = false;

        public bool IsKeyDown => _isKeyDown;

        public KeyManager(Key Key)
        {
            _window = EventBus.onGetMainWindow?.Invoke();
            _key = Key;

            _window.KeyDown += OnKeyDown;
            _window.KeyUp += OnKeyUp;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            //Console.WriteLine("Получено");

            if (e.Key == _key)
            {
                _isKeyDown = true;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == _key)
            {
                _isKeyDown = false;
            }
        }
    }
}
