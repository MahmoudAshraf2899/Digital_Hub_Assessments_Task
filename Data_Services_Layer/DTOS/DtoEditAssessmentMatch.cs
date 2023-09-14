using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public class DtoEditAssessmentMatch
    {
        public long id { get; set; }
        public long assessment_question_id { get; set; }
        public int order { get; set; }
        public string answer_id_key { get; set; }
        public string question_id_key { get; set; }
        public string option { get; set; }
        public string answer { get; set; }
    }
}
