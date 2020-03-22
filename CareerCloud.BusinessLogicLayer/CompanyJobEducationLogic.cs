using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
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
                    exceptions.Add(new ValidationException(200, "Blank Major must be at least 2 characters ....fix it!"));
                }
                else
                {
                    if(poco.Major.Length<2)
                
                    {
                        exceptions.Add(new ValidationException(200, "Blank Major must be at least 2 characters ....fix it!"));
                    }
                }
                if (!(poco.Importance >0 ))
                {
                    exceptions.Add(new ValidationException(201, "Importance Cannot be less than 0 ....fix it!"));
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

        private void Verify()
        {
            throw new NotImplementedException();
        }

        public override void Update(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

    }
}

