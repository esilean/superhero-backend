using System.Collections.Generic;

namespace Application.Activities.DTO
{
    public class ActivitiesEnvelope
    {
        public List<ActivityDto> Activities { get; set; }
        public int ActivityCount { get; set; }
    }
}