using Google.Cloud.Firestore;
using Newtonsoft.Json;
using PersonalWebsiteBE.Core.Models;
using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.IntegrationTests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.IntegrationTests
{
    public static class InitTestData
    {
        public static async Task InitData(string gcpProjectId, string rawDbPrefix) {
            if (!gcpProjectId.Equals("personal-313620")) throw new Exception("DO NOT RUN TESTS ON OTHER PROJECTS");
            // Init db object
            var FireStoreDb = FirestoreDb.Create(gcpProjectId);
            // Run create calls on each data set.
            await FireStoreDb.CreateData<User>(UserData.Value, rawDbPrefix);
            await FireStoreDb.CreateData<Session>(SessionData.Value, rawDbPrefix);
            await FireStoreDb.CreateData<AuthActivity>(ActivityData.Value, rawDbPrefix);
        }

        public static async Task CreateData<T>(this FirestoreDb FireStoreDb, string data, string rawDbPrefix) where T : IDocument {
            // Transform back to it's original object :)
            var transformedDataList = JsonConvert.DeserializeObject<List<T>>(data);
            if (transformedDataList == null || !transformedDataList.Any()) throw new Exception("Could not parse incoming data."); // Something went wrong...
            foreach (var transformedData in transformedDataList)
            {
                // Decipher T to get full collection name
                string dbPrefix = rawDbPrefix != null ? $"{rawDbPrefix}" : "";
                CollectionReference Collection;
                string collectionName = typeof(T)?.ToString()?.Split('.')?.LastOrDefault()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(dbPrefix))
                    Collection = FireStoreDb.Collection(collectionName);
                else
                {
                    Collection = FireStoreDb.Collection(dbPrefix).Document(collectionName).Collection(collectionName);
                }

                // Switch here if there are cases where doing special operations are required
                switch (collectionName) {
                    default:
                        // Put in db or something
                        await Collection.Document(transformedData.Id).SetAsync(transformedData);
                        break;
                }
            }
        }
    }
}
