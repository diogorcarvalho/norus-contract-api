using System;

namespace NorusContract.Domain.Validation
{
  public class BusinessRule : Exception
  {
    public virtual string RuleCode { get; private set; }

    public string RuleDescription { get; set; }

    public BusinessRule(string ruleDescription)
    {
      RuleCode = string.Empty;
      RuleDescription = ruleDescription;
    }

    public BusinessRule(string ruleCode, string ruleDescription)
    {
      RuleCode = ruleCode;
      RuleDescription = ruleDescription;
    }
  }
}