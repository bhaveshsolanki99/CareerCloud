using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic
    {
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository) //: base(repository)
        {
        }

        protected virtual void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (SystemLanguageCodePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.LanguageID))
                {
                    exceptions.Add(new ValidationException(1000, "LannguageID field is required."));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(1001, "Name field is required."));
                }
                if (string.IsNullOrEmpty(poco.NativeName))
                {
                    exceptions.Add(new ValidationException(1002, "NativeName field is required."));
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            /* foreach (SystemCountryCodePoco poco in pocos)
             {
                 if (poco.Id == Guid.Empty)
                 {
                     poco.Id = Guid.NewGuid();
                 }
             }

             _repository.Add(pocos);*/
        }

        public void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            // _repository.Update(pocos);
        }

        public void Get(Guid id)
        {

        }

        public void GetAll()
        {

        }

        public void Delete(SystemCountryCodeLogic[] pocos)
        {

        }
    }
}
