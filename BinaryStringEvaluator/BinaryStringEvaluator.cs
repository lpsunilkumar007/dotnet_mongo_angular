using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryStringAnalysis
{


    public class BinaryStringEvaluator
    {
        public static bool IsGoodBinaryString(string binaryString)
        {
            int count0 = 0; // To track the number of '0's
            int count1 = 0; // To track the number of '1's

            foreach (char bit in binaryString)
            {
                if (bit == '0')
                {
                    count0++;
                }
                else if (bit == '1')
                {
                    count1++;
                }
                else
                {
                    // Invalid character in the binary string
                    return false;
                }

                // Check the prefix condition: at no point should 1's be less than 0's
                if (count1 < count0)
                {
                    return false;
                }
            }

            // Check if the string has an equal number of '0's and '1's
            return count0 == count1;
        }

        public static void RunTests()
        {
            Console.WriteLine("Test Cases for IsGoodBinaryString:");

            // Example test cases
            string test1 = "1100";
            Console.WriteLine($"Binary String: {test1} -> Is Good? {IsGoodBinaryString(test1)}"); // True

            string test2 = "1001";
            Console.WriteLine($"Binary String: {test2} -> Is Good? {IsGoodBinaryString(test2)}"); // True

            string test3 = "1010";
            Console.WriteLine($"Binary String: {test3} -> Is Good? {IsGoodBinaryString(test3)}"); // False (prefix failure)

            string test4 = "110011";
            Console.WriteLine($"Binary String: {test4} -> Is Good? {IsGoodBinaryString(test4)}"); // True

            string test5 = "110100";
            Console.WriteLine($"Binary String: {test5} -> Is Good? {IsGoodBinaryString(test5)}"); // False (prefix failure)

            string test6 = "111000";
            Console.WriteLine($"Binary String: {test6} -> Is Good? {IsGoodBinaryString(test6)}"); // False (unequal 0's and 1's)

            string test7 = "110010";
            Console.WriteLine($"Binary String: {test7} -> Is Good? {IsGoodBinaryString(test7)}"); // False (prefix failure)

            string test8 = "1111";
            Console.WriteLine($"Binary String: {test8} -> Is Good? {IsGoodBinaryString(test8)}"); // False (unequal 0's and 1's)
        }
    }

}
