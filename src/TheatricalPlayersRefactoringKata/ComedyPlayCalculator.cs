using System;

namespace TheatricalPlayersRefactoringKata;

public class ComedyPlayCalculator : IPlayCalculator
{
    public int CalculateTotalAmount(Performance perf)
    {
        var thisAmount = 30000 + 300 * perf.Audience;
        if (perf.Audience > 20)
        {
            thisAmount += 10000 + 500 * (perf.Audience - 20);
        }
        return thisAmount;
    }

    public int CalculateVolumeCredits(Performance perf)
    {
        int volumeCredits = Math.Max(perf.Audience - 30, 0);
        // add extra credit for every ten comedy attendees
        volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
        return volumeCredits;
    }
}
