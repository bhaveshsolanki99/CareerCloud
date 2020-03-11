using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        {
        }

        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (ApplicantSkillPoco poco in pocos)
            {
                if (poco.StartMonth > 12)
                {
                    exceptions.Add(new ValidationException(101, "StartMonth field cannot be greater than 12."));
                }
                if (poco.EndMonth > 12)
                {
                    exceptions.Add(new ValidationException(102, "EndMonth field cannot be greater than 12."));
                }
                if(poco.StartYear < 1900)
                {
                    exceptions.Add(new ValidationException(103, "StartYear field cannot be less than 1900."));
                }
                if (poco.EndYear < poco.StartYear)
                {
                    exceptions.Add(new ValidationException(104, "EndYear field cannot be less than StartYear field."));
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
