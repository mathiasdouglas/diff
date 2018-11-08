namespace WaesDiff.Domain.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Model with the result of the comparision between data
    /// </summary>
    public class DiffResult
    {
        public string Message { get; set; }

        public List<DiffDetail> Detail { get; set; } = new List<DiffDetail>();
    }
}
