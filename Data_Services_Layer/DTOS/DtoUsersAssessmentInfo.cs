using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public class DtoUsersAssessmentInfo
    {
        public long userId { get; set; }
        public int result { get; set; }
        public byte score { get; set; }
    }
}
