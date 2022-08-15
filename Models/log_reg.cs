#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//your namespace
namespace cSharp_log_reg.Models;   //must be the same that is on you program file 
//classname
public class log_reg_Model
{
//* you need to use
// dotnet ef migrations add FirstMigration
// dotnet ef database update
//* only doit after creating you routes with all the info that you need
//this is the primary key
    [Key]
    public int logId { get; set; }
//change the field as needed
    [Required]
    [MinLength(2)]
    [Display(Name = "First name")]
    public string f_name { get; set; }
    [Required]
    [MinLength(2)]
    [Display(Name = "Last name")]
    public string l_Name { get; set; }
    // might need to cam back and change line 25
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    
    public string email { get; set; }

    [Required]
    [MinLength(8,ErrorMessage = "Pass word must be a least 8 characters long ")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string password {get;set;}

    [NotMapped]
    [Required]
    [DataType(DataType.Password)]
    [Compare("password",ErrorMessage = "password does not match")]
    [Display(Name = "Confirm password")]
    public string confirm_password{get;set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public string full_name(){
        return f_name + " " + l_Name;
    }
}


