using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel viewModel;
       
        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            BindingContext = viewModel;
           
        }
        // Обработчики событий для кнопок
        private void OnAddSongClicked(object sender, EventArgs e)
        {
            var addTrackPage = new AddTrackPage(); // NewPage - это ваш класс новой страницы

            // Используем Navigation для перехода к новой странице
            if (Navigation != null)
            {
                Navigation.PushAsync(addTrackPage);
            }

        }

        private async void OnQuoteClicked(object sender, EventArgs e)
        {
            // Логика для кнопки Quote
           await DisplayAlert("Цитата", "Заметка успешно сохранена", "OK");
           
        }

        private void OnSilenceClicked(object sender, EventArgs e)
        {
            // Логика для кнопки Silence

        }


        private void OnCalendarClicked(object sender, EventArgs e)
        {
            // Логика для кнопки Calendar
            // Создаем новую страницу
            var calendarPage = new CalendarPage(); // NewPage - это ваш класс новой страницы

            // Используем Navigation для перехода к новой странице
           if(Navigation != null)
            {
                Navigation.PushAsync(calendarPage);
            }
        }

        private void OnPlaylistClicked(object sender, EventArgs e)
        {
            var playListPage = new PlayListPage(); // NewPage - это ваш класс новой страницы

            // Используем Navigation для перехода к новой странице
            if (Navigation != null)
            {
                Navigation.PushAsync(playListPage);
            }
        }
    }
}
