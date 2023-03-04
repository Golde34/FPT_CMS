using Microsoft.EntityFrameworkCore;
using Server.DTO;
using Server.Entity;

namespace Server.DAO
{
    public class AccountManagement
    {
        private static AccountManagement instance;
        private static readonly object instancelock = new object();

        public AccountManagement() { }

        public static AccountManagement Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null) instance = new AccountManagement();
                }
                return instance;
            }
        }

        public Account AddAccount(Account Account)
        {
            try
            {
                Account _account = GetAccountByUserName(Account.Username);
                if (_account == null)
                {
                    var context = new AppDBContext();
                    context.Accounts.Add(Account);
                    context.SaveChanges();
                    return Account;
                }
                else
                {
                    throw new Exception("The account's username has already bean taken.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Account UpdateAccount(Account Account)
        {
            try
            {
                Account _account = GetAccountByUserName(Account.Username);
                if (_account != null)
                {
                    var context = new AppDBContext();
                    context.Entry<Account>(Account).State = EntityState.Modified;
                    context.SaveChanges();
                    return Account;
                }
                else
                {
                    throw new Exception("The account is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Account DeleteAccount(Account Account)
        {
            try
            {
                Account _account = GetAccountByUserName(Account.Username);
                if (_account != null)
                {
                    var context = new AppDBContext();
                    context.Accounts.Remove(_account);
                    context.SaveChanges();
                    return _account;
                }
                else
                {
                    throw new Exception("The account is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Account GetAccountByUserNameAndPassword(AccountDTO accountDTO)
        {
            Account account = null;
            try
            {
                var _dbContext = new AppDBContext();
                account = _dbContext.Accounts
                    .FirstOrDefault(a => a.Username == accountDTO.Username && a.Password == accountDTO.Password);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return account;
        }

        public IEnumerable<Account> GetAccounts()
        {
            List<Account> accounts;
            try
            {
                var context = new AppDBContext();
                accounts = context.Accounts.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return accounts;
        }

        public Account GetAccountByUserName(string username)
        {
            Account account;
            try
            {
                var _dbContext = new AppDBContext();
                account = _dbContext.Accounts
                    .FirstOrDefault(a => a.Username == username);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return account;
        }

        public Account GetAccountById(string? id)
        {
            Account account;
            try
            {
                var _dbContext = new AppDBContext();
                account = _dbContext.Accounts
                    .FirstOrDefault(a => a.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return account;
        }
    }
}
