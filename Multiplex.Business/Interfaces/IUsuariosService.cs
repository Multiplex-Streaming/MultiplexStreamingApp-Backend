using Multiplex.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.Interfaces
{
    public interface IUsuariosService
    {
        Task<List<AbonadosDTO>> GetAbonadosPendientes();
        UserInfoDTO UserExists(string userMail, string userPass);
        Task<bool> CreateUserAccount(UserAccountDTO userAccount);
        Task<bool> ChangePassword(ChangePasswordDTO changePasswordInfo);
    }
}
