//using GymMembershipManagementSystem.Models;
//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using System.Text;

//namespace Gym_Membership_Management_System.models
//{
//    internal class MembershipPlan
//    {
//        public int PlanId { get; set; }
//        public string PlanName { get; set; } = "";
//        public int DurationInMonths { get; set; }
//        public decimal Fee { get; set; }
//    }
//}


using System;

namespace Gym_Membership_Management_System.models
{
    public class MembershipPlan
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; } = "";
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }

        public MembershipPlan()
        {
        }

        public MembershipPlan(int planId, string planName,
                              decimal price, int durationInDays)
        {
            PlanId = planId;
            PlanName = planName;
            Price = price;
            DurationInDays = durationInDays;
        }

        public override string ToString()
        {
            return $"Plan ID: {PlanId}, Name: {PlanName}, " +
                   $"Price: ₹{Price}, Duration: {DurationInDays} days";
        }
    }
}
