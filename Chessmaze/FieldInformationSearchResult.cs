using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessmaze
{
    public class FieldInformationSearchResult
    {
        public FieldMap AssociatedMap { get; }
        public FieldInformation AssociatedField { get; }
        public Point Position { get; }

        public FieldInformationSearchResult(FieldMap associatedMap, FieldInformation associatedField, Point position)
        {
            AssociatedMap = associatedMap ?? throw new ArgumentNullException(nameof(associatedMap));
            AssociatedField = associatedField ?? throw new ArgumentNullException(nameof(associatedField));
            Position = position;
        }
    }
}
