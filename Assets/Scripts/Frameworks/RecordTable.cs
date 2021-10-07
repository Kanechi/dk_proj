using System.Collections.Generic;
using UnityEngine;

public interface IRecordTable<RecordT>
{
    List<RecordT> RecordList { get; }

    RecordT Get(uint key);
}
