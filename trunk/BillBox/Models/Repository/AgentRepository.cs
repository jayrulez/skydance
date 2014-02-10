using System;
using System.Collections.Generic;
using System.Linq;
using BillBox.Common;

namespace BillBox.Models.Repository
{
    /// <summary>
    /// Performs CRUD operations on the Agent Repository
    /// </summary>
    public class AgentRepository : BaseRepository, IAgentRepository
    {
        /// <summary>
        /// Returns a Response object with an Agent specified by AgentId from the Agent Repository.
        /// </summary>
        /// <param name="AgentId">the intended Agent Id</param>
        /// <returns></returns>
        public IResponse<Agent> GetAgent(int AgentId)
        {
            IResponse<Agent> response = new Response<Agent>();

            try
            {
                using (this.dbContext)
                {
                    var Agent = dbContext.Agents.Find(AgentId);
                    if (Agent == null)
                        response.Error = ErrorCode.AgentNotFound;
                    else
                        response.Result = Agent;
                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        ///  Returns a Response object with an Agent specified by AgentId from the Agent Repository.
        /// The first agent matching the name is returned.
        /// </summary>
        /// <param name="AgentName">the intended Agent unique Name</param>
        public IResponse<Agent> GetAgent(string AgentName)
        {
            IResponse<Agent> response = new Response<Agent>();

            try
            {
                using (this.dbContext)
                {
                    var Agent = dbContext.Agents.FirstOrDefault(a => a.Name == AgentName);
                    if (Agent == null)
                        response.Error = ErrorCode.AgentNotFound;
                    else
                        response.Result = Agent;
                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with all the Agents in the Agent Repository.
        /// </summary>
        /// <returns></returns>
        public IResponse<Agent> GetAgents()
        {
            IResponse<Agent> response = new Response<Agent>();

            try
            {
                using (this.dbContext)
                {
                    var agents = dbContext.Agents.ToList();

                    if (agents.Count() > 0)
                    {
                        response.Results = agents;
                    }
                    else
                    {
                        response.Error = ErrorCode.AgentNotFound;
                    }
                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a page of Agents from the Agent Repository.
        /// </summary>
        /// <param name="PageNumber">the page number to fetch</param>
        /// <param name="PageSize">the size of the page to fetch</param>
        /// <returns></returns>
        public IResponse<Agent> GetAgents(int PageNumber, int PageSize)
        {
            IResponse<Agent> response = new Response<Agent>();

            try
            {
                using (this.dbContext)
                {
                    var agents = dbContext.Agents
                        .ToList()
                        .Skip((PageNumber - 1) * PageSize)
                        .Take(PageSize);
                    

                    if (agents.Count() > 0)
                    {
                        response.Results = agents.ToList();
                    }
                    else
                    {
                        response.Error = ErrorCode.AgentNotFound;
                    }
                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a  list of Agent Branches that matches the specified Agent Id.
        /// </summary>
        /// <param name="AgentId">The Agent Id associated with the branches to fetch</param>
        /// <returns></returns>
        public IResponse<AgentBranch> GetAgentBranches(int AgentId)
        {
            IResponse<AgentBranch> response = new Response<AgentBranch>();

            try
            {
                using (this.dbContext)
                {

                    var agentBranches = dbContext.AgentBranches.ToList().Where(ab => ab.AgentId == AgentId);

                    if (agentBranches.Count() > 0)
                    {
                        response.Results = agentBranches.ToList();
                    }
                    else
                    {
                        response.Error = ErrorCode.AgentNotFound; 
                    }
                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a page of Agent Branches that matches the specified Agent Id.
        /// </summary>
        /// <param name="AgentId">The Agent Id associated with the branches to fetch</param>
        /// <param name="PageNumber">the page number to fetch</param>
        /// <param name="PageSize">the size of the page to fetch</param>
        /// <returns></returns>
        public IResponse<AgentBranch> GetAgentBranches(int AgentId, int PageNumber, int PageSize)
        {
            IResponse<AgentBranch> response = new Response<AgentBranch>();

            try
            {
                using (this.dbContext)
                {

                    var agentBranches = dbContext.AgentBranches
                        .ToList()
                        .Where(ab => ab.AgentId == AgentId)
                        .Skip((PageNumber - 1) * PageSize)
                        .Take(PageSize);

                    if (agentBranches.Count() > 0)
                    {
                        response.Results = agentBranches.ToList();
                    }
                    else
                    {
                        response.Error = ErrorCode.AgentNotFound;
                    }
                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        ///  Returns a Response object with an Agent and its branches specified by the AgentId from the Agent Repository.
        /// </summary>
        /// <param name="AgentId">the intended Agent Id</param>
        /// <returns></returns>
        public IResponse<Agent> GetAgentWithBranches(int AgentId)
        {
            IResponse<Agent> response = new Response<Agent>();

            try
            {
                using (this.dbContext)
                {

                    var agent = dbContext.Agents.Find(AgentId);

                    if (agent == null)
                    {
                        response.Error = ErrorCode.AgentNotFound;
                    }
                    else
                    {                        
                        agent.AgentBranches.ToList();
                        response.Result = agent;
                    }
                }
            }
            catch
            {

                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Add an Agent to the Agent repository and returns a Response object with the Result property set to true if the operation was successful otherwise it is set to false.
        /// </summary>
        /// <param name="Agent">The Agent object to be added to the Agent Repository</param>
        /// <returns></returns>
        public IResponse<bool> AddAgent(Agent Agent)
        {
            IResponse<bool> response = new Response<bool>();

            try
            {
                using (this.dbContext)
                {
                    this.dbContext.Agents.Add(Agent);
                    var result = this.dbContext.SaveChanges();

                    /*Check the number of rows affected by the operation. 0 means the operation failed*/
                    if (result == 0)
                        response.Error = ErrorCode.DbError; //to be changed
                    else
                        response.Result = true;
                }
            }
            catch (Exception ex)
            {
                ex = ex.GetBaseException();

                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    if (ex.Message.Contains(Util.GetAppSetting("UKViolation_EmailAddress")))
                    {
                        response.Error = ErrorCode.DuplicateEmailAddress;
                    }                    
                    else
                    {
                        response.Error = ErrorCode.DbError;
                    }
                }
                else if (ex.Message.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint"))
                {
                    response.Error = ErrorCode.FKError;
                }
                else if (ex.Message.Contains("Validation failed for one or more entities"))
                {
                    response.Error = ErrorCode.DBEntityValidationError;
                }
                else
                {
                    response.Error = ErrorCode.DbError;
                }
            }

            return response;
        }

        /// <summary>
        /// Update an Agent in the Agent Repository and returns a Response object with the Result property set to true if the update was successful otherwise it is set to false.
        /// </summary>
        /// <param name="Agent">the Agent Id</param>
        /// <returns></returns>
        public IResponse<bool> UpdateAgent(Agent Agent)
        {
            IResponse<bool> response = new Response<bool>();

            try
            {
                using (this.dbContext)
                {
                    this.dbContext.Agents.Attach(Agent);
                    var entry = this.dbContext.Entry(Agent);
                    entry.State = System.Data.EntityState.Modified;


                    var result = this.dbContext.SaveChanges();

                    if (result > 0)
                        response.Result = true;
                    else
                        response.Error = ErrorCode.AgentNotFound;
                }
            }
            catch (Exception ex)
            {
                ex = ex.GetBaseException();

                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    if (ex.Message.Contains(Util.GetAppSetting("UKViolation_EmailAddress")))
                    {
                        response.Error = ErrorCode.DuplicateEmailAddress;
                    }
                    else
                    {
                        response.Error = ErrorCode.DbError;
                    }
                }
                else if (ex.Message.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint"))
                {
                    response.Error = ErrorCode.FKError;
                }
                else if (ex.Message.Contains("Validation failed for one or more entities"))
                {
                    response.Error = ErrorCode.DBEntityValidationError;
                }
                else
                {
                    response.Error = ErrorCode.DbError;
                }
            }

            return response;
        }

        /// <summary>
        /// Add an Agent Branch to the Agent repository and returns a Response object with the Result property set to true if the operation was successful otherwise it is set to false.
        /// </summary>
        /// <param name="AgentBranch">The AgentBranch object to be added to the Agent Repository</param>
        /// <returns></returns>
        public IResponse<bool> AddAgentBranch(AgentBranch AgentBranch)
        {
            IResponse<bool> response = new Response<bool>();

            try
            {
                using (this.dbContext)
                {
                    this.dbContext.AgentBranches.Add(AgentBranch);
                    var result = this.dbContext.SaveChanges();

                    /*Check the number of rows affected by the operation. 0 means the operation failed*/
                    if (result == 0)
                        response.Error = ErrorCode.DbError; //to be changed
                    else
                        response.Result = true;
                }
            }
            catch (Exception ex)
            {
                ex = ex.GetBaseException();

                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    if (ex.Message.Contains(Util.GetAppSetting("UKViolation_EmailAddress")))
                    {
                        response.Error = ErrorCode.DuplicateEmailAddress;
                    }
                    else
                    {
                        response.Error = ErrorCode.DbError;
                    }
                }
                else if (ex.Message.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint"))
                {
                    response.Error = ErrorCode.FKError;
                }
                else if (ex.Message.Contains("Validation failed for one or more entities"))
                {
                    response.Error = ErrorCode.DBEntityValidationError;
                }
                else
                {
                    response.Error = ErrorCode.DbError;
                }
            }

            return response;
        }

        /// <summary>
        /// Add an Agent Branch to the Agent repository and returns a Response object with the Result property set to true if the operation was successful otherwise it is set to false.
        /// <param name="AgentBranch">the AgentBranch object to be updated in the Agent Repository</param>
        /// <returns></returns>
        public IResponse<bool> UpdateAgentBranch(AgentBranch AgentBranch)
        {
            IResponse<bool> response = new Response<bool>();

            try
            {
                using (this.dbContext)
                {
                    this.dbContext.AgentBranches.Attach(AgentBranch);
                    var entry = this.dbContext.Entry(AgentBranch);
                    entry.State = System.Data.EntityState.Modified;


                    var result = this.dbContext.SaveChanges();

                    if (result > 0)
                        response.Result = true;
                    else
                        response.Error = ErrorCode.AgentNotFound;
                }
            }
            catch (Exception ex)
            {
                ex = ex.GetBaseException();

                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    if (ex.Message.Contains(Util.GetAppSetting("UKViolation_EmailAddress")))
                    {
                        response.Error = ErrorCode.DuplicateEmailAddress;
                    }
                    else
                    {
                        response.Error = ErrorCode.DbError;
                    }
                }
                else if (ex.Message.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint"))
                {
                    response.Error = ErrorCode.FKError;
                }
                else if (ex.Message.Contains("Validation failed for one or more entities"))
                {
                    response.Error = ErrorCode.DBEntityValidationError;
                }
                else
                {
                    response.Error = ErrorCode.DbError;
                }
            }

            return response;
        }
        
    }
}