using System.Collections.Generic;

namespace SportSections.ViewModels
{
    public class QueryViewModel
    {
        public string SqlQuery { get; set; }

        public bool QueryIsSuccess { get; set; } = true;

        public string[] ColumnNames { get; set; }

        public List<string[]> InfoSets { get; set; }
    }
}
