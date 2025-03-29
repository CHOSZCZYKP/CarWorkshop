namespace CarWorkshop.Models
{
    public class Notification
    {
        public string Type { get; set; }
        public string Message { get; set; }

        public Notification(string type, string message)
        {
            this.Type = type;
            this.Message = message;
        }
    }
}
