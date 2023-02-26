using Server.DAO;
using Server.DTO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class AccountRepository : IAccountRepo
    {
        public Account GetAccountByUserNameAndPassword(AccountDTO accountDTO) => AccountManagement.Instance.GetAccountByUserNameAndPassword(accountDTO);
    }
}
