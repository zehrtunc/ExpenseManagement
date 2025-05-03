using ExpenseManagement.Base;
using ExpenseManagement.Base.Entities;


namespace ExpenseManagement.Schema;

public class CustomerAddressRequest : BaseRequest
{
    public long ExpenseId { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }
}

public class CustomerAddressResponse : BaseResponse
{
    public long ExpenseId { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }
}
