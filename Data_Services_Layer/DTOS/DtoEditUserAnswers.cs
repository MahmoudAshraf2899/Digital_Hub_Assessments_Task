using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public class DtoEditUserAnswers
    {
        public long id { get; set; }
        public string answer { get; set; }
        public byte score { get; set; }
    }
}
