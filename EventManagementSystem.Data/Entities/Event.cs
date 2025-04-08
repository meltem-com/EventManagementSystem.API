using System.Collections.Generic;

namespace EventManagementSystem.Data.Entities
{
    // Represents an event in the system (e.g., concert, workshop, seminar)
    public class Event
    {
        // Unique identifier for the event
        public int Id { get; set; }

        // Title or name of the event
        public string Title { get; set; } = null!;

        // Detailed description of the event
        public string Description { get; set; } = null!;

        // Date and time when the event takes place
        public DateTime Date { get; set; }

        // Location where the event is held
        public string Location { get; set; } = null!;

        // Navigation property for the many-to-many relationship with users
        public ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();
    }
}
