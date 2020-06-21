using System.Collections.Generic;
using NorusContract.Domain.Validation;

namespace NorusContract.Domain.Entities
{
  public abstract class EntityBase
  {
    private List<BusinessRule> _brokenRules = new List<BusinessRule>();

    protected abstract void Validate();

    protected void AddBrokenRule(BusinessRule businessRule)
    {
      _brokenRules.Add(businessRule);
    }

    public bool IsValid()
    {
      _brokenRules.Clear();
      Validate();
      return (_brokenRules.Count == 0);
    }

    public void ThrowExceptionIfInvalid()
    {
      if (!IsValid())
      {
        throw new BusinessException(_brokenRules);
      }
    }
  }
}