using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Text;
using System.IO;
using BuissnessLogic;
using DTO;
using  DataAccessLogic;


namespace Test_gem_fil
{
    class Program
    {

        private Controller controller;

        static void Main(string[] args)
        {
            
           
            string path = @"C:\ST3PRJ3FIL\123456 "+DateTime.Now.ToString("dd-MM-yyyy");

            //using (FileStream fs = File.Create(path));

            
            
                TEST1 test1=new TEST1();
              //  test1.testfil(path);

                //test1.CreateFile();
                test1.SaveMeasurement(145, 5, 125, 135, "222222-2222");
                

                test1.SaveMeasurement(140, 0, 120, 130, "222222-2222");
                

                





        }


        //    //Opretter fil
        //    string path = @"C:\Users\simon\source\repos\ST3PRJ3SimonOgAK\GUI\Test gem fil\bin\Mokai";
        //    using (FileStream fs = File.Create(path)) ;

        //    //Skriver til fil
        //    using (StreamWriter sw = File.AppendText(path))
        //    {
        //        sw.WriteLine("SIMON, OG AK, ER DE BEDSTE");
        //        sw.WriteLine("1, 2, 3");
        //    }

        //    using (StreamWriter sw = File.AppendText(path))
        //    {
        //        sw.WriteLine("SIMON, OG AK, ER DE BEDSTE");
        //        sw.WriteLine("1, 2, 3");
        //    }
        //    //Læser og udskriver
        //    using (StreamReader sr = File.OpenText(path))
        //    {
        //        string s = "";
        //        while ((s = sr.ReadLine()) != null)
        //        {
        //            Console.WriteLine(s);
        //        }
        //    }




        
            

        //}
        
       
                
            

        
    }
        
    
}
