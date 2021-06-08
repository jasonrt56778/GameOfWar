using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfWar
{
    class GameOfWar
    {
        //DeckHolders are created for both players
        private DeckHolder player1Cards = new DeckHolder();
        private DeckHolder player2Cards = new DeckHolder();

        //Queue is created for leftover cards from war
        private Queue<Card> playerWarCards = new Queue<Card>();

        //Players name variables are added to be access from anywhere in the class
        private string player1Name = "";
        private string player2Name = "";

        //Rounder counter added to keep track of how many rounds have happened
        private int roundCounter = 0;

        //Made constants for the default max rounds and the deck length
        private const int DefaultMaxRounds = 500;
        private const int DeckCount = 52;

        //Hands out cards starting with player1 then player2 and so on
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
        //Gives all the cards to the winner when the game is finished
        private void GiveAllCardsToWinner(DeckHolder winner, DeckHolder loser)
        {
            while (loser.DeckCount > 0)
            {
                winner.Deck.Enqueue(loser.Deck.Dequeue());
            }
            int playerWarCardsLength = playerWarCards.Count;
            for (int j = 0; j < playerWarCardsLength; j++)
            {
                winner.Deck.Enqueue(playerWarCards.Dequeue());
            }
        }

        //When loser runs out of cards during war, give all the cards to the winner and end game
        private void EndGameForTie(string winnerName, string loserName)
        {
            Console.WriteLine("Giving all cards to " + winnerName + " since " + loserName + " ran out during war.");
            if (player1Cards.DeckCount > player2Cards.DeckCount)
            {
                GiveAllCardsToWinner(player1Cards, player2Cards);
            }
            else
            {
                GiveAllCardsToWinner(player2Cards, player1Cards);
            }
            Console.WriteLine("Game Ending");
        }

        //In the event of a tie, the user chose to burn three cards (depending on user choice)
        private void BurnThreeCards()
        {
            for (int i = 0; i < 3; i++)
            {
                BurnOneCard();
            }
        }

        //In the event of a tie, the user chose to burn one cards (depending on user choice)
        private void BurnOneCard()
        {
            playerWarCards.Enqueue(player1Cards.Deck.Dequeue());
            playerWarCards.Enqueue(player2Cards.Deck.Dequeue());
        }

        //Prompts the user if during the event of a tie if they want to burn one card or three then flip
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

        //When the game is over, print out the winner
        private void PrintOutWinner(DeckHolder winnerDeck, DeckHolder loserDeck)
        {
            PrintOutCards();
            Console.WriteLine(winnerDeck.FirstCardName + " beats " + loserDeck.FirstCardName + "\n");
        }

        //Print out the amount of cards each player has
        private void PrintOutCards()
        {
            Console.WriteLine(player1Name + ": " + player1Cards.FirstCardName);
            Console.WriteLine(player2Name + ": " + player2Cards.FirstCardName);
        }

        //When a player wins a hand, give back their card and take their opponents card
        //If there was a tie previously, give all the cards the players burned to the winner
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

        //Each player flips their cards and the player with the better card takes both
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

        //In the event of a tie game, if one player has less than 4 cards or 2 cards depending on user input, give the rest to the other player
        //Otherwise, burn 3 cards or burn 1 card and flip, if a player wins, give them all cards in the pot, if they tie again, do it again
        private bool TieGame()
        {
            PrintOutCards();
            Console.WriteLine(player1Cards.FirstCardName + " ties with " + player2Cards.FirstCardName);
            Console.WriteLine("WAR");
            if (player2Cards.DeckCount < 4 && warRulesThreeBool || !warRulesThreeBool && player2Cards.DeckCount < 2)
            {
                EndGameForTie(player1Name, player2Name);
                return true;
            }
            else if (player1Cards.DeckCount < 4 || !warRulesThreeBool && player1Cards.DeckCount < 2)
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

        //Gets the name of the player if the user does not want default options
        //If the user inputs nothing then the players names will go to default (Jason & Eric)
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

        //Gets the max amount of rounds the players want to play
        //If nothing is entered it goes to default value (500)
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
                    rounds = DefaultMaxRounds;
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

        //Prompts the user if they want to use the default settings or their own
        private void PromptUserDefaultSettings()
        {
            bool defaultFlag = true;
            bool doWeWantDefaultFlag = true;
            Console.WriteLine("Default Settings:\n" +
                "Player1 Name: Jason\nPlayer2 Name: Eric\n" +
                "Round Limit: " + DefaultMaxRounds + "\n" +
                "WAR will drop 3 cards and flip the 4th\n" +
                "If you enter nothing the options will go to default");
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
                    rounds = DefaultMaxRounds;
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

        //Prints out the amount of cards each player has
        private void PrintCardCount()
        {
            Console.WriteLine(player1Name + " Card Count: " + player1Cards.DeckCount);
            Console.WriteLine(player2Name + " Card Count: " + player2Cards.DeckCount);
        }

        //Prints out the amount of cards each player has and who won
        private void PrintEndGameScore(string winnerName)
        {
            PrintCardCount();
            Console.WriteLine(winnerName + " Wins");
        }

        //This is where most of the code is accessed.
        //This is the method that runs the game of war
        public void Play()
        {
            PromptUserDefaultSettings();
            HandOutCards();
            //making sure both players still have cards and that it has not gone over the round limit
            while (player1Cards.DeckCount > 0 && player2Cards.DeckCount > 0 && roundCounter < rounds)
            {
                roundCounter++;
                Console.WriteLine("Round " + roundCounter);
                int player1Value = player1Cards.FirstCardValue;
                int player2Value = player2Cards.FirstCardValue;
                //if its a tie game, go to the tie game method
                if (player1Value == player2Value)
                {
                    if (TieGame())
                    {
                        break;
                    }
                }
                //if not a tie game, flip the cards
                else
                {
                    FlipCards(player1Value, player2Value);
                }

            }
            //if we reached max rounds, end game
            if (roundCounter == rounds)
            {
                if (roundCounter == 1)
                    Console.WriteLine("We're at " + rounds + " turn... probably time to end the game.");
                else
                    Console.WriteLine("We're at " + rounds + " turns... probably time to end the game.");
            }
            //Print end game scores
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
