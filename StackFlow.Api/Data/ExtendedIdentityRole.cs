using Microsoft.AspNetCore.Identity;

namespace StackFlow.Api.Data
{
  public class ExtendedIdentityRole : IdentityRole<string>
  {
    public ExtendedIdentityRole() { }

    public string Description { get; set; } = "";
  }
}