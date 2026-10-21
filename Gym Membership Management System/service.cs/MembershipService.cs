
using Gym_Membership_Management_System.models;
using Gym_Membership_Management_System.Models;
//using GymMembershipManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym_Membership_Management_System.Services
{
    public class MembershipService
    {
        private readonly List<Membership> memberships;
        private readonly MemberService memberService;
        private readonly PlanService planService;

        public MembershipService(
            List<Membership> memberships,
            MemberService memberService,
            PlanService planService)
        {
            this.memberships = memberships;
            this.memberService = memberService;
            this.planService = planService;
        }

        public void AssignMembership(
            int membershipId, int memberId, int planId,
            DateTime startDate, decimal amountPaid)
        {
            if (memberships.Any(m => m.MembershipId == membershipId))
                throw new Exception("Membership ID already exists.");

            if (memberService.GetMemberById(memberId) == null)
                throw new Exception("Member not found.");

            MembershipPlan? plan = planService.GetPlanById(planId);

            if (plan == null)
                throw new Exception("Plan not found.");

            if (amountPaid < 0)
                throw new Exception("Payment cannot be negative.");

            memberships.Add(new Membership
            {
                MembershipId = membershipId,
                MemberId = memberId,
                PlanId = planId,
                StartDate = startDate,
                EndDate = startDate.AddDays(plan.DurationInDays),
                AmountPaid = amountPaid,
                Status = startDate <= DateTime.Today &&
                         startDate.AddDays(plan.DurationInDays) > DateTime.Today
                    ? MembershipStatus.Active
                    : startDate.AddDays(plan.DurationInDays) <= DateTime.Today
                        ? MembershipStatus.Expired
                        : MembershipStatus.Active
            });
        }

        public List<Membership> GetAllMemberships()
        {
            RefreshStatuses();
            return memberships.OrderBy(m => m.MembershipId).ToList();
        }

        public Membership? GetMembershipById(int id)
        {
            RefreshStatuses();
            return memberships.FirstOrDefault(m => m.MembershipId == id);
        }

        public void CancelMembership(int id)
        {
            Membership? membership = GetMembershipById(id);

            if (membership == null)
                throw new Exception("Membership not found.");

            if (membership.Status != MembershipStatus.Active)
                throw new Exception("Only active memberships can be cancelled.");

            membership.Status = MembershipStatus.Cancelled;
        }

        public void RenewMembership(
            int newMembershipId, int oldMembershipId, decimal amountPaid)
        {
            Membership? old = GetMembershipById(oldMembershipId);

            if (old == null)
                throw new Exception("Membership not found.");

            if (old.Status == MembershipStatus.Cancelled)
                throw new Exception("Cancelled memberships cannot be renewed.");

            if (amountPaid < 0)
                throw new Exception("Payment cannot be negative.");

            DateTime newStartDate =
                old.EndDate > DateTime.Today ? old.EndDate : DateTime.Today;

            AssignMembership(
                newMembershipId, old.MemberId, old.PlanId,
                newStartDate, amountPaid);
        }

        public bool HasActiveMembership(int memberId, DateTime date)
        {
            return memberships.Any(m =>
                m.MemberId == memberId &&
                m.Status != MembershipStatus.Cancelled &&
                m.StartDate <= date &&
                m.EndDate > date);
        }

        public List<Membership> GetActiveMemberships()
        {
            RefreshStatuses();

            return memberships.Where(m =>
                m.Status == MembershipStatus.Active &&
                m.StartDate <= DateTime.Today &&
                m.EndDate > DateTime.Today).ToList();
        }

        public List<Membership> GetMembershipsForMember(int memberId)
        {
            return memberships.Where(m => m.MemberId == memberId).ToList();
        }

        public List<Membership> GetExpiringMemberships(int days)
        {
            if (days < 0)
                throw new Exception("Days cannot be negative.");

            return GetActiveMemberships().Where(m =>
                m.EndDate <= DateTime.Today.AddDays(days)).ToList();
        }

        public List<IGrouping<int, Membership>> GroupByPlan()
        {
            return memberships.GroupBy(m => m.PlanId).ToList();
        }

        public decimal GetTotalRevenue()
        {
            return memberships.Sum(m => m.AmountPaid);
        }

        public Dictionary<int, int> GetActiveCountsPerPlan()
        {
            return GetActiveMemberships()
                .GroupBy(m => m.PlanId)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public List<Member> GetMembersWithoutActiveMembership()
        {
            return memberService.GetAllMembers()
                .Where(member => !HasActiveMembership(
                    member.MemberId, DateTime.Today))
                .ToList();
        }

        public void RefreshStatuses()
        {
            foreach (Membership membership in memberships)
            {
                if (membership.Status != MembershipStatus.Cancelled)
                {
                    membership.Status =
                        membership.EndDate <= DateTime.Today
                            ? MembershipStatus.Expired
                            : membership.StartDate <= DateTime.Today
                                ? MembershipStatus.Active
                                : MembershipStatus.Active;
                }
            }
        }
    }
}
