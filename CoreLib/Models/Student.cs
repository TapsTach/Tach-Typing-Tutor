namespace CoreLib
{
    [Table("UserAccount")]
    public class Student
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public bool Locked { get; set; }

        public override string ToString()
        {
            return $"[{Id}]     {Username}";
        }
    }
}
