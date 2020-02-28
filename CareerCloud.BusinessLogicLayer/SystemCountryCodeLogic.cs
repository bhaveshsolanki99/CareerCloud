using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic //: BaseLogic<SystemCountryCodePoco>
    {
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository) //: base(repository)
        {
        }

        protected void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (SystemCountryCodePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Code))
                {
                    exceptions.Add(new ValidationException(900, "Code field is required."));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(901, "Name field is required."));
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public void Add(SystemCountryCodePoco[] pocos)
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

        public virtual void Update(SystemCountryCodePoco[] pocos)
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
