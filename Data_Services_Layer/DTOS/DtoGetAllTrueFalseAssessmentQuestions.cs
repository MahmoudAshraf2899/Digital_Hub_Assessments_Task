using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public record DtoGetAllTrueFalseAssessmentQuestions
    {
        public long id { get; init; }
        public long assessment_questions_Id { get; init; }
        public int is_true { get; init; }
        public DateTime? created_at    { get; init; }
        public DateTime? updated_at { get; init; }
    }
}
