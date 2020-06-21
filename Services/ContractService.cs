using System;
using System.Collections.Generic;
using NorusContract.Api.Models;
using NorusContract.Data.Repository;
using NorusContract.Domain.Entities;
using NorusContract.Domain.Validation;

namespace NorusContract.Services
{
  public interface IContractService
  {
    Contract Save(Contract contract);
    Contract Get(int contractId);
    IEnumerable<Contract> GetAll();
    IEnumerable<Contract> Search(string keyword);
    Contract Patch(PatchRequest patch);
    void Remove(int contractId);
  }

  public class ContractService : IContractService
  {
    protected readonly IContractRepository _contractRepository;

    public ContractService(IContractRepository contractRepository)
    {
      _contractRepository = contractRepository;        
    }

    public Contract Get(int contractId)
    {
      return _contractRepository.Get(contractId);
    }

    public IEnumerable<Contract> GetAll()
    {
      return _contractRepository.GetAll();
    }

    public Contract Patch(PatchRequest patch)
    {
      Console.WriteLine(">>> patch.EntityId: " + patch.EntityId.ToString());

      var _contract = _contractRepository.Get(patch.EntityId);

      if (_contract == null) throw new BusinessException("You must enter a valid contract id");

      patch.Field = patch.Field.ToLower();

      if (
        patch.Field.Equals("contractname") == false
        && patch.Field.Equals("contracttype") == false
        && patch.Field.Equals("amount") == false
        && patch.Field.Equals("negotiatedvalue") == false
        && patch.Field.Equals("contractbegin") == false
        && patch.Field.Equals("amountmonths") == false
        && patch.Field.Equals("pdfdocumenturi") == false
      ) {
        throw new BusinessException("You must enter a valid field");
      }

      if (patch.Field.Equals("contractname"))
      {
        if (string.IsNullOrWhiteSpace(patch.StringValue)) throw new BusinessException("You must enter a valid contract name");
        
        try
        {
          _contract.ContractName = patch.StringValue;
          _contractRepository.Patch(_contract);
        }
        catch (Exception ex)
        {
          throw new BusinessException(ex.Message);
        }
      }

      if (patch.Field.Equals("contracttype"))
      {
        if (string.IsNullOrWhiteSpace(patch.StringValue) || (!patch.StringValue.Equals("SELL") && !patch.StringValue.Equals("BUY"))) throw new BusinessException("You must enter a valid contract type");
        
        try
        {
          _contract.ContractType = patch.StringValue;
          _contractRepository.Patch(_contract);
        }
        catch (Exception ex)
        {
          throw new BusinessException(ex.Message);
        }
      }

      if (patch.Field.Equals("amount"))
      {
        if (patch.IntValue == null || patch.IntValue < 1) throw new BusinessException("You must enter a valid amount");
        
        try
        {
          _contract.Amount = patch.IntValue.Value;
          _contractRepository.Patch(_contract);
        }
        catch (Exception ex)
        {
          throw new BusinessException(ex.Message);
        }
      }

      if (patch.Field.Equals("negotiatedvalue"))
      {
        if (patch.FloatValue == null || patch.FloatValue < 0.0f) throw new BusinessException("You must enter a valid negotiated value");
        
        try
        {
          _contract.NegotiatedValue = patch.FloatValue.Value;
          _contractRepository.Patch(_contract);
        }
        catch (Exception ex)
        {
          throw new BusinessException(ex.Message);
        }
      }

      if (patch.Field.Equals("contractbegin"))
      {
        if (patch.DateTimeValue == null) throw new BusinessException("You must enter a valid contract begin");
        
        try
        {
          _contract.ContractBegin = patch.DateTimeValue.Value;
          _contractRepository.Patch(_contract);
        }
        catch (Exception ex)
        {
          throw new BusinessException(ex.Message);
        }
      }

      if (patch.Field.Equals("amountmonths"))
      {
        if (patch.IntValue < 1) throw new BusinessException("You must enter a valid amount months");
        
        try
        {
          _contract.AmountMonths = patch.IntValue.Value;
          _contractRepository.Patch(_contract);
        }
        catch (Exception ex)
        {
          throw new BusinessException(ex.Message);
        }
      }

      if (patch.Field.Equals("pdfdocumenturi"))
      {
        if (string.IsNullOrWhiteSpace(patch.StringValue)) throw new BusinessException("You must enter a valid pdf document URI");
        
        try
        {
          _contract.PdfDocumentURI = patch.StringValue;
          _contractRepository.Patch(_contract);
        }
        catch (Exception ex)
        {
          throw new BusinessException(ex.Message);
        }
      }

      return _contract;
    }

    public void Remove(int contractId)
    {
      var _contract = _contractRepository.Get(contractId);

      if (_contract == null)  throw new BusinessException("You must enter a valid contract id");

      _contractRepository.Remove(_contract);
    }

    public Contract Save(Contract contract)
    {
      if (contract.IsValid() == false) contract.ThrowExceptionIfInvalid();

      _contractRepository.Save(contract);

      return contract;
    }

    public IEnumerable<Contract> Search(string keyword)
    {
      if (string.IsNullOrWhiteSpace(keyword)) return new List<Contract>();

      return _contractRepository.Search(keyword);
    }
  }
}