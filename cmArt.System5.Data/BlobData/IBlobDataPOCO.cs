namespace cmArt.System5.Data
{
    public interface IBlobDataPOCO
    {
        byte[] BlobD { get; set; }
        int BlobDataId { get; set; }
        short BSize { get; set; }
        int LineNo { get; set; }
        int RecordNo { get; set; }
    }
}