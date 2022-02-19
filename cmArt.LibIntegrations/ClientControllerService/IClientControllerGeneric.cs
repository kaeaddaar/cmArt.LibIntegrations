using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cmArt.LibIntegrations.ClientControllerService
{
    public interface IClientControllerGeneric_Guid<T, Ts>
        where T : new()
        where Ts : new()
    {
        Task<string> Add(Guid objId, Ts objToAdd, Dictionary<string, string> QueryStringArgs_NotEncoded);
        Task<string> Add(string idGuid, Ts objToAdd, Dictionary<string, string> QueryStringArgs_NotEncoded);
        Task<string> Delete(Guid objId);
        Task<string> Delete(string idGuid);
        Task<T> Get(Guid objID);
        Task<T> Get(string idGuid);
        Task<string> Update(Guid objId, Ts objToAdd, Dictionary<string, string> QueryStringArgs_NotEncoded);
        Task<string> Update(string idGuid, Ts objToAdd, Dictionary<string, string> QueryStringArgs_NotEncoded);
    }
    public interface IClientControllerGeneric_int<T, Ts>
        where T : new()
        where Ts : new()
    {
        Task<string> Add(int objId, Ts objToAdd, Dictionary<string, string> QueryStringArgs_NotEncoded);
        Task<string> Add(string idIn, Ts objToAdd, Dictionary<string, string> QueryStringArgs_NotEncoded);
        Task<string> Delete(int objId);
        Task<string> Delete(string idIn);
        Task<T> Get(int objID);
        Task<T> Get(string idIn);
        Task<string> Update(int objId, Ts objToAdd, Dictionary<string, string> QueryStringArgs_NotEncoded);
        Task<string> Update(string idIn, Ts objToAdd, Dictionary<string, string> QueryStringArgs_NotEncoded);
    }

}
