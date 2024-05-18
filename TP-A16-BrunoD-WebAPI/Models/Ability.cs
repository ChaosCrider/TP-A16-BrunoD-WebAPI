namespace TP_A16_BrunoD_WebAPI.Models
{
    public class Ability
    {

        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }

        public override string? ToString()
        {
            return string.Format("{0} : {1}", Name, Description);
        }
    }
}
