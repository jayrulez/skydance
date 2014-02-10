using BillBox.Common;

namespace BillBox.Models.Repository
{
    interface ISubscriberRepository
    {
        /// <summary>
        /// Returns a Response object with a Subcriber from the Subscriber Repository that matches the specified SubscirberId
        /// </summary>
        /// <param name="SubscriberId">The Id of the Subscriber to retreive</param>
        /// <returns></returns>
        IResponse<Subscriber> GetSubscriber(int SubscriberId);

        /// <summary>
        /// Returns a Response object with a Subcriber from the Subscriber Repository that matches the specified SubscirberId
        /// </summary>
        /// <param name="Name">The Name of the Subscriber to retreive</param>
        /// <returns></returns>
        IResponse<Subscriber> GetSubscriber(string Name);

        /// <summary>
        /// Returns a Response object with the list of Subcribers in the Subcriber Repository
        /// </summary>
        /// <returns></returns>
        IResponse<Subscriber> GetSubscribers();

        /// <summary>
        /// Returns a Response object with a paged list of Subscribers from the Subscriber Repository in a Response object.
        /// </summary>
        /// <param name="PageNumber">The page number to retrieve</param>
        /// <param name="PageSize">The size of the page to retrieve</param>
        /// <returns></returns>
        IResponse<Subscriber> GetSubscribers(int PageNumber, int PageSize);

        /// <summary>
        /// Retruns a Response object with the list of subsciber ojects with only the SubscriberId and OperatingNames populated
        /// </summary>
        /// <returns></returns>
        IResponse<Subscriber> GetSubscribersName();

        /// <summary>
        /// Retruns a Response object with a paged list of subsciber ojects with only the SubscriberId and OperatingNames populated
        /// </summary>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        IResponse<Subscriber> GetSubscribersName(int PageNumber, int PageSize);
        
        /// <summary>
        /// Returns a Response object with the list of payments for the specified subscriber
        /// </summary>
        /// <param name="SubscriberId"></param>
        /// <returns></returns>
        IResponse<Subscriber> GetSubscriberPayments(int SubscriberId);

        /// <summary>
        /// Returns a Response object with a list of payments for the specified subscriber
        /// </summary>
        /// <param name="SubscriberId"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        IResponse<Subscriber> GetSubscriberPayments(int SubscriberId, int PageNumber, int PageSize);

        /// <summary>
        /// Returns a Response object with a list of Captured Fields for the specified subscriber
        /// </summary>
        /// <param name="SubscriberId"></param>
        /// <returns></returns>
        IResponse<CaptureField> GetSubscriberCapturedFields(int SubscriberId);

        /// <summary>
        /// Add a Subscriber to the Subscriber Repository
        /// </summary>
        /// <param name="Subscriber">The Subscriber object to be added</param>
        /// <returns></returns>
        IResponse<bool> AddSubscriber(Subscriber Subscriber);

        /// <summary>
        /// Update a Subscriber in the Subscriber Repository
        /// </summary>
        /// <param name="Subscriber"></param>
        /// <returns></returns>
        IResponse<bool> UpdateSubscriber(Subscriber Subscriber);
    
    }
}
