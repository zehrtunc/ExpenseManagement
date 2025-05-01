using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transact.Base;

namespace Transact.Schema;

public class EftTransactionRequest : BaseRequest
{
    public long FromAccountId { get; set; }
    public string ReceiverIban { get; set; }
    public string ReceiverName { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string? PaymentCategoryCode { get; set; }
}

public class EftTransactionResponse : BaseResponse
{
    public long FromAccountId { get; set; }
    public string ReceiverIban { get; set; }
    public string ReceiverName { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public decimal? FeeAmount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string ReferenceNumber { get; set; }
    public string? PaymentCategoryCode { get; set; }
}
