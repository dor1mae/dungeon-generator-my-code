using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dungeon_Generator.MapClasses
{
    /// <summary>
    /// Класс, который отвечает за манипуляции с нужным элементом в области перетаскивания
    /// увеличения масштаба и его уменьшения. Может быть использован с любым элементом в любом окне
    /// </summary>
    public class Zoomer
    {
        private Grid _map;
        private ScrollViewer _scrollViewer;
        private bool _isMouseDown = false;
        private Point _mouseDownPosition;
        private Point _offset;
        private KeyManager _keyManager;

        public Zoomer(Grid map, ScrollViewer scrollViewer)
        {
            _map = map;
            _scrollViewer = scrollViewer;
            _keyManager = new KeyManager(Key.LeftCtrl);

            _map.MouseDown += OnMouseDown;
            _map.MouseUp += OnMouseUp;
            _map.MouseMove += OnMouseMove;
            _map.MouseWheel += OnMouseWheel;
            _map.MouseLeave += OnMouseLeave;
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var delta = e.Delta;
            System.Console.WriteLine(delta);

            if (_keyManager.IsKeyDown && (_map.Width + delta / 10 > 0) && (_map.Height + delta / 10 > 0))
            {
                ; _scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                _scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;

                _map.Width += delta / 10;
                _map.Height += delta / 10;
            }
            else
            {
                _scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                _scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                _scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                _scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;

                var point = e.GetPosition(_map);
                _offset += point - _mouseDownPosition;

                _scrollViewer.ScrollToHorizontalOffset(-_offset.X / 10);
                _scrollViewer.ScrollToVerticalOffset(-_offset.Y / 10);
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = true;
            _mouseDownPosition = e.GetPosition(_map);
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            _isMouseDown = false;
        }
    }
}
