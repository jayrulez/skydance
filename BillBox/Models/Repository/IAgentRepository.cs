using BillBox.Common;

namespace BillBox.Models.Repository
{
    interface IAgentRepository
    {
        /// <summary>
        /// Returns a Response object with an Agent specified by AgentId from the Agent Repository.
        /// </summary>
        /// <param name="AgentId">the intended Agent Id</param>
        /// <returns></returns>
        IResponse<Agent> GetAgent(int AgentId);

        /// <summary>
        ///  Returns a Response object with an Agent specified by AgentId from the Agent Repository.
        /// The first agent matching the name is returned.
        /// </summary>
        /// <param name="AgentName">the intended Agent unique Name</param>
        IResponse<Agent> GetAgent(string AgentName);

        /// <summary>
        /// Returns a Response object with all the Agents in the Agent Repository.
        /// </summary>
        /// <returns></returns>
        IResponse<Agent> GetAgents();

        /// <summary>
        /// Returns a Response object with a page of Agents from the Agent Repository.
        /// </summary>
        /// <param name="PageNumber">the page number to fetch</param>
        /// <param name="PageSize">the size of the page to fetch</param>
        /// <returns></returns>
        IResponse<Agent> GetAgents(int PageNumber, int PageSize);
              
        /// <summary>
        /// Returns a Response object with a  list of Agent Branches that matches the specified Agent Id.
        /// </summary>
        /// <param name="AgentId">The Agent Id associated with the branches to fetch</param>
        /// <returns></returns>
        IResponse<AgentBranch> GetAgentBranches(int AgentId);

        /// <summary>
        /// Returns a Response object with a page of Agent Branches that matches the specified Agent Id.
        /// </summary>
        /// <param name="AgentId">The Agent Id associated with the branches to fetch</param>
        /// <param name="PageNumber">the page number to fetch</param>
        /// <param name="PageSize">the size of the page to fetch</param>
        /// <returns></returns>
        IResponse<AgentBranch> GetAgentBranches(int AgentId, int PageNumber, int PageSize);

        /// <summary>
        ///  Returns a Response object with an Agent and its branches specified by the AgentId from the Agent Repository.
        /// </summary>
        /// <param name="AgentId">the intended Agent Id</param>
        /// <returns></returns>
        IResponse<Agent> GetAgentWithBranches(int AgentId);
        
        /// <summary>
        /// Add an Agent to the Agent repository and returns a Response object with the Result property set to true if the operation was successful otherwise it is set to false.
        /// </summary>
        /// <param name="Agent">The Agent object to be added to the Agent Repository</param>
        /// <returns></returns>
        IResponse<bool> AddAgent(Agent Agent);

        /// <summary>
        /// Update an Agent in the Agent Repository and returns a Response object with the Result property set to true if the update was successful otherwise it is set to false.
        /// </summary>
        /// <param name="Agent">the Agent Id</param>
        /// <returns></returns>
        IResponse<bool> UpdateAgent(Agent Agent);

        /// <summary>
        /// Add an Agent Branch to the Agent repository and returns a Response object with the Result property set to true if the operation was successful otherwise it is set to false.
        /// </summary>
        /// <param name="AgentBranch">The AgentBranch object to be added to the Agent Repository</param>
        /// <returns></returns>
        IResponse<bool> AddAgentBranch(AgentBranch AgentBranch);

        /// <summary>
        /// Add an Agent Branch to the Agent repository and returns a Response object with the Result property set to true if the operation was successful otherwise it is set to false.
        /// <param name="AgentBranch">the AgentBranch object to be updated in the Agent Repository</param>
        /// <returns></returns>
        IResponse<bool> UpdateAgentBranch(AgentBranch AgentBranch);
    
    }
}
