using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Enum
{
    public enum OrderStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        Pending = 10,

        /// <summary>
        /// Unpaid
        /// </summary>
        Unpaid = 15,

        /// <summary>
        /// Processing
        /// </summary>
        Processing = 20,

        /// <summary>
        /// Awaiting courier pick up
        /// </summary>
        ReadyForShipment = 22,

        /// <summary>
        /// Unboard with courier
        /// </summary>
        OnboardWithCourier = 23,

        /// <summary>
        /// Complete
        /// </summary>
        Complete = 30,

        /// <summary>
        /// Cancelled
        /// </summary>
        Cancelled = 40
    }
}
