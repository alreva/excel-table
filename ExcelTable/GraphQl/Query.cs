using ExcelTable.DataAccess;

namespace ExcelTable.GraphQl;

public class Query
{
    public HelloWorldMessage GetHello() => new();

    public IQueryable<Meeting> GetMeetings(
        [Service] ExcelTableDbContext dbContext
    ) => dbContext.Meetings;
}

public record HelloWorldMessage(string Message = "Hello, World!");