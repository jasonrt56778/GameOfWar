using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfWar
{
    class War
    {
        private int player1Score = 0;
        private int player2Score = 0;
        private Queue<Card> player1Deck;
        private Queue<Card> player2Deck;
        public static void Main(string[] args)
        {
            War hi = new War();
        }

        public void handOutCards()
        {
            Deck newDeck = new Deck();
            Queue<Card> cards = newDeck.getDeck();
            player1Deck = new Queue<Card>();
            player2Deck = new Queue<Card>();
            for(int i = 0; i < 26; i++)
            {
                player1Deck.Enqueue(cards.Dequeue());
                player2Deck.Enqueue(cards.Dequeue());
            }
        }

        private bool tieGame()
        {
            if (player2Deck.Count < 3 || player2Deck.Count < 3)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    player1Deck.Dequeue();
                    player2Deck.Dequeue();
                }
                int player1Value = player1Deck.Peek().getValue();
                int player2Value = player2Deck.Peek().getValue();
                if (player1Value > player2Value)
                {
                    player1Deck.Enqueue(player2Deck.Dequeue());
                }
                else if(player1Value < player2Value)
                {
                    player2Deck.Enqueue(player2Deck.Dequeue());
                }
                else
                {
                    tieGame();
                }
            }
            return false;
        }

        public War()
        {
            handOutCards();
            Deck player1Cards = new Deck();
            player1Cards.setDeck(player1Deck);
            Deck player2Cards = new Deck();
            player2Cards.setDeck(player2Deck);
            Player player1 = new Player("Jason", player1Cards);
            Player player2 = new Player("Eric", player2Cards);
            int count = 0;
            while (player1Cards.deckCount() > 0 && count < 500)
            {
                int player1Card = player1Cards.getDeck().Peek().getValue();
                int player2Card = player2Cards.getDeck().Peek().getValue();
                if (player1Card > player2Card)
                {
                    Console.WriteLine(player1Cards.getDeck().Peek().getName() + " beats " + player2Cards.getDeck().Peek().getName());
                    player1Cards.getDeck().Enqueue(player1Cards.getDeck().Dequeue());
                    player1Cards.getDeck().Enqueue(player2Cards.getDeck().Dequeue());
                }
                else if (player1Card < player2Card)
                {
                    Console.WriteLine(player2Cards.getDeck().Peek().getName() + " beats " + player1Cards.getDeck().Peek().getName());
                    player2Cards.getDeck().Enqueue(player2Cards.getDeck().Dequeue());
                    player2Cards.getDeck().Enqueue(player1Cards.getDeck().Dequeue());
                }
                else
                {
                    if(tieGame())
                    {
                        break;
                    }
                }
                count++;
            }
            if(player1Cards.deckCount() > player2Cards.deckCount())
            {
                Console.WriteLine("Player1 Wins");
            }
            else
            {
                Console.WriteLine("Player2 Wins");
            }
            //Console.WriteLine("Player1: " + player1Score + "\nPlayer2: " + player2Score);
        }
    }
}
