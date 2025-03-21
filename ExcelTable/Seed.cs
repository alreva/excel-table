using System.Globalization;
using ExcelTable.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ExcelTable;

public static class Seed
{
    public static async Task SeedSampleData(this ExcelTableDbContext ctx)
    {
        if (await ctx.Meetings.AnyAsync()) return;

        Meeting[] meetings =
        [
            new()
            {
                Progress = "In progress",
                Time = DateTimeOffset.Parse("2025-03-18T10:00:00Z", CultureInfo.InvariantCulture),
                Person = "John Doe",
                Position = "CEO",
                CompanyAndBoothNumber = "ACME Inc., 123",
                Email = "no-reply.john.doe@acme.inc",
                LinkedIn = "https://www.linkedin.com/in/johndoe",
                Source = "LinkedIn",
                LeadStatus = "",
                AfterDiscussionNotes = "John is interested in our services."
            },
            new()
            {
                Progress = "In progress",
                Time = DateTimeOffset.Parse("2025-03-18T11:00:00Z", CultureInfo.InvariantCulture),
                Person = "Jane Doe",
                Position = "CTO",
                CompanyAndBoothNumber = "ACME Inc., 124",
                Email = "no-reply.jane.doe@acme.inc",
                LinkedIn = "https://www.linkedin.com/in/janedoe",
                Source = "LinkedIn",
                LeadStatus = "",
                AfterDiscussionNotes = "Jane is interested in our services."
            },
        ];

        await ctx.Meetings.AddRangeAsync(meetings);
        await ctx.SaveChangesAsync();
    }
}