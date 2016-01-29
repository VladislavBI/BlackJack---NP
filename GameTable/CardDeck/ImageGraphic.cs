using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
namespace GameTable.CardDeck
{
    public class ImageGraphic
    {

        #region Позиция карты на большой картинке
        /// <summary>
        /// Начало катринки по х 
        /// </summary>
        Dictionary<CardHierarchy, int> XBegin = new Dictionary<CardHierarchy, int>()
       {
                { CardHierarchy.Ace, 0}, 
                { CardHierarchy.C2, 147}, 
                { CardHierarchy.C3, 295}, 
                { CardHierarchy.C4, 442}, 
                { CardHierarchy.C5, 590}, 
                { CardHierarchy.C6, 738}, 
                { CardHierarchy.C7, 885}, 
                { CardHierarchy.C8, 1033}, 
                { CardHierarchy.C9, 1181}, 
                { CardHierarchy.C10, 1328}, 
                { CardHierarchy.Jack, 1476}, 
                { CardHierarchy.Queen, 1624}, 
                { CardHierarchy.King, 1771},
       };

        /// <summary>
        /// Начало катринки по у 
        /// </summary>
        Dictionary<CardSuit, int> YBegin = new Dictionary<CardSuit, int>()
            {
                { CardSuit.clubs, 0}, 
                { CardSuit.diamonds, 214}, 
                { CardSuit.hearts, 429}, 
                { CardSuit.pikes, 643}, 
            };
        #endregion
        

        System.Drawing.Bitmap pic = new System.Drawing.Bitmap(Properties.Resources.Cards);
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

        public System.Windows.Controls.Image GetCardImage(CardFactory card)
        {
            SettingCard();
            System.Drawing.Image image2 = 
                pic.Clone(new System.Drawing.Rectangle(XBegin[card.thisCardHierarchy], 
                    YBegin[card.thisCardSuit], 148, 215), 
                    System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
            
           using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                image2.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                Image image = new Image() { Width = width, Height = height, Source = GetBitmapImage(ms.ToArray()) };
                image.Margin = new System.Windows.Thickness(5, 0, 0, 0); 
               return image;
            }
        }


    }
}