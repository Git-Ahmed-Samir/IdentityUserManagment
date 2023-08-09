namespace IdentityUserManagment.Shared.DTOs;
public class PageDto : ModuleBaseEntityDto
{
    public int ModuleSectionId { get; set; }

    public List<PageActionDto> PageActions { get; set; }
}
