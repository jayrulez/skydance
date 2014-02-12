using System;
using System.Collections.Generic;
using System.Linq;
using BillBox.Common;

namespace BillBox.Models.Repository
{
    public interface IPaymentRepository
    {
        /// <summary>
        /// Returns a Response object with a payment record, based on the specified Payment Id
        /// </summary>
        /// <param name="Id">The Id of the payment</param>
        /// <returns></returns>
        IResponse<Payment> GetPayment(int Id);

        /// <summary>
        /// Returns a Response object with a payment record, based on the specified Invoice Number
        /// </summary>
        /// <param name="InvoiceNumber">The unique Invoice Number of the payment</param>
        /// <returns></returns>
        IResponse<Payment> GetPaymentByInvoice(int InvoiceNumber);

        /// <summary>
        /// Returns a Response object with a list containg all the payments in the Payment Repository
        /// </summary>
        /// <returns></returns>
        IResponse<Payment> GetPayments();
        
        /// <summary>
        /// Returns a Response object with a paged list of payments
        /// </summary>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        IResponse<Payment> GetPayments(int PageNumber, int PageSize);

        /// <summary>
        /// Returns a Response object with a list containg all the payments in the Payment Repository belonging to the specified Subscriber
        /// </summary>
        /// <param name="SubscriberId">The subscriber Id</param>
        /// <returns></returns>
        IResponse<Payment> GetSubscriberPayments(int SubscriberId);

        /// <summary>
        /// Returns a Response object with a paged list of payments in the Payment Repository belonging to the specified Subscriber
        /// </summary>
        /// <param name="SubscriberId"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        IResponse<Payment> GetSubscriberPayments(int SubscriberId, int PageNumber, int PageSize);

        /// <summary>
        ///  Returns a Response object with a list containg all the payments in the Payment Repository belonging to the specified Agent
        /// </summary>
        /// <param name="AgentId"></param>
        /// <returns></returns>
        IResponse<Payment> GetAgentPayments(int AgentId);

        /// <summary>
        /// Returns a Response object with a paged list of payments in the Payment Repository belonging to the specified Agent
        /// </summary>
        /// <param name="AgentId"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        IResponse<Payment> GetAgentPayments(int AgentId, int PageNumber, int PageSize);

        /// <summary>
        ///  Returns a Response object with a list containg all the payments in the Payment Repository belonging to the specified Agent branch
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        IResponse<Payment> GetAgentBranchPayments(int Id);

        /// <summary>
        /// Returns a Response object with a paged list of payments in the Payment Repository belonging to the specified Agent branch
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        IResponse<Payment> GetAgentBranchPayments(int Id, int PageNumber, int PageSize);

        /// <summary>
        ///  Returns a Response object with a list containg all the payments in the Payment Repository belonging to the specified User
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IResponse<Payment> GetUserPayments(int UserId);

        /// <summary>
        /// Returns a Response object with a paged list of payments in the Payment Repository belonging to the specified User
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        IResponse<Payment> GetUserPayments(int UserId, int PageNumber, int PageSize);

        //IResponse<PaymentInfo> GetPaymentInfo(int PaymentId);

        //IResponse<PaymentCaptureField> GetPaymentCapturedFields(int PaymentId);

        //IResponse<PaymentPaymentTypeCaptureField> GetPaymentTypesCapturedFields(int PaymentId);

        //IResponse<bool> AddPayment(Payment Payment);

        //IResponse<bool> UpdatePayment(Payment Payment);
    }
}
