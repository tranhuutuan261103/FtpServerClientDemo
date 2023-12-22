using MyClassLibrary.Bean.Account;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encoder = System.Drawing.Imaging.Encoder;

namespace MyClassLibrary.Common
{
    public static class ImageHelper
    {
        public static byte[] CompressImage(Image image, int targetSizeKB)
        {
            using (MemoryStream originalStream = new MemoryStream())
            {
                // Lưu ảnh gốc vào MemoryStream
                image.Save(originalStream, ImageFormat.Png);

                // Kiểm tra dung lượng ảnh, nếu nhỏ hơn targetSizeKB thì không cần nén
                if (originalStream.Length <= targetSizeKB * 1024)
                {
                    return originalStream.ToArray();
                }

                // Tính toán tỉ lệ nén để đạt được dung lượng mong muốn
                double compressionRatio = (double)(targetSizeKB * 1024) / originalStream.Length;

                using (MemoryStream compressedStream = new MemoryStream())
                {
                    // Tạo một đối tượng Encoder mới với tỉ lệ nén đã tính
                    EncoderParameters encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, (long)(compressionRatio * 100));

                    // Lưu ảnh với tỉ lệ nén mới vào MemoryStream
                    image.Save(compressedStream, GetEncoderInfo("image/jpeg"), encoderParameters);

                    // Trả về mảng byte của ảnh đã nén
                    return compressedStream.ToArray();
                }
            }
        }

        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            return codecs.FirstOrDefault(codec => codec.MimeType == mimeType);
        }
    }
}
