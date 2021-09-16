using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.ApiCallerService
{
    public class PriorityQueue
    {
        private Queue<ApiCallData> Priority { get; set; }
        private Queue<ApiCallData> Normal { get; set; }
        public PriorityQueue()
        {
            Priority = new Queue<ApiCallData>();
            Normal = new Queue<ApiCallData>();
        }
        public ApiCallData Add(ApiCallData newNormalPriority)
        {
            ApiCallData _new = newNormalPriority ?? new ApiCallData();
            ApiCallData _newData = new ApiCallData();
            _newData.CopyFrom(_new);

            Normal.Enqueue(_newData);
            return _newData;
        }
        public ApiCallData AddPriority(ApiCallData newPriority)
        {
            ApiCallData _new = newPriority ?? new ApiCallData();
            ApiCallData _newData = new ApiCallData();
            _newData.CopyFrom(_new);

            Priority.Enqueue(_newData);
            return _newData;
        }
        public ApiCallData PopOrNull()
        {
            bool PriorityRecordsExist = Priority.Count > 0;
            ApiCallData _data;
            if (PriorityRecordsExist)
            {
                _data = Priority.Dequeue();
                return _data;
            }

            bool NormalRecordsExist = Normal.Count > 0;
            if (NormalRecordsExist)
            {
                _data = Normal.Dequeue();
                return _data;
            }
            else
            {
                return null;
            }
        }
    }
}
