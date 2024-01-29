using FullStackAuth_WebAPI.Models;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class RoutineWithExercisesDto
    {
        public Routine Routine { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}
