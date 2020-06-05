using Chat.Domain;
using Chat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chat.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User currentUser;
        private ChatService chatService;
        private List<Message> messages = new List<Message>();

        public MainWindow(User curUser)
        {
            InitializeComponent();

            chatService = new ChatService();
            currentUser = curUser;

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(UpdMessages);
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();
        }

        private void SendMessageBtnClick(object sender, RoutedEventArgs e)
        {
            if (messageTB.Text == string.Empty) return;
            chatService.SendMessage(new Message
            {
                AuthorLogin = currentUser.Login,
                Author = currentUser.FirstName,
                Text = messageTB.Text
            });
            messageTB.Text = "Введите сообщение";
        }

        private async void UpdMessages(object sender, EventArgs e)
        {
            messages = await chatService.GetMessages();
            messages = messages.OrderBy(x => x.CreationDate).ToList();
            messages.ForEach(x => { if (x.AuthorLogin == currentUser.Login) x.Author = "-> Вы"; });

            messagesDG.ItemsSource = null;
            messagesDG.ItemsSource = messages;
        }


        private void MessageTBGotFocus(object sender, RoutedEventArgs e)
        {
            messageTB.Text = string.Empty;
        }

    }
}
