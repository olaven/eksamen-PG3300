﻿using System;
using System.Threading;
using Item; 

namespace FleaMarket
{
    static class Program
    {
        private static void Main(string[] args)
        {
           var simulation = new Simulation();
           simulation.Run();
            
        }
    }
}