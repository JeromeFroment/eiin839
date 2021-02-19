using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;
using System.IO;

namespace BasicServerHTTPlistener
{
    class MyMethods
    {
        private Uri uri;
        private static int cpt = 0;

        public MyMethods(Uri uri)
        {
            this.uri = uri;
        }

        public System.Collections.Specialized.NameValueCollection getParameters()
        {
            return HttpUtility.ParseQueryString(uri.Query);
        }

        public String myMethods()
        {
            String res = "<h2>Méthode avec paramètre en URL</h2>";
            int count = 0;
            foreach (string key in getParameters().AllKeys)
            {
                string[] values = getParameters().GetValues(key);
                foreach (string value in values)
                {
                    res += "<p>" + (count + 1).ToString() + "e param : " + value + "</p>";
                }
                count++;
            }
            return res;
        }

        public String myMethodsWithExe()
        {
            Console.WriteLine("Helloooooooo");
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Users\user\Documents\GitHub\eiin839\TD2\ExecTest\bin\Debug\ExecTest.exe"; // Specify exe name.
            start.Arguments = "Argument1 Argument2"; // Specify arguments.
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            //
            // Start the process.
            //
            using (Process process = Process.Start(start))
            {
                //
                // Read in all the text from the process with the StreamReader.
                //
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.WriteLine(result);
                    return result;
                }
            }

        }

        public string incrementReload()
        {
            cpt++;
            return "<h2>Nombre de reload : " + cpt + "</h2>";
        }
    }
}