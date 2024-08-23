public class Attendee
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Postion { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    public int MeetingId { get; set; }

    public Meeting Meeting { get; set; } = null!;

}