using GamesCatalog.Models;
using GamesCatalog.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GamesCatalog.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        private bool _isListView;
        private ItemsPage _page;
        public bool Flag { get;set; }

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command ChangeView { get; }

        public Command OnBack { get; }
        public Filter _filter { get; set; }
        public Command FilterView { get; }
        public string ViewIcon { get => _isListView ? "icon_list.png" : "icon_grid.png"; }
        public bool IsListView { get => _isListView; }
        public bool IsGridView { get => !_isListView; }

        public ItemsViewModel(ItemsPage page, bool isListView, bool isFilter, Filter filter)
        {
            Title = "Browse";
            _filter = filter;
            _page = page;
            _isListView = isListView;
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(isFilter, filter));
            ChangeView = new Command(OnChangeView);

            OnBack = new Command(Back);

            FilterView = new Command(OnFilterView);

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        public ItemsViewModel()
        {
           // OnBack = new Command(Back);
        }


       public async Task ExecuteLoadItemsCommand(bool isFilter, Filter filter)
        {
            IsBusy = true;
            if (!isFilter)
            {
                try
                {
                    Items.Clear();
                    var items = await DataStore.GetItemsAsync(true);
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                
                if (filter != null)
                {
                    try
                    {
                        Items.Clear();
                        var items = await DataStore.GetItemsAsync(true);
                        foreach (var item in items)
                        {
                            bool flag = false;
                            if (filter.Name != null)
                            {
                                if (filter.Name.ToLower().Trim() == item.Name.ToLower().Trim())
                                    flag = true;
                            }
                            if (filter.Platforms != null)
                                flag = ParseCheck(item.Platforms,filter.Platforms);
                            if (filter.Mode != null)
                                flag = ParseCheck(item.Mode, filter.Mode);
                            if (filter.Publisher != null)
                            {
                                if (filter.Publisher.ToLower().Trim() == item.Publisher.ToLower().Trim())
                                    flag = true;
                            }
                            if (filter.Developer != null)
                            {
                                if (filter.Developer.ToLower().Trim() == item.Developer.ToLower().Trim())
                                    flag = true;
                            }
                            if (filter.Genre != null)
                                flag = ParseCheck(item.Genre, filter.Genre);
                            if (filter.FromYear != null)
                            {
                                if (int.Parse(filter.FromYear) <= int.Parse(item.ReleaseDate))
                                    flag = true;
                            }
                            if (filter.ToYear != null)
                            {
                                if (int.Parse(filter.ToYear) >= int.Parse(item.ReleaseDate))
                                    flag = true;
                            }

                            if (flag)
                            {
                                Items.Add(item);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                }
            }
        }

        /*
        public bool ParseCheck(string ItemProperty, string filter)
        {
            var strings = ItemProperty.Split(',');
            foreach (var item in strings)
            {
                if (item.ToLower().Trim() == filter.ToLower().Trim())
                    return true;
            }
            return false;
        }
        */
        
        public bool ParseCheck(string ItemProperty, string filter)
        {
            var strings = ItemProperty.Split(',');
            var strings1 = filter.Split(',');
            foreach (var item in strings)
            {
                foreach (var item1 in strings1)
                {
                    if (item.ToLower().Trim() == item1.ToLower().Trim())
                        return true;
                }
            }
            return false;
        }
        

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            if (_filter != null)
            {
                var jsonStr = JsonConvert.SerializeObject(_filter);
                await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}&{nameof(ItemDetailViewModel.isList)}={IsListView}&{nameof(ItemDetailViewModel._filter)}={jsonStr}");
            }
            else
            {
                await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}&{nameof(ItemDetailViewModel.isList)}={IsListView}");
            }
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}&{nameof(ItemDetailViewModel.isList)}={IsListView}");
            // await Shell.Current.GoToAsync($"{nameof(ItemsPage)}?Filter={jsonStr}&flag={true}");

        }

        private void OnChangeView(object obj)
        {
            _isListView = !_isListView;

            OnPropertyChanged("IsListView");
            OnPropertyChanged("IsGridView");
            OnPropertyChanged("ViewIcon");

        }

        private async void Back(object obj)
        {
            var result = await Shell.Current.DisplayAlert(
            "Going Back?",
            "Are you sure you want to go back?",
            "Yes, Please!", "Nope!");
            if (result)
            {
                await Shell.Current.GoToAsync("///LoginPage");
            }
        }



        private async void OnFilterView(object obj)
        {

            await Shell.Current.GoToAsync($"{nameof(FilterItem)}?islist={IsListView}");
        }

        public void UpdateFilter()
        {
            OnPropertyChanged("Items");
        }
    }
}