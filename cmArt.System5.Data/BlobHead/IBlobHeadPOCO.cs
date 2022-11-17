namespace cmArt.System5.Data
{
    public interface IBlobHeadPOCO
    {
        int BlobHeadUnique { get; set; }
        int BlobSize { get; set; }
        string DataFileName { get; set; }
        string DateCreated { get; set; }
        string DateModified { get; set; }
        string Directory { get; set; }
        string Extension { get; set; }
        short FileNo { get; set; }
        short FileType { get; set; }
        int Index { get; set; }
        int KeyNo { get; set; }
        int Link { get; set; }
        int RecordNo { get; set; }
        string Secure { get; set; }
        string TimeCreated { get; set; }
        short UserNo { get; set; }
    }
}