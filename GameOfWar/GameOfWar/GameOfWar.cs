using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfWar
{
    class GameOfWar
    {
        private DeckHolder player1Cards = new DeckHolder();
        private DeckHolder player2Cards = new DeckHolder();
        private Queue<Card> playerWarCards = new Queue<Card>();
        private Player player1;
        private Player player2;
        private string player1Name;
        private string player2Name;
        private int roundCounter = 0;

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
        private void endGameForTie(string winnerName, string loserName)
        {
            Console.WriteLine("Giving all cards to " + winnerName + " since " + loserName + " ran out during war.");
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

        private void burnThreeCards()
        {
            for (int i = 0; i < 3; i++)
            {
                playerWarCards.Enqueue(player1Cards.Deck.Dequeue());
                playerWarCards.Enqueue(player2Cards.Deck.Dequeue());
            }
        }

        private void printOutWinner(DeckHolder winnerDeck, DeckHolder loserDeck)
        {
            Console.WriteLine(player1Name + ": " + player1Cards.firstCardName());
            Console.WriteLine(player2Name + ": " + player2Cards.firstCardName());
            Console.WriteLine(winnerDeck.firstCardName() + " beats " + loserDeck.firstCardName() + "\n");
        }
        private void printOutCards()
        {
            Console.WriteLine(player1Name + ": " + player1Cards.firstCardName());
            Console.WriteLine(player2Name + ": " + player2Cards.firstCardName());
        }

        private void giveFirstPlayerSecondsCards()
        {
            printOutWinner(player1Cards, player2Cards);
            player1Cards.Deck.Enqueue(player1Cards.Deck.Dequeue());
            player1Cards.Deck.Enqueue(player2Cards.Deck.Dequeue());
            if (playerWarCards.Count > 0)
            {
                int playerWarCardsLength = playerWarCards.Count;
                for (int j = 0; j < playerWarCardsLength; j++)
                {
                    player1Cards.Deck.Enqueue(playerWarCards.Dequeue());
                }
            }
        }

        private void giveSecondPlayerFirstsCards()
        {
            printOutWinner(player2Cards, player1Cards);
            player2Cards.Deck.Enqueue(player2Cards.Deck.Dequeue());
            player2Cards.Deck.Enqueue(player1Cards.Deck.Dequeue());
            if (playerWarCards.Count > 0)
            {
                int playerWarCardsLength = playerWarCards.Count;
                for (int j = 0; j < playerWarCardsLength; j++)
                {
                    player2Cards.Deck.Enqueue(playerWarCards.Dequeue());
                }
            }
        }

        private void flipCards(int player1Value, int player2Value)
        {
            if (player1Value > player2Value)
            {
                giveFirstPlayerSecondsCards();
            }
            else if (player1Value < player2Value)
            {
                giveSecondPlayerFirstsCards();
            }
        }

        private bool tieGame()
        {
            printOutCards();
            Console.WriteLine(player1Cards.firstCardName() + " ties with " + player2Cards.firstCardName());
            Console.WriteLine("WAR");
            Console.WriteLine("1...");
            Console.WriteLine("2...");
            Console.WriteLine("3...");

            if (player2Cards.deckCount() < 4)
            {
                endGameForTie(player1Name, player2Name);
                return true;
            }
            else if(player1Cards.deckCount() < 4)
            {
                endGameForTie(player2Name, player1Name);
                return true;
            }
            else
            {
                //burn three cards
                burnThreeCards();
                int player1Value = player1Cards.firstCardValue();
                int player2Value = player2Cards.firstCardValue();
                if(player1Value == player2Value)
                {
                    roundCounter++;
                    Console.WriteLine("Round " + roundCounter);
                    if (tieGame())
                    {
                        return true;
                    }
                }
                else
                {
                    flipCards(player1Value, player2Value);
                }
                
            }
            return false;
        }

        public string getPlayerName()
        {
            Console.Write("Please enter a player name: ");
            string playerName = Console.ReadLine();
            if (playerName.Equals("") && !(player1Name.Equals("Jason")))
            {
                playerName = "Jason";
            }
            else if(playerName.Equals(""))
            {
                playerName = "Eric";
            }
            return playerName;
        }

        private int rounds = 0;
        private void getRounds()
        {
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

        private void promptUserDefaultSettings()
        {
            bool defaultFlag = true;
            bool doWeWantDefaultFlag = true;
            Console.WriteLine("Default Settings:\nPlayer1 Name: Jason\nPlayer2 Name: Eric\nRound Limit: 500");
            Console.Write("Do you want default settings(Y/N): ");
            while (defaultFlag)
            {
                string defaultInput = Console.ReadLine();
                if (defaultInput.ToUpper().Equals("Y"))
                {
                    doWeWantDefaultFlag = true;
                    defaultFlag = false;
                    player1Name = "Jason";
                    player2Name = "Eric";
                    rounds = 500;
                }
                else if (defaultInput.ToUpper().Equals("N"))
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
                player1Name = getPlayerName();
                player2Name = getPlayerName();
                getRounds();
            }
        }

        private void printEndGameScore(string winnerName)
        {
            Console.WriteLine(player1Name + " Card Count: " + player1Cards.deckCount());
            Console.WriteLine(player2Name + " Card Count: " + player2Cards.deckCount());
            Console.WriteLine(winnerName + " Wins");
        }
        public void Play()
        {
            promptUserDefaultSettings();
            handOutCards();
            player1 = new Player(player1Name, player1Cards);
            player2 = new Player(player2Name, player2Cards);
            while (player1Cards.deckCount() > 0 && player2Cards.deckCount() > 0 && roundCounter < rounds)
            {
                roundCounter++;
                Console.WriteLine("Round " + roundCounter);
                int player1Value = player1Cards.firstCardValue();
                int player2Value = player2Cards.firstCardValue();
                if (player1Value == player2Value)
                {
                    if (tieGame())
                    {
                        break;
                    }
                }
                else
                {
                    flipCards(player1Value, player2Value);
                }
                
            }
            if(roundCounter == rounds)
            {
                if (roundCounter == 1)
                    Console.WriteLine("We're at " + rounds + " turn... probably time to end the game.");
                else
                    Console.WriteLine("We're at " + rounds +" turns... probably time to end the game.");
            }
            if(player1Cards.deckCount() > player2Cards.deckCount())
            {
                printEndGameScore(player1Name);
            }
            else
            {
                printEndGameScore(player2Name);
            }
        }
    }
}
