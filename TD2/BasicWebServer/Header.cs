using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BasicServerHTTPlistener
{
    class Header
    {
        private HttpListenerRequest request;
        public Header(HttpListenerRequest httpListenerRequest)
        {
            this.request = httpListenerRequest;
        }

        public void print()
        {
            System.Collections.Specialized.NameValueCollection headers = request.Headers;
            // Get each header and display each value.
            foreach (string key in headers.AllKeys)
            {
                string[] values = headers.GetValues(key);
                if (values.Length > 0)
                {
                    Console.WriteLine("The values of the {0} header are: ", key);
                    foreach (string value in values)
                    {
                        Console.WriteLine("   {0}", value);
                    }
                }
                else
                {
                    Console.WriteLine("There is no value associated with the header.");
                }
            }
        }
    }
}
