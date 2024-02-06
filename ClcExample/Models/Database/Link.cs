using System.ComponentModel.DataAnnotations.Schema;

namespace ClcExample.Models.Database;

public class Link
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public string Path { get; set; }
    
    public string ExternalLink { get; set; }
}