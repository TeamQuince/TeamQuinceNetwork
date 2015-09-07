namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using SocialNetwork.Models;

    public class AddCommentViewModel
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUsername { get; set; }

        public string AuthorProfileImage { get; set; }

        public int PostId { get; set; }

        public int LikesCount { get; set; }

        public string CommentContent { get; set; }

        public DateTime Date { get; set; }

        public static Expression<Func<Comment, AddCommentViewModel>> Create
        {
            get
            {
                return comment => new AddCommentViewModel()
                {
                    Id = comment.Id,
                    AuthorId = comment.Author.Id,
                    AuthorUsername = comment.Author.UserName,
                    AuthorProfileImage = comment.Author.ProfilePicture,
                    LikesCount = 0,
                    CommentContent = comment.Content,
                    Date = comment.PostedOn
                };
            }
        }
    }
}