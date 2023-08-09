namespace IdentityUserManagment.Shared.DTOs;
public class ModuleSectionDto : ModuleBaseEntityDto
{
    public int ModuleId { get; set; }
    public List<PageDto> Pages { get; set; }
}
