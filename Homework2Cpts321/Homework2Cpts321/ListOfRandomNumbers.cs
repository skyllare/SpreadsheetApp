﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Homework2Cpts321
{
    /// <summary>
    /// ListOfRandomNumbers class, which houses the methods 
    /// used in the generation and unique number counts
    /// </summary>
    public class ListOfRandomNumbers
    {
        /// <summary>
        /// GenerateRandomNumberList used to make the main random number lsit
        /// of 10000 numbers
        /// </summary>
        /// <param name="randomList"></param>
        public void GenerateRandomNumberList(ref List<int> randomList)
        {
            Random random = new Random();
            for (int i = 0; i < 10000; ++i) 
            {
                randomList.Add(random.Next(20000));

            }
        }

    }
}