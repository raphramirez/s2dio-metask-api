using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Users
{
  public class RegisterUserDto
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Nickname { get; set; }
    public string Picture { get; set; }
  }
}