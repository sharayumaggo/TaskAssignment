using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PokerHand
{
    class Program
    {
        static void Main(string[] args)
        {
            // Code for input string from command line
            // kindly input each set on new line pressing enter button
            // For command line result press enter 2 times to get result
            Console.Write(new string('\n', 2));
            Console.WriteLine("Please Input Set of 10 Cards " +
                "(Please press enter to input next set. " +
                "For getting the result press enter 2 times)");
            Console.WriteLine(" ");
            // to get the input string from command line
            var line = Console.ReadLine();
            // store the player wining counts in variable
            var player1_count = 0;
            var player2_count = 0;

            // If blank value is passed from command line, it should not throw exception
            while (line != null && line != "")
            {
                var result = Game.Judge(line);
                if (result.Item1 == "Player 1")
                {
                    player1_count += result.Item2;
                }
                else
                {
                    player2_count += result.Item3;
                }
                Console.WriteLine("Please enter another set or press enter");
                line = Console.ReadLine();
            }
            Console.Write(new string('\n', 2));
            if (player1_count > player2_count)
            {
                Console.WriteLine("Player 1 is Winner");
            }
            else if(player1_count==player2_count)
            {
                Console.WriteLine("Both player's score is same");
            }
            else
            {
                Console.WriteLine("Player 2 is Winner");
            }
            Console.WriteLine("-----------------------------------");
            Console.Write(new string('\n', 2));

            Console.WriteLine("Player 1: " + player1_count+" Hands");
            Console.WriteLine(" ");
            Console.WriteLine("Player 2: " + player2_count+" Hands");
            Console.ReadLine();


            //// // code for reading data from text file
            //using (var reader = File.OpenText("poker-hands.txt"))
            //{
            //    var line = reader.ReadLine();
            //    var player1_count = 0;
            //    var player2_count = 0;


            //    while (line != null)
            //    {
            //        var result = Game.Judge(line);
            //        if (result.Item1 == "Player 1")
            //        {
            //            player1_count += result.Item2;
            //        }
            //        else
            //        {
            //            player2_count += result.Item3;
            //        }
            //        line = reader.ReadLine();
            //    }

            //    Console.WriteLine(" ");

            //   Console.WriteLine("Player 1: " + player1_count+" Hands");
            //    Console.WriteLine(" ");
            //    Console.WriteLine("Player 2: " + player2_count+" Hands");
            //    Console.Read();
            //}


        }
    }
}
