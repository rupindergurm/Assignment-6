using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.CompanyDescription;

namespace CareerCloud.gRPC.Services
{
    public class CompanyDescriptionService : CompanyDescriptionBase
    {
        private readonly CompanyDescriptionLogic _logic;
        public CompanyDescriptionService()
        {
            _logic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>());
        }
        public override Task<CompanyDescriptionPayload> ReadCompanyDescription(IdRequest request, ServerCallContext context)
        {
            CompanyDescriptionPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyDescriptionPayload>(() => new CompanyDescriptionPayload() {
                Id = poco.Id.ToString(),

                 Company =  poco.Company.ToString(),


                LanguageId = poco.LanguageId,

             CompanyName = poco.CompanyName,

             CompanyDescription = poco.CompanyDescription
        }); 
        }
    }
}
