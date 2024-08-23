using Microsoft.EntityFrameworkCore;

public static class MeetingEndPoints
{
    public static void Map(RouteGroupBuilder meetingGroup)
    {
        meetingGroup.MapGet("/", GetAllMeetings);
        meetingGroup.MapPost("/", CreateMeeting);
    }
    static async Task<IResult> GetAllMeetings(MeetingDb db)
    {
        return TypedResults.Ok(await db.Meetings.Select(x => new MeetingDto(x)).ToArrayAsync());
    }

    static async Task<IResult> CreateMeeting(Meeting meeting, MeetingDb meetingDb)
    {
        await meetingDb.Meetings.AddAsync(meeting);
        await meetingDb.SaveChangesAsync();

        var result = new MeetingDto(meeting);

        return TypedResults.Created($"/meeting/{result.Id}", result);

    }

}
