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
    }
}
