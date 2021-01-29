using System.Collections.Generic;
using System.Linq;

public class MatchedProfile
{
    public Profile matchedProfile1;
    public Profile matchedProfile2;
    public List<InterestTemplate> commonInterests;

    public MatchedProfile(Profile profile1, Profile profile2)
    {
        matchedProfile1 = profile1;
        matchedProfile2 = profile2;

        commonInterests = profile1.Interests.Where(interest => profile2.Interests.Contains(interest)).ToList();
    }
}