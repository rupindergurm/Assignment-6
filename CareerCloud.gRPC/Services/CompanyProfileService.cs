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
using static CareerCloud.gRPC.Protos.ComanyProfile;

namespace CareerCloud.gRPC.Services
{
    public class CompanyProfileService : ComanyProfileBase
    {
        private readonly CompanyProfileLogic _logic;
        public CompanyProfileService()
        {
            _logic = new CompanyProfileLogic(new EFGenericRepository<CompanyProfilePoco>());
        }
        public override Task<CompanyProfilePayload> ReadCompanyProfile(IdRequest request, ServerCallContext context)
        {
            CompanyProfilePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyProfilePayload>(() => new CompanyProfilePayload() {
                Id = poco.Id.ToString(),

                 RegistrationDate = Timestamp.FromDateTime((DateTime)poco.RegistrationDate),

               CompanyWebsite = poco.CompanyWebsite,

            ContactPhone = poco.ContactPhone,

          ContactName = poco.ContactName,

             CompanyLogo = poco.CompanyLogo
        });
        }
    }
}
