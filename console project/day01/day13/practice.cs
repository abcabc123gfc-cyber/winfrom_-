using System;

namespace day13
{

    #region Account
    abstract public class Account
    {
        /// <summary>
        /// 账号
        /// </summary>
        private string accountNumber;
        public string AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }
        /// <summary>
        /// 账户名
        /// </summary>
        private string accountHolderName;
        public string AccountHolderName
        {
            get { return accountHolderName; }
            set { accountHolderName = value; }
        }
        /// <summary>
        /// 账户余额
        /// </summary>
        private double balance;
        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        public Account(string accountNumber, string accountHolderName, double balance)
        {
            this.AccountNumber = accountNumber;
            this.AccountHolderName = accountHolderName;
            this.Balance = balance;
        }
        /// <summary>
        ///  利息
        /// </summary>
        /// <returns></returns>
        abstract public double CalculateInterest(double balance);
        /// <summary>
        /// 存款,增加余额
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public virtual double Deposit(double amount)
        {
            if (amount >= 0)
            {
                return Balance += amount;
            }
            return Balance;
        }
        /// <summary>
        /// 取款,减少余额
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public virtual void Withdraw(double amount)
        {
            if (amount >= 0)
            {
                return;
            }
            Balance -= amount;
        }
        /// <summary>
        /// 显示账户信息
        /// </summary>
        /// <param name="account"></param>
        public virtual void DisplayAccountInfo(Account account)
        {
           
            Console.WriteLine($"账户信息{account.accountNumber},持有人:{account.accountHolderName},余额:{account.balance}");
        }
    }

    #endregion

    #region 储蓄账户 SavingsAccount
    /// <summary>
    /// 储蓄账户
    /// </summary>
    public class SavingsAccount : Account
    {
        /// <summary>
        /// 利率
        /// </summary>
        private double interestRate;
        public double InterestRate
        {
            get { return interestRate; }
            set { interestRate = value > 0 ? value : interestRate; }
        }
        public SavingsAccount(string accountNumber, string accountHolderName, double balance, double interesRate) : base(accountNumber, accountHolderName, balance)
        {
            InterestRate = interesRate;
        }
        /// <summary>
        /// 输入金额,输出加上利息的金额
        /// </summary>
        /// <param name="balance"></param>
        /// <returns></returns>
        public override double CalculateInterest(double balance)
        {
            Console.WriteLine("子类储蓄方法显示利率");
            return balance * (1 + interestRate);
        }
        /// <summary>
        /// 输出账户信息_储蓄类
        /// </summary>
        /// </summary>
        /// <param name="account"></param>
        public override void DisplayAccountInfo(Account account)
        {
            Console.WriteLine("储蓄账户子类方法_显示账户信息");
            Console.WriteLine($"账户信息:{account.AccountNumber},持有人:{account.AccountHolderName},余额:{account.Balance},利率:{InterestRate}");
        }

    }
    #endregion

    #region 支票类 CheckingAccount
    /// <summary>
    /// 支票类
    /// </summary>
    public class CheckingAccount : Account
    {
        /// <summary>
        /// 透支额度
        /// </summary>
        private double overdraftLimit;
        public double OverdraftLimit
        {
            get { return overdraftLimit; }
            set { overdraftLimit = value; }
        }

        public CheckingAccount(string accountNumber, string accountHolderName, double overdraftLimit, double balance) : base(accountNumber, accountHolderName, balance)
        {
            this.overdraftLimit = overdraftLimit;
        }

        /// <summary>
        /// 输出利率_支票类
        /// </summary>
        /// <param name="balance"></param>
        /// <returns></returns>
        public override double CalculateInterest(double balance)
        {
            return default;
        }
        public override void Withdraw(double amount)
        {
            if (amount < 2000)
            {
                base.Withdraw(amount);
            }
            else
            {
                Console.WriteLine("透支金额不能超过2000");
                return;
            }

        }

        /// <summary>
        /// 输出账户信息_支票类
        /// </summary>
        /// <param name="account"></param>
        public override void DisplayAccountInfo(Account account)
        {
            Console.WriteLine($"账户信息:{account.AccountNumber},持有人:{account.AccountHolderName},余额:{account.Balance},利率:0,支票额度{OverdraftLimit}");
        }

    }
    #endregion
}

