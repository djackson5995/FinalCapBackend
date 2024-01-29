namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class ExerciseDto 
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Weight { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
    }
}
