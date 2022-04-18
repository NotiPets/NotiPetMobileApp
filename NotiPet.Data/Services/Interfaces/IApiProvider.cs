namespace NotiPet.Data.Services
{
    public interface IApiProvider
    {
        public string ApiUrl { get; }
    }
    public class ApiProvider:IApiProvider
    {
        public ApiProvider(string apiUrl)
        {
            ApiUrl = apiUrl;
        }

        public string ApiUrl { get; }
    }
}