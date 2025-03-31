namespace WebMVCR1.Models
{
    public class Person
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public override string ToString()
        {
            if (FirstName != null || LastName != null) return FirstName + " " + LastName;
            else return "приятный незнакомец";
        }
    }
}
