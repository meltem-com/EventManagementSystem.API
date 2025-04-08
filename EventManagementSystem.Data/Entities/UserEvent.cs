namespace EventManagementSystem.Data.Entities
{
    // Represents the many-to-many relationship between Users and Events
    public class UserEvent
    {
        // Foreign key: ID of the user who joined the event
        public int UserId { get; set; }

        // Foreign key: ID of the event that was joined
        public int EventId { get; set; }

        // Date and time when the user joined the event
        public DateTime JoinedAt { get; set; }

        // Navigation property to the related User entity
        public User? User { get; set; }

        // Navigation property to the related Event entity
        public Event? Event { get; set; }
    }
}