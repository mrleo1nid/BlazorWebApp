using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazorWebApp.Shared.Auth;
using BlazorWebApp.Shared.Helpers;
using BlazorWebApp.Shared.Models;
using BlazorWebApp.Shared.NameGeneration;
using Microsoft.AspNetCore.Identity;

namespace BlazorWebApp.Server.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, ApplicationDbContext appcontext)
        {
            string adminEmail = "admin@gmail.com";
            string password = "adminadmin";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("user"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                ApplicationUser admin = new ApplicationUser { Email = adminEmail, UserName = "admin", RegisterDateTime = DateTime.Now};
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            await InitTraits(appcontext);
        }

        private static async Task InitTraits(ApplicationDbContext appcontext)
        {
            if (!appcontext.Traits.Any())
            {
                if (new FileInfo("NamesData/character_traits.txt").Exists)
                {
                    string[] readText = File.ReadAllLines("NamesData/character_traits.txt");
                    if (readText.Any())
                    {
                        string[] reslines = readText.Where(x => x.Length > 3).ToArray();
                        foreach (var line in reslines)
                        {
                            CharacterTrait trait = new CharacterTrait()
                            {
                                Name = line
                            };
                            await appcontext.Traits.AddAsync(trait);
                        }

                        await appcontext.SaveChangesAsync();
                    }
                }
            }
        }

        public static async Task InitNamesContext(NamesDbContext context)
        {
            List<NameString> malestats = new List<NameString>();

            if (!context.NameStrings.Any())
            {
                if (new FileInfo("NamesData/russian_names.json").Exists)
                {
                    var names = JsonSerealizeHelper.DeserializeArrayFromFile<NameString>("NamesData/russian_names.json");
                    await context.NameStrings.AddRangeAsync(names.Where(x=>x.Sex=="Ж"));
                    await context.SaveChangesAsync();
                    malestats.AddRange(names.Where(x => x.Sex == "М"));
                }
            }

            if (!context.SurnameStrings.Any())
            {
                if (new FileInfo("NamesData/russian_surnames.json").Exists)
                {
                    var surnames = JsonSerealizeHelper.DeserializeArrayFromFile<SurnameString>("NamesData/russian_surnames.json");
                    foreach (var surname in surnames)
                    {
                        if (surname.Surname.EndsWith('а'))
                        {
                            surname.Sex = "Ж";
                        }
                        else if (surname.Surname.EndsWith('я'))
                        {
                            surname.Sex = "Ж";
                        }
                        else
                        {
                            surname.Sex = "М";
                        }
                    }
                    await context.SurnameStrings.AddRangeAsync(surnames);
                    await context.SaveChangesAsync();
                }

            }
            if (!context.PatronimicStrings.Any())
            {
                if (new FileInfo("NamesData/russian_patronymic.txt").Exists)
                {
                    string[] readText = File.ReadAllLines("NamesData/russian_patronymic.txt");
                    if (readText.Any())
                    {
                        string[] reslines = readText.Where(x => x.Length > 6).ToArray();
                        foreach (var line in reslines)
                        {
                            var trimed = line.Trim().Split('(');
                            var name = trimed[0].Trim();

                            var inforow = malestats.Where(x => x.Name.Trim() == name).FirstOrDefault();
                            int peoplecount = 0;
                            DateTime wenDateTime = DateTime.Now;
                            if (inforow!=null)
                            {
                                peoplecount = inforow.PeoplesCount;
                                wenDateTime = inforow.WhenPeoplesCount;
                            }
                            NameString nameString = new NameString()
                            {
                                Name = name,
                                PeoplesCount = peoplecount,
                                Sex = "М",
                                WhenPeoplesCount = wenDateTime
                            };
                            await context.NameStrings.AddAsync(nameString);
                            await context.SaveChangesAsync();

                            var parttwo = trimed[1].TrimEnd(')');
                            var elems = parttwo.Split(' ');
                            var reselem = elems.Where(x => x.Length > 3);
                            foreach (var elem in reselem)
                            {
                                PatronimicString str = new PatronimicString()
                                {
                                    Patronimic = elem.TrimEnd(',').Trim(),
                                    NameStringId = nameString.ID,
                                    NameString = nameString
                                };
                                if (str.Patronimic.EndsWith('а'))
                                {
                                    str.Sex = Sex.Female;
                                }
                                else
                                {
                                    str.Sex = Sex.Male;
                                }
                                await context.PatronimicStrings.AddAsync(str);
                            }
                            await context.SaveChangesAsync();
                        }
                    }
                }
            }
        }
    }
}
