namespace WaesDiff.Domain.Models
{
    /// <summary>
    /// Model with the detail, when exist difference between data(left/right)
    /// </summary>
    public class DiffDetail
    {
        public long Offset { get; set; }

        public long Length { get; set; }
    }
}
