using Dungeon_Generator.MapClasses;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Dungeon_Generator
{
    public partial class GeneratorPage : Page
    {
        public int MinSizeRooms { get; set; }
        public int MaxSizeRooms { get; set; }
        public int CellsHeight { get; set; }
        public int CellsWidth { get; set; }
        public MapController _Map { get; set; }
        private Zoomer _zoomer;
        private MapObjectsManager _mapObjectsManager;
        private List<MouseListener> _listeners = new List<MouseListener>();


        // private List<MouseListener> _listeners = new List<MouseListener>();

        public GeneratorPage()
        {
            InitializeComponent();
            _zoomer = new Zoomer(Map, ScrollViewer);
            Console.WriteLine($"{GetType()}: initialized");
            EventBus.onSetGeneratorPage += SetGeneratorPageAttributes;
        }

        public void SetGeneratorPageAttributes(int minSizeRooms, int maxSizeRooms, int cellsHeight, int cellsWidth)
        {
            MinSizeRooms = minSizeRooms;
            MaxSizeRooms = maxSizeRooms;
            CellsHeight = cellsHeight;
            CellsWidth = cellsWidth;
            Console.WriteLine($"{GetType()}: StartSplit");
            StartSplit();
        }

        public void StartSplit()
        {
            DrawCells();
            this._Map._Splitter.Split();

            //Создается то, что будет руководить всеми данными карты
            _mapObjectsManager = new MapObjectsManager(this._Map._map, this._Map._Splitter.Haller.GetRoomList);

        }

        public void DrawCells()
        {
            if (Map.Children != null)
            {
                Map.Children.Clear();
                Map.RowDefinitions.Clear();
                Map.ColumnDefinitions.Clear();
            }

            this._Map = new MapController(this.CellsHeight, this.CellsWidth, this.MaxSizeRooms, this.MinSizeRooms);
            this._Map._map = new Rectangle[this.CellsHeight, this.CellsWidth];

            for (int i = 0; i < CellsHeight; i++)
            {
                Map.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < CellsWidth; i++)
            {
                Map.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < CellsHeight; i++)
            {
                for (int j = 0; j < CellsWidth; j++)
                {
                    this._Map._map[i, j] = new Rectangle()
                    {
                        Name = $"Cell_column{i + 1}_row{j + 1}",
                        Fill = Brushes.Black
                    };
                    Grid.SetColumn(this._Map._map[i, j], i);
                    Grid.SetRow(this._Map._map[i, j], j);
                    Map.Children.Add(this._Map._map[i, j]);
                    _listeners.Add(new MouseListener(this._Map._map[i, j]));
                }
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            // Кнопка "сохранить"

            // Export export = new Export();
            // export.Show();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            // Кнопка "редактировать"

            // Смена генератора на редактор с использованием статической переменной главного окна

            if (MainWindow.Instance != null)
            {
                MainWindow.Instance.mainFrame.Navigate(new Uri("EditorPage.xaml", UriKind.Relative));
            }
        }
    }
}
