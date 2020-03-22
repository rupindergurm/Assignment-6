using CareerCloud.BusinessLogicLayer;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.CompanyJobEducation;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobEducationService : CompanyJobEducationBase
    {
        private readonly CompanyJobEducationLogic _logic;
        public CompanyJobEducationService()
        {

        }
        public override Task<CompanyJobEducationPayload> ReadCompanyJobEducation(IdRequest request, ServerCallContext context)
        {
            CompanyJobEducationPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobEducationPayload>(() => new CompanyJobEducationPayload {
                Id = poco.Id.ToString(),
                Job =  poco.Job.ToString(),
                 Major = poco.Major,
            Importance = poco.Importance
        });
        }
    }
}
