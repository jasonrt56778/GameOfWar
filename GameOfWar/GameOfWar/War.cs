using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfWar
{
    class War
    {
        private DeckHolder player1Cards = new DeckHolder();
        private DeckHolder player2Cards = new DeckHolder();
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
            DeckHolder newDeck = new DeckHolder();
            newDeck.populateDeck();
            Queue<Card> cards = newDeck.Deck;
            for(int i = 0; i < 26; i++)
            {
                player1Cards.Deck.Enqueue(cards.Dequeue());
                player2Cards.Deck.Enqueue(cards.Dequeue());
            }
        }

        //change the player name stuff and add params
        private void tieGameWinner()
        {
            Console.WriteLine("Giving all cards to " + player1Name + " since " + player2Name + " ran out during war.");
            while (player2Cards.deckCount() > 0)
            {
                player1Cards.Deck.Enqueue(player2Cards.Deck.Dequeue());
            }
            int playerWarCardsLength = playerWarCards.Count;
            for (int j = 0; j < playerWarCardsLength; j++)
            {
                player1Cards.Deck.Enqueue(playerWarCards.Dequeue());
            }
            Console.WriteLine("Game Ending");
        }

        private bool tieGame()
        {
            Console.WriteLine(player1Name + ": " + player1Cards.firstCardName());
            Console.WriteLine(player2Name + ": " + player2Cards.firstCardName());
            Console.WriteLine(player1Cards.firstCardName() + " ties with " + player2Cards.firstCardName());
            Console.WriteLine("WAR");
            Console.WriteLine("1...");
            Console.WriteLine("2...");
            Console.WriteLine("3...");

            //make these into methods by themselves
            if (player2Cards.deckCount() < 4)
            {
                tieGameWinner();
                return true;
            }
            else if(player1Cards.deckCount() < 4)
            {
                Console.WriteLine("Giving all cards to " + player2Name + " since " + player1Name + " ran out during war.");
                while (player1Cards.deckCount() > 0)
                {
                    player2Cards.Deck.Enqueue(player1Cards.Deck.Dequeue());
                }
                int playerWarCardsLength = playerWarCards.Count;
                for (int j = 0; j < playerWarCardsLength; j++)
                {
                    player2Cards.Deck.Enqueue(playerWarCards.Dequeue());
                }
                Console.WriteLine("Game Ending");
                return true;
            }
            else
            {
                //burn three cards
                for (int i = 0; i < 3; i++)
                {
                    playerWarCards.Enqueue(player1Cards.Deck.Dequeue());
                    playerWarCards.Enqueue(player2Cards.Deck.Dequeue());
                }
                int player1Value = player1Cards.firstCardValue();
                int player2Value = player2Cards.firstCardValue();
                if (player1Value > player2Value)
                {
                    Console.WriteLine(player1Name + ": " + player1Cards.firstCardName());
                    Console.WriteLine(player2Name + ": " + player2Cards.firstCardName());
                    Console.WriteLine(player1Cards.firstCardName() + " beats " + player2Cards.firstCardName() + "\n");
                    player1Cards.Deck.Enqueue(player1Cards.Deck.Dequeue());
                    player1Cards.Deck.Enqueue(player2Cards.Deck.Dequeue());
                    int playerWarCardsLength = playerWarCards.Count;
                    for (int j = 0; j < playerWarCardsLength; j++)
                    {
                        player1Cards.Deck.Enqueue(playerWarCards.Dequeue());
                    }
                }
                else if(player1Value < player2Value)
                {
                    Console.WriteLine(player1Name + ": " + player1Cards.firstCardName());
                    Console.WriteLine(player2Name + ": " + player2Cards.firstCardName());
                    Console.WriteLine(player2Cards.firstCardName() + " beats " + player1Cards.firstCardName() + "\n");
                    player2Cards.Deck.Enqueue(player2Cards.Deck.Dequeue());
                    player2Cards.Deck.Enqueue(player1Cards.Deck.Dequeue());
                    int playerWarCardsLength = playerWarCards.Count;
                    for (int j = 0; j < playerWarCardsLength; j++)
                    {
                        player2Cards.Deck.Enqueue(playerWarCards.Dequeue());
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
            bool defaultFlag = true;
            bool doWeWantDefaultFlag = true;
            int rounds = 0;
            Console.Write("Do you want default settings(Y/N): ");
            while (defaultFlag)
            {
                string defaultInput = Console.ReadLine();
                if(defaultInput.ToUpper().Equals("Y"))
                {
                    doWeWantDefaultFlag = true;
                    defaultFlag = false;
                    player1Name = "Jason";
                    player2Name = "Eric";
                    rounds = 500;
                }
                else if(defaultInput.ToUpper().Equals("N"))
                {
                    doWeWantDefaultFlag = false;
                    defaultFlag = false;
                }
                else
                {
                    Console.Write("Hmmm, that doesn't look like a valid option.\nPlease enter (Y/N): ");
                }
            }
            if (!doWeWantDefaultFlag)
            {
                Console.Write("Hi!\nPlease enter the first players name: ");
                player1Name = Console.ReadLine();
                if (player1Name.Equals(""))
                {
                    player1Name = "Jason";
                }
                Console.Write("\nPlease enter the second players name: ");
                player2Name = Console.ReadLine();
                if (player2Name.Equals(""))
                {
                    player2Name = "Eric";
                }
                Console.Write("How many rounds should be max: ");
                bool roundFlag = true;
                while (roundFlag)
                {
                    string roundInput = Console.ReadLine();
                    if (roundInput.Equals(""))
                    {
                        rounds = 500;
                        roundFlag = false;
                    }
                    else
                    {
                        try
                        {
                            rounds = Int16.Parse(roundInput);
                            if (rounds < 1 || rounds > 1000)
                            {
                                throw new FormatException();
                            }
                            roundFlag = false;
                        }
                        catch (FormatException ex)
                        {
                            Console.Write("Opps!  That doesn't look like a number we can use.\nPlease enter a valid number: ");
                        }
                    }
                }
            }
            handOutCards();
            player1 = new Player(player1Name, player1Cards);
            player2 = new Player(player2Name, player2Cards);
            while (player1Cards.deckCount() > 0 && player2Cards.deckCount() > 0 && roundCounter < rounds)
            {
                roundCounter++;
                Console.WriteLine("Round " + roundCounter);
                int player1Card = player1Cards.firstCardValue();
                int player2Card = player2Cards.firstCardValue();
                if (player1Card > player2Card)
                {
                    Console.WriteLine(player1Name + ": " + player1Cards.firstCardName());
                    Console.WriteLine(player2Name + ": " + player2Cards.firstCardName());
                    Console.WriteLine(player1Cards.firstCardName() + " beats " + player2Cards.firstCardName() + "\n");
                    player1Cards.Deck.Enqueue(player1Cards.Deck.Dequeue());
                    player1Cards.Deck.Enqueue(player2Cards.Deck.Dequeue());
                }
                else if (player1Card < player2Card)
                {
                    Console.WriteLine(player1Name + ": " + player1Cards.firstCardName());
                    Console.WriteLine(player2Name + ": " + player2Cards.firstCardName());
                    Console.WriteLine(player2Cards.firstCardName() + " beats " + player1Cards.firstCardName() + "\n");
                    player2Cards.Deck.Enqueue(player2Cards.Deck.Dequeue());
                    player2Cards.Deck.Enqueue(player1Cards.Deck.Dequeue());
                }
                else
                {
                    if(tieGame())
                    {
                        break;
                    }
                }
            }
            if(roundCounter == rounds)
            {
                Console.WriteLine("We're at " + rounds +" turns... probably time to end the game.");
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
        }
    }
}
