using Context_Layer.Models;
using Data_Services_Layer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices_Repository_Layer
{
    public interface IAssessment_Options_Repository : IGenericRepository<AssessmentOption>
    {
        AssessmentOption? AddNewAssessmentOption(DtoAddNewAssessmentOption body);
        AssessmentOption? EditAssessmentOption(DtoEditAssessmentOption body);
        List<DtoGetAllAssessmentOptions> GetAllAssessmentOptions();
        DtoGetAssessmentOptionById GetAssessmentOptionsById(long id);
    }
}
