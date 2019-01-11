using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCS.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace CCS.Data
{
    public class SeedData
    {
        public static async void EnsurePopulate(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();

            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (!context.Agencies.Any())
            {
                UserManager<CCSUser> userManager = app.ApplicationServices.GetRequiredService<UserManager<CCSUser>>();
                RoleManager<IdentityRole> roleManger = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();

                await roleManger.CreateAsync(new IdentityRole("Admin"));
                CCSUser user = new CCSUser
                {
                    UserName = "admin",
                    FirstName = "Admini",
                    LastName = "Strator",
                };
                IdentityResult result = await userManager.CreateAsync(user, "longjohns");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }

                CCSUser nonAdmin = new CCSUser
                {
                    UserName = "test",
                    FirstName = "John",
                    LastName = "Doe",
                };
                await userManager.CreateAsync(nonAdmin, "abc123");             
                context.SaveChanges();


                context.Addresses.AddRange(
                    new Address
                    {
                        CityName = "Ogden",
                        StateShortName = "UT",
                        Zipcode = "84408",
                        StreetAddress1 = "123 Fake St.",
                        StreetAddress2 = "",
                    },
                    new Address
                    {
                        CityName = "Farmington",
                        StateShortName = "UT",
                        Zipcode = "84012",
                        StreetAddress1 = "567 Bogus Blvd",
                        StreetAddress2 = "",
                    },
                    new Address
                    {
                        CityName = "Bountiful",
                        StateShortName = "UT",
                        Zipcode = "84010",
                        StreetAddress1 = "Hwy 89, 500 S.",
                        StreetAddress2 = "",
                    });
                context.SaveChanges();

                context.Agencies.AddRange(
                    new Agency {
                        AddressID = 1,
                        AgencyName = "Walmart",
                        IsDonor = true,
                        IsActive = true,
                    },
                    new Agency
                    {
                        AddressID = 2,
                        AgencyName = "Harmons",
                        IsDonor = true,
                        IsActive = true,
                    },
                    new Agency
                    {
                        AddressID = 3,
                        AgencyName = "Costco",
                        IsDonor = true,
                        IsActive = true,
                    },
                    new Agency
                    {
                        AddressID = 1,
                        AgencyName = "Ogden Shelter",
                        IsDonor = false,
                        IsActive = true,
                    });
                context.SaveChanges();

                context.DonationTypes.AddRange(
                    new DonationType
                    {
                        Type = "Taxable"
                    },
                    new DonationType
                    {
                        Type = "In-Kind Taxable"
                    },
                    new DonationType
                    {
                        Type = "In-Kind Non-Taxable"
                    });
                context.SaveChanges();

                context.FoodCategories.AddRange(
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Regular,
                        Description = "Dry"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Regular,
                        Description = "Perishable"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Regular,
                        Description = "Non-Food"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Pantry,
                        Description = "Pantry Pack"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.USDA,
                        Description = "USDA-1"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.USDA,
                        Description = "USDA-2"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.USDA,
                        Description = "USDA-3"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.USDA,
                        Description = "USDA-4"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Grocery,
                        Description = "Bakery"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Grocery,
                        Description = "Dairy"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Grocery,
                        Description = "Produce"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Grocery,
                        Description = "Deli"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Grocery,
                        Description = "Meat"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Grocery,
                        Description = "Frozen (Non-Meat)"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Grocery,
                        Description = "Dry Grocery"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Grocery,
                        Description = "Non-Food"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Internal,
                        Description = "Beans"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Internal,
                        Description = "Bread"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Internal,
                        Description = "Dairy"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Internal,
                        Description = "Meat"
                    },
                    new FoodCategory
                    {
                        FoodCategoryType = FoodCategoryType.Internal,
                        Description = "Produce"
                    });
                context.SaveChanges();

                context.DonationTransactions.AddRange(
                    new DonationTransaction
                    {
                        AgencyID = 1,
                        TimeStamp = DateTime.Parse("November 13, 2018"),
                        TransactionType = TransactionTypeInOut.In,
                        FoodCategory = FoodCategoryType.Regular
                    },
                    new DonationTransaction
                    {
                        AgencyID = 2,
                        TimeStamp = DateTime.Parse("September 8, 2018"),
                        TransactionType = TransactionTypeInOut.In,
                        FoodCategory = FoodCategoryType.Regular
                    },
                    new DonationTransaction
                    {
                        AgencyID = 3,
                        TimeStamp = DateTime.Parse("September 20, 2018"),
                        TransactionType = TransactionTypeInOut.In,
                        FoodCategory = FoodCategoryType.Pantry
                    });
                context.SaveChanges();

                context.DonationTransactions.AddRange(
                    new DonationTransaction
                    {
                        AgencyID = 1,
                        TimeStamp = DateTime.Parse("November 2, 2018"),
                        TransactionType = TransactionTypeInOut.In,
                        FoodCategory = FoodCategoryType.Grocery
                    },
                    new DonationTransaction
                    {
                        AgencyID = 2,
                        TimeStamp = DateTime.Parse("October 20, 2018"),
                        TransactionType = TransactionTypeInOut.In,
                        FoodCategory = FoodCategoryType.Grocery
                    });
                context.SaveChanges();

                context.DonationTransactions.AddRange(
                    new DonationTransaction
                    {
                        AgencyID = 3,
                        TimeStamp = DateTime.Parse("June 20, 2018"),
                        TransactionType = TransactionTypeInOut.In,
                        FoodCategory = FoodCategoryType.USDA
                    },
                    new DonationTransaction
                    {
                        AgencyID = 1,
                        TimeStamp = DateTime.Parse("July 4, 2018"),
                        TransactionType = TransactionTypeInOut.In,
                        FoodCategory = FoodCategoryType.USDA
                    });
                context.SaveChanges();

                FoodCategory pantry = context.FoodCategories.FirstOrDefault(f => f.FoodCategoryType == FoodCategoryType.Pantry);
                ICollection<FoodCategory> usda = context.FoodCategories
                    .Where(f => f.FoodCategoryType == FoodCategoryType.USDA).ToList();
                ICollection<FoodCategory> grocery = context.FoodCategories
                    .Where(f => f.FoodCategoryType == FoodCategoryType.Grocery).ToList();
                ICollection<FoodCategory> regular = context.FoodCategories
                    .Where(f => f.FoodCategoryType == FoodCategoryType.Regular && f.Description != "Pantry Pack").ToList();

                foreach(var reg in regular.Select((value, i) => new { i, value }))
                {
                    context.TransactionLineItems.AddRange(
                        new TransactionLineItem
                        {
                            DonationTransactionID = 1,
                            FoodCategoryID = reg.value.FoodCategoryID,
                            DonationTypeID = 1,
                            Weight = 11
                        },
                        new TransactionLineItem
                        {
                            DonationTransactionID = 2,
                            FoodCategoryID = reg.value.FoodCategoryID,
                            DonationTypeID = 2,
                            Weight = 15
                        }
                    );
                }
                context.SaveChanges();

                context.TransactionLineItems.Add(
                    new TransactionLineItem
                    {
                        DonationTransactionID = 3,
                        FoodCategoryID = pantry.FoodCategoryID,
                        DonationTypeID = 3,
                        Weight = 20,
                        Quantity = 8
                    });
                context.SaveChanges();

                foreach (var gro in grocery.Select((value, i) => new { i, value }))
                {
                    context.TransactionLineItems.AddRange(
                        new TransactionLineItem
                        {
                            DonationTransactionID = 4,
                            FoodCategoryID = gro.value.FoodCategoryID,
                            DonationTypeID = 3,
                            Weight = 8
                        },
                        new TransactionLineItem
                        {
                            DonationTransactionID = 5,
                            FoodCategoryID = gro.value.FoodCategoryID,
                            DonationTypeID = 2,
                            Weight = 32
                        }
                    );
                }
                context.SaveChanges();

                foreach (var us in usda.Select((value, i) => new { i, value }))
                {
                    context.TransactionLineItems.AddRange(
                        new TransactionLineItem
                        {
                            DonationTransactionID = 6,
                            FoodCategoryID = us.value.FoodCategoryID,
                            DonationTypeID = 3,
                            Weight = 17,
                            Cases = 4
                        },
                        new TransactionLineItem
                        {
                            DonationTransactionID = 7,
                            FoodCategoryID = us.value.FoodCategoryID,
                            DonationTypeID = 1,
                            Weight = 21,
                            Cases = 10
                        }
                    );
                }
                context.SaveChanges();

                context.DonationTransactions.Add(
                    new DonationTransaction
                    {
                        AgencyID = 4,
                        TimeStamp = DateTime.Parse("November 20, 2018"),
                        TransactionType = TransactionTypeInOut.Out,
                        FoodCategory = FoodCategoryType.Pantry
                    }
                );

                context.TransactionLineItems.Add(
                    new TransactionLineItem
                    {
                        DonationTransactionID = 8,
                        FoodCategoryID = pantry.FoodCategoryID,
                        DonationTypeID = 3,
                        Weight = 100,
                        Quantity = 20
                    }
                );
                context.SaveChanges();

                context.DonationTransactions.Add(
                    new DonationTransaction
                    {
                        AgencyID = 1,
                        TimeStamp = DateTime.Parse("November 12, 2018"),
                        TransactionType = TransactionTypeInOut.In,
                        FoodCategory = FoodCategoryType.Grocery
                    }
                );
                context.SaveChanges();

                foreach (var gro in grocery.Select((value, i) => new { i, value }))
                {
                    context.TransactionLineItems.Add(
                        new TransactionLineItem
                        {
                            DonationTransactionID = 9,
                            FoodCategoryID = gro.value.FoodCategoryID,
                            DonationTypeID = 3,
                            Weight = 55
                        }
                    );
                }
                context.SaveChanges();


                context.Containers.Add(
                    new Container
                    {
                        FoodCategory = regular.FirstOrDefault(),
                        FoodCategoryID = regular.FirstOrDefault().FoodCategoryID,
                        Location = "PLACE 1",
                        Weight = 20,
                        BinNumber = 468,
                        Description = "FOOD ITEM 1",
                        DateCreated = DateTime.Parse("December 4, 2018")
                    });
                context.Containers.Add(
                    new Container
                    {
                        FoodCategory = pantry,
                        FoodCategoryID = pantry.FoodCategoryID,
                        Location = "PLACE 2",
                        Weight = 10,
                        BinNumber = 237,
                        Description = "FOOD ITEM 2",
                        DateCreated = DateTime.Parse("December 5, 2018")
                    });
                context.SaveChanges();
            }
        }
    }
}
