namespace Commander.Dtos
{
    public class CommandCreateDto
    {
        //we dont add Id as it will be created by an ID generator
        public string HowTo { get; set; }
        public string Line { get; set; }
        public string Platform { get; set; }
    }
}