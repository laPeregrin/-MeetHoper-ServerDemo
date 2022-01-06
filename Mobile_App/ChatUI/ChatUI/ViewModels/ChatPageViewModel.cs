using ChatUI.Abstractions;
using ChatUI.Models;
using MvvmHelpers.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ChatUI.ViewModels
{
    public class ChatPageViewModel : ViewModelBase
    {
        private string _textToSend;

        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        public string TextToSend { get => _textToSend;
                                   set { _textToSend = value; 
                                         RaisePropertyChanged(); } }

        public ChatPageViewModel()
        {

        }

        public ICommand SendMessageCommand => new AsyncCommand(async () =>
        {
            Messages.Add(new Message(null, _textToSend));
            TextToSend = string.Empty;
        });
    }
}
