using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01629177Assignment2.Controllers
{
    public class J2Controller : ApiController
    {
        /// <summary>
        /// Calculates the number of different ways a value of 10 can be rolled from 2 dices m & n.
        /// </summary>
        /// <example>api/J2/DiceGame/6/8</example>
        /// <example>api/J2/DiceGame/12/4</example>
        /// <example>api/J2/DiceGame/3/3</example>
        /// <example>api/J2/DiceGame/5/5</example>
        /// <param name="m">A positive integer representing a `m` sided dice.</param>
        /// <param name="n">A positive integer representing a `n` sided dice.</param>
        /// <returns>Returns the number of different ways a value of 10 can be rolled from the specified sided dies</returns>
        [Route("api/J2/DiceGame/{m}/{n}")]
        public int GetDiceGame(int m, int n)
        {
            /*
             * I know this can be done with for loops, but I wanted
             * to see if I can derive a formula to calculate this in
             * constant time:
             *
             * -------------------------------------------------------
             *
             * First I setup some constraints:
             *  Maximum values for m & n are 9, because only the rolls
             *  less than 9 can be added to another dice to equal 10.
             *  
             *  m: max 9 : 1, 2, 3, 4, 5, 6, 7, 8, 9
             *  n: max 9 : 1, 2, 3, 4, 5, 6, 7, 8, 9
             *
             *  If m + n is less than 10, then I automatically know
             *  that no combination of the dice's numbers will equal
             *  10.
             *
             *  m+n < 10 = 0
             *
             *  Negative side dice isn't possible either, so if this
             *  is encountered, return an error in the form of -1.
             *
             * -------------------------------------------------------
             *
             * Next I tried to see if I could spot a pattern in the
             * dice rolls that would add up to 10.
             * 
             *  m=1, n=9 : 1+9
             *  m=2, n=9 : 1+9, 2+8
             *  m=3, n=9 : 1+9, 2+8, 3+7
             *  m=4, n=9 : 1+9, 2+8, 3+7, 4+6
             *  m=5, n=9 : 1+9, 2+8, 3+7, 4+6, 5+5
             *  m=6, n=9 : 1+9, 2+8, 3+7, 4+6, 5+5, 6+4
             *  m=7, n=9 : 1+9, 2+8, 3+7, 4+6, 5+5, 6+4, 7+3
             *  m=8, n=9 : 1+9, 2+8, 3+7, 4+6, 5+5, 6+4, 7+3, 8+2
             *  m=9, n=9 : 1+9, 2+8, 3+7, 4+6, 5+5, 6+4, 7+3, 8+2, 9+1

             *  m=1, n=8 : 
             *  m=2, n=8 : 2+8
             *  m=3, n=8 : 2+8, 3+7
             *  m=4, n=8 : 2+8, 3+7, 4+6
             *  m=5, n=8 : 2+8, 3+7, 4+6, 5+5
             *  m=6, n=8 : 2+8, 3+7, 4+6, 5+5, 6+4
             *  m=7, n=8 : 2+8, 3+7, 4+6, 5+5, 6+4, 7+3
             *  m=8, n=8 : 2+8, 3+7, 4+6, 5+5, 6+4, 7+3, 8+2
             *  m=9, n=8 : 2+8, 3+7, 4+6, 5+5, 6+4, 7+3, 8+2, 9+1

             *  m=1, n=7 : 
             *  m=2, n=7 : 
             *  m=3, n=7 : 3+7
             *  m=4, n=7 : 3+7, 4+6
             *  m=5, n=7 : 3+7, 4+6, 5+5
             *  m=6, n=7 : 3+7, 4+6, 5+5, 6+4
             *  m=7, n=7 : 3+7, 4+6, 5+5, 6+4, 7+3
             *  m=8, n=7 : 3+7, 4+6, 5+5, 6+4, 7+3, 8+2
             *  m=9, n=7 : 3+7, 4+6, 5+5, 6+4, 7+3, 8+2, 9+1
             *  
             *  - 1 2 3 4 5 6 7 8 9  A 
             *  1                 1  1
             *  2               1 2  2
             *  3             1 2 3  3
             *  4           1 2 3 4  4
             *  5         1 2 3 4 5  5
             *  6       1 2 3 4 5 6  6
             *  7     1 2 3 4 5 6 7  7
             *  8   1 2 3 4 5 6 7 8  8
             *  9 1 2 3 4 5 6 7 8 9  9
             *
             *  A 1 2 3 4 5 6 7 8 9  9
             *
             * -------------------------------------------------------
             *
             *  It looks like there is clearly a pattern, so I try to
             *  see if I can derive a formula from it.
             *
             *  m=4, n=6 : «1»
             *      10-n : 4;
             *      10-m : 6;
             *      m-(10-n) : 0
             *      n-(10-m) : 0
             *      (m-(10-n))+1 : 1

             *  m=5, n=6 : «2»
             *      10-n : 4
             *      10-m : 5
             *      m-(10-n) : 1
             *      n-(10-m) : 1
             *      (m-(10-n))+1 : 2

             *  m=6, n=6 : «3»
             *      10-n : 4
             *      10-m : 4
             *      m-(10-n) : 2
             *      n-(10-m) : 2
             *      (m-(10-n))+1 : 3
             *
             *  m=7, n=6 : «4»
             *      10-n : 4
             *      10-m : 3
             *      m-(10-n) : 3
             *      n-(10-m) : 3
             *      (m-(10-n))+1 : 4
             *
             *  m=8, n=7 : (m-(10-n))+1 = 6
             *  m=5, n=7 : (m-(10-n))+1 = 3
             *
             * -------------------------------------------------------
             *
             *  It looks like `m - (k - n) + 1` can be used, where k = 10
             *  However, values over `k - 1` for m & n breaks the formula:
             *  m=11, n=7 : (m-(10-n))+1 = 3
             * 
             *  So I add more constraints:
             *  k = 10
             *  m = min(k-1, m)
             *  n = min(k-1, n)
             *  
             *  And end up with the final formula:
             *  min(m, k-1) - (k - min(n, k - 1)) + 1
             * 
             *  Theoretically this should work for values of k higher than 10
             *  but I haven't tested that out yet. I think a value of k lower
             *  than 2 will also break it.
             * 
             *  The more I look at it, the more I feel like I should have went
             *  with the for loop method instead, since that might be more
             *  accurate? But I wanted to see how far I can get without using
             *  the for loops.
             *
             */
            const int k = 10;
            if (m < 1 || n < 1) return -1; //Discard invalid-sided dice
            else if (m + n < k) return 0; //Return 0 for low-sided dice
            else return min(m, k - 1) - (k - min(n, k - 1)) + 1;
        }

        private int min(int n1, int n2)
        {
            return n1 < n2 ? n1 : n2;
        }
    }
}
