using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
public enum RPS
{
    Rock = 1,
    Paper =2,
    Scissors =3
};
    public class Round
    {
        public RPS Them;
        public RPS Us;
    }
}
