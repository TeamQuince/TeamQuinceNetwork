namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    public class MessageViewModel
    {
        public string Message { get; set; }

        public MessageViewModel(string message)
        {
            this.Message = message;
        }
    }
}