using System;
using System.Diagnostics;

namespace LeetcodeStrongPasswordChecker
{
    public class Solution
    {
        public int StrongPasswordChecker(string s)
        {
            Console.WriteLine("Checking " + s);

            if (string.IsNullOrEmpty(s))
                return 6;

            var len = s.Length <= 20? s.Length: 20;
            
            var points = 0;

            bool haveLowerCase = false;
            bool haveUpperCase = false;
            bool haveDigit = false;
            bool haveAtLeast3RepeatingCharsInString = false;
            int numRepeating = 0;

            for (int i = 0; i < s.Length; i++)
            {
                var ch = s[i];

                if (char.IsLower(ch))
                    haveLowerCase = true;
                if (char.IsUpper(ch))
                    haveUpperCase = true;
                if (char.IsDigit(ch))
                    haveDigit = true;

                var repeating =GetRepeatingChars(s, ch, i+1, len);
                if (repeating >=2)
                {
                    Console.WriteLine($"repeating chars: {(repeating+1)/3} points");
                    haveAtLeast3RepeatingCharsInString = true;
                    points += (repeating+1) /3;
                    numRepeating += repeating+1;

                    i += repeating; // the i++ will bump this to correct index 
                }
            }

            if (!haveAtLeast3RepeatingCharsInString && numRepeating!=len)
            {
                if (!haveLowerCase)
                {
                    Console.WriteLine("No lowercase - 1 pt");
                    points++;
                }

                if (!haveUpperCase)
                {
                    Console.WriteLine("No uppercase - 1 pt");
                    points++;
                }

                if (!haveDigit)
                {
                    Console.WriteLine("No digit - 1 pt");
                    points++;
                }
            }


            if (s.Length < 6)
            {
                Console.WriteLine($"Shorter than 6 chars - {6 - len} points");
                return 6 - len;
            }
            else if (s.Length > 20)
            {
                Console.WriteLine($"Longer than 20 chars - {s.Length - 20} points");
                points += s.Length - 20;
            }


            return points;
        }

        private int GetRepeatingChars(string s, in char ch, in int i, int lenToParse)
        {
            int count = 0;
            for (var j = i; j < lenToParse && s[j] == ch; j++) 
                count++;
            return count;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var x = new Solution();
            int score = 0;
            score = x.StrongPasswordChecker("aaa");
            Debug.Assert(score == 3);

            score = x.StrongPasswordChecker("aaa123");
            Debug.Assert(score == 1);

            score = x.StrongPasswordChecker("aaaaaaaaaaaaaaaaaaaaa");
            Debug.Assert(score == 7);

            score = x.StrongPasswordChecker("ABABABABABABABABABAB1");
            Debug.Assert(score == 2);

            score = x.StrongPasswordChecker("aaa111");
            Debug.Assert(score == 2);


        }
    }
}
