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
                    ProductName = "1968 Chevrolet Corvette Convertible",
                    ActualAmount = "5000",
                    Description = "This third-generation 1968 Chevrolet Corvette Convertible has just came out a long-term ownership and is finished in its factory color LeMans Blue (976) combined with a black interior. This patinated C3 is equipped with a 4-speed manual transmission with a Hurst shifter, V8 engine, Street Demon carburetor, four-wheel disc brakes, dual exhaust outlets, concealed headlights, chrome trim, three-spoke steering wheel, quadruple tail lights, rear bumper integrated spoiler, black convertible soft top",
                    ProductImagePath = "1968-Chevrolet-Corvette-Convertible.jpg",
                    BidList = new List<Bid>()
                    {
                        new Bid()
                        {
                            BidPrice = "5500",
                            Bidder = "macsherano20",
                        },

                        new Bid()
                        {
                            BidPrice = "5550",
                            Bidder = "codeine-crazy",
                        },

                        new Bid()
                        {
                            BidPrice = "6000",
                            Bidder = "alexis sanchez",
                        }

                    }
                },

                new Product()
                {
                    ProductName = "1958 Chevy Covette",
                    ActualAmount = "2000",
                    ProductImagePath = "chevycovette-1958.jpg",
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
                    ProductName = "1965 alfa romeo gulia 1600 ",
                    ActualAmount = "2000",
                    Description = "Alfa Romeo, with a new twin-cam, four-cylinder engine design. Sporting elegant coachwork designed and built by Pininfarina, the 1.3-litre Giulietta Spider ",
                    ProductImagePath = "alfa-romeo-gulia-1600 1965.jpg",
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
