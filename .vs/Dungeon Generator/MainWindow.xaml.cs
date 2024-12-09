using Dungeon_Generator.GenerationClasses;
using Dungeon_Generator.MapClasses;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Dungeon_Generator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;
        NameGenerator nameGenerator;
        MonsterGenerator mg;
        // ^уйдет после переноса в инитиализе компонент

        public static MainWindow Instance { get; private set; } // тут будет форма
        public MainWindow()
        {
            InitializeComponent();
            //На весь экран показать "Загрузка..."
            //if (Verificator.DataBaseConnection());
            //{ ApplicationContext db = new ApplicationContext();
            //NameGenerator namegenerator = new NameGenerator(db);
            //MonsterGenerator ...
            //TrapGenerator ...
            //Показывается полноценное окно
            //}

            //else MessageBox.Show("База данных не обнаружена. Пожалуйста, проверьте директорию // на наличие Dungeon.db.
            //Если файл отсутствует, переустановите программу.");
            //return 0;
            //}
            db = new ApplicationContext();
            Console.WriteLine(db.Database.Exists());
            nameGenerator = new NameGenerator(db);
            mg = new MonsterGenerator(db);

            mainFrame.Navigate(new Uri("GeneratorPage.xaml", UriKind.Relative));

            EventBus.onGetMainWindow += GetWindow;

            // Инициализация статической переменной для доступа к окну
            // и его содержимого из других классов
            Instance = this;
        }

        private void Generator_Button_Click(object sender, RoutedEventArgs e)
        {
            if (mainFrame.CanGoBack) mainFrame.Refresh();
            
            if (Verificator.MapGeneratorData_Verification(TextBox_MaxSize_Rooms, TextBox_MinSize_Rooms, TextBox_SizeOfX, TextBox_SizeOfY))
            {
                int _minsize = Convert.ToInt32(TextBox_MinSize_Rooms.Text);
                int _maxsize = Convert.ToInt32(TextBox_MaxSize_Rooms.Text);
                int _x = Convert.ToInt32(TextBox_SizeOfX.Text);
                int _y = Convert.ToInt32(TextBox_SizeOfY.Text);

                //Через EventBus посылается событие, вызывающее отработку метода SetGeneratorPageAttributes внутри класса
                EventBus.onSetGeneratorPage?.Invoke(_minsize, _maxsize, _y, _x);

                //NameGenerator.Generate(TitleOfDungeon);

                System.Console.Write("\nНачинаем новый энкаунтер...\n");
                int party_member_amount = Convert.ToInt32(TextBox_MemAmount.Text);
                int party_level = Convert.ToInt32(TextBox_PartyLevel.Text);
                MonsterOutput.Clear();
                mg.BuildEncounter(party_member_amount, party_level, MonsterOutput);
                GenerateDungeonName();
            }
        }

        private void Regenerator_Name_Button_Click(object sender, RoutedEventArgs e)
        {
            GenerateDungeonName();
        }

        private void GenerateDungeonName()
        {
            /*Random rnd = new Random();
            int rows = db.Names.Count();
            string name = db.Names.Find(rnd.Next(rows) + 1).basename;
            rows = db.NameDetails.Count();
            string details = db.NameDetails.Find(rnd.Next(rows) + 1).detail;
            TitleOfDungeon.Text = name + ' ' + details;*/
            TitleOfDungeon.Text = nameGenerator.GenerateName();
        }

        private void OnTestClick(object sender, RoutedEventArgs e)
        {
            TestWindow test = new TestWindow();
            test.Show();
        }

        private void Import_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private Window GetWindow()
        {
            return this;
        }
    }
}
