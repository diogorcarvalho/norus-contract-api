using System.Collections.Generic;
using NorusContract.Date;
using NorusContract.Domain.Entities;
using NorusContract.Domain.Validation;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NorusContract.Data.Repository
{
  public interface IContractRepository
  {
    void Save(Contract contract);
    Contract Get(int contractId);
    IEnumerable<Contract> GetAll();
    IEnumerable<Contract> Search(string keyword);
    void Patch(Contract contract);
    void Remove(Contract contract);
  }

  public class ContractRepository : IContractRepository
  {
    private readonly DataContext _context;

    public ContractRepository(DataContext context)
    {
      _context = context;
    }

    public Contract Get(int contractId)
    {
      return (from contract in _context.Contracts where contract.ContractId == contractId select contract).FirstOrDefault();
    }

    public IEnumerable<Contract> GetAll()
    {
      return (from contract in _context.Contracts select contract).ToList();
    }

    public void Patch(Contract contract)
    {
      _context.Entry<Contract>(contract).State = EntityState.Modified;
      _context.SaveChanges();
    }

    public void Remove(Contract contract)
    {
      _context.Set<Contract>().Remove(contract);
      _context.SaveChanges();
    }

    public void Save(Contract contract)
    {
      _context.Contracts.Add(contract);
      _context.SaveChanges();
    }

    public IEnumerable<Contract> Search(string keyword)
    {
      return (from contract in _context.Contracts
        where
          contract.CustomerName.ToLower().Contains(keyword.ToLower())
          || contract.NegotiatedValue.ToString().Equals(keyword)
          || (keyword.ToLower().Equals("compra") && contract.ContractType.Equals("BUY"))
          || (keyword.ToLower().Equals("venda") && contract.ContractType.Equals("SELL"))
        select contract).ToList();
    }
  }
}