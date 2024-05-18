namespace TP_A16_BrunoD_WebAPI.Models
{
    public class Beast
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public int Health { get; set; }
        public string Description { get; set; }
        public List<Ability> Abilities { get; set; }

        public override string? ToString()
        {
            return string.Format("{0} => Health:{1}, description:{2}",Name, Health, Description);
        }
    }
}
