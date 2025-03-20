using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelTable.DataAccess;

using System.ComponentModel.DataAnnotations;

public class Meeting
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Progress { get; set; } = string.Empty;

    [Required]
    public DateTimeOffset Time { get; set; }

    [Required, MaxLength(100)]
    public string Person { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Position { get; set; } = string.Empty;

    [MaxLength(100)]
    public string CompanyAndBoothNumber { get; set; } = string.Empty;

    [EmailAddress, MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(200)]
    public string LinkedIn { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Source { get; set; } = string.Empty;

    [MaxLength(50)]
    public string LeadStatus { get; set; } = string.Empty;

    public string AfterDiscussionNotes { get; set; } = string.Empty;
}
