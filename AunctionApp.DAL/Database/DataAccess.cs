using AunctionApp.DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AunctionApp.DAL.Database
{
    public class DataAccess
    {
        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            AunctionAppDbContext context = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<AunctionAppDbContext>();

            if (!await context.Products.AnyAsync())
            {
                await context.Products.AddRangeAsync(ProductsWithBids());
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<Product> ProductsWithBids()
        {
            return new List<Product>()
            {
                new Product()
                {
                    ProductName = "English Bronze Of An Eel And Heron",
                    ActualAmount = "1400",
                    ProductImagePath = "~/imges/Eelandheron.jpg",
                    Description = "Lady Fanny Maitland (english Fl. 1870s): A Unique English Bronze Of An Eel And Heron In Mortal Combat",
                    BidList = new List<Bid>()
                    {
                        new Bid()
                        {
                            BidPrice = "1500",
                            Bidder = "germaine-jay",
                        },

                        new Bid()
                        {
                            BidPrice = "1550",
                            Bidder = "Odogwu-jay",
                        },

                        new Bid()
                        {
                            BidPrice = "1700",
                            Bidder = "germaine-jay",
                        }

                    }
                },

                new Product()
                {
                    ProductName = "1958 Chevy Covette",
                    ActualAmount = "2000",
                    ProductImagePath = "~/imges/chevycovette-1958.jpg",
                    Description = "The vehicle comes equipped with a 4-speed manual transmission, 283 V8 engine, steel wheels, knock-off hub caps, hard top, and white wall tires.",
                    BidList = new List<Bid>()
                    {
                        new Bid()
                        {
                            BidPrice = "2500",
                            Bidder = "johnson-jay",
                        },

                        new Bid()
                        {
                            BidPrice = "2550",
                            Bidder = "chief-jay",
                        },

                        new Bid()
                        {
                            BidPrice = "2700",
                            Bidder = "igwe-jay",
                        }

                    }
                },

                 new Product()
                {
                    ProductName = "alfa romeo gulia 1600 1965.jpg",
                    ActualAmount = "2000",
                    Description = "Alfa Romeo, with a new twin-cam, four-cylinder engine design. Sporting elegant coachwork designed and built by Pininfarina, the 1.3-litre Giulietta Spider ",
                    ProductImagePath = "~/imges/alfa-romeo-gulia-1600 1965.jpg",
                    BidList = new List<Bid>()
                    {
                        new Bid()
                        {
                            BidPrice = "2500",
                            Bidder = "macsherano20",
                        },

                        new Bid()
                        {
                            BidPrice = "2550",
                            Bidder = "codeine-crazy",
                        },

                        new Bid()
                        {
                            BidPrice = "2700",
                            Bidder = "alexis sanchez",
                        }

                    }
                }
            };
        }
    }
}
