using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public record DtoUserInfo
    {
        public long id { get; init; }        
        public string userName { get; set; }
        
    }
}
