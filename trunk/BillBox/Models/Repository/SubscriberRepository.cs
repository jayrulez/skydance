using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillBox.Common;
//using BillBox.Models;

namespace BillBox.Models.Repository
{
    public class SubscriberRepository: BaseRepository, ISubscriberRepository
    {
        /// <summary>
        /// Returns a Response object with a Subcriber from the Subscriber Repository that matches the specified SubscirberId
        /// </summary>
        /// <param name="SubscriberId">The Id of the Subscriber to retreive</param>
        /// <returns></returns>
        public IResponse<Subscriber> GetSubscriber(int SubscriberId)
        {
            IResponse<Subscriber> response = new Response<Subscriber>();

            try
            {
                using (dbContext)
                {
                    var subscriber = dbContext.Subscribers.Find(SubscriberId);

                    if (subscriber == null)
                    {
                        response.Error = ErrorCode.SubscriberNotFound;
                    }
                    else
                    {
                        /*Join with parish to get the parish name*/
                        subscriber.Parish.ToString();

                        response.Result = subscriber;
                    }
                }
            }
            catch (Exception)
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a Subcriber from the Subscriber Repository that matches the specified SubscirberId
        /// </summary>
        /// <param name="Name">The Name of the Subscriber to retreive</param>
        /// <returns></returns>
        public IResponse<Subscriber> GetSubscriber(string Name)
        {
            IResponse<Subscriber> response = new Response<Subscriber>();

            try
            {
                using (dbContext)
                {
                    var subscriber = dbContext.Subscribers.FirstOrDefault(s =>s.OperatingName == Name);

                    if (subscriber == null)
                    {
                        response.Error = ErrorCode.SubscriberNotFound;
                    }
                    else
                    {
                        /*Join with parish to get the parish name*/
                        subscriber.Parish.ToString();

                        response.Result = subscriber;
                    }
                }
            }
            catch (Exception)
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with the list of Subcribers in the Subcriber Repository
        /// </summary>
        /// <returns></returns>
        public IResponse<Subscriber> GetSubscribers()
        {
            IResponse<Subscriber> response = new Response<Subscriber>();

            try
            {
                using (dbContext)
                {
                    var subscribers = dbContext.Subscribers.ToList();

                    if (subscribers.Count > 0)
                    {
                        response.Results = subscribers;                        
                    }
                    else
                    {
                        response.Error = ErrorCode.SubscriberNotFound;
                    }
                }
            }
            catch (Exception)
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }
       
        /// <summary>
        /// Returns a Response object with a list of Subscribers from the Subscriber Repository in a Response object based on the specified PageNumber and PageSize
        /// </summary>
        /// <param name="PageNumber">The page number to retrieve</param>
        /// <param name="PageSize">The size of the page to retrieve (number of rows)</param>
        /// <returns></returns>
        public IResponse<Subscriber> GetSubscribers(int PageNumber, int PageSize)
        {
            IResponse<Subscriber> response = new Response<Subscriber>();

            try
            {

            }
            catch (Exception)
            {

                throw;
            }

            return response;
        }
        
        /// <summary>
        /// Retruns a Response object with a list of subsciber ojects with only the SubscriberId and OperatingNames populated
        /// </summary>
        /// <returns></returns>
        public IResponse<Subscriber> GetSubscribersName()
        {
            IResponse<Subscriber> response = new Response<Subscriber>();

            try
            {
                using (dbContext)
                {
                    var subscribers = dbContext.Subscribers.ToList()
                        .Select(s => new Subscriber 
                        { 
                            SubscriberId = s.SubscriberId, 
                            OperatingName = s.OperatingName
                        });

                    if (subscribers.Count() > 0)
                    {
                        response.Results = subscribers.ToList();
                    }
                    else
                    {
                        response.Error = ErrorCode.SubscriberNotFound;
                    }
                }
            }
            catch (Exception)
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Retruns a Response object with a list of subsciber ojects with only the SubscriberId and OperatingNames populated
        /// </summary>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IResponse<Subscriber> GetSubscribersName(int PageNumber, int PageSize)
        {
            IResponse<Subscriber> response = new Response<Subscriber>();

            try
            {
                using (dbContext)
                {
                    var subscribers = dbContext.Subscribers.ToList()
                        .Select(s => new Subscriber
                        {
                            SubscriberId = s.SubscriberId,
                            OperatingName = s.OperatingName
                        });

                    if (subscribers.Count() > 0)
                    {
                        response.Results = subscribers
                            .Skip((PageNumber - 1) * PageSize)
                            .Take(PageSize)
                            .ToList();
                    }
                    else
                    {
                        response.Error = ErrorCode.SubscriberNotFound;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a list of payments for all subscribers
        /// </summary>
        /// <returns></returns>
        public IResponse<Subscriber> GetSubscribersPayments()
        {
            IResponse<Subscriber> response = new Response<Subscriber>();

            try
            {
                using (dbContext)
                {
                   
                }
            }
            catch (Exception)
            {

                throw;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a list of payments for all subscribers
        /// </summary>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IResponse<Subscriber> GetSubscribersPayments(int PageNumber, int PageSize)
        {
            IResponse<Subscriber> response = new Response<Subscriber>();

            try
            {
                using (dbContext)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a list of payments for the specified subscriber
        /// </summary>
        /// <param name="SubscriberId"></param>
        /// <returns></returns>
        public IResponse<Subscriber> GetSubscriberPayments(int SubscriberId)
        {
            IResponse<Subscriber> response = new Response<Subscriber>();

            try
            {
                using (dbContext)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a list of payments for the specified subscriber
        /// </summary>
        /// <param name="SubscriberId"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IResponse<Subscriber> GetSubscriberPayments(int SubscriberId, int PageNumber, int PageSize)
        {
            IResponse<Subscriber> response = new Response<Subscriber>();

            try
            {
                using (dbContext)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a list of Captured Fields for the specified subscriber
        /// </summary>
        /// <param name="SubscriberId"></param>
        /// <returns></returns>
        public IResponse<Subscriber> GetSubscriberCapturedFields(int SubscriberId)
        {
            IResponse<Subscriber> response = new Response<Subscriber>();

            try
            {
                using (dbContext)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }

            return response;
        }

        /// <summary>
        /// Add a Subscriber to the Subscriber Repository
        /// </summary>
        /// <param name="Subscriber">The Subscriber object to be added</param>
        /// <returns></returns>
        public IResponse<bool> AddSubscriber(Subscriber Subscriber)
        {
            IResponse<bool> response = new Response<bool>();

            try
            {
                using (dbContext)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }

            return response;
        }

        /// <summary>
        /// Update a Subscriber in the Subscriber Repository
        /// </summary>
        /// <param name="Subscriber"></param>
        /// <returns></returns>
        public IResponse<bool> UpdateSubscriber(Subscriber Subscriber)
        {
            IResponse<bool> response = new Response<bool>();

            try
            {
                using (dbContext)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }

            return response;
        }
    
    }
}