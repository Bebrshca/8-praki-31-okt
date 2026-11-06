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

namespace _8prak
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Random rnd = new Random();
        private readonly List<Warrior> warriors = new List<Warrior>();
        private readonly Dictionary<Warrior, Canvas> figures = new Dictionary<Warrior, Canvas>();
        private readonly Dictionary<Warrior, Rectangle> healthBars = new Dictionary<Warrior, Rectangle>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAddLight_Click(object sender, RoutedEventArgs e)
        {
            AddWarrior(new LightArmorWarrior("Лёгкий воин №" + (warriors.Count + 1), 100),
                       Brushes.DodgerBlue);
        }

        private void BtnAddHeavy_Click(object sender, RoutedEventArgs e)
        {
            AddWarrior(new HeavyArmorWarrior("Тяжёлый воин №" + (warriors.Count + 1), 100),
                       Brushes.Gray);
        }

        private void BtnDamage_Click(object sender, RoutedEventArgs e)
        {
            foreach (Warrior warrior in warriors)
            {
                if (!warrior.IsAlive) continue;

                int damage = rnd.Next(10, 31);
                warrior.TakeDamage(damage);

                Log.Items.Add(string.Format("{0}: урон {1}, доспех погасил часть, прошло {2} (коэф. {3:0.0}). Осталось {4} HP.",
                    warrior.Name, damage, warrior.LastDamage, warrior.ArmorFactor, warrior.Health));
            }

            if (Log.Items.Count > 0)
                Log.ScrollIntoView(Log.Items[Log.Items.Count - 1]);

            RefreshView();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            warriors.Clear();
            figures.Clear();
            healthBars.Clear();
            Battlefield.Children.Clear();
            Log.Items.Clear();
        }

        private void AddWarrior(Warrior warrior, Brush bodyColor)
        {
            warriors.Add(warrior);

            Canvas figure = new Canvas { Width = 44, Height = 78 };

            Rectangle barBack = new Rectangle { Width = 44, Height = 6, Fill = Brushes.DarkGray, RadiusX = 2, RadiusY = 2 };
            Rectangle bar = new Rectangle { Width = 44, Height = 6, Fill = Brushes.Green, RadiusX = 2, RadiusY = 2 };
            Canvas.SetLeft(barBack, 0); Canvas.SetTop(barBack, 0);
            Canvas.SetLeft(bar, 0); Canvas.SetTop(bar, 0);

            Ellipse head = new Ellipse { Width = 20, Height = 20, Fill = Brushes.Wheat, Stroke = Brushes.Black, StrokeThickness = 1 };
            Canvas.SetLeft(head, 12); Canvas.SetTop(head, 8);

            Rectangle body = new Rectangle { Width = 32, Height = 44, Fill = bodyColor, Stroke = Brushes.Black, StrokeThickness = 1, RadiusX = 5, RadiusY = 5 };
            Canvas.SetLeft(body, 6); Canvas.SetTop(body, 28);

            figure.Children.Add(barBack);
            figure.Children.Add(bar);
            figure.Children.Add(head);
            figure.Children.Add(body);

            Canvas.SetLeft(figure, rnd.Next(0, 716));
            Canvas.SetTop(figure, rnd.Next(0, 250));

            figures[warrior] = figure;
            healthBars[warrior] = bar;

            Battlefield.Children.Add(figure);
            RefreshView();
        }

        private void RefreshView()
        {
            foreach (Warrior warrior in warriors)
            {
                double part = (double)warrior.Health / warrior.MaxHealth;

                Rectangle bar = healthBars[warrior];
                bar.Width = 44 * part;
                bar.Fill = part > 0.5 ? Brushes.Green : (part > 0.25 ? Brushes.Orange : Brushes.Red);

                figures[warrior].Opacity = warrior.IsAlive ? 1.0 : 0.35;
                figures[warrior].ToolTip = string.Format("{0}\nЖизни: {1}/{2}\nКоэффициент доспеха: {3:0.0}",
                    warrior.Name, warrior.Health, warrior.MaxHealth, warrior.ArmorFactor);
            }
        }
    }
}
