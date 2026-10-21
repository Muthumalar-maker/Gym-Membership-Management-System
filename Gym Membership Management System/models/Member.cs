using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Membership_Management_System.models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string FullName { get; set; } = "";
        public int Age { get; set; }
        public string PhoneNumber { get; set; } = "";
        public DateTime JoinDate { get; set; } = DateTime.Today;
    }


}



