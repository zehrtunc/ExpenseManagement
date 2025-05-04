
using ExpenseManagement.Base;

namespace ExpenseManagement.Schema;

public class BankAccountRequest : BaseRequest
{
    public long UserId { get; set; }
    public string AccountNumber { get; set; }
    public string IBAN { get; set; }
    public string BankName { get; set; }
    public string CurrencyCode { get; set; }
}

public class BankAccountResponse : BaseResponse
{
    public long UserId { get; set; }
    public string AccountNumber { get; set; }
    public string IBAN { get; set; }
    public string BankName { get; set; }
    public string CurrencyCode { get; set; }
}
