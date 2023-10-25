using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1
{
    public partial class CalendarMessagePage : ContentPage
    {
        public ICommand ButtonCommand { get; }
        private string message;
        private string day;
        public CalendarMessagePage(string message, string day)
        {
            this.message = message;
            this.day = day;

        InitializeComponent();
           string data = Preferences.Get(day, string.Empty);
            noteEditor.Text = data;
        }


        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            string noteText = noteEditor.Text;

            // Сохраняем текст в Preferences
            Preferences.Set(day, noteText);

            // Можно также добавить сообщение об успешном сохранении или другую логику
            DisplayAlert("Сохранено", "Заметка успешно сохранена", "OK");
        }

    }
}