using System.Globalization;
using ExcelTable.DataAccess;
using OfficeOpenXml;

namespace ExcelTable.Import;

public static class ExcelTableImporter
{
    private static readonly CultureInfo Culture = CultureInfo.GetCultureInfo("en-US");
    private static TimeZoneInfo CetZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
    
    public static async Task ImportExcel(
        this ExcelTableDbContext db,
        IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        using var package = new ExcelPackage(stream);
        
        var worksheet = package.Workbook.Worksheets[0];
        var meetings = new List<Meeting>();

        int rowCount = worksheet.Dimension.Rows;

        // assuming row 1 is sheet title and row 2 has headers
        for (int row = 3; row <= rowCount; row++)
        {
            if (!int.TryParse(worksheet.Cells[row, 1].Text, out var id))
            {
                continue;
            }

            meetings.Add(new Meeting
            {
                Id = id,
                Progress = worksheet.Cells[row, 2].Text,
                Time = ParseDateTime(worksheet.Cells[row, 3]),
                Person = worksheet.Cells[row, 4].Text,
                Position = worksheet.Cells[row, 5].Text,
                CompanyAndBoothNumber = worksheet.Cells[row, 6].Text,
                Email = worksheet.Cells[row, 7].Text,
                LinkedIn = worksheet.Cells[row, 8].Text,
                Source = worksheet.Cells[row, 9].Text,
                LeadStatus = worksheet.Cells[row, 10].Text,
                AfterDiscussionNotes = worksheet.Cells[row, 11].Text
            });
        }

        await using var transaction = await db.Database.BeginTransactionAsync();
        db.Meetings.RemoveRange(db.Meetings);
        db.Meetings.AddRange(meetings);
        await db.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    private static DateTimeOffset? ParseDateTime(ExcelRange cell)
    {
        if (cell.Value is DateTime dt)
        {
            // Excel date detected correctly
            var nonTimeZoneDate
                = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, DateTimeKind.Utc);
            return new DateTimeOffset(dt, CetZone.GetUtcOffset(nonTimeZoneDate));
        }
        if (double.TryParse(cell.Text, out var oaDate))
        {
            // Excel numeric date
            return new DateTimeOffset(DateTime.FromOADate(oaDate), TimeSpan.Zero);
        }
        if (DateTimeOffset.TryParse(cell.Text, Culture, out var parsedDto))
        {
            // Plain string parsing (fallback)
            return parsedDto;
        }

        return null; // fallback
    }
}