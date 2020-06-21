namespace NorusContract.Domain.Entities
{
  public class User : EntityBase
  {
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string HashPassword { get; set; }

    protected override void Validate() {}
  }
}