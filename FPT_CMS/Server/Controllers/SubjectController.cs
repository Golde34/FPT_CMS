using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DAO;
using Server.Entity;

namespace Server.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class SubjectController
{
    [HttpGet]
    public ActionResult<IEnumerable<Subject>> GetSubjects()
    {
        List<Subject> _subjects;
        try
        {
            var _subjectManagement = new SubjectManagement();
            _subjects = _subjectManagement.GetSubjects().ToList();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return _subjects;
    }
}