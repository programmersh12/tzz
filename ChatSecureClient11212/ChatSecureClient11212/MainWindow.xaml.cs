using System.Windows;
using Microsoft.Win32;

namespace ChatSecureClient11212
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowEmojiPopup_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Text += "😀"; // заглушка: вставка эмодзи
        }

        private void ShowStickerPicker_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Text += "[Стикер]"; // заглушка: вставка стикера
        }

        private void AttachFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Выберите файл";
            if (dialog.ShowDialog() == true)
            {
                string fileName = System.IO.Path.GetFileName(dialog.FileName);
                MessageBox.Text += $"[Файл: {fileName}]";
                // Здесь можно сохранить путь и отправить файл через сеть
            }
        }
    }
}
