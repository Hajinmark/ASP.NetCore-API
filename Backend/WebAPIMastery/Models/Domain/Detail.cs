namespace WebAPIMastery.Models.Domain
{
    public class Detail
    {
        public Guid Id { get; set; }
        public string Address { get; set; } 
        public string City { get; set; }
        public Guid PersonId { get; set; }  

        //Navigation Property
        internal Person Person { get; set; }  
    }
}
