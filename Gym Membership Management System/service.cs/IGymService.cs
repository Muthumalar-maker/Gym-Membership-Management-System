
using Gym_Membership_Management_System.models;

namespace Gym_Membership_Management_System
{
    public interface IGymService
    {
        void RegisterMember(Member member);
        void DisplayMembers();
        void SearchMember(int id);
        void SearchMemberByNameOrPhone(string search);
        void UpdateMember(int id, string name, string phone);
        void DeleteMember(int id);

        void DisplayPlans();
        void AssignMembership(int memberId, int planId);
        void DisplayMemberships();
        void RenewMembership(int membershipId);
        void CancelMembership(int membershipId);
        void SaveData();
        void AddPlan(MembershipPlan plan);
        MembershipPlan? GetPlanById(int planId);
        void SearchPlansByName(string name);
        void UpdatePlan(int planId, string name, int duration, decimal fee);

    }
}
