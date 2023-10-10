using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatDotnetConduit.Domain.Common;

namespace DatDotnetConduit.Domain.Entities
{
    public class Follow : BaseEntity<Guid>
    {
        public Guid FollowingId { get; set; }
        public User Following { get; set; }
        public Guid FollowerId { get; set; }
        public User Follower { get; set; }
    }
}
