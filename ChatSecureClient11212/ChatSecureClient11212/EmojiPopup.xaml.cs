using System;
using System.Windows;
using System.Windows.Controls;

namespace ChatSecureClient11212
{
    public partial class EmojiPopup : Window
    {
        public string SelectedEmoji { get; private set; }

        public EmojiPopup()
        {
            InitializeComponent();
        }

        private void Emoji_Click(object sender, RoutedEventArgs e)
        {
            SelectedEmoji = (sender as Button)?.Content.ToString();
            DialogResult = true;
            Close();
        }
    }
}
