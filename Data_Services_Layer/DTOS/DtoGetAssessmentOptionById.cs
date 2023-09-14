using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Services_Layer.DTOS
{
    public record DtoGetAssessmentOptionById
    {
        public int order { get; init; }
        public byte correct { get; init; }
        public string option { get; init; }
        public string questionText { get; init; }
    }
}
