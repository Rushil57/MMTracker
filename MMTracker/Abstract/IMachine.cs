using MMTracker.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Abstract
{
    public interface IMachine
    {
        Task<int> InsertUpdateMachine(MachineDto model);
        Task DeleteMachineByKey(int id);
        Task<MachineDto> GetMachineByKey(int id);
        Task<List<MachineDto>> GetAllMachine(int id = -1);
    }
}
