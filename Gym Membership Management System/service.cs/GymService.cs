using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using Gym_Membership_Management_System.models;

namespace Gym_Membership_Management_System.services
{
    public class GymService
    {
        private List<Member> members = new List<Member>();
        private string fileName = "members.json";

        public GymService()
        {
            LoadData();
        }
        public void AddMember(Member member)
        {
            if (members.Any(m => m.MemberId == member.MemberId))
                throw new Exception("Member ID already exists.");

            if (member.MemberId <= 0)
                throw new Exception("Member ID must be positive.");

            if (member.Age < 16 || member.Age > 100)
                throw new Exception("Age must be between 16 and 100.");

            if (member.PhoneNumber.Length != 10 ||
                !member.PhoneNumber.All(char.IsDigit))
                throw new Exception("Phone number must contain 10 digits.");

            if (members.Any(m => m.PhoneNumber == member.PhoneNumber))
                throw new Exception("Phone number already exists.");

            members.Add(member);
            SaveData();

            Console.WriteLine("Member registered successfully!");
        }
        public void DisplayMembers()
        {
            if (members.Count == 0)
            {
                Console.WriteLine("No members found.");
                return;
            }

            foreach (Member m in members)
            {
                Console.WriteLine(
                    $"ID: {m.MemberId} " +
                    $"Name: {m.FullName} " +
                    $"Age: {m.Age} " +
                    $"Phone: {m.PhoneNumber} "+
                    $"Join Date: {m.JoinDate:dd-MM-yyyy}");
            }
            foreach (Member m in members)
            {
                Console.WriteLine($"Member: {m.MemberId}" + $"MemberName:{m.FullName}");
            }
        }
        public void SearchMember(int id)
        {
            Member? member = members.FirstOrDefault(
                m => m.MemberId == id);

            if (member == null)
            {
                Console.WriteLine("Member not found.");
                return;
            }

            Console.WriteLine(
                $"ID: {member.MemberId}, Name: {member.FullName}, " +
                $"Age: {member.Age}, Phone: {member.PhoneNumber}");
        }
        public void SearchMemberByNameOrPhone(string search)
        {
            var result = members.Where(m =>
                m.FullName.Contains(search,
                    StringComparison.OrdinalIgnoreCase) ||
                m.PhoneNumber.Contains(search));

            if (!result.Any())
            {
                Console.WriteLine("No matching members found.");
                return;
            }

            foreach (Member m in result)
            {
                Console.WriteLine(
                    $"{m.MemberId} | {m.FullName} | {m.PhoneNumber}");
            }
        }

        public void UpdateMember(int id, string name, string phone)
        {
            Member? member = members.FirstOrDefault(
                m => m.MemberId == id);

            if (member == null)
                throw new Exception("Member not found.");

            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name cannot be empty.");

            if (phone.Length != 10 ||
                !phone.All(char.IsDigit))
                throw new Exception("Phone must contain 10 digits.");

            if (members.Any(m =>
                m.MemberId != id && m.PhoneNumber == phone))
                throw new Exception("Phone number already exists.");

            member.FullName = name;
            member.PhoneNumber = phone;

            SaveData();
            Console.WriteLine("Member updated successfully!");
        }
        public void DeleteMember(int id)
        {
            Member? member = members.FirstOrDefault(
                m => m.MemberId == id);

            if (member == null)
                throw new Exception("Member not found.");

            members.Remove(member);
            SaveData();

            Console.WriteLine("Member deleted successfully.");
        }

        public void SaveData()
        {
            string json = JsonSerializer.Serialize(
                members,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(fileName, json);
        }
        public void LoadData()
        {
            if (!File.Exists(fileName))
                return;

            try
            {
                string json = File.ReadAllText(fileName);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    members = JsonSerializer.Deserialize<List<Member>>(json)
                        ?? new List<Member>();
                }
            }
            catch (JsonException)
            {
                Console.WriteLine("Invalid JSON data. Please check members.json.");

            }
        }
    }
}

