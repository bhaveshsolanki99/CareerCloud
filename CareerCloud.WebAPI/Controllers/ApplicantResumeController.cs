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
    public class ApplicantResumeController : ControllerBase
    {
        private ApplicantResumeLogic _logic;

        public ApplicantResumeController()
        {
            var repo = new EFGenericRepository<ApplicantResumePoco>();
            _logic = new ApplicantResumeLogic(repo);
        }

        [HttpPost]
        [Route("resume/")]
        public ActionResult PostApplicantResume([FromBody] ApplicantResumePoco[] pocos)
        {
            _logic.Add(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("resume/{applicantResumeId}")]
        public ActionResult GetApplicantResume(Guid applicantResumeId)
        {
            ApplicantResumePoco poco = _logic.Get(applicantResumeId);
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
        [Route("resume/")]
        public ActionResult PutApplicantResume([FromBody] ApplicantResumePoco[] pocos)
        {
            _logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("resume/")]
        public ActionResult DeleteApplicantResume([FromBody] ApplicantResumePoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("resume/")]
        public ActionResult GetAllApplicantResume()
        {
            List<ApplicantResumePoco> pocos = _logic.GetAll();

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