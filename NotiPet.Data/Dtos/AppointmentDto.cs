using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NotiPet.Data.Dtos
{
    public class AppointmentDto
    {
        public string Id { get; set; }
        public string SpecialistId { get; set; }
        public SpecialistDto Specialist { get; set; }
        public int AppointmentStatus { get; set; }
        public bool IsEmergency { get; set; }
        public bool Active { get; set; }
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-ddThh:mm:ss.000Z")]
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }

        
    }
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}