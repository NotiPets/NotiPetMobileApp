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

        public IObservable<string> Authentication(IAuthenticationRequestViewModel viewModel);
        public IObservable<string> SignUp(IRegisterRequestViewModel viewModel);

    }
}