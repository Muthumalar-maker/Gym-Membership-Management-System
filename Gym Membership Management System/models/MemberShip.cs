//namespace GymMembershipManagementSystem.Models
//{
//    public class MembershipPlan
//    {

//        public string PlanId { get; set; } = "";
//        public int MemberId { get; set; } 
//        public string PlanName { get; set; } = "";
//        public int DurationInDays { get; set; }
//        public decimal Fee { get; set; }
//    }
//}

namespace Gym_Membership_Management_System.Models
{
    public enum MembershipStatus
    {
        Active,
        Expired,
        Cancelled
    }

    public class Membership
    {
        public int MembershipId { get; set; }
        public int MemberId { get; set; }
        public int PlanId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public MembershipStatus Status { get; set; }
        public decimal AmountPaid { get; set; }
    }
}