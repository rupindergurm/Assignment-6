using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
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
                if (poco.StartMonth == 13)
                {
                    exceptions.Add(new ValidationException(101, "StartMonth cannot greater than 12 ....fix it!"));
                }

                if (poco.EndMonth == 13)
                {
                    exceptions.Add(new ValidationException(102, "EndMonth cannot grater than 12 ....fix it!"));
                }
                if (poco.StartYear == 1899)
                {
                    exceptions.Add(new ValidationException(103, "StartYear cannot less than 1900 ....fix it!"));
                }
                if (poco.EndYear== 2016)
                {
                    exceptions.Add(new ValidationException(104, "EndYear cannot less than StartYear ....fix it!"));
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

