using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelTable.DataAccess;

using System.ComponentModel.DataAnnotations;

public class Meeting
{
    [Required, Key]
    public int Id { get; set; }

    [MaxLength(255)]
    public string? Progress { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Time { get; set; }

    [MaxLength(255)]
    public string? Person { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Position { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? CompanyAndBoothNumber { get; set; } = string.Empty;

    [EmailAddress, MaxLength(100)]
    public string? Email { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? LinkedIn { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Source { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? LeadStatus { get; set; } = string.Empty;

    public string? AfterDiscussionNotes { get; set; } = string.Empty;
}
