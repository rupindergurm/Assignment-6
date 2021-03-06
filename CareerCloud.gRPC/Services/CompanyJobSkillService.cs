﻿using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.CompanyJobSkill;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobSkillService : CompanyJobSkillBase
    {
        private readonly CompanyJobSkillLogic _logic;
        public CompanyJobSkillService()
        {
            _logic = new CompanyJobSkillLogic(new EFGenericRepository<CompanyJobSkillPoco>());
        }
        public override Task<CompanyJobSkillPayload> ReadCompanyJobSkill(IdRequest request, ServerCallContext context)
        {
            CompanyJobSkillPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobSkillPayload>(() => new CompanyJobSkillPayload() {
                Id = poco.Id.ToString(),
                 Job = poco.Job.ToString(),
                 Skill = poco.Skill,

             SkillLevel =poco.SkillLevel,
             Importance = (Int32)poco.Importance
        });
        }
    }
}
