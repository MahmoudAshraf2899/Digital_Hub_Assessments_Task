﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public class DtoAssignUsersToAssessment
    {
        public long assessment_id { get; set; }
        public List<DtoUsersAssessmentInfo> usersInformations { get; set; }
         
    }
}
