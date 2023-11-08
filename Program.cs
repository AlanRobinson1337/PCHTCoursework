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
        //TODO: Convert lists from String to Double. Use it's own class.
        //TODO: Implement Math in Calculation Class.
    {

        public static void Main(string[] args)
        {
            /**
             * This block scans directories and pulls the file paths from each where it can return a list of all paths
            **/
            //UniLaptop rootPath
            String[] rootPath = { @"C:\Users\alanr\Source\Repos\PCHTCoursework\Data\CSVData\ASD\", @"C:\Users\alanr\Source\Repos\PCHTCoursework\Data\CSVData\TD\" };
            //HomePC rootPath
            //System.String[] rootPath = { @"C:\Users\HP\Desktop\PCHTCoursework\PCHTCoursework\Data\CSVData\ASD\", @"C:\Users\HP\Desktop\PCHTCoursework\PCHTCoursework\Data\CSVData\TD\"};
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
            List<string> listX, listY;
            List<double> stdPDDevsASD = new List<double>();
            List<double> stdPDDevsTD = new List<double>();
            List<double> stdFDDevsASD = new List<double>();
            List<double> stdFDDevsTD = new List<double>();

            /**
             * get standard Deviation for PupilDiamater & Fixation Duration
             * */
            foreach (string emo in emotion) 
            {
                listX = FileFilter(TDFiles, emo);
                foreach (string s in listX)
                {
                    List<double> TDFilePD = ReadInPupilDiamater(s);
                    List<double> TDFileFD = ReadInFixationDuration(s);
                    double stdPDDevTD = CalculateStandardDeviation(TDFilePD);
                    double stdFDDevTD = CalculateStandardDeviation(TDFileFD);
                    stdPDDevsTD.Add(stdPDDevTD);
                    stdFDDevsTD.Add(stdFDDevTD);
                    
                }
                listY = FileFilter(ASDFiles, emo);
                foreach(string s in listY)
                {
                    List<double> ASDFilePD = ReadInPupilDiamater(s);
                    List<double> ASDFileFD = ReadInFixationDuration(s);
                    double stdPDDevASD = CalculateStandardDeviation(ASDFilePD);
                    double stdFDDevASD = CalculateStandardDeviation(ASDFileFD);
                    stdPDDevsASD.Add(stdPDDevASD);
                    stdFDDevsASD.Add(stdFDDevASD);
                }
                foreach(double s in stdFDDevsASD)
                {
                    Console.WriteLine(s);
                }
            }

        }
    }
}