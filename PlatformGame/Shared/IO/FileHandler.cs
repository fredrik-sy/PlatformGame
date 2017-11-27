namespace PlatformGame.Shared.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Threading.Tasks;

    internal static class FileHandler
    {
        public static T ReadFromBinaryFile<T>(string path)
        {
            using (Stream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(stream);
            }
        }

        public static void WriteToBinaryFile<T>(string path, T objectToWrite)
        {
            using (Stream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, objectToWrite);
            }
        }

        public static void DeleteFiles(string path, string searchPattern)
        {
            string[] fileNames = Directory.GetFiles(path, searchPattern);

            foreach (string fileName in fileNames)
            {
                File.Delete(fileName);
            }
        }
    }
}
