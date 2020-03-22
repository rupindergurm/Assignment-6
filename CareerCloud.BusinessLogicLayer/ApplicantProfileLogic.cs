using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic : BaseLogic<ApplicantProfilePoco>
    {
        public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> repository) : base(repository)
        {
        }
        protected override void Verify(ApplicantProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantProfilePoco poco in pocos)
            {
                if (poco.CurrentSalary==-1)
                {
                    exceptions.Add(new ValidationException(111, "Currentsalary cannot be negative....fix it!"));
                }
                if (poco.CurrentRate==-1)
                {
                    exceptions.Add(new ValidationException(112, "CurrentRate cannot be negative....fix it!"));
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        public override void Add(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);

        }

        private void Verify()
        {
            throw new NotImplementedException();
        }

        public override void Update(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

    }
}

