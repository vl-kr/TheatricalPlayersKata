using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string FormatTitle(Invoice invoice)
        {
            return string.Format("Statement for {0}\n", invoice.Customer);
        }

        public string FormatTotalAmount(CultureInfo cultureInfo, int totalAmount)
        {
            return String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        }

        public string FormatEarnedCredits(int volumeCredits)
        {
            return String.Format("You earned {0} credits\n", volumeCredits);
        }

        public string FormatStatementItem(CultureInfo cultureInfo, Play play, int thisAmount, Performance perf)
        {
            return String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
        }

        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = FormatTitle(invoice);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayID];
                var playCalc = play.GetPlayCalculator();
                int thisAmount = playCalc.CalculateTotalAmount(perf);
                // add volume credits
                volumeCredits += playCalc.CalculateVolumeCredits(perf);

                // print line for this order
                result += FormatStatementItem(cultureInfo, play, thisAmount, perf);
                totalAmount += thisAmount;
            }
            result += FormatTotalAmount(cultureInfo, totalAmount);
            result += FormatEarnedCredits(volumeCredits);
            return result;
        }
    }
}
