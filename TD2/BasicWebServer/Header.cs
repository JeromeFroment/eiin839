using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        public NameValueCollection getHeaders()
        {
            return request.Headers;
        }

        public NameValueCollection getAcceptHeaders()
        {
            NameValueCollection headers = getHeaders();
            NameValueCollection res = new NameValueCollection();

            foreach (string key in headers.AllKeys)
            {
                // Tri possible des headers que l'on souhaite afficher
                /* if (key.Equals(HttpRequestHeader.Accept))
                {
                    res.Add(key, headers.Get(key));
                } */
                res.Add(key, headers.Get(key));
            }
            return res;
        }

        public void print(String collectionName, NameValueCollection headers)
        {
            Console.WriteLine(collectionName);
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
