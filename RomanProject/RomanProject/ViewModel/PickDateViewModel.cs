using RomanProject.Tools;
using System;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;

namespace RomanProject.ViewModel
{
    class PickDateViewModel : BaseViewModel
    {
       static readonly string[] WEST_ZODIAKS = {"Oven","Teletz","Blyznyuky","Rak","Lev","Diva","Vahy","Scorpion","Strilets","Kozerih","Vodoliy","Ryba"  };
       static readonly string[] CHINEASE_ZODIAKS = { "Shchur", "Byk", "Tyhr", "Krolyk", "Drakon", "Zmiya", "Kiny", "Vivtsa", "Mavpa", "Piven", "Sobaka", "Svynya" };
       static readonly int MLSECONDS_TO_WAIT = 2000;
        #region Fields
        private DateTime _date = DateTime.Today;
        private string _birthdayMessage;
        private string _age;
        private string _westZodiak;
        private string _chineaseZodiak;

        #region Commands
        private RelayCommand<object> _checkDate;
        #endregion
        #endregion

        #region Properties
        public DateTime Date
        {
            get {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public string Age {
            get {
                return _age;
            }
            set {
                _age = value;
                OnPropertyChanged();
            }
        }

        public string BirthdayMessage {
            get {
               return _birthdayMessage;
            }
            set {
                _birthdayMessage = value;
                OnPropertyChanged();
            }
        }

        public string WestZodiak
        {
            get
            {
                return _westZodiak;
            }
            set
            {
                _westZodiak = value;
                OnPropertyChanged();
            }
        }

        public string ChineaseZodiak
        {
            get
            {
                return _chineaseZodiak;
            }
            set
            {
                _chineaseZodiak = value;
                OnPropertyChanged();
            }
        }

        #region Commands
        public RelayCommand<object> CheckDate
        {
            get
            {
                return _checkDate ?? (_checkDate = new RelayCommand<object>(
                           o =>
                           {
                               CheckDateClick(o);
                           }));
            }
        }
        #endregion
        #endregion

        private async void CheckDateClick(object obj) {
            ClearFields();
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() => Thread.Sleep(MLSECONDS_TO_WAIT));
            LoaderManager.Instance.HideLoader();
            if (!IsDateCorrect(Date))
            {
                MessageBox.Show($"Incorrect date: {Date.ToShortDateString()}");
                return;
            }
            Age = "Age: " + CountAge(Date).ToString();
            if (HasBirthdayToday(Date)) BirthdayMessage = "Congratulations!!!";
            else BirthdayMessage = "Sorry, it is not your birthday today";
            WestZodiak = "West zodiak: " + WEST_ZODIAKS[Date.Month-1];
            ChineaseZodiak ="Chinease zodiak: " + CHINEASE_ZODIAKS[(Date.Year-4)%12];
        }

        private void waitSeconds(int seconds) {
            Thread.Sleep(seconds);
        }

        private Boolean IsDateCorrect(DateTime date) {
            return date.CompareTo(DateTime.Today)<=0 && DateTime.Today.Year - date.Year < 135;
        }

        private int CountAge(DateTime date)
        {
            int age = DateTime.Today.Year - date.Year;
            if (date.Month.CompareTo(DateTime.Today.Month) > 0)
            {
                age--;
            } else if (date.Month.CompareTo(DateTime.Today.Month) == 0 && date.Day.CompareTo(DateTime.Today.Day) > 0) {
                age--;
            }
            return age;
        }

        private Boolean HasBirthdayToday(DateTime date) {
            return Date.Month == DateTime.Today.Month && Date.Day == DateTime.Today.Day;
        }

        private void ClearFields() {
            BirthdayMessage = "";
            Age = "";
            ChineaseZodiak = "";
            WestZodiak = "";
        }
    }
}
