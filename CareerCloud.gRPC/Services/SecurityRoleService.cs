using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.SecurityRole;

namespace CareerCloud.gRPC.Services
{
    public class SecurityRoleService : SecurityRoleBase
    {
        private readonly SecurityRoleLogic _logic;

        public SecurityRoleService()
        {
            _logic = new SecurityRoleLogic(new EFGenericRepository<SecurityRolePoco>());
        }
        public override Task<SecurityRolePayload> ReadSecurityRole(IdRequest request, ServerCallContext context)
        {

            SecurityRolePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityRolePayload>(() => new SecurityRolePayload()
            {
                Id = poco.Id.ToString(),
                Role = poco.Role,
                IsInactive = poco.IsInactive
            });
        }
    }
}
