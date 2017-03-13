using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Utilities
{
    public class FileHelper
    {

    }
    public static class FileUtils
    {
        public static Encoding UTF8_ENCODING = new UTF8Encoding(false);
        public static string[] IMAGE_EXTENSIONS = { ".jpg", ".jpeg", ".gif", ".svg", ".bmp", ".png", ".tif", ".tiff", ".jfif" };
        public static string[] HTML_EXTENSIONS = { ".htm", ".html", ".xml", ".xhtml" };

        const int BUFFER_SIZE = 4096;

        public static string[] GetFiles(this string v, string filter = "*")
        {
            return Directory.GetFiles(v, filter);
        }
        public static string[] GetAllFiles(this string v, string filter = "*")
        {
            return Directory.GetFiles(v, filter, SearchOption.AllDirectories);
        }
        public static string[] GetDirectories(this string v, string filter = "*")
        {
            return Directory.GetDirectories(v, filter);
        }
        public static string[] GetAllDirectories(this string v, string filter = "*")
        {
            return Directory.GetDirectories(v, filter, SearchOption.AllDirectories);
        }
        public static bool IsDirectory(this string v)
        {
            return Directory.Exists(v);
        }
        public static void CombineFiles(this string directory)
        {


            var target = Path.Combine(directory, ".txt");
            target.CreateDirectoryIfNotExists();
            var directories = Directory.GetDirectories(directory, "*", SearchOption.AllDirectories);

            foreach (var item in directories)
            {

                var files = Directory.GetFiles(item, "*.txt");

                var writer = new StreamWriter(Path.Combine(target, Path.GetFileName(item) + ".txt"), false, UTF8_ENCODING);

                foreach (var f in files)
                {
                    writer.Write(File.ReadAllText(f, UTF8_ENCODING));
                }
                writer.Write(Environment.NewLine + Environment.NewLine);
                writer.Close();

            }

        }
        public static string PathCombine(this string dir, string f)
        {

            return Path.Combine(dir, f);
        }
        public static bool IsFile(this string f)
        {
            return File.Exists(f);
        }

        public static string GetUniqueFileName(this string dir, string ext)
        {
            var files = Directory.GetFiles(dir, "*", SearchOption.AllDirectories).Select(i => i.GetFileNameWithoutExtension());
            var n = StringHelper.RandomString(5);


            while (files.Contains(n))
            {

            }
            return Path.Combine(dir, n + ext);
        }
        public static void CreateIfNoExist(this string dir)
        {
            if (Directory.Exists(dir)) return;
            Directory.CreateDirectory(dir);
        }

        public static async void WriteFileAsync(this string fileName, string c)
        {
            using (var stream = new FileStream(
            fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None, BUFFER_SIZE))
            {


                byte[] buffer = Encoding.UTF8.GetBytes(c);
                var t = Task.Factory.FromAsync(
                     stream.BeginWrite, stream.EndWrite, buffer, 0, buffer.Length, null);
                await t;

            }

        }
        public static IEnumerable<string> ReadFileAllLines(this string v)
        {

            return File.ReadAllLines(v, UTF8_ENCODING);
        }
        public static String FileToString(this string f, bool isUtf8 = true)
        {
            if (!File.Exists(f)) return null;
            if (isUtf8)
                return File.ReadAllText(f, UTF8_ENCODING);
            else return File.ReadAllText(f, Encoding.GetEncoding("gb2312"));
        }
        public static void StringToFile(this string t, string f, bool append = false)
        {
            if (append)
            {
                File.AppendAllText(f, t);
            }
            else
            {
                File.WriteAllText(f, t, UTF8_ENCODING);

            }
        }
        public static string GetApplicationPath(this String fileName)
        {
            return Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), fileName);
        }
        public static string GetDirectoryName(this string path)
        {
            return Path.GetDirectoryName(path);
        }
        public static string GetFileNameWithoutExtension(this string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
        public static string GetFileName(this string path)
        {
            return Path.GetFileName(path);
        }
        public static IEnumerable<string> GetFilesFilter(this string path, string pattern)
        {
            return Directory.GetFiles(path).Where(i => Regex.IsMatch(Path.GetExtension(i), pattern));
        }

        public static void CopyFile(this string fullPath, string dir, string fileName)
        {
            var target = Path.Combine(dir, fileName);
            if (fullPath == target) return;
            if (File.Exists(target))
            {
                target = Path.Combine(dir, Path.GetFileName(dir) + " " + fileName);
            }
            int count = 1;
            while (File.Exists(target))
            {
                target = Path.Combine(dir, Path.GetFileNameWithoutExtension(fileName) + " " + count.ToString().PadLeft(5, '0') + Path.GetExtension(fileName));
                count++;
            }
            File.Copy(fullPath, target);
        }
        public static void CreateDirectoryIfNotExists(this string v)
        {
            if (Directory.Exists(v)) return;

            Directory.CreateDirectory(v);
        }
        public static void DeleteFileIfExist(this string v)
        {
            if (File.Exists(v))
            {
                File.Delete(v);
            }
        }
    }
}
