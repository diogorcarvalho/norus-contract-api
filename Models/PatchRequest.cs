using System;

namespace NorusContract.Api.Models
{
  public class PatchRequest
  {
    public int EntityId { get; set; }
    public string Field { get; set; }
    public string StringValue { get; set; }
    public int? IntValue { get; set; }
    public float? FloatValue { get; set; }
    public DateTime? DateTimeValue { get; set; }
  }

}