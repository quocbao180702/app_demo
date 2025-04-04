namespace EmployeeManagement.Api
{
    public class BaseRequeset
    {
        public string? Term { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public Sorter? Sorter { get; set; }
    }

    public class Sorter
    {
        public string? Field { get; set; }
        public string? Order { get; set; }
    }

}
