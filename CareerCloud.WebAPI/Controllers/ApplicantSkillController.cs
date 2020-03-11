using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantSkillController : ControllerBase
    {
        private ApplicantSkillLogic _logic;

        public ApplicantSkillController()
        {
            var repo = new EFGenericRepository<ApplicantSkillPoco>();
            _logic = new ApplicantSkillLogic(repo);
        }

        [HttpPost]
        [Route("skill/")]
        public ActionResult PostApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
        {
            _logic.Add(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("skill/{applicantProfileId}")]
        public ActionResult GetApplicantSkill(Guid applicantSkillId)
        {
            ApplicantSkillPoco poco = _logic.Get(applicantSkillId);
            if (poco == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(poco);
            }
        }

        [HttpPut]
        [Route("skill/")]
        public ActionResult PutApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
        {
            _logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("skill/")]
        public ActionResult DeleteApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("skill/")]
        public ActionResult GetAllApplicantSkill()
        {
            List<ApplicantSkillPoco> pocos = _logic.GetAll();

            if (pocos == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pocos);
            }
        }
    }
}