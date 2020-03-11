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
    public class ApplicantProfileController : ControllerBase
    {
        private ApplicantProfileLogic _logic;

        public ApplicantProfileController()
        {
            var repo = new EFGenericRepository<ApplicantProfilePoco>();
            _logic = new ApplicantProfileLogic(repo);
        }

        [HttpPost]
        [Route("profile/")]
        public ActionResult PostApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)
        {
            _logic.Add(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("profile/{applicantProfileId}")]
        public ActionResult GetApplicantProfile(Guid applicantProfileId)
        {
            ApplicantProfilePoco poco = _logic.Get(applicantProfileId);
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
        [Route("profile/")]
        public ActionResult PutApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)
        {
            _logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("profile/")]
        public ActionResult DeleteApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("profile/")]
        public ActionResult GetAllApplicantProfile()
        {
            List<ApplicantProfilePoco> pocos = _logic.GetAll();

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