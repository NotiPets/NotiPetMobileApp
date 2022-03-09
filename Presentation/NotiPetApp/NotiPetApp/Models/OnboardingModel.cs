using System;
using System.Collections.Generic;
using System.Text;

namespace NotiPetApp.Models
{
    public class OnboardingModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }

        public OnboardingModel()
        {
            Title = "NotiPet";

        }
    }
}
