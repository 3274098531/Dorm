using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using LinqSpecs;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using MyFramework.Domain;
using MyFramework.Helper;
using MyFramework.Helper.BirthdatyHelper;
using MyFramework.Helper.StringDecrypt;
using MyFramework.Web.Page;
using Version = System.Version;

namespace MyFramework.Application.InterfaceAchieve
{
    [IocRegister]
    public class FrameworkService : IFrameworkService
    {
        public FrameworkService(IRepository repository)
        {
            Repository = repository;
        }

        public IRepository Repository { get; set; }


        public IList<Account> GetAllAccountByRole(string id)
        {
            string role = Repository.GetById<Role>(new Guid(id)).Name;
            return Repository.GetAll(new Account.ByRole(role)).ToList();
        }

        public Account GetCurrentAccount()
        {
            string username = IoC.Get<IHttpService>().GetInlineAccountUserName();
            if (username == null) throw new Exception("没有用户登陆");
            return GetAccountByUserName(username);
        }


        public IRequestLogCommand CreateRequestLog(string sessionId)
        {
            int nextIndex = 1;
            IQueryable<Log> logs = Repository.GetAll(new Log.BySessionId(sessionId));
            if (logs.Any())
                nextIndex = logs.Max(x => x.Index) + 1;

            var log = new Log(sessionId, nextIndex);
            Repository.Save(log);
            return new RequestLogCommand(log);
        }

        public IList<Log> GetAllRequestLog()
        {
            return Repository.GetAll<Log>().OrderBy(x => x.Time).ToList();
        }

        public IAccountCommand EditAccount()
        {
            return new AccountCommand(GetCurrentAccount(), Repository);
        }

        public MyFramework.Domain.Version GetVersion()
        {
            return Repository.GetAll<MyFramework.Domain.Version>().FirstOrDefault();
        }

        #region Account

        public string UserName { get; set; }

        public Account GetAccountById(string id)
        {
            return Repository.GetById<Account>(new Guid(id));
        }

        public bool AccountIsExit(string userName)
        {
            return Repository.IsExisted(new Account.ByUserName(userName));
        }

        public Account GetAccountByUserName(string userName)
        {
            return Repository.GetOne(new Account.ByUserName(userName));
        }

        public Account LogOnValid(string username, string password)
        {
            if (!Repository.IsExisted(new Account.ByUserName(username)))
                return null;
            if (username == FrameworkRole.Root.Value() && password.ToDecode() == DateTime.Now.ToPassWord())
                return GetAccountByUserName(username);

            Account account = GetAccountByUserName(username);
            return account.PassWord == password ? account : null;
        }


        public IAccountCommand EditAccountByUserName(string userName)
        {
            if (!AccountIsExit(userName)) throw new Exception("用户名不存在");
            Account account = GetAccountByUserName(userName);
            return new AccountCommand(account, Repository);
        }


        public IAccountCommand EditAccountById(string id)
        {
            Account account = GetAccountById(id);
            return new AccountCommand(account, Repository);
        }

        public void DeleteAccount(string id)
        {
            Repository.Remove(new ById<Account>(id));
        }


        public PageList<Account> GetAllAccount(int? currentsize, int? pagesize,
            params Specification<Account>[] specification)
        {
            int index = currentsize ?? 1;
            var username = IoC.Get<IHttpService>().GetInlineAccountUserName();
            var rootname = FrameworkRole.Root.Value();
            IQueryable<Account> accounts = Repository.GetAll(specification).Where(x=>x.Roles.Select(y=>y.Name).All(z=>z!=rootname)&&
                x.UserName!=username)
                 .OrderBy(x => x.UserName);
            int count = accounts.Count();
            int size = specification != null
                ? count
                : pagesize ?? int.Parse(ConfigurationManager.AppSettings[FrameworkKeys.PageSize]);
            List<Account> accountlist = accounts.Skip((index - 1)*(count)).Take(count).ToList();
            return new PageList<Account>(index, size, count, accountlist);
        }

        public IList<Account> GetAllAccount(params Expression<Func<Account, bool>>[] expressions)
        {
            Expression<Func<Account, bool>> expression = expressions.Aggregate((x, y) => x.And(y));
            return Repository.GetAll(expression)
                .OrderBy(x => x.UserName).ToList();
        }

        public Role GetRoleByName(string name)
        {
            return Repository.GetOne(new Role.ByName(name));
        }

        public Role GetRoleById(string id)
        {
            return Repository.GetById<Role>(new Guid(id));
        }

        public IRoleCommand EditRoleById(string id)
        {
            return new RoleCommand(GetRoleById(id), Repository);
        }

        public void DeleteRole(string id)
        {
            Repository.Remove(new ById<Role>(id));
        }

        public IRoleCommand CreateRole(int grade)
        {
            var role = new Role(grade);
            Repository.Save(role);
            return new RoleCommand(role, Repository);
        }

        public IList<Role> GetAllRole()
        {
            return Repository.GetAll<Role>(x=>x.Grade>0).OrderBy(x=>x.Grade).ToList();
        }

        public IAccountCommand CreateAccount(string username)
        {
            var account = new Account(username);
            Repository.Save(account);
            return new AccountCommand(account, Repository);
        }

        #endregion
    }
}