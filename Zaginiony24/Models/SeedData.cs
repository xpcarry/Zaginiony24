using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Zaginiony24.Models
{
    public static class SeedData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                if (!context.Users.Any())
                {
                    var users = new List<AppUser>()
                    {
                        new AppUser
                        {
                            Email = "admin@zaginiony24.pl",
                            EmailConfirmed = true,
                            PhoneNumber = "602212512",
                            UserName = "admin",
                            Name = "Bartek",
                            Surname = "Losowy",
                            DateJoined = DateTime.Now,
                            IsActive = true
                        },
                        new AppUser
                        {

                            Email = "user@zaginiony24.pl",
                            EmailConfirmed = true,
                            PhoneNumber = "632398126",
                            UserName = "testowyUser",
                            Name = "Robert",
                            Surname = "Smolinski",
                            DateJoined = DateTime.Now,
                            IsActive = true
                        },
                        new AppUser
                        {
                            Email = "andzelika@zaginiony24.pl",
                            EmailConfirmed = true,
                            PhoneNumber = "524936355",
                            UserName = "ruczaja",
                            Name = "Andzelika",
                            Surname = "Ruczaj",
                            DateJoined = DateTime.Now,
                            IsActive = true
                        },
                        new AppUser
                        {
                            Email = "franciszek995@zaginiony24.pl",
                            EmailConfirmed = true,
                            PhoneNumber = "668745294",
                            UserName = "majfranciszek",
                            Name = "Franciszek",
                            Surname = "Maj",
                            DateJoined = DateTime.Now
                        },
                        new AppUser
                        {
                            Email = "wolna.alicja@zaginiony24.pl",
                            EmailConfirmed = true,
                            PhoneNumber = "576933090",
                            UserName = "alicjaw",
                            Name = "Alicja",
                            Surname = "Wolna",
                            DateJoined = DateTime.Now,
                            IsActive = true
                        },
                        new AppUser
                        {
                            Email = "ernest.takos@zaginiony24.pl",
                            EmailConfirmed = true,
                            PhoneNumber = "782262485",
                            UserName = "takos932",
                            Name = "Ernest",
                            Surname = "Takos",
                            DateJoined = DateTime.Now,
                            IsActive = true

                        },
                        new AppUser
                        {
                            Email = "nadia.kutrapali@zaginiony24.pl",
                            EmailConfirmed = true,
                            PhoneNumber = "839576722",
                            UserName = "nadiia",
                            Name = "Nadia",
                            Surname = "Kutrapali",
                            DateJoined = DateTime.Now,
                            IsActive = true
                        },
                        new AppUser
                        {
                            Email = "robert.smolinski@zaginiony24.pl",
                            EmailConfirmed = true,
                            PhoneNumber = "635796411",
                            UserName = "robertsm",
                            Name = "Robert",
                            Surname = "Smolinski",
                            DateJoined = DateTime.Now,
                            IsActive = true
                        }
                    };

                    foreach (var user in users)
                    {
                        var result = userManager.CreateAsync(user, "Passw0rd!").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                    }

                    await roleManager.CreateAsync(new Role { Name = "Administrator" });

                    try
                    {
                        await userManager.AddToRoleAsync(await userManager.FindByNameAsync("admin"), "Administrator");
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    context.SaveChanges();
                }

                if (!context.Notices.Any())
                {
                    var list = new List<Notice>
                    {
                        new Notice
                        {
                            Name = "Maciej",
                            Surname = "Konradowski",
                            City = "Lublin",
                            District = "lubelskie",
                            Gender = "Mężczyzna",
                            DateOfDisappearance = new DateTime(2020, 03, 04),
                            LastSeenPlace = "Lublin",
                            Height = 193,
                            Age = 32,
                            EyeColor = "Piwne",
                            SpecialCharacters = null,
                            Description =
                                "W dniu zaginięcia miał duży zarost. Ubrany był w czarne dresy i bluzkę z napisem. Na sobie miał brązową kurtkę i wiązane buty tego samego koloru.",
                            DatePosted = DateTime.Now,
                            AppUser = await userManager.FindByNameAsync("testowyUser")
                        },
                        new Notice
                        {
                            Name = "Justyna",
                            Surname = "Kowalska",
                            City = "Warszawa",
                            District = "mazowieckie",
                            Gender = "Kobieta",
                            DateOfDisappearance = new DateTime(2020, 01, 22),
                            LastSeenPlace = "Warszawa",
                            Height = 175,
                            Age = 23,
                            EyeColor = "Niebieskie",
                            SpecialCharacters = null,
                            Description =
                                "W 23-latka wyszła ze swojego miejsca zamieszkania w Warszawie i dotąd nie wróciła. Nie nawiązała też kontaktu z rodziną. Policja opublikowała na swojej stronie internetowej zdjęcie zaginionej.",
                            DatePosted = DateTime.Now,
                            AppUser = await userManager.FindByNameAsync("robertsm")
                        },
                        new Notice
                        {
                            Name = "Łukasz",
                            Surname = "Malisz",
                            City = "Opole",
                            District = "opolskie",
                            Gender = "Mężczyzna",
                            DateOfDisappearance = new DateTime(2019, 12, 29),
                            LastSeenPlace = "Opole",
                            Height = 187,
                            Age = 66,
                            EyeColor = "Zielone",
                            SpecialCharacters = "Tatuaż smoka na prawym ramieniu",
                            Description =
                                "Poszukiwany mężczyzna jest chory na szichofrenie, wyszedł z domu o godzinie 15:20 Ostatni raz był widziany na kamerach w centrum miasta o 16:08, Zmartwiona rodzina błaga o pomoc.",
                            DatePosted = DateTime.Now,
                            AppUser = await userManager.FindByNameAsync("nadiia")
                        },
                        new Notice
                        {
                            Name = "Małgosia",
                            Surname = "Wichura",
                            City = "Kraków",
                            District = "małopolskie",
                            Gender = "Kobieta",
                            DateOfDisappearance = new DateTime(2020, 03, 12),
                            LastSeenPlace = "Kraków",
                            Height = 154,
                            Age = 16,
                            EyeColor = "Piwne",
                            SpecialCharacters = "blizna nad prawym okiem",
                            Description =
                                "Ostatni raz widziana w krakowie na dworcu, nikogo nie poinformowała dokąd jedzie, ",
                            DatePosted = DateTime.Now,
                            AppUser = await userManager.FindByNameAsync("testowyUser")
                        },
                        new Notice
                        {
                            Name = "Michał",
                            Surname = "Piskorz",
                            City = "Gdańsk",
                            District = "pomorskie",
                            Gender = "Mężczyzna",
                            DateOfDisappearance = new DateTime(2018, 06, 03),
                            LastSeenPlace = "Warszawa",
                            Height = 181,
                            Age = 35,
                            EyeColor = "szare",
                            SpecialCharacters = null,
                            Description =
                                "Mężczyzna widziany po raz ostatni o 21:37 na plaży w towarzytwie 3 innych osob. Policja posiada zdjęcia umożliwiające identyfikacje",
                            DatePosted = DateTime.Now,
                            AppUser = await userManager.FindByNameAsync("ruczaja")
                        },
                        new Notice
                        {
                            Name = "Karolina",
                            Surname = "Nowak",
                            City = "Łódź",
                            District = "łódzkie",
                            Gender = "Kobieta",
                            DateOfDisappearance = new DateTime(2009, 11, 01),
                            LastSeenPlace = "Skierniewice",
                            Height = 167,
                            Age = 27,
                            EyeColor = "Niebieskie",
                            SpecialCharacters = "tatuaż na prawym biodrze w kształcie lisa",
                            Description =
                                "Młoda mieszkana Łodzi wyjechała na cmentarz w Skieniewicach i z niego nie wrociła. Zaniepokojona rodzina prosi o pomoc",
                            DatePosted = DateTime.Now,
                            AppUser = await userManager.FindByNameAsync("testowyUser")
                        },
                        new Notice
                        {
                            Name = "Aleksander",
                            Surname = "Cichowlas",
                            City = "Piastów",
                            District = "mazowieckie",
                            Gender = "Mężczyzna",
                            DateOfDisappearance = new DateTime(2020, 01, 22),
                            LastSeenPlace = "Warszawa",
                            Height = 181,
                            Age = 42,
                            EyeColor = "Brązowe",
                            SpecialCharacters = null,
                            Description =
                                "Mężczyzna wyszedł z domu o godznie 8:00 ostatni raz widziany jak wysiadał z pociągu na dworcu Waszawa śródmieście o godznie 8:42. Policja prosi o pomoc w odnalezneniu zaginionego",
                            DatePosted = DateTime.Now,
                            AppUser = await userManager.FindByNameAsync("takos932")
                        },
                        new Notice
                        {
                            Name = "Krzystof",
                            Surname = "Czikita",
                            City = "Świnoujście",
                            District = "zachodniopomorskie",
                            Gender = "Mężczyzna",
                            DateOfDisappearance = new DateTime(2018, 09, 21),
                            LastSeenPlace = "Szczecin",
                            Height = 179,
                            Age = 16,
                            EyeColor = "Niebieskie",
                            SpecialCharacters = "znamie na lewym policzku",
                            Description =
                                "Rodzina Prosi o pomoc w odnalezieniu syna. Ostatni raz widziany w galerii handlowej w Szczecinie",
                            DatePosted = DateTime.Now,
                            AppUser = await userManager.FindByNameAsync("testowyUser")
                        },
                        new Notice
                        {
                            Name = "Justyna",
                            Surname = "Kowalska",
                            City = "Warszawa",
                            District = "mazowieckie",
                            Gender = "Kobieta",
                            DateOfDisappearance = new DateTime(2020, 01, 22),
                            LastSeenPlace = "Warszawa",
                            Height = 175,
                            Age = 23,
                            EyeColor = "Niebieskie",
                            SpecialCharacters = null,
                            Description =
                                "23-latka wyszła ze swojego miejsca zamieszkania w Warszawie i dotąd nie wróciła. Nie nawiązała też kontaktu z rodziną. Policja opublikowała na swojej stronie internetowej zdjęcie zaginionej.",
                            DatePosted = DateTime.Now,
                            AppUser = await userManager.FindByNameAsync("testowyUser")
                        },
                        new Notice
                        {
                            Name = "Aniela",
                            Surname = "Grey",
                            City = "Bolesławiec",
                            District = "dolnośląskie",
                            Gender = "Kobieta",
                            DateOfDisappearance = new DateTime(2019, 02, 28),
                            LastSeenPlace = "Wrocław",
                            Height = 171,
                            Age = 51,
                            EyeColor = "Piwne",
                            SpecialCharacters = null,
                            Description =
                                "Poszukiwana wyjechała służbowo do wrocławia o 18:00 wymeldowała się z hotelu i nie wrociła do domu.",
                            DatePosted = DateTime.Now,
                            AppUser = await userManager.FindByNameAsync("testowyUser")
                        },
                        new Notice
                        {
                            Name = "Kamil",
                            Surname = "Pindor",
                            City = "Mońki",
                            District = "podlaskie",
                            Gender = "Mężczyzna",
                            DateOfDisappearance = new DateTime(2019, 12, 24),
                            LastSeenPlace = "Mońki",
                            Height = 192,
                            Age = 33,
                            EyeColor = "Zielone",
                            SpecialCharacters = "brak 3 palców u lewej ręki",
                            Description = "Mężczyzna po kłótni z żoną wyszedł i nie wrocił do domu",
                            DatePosted = DateTime.Now,
                            AppUser = await userManager.FindByNameAsync("testowyUser")
                        },
                        new Notice
                        {
                            Name = "Urszula",
                            Surname = "Matejko",
                            City = "Warszawa",
                            District = "mazowieckie",
                            Gender = "Kobieta",
                            DateOfDisappearance = new DateTime(2020, 04, 02),
                            LastSeenPlace = "Warszawa",
                            Height = 166,
                            Age = 29,
                            EyeColor = "Zielone",
                            SpecialCharacters = "tatuaż w kształcie węża na lewym udzie",
                            Description = "Młoda kobieta wyszła na zakupy i nie wrociła. Rodzina błaga o pomoc",
                            DatePosted = DateTime.Now,
                            AppUser = await userManager.FindByNameAsync("testowyUser")
                        }
                    };

                    context.Notices.AddRange(list);
                    context.SaveChanges();
                }
            }
        }
    }
}
