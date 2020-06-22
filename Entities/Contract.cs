using NorusContract.Domain.Validation;
using System;

namespace NorusContract.Domain.Entities
{  
  public class Contract : EntityBase
  {
    public int ContractId { get; set; }
    public string CustomerName { get; set; }
    public string ContractType { get; set; } // "BUY" || "SELL"
    public int Amount { get; set; }
    public float NegotiatedValue { get; set; }
    public DateTime ContractBegin { get; set; }
    public int? AmountMonths { get; set; }
    public string PdfDocumentURI { get; set; }

    protected override void Validate()
    {
      if (string.IsNullOrWhiteSpace(CustomerName))
      {
        AddBrokenRule(new BusinessRule("Customer name is invalid"));
      }
      
      if (string.IsNullOrWhiteSpace(ContractType)|| (ContractType.Equals("SELL") == false && ContractType.Equals("BUY") == false))
      {
        AddBrokenRule(new BusinessRule("Contract type is invalid"));
      }

      if (Amount < 1)
      {
        AddBrokenRule(new BusinessRule("Amount is invalid"));
      }

      if (NegotiatedValue < 0.0f)
      {
        AddBrokenRule(new BusinessRule("Negotiated value is invalid"));
      }

      if (ContractBegin == null)
      {
        AddBrokenRule(new BusinessRule("Contract begin is invalid"));
      }

      if (AmountMonths != null && AmountMonths < 1)
      {
        AddBrokenRule(new BusinessRule("Amount months is invalid"));
      }

      if (string.IsNullOrWhiteSpace(PdfDocumentURI))
      {
        AddBrokenRule(new BusinessRule("Pdf document URI is invalid"));
      }
    }
  }
}