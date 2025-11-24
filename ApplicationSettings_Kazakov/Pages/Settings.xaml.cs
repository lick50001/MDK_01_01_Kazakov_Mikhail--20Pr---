using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using WinForms = System.Windows.Forms;

namespace ApplicationSettings_Kazakov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        private MainWindow mainWindow;

        public Settings(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;

            if (mainWindow != null)
            {
                tb_fontPreview.Text = mainWindow.FontFamily?.Source ?? "Segoe UI";
            }
        }

        private void OpenDataBase(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = @"c:\",
                Filter = "Access files (*.accdb)|*.accdb|All files (*.*)|*.*",
                FilterIndex = 2
            };

            if (dialog.ShowDialog() == true)
                tb_database.Text = dialog.FileName;
        }

        private void SelectScreenResolution(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is TextBlock textBlock)
            {
                string resolution = textBlock.Text;
                var parts = resolution.Split(new string[] { " x " }, StringSplitOptions.None);

                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int width) &&
                    int.TryParse(parts[1], out int height))
                {
                    mainWindow.Width = width;
                    mainWindow.Height = height;
                }
            }
        }

        private void SelectColorApplication(object sender, RoutedEventArgs e)
        {
            using (var colorDialog = new WinForms.ColorDialog())
            {
                colorDialog.AllowFullOpen = true;
                colorDialog.ShowHelp = false;

                if (colorDialog.ShowDialog() == WinForms.DialogResult.OK)
                {
                    var sysColor = colorDialog.Color;
                    var wpfColor = Color.FromArgb(sysColor.A, sysColor.R, sysColor.G, sysColor.B);
                    var brush = new SolidColorBrush(wpfColor);

                    gr_header.Background = brush;
                    gr_application.Fill = brush;
                }
            }
        }

        private void SelectColorText(object sender, RoutedEventArgs e)
        {
            using (var colorDialog = new WinForms.ColorDialog())
            {
                if (colorDialog.ShowDialog() == WinForms.DialogResult.OK)
                {
                    var sysColor = colorDialog.Color;
                    var wpfColor = Color.FromArgb(sysColor.A, sysColor.R, sysColor.G, sysColor.B);
                    var brush = new SolidColorBrush(wpfColor);

                    mainWindow.Foreground = brush;
                    gr_textColor.Fill = brush;
                }
            }
        }

        private void SelectFonts(object sender, RoutedEventArgs e)
        {
            using (var fontDialog = new WinForms.FontDialog())
            {
                fontDialog.ShowColor = true;
                fontDialog.ShowEffects = true;

                if (fontDialog.ShowDialog() == WinForms.DialogResult.OK)
                {
                    var fontFamily = new FontFamily(fontDialog.Font.Name);
                    var fontSize = fontDialog.Font.Size * 96.0 / 72.0;
                    var fontWeight = fontDialog.Font.Bold ? FontWeights.Bold : FontWeights.Normal;
                    var fontStyle = fontDialog.Font.Italic ? FontStyles.Italic : FontStyles.Normal;

                    tb_fontPreview.FontFamily = fontFamily;
                    tb_fontPreview.FontSize = fontSize;
                    tb_fontPreview.FontWeight = fontWeight;
                    tb_fontPreview.FontStyle = fontStyle;

                    tb_fontPreview.Text = fontDialog.Font.Name;
                }
            }
        }
    }
}