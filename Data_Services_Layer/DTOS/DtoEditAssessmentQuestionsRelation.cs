﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public class DtoEditAssessmentQuestionsRelation
    {
        public long id { get; set; }
        public long assessment_questions_id { get; set; }
        public long assessment_id { get; set; }
    }
}
