using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobEducationLogic : BaseLogic<CompanyJobEducationPoco>
    {
        public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyJobEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (CompanyJobEducationPoco poco in pocos)
            {
                if (String.IsNullOrEmpty(poco.Major))
                {
                    exceptions.Add(new ValidationException(200, "Major field cannot be blank."));
                }
                else if (poco.Major.Length < 2)
                {
                    exceptions.Add(new ValidationException(200, "Major field contains less than 2 characters."));
                }
                if (poco.Importance < 0)
                {
                    exceptions.Add(new ValidationException(201, "Importance field cannot be less than 0."));
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
