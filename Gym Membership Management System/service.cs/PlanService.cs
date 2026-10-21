
using Gym_Membership_Management_System.Models;
//using GymMembershipManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym_Membership_Management_System.Services
{
    public class PlanService
    {
        private readonly List<MembershipPlan> plans;

        public PlanService(List<MembershipPlan> plans)
        {
            this.plans = plans;
        }

        public void AddPlan(MembershipPlan plan)
        {
            if (plans.Any(p => p.PlanId == plan.PlanId))
                throw new Exception("Plan ID already exists.");

            if (string.IsNullOrWhiteSpace(plan.PlanName))
                throw new Exception("Plan name cannot be empty.");

            if (plan.DurationInDays <= 0)
                throw new Exception("Duration must be positive.");

            if (plan.Fee < 0)
                throw new Exception("Fee cannot be negative.");

            plans.Add(plan);
        }

        public List<MembershipPlan> GetAllPlans()
        {
            return plans.OrderBy(p => p.PlanId).ToList();
        }

        public MembershipPlan? GetPlanById(int id)
        {
            return plans.FirstOrDefault(p => p.PlanId == id);
        }

        public List<MembershipPlan> SearchPlans(string name)
        {
            return plans.Where(p =>
                p.PlanName.Contains(
                    name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void UpdatePlan(
            int id, string name, int duration, decimal fee)
        {
            MembershipPlan? plan = GetPlanById(id);

            if (plan == null)
                throw new Exception("Plan not found.");

            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Plan name cannot be empty.");

            if (duration <= 0 || fee < 0)
                throw new Exception("Invalid duration or fee.");

            plan.PlanName = name;
            plan.DurationInDays = duration;
            plan.Fee = fee;
        }
    }
}
