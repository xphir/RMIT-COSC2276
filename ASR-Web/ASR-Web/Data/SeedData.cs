using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Asr.Models;
using Microsoft.AspNetCore.Identity;
using ASR_Web.Data;
using ASR_Web.Models;

namespace Asr.Data
{
    public static class SeedData
    {
        public static async Task InitialiseAsync(IServiceProvider serviceProvider)
        {
            using(var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>())
            using(var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>())
            using(var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                await InitialiseUsersAsync(userManager, roleManager);
                await InitialiseAsrDataAsync(context, userManager);
            }
        }

        private static async Task InitialiseUsersAsync(
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            foreach(var role in new[] { Constants.StaffRole, Constants.StudentRole })
            {
                if(!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
            }

            await CreateUserAndEnsureUserHasRoleAsync(userManager, "e12345@rmit.edu.au", Constants.StaffRole);
            await CreateUserAndEnsureUserHasRoleAsync(userManager, "e56789@rmit.edu.au", Constants.StaffRole);
            await CreateUserAndEnsureUserHasRoleAsync(userManager, "s1234567@student.rmit.edu.au", Constants.StudentRole);
            await CreateUserAndEnsureUserHasRoleAsync(userManager, "s4567890@student.rmit.edu.au", Constants.StudentRole);

            await CreateUserAndEnsureUserHasRoleAsync(userManager, "e54321@rmit.edu.au", Constants.StaffRole);
            await CreateUserAndEnsureUserHasRoleAsync(userManager, "e98765@rmit.edu.au", Constants.StaffRole);
            await CreateUserAndEnsureUserHasRoleAsync(userManager, "s7654321@student.rmit.edu.au", Constants.StudentRole);
            await CreateUserAndEnsureUserHasRoleAsync(userManager, "s0987654@student.rmit.edu.au", Constants.StudentRole);

            await EnsureUserHasRoleAsync(userManager, "Elliot.Schot@gmail.com", Constants.StaffRole);
        }

        private static async Task CreateUserAndEnsureUserHasRoleAsync(
            UserManager<ApplicationUser> userManager, string userName, string role)
        {
            if(await userManager.FindByNameAsync(userName) == null)
                await userManager.CreateAsync(new ApplicationUser { UserName = userName, Email = userName }, "abc123");
            await EnsureUserHasRoleAsync(userManager, userName, role);
        }

        private static async Task EnsureUserHasRoleAsync(
            UserManager<ApplicationUser> userManager, string userName, string role)
        {
            var user = await userManager.FindByNameAsync(userName);
            if(user != null && !await userManager.IsInRoleAsync(user, role))
                await userManager.AddToRoleAsync(user, role);
        }

        private static async Task InitialiseAsrDataAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            // Look for any rooms.
            if(await context.Room.AnyAsync())
                return; // DB has been seeded.

            await context.Room.AddRangeAsync(
                new Room { RoomID = "A" },
                new Room { RoomID = "B" },
                new Room { RoomID = "C" },
                new Room { RoomID = "D" }
            );

            await CreateStaffAsync(context, "e12345", "Matt");
            await CreateStaffAsync(context, "e56789", "Joe");
            await CreateStaffAsync(context, "e54321", "Rod");
            await CreateStaffAsync(context, "e98765", "Alex");

            await CreateStudentAsync(context, "s1234567", "Kevin");
            await CreateStudentAsync(context, "s4567890", "Olivier");
            await CreateStudentAsync(context, "s7654321", "Elliot");
            await CreateStudentAsync(context, "s0987654", "Philip");

            await context.Slot.AddRangeAsync(
                new Slot
                {
                    RoomID = "A",
                    StartTime = new DateTime(2019, 2, 20, 10, 0, 0),
                    StaffID = "e12345"
                },
                new Slot
                {
                    RoomID = "B",
                    StartTime = new DateTime(2019, 2, 20, 10, 0, 0),
                    StaffID = "e56789",
                    StudentID = "s1234567"
                },
                new Slot
                {
                    RoomID = "C",
                    StartTime = new DateTime(2019, 2, 20, 10, 0, 0),
                    StaffID = "e54321",
                    StudentID = "s4567890"
                },
                new Slot
                {
                    RoomID = "D",
                    StartTime = new DateTime(2019, 2, 20, 10, 0, 0),
                    StaffID = "e98765",
                },
                new Slot
                {
                    RoomID = "A",
                    StartTime = new DateTime(2019, 2, 27, 09, 0, 0),
                    StaffID = "e12345"
                },
                new Slot
                {
                    RoomID = "B",
                    StartTime = new DateTime(2019, 2, 23, 11, 0, 0),
                    StaffID = "e56789",
                },
                new Slot
                {
                    RoomID = "C",
                    StartTime = new DateTime(2019, 2, 25, 13, 0, 0),
                    StaffID = "e54321",
                },
                new Slot
                {
                    RoomID = "D",
                    StartTime = new DateTime(2019, 2, 25, 10, 0, 0),
                    StaffID = "e98765",
                },
                new Slot
                {
                    RoomID = "A",
                    StartTime = new DateTime(2019, 2, 22, 09, 0, 0),
                    StaffID = "e12345",
                },
                new Slot
                {
                    RoomID = "A",
                    StartTime = new DateTime(2019, 2, 22, 10, 0, 0),
                    StaffID = "e56789",
                },
                new Slot
                {
                    RoomID = "A",
                    StartTime = new DateTime(2019, 2, 22, 11, 0, 0),
                    StaffID = "e54321",
                },
                new Slot
                {
                    RoomID = "A",
                    StartTime = new DateTime(2019, 2, 22, 12, 0, 0),
                    StaffID = "e98765",
                },
                new Slot
                {
                    RoomID = "A",
                    StartTime = new DateTime(2019, 2, 22, 13, 0, 0),
                    StaffID = "e12345",
                    StudentID = "s4567890"
                }
            );

            //await context.SaveChangesAsync();

            await UpdateUserAsync(userManager, "e12345@rmit.edu.au", "e12345");
            await UpdateUserAsync(userManager, "e56789@rmit.edu.au", "e56789");
            await UpdateUserAsync(userManager, "s1234567@student.rmit.edu.au", "s1234567");
            await UpdateUserAsync(userManager, "s4567890@student.rmit.edu.au", "s4567890");

            await UpdateUserAsync(userManager, "e54321@rmit.edu.au", "e54321");
            await UpdateUserAsync(userManager, "e98765@rmit.edu.au", "e98765");
            await UpdateUserAsync(userManager, "s7654321@student.rmit.edu.au", "s7654321");
            await UpdateUserAsync(userManager, "s0987654@student.rmit.edu.au", "s0987654");

            await CreateStudentAsync(context, "Elliot.Schot", "Elliot");
            await context.SaveChangesAsync();

            await UpdateUserAsync(userManager, "Elliot.Schot@gmail.com", "Elliot.Schot");
        }

        private static async Task CreateStaffAsync(ApplicationDbContext context, string id, string name)
        {
            await context.Staff.AddAsync(new Staff
            {
                StaffID = id,
                Email = id + "@rmit.edu.au",
                Name = name
            });
        }

        private static async Task CreateStudentAsync(ApplicationDbContext context, string id, string name)
        {
            await context.Student.AddAsync(new Student
            {
                StudentID = id,
                Email = id + "@student.rmit.edu.au",
                Name = name
            });
        }

        private static async Task UpdateUserAsync(UserManager<ApplicationUser> userManager, string userName, string id)
        {
            var user = await userManager.FindByNameAsync(userName);
            if(user.UserName.StartsWith('e'))
                user.StaffID = id;
            if(user.UserName.StartsWith('s'))
                user.StudentID = id;

            await userManager.UpdateAsync(user);
        }
    }
}
