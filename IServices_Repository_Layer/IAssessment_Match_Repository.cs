using Context_Layer.Models;
using Data_Services_Layer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices_Repository_Layer
{
    public interface IAssessment_Match_Repository : IGenericRepository<AssessmentMatch>
    {
        AssessmentMatch? AddNewAssessmentMatch(DtoAddNewAssessmentMatch body);
        AssessmentMatch? EditAssessmentMatch(DtoEditAssessmentMatch body);
        List<DtoGetAllAssessementsMatch> GetAllAssessmentsMatch();
        DtoGetAssessmentMatchById GetAssessmentMatchById(long id);
    }
}
