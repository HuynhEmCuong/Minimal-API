using System.ComponentModel.DataAnnotations;

public class Meeting
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime Time_Meeting { get; set; }

    public string Location { get; set; } = string.Empty;

    public string Host { get; set; } = string.Empty;

    public string Objective { get; set; } = string.Empty; //  Mục tiêu cuộc hop     

    public ICollection<Attendee> Attendees { get; } = new List<Attendee>();
}