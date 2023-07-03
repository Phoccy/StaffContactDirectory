namespace StaffContactDirectory.Model
{
    [Table("Departments")]
    public class DepartmentsModel
    {
        public int Id { get; set; }        
        public string Name { get; set; }
    }
}