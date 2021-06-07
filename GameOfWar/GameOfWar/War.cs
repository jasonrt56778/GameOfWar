using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfWar
{
    class War
    {
        private Deck player1Cards = new Deck();
        private Deck player2Cards = new Deck();
        private Queue<Card> playerWarCards = new Queue<Card>();
        private Player player1;
        private Player player2;
        private string player1Name;
        private string player2Name;
        private int roundCounter = 0;
        public static void Main(string[] args)
        {
            War hi = new War();
        }

        public void handOutCards()
        {
            Deck newDeck = new Deck();
            newDeck.populateDeck();
            Queue<Card> cards = newDeck.getDeck();
            for(int i = 0; i < 26; i++)
            {
                player1Cards.getDeck().Enqueue(cards.Dequeue());
                player2Cards.getDeck().Enqueue(cards.Dequeue());
            }
        }

        private bool tieGame()
        {
            
            
            Console.WriteLine(player1Name + ": " + player1Cards.getNameOfFirstCard());
            Console.WriteLine(player2Name + ": " + player2Cards.getNameOfFirstCard());
            Console.WriteLine(player1Cards.getNameOfFirstCard() + " ties with " + player2Cards.getNameOfFirstCard());
            Console.WriteLine("WAR");
            Console.WriteLine("1...");
            Console.WriteLine("2...");
            Console.WriteLine("3...");
            if (player2Cards.deckCount() < 4)
            {
                Console.WriteLine("Giving all cards to " + player1Name + " since " + player2Name + " ran out during war.");
                while(player2Cards.deckCount() > 0)
                {
                    player1Cards.getDeck().Enqueue(player2Cards.getDeck().Dequeue());
                }
                int playerWarCardsLength = playerWarCards.Count;
                for (int j = 0; j < playerWarCardsLength; j++)
                {
                    player1Cards.getDeck().Enqueue(playerWarCards.Dequeue());
                }
                Console.WriteLine("Game Ending");
                return true;
            }
            else if(player1Cards.deckCount() < 4)
            {
                Console.WriteLine("Giving all cards to " + player2Name + " since " + player1Name + " ran out during war.");
                while (player1Cards.deckCount() > 0)
                {
                    player2Cards.getDeck().Enqueue(player1Cards.getDeck().Dequeue());
                }
                int playerWarCardsLength = playerWarCards.Count;
                for (int j = 0; j < playerWarCardsLength; j++)
                {
                    player2Cards.getDeck().Enqueue(playerWarCards.Dequeue());
                }
                Console.WriteLine("Game Ending");
                return true;
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    playerWarCards.Enqueue(player1Cards.getDeck().Dequeue());
                    playerWarCards.Enqueue(player2Cards.getDeck().Dequeue());
                }
                int player1Value = player1Cards.getValueOfFirstCard();
                int player2Value = player2Cards.getValueOfFirstCard();
                if (player1Value > player2Value)
                {
                    Console.WriteLine(player1Name + ": " + player1Cards.getNameOfFirstCard());
                    Console.WriteLine(player2Name + ": " + player2Cards.getNameOfFirstCard());
                    Console.WriteLine(player1Cards.getNameOfFirstCard() + " beats " + player2Cards.getNameOfFirstCard() + "\n");
                    player1Cards.getDeck().Enqueue(player1Cards.getDeck().Dequeue());
                    player1Cards.getDeck().Enqueue(player2Cards.getDeck().Dequeue());
                    int playerWarCardsLength = playerWarCards.Count;
                    for (int j = 0; j < playerWarCardsLength; j++)
                    {
                        player1Cards.getDeck().Enqueue(playerWarCards.Dequeue());
                    }
                }
                else if(player1Value < player2Value)
                {
                    Console.WriteLine(player1Name + ": " + player1Cards.getNameOfFirstCard());
                    Console.WriteLine(player2Name + ": " + player2Cards.getNameOfFirstCard());
                    Console.WriteLine(player2Cards.getNameOfFirstCard() + " beats " + player1Cards.getNameOfFirstCard() + "\n");
                    player2Cards.getDeck().Enqueue(player2Cards.getDeck().Dequeue());
                    player2Cards.getDeck().Enqueue(player1Cards.getDeck().Dequeue());
                    int playerWarCardsLength = playerWarCards.Count;
                    for (int j = 0; j < playerWarCardsLength; j++)
                    {
                        player2Cards.getDeck().Enqueue(playerWarCards.Dequeue());
                    }
                }
                else
                {
                    roundCounter++;
                    Console.WriteLine("Round " + roundCounter);
                    if (tieGame())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public War()
        {
            handOutCards();
            player1 = new Player("Jason", player1Cards);
            player2 = new Player("Eric", player2Cards);
            player1Name = player1.getName();
            player2Name = player2.getName();
            while (player1Cards.deckCount() > 0 && player2Cards.deckCount() > 0 && roundCounter < 500)
            {
                roundCounter++;
                Console.WriteLine("Round " + roundCounter);
                int player1Card = player1Cards.getValueOfFirstCard();
                int player2Card = player2Cards.getValueOfFirstCard();
                if (player1Card > player2Card)
                {
                    Console.WriteLine(player1Name + ": " + player1Cards.getNameOfFirstCard());
                    Console.WriteLine(player2Name + ": " + player2Cards.getNameOfFirstCard());
                    Console.WriteLine(player1Cards.getNameOfFirstCard() + " beats " + player2Cards.getNameOfFirstCard() + "\n");
                    player1Cards.getDeck().Enqueue(player1Cards.getDeck().Dequeue());
                    player1Cards.getDeck().Enqueue(player2Cards.getDeck().Dequeue());
                }
                else if (player1Card < player2Card)
                {
                    Console.WriteLine(player1Name + ": " + player1Cards.getNameOfFirstCard());
                    Console.WriteLine(player2Name + ": " + player2Cards.getNameOfFirstCard());
                    Console.WriteLine(player2Cards.getNameOfFirstCard() + " beats " + player1Cards.getNameOfFirstCard() + "\n");
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
            }
            if(roundCounter == 500)
            {
                Console.WriteLine("We're at 500 turns... probably time to end the game.");
            }
            if(player1Cards.deckCount() > player2Cards.deckCount())
            {
                Console.WriteLine(player1Name + " Card Count: " + player1Cards.deckCount());
                Console.WriteLine(player2Name + " Card Count: " + player2Cards.deckCount());
                Console.WriteLine(player1Name + " Wins");
            }
            else
            {
                Console.WriteLine(player1Name + " Card Count: " + player1Cards.deckCount());
                Console.WriteLine(player2Name + " Card Count: " + player2Cards.deckCount());
                Console.WriteLine(player2Name + " Wins");
            }
            //Console.WriteLine("Player1: " + player1Score + "\nPlayer2: " + player2Score);
        }
    }
}
