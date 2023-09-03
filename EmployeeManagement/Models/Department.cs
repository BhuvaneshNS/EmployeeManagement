namespace EmployeeManagement.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
         IEnumerable<Employee>? Employees { get; set; }
    }
}