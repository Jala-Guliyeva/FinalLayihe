using MedicioBackend.Models;
using System.Collections;
using System.Collections.Generic;

namespace MedicioBackend.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> sliders { get; set; }
        public IEnumerable<Doctor>doctors { get; set; }
        public IEnumerable<Question>questions { get; set; }
        public Bio bios { get; set; }
    }
}
