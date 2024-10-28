using System;

namespace WhatDay1
{
    class WhatDay
    {
        enum MonthName
        {
            January,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December,
        }
        static System.Collections.ICollection DaysInMonths
           = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        static void Main(string[] args)
        {
            //Start:
            Console.Write("\nВведите порядковый номер дня года от 1 до 365:\t");
            int dayNum = int.Parse(Console.ReadLine());
            int monthNum = 0;

            //if (dayNum <= 181)
            //{
            //    if (dayNum <= 31)
            //    { // January
            //        goto End;
            //    }
            //    else
            //    {
            //        dayNum -= 31;
            //        monthNum++;
            //    }

            //    if (dayNum <= 28)
            //    { // February
            //        goto End;
            //    }
            //    else
            //    {
            //        dayNum -= 28;
            //        monthNum++;
            //    }

            //    if (dayNum <= 31)
            //    { // March
            //        goto End;
            //    }
            //    else
            //    {
            //        dayNum -= 31;
            //        monthNum++;
            //    }

            //    if (dayNum <= 30)
            //    { // April
            //        goto End;
            //    }
            //    else
            //    {
            //        dayNum -= 30;
            //        monthNum++;
            //    }

            //    if (dayNum <= 31)
            //    { // May
            //        goto End;
            //    }
            //    else
            //    {
            //        dayNum -= 31;
            //        monthNum++;
            //    }

            //    if (dayNum <= 30)
            //    { // June
            //        goto End;
            //    }
            //    else
            //    {
            //        dayNum -= 30;
            //        monthNum++;
            //    }
            //}
            //else
            //{
            //    if (dayNum <= 212)
            //    { // July
            //        goto End;
            //    }
            //    else
            //    {
            //        dayNum -= 212;
            //        monthNum += 7;
            //    }

            //    if (dayNum <= 31)
            //    { // August
            //        goto End;
            //    }
            //    else
            //    {
            //        dayNum -= 31;
            //        monthNum++;
            //    }

            //    if (dayNum <= 30)
            //    { // September
            //        goto End;
            //    }
            //    else
            //    {
            //        dayNum -= 30;
            //        monthNum++;
            //    }

            //    if (dayNum <= 31)
            //    { // October
            //        goto End;
            //    }
            //    else
            //    {
            //        dayNum -= 31;
            //        monthNum++;
            //    }

            //    if (dayNum <= 30)
            //    { // November
            //        goto End;
            //    }
            //    else
            //    {
            //        dayNum -= 30;
            //        monthNum++;
            //    }

            //    if (dayNum <= 31)
            //    { // December
            //        goto End;
            //    }
            //    else
            //    {
            //        dayNum -= 31;
            //        monthNum++;
            //    }
            //}
            //End:

            //string? monthName = null;

            //switch (monthNum)
            //{
            //    case 0:
            //        monthName = "January"; break;
            //    case 1:
            //        monthName = "February"; break;
            //    case 2:
            //        monthName = "March"; break;
            //    case 3:
            //        monthName = "April"; break;
            //    case 4:
            //        monthName = "May"; break;
            //    case 5:
            //        monthName = "June"; break;
            //    case 6:
            //        monthName = "July"; break;
            //    case 7:
            //        monthName = "August"; break;
            //    case 8:
            //        monthName = "September"; break;
            //    case 9:
            //        monthName = "October"; break;
            //    case 10:
            //        monthName = "November"; break;
            //    case 11:
            //        monthName = "December"; break;
            //    default:
            //        Console.WriteLine("\nВведено число больше 365-ти");
            //        goto Start;

            foreach (int daysInMonths in DaysInMonths)
            {
                if (dayNum <= daysInMonths) break;
                else
                {
                    dayNum -= daysInMonths;
                    monthNum++;
                }
            }

            MonthName temp = (MonthName)monthNum;
            string monthName = temp.ToString();

            Console.WriteLine($"\n{dayNum},{monthName}");
            Console.ReadKey();
        }

    }
}
        
    
