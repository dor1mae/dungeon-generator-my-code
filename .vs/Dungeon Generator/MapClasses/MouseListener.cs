using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Dungeon_Generator.MapClasses
{
    public class MouseListener
    {
        private Rectangle _rect;
        private static bool _pressed = false;

        public MouseListener(Rectangle rect)
        {
            _rect = rect;

            _rect.MouseLeftButtonDown += OnMouseLeftButtonDown;
            _rect.MouseLeftButtonUp += OnMouseLeftButtonUp;
            _rect.MouseEnter += OnMouseEnter;
        }

        private void OnMouseEnter(object obj, MouseEventArgs e)
        {
			if(_pressed)
            {
                _rect.Fill = Brushes.White;
            }
        }

        private void OnMouseLeftButtonDown(object obj, MouseButtonEventArgs e)
        {
            _rect.Fill = Brushes.White;

            _pressed = true;
        }

        private void OnMouseLeftButtonUp(object obj, MouseButtonEventArgs e)
        {
            _pressed = false;
        }
    }
}
