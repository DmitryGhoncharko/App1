using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Xamarin.Essentials;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Diagnostics;
using Xamarin.Forms.PlatformConfiguration;
using System.IO;

namespace App1
{
    public partial class AddTrackPage : ContentPage
    {
        public AddTrackPage()
        {
            InitializeComponent();
            UpdatePlaylistView();
        }

        private void CreatePlaylist_Clicked(object sender, EventArgs e)
        {
            // Отобразите поле ввода имени плейлиста и кнопку "Создать плейлист с именем"
            PlaylistNameEntry.IsVisible = true;
            CreatePlaylistWithNameButton.IsVisible = true;
        }

        private async void CreatePlaylistWithName_Clicked(object sender, EventArgs e)
        {
            string playlistName = PlaylistNameEntry.Text;

            // Получите текущий список имен плейлистов
            string playlistNames = Preferences.Get("Плейлисты", "");

            // Добавьте новое имя плейлиста и разделитель
            playlistNames += (string.IsNullOrEmpty(playlistNames) ? "" : ",") + playlistName;

            // Сохраните обновленный список имен плейлистов
            Preferences.Set("Плейлисты", playlistNames);

            // Скрыть поле ввода и обновить представление плейлистов
            PlaylistNameEntry.IsVisible = false;
            UpdatePlaylistView();
        }

        private async void AddSong_Clicked(object sender, EventArgs e)
        {
            var playlistName = (string)((Button)sender).CommandParameter;

            try
            {
                var fileResult = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Выберите файл",
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.Android, new[] { "audio/*" } }
                    }),
                });
                
               
                if (fileResult != null)
                {
                    string songUri = fileResult.FullPath;
                    
                    Console.WriteLine(songUri);

                    string playlistJson = Preferences.Get(playlistName, string.Empty);

                    List<string> playlist;
                    if (string.IsNullOrEmpty(playlistJson))
                    {
                        playlist = new List<string>();
                    }
                    else
                    {
                        playlist = JsonConvert.DeserializeObject<List<string>>(playlistJson);
                        Console.WriteLine(playlist.ToString());
                    }

                    playlist.Add(songUri);

                    playlistJson = JsonConvert.SerializeObject(playlist);

                    Preferences.Set(playlistName, playlistJson);

                    UpdatePlaylistView();
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок при выборе файла
                Console.WriteLine($"Ошибка выбора файла: {ex.Message}");
            }
        }
      
        
        private void UpdatePlaylistView()
        {
            string playlistNames = Preferences.Get("Плейлисты", "");
            if (!string.IsNullOrEmpty(playlistNames))
            {
                var playlistNamesList = playlistNames.Split(',').ToList();
                PlaylistListView.ItemsSource = playlistNamesList.Select(name => new { PlaylistName = name }).ToList();
            }
        }
    }
}