using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transact.Base;

namespace Transact.Schema;

public class MoneyTransferRequest : BaseRequest
{
    public long FromAccountId { get; set; }
    public long ToAccountId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
}

public class MoneyTransferResponse : BaseResponse
{
    public long FromAccountId { get; set; }
    public long ToAccountId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public decimal? FeeAmount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string ReferenceNumber { get; set; }
}
