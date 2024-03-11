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

        public int CalculateAmountTragedy(Performance perf)
        {
            var thisAmount = 40000;
            if (perf.Audience > 30)
            {
                thisAmount += 1000 * (perf.Audience - 30);
            }
            return thisAmount;
        }

        public int CalculateAmountComedy(Performance perf)
        {
            var thisAmount = 30000 + 300 * perf.Audience;
            if (perf.Audience > 20)
            {
                thisAmount += 10000 + 500 * (perf.Audience - 20);
            }
            return thisAmount;
        }

        public int CalculateAmount(Play play, Performance perf)
        {
            return play.Type switch
            {
                "tragedy" => CalculateAmountTragedy(perf),
                "comedy" => CalculateAmountComedy(perf),
                _ => throw new Exception("unknown type: " + play.Type),
            };
        }

        public int CalculateVolumeCredits(Performance perf, Play play)
        {
            int volumeCredits = Math.Max(perf.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
            return volumeCredits;
        }

        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = FormatTitle(invoice);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach(var perf in invoice.Performances)
            {
                var play = plays[perf.PlayID];
                int thisAmount = CalculateAmount(play, perf);
                // add volume credits
                volumeCredits += CalculateVolumeCredits(perf, play);

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
