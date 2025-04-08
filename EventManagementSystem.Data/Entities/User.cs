namespace EventManagementSystem.Data.Entities
{
    // Represents a user in the system (either an Admin or a regular User)
    public class User
    {
        // Unique identifier for the user
        public int Id { get; set; }

        // User's first name
        public string FirstName { get; set; } = null!;

        // User's last name
        public string LastName { get; set; } = null!;

        // User's email address (should be unique)
        public string Email { get; set; } = null!;

        // User's phone number
        public string PhoneNumber { get; set; } = null!;

        // User's password (encrypted using Data Protection)
        public string Password { get; set; } = null!;

        // Role of the user (e.g., "Admin" or "User")
        public string Role { get; set; } = "User";

        // Navigation property for the many-to-many relationship with events
        public ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();
    }
}
