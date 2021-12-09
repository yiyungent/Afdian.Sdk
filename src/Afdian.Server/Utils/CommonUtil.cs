namespace Afdian.Server.Utils
{
    public class CommonUtil
    {
        public static string Version()
        {
            string version = System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(Server.Startup).Assembly.Location).ProductVersion.Split('+').First();

            return version;
        }
    }
}
