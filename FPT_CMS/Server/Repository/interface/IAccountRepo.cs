using Server.DTO;
using Server.Entity;

namespace Server.Repository.@interface
{
    public interface IAccountRepo
    {
        Account GetAccountByUserNameAndPassword(AccountDTO accountDTO);
    }
}
