using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public record DtoGetAllAssessmentQuestions
    {
        public long id { get; init; }
        public int level { get; init; }
        public int order { get; init; }
        public int assessmentDuration { get; init; }
        public string question { get; init; }
        public string type { get; init; }
        public string assessmentTitle { get; init; }
        public string assessmentShortDescription { get; init; }
        public string assessmentDescription { get; init; }
        public string assessmentSlug { get; init; }
        public DateTime? created_at { get; init; }
        public DateTime? updated_at { get; init; }
    }
}
