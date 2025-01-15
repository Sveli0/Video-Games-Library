using It_career_project.Data;
using It_career_project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace It_career_project.Business
{
    /// <summary>
    /// Business logic for the table GiftCards
    /// </summary>
    public class GiftCardController
    {
        private VideoGamePlatformContext context;

        /// <summary>
        /// Constructor used in Presentation layer
        /// </summary>
        public GiftCardController()
        {
            context = new VideoGamePlatformContext();
        }

        /// <summary>
        /// Constructer used in tests
        /// </summary>
        public GiftCardController(VideoGamePlatformContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Generates 1000 random gift cards
        /// </summary>
        /// Generates 1000 random gift cards with values between 5-20 euro and adds them to the database
        public void GenerateRandomGiftCards()
        {
            string symbaAssortmentString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char[] SymbolAssortment = symbaAssortmentString.ToCharArray();
            int uniqueSymbols = SymbolAssortment.Length;
            List<GiftCard> newGiftCards = new List<GiftCard>();

            decimal[] PossibleValues = { 5.00m, 10.00m, 15.00m, 20.00m };

            Random r = new Random();

            List<GiftCard> removeAll =
                context
                .GiftCards
                .ToList();

            context.GiftCards.RemoveRange(removeAll);
            context.SaveChanges();

            List<string> alreadyUsed = new List<string>();

            for (int i = 0; i < 1000; i++)
            {
                char c1 = SymbolAssortment[r.Next(0, uniqueSymbols)];
                char c2 = SymbolAssortment[r.Next(0, uniqueSymbols)];
                char c3 = SymbolAssortment[r.Next(0, uniqueSymbols)];
                char c4 = SymbolAssortment[r.Next(0, uniqueSymbols)];
                char c5 = SymbolAssortment[r.Next(0, uniqueSymbols)];
                char c6 = SymbolAssortment[r.Next(0, uniqueSymbols)];
                char c7 = SymbolAssortment[r.Next(0, uniqueSymbols)];
                char c8 = SymbolAssortment[r.Next(0, uniqueSymbols)];
                char c9 = SymbolAssortment[r.Next(0, uniqueSymbols)];
                char c10 = SymbolAssortment[r.Next(0, uniqueSymbols)];
                char c11 = SymbolAssortment[r.Next(0, uniqueSymbols)];
                char c12 = SymbolAssortment[r.Next(0, uniqueSymbols)];
                char c13 = SymbolAssortment[r.Next(0, uniqueSymbols)];
                char c14 = SymbolAssortment[r.Next(0, uniqueSymbols)];
                char c15 = SymbolAssortment[r.Next(0, uniqueSymbols)];
                char c16 = SymbolAssortment[r.Next(0, uniqueSymbols)];

                string code = $"{c1}{c2}{c3}{c4}-" +
                    $"{c5}{c6}{c7}{c8}-" +
                    $"{c9}{c10}{c11}{c12}-" +
                    $"{c13}{c14}{c15}{c16}";

                if (alreadyUsed.Contains(code))
                {
                    i--;
                }
                else
                {
                    decimal value = PossibleValues[r.Next(0, 4)];
                    GiftCard card = new GiftCard(code, value);

                    newGiftCards.Add(card);
                    alreadyUsed.Add(code);
                }
            }

            context.GiftCards.AddRange(newGiftCards);
            context.SaveChanges();
        }

        /// <summary>
        /// Redeems a gift card by removing it and returing it's value in euro
        /// </summary>
        /// <param name="code"></param>
        /// <returns>The gift cards value in euro</returns>
        public decimal RedeemGiftCard(string code)
        {
            GiftCard card = GetCardByCode(code);

            context
                .GiftCards
                .Remove(card);

            context.SaveChanges();

            return card.Value;
        }

        /// <summary>
        /// Gets a card by it's code
        /// </summary>
        /// <param name="code">the code of the gift card which is checked to find it</param>
        /// <returns>An object from the class GiftCard</returns>
        public GiftCard GetCardByCode(string code)
        {
            GiftCard card =
                context
                .GiftCards
                .FirstOrDefault(x => x.Code == code);

            if (card == null)
            {
                throw new ArgumentException("Card already redeemed, doesn't exist or is expired!");
            }

            return card;
        }
    }
}