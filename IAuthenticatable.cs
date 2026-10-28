
namespace LibraryManagementSystem
{
    internal interface IAuthenticatable
    {
        bool CheckPassword(string password);
    }
}
