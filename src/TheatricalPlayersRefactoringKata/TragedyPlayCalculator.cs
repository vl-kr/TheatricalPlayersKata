using System;

namespace TheatricalPlayersRefactoringKata;

public class TragedyPlayCalculator : IPlayCalculator
{
    public int CalculateTotalAmount(Performance perf)
    {
        var thisAmount = 40000;
        if (perf.Audience > 30)
        {
            thisAmount += 1000 * (perf.Audience - 30);
        }
        return thisAmount;
    }

    public int CalculateVolumeCredits(Performance perf)
    {
        return Math.Max(perf.Audience - 30, 0);
    }
}
