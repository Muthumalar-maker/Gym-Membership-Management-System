namespace Gym_Membership_Management_System.Models
{
    public class MembershipPlan
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; } = "";
        public int DurationInDays { get; set; }
        public decimal Fee { get; set; }
    }
}