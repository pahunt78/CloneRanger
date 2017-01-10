namespace CloneRanger.Examples
{
    public class NoParameterlessConstructor
    {
        public NoParameterlessConstructor(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}