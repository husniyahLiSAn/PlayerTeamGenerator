using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerTeamGeneratorWeb.API.Models
{
    public class player
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public bool is_deleted { get; set; }
    }

    public class Players
    {
        public int id { get; set; }
        public string name { get; set; }
        public string position { get; set; }
    }

    public class ListPlayer : Players
    {
        public int idSkill { get; set; }
        public string skill { get; set; }
        public int value { get; set; }
        public int playerId { get; set; }
    }

    public class FormPlayer
    {
        public string name { get; set; }
        public string position { get; set; }
        public List<FormPlayerSkills> playerSkills { get; set; } = new List<FormPlayerSkills>();
    }

    public class GetAllPlayers
    {
        public int id { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public List<PlayerSkills> playerSkills { get; set; } = new List<PlayerSkills>();
    }

    public class GridDataListPlayers
    {
        public List<GetAllPlayers> Data { get; set; } = new List<GetAllPlayers>();
        public int Total { get; set; }
    }
}
