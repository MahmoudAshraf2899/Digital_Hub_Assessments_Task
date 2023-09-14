using Context_Layer.Models;
using Data_Services_Layer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices_Repository_Layer
{
    public interface IAssessment_Text_Repository : IGenericRepository<AssessmentText>
    {
        AssessmentText? AddNewAssessmentText(DtoAddNewAssessmentText body);
        AssessmentText? EditAssessmentText(DtoEditAssessmentText body);
        List<DtoGetAllAssessmentText> GetAllAssessmentText();
        DtoGetAssessmentTextById GetAssessmentTextById(long id);
    }
}
