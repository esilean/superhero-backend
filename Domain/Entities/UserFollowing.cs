using System;

namespace Domain.Entities
{
    public class UserFollowing
    {
        public Guid ObserverId { get; set; }
        public virtual User Observer { get; set; }
        public Guid TargetId { get; set; }
        public virtual User Target { get; set; }
    }
}