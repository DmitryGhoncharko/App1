using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Xamarin.Essentials;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Newtonsoft.Json;
using Plugin.SimpleAudioPlayer;

namespace App1
{
    public partial class TrackListPage : ContentPage
    {
        string playListName;
       
        public TrackListPage(List<Track> trackUris, string playListName)
        {
            InitializeComponent();
            LoadTracks(trackUris);
            this.playListName = playListName;
        }

        private void LoadTracks(List<Track> trackUris)
        {
            // Загрузите треки с указанными URI в список отображения
            var trackList = new List<Track>();

            foreach (Track uri in trackUris)
            {
                // Здесь можно извлечь метаданные трека из URI или других источников
                // Например, имя трека или другие сведения о нем

                // Создайте объект Track и добавьте его в список
                trackList.Add(new Track { Uri = uri.Uri });
            }

            TrackListView.ItemsSource = trackList;
        }

        private void OnTrackSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Track selectedTrack)
            {
                
            }
        }
        private void OnPlayClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Track selectedTrack)
            {
                // Воспроизвести выбранный трек, используя ваш механизм воспроизведения
                PlayTrack(selectedTrack.Uri);
            }
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Track selectedTrack)
            {
                // Удалить выбранный трек из списка и обновить ListView
                RemoveTrack("",selectedTrack.Uri);
            }
        }
        private void PlayTrack(string trackUri)
        {
            try
            {
                ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;
                string cleanedString = trackUri.Replace("[", "").Replace("]", "").Replace("\"", "");
                Console.WriteLine(cleanedString);
                player.Load(cleanedString);
                player.Play();
            }
            catch (Exception ex)
            {
                // Обработка ошибок воспроизведения
                Console.WriteLine(trackUri);
                Console.WriteLine($"Ошибка воспроизведения трека: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }


        private void RemoveTrack(string playlistName, string trackUri)
        {
            // Получите текущий список треков для плейлиста
            string playlistJson = Preferences.Get(playlistName, string.Empty);

            // Проверьте, существует ли плейлист и трек для удаления
            if (!string.IsNullOrEmpty(playlistJson))
            {
                List<string> playlist = JsonConvert.DeserializeObject<List<string>>(playlistJson);

                // Проверьте, существует ли трек в плейлисте
                if (playlist.Contains(trackUri))
                {
                    // Удалите трек из списка
                    playlist.Remove(trackUri);

                    // Обновите список треков в настройках
                    playlistJson = JsonConvert.SerializeObject(playlist);
                    Preferences.Set(playlistName, playlistJson);
                }
            }

            // После удаления обновите представление плейлистов
           // UpdatePlaylistView();
        }
    }
}