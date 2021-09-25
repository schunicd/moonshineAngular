namespace TheMoonshineCafe.DataAccess.Models{
    public class Admin{
        public int id { get; set; }
        public string name{ get; set; }
        public string email{ get; set; }
        public string phoneNumber { get; set; }
        public int accessLevel { get; set; }
    }
}