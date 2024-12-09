using System;
using System.Windows;
using System.Windows.Controls;

namespace Dungeon_Generator
{
    static public class Verificator
    {
        static public bool MapGeneratorData_Verification(TextBox MaxSize, TextBox MinSize, TextBox SizeOfX, TextBox SizeOfY)
        {
            if (!Verification_SizeOfY(SizeOfY) || !Verification_SizeOfX(SizeOfX))
            {
                return false;
            }

            int _x = Convert.ToInt32(SizeOfX.Text);
            int _y = Convert.ToInt32(SizeOfY.Text);

            if (!Verification_MinSize_Rooms(MinSize, _x, _y))
            {
                return false;
            }
            if (!Verification_MaxSize_Rooms(MaxSize, MinSize, _x, _y))
            {
                return false;
            }

            int _maxsize_rooms = Convert.ToInt32(MaxSize.Text);
            int _minsize_rooms = Convert.ToInt32(MinSize.Text);

            return true;
        }

        static private bool Verification_MaxSize_Rooms(TextBox MaxSize, TextBox MinSize, int SizeOfX, int SizeOfY)
        {
            int _maxsize_rooms;
            try
            {
                _maxsize_rooms = Convert.ToInt32(MaxSize.Text);
            }
            catch
            {
                MessageBox.Show("Неверный тип данных");
                return false;
            }

            if (_maxsize_rooms <= 0 || (_maxsize_rooms > SizeOfX || _maxsize_rooms > SizeOfY) || _maxsize_rooms <= Convert.ToInt32(MinSize.Text))
            {
                MessageBox.Show("Максимальный размер комнаты не может быть меньше или равен нулю или быть больше размера сетки");
                return false;
            }
            else { return true; }
        }

        static private bool Verification_MinSize_Rooms(TextBox MinSize, int SizeOfX, int SizeOfY)
        {
            {
                int _minsize_rooms;
                try
                {
                    _minsize_rooms = Convert.ToInt32(MinSize.Text);
                }
                catch
                {
                    MessageBox.Show("Неверный тип данных");
                    return false;
                }

                if (_minsize_rooms <= 6 || (_minsize_rooms > SizeOfX || _minsize_rooms > SizeOfY))
                {
                    MessageBox.Show("Минимальный размер комнаты не может быть меньше одного или равен или быть больше размера сетки");
                    return false;
                }
                else { return true; }
            }
        }

        static private bool Verification_SizeOfX(TextBox SizeOfX)
        {
            {
                int _x;
                try
                {
                    _x = Convert.ToInt32(SizeOfX.Text);
                }
                catch
                {
                    MessageBox.Show("Неверный тип данных");
                    return false;
                }

                if (_x <= 0 || _x > 150)
                {
                    MessageBox.Show("Плоскость сетки X не может быть меньше или равна нулю или быть больше 150");
                    return false;
                }
                else { return true; }
            }
        }

        static private bool Verification_SizeOfY(TextBox SizeOfY)
        {
            {
                int _y;
                try
                {
                    _y = Convert.ToInt32(SizeOfY.Text);
                }
                catch
                {
                    MessageBox.Show("Неверный тип данных");
                    return false;
                }

                if (_y <= 0 || _y > 150)
                {
                    MessageBox.Show("Плоскость сетки X не может быть меньше или равна нулю или быть больше 150");
                    return false;
                }
                else { return true; }
            }
        }
    }
}
