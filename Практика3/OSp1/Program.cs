using System;
using System.IO;
using System.ComponentModel;
using System.Text.Json;
using System.Collections.Generic;
using System.Xml;
using System.IO.Compression;

namespace L1
{
    class Program
    {

        private static void Remove(string FilePath)
        {
            try
            {
                File.Delete(FilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine($"File {FilePath} was successfully deleted");
            }
        }
        

        static void Main(string[] args)
        {
            string FilePath = Path.Combine(Environment.CurrentDirectory, "test");
            var partitions = DriveInfo.GetDrives();
            foreach (var p in partitions)
            {

                try
                {
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(p))
                    {
                        Console.WriteLine($"{descriptor.Name}: {descriptor.GetValue(p)}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            Console.WriteLine(".....................................................................");


            Console.WriteLine("\n");
            FilePath += ".txt";

            Console.WriteLine("Введите текст");
            string c = Console.ReadLine();
            using (StreamWriter sw = File.CreateText(FilePath))
            {
                sw.Write(c);
            }
            

            using (StreamReader sr = File.OpenText(FilePath))
            {
                Console.WriteLine($"Прочитано: {sr.ReadToEnd()}");
            }
            Console.WriteLine(".....................................................................");
            Remove(FilePath);


            Console.WriteLine("\n");
            FilePath += ".json";

            Dictionary<string, string> example = new Dictionary<string, string>();
            example.Add("a", "1");
            example.Add("b", "2");
            example.Add("c", "3");

            string serialized = JsonSerializer.Serialize(example);
            Console.WriteLine($"Обьект: {serialized}");

            using (StreamWriter sw = File.CreateText(FilePath))
            {
                sw.Write(serialized);
            }
            

            Dictionary<string, string> loaded;
            using (StreamReader sr = File.OpenText(FilePath))
            {
                loaded = JsonSerializer.Deserialize<Dictionary<string, string>>(sr.ReadToEnd());
            }
            
            Remove(FilePath);
            Console.WriteLine(".....................................................................");


            
            FilePath += ".xml";

            Console.WriteLine("Введите текст:");
            string g = Console.ReadLine();
            using (StreamWriter sw = File.CreateText(FilePath))
            {
                using (XmlWriter xw = XmlWriter.Create(sw))
                {
                    xw.WriteStartDocument();
                    xw.WriteStartElement("field");
                    xw.WriteString(g);
                    xw.WriteEndElement();
                    xw.WriteEndDocument();
                }
            }
            

            using (StreamReader sr = File.OpenText(FilePath))
            {
                using (XmlReader xr = XmlReader.Create(sr))
                {
                    xr.MoveToContent();
                    Console.WriteLine($"Прочитано: {xr.ReadElementContentAsString()}");
                }
            }

            Remove(FilePath);


            Console.WriteLine(".....................................................................");


            
            FilePath += ".zip";

            List<string> files = new List<string>() { "A.txt", "B.txt", "C.txt" };

            using (ZipArchive archive = new ZipArchive(File.OpenWrite(FilePath), ZipArchiveMode.Create))
            {
                foreach (string file in files)
                {
                    string path = Path.Combine(Environment.CurrentDirectory, file);
                    using (StreamWriter sw = File.CreateText(path))
                        sw.Write(file);
                    FileInfo info = new FileInfo(path);

                    archive.CreateEntryFromFile(info.FullName, info.Name, CompressionLevel.Optimal);
                }
            }
            Console.WriteLine("архив создан");
            Console.WriteLine($"Путь к архиву: {FilePath}");
            using (ZipArchive archive = ZipFile.OpenRead(FilePath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    Console.WriteLine($"Имя: {entry.Name}");
                    Console.WriteLine($"\tВремяe: {entry.LastWriteTime}");
                    Console.WriteLine($"\tВнешние аттрибуты: {entry.ExternalAttributes}");
                    Console.WriteLine($"\tСжатый обьем : {entry.CompressedLength}");
                    Console.WriteLine($"\tобьем: {entry.Length}");
                }
            }
        }
    }
}
