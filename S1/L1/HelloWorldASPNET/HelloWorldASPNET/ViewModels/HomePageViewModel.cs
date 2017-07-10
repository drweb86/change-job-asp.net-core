using System.Collections.Generic;
using HelloWorldASPNET.Entities;

namespace HelloWorldASPNET.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<AlBarto> AlBartos { get; set; }
        public string Test { get; set; }
    }
}
