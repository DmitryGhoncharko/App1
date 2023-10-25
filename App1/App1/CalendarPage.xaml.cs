using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1
{
    public partial class CalendarPage : ContentPage
    {
        public ICommand ButtonCommand { get; }
        public CalendarPage()
        {
           

        InitializeComponent();
            // Создаем экземпляр команды с обработчиком
            ButtonCommand = new Command<string>(OnButtonClicked);
            BindingContext = this; // Установка текущей страницы в качестве контекста привязки
            // Создание заданного количества кнопок
            // Получаем текущую дату
            DateTime currentDate = DateTime.Now;

            // Получаем первый день следующего месяца и вычитаем 1 день,
            // чтобы получить последний день текущего месяца
            DateTime lastDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1)
                .AddMonths(1)
                .AddDays(-1);

            // Получаем количество дней в текущем месяце
            int numberOfDays = lastDayOfMonth.Day;
            // Удаление кнопки по её номеру
            Button buttonToRemove = null;
            for(int i=31; i>numberOfDays; i--)
            {
                string day = i.ToString();
                foreach (var child in buttonGrid.Children)
                {
                    if (child is Button button && button.Text == day)
                    {
                        buttonToRemove = button;
                        break;
                    }
                }

                if (buttonToRemove != null)
                {
                    buttonGrid.Children.Remove(buttonToRemove);
                }
            }
        }


        // Обработчик команды
        private void OnButtonClicked(string day)
        {
            if (int.TryParse(day, out int dayNumber))
            {
                var calendarPage = new CalendarMessagePage("message",day); // CalendarMessagePage - это ваш класс новой страницы

                // Используем Navigation для перехода к новой странице
                if (Navigation != null)
                {
                    Navigation.PushAsync(calendarPage);
                }
            }
        }


    }
}