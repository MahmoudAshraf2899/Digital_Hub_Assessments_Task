using Context_Layer.Models;
using Data_Services_Layer.DTOS;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;

namespace Assessments_Task.Helpers
{
    public class TokenManagerFactor
    {
        public static DtoUserInfo GetUserInfo(IIdentity token)
        {
            try
            {
                var identity = (ClaimsIdentity)token;
                List<Claim> claims = identity.Claims.ToList();
                var account = new DtoUserInfo();

                var sub = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
                var dns = claims.FirstOrDefault(x => x.Type == ClaimTypes.Dns);

                if (sub != null)
                {
                    using var context = new edulmsContext();
                    account = context.Users.Where(x => x.Username == sub.Value &&  x.IsBanned != 0)
                    .Select(x => new DtoUserInfo
                    {
                        id = x.Id,
                        userName = x.Username,


                    }).FirstOrDefault();

                    if (account == null)
                    {

                        return new DtoUserInfo();
                    }
                    
                    return account;
                }

                return account;
            }
            catch (Exception ex)
            {
                DtoUserInfo info = new DtoUserInfo();
                info.userName = ex.Message;
                return info;

            }
        }
    }
}
