using Gym_Membership_Management_System.models;
using Gym_Membership_Management_System.Models;
using Gym_Membership_Management_System.Services;
using Gym_Membership_Management_System.Utilities;
//using GymMembershipManagementSystem.Models;
using System;

namespace Gym_Membership_Management_System
{
    internal class Program
    {
        static void Main()
        {
            JsonDataService storage = new JsonDataService();
            OutputWriter output = new OutputWriter();

            GymData data = storage.LoadData();

            MemberService memberService =
                new MemberService(data.Members);

            PlanService planService =
                new PlanService(data.Plans);

            MembershipService membershipService =
                new MembershipService(
                    data.Memberships, memberService, planService);

            while (true)
            {
                Console.WriteLine("\n===== GYM MEMBERSHIP SYSTEM =====");
                Console.WriteLine("1. Register Member");
                Console.WriteLine("2. View Members");
                Console.WriteLine("3. Search Member");
                Console.WriteLine("4. Add Membership Plan");
                Console.WriteLine("5. View Plans");
                Console.WriteLine("6. Assign Membership");
                Console.WriteLine("7. View Memberships");
                Console.WriteLine("8. Renew Membership");
                Console.WriteLine("9. Cancel Membership");
                Console.WriteLine("10. Active Memberships");
                Console.WriteLine("11. Expiring Memberships");
                Console.WriteLine("12. Revenue Report");
                Console.WriteLine("13. Active Counts Per Plan");
                Console.WriteLine("14. Members Without Active Membership");
                Console.WriteLine("15. Save Data");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");

                try
                {
                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("Enter a valid menu number.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            Console.Write("Member ID: ");
                            int memberId = int.Parse(Console.ReadLine()!);

                            Console.Write("Full name: ");
                            string name = Console.ReadLine() ?? "";

                            Console.Write("Age: ");
                            int age = int.Parse(Console.ReadLine()!);

                            Console.Write("Phone: ");
                            string phone = Console.ReadLine() ?? "";

                            memberService.RegisterMember(new Member
                            {
                                MemberId = memberId,
                                FullName = name,
                                Age = age,
                                PhoneNumber = phone,
                                JoinDate = DateTime.Today
                            });

                            output.Display("Member registered successfully.");
                            break;

                        case 2:
                            foreach (Member m in memberService.GetAllMembers())
                                output.Display(
                                    $"{m.MemberId} | {m.FullName} | {m.Age} | {m.PhoneNumber}");
                            break;

                        case 3:
                            Console.Write("Enter name or phone: ");
                            string keyword = Console.ReadLine() ?? "";

                            foreach (Member m in memberService.SearchMembers(keyword))
                                output.Display(
                                    $"{m.MemberId} | {m.FullName} | {m.PhoneNumber}");
                            break;

                        case 4:

                            Console.Write("Plan ID: ");
                            int planId = int.Parse(Console.ReadLine()!);

                            Console.Write("Plan name: ");
                            string planName = Console.ReadLine() ?? "";

                            Console.Write("Duration in days: ");
                            int duration = int.Parse(Console.ReadLine()!);

                            Console.Write("Fee: ");
                            decimal fee = decimal.Parse(Console.ReadLine()!);

                            planService.AddPlan(new MembershipPlan
                            {
                                PlanId = planId,
                                PlanName = planName,
                                DurationInDays = duration,
                                Fee = fee
                            });

                            output.Display("Plan added successfully.");
                            break;

                        case 5:
                            foreach (MembershipPlan p in planService.GetAllPlans())
                                output.Display(
                                    $"{p.PlanId} | {p.PlanName} | {p.DurationInDays} days | Rs.{p.Fee}");
                            break;

                        case 6:
                            Console.Write("Membership ID: ");
                            int mid = int.Parse(Console.ReadLine()!);

                            Console.Write("Member ID: ");
                            int memId = int.Parse(Console.ReadLine()!);

                            Console.Write("Plan ID: ");
                            int pid = int.Parse(Console.ReadLine()!);

                            Console.Write("Start date (yyyy-MM-dd): ");
                            DateTime start = DateTime.Parse(Console.ReadLine()!);

                            Console.Write("Amount paid: ");
                            decimal paid = decimal.Parse(Console.ReadLine()!);

                            membershipService.AssignMembership(
                                mid, memId, pid, start, paid);

                            output.Display("Membership assigned successfully.");
                            break;

                        case 7:
                            foreach (Membership m in membershipService.GetAllMemberships())
                                output.Display(
                                    $"{m.MembershipId} | Member {m.MemberId} | Plan {m.PlanId} | {m.StartDate:yyyy-MM-dd} to {m.EndDate:yyyy-MM-dd} | {m.Status} | Rs.{m.AmountPaid}");
                            break;

                        case 8:
                            Console.Write("New membership ID: ");
                            int newId = int.Parse(Console.ReadLine()!);

                            Console.Write("Old membership ID: ");
                            int oldId = int.Parse(Console.ReadLine()!);

                            Console.Write("Amount paid: ");
                            decimal renewalPaid = decimal.Parse(Console.ReadLine()!);

                            membershipService.RenewMembership(
                                newId, oldId, renewalPaid);

                            output.Display("Membership renewed successfully.");
                            break;

                        case 9:
                            Console.Write("Membership ID to cancel: ");
                            int cancelId = int.Parse(Console.ReadLine()!);

                            membershipService.CancelMembership(cancelId);
                            output.Display("Membership cancelled.");
                            break;

                        case 10:
                            foreach (Membership m in membershipService.GetActiveMemberships())
                                output.Display(
                                    $"Membership {m.MembershipId} | Member {m.MemberId} | Plan {m.PlanId} | Ends {m.EndDate:yyyy-MM-dd}");
                            break;

                        case 11:
                            Console.Write("Expiring within how many days? ");
                            int days = int.Parse(Console.ReadLine()!);

                            foreach (Membership m in membershipService.GetExpiringMemberships(days))
                                output.Display(
                                    $"Membership {m.MembershipId} | Ends {m.EndDate:yyyy-MM-dd}");
                            break;

                        case 12:
                            output.Display(
                                "Total revenue: Rs." +
                                membershipService.GetTotalRevenue());
                            break;

                        case 13:
                            foreach (var item in membershipService.GetActiveCountsPerPlan())
                                output.Display(
                                    $"Plan {item.Key}: {item.Value} active membership(s)");
                            break;

                        case 14:
                            foreach (Member m in membershipService.GetMembersWithoutActiveMembership())
                                output.Display($"{m.MemberId} | {m.FullName}");
                            break;

                        case 15:
                            storage.SaveData(data);
                            output.Display("Data saved to gymdata.json.");
                            break;

                        //case 0:
                        //    storage.SaveData(data);
                        //    output.Display("Data saved. Exiting application.");
                        //    return;

                        case 0:
                            storage.SaveData(data);
                            output.Display("Data saved successfully.");
                            output.OpenOutputFile();
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
