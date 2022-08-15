#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[NotMapped]
public class userlogin{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    // variable name of the field must be different form you table names or you will get duplicated error messages
    public string loginEmail { get; set; }
    
    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]

    public string loginPassword {get;set;}
}