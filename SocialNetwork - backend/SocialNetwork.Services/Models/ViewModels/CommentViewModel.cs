//namespace SocialNetwork.Services.Models.ViewModels
//{
//    using System;
//    using System.Linq;
//    using System.Linq.Expressions;

//    using SocialNetwork.Models;

//    public class CommentViewModel
//    {
//        public int Id { get; set; }

//        public string AuthorId { get; set; }

//        public string AuthorUsername { get; set; }

//        public string AuthorProfileImage { get; set; }

//        public string CommentContent { get; set; }

//        public DateTime Date { get; set; }

//        public int LikesCount { get; set; }

//        public bool Liked { get; set; }

//        public static Expression<Func<Comment, CommentViewModel>> Create
//        {
//            get
//            {
//                return comment => new CommentViewModel()
//                {
//                    Id = comment.Id,
//                    AuthorId = comment.Author.Id,
//                    AuthorUsername = comment.Author.UserName,
//                    AuthorProfileImage = comment.Author.ProfilePicture,
//                    CommentContent = comment.Content,
//                    Date = comment.PostedOn,
//                    LikesCount = comment.Likes.Count,
//                };
//            }
//        }

//        public static object CreatePreview(ApplicationUser user, Comment comment)
//        {
//            return new
//            {
//                Id = comment.Id,
//                AuthorId = comment.Author.Id,
//                AuthorUsername = comment.Author.UserName,
//                AuthorProfileImage = comment.Author.ProfilePicture,
//                CommentContent = comment.Content,
//                Date = comment.PostedOn,
//                LikesCount = comment.Likes.Count,
//                Liked = comment.Likes.Any(l => l.Author.Id == user.Id)
//            };
//        }
//    }
//}