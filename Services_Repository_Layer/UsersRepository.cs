using Context_Layer.Models;
using IServices_Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Repository_Layer
{
    public class UsersRepository : GenericRepository<edulmsContext, User>, IUsersRepository
    {
    }
}
