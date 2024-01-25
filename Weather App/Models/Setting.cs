namespace Weather_App.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
