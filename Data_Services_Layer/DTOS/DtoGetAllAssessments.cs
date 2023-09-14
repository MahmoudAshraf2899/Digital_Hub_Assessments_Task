using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public record DtoGetAllAssessments
    {
        public long Id { get; init; }
        public int duration { get; init; }
        public string description { get; init; }
        public string short_description { get; init; }
        public string slug { get; init; }       
        public DateTime? created_at { get; init; }
        public DateTime? updated_at { get; init; }
    }
}
