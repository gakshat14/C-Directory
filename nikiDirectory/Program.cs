using System;
using System.IO;
using System.Text.RegularExpressions;

namespace nikiDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            //defining path for directory
            String SourceDirectory = "Directory";

            //initializing line which will be used to read a file line by line
            string line = "";

            //Defining Regular Expression for finding telephone number 
            Regex reg = new Regex(@"\+?\d[\d -]{8,12}\d");

            try
            {
                //finding all the .txt files inside a directory and storing them inside txtFiles
                var txtFiles = Directory.EnumerateFiles(SourceDirectory, "*.txt");
                
                //enumerating files inside txtFiles
                foreach (var f in txtFiles)
                {
                    //just returning file name instead of path eg "Directory/File1.txt" is turned to "File1.txt"
                    string fileName = f.Substring(SourceDirectory.Length + 1);
                    Console.WriteLine("From File {0}", fileName);
                    
                    //opening file to read them
                    using (var fileStream = new FileStream(f,FileMode.Open))
                    {
                        //Reading file
                        using (var myReader = new StreamReader(fileStream))
                        {
                            while (line != null) {
                                //storing read line in "line"
                                line = myReader.ReadLine();
                                {
                                    //checking whether line is null to avoid argumentNullException
                                    if (line != null)
                                    {
                                        //storing all the matched values
                                        MatchCollection m = reg.Matches(line);
                                        //iterating matchCollection to print the values
                                        foreach (var number in m)
                                        {
                                            Console.WriteLine(number);
                                        }
                                        
                                    }
                                }
                            }
                            //closing myReader
                            myReader.Close();
                        }
                        //Closing fileStream
                        fileStream.Close();
                    }
                    line = "";
                }
            }

            catch (Exception e)
            {

                Console.WriteLine("Oops! Your file wasn't found. Try making a folder and put some .txt file {0}", e.Message);
                throw;

            }

            Console.ReadLine();
        }
    }
}

