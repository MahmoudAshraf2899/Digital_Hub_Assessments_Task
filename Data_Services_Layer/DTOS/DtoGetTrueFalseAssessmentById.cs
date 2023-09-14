using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public record DtoGetTrueFalseAssessmentById
    {
        public long id { get; init; }
        public long assessment_question_id { get; init; }
        public DateTime? created_at { get; init; }
        public string questionText { get; init; }
    }
}
