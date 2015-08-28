namespace TQSN.Model
{
    using System.Collections.Generic;
    
    public interface WallPosts
    {
       ICollection<Post> Wall { get; set; }
       string Type { get; }
       

    }
}
