using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitializePreferences();
            MainPage = new NavigationPage(new MainPage());
        }
        private void InitializePreferences()
        {
            // Установка начальных значений по умолчанию для настроек
            if (!Preferences.ContainsKey("Плейлисты"))
            {
                Preferences.Set("Плейлисты", "");
            }
        }
        protected override void OnStart()
        {
           
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
