﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;

namespace BasicServerHTTPlistener
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            //if HttpListener is not supported by the Framework
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
                return;
            }


            // Create a listener.
            HttpListener listener = new HttpListener();

            // Add the prefixes.
            if (args.Length != 0)
            {
                foreach (string s in args)
                {
                    listener.Prefixes.Add(s);
                    // don't forget to authorize access to the TCP/IP addresses localhost:xxxx and localhost:yyyy 
                    // with netsh http add urlacl url=http://localhost:xxxx/ user="Tout le monde"
                    // and netsh http add urlacl url=http://localhost:yyyy/ user="Tout le monde"
                    // user="Tout le monde" is language dependent, use user=Everyone in english 

                }
            }
            else
            {
                Console.WriteLine("Syntax error: the call must contain at least one web server url as argument");
            }
            listener.Start();

            // get args 
            foreach (string s in args)
            {
                Console.WriteLine("Listening for connections on " + s);
            }

            // Trap Ctrl-C on console to exit 
            Console.CancelKeyPress += delegate {
                // call methods to close socket and exit
                listener.Stop();
                listener.Close();
                Environment.Exit(0);
            };


            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }
                if (!request.Url.LocalPath.Equals("/favicon.ico"))
                {
                    // get url 
                    Console.WriteLine($"Received request for {request.Url}");

                    //get url protocol
                    Console.WriteLine(request.Url.Scheme);
                    //get user in url
                    Console.WriteLine(request.Url.UserInfo);
                    //get host in url
                    Console.WriteLine(request.Url.Host);
                    //get port in url
                    Console.WriteLine(request.Url.Port);
                    //get path in url 
                    Console.WriteLine(request.Url.LocalPath);

                    // parse path in url 
                    foreach (string str in request.Url.Segments)
                    {
                        Console.WriteLine(str);
                    }

                    //get params un url. After ? and between &

                    Console.WriteLine(request.Url.Query);

                    //parse params in url
                    Console.WriteLine("param1 = " + HttpUtility.ParseQueryString(request.Url.Query).Get("param1"));
                    Console.WriteLine("param2 = " + HttpUtility.ParseQueryString(request.Url.Query).Get("param2"));
                    Console.WriteLine("param3 = " + HttpUtility.ParseQueryString(request.Url.Query).Get("param3"));
                    Console.WriteLine("param4 = " + HttpUtility.ParseQueryString(request.Url.Query).Get("param4"));

                    //
                    Console.WriteLine(documentContents);

                    // Obtain a response object.
                    HttpListenerResponse response = context.Response;

                    // Question 1 : Header - Affiche tous les headers dans la console lors d'une requête
                    Header header = new Header(request);
                    Console.WriteLine("Headers : \n");
                    header.print("Accept Headers", header.getAcceptHeaders());

                    /* Question 4 : retourne un contenu HTML variable selon les valeurs de paramètres mis dans l'url */
                    // MyMethods myMethods = new MyMethods(request.Url);
                    // string responseString = myMethods.myMethods();

                    /* Question 6 : réflexion -> Appelle la méthode adéquat selon l'URL (si .../myMethods appelle myMethods) */
                    Type type = typeof(MyMethods);
                    MethodInfo method = type.GetMethod(request.Url.LocalPath.Substring(1));
                    string responseString = "<!DOCTYPE html><meta http-equiv=\"Content-Type\" content=\"text/html; charset = UTF-16\"/><HTML><BODY> <h1>Hello there !</h1>";
                    MyMethods c = new MyMethods(request.Url);
                    responseString += c.incrementReload();
                    responseString += "<p>http://localhost:8080/myMethodsWithExe</p>";
                    responseString += "<p>http://localhost:8080/myMethods?param1=alex" + "&amp" + "param2 =nicaise</p>";
                    if (method != null)
                    {
                        responseString += (string)method.Invoke(c, null);
                    }
                    responseString += "</body></html>";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();
                }
            }
            // Httplistener neither stop ... But Ctrl-C do that ...
            // listener.Stop();
        }
    }
}