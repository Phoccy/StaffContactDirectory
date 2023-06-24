using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffContactDirectory.Models;

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

    public DepartmentsModel Department { get; set; }


    //public PeopleModel(int id, string name, string phone, int departmentId, string street, string suburb, string state, int postcode, string country)
    //{
    //    Id = id;
    //    Name = name;
    //    Phone = phone;
    //    DepartmentId = departmentId;
    //    Street = street;
    //    Suburb = suburb;
    //    State = state;
    //    Postcode = postcode;
    //    Country = country;
    //}

    public Command<PeopleModel> GetPerson { get; set; }


}
