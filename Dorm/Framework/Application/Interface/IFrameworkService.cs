using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LinqSpecs;
using MyFramework.Domain;
using MyFramework.Web.Page;

namespace MyFramework.Application.Interface
{
    public interface IFrameworkService
    {
        Account GetAccountById(string id);
        bool AccountIsExit(string userName);
        Account GetAccountByUserName(string userName);
        Account LogOnValid(string username, string password);
       
        IAccountCommand EditAccountByUserName(string userName);
        IAccountCommand EditAccountById(string id);
        void DeleteAccount(string id);
        IAccountCommand CreateAccount(string username);

        PageList<Account> GetAllAccount(int? currentsize, int? pagesize,params Specification<Account>[] specification);

        IList<Account> GetAllAccount(params Expression<Func<Account,bool>>[] expressions );
        Role GetRoleByName(string name);
        Role GetRoleById(string id);
        IRoleCommand EditRoleById(string id);
        void DeleteRole(string id);
        IRoleCommand CreateRole(int grade);
        IList<Role> GetAllRole();
        
        IList<Account> GetAllAccountByRole(string id);
        Account GetCurrentAccount();


        IRequestLogCommand CreateRequestLog(string sessionId);


        IList<Log> GetAllRequestLog();
        IAccountCommand EditAccount();
        MyFramework.Domain.Version GetVersion();
    }
}