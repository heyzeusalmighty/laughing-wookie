using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Occultation.DataModels;

namespace Occultation.ViewModels
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
        }

        public ScienceTrack GetScienceTrack()
        {
            return new ScienceTrack();
        }
    }
}
