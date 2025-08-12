namespace ToursAPI.DTOs;

public class IndustryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<CompanyDto> Companies { get; set; }
}

public class IndustryCreateDto
{
    public string Name { get; set; }
}

public class IndustryUpdateDto
{
    public string Name { get; set; }
}