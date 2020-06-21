using System;
using System.Collections.Generic;
using System.Text;

namespace NorusContract.Domain.Validation
{
  public class BusinessException : Exception
  {
    public List<BusinessRule> BusinessRules { get; private set; }

    public BusinessException() { }

    public BusinessException(Dictionary.BusinessRules businessRule)
    {
      BusinessRules = new List<BusinessRule>
      {
        new BusinessRule(businessRule.ToString(), businessRule.GetBusinessRuleDescription())
      };
    }

    public BusinessException(Dictionary.BusinessRules businessRule, string ruleDescription)
    {
      BusinessRules = new List<BusinessRule>
      {
        new BusinessRule(businessRule.ToString(), ruleDescription)
      };
    }

    public BusinessException(string ruleDescription)
    {
      BusinessRules = new List<BusinessRule>
      {
        new BusinessRule(ruleDescription)
      };
    }

    public BusinessException(BusinessRule businessRule)
    {
      BusinessRules = new List<BusinessRule>
      {
        businessRule
      };
    }

    public BusinessException(IEnumerable<string> errors)
    {
      BusinessRules = new List<BusinessRule>();
      foreach (string error in errors)
      {
        BusinessRules.Add(new BusinessRule(error));
      }
    }

    public BusinessException(List<BusinessRule> businessRules)
    {
      BusinessRules = new List<BusinessRule>();
      BusinessRules.AddRange(businessRules);
    }

    public override string Message
    {
      get
      {
        StringBuilder errorMessage = new StringBuilder();
        foreach (BusinessRule businessRule in BusinessRules)
        {
          //errorMessage.Append(string.Format("{0};{1}", businessRule.RuleCode, businessRule.RuleDescription));
          errorMessage.Append(string.Format("{0}", businessRule.RuleDescription));
        }
        return errorMessage.ToString();
      }
    }
  }
}