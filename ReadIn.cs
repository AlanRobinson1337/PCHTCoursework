using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Syncfusion.XlsIO.Implementation.XmlSerialization;

namespace PCHTCoursework
{
    static class ReadIn
    {
        internal static List<double> readInCSV(string csvFilePath, int csvValue)
        {
            using (var reader = new StreamReader(csvFilePath))
            {
                List<string> csvValues = new List<string>();
                List<double> result = new List<double>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    csvValues.Add(values[csvValue]);
                }
                foreach (var value in csvValues)
                {
                    try { result.Add(double.Parse(value)); 
                    }
                    catch { 
                    }
                }
                return result;
            }
        }
        //Readin CSV for DataClass
        public static List<double> readInCSVDataClass(string csvFilePath)
        {
            using (var reader = new StreamReader(csvFilePath))
            {
                List<string> csvValues = new List<string>();
                List<double> result = new List<double>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    csvValues.Add(values[0]);
                    csvValues.Add(values[1]);
                    csvValues.Add(values[2]);

                }
                foreach (var value in csvValues)
                {
                    try
                    {
                        result.Add(double.Parse(value));
                    }
                    catch
                    {
                    }
                }
                return result;
            }
        }
        public static List<double> ReadInPupilDiamater(string csvFilePath)
        {
            List<double> list = readInCSV(csvFilePath, 0);
            return list;
        }
        public static List<double> ReadInReigonOfInterest(string csvFilePath)
        {
            List<double> list = readInCSV(csvFilePath, 1);
            return list;
        }
        public static List<double> ReadInFixationDuration(string csvFilePath)
        {
            List<double> list = readInCSV(csvFilePath, 2);
            return list;
        }

    }
}
