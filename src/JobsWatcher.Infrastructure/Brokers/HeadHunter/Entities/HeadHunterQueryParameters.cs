using System;
using Refit;

namespace JobsWatcher.Infrastructure.Brokers.HeadHunter.Entities
{
    public class HeadHunterQueryParameters
    {
        [AliasAs("specialization")]
        public int? SpecializationId { get; set; }

        [AliasAs("schedule")]
        [Query(CollectionFormat.Multi)]
        public string[] Schedules { get; set; }
        
        [AliasAs("area")]
        [Query(CollectionFormat.Multi)]
        public string[] Areas { get; set; }

        [AliasAs("text")]
        public string SearchText { get; set; }

        [AliasAs("per_page")]
        public int? ItemsPerPage { get; set; }

        [AliasAs("page")]
        public int? PageNumber { get; set; }
        [AliasAs("date_from")]
        public string DateFrom { get; set; }
        [AliasAs("date_to")]
        public string DateTo { get; set; }
    }
}