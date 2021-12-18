using System.Collections.Generic;
using System.Drawing;

namespace Chessmaze
{
    public class Edge
    {
        public FieldInformationSearchResult From { get; set; }

        public FieldInformationSearchResult To { get; set; }

        public Point CoordFrom { get; set; }

        public Point CoordTo { get; set; }

        public int Cost { get; set; }

        public IList<FieldInformationSearchResult> VisitedFields { get; set; }
    }
}
