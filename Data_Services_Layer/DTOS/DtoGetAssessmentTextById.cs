using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public record DtoGetAssessmentTextById
    {
        public int order { get; init; }
        public string answer { get; init; }
        public string questionText { get; init; }
        public DateTime? created_at { get; init; }
        public DateTime? updated_at { get; init; }
    }
}
