using ChatUI.ViewModels;
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
    public partial class ChatInputBarView : ContentView
    {
        public ChatInputBarView()
        {
            InitializeComponent();
        }
        public void Handle_Completed(object sender, EventArgs e)
        {
            (Parent.Parent.BindingContext as ChatPageViewModel).OnSendCommand.Execute(null);
            _ = chatTextInput.Focus();
        }
        public void UnFocusEntry()
        {
            chatTextInput?.Unfocus();
        }
    }
}