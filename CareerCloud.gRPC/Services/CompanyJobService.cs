using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.CompanyJob;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobService : CompanyJobBase
    {
        private readonly CompanyJobLogic _logic;
        public CompanyJobService()
        {
            _logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
        }
        public override Task<CompanyJobPayload> ReadCompanyJob(IdRequest request, ServerCallContext context)
        {
            CompanyJobPoco poco = _logic.Get(Guid.Parse(request.id));
            return new Task<CompanyJobPayload>(() => new CompanyJobPayload() {
                Id = poco.Id.ToString(),
                Company =  poco.Company.ToString(),

                 ProfileCreated = Timestamp.FromDateTime((DateTime)poco.ProfileCreated),

                IsInactive = poco.IsInactive,

             IsCompanyHidden = poco.IsCompanyHidden
        });
        }
    }
}
