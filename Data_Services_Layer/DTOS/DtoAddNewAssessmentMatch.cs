using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public class DtoAddNewAssessmentMatch
    {
        public long assessment_question_id { get; set; }
        public int order { get; set; }
        public string option { get; set; }
        public string answer { get; set; }
        public string AnswerIdKey { get; set; }
        public string QuestionIdKey { get; set; }
        public DateTime? created_at { get; set; }
    }
}
