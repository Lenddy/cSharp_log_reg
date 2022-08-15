using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using cSharp_log_reg.Models;
using Microsoft.AspNetCore.Identity;

namespace cSharp_log_reg.Controllers;

public class log_inController : Controller
{
private DB db;
// creating private field redirect people to hte index page if tey are not log in 

private int? uid{
    get{
        return HttpContext.Session.GetInt32("userId");
    }
}
private bool loggeIn{
get{
    return uid!= null;
}
}


public log_inController(DB context){
    db = context;
}
[HttpGet("")]
// if the user is already log in this will redirect them  to the dashboard
public IActionResult index(){
    if(loggeIn){
        return RedirectToAction("dashboard");
    }
    return View("index");
}

[HttpGet("/dashboard")]

public IActionResult dashboard(){
    // redirecting to index page if people are not log in 
    if(!loggeIn){
        return RedirectToAction("index");
    }

    return View("dashboard");
}

[HttpPost("/register")]
public IActionResult register(log_reg_Model newUser){
    if(ModelState.IsValid){
        // checking to see if the email inputted already exist
        if(db.log.Any(user=> user.email==newUser.email) ){

            // adding a validation error
            ModelState.AddModelError("email", "this email is already taken ");
        }
        }
        // you need to check a second time for validations 
        if(ModelState.IsValid == false){
            return index();
        }
        
        //* this is fo hashing the passwords
        // click the light bulb when it appears
        PasswordHasher<log_reg_Model> hash = new PasswordHasher<log_reg_Model>();

        // we need to call the instance of log_reg_Model(newUser) assign it to the variable that we assign the PasswordHasher then .HashPassword(class instance ,instance name .password)
        newUser.password = hash.HashPassword(newUser,newUser.password);
    
    // we need to add it to the list  and save the changes
    db.log.Add(newUser);
    db.SaveChanges();

    // sending the new users id to session 
    HttpContext.Session.SetInt32("userId",newUser.logId);
    HttpContext.Session.SetString("userName",newUser.full_name());
    // HttpContext.Session.SetString("dateCreated",newUser.CreatedAt);
    return RedirectToAction("dashboard");
}

[HttpPost("/logIn")]
public IActionResult logIn(userlogin logInUser){

    if(ModelState.IsValid == false){
        return index();
    }
    // cheking if there is a user in the data base if the info inputed pass the validations
    log_reg_Model? dbuser = db.log.FirstOrDefault(user=> user.email == logInUser.loginEmail);

    // if there is no user matching the info we will show a validation error message
    if(dbuser == null){
        ModelState.AddModelError("loginEmail","email/password does not match");
        return index();
    }

    // if the user exist
    PasswordHasher<userlogin> hash = new PasswordHasher<userlogin>();

    // here we are checking if the passwords provided match with the one store in the data base
    // you have to  pass the name of the class instance  that you are checking for, then the password that is store in the data base, then you are checking if the password given by the user match 
    PasswordVerificationResult compare = hash.VerifyHashedPassword(logInUser,dbuser.password,logInUser.loginPassword);

    // something will be store as a 0 if you ge a n error 
    //* this is ha you check for errors
    if (compare == 0){
        ModelState.AddModelError("loginPassword","email/password does not match");
    }
    // if there is no error return then  whe log the user in 
    HttpContext.Session.SetInt32("userId",dbuser.logId);
    HttpContext.Session.SetString("userName",dbuser.full_name());
    return RedirectToAction("dashboard");
}

[HttpPost("/logout")]
public IActionResult logout(){
    HttpContext.Session.Clear();
    return RedirectToAction("index");
}

}
