public class MeetingDto
{

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public DateTime Time_Meeting { get; set; }

    public string Location { get; set; } = string.Empty;

    public string Host { get; set; } = string.Empty;

    public string Objective { get; set; } = string.Empty; //  Mục tiêu cuộc hop     

    public MeetingDto()
    {

    }

    public MeetingDto(Meeting item) => (Name, Time_Meeting, Location, Host, Objective) = (item.Name, item.Time_Meeting, item.Location, item.Host, item.Objective);
}