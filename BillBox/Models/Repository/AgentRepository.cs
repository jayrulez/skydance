using System;
using System.Collections.Generic;
using System.Linq;
using BillBox.Common;

namespace BillBox.Models.Repository
{
    public class AgentRepository : BaseRepository, IAgentRepository
    {
        /// <summary>
        /// Returns a specified Agent from the AgentRepository in a generic Response object.
        /// </summary>
        /// <param name="AgentId">the Agent unique identifer</param>
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
        /// Returns a specified Agent from the AgentRepository in a generic Response object based on the specified Agent Name.
        /// The first agent matching the name is returned.
        /// </summary>
        /// <param name="AgentName">the Agent unique Name</param>
        /// <returns></returns>
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
        /// Returns a list of Agents from the AgentRepository in a generic Response object that name starts with the specified name
        /// </summary>
        /// <param name="Name">the name of the agent</param>
        /// <returns></returns>
        public IResponse<Agent> GetAgents(string Name)
        {
            IResponse<Agent> response = new Response<Agent>();

            try
            {
                using (this.dbContext)
                {
                    var agents = dbContext.Agents.ToList().Where(a => a.Name.StartsWith(Name));
                    if (agents == null)
                        response.Error = ErrorCode.AgentNotFound;
                    else
                    {
                        response.Results = agents.ToList();
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
        /// Return an Agent Branche that matches the specified branch id.
        /// </summary>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        public IResponse<AgentBranch> GetAgentBranch(int BranchId)
        {
            IResponse<AgentBranch> response = new Response<AgentBranch>();

            try
            {
                using (this.dbContext)
                {
                    var agentBranch = dbContext.AgentBranches.Find(BranchId);

                    if (agentBranch != null)
                    {
                        response.Result = agentBranch;
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
        /// Returns a list of AgentBranches that matches the specified Agent Id
        /// </summary>
        /// <param name="AgentId">The Agent for the branches to return</param>
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
        /// Returns an agent along with its branches based on the specifed Agent Id
        /// </summary>
        /// <param name="AgentId">The Agent Id to fetch</param>
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
        /// Add an Agent to the Agent repository
        /// </summary>
        /// <param name="Agent">The Agent object to be added to the repository</param>
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
        /// Update an agent in the agent repository. Returns true on success and false on failure
        /// </summary>
        /// <param name="Agent">the Agent object to be updated in the repository</param>
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
        /// Add an AgentBranch to the Agent repository
        /// </summary>
        /// <param name="AgentBranch">The AgentBranch object to be added to the repository</param>
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
        /// Update an Agent Branch in the agent repository. Returns true on success and false on failure
        /// </summary>
        /// <param name="AgentBranch">the AgentBranch object to be updated in the repository</param>
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