using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private string myStringProperty;
        private int rate;
        private int age;
        private string city;
        public string MyStringProperty
        {
            get => myStringProperty;
            set
            {
                myStringProperty = value;
                OnPropertyChanged(nameof(MyStringProperty));
            }
        }

        public int Rate
        {
            get => rate;
            set
            {
                rate = value;
                OnPropertyChanged(nameof(Rate));
            }
        }

        public int Age 
        { 
           get => age;
           set
           {
                age = value;
                OnPropertyChanged(nameof(Age));
           } 
        }
        public string City 
        {
            get => city;
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = this;
            Rate = 10;
            Age = 23;
            City = "Kyiv";
            MyStringProperty = "Чтобы ни выпало мне в жизни, я всегда буду помнить слова: Чем больше сила, тем больше и ответственность. Это мой дар, моё проклятье. Кто я? Я Человек-Паук";
        }
        public void EditorTextChanged(object sender, TextChangedEventArgs e)
        {
            _ = e.OldTextValue;
            string newText = e.NewTextValue;
            Console.WriteLine(newText);
        }
    }
}