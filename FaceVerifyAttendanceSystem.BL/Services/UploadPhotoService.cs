using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Http;

namespace FaceVerifyAttendanceSystem.BL.Services
{
    public class UploadPhotoService
    {
        public static string UploadFileToGoogleDrive(string credentialPath, string folderId, string fileToUpload)
        {
            GoogleCredential credential;

            using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(new[]
                { DriveService.ScopeConstants.DriveFile });
            }

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "UploadPhoto"
            });

            var fileMetaData = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(fileToUpload),
                Parents = new List<string> { folderId }
            };

            FilesResource.CreateMediaUpload request;

            using (var stream = new FileStream(fileToUpload, FileMode.Open))
            {
                request = service.Files.Create(fileMetaData, stream, "");
                request.Fields = "id";
                request.Upload();
            }

            var uploadPhoto = request.ResponseBody;

            return $"https://drive.google.com/thumbnail?id={uploadPhoto.Id}";
        }

        public static async Task<string> UploadProfilePhoto(string credentialPath, string folderId, IFormFile photo)
        {
            var filePath = Path.GetTempFileName();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            var uploadedUrl = UploadFileToGoogleDrive(credentialPath, folderId, filePath);

            System.IO.File.Delete(filePath);

            return uploadedUrl;
        }

        public static void DeleteFileFromGoogleDrive(string credentialPath, string fileId)
        {
            GoogleCredential credential;

            using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(new[]
                { DriveService.ScopeConstants.Drive });
            }

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "DeletePhoto"
            });

            service.Files.Delete(fileId).Execute();
        }
    }
}