using Dapper;
using PlayerTeamGeneratorWeb.API.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerTeamGeneratorWeb.API.Repositories
{
    public class PlayerTeamRepository
    {
        private readonly SqlConnection sqlConnection;
        DynamicParameters parameters = new DynamicParameters();
        private readonly CommandType cmd = CommandType.StoredProcedure;

        public PlayerTeamRepository (ConnectionStrings connection)
        {
            sqlConnection = new SqlConnection(connection.Value);
        }

        //TODO: View Data List Players
        public async Task<GridDataListPlayers> GetDataListPlayers(string filter, string sort, int start, int pageLimit)
        {
            var sp = "SP_GetListPlayers";
            parameters.Add("Condition", filter);
            parameters.Add("Sort", sort);
            parameters.Add("Start", start);
            parameters.Add("PageLimit", pageLimit);
            parameters.Add("TotalCount", DbType.Int32, direction: ParameterDirection.Output);

            var result = await sqlConnection.QueryAsync<ListPlayer>(sp, parameters, commandType: cmd);
            var total = parameters.Get<int>("TotalCount");
            List<GetAllPlayers> res = new List<GetAllPlayers>(total);
            var group = result.GroupBy( w => w.id).ToList();
            foreach (var lists in group)
            {
                var Data = new GetAllPlayers();
                foreach (var player in lists)
                {
                    Data.id = player.id;
                    Data.name = player.name;
                    Data.position = player.position;

                    if (Data.id.Equals(player.playerId))
                    {
                        var ins = new PlayerSkills()
                        {
                            id = player.idSkill,
                            skill = player.skill,
                            value = player.value,
                            playerId = player.playerId
                        };
                        Data.playerSkills.Add(ins);
                    }
                }
                res.Add(Data);
            }
            var list = new GridDataListPlayers
            {
                Data = res,
                Total = total
            };
            return list;
        }

        // TODO: Insert Data Player
        public async Task<int> InsertPlayer(FormPlayer model)
        {
            var sp = "SP_InsertPlayer";
            parameters.Add("name", model.name);
            parameters.Add("position", model.position);
            parameters.Add("id", DbType.Int32, direction: ParameterDirection.Output);
            var result = await sqlConnection.ExecuteAsync(sp, parameters, commandType: cmd);
            if (result >= 0)
                return parameters.Get<int>("id");
            else
                return result;
        }

        // TODO: Update Data Player
        public async Task<int> UpdatePlayer(int playerId, FormPlayer model)
        {
            var sp = "SP_UpdatePlayer";
            parameters.Add("id", playerId);
            parameters.Add("name", model.name);
            parameters.Add("position", model.position);
            var result = await sqlConnection.ExecuteAsync(sp, parameters, commandType: cmd);
            return result;
        }

        // TODO: Insert Data Player Skill
        public async Task<int> InsertPlayerSkill(int id, FormPlayerSkills model)
        {
            var sp = "SP_InsertPlayerSkill";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("id", id);
            parameter.Add("name", model.skill);
            parameter.Add("value", model.value);
            var result = await sqlConnection.ExecuteAsync(sp, parameter, commandType: cmd);
            return result;
        }

        // TODO: Delete Data Player
        public async Task<int> DeletePlayer(int playerId)
        {
            var sp = "SP_DeletePlayer";
            parameters.Add("id", playerId);
            var result = await sqlConnection.ExecuteAsync(sp, parameters, commandType: cmd);
            return result;
        }
    }
}
