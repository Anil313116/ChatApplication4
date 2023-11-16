namespace ChatApplication4.Models
{
    public class ChatViewModel
    {
        public IEnumerable<string> CustomUser { get; set; } // List of online users

        public ChatViewModel()
        {
            CustomUser = new List<string>();
        }


        ChatHub chatHub = new ChatHub();
    }


}
