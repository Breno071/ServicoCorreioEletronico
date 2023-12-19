namespace ClientApp.Models
{
    public class Pet
    {
        public string sex { get; set; } = string.Empty;
        public string age { get; set; } = string.Empty;
        public string size { get; set; } = string.Empty;
        public int order { get; set; }
        public string pet_id { get; set; } = string.Empty;
        public string pet_name { get; set; } = string.Empty;
        public string primary_breed { get; set; } = string.Empty;
        public string secondary_breed { get; set; } = string.Empty;
        public string addr_city { get; set; } = string.Empty;
        public string addr_state_code { get; set; } = string.Empty;
        public string details_url { get; set; } = string.Empty;
        public string results_photo_url { get; set; } = string.Empty;
        public int results_photo_width { get; set; }
        public int results_photo_height { get; set; }
        public string large_results_photo_url { get; set; } = string.Empty;
        public int large_results_photo_width { get; set; }
        public int large_results_photo_height { get; set; }
    }

    public class Request
    {
        public string status { get; set; } = string.Empty;
        public List<Pet> pets { get; set; } = new();
    }
}
