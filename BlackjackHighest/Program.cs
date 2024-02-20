using System;
using System.Collections.Generic;

public class Program
{
    /*PROBLEM
     * have the function BlackjackHighest(strArr) take the strArr parameter being passed which will
     * be an array of numbers and letters representing blackjack cards. Numbers in the array will be written out.
     * So for example strArr may be ["two","three","ace","king"]. The full list of possibilities for strArr is: two, three,
     * four, five, six, seven, eight, nine, ten, jack, queen, king, ace. Your program should output below, above,
     * or blackjack signifying if you have blackjack (numbers add up to 21) or not and the highest card in your hand in
     * relation to whether or not you have blackjack. If the array contains an ace but your hand will go above 21, you must
     * count the ace as a 1. You must always try and stay below the 21 mark. So using the array mentioned above, the output
     * should be below king. The ace is counted as a 1 in this example because if it wasn't you would be above the 21 mark.
     * Another example would be if strArr was ["four","ten","king"], the output here should be above king. If you have a
     * tie between a ten and a face card in your hand, return the face card as the "highest card". If you have multiple
     * face cards, the order of importance is jack, queen, then king.
     */

    /*SOLUTION
     * Create two dictionaries: cardValues and cardOrder. cardValues maps card names to their respective values in the game of Blackjack.
     * cardOrder maps card names to their order in terms of rank, with the ace being the highest.
     * Initialize variables: total to store the total value of the hand, aceCount to store the number of aces in the hand, and 
     * highestCard to store the name of the highest card in the hand.
     * Iterate through each card in the input array strArr:
     *      Add the value of the current card to the total.
     *      If the current card is an ace, increment the aceCount.
     *      If the current card has a higher rank than the current highestCard, update the highestCard to the current card.
     * After iterating through all the cards, check if there are any aces in the hand and if the total value is greater than 21. If so, enter a while loop:
     *      Subtract 10 from the total value for each ace in the hand, as long as the total value remains greater than 21. This is done to account for 
     *      the fact that aces can be counted as either 1 or 11 in Blackjack.
     *      Decrement the aceCount for each ace processed in the loop.
     * After processing the aces, check if all aces have been counted as 1 and if the highestCard is still an ace. If so, iterate 
     * through the hand again to find the highest card that is not an ace and update the highestCard.
     * Determine the final result based on the total value of the hand:
     *      If the total value is exactly 21, return "blackjack " + highestCard.
     *      If the total value is less than 21, return "below " + highestCard.
     *      If the total value is greater than 21, return "above " + highestCard.
     * The final result is printed to the console in the Main method for each test case.
     */

    /*PSEUDOCODE
     * Create a dictionary cardValues that maps card names to their values in Blackjack
     * Create a dictionary cardOrder that maps card names to their order in terms of rank

     *Initialize total to 0
     *Initialize aceCount to 0
     *Initialize highestCard to an empty string

     *For each card in the input array:
         *Add the value of the card to total
         *If the card is an ace, increment aceCount
         *If the card has a higher rank than highestCard, update highestCard to the card

     *After iterating through all the cards:
         *While aceCount is greater than 0 and total is greater than 21:
             *Subtract 10 from total
             *Decrement aceCount

         *If aceCount is 0 and highestCard is an ace:
             *For each card in the input array:
                 *If the card is not an ace and has a higher value than the current highestCard:
                     *Update highestCard to the card

     *If total is exactly 21:
         *Return "blackjack " + highestCard
     *Else if total is less than 21:
         *Return "below " + highestCard
     *Else:
         *Return "above " + highestCard
     */

    public static string BlackjackHighest(string[] strArr)
    {
        Dictionary<string, int> cardValues = new Dictionary<string, int>
        {
            {"two", 2}, {"three", 3}, {"four", 4}, {"five", 5}, {"six", 6}, {"seven", 7}, {"eight", 8}, {"nine", 9}, {"ten", 10},
            {"jack", 10}, {"queen", 10}, {"king", 10}, {"ace", 11}
        };

        Dictionary<string, int> cardOrder = new Dictionary<string, int>
        {
            {"two", 2}, {"three", 3}, {"four", 4}, {"five", 5}, {"six", 6}, {"seven", 7}, {"eight", 8}, {"nine", 9}, {"ten", 10},
            {"jack", 11}, {"queen", 12}, {"king", 13}, {"ace", 14}
        };

        int total = 0;
        int aceCount = 0;
        string highestCard = "";

        foreach (string card in strArr)
        {
            int cardValue = cardValues[card];
            total += cardValue;

            if (card == "ace")
            {
                aceCount++;
            }

            if (highestCard == "" || cardOrder[card] > cardOrder[highestCard])
            {
                highestCard = card;
            }
        }

        while (aceCount > 0 && total > 21)
        {
            total -= 10;
            aceCount--;
        }

        if (aceCount == 0 && highestCard == "ace")
        {
            int highestValue = 0;
            foreach (string card in strArr)
            {
                if (card != "ace" && cardValues[card] > highestValue)
                {
                    highestValue = cardValues[card];
                    highestCard = card;
                }
            }
        }

        if (total == 21)
        {
            return "blackjack " + highestCard;
        }
        else if (total < 21)
        {
            return "below " + highestCard;
        }
        else
        {
            return "above " + highestCard;
        }
    }


    static void Main(string[] args)
    {
        TestBlackjackHighest(new String[] { "jack", "ace" }, "blackjack ace");
        TestBlackjackHighest(new String[] { "ace", "queen" }, "blackjack ace");
        TestBlackjackHighest(new String[] { "ace", "ace", "nine" }, "blackjack ace");
        TestBlackjackHighest(new String[] { "four", "ten", "king" }, "above king");
        TestBlackjackHighest(new String[] { "ace", "ace", "nine" }, "blackjack ace");
        TestBlackjackHighest(new String[] { "two", "three", "ace", "ace" }, "below ace");
    }

    static void TestBlackjackHighest(string[] cards, string expected)
    {
        string result = BlackjackHighest(cards);
        Console.WriteLine($"Cards: [{string.Join(", ", cards)}], Result: {result}, Expected: {expected}");
        Console.WriteLine(result == expected ? "Test Passed" : "Test Failed");
        Console.WriteLine();
    }
}