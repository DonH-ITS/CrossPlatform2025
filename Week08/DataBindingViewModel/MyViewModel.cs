using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography;


namespace ViewModelExample
{
    public class MyViewModel : INotifyPropertyChanged
    {
        private System.Timers.Timer _timer;
        private int _id = 4;
        private int _seconds = 0;
        private string _modelText = "";
        private bool _isRunning = false;

        public ObservableCollection<Book> Books { get; set; }
        
        public string ModelText
        {
            get
            {
                return _modelText;
            }
            set
            {
                if (_modelText != value) {
                    _modelText = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Seconds
        {
            get => _seconds;
            set
            {
                _seconds = value;
                OnPropertyChanged();
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(StartStop));
            }
        }

        public string StartStop
        {
            get
            {
                if (IsRunning)
                    return "Stop Timer";
                else
                    return "Start Timer";
            }
        }

        public MyViewModel() {
            _timer = new System.Timers.Timer
            {
                Interval = 1000
            };
            _timer.Elapsed += (s, e) =>
            {
                ++Seconds;
            };
            Books = new ObservableCollection<Book>
              {
              new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", PublishedYear = 1925 },
              new Book { Id = 2, Title = "1984", Author = "George Orwell", PublishedYear = 1949 },
              new Book { Id = 3, Title = "To Kill a Mockingbird", Author = "Harper Lee", PublishedYear = 1960 }
              };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void StartStopTimer() {
            if (IsRunning)
                _timer.Stop();
            else
                _timer.Start();
            IsRunning = !IsRunning;
        }

        public void AddBooks(string title, string author, int year) {
            Books.Add(new Book { Id = _id++, Title = title, Author = author, PublishedYear = year });
           
        }

        protected virtual void OnPropertyChanged(string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}