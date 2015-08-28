namespace ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TQSN.Data;
    using TQSN.Model;

    class Client
    {
        static void Main(string[] args)
        {
            var context = new ApplicationDbContext();
            /*
               AddUsers(context);
               context.SaveChanges();
               AddGroups(context);
               context.SaveChanges();
               AddPosts(context);
               AddUsersToGroups(context);
               AddCommentToPost(context);
               AddCommentToComment(context);
               AddLikeToPost(context);
               AddLikeToComment(context);
             */
             


            // GetAllReceivedCommentsFromPost(context);
           // GetAllREceivedCommentsFromComment(context);
            //TO DO method for testing - GET ALL COMMENTS with RECcursion
            //Console.WriteLine(context.Posts.Find(1).LikesReceivedCollection.Count);

            //GetAllCommentsByUser(context);
            ////TO DO method for testing - GET ALL LIkes by User
        

          //GetAllPostsOnWall(context);
          //GetPostAuthoredByUser(context);


          var start = context.Posts.Count();
            Console.WriteLine(start);
          

        }

        private static void GetAllCommentsByUser(ApplicationDbContext context)
        {
            var userComments = context.Users.FirstOrDefault(p => p.UserName == "pepster").CommentAuthoredCollection;
            var user2 = context.Users.FirstOrDefault(u => u.UserName == "straho").CommentAuthoredCollection;
            var user3 = context.Users.FirstOrDefault(u => u.UserName == "nadq");

            foreach (var comment in userComments)
            {
                Console.WriteLine("User: {0}, Comment{1}, Comment Reciever{2}", comment.Author.UserName, comment.Content, comment.CommentReceiver.Type);
            }
         //   var commentsDone = context.Users.
        }

        private static void AddLikeToComment(ApplicationDbContext context)
        {
            var user = context.Users.FirstOrDefault(p => p.UserName == "pepster");
            var user2 = context.Users.FirstOrDefault(u => u.UserName == "straho");
            var user3 = context.Users.FirstOrDefault(u => u.UserName == "nadq");

            context.Likes.Add(new Like()
            {
                Author = user,
                LikeReceiverCommentId = 1
            });
            context.SaveChanges();
            context.Likes.Add(new Like()
            {
                Author = user2,
                LikeReceiverCommentId = 1
            });
            context.SaveChanges();
            
        }

        private static void AddLikeToPost(ApplicationDbContext context)
        {
            var user = context.Users.FirstOrDefault(p => p.UserName == "pepster");
            var user2 = context.Users.FirstOrDefault(u => u.UserName == "straho");
            var user3 = context.Users.FirstOrDefault(u => u.UserName == "nadq");

            context.Likes.Add(new Like()
            {
                Author = user,
                LikeReceiverPostId = 1
            });
            context.SaveChanges();
            context.Likes.Add(new Like()
            {
                Author = user2,
                LikeReceiverPostId = 1
            });
            context.SaveChanges();
        }

        private static void GetAllREceivedCommentsFromComment(ApplicationDbContext context)
        {
            var RecievedComments = context.Comments.FirstOrDefault(c => c.Id == 1).CommentsRecivedCollection;
            foreach (var comment in RecievedComments)
            {
                Console.WriteLine(comment.Content);
            }
        }

        private static void GetAllReceivedCommentsFromPost(ApplicationDbContext context)
        {
            var RecievedComments = context.Posts.FirstOrDefault(p => p.Id == 1).CommentsRecivedCollection.ToList();
            foreach (var coment in RecievedComments)
            {
                Console.WriteLine(coment.Content);
            }
        }

        private static void AddCommentToComment(ApplicationDbContext context)
        {
            var user = context.Users.FirstOrDefault(p => p.UserName == "pepster");
            var user2 = context.Users.FirstOrDefault(u => u.UserName == "straho");
            var user3 = context.Users.FirstOrDefault(u => u.UserName == "nadq");

            var comment = new Comment()
            {
                AuthorId = user2.Id,
                Content = "comment po coment 1 ot post 1 from straho",
                PostedOn = DateTime.Now,
                CommentReciverCommentId = 1
            };
            context.Comments.Add(comment);
            context.SaveChanges();
            var comment2 = new Comment()
            {
                AuthorId = user.Id,
                Content = "comment po coment na comment1 ot post 1 from pepster",
                PostedOn = DateTime.Now,
                CommentReciverComment = comment
            };
            context.Comments.Add(comment2);
            context.SaveChanges();
        }

        private static void AddCommentToPost(ApplicationDbContext context)
        {
            var user = context.Users.FirstOrDefault(p => p.UserName == "pepster");
            var user2 = context.Users.FirstOrDefault(u => u.UserName == "straho");
            var user3 = context.Users.FirstOrDefault(u => u.UserName == "nadq");

            var post = context.Posts.Find(3);
            var comment = new Comment()
            {
                AuthorId    = user.Id,
                Content = "comment po post 1 from nadq",
                PostedOn = DateTime.Now,
                CommentReciverPostId = 1
            };
            context.Comments.Add(comment);
            context.SaveChanges();
        }

        private static void AddUsersToGroups(ApplicationDbContext context)
        {
            var user = context.Users.FirstOrDefault(p => p.UserName == "pepster");
            var user2 = context.Users.FirstOrDefault(u => u.UserName == "straho");
            var user3 = context.Users.FirstOrDefault(u => u.UserName == "nadq");


            var groups = context.Groups.ToList();
            foreach (var group in groups)
            {
                group.Members.Add(user);
                group.Members.Add(user2);
                group.Members.Add(user3);
            }

            var totalGroups = context.Groups.ToList();
            foreach (var group in totalGroups)
            {
                Console.WriteLine(group.Name);
                foreach (var member in group.Members)
                {
                    Console.WriteLine(member.UserName);
                }
            }

            foreach (var group in user.Groups.ToList())
            {
                Console.WriteLine(group.Name);
            }


        }

        private static void GetPostAuthoredByUser(ApplicationDbContext context)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == "pepster");
            var posts = context.Users.FirstOrDefault(u => u.Id == user.Id).PostsAuthored;
            foreach (var post in posts)
            {
                Console.WriteLine("PostId: " + post.Id);
                Console.WriteLine("postedOn: "+ post.PostedOn);
                
                Console.WriteLine(" Posted on Wall: "+post.Wall.Type);
                Console.WriteLine(" PostContent: "+post.Content);
                Console.WriteLine("postAuthor: "+post.Author.UserName);
            }
        }

        private static void GetAllPostsOnWall(ApplicationDbContext context)
        {
            Console.WriteLine("Pepster's UserWall");
            var user = context.Users.FirstOrDefault(u => u.UserName == "pepster");
            var postsbyUser = user.Wall;
            foreach (var post in postsbyUser)
            {
                Console.WriteLine(post.Content);
            }
            Console.WriteLine("Group 1 Wall");
            var postsByGroupId = context.Groups.FirstOrDefault(g => g.Id == 1).Wall;
            foreach (var post in postsByGroupId)
            {
                Console.WriteLine(post.Content);
            }
        }

        private static void AddPosts(ApplicationDbContext context)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == "pepster");
            var user2 = context.Users.FirstOrDefault(u => u.UserName == "straho");
            var user3 = context.Users.FirstOrDefault(u => u.UserName == "nadq");

            context.Posts.Add(new Post()
            {
                Content = "post1 wyw grupa 1",
                WallOwnerGroupId = 1,
                PostedOn = DateTime.Now,
                AuthorId =  user.Id

            });
                context.Posts.Add(new Post()
            {
                Content = "post2 wyw grupa 1",
                WallOwnerGroupId = 1,
                PostedOn = DateTime.Now,
                AuthorId = user2.Id

            });
                     context.Posts.Add(new Post()
            {
                Content = "post wyw grupa 1",
                WallOwnerGroupId = 1,
                PostedOn = DateTime.Now,
                AuthorId = user3.Id

            });
            
            context.Posts.Add(new Post()
            {
                Content = "post na stenata na pepster",
                WallOwnerUser = user,
                PostedOn = DateTime.Now,
                AuthorId = user.Id 
                
            });
            context.Posts.Add(new Post()
            {
                Content = "wtori dubliran post na stenata na pepster",
                WallOwnerUser = user,
                PostedOn = DateTime.Now,
                AuthorId = user2.Id
            });
            context.Posts.Add(new Post()
            {
                Content = "wtori dubliran post na stenata na pepster",
                WallOwnerUser = user,
                PostedOn = DateTime.Now,
                AuthorId = user3.Id
            });
            context.SaveChanges();
        }

        private static void AddGroups(ApplicationDbContext context)
        {
            context.Groups.Add(new Group()
            {
                Name = "Group 1"
            });
            
            context.Groups.Add(new Group()
            {
                Name = "Group 2"
            });
            context.Groups.Add(new Group()
            {
                Name = "Group 3"
            });
            context.SaveChanges();
        }

        private static void AddUsers(ApplicationDbContext context)
        {
            context.Users.Add(new ApplicationUser()
            {
                UserName = "pepster"
            });
           
            context.Users.Add(new ApplicationUser()
            {
                UserName = "nadq"
            });
            context.Users.Add(new ApplicationUser()
            {
                UserName = "straho"
            });

            context.SaveChanges();
        }
    }
}
