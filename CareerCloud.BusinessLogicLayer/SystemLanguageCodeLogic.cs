using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic
    {
        protected IDataRepository<SystemLanguageCodePoco> _repository;
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository) //: base(repository)
        {
            _repository = repository;
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

        public SystemLanguageCodePoco Get(string lang)
        {
            return _repository.GetSingle(c => c.LanguageID == lang);
        }

        public IList<SystemLanguageCodePoco> GetAll()
        {
            return _repository.GetAll();
        }

        public void Delete(SystemLanguageCodePoco[] pocos)
        {
            _repository.Remove();
        }
    }
}
