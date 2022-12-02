using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    public enum RockPaperScissors
    {
        Rock = 1,
        Paper =2,
        Scissors =3
    };

    public enum WinLoseDraw
    {
        Lose,
        Win,
        Draw
    }
    public class RoundPartA
    {
        public RockPaperScissors Them;
        public RockPaperScissors Us;
    }

    public class RoundPartB
    {
        public RockPaperScissors Them;
        public WinLoseDraw WhatShouldWeDo;
    }
}
