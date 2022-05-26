namespace CompetitionManager.Web.Controllers.TaskResults.Dto
{
    public class CreateUpdateTaskResultDto
    {
        public int TaskResultId { get; set; }
        public int CompetitionId { get; set; }
        public int TeamId { get; set; }
        public int TaskId { get; set; }
        public double ElapsedMinutes { get; set; }
        public double Result { get; set; }
    }
}
