namespace Ewamall.WebAPI.DTOs
{
    public class CreateIndustryAndDetailCommand
    {
        public string IndustryName { get;  set; }
        public bool IsActive { get;  set; }
        public int Level { get; set; }
        public bool IsLeaf { get; set; }
        public string Path { get; set; }
        public int RootId { get; set; }
        public IEnumerable<DetailCommand>? ExistDetails { get; set; }
        public IEnumerable<DetailCommand>? NewDetails { get; set; }
    }
    public class DetailCommand
    {
        public int DetailId { get;  set; }
        public string? DetailName { get; set; }
        public string? DetailDescription { get; set; }
    }
}
