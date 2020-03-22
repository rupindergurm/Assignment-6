using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.ApplicantSkill;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantSkillService : ApplicantSkillBase
    {
        private readonly ApplicantSkillLogic _logic;
        public ApplicantSkillService()
        { _logic = new ApplicantSkillLogic(new EFGenericRepository<ApplicantSkillPoco>()); }
        public override Task<ApplicantSkillPayload> ReadApplicantSkill(IdRequest request, ServerCallContext context)
        {
            ApplicantSkillPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantSkillPayload>(() => new ApplicantSkillPayload() {
                Id = poco.Id.ToString(),
                 Applicant  = poco.Applicant.ToString(),
                 Skill = poco.Skill,

             SkillLevel = poco.SkillLevel,

             StartMonth = poco.StartMonth,

             StartYear = poco.StartYear,

             EndMonth = poco.EndMonth,

             EndYear = poco.EndYear
        });
        }
    }
}
