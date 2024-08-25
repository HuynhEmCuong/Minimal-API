using Microsoft.EntityFrameworkCore;

class MeetingDb : DbContext
{
    public MeetingDb(DbContextOptions<MeetingDb> options) : base(options) { }

    public DbSet<Meeting> Meetings { get; set; }

    //Add Table in Sql 
    public DbSet<Attendee> Attendees { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Config Foregin Key   Meeting with many Attendess
        modelBuilder.Entity<Meeting>().HasMany(e => e.Attendees).WithOne(e => e.Meeting).HasForeignKey(e => e.MeetingId).IsRequired();
    }
}
