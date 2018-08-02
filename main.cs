using System;
using System.Linq;
using System.Collections.Generic;

class MainClass {
 
  //Number of people alive in each year of the 20th century
  //Index (i) corresponds to year (1900+i)

  static int [] NumAlive = new int[101];

  // Array listing the people born in each year
  // Each person is represented by an integer, 
  //  corresponding to the number of years he or she lived
  //  to be. For simplicity, we will presume everyone was
  //  born and died on the same day of each year. A child
  //  that died in infancy will be considered to have lived
  //  for 0 years.
  static List<int> [] People = new List<int>[101];

  // List of people enumerated in "names.csv" (with a header)
  static string[] RawData;

  public static void Main (string[] args) 
  {
    int BirthYear, DeathYear, CurrentYear;
    int Longevity;

    //Initialize the number of living people to 0
    for (CurrentYear = 0; CurrentYear <= 100; CurrentYear++) 
    {
      NumAlive[CurrentYear] = 0;
      People[CurrentYear] = new List<int>(0);
    }

    //Make a list of the people alive in each year
    RawData = System.IO.File.ReadAllLines(@"names.csv");
    for (int i = 1; i < RawData.Length; i++)
    {
      BirthYear = Convert.ToInt32(RawData[i].Split(',')[1]);
      DeathYear = Convert.ToInt32(RawData[i].Split(',')[2]);
      People[BirthYear-1900].Add(DeathYear-BirthYear);
    }

    //Count the number of people alive in each year
    for (CurrentYear = 0; CurrentYear <= 100; CurrentYear++)
    {
      if (People[CurrentYear].Count > 0)
      {
         Longevity = People[CurrentYear].Max();
         for (int i = 0; i <= Longevity; i++)
         {
           NumAlive[CurrentYear+i] += People[CurrentYear].FindAll(m => m >= i).Count();
         }
      }
    }

    //Maximum number of people alive in any given year
    int MaxAlive = NumAlive.Max();
    List<int> YearsMax = new List<int>(0);

    //Year(s) where this maximum is achieved
    for (int i = 0; i < NumAlive.Length; i++)
    {
      if (NumAlive[i] == MaxAlive) {YearsMax.Add(1900+i);}
    }

    Console.WriteLine ("The maximum number of people alive, " + MaxAlive + ", is achieved in year(s) " + String.Join(",",YearsMax));
  }
}
