using System.Collections.Generic;
using System.Linq;

public static class Evaluator
{
  // Returns an IEnumerable of different matches that can be counted for score
  public static IEnumerable<string> GetMatches(Profile p1, Profile p2)
  {
    return p1.Interests.Where(interest => p2.Interests.Contains(interest)).Select(interest => interest.name);
  } 
}
