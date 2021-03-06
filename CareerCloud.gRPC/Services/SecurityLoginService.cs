﻿using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.SecurityLogin;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginService :SecurityLoginBase
    {
        private readonly SecurityLoginLogic _logic;
        public SecurityLoginService()
        {
            _logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>());
        }
        public override Task<SecurityLoginPayload> ReadSecurityLogin(IdRequest request, ServerCallContext context)
        {
            SecurityLoginPoco poco =  _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityLoginPayload>(
                () => new SecurityLoginPayload() {
                    Id = poco.Id.ToString(),
                     Login = poco.Login,
                    Password = poco.Password,
                    Created = Timestamp.FromDateTime((DateTime)poco.Created),
                   PasswordUpdate = poco.PasswordUpdate is null ? null : Timestamp.FromDateTime((DateTime)poco.PasswordUpdate),
                 AgreementAccepted =   poco.AgreementAccepted is null ? null : Timestamp.FromDateTime((DateTime)poco.AgreementAccepted),
                    
             IsLocked = poco.IsLocked,
             IsInactive = poco.IsInactive,

             EmailAddress = poco.EmailAddress,

             PhoneNumber = poco.PhoneNumber,

            FullName = poco.FullName,

             ForceChangePassword = poco.ForceChangePassword,

             PrefferredLanguage = poco.PrefferredLanguage

        });
        }
    }
}
