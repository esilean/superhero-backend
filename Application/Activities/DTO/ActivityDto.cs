using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Attend.DTO;
using Application.Comments.DTO;

namespace Application.Activities.DTO
{
    public class ActivityDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }

        [JsonPropertyName("attendees")]
        public ICollection<AttendeeDto> UserActivities { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}