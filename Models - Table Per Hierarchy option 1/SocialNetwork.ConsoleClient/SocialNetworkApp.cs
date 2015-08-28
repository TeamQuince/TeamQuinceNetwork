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

            //AddUsers();
            //PostToUser(1, 2, "Hi Plami, Hi are you?");
            //CommentToUserPost(2, 1, "I am fine, thank you.");
            //LikeUserPost(2, 1);
            //AddGroups();
            //AddUserToGroup(1, 1);
            //AddUserToGroup(2, 1);
            //AddUserToGroup(1, 2);

            var context = new SocialNetworkContext();
            //var posts = context.Users.Find(2).UserPosts;
            //foreach (var post in posts)
            //{
            //    Console.WriteLine("User is: {0}", context.Users.Find(2).Name);
            //    Console.WriteLine("Author: {0}, Target: {1}", post.Author.Name, post.User.Name);
            //    Console.WriteLine(post.Content);
            //}

            var group = context.Groups.Find(1);
            foreach (var member in group.Members)
            {
                Console.WriteLine("Member: {0}", member.Name);
            }
            //PostToGroup(1, 1, "Ho ho ho");
            //PostToGroup(2, 1, "Ha ha ha");
            foreach (var post in context.Groups.Find(1).Posts)
            {
                Console.WriteLine("Content: {0}", post.Content);
            }
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
            context.Users.Add(straho);
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

        public static void AddGroups()
        {
            var context = new SocialNetworkContext();
            var teamGroup = new Group() 
            {
                Name = "Team Quince",
                Description = "A great team",
                CreatedOn = DateTime.Now
            };
            var blagoGroup = new Group() 
            {
                Name = "Blagoevgrad",
                Description = "A nice city",
                CreatedOn = DateTime.Now
            };
            context.Groups.Add(teamGroup);
            context.Groups.Add(blagoGroup);
            context.SaveChanges();
        }

        public static void AddUserToGroup(int userId, int groupId)
        {
            var context = new SocialNetworkContext();
            var user = context.Users.Find(userId);
            var group = context.Groups.Find(groupId);
            group.Members.Add(user);
            context.SaveChanges();
        }

        public static void PostToGroup(int fromUserId, int toGroupId, string content)
        {
            var context = new SocialNetworkContext();

            var sender = context.Users.Find(fromUserId);
            var recipient = context.Groups.Find(toGroupId);
            var post = new GroupPost()
            {
                Content = content,
                Author = sender,
                PostedOn = DateTime.Now,
            };
            recipient.Posts.Add(post);
            context.SaveChanges();
        }
    }
}
