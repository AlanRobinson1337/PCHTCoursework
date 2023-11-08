using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCHTCoursework
{
    internal class DataClass
    {
        private String name { get; set; }
        private String emotion { get; set; }
        private List<double> pupilDilation { get; set; }
        private List<double> fixationDuration { get; set; }
        private List<int> reigonOfInterest { get; set; }
        private List<DataClass> dataClasses { get; set; }

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
