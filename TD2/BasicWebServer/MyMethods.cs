using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BasicServerHTTPlistener
{
    class MyMethods
    {
        private Uri uri;

        public MyMethods(Uri uri)
        {
            this.uri = uri;
        }

        public System.Collections.Specialized.NameValueCollection getParameters()
        {
            return HttpUtility.ParseQueryString(uri.Query);
        }

        public String myMethods(System.Collections.Specialized.NameValueCollection parameters)
        {
            String res = "<HTML><BODY> Hello ";
            int count = 0;
            foreach(string key in parameters.AllKeys)
            {
                string[] values = parameters.GetValues(key);
                foreach(string value in values)
                {
                    if (count == 0) res += " " + value;
                    else res += " et " + value;
                }
                count++;
            }
            res += "</BODY></HTML>";
            return res;
        }
    }
}
