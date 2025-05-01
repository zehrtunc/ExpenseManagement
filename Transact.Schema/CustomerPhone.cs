using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transact.Base;

namespace Transact.Schema;

public class CustomerPhoneRequest : BaseRequest
{
    public long CustomerId { get; set; }
    public string CountryCode { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsDefault { get; set; }
}

public class CustomerPhoneResponse : BaseResponse
{
    public long CustomerId { get; set; }
    public string CountryCode { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsDefault { get; set; }
}
