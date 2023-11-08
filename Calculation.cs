using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCHTCoursework
{
    static class Calculation
    {   
        /**
         * Add 
         */
        public static double AveragePupilDilation() //needs standard deviation
        {
            return 1.0;
        }

        public static void AveragefixationDilation() //needs standard deviation
        {
            throw new NotImplementedException();
        }

        public static void AverageFixationDilation()
        {
            throw new NotImplementedException();
        }
        public static double CalculateStandardDeviation(List<double> values)
        {
            double standardDeviation = 0;

            if (values.Any())
            {
                // Compute the average.     
                double avg = values.Average();

                // Perform the Sum of (value-avg)_2_2.      
                double sum = values.Sum(d => Math.Pow(d - avg, 2));

                // Put it all together.      
                standardDeviation = Math.Sqrt((sum) / (values.Count() - 1));
            }

            return standardDeviation;
        }
    }
}
