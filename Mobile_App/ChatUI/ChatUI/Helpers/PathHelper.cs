using System;

namespace ChatUI.Helpers
{
    public static class PathHelper
    {
        public const string DefaultImageUrl = "https://cdn.business2community.com/wp-content/uploads/2017/08/blank-profile-picture-973460_640.png";
        public static string ApplicationPath => Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public static string UserCredentialFolder => ApplicationPath + "/Data";
        public static string UserCredentialFile => ApplicationPath + "/cred.txt";
    }
}
