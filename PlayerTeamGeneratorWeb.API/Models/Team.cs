using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerTeamGeneratorWeb.API.Models
{
    public class ListMember
    {
        public int player_id { get; set; }
        public int playerskill_id { get; set; }
    }

    public class team : ListMember
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public bool is_deleted { get; set; }
    }

    public class AddTeam
    {
        public string position { get; set; }
        public string mainSkill { get; set; }
        public int numberOfPlayers { get; set; }
    }

    public class Team
    {
        public string name { get; set; }
        public string position { get; set; }
    }

    public class ListBestPlayer : ListTeam
    {
        public int id { get; set; }
        public string skill { get; set; }
        public int value { get; set; }
    }

    public class ListTeam
    {
        public string name { get; set; }
        public string position { get; set; }
        public List<FormPlayerSkills> playerSkills { get; set; } = new List<FormPlayerSkills>();
    }

    public class GridDataListBestPlayers
    {
        public List<ListTeam> Data { get; set; } = new List<ListTeam>();
        public int Total { get; set; }
    }
}
