using GamesCatalog.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using GamesCatalog.Views;

namespace GamesCatalog.ViewModels
{
    [QueryProperty(nameof(isList), nameof(isList))]
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    [QueryProperty(nameof(_filter), nameof(_filter))]
    public class ItemDetailViewModel : BaseViewModel
    {
        public Command RedactItem { get; }
        public Command OnBack { get; }
        public ItemDetailViewModel()
        {
            //isList = islist;
            RedactItem = new Command(OnRedactClicked);
            OnBack = new Command(Back);
        }
        public Item item { get; set; }

        private string itemId;
        private string filter;
        private bool islist;
        private string text ;
        private string description ;
        private string mode;
        private string imagesrc;
        private string publisher;
        private string developer;
        private string genre;
        private string platforms;
        private string releasedate;
        private string videourl;

        public bool isList
        {
            get
            {
                return islist;
            }
            set
            {
                islist = value;
            }
        }

        public string _filter
        {
            get
            {
                return filter;
            }
            set
            {
                filter = Uri.UnescapeDataString(value);
            }
        }
        public string Id { get; set; }

        private async void OnRedactClicked(object obj)
        {
            // await Shell.Current.GoToAsync($"ItemRedactorPage?id={itemId}");
            await Shell.Current.GoToAsync($"{nameof(ItemRedactorPage)}?id={itemId}");
           // await Shell.Current.GoToAsync($"ItemsPage?flag={false}");


        }


        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ImageSrc
        {
            get => imagesrc;
            set => SetProperty(ref imagesrc, value);
        }

        public string Mode
        {
            get => mode;
            set => SetProperty(ref mode, value);
        }

        public string Publisher
        {
            get => publisher;
            set => SetProperty(ref publisher, value);
        }

        public string Developer
        {
            get => developer;
            set => SetProperty(ref developer, value);
        }

        public string Genre
        {
            get => genre;
            set => SetProperty(ref genre, value);
        }

        public string Platforms
        {
            get => platforms;
            set => SetProperty(ref platforms, value);
        }

        public string ReleaseData
        {
            get => releasedate;
            set => SetProperty(ref releasedate, value);
        }

        public string VideoUrl
        {
            get => videourl;
            set => SetProperty(ref videourl, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                ImageSrc = item.ImageSrc;
                Genre = item.Genre;
                Platforms = item.Platforms;
                Developer = item.Developer;
                ReleaseData = item.ReleaseDate;
                Publisher = item.Publisher;
                Text = item.Name;
                Description = item.Description;
                Mode = item.Mode;
                VideoUrl = item.VideoUrl;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void Back(object obj)
        {
            // var a = nameof(ItemsViewModel.IsListView);
            if (_filter != null)
            {
                //await Shell.Current.GoToAsync($"{nameof(ItemsPage)}?islist={isList}");
                await Shell.Current.GoToAsync($"{nameof(ItemsPage)}?Filter={_filter}&flag={true}&islist={islist}");

            }
            else
            {
                await Shell.Current.GoToAsync($"{nameof(ItemsPage)}?islist={isList}");

            }

        }
        /*
                public ItemDetailViewModel(Item item)
                {
                    _Item = item;

                    Id = _Item.Id;
                    text = _Item.Name;
                    Description = _Item.Description;
                    Mode = _Item.Mode;
                    Publisher = _Item.Publisher;
                    Developer = _Item.Developer;
                    Genre = _Item.Genre;
                    Platforms = _Item.Platforms;
                    ImageSrc = _Item.ImageSrc;
                    ReleaseDate = _Item.ReleaseDate;
                    VideoUrl = _Item.VideoUrl;
                }
        */
    }
}
