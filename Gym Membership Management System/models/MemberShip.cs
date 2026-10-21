namespace GymMembershipManagementSystem.Models
{
    public class MembershipPlan
    {
        
        public string PlanId { get; set; } = "";
        public int MemberId { get; set; } 
        public string PlanName { get; set; } = "";
        public int DurationInDays { get; set; }
        public decimal Fee { get; set; }
    }
}

