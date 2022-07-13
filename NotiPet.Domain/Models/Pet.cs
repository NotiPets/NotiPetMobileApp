using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NotiPet.Domain.Annotations;

namespace NotiPet.Domain.Models
{
    public class Pet:INotifyPropertyChanged
    {
        public Pet(string id, string name, int petTypeId, string petTypeName, string userId, User user, int size, bool active, string pictureUrl, string description, bool gender, bool vaccinated, bool castrated, bool hasTracker, DateTime birthdate, DateTime created, DateTime updated)
        {
            Id = id;
            Name = name;
            PetTypeId = petTypeId;
            PetTypeName = petTypeName;
            UserId = userId;
            User = user;
            Size = size;
            Active = active;
            PictureUrl = pictureUrl;
            Description = description;
            Gender = gender;
            Vaccinated = vaccinated;
            Castrated = castrated;
            HasTracker = hasTracker;
            Birthdate = birthdate;
            Created = created;
            Updated = updated;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int PetTypeId { get; set; }
        public string PetTypeName { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int Size { get; set; }
        public bool Active { get; set; }
        public string PictureUrl { get; set; }
        public string Description { get; set; }
        public bool Gender { get; set; }
        public bool Vaccinated { get; set; }
        public bool Castrated { get; set; }
        public bool HasTracker { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string SizeName => $"{(EPetSize)Size}";
        public bool IsSelected { get; private set; }

        public void SetIsSelected(bool isSelected)
        {
            IsSelected = isSelected;
            OnPropertyChanged(nameof(IsSelected));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum EPetSize
    {
                Small,
                Medium,
                Large
    }
}