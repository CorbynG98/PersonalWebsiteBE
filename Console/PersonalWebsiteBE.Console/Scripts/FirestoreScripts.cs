using Google.Cloud.Firestore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PersonalWebsiteBE.Core.Models;
using PersonalWebsiteBE.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class FirestoreScripts
{
    public static async Task<JArray> GetDocumentsInCollectionJSONAsync<TEntity>(this IServiceProvider serviceProvider, int limit = 10) where TEntity : IDocument {
        // Init DB
        var gcSettings = serviceProvider.GetService<IFireStoreSettings>();
        var FireStoreDb = FirestoreDb.Create(gcSettings.ProjectId);
        // Build the Collection object with Root if available
        string dbPrefix = gcSettings.Root!= null ? $"{gcSettings.Root}" : "";
        CollectionReference Collection;
        string collectionName = typeof(TEntity)?.ToString()?.Split('.')?.LastOrDefault()?.Trim() ?? "";
        if (string.IsNullOrWhiteSpace(dbPrefix))
            Collection = FireStoreDb.Collection(collectionName);
        else
        {
            Collection = FireStoreDb.Collection(dbPrefix).Document(collectionName).Collection(collectionName);
        }

        // Get the documents
        var documents = await Collection.Limit(limit).GetSnapshotAsync();
        var objects = new JArray();
        if (documents.Count > 0) {
            foreach (var document in documents)
            {
                // Parse :)
                TEntity entity = document.ConvertTo<TEntity>();
                entity.Id = document.Id;
                // Put in list of objects after converting to string json
                objects.Add(JObject.FromObject(entity));
            }
        }
        return objects;
    }
}
