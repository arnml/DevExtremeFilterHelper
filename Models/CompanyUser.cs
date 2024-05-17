namespace dgFilter.Models
{
    public class CompanyUser
    {
        public Guid Id { get; set; } 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public static List<CompanyUser> GetUsers()
        {
            return new List<CompanyUser>
            {
                new CompanyUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Password = "password123",
                    Address = "123 Elm Street, Springfield, IL"
                },
                new CompanyUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Password = "password456",
                    Address = "456 Oak Avenue, Metropolis, NY"
                },
                new CompanyUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Michael",
                    LastName = "Johnson",
                    Email = "michael.johnson@example.com",
                    Password = "password789",
                    Address = "789 Pine Road, Smallville, KS"
                },
                new CompanyUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Emily",
                    LastName = "Davis",
                    Email = "emily.davis@example.com",
                    Password = "password321",
                    Address = "321 Maple Lane, Gotham, NJ"
                },
                new CompanyUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Chris",
                    LastName = "Brown",
                    Email = "chris.brown@example.com",
                    Password = "password654",
                    Address = "654 Birch Boulevard, Star City, CA"
                }
            };
        }
    }
}
