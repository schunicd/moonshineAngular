using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheMoonshineCafe.Models;

namespace TheMoonshineCafe.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _client;

        public S3Service(IAmazonS3 client)
        {
            _client = client;
        }

        public async Task<S3Response> CreateBucketAsync(string bucketName)
        {
            try
            {
                if (await AmazonS3Util.DoesS3BucketExistV2Async(_client, bucketName) == false)
                {
                    var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        UseClientRegion = true
                    };

                    var response = await _client.PutBucketAsync(putBucketRequest);

                    return new S3Response
                    {
                        Message = response.ResponseMetadata.RequestId,
                        Status = response.HttpStatusCode
                    };
                }
            }
            catch (AmazonS3Exception e)
            {
                return new S3Response
                {
                    Message = e.Message,
                    Status = e.StatusCode
                };
            }
            catch (Exception e)
            {
                return new S3Response
                {
                    Message = e.Message,
                    Status = System.Net.HttpStatusCode.InternalServerError
                };
            }

            return new S3Response
            {
                Message = "Something went wrong.",
                Status = System.Net.HttpStatusCode.InternalServerError
            };
        }

        private const string BucketName = "moonshinephotostest";
        private const string FilePath = "C:\\Users\\derek\\Desktop\\band.jpg"; //Option 1
        private const string UploadWithKeyName = "UploadWithKeyName"; //Option 2
        private const string FileStreamUpload = "FileStreamUpload"; //Option 3
        private const string AdvancedUpload = "AdvancedUpload"; //Option 4

        public async Task UploadFileAsync(IFormFile file)
        {
            try
            {

                var transferRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = file.OpenReadStream(),
                    AutoCloseStream = false,
                    BucketName = BucketName,
                    Key = file.FileName,
                    StorageClass = S3StorageClass.Standard
                };

                transferRequest.Metadata.Add("Date-UTC-Uploaded", DateTime.UtcNow.ToString());

                await new TransferUtility(_client).UploadAsync(transferRequest);

                //var fileTransferUtility = new TransferUtility(_client);

                //Option 1
                //await fileTransferUtility.UploadAsync("C:\\fakepath\\" + file.FileName, BucketName);

                /*
                //Option 2
                await fileTransferUtility.UploadAsync(FilePath, bucketName, UploadWithKeyName);

                //Option 3
                using (var fileToUpload = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    await fileTransferUtility.UploadAsync(fileToUpload, bucketName, FileStreamUpload);
                }

                //Option 4
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest()
                {
                    BucketName = bucketName,
                    FilePath = FilePath,
                    StorageClass = S3StorageClass.Standard,
                    PartSize = 6291456, //6MB
                    Key = AdvancedUpload,
                    CannedACL = S3CannedACL.BucketOwnerFullControl 
                };

                fileTransferUtilityRequest.Metadata.Add("param1", "Value1");
                fileTransferUtilityRequest.Metadata.Add("param2", "Value2");

                await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
                */
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message: '{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message: '{0}' when writing an object", e.Message);
            }
        }

    }

}
