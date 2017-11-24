using System;
using System.Drawing.Imaging;
using MyFramework.Application.Interface;
using MyFramework.Domain;
using MyFramework.Helper;
using MyFramework.Helper.StringDecrypt;

namespace MyFramework.Application.InterfaceAchieve
{
    public class AccountCommand : IAccountCommand
    {
        private readonly Account _account;
        private readonly IRepository _repository;


        public AccountCommand(Account account, IRepository repository)
        {
            _account = account;
            _repository = repository;
        }


        

        public IAccountCommand PassWord(string password)
        {
            _account.PassWord = password.ToEncode();


            return this;
        }

        public IAccountCommand ResetPassWord(string password)
        {
            _account.PassWord = password.ToEncode();
            return this;
        }

        public IAccountCommand Photo(byte[] photo)
        {
            _account.Photo = photo;
            return this;
        }

         

        


        public IAccountCommand Email(string email)
        {
            _account.Email = email;


            return this;
        }

        public IAccountCommand AddRole(string roleid)
        { 
            var _role = _repository.GetById<Role>(new Guid(roleid));

            if (!_account.Roles.Contains(_role))
            {
                _account.AddRole(_role);
            };
             
            return this;
        }

        public IAccountCommand RemoveRole(string roleid)
        {
            if (!_repository.IsExisted(new Role.By(roleid)))
                throw new Exception("该权限不存在"); 
                _account.RemoveRole(_repository.GetOne(new Role.By(roleid)));
            return this;
        }

        public IAccountCommand IsEable()
        {
            if (_account.Id == Guid.Empty)
            {
                _account.IsEnable = true;
            }
            else
            {
                _account.IsEnable = !_account.IsEnable;
            }


            return this;
        }

        public IAccountCommand RealName(string s)
        {
            _account.RealName = s;
            return this;
        }

        public IAccountCommand QQ(string qq)
        {
            _account.QQ = qq;
            return this;
        }

        public IAccountCommand Phone(string phone)
        {
            _account.Phone = phone;
            return this;
        }

        public IAccountCommand RemoveAllRole()
        {
          _account.Roles.Clear();
            return this;
        }


        public Guid Id { get { return _account.Id; } }
    }
}