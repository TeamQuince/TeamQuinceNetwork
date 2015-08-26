namespace SocialNetwork.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SocialNetwork.Data;
    using SocialNetwork.Models;
    using System.Data.Entity.Validation;

    public class SocialNetworkApp
    {
        public static void Main()
        {

            AddUsers();
            PostToUser(1, 2, "Hi Plami, Hi are you?");
            CommentToUserPost(2, 1, "I am fine, thank you.");
            LikeUserPost(2, 1);
        }

        public static void AddUsers()
        {
            var context = new SocialNetworkContext();
            if (context.Users.Any())
            {
                return;
            }
            var straho = new User()
            {
                Name = "Strahil",
                Username = "straho"
            };
            var plamena = new User()
            {
                Name = "Plamena",
                Username = "plami"
            };
            context.Users.Add(straho);
            context.Users.Add(plamena);
            context.SaveChanges();
        }

        public static void PostToUser(int fromUserId, int toUserId, string content)
        {
            var context = new SocialNetworkContext();

            var sender = context.Users.Find(fromUserId);
            var recipient = context.Users.Find(toUserId);
            
            var post = new UserWallPost()
            {
                Content = content,
                Author = sender,
                PostedOn = DateTime.Now,
                WallOwner = recipient
            };

            context.Postings.Add(post);
            context.SaveChanges();
        }

        public static void CommentToUserPost(int fromUserId, int toUserPostId, string content)
        {
            var context = new SocialNetworkContext();

            var sender = context.Users.Find(fromUserId);
            var userPost = context.Postings.Find(toUserPostId);
            var comment = new UserWallPostComment()
            {
                Content = content,
                Author = sender,
                PostedOn = DateTime.Now,
                UserPost = userPost
            };
            context.Postings.Add(comment);
            context.SaveChanges();
        }

        public static void LikeUserPost(int userId, int userPostId)
        {
            var context = new SocialNetworkContext();

            var user = context.Users.Find(userId);
            var post = context.Postings.Find(userPostId);
            var like = new Like()
            {
                Author = user,
                Posting = post
            };
            context.Likes.Add(like);
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            
        }
    }
}
