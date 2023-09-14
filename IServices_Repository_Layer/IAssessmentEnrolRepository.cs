using Context_Layer.Models;
using Data_Services_Layer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices_Repository_Layer
{
    public interface IAssessmentEnrolRepository : IGenericRepository<AssessmentEnrol>
    {
        bool? AssignUsersToAssessment(DtoAssignUsersToAssessment body);
        bool? RemoveUserFromAssessment(long userId, long assessmentId);
        List<DtoGetUsersOnSpecificAssessment> GetUsersOnSpecificAssessment(long assessmentId);

    }
}
