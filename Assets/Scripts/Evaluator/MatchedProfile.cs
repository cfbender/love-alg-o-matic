using System.Collections.Generic;
using System.Linq;

public class MatchedProfile
{
    public Profile Profile1;
    public Profile Profile2;
    public EvaluationResult EvaluationResult;

    public MatchedProfile(Profile profile1, Profile profile2, EvaluationResult evaluationResult)
    {
        Profile1 = profile1;
        Profile2 = profile2;
        EvaluationResult = evaluationResult;
    }
}