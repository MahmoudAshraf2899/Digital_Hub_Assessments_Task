﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public class DtoAddNewAssessmentText
    {
        public long assessment_question_id { get; set; }
        public int order { get; set; }
        public string answer { get; set; }
    }
}
