using System;
using System.ComponentModel;
using System.Reflection;

namespace NorusContract.Domain.Validation
{
  public static class Dictionary
  {
    public enum BusinessRules
    {
      [Description("This item cannot be deleted")]
      CANNOT_DELETE_ITEM,
      [Description("This item already exists")]
      ITEM_ALREADY_EXISTS,
      [Description("Not found.")]
      NOT_FOUND,
      [Description("This item cannot be changed.")]
      CANNOT_CHANGE_ITEM,
      [Description("This item cannot be created.")]
      CANNOT_CREATE_ITEM,
      [Description("Cannot use system email credentials.")]
      CANNOT_USE_SYSTEM_EMAIL_CREDENTIALS,
      [Description("There was a problem with payment gateway integration.")]
      THERE_WAS_A_PROBLEM_WITH_PAYMENT_GATEWAY,
      [Description("Query parameter error")]
      QUERY_PARAMETER_ERROR,
      [Description("Invalid value")]
      INVALID_VALUE
    }

    public static string GetBusinessRuleDescription(this BusinessRules value)
    {
      FieldInfo field;
      DescriptionAttribute attribute;
      string result;

      field = value.GetType().GetField(value.ToString());
      attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
      result = attribute != null ? attribute.Description : string.Empty;

      return result;
    }
  }
}