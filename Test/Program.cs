using System;
using System.IO;
using System.Net;

namespace Test
{

    class Program
    {
        static string ftpUsername = "linkFTP";
        static string ftpPassword = "723982nn";
        static string localFile = "test.txt";
        static void Main(string[] args)
        {
            //NewMethod();
            using (var client = new WebClient())
            {
                client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                //client.UploadFile("ftp://10.67.89.99:2221/test.txt", WebRequestMethods.Ftp.UploadFile, localFile);
                //client.UploadFile("ftp://109.252.71.16/test.txt", WebRequestMethods.Ftp.UploadFile, localFile);
                client.UploadFile("ftp://192.168.1.3/test.txt", WebRequestMethods.Ftp.UploadFile, localFile);
            }
            Console.Read();
        }

        private static void NewMethod()
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://localhost/test.htm");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("linkFTP", "723982nn");

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            Console.WriteLine(reader.ReadToEnd());

            Console.WriteLine($"Download Complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }
    }


}
