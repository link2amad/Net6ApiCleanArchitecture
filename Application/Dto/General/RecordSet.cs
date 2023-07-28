using System.Collections.Generic;

namespace Application.Dto
{
    public class RecordSet<T>
    {
        public int TotalRows { set; get; }
        public IList<T> Items { set; get; }
    }
}