using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PersonalWebsiteBE.Core.Settings;

namespace PersonalWebsiteBE.Services.Helpers.GoogleCloud
{
    public class GoogleStorageHelper
    {
        private readonly IFireStoreSettings googleCloudConfig;
        private readonly StorageClient storageClient;

        public GoogleStorageHelper(IFireStoreSettings googleCloudConfig) {
            this.googleCloudConfig = googleCloudConfig;
            // Create the client, using environment variables
            storageClient = StorageClient.Create();
        }

        public string CreateStorageBucket(string bucketName) {
            // Create the bucket
            Bucket bucket = new Bucket()
            {
                Location = googleCloudConfig.DataLocation,
                StorageClass = googleCloudConfig.StorageClass,
                Name = bucketName
            };

            // Send this data on to google :)
            Bucket newBucket;
            try
            {
                newBucket = storageClient.CreateBucket(googleCloudConfig.ProjectId, bucket);
            }
            catch (GoogleApiException ex) {
                if (ex.Error.Code == 409) return null; // Already created
                throw;
            }
            return newBucket.Name;
        }

        public string UploadFileToBucket(byte[] fileData, string bucketName, string objectName)
        {
            // Convert byte array to memory stream
            MemoryStream memStream = new MemoryStream();
            memStream.Write(fileData, 0, fileData.Length);

            // Upload the memory stream
            var fileObject = storageClient.UploadObject(bucketName, objectName, null, memStream);

            // Return the public read url for this file
            return fileObject.MediaLink;
        }

        public string UploadFileToBucket(IFormFile file, string bucketName, string objectName) {
            // Convert content to file stream
            MemoryStream memStream = new MemoryStream();
            file.CopyTo(memStream);

            // Upload the file stream
            var fileObject = storageClient.UploadObject(bucketName, objectName, null, memStream);

            // Setup some permissions so this file can be accessed publicly
            fileObject.Acl = fileObject.Acl ?? new List<ObjectAccessControl>();
            storageClient.UpdateObject(fileObject, new UpdateObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead });

            // Return the public read url for this file
            return fileObject.MediaLink;
        }

        public void RemoveObjectFromBucket(string bucketName, string objectName) {
            storageClient.DeleteObject(bucketName, objectName);
        }
    }
}
