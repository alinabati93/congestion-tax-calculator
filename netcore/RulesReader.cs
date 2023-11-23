using congestion.calculator.Enum;
using congestion.calculator.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator
{
    public static class RulesReader
    {
        public static Rules ReadRulesFromExcelFile(string filePath)
        {
            var res = new Rules();

            FileInfo existingFile = new FileInfo(filePath);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                //get the first worksheet in the workbook
                #region get general settings
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                res.maxFeeInDay = int.Parse(worksheet.Cells[1, 2].Value.ToString());

                var tollFreeVehicles = worksheet.Cells[2, 2].Value.ToString().Split(',').ToList();
                foreach (var vehicle in tollFreeVehicles)
                {
                    res.tollFreeVehicles.Add((VehicleType)int.Parse(vehicle));
                }

                var tollFreeMonths = worksheet.Cells[3, 2].Value.ToString().Split(',').ToList();
                foreach (var month in tollFreeMonths)
                {
                    res.tollFreeMonths.Add(int.Parse(month));
                }

                var tollFreeDaysOfWeek = worksheet.Cells[3, 2].Value.ToString().Split(',').ToList();
                foreach (var day in tollFreeDaysOfWeek)
                {
                    res.tollFreeDaysOfWeek.Add((DayOfWeek)int.Parse(day));
                }
                #endregion

                #region get toll free days of year (holidays)
                worksheet = package.Workbook.Worksheets[1];

                int rowCount = worksheet.Dimension.End.Row;     //get row count

                for (int i = 2; i <= rowCount; i++)
                {
                    res.tollFreeDaysOfYear.Add(new ExemptDay
                    {
                        Date = DateOnly.FromDateTime(DateTime.Parse(worksheet.Cells[i, 2].Value.ToString())),
                        CountOfFreeDaysBefore = int.Parse(worksheet.Cells[i, 1].Value.ToString()),
                        CountOfFreeDaysAfter = int.Parse(worksheet.Cells[i, 3].Value.ToString())
                    });
                }
                #endregion

                #region get fee based on time
                worksheet = package.Workbook.Worksheets[2];

                rowCount = worksheet.Dimension.End.Row;     //get row count

                for (int i = 2; i <= rowCount; i++)
                {
                    res.Fees.Add(new FeeBasedOnTime
                    {
                        StartTime = TimeOnly.FromDateTime(DateTime.Parse(worksheet.Cells[i, 1].Value.ToString())),
                        EndTime = TimeOnly.FromDateTime(DateTime.Parse(worksheet.Cells[i, 2].Value.ToString())),
                        Fee = int.Parse(worksheet.Cells[i, 3].Value.ToString()),
                    });
                }
                res.Fees = res.Fees.OrderBy(x => x.StartTime).ToList();
                #endregion

            }

            return res;
        }
    }
}
