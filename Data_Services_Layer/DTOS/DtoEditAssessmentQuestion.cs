using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public class DtoEditAssessmentQuestion
    {
        public long id { get; set; }
        public string question { get; set; }
        public string type { get; set; }
        public int level { get; set; }
        public int order { get; set; }

    }
}
