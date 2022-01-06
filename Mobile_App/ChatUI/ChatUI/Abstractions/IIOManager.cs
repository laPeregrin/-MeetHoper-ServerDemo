namespace ChatUI.Abstractions
{
    public interface IIOManager
    {
        void EnsureDirectoryCreated(string path);

        void DeleteFile(string path);

        string ReadAll(string path);

        T ReadAll<T>(string path);

        void Write<T>(string path, T data);

        void Write(string path, string data);
    }
}
