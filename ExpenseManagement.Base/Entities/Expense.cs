
using ExpenseManagement.Base.Enums;
using ExpenseManagement.Base.Domain;

namespace ExpenseManagement.Base.Entities;

public class Expense : BaseEntity
{
    public long UserId { get; set; }
    public virtual User User { get; set; }

    public decimal Amount { get; set; }

    public long CategoryId { get; set; }
    public virtual ExpenseCategory ExpenseCategory { get; set; }

    public DateTime RequestDate { get; set; }
    public ExpenseStatus Status { get; set; }//requet oluşturulduğunda pending olur.
    public string? Location { get; set; } //masrafın yapılıdğı yer

    public long? ReviewById { get; set; }//request oluşturulduğunda hemen ilgilenen olmayacak.
    public virtual User? ReviewByUser { get; set; }

    public DateTime? ReviewDate { get; set; }
    public string? RejectionReason { get; set; }//ilk değerlendirme anında red edilme durumu olmadığından boşkalmalı.

    public virtual PaymentTransaction? PaymentTransaction { get; set; }
    public virtual ICollection<ExpenseDocument> ExpenseDocuments { get; set; }


}
