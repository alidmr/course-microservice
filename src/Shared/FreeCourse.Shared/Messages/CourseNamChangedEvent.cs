
namespace FreeCourse.Shared.Messages
{
    public class CourseNamChangedEvent
    {
        public string CourseId { get; set; }
        public string UpdatedName { get; set; }
    }
}
