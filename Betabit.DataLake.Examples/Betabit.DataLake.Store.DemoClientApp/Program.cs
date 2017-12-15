using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Azure.DataLake.Store;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest.Azure.Authentication;

namespace Betabit.DataLake.Store.DemoClientApp
{
    class Program
    {
        #region Fields

        private static AdlsClient client;

        #endregion

        #region VERY SECRET STUFF HERE

        const string TENANT_ID = "<TENANT_ID>";
        const string ACCOUNT_FQDN = "<DATA_LAKE_STORE_ACCOUNT_FQDN>";
        const string CLIENT_ID = "<CLIENT_ID>";
        const string CLIENT_SECRET = "<CLIENT_SECRET>";

        #endregion

        static void Main(string[] args)
        {
            // Obtain AAD token
            var clientCredentials = new ClientCredential(CLIENT_ID, CLIENT_SECRET);
            var serviceClientCredentials = ApplicationTokenProvider.LoginSilentAsync(TENANT_ID, clientCredentials).GetAwaiter().GetResult();

            // Create ADLS client object
            client = AdlsClient.CreateClient(ACCOUNT_FQDN, serviceClientCredentials);
            using (var writer = new StreamWriter(client.CreateFile("/temp/helloworld.txt", IfExists.Overwrite)))
            {
                writer.WriteLine("Hello Azure Data Lake Store world!");
            }

            Console.WriteLine("All done here.");
            Console.WriteLine("Press <enter> to exit.");
            Console.ReadLine();
        }

        private static void CreateFileAndDirectory(string filename)
        {
            using (var streamWriter = new StreamWriter(client.GetAppendStream(filename)))
            {
                //upload your content here
            }
        }

        private static string ReadFile(string filename)
        {
            using (var readStream = new StreamReader(client.GetReadStream(filename)))
            {
                return readStream.ReadToEnd();
            }
        }

        private static DirectoryEntry GetProperties(string name)
        {
            return client.GetDirectoryEntry(name);
        }

        private static void RenameFile(string originalFilename, string destinationFilename)
        {
            client.Rename(originalFilename, destinationFilename, true);
        }

        private static IEnumerable<DirectoryEntry> EnumerateDirectory(string name)
        {
            return client.EnumerateDirectory(name);
        }

        private static void DeleteDirectoryRecursive(string name)
        {
            client.DeleteRecursive(name);
        }
    }
}