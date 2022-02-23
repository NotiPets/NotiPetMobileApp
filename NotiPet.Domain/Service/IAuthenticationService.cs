using System;
using System.Collections.Generic;
using DynamicData;
using NotiPet.Domain.Models;

namespace NotiPet.Domain.Service
{
    public interface IAuthenticationService
    {
        public SourceList<SocialNetwork> SocialNetworks { get; }

        public IObservable<IEnumerable<SocialNetwork>> GetSocialNetworks();
        
        public IObservable<UserRole> Authentication(string username , string password);
        public IObservable<UserRole> SignUp(UserRole userRole);

    }
}