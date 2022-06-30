using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MedicioBackend.Helpers
{
    public class Helper
    {
        public static void DeleteFile(IWebHostEnvironment env,string folder,string fileName)
        {
            string path = env.WebRootPath;
            string resultPath=Path.Combine(path,folder,fileName);
            if (!System.IO.File.Exists(resultPath))
            {
                System.IO.File.Delete(resultPath);
            }

            
        }

        public enum Roles
        {
            Admin,
            Member,
            SuperAdmin
        }
    }
}
