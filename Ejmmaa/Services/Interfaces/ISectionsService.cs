
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Services.Interfaces
{
    public interface ISectionsService
    {

        public List<SectionsViewModel> GetAllSections(SectionDto sectionDto); 

        public SectionsViewModel GetSectionById(SectionDto sectionDto); 

        public bool AddSection(SectionDto  sectionDto); 

        public bool UpdateSection(SectionDto sectionDto);

        public bool DeleteSection(SectionDto sectionDto);

    }
}