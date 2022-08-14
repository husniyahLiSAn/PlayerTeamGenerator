using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerTeamGeneratorWeb.API.Models
{
    public class skill
    {
        public int id { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public bool is_deleted { get; set; }
    }

    public class FormPlayerSkills
    {
        public string skill { get; set; }
        public int value { get; set; }
    }
}
