using Dungeon_Generator.MapClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dungeon_Generator
{
    /// <summary>
    /// Логика взаимодействия для EditorPage.xaml
    /// </summary>
    public partial class EditorPage : Page
    {
        public MapController _Map { get; set; }
        private Zoomer _zoomer;
        private Window _window;
        private List<MouseListener> _listeners = new List<MouseListener>();

        public EditorPage()
        {
            InitializeComponent();
            _zoomer = new Zoomer(Map, ScrollViewer);
            // EventBus.onGetMainWindow?.Invoke();
            DrawMap();
        }

        private void DrawMap()
        {
            //С помощью события прошу класс MapController выслать мне карту
            var map = EventBus.onGetMap?.Invoke();

            //Создаю согласно размеру карты новую сетку грида
            for (int i = 0; i < map.GetLength(1); i++)
            {
                Map.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < map.GetLength(0); i++)
            {
                Map.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    //Так как у нас не копия карты, а ссылка на оригинальную карту, при попытке перенести все в таком виде
                    //будет выскакивать ошибка, поэтому я создаю пустую ячейку, после чего настраиваю ее так, как она выглядит в карте
                    //и вставляю ее в новую сетку грида
                    Rectangle cell = new Rectangle();
                    cell.Fill = map[i, j].Fill;

                    Grid.SetColumn(cell, i);
                    Grid.SetRow(cell, j);

                    
                    Map.Children.Add(cell);
                    _listeners.Add(new MouseListener(cell));
                }
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void plus_button_Click(object sender, RoutedEventArgs e)
        {
            Map.Width += 75;
            Map.Height += 75;
        }

        private void minus_button_Click(object sender, RoutedEventArgs e)
        {

            Map.Width /= 1.5;
            Map.Height /= 1.5;
        }
    }
}
