using System.ComponentModel.DataAnnotations;

namespace PomocKolezenska.Models.Request;

public class NewQuestionModel
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Content { get; set; }
}
