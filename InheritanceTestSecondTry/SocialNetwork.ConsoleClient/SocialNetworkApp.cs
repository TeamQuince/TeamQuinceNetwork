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
        private static SocialNetworkContext ctx = new SocialNetworkContext();
        public static void Main()
        {

            AddUsers();
            PostToUser(1, 2, "Hi Plami, Hi are you?");
            CommentToUserPost(2, 1, "I am fine, thank you.");
            LikeUserPost(2, 1);
            AddGroups();
            AddUserToGroup(1, 1);
            AddUserToGroup(2, 1);
            AddUserToGroup(1, 2);

            var user1 = ctx.Users.Find(1);
            var posts = ctx.Postings.OfType<UserWallPost>().Where(p => p.AuthorId == user1.Id);
            foreach (var post in posts)
            {
                Console.WriteLine("User is: {0}", user1.Name);                
                Console.WriteLine("Author: {0}, Target: {1}", post.Author.Name, post.WallOwner.Name);
                Console.WriteLine(post.Content);
            }

            var group = ctx.Groups.Find(1);
            foreach (var member in group.Members)
            {
                Console.WriteLine("Member: {0}", member.Name);
            }

            PostToGroup(1, 1, "Ho ho ho");
            int groupPostId = ctx.Postings.Where(p => p is GroupWallPost && p.AuthorId == 1).Select(p => p.Id).FirstOrDefault();
            CommentToGroupPost(2, groupPostId, "Ha ha ha");
            foreach (var post in ctx.Groups.Find(1).Postings)
            {
                Console.WriteLine("Content: {0}", post.Content);
            }
        }

        private static void CommentToGroupPost(int fromUserId, int groupPostId, string content)
        {
            var context = new SocialNetworkContext();

            var sender = context.Users.Find(fromUserId);
            var post = new GroupWallPostComment()
            {
                Content = content,
                Author = sender,
                PostedOn = DateTime.Now,
                GroupPostId = groupPostId
            };

            context.Postings.Add(post);
            context.SaveChanges();
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
            var recipientGroup = context.Groups.Find(toGroupId);
            var post = new GroupWallPost()
            {
                Content = content,
                Author = sender,
                PostedOn = DateTime.Now,
                GroupId = toGroupId
            };
            recipientGroup.Postings.Add(post);
            context.SaveChanges();
        }
    }
}
