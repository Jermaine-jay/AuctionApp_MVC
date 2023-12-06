using AunctionApp.DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AunctionApp.DAL.Database
{
    public static class DataAccess
    {
        public static async Task EnsurePopulatedAsync(this IApplicationBuilder app)
        {
            AunctionAppDbContext context = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<AunctionAppDbContext>();

            context.Database.EnsureCreated();
            var productExist = await context.Products.AnyAsync();

            if (!productExist)
            {
                await context.Products.AddRangeAsync(await ProductsWithBids());
                await context.SaveChangesAsync();
            }
        }


        private static async Task<IEnumerable<Product>> ProductsWithBids()
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
                },

                new Product()
                {
                    ProductName = "1970 Bentley T1",
                    ActualAmount = "5000",
                    Description = "This right-hand drive 1970 Bentley T1 is available in this gorgeous color combination of silver with a black interior. " +
                    "The vehicle comes equipped with an automatic transmission, air conditioning, power windows, power steering, steel wheels, chrome bumpers, and white line tires." +
                    "A very presentable Bentley that is mechanically sound.",
                    ProductImagePath = "1970 Bentley T1.jpg",
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
                },

                new Product()
                {
                    ProductName = "1986 Alfa Romeo Spider Veloce",
                    ActualAmount = "10000",
                    Description = "Presenting this 1986 Alfa Romeo Spider Veloce that is finished in a captivating triple-black color scheme. Equipped with a 5-speed manual transmission, inline-four cylinder engine, fuel injection, four-wheel disc brakes, single exhaust outlet, Jaeger instruments, black convertible soft top, three-spoke steering wheel, \"Pininfarina\" badging, Sylvania Halogen headlights, Sumitomo tires, FPS alloy wheels, and a full-size spare tire fitted in the trunk. Amenities include power windows, forward-folding front seats, dual-side mirrors, glove box, cigar lighter with an ashtray, door pockets, and sun visors with a vanity mirror on the passenger side.",
                    ProductImagePath = "1986 Alfa Romeo Spider Veloce.jpg",
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
                },


                new Product()
                {
                    ProductName = "1982 Aston Martin V8 Volante",
                    ActualAmount = "12000",
                    Description = "Presenting this left-hand-drive 1982 Aston Martin V8 Volante that is finished in a color scheme of Canterbury Blue complemented over a tan interior with Dark Blue piping and Blue carpets. Equipped with 3-speed automatic transmission, V8 engine, front-wheel disc brakes, dual exhaust outlets, Smiths instruments, VDO temperature gauge, option dash pad, power operated convertible soft top, side marker lights, three-spoke steering wheel, \"Volante\" badging, alloy wheels with Goodyear tires, jack, tool roll, and a full-size spare tire fitted in the trunk",
                    ProductImagePath = "1982 Aston Martin V8 Volante.jpg",
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
                },


                new Product()
                {
                    ProductName = "1994 Porsche 964 Carrera 4 Wide-Body Coupe",
                    ActualAmount = "19000",
                    Description = "Presenting this amazing and highly collectible 1994 Porsche 964 Carrera 4 Wide-Body Coupe (1 of 267 ever produced). Available in Guards Red with a black interior. The vehicle comes equipped with a 5-speed manual transmission, Flat 6 Cylinder 3.6-liter engine, automatic speed control, air conditioning, power windows, power steering, sunroof, 4-wheel disc brakes, and spare tir ",
                    ProductImagePath = "1994 Porsche 964 Carrera 4.jpg",
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
                },


                new Product()
                {
                    ProductName = "1994 Porsche 964 Carrera 4 Wide-Body Coupe",
                    ActualAmount = "19000",
                    Description = "Presenting this amazing and highly collectible 1994 Porsche 964 Carrera 4 Wide-Body Coupe (1 of 267 ever produced). Available in Guards Red with a black interior. The vehicle comes equipped with a 5-speed manual transmission, Flat 6 Cylinder 3.6-liter engine, automatic speed control, air conditioning, power windows, power steering, sunroof, 4-wheel disc brakes, and spare tir ",
                    ProductImagePath = "1994 Porsche 964 Carrera 4.jpg",
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
                },

                new Product()
                {
                    ProductName = "1994 Porsche 964 Carrera 4 Wide-Body Coupe",
                    ActualAmount = "19000",
                    Description = "Presenting this amazing and highly collectible 1994 Porsche 964 Carrera 4 Wide-Body Coupe (1 of 267 ever produced). Available in Guards Red with a black interior. The vehicle comes equipped with a 5-speed manual transmission, Flat 6 Cylinder 3.6-liter engine, automatic speed control, air conditioning, power windows, power steering, sunroof, 4-wheel disc brakes, and spare tir ",
                    ProductImagePath = "1994 Porsche 964 Carrera 4.jpg",
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
                },

                new Product()
                {
                    ProductName = "1982 Aston Martin V8 Volante",
                    ActualAmount = "12000",
                    Description = "Presenting this left-hand-drive 1982 Aston Martin V8 Volante that is finished in a color scheme of Canterbury Blue complemented over a tan interior with Dark Blue piping and Blue carpets. Equipped with 3-speed automatic transmission, V8 engine, front-wheel disc brakes, dual exhaust outlets, Smiths instruments, VDO temperature gauge, option dash pad, power operated convertible soft top, side marker lights, three-spoke steering wheel, \"Volante\" badging, alloy wheels with Goodyear tires, jack, tool roll, and a full-size spare tire fitted in the trunk",
                    ProductImagePath = "1982 Aston Martin V8 Volante.jpg",
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
                },

                new Product()
                {
                    ProductName = "1982 Aston Martin V8 Volante",
                    ActualAmount = "12000",
                    Description = "Presenting this left-hand-drive 1982 Aston Martin V8 Volante that is finished in a color scheme of Canterbury Blue complemented over a tan interior with Dark Blue piping and Blue carpets. Equipped with 3-speed automatic transmission, V8 engine, front-wheel disc brakes, dual exhaust outlets, Smiths instruments, VDO temperature gauge, option dash pad, power operated convertible soft top, side marker lights, three-spoke steering wheel, \"Volante\" badging, alloy wheels with Goodyear tires, jack, tool roll, and a full-size spare tire fitted in the trunk",
                    ProductImagePath = "1982 Aston Martin V8 Volante.jpg",
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
                },

                new Product()
                {
                    ProductName = "1982 Aston Martin V8 Volante",
                    ActualAmount = "12000",
                    Description = "Presenting this left-hand-drive 1982 Aston Martin V8 Volante that is finished in a color scheme of Canterbury Blue complemented over a tan interior with Dark Blue piping and Blue carpets. Equipped with 3-speed automatic transmission, V8 engine, front-wheel disc brakes, dual exhaust outlets, Smiths instruments, VDO temperature gauge, option dash pad, power operated convertible soft top, side marker lights, three-spoke steering wheel, \"Volante\" badging, alloy wheels with Goodyear tires, jack, tool roll, and a full-size spare tire fitted in the trunk",
                    ProductImagePath = "1982 Aston Martin V8 Volante.jpg",
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
                },

                new Product()
                {
                    ProductName = "1986 Alfa Romeo Spider Veloce",
                    ActualAmount = "10000",
                    Description = "Presenting this 1986 Alfa Romeo Spider Veloce that is finished in a captivating triple-black color scheme. Equipped with a 5-speed manual transmission, inline-four cylinder engine, fuel injection, four-wheel disc brakes, single exhaust outlet, Jaeger instruments, black convertible soft top, three-spoke steering wheel, \"Pininfarina\" badging, Sylvania Halogen headlights, Sumitomo tires, FPS alloy wheels, and a full-size spare tire fitted in the trunk. Amenities include power windows, forward-folding front seats, dual-side mirrors, glove box, cigar lighter with an ashtray, door pockets, and sun visors with a vanity mirror on the passenger side.",
                    ProductImagePath = "1986 Alfa Romeo Spider Veloce.jpg",
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
                },

                new Product()
                {
                    ProductName = "1986 Alfa Romeo Spider Veloce",
                    ActualAmount = "10000",
                    Description = "Presenting this 1986 Alfa Romeo Spider Veloce that is finished in a captivating triple-black color scheme. Equipped with a 5-speed manual transmission, inline-four cylinder engine, fuel injection, four-wheel disc brakes, single exhaust outlet, Jaeger instruments, black convertible soft top, three-spoke steering wheel, \"Pininfarina\" badging, Sylvania Halogen headlights, Sumitomo tires, FPS alloy wheels, and a full-size spare tire fitted in the trunk. Amenities include power windows, forward-folding front seats, dual-side mirrors, glove box, cigar lighter with an ashtray, door pockets, and sun visors with a vanity mirror on the passenger side.",
                    ProductImagePath = "1986 Alfa Romeo Spider Veloce.jpg",
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
                },

                new Product()
                {
                    ProductName = "1986 Alfa Romeo Spider Veloce",
                    ActualAmount = "10000",
                    Description = "Presenting this 1986 Alfa Romeo Spider Veloce that is finished in a captivating triple-black color scheme. Equipped with a 5-speed manual transmission, inline-four cylinder engine, fuel injection, four-wheel disc brakes, single exhaust outlet, Jaeger instruments, black convertible soft top, three-spoke steering wheel, \"Pininfarina\" badging, Sylvania Halogen headlights, Sumitomo tires, FPS alloy wheels, and a full-size spare tire fitted in the trunk. Amenities include power windows, forward-folding front seats, dual-side mirrors, glove box, cigar lighter with an ashtray, door pockets, and sun visors with a vanity mirror on the passenger side.",
                    ProductImagePath = "1986 Alfa Romeo Spider Veloce.jpg",
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
                },

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
                },
            };
        }
    }
}
