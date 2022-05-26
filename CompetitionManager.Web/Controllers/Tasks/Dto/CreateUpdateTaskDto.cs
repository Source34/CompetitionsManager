using System.Collections.Generic;

namespace CompetitionManager.Web.Controllers.Tasks.Dto
{
    public class CreateUpdateTaskDto
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Solution { get; set; }
        public string InputExample { get; set; }
        public string OutputExample { get; set; }
        public double Points { get; set; }
        public virtual List<int> Competitions { get; set; } = new();
        public virtual List<int> TaskResults { get; set; } = new();
    }
}
