namespace PagueVeloz.Models
{
    public class PeopleQuery
    {
        public string StateId { get; set; }

        public string Name { get; set; }

        public string SortBy { get; set; }

        public string RegistrationDateStartString { get; set; }
        public string RegistrationDateEndString { get; set; }

        public string BirthDateStartString { get; set; }
        public string BirthDateEndString { get; set; }

        public bool IsSortAscending { get; set; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }
    }
}