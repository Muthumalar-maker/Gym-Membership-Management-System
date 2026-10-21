
using Gym_Membership_Management_System.models;
using Gym_Membership_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym_Membership_Management_System.Services
{
    public class MemberService
    {
        private readonly List<Member> members;

        public MemberService(List<Member> members)
        {
            this.members = members;
        }

        public void RegisterMember(Member member)
        {
            if (members.Any(m => m.MemberId == member.MemberId))
                throw new Exception("Member ID already exists.");

            if (string.IsNullOrWhiteSpace(member.FullName))
                throw new Exception("Name cannot be empty.");

            if (member.Age < 16 || member.Age > 100)
                throw new Exception("Age must be between 16 and 100.");

            if (member.PhoneNumber.Length != 10 ||
                !member.PhoneNumber.All(char.IsDigit))
                throw new Exception("Enter a valid 10-digit phone number.");

            members.Add(member);
        }

        public List<Member> GetAllMembers()
        {
            return members.OrderBy(m => m.MemberId).ToList();
        }

        public Member? GetMemberById(int id)
        {
            return members.FirstOrDefault(m => m.MemberId == id);
        }

        public List<Member> SearchMembers(string keyword)
        {
            return members.Where(m =>
                m.FullName.Contains(
                    keyword, StringComparison.OrdinalIgnoreCase) ||
                m.PhoneNumber.Contains(keyword))
                .ToList();
        }

        public void UpdatePhoneNumber(int id, string phone)
        {
            Member? member = GetMemberById(id);

            if (member == null)
                throw new Exception("Member not found.");

            if (phone.Length != 10 || !phone.All(char.IsDigit))
                throw new Exception("Enter a valid 10-digit phone number.");

            member.PhoneNumber = phone;
        }
    }
}
