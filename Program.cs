using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
             SolveQuestion01();
             SolveQuestion02();
             SolveQuestion03();
             SolveQuestion04();
             SolveQuestion05();
             SolveQuestion06();
        }

        #region Question 01: String Efficiency & StringBuilder
        /* * (a) Why the original code is inefficient:
         * Strings in C# are "immutable," meaning once a string object is created in memory, it cannot be changed.
         * Every time the code does `productList += ...`, the runtime doesn't just append to the existing string. 
         * Instead, it creates an entirely new string object in the Managed Heap, copies the old content, 
         * appends the new content, and then abandons the old object for Garbage Collection.
         */
        static void SolveQuestion01()
        {
            const int iterations = 5000;
            Stopwatch sw = new Stopwatch();

           
            sw.Start();
            string productList = "";
            for (int i = 1; i <= iterations; i++)
            {
                productList += "PROD-" + i + ",";
            }
            sw.Stop();
            long slowTime = sw.ElapsedMilliseconds;
            Console.WriteLine($"String Concatenation Time: {slowTime}ms");

            sw.Restart();
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= iterations; i++)
            {
                sb.Append("PROD-").Append(i).Append(",");
            }
            string efficientList = sb.ToString();
            sw.Stop();
            long fastTime = sw.ElapsedMilliseconds;

            Console.WriteLine($"StringBuilder Time: {fastTime}ms");
            Console.WriteLine($"StringBuilder is approximately {slowTime / (fastTime == 0 ? 1 : fastTime)}x faster.");
        }
        #endregion

        #region Question 02: Ticket Pricing System
        static void SolveQuestion02()
        {
            Console.WriteLine("--- Cinema Ticket Pricing System ---");

            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter Day of Week (1-7, where 6=Fri, 7=Sat): ");
            int day = int.Parse(Console.ReadLine());

            Console.Write("Do you have a valid Student ID? (yes/no): ");
            bool isStudent = Console.ReadLine().Trim().ToLower() == "yes";

            double basePrice = 0;
            string breakdown = "";

            if (age < 5)
            {
                basePrice = 0;
                breakdown = "Age < 5: Free";
            }
            else if (age <= 12)
            {
                basePrice = 30;
                breakdown = "Age 5-12: 30 LE";
            }
            else if (age < 60)
            {
                basePrice = 50;
                breakdown = "Age 13-59: 50 LE";
            }
            else
            {
                basePrice = 25;
                breakdown = "Age 60+: 25 LE";
            }

            double finalPrice = basePrice;

            if (basePrice > 0 && (day == 6 || day == 7))
            {
                finalPrice += 10;
                breakdown += "\nWeekend Surcharge: +10 LE";
            }

            if (basePrice > 0 && isStudent)
            {
                double discount = finalPrice * 0.20;
                finalPrice -= discount;
                breakdown += $"\nStudent Discount (20%): -{discount} LE";
            }

            Console.WriteLine("\n--- Receipt ---");
            Console.WriteLine(breakdown);
            Console.WriteLine($"Final Total: {finalPrice} LE");
        }
        #endregion

        #region Question 03: Switch Statements
        static void SolveQuestion03()
        {
            string fileExtension = ".pdf";
            string fileType;

            switch (fileExtension)
            {
                case ".pdf":
                    fileType = "PDF Document";
                    break;
                case ".docx":
                case ".doc":
                    fileType = "Word Document";
                    break;
                case ".xlsx":
                case ".xls":
                    fileType = "Excel Spreadsheet";
                    break;
                case ".jpg":
                case ".png":
                case ".gif":
                    fileType = "Image File";
                    break;
                default:
                    fileType = "Unknown File Type";
                    break;
            }

            string fileTypeExpr = fileExtension switch
            {
                ".pdf" => "PDF Document",
                ".docx" or ".doc" => "Word Document",
                ".xlsx" or ".xls" => "Excel Spreadsheet",
                ".jpg" or ".png" or ".gif" => "Image File",
                _ => "Unknown File Type"
            };
        }
        #endregion

        #region Question 04: Ternary Operator
        static void SolveQuestion04()
        {
            int temperature = 35;

            string weatherAdvice = (temperature < 0) ? "Freezing! Stay indoors."
                                 : (temperature < 15) ? "Cold. Wear a jacket."
                                 : (temperature < 25) ? "Pleasant weather."
                                 : (temperature < 35) ? "Warm. Stay hydrated."
                                 : "Hot! Avoid sun exposure.";

            Console.WriteLine(weatherAdvice);

            /*
             * Answer: 
             * Is it more readable? Generally, NO. Nested ternaries are often harder to read and debug 
             * than a clean if-else if block because they lack clear indentation and labels.
             * * When to choose which?
             * 1. Use Ternary for simple, binary assignments (e.g., status = isActive ? "Online" : "Offline").
             * 2. Use If-Else for complex logic, multi-line blocks, or when you have more than 2-3 conditions.
             */
        }
        #endregion

        #region Question 05: Password Validation
        static void SolveQuestion05()
        {
            int attempts = 0;
            bool isValid = false;

            do
            {
                Console.Write("\nEnter a new password: ");
                string password = Console.ReadLine() ?? "";
                attempts++;

                List<string> errors = new List<string>();

                if (password.Length < 8) errors.Add("- Minimum 8 characters");

                bool hasUpper = false;
                bool hasDigit = false;
                bool hasSpace = false;

                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpper = true;
                    if (char.IsDigit(c)) hasDigit = true;
                    if (char.IsWhiteSpace(c)) hasSpace = true;
                }

                if (!hasUpper) errors.Add("- At least one uppercase letter");
                if (!hasDigit) errors.Add("- At least one digit");
                if (hasSpace) errors.Add("- No spaces allowed");

                if (errors.Count == 0)
                {
                    Console.WriteLine("Password accepted!");
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid password:");
                    foreach (var err in errors) Console.WriteLine(err);

                    if (attempts >= 5)
                    {
                        Console.WriteLine("Account locked");
                        return;
                    }
                    Console.WriteLine($"Attempts remaining: {5 - attempts}");
                }

            } while (!isValid && attempts < 5);
        }
        #endregion

        #region Question 06: Array Processing
        static void SolveQuestion06()
        {
            int[] scores = { 85, 42, 91, 67, 55, 78, 39, 88, 72, 95, 60, 48 };

            Console.WriteLine("Failing Scores (< 50):");
            foreach (int s in scores)
            {
                if (s < 50) Console.Write(s + " ");
            }
            Console.WriteLine();

            foreach (int s in scores)
            {
                if (s > 90)
                {
                    Console.WriteLine($"First score above 90: {s}");
                    break; // Stop searching immediately
                }
            }

            
            double sum = 0;
            int count = 0;
            foreach (int s in scores)
            {
                if (s >= 40)
                {
                    sum += s;
                    count++;
                }
            }
            double average = (count > 0) ? sum / count : 0;
            Console.WriteLine($"Class average (excluding absent students): {average:F2}");

            int a = 0, b = 0, c = 0, d = 0, f = 0;
            foreach (int s in scores)
            {
                if (s >= 90) a++;
                else if (s >= 80) b++;
                else if (s >= 70) c++;
                else if (s >= 60) d++;
                else f++;
            }
            Console.WriteLine($"Grade Count -> A: {a}, B: {b}, C: {c}, D: {d}, F: {f}");
        }
        #endregion
    }
}