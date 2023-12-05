using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml;
using System.IO.Compression;

namespace BlockChain.Common
{
    public static class DictionaryHelper
    {
        public static void Serialize(string path, IDictionary<string, string> data)
        {
            using (var file = File.Create(path))
            using (var deflate = new DeflateStream(file, CompressionMode.Compress))
            using (var writer = new BinaryWriter(deflate))
            {
                writer.Write(data.Count);
                foreach (var pair in data)
                {
                    writer.Write(pair.Key);
                    writer.Write(pair.Value);
                }
            }
        }
        public static IDictionary<string, string> Deserialize(string path)
        {
            using (var file = File.OpenRead(path))
            using (var deflate = new DeflateStream(file, CompressionMode.Decompress))
            using (var reader = new BinaryReader(deflate))
            {
                int count = reader.ReadInt32();
                var data = new Dictionary<string, string>(count);
                while (count-- > 0)
                {
                    data.Add(reader.ReadString(), reader.ReadString());
                }
                return data;
            }
        }

    }
}
