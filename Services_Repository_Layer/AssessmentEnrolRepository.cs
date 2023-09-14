using Context_Layer.Models;
using Data_Services_Layer.DTOS;
using IServices_Repository_Layer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Repository_Layer
{
    public class AssessmentEnrolRepository : GenericRepository<edulmsContext, AssessmentEnrol>, IAssessmentEnrolRepository
    {
        private bool CheckIfAssessmentExist(long id)
        {

            var isExist = Context.Assessments.AsNoTracking()
                                                          .Where(c => c.Id == id)
                                                          .Any();
            return isExist;
        }
        private bool CheckIfUserIsExist(long id)
        {

            var isExist = Context.Users.AsNoTracking()
                                                     .Where(c => c.Id == id)
                                                     .Any();
            return isExist;
        }
        public bool? AssignUsersToAssessment(DtoAssignUsersToAssessment body)
        {
            if (body is not null)
            {
                var checkIfAssessmentExist = CheckIfAssessmentExist(body.assessment_id);
                if (checkIfAssessmentExist)
                {
                    if (body.usersInformations.Count > 0)
                    {
                        List<AssessmentEnrol> ListOfEnrolled = new List<AssessmentEnrol>();
                        foreach (var user in body.usersInformations)
                        {

                            var checkIfUserIsExist = CheckIfUserIsExist(user.userId);
                            if (checkIfUserIsExist)
                            {
                                AssessmentEnrol assessmentEnrol = new AssessmentEnrol()
                                {
                                    AssessmentId = body.assessment_id,
                                    CreatedAt = DateTime.Now.Date,
                                    UserId = user.userId,
                                    Score = user.score,
                                    Result = user.score,
                                };
                                ListOfEnrolled.Add(assessmentEnrol);
                            }
                        }
                        AddRange(ListOfEnrolled);
                        Save();
                        return true;
                    }
                    return false;

                }
                return false;
            }
            return false;
        }

        public List<DtoGetUsersOnSpecificAssessment> GetUsersOnSpecificAssessment(long assessmentId)
        {
            var result = (from q in Context.AssessmentEnrols.AsNoTracking()
                          where q.AssessmentId == assessmentId
                          select new DtoGetUsersOnSpecificAssessment
                          {
                              userId = q.User.Id,
                              displayName = q.User.DisplayName,
                              email = q.User.Email,
                              firstName = q.User.FirstName,
                              lastName = q.User.LastName,
                              isBanned = q.User.IsBanned,
                              userName = q.User.Username
                          }).OrderByDescending(c => c.userId).ToList();
            return result;
        }
        public bool? RemoveUserFromAssessment(long userId, long assessmentId)
        {
            var targetObject = FindBy(c => c.AssessmentId == assessmentId && c.UserId == userId).FirstOrDefault();
            if (targetObject is not null)
            {
                Delete(targetObject);
                Save();
                return true;
            }
            return false;
        }
    }
}
