using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using GamesCatalog.Services;
using GamesCatalog.Views;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using GamesCatalog.Models;
using Firebase.Database.Query;

namespace GamesCatalog.ViewModels
{
   public class ItemRedactorViewModel : BaseViewModel
    {
        Item item;
        private ItemRedactorPage _page;
        //private readonly IFirebaseDatebaseService _firebaseDatebaseService;
        private readonly IFirebaseStorageService _firebaseStorageService;
        private readonly FirebaseClient _firebaseClient
           = new FirebaseClient("https://gamescatalog-bfe2d-default-rtdb.europe-west1.firebasedatabase.app/");

        public string Name { get; set; }

        public string Id { get; set; }
        public string Description { get; set; }
        public string Mode { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public string Genre { get; set; }
        public string Release { get; set; }

        public string Platforms { get; set; }

        public string ImageSrc { get; set; }
        public string VideoUrl { get; set; }

        public Command AddImage { get; }
        public Command AddVideo { get; }
        public Command Save { get; }

        FileResult _image;
        FileResult _video;

        public ItemRedactorViewModel(string id)
        {
            Id = id;
            _firebaseStorageService = DependencyService.Get<IFirebaseStorageService>();
            AddImage = new Command(OnAddImageButtonClicked);
            AddVideo = new Command(OnAddVideoButtonClicked);
            Save = new Command(OnSaveButtonClicked);
        }
        // private FileResult _image;
        // private FileResult _video;

        private async void OnAddImageButtonClicked()
        {
            _image = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Select image" });
        }

        private async void OnAddVideoButtonClicked()
        {
            _video = await MediaPicker.PickVideoAsync(new MediaPickerOptions { Title = "Select video" });
        }

        private async void OnSaveButtonClicked()
        {
            item = await DataStore.GetItemAsync(Id);
            ImageSrc = await LoadImage();
            VideoUrl = await LoadVideo();
            if (ImageSrc != null)
            {
                item.ImageSrc = ImageSrc;
            }
            if (VideoUrl != null)
            {
                item.VideoUrl = VideoUrl;
            }
            if (Name != null)
            {
                item.Name = Name;
            }
            if (Description != null)
            {
                item.Description = Description;
            }
            if (Platforms != null)
            {
                item.Platforms = Platforms;
            }
            if (Publisher != null)
            {
                item.Publisher = Publisher;
            }
            if (Developer != null)
            {
                item.Developer = Developer;
            }
            if (Release != null)
            {
                if (int.TryParse(Release, out var number))
                item.ReleaseDate = Release;
            }
            if (Mode != null)
            {
                item.Mode = Mode;
            }
            if (Genre != null)
            {
                item.Genre = Genre;
            }
            await _firebaseClient.Child("Games").Child(item.Id).PutAsync(item);
            await Shell.Current.GoToAsync($"ItemsPage?flag={false}");

        }

        private async Task<string> LoadImage()
        {
            if (_image == null)
            {
                return null;
            }

            return await _firebaseStorageService.LoadImage(_image);
        }

        private async Task<string> LoadVideo()
        {
            if (_video == null)
            {
                return null;
            }

            return await _firebaseStorageService.LoadVideo(_video);
        }
    }
}
