using System;
using System.Collections.Generic;
using System.Linq;
using congestion.calculator;
using congestion.calculator.Enum;
using congestion.calculator.Interface;
using congestion.calculator.Model;

public class CongestionTaxCalculator
{
    private Rules rules = new Rules();

    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */

    public int GetTax(VehicleType vehicleType, DateTime[] dates)
    {
        if (IsTollFreeVehicle(vehicleType))
            return 0;

        int totalFee = 0;

        dates = dates.OrderBy(d => d).ToArray();

        var distinctDays = dates.Select(x => x.DayOfYear).Distinct();

        foreach (var day in distinctDays)
        {
            int totalFeeInDay = 0;

            int maxFee = 0;
            DateTime start = DateTime.MinValue;

            foreach (DateTime date in dates.Where(x => x.DayOfYear == day))
            {
                if (date.Subtract(start).TotalMinutes > 60)
                {
                    start = date;
                    totalFeeInDay += maxFee;
                    maxFee = 0;
                }

                int fee = getTollFee(date);
                if (fee > maxFee)
                    maxFee = fee;
            }

            totalFeeInDay += maxFee;

            if (totalFeeInDay > rules.maxFeeInDay)
                totalFeeInDay = rules.maxFeeInDay;

            totalFee += totalFeeInDay;

        }
        return totalFee;
    }

    private bool IsTollFreeVehicle(VehicleType vehicleType)
    {
        if (rules.tollFreeVehicles.Contains(vehicleType))
            return true;
        return false;
    }

    private int getTollFee(DateTime dateTime)
    {
        if (IsTollFreeDate(dateTime))
            return 0;

        dateTime = dateTime.AddSeconds(-dateTime.Second);

        TimeOnly timeOnly = TimeOnly.FromDateTime(dateTime);

        var fee = rules.Fees.FirstOrDefault(x => x.StartTime.CompareTo(timeOnly) <= 0 && x.EndTime.CompareTo(timeOnly) >= 0);
        if (fee == null)
        {
            fee = rules.Fees.OrderBy(x => x.StartTime).LastOrDefault();
        }
        if (fee != null)
            return fee.Fee;
        
        return 0;
    }

    private bool IsTollFreeDate(DateTime dateTime)
    {

        DateOnly dateOnly = DateOnly.FromDateTime(dateTime);

        if (rules.tollFreeDaysOfWeek.Contains(dateTime.DayOfWeek))
            return true;

        if (rules.tollFreeMonths.Contains(dateTime.Month))
            return true;

        if (rules.tollFreeDaysOfYear.Any(x => x.StartDate.CompareTo(dateOnly) >= 0 && x.EndDate.CompareTo(dateOnly) <= 0))
            return true;

        return false;
    }



    public CongestionTaxCalculator(string rulesFilePath)
    {
        this.rules = RulesReader.ReadRulesFromExcelFile(rulesFilePath);
    }


}