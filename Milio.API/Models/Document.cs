namespace Milio.API.Models
{
    public class Document 
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicID { get; set; }
        public virtual Carer Carer { get; set; }
        public int CarerId { get; set; }
    }
}