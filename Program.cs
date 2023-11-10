using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using static PCHTCoursework.Calculation;
using static PCHTCoursework.ReadIn;
using static PCHTCoursework.FilePuller;

namespace PCHTCoursework
{
    public class main 
    {

        public static void Main(string[] args)
        {
            /**
             * This block scans directories and pulls the file paths from each where it can return a list of all paths
            **/
            //UniLaptop rootPath
            //String[] rootPath = { @"C:\Users\alanr\Source\Repos\PCHTCoursework\Data\CSVData\ASD\", @"C:\Users\alanr\Source\Repos\PCHTCoursework\Data\CSVData\TD\" };
            //HomePC rootPath
            System.String[] rootPath = { @"C:\Users\HP\Desktop\PCHTCoursework\PCHTCoursework\Data\CSVData\ASD\", @"C:\Users\HP\Desktop\PCHTCoursework\PCHTCoursework\Data\CSVData\TD\"};
            string[] ASDDirs = new string[50];
            List<string> ASDFiles = new();
            string[] TDDirs = new string[50];
            List<string> TDFiles = new List<string>();
            foreach (string path in rootPath) 
            {
                if (path.Contains("ASD"))
                {
                    ASDDirs = GetASDDirectories(path);
                    ASDFiles = GetFiles(ASDDirs);
                    
                }
                if (path.Contains("TD"))
                {
                    TDDirs = GetTDDirectories(path);
                    TDFiles = GetFiles(TDDirs);
                }
            }
            /**
             * Splitting files into emotions
            **/
            string[] emotion = { "happy", "neutral", "angry" };
            List<string> TDFileList, ASDFileList;
            /**
             * Get all data from all files in two collections of dataClasses
             * And one collection of all data classes
             */
            DataClasses allDataClasses = new DataClasses();
            int i = 1;
            DataClasses dataClassesTD = new DataClasses();
            foreach (string emo in emotion)
            {
                TDFileList = FileFilter(TDFiles, emo);
                foreach (String s in TDFileList)
                {
                    DataClass dataClass = new DataClass( "TD" + i, emo);
                    dataClass = readInCSVDataClass(dataClass, s);
                    dataClass.pupilDilationStandardDevidation= CalculateStandardDeviation(dataClass.pupilDilation);
                    dataClass.fixationDurationStandardDeviation= CalculateStandardDeviation(dataClass.fixationDuration);
                    dataClassesTD.AddDataClassToList(dataClass);
                    allDataClasses.AddDataClassToList(dataClass);
                    i++;
                }
                i = 1;
            }
            i = 1;
            DataClasses dataClassesASD = new DataClasses();
            foreach (string emo in emotion)
            {
                ASDFileList = FileFilter(ASDFiles, emo);
                foreach (String s in ASDFileList)
                {
                    DataClass dataClass = new DataClass("ASD" + i,emo);
                    dataClass = readInCSVDataClass(dataClass, s);
                    dataClass.pupilDilationStandardDevidation = CalculateStandardDeviation(dataClass.pupilDilation);
                    dataClass.fixationDurationStandardDeviation = CalculateStandardDeviation(dataClass.fixationDuration);
                    dataClassesASD.AddDataClassToList(dataClass);
                    allDataClasses.AddDataClassToList(dataClass);
                    i++;
                }
                i = 1;
            }
            Console.WriteLine("Emotion & Subject \t Fixation Duration \t Pupil Dilation");
            foreach (DataClass d in allDataClasses.dataClasses) {
                
                Console.WriteLine(d.emotion + d.name +"\t\t"+d.fixationDurationStandardDeviation+"\t"+d.pupilDilationStandardDevidation);
            }
            Console.WriteLine();

            DataClasses roiIsOne = new DataClasses();
            DataClasses roiIsTwo = new DataClasses();
            //add standard deviation with reigon of interest
            foreach (DataClass data in allDataClasses.dataClasses)
            {
                DataClass dataClassOne = new DataClass(data.name, data.emotion);
                DataClass dataClassTwo = new DataClass(data.name, data.emotion);
                double[] pupilDilation = data.pupilDilation.ToArray();
                double[] fixationDuration = data.fixationDuration.ToArray();
                double[] reigonOfInterest = data.reigonOfInterest.ToArray();
                for (int j = 0; j < reigonOfInterest.Length; j++)
                {
                    if (reigonOfInterest[j] == 1)
                    {
                        dataClassOne.reigonOfInterest.Add(reigonOfInterest[j]);
                        dataClassOne.fixationDuration.Add(fixationDuration[j]);
                        dataClassOne.pupilDilation.Add(pupilDilation[j]);
                        
                    }
                    else
                    {
                        dataClassTwo.reigonOfInterest.Add(reigonOfInterest[j]);
                        dataClassTwo.fixationDuration.Add(fixationDuration[j]);
                        dataClassTwo.pupilDilation.Add(pupilDilation[j]);
                        
                    }
                }
                roiIsOne.AddDataClassToList(dataClassOne);
                roiIsTwo.AddDataClassToList(dataClassTwo);
            }
            foreach (DataClass item in roiIsOne.dataClasses)
            {
                Console.WriteLine(item.name + item.emotion);
            }
        }
    }
}