using Server.DAO;
using Server.DTO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class AccountRepository : IAccountRepo
    {
        Account IAccountRepo.AddAccount(Account Account) => AccountManagement.Instance.AddAccount(Account);
        Account IAccountRepo.UpdateAccount(Account Account) => AccountManagement.Instance.UpdateAccount(Account);
        Account IAccountRepo.DeleteAccount(Account Account) => AccountManagement.Instance.DeleteAccount(Account);
        IEnumerable<Account> IAccountRepo.GetAccounts() => AccountManagement.Instance.GetAccounts();
        Account IAccountRepo.GetAccountById(string? id) => AccountManagement.Instance.GetAccountById(id);
        public Account GetAccountByUserNameAndPassword(AccountDTO accountDTO) => AccountManagement.Instance.GetAccountByUserNameAndPassword(accountDTO);

        public Account GetAccountByUserName(string username) => AccountManagement.Instance.GetAccountByUserName(username);
    }
}
