using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class Game
    {
        private List<Card> userHand;
        private List<Card> dealerHand;
        private Random random;

        public Game()
        {
            random = new Random();
        }

        public void Start()
        {
            userHand = DealInitialHand();
            int userScore = CalculateScore(userHand);

            // User's turn
            while (userScore < 21)
            {
                Game.DisplayHand("Your", userHand, userScore);

                Console.WriteLine("Do you want to draw another card? (yes/no)");
                string input = Console.ReadLine()?.ToLower();

                if (input == "yes")
                {
                    userHand.Add(DrawCard());
                    userScore = CalculateScore(userHand);

                    if (userScore > 21)
                    {
                        Game.DisplayHand("Your", userHand, userScore);
                        Console.WriteLine("Bust! You lose.");
                        return;
                    }

                    if (userScore == 21)
                    {
                        Console.WriteLine("Blackjack! You win!");
                        return;
                    }
                }
                else
                {
                    break;
                }
            }

            // Dealer's turn
            dealerHand = new List<Card>();
            int dealerScore = 0;

            while (dealerScore < 16)
            {
                dealerHand.Add(DrawCard());
                dealerScore = CalculateScore(dealerHand);
                Game.DisplayHand("Dealer's", dealerHand, dealerScore);

                if (dealerScore > 21)
                {
                    Console.WriteLine("Dealer busts! You win!");
                    return;
                }

                if (dealerScore == 21)
                {
                    Console.WriteLine("Dealer hits Blackjack! Dealer wins!");
                    return;
                }
            }

            // Determine the winner
            if (userScore > dealerScore)
            {
                Console.WriteLine("You win with a higher score!");
            }
            else if (dealerScore > userScore)
            {
                Console.WriteLine("Dealer wins with a higher score!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
        }

        private List<Card> DealInitialHand()
        {
            return new List<Card> { DrawCard(), DrawCard() };
        }

        private Card DrawCard()
        {
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            string suit = suits[random.Next(suits.Length)];
            string rank = ranks[random.Next(ranks.Length)];

            return new Card(rank, suit);
        }

        private int CalculateScore(List<Card> hand)
        {
            int score = 0;
            int aceCount = 0;

            foreach (var card in hand)
            {
                if (int.TryParse(card.Rank, out int value))
                {
                    score += value;
                }
                else if (card.Rank == "Ace")
                {
                    score += 11;
                    aceCount++;
                }
                else
                {
                    score += 10;
                }
            }

            while (score > 21 && aceCount > 0)
            {
                score -= 10;
                aceCount--;
            }

            return score;
        }

        private static void DisplayHand(string owner, List<Card> hand, int score)
        {
            Console.WriteLine($"{owner} hand:");
            foreach (var card in hand)
            {
                Console.WriteLine(card.ToString());
            }
            Console.WriteLine($"{owner} total score: {score}\n");
        }
    }
}
