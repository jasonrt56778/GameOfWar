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
        private string player1Name = "";
        private string player2Name = "";
        private int roundCounter = 0;
        private const int MaxRounds = 500;
        private const int DeckCount = 52;

        public void HandOutCards()
        {
            DeckHolder newDeck = new DeckHolder();
            newDeck.PopulateDeck();
            Queue<Card> cards = newDeck.Deck;
            for (int i = 0; i < DeckCount / 2; i++)
            {
                player1Cards.Deck.Enqueue(cards.Dequeue());
                player2Cards.Deck.Enqueue(cards.Dequeue());
            }
        }

        //Make final for rounds and deckLength as 52
        private void EndGameForTie(string winnerName, string loserName)
        {
            Console.WriteLine("Giving all cards to " + winnerName + " since " + loserName + " ran out during war.");
            while (player2Cards.DeckCount > 0)
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

        //In the event of a tie, the user chose to burn three cards
        private void BurnThreeCards()
        {
            for (int i = 0; i < 3; i++)
            {
                BurnOneCard();
            }
        }

        private void BurnOneCard()
        {
            playerWarCards.Enqueue(player1Cards.Deck.Dequeue());
            playerWarCards.Enqueue(player2Cards.Deck.Dequeue());
        }

        private bool warRulesThreeBool = true;
        private void PromptUserWarRules()
        {
            bool warRulesFlag = true;
            Console.Write("Do you want to have WAR consist of putting down 1 card then\nflipping or putting down 3 cards then flipping(1/3): ");
            while (warRulesFlag)
            {
                string defaultInput = Console.ReadLine();
                if (defaultInput.Equals("1"))
                {
                    warRulesThreeBool = false;
                    warRulesFlag = false;

                }
                else if (defaultInput.Equals("3") || defaultInput.Equals(""))
                {
                    warRulesThreeBool = true;
                    warRulesFlag = false;
                }
                else
                {
                    Console.Write("Hmmm, that doesn't look like a valid option.\nPlease enter (1/3): ");
                }
            }
        }

        private void PrintOutWinner(DeckHolder winnerDeck, DeckHolder loserDeck)
        {
            PrintOutCards();
            Console.WriteLine(winnerDeck.FirstCardName + " beats " + loserDeck.FirstCardName + "\n");
        }
        private void PrintOutCards()
        {
            Console.WriteLine(player1Name + ": " + player1Cards.FirstCardName);
            Console.WriteLine(player2Name + ": " + player2Cards.FirstCardName);
        }

        private void GiveCards(DeckHolder winner, DeckHolder loser)
        {
            PrintOutWinner(winner, loser);
            winner.Deck.Enqueue(winner.Deck.Dequeue());
            winner.Deck.Enqueue(loser.Deck.Dequeue());
            if (playerWarCards.Count > 0)
            {
                int playerWarCardsLength = playerWarCards.Count;
                for (int j = 0; j < playerWarCardsLength; j++)
                {
                    winner.Deck.Enqueue(playerWarCards.Dequeue());
                }
            }
        }

        private void FlipCards(int player1Value, int player2Value)
        {
            if (player1Value > player2Value)
            {
                GiveCards(player1Cards, player2Cards);
            }
            else if (player1Value < player2Value)
            {
                GiveCards(player2Cards, player1Cards);
            }
        }

        private bool TieGame()
        {
            PrintOutCards();
            Console.WriteLine(player1Cards.FirstCardName + " ties with " + player2Cards.FirstCardName);
            Console.WriteLine("WAR");
            if (player2Cards.DeckCount < 4)
            {
                EndGameForTie(player1Name, player2Name);
                return true;
            }
            else if (player1Cards.DeckCount < 4)
            {
                EndGameForTie(player2Name, player1Name);
                return true;
            }
            else
            {
                if (warRulesThreeBool)
                {
                    Console.WriteLine("1...");
                    Console.WriteLine("2...");
                    Console.WriteLine("3...");
                    BurnThreeCards();
                }
                else
                {
                    Console.WriteLine("1...");
                    BurnOneCard();
                }
                int player1Value = player1Cards.FirstCardValue;
                int player2Value = player2Cards.FirstCardValue;
                if (player1Value == player2Value)
                {
                    roundCounter++;
                    Console.WriteLine("Round " + roundCounter);
                    if (TieGame())
                    {
                        return true;
                    }
                }
                else
                {
                    FlipCards(player1Value, player2Value);
                }

            }
            return false;
        }

        public string GetPlayerName()
        {
            bool nameFlag = true;
            Console.Write("Please enter a player name: ");
            string playerName = "";
            while (nameFlag)
            {
                playerName = Console.ReadLine();
                if (playerName.Equals("") && !(player1Name.Equals("Jason")))
                {
                    playerName = "Jason";
                    nameFlag = false;
                }
                else if (playerName.Equals(""))
                {
                    playerName = "Eric";
                    nameFlag = false;
                }
                else if (playerName.Length > 20)
                {
                    Console.Write("Sorry, that name is too long.\nPlease enter a name under 20 characters: ");
                }
                else
                {
                    nameFlag = false;
                }
            }
            return playerName;
        }

        private int rounds = 0;
        private void GetRounds()
        {
            Console.Write("How many rounds should be max: ");
            bool roundFlag = true;
            while (roundFlag)
            {
                string roundInput = Console.ReadLine();
                if (roundInput.Equals(""))
                {
                    rounds = MaxRounds;
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
                    catch (FormatException)
                    {
                        Console.Write("Opps!  That doesn't look like a number we can use.\nPlease enter a valid number: ");
                    }
                }
            }
        }

        private void PromptUserDefaultSettings()
        {
            bool defaultFlag = true;
            bool doWeWantDefaultFlag = true;
            Console.WriteLine("Default Settings:\nPlayer1 Name: Jason\nPlayer2 Name: Eric\nRound Limit: " + MaxRounds + "\nIf you enter nothing the options will go to default");
            Console.Write("Do you want default settings(Y/N): ");
            while (defaultFlag)
            {
                string defaultInput = Console.ReadLine();
                if (defaultInput.ToUpper().Equals("Y") || defaultInput.ToUpper().Equals(""))
                {
                    doWeWantDefaultFlag = true;
                    defaultFlag = false;
                    player1Name = "Jason";
                    player2Name = "Eric";
                    rounds = MaxRounds;
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
                player1Name = GetPlayerName();
                player2Name = GetPlayerName();
                GetRounds();
                PromptUserWarRules();
            }
        }

        private void PrintCardCount()
        {
            Console.WriteLine(player1Name + " Card Count: " + player1Cards.DeckCount);
            Console.WriteLine(player2Name + " Card Count: " + player2Cards.DeckCount);
        }
        private void PrintEndGameScore(string winnerName)
        {
            PrintCardCount();
            Console.WriteLine(winnerName + " Wins");
        }
        public void Play()
        {
            PromptUserDefaultSettings();
            HandOutCards();
            while (player1Cards.DeckCount > 0 && player2Cards.DeckCount > 0 && roundCounter < rounds)
            {
                roundCounter++;
                Console.WriteLine("Round " + roundCounter);
                int player1Value = player1Cards.FirstCardValue;
                int player2Value = player2Cards.FirstCardValue;
                if (player1Value == player2Value)
                {
                    if (TieGame())
                    {
                        break;
                    }
                }
                else
                {
                    FlipCards(player1Value, player2Value);
                }

            }
            if (roundCounter == rounds)
            {
                if (roundCounter == 1)
                    Console.WriteLine("We're at " + rounds + " turn... probably time to end the game.");
                else
                    Console.WriteLine("We're at " + rounds + " turns... probably time to end the game.");
            }
            if (player1Cards.DeckCount > player2Cards.DeckCount)
            {
                PrintEndGameScore(player1Name);
            }
            else if (player1Cards.DeckCount < player2Cards.DeckCount)
            {
                PrintEndGameScore(player2Name);
            }
            else
            {
                PrintCardCount();
                Console.WriteLine("The game ended in a tie");
            }
        }
    }
}
