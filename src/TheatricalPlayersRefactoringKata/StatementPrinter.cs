﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string GetTitle(Invoice invoice)
        {
            return string.Format("Statement for {0}\n", invoice.Customer);
        }

        public string GetInvoiceFooter(CultureInfo cultureInfo, int totalAmount, int volumeCredits)
        {
            return String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100))
                + String.Format("You earned {0} credits\n", volumeCredits);
        }

        public string PrintInvoiceItem(CultureInfo cultureInfo, Play play, int thisAmount, Performance perf)
        {
             return String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
        }

        public int CalculateAmount(Play play, Performance perf)
        {
            var thisAmount = 0;
            switch (play.Type)
            {
                case "tragedy":
                    thisAmount = 40000;
                    if (perf.Audience > 30)
                    {
                        thisAmount += 1000 * (perf.Audience - 30);
                    }
                    break;
                case "comedy":
                    thisAmount = 30000;
                    if (perf.Audience > 20)
                    {
                        thisAmount += 10000 + 500 * (perf.Audience - 20);
                    }
                    thisAmount += 300 * perf.Audience;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            return thisAmount;
        }

        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = GetTitle(invoice);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach(var perf in invoice.Performances)
            {
                var play = plays[perf.PlayID];
                int thisAmount = CalculateAmount(play, perf);
                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

                // print line for this order
                result += PrintInvoiceItem(cultureInfo, play, thisAmount, perf);
                totalAmount += thisAmount;
            }
            result += GetInvoiceFooter(cultureInfo, totalAmount, volumeCredits);
            return result;
        }
    }
}
