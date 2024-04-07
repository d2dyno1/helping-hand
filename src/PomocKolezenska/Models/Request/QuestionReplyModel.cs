using System.ComponentModel.DataAnnotations;

namespace PomocKolezenska.Models.Request;

public class QuestionReplyModel
{
    [Required]
    public string Content { get; set; }
}