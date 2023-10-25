using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Xamarin.Essentials;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Diagnostics;

namespace App1
{
    public partial class PlayListPage : ContentPage
    {
        public PlayListPage()
        {
            InitializeComponent();
            UpdatePlaylistView();
        }

        private void UpdatePlaylistView()
        {
            string playlistNames = Preferences.Get("Плейлисты", "");
            if (!string.IsNullOrEmpty(playlistNames))
            {
                var playlistNamesList = playlistNames.Split(',').ToList();
                List<Playlist> playlists = new List<Playlist>();

                foreach (var name in playlistNamesList)
                {
                    playlists.Add(new Playlist { PlaylistName = name });
                }

                PlaylistListView.ItemsSource = playlists;
            }
        }

        private async void OnPlaylistSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Playlist selectedPlaylist)
            {
                // Здесь мы используем DisplayAlert для отображения диалогового окна с двумя кнопками
                string action = await DisplayActionSheet("Выберите действие", "Отмена", null, "Открыть плейлист");

                if (action == "Открыть плейлист")
                {
                    // Получите строку с URI треков из выбранного плейлиста
                    string playlistJson = Preferences.Get(selectedPlaylist.PlaylistName, string.Empty);

                    if (!string.IsNullOrEmpty(playlistJson))
                    {
                        // Разделите строку на отдельные URI, используя запятую как разделитель
                        string[] trackUris = playlistJson.Split(',');

                        // Создайте список треков
                        List<Track> playlistTracks = new List<Track>();

                        // Добавьте каждый URI в список треков
                        foreach (string uri in trackUris)
                        {
                            playlistTracks.Add(new Track { Uri = uri });
                        }

                        // Создайте новую страницу TrackListPage и передайте в неё список треков
                       TrackListPage trackListPage = new TrackListPage(playlistTracks, selectedPlaylist.PlaylistName);
                      
                      //   Откройте новую страницу с треками
                        Navigation.PushAsync(trackListPage);
                    }
                }
            }
        }
    }
    public class Playlist
    {
        public string PlaylistName { get; set; }
    }
    public class Track
    {
        public string Uri { get; set; }
    }
}