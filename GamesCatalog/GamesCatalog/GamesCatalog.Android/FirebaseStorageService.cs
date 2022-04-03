using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Storage;
using GamesCatalog.Services;
using Xamarin.Essentials;

namespace GamesCatalog.Droid.Services
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        private static readonly string StorageUrl = "gamescatalog-bfe2d.appspot.com";
        private readonly FirebaseStorage _firebaseStorage = new FirebaseStorage(StorageUrl);

        public async Task<string> LoadImage(FileResult file)
        {
            await _firebaseStorage.Child($"{file.FileName}")
                .PutAsync(await file.OpenReadAsync());

            return await _firebaseStorage.Child($"{file.FileName}").GetDownloadUrlAsync();
        }

        public async Task<string> LoadVideo(FileResult video)
        {
            await _firebaseStorage.Child($"{video.FileName}")
                .PutAsync(await video.OpenReadAsync());

            return await _firebaseStorage.Child($"{video.FileName}").GetDownloadUrlAsync();
        }

        public async Task RemoveImage(string fileName)
        {
            await _firebaseStorage
                .Child("images")
                .Child($"{fileName}.jpg")
                .DeleteAsync();
        }

        public async Task RemoveVideo(string fileName)
        {
            await _firebaseStorage
                .Child("videos")
                .Child($"{fileName}.mp4")
                .DeleteAsync();
        }
    }
}