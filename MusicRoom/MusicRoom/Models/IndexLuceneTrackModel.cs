using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicRoom.Models
{
    public class IndexLuceneTrackModel
    {
        public int Limit { get; set; }
        public bool SearchDefault { get; set; }
        public TrackData TrackData { get; set; }
        public IEnumerable<TrackData> AllTrackData { get; set; }
        public IEnumerable<TrackData> SearchIndexData { get; set; }
        public IList<SelectedList> SearchFieldList { get; set; }
        public string SearchTerm { get; set; }
        public string SearchField { get; set; }
    }
}