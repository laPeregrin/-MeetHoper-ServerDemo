using ChatUI.Abstractions;
using ChatUI.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatUI.ViewModels
{
    public class ChatPageViewModel : ViewModelBase
    {
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        public string TextToSend { get; set; }
        public ICommand OnSendCommand { get; set; }

        public ChatPageViewModel()
        {
            Messages.Add(new Message() { Text = "Hi" });
            Messages.Add(new Message() { Text = "Hello", User = App.User });
            Messages.Add(new Message() { Text = "Test message"});

            OnSendCommand = new Command(() =>
            {
                if (!string.IsNullOrEmpty(TextToSend))
                {
                    Messages.Add(new Message() { Text = TextToSend, User = App.User });
                    TextToSend = string.Empty;
                }
            });
        }
    }
}
