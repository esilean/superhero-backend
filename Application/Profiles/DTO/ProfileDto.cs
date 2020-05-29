using Domain.Entities;
using System.Collections.Generic;

namespace Application.Profiles.DTO
{
    public class ProfileDto
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}