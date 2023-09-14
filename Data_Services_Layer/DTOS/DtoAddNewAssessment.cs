using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public class DtoAddNewAssessment
    {
        public string title { get; set; }
        public string short_description { get; set; }
        public string description { get; set; }
        public string slug { get; set; }
        public byte published { get; set; }
        public int duration { get; set; }
        public int? category_id { get; set; }
    }
}
