
using Transact.Base;

namespace Transact.Schema;

public class AccountRequest : BaseRequest
{
    public string Name { get; set; }
    public string CurrencyCode { get; set; } //Para birimi
    public long CustomerId { get; set; }

}

public class AccountResponse : BaseResponse
{
    public long CustomerId { get; set; }
    public string Name { get; set; }
    public int AccountNumber { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyCode { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }
}
