namespace Dept_subDept_Module.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public int? Parent_Id { get; set; }
    }
}
