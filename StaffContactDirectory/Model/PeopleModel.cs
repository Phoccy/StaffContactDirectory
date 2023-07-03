
namespace StaffContactDirectory.Model
{
    [Table("People")]
    public class PeopleModel
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public string ImageSrc { get; set; }

        [Ignore]
        public DepartmentsModel Departments { get; set; }
    }
}