using Dapper;
using PlayerTeamGeneratorWeb.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerTeamGeneratorWeb.API.Repositories
{
    public class TeamRepository
    {
        private readonly SqlConnection sqlConnection;
        private readonly DynamicParameters parameters = new DynamicParameters();
        private readonly CommandType cmd = CommandType.StoredProcedure;

        public TeamRepository(ConnectionStrings connection)
        {
            sqlConnection = new SqlConnection(connection.Value);
        }

        //TODO: View Data List Best Players
        public async Task<GridDataListBestPlayers> GetMemberofTeam(string filter, string sort, int start, int pageLimit)
        {
            var sp = "SP_GetMemberofTeam";
            DynamicParameters param = new DynamicParameters();
            param.Add("Condition", filter);
            param.Add("Sort", sort);
            param.Add("Start", start);
            param.Add("PageLimit", pageLimit);
            param.Add("TotalCount", DbType.Int32, direction: ParameterDirection.Output);

            var result = await sqlConnection.QueryAsync<ListBestPlayer>(sp, param, commandType: cmd);
            var total = parameters.Get<int>("TotalCount");
            List<ListTeam> res = new List<ListTeam>(total);
            var group = result.GroupBy(x => x.id).ToList();
            foreach (var lists in group)
            {
                var Data = new ListTeam();
                foreach (var player in lists)
                {
                    Data.name = player.name;
                    Data.position = player.position;

                    var ins = new FormPlayerSkills()
                    {
                        skill = player.skill,
                        value = player.value,
                    };
                    Data.playerSkills.Add(ins);
                }
                res.Add(Data);
            }
            var list = new GridDataListBestPlayers
            {
                Data = res,
                Total = total
            };
            return list;
        }

        // TODO: Add Data Player to Team
        public async Task<List<ListTeam>> AddTeam(List<AddTeam> model)
        {
            var sp = "SP_InsertPlayerToTeam";
            DynamicParameters parameter = new DynamicParameters();
            List<ListTeam> team = new List<ListTeam>();
            foreach (var item in model)
            {
                parameter.Add("position", item.position);
                parameter.Add("mainskill", item.mainSkill);
                parameter.Add("numberofplayers", item.numberOfPlayers);
                var result = await sqlConnection.QueryAsync<ListBestPlayer>(sp, parameter, commandType: cmd);
                if (result != null)
                {
                    foreach (var player in result)
                    {
                        var Data = new ListTeam();

                        Data.name = player.name;
                        Data.position = player.position;
                        var ins = new FormPlayerSkills()
                        {
                            skill = player.skill,
                            value = player.value,
                        };
                        Data.playerSkills.Add(ins);
                        team.Add(Data);
                    }
                }
            }
            return team;
        }

        // TODO: Check Validation Data Position
        public async Task<int> ValidationPosition(string position)
        {
            parameters.Add("name", position);
            parameters.Add("id", DbType.Int32, direction: ParameterDirection.Output);
            var result = await sqlConnection.ExecuteAsync("[SP_CheckPosition]", parameters, commandType: cmd);
            var id = parameters.Get<int>("id");
            return id;
        }


        // TODO: Check Validation Data Skill
        public async Task<int> ValidationSkill(string mainSkill)
        {
            parameters.Add("name", mainSkill);
            parameters.Add("id", DbType.Int32, direction: ParameterDirection.Output);
            var result = await sqlConnection.ExecuteAsync("[SP_CheckSkill]", parameters, commandType: cmd);
            var id = parameters.Get<int>("id");
            return id;
        }
    }
}
