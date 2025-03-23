using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.GetUsers;

public interface IGetUsersQueryService
{
    IQueryable<GetUsersDTO> GetUsers(string? searchTerm, string? sortColumn, string? sortOrder);
}
