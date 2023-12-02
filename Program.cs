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
using Syncfusion.XlsIO.Implementation.PivotAnalysis;
using System.Drawing;

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
                    dataClass.reigonOfInterest.Add(dataClass.reigonOfInterest[i]);
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
                    dataClass.reigonOfInterest.Add(dataClass.reigonOfInterest[i]);
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
            //------------WORKING AREA------------------------------------------------------
            //Averages by subject
            Console.WriteLine("By Individual Subject");
            double averagpupildilation = 0;
            double averageFixationDuration = 0;
            foreach (DataClass dataClass in allDataClasses.dataClasses)
            {
                foreach (double data in dataClass.pupilDilation)
                {
                    averagpupildilation += data;
                }
                foreach (double data in dataClass.fixationDuration)
                {
                    averageFixationDuration += data;
                }
                averagpupildilation /= dataClass.pupilDilation.Count();
                averageFixationDuration /= dataClass.fixationDuration.Count();
                
                Console.WriteLine(dataClass.name +"\t"+dataClass.emotion+"\t\t Pupil Dilation  "+ averagpupildilation
                    +"\t\t Fixation Duration  "+averageFixationDuration);
            }
            //DONE Average pupil dilation of TD & ASD by emotion
            //DONE Average Fixation Duration of TD & ASD by emotion
            double emotionPDTD =0, emotionFDTD=0, emotionFDASD=0,emotionPDASD = 0;
            foreach (string emo in emotion)
            {
                foreach (DataClass item in allDataClasses.dataClasses)
                {
                    if (item.emotion.Equals(emo))
                    {
                        if (item.name.Contains("TD"))
                        {
                            foreach (double d in item.pupilDilation)
                            {
                                emotionPDTD += d;
                            }emotionPDTD /= item.pupilDilation.Count();
                            foreach (double d in item.fixationDuration)
                            {
                                emotionFDTD += d;
                            }
                            emotionFDTD /= item.fixationDuration.Count();
                        }
                        else
                        {
                            foreach (double d in item.pupilDilation)
                            {
                                emotionPDASD += d;
                            }emotionPDASD /= item.pupilDilation.Count();
                            foreach (double d in item.fixationDuration)
                            {
                                emotionFDASD += d;
                            }emotionFDASD /= item.fixationDuration.Count();
                        }
                    }
                }
                Console.WriteLine("\nBy Emotion");
                Console.WriteLine(emo + "\tTD \tPupil Dilation\t\t" + emotionPDTD);
                Console.WriteLine(emo + "\tTD \tFixation Duration\t" + emotionFDTD);
                Console.WriteLine(emo + "\tASD \tPupil Dilation\t\t" + emotionPDASD);
                Console.WriteLine(emo + "\tASD \tFixation Duration\t" + emotionFDASD);
            }
            
            List<double> allEmoPDTD = new List<double>();
            List<double> allEmoFDTD = new List<double>();
            List<double> allEmoPDASD = new List<double>();
            List<double> allEmoFDASD = new List<double>();
            foreach (DataClass item in allDataClasses.dataClasses)
            {
                if (item.name.Contains("TD")){
                    foreach (double d in item.pupilDilation)
                    {
                        allEmoPDTD.Add(d);
                    }
                    foreach (double d in item.fixationDuration)
                    {
                        allEmoFDTD.Add(d);
                    }
                }else
                {
                    foreach (double d in item.pupilDilation)
                    {
                        allEmoPDASD.Add(d);
                    }
                    foreach (double d in item.fixationDuration)
                    {
                        allEmoFDASD.Add(d);
                    }
                }
            }
            double ASDPD, ASDFD, TDPD, TDFD;
            TDPD = CalculateStandardDeviation(allEmoPDTD);
            TDFD = CalculateStandardDeviation(allEmoFDTD);
            ASDPD = CalculateStandardDeviation(allEmoPDASD);
            ASDFD = CalculateStandardDeviation(allEmoFDASD);
            Console.WriteLine("\nBy Group");
            Console.WriteLine("TD\nFixation Duration \t" + TDFD);
            Console.WriteLine("Pupil Dilation\t\t" + TDPD);
            Console.WriteLine("\nASD\nFixation Duration \t" + ASDFD);
            Console.WriteLine("Pupil Dilation\t\t" + ASDPD);
            //TODO Average FD for each of the two groups for face vs non - face regions
            //TODO Average FD for each of the two groups for face vs non-face regions By Emotion
            List<DataClass> RoiOne = new List<DataClass>();
            List<DataClass> RoiTwo = new List<DataClass>();
            List<DataClass> ASdRoiOne = new List<DataClass>();
            List<DataClass> ASdRoiTwo = new List<DataClass>();
            foreach (DataClass item in allDataClasses.dataClasses) //SPLITTER
            {
                DataClass ones = new DataClass();
                DataClass twos = new DataClass();
                ones.emotion = item.emotion;
                twos.emotion = item.emotion;
                ones.name = item.name;
                twos.name = item.name;
                
                    for (int j = 0; j < item.reigonOfInterest.Count()-1; j++)
                    {
                        if (item.reigonOfInterest[j] == 1)
                        {

                            ones.reigonOfInterest.Add(item.reigonOfInterest[j]);
                            ones.pupilDilation.Add(item.pupilDilation[j]);
                            ones.fixationDuration.Add(item.fixationDuration[j]);
                        }
                        else
                        {
                            twos.reigonOfInterest.Add(item.reigonOfInterest[j]);
                            twos.pupilDilation.Add(item.pupilDilation[j]);
                            twos.fixationDuration.Add(item.fixationDuration[j]);
                        }
                    }
                    RoiOne.Add(ones);
                    RoiTwo.Add(twos);
            } //SPLITTER SUCCESS
            Console.WriteLine("\nBy Data Subject");
            Console.WriteLine("ROI is ONE");
            foreach (DataClass item in RoiOne)
            {
                Console.WriteLine(item.name+"\t"+item.emotion+"\t" +
                    "\tPupil Dilation \t"+CalculateStandardDeviation(item.pupilDilation)
                    +"\tFixation Duration\t"+CalculateStandardDeviation(item.fixationDuration));
            }
            Console.WriteLine("ROI is TWO");
            foreach (DataClass item in RoiTwo)
            {
                Console.WriteLine(item.name + "\t" + item.emotion + "\t" +
                    "\tPupil Dilation \t" + CalculateStandardDeviation(item.pupilDilation)
                    + "\tFixation Duration\t" + CalculateStandardDeviation(item.fixationDuration));
            }
            //===========END OF WORKING AREA------------------------------------------------

            //DataClasses roiIsOne = new DataClasses();
            //DataClasses roiIsTwo = new DataClasses();
            ////add standard deviation with reigon of interest
            //foreach (DataClass data in allDataClasses.dataClasses)
            //{
            //    DataClass dataClassOne = new DataClass(data.name, data.emotion);
            //    DataClass dataClassTwo = new DataClass(data.name, data.emotion);
            //    double[] pupilDilation = data.pupilDilation.ToArray();
            //    double[] fixationDuration = data.fixationDuration.ToArray();
            //    double[] reigonOfInterest = data.reigonOfInterest.ToArray();
            //    for (int j = 0; j < reigonOfInterest.Length; j++)
            //    {
            //        if (reigonOfInterest[j] == 1)
            //        {
            //            dataClassOne.reigonOfInterest.Add(reigonOfInterest[j]);
            //            dataClassOne.fixationDuration.Add(fixationDuration[j]);
            //            dataClassOne.pupilDilation.Add(pupilDilation[j]);

            //        }
            //        else
            //        {
            //            dataClassTwo.reigonOfInterest.Add(reigonOfInterest[j]);
            //            dataClassTwo.fixationDuration.Add(fixationDuration[j]);
            //            dataClassTwo.pupilDilation.Add(pupilDilation[j]);

            //        }
            //    }
            //    roiIsOne.AddDataClassToList(dataClassOne);
            //    roiIsTwo.AddDataClassToList(dataClassTwo);
            //}
            //foreach (DataClass data in roiIsOne.dataClasses.Concat(roiIsTwo.dataClasses))
            //{
            //    List<double> ones = new List<double>(); //change to class
            //    List<double> twos = new List<double>();
            //    foreach (DataClass item in roiIsOne.dataClasses.Concat(roiIsTwo.dataClasses)) 
            //    {
            //        if (data.name.Equals(item.name) && data.emotion.Equals(item.emotion))
            //        {
            //            for (int z = 0; z < item.reigonOfInterest.Count; z++)
            //            {
            //                if (item.reigonOfInterest[z] == 1)
            //                {
            //                    ones.Add(item.fixationDuration[z]); //insert class in working if
            //                }
            //                else { twos.Add(item.fixationDuration[z]); }
            //            }
            //        }
            //    }
            //    Console.WriteLine(data.name+data.emotion+"\tFixation Duration Ones "+CalculateStandardDeviation(ones));
            //    Console.WriteLine(data.name + data.emotion + "\tFixation Duration Twos " + CalculateStandardDeviation(twos));
            //    //for ones && twos 
            //        //for class in ones & twos
            //            //if names match && emotions match && roi != roi
            //                //add to all subjects per emotion emotion fixation standard dev
            //}

            //new code here
        }
    }
}