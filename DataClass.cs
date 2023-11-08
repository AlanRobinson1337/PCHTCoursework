using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCHTCoursework
{
    internal class DataClass
    {
        private String name;
        private String emotion;
        private List<double> pupilDilation;
        private List<double> fixationDuration;
        private List<int> reigonOfInterest;

        public DataClass(string name, string emotion, List<double> pupilDilation, List<double> fixationDuration, List<int> reigonOfInterest)
        {
            this.name = name;
            this.emotion = emotion;
            this.pupilDilation = pupilDilation;
            this.fixationDuration = fixationDuration;
            this.reigonOfInterest = reigonOfInterest;
        }

    }
}
