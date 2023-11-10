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
        //internal static List<double> readInCSV(string csvFilePath, int csvValue)
        //{
        //    using (var reader = new StreamReader(csvFilePath))
        //    {
        //        List<string> csvValues = new List<string>();
        //        List<double> result = new List<double>();
        //        while (!reader.EndOfStream)
        //        {
        //            var line = reader.ReadLine();
        //            var values = line.Split(',');

        //            csvValues.Add(values[csvValue]);
        //        }
        //        foreach (var value in csvValues)
        //        {
        //            try { result.Add(double.Parse(value)); 
        //            }
        //            catch { 
        //            }
        //        }
        //        return result;
        //    }
        //}
        //Readin CSV for DataClass
        public static DataClass readInCSVDataClass(DataClass dataclass, string csvFilePath)
        {
            using (var reader = new StreamReader(csvFilePath))
            {
                List<string> csvValues = new List<string>();
                List<double> result = new List<double>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    try
                    {
                        dataclass.pupilDilation.Add(double.Parse(values[0]));
                        dataclass.reigonOfInterest.Add(double.Parse(values[1]));
                        dataclass.fixationDuration.Add(double.Parse(values[2]));
                    }
                    catch { }
                }
                return dataclass;
            }
        }
        //public static List<double> ReadInPupilDiamater(string csvFilePath)
        //{
        //    List<double> list = readInCSV(csvFilePath, 0);
        //    return list;
        //}
        //public static List<double> ReadInReigonOfInterest(string csvFilePath)
        //{
        //    List<double> list = readInCSV(csvFilePath, 1);
        //    return list;
        //}
        //public static List<double> ReadInFixationDuration(string csvFilePath)
        //{
        //    List<double> list = readInCSV(csvFilePath, 2);
        //    return list;
        //}

    }
}
