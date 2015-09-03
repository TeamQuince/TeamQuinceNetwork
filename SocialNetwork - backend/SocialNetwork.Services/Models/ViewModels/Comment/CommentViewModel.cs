namespace SocialNetwork.Services.Models.ViewModels.Comment
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using SocialNetwork.Models;

    public class CommentViewModel
    {
        // ID
        [Key]
        public int Id { get; set; }

        // AUTHOR ID
        public string AuthorId { get; set; }

        // AUTHOR USERNAME
        public string AuthorUsername { get; set; }

        // AUTHOR PROFILE IMAGE
        public string AuthorProfileImage { get; set; }

        // CONTENT
        public string CommentContent { get; set; }

        // DATE
        public DateTime Date { get; set; }

        // LIKES COUNT
        public int LikesCount { get; set; }

        // IS LIKED
        public bool Liked { get; set; }

        // CREATE
        public static Expression<Func<Comment, CommentViewModel>> Create
        {
            get
            {
                return comment => new CommentViewModel
                {
                    Id = comment.Id,
                    Date = comment.PostedOn,
                    AuthorUsername = comment.Author.UserName,
                    AuthorId = comment.Author.Id,
                    AuthorProfileImage = comment.Author.ProfilePicture,
                    CommentContent = comment.Content,
                    LikesCount = comment.Likes.Count,
                    Liked = comment.Likes.Any(l => l.Author.Id == comment.Author.Id)
                };
            }
        }
    }
}