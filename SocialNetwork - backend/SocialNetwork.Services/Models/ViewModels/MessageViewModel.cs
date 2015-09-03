namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    public class MessageViewModel
    {
        public string message { get; set; }
        public MessageViewModel(string message)
        {
            this.message = message;
        }
    }
}