using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCHTCoursework
{
    internal class DataClasses
    {
        public List<DataClass> dataClasses { get; set; } 

        public DataClasses()
        {
            this.dataClasses = new List<DataClass>();
        }
        public void AddDataClassToList(DataClass dataClass)
        {
            this.dataClasses.Add(dataClass);
        }
        
    }
}
