using System.IO;
using System.Windows.Media.Imaging;

namespace Casus4
{
    public static class ConceptImageHelper
    {

        public static byte[] ImageSourceToByteArray(BitmapImage bitmapImage)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }
    }
}