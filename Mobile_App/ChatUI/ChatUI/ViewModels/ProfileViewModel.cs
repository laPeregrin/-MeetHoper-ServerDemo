using ChatUI.Abstractions;

namespace ChatUI.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private readonly IAPIInteraction _apiWorker;
        private string _myStringProperty;
        private int _rate;
        private int _age;
        private string _city;

        public string MyStringProperty
        {
            get => _myStringProperty;
            set
            {
                _myStringProperty = value;
                RaisePropertyChanged();
            }
        }

        public int Rate
        {
            get => _rate;
            set
            {
                _rate = value;
                RaisePropertyChanged();
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                RaisePropertyChanged();
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                RaisePropertyChanged();
            }
        }

        public ProfileViewModel(IAPIInteraction apiWorker)
        {
            _apiWorker = apiWorker;
            Rate = 3;
            Age = 23;
            City = "Kyiv";
            MyStringProperty = "Чтобы ни выпало мне в жизни, я всегда буду помнить слова: Чем больше сила, тем больше и ответственность. Это мой дар, моё проклятье. Кто я? Я Человек-Паук";
        }

    }
}
