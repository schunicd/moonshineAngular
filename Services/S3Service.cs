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

        public async Task<List<string>> GetAlbums()
        {
            AmazonS3Client client = new AmazonS3Client();
            ListObjectsRequest request = new ListObjectsRequest
            {
                BucketName = "moonshinephotostest",
                Prefix = "photoAlbums/"

            };

            
            List<string> albums = new List<string>();
            do
            {
                ListObjectsResponse response = await client.ListObjectsAsync(request);

                IEnumerable<S3Object> folders = response.S3Objects.Where(x => x.Key.EndsWith(@"/"));

                folders.ToList().ForEach(x => { Console.WriteLine("IN GET ALBUMS: " + x.Key); });

                if (response.IsTruncated)
                {
                    request.Marker = response.NextMarker;
                }
                else
                {
                    request = null;
                }

            } while (request != null);

            return albums;
        }

        public async Task<List<string>> GetPhotos()
        {
            AmazonS3Client client = new AmazonS3Client();
            ListObjectsRequest listRequest = new ListObjectsRequest
            {
                BucketName = "moonshinephotostest"
            };

            ListObjectsResponse listResponse;
            List<string> photos = new List<string>();
            do
            {
                listResponse = await client.ListObjectsAsync(listRequest);
                foreach (S3Object obj in listResponse.S3Objects)
                {

                    photos.Add(obj.Key);

                    //Console.WriteLine(obj.Key);
                    //Console.WriteLine(" Size - " + obj.Size);
                }

                listRequest.Marker = listResponse.NextMarker;
            } while (listResponse.IsTruncated);

            return photos;
        }

        private const string GalleryBucketName = "moonshinephotostest/photoAlbums/"; //declaring bucketname as a constant
        public async Task UploadFileAsync(IFormFile file, string albumName)
        {
            try
            {
                var transferRequest = new TransferUtilityUploadRequest() //setting values for the request to transfer a file
                {
                    InputStream = file.OpenReadStream(),        //setting the selected file as the input stream
                    AutoCloseStream = false,                    //
                    BucketName = GalleryBucketName + albumName,  //setting bucket name to constant declared above
                    Key = file.FileName,                        //setting key as filename so we can get the files by their names from the bucket
                    StorageClass = S3StorageClass.Standard      //setting storage class as standard since it has high reliability
                };

                transferRequest.Metadata.Add("Date-UTC-Uploaded", DateTime.UtcNow.ToString()); //adding metadata of when the file was uploaded

                await new TransferUtility(_client).UploadAsync(transferRequest);    //initiating transfer request

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


        private const string BandImageBucketName = "moonshinephotostest/bandImages"; //declaring bucketname as a constant
        public async Task UploadBandImageAsync(IFormFile file)
        {
            try
            {
                var transferRequest = new TransferUtilityUploadRequest() //setting values for the request to transfer a file
                {
                    InputStream = file.OpenReadStream(),    //setting the selected file as the input stream
                    AutoCloseStream = false,                //
                    BucketName = BandImageBucketName,       //setting bucket name to constant declared above
                    Key = file.FileName,                    //setting key as filename so we can get the files by their names from the bucket
                    StorageClass = S3StorageClass.Standard  //setting storage class as standard since it has high reliability
                };

                transferRequest.Metadata.Add("Date-UTC-Uploaded", DateTime.UtcNow.ToString()); //adding metadata of when the file was uploaded

                await new TransferUtility(_client).UploadAsync(transferRequest);    //initiating transfer request

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

        public async Task DeleteImage(string fileName)
        {
            string bucketname = "moonshinephotostest";
            string key = "photoAlbums/test/" + fileName;
            //switch (choice) { 
            //    case 0:
            //        bucketname = BandImageBucketName;
            //        break;
            //    case 1:
            //        bucketname = GalleryBucketName;
            //        break;
            //}
            
            try
            {
                DeleteObjectResponse resp = await _client.DeleteObjectAsync(bucketname, key);

                Console.WriteLine("Delete Successful!");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Delete Failed!");
            }
        }
    }

}
