using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace StaffContactDirectory.Models;

public class DepartmentsModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    //public DepartmentsModel(int id, string name)
    //{
    //    Id = id;
    //    Name = name;
    //}
}
