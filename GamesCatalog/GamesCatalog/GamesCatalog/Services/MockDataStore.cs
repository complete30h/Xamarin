using GamesCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;

namespace GamesCatalog.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;
        private readonly FirebaseClient _firebaseClient
            = new FirebaseClient("https://gamescatalog-bfe2d-default-rtdb.europe-west1.firebasedatabase.app/");

        public MockDataStore()
        {
           items = new List<Item>();

            var taskGetAllItems = _firebaseClient.Child("Games").OnceAsync<Item>();
            taskGetAllItems.Wait();
            IEnumerable<FirebaseObject<Item>> resultItems = taskGetAllItems.Result;
            foreach (var item in resultItems)
            {
                var a = new Item(id: item.Key,
                    name: item.Object.Name,
                    imagesrc: item.Object.ImageSrc,
                    description: item.Object.Description,
                    mode: item.Object.Mode,
                    publisher: item.Object.Publisher,
                    developer: item.Object.Developer,
                    genre: item.Object.Genre,
                    platforms: item.Object.Platforms,
                    releasedate: item.Object.ReleaseDate,
                    videourl: item.Object.VideoUrl);
                items.Add(a);
            }
        }
        /*
        Id = id;
            Name = name;
            ImageSrc = ImageSrc;
            Description = description;
            Mode = mode;
            Publisher = publisher;
            Developer = developer;
            Genre = genre;
            Platforms = platforms;
            ReleaseDate = releasedate;
            VideoUrl = videourl;
        */
        public async Task<List<Item>> GetAllContent()
        {
            return (await _firebaseClient.Child("Games").OnceAsync<Item>()).Select(item =>
            {
                return new Item(
                    id: item.Key,
                    name: item.Object.Name,
                    imagesrc : item.Object.ImageSrc,
                    description: item.Object.Description,
                    mode : item.Object.Mode,
                    publisher : item.Object.Publisher,
                    developer : item.Object.Developer,
                    genre : item.Object.Genre,
                    platforms : item.Object.Platforms,
                    releasedate : item.Object.ReleaseDate,
                    videourl : item.Object.VideoUrl
                );
            }).ToList();
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}

/*
new Item
{
    Id = Guid.NewGuid().ToString(),
    Name = "Witcher 3",
    ImageSrc = "https://upload.wikimedia.org/wikipedia/ru/thumb/a/a2/The_Witcher_3-_Wild_Hunt_Cover.jpg/200px-The_Witcher_3-_Wild_Hunt_Cover.jpg",
    Description = "The game takes place in a fictional fantasy world based on Slavic mythology. Players control Geralt of Rivia, a monster slayer for hire known as a Witcher, and search for his adopted daughter, who is on the run from the otherworldly Wild Hunt.",
    Developer = "CD Project",
    ReleaseDate = "Soon",
    Publisher = "CD Projekt",
    Genre = "action/RPG",
    Mode = "Single",
    Platforms = "IOS, Windows",
    VideoUrl = "https://sec.ch9.ms/ch9/5d93/a1eab4bf-3288-4faf-81c4-294402a85d93/XamarinShow_mid.mp4"
},"D:\Games\5970.jpg"
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Battle Brothers",
                    ImageSrc = "https://i.playground.ru/e/ww_iHQRsczLBLAudiLDJtQ.jpeg",
                    Description = "Battle Brothers is a turn based tactical RPG which has you leading a mercenary company in a gritty, low-power, medieval fantasy world.",
                    Developer = "OverhypeStudios",
                    Publisher = "OverhypeStudios",
                    Genre = "Turn-based strategy",
                    Mode = "Single",
                    Platforms = "Windows, Nintendo Switch"
                },
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Divinity II Original Sin",
                    ImageSrc = "https://upload.wikimedia.org/wikipedia/ru/4/48/Divinity_Original_Sin_2_cover_art.jpg",
                    Description = "The Godwoken sails to the island of Reaper Coast. There, they expand their Source powers. Encountering their God again, they are directed to the Well of Ascension, where they can absorb enough Source to become Divine.",
                    Developer = "Larian Studios",
                    Publisher = "Larian Studios",
                    Genre = "RPG",
                    Mode = "Single, Multiplayer",
                    Platforms = "Windows, macOS, Xbox One"
                },
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Among Us",
                    ImageSrc = "https://assets-prd.ignimgs.com/2020/12/15/among-us-button-fin-1608054673652.jpg?width=120&crop=1%3A1%2Csmart",
                    Description = "Among Us is a multiplayer game where 10 players get dropped into an alien spaceship, sky headquarters or planet base, where each player is designated with a private role of either a “crewmate” and an “impostor.”",
                    Developer = "InnerSloth",
                    Publisher = "InnerSloth",
                    Genre = "Social deduction",
                    Mode = "Multiplayer",
                    Platforms = "Android, iOS, Windows,Xbox One"
                },
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Darkest Dungeon",
                    ImageSrc = "https://cdn1.epicgames.com/21916c391ae4425d8f6cce2382aebd0c/offer/EGS_DarkestDungeon_RedHookStudios_S2-1200x1600-1888311b95f43a3b02df2c452bd4ffee.jpg",
                    Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit.",
                    Developer = "Red Hook Studios",
                    Publisher = "Red Hook Studios",
                    Genre = "RPG, Dungeon",
                    Mode = "Single",
                    Platforms = "Windows, Linux"
                },
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Darkwood",
                    ImageSrc = "https://static-cdn.jtvnw.net/ttv-boxart/313146_IGDB-120x120.jpg",
                    Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit",
                    Developer = "Acid Wizard Studio",
                    Publisher = "Acid Wizard Studio",
                    Genre = "Survival horror",
                    Mode = "Single",
                    Platforms = "Windows, macOS, SteamOS, Ubuntu"
                },
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Total War Warhammer II",
                    ImageSrc = "https://upload.wikimedia.org/wikipedia/ru/1/13/%D0%9E%D0%B1%D0%BB%D0%BE%D0%B6%D0%BA%D0%B0_%D0%B8%D0%B3%D1%80%D1%8B_Total_War-_Warhammer_II.jpg",
                    Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit",
                    Developer = "CreativeAssembly",
                    Publisher = "Sega",
                    Genre = "Turn-based strategy, RTS",
                    Mode = "Single, Multiplayer",
                    Platforms = "Windows, macOS, Linux"
                },
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Crusader Kings III",
                    ImageSrc = "https://www.aresgalaxy.org/wp-content/uploads/2020/12/crusader-kings-3.png",
                    Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit",
                    Developer = "Paradox Development Studio",
                    Publisher = "Paradox Interactive",
                    Genre = "Grand strategy",
                    Mode = "Single, Multiplayer",
                    Platforms = "Windows, macOS, Linux"
                },
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Digger Online",
                    ImageSrc = "https://cdn.akamai.steamstatic.com/steam/apps/278970/header.jpg?t=1639142323",
                    Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit",
                    Developer = "Dmitriev Maxim",
                    Publisher = "Dmitriev Maxim",
                    Genre = "Adventure",
                    Mode = "Multiplayer",
                    Platforms = "Windows"
                },
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Age of Empires II Definitive Edition",
                    ImageSrc = "https://upload.wikimedia.org/wikipedia/ru/6/65/Age_of_Empires_II_Definitive_Edition_-_cover.jpg",
                    Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit",
                    Developer = "Forgotten Empires",
                    Publisher = "Xbox Game Studios",
                    Genre = "RTS",
                    Mode = "Single, Multiplayer",
                    Platforms = "Windows"
                }*/