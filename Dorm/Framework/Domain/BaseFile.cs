using System.IO;

namespace MyFramework.Domain
{
    public class BaseFile : Entity
    {
        public string Title { get; set; }
        public new string Name { get; set; }
        public string Type { get; set; }
        public byte[] Content { get; set; }
        public int Length { get; set; }
        public string SavePath { get; set; }

        public string GetSize()
        {
            if (Content == null)
                return "0 KB";
            return string.Format("{0:N0} KB", Length/1024);
        }
    }

    public static class FileHelp
    {
        public static byte[] ToBytes(this Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
    }
}