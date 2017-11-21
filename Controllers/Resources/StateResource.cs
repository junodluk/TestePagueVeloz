namespace PagueVeloz.Controllers.Resources
{
    public class StateResource
    {
        // public int Id { get; set; }
        public string Initials { get; set; }

        public string Name { get; set; }

        public int MinimumAge { get; set; }

        public bool RequireRG { get; set; }

        public StateResource() {
            MinimumAge = 0;
            RequireRG = false;
        }
    }
}