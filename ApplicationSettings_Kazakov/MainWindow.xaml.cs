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

// Добавьте это, если класс Settings находится в этом namespace
using ApplicationSettings_Kazakov.Pages;

namespace ApplicationSettings_Kazakov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum pages
        {
            settings
        }

        public void OpenPages(pages _pages)
        {
            if (_pages == pages.settings)
                frame.Navigate(new Pages.Settings(this));
        }

        public MainWindow()
        {
            InitializeComponent();
            OpenPages(pages.settings);
        }
    }
}