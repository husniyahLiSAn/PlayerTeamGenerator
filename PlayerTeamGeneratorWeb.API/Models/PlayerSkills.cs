using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerTeamGeneratorWeb.API.Models
{
    public class playerskills
    {
        public int id { get; set; }
        public int player_id { get; set; }
        public int skill_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public bool is_deleted { get; set; }
    }

    public class PlayerSkills
    {
        public int id { get; set; }
        public string skill { get; set; }
        public int value { get; set; }
        public int playerId { get; set; }
    }
}
