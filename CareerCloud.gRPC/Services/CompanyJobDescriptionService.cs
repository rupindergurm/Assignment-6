using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.CompanyJobDescription;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobDescriptionService : CompanyJobDescriptionBase
    {
        private readonly CompanyJobDescriptionLogic _logic;
        public CompanyJobDescriptionService()
        {
            _logic = new CompanyJobDescriptionLogic(new EFGenericRepository<CompanyJobDescriptionPoco>());
        }
        public override Task<CompanyJobDescriptionPayload> ReadCompanyJobDescription(IdRequest request, ServerCallContext context)
        {
            CompanyJobDescriptionPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobDescriptionPayload>(() => new CompanyJobDescriptionPayload() {
                Id = poco.Id.ToString(),
                 Job = poco.Job.ToString(),
                 JobName = poco.JobName,
             JobDescriptions = poco.JobDescriptions
        });
        }
    }
}
