﻿using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.SecurityLoginsRole;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsRoleService : SecurityLoginsRoleBase
    {
        private readonly SecurityLoginsRoleLogic _logic;
        public SecurityLoginsRoleService()
        {
            _logic = new SecurityLoginsRoleLogic(new EFGenericRepository<SecurityLoginsRolePoco>());
        }
        public override Task<SecurityLoginsRolePayload> ReadSecurityLoginsRole(IdRequest request, ServerCallContext context)
        {
            SecurityLoginsRolePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityLoginsRolePayload>(() => new SecurityLoginsRolePayload() {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                Role = poco.Role.ToString()
            });

        }
    }
}
