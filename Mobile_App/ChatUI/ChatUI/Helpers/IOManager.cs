using ChatUI.Abstractions;
using Newtonsoft.Json;
using System;
using System.IO;

namespace ChatUI.Helpers
{
    public class IOManager : IIOManager
    {

        public void DeleteFile(string path)
        {
            if (!File.Exists(path))
                return;

            try { File.Delete(path); } catch { }
        }

        public void EnsureDirectoryCreated(string path)
        {
            if (Directory.Exists(path) || string.IsNullOrWhiteSpace(path))
                return;

            var dir = new DirectoryInfo(path);
            if (dir.Parent != null && !dir.Parent.Exists)
            {
                EnsureDirectoryCreated(dir.Parent.FullName);
            }
            try
            {
                dir.Create();
            }
            catch { }
        }
        
        public string ReadAll(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return string.Empty;

            var result = File.ReadAllText(path);
            return result;
        }

        public T ReadAll<T>(string path)
        {
            var result = default(T);
            var line = ReadAll(path);
            if (!string.IsNullOrEmpty(line))
                result = JsonConvert.DeserializeObject<T>(line);

            return result;
        }

        public void Write(string path, string data)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));
            try
            {
                using (var stream = File.Create(path))
                {
                    if (stream.CanWrite)
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            writer.Write(data);
                        }
                    }
                }
            }
            catch { }
        }

        public void Write<T>(string path, T data) =>
           Write(path, JsonConvert.SerializeObject(data));

    }
}
