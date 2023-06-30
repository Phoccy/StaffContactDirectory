namespace StaffContactDirectory.Database
{
    public class RemoteDatabase : DbContext
    {
        private readonly string _connectionString = @"Data Source = mysqlserver20230628.database.windows.net; Initial Catalog = StaffContactDirectory; User ID = azureuser; Password=AzureP455;Connect Timeout = 30; Encrypt=True;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";
        public DbSet<PeopleModel> People { get; set; }
        public DbSet<DepartmentsModel> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer(_connectionString);
                base.OnConfiguring(optionsBuilder);
            }
            catch (Exception e)
            {
                Shell.Current.DisplayAlert("No Database Access", e.ToString(), "OK");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeopleModel>()
                .HasOne(p => p.Departments)
                .WithMany()
                .HasForeignKey(p => p.DepartmentId);
        }
    }
}