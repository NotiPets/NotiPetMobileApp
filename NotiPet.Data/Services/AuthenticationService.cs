
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using AutoMapper;
using DynamicData;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;

namespace NotiPet.Data.Services
{
    public class AuthenticationService:IAuthenticationService,IDisposable
    {
        private readonly IUserServiceApi _userServiceApi;
        private readonly IMapper _mapper;
        private SourceList<SocialNetwork> _sourceList = new SourceList<SocialNetwork>();
        public SourceList<SocialNetwork> SocialNetworks => _sourceList;

        public AuthenticationService(IUserServiceApi userServiceApi,IMapper mapper)
        {
            _userServiceApi = userServiceApi;
            _mapper = mapper;
        }
        public IObservable<IEnumerable<SocialNetwork>> GetSocialNetworks()
        {
            return Observable.Return(new List<SocialNetwork>()
            {
                new SocialNetwork()
                {
                    Name = "Facebook",
                    Icon = "FbIcon",
                    Id = 1,
                    Code = "FB",
                    Color = "#4267B2"
                },
                new SocialNetwork()
                {
                    Name = "Google",
                    Icon = "GBIcon",
                    Id = 2,
                    Code = "GB",
                    Color = "#DB4437"
                },
                new SocialNetwork()
                {
                    Name = "NotiPed",
                    Icon = "NT",
                    Id = 3,
                    Code = "NT",
                    Color = "#00999b"
                }
            }).Do(_sourceList.AddRange);
        }
        public IObservable<UserRole> Authentication(string username, string password)
        {
            return _userServiceApi.LogIn(username, password)
                .Select(_mapper.Map<UserRole>);

        }

        public IObservable<UserRole> SignUp(UserRole userRole)
        {
            return _userServiceApi.SingUp(_mapper.Map<UserRoleDto>(userRole))
                .Select(_mapper.Map<UserRole>);
        }

        public void Dispose()
        {
            _sourceList?.Dispose();
        }
    }
}