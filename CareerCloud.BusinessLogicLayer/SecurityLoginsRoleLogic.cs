using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityLoginsRoleLogic : BaseLogic<SecurityLoginsRolePoco>
    {
        public SecurityLoginsRoleLogic(IDataRepository<SecurityLoginsRolePoco> repository) : base(repository)
        {
        }

        protected override void Verify(SecurityLoginsRolePoco[] pocos)
        {

        }

        public override void Add(SecurityLoginsRolePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(SecurityLoginsRolePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
