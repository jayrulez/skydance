using System;
using System.Linq;
using System.Data.Entity;

using BillBox.Common;
using System.Collections.Generic;

namespace BillBox.Models.Repository
{
    public class PaymentRepository : BaseRepository, IPaymentRepository 
    {
        /// <summary>
        /// Returns a Response object with a payment record, based on the specified Payment Id
        /// </summary>
        /// <param name="Id">The Id of the payment</param>
        /// <returns></returns>
        public IResponse<Payment> GetPayment(int Id)
        {
            IResponse<Payment> response = new Response<Payment>();

            try
            {
                using (this.dbContext)
                {

                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a payment record, based on the specified Invoice Number
        /// </summary>
        /// <param name="InvoiceNumber">The unique Invoice Number of the payment</param>
        /// <returns></returns>
        public IResponse<Payment> GetPaymentByInvoice(int InvoiceNumber)
        {
            IResponse<Payment> response = new Response<Payment>();

            try
            {
                using (this.dbContext)
                {

                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a list containg all the payments in the Payment Repository
        /// </summary>
        /// <returns></returns>
        public IResponse<Payment> GetPayments()
        {
            IResponse<Payment> response = new Response<Payment>();

            try
            {
                using (this.dbContext)
                {

                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a paged list of payments
        /// </summary>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IResponse<Payment> GetPayments(int PageNumber, int PageSize)
        {
            IResponse<Payment> response = new Response<Payment>();

            try
            {
                using (this.dbContext)
                {

                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a list containg all the payments in the Payment Repository belonging to the specified Subscriber
        /// </summary>
        /// <param name="SubscriberId">The subscriber Id</param>
        /// <returns></returns>
        public IResponse<Payment> GetSubscriberPayments(int SubscriberId)
        {
            IResponse<Payment> response = new Response<Payment>();

            try
            {
                using (this.dbContext)
                {

                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a paged list of payments in the Payment Repository belonging to the specified Subscriber
        /// </summary>
        /// <param name="SubscriberId"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IResponse<Payment> GetSubscriberPayments(int SubscriberId, int PageNumber, int PageSize)
        {
            IResponse<Payment> response = new Response<Payment>();

            try
            {
                using (this.dbContext)
                {

                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        ///  Returns a Response object with a list containg all the payments in the Payment Repository belonging to the specified Agent
        /// </summary>
        /// <param name="AgentId"></param>
        /// <returns></returns>
        public IResponse<Payment> GetAgentPayments(int AgentId)
        {
            IResponse<Payment> response = new Response<Payment>();

            try
            {
                using (this.dbContext)
                {

                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a paged list of payments in the Payment Repository belonging to the specified Agent
        /// </summary>
        /// <param name="AgentId"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IResponse<Payment> GetAgentPayments(int AgentId, int PageNumber, int PageSize)
        {
            IResponse<Payment> response = new Response<Payment>();

            try
            {
                using (this.dbContext)
                {

                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        ///  Returns a Response object with a list containg all the payments in the Payment Repository belonging to the specified Agent branch
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IResponse<Payment> GetAgentBranchPayments(int Id)
        {
            IResponse<Payment> response = new Response<Payment>();

            try
            {
                using (this.dbContext)
                {

                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a paged list of payments in the Payment Repository belonging to the specified Agent branch
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IResponse<Payment> GetAgentBranchPayments(int Id, int PageNumber, int PageSize)
        {
            IResponse<Payment> response = new Response<Payment>();

            try
            {
                using (this.dbContext)
                {

                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        ///  Returns a Response object with a list containg all the payments in the Payment Repository belonging to the specified User
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IResponse<Payment> GetUserPayments(int UserId)
        {
            IResponse<Payment> response = new Response<Payment>();

            try
            {
                using (this.dbContext)
                {

                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Returns a Response object with a paged list of payments in the Payment Repository belonging to the specified User
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IResponse<Payment> GetUserPayments(int UserId, int PageNumber, int PageSize)
        {
            IResponse<Payment> response = new Response<Payment>();

            try
            {
                using (this.dbContext)
                {

                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }
    }
}