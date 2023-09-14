using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public record DtoGetAssessmentById
    {
        public long Id { get; init; }
        public string title { get; init; }
        public string description { get; init; }
        public string slug { get; init; }
        public string short_description { get; init; }
        public string isPublishedStatus { get; init; }
        public int duration { get; init; }
        public DateTime? createdAt { get; init; }
        public DateTime? updatedAt { get; init; }

    }
}
