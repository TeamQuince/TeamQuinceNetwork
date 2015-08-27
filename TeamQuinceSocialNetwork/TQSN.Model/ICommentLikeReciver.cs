using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQSN.Model
{
    public interface ICommentLikeReceiver
    {
        ICollection<Comment> CommentsRecivedCollection { get; set; }
        ICollection<Like> LikesReceivedCollection { get; set; }
        string Type { get; }
    }
}
