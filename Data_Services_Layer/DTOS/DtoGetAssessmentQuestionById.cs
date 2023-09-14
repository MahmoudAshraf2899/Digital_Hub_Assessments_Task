using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public record DtoGetAssessmentQuestionById
    {         
        public int order { get; init; }
        public int level { get; init; }
        public string question { get; init; }
        public string type { get; init; }
    }
}
