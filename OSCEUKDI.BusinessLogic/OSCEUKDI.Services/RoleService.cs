using OSCEUKDI.Common.Helpers;
using OSCEUKDI.Common.Interfaces;
using OSCEUKDI.Common.Interfaces.RepoInterfaces;
using OSCEUKDI.Common.Interfaces.RepoInterfaces.OSCEUKDIRepoInterfaces;
using OSCEUKDI.Entities.Models;
using OSCEUKDI.Entities.ViewModel;
using OSCEUKDI.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Services
{
    public interface IRoleService : IEntityService<Role>
    {
        List<VMLookup> getLookupRole();
        IEnumerable<GridRole> getRole();
        //VMListRole getRole(DataTableAjaxPostModel model);
        IEnumerable<VMRoleAccessMenu> getRoleAccessMenu(int id);
        List<VMDropDown> GetRoles();
    }

    public class RoleService : EntityService<Role>, IRoleService
    {
        IUnitOfWork _unitOfWork;
        IRoleRepository _RoleRepository;

        public RoleService(IUnitOfWork unitOfWork, IRoleRepository RoleRepository)
            : base(unitOfWork, RoleRepository)
        {
            _unitOfWork = unitOfWork;
            _RoleRepository = RoleRepository;
        }

        public List<VMLookup> getLookupRole()
        {
            return _RoleRepository.getLookupRole();
        }
        public IEnumerable<GridRole> getRole()
        {
            return _RoleRepository.getRole();
        }
        public IEnumerable<VMRoleAccessMenu> getRoleAccessMenu(int id)
        {
            return _RoleRepository.getRoleAccessMenu(id);
        }

        public List<VMDropDown> GetRoles()
        {
            return _RoleRepository.getDropDownRole();
        }
    }
}
