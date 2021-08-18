namespace Commander.Dtos
{
    public class CommandReadDto
    {
        public int Id { get; set; }
        public string HowTo { get; set; }
        public string Line { get; set; }

        //we take out platform so that e dont expose that detail to out client
    }
}