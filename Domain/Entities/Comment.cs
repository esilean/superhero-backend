using System;

namespace Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public virtual User Author { get; set; }
        public virtual Activity Activity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}