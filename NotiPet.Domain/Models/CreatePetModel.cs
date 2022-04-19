using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NotiPet.Domain.Annotations;

namespace NotiPet.Domain.Models
{
    public enum EPetTypeId
    {
        Dog,
        Cat,
        Bunny,
        Other
    }
    public class CreatePetModel:INotifyPropertyChanged
    {
        public string Name { get; set; }
        public int? PetType { get; set; }
        public string User { get; set; }
        public int? Size { get; set; }
        public string PictureUrl { get; set; }
        public string Description { get; set; }
        public bool Gender { get; set; }
        public bool Vaccinated { get; set; }
        public bool Castrated { get; set; }
        public bool HasTracker { get; set; }
        public DateTime Birthdate { get; set; }=DateTime.Today;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class PetInformation
    {
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}