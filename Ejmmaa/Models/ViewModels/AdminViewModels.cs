namespace Ejmmaa.Models.ViewModels
{
    public class AdminViewModels
    {
        public IEnumerable<ClanViewModel> Clans { get; set; }
        public IEnumerable<SectionsViewModel> Sections { get; set; }
        public IEnumerable<ClanMembersViewModel> Members { get; set; }
    }
}