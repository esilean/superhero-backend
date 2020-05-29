using System;

namespace Domain.Entities
{
    public class UserActivity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        public DateTime DateJoined { get; set; }
        public bool IsHost { get; set; }
    }
}