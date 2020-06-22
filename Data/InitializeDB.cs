using NorusContract.Domain.Entities;
using NorusContract.Services;
using System;

namespace NorusContract.Date
{
  public class InitializeDB
  {
    public static void Initialize(DataContext context)
    {
      context.Contracts.Add(new Contract {
        CustomerName = "Cliente A",
        ContractType = "SELL",
        Amount = 2,
        NegotiatedValue = 2500.0f,
        ContractBegin = DateTime.UtcNow,
        AmountMonths = 3,
        PdfDocumentURI = "http://localhost:5000/pdfs/41d98a2e-77c4-46cc-a146-b6081ca9aff4.pdf"
      });

      context.Contracts.Add(new Contract {
        CustomerName = "Cliente B",
        ContractType = "SELL",
        Amount = 3,
        NegotiatedValue = 3900.0f,
        ContractBegin = DateTime.UtcNow,
        AmountMonths = 6,
        PdfDocumentURI = "http://localhost:5000/pdfs/a59cb629-a05a-47e3-88e3-0c6223232ecb.pdf"
      });

      context.Contracts.Add(new Contract {
        CustomerName = "Cliente C",
        ContractType = "SELL",
        Amount = 4,
        NegotiatedValue = 8150.0f,
        ContractBegin = DateTime.UtcNow,
        AmountMonths = 7,
        PdfDocumentURI = "http://localhost:5000/pdfs/71c3f97a-cf19-486b-bba4-19240348d9d9.pdf"
      });

      context.Contracts.Add(new Contract {
        CustomerName = "Cliente D",
        ContractType = "BUY",
        Amount = 15,
        NegotiatedValue = 430.0f,
        ContractBegin = DateTime.UtcNow,
        AmountMonths = null,
        PdfDocumentURI = "http://localhost:5000/pdfs/e71b021f-fe71-44a2-8069-0984a807207c.pdf"
      });

      context.Contracts.Add(new Contract {
        CustomerName = "Cliente E",
        ContractType = "BUY",
        Amount = 3,
        NegotiatedValue = 9450.0f,
        ContractBegin = DateTime.UtcNow,
        AmountMonths = 12,
        PdfDocumentURI = "http://localhost:5000/pdfs/5c6183ce-9437-4f5d-b681-b01c7fe359a1.pdf"
      });

      context.Contracts.Add(new Contract {
        CustomerName = "Cliente F",
        ContractType = "BUY",
        Amount = 1,
        NegotiatedValue = 45.50f,
        ContractBegin = DateTime.UtcNow,
        AmountMonths = null,
        PdfDocumentURI = "http://localhost:5000/pdfs/ecd4d1af-e64d-457c-b300-2bd5944181bc.pdf"
      });

      /// USERS ///

      var _cryptographyService = new CryptographyService();

      context.Users.Add(new User {
        Name = "Diogo Ramos de Carvalho",
        UserName = "diogo",
        Email = "diogorcarvalho@gmail.com",
        HashPassword = _cryptographyService.GenerateHash("123456")
      });

      context.SaveChanges();
    }
  }
}