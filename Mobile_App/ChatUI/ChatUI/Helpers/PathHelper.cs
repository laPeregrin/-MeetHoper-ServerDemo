using System;

namespace ChatUI.Helpers
{
    public static class PathHelper
    {
        public static string ApplicationPath => Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public static string UserCredentialFolder => ApplicationPath + "/Data";
        public static string UserCredentialFile => ApplicationPath + "/cred.txt";
    }
}
