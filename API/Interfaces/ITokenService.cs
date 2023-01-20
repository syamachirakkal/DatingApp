using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
     string CreateToken(AppUser user) ;
     //any class which implements this inteface has to support this method 
     //and return a string from the methodand it has to take an appuser as argument  
    }
}