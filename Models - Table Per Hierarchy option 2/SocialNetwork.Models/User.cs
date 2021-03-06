﻿namespace SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        private ICollection<Posting> postings;
        private ICollection<Group> groups;
        //private ICollection<UserWallPostComment> userWallPostsComments;
        //private ICollection<GroupWallPost> groupWallPosts;
        //private ICollection<GroupWallPostComment> groupWallPostComments;

        public User()
        {
            this.postings = new HashSet<Posting>();
            this.groups = new HashSet<Group>();
            //this.userWallPostsComments = new HashSet<UserWallPostComment>();
            //this.groupWallPosts = new HashSet<GroupWallPost>();
            //this.groupWallPostComments = new HashSet<GroupWallPostComment>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        public string ProfilePicture { get; set; }

        public string WallPicture { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Username { get; set; }

        public virtual ICollection<Posting> Postings
        {
            get
            {
                return this.postings;
            }

            set
            {
                this.postings = value;
            }
        }

        public virtual ICollection<Group> Groups
        {
            get
            {
                return this.groups;
            }

            set
            {
                this.groups = value;
            }
        }

        //public virtual ICollection<UserWallPostComment> UserWallPostsComments
        //{
        //    get
        //    {
        //        return this.userWallPostsComments;
        //    }

        //    set
        //    {
        //        this.userWallPostsComments = value;
        //    }
        //}
        //public virtual ICollection<GroupWallPost> GroupWallPosts
        //{
        //    get
        //    {
        //        return this.groupWallPosts;
        //    }

        //    set
        //    {
        //        this.groupWallPosts = value;
        //    }
        //}

        //public virtual ICollection<GroupWallPostComment> GroupWallPostComment
        //{
        //    get
        //    {
        //        return this.groupWallPostComments;
        //    }

        //    set
        //    {
        //        this.groupWallPostComments = value;
        //    }
        //}
    }
}
