using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
namespace GamesCatalog.Services
{
    public interface IFirebaseStorageService
    {
        Task<string> LoadImage(FileResult image);

        Task<string> LoadVideo(FileResult video);

        Task RemoveImage(string fileName);

        Task RemoveVideo(string fileName);
    }
}