using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public class DtoAddNewTrueFalseQuestion
    {
        public long assessment_questions_Id { get; set; }
        public int is_true { get; set; }        
    }
}
