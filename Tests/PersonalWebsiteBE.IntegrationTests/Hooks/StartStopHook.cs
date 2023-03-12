using Google.Cloud.Firestore;
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Core.Settings;

namespace PersonalWebsiteBE.IntegrationTests.Hooks
{
    [Binding]
    public sealed class StartStopHook
    {
        [BeforeTestRun]
        public static void BeforeTesting() {
            var serviceProvider = Startup.ConfigureServices();
            // Init test data
            var gcpSettings = serviceProvider.GetService<IFireStoreSettings>();
            InitTestData.InitData(gcpSettings?.ProjectId ?? "", gcpSettings.Root ?? "").GetAwaiter().GetResult();
            // Setup some final data :)
            Environments.ServiceProvider = serviceProvider;
        }

        [AfterTestRun]
        public static async Task AfterTesting() {
            var clearCollections = new List<string>() {
                nameof(User), nameof(Session), nameof(AuthActivity)
            };
            // Clear all the data in this collections then delete the collection lol. Cleaning up boi.
            var googleCloudSettings = Environments.ServiceProvider.GetService<IFireStoreSettings>();
            if (googleCloudSettings == null) throw new Exception("No GCP settings to run tests with.");
            if (!googleCloudSettings.ProjectId.Equals("personal-313620")) throw new Exception("DO NOT RUN TESTS ON OTHER PROJECTS!");
            var FireStoreDb = FirestoreDb.Create(googleCloudSettings.ProjectId);
            // Clear each collection
            foreach (var collection in clearCollections)
            {
                // Generate collection reference with prefix
                string dbPrefix = googleCloudSettings.Root != null ? $"{googleCloudSettings.Root }" : "";
                CollectionReference Collection;
                if (string.IsNullOrWhiteSpace(dbPrefix))
                    Collection = FireStoreDb.Collection(collection);
                else
                    Collection = FireStoreDb.Collection(dbPrefix).Document(collection).Collection(collection);

                // Get first 100 documents (This is for batching so we don't do too much at once).
                var snapshot = await Collection.Limit(100).GetSnapshotAsync();
                IReadOnlyList<DocumentSnapshot> documents = snapshot.Documents;
                while (documents.Count > 0)
                {
                    foreach (var document in documents)
                    {
                        Console.WriteLine($"Deleting document {document.Id} from {collection}");
                        await document.Reference.DeleteAsync();
                    }
                    snapshot = await Collection.Limit(100).GetSnapshotAsync();
                    documents = snapshot.Documents;
                }
            }
        }
    }
}
