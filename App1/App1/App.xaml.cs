using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            CheckAndRequestPermissions();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        async Task CheckAndRequestPermissions()
        {
            // Проверка разрешения на чтение файлов
            var readStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (readStatus != PermissionStatus.Granted)
            {
                // Разрешение на чтение файлов не предоставлено, запросите его у пользователя
                var readRequest = await Permissions.RequestAsync<Permissions.StorageRead>();
                if (readRequest != PermissionStatus.Granted)
                {
                    // Разрешение не было предоставлено, обработайте эту ситуацию
                    // Например, вы можете показать пользователю сообщение о том, что разрешение требуется для работы приложения.
                }
            }

            // Проверка разрешения на запись файлов
            var writeStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (writeStatus != PermissionStatus.Granted)
            {
                // Разрешение на запись файлов не предоставлено, запросите его у пользователя
                var writeRequest = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (writeRequest != PermissionStatus.Granted)
                {
                    // Разрешение не было предоставлено, обработайте эту ситуацию
                    // Например, вы можете показать пользователю сообщение о том, что разрешение требуется для работы приложения.
                }
            }

            // Теперь у вас есть разрешения на чтение и запись файлов
            if (readStatus == PermissionStatus.Granted && writeStatus == PermissionStatus.Granted)
            {
                // Ваш код для работы с файлами здесь
            }
        }
    }
}
