using It_career_project.Data;
using It_career_project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace It_career_project.Business
{
    /// <summary>
    /// Business logic for the table Sales
    /// </summary>
    public class SaleController
    {
        private VideoGamePlatformContext context;

        /// <summary>
        /// Constructor used in Presentation layer
        /// </summary>
        public SaleController()
        {
            context = new VideoGamePlatformContext();
        }

        /// <summary>
        /// Constructer used in tests
        /// </summary>
        public SaleController(VideoGamePlatformContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Adds a sale after getting it's details
        /// </summary>
        public void AddSale(string gameTitle, int discount, DateTime startDate, DateTime endDate)
        {
            VideoGameController vgController = new VideoGameController(context);
            VideoGame videoGameOnSale = vgController.GetGameByTitle(gameTitle);

            if (discount <= 0)
            {
                throw new ArgumentException("Discount must be higher than 0%!");
            }

            if (discount >= 100)
            {
                throw new ArgumentException("Discount must be lower than 100%!");
            }

            if (startDate > endDate)
            {
                throw new ArgumentException("Start date cannot be after end date!");
            }

            if (endDate < DateTime.Now)
            {
                throw new ArgumentException("Date has already passed!");
            }

            Sale thereIsAlreadyASale = context.Sales.FirstOrDefault(x => x.GameId == videoGameOnSale.Id);

            if (thereIsAlreadyASale != null)
            {
                throw new ArgumentException("There is already a sale for this game!");
            }

            
            context
                .Sales
                .Add(new Sale(videoGameOnSale.Id, discount, startDate, endDate));

            context.SaveChanges();

            Sale sale = GetSaleByGameTitle(videoGameOnSale.GameTitle);

            MakeSaleDiscountEffective(sale);


            
            
        }

        /// <summary>
        /// Gets a sale with a given game title from the table Sales
        /// </summary>
        /// <param name="gameTitle">gameTitle is used to find the video game that the sale is for
        /// and get the object, to then take it's id to then get the Sale with that
        /// gameId</param>
        /// <returns>Sale with the given game</returns>
        public Sale GetSaleByGameTitle(string gameTitle)
        {
            VideoGameController vgController = new VideoGameController(context);
            VideoGame VideoGameWithSaidTitle = vgController.GetGameByTitle(gameTitle);

            Sale sale =
                context
                .Sales
                .FirstOrDefault(x => x.GameId == VideoGameWithSaidTitle.Id);

            if (sale == null)
            {
                throw new ArgumentException("There is no sale for this game!");
            }

            return sale;
        }

        /// <summary>
        /// Refreshes the current sales
        /// </summary>
        /// checks if the sales end dates have passed and removes them if that is the case
        public void RefreshSales()
        {
            List<Sale> outDatedSales =
               context
               .Sales
               .Where(x => x.EndDate < DateTime.Now)
               .ToList();

            if (outDatedSales.Count == 0)
            {
                return;
            }

            foreach (Sale outDatedSale in outDatedSales)
            {
                RevertGamePriceToBeforeSale(outDatedSale);
            }

            context
                .Sales
                .RemoveRange(outDatedSales);

            context.SaveChanges();
        }

        /// <summary>
        /// Takes a sale and changes the affected game's price to be after the discount
        /// </summary>
        /// <param name="sale">The sale which's game should be affected</param>
        public void MakeSaleDiscountEffective(Sale sale)
        {
            VideoGameController vgController = new VideoGameController(context);
            VideoGame videoGameToBeAffected = vgController.GetGameById(sale.GameId);

            videoGameToBeAffected.Price -= videoGameToBeAffected.Price * (sale.Discount / 100.0m);

            context.Update(videoGameToBeAffected);
            context.SaveChanges();
        }

        /// <summary>
        /// Reverts a games Price to before the sale
        /// </summary>
        /// <param name="sale">The sale that is now to be removed</param>
        public void RevertGamePriceToBeforeSale(Sale sale)
        {
            VideoGameController vgController = new VideoGameController(context);
            VideoGame videoGameToBeAffected = vgController.GetGameById(sale.GameId);

            videoGameToBeAffected.Price /= (1 - (sale.Discount / 100.0m));

            context.Update(videoGameToBeAffected);
            context.SaveChanges();
        }

        /// <summary>
        /// Removes a sale by getting it's gameTitle
        /// </summary>
        /// <param name="gameTitle"> Game title of the game which's sale should be removed</param>
        public void RemoveSale(string gameTitle)
        {
            VideoGameController vgController = new VideoGameController(context);
            List<Sale> allSalesOfAGame = context.Sales.ToList();

            if (allSalesOfAGame.Count == 0)
            {
                throw new ArgumentException("There are no ongoing sales!");
            }

            Sale saleToBeRemoved = GetSaleByGameTitle(gameTitle);


            context.Sales.Remove(saleToBeRemoved);
            context.SaveChanges();

            RevertGamePriceToBeforeSale(saleToBeRemoved);
        }

        /// <summary>
        /// Gets a sale by its game id
        /// </summary>
        /// <param name="gameId">the gameid of the sale </param>
        /// <returns></returns>
        public Sale GetSaleByGameId(int gameId)
        {
            Sale sale =
                context
                .Sales
                .FirstOrDefault(x => x.GameId == gameId);

            return sale;
        }

        public List<string> GetInfoOfAllSales()
        {
            VideoGameController vgController = new VideoGameController(context);
            List<Sale> allSalesOfAGame = context.Sales.ToList();

            if (allSalesOfAGame.Count == 0)
            {
                throw new ArgumentException("There are no ongoing sales!");
            }

            List<string> SaleInfo = new List<string>();

            foreach (Sale sale in allSalesOfAGame)
            {
                VideoGame currentVideoGame = vgController.GetGameById(sale.GameId);

                SaleInfo.Add($"{currentVideoGame.GameTitle} - {sale.Discount}% Sale, " +
                    $"Started: {sale.StartDate.Month}/{sale.StartDate.Day}/{sale.StartDate.Year}" +
                    $" Ends: {sale.EndDate.Month}/{sale.EndDate.Day}/{sale.EndDate.Year}");
            }

            return SaleInfo;
        }
    }
}