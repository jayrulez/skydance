using BillBox.Common;

namespace BillBox.Models.Repository
{
    interface IAgentRepository
    {
        /// <summary>
        /// Returns a specified Agent from the AgentRepository in a generic Response object.
        /// </summary>
        /// <param name="AgentId">the Agent unique identifer</param>
        /// <returns></returns>
        IResponse<Agent> GetAgent(int AgentId);

        /// <summary>
        /// Returns a specified Agent from the AgentRepository in a generic Response object based on the specified Agent Name.
        /// The first agent matching the name is returned.
        /// </summary>
        /// <param name="AgentName">the Agent unique Name</param>
        IResponse<Agent> GetAgent(string Name);

        /// <summary>
        /// Returns a list of Agents from the AgentRepository in a generic Response object that name starts with the specified name
        /// </summary>
        /// <param name="Name">the name of the agent</param>
        /// <returns></returns>
        IResponse<Agent> GetAgents(string Name);

        /// <summary>
        /// Return an Agent Branche that matches the specified branch id.
        /// </summary>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        IResponse<AgentBranch> GetAgentBranch(int BranchId);

        /// <summary>
        /// Returns a list of AgentBranches that matches the specified Agent Id
        /// </summary>
        /// <param name="AgentId">The Agent for the branches to return</param>
        /// <returns></returns>
        IResponse<AgentBranch> GetAgentBranches(int AgentId);

        /// <summary>
        /// Returns an agent along with its branches based on the specifed Agent Id
        /// </summary>
        /// <param name="AgentId">The Agent Id to fetch</param>
        /// <returns></returns>
        IResponse<Agent> GetAgentWithBranches(int AgentId);
        
        /// <summary>
        /// Add an Agent to the Agent repository
        /// </summary>
        /// <param name="Agent">The Agent object to be added to the repository</param>
        /// <returns></returns>
        IResponse<bool> AddAgent(Agent Agent);

        /// <summary>
        /// Update an agent in the agent repository. Returns true on success and false on failure
        /// </summary>
        /// <param name="Agent">the Agent Id</param>
        /// <returns></returns>
        IResponse<bool> UpdateAgent(Agent Agent);

        /// <summary>
        /// Add an AgentBranch to the Agent repository
        /// </summary>
        /// <param name="AgentBranch">The AgentBranch object to be added to the repository</param>
        /// <returns></returns>
        IResponse<bool> AddAgentBranch(AgentBranch AgentBranch);

        /// <summary>
        /// Update an Agent Branch in the agent repository. Returns true on success and false on failure
        /// </summary>
        /// <param name="AgentBranch">the AgentBranch object to be updated in the repository</param>
        /// <returns></returns>
        IResponse<bool> UpdateAgentBranch(AgentBranch AgentBranch);
    
    }
}
