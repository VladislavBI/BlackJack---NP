using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace GameTable.CardDeck
{
    public partial class GameTableWindow
    {

        System.Drawing.Bitmap pic = new System.Drawing.Bitmap("cards.png");
        int sizeCard;
        double coef;
        int width;
        int height;

        private void SettingCard()
        {
            sizeCard = 100;


            coef = 148.0 / 215.0;
            width = (int)(sizeCard * coef);
            height = sizeCard;

        }
        private BitmapImage GetBitmapImage(byte[] imageBytes)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new System.IO.MemoryStream(imageBytes);
            bitmapImage.EndInit();
            return bitmapImage;
        }
        private byte[] BitmapImageToByteArray(BitmapImage bitmapImage)
        {
            System.IO.MemoryStream stream = (System.IO.MemoryStream)bitmapImage.StreamSource;
            byte[] data = stream.ToArray();
            return data;
        }

       /* private void AddCard(Card card)
        {
            SettingCard();
            System.Drawing.Image image2 = pic.Clone(new System.Drawing.Rectangle(card.BeginX, card.BeginY, 148, 215), System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                image2.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                Image image = new Image() { Width = width, Height = height, Source = GetBitmapImage(ms.ToArray()) };
                wpCards.Children.Add(image);
            }
        }*/


    }
}