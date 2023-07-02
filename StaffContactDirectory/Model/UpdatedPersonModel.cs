namespace StaffContactDirectory.Model
{

    public class UpdatedPersonModel : PeopleModel
    {
        public new int Id { get; set; }
        public new string Name { get; set; }
        public new string Phone { get; set; }
        public new int DepartmentId { get; set; }
        public new string Street { get; set; }
        public new string Suburb { get; set; }
        public new string State { get; set; }
        public new string Postcode { get; set; }
        public new string Country { get; set; }
        public new string ImageSrc { get; set; }
    }
}