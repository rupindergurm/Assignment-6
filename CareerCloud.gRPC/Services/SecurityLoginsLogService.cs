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
using static CareerCloud.gRPC.Protos.SecurityLoginsLog;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsLogService : SecurityLoginsLogBase
    {
        private readonly SecurityLoginsLogLogic _logic;

        public SecurityLoginsLogService()
        {
            _logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());
        }
        public override Task<SecurityLoginsLogPayload> ReadSecurityLoginsLog(IdRequest request, ServerCallContext context)
        {
            SecurityLoginsLogPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityLoginsLogPayload>(() => new SecurityLoginsLogPayload() {
                Id = poco.Id.ToString(),
                Login =  poco.Login.ToString(),

                SourceIP =poco.SourceIP,

            LogonDate = Timestamp.FromDateTime((DateTime)poco.LogonDate),

                IsSuccesful = poco.IsSuccesful
        });

        }
    }
}
