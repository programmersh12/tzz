using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChatSecureClient11212
{
    public partial class StickerPicker : Window
    {
        public string SelectedStickerPath { get; private set; }

        public StickerPicker()
        {
            InitializeComponent();
            LoadStickers();
        }

        private void LoadStickers()
        {
            string stickerFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Stickers");
            if (!Directory.Exists(stickerFolder)) return;

            foreach (var file in Directory.GetFiles(stickerFolder))
            {
                var image = new Image
                {
                    Source = new BitmapImage(new Uri(file)),
                    Width = 64,
                    Height = 64,
                    Margin = new Thickness(5)
                };

                var button = new Button
                {
                    Content = image,
                    Tag = file,
                    Padding = new Thickness(0),
                    BorderThickness = new Thickness(0)
                };

                button.Click += (s, e) =>
                {
                    SelectedStickerPath = (string)((Button)s).Tag;
                    DialogResult = true;
                    Close();
                };

                StickerPanel.Children.Add(button);
            }
        }
    }
}
