using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Server.DAO;
using Server.Entity;

namespace Server.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class SemesterController
{
    public ActionResult<List<Semester>> GetSemesters()
    {
        List<Semester> _semesters;
        try
        {
            var _semesterManagement = new SemesterManagement();
            _semesters = _semesterManagement.GetSemesters().ToList();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return _semesters;
    }
}