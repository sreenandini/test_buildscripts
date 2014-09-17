using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace BMC.Common.Compression
{
    public static class CompressionHelper
    {
        public static string Compress(string plainText)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(plainText);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (GZipStream zip = new GZipStream(memoryStream, CompressionMode.Compress, true))
                    zip.Write(buffer, 0, buffer.Length);

                memoryStream.Position = 0;
                MemoryStream outStream = new MemoryStream();

                byte[] compressed = new byte[memoryStream.Length];
                memoryStream.Read(compressed, 0, compressed.Length);

                byte[] gzBuffer = new byte[compressed.Length + 4];
                Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);

                Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
                return Convert.ToBase64String(gzBuffer);
            }
        }

        public static string Decompress(string compressedText)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedText);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                memoryStream.Write(gzBuffer, 4, gzBuffer.Length - 4);

                byte[] buffer = new byte[msgLength];

                memoryStream.Position = 0;

                using (GZipStream zip = new GZipStream(memoryStream, CompressionMode.Decompress))
                    zip.Read(buffer, 0, buffer.Length);

                return Encoding.UTF8.GetString(buffer);
            }

        }

        public static string DeflateCompress(string plainText)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(plainText);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (DeflateStream deflate = new DeflateStream(memoryStream, CompressionMode.Compress, true))
                    deflate.Write(buffer, 0, buffer.Length);

                memoryStream.Position = 0;
                MemoryStream outStream = new MemoryStream();

                byte[] compressed = new byte[memoryStream.Length];
                memoryStream.Read(compressed, 0, compressed.Length);

                byte[] gzBuffer = new byte[compressed.Length + 4];
                Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);

                Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
                return Convert.ToBase64String(gzBuffer);
            }
        }

        public static string DeflateDecompress(string compressedText)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedText);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                memoryStream.Write(gzBuffer, 4, gzBuffer.Length - 4);

                byte[] buffer = new byte[msgLength];

                memoryStream.Position = 0;

                using (DeflateStream deflate = new DeflateStream(memoryStream, CompressionMode.Decompress))
                    deflate.Read(buffer, 0, buffer.Length);

                return Encoding.UTF8.GetString(buffer);
            }

        }
    }
}
