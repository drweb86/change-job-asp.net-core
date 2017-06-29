using Microsoft.Extensions.Configuration;

namespace HelloWorldASPNET.Services
{
    public class DogCare : IPuppyService
    {
        private readonly IConfiguration _configurationService;

        public DogCare(IConfiguration configuration)
        {
            _configurationService = configuration;
        }

        public string DoSound()
        {
            return _configurationService["greeting"];
        }
    }
}