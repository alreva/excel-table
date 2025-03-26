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
                Time = "March 18 1PM",
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
                Time = "2025-03-18 11:00",
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