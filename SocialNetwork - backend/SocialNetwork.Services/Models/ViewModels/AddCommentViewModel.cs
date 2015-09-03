//namespace SocialNetwork.Services.Models.ViewModels
//{
//    using System;
//    using System.Linq.Expressions;

//    using SocialNetwork.Models;

//    public class AddCommentViewModel
//    {
//        public int id { get; set; }

//        public string authorId { get; set; }

//        public int postId { get; set; }

//        public int likes { get; set; }

//        public string content { get; set; }

//        public string date { get; set; }

//        public static Expression<Func<Comment, AddCommentViewModel>> Create
//        {
//            get
//            {
//                return comment => new AddCommentViewModel()
//                {
//                    id = comment.Id,
//                    authorId = comment.Author.Id,
//                    likes = 0,
//                    content = comment.Content,
//                    date = comment.PostedOn.ToString()
//                };
//            }
//        }
//    }
//}