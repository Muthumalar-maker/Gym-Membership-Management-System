
using Gym_Membership_Management_System.models;
using Gym_Membership_Management_System.Models;
//using GymMembershipManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Gym_Membership_Management_System.Utilities
{
    public class GymData
    {
        public List<Member> Members { get; set; } = new();
        public List<MembershipPlan> Plans { get; set; } = new();
        public List<Membership> Memberships { get; set; } = new();
    }

    public class JsonDataService
    {
        private readonly string filePath = "gymdata.json";

        private readonly JsonSerializerOptions options =
            new JsonSerializerOptions { WriteIndented = true };

        public GymData LoadData()
        {
            try
            {
                if (!File.Exists(filePath))
                    return new GymData();

                string json = File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(json))
                    return new GymData();

                return JsonSerializer.Deserialize<GymData>(json)
                       ?? new GymData();
            }
            catch (JsonException)
            {
                Console.WriteLine("JSON file is invalid. Starting with empty data.");
                return new GymData();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Could not read data: " + ex.Message);
                return new GymData();
            }
        }

        public void SaveData(GymData data)
        {
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, json);
        }
    }
}
