﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCHTCoursework
{
    internal class DataClass
    {
        public String name { get; set; }
        public String emotion { get; set; }
        public List<double> pupilDilation { get; set; }
        public List<double> fixationDuration { get; set; }
        public List<double> reigonOfInterest { get; set; }
        private List<DataClass> dataClasses { get; set; }

        public DataClass(string name, string emotion)
        {
            this.name = name;
            this.emotion = emotion;
            this.pupilDilation = new List<double>();
            this.fixationDuration = new List<double>();
            this.reigonOfInterest = new List<double>();
        }

        //public DataClass(string name, string emotion, List<double> pupilDilation, List<double> fixationDuration, List<int> reigonOfInterest)
        //{
        //    this.name = name;
        //    this.emotion = emotion;
        //    this.pupilDilation = pupilDilation;
        //    this.fixationDuration = fixationDuration;
        //    this.reigonOfInterest = reigonOfInterest;
        //}

    }
}
