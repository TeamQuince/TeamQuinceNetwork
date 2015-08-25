namespace SocialNetwork.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SocialNetwork.Data;
    using SocialNetwork.Models;

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
            if (context.Users.Count() > 0)
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
            context.Users.Add(plamena);
            context.SaveChanges();
        }

        public static void PostToUser(int fromUserId, int toUserId, string content)
        {
            var context = new SocialNetworkContext();

            var sender = context.Users.Find(fromUserId);
            var recipient = context.Users.Find(toUserId);
            var post = new UserPost()
            {
                Content = content,
                Author = sender,
                PostedOn = DateTime.Now,
                User = recipient
            };
            context.UserPosts.Add(post);
            context.SaveChanges();
        }

        public static void CommentToUserPost(int fromUserId, int toUserPostId, string content)
        {
            var context = new SocialNetworkContext();

            var sender = context.Users.Find(fromUserId);
            var userPost = context.UserPosts.Find(toUserPostId);
            var comment = new UserPostComment()
            {
                Content = content,
                Author = sender,
                PostedOn = DateTime.Now,
                UserPost = userPost
            };
            context.UserPostComments.Add(comment);
            context.SaveChanges();
        }

        public static void LikeUserPost(int userId, int userPostId)
        {
            var context = new SocialNetworkContext();

            var user = context.Users.Find(userId);
            var post = context.UserPosts.Find(userPostId);
            var like = new UserPostLike()
            {
                Author = user,
                Post = post
            };
            context.UserPostLikes.Add(like);
            context.SaveChanges(); 
        }
    }
}
