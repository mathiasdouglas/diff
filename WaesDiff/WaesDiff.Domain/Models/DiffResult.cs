namespace WaesDiff.Domain.Models
{
    using System.Collections.Generic;

    public class DiffResult
    {
        public string Message { get; set; }

        public List<DiffDetail> Detail { get; set; } = new List<DiffDetail>();
    }
}
