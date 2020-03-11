using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (CompanyLocationPoco poco in pocos)
            {
                if (String.IsNullOrEmpty(poco.CountryCode))
                {
                    exceptions.Add(new ValidationException(500, "CountryCode field cannot be blank."));
                }
                if (String.IsNullOrEmpty(poco.Province))
                {
                    exceptions.Add(new ValidationException(501, "Province field cannot be blank."));
                }
                if (String.IsNullOrEmpty(poco.Street))
                {
                    exceptions.Add(new ValidationException(502, "Street field cannot be blank."));
                }
                if (String.IsNullOrEmpty(poco.City))
                {
                    exceptions.Add(new ValidationException(503, "City field cannot be blank."));
                }
                if (String.IsNullOrEmpty(poco.PostalCode))
                {
                    exceptions.Add(new ValidationException(504, "PostalCode field cannot be blank."));
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
