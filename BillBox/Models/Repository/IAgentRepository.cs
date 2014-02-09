using BillBox.Common;

namespace BillBox.Models.Repository
{
    interface IAgentRepository
    {
        IResponse<Agent> GetAgent(int AgentId);
        IResponse<Agent> GetAgent(string Name);
        IResponse<Agent> GetAgents(string Name);
        IResponse<AgentBranch> GetAgentBranches(int BranchId);
        IResponse<Agent> GetAgentWithBranches(int AgentId);
        IResponse<bool> AddAgent(Agent Agent);
        IResponse<bool> UpdateAgent(Agent Agent);

        IResponse<bool> AddAgentBranch(AgentBranch AgentBranch);
        IResponse<bool> UpdateAgentBranch(AgentBranch AgentBranch);
    }
}
