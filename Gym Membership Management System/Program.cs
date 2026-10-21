using System;
using System.ComponentModel;
using Gym_Membership_Management_System.models;
using Gym_Membership_Management_System.services;

namespace Gym_Membership_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GymService service = new GymService();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n---- GYM MEMBERSHIP MANAGEMENT ---");
                Console.WriteLine("1. Register Member");
                Console.WriteLine("2. Display All Members");
                Console.WriteLine("3. Search Member by ID");
                Console.WriteLine("4. Search by Name or Phone");
                Console.WriteLine("5. Update Member");
                Console.WriteLine("6. Add the ");
                Console.WriteLine("6. Delete Member");
                Console.WriteLine("7. Save Data");
                Console.WriteLine("8. Add Member");
                Console.WriteLine("9. Edit Member");
                Console.WriteLine("10.Cancel Member");
                
                Console.WriteLine("0. Exit");
                Console.Write("Enter choice: ");


                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Enter a valid number.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            {
                                Member member = new Member();

                                Console.Write("Member ID: ");
                                member.MemberId = int.Parse(
                                    Console.ReadLine() ?? "");

                                Console.Write("Full Name: ");
                                member.FullName =
                                    Console.ReadLine() ?? "";

                                Console.Write("Age: ");
                                member.Age = int.Parse(
                                    Console.ReadLine() ?? "");

                                Console.Write("Phone Number: ");
                                member.PhoneNumber =
                                    Console.ReadLine() ?? "";

                                member.JoinDate = DateTime.Today;

                                service.AddMember(member);
                                break;
                            }

                        case 2:
                            service.DisplayMembers();
                            break;

                        case 3:
                            Console.Write("Enter Member ID: ");

                            if (int.TryParse(
                                Console.ReadLine(), out int searchId))
                                service.SearchMember(searchId);
                            else
                                Console.WriteLine("Invalid ID.");
                            break;

                        case 4:
                            Console.Write("Enter name or phone: ");
                            string search = Console.ReadLine() ?? "";
                            service.SearchMemberByNameOrPhone(search);
                            break;

                        case 5:
                            {
                                Console.Write("Enter Member ID: ");

                                if (!int.TryParse(
                                    Console.ReadLine(), out int updateId))
                                {
                                    Console.WriteLine("Invalid ID.");
                                    break;
                                }

                                Console.Write("New Name: ");
                                string name = Console.ReadLine() ?? "";

                                Console.Write("New Phone Number: ");
                                string phone = Console.ReadLine() ?? "";

                                service.UpdateMember(updateId, name, phone);
                                break;
                            }

                        case 6:
                            Console.Write("Enter Member ID to delete: ");

                            if (int.TryParse(
                                Console.ReadLine(), out int deleteId))
                                service.DeleteMember(deleteId);
                            else
                                Console.WriteLine("Invalid ID.");
                            break;

                        case 7:
                            service.SaveData();
                            Console.WriteLine("Data saved successfully.");
                            break;

                        case 8:
                            service.SaveData();
                            Console.WriteLine("Add Member.");
                            break;

                        case 9:
                            service.SaveData();
                            Console.WriteLine("Edit Member.");
                            break;

                        case 10:
                            service.SaveData();
                            Console.WriteLine("Cancel Member");
                            break;

                        case 0:
                            service.SaveData();
                            running = false;
                            Console.WriteLine("Data saved. bye!");
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter valid input.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
