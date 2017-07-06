using Microsoft.AspNetCore.Mvc;

namespace HelloWorldASPNET.Controllers
{
    [Route("secret")]
    public class TopSecretController
    {
        [Route("")]
        public string TopSecretInformation()
        {
            return "Big Bro watches you bro...";
        }

        [Route("call-scally")]
        public string AgentScallyPhone()
        {
            return "911";
        }
    }

    [Route("no-forn/[controller]")]
    public class ClassifiedController
    {
        [Route("trueth")]
        public string TheTruethAboutTowers()
        {
            return "Coming soon...";
        }
    }
}
