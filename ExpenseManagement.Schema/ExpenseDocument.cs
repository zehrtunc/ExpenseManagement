using ExpenseManagement.Base;


namespace ExpenseManagement.Schema;

public class ExpenseDocumentRequest : BaseRequest
{
    public long ExpenseId { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }
}

public class ExpenseDocumentResponse : BaseResponse
{
    public long ExpenseId { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }
}
