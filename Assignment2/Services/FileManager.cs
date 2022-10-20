using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.FileManagers
{
    public interface IFileManager //Här gör vi ett interface som skriver ner det till en fil i våran Filemanager
    {
        public void save(string filePath, string content);
        public string Read(string filePath);

    }

    public class FileManager : IFileManager
    {
        public string Read(string filePath)
        {
            using var sr = new StreamReader(filePath);  
            return sr.ReadToEnd();
        }

        public void save(string filePath, string content)
        {
            using var sw = new StreamWriter(filePath);
            sw.WriteLine(content);
        }
    }
}
