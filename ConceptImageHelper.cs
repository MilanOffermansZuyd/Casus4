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

        public static byte[]? CombinePhotos(List<byte[]> fotos)
        {
            if (fotos is null)
            {
                return null;
            }

            using (var stream = new MemoryStream())
            {
                foreach (var foto in fotos)
                {
                    var lengteBytes = BitConverter.GetBytes(foto.Length);
                    stream.Write(lengteBytes, 0, 4);
                    stream.Write(foto, 0, foto.Length);
                }
                return stream.ToArray();
            }
        }

        public static List<byte[]>? SplitPhotos(byte[] combinedBytes)
        {
            if (combinedBytes is null)
            {
                return null;
            }

            var fotos = new List<byte[]>();

            using (var stream = new MemoryStream(combinedBytes))
            using (var reader = new BinaryReader(stream))
            {
                while (stream.Position < stream.Length)
                {
                    int lengte = reader.ReadInt32();
                    byte[] foto = reader.ReadBytes(lengte);
                    fotos.Add(foto);
                }
            }
            return fotos;
        }
    }
}