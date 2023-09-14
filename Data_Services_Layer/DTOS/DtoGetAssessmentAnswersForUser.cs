using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public record DtoGetAssessmentAnswersForUser
    {
        public string userName { get; init; }
        public string assessmentTitle { get; init; }
        public string assessmentDescription { get; init; }
        public string assessmentShortDescription { get; init; }
        public string questionText { get; init; }
        public string answer { get; init; }
        public byte score { get; init; }
        public DateTime? created_at { get; init; }
        public DateTime? updated_at { get; init; }
    }
}
