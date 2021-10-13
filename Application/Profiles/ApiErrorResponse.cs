using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Profiles
{
  public class ApiErrorResponse
  {
    public string Title { get; set; }
    public string Instance { get; set; }
    public int Status { get; set; }
    public string[] Errors { get; set; }
  }
}