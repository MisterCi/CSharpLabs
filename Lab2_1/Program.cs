﻿using System;

namespace Lab2_1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Введіть кінець діапазона від 2 до : ");
            
            DateTime start = DateTime.Now;
            
            int maxNumber = int.Parse(Console.ReadLine());
            
            for (int i = 2; i <= maxNumber; i++) 
            {
                bool isPrime = true;
                
                for (int j = 2; j < i; j++) 
                {
                    if (i % j == 0 && i % 1 == 0) 
                    {
                        isPrime = false;
                    }
                }
                
                if (isPrime) 
                {
                    Console.Write("{0} ", i);
                }
            }
            
            DateTime end = DateTime.Now; 
            Console.WriteLine("\n{0} сек.", (end-start).TotalSeconds);
        }
    }
}