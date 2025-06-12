using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            MessageBox.Text += "😀"; // вставка эмодзи
        }

        private void ShowStickerPicker_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Text += "[Стикер]"; // вставка стикера (заглушка)
        }

        private void AttachFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Выберите файл";
            if (dialog.ShowDialog() == true)
            {
                string fileName = System.IO.Path.GetFileName(dialog.FileName);
                MessageBox.Text += $"[Файл: {fileName}]";
                // В дальнейшем здесь можно добавить отправку файла по сети
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageBox.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                AddMessageBubble("Вы", message, true);
                MessageBox.Clear();
            }
        }

        private void AddMessageBubble(string senderName, string message, bool isOwn)
        {
            var bubble = new Border
            {
                Background = isOwn ? Brushes.LightBlue : Brushes.LightGray,
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(10),
                Margin = new Thickness(5),
                MaxWidth = 250,
                HorizontalAlignment = isOwn ? HorizontalAlignment.Right : HorizontalAlignment.Left,
                Child = new TextBlock
                {
                    Text = $"{senderName}: {message}",
                    TextWrapping = TextWrapping.Wrap,
                    Foreground = Brushes.Black
                }
            };

            ChatPanel.Children.Add(bubble);
        }
    }
}
