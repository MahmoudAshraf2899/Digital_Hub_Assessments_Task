using Assessments_Task.Helpers;
using Context_Layer.Models;
using Data_Services_Layer.DTOS;
using IServices_Repository_Layer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Services_Repository_Layer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assessments_Task.Controllers
{
    [Route("api/assessments/[controller]")]
    [ApiController]
    public class AssessmentsController : ControllerBase
    {
        private long _accountId;
        private string _language;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _accessor;
        private readonly IAssessmentsRepository _assessmentsRepository;
        private readonly IAssessmentsQuestionsRepository _assessmentsQuestionsRepository;
        private readonly ITrueOrFalseAssessmentRepository _trueOrFalseAssessmentRepository;
        private readonly IAssessment_Match_Repository _assessment_Match_Repository;
        private readonly IAssessment_Options_Repository _assessment_Options_Repository;
        private readonly IAssessment_Text_Repository _assessment_Text_Repository;
        private readonly IAssessmentQuestionsRelationRepository _assessmentQuestionsRelationRepository;
        private readonly IAssessmentEnrolRepository _assessmentEnrolRepository;
        private readonly IAssessmentAnswerRepository _assessmentAnswerRepository;
        private readonly edulmsContext _context;
        private readonly IUsersRepository _usersRepository;

        public AssessmentsController(IConfiguration config,
            IUsersRepository usersRepository,
            IHttpContextAccessor accessor,
            IAssessmentsRepository assessmentsRepository,
            IAssessmentsQuestionsRepository assessmentsQuestionsRepository,
            ITrueOrFalseAssessmentRepository trueOrFalseAssessmentRepository,
            IAssessment_Match_Repository assessment_Match_Repository,
            IAssessment_Options_Repository assessment_Options_Repository,
            IAssessment_Text_Repository assessment_Text_Repository,
            IAssessmentQuestionsRelationRepository assessmentQuestionsRelationRepository,
            IAssessmentEnrolRepository assessmentEnrolRepository,
            IAssessmentAnswerRepository assessmentAnswerRepository,
             edulmsContext context)
        {
            _config = config;
            _accessor = accessor;
            _assessmentsRepository = assessmentsRepository;
            _assessmentsQuestionsRepository = assessmentsQuestionsRepository;
            _trueOrFalseAssessmentRepository = trueOrFalseAssessmentRepository;
            _assessment_Match_Repository = assessment_Match_Repository;
            _assessment_Options_Repository = assessment_Options_Repository;
            _assessment_Text_Repository = assessment_Text_Repository;
            _assessmentQuestionsRelationRepository = assessmentQuestionsRelationRepository;
            _assessmentEnrolRepository = assessmentEnrolRepository;
            _assessmentAnswerRepository = assessmentAnswerRepository;
            _usersRepository = usersRepository;
            _context = context;
            StringValues languageHeader = "";
            StringValues tokenHeader = "";
            _accessor.HttpContext.Request.Headers.TryGetValue("Lang", out languageHeader);

            if (languageHeader.Any() == true)
            {
                _language = languageHeader[0].ToString();
            }
            else
            {
                _language = "en";
            }

            #region Extract Data From Token

            var token = _accessor.HttpContext.User.Identity;
            var identity = (ClaimsIdentity)token;
            _accessor.HttpContext.Request.Headers.TryGetValue("Authorization", out tokenHeader);

            if (tokenHeader.Any() == true)
            {
                if (tokenHeader[0] != null)
                {
                    var userInfo = TokenManagerFactor.GetUserInfo(identity);
                    if (userInfo.id != 0)
                    {
                        _accountId = userInfo.id;
                    }
                }
            }
            #endregion
        }

        #region Authorization & Authentication
        private Tuple<string, TokenResults> GenerateAdminToken(User info)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var iss = "Seven Seas Server";

            var claims = new[] {
                new Claim(ClaimTypes.Email, info.Username),
                new Claim(ClaimTypes.Actor, iss),
                new Claim(ClaimTypes.Version, info.Id.ToString()),
                new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString("D")),
                //new Claim(ClaimTypes.Dns, info.GroupPermission.ToString()),

             };
            //Let Token Expires After Three Days
            var token = new JwtSecurityToken(expires: DateTime.Now.AddDays(3), claims: claims, signingCredentials: credentials);

            var tokenValid = new JwtSecurityTokenHandler().WriteToken(token);

            var result = new TokenResults
            {
                access_token = tokenValid,
                iss = "api/Nightmare",
                sub = info.Username,
                //spi = info.GroupPermission.ToString(),
                acn = info.Email,
                aci = info.Id.ToString(),
            };
            var finalResult = Tuple.Create(tokenValid, result);
            return finalResult;
        }

        //[Authorize]
        [HttpPost]
        [Route("AddNewUser")]
        public async Task<IActionResult> AddNewUser(DtoRegister model)
        {
            //First : Check If User Name is Exist in Users Table before
            bool isExist = _context.Users.Where(C => C.Username == model.userName).Any();
            if (isExist)
            {
                //User Already Exist
                string userNameAlert = "User Name is Already Exist";
                return BadRequest(userNameAlert);
            }
            //Second : Check If Passwords Matches
            string passwordMatching = "Password Doesn't Match";
            if (model.password != model.confirmPassword)
            {
                return BadRequest(passwordMatching);
            }
            //Third : Add Operation
            #region Add New User (We Should Move It To Repository but deadline )
            User newAdmin = new User();
            newAdmin.Email = model.email;
            newAdmin.CreatedAt = DateTime.Now.Date;
            newAdmin.CreatedBy = _accountId;
            newAdmin.Username = model.userName;
            newAdmin.FirstName = model.firstName;
            newAdmin.LastName = model.lastName;
            newAdmin.DisplayName = model.firstName + "_" + model.lastName;
            newAdmin.IsBanned = 0;
            newAdmin.IsVerified = 1;
            newAdmin.Password = PasswordHash.CreateHash(model.password);
            newAdmin.ConfirmCode = "UIWEE45";

            await _usersRepository.AddAsync(newAdmin);
            await _usersRepository.SaveAsync();
            #endregion
            return Ok();

        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] DtoLogin dto)
        {
            var userObj = _usersRepository.FindBy(c => c.IsBanned == 0 && c.IsVerified > 0 && c.Username == dto.userName)
                                          .FirstOrDefault();
            if (userObj is not null)
            {
                var checkPassword = PasswordHash.CreateHash(dto.password);
                if (!PasswordHash.ValidatePassword(dto.password, userObj.Password))
                {
                    return Ok("InvalidPassword");
                }
            }
            else
            {
                return Ok("UserNotFound");
            }

            var token = GenerateAdminToken(userObj);
            #region  Update ApiKey for this user 
            /* I Left This Point it's not clear to me what api key refer to  */
            //userObj.ApiKey = token.Item1;
            //_usersRepository.Update(userObj);
            //_usersRepository.Save(); 
            #endregion

            return Ok(token.Item2);

        }

        [Authorize]
        #endregion

        #region Assessments
        [Authorize]
        [HttpGet]
        [Route("GetAllAssessments")]
        public IActionResult GetAllPublishedAssessments()
        {
            var result = _assessmentsRepository.GetAllPublishedAssessments();
            //If Result is null We Can handle it in client side with no data to display message

            return Ok(result);
        }


        [Authorize]
        [HttpGet]
        [Route("GetAssessmentById")]
        public IActionResult GetAssessmentById(long id)
        {
            var result = _assessmentsRepository.GetAssessmentById(id);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        [Route("AddNewAssessment")]
        public IActionResult AddNewAssessment(DtoAddNewAssessment body)
        {
            var result = _assessmentsRepository.AddNewAssessment(body, _accountId);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        [Route("EditAssessment")]
        public IActionResult EditAssessment(DtoEditAssessments body)
        {
            var result = _assessmentsRepository.EditAssessment(body);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        #endregion

        #region Assessments_Questions
        [Authorize]
        [HttpGet]
        [Route("GetAllAssessmentsQuestions")]
        public IActionResult GetAllAssessmentsQuestions()
        {
            var result = _assessmentsQuestionsRepository.GetAllAssessmentsQuestions();
            //If Result is null We Can handle it in client side with no data to display message

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAssessmentQuestionById")]
        public IActionResult GetAssessmentQuestionById(long id)
        {
            var result = _assessmentsQuestionsRepository.GetAssessmentQuestionById(id);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        [Route("AddNewAssessmentQuestion")]
        public IActionResult AddNewAssessmentQuestion(DtoAddNewAssessmentQuestion body)
        {
            var result = _assessmentsQuestionsRepository.AddNewAssessmentQuestion(body);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        [Route("EditAssessmentQuestion")]
        public IActionResult EditAssessmentQuestion(DtoEditAssessmentQuestion body)
        {
            var result = _assessmentsQuestionsRepository.EditAssessmentQuestion(body);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        #endregion

        #region Assessment_true_false
        [Authorize]
        [HttpPost]
        [Route("AddNewTrueFalseAssessment")]
        public IActionResult AddNewTrueFalseAssessment(DtoAddNewTrueFalseQuestion body)
        {
            var result = _trueOrFalseAssessmentRepository.AddNewTrueFalseAssessment(body);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }
        [Authorize]
        [HttpPost]
        [Route("EditTrueFalseAssessment")]
        public IActionResult EditTrueFalseAssessment(DtoEditTrueFalseAssessment body)
        {
            var result = _trueOrFalseAssessmentRepository.EditTrueFalseAssessment(body);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllTrueFalseAssessmentQuestions")]
        public IActionResult GetAllTrueFalseAssessmentQuestions()
        {
            //If Result is null We Can handle it in client side with no data to display message
            var result = _trueOrFalseAssessmentRepository.GetAllTrueFalseAssessmentQuestions();
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("GetTrueFalseAssessmentQuestionById")]
        public IActionResult GetTrueFalseAssessmentQuestionById(long id)
        {

            var result = _trueOrFalseAssessmentRepository.GetTrueFalseAssessmentQuestionById(id);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }
        #endregion

        #region Assessemnt_Match
        [Authorize]
        [HttpPost]
        [Route("AddNewAssessmentMatch")]
        public IActionResult AddNewAssessmentMatch(DtoAddNewAssessmentMatch body)
        {
            var result = _assessment_Match_Repository.AddNewAssessmentMatch(body);
            if (result is not null)//We Can Handle returned Result Here by return Tuple but deadline doesn't help me..
                return Ok(result);

            return BadRequest();
        }


        [Authorize]
        [HttpPost]
        [Route("EditAssessmentMatch")]
        public IActionResult EditAssessmentMatch(DtoEditAssessmentMatch body)
        {
            var result = _assessment_Match_Repository.EditAssessmentMatch(body);
            if (result is not null)//We Can Handle returned Result Here by return Tuple but deadline doesn't help me..
                return Ok(result);

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllAssessmentsMatch")]
        public IActionResult GetAllAssessmentsMatch()
        {
            //If Result is null We Can handle it in client side with no data to display message
            var result = _assessment_Match_Repository.GetAllAssessmentsMatch();
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAssessmentMatchById")]
        public IActionResult GetAssessmentMatchById(long id)
        {

            var result = _assessment_Match_Repository.GetAssessmentMatchById(id);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        #endregion

        #region Assessment_Options
        [Authorize]
        [HttpPost]
        [Route("AddNewAssessmentOption")]
        public IActionResult AddNewAssessmentOption(DtoAddNewAssessmentOption body)
        {
            var result = _assessment_Options_Repository.AddNewAssessmentOption(body);
            if (result is not null)//We Can Handle returned Result Here by return Tuple but deadline doesn't help me..
                return Ok(result);

            return BadRequest();
        }


        [Authorize]
        [HttpPost]
        [Route("EditAssessmentOption")]
        public IActionResult EditAssessmentOption(DtoEditAssessmentOption body)
        {
            var result = _assessment_Options_Repository.EditAssessmentOption(body);
            if (result is not null)//We Can Handle returned Result Here by return Tuple but deadline doesn't help me..
                return Ok(result);

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllAssessmentOptions")]
        public IActionResult GetAllAssessmentOptions()
        {
            //If Result is null We Can handle it in client side with no data to display message
            var result = _assessment_Options_Repository.GetAllAssessmentOptions();
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAssessmentOptionsById")]
        public IActionResult GetAssessmentOptionsById(long id)
        {

            var result = _assessment_Options_Repository.GetAssessmentOptionsById(id);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }
        #endregion

        #region Assessment_Text
        [Authorize]
        [HttpPost]
        [Route("AddNewAssessmentText")]
        public IActionResult AddNewAssessmentText(DtoAddNewAssessmentText body)
        {
            var result = _assessment_Text_Repository.AddNewAssessmentText(body);
            if (result is not null)//We Can Handle returned Result Here by return Tuple but deadline doesn't help me..
                return Ok(result);

            return BadRequest();
        }


        [Authorize]
        [HttpPost]
        [Route("EditAssessmentText")]
        public IActionResult EditAssessmentText(DtoEditAssessmentText body)
        {
            var result = _assessment_Text_Repository.EditAssessmentText(body);
            if (result is not null)//We Can Handle returned Result Here by return Tuple but deadline doesn't help me..
                return Ok(result);

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllAssessmentText")]
        public IActionResult GetAllAssessmentText()
        {
            //If Result is null We Can handle it in client side with no data to display message
            var result = _assessment_Text_Repository.GetAllAssessmentText();
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAssessmentTextById")]
        public IActionResult GetAssessmentTextById(long id)
        {

            var result = _assessment_Text_Repository.GetAssessmentTextById(id);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }
        #endregion

        #region Assessment Questions Relation 
        [Authorize]
        [HttpPost]
        [Route("AddNewAssessmentQuestionsRelation")]
        public IActionResult AddNewAssessmentQuestionsRelation(DtoAddNewAssessmentQuestionsRelation body)
        {
            var result = _assessmentQuestionsRelationRepository.AddNewAssessmentQuestionsRelation(body);
            if (result is not null)//We Can Handle returned Result Here by return Tuple but deadline doesn't help me..
                return Ok(result);

            return BadRequest();
        }


        [Authorize]
        [HttpPost]
        [Route("EditAssessmentQuestionsRelation")]
        public IActionResult EditAssessmentQuestionsRelation(DtoEditAssessmentQuestionsRelation body)
        {
            var result = _assessmentQuestionsRelationRepository.EditAssessmentQuestionsRelation(body);
            if (result is not null)//We Can Handle returned Result Here by return Tuple but deadline doesn't help me..
                return Ok(result);

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllAssessmentQuestions")]
        public IActionResult GetAllAssessmentQuestions()
        {
            //If Result is null We Can handle it in client side with no data to display message
            var result = _assessmentQuestionsRelationRepository.GetAllAssessmentQuestions();
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAssessmentQuestionsById")]
        public IActionResult GetAssessmentQuestionsById(long id)
        {

            var result = _assessmentQuestionsRelationRepository.GetAssessmentQuestionsById(id);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }
        #endregion

        #region Assessment Enrol
        [Authorize]
        [HttpPost]
        [Route("AssignUsersToAssessment")]
        public IActionResult AssignUsersToAssessment(DtoAssignUsersToAssessment body)
        {
            var result = _assessmentEnrolRepository.AssignUsersToAssessment(body);
            if (result == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        [Route("RemoveUserFromAssessment")]
        public IActionResult RemoveUserFromAssessment(long userId, long assessmentId)
        {
            var result = _assessmentEnrolRepository.RemoveUserFromAssessment(userId, assessmentId);
            if (result == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        [Route("GetUsersOnSpecificAssessment")]
        public IActionResult GetUsersOnSpecificAssessment(long assessmentId)
        {
            var result = _assessmentEnrolRepository.GetUsersOnSpecificAssessment(assessmentId);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }


        #endregion

        #region Assessment Answers
        [Authorize]
        [HttpPost]
        [Route("AddUserAnswers")]
        public IActionResult AddUserAnswers(DtoAddUserAnswers body)
        {
            var result = _assessmentAnswerRepository.AddUserAnswers(body);
            if (result is not null)//We Can Handle returned Result Here by return Tuple but deadline doesn't help me..
                return Ok(result);

            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        [Route("EditUserAnswers")]
        public IActionResult EditUserAnswers(DtoEditUserAnswers body)
        {
            var result = _assessmentAnswerRepository.EditUserAnswers(body);
            if (result is not null)//We Can Handle returned Result Here by return Tuple but deadline doesn't help me..
                return Ok(result);

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        [Route("GetAssessmentAnswersForUser")]
        public IActionResult GetAssessmentAnswersForUser(long userId)
        {
            var result = _assessmentAnswerRepository.GetAssessmentAnswersForUser(userId);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        [Route("GetAssessmentAnswersForUserWithSpecificAssessment")]
        public IActionResult GetAssessmentAnswersForUserWithSpecificAssessment(long userId, long assessmentId)
        {
            var result = _assessmentAnswerRepository.GetAssessmentAnswersForUserWithSpecificAssessment(userId, assessmentId);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }
        #endregion
    }
}
