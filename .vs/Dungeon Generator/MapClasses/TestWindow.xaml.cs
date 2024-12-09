using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Dungeon_Generator.MapClasses
{
    /// <summary>
    /// Временный класс, созданный для проверки того, где находится сгенерированная карта
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();

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
                }
            }
        }

        public void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var delta = e.Delta;
            System.Console.WriteLine(delta);

            //LayoutTransform = new ScaleTransform(1 + delta / 1000, 1 + delta / 1000);
            Map.Width += delta / 100;
            Map.Height += delta / 100;
        }
    }
}
