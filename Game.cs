using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHand
{
    class Game
    {
        // return winner and count of hands for both players
   
        public static Tuple<string,int,int> Judge(string hands)
        {
            //split the string by space
            var cards = hands.Split(' ');
            //take first five cards for player 1 and next five for player 2
            var _player1 = new Hand(cards.Take(5));
            var _player2 = new Hand(cards.Skip(5).Take(5));
            // store winner and count for each player in variable 
            string winner = "";
            int playerOneCount = 0;
            int playerTwoCount = 0;

            if (_player1.Weight.Item1 == _player2.Weight.Item1)
            {
                if (_player1.Weight.Item2 > _player2.Weight.Item2)
                {
                    winner = "Player 1";
                    playerOneCount = 1;

                }
                else
                {
                    winner = "Player 2";
                    playerTwoCount = 1;
                }
                
                return new Tuple<string, int, int> (winner, playerOneCount, playerTwoCount);


            }

            if(_player1.Weight.Item1 > _player2.Weight.Item1)
            {
                winner = "Player 1";
                playerOneCount = 1;

            }
            else
            {
                winner = "Player 2";
                playerTwoCount = 1;
            }

            return new Tuple<string, int, int>(winner, playerOneCount, playerTwoCount);
        }
    }
}
