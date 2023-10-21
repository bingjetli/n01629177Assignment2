using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01629177Assignment2.Controllers
{
    public class J3Controller : ApiController
    {
        /* ---------------------------------------------------------------------
         * J3 Secret Instructions (2021)
         * ---------------------------------------------------------------------
         *
         * ## PROBLEM DESCRIPTION
         * Professor Santos has decided to hide a secret formula for a new type
         * of biofuel. She has however, left a sequence of coded instructions
         * for her assistant.
         * 
         * Each instruction is a sequence of five digits which represents a 
         * direction to turn and the number of steps to take.
         * 
         * The first 2 digits represent the direction to turn:
         *  - If their sum is odd, then the direction to turn is left,
         *  - If their sum is even, and not zero, then the direction to
         *    turn is right,
         *  - If their sum is zero, then the direction to turn is the 
         *    same as the previous instruction.
         * 
         * The remaining three digits represent the number of steps to take
         * which will always be at least 100. 
         * 
         * Your job is to decode the instructions so the assistant can use
         * them to find the secret formula.
         * 
         * ## INPUT SPECIFICATION
         * There will be at least two lines of input. Each line except the
         * last line will contain exactly five digits representing an
         * instruction. The first line will not begin with 00. The last line
         * will contain 99999 and no other line will contain 99999.
         * 
         * ## OUTPUT SPECIFICATION
         * There must be one line of output for each line of input except the
         * last line of input. These output lines correspond to the input lines
         * (in order). Each output line gives the decoding of the corresponding
         * instruction: either right or left, followed by a single space,
         * followed by the number of steps to be taken in that direction.
         * 
         * ## SAMPLE INPUT
         * 57234
         * 00907
         * 34100
         * 99999
         * 
         * ## SAMPLE OUTPUT
         * right 234
         * right 907
         * left 100
         */

        //api/J3/SecretInstructions?instructions=XXXXX\nYYYYY\n99999

        /// <summary>
        /// Decodes a list of encoded instructions.
        /// </summary>
        /// <example>api/J3/GetSecretInstructions?instructions=57234\n00907\n34100\n99999</example>
        /// <param name="instructions">A list of 5 digit instructions separated by \n</param>
        /// <returns>A list of decoded instructions as per the J3 problem.</returns>
        public string[] GetSecretInstructions(string instructions) {
            //Split the string into a string array.
            string[] separator = new string[] { "\\n" };
            //string[] instructions_array = instructions.Split('\n');
            //string[] instructions_array = instructions.Split(',');
            string[] instructions_array = instructions.Split(separator, StringSplitOptions.None);
            List<string> decoded_instructions_array = new List<string>();
            bool has_parsing_error = false;
            string direction = "forward";

            for(int i=0; i<instructions_array.Length; i++)
            {
                //Stop processing when 99999 is reached.
                if (instructions_array[i] == "99999") break;

                //Try to obtain the first digit
                int a = -1;
                has_parsing_error = !int.TryParse(instructions_array[i][0].ToString(), out a);
                if (has_parsing_error) break;

                //Try to obtain the second digit
                int b = -1;
                has_parsing_error = !int.TryParse(instructions_array[i][1].ToString(), out b);
                if (has_parsing_error) break;

                //Try to obtain the number of steps
                int number_of_steps = -1;
                has_parsing_error = !int.TryParse(instructions_array[i].Substring(2), out number_of_steps);
                if (has_parsing_error) break;

                //Decode direction
                //The way this is written, there should never be negative digits because
                //it'll result in a parsing error, although the thought just occurred to
                //me that I should make it robust enough to handle that.
                //
                //But for now, 0 + 0 is the only way the sum will equal 0. So I can just
                //make an if-statement to handle that.
                if(a == 0 && b == 0)
                {
                    //Direction does not change from the previous direction.
                    decoded_instructions_array.Add(direction + " " + number_of_steps);
                }
                else
                {
                    if((a + b) % 2 == 0)
                    {
                        //Even
                        direction = "right";
                        decoded_instructions_array.Add(direction + " " + number_of_steps);
                    }
                    else
                    {
                        //Odd
                        direction = "left";
                        decoded_instructions_array.Add(direction + " " + number_of_steps);
                    }
                }
                
            }

            //Return error case if an error occured during parsing.
            if (has_parsing_error) return new string[]
            {
                "Error: One of the instructions in the list contained something other than a number."
            };

            //Otherwise return the decoded instructions.
            return decoded_instructions_array.ToArray();
        }
    }
}
