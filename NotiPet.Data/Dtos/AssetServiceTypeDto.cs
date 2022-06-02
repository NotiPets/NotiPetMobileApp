using Realms;

namespace NotiPet.Data.Dtos
{
    public class AssetServiceTypeDto:RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}