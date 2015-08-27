﻿namespace SocialNetwork.ConsoleClient
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
            //AddUserToGroup(1, 3);
            //AddUserToGroup(2, 3);
            //AddUserToGroup(1, 4);

            var context = new SocialNetworkContext();
            var posts = context.Users.Find(1).Posts;
            //foreach (var post in posts)
            //{
            //    Console.WriteLine("Author: {0}, Content: {1}", post.Author.Name, post.Content);
            //    Console.WriteLine(post.Content);
            //}

            var group = context.Groups.Find(3);
            foreach (var member in group.Members)
            {
                Console.WriteLine("Member: {0}", member.Name);
            }
            PostToGroup(1, 3, "Ho ho ho");
            PostToGroup(2, 3, "Ha ha ha");
            foreach (var post in group.Wall.Posts)
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
                Username = "straho",
                Wall = new Wall()
            };

            var plamena = new User()
            {
                Name = "Plamena",
                Username = "plami",
                Wall = new Wall()
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
            var post = new Post()
            {
                Content = content,
                Author = sender,
                PostedOn = DateTime.Now
            };

            recipient.Wall.Posts.Add(post);
            context.SaveChanges();
        }

        public static void CommentToUserPost(int fromUserId, int toUserPostId, string content)
        {
            var context = new SocialNetworkContext();

            var sender = context.Users.Find(fromUserId);
            var post = context.Posts.Find(toUserPostId);
            var comment = new Comment()
            {
                Content = content,
                Author = sender,
                PostedOn = DateTime.Now,
            };

            post.Comments.Add(comment);
            context.SaveChanges();
        }

        public static void LikeUserPost(int userId, int userPostId)
        {
            var context = new SocialNetworkContext();

            var user = context.Users.Find(userId);
            var post = context.Posts.Find(userPostId);
            var like = new PostLike()
            {
                Author = user,
            };
            post.Likes.Add(like);
            context.SaveChanges();
        }

        public static void AddGroups()
        {
            var context = new SocialNetworkContext();
            var teamGroup = new Group()
            {
                Name = "Team Quince",
                Description = "A great team",
                CreatedOn = DateTime.Now,
                Wall = new Wall()
            };
            var blagoGroup = new Group()
            {
                Name = "Blagoevgrad",
                Description = "A nice city",
                CreatedOn = DateTime.Now,
                Wall = new Wall()
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
            var group = context.Groups.Find(toGroupId);
            var post = new Post()
            {
                Content = content,
                Author = sender,
                PostedOn = DateTime.Now,
            };
            group.Wall.Posts.Add(post);
            context.SaveChanges();
        }
    }
}
